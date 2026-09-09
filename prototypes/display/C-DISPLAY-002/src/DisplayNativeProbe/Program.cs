using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Win32;

const uint QdcOnlyActivePaths = 0x00000002;
const uint DisplayDeviceAttachedToDesktop = 0x00000001;
const int EnumCurrentSettings = -1;
const uint CdsTest = 0x00000002;
const uint DmPelsWidth = 0x00080000;
const uint DmPelsHeight = 0x00100000;

var bufferResult = NativeMethods.GetDisplayConfigBufferSizes(
    QdcOnlyActivePaths,
    out var activePathCount,
    out var activeModeCount);

var displays = new List<object>();
for (uint adapterIndex = 0; ; adapterIndex++)
{
    var adapter = DisplayDevice.Create();
    if (!NativeMethods.EnumDisplayDevices(null, adapterIndex, ref adapter, 0))
    {
        break;
    }

    if ((adapter.StateFlags & DisplayDeviceAttachedToDesktop) == 0)
    {
        continue;
    }

    var current = DevMode.Create();
    if (!NativeMethods.EnumDisplaySettings(adapter.DeviceName, EnumCurrentSettings, ref current))
    {
        continue;
    }

    var currentValidation = NativeMethods.ChangeDisplaySettingsEx(
        adapter.DeviceName,
        ref current,
        IntPtr.Zero,
        CdsTest,
        IntPtr.Zero);

    var invalid = current;
    invalid.PelsWidth = 1;
    invalid.PelsHeight = 1;
    invalid.Fields = DmPelsWidth | DmPelsHeight;
    var invalidValidation = NativeMethods.ChangeDisplaySettingsEx(
        adapter.DeviceName,
        ref invalid,
        IntPtr.Zero,
        CdsTest,
        IntPtr.Zero);

    var modeCount = 0;
    for (var modeIndex = 0; ; modeIndex++)
    {
        var mode = DevMode.Create();
        if (!NativeMethods.EnumDisplaySettings(adapter.DeviceName, modeIndex, ref mode))
        {
            break;
        }
        modeCount++;
    }

    var after = DevMode.Create();
    var afterAvailable = NativeMethods.EnumDisplaySettings(adapter.DeviceName, EnumCurrentSettings, ref after);
    var stateUnchanged = afterAvailable &&
        after.PelsWidth == current.PelsWidth &&
        after.PelsHeight == current.PelsHeight &&
        after.DisplayFrequency == current.DisplayFrequency &&
        after.BitsPerPel == current.BitsPerPel;

    displays.Add(new
    {
        deviceName = adapter.DeviceName,
        description = adapter.DeviceString,
        deviceIdHash = HashIdentity(adapter.DeviceId),
        deviceIdPersisted = false,
        primary = (adapter.StateFlags & 0x00000004) != 0,
        currentMode = new
        {
            width = current.PelsWidth,
            height = current.PelsHeight,
            bitsPerPixel = current.BitsPerPel,
            frequencyHz = current.DisplayFrequency
        },
        enumeratedModeCount = modeCount,
        currentValidationCode = currentValidation,
        invalidValidationCode = invalidValidation,
        validationFlags = "CDS_TEST",
        stateUnchanged
    });
}

var result = new
{
    schemaVersion = 1,
    capturedAtUtc = DateTimeOffset.UtcNow,
    capabilities = new[] { "C-DISPLAY-001", "C-DISPLAY-002" },
    api = new[]
    {
        "GetDisplayConfigBufferSizes",
    "EnumDisplayDevices",
    "EnumDisplaySettings",
    "ChangeDisplaySettingsEx(CDS_TEST)",
    "HKLM SYSTEM CurrentControlSet Enum DISPLAY EDID read"
},
    mutation = "NONE: CDS_TEST requests validation without applying a display change.",
    ccdResultCode = bufferResult,
    activePathCount,
    activeModeCount,
    displays,
    edidSummary = ReadEdidSummary()
};

Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));

static string HashIdentity(string value)
{
    if (string.IsNullOrWhiteSpace(value)) { return "UNAVAILABLE"; }
    return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)))[..32];
}

static object ReadEdidSummary()
{
    var rows = new List<object>();
    using var displayKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Enum\DISPLAY");
    if (displayKey is null)
    {
        return new
        {
            queried = true,
            rawEdidPersisted = false,
            keyAvailable = false,
            count = 0,
            displays = rows
        };
    }

    foreach (var manufacturerKeyName in displayKey.GetSubKeyNames())
    {
        using var manufacturerKey = displayKey.OpenSubKey(manufacturerKeyName);
        if (manufacturerKey is null) { continue; }
        foreach (var instanceKeyName in manufacturerKey.GetSubKeyNames())
        {
            using var parametersKey = manufacturerKey.OpenSubKey($@"{instanceKeyName}\Device Parameters");
            if (parametersKey?.GetValue("EDID") is not byte[] edid) { continue; }
            rows.Add(new
            {
                displayKeyHash = HashIdentity($"{manufacturerKeyName}\\{instanceKeyName}"),
                edidHash = Convert.ToHexString(SHA256.HashData(edid))[..32],
                byteLength = edid.Length,
                headerValid = edid.Length >= 8 &&
                    edid[0] == 0x00 &&
                    edid[1] == 0xFF &&
                    edid[2] == 0xFF &&
                    edid[3] == 0xFF &&
                    edid[4] == 0xFF &&
                    edid[5] == 0xFF &&
                    edid[6] == 0xFF &&
                    edid[7] == 0x00,
                hasExtensionBlock = edid.Length > 126 && edid[126] > 0
            });
        }
    }

    return new
    {
        queried = true,
        rawEdidPersisted = false,
        keyAvailable = true,
        count = rows.Count,
        displays = rows
    };
}

internal static class NativeMethods
{
    [DllImport("user32.dll")]
    internal static extern int GetDisplayConfigBufferSizes(
        uint flags,
        out uint numberOfPathArrayElements,
        out uint numberOfModeInfoArrayElements);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool EnumDisplayDevices(
        string? device,
        uint deviceIndex,
        ref DisplayDevice displayDevice,
        uint flags);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool EnumDisplaySettings(
        string deviceName,
        int modeNumber,
        ref DevMode deviceMode);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern int ChangeDisplaySettingsEx(
        string deviceName,
        ref DevMode deviceMode,
        IntPtr window,
        uint flags,
        IntPtr parameter);
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct DisplayDevice
{
    internal int Size;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] internal string DeviceName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal string DeviceString;
    internal uint StateFlags;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal string DeviceId;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal string DeviceKey;

    internal static DisplayDevice Create() => new()
    {
        Size = Marshal.SizeOf<DisplayDevice>(),
        DeviceName = string.Empty,
        DeviceString = string.Empty,
        DeviceId = string.Empty,
        DeviceKey = string.Empty
    };
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct DevMode
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] internal string DeviceName;
    internal ushort SpecVersion;
    internal ushort DriverVersion;
    internal ushort Size;
    internal ushort DriverExtra;
    internal uint Fields;
    internal int PositionX;
    internal int PositionY;
    internal uint DisplayOrientation;
    internal uint DisplayFixedOutput;
    internal short Color;
    internal short Duplex;
    internal short YResolution;
    internal short TTOption;
    internal short Collate;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] internal string FormName;
    internal ushort LogPixels;
    internal uint BitsPerPel;
    internal uint PelsWidth;
    internal uint PelsHeight;
    internal uint DisplayFlags;
    internal uint DisplayFrequency;
    internal uint ICMMethod;
    internal uint ICMIntent;
    internal uint MediaType;
    internal uint DitherType;
    internal uint Reserved1;
    internal uint Reserved2;
    internal uint PanningWidth;
    internal uint PanningHeight;

    internal static DevMode Create() => new()
    {
        DeviceName = string.Empty,
        FormName = string.Empty,
        Size = checked((ushort)Marshal.SizeOf<DevMode>())
    };
}
