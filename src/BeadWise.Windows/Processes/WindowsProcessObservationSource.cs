using System.ComponentModel;
using System.Diagnostics;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;

namespace BeadWise.Windows.Processes;

public sealed class WindowsProcessObservationSource : IProcessObservationSource
{
    public const string ObservationSource = "System.Diagnostics.Process";

    private readonly Func<IReadOnlyList<ProcessReadTarget>> _enumerateProcesses;

    public WindowsProcessObservationSource()
        : this(EnumerateProcesses)
    {
    }

    internal WindowsProcessObservationSource(
        Func<IReadOnlyList<ProcessReadTarget>> enumerateProcesses)
    {
        ArgumentNullException.ThrowIfNull(enumerateProcesses);
        _enumerateProcesses = enumerateProcesses;
    }

    public Result<IReadOnlyList<ProcessObservation>> Observe()
    {
        IReadOnlyList<ProcessReadTarget> targets;
        try
        {
            targets = _enumerateProcesses();
        }
        catch (Exception exception) when (IsExpectedProcessReadFailure(exception))
        {
            var error = CreateError("PROCESS_ENUMERATION", exception);
            return error.Category == ErrorCategory.Unsupported
                ? Result<IReadOnlyList<ProcessObservation>>.Unsupported(error)
                : Result<IReadOnlyList<ProcessObservation>>.Failed(error);
        }

        var timestamp = DateTimeOffset.UtcNow;
        var observations = new List<ProcessObservation>(targets.Count);
        var isPartial = false;

        foreach (var target in targets)
        {
            using (target)
            {
                var identity = ReadIdentity(target, timestamp);
                var processName = ReadString(
                    target.ReadProcessName,
                    "PROCESS_NAME",
                    timestamp);
                var executablePath = ReadString(
                    target.ReadExecutablePath,
                    "PROCESS_EXECUTABLE_PATH",
                    timestamp);

                observations.Add(new ProcessObservation(
                    target.ProcessId,
                    identity,
                    processName,
                    executablePath));

                isPartial |= identity.Availability != ObservationAvailability.Available
                    || processName.Availability != ObservationAvailability.Available
                    || executablePath.Availability != ObservationAvailability.Available;
            }
        }

        IReadOnlyList<ProcessObservation> resultValue = observations.AsReadOnly();
        if (!isPartial)
        {
            return Result<IReadOnlyList<ProcessObservation>>.Success(resultValue);
        }

        return Result<IReadOnlyList<ProcessObservation>>.Partial(
            resultValue,
            new Error(
                "PROCESS_OBSERVATION_PARTIAL",
                ErrorCategory.PartialFailure,
                "One or more process fields were unavailable.",
                recoverable: true));
    }

    private static Observation<ProcessIdentity> ReadIdentity(
        ProcessReadTarget target,
        DateTimeOffset timestamp)
    {
        try
        {
            var creationTime = target.ReadCreationTime().ToUniversalTime();
            return Observation<ProcessIdentity>.Available(
                new ProcessIdentity(target.ProcessId, creationTime),
                ObservationSource,
                timestamp);
        }
        catch (Exception exception) when (IsExpectedProcessReadFailure(exception))
        {
            return WithoutValue<ProcessIdentity>(
                "PROCESS_CREATION_TIME",
                timestamp,
                exception);
        }
    }

    private static Observation<string> ReadString(
        Func<string> read,
        string errorCodePrefix,
        DateTimeOffset timestamp)
    {
        try
        {
            var value = read();
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException("The process field did not contain a value.");
            }

            return Observation<string>.Available(value, ObservationSource, timestamp);
        }
        catch (Exception exception) when (IsExpectedProcessReadFailure(exception))
        {
            return WithoutValue<string>(errorCodePrefix, timestamp, exception);
        }
    }

    private static Observation<T> WithoutValue<T>(
        string errorCodePrefix,
        DateTimeOffset timestamp,
        Exception exception)
    {
        var error = CreateError(errorCodePrefix, exception);
        return error.Category == ErrorCategory.Unsupported
            ? Observation<T>.Unsupported(ObservationSource, timestamp, error)
            : Observation<T>.PartialWithoutValue(ObservationSource, timestamp, error);
    }

    private static Error CreateError(string codePrefix, Exception exception)
    {
        var category = exception switch
        {
            UnauthorizedAccessException => ErrorCategory.AccessDenied,
            Win32Exception { NativeErrorCode: 5 } => ErrorCategory.AccessDenied,
            PlatformNotSupportedException => ErrorCategory.Unsupported,
            NotSupportedException => ErrorCategory.Unsupported,
            _ => ErrorCategory.Unavailable
        };
        var codeSuffix = category switch
        {
            ErrorCategory.AccessDenied => "ACCESS_DENIED",
            ErrorCategory.Unsupported => "UNSUPPORTED",
            ErrorCategory.Unavailable => "UNAVAILABLE",
            _ => throw new InvalidOperationException("Unexpected process observation error category.")
        };

        return new Error(
            $"{codePrefix}_{codeSuffix}",
            category,
            exception.GetType().FullName,
            recoverable: category != ErrorCategory.Unsupported);
    }

    private static bool IsExpectedProcessReadFailure(Exception exception) =>
        exception is Win32Exception
            or UnauthorizedAccessException
            or InvalidOperationException
            or PlatformNotSupportedException
            or NotSupportedException;

    private static IReadOnlyList<ProcessReadTarget> EnumerateProcesses()
    {
        var processes = Process.GetProcesses();
        try
        {
            return processes.Select(CreateTarget).ToArray();
        }
        catch
        {
            foreach (var process in processes)
            {
                process.Dispose();
            }

            throw;
        }
    }

    private static ProcessReadTarget CreateTarget(Process process) =>
        new(
            process.Id,
            () => process.ProcessName,
            () => new DateTimeOffset(process.StartTime.ToUniversalTime()),
            () => process.MainModule?.FileName
                ?? throw new InvalidOperationException("The executable path is unavailable."),
            process.Dispose);
}
