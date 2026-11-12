using BeadWise.Core.Contracts;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;
using BeadWise.Core.Logging;

namespace BeadWise.Application.Processes;

public sealed class ObserveProcessesUseCase
{
    private readonly IProcessObservationSource _source;
    private readonly IExecutionLog _log;

    public ObserveProcessesUseCase(IProcessObservationSource source, IExecutionLog? log = null)
    {
        ArgumentNullException.ThrowIfNull(source);
        _source = source;
        _log = log ?? NullExecutionLog.Instance;
    }

    public Result<IReadOnlyList<ProcessObservation>> Execute()
    {
        var runId = Guid.NewGuid();
        _log.Write(new ExecutionLogEntry(DateTimeOffset.UtcNow, ExecutionEvent.ObservationStarted,
            FeatureId: "C-PROCESSES-001", DiagnosticRunId: runId));
        var result = _source.Observe();
        _log.Write(new ExecutionLogEntry(DateTimeOffset.UtcNow, ExecutionEvent.ObservationCompleted,
            FeatureId: "C-PROCESSES-001", DiagnosticRunId: runId,
            ErrorCode: result.Error?.Code, ErrorCategory: result.Error?.Category));
        return result;
    }
}
