using BeadWise.Core.Diagnostics;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Results;
using BeadWise.Windows.Diagnostics;

namespace BeadWise.Windows.Tests.Diagnostics;

public sealed class WindowsDisplayInventorySourceTests
{
    private const uint CompleteFields = 0x005C0000;

    [Fact]
    public void UsesMonitorNamesAndHashesIdentityWhileDeduplicatingModes()
    {
        var mode = new DisplayNativeMode(CompleteFields, 1920, 1080, 60, 32);
        var snapshot = new DisplayNativeSnapshot(
            [new("Graphics adapter", [new("monitor-id", "Monitor A"), new("monitor-id-2", "Monitor B")],
                Ok(mode), Ok<IReadOnlyList<DisplayNativeMode>>([mode, mode]))], []);

        var result = new WindowsDisplayInventorySource(() => snapshot).Observe();

        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.Equal(2, result.Value!.Count);
        var monitor = result.Value[0];
        Assert.Equal("Graphics adapter", monitor.AdapterName.Value);
        Assert.Equal("Monitor A", monitor.Name.Value);
        Assert.Matches("^[0-9A-F]{64}$", monitor.Identity.Value);
        Assert.NotEqual("monitor-id", monitor.Identity.Value);
        Assert.NotEqual(monitor.Identity.Value, result.Value[1].Identity.Value);
        Assert.Equal(new DisplayMode(1920, 1080, 60, 32), monitor.CurrentMode.Value);
        Assert.Equal(new DisplayMode(1920, 1080, 60, 32), Assert.Single(monitor.SupportedModes.Value));
    }

    [Fact]
    public void MissingFieldsAndDefaultRefreshRateRemainPartial()
    {
        var valid = new DisplayNativeMode(CompleteFields, 1920, 1080, 60, 32);
        var missingFrequency = new DisplayNativeMode(0x001C0000, 1920, 1080, 144, 32);
        var defaultFrequency = new DisplayNativeMode(CompleteFields, 1280, 720, 1, 32);
        var snapshot = new DisplayNativeSnapshot(
            [new("GPU", [new("id", "Monitor")], Ok(missingFrequency),
                Ok<IReadOnlyList<DisplayNativeMode>>([valid, missingFrequency, defaultFrequency]))], []);

        var result = new WindowsDisplayInventorySource(() => snapshot).Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        var monitor = Assert.Single(result.Value!);
        Assert.False(monitor.CurrentMode.HasValue);
        Assert.Equal(ObservationAvailability.Partial, monitor.CurrentMode.Availability);
        Assert.Equal(ObservationAvailability.Partial, monitor.SupportedModes.Availability);
        Assert.Equal(new DisplayMode(1920, 1080, 60, 32), Assert.Single(monitor.SupportedModes.Value));
    }

    [Fact]
    public void MixedInventoryPreservesAHeadlessAdapterAlongsideMonitors()
    {
        var mode = new DisplayNativeMode(CompleteFields, 1920, 1080, 60, 32);
        var snapshot = new DisplayNativeSnapshot(
            [new("GPU with monitor", [new("monitor-id", "Monitor")], Ok(mode), Ok<IReadOnlyList<DisplayNativeMode>>([mode])),
             new("Headless GPU", [], Ok(mode), Ok<IReadOnlyList<DisplayNativeMode>>([mode]), "adapter-id")], []);

        var result = new WindowsDisplayInventorySource(() => snapshot).Observe();

        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.Equal(2, result.Value!.Count);
        var monitor = Assert.Single(result.Value, item => item.Kind == DisplayObservationKind.Monitor);
        Assert.Equal("GPU with monitor", monitor.AdapterName.Value);
        Assert.Equal("Monitor", monitor.Name.Value);
        var adapter = Assert.Single(result.Value, item => item.Kind == DisplayObservationKind.Adapter);
        Assert.Equal("Headless GPU", adapter.AdapterName.Value);
        Assert.Equal(ObservationAvailability.Unknown, adapter.Name.Availability);
        Assert.Matches("^[0-9A-F]{64}$", adapter.Identity.Value);
        Assert.NotEqual("adapter-id", adapter.Identity.Value);
        Assert.Equal(WindowsDisplayInventorySource.AdapterIdentitySource, adapter.Identity.Source);
        Assert.Equal(new DisplayMode(1920, 1080, 60, 32), adapter.CurrentMode.Value);
        Assert.Equal(WindowsDisplayInventorySource.ModeObservationSource, adapter.CurrentMode.Source);
    }

    [Fact]
    public void UnavailableMonitorPropertiesDoNotInventAnAdapterAsMonitor()
    {
        var unavailable = new Error("DISPLAY_MODE_UNAVAILABLE", ErrorCategory.Unavailable, recoverable: true);
        var snapshot = new DisplayNativeSnapshot(
            [new("GPU", [new("", "")], Result<DisplayNativeMode>.Failed(unavailable),
                Result<IReadOnlyList<DisplayNativeMode>>.Failed(unavailable)),
             new("Headless adapter", [], Result<DisplayNativeMode>.Failed(unavailable),
                Result<IReadOnlyList<DisplayNativeMode>>.Failed(unavailable))], []);

        var result = new WindowsDisplayInventorySource(() => snapshot).Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        var monitor = Assert.Single(result.Value!, item => item.Kind == DisplayObservationKind.Monitor);
        Assert.False(monitor.Identity.HasValue);
        Assert.False(monitor.Name.HasValue);
        Assert.False(monitor.CurrentMode.HasValue);
        Assert.False(monitor.SupportedModes.HasValue);
        var adapter = Assert.Single(result.Value!, item => item.Kind == DisplayObservationKind.Adapter);
        Assert.Equal("Headless adapter", adapter.AdapterName.Value);
        Assert.Equal(ObservationAvailability.Unknown, adapter.Identity.Availability);
        Assert.Equal(ObservationAvailability.Unknown, adapter.Name.Availability);
        Assert.False(adapter.CurrentMode.HasValue);
    }

    [Fact]
    public void ReaderAccessFailureIsADataFailure()
    {
        var result = new WindowsDisplayInventorySource(() => throw new UnauthorizedAccessException()).Observe();
        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal(ErrorCategory.AccessDenied, result.Error?.Category);
    }

    [Fact]
    public void InactiveMonitorDoesNotClaimAdapterModesAsItsOwn()
    {
        var mode = new DisplayNativeMode(CompleteFields, 1920, 1080, 60, 32);
        var snapshot = new DisplayNativeSnapshot(
            [new("GPU", [new("id", "Inactive", false)], Ok(mode), Ok<IReadOnlyList<DisplayNativeMode>>([mode]))], []);

        var result = new WindowsDisplayInventorySource(() => snapshot).Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        var monitor = Assert.Single(result.Value!);
        Assert.False(monitor.CurrentMode.HasValue);
        Assert.False(monitor.SupportedModes.HasValue);
    }

    [Fact]
    public void MissingMonitorsRemainExplicitlyPartial()
    {
        var result = new WindowsDisplayInventorySource(() => new DisplayNativeSnapshot([], [])).Observe();
        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Empty(result.Value!);
        Assert.Equal(ErrorCategory.PartialFailure, result.Error?.Category);
    }
    private static Result<T> Ok<T>(T value) where T : notnull => Result<T>.Success(value);
}
