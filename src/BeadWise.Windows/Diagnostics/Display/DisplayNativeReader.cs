using System.ComponentModel;
using System.Runtime.InteropServices;
using BeadWise.Core.Errors;
using BeadWise.Core.Results;

namespace BeadWise.Windows.Diagnostics;

internal static class DisplayNativeReader
{
    private const uint Active = 0x00000001;
    private const uint InterfaceName = 0x00000001;
    private const int CurrentSettings = -1;

    internal static DisplayNativeSnapshot Read()
    {
        if (!OperatingSystem.IsWindows()) throw new PlatformNotSupportedException();
        var adapters = new List<DisplayNativeAdapter>();
        for (uint index = 0; ; index++)
        {
            var adapter = NativeDisplayDevice.Create();
            if (!EnumDisplayDevices(null, index, ref adapter, 0)) break;
            var monitors = new List<DisplayNativeMonitor>();
            for (uint monitorIndex = 0; ; monitorIndex++)
            {
                var monitor = NativeDisplayDevice.Create();
                if (!EnumDisplayDevices(adapter.DeviceName, monitorIndex, ref monitor, InterfaceName)) break;
                monitors.Add(new DisplayNativeMonitor(monitor.DeviceId, monitor.DeviceString,
                    (adapter.StateFlags & Active) != 0 && (monitor.StateFlags & Active) != 0));
            }
            var attached = (adapter.StateFlags & Active) != 0;
            var current = attached ? ReadCurrent(adapter.DeviceName) : Failure<DisplayNativeMode>("DISPLAY_CURRENT_MODE_UNAVAILABLE");
            var supported = attached ? ReadSupported(adapter.DeviceName) : Failure<IReadOnlyList<DisplayNativeMode>>("DISPLAY_SUPPORTED_MODES_UNAVAILABLE");
            adapters.Add(new DisplayNativeAdapter(adapter.DeviceString, monitors.AsReadOnly(), current, supported, adapter.DeviceKey));
        }
        return new DisplayNativeSnapshot(adapters.AsReadOnly(), []);
    }

    private static Result<DisplayNativeMode> ReadCurrent(string adapter)
    {
        var native = NativeDevMode.Create();
        return EnumDisplaySettings(adapter, CurrentSettings, ref native)
            ? Result<DisplayNativeMode>.Success(ToMode(native))
            : Failure<DisplayNativeMode>("DISPLAY_CURRENT_MODE_UNAVAILABLE");
    }

    private static Result<IReadOnlyList<DisplayNativeMode>> ReadSupported(string adapter)
    {
        var modes = new List<DisplayNativeMode>();
        for (var index = 0; ; index++)
        {
            var native = NativeDevMode.Create();
            if (!EnumDisplaySettings(adapter, index, ref native)) break;
            modes.Add(ToMode(native));
        }
        return modes.Count == 0
            ? Failure<IReadOnlyList<DisplayNativeMode>>("DISPLAY_SUPPORTED_MODES_UNAVAILABLE")
            : Result<IReadOnlyList<DisplayNativeMode>>.Success(modes.AsReadOnly());
    }

    private static DisplayNativeMode ToMode(NativeDevMode mode) =>
        new(mode.Fields, mode.PelsWidth, mode.PelsHeight, mode.DisplayFrequency, mode.BitsPerPel);

    private static Result<T> Failure<T>(string code) =>
        Result<T>.Failed(new Error(code, ErrorCategory.Unavailable, recoverable: true));

    internal static bool IsExpectedFailure(Exception exception) => exception is Win32Exception
        or UnauthorizedAccessException or PlatformNotSupportedException or DllNotFoundException or EntryPointNotFoundException;

    internal static Error ErrorFromException(Exception exception)
    {
        var category = exception switch
        {
            UnauthorizedAccessException => ErrorCategory.AccessDenied,
            Win32Exception { NativeErrorCode: 5 } => ErrorCategory.AccessDenied,
            PlatformNotSupportedException or DllNotFoundException or EntryPointNotFoundException => ErrorCategory.Unsupported,
            _ => ErrorCategory.Unavailable
        };
        var suffix = category switch
        {
            ErrorCategory.AccessDenied => "ACCESS_DENIED",
            ErrorCategory.Unsupported => "UNSUPPORTED",
            _ => "UNAVAILABLE"
        };
        return new Error($"DISPLAY_ENUMERATION_{suffix}", category, exception.GetType().FullName,
            recoverable: category != ErrorCategory.Unsupported);
    }

    [DllImport("user32.dll", EntryPoint = "EnumDisplayDevicesW", CharSet = CharSet.Unicode, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumDisplayDevices(string? device, uint index, ref NativeDisplayDevice display, uint flags);

    [DllImport("user32.dll", EntryPoint = "EnumDisplaySettingsW", CharSet = CharSet.Unicode, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumDisplaySettings(string device, int mode, ref NativeDevMode displayMode);
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct NativeDisplayDevice
{
    internal int Size;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] internal string DeviceName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal string DeviceString;
    internal uint StateFlags;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal string DeviceId;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal string DeviceKey;

    internal static NativeDisplayDevice Create() => new()
    {
        Size = Marshal.SizeOf<NativeDisplayDevice>(), DeviceName = "", DeviceString = "", DeviceId = "", DeviceKey = ""
    };
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct NativeDevMode
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] internal string DeviceName;
    internal ushort SpecVersion, DriverVersion, Size, DriverExtra;
    internal uint Fields;
    internal int PositionX, PositionY;
    internal uint DisplayOrientation, DisplayFixedOutput;
    internal short Color, Duplex, YResolution, TTOption, Collate;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] internal string FormName;
    internal ushort LogPixels;
    internal uint BitsPerPel, PelsWidth, PelsHeight, DisplayFlags, DisplayFrequency;
    internal uint ICMMethod, ICMIntent, MediaType, DitherType, Reserved1, Reserved2, PanningWidth, PanningHeight;

    internal static NativeDevMode Create() => new()
    {
        DeviceName = "", FormName = "", Size = checked((ushort)Marshal.SizeOf<NativeDevMode>())
    };
}
