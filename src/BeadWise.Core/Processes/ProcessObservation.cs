using BeadWise.Core.Observations;

namespace BeadWise.Core.Processes;

public sealed class ProcessObservation
{
    public ProcessObservation(
        int processId,
        Observation<ProcessIdentity> identity,
        Observation<string> processName,
        Observation<string> executablePath)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(processId);
        ArgumentNullException.ThrowIfNull(identity);
        ArgumentNullException.ThrowIfNull(processName);
        ArgumentNullException.ThrowIfNull(executablePath);

        if (identity.HasValue && identity.Value.ProcessId != processId)
        {
            throw new ArgumentException("The process identity must belong to the observed process.", nameof(identity));
        }

        ProcessId = processId;
        Identity = identity;
        ProcessName = processName;
        ExecutablePath = executablePath;
    }

    public int ProcessId { get; }

    public Observation<ProcessIdentity> Identity { get; }

    public Observation<string> ProcessName { get; }

    public Observation<string> ExecutablePath { get; }
}
