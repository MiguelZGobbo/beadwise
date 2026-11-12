using System.ComponentModel;
using System.Runtime.InteropServices;
using BeadWise.Core.Diagnostics;
using BeadWise.Core.Errors;
using BeadWise.Core.Results;

namespace BeadWise.Windows.Diagnostics;

internal static class AudioNativeReader
{
    private const int ChangedMode = unchecked((int)0x80010106);
    private const int NotFound = unchecked((int)0x80070490);
    private const uint AllStates = 0x0f;

    internal static AudioNativeSnapshot Read()
    {
        if (!OperatingSystem.IsWindows()) throw new PlatformNotSupportedException();
        var initialize = CoInitializeEx(IntPtr.Zero, 0);
        if (initialize < 0 && initialize != ChangedMode) Marshal.ThrowExceptionForHR(initialize);
        var uninitialize = initialize >= 0;
        IMMDeviceEnumerator? enumerator = null;
        try
        {
            enumerator = (IMMDeviceEnumerator)(object)new MMDeviceEnumerator();
            var endpoints = new List<AudioNativeEndpoint>();
            var defaults = new List<AudioNativeDefault>();
            var errors = new List<Error>();
            foreach (var direction in Enum.GetValues<AudioDirection>())
            {
                try { ReadEndpoints(enumerator, direction, endpoints, errors); }
                catch (Exception exception) when (IsExpectedFailure(exception))
                {
                    errors.Add(ErrorFromException("AUDIO_ENUMERATION", exception));
                }
                foreach (var role in Enum.GetValues<AudioRole>())
                {
                    var identity = ReadField(() => ReadDefault(enumerator, direction, role), "AUDIO_DEFAULT");
                    if (identity.Error is not null) errors.Add(identity.Error);
                    defaults.Add(new AudioNativeDefault(direction, role, identity));
                }
            }
            return new AudioNativeSnapshot(endpoints.AsReadOnly(), defaults.AsReadOnly(), errors.AsReadOnly());
        }
        finally
        {
            Release(enumerator);
            if (uninitialize) CoUninitialize();
        }
    }

    private static void ReadEndpoints(IMMDeviceEnumerator enumerator, AudioDirection direction,
        List<AudioNativeEndpoint> endpoints, List<Error> errors)
    {
        IMMDeviceCollection? collection = null;
        try
        {
            var hr = enumerator.EnumAudioEndpoints(direction, AllStates, out collection);
            if (hr < 0 || collection is null) { errors.Add(FromHResult("AUDIO_ENUMERATION", hr)); return; }
            hr = collection.GetCount(out var count);
            if (hr < 0) { errors.Add(FromHResult("AUDIO_ENUMERATION", hr)); return; }
            for (uint index = 0; index < count; index++)
            {
                IMMDevice? device = null;
                try
                {
                    hr = collection.Item(index, out device);
                    if (hr < 0 || device is null) { errors.Add(FromHResult("AUDIO_ENDPOINT", hr)); continue; }
                    endpoints.Add(new AudioNativeEndpoint(direction,
                        ReadField(() => ReadId(device), "AUDIO_IDENTITY"),
                        ReadField(() => ReadName(device), "AUDIO_NAME"),
                        ReadField(() => ReadState(device), "AUDIO_STATE")));
                }
                catch (Exception exception) when (IsExpectedFailure(exception))
                {
                    errors.Add(ErrorFromException("AUDIO_ENDPOINT", exception));
                }
                finally { Release(device); }
            }
        }
        finally { Release(collection); }
    }

    private static Result<string> ReadDefault(IMMDeviceEnumerator enumerator, AudioDirection direction, AudioRole role)
    {
        IMMDevice? device = null;
        try
        {
            var hr = enumerator.GetDefaultAudioEndpoint(direction, role, out device);
            if (hr == NotFound) return Result<string>.Success(string.Empty);
            return hr < 0 || device is null
                ? Result<string>.Failed(FromHResult("AUDIO_DEFAULT", hr))
                : ReadId(device);
        }
        finally { Release(device); }
    }

    private static Result<T> ReadField<T>(Func<Result<T>> read, string prefix)
    {
        try { return read(); }
        catch (Exception exception) when (IsExpectedFailure(exception))
        {
            return Result<T>.Failed(ErrorFromException(prefix, exception));
        }
    }

    private static Result<string> ReadId(IMMDevice device)
    {
        IntPtr pointer = IntPtr.Zero;
        try
        {
            var hr = device.GetId(out pointer);
            if (hr < 0) return Result<string>.Failed(FromHResult("AUDIO_IDENTITY", hr));
            var value = Marshal.PtrToStringUni(pointer);
            return string.IsNullOrWhiteSpace(value)
                ? Result<string>.Failed(FromHResult("AUDIO_IDENTITY", 0))
                : Result<string>.Success(value);
        }
        finally { Marshal.FreeCoTaskMem(pointer); }
    }

    private static Result<uint> ReadState(IMMDevice device)
    {
        var hr = device.GetState(out var state);
        return hr < 0 ? Result<uint>.Failed(FromHResult("AUDIO_STATE", hr)) : Result<uint>.Success(state);
    }

    private static Result<string> ReadName(IMMDevice device)
    {
        IPropertyStore? properties = null;
        try
        {
            var hr = device.OpenPropertyStore(0, out properties);
            if (hr < 0 || properties is null) return Result<string>.Failed(FromHResult("AUDIO_NAME", hr));
            var value = new PropVariant();
            try
            {
                var key = new PropertyKey { FormatId = new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"), PropertyId = 14 };
                hr = properties.GetValue(ref key, ref value);
                if (hr < 0) return Result<string>.Failed(FromHResult("AUDIO_NAME", hr));
                var name = value.Type == 31 ? Marshal.PtrToStringUni(value.Value.Pointer) : null;
                return string.IsNullOrWhiteSpace(name)
                    ? Result<string>.Failed(FromHResult("AUDIO_NAME", 0))
                    : Result<string>.Success(name);
            }
            finally { PropVariantClear(ref value); }
        }
        finally { Release(properties); }
    }

    internal static bool IsExpectedFailure(Exception exception) => exception is COMException
        or Win32Exception or UnauthorizedAccessException or PlatformNotSupportedException
        or DllNotFoundException or EntryPointNotFoundException;

    internal static Error ErrorFromException(string prefix, Exception exception)
    {
        var category = exception switch
        {
            UnauthorizedAccessException => ErrorCategory.AccessDenied,
            COMException { HResult: unchecked((int)0x80070005) } => ErrorCategory.AccessDenied,
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
        return new Error($"{prefix}_{suffix}", category,
            exception.GetType().FullName, recoverable: category != ErrorCategory.Unsupported);
    }

    private static Error FromHResult(string prefix, int hr)
    {
        var category = hr == unchecked((int)0x80070005) ? ErrorCategory.AccessDenied : ErrorCategory.Unavailable;
        var suffix = category == ErrorCategory.AccessDenied ? "ACCESS_DENIED" : "UNAVAILABLE";
        return new Error($"{prefix}_{suffix}", category, $"HRESULT 0x{hr:X8}", recoverable: true);
    }

    private static void Release(object? instance)
    {
        if (instance is not null && Marshal.IsComObject(instance)) Marshal.ReleaseComObject(instance);
    }

    [DllImport("ole32.dll")] private static extern int CoInitializeEx(IntPtr reserved, uint coInit);
    [DllImport("ole32.dll")] private static extern void CoUninitialize();
    [DllImport("ole32.dll")] private static extern int PropVariantClear(ref PropVariant value);
}

[ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
internal sealed class MMDeviceEnumerator { }

[ComImport, Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IMMDeviceEnumerator
{
    [PreserveSig] int EnumAudioEndpoints(AudioDirection flow, uint stateMask, out IMMDeviceCollection? devices);
    [PreserveSig] int GetDefaultAudioEndpoint(AudioDirection flow, AudioRole role, out IMMDevice? endpoint);
}

[ComImport, Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IMMDeviceCollection
{
    [PreserveSig] int GetCount(out uint count);
    [PreserveSig] int Item(uint index, out IMMDevice? device);
}

[ComImport, Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IMMDevice
{
    [PreserveSig] int Activate(ref Guid iid, uint context, IntPtr parameters, out IntPtr result);
    [PreserveSig] int OpenPropertyStore(uint access, out IPropertyStore? properties);
    [PreserveSig] int GetId(out IntPtr id);
    [PreserveSig] int GetState(out uint state);
}

[ComImport, Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IPropertyStore
{
    [PreserveSig] int GetCount(out uint count);
    [PreserveSig] int GetAt(uint index, out PropertyKey key);
    [PreserveSig] int GetValue(ref PropertyKey key, ref PropVariant value);
}

[StructLayout(LayoutKind.Sequential)]
internal struct PropertyKey { internal Guid FormatId; internal uint PropertyId; }

[StructLayout(LayoutKind.Sequential)]
internal struct PropVariant
{
    internal ushort Type;
    private ushort _reserved1, _reserved2, _reserved3;
    internal PropVariantValue Value;
}

[StructLayout(LayoutKind.Explicit)]
internal struct PropVariantValue
{
    [FieldOffset(0)] internal IntPtr Pointer;
    [FieldOffset(0)] internal PropVariantCountedArray CountedArray;
}

[StructLayout(LayoutKind.Sequential)]
internal struct PropVariantCountedArray { internal uint Count; internal IntPtr Elements; }
