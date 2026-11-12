using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using BeadWise.Core.Observations;
using BeadWise.Core.Results;
using BeadWise.Windows.Diagnostics;

namespace BeadWise.Windows.Tests.Diagnostics;

public sealed class WindowsInventoryNativeParityTests
{
    [Fact]
    public void SystemInventoryMatchesNativeWindowsBuildAndProcessorCount()
    {
        var version = new OsVersion { Size = (uint)Marshal.SizeOf<OsVersion>(), ServicePack = string.Empty };
        Assert.Equal(0, RtlGetVersion(ref version));
        var activeProcessors = GetActiveProcessorCount(0xffff);
        Assert.True(activeProcessors > 0);

        var result = new WindowsSystemInventorySource().Observe();

        Assert.True(result.Status is ResultStatus.Success or ResultStatus.Partial);
        var system = Assert.IsType<BeadWise.Core.Diagnostics.SystemInventory>(result.Value);
        Assert.True(system.WindowsBuild.HasValue);
        Assert.Equal(version.Build.ToString(CultureInfo.InvariantCulture), system.WindowsBuild.Value);
        Assert.True(system.Processors.HasValue);
        Assert.NotEmpty(system.Processors.Value);
        Assert.All(system.Processors.Value, cpu => Assert.True(cpu.LogicalProcessors.HasValue));
        Assert.Equal((long)activeProcessors, system.Processors.Value.Sum(cpu => (long)cpu.LogicalProcessors.Value));
        Assert.True(system.IsElevated.HasValue);
    }

    [Fact]
    public void PresentDevicesAndStatusMatchConfigurationManager()
    {
        var ids = ReadPresentIds();
        Assert.NotEmpty(ids);
        var expected = ids.ToDictionary(Hash, StringComparer.OrdinalIgnoreCase);

        var result = new WindowsDriverInventorySource().Observe();

        Assert.True(result.Status is ResultStatus.Success or ResultStatus.Partial);
        Assert.NotNull(result.Value);
        Assert.NotEmpty(result.Value);
        var matched = result.Value.Where(device => device.Identity.HasValue && expected.ContainsKey(device.Identity.Value)).ToArray();
        Assert.True(matched.Length >= expected.Count * 0.8, $"Matched {matched.Length} of {expected.Count} present devices.");
        Assert.True(matched.Length >= result.Value.Count * 0.8, $"Matched {matched.Length} of {result.Value.Count} collected devices.");
        var observed = Assert.Single(matched.Take(1));
        Assert.Equal(0u, CM_Locate_DevNode(out var devInst, expected[observed.Identity.Value], 0));
        Assert.Equal(0u, CM_Get_DevNode_Status(out var status, out var problem, devInst, 0));
        Assert.Equal(ObservationAvailability.Available, observed.DeviceStatus.Availability);
        Assert.Equal(status, observed.DeviceStatus.Value);
        Assert.Equal(ObservationAvailability.Available, observed.ProblemCode.Availability);
        Assert.Equal((status & 0x400) == 0 ? 0u : problem, observed.ProblemCode.Value);

        var versionsCompared = 0;
        foreach (var device in matched.Where(device => device.Version.HasValue))
        {
            if (CM_Locate_DevNode(out var current, expected[device.Identity.Value], 0) != 0)
            {
                continue;
            }

            var version = ReadDriverVersion(current);
            if (version is null)
            {
                continue;
            }

            Assert.Equal(version, device.Version.Value);
            versionsCompared++;
        }

        Assert.True(versionsCompared > 0, "No driver version could be compared with Configuration Manager.");
    }

    private static string[] ReadPresentIds()
    {
        const uint present = 0x100;
        for (var attempt = 0; attempt < 3; attempt++)
        {
            Assert.Equal(0u, CM_Get_Device_ID_List_Size(out var length, null, present));
            var buffer = new byte[checked((int)length * 2)];
            var status = CM_Get_Device_ID_List(null, buffer, length, present);
            if (status == 0x1a)
            {
                continue;
            }

            Assert.Equal(0u, status);
            return Encoding.Unicode.GetString(buffer).Split('\0', StringSplitOptions.RemoveEmptyEntries);
        }

        throw new InvalidOperationException("The device list changed repeatedly during the test.");
    }

    private static string Hash(string id) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(id.Trim().ToUpperInvariant())));

    private static string? ReadDriverVersion(uint devInst)
    {
        var key = new DevicePropertyKey { Format = new Guid("a8b865dd-2e3d-4094-ad97-e593a70c75d6"), Id = 3 };
        var buffer = new byte[4096];
        var size = (uint)buffer.Length;
        return CM_Get_DevNode_Property(devInst, ref key, out var type, buffer, ref size, 0) == 0 && type == 0x12
            ? Encoding.Unicode.GetString(buffer, 0, (int)size).TrimEnd('\0')
            : null;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct DevicePropertyKey
    {
        public Guid Format;
        public uint Id;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct OsVersion
    {
        public uint Size;
        public uint Major;
        public uint Minor;
        public uint Build;
        public uint Platform;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string ServicePack;
    }

    [DllImport("ntdll.dll")]
    private static extern int RtlGetVersion(ref OsVersion version);

    [DllImport("kernel32.dll")]
    private static extern uint GetActiveProcessorCount(ushort groupNumber);

    [DllImport("cfgmgr32.dll", EntryPoint = "CM_Get_Device_ID_List_SizeW", CharSet = CharSet.Unicode)]
    private static extern uint CM_Get_Device_ID_List_Size(out uint length, string? filter, uint flags);

    [DllImport("cfgmgr32.dll", EntryPoint = "CM_Get_Device_ID_ListW", CharSet = CharSet.Unicode)]
    private static extern uint CM_Get_Device_ID_List(string? filter, [Out] byte[] buffer, uint length, uint flags);

    [DllImport("cfgmgr32.dll", EntryPoint = "CM_Locate_DevNodeW", CharSet = CharSet.Unicode)]
    private static extern uint CM_Locate_DevNode(out uint devInst, string deviceId, uint flags);

    [DllImport("cfgmgr32.dll")]
    private static extern uint CM_Get_DevNode_Status(out uint status, out uint problem, uint devInst, uint flags);

    [DllImport("cfgmgr32.dll", EntryPoint = "CM_Get_DevNode_PropertyW")]
    private static extern uint CM_Get_DevNode_Property(uint devInst, ref DevicePropertyKey key,
        out uint propertyType, [Out] byte[] buffer, ref uint size, uint flags);
}
