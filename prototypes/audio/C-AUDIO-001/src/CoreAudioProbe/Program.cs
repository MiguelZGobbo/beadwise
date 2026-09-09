using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

var enumerator = (IMMDeviceEnumerator)(object)new MMDeviceEnumeratorComObject();
var endpointRows = new List<EndpointRow>();
var defaults = new List<object>();
var captureDiagnostics = new List<object>();

foreach (var flow in new[] { DataFlow.Render, DataFlow.Capture })
{
    ThrowIfFailed(enumerator.EnumAudioEndpoints(flow, DeviceState.All, out var collection));
    ThrowIfFailed(collection.GetCount(out var count));
    for (uint index = 0; index < count; index++)
    {
        ThrowIfFailed(collection.Item(index, out var device));
        ThrowIfFailed(device.GetId(out var id));
        ThrowIfFailed(device.GetState(out var state));

        var friendlyName = "<PROPERTY_UNAVAILABLE>";
        var friendlyNameStatus = "UNAVAILABLE";
        if (device.OpenPropertyStore(0, out var propertyStore) >= 0)
        {
            var propertyKey = PropertyKey.DeviceFriendlyName;
            if (propertyStore.GetValue(ref propertyKey, out var value) >= 0)
            {
                try
                {
                    var text = value.GetString();
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        friendlyName = text;
                        friendlyNameStatus = "PASS";
                    }
                }
                finally
                {
                    value.Clear();
                }
            }
        }

        endpointRows.Add(new EndpointRow(
            HashIdentity(id),
            friendlyNameStatus,
            flow.ToString().ToUpperInvariant(),
            (uint)state,
            ExpandStates(state),
            "UNKNOWN_NOT_QUERIED"));

        if (flow == DataFlow.Capture)
        {
            captureDiagnostics.Add(ReadCaptureVolumeDiagnostic(device, id));
        }
    }

    foreach (var role in new[] { Role.Console, Role.Multimedia, Role.Communications })
    {
        var hr = enumerator.GetDefaultAudioEndpoint(flow, role, out var defaultDevice);
        if (hr >= 0)
        {
            ThrowIfFailed(defaultDevice.GetId(out var defaultId));
            defaults.Add(new
            {
                flow = flow.ToString().ToUpperInvariant(),
                role = role.ToString().ToUpperInvariant(),
                endpointIdHash = HashIdentity(defaultId),
                status = "PASS"
            });
        }
        else
        {
            defaults.Add(new
            {
                flow = flow.ToString().ToUpperInvariant(),
                role = role.ToString().ToUpperInvariant(),
                endpointIdHash = (string?)null,
                status = "UNAVAILABLE",
                hresult = $"0x{hr:X8}"
            });
        }
    }
}

var result = new
{
    schemaVersion = 1,
    capturedAtUtc = DateTimeOffset.UtcNow,
    capabilities = new[] { "C-AUDIO-001", "C-AUDIO-003", "C-AUDIO-006", "C-AUDIO-007", "C-AUDIO-010" },
    api = new[]
    {
        "IMMDeviceEnumerator.EnumAudioEndpoints",
        "IMMDeviceEnumerator.GetDefaultAudioEndpoint",
        "IMMDevice.GetId/GetState",
        "IPropertyStore.GetValue(PKEY_Device_FriendlyName)",
        "IAudioEndpointVolume.GetMute/GetMasterVolumeLevelScalar"
    },
    mutation = "NONE",
    summary = new
    {
        endpointCount = endpointRows.Count,
        renderCount = endpointRows.Count(row => row.Flow == "RENDER"),
        captureCount = endpointRows.Count(row => row.Flow == "CAPTURE"),
        activeCount = endpointRows.Count(row => row.States.Contains("ACTIVE")),
        disabledCount = endpointRows.Count(row => row.States.Contains("DISABLED")),
        unpluggedCount = endpointRows.Count(row => row.States.Contains("UNPLUGGED"))
    },
    endpoints = endpointRows,
    defaultEndpoints = defaults,
    captureDiagnostics,
    rawEndpointMetadataPersisted = false,
    limitations = new[]
    {
        "No endpoint state, role, volume, mute, route, service, device, or privacy setting was changed.",
        "Transport and virtual-versus-physical classification require PnP/topology correlation and remain UNKNOWN_NOT_QUERIED.",
        "No capture stream was opened and no user audio was retained.",
        "Hotplug, permission-blocked, disabled, sleep/resume, and session fixtures remain untested unless naturally present."
    }
};

Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));

static void ThrowIfFailed(int hresult)
{
    if (hresult < 0)
    {
        Marshal.ThrowExceptionForHR(hresult);
    }
}

static string[] ExpandStates(DeviceState state)
{
    var states = new List<string>();
    if ((state & DeviceState.Active) != 0) states.Add("ACTIVE");
    if ((state & DeviceState.Disabled) != 0) states.Add("DISABLED");
    if ((state & DeviceState.NotPresent) != 0) states.Add("NOT_PRESENT");
    if ((state & DeviceState.Unplugged) != 0) states.Add("UNPLUGGED");
    if (states.Count == 0) states.Add("UNKNOWN");
    return states.ToArray();
}

static string HashIdentity(string value) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)))[..32];

static object ReadCaptureVolumeDiagnostic(IMMDevice device, string id)
{
    var endpointVolumeId = new Guid("5CDF2C82-841E-4546-9722-0CF74078229A");
    const uint ClsctxInprocServer = 0x1;
    var activation = device.Activate(ref endpointVolumeId, ClsctxInprocServer, IntPtr.Zero, out var pointer);
    if (activation < 0 || pointer is not IAudioEndpointVolume endpointVolume)
    {
        return new
        {
            endpointIdHash = HashIdentity(id),
            activationStatus = $"0x{activation:X8}",
            muteQueryStatus = "NOT_RUN",
            volumeQueryStatus = "NOT_RUN",
            muted = (bool?)null,
            volumeBucket = (string?)null
        };
    }

    var muteStatus = endpointVolume.GetMute(out var muted);
    var volumeStatus = endpointVolume.GetMasterVolumeLevelScalar(out var scalar);
    return new
    {
        endpointIdHash = HashIdentity(id),
        activationStatus = "PASS",
        muteQueryStatus = muteStatus >= 0 ? "PASS" : $"0x{muteStatus:X8}",
        volumeQueryStatus = volumeStatus >= 0 ? "PASS" : $"0x{volumeStatus:X8}",
        muted = muteStatus >= 0 ? muted : (bool?)null,
        volumeBucket = volumeStatus >= 0 ? BucketVolume(scalar) : null
    };
}

static string BucketVolume(float scalar)
{
    if (scalar <= 0.001f) return "ZERO";
    if (scalar < 0.34f) return "LOW";
    if (scalar < 0.67f) return "MEDIUM";
    return "HIGH";
}

internal sealed record EndpointRow(
    string EndpointIdHash,
    string FriendlyNameStatus,
    string Flow,
    uint StateMask,
    string[] States,
    string Transport);

internal enum DataFlow
{
    Render,
    Capture,
    All,
    Count
}

internal enum Role
{
    Console,
    Multimedia,
    Communications,
    Count
}

[Flags]
internal enum DeviceState : uint
{
    Active = 0x00000001,
    Disabled = 0x00000002,
    NotPresent = 0x00000004,
    Unplugged = 0x00000008,
    All = Active | Disabled | NotPresent | Unplugged
}

[ComImport]
[Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
internal sealed class MMDeviceEnumeratorComObject
{
}

[ComImport]
[Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IMMDeviceEnumerator
{
    [PreserveSig]
    int EnumAudioEndpoints(DataFlow dataFlow, DeviceState stateMask, out IMMDeviceCollection devices);

    [PreserveSig]
    int GetDefaultAudioEndpoint(DataFlow dataFlow, Role role, out IMMDevice endpoint);

    [PreserveSig]
    int GetDevice([MarshalAs(UnmanagedType.LPWStr)] string id, out IMMDevice device);

    [PreserveSig]
    int RegisterEndpointNotificationCallback(IntPtr client);

    [PreserveSig]
    int UnregisterEndpointNotificationCallback(IntPtr client);
}

[ComImport]
[Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IMMDeviceCollection
{
    [PreserveSig]
    int GetCount(out uint count);

    [PreserveSig]
    int Item(uint index, out IMMDevice device);
}

[ComImport]
[Guid("D666063F-1587-4E43-81F1-B948E807363F")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IMMDevice
{
    [PreserveSig]
    int Activate(ref Guid interfaceId, uint classContext, IntPtr activationParameters, [MarshalAs(UnmanagedType.IUnknown)] out object interfacePointer);

    [PreserveSig]
    int OpenPropertyStore(uint accessMode, out IPropertyStore properties);

    [PreserveSig]
    int GetId([MarshalAs(UnmanagedType.LPWStr)] out string id);

    [PreserveSig]
    int GetState(out DeviceState state);
}

[ComImport]
[Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IPropertyStore
{
    [PreserveSig]
    int GetCount(out uint propertyCount);

    [PreserveSig]
    int GetAt(uint propertyIndex, out PropertyKey key);

    [PreserveSig]
    int GetValue(ref PropertyKey key, out PropVariant value);

    [PreserveSig]
    int SetValue(ref PropertyKey key, ref PropVariant value);

    [PreserveSig]
    int Commit();
}

[ComImport]
[Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IAudioEndpointVolume
{
    [PreserveSig]
    int RegisterControlChangeNotify(IntPtr notify);

    [PreserveSig]
    int UnregisterControlChangeNotify(IntPtr notify);

    [PreserveSig]
    int GetChannelCount(out uint channelCount);

    [PreserveSig]
    int SetMasterVolumeLevel(float levelDb, Guid eventContext);

    [PreserveSig]
    int SetMasterVolumeLevelScalar(float level, Guid eventContext);

    [PreserveSig]
    int GetMasterVolumeLevel(out float levelDb);

    [PreserveSig]
    int GetMasterVolumeLevelScalar(out float level);

    [PreserveSig]
    int SetChannelVolumeLevel(uint channelNumber, float levelDb, Guid eventContext);

    [PreserveSig]
    int SetChannelVolumeLevelScalar(uint channelNumber, float level, Guid eventContext);

    [PreserveSig]
    int GetChannelVolumeLevel(uint channelNumber, out float levelDb);

    [PreserveSig]
    int GetChannelVolumeLevelScalar(uint channelNumber, out float level);

    [PreserveSig]
    int SetMute([MarshalAs(UnmanagedType.Bool)] bool mute, Guid eventContext);

    [PreserveSig]
    int GetMute([MarshalAs(UnmanagedType.Bool)] out bool mute);
}

[StructLayout(LayoutKind.Sequential)]
internal struct PropertyKey
{
    internal Guid FormatId;
    internal uint PropertyId;

    internal static PropertyKey DeviceFriendlyName => new()
    {
        FormatId = new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),
        PropertyId = 14
    };
}

[StructLayout(LayoutKind.Explicit)]
internal struct PropVariant
{
    [FieldOffset(0)] private ushort valueType;
    [FieldOffset(8)] private IntPtr pointerValue;

    internal readonly string? GetString() => valueType == 31
        ? Marshal.PtrToStringUni(pointerValue)
        : null;

    internal void Clear() => NativeMethods.PropVariantClear(ref this);
}

internal static class NativeMethods
{
    [DllImport("ole32.dll")]
    internal static extern int PropVariantClear(ref PropVariant value);
}
