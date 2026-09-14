using BeadWise.Core.Results;
using BeadWise.Core.Sessions;

namespace BeadWise.Core.Contracts;

public interface ISessionStore
{
    Result<Session> Save(Session session);

    Result<Session> Load(Guid sessionId);
}
