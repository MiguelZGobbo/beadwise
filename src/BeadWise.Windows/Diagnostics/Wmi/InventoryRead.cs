using System.ComponentModel;
using System.Management;
using System.Runtime.InteropServices;
using System.Security;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;

namespace BeadWise.Windows.Diagnostics;

internal static class InventoryRead
{
    internal static bool IsExpectedFailure(Exception exception) => exception switch
    {
        ManagementException { ErrorCode: ManagementStatus.OutOfMemory or ManagementStatus.CriticalError } => false,
        COMException { HResult: unchecked((int)0x8007000E) or unchecked((int)0x8000FFFF) } => false,
        ManagementException or COMException or Win32Exception or UnauthorizedAccessException
            or SecurityException or TimeoutException or PlatformNotSupportedException
            or NotSupportedException or DllNotFoundException or EntryPointNotFoundException => true,
        _ => false
    };

    internal static Error Error(string code, Exception exception)
    {
        var category = exception switch
        {
            UnauthorizedAccessException or SecurityException => ErrorCategory.AccessDenied,
            Win32Exception { NativeErrorCode: 5 } => ErrorCategory.AccessDenied,
            COMException { HResult: unchecked((int)0x80070005) or unchecked((int)0x80041003) } => ErrorCategory.AccessDenied,
            ManagementException { ErrorCode: ManagementStatus.AccessDenied } => ErrorCategory.AccessDenied,
            ManagementException { ErrorCode: ManagementStatus.Timedout } or TimeoutException => ErrorCategory.Timeout,
            ManagementException { ErrorCode: ManagementStatus.NotSupported or ManagementStatus.InvalidClass or ManagementStatus.InvalidNamespace }
                or PlatformNotSupportedException or NotSupportedException or DllNotFoundException or EntryPointNotFoundException => ErrorCategory.Unsupported,
            _ => ErrorCategory.Unavailable
        };
        return new Error($"{code}_{category.ToString().ToUpperInvariant()}", category,
            exception.GetType().FullName, recoverable: category != ErrorCategory.Unsupported);
    }

    internal static Observation<T> Missing<T>(string source, DateTimeOffset timestamp, Error? error = null) =>
        error switch
        {
            null => Observation<T>.Unknown(source, timestamp),
            { Category: ErrorCategory.Unsupported } => Observation<T>.Unsupported(source, timestamp, error),
            _ => Observation<T>.PartialWithoutValue(source, timestamp, error)
        };

    internal static Observation<string> Text(object? value, string source, DateTimeOffset timestamp, Error? error = null) =>
        value is string text && !string.IsNullOrWhiteSpace(text)
            ? Observation<string>.Available(text.Trim(), source, timestamp)
            : Missing<string>(source, timestamp, error);

    internal static Observation<bool> Boolean(object? value, string source, DateTimeOffset timestamp, Error? error = null) =>
        value is bool boolean
            ? Observation<bool>.Available(boolean, source, timestamp)
            : Missing<bool>(source, timestamp, error);

    internal static Observation<int> Count(object? value, string source, DateTimeOffset timestamp) => value switch
    {
        uint number when number <= int.MaxValue && number > 0 => Observation<int>.Available((int)number, source, timestamp),
        ushort number when number > 0 => Observation<int>.Available(number, source, timestamp),
        int number when number > 0 => Observation<int>.Available(number, source, timestamp),
        _ => Missing<int>(source, timestamp)
    };

    internal static bool Available<T>(Observation<T> observation) => observation.Availability == ObservationAvailability.Available;

    internal static Error PartialError(string code) => new(code, ErrorCategory.PartialFailure, recoverable: true);
}

internal sealed record WmiRead(
    IReadOnlyList<IReadOnlyDictionary<string, object?>> Rows,
    string Source,
    DateTimeOffset Timestamp,
    Error? Error)
{
    internal static WmiRead Capture(IWmiQuery query, string className, params string[] properties)
    {
        var source = $"WMI:{className}";
        var timestamp = DateTimeOffset.UtcNow;
        try
        {
            return new WmiRead(query.Query(className, properties), source, timestamp, null);
        }
        catch (Exception exception) when (InventoryRead.IsExpectedFailure(exception))
        {
            return new WmiRead([], source, timestamp, InventoryRead.Error("WMI_READ", exception));
        }
    }

    internal object? First(string property) => Rows.FirstOrDefault()?.GetValueOrDefault(property);
    internal Observation<string> Text(string property) => InventoryRead.Text(First(property), Source, Timestamp, Error);
    internal Observation<bool> Boolean(string property) => InventoryRead.Boolean(First(property), Source, Timestamp, Error);
}
