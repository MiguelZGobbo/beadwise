using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Results;
using BeadWise.Windows.Diagnostics;

namespace BeadWise.Windows.Tests.Diagnostics;

public sealed class WindowsSystemInventorySourceTests
{
    [Fact]
    public void CollectsStructuredFieldsFromIndependentSources()
    {
        var query = CompleteQuery();
        var source = new WindowsSystemInventorySource(query, () => true);

        var result = source.Observe();

        Assert.Equal(ResultStatus.Success, result.Status);
        var system = Assert.IsType<BeadWise.Core.Diagnostics.SystemInventory>(result.Value);
        Assert.Equal("Windows Example", system.WindowsEdition.Value);
        Assert.Equal("10.0.12345", system.WindowsVersion.Value);
        Assert.Equal("12345", system.WindowsBuild.Value);
        Assert.Equal("64-bit", system.Architecture.Value);
        var cpu = Assert.Single(system.Processors.Value);
        Assert.Equal("Example CPU", cpu.Name.Value);
        Assert.Equal(8, cpu.PhysicalCores.Value);
        Assert.Equal(16, cpu.LogicalProcessors.Value);
        var gpu = Assert.Single(system.Gpus.Value);
        Assert.Equal("Example GPU", gpu.Name.Value);
        Assert.Equal("3.2.1", gpu.DriverVersion.Value);
        var board = Assert.Single(system.BaseBoards.Value);
        Assert.Equal("Example Inc", board.Manufacturer.Value);
        Assert.Equal("Board", board.Product.Value);
        Assert.Equal("Mobile", system.FormFactor.Value);
        Assert.False(system.HasBattery.Value);
        Assert.False(system.HypervisorPresent.Value);
        Assert.True(system.IsElevated.Value);
        Assert.Equal("WMI:Win32_OperatingSystem", system.WindowsBuild.Source);
        Assert.Equal("Windows token elevation", system.IsElevated.Source);
        Assert.Equal(TimeSpan.Zero, system.WindowsBuild.Timestamp.Offset);
    }

    [Fact]
    public void MissingFieldsAndAccessFailuresDoNotInventValuesOrHideOtherFields()
    {
        var query = CompleteQuery();
        query.Set("Win32_Processor", FakeWmiQuery.Row(("Name", "CPU"), ("NumberOfCores", null), ("NumberOfLogicalProcessors", 8u)));
        query.Fail("Win32_ComputerSystem", new UnauthorizedAccessException());
        query.Fail("Win32_Battery", new PlatformNotSupportedException());
        var source = new WindowsSystemInventorySource(query, () => throw new UnauthorizedAccessException());

        var result = source.Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        var system = result.Value!;
        Assert.Equal("12345", system.WindowsBuild.Value);
        Assert.Equal(ObservationAvailability.Partial, system.Processors.Availability);
        Assert.Equal(ObservationAvailability.Unknown, Assert.Single(system.Processors.Value).PhysicalCores.Availability);
        Assert.Equal(ErrorCategory.AccessDenied, system.HypervisorPresent.Error?.Category);
        Assert.False(system.HypervisorPresent.HasValue);
        Assert.Equal(ObservationAvailability.Unsupported, system.HasBattery.Availability);
        Assert.False(system.HasBattery.HasValue);
        Assert.Equal(ErrorCategory.AccessDenied, system.IsElevated.Error?.Category);
    }

    [Fact]
    public void UnknownFormFactorAndNullHypervisorRemainUnknown()
    {
        var query = CompleteQuery();
        query.Set("Win32_ComputerSystem", FakeWmiQuery.Row(("PCSystemType", (ushort)0), ("HypervisorPresent", null)));

        var result = new WindowsSystemInventorySource(query, () => false).Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal(ObservationAvailability.Unknown, result.Value!.FormFactor.Availability);
        Assert.Equal(ObservationAvailability.Unknown, result.Value.HypervisorPresent.Availability);
    }

    [Fact]
    public void BatteryAndHypervisorAreTrueOnlyWhenObserved()
    {
        var query = CompleteQuery();
        query.Set("Win32_Battery", FakeWmiQuery.Row(("Status", "OK")));
        query.Set("Win32_ComputerSystem", FakeWmiQuery.Row(("PCSystemType", (ushort)1), ("HypervisorPresent", true)));

        var result = new WindowsSystemInventorySource(query, () => false).Observe();

        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.True(result.Value!.HasBattery.Value);
        Assert.True(result.Value.HypervisorPresent.Value);
        Assert.Equal("Desktop", result.Value.FormFactor.Value);
    }

    [Fact]
    public void WmiTimeoutIsTypedAndLeavesOtherSectionsAvailable()
    {
        var query = CompleteQuery();
        query.Fail("Win32_Processor", new TimeoutException());

        var result = new WindowsSystemInventorySource(query, () => false).Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.False(result.Value!.Processors.HasValue);
        Assert.Equal(ErrorCategory.Timeout, result.Value.Processors.Error?.Category);
        Assert.Equal("WMI:Win32_Processor", result.Value.Processors.Source);
        Assert.Equal("Example GPU", Assert.Single(result.Value.Gpus.Value).Name.Value);
    }

    [Fact]
    public void CatastrophicComOutOfMemoryIsNotConvertedIntoPartialInventory()
    {
        var query = CompleteQuery();
        query.Fail("Win32_Battery", new System.Runtime.InteropServices.COMException("Out of memory", unchecked((int)0x8007000E)));

        Assert.Throws<System.Runtime.InteropServices.COMException>(() => new WindowsSystemInventorySource(query, () => false).Observe());
    }

    private static FakeWmiQuery CompleteQuery()
    {
        var query = new FakeWmiQuery();
        query.Set("Win32_OperatingSystem", FakeWmiQuery.Row(("Caption", "Windows Example"), ("Version", "10.0.12345"), ("BuildNumber", "12345"), ("OSArchitecture", "64-bit")));
        query.Set("Win32_Processor", FakeWmiQuery.Row(("Name", "Example CPU"), ("NumberOfCores", 8u), ("NumberOfLogicalProcessors", 16u)));
        query.Set("Win32_VideoController", FakeWmiQuery.Row(("Name", "Example GPU"), ("DriverVersion", "3.2.1")));
        query.Set("Win32_BaseBoard", FakeWmiQuery.Row(("Manufacturer", "Example Inc"), ("Product", "Board")));
        query.Set("Win32_ComputerSystem", FakeWmiQuery.Row(("PCSystemType", (ushort)2), ("HypervisorPresent", false)));
        query.Set("Win32_Battery");
        return query;
    }
}
