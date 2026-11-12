using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using BeadWise.Core.Errors;
using Microsoft.Win32.SafeHandles;

namespace BeadWise.Windows.Diagnostics;

internal static class PresentDeviceReader
{
    private const uint Present = 0x2;
    private const uint AllClasses = 0x4;
    private const int NoMoreItems = 259;
    private const int InsufficientBuffer = 122;
    private const int InvalidData = 13;
    private const int NotFound = 1168;
    private const uint DeviceHasProblem = 0x400;
    private const uint DeviceDescription = 0;
    private const uint DeviceClass = 7;
    private const uint FriendlyName = 12;
    private const uint RegistryString = 1;
    private const uint MaximumBufferSize = 65536;

    internal static DeviceEnumeration Read()
    {
        using var set = SetupDiGetClassDevs(IntPtr.Zero, null, IntPtr.Zero, Present | AllClasses);
        if (set.IsInvalid)
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        var devices = new List<NativeDevice>();
        for (uint index = 0; ; index++)
        {
            var info = new DeviceInfoData { Size = (uint)Marshal.SizeOf<DeviceInfoData>() };
            if (!SetupDiEnumDeviceInfo(set, index, ref info))
            {
                var code = Marshal.GetLastWin32Error();
                if (code == NoMoreItems)
                {
                    return new DeviceEnumeration(devices.AsReadOnly(), null);
                }

                var exception = new Win32Exception(code);
                if (devices.Count == 0)
                {
                    throw exception;
                }

                return new DeviceEnumeration(devices.AsReadOnly(), InventoryRead.Error("DEVICE_ENUMERATION", exception));
            }

            var identity = ReadIdentity(set, ref info);
            var friendlyName = ReadProperty(set, ref info, FriendlyName);
            var description = friendlyName.Value is null ? ReadProperty(set, ref info, DeviceDescription) : default;
            var deviceClass = ReadProperty(set, ref info, DeviceClass);
            var status = ReadStatus(info.DevInst);
            devices.Add(new NativeDevice(identity.Value, friendlyName.Value ?? description.Value, deviceClass.Value,
                status.ProblemCode, status.Status, identity.Error, friendlyName.Error ?? description.Error,
                deviceClass.Error, status.Error));
        }
    }

    private static (string? Value, Error? Error) ReadIdentity(DeviceInfoSet set, ref DeviceInfoData info)
    {
        SetupDiGetDeviceInstanceId(set, ref info, null, 0, out var required);
        var code = Marshal.GetLastWin32Error();
        if (code != InsufficientBuffer || required == 0 || required > MaximumBufferSize)
        {
            return (null, InventoryRead.Error("DEVICE_ID", new Win32Exception(code)));
        }

        var buffer = new StringBuilder((int)required);
        return SetupDiGetDeviceInstanceId(set, ref info, buffer, required, out _)
            ? (buffer.ToString(), null)
            : (null, InventoryRead.Error("DEVICE_ID", new Win32Exception(Marshal.GetLastWin32Error())));
    }

    private static (string? Value, Error? Error) ReadProperty(DeviceInfoSet set, ref DeviceInfoData info, uint property)
    {
        SetupDiGetDeviceRegistryProperty(set, ref info, property, out _, null, 0, out var required);
        var code = Marshal.GetLastWin32Error();
        if (code is InvalidData or NotFound)
        {
            return (null, null);
        }

        if (code != InsufficientBuffer || required == 0 || required > MaximumBufferSize)
        {
            return (null, InventoryRead.Error("DEVICE_PROPERTY", new Win32Exception(code)));
        }

        var buffer = new byte[(int)required];
        if (!SetupDiGetDeviceRegistryProperty(set, ref info, property, out var type, buffer, required, out _))
        {
            return (null, InventoryRead.Error("DEVICE_PROPERTY", new Win32Exception(Marshal.GetLastWin32Error())));
        }

        return type == RegistryString
            ? (Encoding.Unicode.GetString(buffer).TrimEnd('\0'), null)
            : (null, null);
    }

    private static (uint? Status, uint? ProblemCode, Error? Error) ReadStatus(uint devInst)
    {
        var result = CM_Get_DevNode_Status(out var status, out var problem, devInst, 0);
        if (result != 0)
        {
            var win32Code = CM_MapCrToWin32Err(result, 31);
            return (null, null, InventoryRead.Error("DEVICE_STATUS", new Win32Exception((int)win32Code)));
        }

        if ((status & DeviceHasProblem) != 0 && problem == 0)
        {
            return (status, null, new Error("DEVICE_PROBLEM_UNAVAILABLE", ErrorCategory.Unavailable, recoverable: true));
        }

        return (status, (status & DeviceHasProblem) == 0 ? 0u : problem, null);
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct DeviceInfoData
    {
        public uint Size;
        public Guid ClassGuid;
        public uint DevInst;
        public UIntPtr Reserved;
    }

    private sealed class DeviceInfoSet : SafeHandleZeroOrMinusOneIsInvalid
    {
        private DeviceInfoSet() : base(true) { }
        protected override bool ReleaseHandle() => SetupDiDestroyDeviceInfoList(handle);
    }

    [DllImport("setupapi.dll", EntryPoint = "SetupDiGetClassDevsW", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern DeviceInfoSet SetupDiGetClassDevs(IntPtr classGuid, string? enumerator, IntPtr parent, uint flags);

    [DllImport("setupapi.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetupDiEnumDeviceInfo(DeviceInfoSet set, uint index, ref DeviceInfoData data);

    [DllImport("setupapi.dll", EntryPoint = "SetupDiGetDeviceInstanceIdW", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetupDiGetDeviceInstanceId(DeviceInfoSet set, ref DeviceInfoData data,
        StringBuilder? instanceId, uint instanceIdSize, out uint requiredSize);

    [DllImport("setupapi.dll", EntryPoint = "SetupDiGetDeviceRegistryPropertyW", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetupDiGetDeviceRegistryProperty(DeviceInfoSet set, ref DeviceInfoData data,
        uint property, out uint propertyType, byte[]? buffer, uint bufferSize, out uint requiredSize);

    [DllImport("setupapi.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetupDiDestroyDeviceInfoList(IntPtr set);

    [DllImport("cfgmgr32.dll")]
    private static extern uint CM_Get_DevNode_Status(out uint status, out uint problemNumber, uint devInst, uint flags);

    [DllImport("cfgmgr32.dll")]
    private static extern uint CM_MapCrToWin32Err(uint configRet, uint defaultError);
}
