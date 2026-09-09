using System.Runtime.InteropServices;
using System.Text.Json;

internal static class Program
{
    private const int DxgiErrorNotFound = unchecked((int)0x887A0002);
    private static readonly Guid Factory1Guid = new("770aae78-f26f-4dba-a829-253c83d1b387");
    private static readonly Guid Adapter3Guid = new("645967A4-1392-4310-A798-8053CE3E93FD");

    [DllImport("dxgi.dll")]
    private static extern int CreateDXGIFactory1(ref Guid riid, out IntPtr factory);

    [DllImport("d3d11.dll")]
    private static extern int D3D11CreateDevice(
        IntPtr adapter,
        int driverType,
        IntPtr software,
        uint flags,
        [In] int[] featureLevels,
        uint featureLevelCount,
        uint sdkVersion,
        out IntPtr device,
        out int selectedFeatureLevel,
        out IntPtr immediateContext);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int EnumAdapters1Delegate(IntPtr factory, uint adapterIndex, out IntPtr adapter);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int GetDesc1Delegate(IntPtr adapter, out DxgiAdapterDesc1 description);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int QueryVideoMemoryInfoDelegate(
        IntPtr adapter,
        uint nodeIndex,
        int memorySegmentGroup,
        out DxgiQueryVideoMemoryInfo memoryInfo);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct DxgiAdapterDesc1
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string Description;
        public uint VendorId;
        public uint DeviceId;
        public uint SubSysId;
        public uint Revision;
        public UIntPtr DedicatedVideoMemory;
        public UIntPtr DedicatedSystemMemory;
        public UIntPtr SharedSystemMemory;
        public long AdapterLuid;
        public uint Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct DxgiQueryVideoMemoryInfo
    {
        public ulong Budget;
        public ulong CurrentUsage;
        public ulong AvailableForReservation;
        public ulong CurrentReservation;
    }

    private static T GetVtableDelegate<T>(IntPtr instance, int methodIndex) where T : Delegate
    {
        IntPtr vtable = Marshal.ReadIntPtr(instance);
        IntPtr method = Marshal.ReadIntPtr(vtable, methodIndex * IntPtr.Size);
        return Marshal.GetDelegateForFunctionPointer<T>(method);
    }

    private static string FeatureLevelName(int value) => value switch
    {
        0xc200 => "12_2",
        0xc100 => "12_1",
        0xc000 => "12_0",
        0xb100 => "11_1",
        0xb000 => "11_0",
        0xa100 => "10_1",
        0xa000 => "10_0",
        0x9300 => "9_3",
        0x9200 => "9_2",
        0x9100 => "9_1",
        _ => $"UNKNOWN_0x{value:X}"
    };

    private static int Main()
    {
        var adapters = new List<object>();
        var memoryQueries = new List<object>();
        var identityStatus = "FAIL";
        var memoryStatus = "UNSUPPORTED";
        string? factoryError = null;

        IntPtr factory = IntPtr.Zero;
        Guid factoryGuid = Factory1Guid;
        int factoryHr = CreateDXGIFactory1(ref factoryGuid, out factory);
        if (factoryHr >= 0 && factory != IntPtr.Zero)
        {
            try
            {
                var enumerate = GetVtableDelegate<EnumAdapters1Delegate>(factory, 12);
                for (uint index = 0; ; index++)
                {
                    int enumerateHr = enumerate(factory, index, out IntPtr adapter);
                    if (enumerateHr == DxgiErrorNotFound)
                        break;
                    if (enumerateHr < 0)
                    {
                        factoryError = $"EnumAdapters1 HRESULT=0x{enumerateHr:X8}";
                        break;
                    }

                    try
                    {
                        var getDescription = GetVtableDelegate<GetDesc1Delegate>(adapter, 10);
                        int descriptionHr = getDescription(adapter, out DxgiAdapterDesc1 description);
                        if (descriptionHr < 0)
                        {
                            adapters.Add(new { index, status = "FAIL", hresult = $"0x{descriptionHr:X8}" });
                            continue;
                        }

                        adapters.Add(new
                        {
                            index,
                            status = "PASS",
                            description = description.Description.TrimEnd('\0'),
                            vendorId = $"0x{description.VendorId:X4}",
                            deviceId = $"0x{description.DeviceId:X4}",
                            subsystemId = $"0x{description.SubSysId:X8}",
                            revision = description.Revision,
                            dedicatedVideoMemoryBytes = description.DedicatedVideoMemory.ToUInt64(),
                            dedicatedSystemMemoryBytes = description.DedicatedSystemMemory.ToUInt64(),
                            sharedSystemMemoryBytes = description.SharedSystemMemory.ToUInt64(),
                            flags = description.Flags
                        });

                        Guid adapter3Guid = Adapter3Guid;
                        int queryInterfaceHr = Marshal.QueryInterface(adapter, ref adapter3Guid, out IntPtr adapter3);
                        if (queryInterfaceHr < 0 || adapter3 == IntPtr.Zero)
                        {
                            memoryQueries.Add(new { index, status = "UNSUPPORTED", hresult = $"0x{queryInterfaceHr:X8}" });
                            continue;
                        }

                        try
                        {
                            var queryMemory = GetVtableDelegate<QueryVideoMemoryInfoDelegate>(adapter3, 14);
                            int memoryHr = queryMemory(adapter3, 0, 0, out DxgiQueryVideoMemoryInfo info);
                            memoryQueries.Add(memoryHr >= 0
                                ? new
                                {
                                    index,
                                    status = "PASS",
                                    hresult = "0x00000000",
                                    budgetBytes = info.Budget,
                                    currentUsageBytes = info.CurrentUsage,
                                    availableForReservationBytes = info.AvailableForReservation,
                                    currentReservationBytes = info.CurrentReservation
                                }
                                : new
                                {
                                    index,
                                    status = "UNSUPPORTED",
                                    hresult = $"0x{memoryHr:X8}",
                                    budgetBytes = 0UL,
                                    currentUsageBytes = 0UL,
                                    availableForReservationBytes = 0UL,
                                    currentReservationBytes = 0UL
                                });
                        }
                        finally
                        {
                            Marshal.Release(adapter3);
                        }
                    }
                    finally
                    {
                        Marshal.Release(adapter);
                    }
                }
            }
            finally
            {
                Marshal.Release(factory);
            }
        }
        else
        {
            factoryError = $"CreateDXGIFactory1 HRESULT=0x{factoryHr:X8}";
        }

        if (adapters.Count > 0)
            identityStatus = "PASS";
        if (memoryQueries.Any(item => item.GetType().GetProperty("status")?.GetValue(item)?.ToString() == "PASS"))
            memoryStatus = "PASS";

        int[] featureLevels = [0xc200, 0xc100, 0xc000, 0xb100, 0xb000, 0xa100, 0xa000, 0x9300, 0x9200, 0x9100];
        int deviceHr = D3D11CreateDevice(IntPtr.Zero, 1, IntPtr.Zero, 0, featureLevels,
            (uint)featureLevels.Length, 7, out IntPtr device, out int selectedFeatureLevel, out IntPtr context);
        if (context != IntPtr.Zero) Marshal.Release(context);
        if (device != IntPtr.Zero) Marshal.Release(device);
        string apiStatus = deviceHr >= 0 ? "PASS" : "FAIL";

        var result = new
        {
            generatedAt = DateTimeOffset.Now,
            safety = new { persistentMutation = false },
            probes = new object[]
            {
                new
                {
                    featureId = "C-GPU-001",
                    status = identityStatus,
                    mechanism = "DXGI CreateDXGIFactory1/EnumAdapters1/GetDesc1",
                    evidence = new { dxgiAdapters = adapters, factoryError },
                    limitations = new[] { "Current host only; no absent, multi-GPU, or ARM64 fixture." }
                },
                new
                {
                    featureId = "C-GPU-002",
                    status = memoryStatus,
                    mechanism = "IDXGIAdapter3::QueryVideoMemoryInfo",
                    evidence = new { memoryQueries },
                    limitations = new[] { "One instantaneous budget/usage query; no sustained pressure workload." }
                },
                new
                {
                    featureId = "C-GPU-006",
                    status = apiStatus,
                    mechanism = "D3D11CreateDevice feature-level negotiation",
                    evidence = new
                    {
                        d3d11 = new
                        {
                            hresult = $"0x{deviceHr:X8}",
                            featureLevel = deviceHr >= 0 ? FeatureLevelName(selectedFeatureLevel) : "UNAVAILABLE"
                        }
                    },
                    limitations = new[] { "D3D11 hardware path only; Vulkan/OpenGL and missing-runtime fixtures remain untested." }
                }
            }
        };

        Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));
        return 0;
    }
}
