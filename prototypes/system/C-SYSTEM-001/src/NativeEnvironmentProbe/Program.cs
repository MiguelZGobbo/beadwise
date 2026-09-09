using System.Runtime.InteropServices;
using System.Text.Json;

var nativeSystemInfo = NativeMethods.GetSystemInfo();
var version = NativeMethods.GetRtlVersion();

var result = new
{
    capturedAt = DateTimeOffset.Now.ToString("o"),
    api = "GetNativeSystemInfo/RtlGetVersion",
    architecture = RuntimeInformation.OSArchitecture.ToString(),
    processorArchitecture = nativeSystemInfo.wProcessorArchitecture,
    processorLevel = nativeSystemInfo.wProcessorLevel,
    processorRevision = nativeSystemInfo.wProcessorRevision,
    processorCount = nativeSystemInfo.dwNumberOfProcessors,
    pageSize = nativeSystemInfo.dwPageSize,
    minimumApplicationAddress = nativeSystemInfo.lpMinimumApplicationAddress.ToString("X"),
    maximumApplicationAddress = nativeSystemInfo.lpMaximumApplicationAddress.ToString("X"),
    allocationGranularity = nativeSystemInfo.dwAllocationGranularity,
    windowsMajor = version.dwMajorVersion,
    windowsMinor = version.dwMinorVersion,
    windowsBuild = version.dwBuildNumber,
    rtlStatus = version.Status
};

Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));

internal static class NativeMethods
{
    [DllImport("kernel32.dll")]
    private static extern void GetNativeSystemInfo(out SYSTEM_INFO lpSystemInfo);

    [DllImport("ntdll.dll")]
    private static extern int RtlGetVersion(ref RTL_OSVERSIONINFOEXW lpVersionInformation);

    internal static SYSTEM_INFO GetSystemInfo()
    {
        GetNativeSystemInfo(out var info);
        return info;
    }

    internal static VersionResult GetRtlVersion()
    {
        var version = new RTL_OSVERSIONINFOEXW
        {
            dwOSVersionInfoSize = (uint)Marshal.SizeOf<RTL_OSVERSIONINFOEXW>()
        };

        var status = RtlGetVersion(ref version);
        return new VersionResult(version.dwMajorVersion, version.dwMinorVersion, version.dwBuildNumber, status);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SYSTEM_INFO
    {
        public ushort wProcessorArchitecture;
        public ushort wReserved;
        public uint dwPageSize;
        public nuint lpMinimumApplicationAddress;
        public nuint lpMaximumApplicationAddress;
        public nuint dwActiveProcessorMask;
        public uint dwNumberOfProcessors;
        public uint dwProcessorType;
        public uint dwAllocationGranularity;
        public ushort wProcessorLevel;
        public ushort wProcessorRevision;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct RTL_OSVERSIONINFOEXW
    {
        public uint dwOSVersionInfoSize;
        public uint dwMajorVersion;
        public uint dwMinorVersion;
        public uint dwBuildNumber;
        public uint dwPlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szCSDVersion;
    }

    internal sealed record VersionResult(uint dwMajorVersion, uint dwMinorVersion, uint dwBuildNumber, int Status);
}
