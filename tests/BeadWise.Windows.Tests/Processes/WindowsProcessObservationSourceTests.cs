using System.ComponentModel;
using System.Diagnostics;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Results;
using BeadWise.Windows.Processes;

namespace BeadWise.Windows.Tests.Processes;

public sealed class WindowsProcessObservationSourceTests
{
    [Fact]
    public void CompleteCollectionReturnsSuccess()
    {
        var creationTime = new DateTimeOffset(2026, 9, 9, 12, 0, 0, TimeSpan.FromHours(3));
        using var complete = new ProcessReadTarget(
            70,
            () => "complete",
            () => creationTime,
            () => "C:\\complete.exe");
        var source = new WindowsProcessObservationSource(() => [complete]);

        var result = source.Observe();

        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.Null(result.Error);
        var observed = Assert.Single(result.Value!);
        Assert.Equal(creationTime.ToUniversalTime(), observed.Identity.Value.CreationTime);
        Assert.Equal(TimeSpan.Zero, observed.Identity.Value.CreationTime.Offset);
        Assert.Equal(TimeSpan.Zero, observed.Identity.Timestamp.Offset);
    }

    [Fact]
    public void ObserveEnumeratesRealProcessesWithoutGlobalException()
    {
        var source = new WindowsProcessObservationSource();

        var result = source.Observe();

        Assert.True(result.Status is ResultStatus.Success or ResultStatus.Partial);
        Assert.NotNull(result.Value);
        Assert.NotEmpty(result.Value);
    }

    [Fact]
    public void ObserveIncludesCurrentProcessWithStableIdentityAndName()
    {
        using var currentProcess = Process.GetCurrentProcess();
        var expectedCreationTime = new DateTimeOffset(currentProcess.StartTime.ToUniversalTime());
        var expectedName = currentProcess.ProcessName;
        var source = new WindowsProcessObservationSource();

        var result = source.Observe();

        var observed = Assert.Single(result.Value!, process => process.ProcessId == currentProcess.Id);
        Assert.Equal(ObservationAvailability.Available, observed.Identity.Availability);
        Assert.Equal(currentProcess.Id, observed.Identity.Value.ProcessId);
        Assert.Equal(expectedCreationTime, observed.Identity.Value.CreationTime);
        Assert.Equal(ObservationAvailability.Available, observed.ProcessName.Availability);
        Assert.Equal(expectedName, observed.ProcessName.Value);
    }

    [Fact]
    public void ObserveReportsCurrentExecutablePathOrExplicitAbsence()
    {
        using var currentProcess = Process.GetCurrentProcess();
        var expectedPath = currentProcess.MainModule?.FileName;
        var source = new WindowsProcessObservationSource();

        var result = source.Observe();

        var observed = Assert.Single(result.Value!, process => process.ProcessId == currentProcess.Id);
        if (expectedPath is not null)
        {
            Assert.True(observed.ExecutablePath.HasValue);
            Assert.Equal(ObservationAvailability.Available, observed.ExecutablePath.Availability);
            Assert.Equal(expectedPath, observed.ExecutablePath.Value);
        }
        else
        {
            Assert.True(observed.ExecutablePath.Availability is
                ObservationAvailability.Partial or ObservationAvailability.Unsupported);
            Assert.NotNull(observed.ExecutablePath.Error);
        }
    }

    [Fact]
    public void FieldAccessFailurePreservesOtherProcessesAndFieldStates()
    {
        var timestamp = new DateTimeOffset(2026, 9, 9, 12, 0, 0, TimeSpan.Zero);
        using var complete = new ProcessReadTarget(
            71,
            () => "complete",
            () => timestamp,
            () => "C:\\complete.exe");
        using var restricted = new ProcessReadTarget(
            72,
            () => "restricted",
            () => throw new Win32Exception(5),
            () => throw new Win32Exception(5));
        var source = new WindowsProcessObservationSource(() => [complete, restricted]);

        var result = source.Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal(ErrorCategory.PartialFailure, result.Error?.Category);
        Assert.Equal(2, result.Value?.Count);

        var completeObservation = Assert.Single(result.Value!, process => process.ProcessId == 71);
        Assert.Equal(ObservationAvailability.Available, completeObservation.Identity.Availability);
        Assert.Equal(ObservationAvailability.Available, completeObservation.ExecutablePath.Availability);

        var restrictedObservation = Assert.Single(result.Value!, process => process.ProcessId == 72);
        Assert.Equal("restricted", restrictedObservation.ProcessName.Value);
        Assert.Equal(
            WindowsProcessObservationSource.ObservationSource,
            restrictedObservation.ProcessName.Source);
        Assert.False(restrictedObservation.Identity.HasValue);
        Assert.Equal(ObservationAvailability.Partial, restrictedObservation.Identity.Availability);
        Assert.Equal(ErrorCategory.AccessDenied, restrictedObservation.Identity.Error?.Category);
        Assert.Equal("PROCESS_CREATION_TIME_ACCESS_DENIED", restrictedObservation.Identity.Error?.Code);
        Assert.Equal(
            WindowsProcessObservationSource.ObservationSource,
            restrictedObservation.Identity.Source);
        Assert.False(restrictedObservation.ExecutablePath.HasValue);
        Assert.Equal(ObservationAvailability.Partial, restrictedObservation.ExecutablePath.Availability);
        Assert.Equal(ErrorCategory.AccessDenied, restrictedObservation.ExecutablePath.Error?.Category);
        Assert.Equal(
            "PROCESS_EXECUTABLE_PATH_ACCESS_DENIED",
            restrictedObservation.ExecutablePath.Error?.Code);
        Assert.Equal(
            WindowsProcessObservationSource.ObservationSource,
            restrictedObservation.ExecutablePath.Source);
    }

    [Fact]
    public void ExitedProcessFieldIsUnavailableWithoutFabricatedValue()
    {
        using var exited = new ProcessReadTarget(
            73,
            () => throw new InvalidOperationException("Process has exited."),
            () => throw new InvalidOperationException("Process has exited."),
            () => throw new InvalidOperationException("Process has exited."));
        var source = new WindowsProcessObservationSource(() => [exited]);

        var result = source.Observe();

        var observed = Assert.Single(result.Value!);
        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.False(observed.Identity.HasValue);
        Assert.Equal(ErrorCategory.Unavailable, observed.Identity.Error?.Category);
        Assert.False(observed.ProcessName.HasValue);
        Assert.Equal(ErrorCategory.Unavailable, observed.ProcessName.Error?.Category);
        Assert.False(observed.ExecutablePath.HasValue);
        Assert.Equal(ErrorCategory.Unavailable, observed.ExecutablePath.Error?.Category);
    }

    [Fact]
    public void EnumerationFailureReturnsStructuredFailure()
    {
        var source = new WindowsProcessObservationSource(
            () => throw new Win32Exception(5));

        var result = source.Observe();

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Null(result.Value);
        Assert.Equal(ErrorCategory.AccessDenied, result.Error?.Category);
        Assert.Equal("PROCESS_ENUMERATION_ACCESS_DENIED", result.Error?.Code);
    }
}
