using BeadWise.Application.Planning;
using BeadWise.Application.Recovery;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Logging;
using BeadWise.Core.Operations;
using BeadWise.Core.Planning;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Snapshots;

namespace BeadWise.Application.Tests.Recovery;

public sealed class RecoverControlledFixtureUseCaseTests
{
    private static readonly DateTimeOffset Now = new(2026, 11, 12, 12, 0, 0, TimeSpan.Zero);
    private static readonly ProcessIdentity Original = new(42, Now);
    private static readonly ProcessIdentity Replacement = new(43, Now.AddSeconds(1));
    private static readonly Error OriginalError = new("VERIFY_FAILED", ErrorCategory.VerifyFailed);

    [Fact]
    public async Task ExplicitResetPersistsBeforeAndAfterRestorationAndVerification()
    {
        var c = new Context();
        var result = await c.Run();

        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.Equal(SessionState.Recovered, result.Value!.State);
        Assert.Equal(["save:Recovering", "restore", "verify", "save:Recovered"], c.Events);
        Assert.Equal(Original, result.Value.RecoveryEvidence!.OriginalIdentity);
        Assert.Equal(Replacement, result.Value.RecoveryEvidence.RecoveredIdentity);
        Assert.Null(result.Value.Failure);
        Assert.Equal(c.Session.Snapshots, result.Value.Snapshots);
        Assert.Contains(c.Log.Entries, e => e.Event is ExecutionEvent.RecoveryStarted);
        Assert.Contains(c.Log.Entries, e => e.Event is ExecutionEvent.VerifyCompleted);
        Assert.Contains(c.Log.Entries, e => e.Event is ExecutionEvent.RecoveryCompleted);
    }

    [Fact]
    public async Task FailureRecoveryPreservesOriginalErrorWhenRecreationFails()
    {
        var c = new Context(failed: true);
        var error = new Error("RECREATE_FAILED", ErrorCategory.RecoveryFailed);
        c.Port.Restore = _ => Task.FromResult(Result<ProcessIdentity>.Failed(error));
        var result = await c.Run(RecoveryReason.FailureRecovery);

        Assert.Equal(ResultStatus.Failed, result.Status);
        var recorded = c.Store.Saved[^1];
        Assert.Equal(SessionState.Failed, recorded.State);
        Assert.Equal(OriginalError, recorded.Failure);
        Assert.Equal(OriginalError, recorded.RecoveryEvidence!.OriginalError);
        Assert.Equal(error, recorded.RecoveryEvidence.RecoveryError);
        Assert.DoesNotContain("verify", c.Events);
    }

    [Fact]
    public async Task UnexpectedAdapterFailureIsRecordedWithoutLosingOriginalFailure()
    {
        var c = new Context(failed: true);
        c.Port.Restore = _ => throw new IOException("fixture pipe failed");
        var result = await c.Run(RecoveryReason.FailureRecovery);

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal("RECOVERY_ADAPTER_FAILED", result.Error!.Code);
        Assert.Equal(OriginalError, c.Store.Saved[^1].Failure);
        Assert.Equal("RECOVERY_ADAPTER_FAILED", c.Store.Saved[^1].RecoveryEvidence!.RecoveryError!.Code);
    }

    [Fact]
    public async Task PreSaveFailurePreventsRestoration()
    {
        var c = new Context();
        c.Store.FailAt = 1;
        var result = await c.Run();
        Assert.NotEqual(ResultStatus.Success, result.Status);
        Assert.Equal(["save:Recovering"], c.Events);
    }

    [Fact]
    public async Task DeniedPrivilegeBlocksRecoveryBeforePersistenceOrProcessAction()
    {
        var c = new Context();
        var deniedPlan = new ChangePlan(c.Plan.PlanId, c.Plan.FeatureIds, c.Plan.Operations,
            c.Plan.Preconditions, c.Plan.Risk, c.Plan.Warnings, [PrivilegeRequirement.Administrator],
            c.Plan.RestartRequirements, c.Plan.RecoveryInformation);
        var boundary = new DenyPrivilege();
        var useCase = new RecoverControlledFixtureUseCase(c.Port, c.Store, privilegeBoundary: boundary);

        var result = await useCase.ExecuteAsync(deniedPlan, c.Session, RecoveryReason.ExplicitFixtureReset);

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Contains(PrivilegeRequirement.Administrator, boundary.Requirements);
        Assert.Contains(PrivilegeRequirement.None, boundary.Requirements);
        Assert.Empty(c.Events);
    }

    [Fact]
    public async Task PostSaveFailureReturnsPartialWithRestoredSession()
    {
        var c = new Context();
        c.Store.FailAt = 2;
        var result = await c.Run();
        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal("RECOVERY_RESULT_PERSIST_FAILED", result.Error!.Code);
        Assert.Equal(SessionState.Recovered, result.Value!.State);
        Assert.Equal(Replacement, result.Value.RecoveryEvidence!.RecoveredIdentity);
    }

    [Fact]
    public async Task VerificationMismatchCannotCompleteRecovery()
    {
        var c = new Context();
        c.Port.Verified = Result<bool>.Success(false);
        var result = await c.Run();
        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("RECOVERY_VERIFY_FAILED", result.Error!.Code);
        Assert.Equal(SessionState.Failed, c.Store.Saved[^1].State);
        Assert.Equal("RECOVERY_VERIFY_FAILED", c.Store.Saved[^1].RecoveryEvidence!.RecoveryError!.Code);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task PartialVerificationRemainsPartialAndRetainsEvidence(bool observedValue)
    {
        var c = new Context();
        var error = new Error("RECOVERY_VERIFY_INCONCLUSIVE", ErrorCategory.PartialFailure);
        c.Port.Verified = Result<bool>.Partial(observedValue, error);

        var result = await c.Run();

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal(error, result.Error);
        Assert.Equal(SessionState.Failed, result.Value!.State);
        Assert.Equal(Original, result.Value.RecoveryEvidence!.OriginalIdentity);
        Assert.Equal(Replacement, result.Value.RecoveryEvidence.RecoveredIdentity);
        Assert.Equal(error, result.Value.RecoveryEvidence.RecoveryError);
        Assert.Equal(c.Session.Snapshots, result.Value.Snapshots);
    }

    [Fact]
    public async Task AlreadyCancelledRecoveryDoesNotWriteOrRestore()
    {
        var c = new Context();
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();
        var result = await c.Run(token: cancellation.Token);
        Assert.Equal(ResultStatus.Cancelled, result.Status);
        Assert.Empty(c.Events);
        Assert.Empty(c.Store.Saved);
    }

    [Fact]
    public async Task CancelledReplayCannotOverwritePersistedRecovery()
    {
        var c = new Context();
        var first = await c.Run();
        Assert.Equal(ResultStatus.Success, first.Status);
        var savedCount = c.Store.Saved.Count;
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();

        var replay = await c.Run(token: cancellation.Token);

        Assert.Equal(ResultStatus.Cancelled, replay.Status);
        Assert.Equal(savedCount, c.Store.Saved.Count);
        Assert.Same(first.Value, c.Store.Current);
    }

    [Fact]
    public async Task StaleCompletedReplayReverifiesWithoutOverwritingRecoveredSession()
    {
        var c = new Context();
        var first = await c.Run();
        var savedCount = c.Store.Saved.Count;

        var replay = await c.Run();

        Assert.Equal(ResultStatus.Success, replay.Status);
        Assert.Same(first.Value, replay.Value);
        Assert.Equal(savedCount, c.Store.Saved.Count);
        Assert.Equal(1, c.Events.Count(item => item == "restore"));
        Assert.Equal(2, c.Events.Count(item => item == "verify"));
    }

    [Fact]
    public async Task InconclusiveReplayKeepsDurableRecoveredSession()
    {
        var c = new Context();
        var first = await c.Run();
        var savedCount = c.Store.Saved.Count;
        c.Port.Verified = Result<bool>.Failed(new Error("NOT_OBSERVABLE", ErrorCategory.Unavailable));

        var replay = await c.Run();

        Assert.Equal(ResultStatus.Partial, replay.Status);
        Assert.Same(first.Value, replay.Value);
        Assert.Equal(savedCount, c.Store.Saved.Count);
        Assert.Same(first.Value, c.Store.Current);
    }

    [Fact]
    public async Task CancelledVerificationReplayRemainsCancelledAndDoesNotWrite()
    {
        var c = new Context();
        var first = await c.Run();
        var savedCount = c.Store.Saved.Count;
        var error = new Error("VERIFY_CANCELLED", ErrorCategory.Cancelled);
        c.Port.Verified = Result<bool>.Cancelled(error);

        var replay = await c.Run();

        Assert.Equal(ResultStatus.Cancelled, replay.Status);
        Assert.Equal(error, replay.Error);
        Assert.Equal(savedCount, c.Store.Saved.Count);
        Assert.Same(first.Value, c.Store.Current);
    }

    [Fact]
    public async Task SynchronousReplayCannotReportSuccessAfterCallerCancels()
    {
        var c = new Context();
        var first = await c.Run();
        var savedCount = c.Store.Saved.Count;
        using var cancellation = new CancellationTokenSource();
        c.Port.VerifyOverride = _ =>
        {
            cancellation.Cancel();
            return Task.FromResult(Result<bool>.Success(true));
        };

        var replay = await c.Run(token: cancellation.Token);

        Assert.Equal(ResultStatus.Cancelled, replay.Status);
        Assert.Equal(savedCount, c.Store.Saved.Count);
        Assert.Same(first.Value, c.Store.Current);
    }

    [Fact]
    public async Task ConfirmedExitedRecoveryCanStartANewOwnedRestoration()
    {
        var c = new Context();
        var first = await c.Run();
        var verifications = 0;
        c.Port.VerifyOverride = _ => Task.FromResult(Result<bool>.Success(++verifications != 1));

        var replay = await c.Run();

        Assert.Equal(ResultStatus.Success, first.Status);
        Assert.Equal(ResultStatus.Success, replay.Status);
        Assert.Equal(4, c.Store.Saved.Count);
        Assert.Equal(2, c.Events.Count(item => item == "restore"));
        Assert.Equal(SessionState.Recovered, c.Store.Current.State);
    }

    [Fact]
    public async Task CancellationWhileAnotherAttemptVerifiesCannotOverwriteItsSuccess()
    {
        var c = new Context();
        var entered = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var finish = new TaskCompletionSource<Result<bool>>(TaskCreationOptions.RunContinuationsAsynchronously);
        c.Port.VerifyOverride = _ => { entered.SetResult(); return finish.Task; };
        var first = c.Run();
        await entered.Task.WaitAsync(TimeSpan.FromSeconds(3));
        using var cancellation = new CancellationTokenSource();
        var second = c.Run(token: cancellation.Token);
        cancellation.Cancel();
        finish.SetResult(Result<bool>.Success(true));

        var firstResult = await first;
        var secondResult = await second;
        Assert.Equal(ResultStatus.Success, firstResult.Status);
        Assert.Equal(ResultStatus.Cancelled, secondResult.Status);
        Assert.Same(firstResult.Value, c.Store.Current);
        Assert.Equal(SessionState.Recovered, c.Store.Current.State);
        Assert.Equal(2, c.Store.Saved.Count);
        Assert.Equal(1, c.Events.Count(item => item == "restore"));
    }

    [Fact]
    public async Task ConcurrentInconclusiveReplayDoesNotReplaceAnEarlierVerifiedSuccess()
    {
        var c = new Context();
        var entered = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var finish = new TaskCompletionSource<Result<bool>>(TaskCreationOptions.RunContinuationsAsynchronously);
        var verificationCount = 0;
        c.Port.VerifyOverride = _ =>
        {
            if (++verificationCount != 1)
                return Task.FromResult(Result<bool>.Failed(new Error("NOT_OBSERVABLE", ErrorCategory.Unavailable)));
            entered.SetResult();
            return finish.Task;
        };
        var first = c.Run();
        await entered.Task.WaitAsync(TimeSpan.FromSeconds(3));
        var second = c.Run();
        finish.SetResult(Result<bool>.Success(true));

        var firstResult = await first;
        var secondResult = await second;
        Assert.Equal(ResultStatus.Success, firstResult.Status);
        Assert.Equal(ResultStatus.Partial, secondResult.Status);
        Assert.Same(firstResult.Value, secondResult.Value);
        Assert.Same(firstResult.Value, c.Store.Current);
        Assert.Equal(2, c.Store.Saved.Count);
        Assert.Equal(1, c.Events.Count(item => item == "restore"));
    }

    [Fact]
    public async Task RecoveryTimesOutWhenPortDoesNotComplete()
    {
        var c = new Context();
        var pending = new TaskCompletionSource<Result<ProcessIdentity>>();
        c.Port.Restore = _ => pending.Task;
        var result = await c.Run(timeout: TimeSpan.FromMilliseconds(30));
        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal("RECOVERY_TIMEOUT", result.Error!.Code);
        Assert.Equal("RECOVERY_TIMEOUT", c.Store.Saved[^1].RecoveryEvidence!.RecoveryError!.Code);
        pending.SetResult(Result<ProcessIdentity>.Failed(
            new Error("LATE_FAILURE", ErrorCategory.RecoveryFailed)));
    }

    [Fact]
    public async Task RestorationReturningAfterDeadlineCannotBeReportedAsSuccess()
    {
        var c = new Context();
        var clock = new AdvancingClock();
        c.Port.Restore = _ =>
        {
            clock.Advance(TimeSpan.FromSeconds(6));
            return Task.FromResult(Result<ProcessIdentity>.Success(Replacement));
        };
        var useCase = new RecoverControlledFixtureUseCase(c.Port, c.Store, timeProvider: clock);
        var result = await useCase.ExecuteAsync(c.Plan, c.Session, RecoveryReason.ExplicitFixtureReset);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("RECOVERY_TIMEOUT", result.Error!.Code);
        Assert.Equal(Replacement, c.Store.Saved[^1].RecoveryEvidence!.RecoveredIdentity);
        Assert.DoesNotContain("verify", c.Events);
    }

    [Fact]
    public async Task WrongReasonAndPlanAreRejectedBeforePersistOrRestore()
    {
        var c = new Context();
        var wrongReason = await c.Run(RecoveryReason.FailureRecovery);
        Assert.Equal(ResultStatus.Failed, wrongReason.Status);
        Assert.Empty(c.Events);
        var changed = new ChangePlan(Guid.NewGuid(), c.Plan.FeatureIds, c.Plan.Operations,
            c.Plan.Preconditions, c.Plan.Risk, c.Plan.Warnings, c.Plan.PrivilegeRequirements,
            c.Plan.RestartRequirements, c.Plan.RecoveryInformation);
        var wrongPlan = await c.UseCase().ExecuteAsync(changed, c.Session, RecoveryReason.ExplicitFixtureReset);
        Assert.Equal(ResultStatus.Failed, wrongPlan.Status);
        Assert.Empty(c.Events);
    }

    private sealed class Context
    {
        public List<string> Events { get; } = [];
        public ChangePlan Plan { get; } = new CreateControlledProcessChangePlanUseCase()
            .Execute(Original, "af440a71-5983-4056-944a-2fbc9863d739").Value!;
        public Session Session { get; }
        public Store Store { get; }
        public RecoveryPort Port { get; }
        public RecordingLog Log { get; } = new();

        public Context(bool failed = false)
        {
            var id = Guid.NewGuid();
            var operation = Plan.Operations[0];
            var resource = operation.ExecutionResources[0];
            Session = new Session(id, failed ? SessionState.Failed : SessionState.Completed,
                Now, Now, Plan.PlanId, Plan.Operations,
                [new Snapshot(Guid.NewGuid(), id, Plan.Operations[0].OperationId, Now,
                    "ControlledProcessState", $"Running|resource={resource.ResourceType}:{resource.ResourceKey}|action={operation.OperationId:D}", ExistenceSemantics.Existed)],
                [], failed ? OriginalError : null,
                failed ? RecoveryState.Required : RecoveryState.NotRequired);
            Store = new Store(Events, Session);
            Port = new RecoveryPort(Events);
        }

        public RecoverControlledFixtureUseCase UseCase(TimeSpan? timeout = null) =>
            new(Port, Store, recoveryTimeout: timeout, log: Log);
        public Task<Result<Session>> Run(RecoveryReason reason = RecoveryReason.ExplicitFixtureReset,
            CancellationToken token = default, TimeSpan? timeout = null) =>
            UseCase(timeout).ExecuteAsync(Plan, Session, reason, token);
    }

    private sealed class RecoveryPort(List<string> events) : IControlledFixtureRecovery
    {
        public ProcessIdentity OriginalIdentity => Original;
        public Func<CancellationToken, Task<Result<ProcessIdentity>>> Restore { get; set; } =
            _ => Task.FromResult(Result<ProcessIdentity>.Success(Replacement));
        public Result<bool> Verified { get; set; } = Result<bool>.Success(true);
        public Func<CancellationToken, Task<Result<bool>>>? VerifyOverride { get; set; }
        public Task<Result<ProcessIdentity>> RestoreAsync(ChangePlan plan, CancellationToken token)
        {
            events.Add("restore");
            return Restore(token);
        }
        public Task<Result<bool>> VerifyAsync(ChangePlan plan, ProcessIdentity identity, CancellationToken token)
        {
            events.Add("verify");
            return VerifyOverride?.Invoke(token) ?? Task.FromResult(Verified);
        }
    }

    private sealed class Store(List<string> events, Session initial) : ISessionStore
    {
        public int FailAt { get; set; }
        public List<Session> Saved { get; } = [];
        public Session Current { get; private set; } = initial;
        public Result<Session> Save(Session session)
        {
            events.Add($"save:{session.State}");
            Saved.Add(session);
            if (Saved.Count == FailAt)
                return Result<Session>.Failed(new Error("STORE_FAILED", ErrorCategory.Unavailable));
            Current = session;
            return Result<Session>.Success(session);
        }
        public Result<Session> Load(Guid sessionId) => sessionId == Current.SessionId
            ? Result<Session>.Success(Current)
            : Result<Session>.Failed(new Error("SESSION_NOT_FOUND", ErrorCategory.Unavailable));
    }

    private sealed class RecordingLog : IExecutionLog
    {
        public List<ExecutionLogEntry> Entries { get; } = [];
        public void Write(ExecutionLogEntry entry) => Entries.Add(entry);
    }

    private sealed class DenyPrivilege : IPrivilegeBoundary
    {
        public IReadOnlyList<PrivilegeRequirement> Requirements { get; private set; } = [];
        public Result<bool> Authorize(IReadOnlyList<PrivilegeRequirement> requirements)
        {
            Requirements = requirements;
            return Result<bool>.Blocked(new Error("PRIVILEGE_DENIED", ErrorCategory.Blocked));
        }
    }

    private sealed class AdvancingClock : TimeProvider
    {
        private long _ticks;
        public override long TimestampFrequency => TimeSpan.TicksPerSecond;
        public override long GetTimestamp() => _ticks;
        public override DateTimeOffset GetUtcNow() => Now.AddTicks(_ticks);
        public void Advance(TimeSpan elapsed) => _ticks += elapsed.Ticks;
    }
}
