using BeadWise.Application.Sessions;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Planning;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;

namespace BeadWise.Application.Tests.Sessions;

public sealed class CreateAndPersistSessionUseCaseTests
{
    private static readonly Guid SessionId = Guid.Parse("b68a1498-37b1-4a02-ae07-e8b21b7cb94c");
    private static readonly Guid PlanId = Guid.Parse("cd894e80-a901-4086-9aa1-507b72866050");
    private static readonly Guid OperationId = Guid.Parse("369006b6-9385-4111-ae50-2320fe499ff7");
    private static readonly DateTimeOffset CreatedAt = new(2026, 9, 14, 18, 0, 0, TimeSpan.Zero);

    [Fact]
    public void CreatesPlannedSessionAndPersistsItOnce()
    {
        var store = new FakeSessionStore(session => Result<Session>.Success(session));
        var useCase = new CreateAndPersistSessionUseCase(store);

        var result = useCase.Execute(CreatePlan(), SessionId, CreatedAt);

        Assert.Equal(1, store.SaveCallCount);
        var saved = Assert.IsType<Session>(store.SavedSession);
        Assert.Same(saved, result.Value);
        Assert.Equal(SessionId, saved.SessionId);
        Assert.Equal(SessionState.Planned, saved.State);
        Assert.Equal(CreatedAt, saved.CreatedAt);
        Assert.Equal(CreatedAt, saved.UpdatedAt);
        Assert.Equal(PlanId, saved.PlanId);
        Assert.Equal(OperationId, Assert.Single(saved.Operations).OperationId);
        Assert.Empty(saved.Snapshots);
        Assert.Empty(saved.Results);
        Assert.Null(saved.Failure);
        Assert.Equal(RecoveryState.NotRequired, saved.RecoveryState);
    }

    [Fact]
    public void PreservesStoreFailure()
    {
        var expected = Result<Session>.Failed(
            new Error("SESSION_IO_ERROR", ErrorCategory.Unavailable, "Disk unavailable."));
        var store = new FakeSessionStore(_ => expected);
        var useCase = new CreateAndPersistSessionUseCase(store);

        var actual = useCase.Execute(CreatePlan(), SessionId, CreatedAt);

        Assert.Same(expected, actual);
        Assert.Equal(1, store.SaveCallCount);
    }

    private static ChangePlan CreatePlan()
    {
        var operation = new Operation(
            OperationId,
            "Stop controlled process fixture",
            "Running",
            "NotRunning",
            Risk.Medium,
            PrivilegeRequirement.None,
            RestartRequirement.None,
            Reversibility.Recoverable,
            [new ExecutionResource("Process", "fixture-alpha|pid=42")]);

        return new ChangePlan(
            PlanId,
            ["C-PROCESSES-007"],
            [operation],
            ["Target is controlled"],
            Risk.Medium,
            ["Fixture only"],
            [PrivilegeRequirement.None],
            [RestartRequirement.None],
            "Recreate the controlled fixture if required.");
    }

    private sealed class FakeSessionStore(
        Func<Session, Result<Session>> save) : ISessionStore
    {
        public int SaveCallCount { get; private set; }

        public Session? SavedSession { get; private set; }

        public Result<Session> Save(Session session)
        {
            SaveCallCount++;
            SavedSession = session;
            return save(session);
        }

        public Result<Session> Load(Guid sessionId) =>
            throw new InvalidOperationException("Load is not used by this test.");
    }
}
