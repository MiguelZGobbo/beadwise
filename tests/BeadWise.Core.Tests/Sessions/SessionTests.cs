using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Snapshots;

namespace BeadWise.Core.Tests.Sessions;

public sealed class SessionTests
{
    private static readonly Guid SessionId = Guid.Parse("7b766a4a-461f-44e2-bc6f-b66962f3ee06");
    private static readonly Guid PlanId = Guid.Parse("1ee3b188-2082-4893-96b8-a3603995d96b");
    private static readonly DateTimeOffset CreatedAt = new(2026, 9, 9, 12, 0, 0, TimeSpan.Zero);
    private static readonly Error Failure = new(
        "APPLY_FAILED",
        ErrorCategory.ApplyFailed,
        "Controlled operation failed.",
        recoverable: true);

    [Fact]
    public void CreateStartsAGenericSessionInCreatedState()
    {
        var session = Session.Create(SessionId, CreatedAt);

        Assert.Equal(SessionId, session.SessionId);
        Assert.Equal(SessionState.Created, session.State);
        Assert.Equal(CreatedAt, session.CreatedAt);
        Assert.Equal(CreatedAt, session.UpdatedAt);
        Assert.Null(session.PlanId);
        Assert.Empty(session.Operations);
        Assert.Empty(session.Snapshots);
        Assert.Empty(session.Results);
        Assert.Null(session.Failure);
        Assert.Equal(RecoveryState.NotRequired, session.RecoveryState);
    }

    [Fact]
    public void ConstructorPreservesStateAndCopiesCollections()
    {
        var operations = new List<Operation> { CreateOperation() };
        var snapshots = new List<Snapshot> { CreateSnapshot() };
        var results = new List<Result<string>> { Result<string>.Success("Applied") };
        var session = new Session(
            SessionId,
            SessionState.Verifying,
            CreatedAt,
            CreatedAt.AddMinutes(1),
            PlanId,
            operations,
            snapshots,
            results,
            null,
            RecoveryState.NotRequired);

        operations.Clear();
        snapshots.Clear();
        results.Clear();

        Assert.Equal(SessionId, session.SessionId);
        Assert.Equal(SessionState.Verifying, session.State);
        Assert.Equal(CreatedAt, session.CreatedAt);
        Assert.Equal(CreatedAt.AddMinutes(1), session.UpdatedAt);
        Assert.Equal(PlanId, session.PlanId);
        Assert.Single(session.Operations);
        Assert.Single(session.Snapshots);
        Assert.Single(session.Results);
    }

    [Fact]
    public void ValidExecutionLifecycleTransitionsAreAccepted()
    {
        var session = Session.Create(SessionId, CreatedAt)
            .TransitionTo(SessionState.Planned, CreatedAt.AddSeconds(1))
            .TransitionTo(SessionState.Approved, CreatedAt.AddSeconds(2))
            .TransitionTo(SessionState.Applying, CreatedAt.AddSeconds(3))
            .TransitionTo(SessionState.Verifying, CreatedAt.AddSeconds(4))
            .TransitionTo(SessionState.Completed, CreatedAt.AddSeconds(5));

        Assert.Equal(SessionState.Completed, session.State);
        Assert.Equal(CreatedAt.AddSeconds(5), session.UpdatedAt);
    }

    [Fact]
    public void RecoveryLifecyclePreservesFailureAndUpdatesRecoveryState()
    {
        var failed = Session.Create(SessionId, CreatedAt)
            .TransitionTo(SessionState.Failed, CreatedAt.AddSeconds(1), Failure);
        var recovering = failed.TransitionTo(SessionState.Recovering, CreatedAt.AddSeconds(2));
        var recovered = recovering.TransitionTo(SessionState.Recovered, CreatedAt.AddSeconds(3));

        Assert.Equal(RecoveryState.InProgress, recovering.RecoveryState);
        Assert.Equal(SessionState.Recovered, recovered.State);
        Assert.Equal(RecoveryState.Completed, recovered.RecoveryState);
        Assert.Equal(Failure, recovered.Failure);
    }

    [Fact]
    public void FailedRecoveryDoesNotReplaceTheOriginalFailure()
    {
        var recoveryFailure = new Error("RECOVERY_FAILED", ErrorCategory.RecoveryFailed);
        var failed = Session.Create(SessionId, CreatedAt)
            .TransitionTo(SessionState.Failed, CreatedAt.AddSeconds(1), Failure)
            .TransitionTo(SessionState.Recovering, CreatedAt.AddSeconds(2))
            .TransitionTo(SessionState.Failed, CreatedAt.AddSeconds(3), recoveryFailure);

        Assert.Equal(Failure, failed.Failure);
        Assert.Equal(RecoveryState.Failed, failed.RecoveryState);
    }

    [Fact]
    public void CompletedSessionCanOnlyStartAnExplicitFixtureReset()
    {
        var completed = Session.Create(SessionId, CreatedAt)
            .TransitionTo(SessionState.Planned, CreatedAt)
            .TransitionTo(SessionState.Approved, CreatedAt)
            .TransitionTo(SessionState.Applying, CreatedAt)
            .TransitionTo(SessionState.Verifying, CreatedAt)
            .TransitionTo(SessionState.Completed, CreatedAt);
        var identity = new ProcessIdentity(42, CreatedAt);

        Assert.Throws<InvalidOperationException>(() => completed.TransitionTo(SessionState.Recovering, CreatedAt));
        Assert.Throws<InvalidOperationException>(() => completed.BeginRecovery(RecoveryReason.FailureRecovery, identity, CreatedAt));
        var recovering = completed.BeginRecovery(RecoveryReason.ExplicitFixtureReset, identity, CreatedAt);
        var recovered = recovering.CompleteRecovery(new ProcessIdentity(43, CreatedAt), CreatedAt);
        Assert.Null(recovered.Failure);
        Assert.Equal(RecoveryReason.ExplicitFixtureReset, recovered.RecoveryEvidence!.Reason);
        Assert.Equal(identity, recovered.RecoveryEvidence.OriginalIdentity);
        Assert.Equal(43, recovered.RecoveryEvidence.RecoveredIdentity!.Value.ProcessId);
    }

    [Fact]
    public void TypedRecoveryPreservesBothOriginalAndRecoveryErrors()
    {
        var error = new Error("RECREATE_FAILED", ErrorCategory.RecoveryFailed);
        var failed = Session.Create(SessionId, CreatedAt)
            .TransitionTo(SessionState.Failed, CreatedAt, Failure)
            .BeginRecovery(RecoveryReason.FailureRecovery, new ProcessIdentity(42, CreatedAt), CreatedAt)
            .TransitionTo(SessionState.Failed, CreatedAt, error);

        Assert.Equal(Failure, failed.Failure);
        Assert.Equal(Failure, failed.RecoveryEvidence!.OriginalError);
        Assert.Equal(error, failed.RecoveryEvidence.RecoveryError);
        Assert.Throws<InvalidOperationException>(() => failed.BeginRecovery(
            RecoveryReason.ExplicitFixtureReset, failed.RecoveryEvidence.OriginalIdentity, CreatedAt));
    }

    [Fact]
    public void FailedExplicitResetCanBeRetriedWithoutInventingAnOriginalFailure()
    {
        var completed = Session.Create(SessionId, CreatedAt)
            .TransitionTo(SessionState.Planned, CreatedAt)
            .TransitionTo(SessionState.Approved, CreatedAt)
            .TransitionTo(SessionState.Applying, CreatedAt)
            .TransitionTo(SessionState.Verifying, CreatedAt)
            .TransitionTo(SessionState.Completed, CreatedAt);
        var identity = new ProcessIdentity(42, CreatedAt);
        var failed = completed.BeginRecovery(RecoveryReason.ExplicitFixtureReset, identity, CreatedAt)
            .TransitionTo(SessionState.Failed, CreatedAt, new Error("RESET_FAILED", ErrorCategory.RecoveryFailed));
        Assert.Null(failed.Failure);
        Assert.Equal("RESET_FAILED", failed.RecoveryEvidence!.RecoveryError!.Code);

        var retry = failed.BeginRecovery(RecoveryReason.ExplicitFixtureReset, identity, CreatedAt);
        Assert.Null(retry.Failure);
        Assert.Null(retry.RecoveryEvidence!.OriginalError);
        Assert.Null(retry.RecoveryEvidence.RecoveryError);
    }

    [Fact]
    public void EvidenceCannotCarryAnEarlierRecoveryErrorIntoAnActiveAttempt()
    {
        var original = Session.Create(SessionId, CreatedAt)
            .TransitionTo(SessionState.Failed, CreatedAt, Failure)
            .BeginRecovery(RecoveryReason.FailureRecovery, new ProcessIdentity(42, CreatedAt), CreatedAt)
            .TransitionTo(SessionState.Failed, CreatedAt, new Error("RESET_FAILED", ErrorCategory.RecoveryFailed));
        Assert.Throws<ArgumentException>(() => new Session(original.SessionId, SessionState.Recovering,
            original.CreatedAt, original.UpdatedAt, original.PlanId, original.Operations, original.Snapshots,
            original.Results, original.Failure, RecoveryState.InProgress, original.RecoveryEvidence));
        Assert.Throws<InvalidOperationException>(() => original.TransitionTo(SessionState.Recovering, CreatedAt));
        Assert.Null(original.BeginRecovery(RecoveryReason.FailureRecovery,
            original.RecoveryEvidence!.OriginalIdentity, CreatedAt).RecoveryEvidence!.RecoveryError);
    }

    [Fact]
    public void EvidenceCannotBeAttachedToARequiredButUnattemptedRecovery()
    {
        var recovered = Session.Create(SessionId, CreatedAt)
            .TransitionTo(SessionState.Failed, CreatedAt, Failure)
            .BeginRecovery(RecoveryReason.FailureRecovery, new ProcessIdentity(42, CreatedAt), CreatedAt)
            .CompleteRecovery(new ProcessIdentity(43, CreatedAt), CreatedAt);
        Assert.Throws<ArgumentException>(() => new Session(recovered.SessionId, SessionState.Failed,
            recovered.CreatedAt, recovered.UpdatedAt, recovered.PlanId, recovered.Operations, recovered.Snapshots,
            recovered.Results, recovered.Failure, RecoveryState.Required, recovered.RecoveryEvidence));
    }

    [Fact]
    public void ObviouslyInvalidTransitionIsRejected()
    {
        var session = Session.Create(SessionId, CreatedAt);

        Assert.Throws<InvalidOperationException>(
            () => session.TransitionTo(SessionState.Completed, CreatedAt.AddSeconds(1)));
    }

    [Fact]
    public void FailedTransitionRequiresStructuredError()
    {
        var session = Session.Create(SessionId, CreatedAt);

        Assert.Throws<ArgumentNullException>(
            () => session.TransitionTo(SessionState.Failed, CreatedAt.AddSeconds(1)));
    }

    [Fact]
    public void FailedSessionRequiresStructuredError()
    {
        var create = () => new Session(
            SessionId,
            SessionState.Failed,
            CreatedAt,
            CreatedAt,
            PlanId,
            [],
            [],
            [],
            null,
            RecoveryState.Required);

        Assert.Throws<ArgumentNullException>(create);
    }

    [Fact]
    public void SessionRejectsEmptyIdentity()
    {
        Assert.Throws<ArgumentException>(() => Session.Create(Guid.Empty, CreatedAt));
    }

    [Fact]
    public void SessionRejectsUpdateBeforeCreation()
    {
        var create = () => new Session(
            SessionId,
            SessionState.Created,
            CreatedAt,
            CreatedAt.AddSeconds(-1),
            null,
            [],
            [],
            [],
            null,
            RecoveryState.NotRequired);

        Assert.Throws<ArgumentOutOfRangeException>(create);
    }

    [Fact]
    public void SessionRejectsFailureOnNonFailureTransition()
    {
        var session = Session.Create(SessionId, CreatedAt);

        Assert.Throws<ArgumentException>(
            () => session.TransitionTo(SessionState.Planned, CreatedAt.AddSeconds(1), Failure));
    }

    [Fact]
    public void SessionRejectsInconsistentRecoveryState()
    {
        var create = () => new Session(
            SessionId,
            SessionState.Created,
            CreatedAt,
            CreatedAt,
            null,
            [],
            [],
            [],
            null,
            RecoveryState.Completed);

        Assert.Throws<ArgumentException>(create);
    }

    [Fact]
    public void SessionRejectsUnknownStateValues()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Session(SessionId, (SessionState)999,
            CreatedAt, CreatedAt, null, [], [], [], null, RecoveryState.NotRequired));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Session(SessionId, SessionState.Created,
            CreatedAt, CreatedAt, null, [], [], [], null, (RecoveryState)999));
    }

    [Fact]
    public void TransitionRejectsTimestampBeforeCurrentUpdate()
    {
        var session = Session.Create(SessionId, CreatedAt);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => session.TransitionTo(SessionState.Planned, CreatedAt.AddSeconds(-1)));
    }

    private static Operation CreateOperation() =>
        new(
            Guid.Parse("cb984403-4faf-4182-876b-710404f0b6bf"),
            "Restart controlled process",
            "Running",
            "Replaced",
            Risk.Medium,
            PrivilegeRequirement.None,
            RestartRequirement.Process,
            Reversibility.Recoverable,
            [new ExecutionResource("Process", "42:2026-09-09T12:00:00Z")]);

    private static Snapshot CreateSnapshot() =>
        new(
            Guid.Parse("7466e4ac-b1bc-4b7c-846a-85cc24eefebe"),
            SessionId,
            Guid.Parse("cb984403-4faf-4182-876b-710404f0b6bf"),
            CreatedAt,
            "ProcessState",
            "Running",
            ExistenceSemantics.Existed);
}
