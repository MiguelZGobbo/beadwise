using BeadWise.Core.Contracts;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;

namespace BeadWise.Application.Sessions;

public sealed class LoadSessionUseCase
{
    private readonly ISessionStore _store;

    public LoadSessionUseCase(ISessionStore store)
    {
        ArgumentNullException.ThrowIfNull(store);
        _store = store;
    }

    public Result<Session> Execute(Guid sessionId) =>
        _store.Load(sessionId);
}
