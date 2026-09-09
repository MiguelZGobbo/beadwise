namespace BeadWise.Windows.Processes;

internal sealed class ProcessReadTarget : IDisposable
{
    private readonly Action _dispose;
    private bool _disposed;

    public ProcessReadTarget(
        int processId,
        Func<string> readProcessName,
        Func<DateTimeOffset> readCreationTime,
        Func<string> readExecutablePath,
        Action? dispose = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(processId);
        ArgumentNullException.ThrowIfNull(readProcessName);
        ArgumentNullException.ThrowIfNull(readCreationTime);
        ArgumentNullException.ThrowIfNull(readExecutablePath);

        ProcessId = processId;
        ReadProcessName = readProcessName;
        ReadCreationTime = readCreationTime;
        ReadExecutablePath = readExecutablePath;
        _dispose = dispose ?? (() => { });
    }

    public int ProcessId { get; }

    public Func<string> ReadProcessName { get; }

    public Func<DateTimeOffset> ReadCreationTime { get; }

    public Func<string> ReadExecutablePath { get; }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _dispose();
        _disposed = true;
    }
}
