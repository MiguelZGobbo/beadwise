namespace BeadWise.Core.Logging;

public interface IExecutionLog
{
    void Write(ExecutionLogEntry entry);
}

public sealed class NullExecutionLog : IExecutionLog
{
    public static NullExecutionLog Instance { get; } = new();
    private NullExecutionLog() { }
    public void Write(ExecutionLogEntry entry) { }
}
