using BeadWise.Application.Sessions;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;

namespace BeadWise.Application.Tests.Sessions;

public sealed class LoadSessionUseCaseTests
{
    private static readonly Guid SessionId = Guid.Parse("b68a1498-37b1-4a02-ae07-e8b21b7cb94c");
    private static readonly DateTimeOffset CreatedAt = new(2026, 9, 14, 18, 0, 0, TimeSpan.Zero);

    [Fact]
    public void LoadsRequestedSessionOnceAndPreservesResult()
    {
        var session = Session.Create(SessionId, CreatedAt);
        var expected = Result<Session>.Success(session);
        var store = new FakeSessionStore(expected);
        var useCase = new LoadSessionUseCase(store);

        var actual = useCase.Execute(SessionId);

        Assert.Same(expected, actual);
        Assert.Equal(1, store.LoadCallCount);
        Assert.Equal(SessionId, store.LoadedSessionId);
    }

    [Fact]
    public void PreservesStructuredLoadFailure()
    {
        var expected = Result<Session>.Failed(
            new Error("SESSION_NOT_FOUND", ErrorCategory.Unavailable));
        var store = new FakeSessionStore(expected);
        var useCase = new LoadSessionUseCase(store);

        var actual = useCase.Execute(SessionId);

        Assert.Same(expected, actual);
        Assert.Equal(1, store.LoadCallCount);
    }

    private sealed class FakeSessionStore(Result<Session> loadResult) : ISessionStore
    {
        public int LoadCallCount { get; private set; }

        public Guid LoadedSessionId { get; private set; }

        public Result<Session> Save(Session session) =>
            throw new InvalidOperationException("Save is not used by this test.");

        public Result<Session> Load(Guid sessionId)
        {
            LoadCallCount++;
            LoadedSessionId = sessionId;
            return loadResult;
        }
    }
}
