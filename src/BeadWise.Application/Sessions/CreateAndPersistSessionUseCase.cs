using BeadWise.Core.Contracts;
using BeadWise.Core.Planning;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;

namespace BeadWise.Application.Sessions;

public sealed class CreateAndPersistSessionUseCase
{
    private readonly ISessionStore _store;

    public CreateAndPersistSessionUseCase(ISessionStore store)
    {
        ArgumentNullException.ThrowIfNull(store);
        _store = store;
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

        return _store.Save(session);
    }
}
