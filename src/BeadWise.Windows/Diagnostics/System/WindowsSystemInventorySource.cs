using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;
using BeadWise.Core.Contracts;
using BeadWise.Core.Diagnostics;
using BeadWise.Core.Observations;
using BeadWise.Core.Results;
using Microsoft.Win32.SafeHandles;

namespace BeadWise.Windows.Diagnostics;

public sealed class WindowsSystemInventorySource : ISystemInventorySource
{
    private const string ElevationSource = "Windows token elevation";
    private readonly IWmiQuery _query;
    private readonly Func<bool> _readElevation;

    public WindowsSystemInventorySource() : this(new WmiQuery(), ReadElevation) { }

    internal WindowsSystemInventorySource(IWmiQuery query, Func<bool> readElevation)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(readElevation);
        _query = query;
        _readElevation = readElevation;
    }

    public Result<SystemInventory> Observe()
    {
        var os = WmiRead.Capture(_query, "Win32_OperatingSystem", "Caption", "Version", "BuildNumber", "OSArchitecture");
        var processors = WmiRead.Capture(_query, "Win32_Processor", "Name", "NumberOfCores", "NumberOfLogicalProcessors");
        var gpus = WmiRead.Capture(_query, "Win32_VideoController", "Name", "DriverVersion");
        var boards = WmiRead.Capture(_query, "Win32_BaseBoard", "Manufacturer", "Product");
        var computer = WmiRead.Capture(_query, "Win32_ComputerSystem", "PCSystemType", "HypervisorPresent");
        var batteries = WmiRead.Capture(_query, "Win32_Battery", "Status");

        var result = new SystemInventory(
            os.Text("Caption"), os.Text("Version"), os.Text("BuildNumber"), os.Text("OSArchitecture"),
            ReadProcessors(processors), ReadGpus(gpus), ReadBoards(boards),
            ReadFormFactor(computer), ReadBattery(batteries), computer.Boolean("HypervisorPresent"),
            ReadTokenElevation());

        var complete = InventoryRead.Available(result.WindowsEdition)
            && InventoryRead.Available(result.WindowsVersion) && InventoryRead.Available(result.WindowsBuild)
            && InventoryRead.Available(result.Architecture) && InventoryRead.Available(result.Processors)
            && InventoryRead.Available(result.Gpus) && InventoryRead.Available(result.BaseBoards)
            && InventoryRead.Available(result.FormFactor) && InventoryRead.Available(result.HasBattery)
            && InventoryRead.Available(result.HypervisorPresent) && InventoryRead.Available(result.IsElevated);
        return complete
            ? Result<SystemInventory>.Success(result)
            : Result<SystemInventory>.Partial(result, InventoryRead.PartialError("SYSTEM_INVENTORY_PARTIAL"));
    }

    private static Observation<IReadOnlyList<ProcessorInfo>> ReadProcessors(WmiRead read) =>
        ReadList(read, row => new ProcessorInfo(
            InventoryRead.Text(row.GetValueOrDefault("Name"), read.Source, read.Timestamp),
            InventoryRead.Count(row.GetValueOrDefault("NumberOfCores"), read.Source, read.Timestamp),
            InventoryRead.Count(row.GetValueOrDefault("NumberOfLogicalProcessors"), read.Source, read.Timestamp)),
            cpu => InventoryRead.Available(cpu.Name) && InventoryRead.Available(cpu.PhysicalCores)
                && InventoryRead.Available(cpu.LogicalProcessors));

    private static Observation<IReadOnlyList<GpuInfo>> ReadGpus(WmiRead read) =>
        ReadList(read, row => new GpuInfo(
            InventoryRead.Text(row.GetValueOrDefault("Name"), read.Source, read.Timestamp),
            InventoryRead.Text(row.GetValueOrDefault("DriverVersion"), read.Source, read.Timestamp)),
            gpu => InventoryRead.Available(gpu.Name) && InventoryRead.Available(gpu.DriverVersion));

    private static Observation<IReadOnlyList<BaseBoardInfo>> ReadBoards(WmiRead read) =>
        ReadList(read, row => new BaseBoardInfo(
            InventoryRead.Text(row.GetValueOrDefault("Manufacturer"), read.Source, read.Timestamp),
            InventoryRead.Text(row.GetValueOrDefault("Product"), read.Source, read.Timestamp)),
            board => InventoryRead.Available(board.Manufacturer) && InventoryRead.Available(board.Product));

    private static Observation<IReadOnlyList<T>> ReadList<T>(
        WmiRead read, Func<IReadOnlyDictionary<string, object?>, T> map, Func<T, bool> complete)
    {
        if (read.Error is not null)
        {
            return InventoryRead.Missing<IReadOnlyList<T>>(read.Source, read.Timestamp, read.Error);
        }

        IReadOnlyList<T> items = Array.AsReadOnly(read.Rows.Select(map).ToArray());
        return items.All(complete)
            ? Observation<IReadOnlyList<T>>.Available(items, read.Source, read.Timestamp)
            : Observation<IReadOnlyList<T>>.Partial(items, read.Source, read.Timestamp,
                InventoryRead.PartialError("SYSTEM_FIELDS_PARTIAL"));
    }

    private static Observation<string> ReadFormFactor(WmiRead read)
    {
        var type = read.First("PCSystemType") switch
        {
            ushort number => (int)number,
            uint number when number <= int.MaxValue => (int)number,
            int number => number,
            _ => 0
        };
        var name = type switch
        {
            1 => "Desktop",
            2 => "Mobile",
            3 => "Workstation",
            4 => "Enterprise Server",
            5 => "SOHO Server",
            6 => "Appliance PC",
            7 => "Performance Server",
            _ => null
        };
        return InventoryRead.Text(name, read.Source, read.Timestamp, read.Error);
    }

    private static Observation<bool> ReadBattery(WmiRead read) =>
        read.Error is null
            ? Observation<bool>.Available(read.Rows.Count > 0, read.Source, read.Timestamp)
            : InventoryRead.Missing<bool>(read.Source, read.Timestamp, read.Error);

    private Observation<bool> ReadTokenElevation()
    {
        var timestamp = DateTimeOffset.UtcNow;
        try
        {
            return Observation<bool>.Available(_readElevation(), ElevationSource, timestamp);
        }
        catch (Exception exception) when (InventoryRead.IsExpectedFailure(exception))
        {
            return InventoryRead.Missing<bool>(ElevationSource, timestamp, InventoryRead.Error("TOKEN_ELEVATION", exception));
        }
    }

    private static bool ReadElevation()
    {
        using var identity = WindowsIdentity.GetCurrent();
        if (!GetTokenInformation(identity.AccessToken, 20, out var elevation, sizeof(uint), out _))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        return elevation != 0;
    }

    [DllImport("advapi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetTokenInformation(SafeAccessTokenHandle token, int informationClass,
        out uint information, int informationLength, out int returnLength);
}
