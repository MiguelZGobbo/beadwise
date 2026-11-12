using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using BeadWise.Core.Contracts;
using BeadWise.Core.Diagnostics;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Results;

namespace BeadWise.Windows.Diagnostics;

public sealed class WindowsDisplayInventorySource : IDisplayInventorySource
{
    public const string ObservationSource = "Windows.GDI.EnumDisplayDevices";
    public const string ModeObservationSource = "Windows.GDI.EnumDisplaySettings(adapter)";
    public const string AdapterIdentitySource = "Windows.GDI.EnumDisplayDevices.DeviceKey";
    private readonly Func<DisplayNativeSnapshot> _read;

    public WindowsDisplayInventorySource() : this(DisplayNativeReader.Read) { }
    internal WindowsDisplayInventorySource(Func<DisplayNativeSnapshot> read) =>
        _read = read ?? throw new ArgumentNullException(nameof(read));

    public Result<IReadOnlyList<DisplayObservation>> Observe()
    {
        DisplayNativeSnapshot snapshot;
        try { snapshot = _read(); }
        catch (Exception exception) when (DisplayNativeReader.IsExpectedFailure(exception))
        {
            var error = DisplayNativeReader.ErrorFromException(exception);
            return error.Category == ErrorCategory.Unsupported
                ? Result<IReadOnlyList<DisplayObservation>>.Unsupported(error)
                : Result<IReadOnlyList<DisplayObservation>>.Failed(error);
        }

        var timestamp = DateTimeOffset.UtcNow;
        var partial = snapshot.Errors.Count != 0;
        var observations = new List<DisplayObservation>();
        foreach (var adapter in snapshot.Adapters)
        {
            if (adapter.Monitors.Count == 0)
            {
                var identity = string.IsNullOrWhiteSpace(adapter.Identity)
                    ? Observation<string>.Unknown(AdapterIdentitySource, timestamp)
                    : ReadString(adapter.Identity, timestamp, "DISPLAY_ADAPTER_IDENTITY", hash: true, AdapterIdentitySource);
                var adapterName = ReadString(adapter.Name, timestamp, "DISPLAY_ADAPTER_NAME", hash: false);
                var current = ReadCurrent(adapter.CurrentMode, timestamp);
                var supported = ReadSupported(adapter.SupportedModes, timestamp);
                partial |= identity.Availability != ObservationAvailability.Available
                    || adapterName.Availability != ObservationAvailability.Available
                    || current.Availability != ObservationAvailability.Available
                    || supported.Availability != ObservationAvailability.Available;
                observations.Add(new DisplayObservation(identity, adapterName,
                    Observation<string>.Unknown(ObservationSource, timestamp), current, supported,
                    DisplayObservationKind.Adapter));
            }
            foreach (var monitor in adapter.Monitors)
            {
                var identity = ReadString(monitor.Identity, timestamp, "DISPLAY_IDENTITY", hash: true);
                var adapterName = ReadString(adapter.Name, timestamp, "DISPLAY_ADAPTER_NAME", hash: false);
                var name = ReadString(monitor.Name, timestamp, "DISPLAY_MONITOR_NAME", hash: false);
                var current = monitor.Active
                    ? ReadCurrent(adapter.CurrentMode, timestamp)
                    : Unavailable<DisplayMode>("DISPLAY_MONITOR_INACTIVE", timestamp, ModeObservationSource);
                var supported = monitor.Active
                    ? ReadSupported(adapter.SupportedModes, timestamp)
                    : Unavailable<IReadOnlyList<DisplayMode>>("DISPLAY_MONITOR_INACTIVE", timestamp, ModeObservationSource);
                partial |= identity.Availability != ObservationAvailability.Available
                    || adapterName.Availability != ObservationAvailability.Available
                    || name.Availability != ObservationAvailability.Available
                    || current.Availability != ObservationAvailability.Available
                    || supported.Availability != ObservationAvailability.Available;
                observations.Add(new DisplayObservation(identity, adapterName, name, current, supported));
            }
        }

        IReadOnlyList<DisplayObservation> value = observations.AsReadOnly();
        if (value.Count == 0) partial = true;
        return partial
            ? Result<IReadOnlyList<DisplayObservation>>.Partial(value,
                snapshot.Errors.FirstOrDefault() ?? new Error("DISPLAY_INVENTORY_PARTIAL", ErrorCategory.PartialFailure,
                    value.Count == 0 ? "No monitor entries were available in the current session." :
                        "One or more display fields or enumerations were unavailable.", recoverable: true),
                snapshot.Errors.Select(error => error.Code).Distinct(StringComparer.Ordinal))
            : Result<IReadOnlyList<DisplayObservation>>.Success(value);
    }

    private static Observation<string> ReadString(string value, DateTimeOffset timestamp, string code, bool hash,
        string source = ObservationSource)
    {
        if (string.IsNullOrWhiteSpace(value)) return Unavailable<string>($"{code}_UNAVAILABLE", timestamp, source);
        var result = hash ? Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value))) : value;
        return Observation<string>.Available(result, source, timestamp);
    }

    private static Observation<DisplayMode> ReadCurrent(Result<DisplayNativeMode> result, DateTimeOffset timestamp)
    {
        if (result.Status == ResultStatus.Success && TryMode(result.Value, out var mode))
            return Observation<DisplayMode>.Available(mode, ModeObservationSource, timestamp);
        return Observation<DisplayMode>.PartialWithoutValue(ModeObservationSource, timestamp,
            result.Error ?? new Error("DISPLAY_CURRENT_MODE_UNAVAILABLE", ErrorCategory.Unavailable, recoverable: true));
    }

    private static Observation<IReadOnlyList<DisplayMode>> ReadSupported(
        Result<IReadOnlyList<DisplayNativeMode>> result, DateTimeOffset timestamp)
    {
        if (result.Status != ResultStatus.Success || result.Value is null)
            return Observation<IReadOnlyList<DisplayMode>>.PartialWithoutValue(ModeObservationSource, timestamp,
                result.Error ?? new Error("DISPLAY_SUPPORTED_MODES_UNAVAILABLE", ErrorCategory.Unavailable, recoverable: true));
        var modes = new List<DisplayMode>();
        var invalid = false;
        foreach (var native in result.Value)
        {
            if (!TryMode(native, out var mode)) { invalid = true; continue; }
            if (!modes.Contains(mode)) modes.Add(mode);
        }
        IReadOnlyList<DisplayMode> value = modes.AsReadOnly();
        return invalid
            ? Observation<IReadOnlyList<DisplayMode>>.Partial(value, ModeObservationSource, timestamp,
                new Error("DISPLAY_MODE_FIELDS_UNAVAILABLE", ErrorCategory.Unavailable, recoverable: true))
            : Observation<IReadOnlyList<DisplayMode>>.Available(value, ModeObservationSource, timestamp);
    }

    private static bool TryMode(DisplayNativeMode native, out DisplayMode mode)
    {
        const uint requiredFields = 0x005c0000;
        if ((native.Fields & requiredFields) != requiredFields || native.Width == 0 || native.Height == 0
            || native.Width > int.MaxValue || native.Height > int.MaxValue || native.BitsPerPixel == 0
            || native.Frequency <= 1)
        {
            mode = null!;
            return false;
        }
        mode = new DisplayMode((int)native.Width, (int)native.Height, native.Frequency, native.BitsPerPixel);
        return true;
    }

    private static Observation<T> Unavailable<T>(string code, DateTimeOffset timestamp, string source) =>
        Observation<T>.PartialWithoutValue(source, timestamp,
            new Error(code, ErrorCategory.Unavailable, recoverable: true));
}

internal readonly record struct DisplayNativeMode(uint Fields, uint Width, uint Height, uint Frequency, uint BitsPerPixel);
internal sealed record DisplayNativeMonitor(string Identity, string Name, bool Active = true);
internal sealed record DisplayNativeAdapter(string Name, IReadOnlyList<DisplayNativeMonitor> Monitors,
    Result<DisplayNativeMode> CurrentMode, Result<IReadOnlyList<DisplayNativeMode>> SupportedModes,
    string? Identity = null);
internal sealed record DisplayNativeSnapshot(IReadOnlyList<DisplayNativeAdapter> Adapters, IReadOnlyList<Error> Errors);
