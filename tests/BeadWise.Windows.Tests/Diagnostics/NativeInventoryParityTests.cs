using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using BeadWise.Core.Diagnostics;
using BeadWise.Core.Observations;
using BeadWise.Core.Results;
using BeadWise.Windows.Diagnostics;

namespace BeadWise.Windows.Tests.Diagnostics;

public sealed class NativeInventoryParityTests
{
    [Fact]
    public void NativeStructsMatchWindowsAbi()
    {
        Assert.Equal(IntPtr.Size == 8 ? 24 : 16, Marshal.SizeOf<PropVariant>());
        Assert.Equal(8, Marshal.OffsetOf<PropVariant>(nameof(PropVariant.Value)).ToInt32());
        Assert.Equal(840, Marshal.SizeOf<NativeDisplayDevice>());
        Assert.Equal(220, Marshal.SizeOf<NativeDevMode>());
    }

    [AudioHardwareFact]
    public void AudioInventoryMatchesIndependentlyEnumeratedEndpointIdsAndStates()
    {
        var reference = ReferenceAudioEndpoints();
        Assert.True(reference.Endpoints.Count > 0, "Audio endpoints disappeared before the parity check.");

        var result = new WindowsAudioInventorySource().Observe();

        Assert.True(result.Status is ResultStatus.Success or ResultStatus.Partial,
            $"Audio collection was unavailable: {result.Error?.Code}");
        Assert.NotNull(result.Value);
        Assert.Equal(reference.Endpoints.Count, result.Value.Count);
        foreach (var expected in reference.Endpoints)
        {
            var actual = Assert.Single(result.Value, item => item.Identity.HasValue && item.Identity.Value == expected.Identity);
            Assert.Equal(expected.Direction, actual.Direction);
            Assert.Equal(ObservationAvailability.Available, actual.State.Availability);
            Assert.Equal(expected.State, actual.State.Value);
            Assert.Equal(ObservationAvailability.Available, actual.DefaultRoles.Availability);
            var roles = Enum.GetValues<AudioRole>()
                .Where(role => reference.Defaults[(expected.Direction, role)] == expected.Identity).ToArray();
            Assert.Equal(roles, actual.DefaultRoles.Value);
        }
    }

    [DisplayHardwareFact]
    public void DisplayInventoryMatchesIndependentlyEnumeratedMonitorEntries()
    {
        var reference = ReferenceMonitors();
        var headless = ReferenceHeadlessAdapters();
        Assert.True(reference.Count > 0 || headless.Count > 0, "No display entries were available for the parity check.");

        var result = new WindowsDisplayInventorySource().Observe();

        Assert.True(result.Status is ResultStatus.Success or ResultStatus.Partial,
            $"Display collection was unavailable: {result.Error?.Code}");
        Assert.NotNull(result.Value);
        var monitors = result.Value.Where(item => item.Kind == DisplayObservationKind.Monitor).ToArray();
        Assert.Equal(reference.Count, monitors.Length);
        foreach (var expected in reference)
        {
            Assert.Contains(monitors, item => item.Identity.HasValue && item.Identity.Value == expected.Identity
                && item.Name.HasValue && item.Name.Value == expected.Name
                && item.AdapterName.HasValue && item.AdapterName.Value == expected.AdapterName);
        }
        var actualAdapters = result.Value.Where(item => item.Kind == DisplayObservationKind.Adapter).ToArray();
        Assert.All(actualAdapters, item => Assert.Equal(ObservationAvailability.Unknown, item.Name.Availability));
        var actualKeys = actualAdapters.Select(item => (
                Name: item.AdapterName.HasValue ? item.AdapterName.Value : null,
                Identity: item.Identity.HasValue ? item.Identity.Value : null))
            .OrderBy(item => item.Name, StringComparer.Ordinal).ThenBy(item => item.Identity, StringComparer.Ordinal);
        var expectedKeys = headless.OrderBy(item => item.Name, StringComparer.Ordinal)
            .ThenBy(item => item.Identity, StringComparer.Ordinal);
        Assert.Equal(expectedKeys, actualKeys);
    }

    [MonitorHardwareFact]
    public void DisplayCurrentModeMatchesIndependentNativeQuery()
    {
        var reference = ReferenceMonitors();
        var result = new WindowsDisplayInventorySource().Observe();
        Assert.True(result.Status is ResultStatus.Success or ResultStatus.Partial,
            $"Display collection was unavailable: {result.Error?.Code}");
        var comparisons = 0;
        foreach (var monitor in reference.Where(item => item.Active))
        {
            var mode = NativeDevMode.Create();
            if (!EnumDisplaySettings(monitor.AdapterDeviceName, -1, ref mode)
                || (mode.Fields & 0x005c0000) != 0x005c0000 || mode.DisplayFrequency <= 1
                || mode.PelsWidth == 0 || mode.PelsHeight == 0 || mode.BitsPerPel == 0
                || mode.PelsWidth > int.MaxValue || mode.PelsHeight > int.MaxValue) continue;
            var actual = Assert.Single(result.Value!, item => item.Identity.HasValue && item.Identity.Value == monitor.Identity);
            Assert.Equal(ObservationAvailability.Available, actual.CurrentMode.Availability);
            Assert.Equal(new DisplayMode((int)mode.PelsWidth, (int)mode.PelsHeight, mode.DisplayFrequency, mode.BitsPerPel),
                actual.CurrentMode.Value);
            comparisons++;
        }
        Assert.True(comparisons > 0, "No active monitor supplied a complete current mode for the parity check.");
    }

    internal static bool HasAudioEndpoints() => ReferenceAudioEndpoints().Endpoints.Count > 0;

    internal static bool ConfirmNoDisplayMonitors()
    {
        if (ReferenceMonitors().Count != 0) return false;
        var count = 0;
        MonitorCallback callback = (IntPtr monitor, IntPtr dc, IntPtr rectangle, IntPtr data) =>
        {
            count++;
            return true;
        };
        return EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, IntPtr.Zero) && count == 0;
    }

    internal static bool ConfirmNoDisplayDevices() =>
        ReferenceHeadlessAdapters().Count == 0 && ConfirmNoDisplayMonitors();

    private static ReferenceAudioSnapshot ReferenceAudioEndpoints()
    {
        var initialize = CoInitializeEx(IntPtr.Zero, 0);
        Assert.True(initialize >= 0 || initialize == unchecked((int)0x80010106));
        ReferenceEnumerator? enumerator = null;
        try
        {
            enumerator = (ReferenceEnumerator)(object)new ReferenceEnumeratorClass();
            var rows = new List<(string, AudioDirection, AudioEndpointState)>();
            var defaults = new Dictionary<(AudioDirection, AudioRole), string?>();
            foreach (var direction in new[] { AudioDirection.Render, AudioDirection.Capture })
            {
                ReferenceCollection? collection = null;
                try
                {
                    Assert.True(enumerator.EnumAudioEndpoints((int)direction, 0x0f, out collection) >= 0);
                    Assert.NotNull(collection);
                    Assert.True(collection.GetCount(out var count) >= 0);
                    for (uint index = 0; index < count; index++)
                    {
                        ReferenceDevice? device = null;
                        try
                        {
                            Assert.True(collection.Item(index, out device) >= 0);
                            Assert.NotNull(device);
                            IntPtr id = IntPtr.Zero;
                            try
                            {
                                Assert.True(device.GetId(out id) >= 0);
                                var raw = Marshal.PtrToStringUni(id);
                                Assert.False(string.IsNullOrWhiteSpace(raw));
                                Assert.True(device.GetState(out var state) >= 0);
                                rows.Add((Hash(raw!), direction, (AudioEndpointState)state));
                            }
                            finally { Marshal.FreeCoTaskMem(id); }
                        }
                        finally { Release(device); }
                    }
                }
                finally { Release(collection); }
                foreach (var role in Enum.GetValues<AudioRole>())
                {
                    ReferenceDevice? device = null;
                    try
                    {
                        var hr = enumerator.GetDefaultAudioEndpoint((int)direction, (int)role, out device);
                        if (hr == unchecked((int)0x80070490)) { defaults[(direction, role)] = null; continue; }
                        Assert.True(hr >= 0);
                        Assert.NotNull(device);
                        IntPtr id = IntPtr.Zero;
                        try
                        {
                            Assert.True(device.GetId(out id) >= 0);
                            var raw = Marshal.PtrToStringUni(id);
                            Assert.False(string.IsNullOrWhiteSpace(raw));
                            defaults[(direction, role)] = Hash(raw!);
                        }
                        finally { Marshal.FreeCoTaskMem(id); }
                    }
                    finally { Release(device); }
                }
            }
            return new ReferenceAudioSnapshot(rows, defaults);
        }
        finally
        {
            Release(enumerator);
            if (initialize >= 0) CoUninitialize();
        }
    }

    private static List<(string Identity, string Name, string AdapterName, string AdapterDeviceName, bool Active)> ReferenceMonitors()
    {
        var rows = new List<(string, string, string, string, bool)>();
        for (uint index = 0; ; index++)
        {
            var adapter = ReferenceDisplayDevice.Create();
            if (!EnumDisplayDevices(null, index, ref adapter, 0)) break;
            for (uint monitorIndex = 0; ; monitorIndex++)
            {
                var monitor = ReferenceDisplayDevice.Create();
                if (!EnumDisplayDevices(adapter.DeviceName, monitorIndex, ref monitor, 1)) break;
                Assert.False(string.IsNullOrWhiteSpace(monitor.DeviceId));
                rows.Add((Hash(monitor.DeviceId), monitor.DeviceString, adapter.DeviceString, adapter.DeviceName,
                    (adapter.StateFlags & 1) != 0 && (monitor.StateFlags & 1) != 0));
            }
        }
        return rows;
    }

    private static List<(string? Name, string? Identity)> ReferenceHeadlessAdapters()
    {
        var rows = new List<(string?, string?)>();
        for (uint index = 0; ; index++)
        {
            var adapter = ReferenceDisplayDevice.Create();
            if (!EnumDisplayDevices(null, index, ref adapter, 0)) break;
            var monitor = ReferenceDisplayDevice.Create();
            if (EnumDisplayDevices(adapter.DeviceName, 0, ref monitor, 1)) continue;
            rows.Add((string.IsNullOrWhiteSpace(adapter.DeviceString) ? null : adapter.DeviceString,
                string.IsNullOrWhiteSpace(adapter.DeviceKey) ? null : Hash(adapter.DeviceKey)));
        }
        return rows;
    }

    private static string Hash(string value) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    private static void Release(object? value)
    {
        if (value is not null && Marshal.IsComObject(value)) Marshal.ReleaseComObject(value);
    }

    [DllImport("ole32.dll")] private static extern int CoInitializeEx(IntPtr reserved, uint mode);
    [DllImport("ole32.dll")] private static extern void CoUninitialize();
    [DllImport("user32.dll", EntryPoint = "EnumDisplayDevicesW", CharSet = CharSet.Unicode, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumDisplayDevices(string? device, uint index, ref ReferenceDisplayDevice result, uint flags);
    [DllImport("user32.dll", EntryPoint = "EnumDisplaySettingsW", CharSet = CharSet.Unicode, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumDisplaySettings(string device, int mode, ref NativeDevMode result);
    private delegate bool MonitorCallback(IntPtr monitor, IntPtr dc, IntPtr rectangle, IntPtr data);
    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumDisplayMonitors(IntPtr dc, IntPtr clip, MonitorCallback callback, IntPtr data);

    private sealed record ReferenceAudioSnapshot(
        List<(string Identity, AudioDirection Direction, AudioEndpointState State)> Endpoints,
        Dictionary<(AudioDirection, AudioRole), string?> Defaults);

    [ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    private sealed class ReferenceEnumeratorClass { }
    [ComImport, Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface ReferenceEnumerator
    {
        [PreserveSig] int EnumAudioEndpoints(int direction, uint states, out ReferenceCollection? collection);
        [PreserveSig] int GetDefaultAudioEndpoint(int direction, int role, out ReferenceDevice? device);
    }
    [ComImport, Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface ReferenceCollection
    {
        [PreserveSig] int GetCount(out uint count);
        [PreserveSig] int Item(uint index, out ReferenceDevice? device);
    }
    [ComImport, Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface ReferenceDevice
    {
        [PreserveSig] int Activate(ref Guid id, uint context, IntPtr parameters, out IntPtr result);
        [PreserveSig] int OpenPropertyStore(uint access, out IntPtr properties);
        [PreserveSig] int GetId(out IntPtr id);
        [PreserveSig] int GetState(out uint state);
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct ReferenceDisplayDevice
    {
        internal int Size;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] internal string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal string DeviceString;
        internal uint StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal string DeviceId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal string DeviceKey;
        internal static ReferenceDisplayDevice Create() => new()
        {
            Size = Marshal.SizeOf<ReferenceDisplayDevice>(), DeviceName = "", DeviceString = "", DeviceId = "", DeviceKey = ""
        };
    }
}

internal sealed class AudioHardwareFactAttribute : FactAttribute
{
    public AudioHardwareFactAttribute()
    {
        if (!OperatingSystem.IsWindows()) { Skip = "Core Audio requires Windows."; return; }
        try
        {
            if (!NativeInventoryParityTests.HasAudioEndpoints()) Skip = "No audio endpoints are present.";
        }
        catch (Exception)
        {
            // Leave native failures runnable so the parity test reports them.
        }
    }
}

internal sealed class DisplayHardwareFactAttribute : FactAttribute
{
    public DisplayHardwareFactAttribute()
    {
        if (!OperatingSystem.IsWindows()) { Skip = "GDI display enumeration requires Windows."; return; }
        try
        {
            if (NativeInventoryParityTests.ConfirmNoDisplayDevices()) Skip = "No display devices are present in this session.";
        }
        catch (Exception)
        {
            // Leave native failures runnable so the parity test reports them.
        }
    }
}

internal sealed class MonitorHardwareFactAttribute : FactAttribute
{
    public MonitorHardwareFactAttribute()
    {
        if (!OperatingSystem.IsWindows()) { Skip = "GDI display enumeration requires Windows."; return; }
        try
        {
            if (NativeInventoryParityTests.ConfirmNoDisplayMonitors()) Skip = "No display monitors are present in this session.";
        }
        catch (Exception)
        {
            // Leave native failures runnable so the parity test reports them.
        }
    }
}
