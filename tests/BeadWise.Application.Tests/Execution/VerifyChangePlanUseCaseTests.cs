using BeadWise.Application.Execution;
using BeadWise.Application.Planning;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Operations;
using BeadWise.Core.Planning;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Snapshots;

namespace BeadWise.Application.Tests.Execution;

public sealed class VerifyChangePlanUseCaseTests
{
    private static readonly DateTimeOffset Now = new(2026, 11, 12, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public async Task CompletesOnlyAfterObservationAndPersistsBothTransitions()
    {
        var context = new TestContext();
        var result = await context.Execute();

        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.Equal(SessionState.Completed, result.Value!.State);
        Assert.Equal(["save:Verifying", "observe", "save:Completed"], context.Events);
        Assert.Equal($"VERIFY_SUCCEEDED:{context.Plan.Operations[0].OperationId:D}", result.Value.Results[^1].Value);
        Assert.Equal(context.Session.Snapshots, result.Value.Snapshots);
        Assert.Equal(context.Session.Results[0], result.Value.Results[0]);
    }

    [Fact]
    public async Task WaitsAsynchronouslyForDelayedExit()
    {
        var context = new TestContext();
        context.Source.Read = () => context.Available(context.Source.Calls < 3);
        var pending = context.Execute();
        Assert.False(pending.IsCompleted);
        await context.Clock.TickAsync();
        await context.Clock.TickAsync();
        var result = await pending.WaitAsync(TimeSpan.FromSeconds(5));
        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.Equal(3, context.Source.Calls);
        Assert.Equal(Now.AddMilliseconds(200), result.Value!.UpdatedAt);
    }

    [Fact]
    public async Task TimesOutAtFiveSecondsWhenProcessRemainsAlive()
    {
        var context = new TestContext();
        context.Source.Read = () => context.Available(true);
        var pending = context.Execute();
        for (var i = 0; i < 50; i++)
        {
            await context.Clock.TickAsync();
        }

        var result = await pending.WaitAsync(TimeSpan.FromSeconds(5));
        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("VERIFY_TIMEOUT", result.Error!.Code);
        Assert.Equal(SessionState.Failed, context.Store.Saved[^1].State);
        Assert.Equal(RecoveryState.Required, context.Store.Saved[^1].RecoveryState);
        Assert.Equal(Now.AddSeconds(5), context.Store.Saved[^1].UpdatedAt);
        Assert.Equal(51, context.Source.Calls);
    }

    [Theory]
    [InlineData("unknown")]
    [InlineData("partial-value")]
    [InlineData("partial-result")]
    [InlineData("access-denied")]
    public async Task InconclusiveObservationNeverCompletes(string kind)
    {
        var context = new TestContext();
        context.Source.Read = () => kind switch
        {
            "unknown" => Result<Observation<bool>>.Success(Observation<bool>.Unknown("test", Now)),
            "partial-value" => Result<Observation<bool>>.Success(Observation<bool>.Partial(false, "test", Now)),
            "partial-result" => Result<Observation<bool>>.Partial(Observation<bool>.Available(false, "test", Now)),
            _ => Result<Observation<bool>>.Failed(new Error("VERIFY_ACCESS_DENIED", ErrorCategory.AccessDenied))
        };
        var result = await context.Execute();
        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal(SessionState.Failed, context.Store.Saved[^1].State);
        Assert.Equal(1, context.Source.Calls);
        Assert.DoesNotContain(context.Store.Saved.SelectMany(s => s.Results), r => r.Value?.StartsWith("VERIFY_SUCCEEDED:") == true);
    }

    [Fact]
    public async Task DelayedContinuationCannotConfirmAnExitAfterDeadline()
    {
        var context = new TestContext();
        context.Source.Read = () => context.Available(context.Source.Calls == 1);
        var pending = context.Execute();
        await context.Clock.TickAsync(TimeSpan.FromMilliseconds(5500));

        var result = await pending.WaitAsync(TimeSpan.FromSeconds(5));
        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("VERIFY_TIMEOUT", result.Error!.Code);
        Assert.Equal(1, context.Source.Calls);
    }

    [Fact]
    public async Task ObservationThatReturnsAfterDeadlineCannotConfirmExit()
    {
        var context = new TestContext();
        context.Source.Read = () =>
        {
            context.Clock.Advance(TimeSpan.FromSeconds(6));
            return context.Available(false);
        };

        var result = await context.Execute();
        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("VERIFY_TIMEOUT", result.Error!.Code);
    }

    [Fact]
    public async Task ExitObservedExactlyAtDeadlineCanComplete()
    {
        var context = new TestContext();
        context.Source.Read = () => context.Available(context.Source.Calls == 1);
        var pending = context.Execute();
        await context.Clock.TickAsync(TimeSpan.FromSeconds(5));
        var result = await pending.WaitAsync(TimeSpan.FromSeconds(5));
        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.Equal(Now.AddSeconds(5), result.Value!.UpdatedAt);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task CancellationPersistsVerifyingAndAllowsExplicitRetry(bool beforeStart)
    {
        var context = new TestContext();
        context.Source.Read = () => context.Available(true);
        using var cancellation = new CancellationTokenSource();
        if (beforeStart) cancellation.Cancel();
        var pending = context.Execute(cancellation.Token);
        cancellation.Cancel();
        var result = await pending.WaitAsync(TimeSpan.FromSeconds(5));

        Assert.Equal(ResultStatus.Cancelled, result.Status);
        var interrupted = context.Store.Saved[^1];
        Assert.Equal(SessionState.Verifying, interrupted.State);
        Assert.Equal(ResultStatus.Cancelled, interrupted.Results[^1].Status);
        Assert.Equal(beforeStart ? 0 : 1, context.Source.Calls);
        context.Source.Read = () => context.Available(false);
        var retry = await context.UseCase.ExecuteAsync(context.Plan, interrupted);
        Assert.Equal(SessionState.Completed, retry.Value!.State);
        Assert.Equal(3, retry.Value.Results.Count);
    }

    [Theory]
    [InlineData("null-plan")]
    [InlineData("null-session")]
    [InlineData("approved")]
    [InlineData("completed")]
    [InlineData("wrong-plan")]
    [InlineData("changed-operation")]
    [InlineData("missing-snapshot")]
    [InlineData("wrong-snapshot-session")]
    [InlineData("wrong-snapshot-operation")]
    [InlineData("wrong-snapshot-kind")]
    [InlineData("missing-apply")]
    [InlineData("wrong-apply-operation")]
    [InlineData("partial-apply")]
    public async Task RejectsInvalidInputBeforeObservationOrPersistence(string kind)
    {
        var context = new TestContext();
        var original = context.Session;
        var snapshot = original.Snapshots[0];
        var operation = original.Operations[0];
        var alteredOperation = new Operation(operation.OperationId, operation.Description, operation.CurrentState,
            "Running", operation.Risk, operation.PrivilegeRequirement, operation.RestartRequirement,
            operation.Reversibility, operation.ExecutionResources);
        var session = new Session(original.SessionId,
            kind == "approved" ? SessionState.Approved : kind == "completed" ? SessionState.Completed : original.State,
            original.CreatedAt, original.UpdatedAt,
            kind == "wrong-plan" ? Guid.NewGuid() : original.PlanId,
            kind == "changed-operation" ? [alteredOperation] : original.Operations,
            kind == "missing-snapshot" ? [] : [new Snapshot(snapshot.SnapshotId,
                kind == "wrong-snapshot-session" ? Guid.NewGuid() : snapshot.SessionId,
                kind == "wrong-snapshot-operation" ? Guid.NewGuid() : snapshot.OperationId,
                snapshot.CapturedAt, kind == "wrong-snapshot-kind" ? "Unrelated" : snapshot.StateKind,
                snapshot.OriginalState, snapshot.ExistenceSemantics)],
            kind switch
            {
                "missing-apply" => [],
                "wrong-apply-operation" => [Result<string>.Success($"APPLY_DISPATCHED:{Guid.NewGuid():D}")],
                "partial-apply" => [Result<string>.Partial(original.Results[0].Value)],
                _ => original.Results
            }, null, RecoveryState.NotRequired);

        var result = await context.UseCase.ExecuteAsync(kind == "null-plan" ? null : context.Plan,
            kind == "null-session" ? null : session);
        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal(ErrorCategory.InvalidPlan, result.Error!.Category);
        Assert.Empty(context.Events);
    }

    [Fact]
    public async Task InitialSaveFailurePreventsObservation()
    {
        var context = new TestContext();
        context.Store.FailOnSave = 1;
        var result = await context.Execute();
        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("SAVE_FAILED", result.Error!.Code);
        Assert.Equal(0, context.Source.Calls);
    }

    [Theory]
    [InlineData("success")]
    [InlineData("failure")]
    [InlineData("cancelled")]
    public async Task FinalSaveFailureReturnsPartialWithInMemoryOutcome(string outcome)
    {
        var context = new TestContext();
        context.Store.FailOnSave = 2;
        if (outcome == "failure")
            context.Source.Read = () => Result<Observation<bool>>.Failed(new Error("READ_FAILED", ErrorCategory.Unavailable));
        using var cancellation = new CancellationTokenSource();
        if (outcome == "cancelled") cancellation.Cancel();

        var result = await context.Execute(cancellation.Token);
        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal("VERIFY_RESULT_PERSIST_FAILED", result.Error!.Code);
        Assert.Equal(outcome switch
        {
            "success" => SessionState.Completed,
            "failure" => SessionState.Failed,
            _ => SessionState.Verifying
        }, result.Value!.State);
        Assert.Equal(2, result.Value.Results.Count);
    }

    private sealed class TestContext
    {
        public List<string> Events { get; } = [];
        public ManualTimeProvider Clock { get; } = new(Now);
        public ChangePlan Plan { get; } = new CreateControlledProcessChangePlanUseCase()
            .Execute(new ProcessIdentity(42, Now.AddMinutes(-1)), "af440a71-5983-4056-944a-2fbc9863d739").Value!;
        public Session Session { get; }
        public StateSource Source { get; }
        public SessionStore Store { get; }
        public VerifyChangePlanUseCase UseCase { get; }

        public TestContext()
        {
            var sessionId = Guid.NewGuid();
            var operation = Plan.Operations[0];
            Session = new Session(sessionId, SessionState.Applying, Now.AddSeconds(-1), Now, Plan.PlanId,
                Plan.Operations, [new Snapshot(Guid.NewGuid(), sessionId, operation.OperationId, Now,
                    "ControlledProcessState", "Running", ExistenceSemantics.Existed)],
                [Result<string>.Success($"APPLY_DISPATCHED:{operation.OperationId:D}")], null, RecoveryState.NotRequired);
            Source = new StateSource(Events, () => Available(false));
            Store = new SessionStore(Events);
            UseCase = new VerifyChangePlanUseCase(Source, Store, Clock);
        }

        public Result<Observation<bool>> Available(bool running) =>
            Result<Observation<bool>>.Success(Observation<bool>.Available(running, "test", Clock.GetUtcNow()));
        public Task<Result<Session>> Execute(CancellationToken token = default) => UseCase.ExecuteAsync(Plan, Session, token);
    }

    private sealed class StateSource(List<string> events, Func<Result<Observation<bool>>> read) : IControlledProcessStateSource
    {
        public Func<Result<Observation<bool>>> Read { get; set; } = read;
        public int Calls { get; private set; }
        public Result<Observation<bool>> Observe(ChangePlan plan)
        {
            events.Add("observe");
            Calls++;
            return Read();
        }
    }

    private sealed class SessionStore(List<string> events) : ISessionStore
    {
        public List<Session> Saved { get; } = [];
        public int FailOnSave { get; set; }
        public Result<Session> Save(Session session)
        {
            events.Add($"save:{session.State}");
            Saved.Add(session);
            return Saved.Count == FailOnSave
                ? Result<Session>.Failed(new Error("SAVE_FAILED", ErrorCategory.Unavailable))
                : Result<Session>.Success(session);
        }
        public Result<Session> Load(Guid id) => throw new NotSupportedException();
    }

    private sealed class ManualTimeProvider(DateTimeOffset initial) : TimeProvider
    {
        private readonly SemaphoreSlim _scheduled = new(0);
        private ManualTimer? _timer;
        private long _ticks;
        public override long TimestampFrequency => TimeSpan.TicksPerSecond;
        public override long GetTimestamp() => Interlocked.Read(ref _ticks);
        public override DateTimeOffset GetUtcNow() => initial.AddTicks(GetTimestamp());
        public override ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
        {
            Assert.Equal(TimeSpan.FromMilliseconds(100), dueTime);
            var timer = new ManualTimer(callback, state);
            _timer = timer;
            _scheduled.Release();
            return timer;
        }
        public void Advance(TimeSpan elapsed) => Interlocked.Add(ref _ticks, elapsed.Ticks);
        public async Task TickAsync(TimeSpan? elapsed = null)
        {
            Assert.True(await _scheduled.WaitAsync(TimeSpan.FromSeconds(5)));
            Advance(elapsed ?? TimeSpan.FromMilliseconds(100));
            _timer!.Fire();
        }
        private sealed class ManualTimer(TimerCallback callback, object? state) : ITimer
        {
            private bool _disposed;
            public void Fire() { if (!_disposed) callback(state); }
            public bool Change(TimeSpan dueTime, TimeSpan period) => throw new NotSupportedException();
            public void Dispose() => _disposed = true;
            public ValueTask DisposeAsync() { Dispose(); return ValueTask.CompletedTask; }
        }
    }
}
