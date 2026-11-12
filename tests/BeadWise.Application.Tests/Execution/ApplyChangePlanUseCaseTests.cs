using BeadWise.Application.Execution;
using BeadWise.Application.Planning;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Planning;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Snapshots;

namespace BeadWise.Application.Tests.Execution;

public sealed class ApplyChangePlanUseCaseTests
{
    [Fact]
    public void PrivilegeBoundaryRunsBeforePreparingOrPersisting()
    {
        var events = new List<string>();
        var plan = CreatePlan();
        var session = CreateApprovedSession(plan);
        var executor = CreateExecutor(events, plan, session, plan.Operations[0]);
        var useCase = new ApplyChangePlanUseCase(executor, new RecordingSessionStore(events),
            privilegeBoundary: new DenyingBoundary(events));

        var result = useCase.Execute(plan, session);

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("ADMIN_REQUIRED", result.Error?.Code);
        Assert.Equal(["privilege"], events);
    }

    private sealed class DenyingBoundary(List<string> events) : IPrivilegeBoundary
    {
        public Result<bool> Authorize(IReadOnlyList<PrivilegeRequirement> requirements)
        {
            events.Add("privilege");
            return Result<bool>.Blocked(new Error("ADMIN_REQUIRED", ErrorCategory.AdminRequired));
        }
    }

    private static readonly DateTimeOffset CreatedAt =
        new(2026, 9, 15, 12, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset ApplyingAt = CreatedAt.AddMinutes(1);
    private static readonly ProcessIdentity TargetIdentity =
        new(4242, CreatedAt.AddHours(-1));

    [Fact]
    public void PersistsSnapshotBeforeExecutionAndRecordsAcceptedApplyWithoutStartingVerify()
    {
        var events = new List<string>();
        var plan = CreatePlan();
        var approvedSession = CreateApprovedSession(plan);
        var operation = Assert.Single(plan.Operations);
        var snapshot = new Snapshot(
            Guid.Parse("99c912cd-a25c-4864-a12b-bb26037048d9"),
            approvedSession.SessionId,
            operation.OperationId,
            ApplyingAt,
            "ControlledProcessState",
            "Running",
            ExistenceSemantics.Existed);
        var executor = new FakeControlledProcessActionExecutor(
            events,
            plan,
            plan.PlanId,
            snapshot,
            operation);
        var store = new RecordingSessionStore(events);
        var useCase = new ApplyChangePlanUseCase(
            executor,
            store,
            new FixedTimeProvider(ApplyingAt));

        var result = useCase.Execute(plan, approvedSession);

        Assert.Equal(ResultStatus.Success, result.Status);
        var session = Assert.IsType<Session>(result.Value);
        Assert.Equal(SessionState.Applying, session.State);
        Assert.Equal(plan.PlanId, session.PlanId);
        Assert.Equal(operation.OperationId, Assert.Single(session.Operations).OperationId);
        Assert.Same(snapshot, Assert.Single(session.Snapshots));
        Assert.Contains(operation.OperationId.ToString("D"), Assert.Single(session.Results).Value);
        Assert.Equal(
            ["prepare", "save:Applying:0", "execute", "save:Applying:1"],
            events);
        Assert.True(executor.Preparation.IsDisposed);
    }

    [Fact]
    public void InvalidatesPreparationAndDoesNotExecuteWhenPreApplyPersistenceFails()
    {
        var events = new List<string>();
        var plan = CreatePlan();
        var session = CreateApprovedSession(plan);
        var operation = Assert.Single(plan.Operations);
        var executor = CreateExecutor(events, plan, session, operation);
        var storeError = new Error(
            "SESSION_IO_ERROR",
            ErrorCategory.Unavailable,
            "Disk unavailable.");
        var store = new RecordingSessionStore(
            events,
            _ => Result<Session>.Failed(storeError));
        var useCase = new ApplyChangePlanUseCase(executor, store);

        var result = useCase.Execute(plan, session);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Same(storeError, result.Error);
        Assert.Equal(["prepare", "save:Applying:0"], events);
        Assert.True(executor.Preparation.IsDisposed);
    }

    [Fact]
    public void InvalidPreparationIsDisposedBeforeReturningFailure()
    {
        var events = new List<string>();
        var plan = CreatePlan();
        var session = CreateApprovedSession(plan);
        var operation = Assert.Single(plan.Operations);
        var executor = CreateExecutor(
            events,
            plan,
            session,
            operation,
            preparationPlanId: Guid.NewGuid());
        var store = new RecordingSessionStore(events);
        var useCase = new ApplyChangePlanUseCase(executor, store);

        var result = useCase.Execute(plan, session);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("INVALID_APPLY_PREPARATION", result.Error?.Code);
        Assert.Equal(["prepare"], events);
        Assert.True(executor.Preparation.IsDisposed);
    }

    [Fact]
    public void PartialPrepareInvalidatesReturnedAuthorizationWithoutExecuting()
    {
        var events = new List<string>();
        var plan = CreatePlan();
        var session = CreateApprovedSession(plan);
        var operation = Assert.Single(plan.Operations);
        var executor = CreateExecutor(
            events,
            plan,
            session,
            operation,
            prepareIsPartial: true);
        var useCase = new ApplyChangePlanUseCase(
            executor,
            new RecordingSessionStore(events));

        var result = useCase.Execute(plan, session);

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal(["prepare"], events);
        Assert.True(executor.Preparation.IsDisposed);
    }

    [Fact]
    public void ReturnsPartialWithRecordedApplyWhenPostApplyPersistenceFails()
    {
        var events = new List<string>();
        var plan = CreatePlan();
        var session = CreateApprovedSession(plan);
        var operation = Assert.Single(plan.Operations);
        var executor = CreateExecutor(events, plan, session, operation);
        var saveCount = 0;
        var store = new RecordingSessionStore(
            events,
            saved => ++saveCount == 1
                ? Result<Session>.Success(saved)
                : Result<Session>.Failed(
                    new Error("SESSION_IO_ERROR", ErrorCategory.Unavailable)));
        var useCase = new ApplyChangePlanUseCase(executor, store);

        var result = useCase.Execute(plan, session);

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal("APPLY_RESULT_PERSIST_FAILED", result.Error?.Code);
        var recorded = Assert.IsType<Session>(result.Value);
        Assert.Equal(SessionState.Applying, recorded.State);
        Assert.Contains(operation.OperationId.ToString("D"), Assert.Single(recorded.Results).Value);
        Assert.Equal(
            ["prepare", "save:Applying:0", "execute", "save:Applying:1"],
            events);
    }

    [Fact]
    public void PersistsFailedApplyWithoutStartingVerifyOrRecovery()
    {
        var events = new List<string>();
        var plan = CreatePlan();
        var session = CreateApprovedSession(plan);
        var operation = Assert.Single(plan.Operations);
        var applyError = new Error(
            "TARGET_EXITED_DURING_APPLY",
            ErrorCategory.Unavailable,
            recoverable: true);
        var executor = CreateExecutor(
            events,
            plan,
            session,
            operation,
            executeResult: Result<Operation>.Failed(applyError));
        var store = new RecordingSessionStore(events);
        var useCase = new ApplyChangePlanUseCase(executor, store);

        var result = useCase.Execute(plan, session);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Same(applyError, result.Error);
        var failed = Assert.IsType<Session>(store.SavedSessions[^1]);
        Assert.Equal(SessionState.Failed, failed.State);
        Assert.Same(applyError, failed.Failure);
        Assert.Equal(RecoveryState.Required, failed.RecoveryState);
        Assert.Equal(ResultStatus.Failed, Assert.Single(failed.Results).Status);
        Assert.DoesNotContain(
            store.SavedSessions,
            saved => saved.State is SessionState.Verifying or SessionState.Completed or SessionState.Recovered);
    }

    [Fact]
    public void RejectsSessionFromAnotherPlanBeforePreparingOrPersisting()
    {
        var events = new List<string>();
        var plan = CreatePlan();
        var otherPlan = CreatePlan();
        var session = CreateApprovedSession(otherPlan);
        var executor = CreateExecutor(
            events,
            plan,
            session,
            Assert.Single(plan.Operations));
        var store = new RecordingSessionStore(events);
        var useCase = new ApplyChangePlanUseCase(executor, store);

        var result = useCase.Execute(plan, session);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("SESSION_PLAN_MISMATCH", result.Error?.Code);
        Assert.Empty(events);
    }

    [Fact]
    public void RejectsChangedPlanMetadataWithoutPersistingOrExecuting()
    {
        var events = new List<string>();
        var plan = CreatePlan();
        var changedPlan = CopyPlan(plan, warnings: ["A warning that was not approved."]);
        var session = CreateApprovedSession(plan);
        var operation = Assert.Single(plan.Operations);
        var executor = CreateExecutor(events, plan, session, operation);
        var useCase = new ApplyChangePlanUseCase(
            executor,
            new RecordingSessionStore(events));

        var result = useCase.Execute(changedPlan, session);

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("INVALID_APPLY_PLAN", result.Error?.Code);
        Assert.Equal(["prepare"], events);
    }

    [Fact]
    public void PreservesBlockedStatusFromImmediatePreMutationRevalidation()
    {
        var events = new List<string>();
        var plan = CreatePlan();
        var session = CreateApprovedSession(plan);
        var operation = Assert.Single(plan.Operations);
        var blocked = Result<Operation>.Blocked(
            new Error("TARGET_CHANGED", ErrorCategory.TargetChanged));
        var executor = CreateExecutor(
            events,
            plan,
            session,
            operation,
            executeResult: blocked);
        var store = new RecordingSessionStore(events);
        var useCase = new ApplyChangePlanUseCase(executor, store);

        var result = useCase.Execute(plan, session);

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("TARGET_CHANGED", result.Error?.Code);
        Assert.Equal(ResultStatus.Blocked, Assert.Single(store.SavedSessions[^1].Results).Status);
    }

    [Fact]
    public void PartialExecutionWithoutErrorIsPersistedAndReturnedWithoutThrowing()
    {
        var events = new List<string>();
        var plan = CreatePlan();
        var session = CreateApprovedSession(plan);
        var operation = Assert.Single(plan.Operations);
        var executor = CreateExecutor(
            events,
            plan,
            session,
            operation,
            executeResult: Result<Operation>.Partial(
                operation,
                warnings: ["Execution outcome is incomplete."]));
        var store = new RecordingSessionStore(events);
        var useCase = new ApplyChangePlanUseCase(executor, store);

        var result = useCase.Execute(plan, session);

        Assert.Equal(ResultStatus.Partial, result.Status);
        var recorded = Assert.IsType<Session>(result.Value);
        Assert.Equal(SessionState.Applying, recorded.State);
        Assert.Equal(ResultStatus.Partial, Assert.Single(recorded.Results).Status);
        Assert.Equal(["Execution outcome is incomplete."], result.Warnings);
    }

    private static ChangePlan CopyPlan(
        ChangePlan source,
        IEnumerable<string>? warnings = null) =>
        new(
            source.PlanId,
            source.FeatureIds,
            source.Operations,
            source.Preconditions,
            source.Risk,
            warnings ?? source.Warnings,
            source.PrivilegeRequirements,
            source.RestartRequirements,
            source.RecoveryInformation);

    private static FakeControlledProcessActionExecutor CreateExecutor(
        List<string> events,
        ChangePlan plan,
        Session session,
        Operation operation,
        Guid? preparationPlanId = null,
        Result<Operation>? executeResult = null,
        bool prepareIsPartial = false)
    {
        var snapshot = new Snapshot(
            Guid.NewGuid(),
            session.SessionId,
            operation.OperationId,
            ApplyingAt,
            "ControlledProcessState",
            "Running",
            ExistenceSemantics.Existed);

        return new FakeControlledProcessActionExecutor(
            events,
            plan,
            preparationPlanId ?? plan.PlanId,
            snapshot,
            operation,
            executeResult,
            prepareIsPartial);
    }

    private static ChangePlan CreatePlan()
    {
        var result = new CreateControlledProcessChangePlanUseCase()
            .Execute(TargetIdentity, "4669f26e-1dcc-4a44-be85-29cc2b2ee6a0");

        return Assert.IsType<ChangePlan>(result.Value);
    }

    private static Session CreateApprovedSession(ChangePlan plan)
    {
        var planned = new Session(
            Guid.Parse("7ec72282-9502-43f2-8c5e-edfd6a750ab3"),
            SessionState.Planned,
            CreatedAt,
            CreatedAt,
            plan.PlanId,
            plan.Operations,
            [],
            [],
            null,
            RecoveryState.NotRequired);

        return planned.TransitionTo(SessionState.Approved, CreatedAt.AddSeconds(1));
    }

    private sealed class FakeControlledProcessActionExecutor(
        List<string> events,
        ChangePlan approvedPlan,
        Guid planId,
        Snapshot snapshot,
        Operation operation,
        Result<Operation>? executeResult = null,
        bool prepareIsPartial = false) : IControlledProcessActionExecutor
    {
        public FakePreparation Preparation { get; } = new(planId, snapshot);

        public Result<IControlledProcessActionPreparation> Prepare(
            ChangePlan plan,
            Session approvedSession)
        {
            events.Add("prepare");
            if (!approvedPlan.HasSameLogicalContentAs(plan))
            {
                return Result<IControlledProcessActionPreparation>.Blocked(
                    new Error("INVALID_APPLY_PLAN", ErrorCategory.InvalidPlan));
            }

            return prepareIsPartial
                ? Result<IControlledProcessActionPreparation>.Partial(
                    Preparation,
                    new Error("PREPARE_PARTIAL", ErrorCategory.PartialFailure))
                : Result<IControlledProcessActionPreparation>.Success(Preparation);
        }

        public Result<Operation> Execute(
            ChangePlan plan,
            Session applyingSession,
            IControlledProcessActionPreparation preparation)
        {
            events.Add("execute");
            return executeResult ?? Result<Operation>.Success(operation);
        }
    }

    private sealed class FakePreparation(Guid planId, Snapshot snapshot) : IControlledProcessActionPreparation
    {
        public Guid PreparationId { get; } = Guid.NewGuid();

        public Guid PlanId => planId;

        public Guid OperationId => snapshot.OperationId;

        public Snapshot Snapshot => snapshot;

        public bool IsDisposed { get; private set; }

        public void Dispose() => IsDisposed = true;
    }

    private sealed class RecordingSessionStore(
        List<string> events,
        Func<Session, Result<Session>>? save = null) : ISessionStore
    {
        public List<Session> SavedSessions { get; } = [];

        public Result<Session> Save(Session session)
        {
            events.Add($"save:{session.State}:{session.Results.Count}");
            SavedSessions.Add(session);
            return save?.Invoke(session) ?? Result<Session>.Success(session);
        }

        public Result<Session> Load(Guid sessionId) =>
            throw new InvalidOperationException("Load is not used by this test.");
    }

    private sealed class FixedTimeProvider(DateTimeOffset utcNow) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => utcNow;
    }
}
