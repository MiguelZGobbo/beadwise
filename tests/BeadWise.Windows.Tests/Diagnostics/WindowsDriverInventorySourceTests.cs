using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Results;
using BeadWise.Windows.Diagnostics;

namespace BeadWise.Windows.Tests.Diagnostics;

public sealed class WindowsDriverInventorySourceTests
{
    [Fact]
    public void MatchesMetadataCaseInsensitivelyAndKeepsFalseAndZero()
    {
        var query = new FakeWmiQuery();
        query.Set("Win32_PnPSignedDriver", FakeWmiQuery.Row(("DeviceID", "pci\\ven_1234\\abc"), ("DeviceName", "Adapter"), ("DeviceClass", "Net"), ("DriverProviderName", "Vendor"), ("DriverVersion", "1.2"), ("IsSigned", false)));
        var source = new WindowsDriverInventorySource(query, () => new DeviceEnumeration([new NativeDevice("PCI\\VEN_1234\\ABC", "Adapter", "Net", 0, 8)], null));

        var result = source.Observe();

        Assert.Equal(ResultStatus.Success, result.Status);
        var item = Assert.Single(result.Value!);
        Assert.Equal("Adapter", item.Name.Value);
        Assert.Equal("Net", item.ClassName.Value);
        Assert.Equal("Vendor", item.Provider.Value);
        Assert.Equal("1.2", item.Version.Value);
        Assert.False(item.Signed.Value);
        Assert.Equal(0u, item.ProblemCode.Value);
        Assert.Equal(8u, item.DeviceStatus.Value);
        Assert.Equal("F93E8B95942C85A85FAAB4B3391554567F78611568B967D530EB4C3A8A8A042E", item.Identity.Value);
        Assert.DoesNotContain("VEN_1234", item.Identity.Value, StringComparison.OrdinalIgnoreCase);
        Assert.Equal("CfgMgr32:CM_Get_DevNode_Status", item.ProblemCode.Source);
    }

    [Fact]
    public void NullSignatureAndUnresolvedIdentityAreUnknownAndDeviceIsRetained()
    {
        var query = new FakeWmiQuery();
        query.Set("Win32_PnPSignedDriver", FakeWmiQuery.Row(("DeviceID", "PCI\\ONE"), ("DeviceName", "One"), ("DeviceClass", "Net"), ("DriverProviderName", "Vendor"), ("DriverVersion", "1"), ("IsSigned", null)));
        var source = new WindowsDriverInventorySource(query, () => new DeviceEnumeration([
            new NativeDevice("PCI\\ONE", "One", "Net", null, null),
            new NativeDevice(null, "Unknown device", null, null, null)
        ], null));

        var result = source.Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal(2, result.Value!.Count);
        var first = result.Value[0];
        Assert.Equal(ObservationAvailability.Unknown, first.Signed.Availability);
        Assert.Equal(ObservationAvailability.Unknown, first.ProblemCode.Availability);
        Assert.Equal(ObservationAvailability.Unknown, first.DeviceStatus.Availability);
        var second = result.Value[1];
        Assert.Equal("Unknown device", second.Name.Value);
        Assert.Equal(ObservationAvailability.Unknown, second.Identity.Availability);
        Assert.Equal(ObservationAvailability.Unknown, second.Provider.Availability);
    }

    [Fact]
    public void MetadataProviderFailurePreservesNativeDevicesAndTypedError()
    {
        var query = new FakeWmiQuery();
        query.Fail("Win32_PnPSignedDriver", new UnauthorizedAccessException());
        var source = new WindowsDriverInventorySource(query, () => new DeviceEnumeration([new NativeDevice("PCI\\ONE", "Device", "System", 0, 8)], null));

        var result = source.Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        var device = Assert.Single(result.Value!);
        Assert.Equal("Device", device.Name.Value);
        Assert.Equal(0u, device.ProblemCode.Value);
        Assert.Equal(ErrorCategory.AccessDenied, device.Provider.Error?.Category);
        Assert.False(device.Signed.HasValue);
    }

    [Fact]
    public void NativeEnumerationFailureReturnsTypedFailure()
    {
        var source = new WindowsDriverInventorySource(new FakeWmiQuery(), () => throw new UnauthorizedAccessException());

        var result = source.Observe();

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal(ErrorCategory.AccessDenied, result.Error?.Category);
    }

    [Fact]
    public void StatusFailureAndInterruptedEnumerationPreserveTheDevice()
    {
        var statusError = new Error("DEVICE_STATUS_ACCESS_DENIED", ErrorCategory.AccessDenied);
        var enumerationError = new Error("DEVICE_ENUMERATION_UNAVAILABLE", ErrorCategory.Unavailable);
        var source = new WindowsDriverInventorySource(new FakeWmiQuery(), () => new DeviceEnumeration([
            new NativeDevice("PCI\\ONE", "Device", "System", null, null, StatusError: statusError)
        ], enumerationError));

        var result = source.Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal(ErrorCategory.Unavailable, result.Error?.Category);
        var device = Assert.Single(result.Value!);
        Assert.Equal("Device", device.Name.Value);
        Assert.False(device.ProblemCode.HasValue);
        Assert.Equal(ErrorCategory.AccessDenied, device.ProblemCode.Error?.Category);
        Assert.Equal("CfgMgr32:CM_Get_DevNode_Status", device.DeviceStatus.Source);
    }

    [Fact]
    public void AmbiguousMetadataIsNotArbitrarilyAssigned()
    {
        var query = new FakeWmiQuery();
        query.Set("Win32_PnPSignedDriver",
            FakeWmiQuery.Row(("DeviceID", "PCI\\ONE"), ("DriverProviderName", "Vendor A")),
            FakeWmiQuery.Row(("DeviceID", "pci\\one"), ("DriverProviderName", "Vendor B")));
        var source = new WindowsDriverInventorySource(query, () => new DeviceEnumeration([
            new NativeDevice("PCI\\ONE", "Device", "System", 0, 8)
        ], null));

        var result = source.Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        var device = Assert.Single(result.Value!);
        Assert.False(device.Provider.HasValue);
        Assert.Equal(ErrorCategory.PartialFailure, device.Provider.Error?.Category);
    }

    [Fact]
    public void UnsupportedDeviceApiIsReportedAsUnsupported()
    {
        var source = new WindowsDriverInventorySource(new FakeWmiQuery(), () => throw new PlatformNotSupportedException());

        var result = source.Observe();

        Assert.Equal(ResultStatus.Unsupported, result.Status);
        Assert.Equal(ErrorCategory.Unsupported, result.Error?.Category);
    }
}
