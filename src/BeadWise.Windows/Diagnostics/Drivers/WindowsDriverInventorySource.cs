using System.Security.Cryptography;
using System.Text;
using BeadWise.Core.Contracts;
using BeadWise.Core.Diagnostics;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Results;

namespace BeadWise.Windows.Diagnostics;

public sealed class WindowsDriverInventorySource : IDriverInventorySource
{
    private const string DeviceSource = "SetupAPI:present devices";
    private const string StatusSource = "CfgMgr32:CM_Get_DevNode_Status";
    private readonly IWmiQuery _query;
    private readonly Func<DeviceEnumeration> _readDevices;

    public WindowsDriverInventorySource() : this(new WmiQuery(), PresentDeviceReader.Read) { }

    internal WindowsDriverInventorySource(IWmiQuery query, Func<DeviceEnumeration> readDevices)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(readDevices);
        _query = query;
        _readDevices = readDevices;
    }

    public Result<IReadOnlyList<DriverObservation>> Observe()
    {
        DeviceEnumeration devices;
        try
        {
            devices = _readDevices();
        }
        catch (Exception exception) when (InventoryRead.IsExpectedFailure(exception))
        {
            var error = InventoryRead.Error("DEVICE_ENUMERATION", exception);
            return error.Category == ErrorCategory.Unsupported
                ? Result<IReadOnlyList<DriverObservation>>.Unsupported(error)
                : Result<IReadOnlyList<DriverObservation>>.Failed(error);
        }

        var deviceTimestamp = DateTimeOffset.UtcNow;
        var metadata = WmiRead.Capture(_query, "Win32_PnPSignedDriver", "DeviceID", "DeviceName",
            "DeviceClass", "DriverProviderName", "DriverVersion", "IsSigned");
        var byDevice = metadata.Rows
            .Where(row => row.GetValueOrDefault("DeviceID") is string id && !string.IsNullOrWhiteSpace(id))
            .GroupBy(row => ((string)row["DeviceID"]!).Trim(), StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.ToArray(), StringComparer.OrdinalIgnoreCase);

        var observations = devices.Devices.Select(device => ReadDevice(device, deviceTimestamp, metadata, byDevice)).ToArray();
        IReadOnlyList<DriverObservation> result = Array.AsReadOnly(observations);
        var complete = devices.Error is null && metadata.Error is null && observations.All(IsComplete);
        return complete
            ? Result<IReadOnlyList<DriverObservation>>.Success(result)
            : Result<IReadOnlyList<DriverObservation>>.Partial(result,
                devices.Error ?? InventoryRead.PartialError("DRIVER_INVENTORY_PARTIAL"));
    }

    private static DriverObservation ReadDevice(NativeDevice device, DateTimeOffset timestamp, WmiRead metadata,
        IReadOnlyDictionary<string, IReadOnlyDictionary<string, object?>[]> byDevice)
    {
        var id = string.IsNullOrWhiteSpace(device.InstanceId) ? null : device.InstanceId.Trim();
        IReadOnlyDictionary<string, object?>? row = null;
        var metadataError = metadata.Error;
        if (id is not null && byDevice.TryGetValue(id, out var candidates))
        {
            if (candidates.Length == 1)
            {
                row = candidates[0];
            }
            else
            {
                metadataError = new Error("DRIVER_METADATA_AMBIGUOUS", ErrorCategory.PartialFailure, recoverable: true);
            }
        }

        var identity = id is null
            ? InventoryRead.Missing<string>(DeviceSource, timestamp, device.IdentityError)
            : Observation<string>.Available(Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(id.ToUpperInvariant()))), DeviceSource, timestamp);
        var name = ReadDeviceText(device.Name, device.NameError, row?.GetValueOrDefault("DeviceName"), metadata, metadataError, timestamp);
        var className = ReadDeviceText(device.ClassName, device.ClassError, row?.GetValueOrDefault("DeviceClass"), metadata, metadataError, timestamp);
        return new DriverObservation(identity, name, className,
            InventoryRead.Text(row?.GetValueOrDefault("DriverProviderName"), metadata.Source, metadata.Timestamp, metadataError),
            InventoryRead.Text(row?.GetValueOrDefault("DriverVersion"), metadata.Source, metadata.Timestamp, metadataError),
            InventoryRead.Boolean(row?.GetValueOrDefault("IsSigned"), metadata.Source, metadata.Timestamp, metadataError),
            ReadStatus(device.ProblemCode, device.StatusError, timestamp),
            ReadStatus(device.DeviceStatus, device.StatusError, timestamp));
    }

    private static Observation<string> ReadDeviceText(string? nativeValue, Error? nativeError, object? wmiValue,
        WmiRead metadata, Error? metadataError, DateTimeOffset timestamp)
    {
        if (!string.IsNullOrWhiteSpace(nativeValue))
        {
            return InventoryRead.Text(nativeValue, DeviceSource, timestamp);
        }

        if (wmiValue is string text && !string.IsNullOrWhiteSpace(text))
        {
            return InventoryRead.Text(text, metadata.Source, metadata.Timestamp);
        }

        return nativeError is not null
            ? InventoryRead.Missing<string>(DeviceSource, timestamp, nativeError)
            : InventoryRead.Missing<string>(metadata.Source, metadata.Timestamp, metadataError);
    }

    private static Observation<uint> ReadStatus(uint? value, Error? error, DateTimeOffset timestamp) =>
        value.HasValue
            ? Observation<uint>.Available(value.Value, StatusSource, timestamp)
            : InventoryRead.Missing<uint>(StatusSource, timestamp, error);

    private static bool IsComplete(DriverObservation item) =>
        InventoryRead.Available(item.Identity) && InventoryRead.Available(item.Name)
        && InventoryRead.Available(item.ClassName) && InventoryRead.Available(item.Provider)
        && InventoryRead.Available(item.Version) && InventoryRead.Available(item.Signed)
        && InventoryRead.Available(item.ProblemCode) && InventoryRead.Available(item.DeviceStatus);
}

internal sealed record NativeDevice(string? InstanceId, string? Name, string? ClassName,
    uint? ProblemCode, uint? DeviceStatus, Error? IdentityError = null, Error? NameError = null,
    Error? ClassError = null, Error? StatusError = null);

internal sealed record DeviceEnumeration(IReadOnlyList<NativeDevice> Devices, Error? Error);
