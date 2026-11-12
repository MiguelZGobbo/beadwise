using BeadWise.Core.Contracts;
using BeadWise.Core.Planning;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Logging;

namespace BeadWise.Application.Sessions;

public sealed class CreateAndPersistSessionUseCase
{
    private readonly ISessionStore _store;
    private readonly IExecutionLog _log;

    public CreateAndPersistSessionUseCase(ISessionStore store, IExecutionLog? log = null)
    {
        ArgumentNullException.ThrowIfNull(store);
        _store = store;
        _log = log ?? NullExecutionLog.Instance;
    }

    public Result<Session> Execute(
        ChangePlan plan,
        Guid sessionId,
        DateTimeOffset createdAt)
    {
        ArgumentNullException.ThrowIfNull(plan);

        var session = new Session(
            sessionId,
            SessionState.Planned,
            createdAt,
            createdAt,
            plan.PlanId,
            plan.Operations,
            [],
            [],
            null,
            RecoveryState.NotRequired);

        var result = _store.Save(session);
        _log.WriteForPlan(result.Status is ResultStatus.Success ? ExecutionEvent.SessionPersisted : ExecutionEvent.Failure,
            plan, sessionId, result.Error);
        return result;
    }
}
