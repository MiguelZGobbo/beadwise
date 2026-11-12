using BeadWise.Application.Execution;
using BeadWise.Application.Planning;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Planning;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Windows.Processes;
using BeadWise.Windows.Sessions;
using BeadWise.Windows.Tests.Fixtures;
using ControlledFixture = BeadWise.Windows.Tests.Fixtures.ControlledProcessFixture;

namespace BeadWise.Windows.Tests.Processes;

[Collection(ControlledProcessCollection.Name)]
public sealed class ControlledProcessFixtureActionExecutorTests
{
    private static readonly DateTimeOffset CreatedAt =
        new(2026, 1, 1, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public async Task RealApplyStopsOnlyTheOwnedFixtureInstance()
    {
        await using var first = await ControlledFixture.StartAsync();
        await using var second = await ControlledFixture.StartAsync();
        var plan = CreatePlan(first.Identity, first.FixtureKey);
        var session = CreateApprovedSession(plan);
        var executor = first.CreateActionExecutor(plan);

        using var preparation = Assert.IsAssignableFrom<IControlledProcessActionPreparation>(
            executor.Prepare(plan, session).Value);
        var applying = CreateApplyingSession(session, preparation);

        var result = executor.Execute(plan, applying, preparation);

        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.Same(Assert.Single(plan.Operations), result.Value);
        await WaitUntilAsync(() => !first.IsRunning);
        Assert.True(second.IsRunning);
    }

    [Fact]
    public async Task ApplyUseCasePersistsSnapshotAndResultAroundRealFixtureMutation()
    {
        var storeRoot = Path.Combine(
            Path.GetTempPath(),
            "BeadWise.Tests",
            Guid.NewGuid().ToString("N"));

        try
        {
            await using var first = await ControlledFixture.StartAsync();
            await using var second = await ControlledFixture.StartAsync();
            var plan = CreatePlan(first.Identity, first.FixtureKey);
            var approvedSession = CreateApprovedSession(plan);
            var store = new JsonSessionStore(storeRoot);
            var useCase = new ApplyChangePlanUseCase(
                first.CreateActionExecutor(plan),
                store);

            var result = useCase.Execute(plan, approvedSession);

            Assert.Equal(ResultStatus.Success, result.Status);
            var persisted = Assert.IsType<Session>(
                store.Load(approvedSession.SessionId).Value);
            Assert.Equal(SessionState.Applying, persisted.State);
            Assert.Equal(plan.PlanId, persisted.PlanId);
            Assert.Equal(
                Assert.Single(plan.Operations).OperationId,
                Assert.Single(persisted.Operations).OperationId);
            Assert.Single(persisted.Snapshots);
            Assert.Equal(ResultStatus.Success, Assert.Single(persisted.Results).Status);
            await WaitUntilAsync(() => !first.IsRunning);
            Assert.True(second.IsRunning);
        }
        finally
        {
            if (Directory.Exists(storeRoot))
            {
                Directory.Delete(storeRoot, recursive: true);
            }
        }
    }

    [Fact]
    public void SamePidWithDifferentCreationTimeIsRejectedWithoutMutation()
    {
        var expectedIdentity = new ProcessIdentity(42, CreatedAt);
        var changedIdentity = new ProcessIdentity(42, CreatedAt.AddSeconds(1));
        var killCount = 0;
        var target = new ControlledProcessTarget(
            expectedIdentity,
            isOwnedLaunchContext: () => true,
            isRunning: () => true,
            readIdentity: () => changedIdentity,
            kill: () => killCount++);
        var fixtureKey = Guid.Parse("df946930-da47-43ce-be8f-6c777f109b16");
        var plan = CreatePlan(expectedIdentity, fixtureKey);
        var session = CreateApprovedSession(plan);
        var executor = new ControlledProcessFixtureActionExecutor(target, fixtureKey, plan);

        var result = executor.Prepare(plan, session);

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("TARGET_CHANGED", result.Error?.Code);
        Assert.Equal(0, killCount);
    }

    [Fact]
    public void WrongFixtureKeyInExecutionResourceIsRejectedWithoutMutation()
    {
        var fixtureKey = Guid.Parse("311f24d6-57a7-44d0-b7fd-630252119e65");
        var plan = CreatePlan(
            new ProcessIdentity(42, CreatedAt),
            Guid.Parse("32cac4ee-54d1-42af-970e-b65c647116ec"));
        var session = CreateApprovedSession(plan);
        var killCount = 0;
        var target = CreateTarget(new ProcessIdentity(42, CreatedAt), () => killCount++);
        var executor = new ControlledProcessFixtureActionExecutor(target, fixtureKey, plan);

        var result = executor.Prepare(plan, session);

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("FIXTURE_OWNERSHIP_MISMATCH", result.Error?.Code);
        Assert.Equal(0, killCount);
    }

    [Fact]
    public async Task WrongFixtureKeyLeavesTheRealFixtureRunning()
    {
        await using var fixture = await ControlledFixture.StartAsync();
        var plan = CreatePlan(fixture.Identity, Guid.NewGuid());
        var executor = fixture.CreateActionExecutor(plan);

        var result = executor.Prepare(plan, CreateApprovedSession(plan));

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("FIXTURE_OWNERSHIP_MISMATCH", result.Error?.Code);
        Assert.True(fixture.IsRunning);
    }

    [Fact]
    public void ChangedPlanMetadataIsRejectedByTheBoundExecutorWithoutMutation()
    {
        var fixtureKey = Guid.Parse("f9c50abe-6ba5-4caf-b1f3-7e0ec6639826");
        var identity = new ProcessIdentity(42, CreatedAt);
        var approvedPlan = CreatePlan(identity, fixtureKey);
        var changedPlan = new ChangePlan(
            approvedPlan.PlanId,
            approvedPlan.FeatureIds,
            approvedPlan.Operations,
            approvedPlan.Preconditions,
            approvedPlan.Risk,
            [.. approvedPlan.Warnings, "Unapproved warning."],
            approvedPlan.PrivilegeRequirements,
            approvedPlan.RestartRequirements,
            approvedPlan.RecoveryInformation);
        var killCount = 0;
        var executor = new ControlledProcessFixtureActionExecutor(
            CreateTarget(identity, () => killCount++),
            fixtureKey,
            approvedPlan);

        var result = executor.Prepare(changedPlan, CreateApprovedSession(changedPlan));

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("INVALID_APPLY_PLAN", result.Error?.Code);
        Assert.Equal(0, killCount);
    }

    [Fact]
    public void UnexpectedOperationStateIsRejectedWithoutMutation()
    {
        var fixtureKey = Guid.Parse("ac6222fc-8dfa-4bc1-93c2-6f1773168e02");
        var identity = new ProcessIdentity(42, CreatedAt);
        var valid = CreatePlan(identity, fixtureKey);
        var invalidOperation = CopyOperation(
            Assert.Single(valid.Operations),
            currentState: "Stopped");
        var invalid = CopyPlan(valid, invalidOperation);
        var session = CreateApprovedSession(invalid);
        var killCount = 0;
        var executor = new ControlledProcessFixtureActionExecutor(
            CreateTarget(identity, () => killCount++),
            fixtureKey,
            invalid);

        var result = executor.Prepare(invalid, session);

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("CONTROLLED_OPERATION_NOT_AUTHORIZED", result.Error?.Code);
        Assert.Equal(0, killCount);
    }

    [Fact]
    public void UnexpectedTargetStateIsRejectedWithoutMutation()
    {
        var fixtureKey = Guid.Parse("025de73c-18ac-4a57-b7bb-77ccf5d3cdf9");
        var identity = new ProcessIdentity(42, CreatedAt);
        var valid = CreatePlan(identity, fixtureKey);
        var source = Assert.Single(valid.Operations);
        var invalidOperation = new Operation(
            source.OperationId,
            source.Description,
            source.CurrentState,
            "Restarted",
            source.Risk,
            source.PrivilegeRequirement,
            source.RestartRequirement,
            source.Reversibility,
            source.ExecutionResources);
        var invalid = CopyPlan(valid, invalidOperation);
        var killCount = 0;
        var executor = new ControlledProcessFixtureActionExecutor(
            CreateTarget(identity, () => killCount++),
            fixtureKey,
            invalid);

        var result = executor.Prepare(invalid, CreateApprovedSession(invalid));

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("CONTROLLED_OPERATION_NOT_AUTHORIZED", result.Error?.Code);
        Assert.Equal(0, killCount);
    }

    [Fact]
    public void UnexpectedExecutionResourceIsRejectedWithoutMutation()
    {
        var fixtureKey = Guid.Parse("a696aa73-4bd8-456c-af4e-f0cbf9d601c5");
        var identity = new ProcessIdentity(42, CreatedAt);
        var valid = CreatePlan(identity, fixtureKey);
        var source = Assert.Single(valid.Operations);
        var invalidOperation = new Operation(
            source.OperationId,
            source.Description,
            source.CurrentState,
            source.TargetState,
            source.Risk,
            source.PrivilegeRequirement,
            source.RestartRequirement,
            source.Reversibility,
            [new ExecutionResource("Service", source.ExecutionResources[0].ResourceKey)]);
        var invalid = CopyPlan(valid, invalidOperation);
        var killCount = 0;
        var executor = new ControlledProcessFixtureActionExecutor(
            CreateTarget(identity, () => killCount++),
            fixtureKey,
            invalid);

        var result = executor.Prepare(invalid, CreateApprovedSession(invalid));

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("FIXTURE_OWNERSHIP_MISMATCH", result.Error?.Code);
        Assert.Equal(0, killCount);
    }

    [Fact]
    public void UnownedLaunchContextIsRejectedWithoutMutation()
    {
        var fixtureKey = Guid.Parse("cbefc93f-6cd2-4d85-859d-c4da7d48b5af");
        var identity = new ProcessIdentity(42, CreatedAt);
        var plan = CreatePlan(identity, fixtureKey);
        var killCount = 0;
        var target = new ControlledProcessTarget(
            identity,
            isOwnedLaunchContext: () => false,
            isRunning: () => true,
            readIdentity: () => identity,
            kill: () => killCount++);
        var executor = new ControlledProcessFixtureActionExecutor(target, fixtureKey, plan);

        var result = executor.Prepare(plan, CreateApprovedSession(plan));

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("FIXTURE_OWNERSHIP_MISMATCH", result.Error?.Code);
        Assert.Equal(0, killCount);
    }

    [Fact]
    public void ExitedTargetIsUnavailableAndNeverMutated()
    {
        var fixtureKey = Guid.Parse("665b2c7f-1124-4cea-9fe9-405337b18ac6");
        var identity = new ProcessIdentity(42, CreatedAt);
        var plan = CreatePlan(identity, fixtureKey);
        var session = CreateApprovedSession(plan);
        var killCount = 0;
        var target = new ControlledProcessTarget(
            identity,
            isOwnedLaunchContext: () => true,
            isRunning: () => false,
            readIdentity: () => identity,
            kill: () => killCount++);
        var executor = new ControlledProcessFixtureActionExecutor(target, fixtureKey, plan);

        var result = executor.Prepare(plan, session);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("TARGET_NOT_RUNNING", result.Error?.Code);
        Assert.Equal(0, killCount);
    }

    [Fact]
    public async Task AlreadyExitedRealFixtureFailsClosed()
    {
        var fixture = await ControlledFixture.StartAsync();
        var plan = CreatePlan(fixture.Identity, fixture.FixtureKey);
        var executor = fixture.CreateActionExecutor(plan);
        var session = CreateApprovedSession(plan);
        await fixture.DisposeAsync();

        var result = executor.Prepare(plan, session);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("TARGET_NOT_RUNNING", result.Error?.Code);
        await fixture.DisposeAsync();
    }

    [Fact]
    public void ExitBetweenPrepareAndExecuteIsReportedWithoutCallingMutation()
    {
        var fixtureKey = Guid.Parse("7f207013-a57f-4928-a2af-a21f31138df4");
        var identity = new ProcessIdentity(42, CreatedAt);
        var plan = CreatePlan(identity, fixtureKey);
        var session = CreateApprovedSession(plan);
        var runningReadCount = 0;
        var killCount = 0;
        var target = new ControlledProcessTarget(
            identity,
            isOwnedLaunchContext: () => true,
            isRunning: () => Interlocked.Increment(ref runningReadCount) == 1,
            readIdentity: () => identity,
            kill: () => killCount++);
        var executor = new ControlledProcessFixtureActionExecutor(target, fixtureKey, plan);
        using var preparation = Assert.IsAssignableFrom<IControlledProcessActionPreparation>(
            executor.Prepare(plan, session).Value);
        var applying = CreateApplyingSession(session, preparation);

        var result = executor.Execute(plan, applying, preparation);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("TARGET_EXITED_DURING_APPLY", result.Error?.Code);
        Assert.Equal(0, killCount);
    }

    [Fact]
    public async Task OnePreparationCanBeConsumedByOnlyOneConcurrentExecute()
    {
        var fixtureKey = Guid.Parse("31f2f9de-eb99-44a5-88c9-465e5009a243");
        var identity = new ProcessIdentity(42, CreatedAt);
        var plan = CreatePlan(identity, fixtureKey);
        var session = CreateApprovedSession(plan);
        var killCount = 0;
        var executor = new ControlledProcessFixtureActionExecutor(
            CreateTarget(identity, () => Interlocked.Increment(ref killCount)),
            fixtureKey,
            plan);
        using var preparation = Assert.IsAssignableFrom<IControlledProcessActionPreparation>(
            executor.Prepare(plan, session).Value);
        var applying = CreateApplyingSession(session, preparation);

        var results = await Task.WhenAll(
            Task.Run(() => executor.Execute(plan, applying, preparation)),
            Task.Run(() => executor.Execute(plan, applying, preparation)));

        Assert.Equal(1, results.Count(result => result.Status is ResultStatus.Success));
        Assert.Equal(1, results.Count(result => result.Status is ResultStatus.Blocked));
        Assert.Contains(results, result => result.Error?.Code == "PREPARATION_ALREADY_USED");
        Assert.Equal(1, killCount);
    }

    [Fact]
    public void DisposedPreparationFailsClosedWithoutMutation()
    {
        var fixtureKey = Guid.Parse("81ea9931-f47d-42b9-8cda-3f6735273eb9");
        var identity = new ProcessIdentity(42, CreatedAt);
        var plan = CreatePlan(identity, fixtureKey);
        var session = CreateApprovedSession(plan);
        var killCount = 0;
        var executor = new ControlledProcessFixtureActionExecutor(
            CreateTarget(identity, () => killCount++),
            fixtureKey,
            plan);
        var preparation = Assert.IsAssignableFrom<IControlledProcessActionPreparation>(
            executor.Prepare(plan, session).Value);
        var applying = CreateApplyingSession(session, preparation);
        preparation.Dispose();

        var result = executor.Execute(plan, applying, preparation);

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("PREPARATION_ALREADY_USED", result.Error?.Code);
        Assert.Equal(0, killCount);
    }

    [Fact]
    public void AccessDeniedDuringMutationIsReturnedAsStructuredFailure()
    {
        var fixtureKey = Guid.Parse("4bc05c1e-e152-4f25-bc79-6d0d4c48cffe");
        var identity = new ProcessIdentity(42, CreatedAt);
        var plan = CreatePlan(identity, fixtureKey);
        var session = CreateApprovedSession(plan);
        var executor = new ControlledProcessFixtureActionExecutor(
            CreateTarget(identity, () => throw new UnauthorizedAccessException()),
            fixtureKey,
            plan);
        using var preparation = Assert.IsAssignableFrom<IControlledProcessActionPreparation>(
            executor.Prepare(plan, session).Value);

        var result = executor.Execute(
            plan,
            CreateApplyingSession(session, preparation),
            preparation);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("APPLY_ACCESS_DENIED", result.Error?.Code);
        Assert.Equal(ErrorCategory.AccessDenied, result.Error?.Category);
    }

    [Fact]
    public void DisposedTargetReferenceFailsClosedWithoutMutation()
    {
        var fixtureKey = Guid.Parse("91081c77-e4d5-418f-babe-b4fa3815ada7");
        var identity = new ProcessIdentity(42, CreatedAt);
        var plan = CreatePlan(identity, fixtureKey);
        var target = new ControlledProcessTarget(
            identity,
            isOwnedLaunchContext: () => throw new ObjectDisposedException("process"),
            isRunning: () => throw new InvalidOperationException("must not continue"),
            readIdentity: () => throw new InvalidOperationException("must not continue"),
            kill: () => throw new InvalidOperationException("must not mutate"));
        var executor = new ControlledProcessFixtureActionExecutor(target, fixtureKey, plan);

        var result = executor.Prepare(plan, CreateApprovedSession(plan));

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("TARGET_NOT_RUNNING", result.Error?.Code);
    }

    [Fact]
    public void EachPrepareReturnsAnIndependentSingleUseAuthorization()
    {
        var fixtureKey = Guid.Parse("9202f3cc-b0e9-40fb-807d-0081060d24ed");
        var identity = new ProcessIdentity(42, CreatedAt);
        var plan = CreatePlan(identity, fixtureKey);
        var session = CreateApprovedSession(plan);
        var executor = new ControlledProcessFixtureActionExecutor(
            CreateTarget(identity, () => { }),
            fixtureKey,
            plan);

        using var first = Assert.IsAssignableFrom<IControlledProcessActionPreparation>(
            executor.Prepare(plan, session).Value);
        using var second = Assert.IsAssignableFrom<IControlledProcessActionPreparation>(
            executor.Prepare(plan, session).Value);

        Assert.NotEqual(first.PreparationId, second.PreparationId);
        Assert.NotSame(first, second);
        first.Dispose();
        var secondResult = executor.Execute(
            plan,
            CreateApplyingSession(session, second),
            second);
        Assert.Equal(ResultStatus.Success, secondResult.Status);
    }

    [Fact]
    public void PreparationFromAnotherExecutorCannotAuthorizeMutation()
    {
        var fixtureKey = Guid.Parse("37803b17-23db-43e4-a7ce-1c5ad714105a");
        var identity = new ProcessIdentity(42, CreatedAt);
        var plan = CreatePlan(identity, fixtureKey);
        var session = CreateApprovedSession(plan);
        var firstKillCount = 0;
        var secondKillCount = 0;
        var firstExecutor = new ControlledProcessFixtureActionExecutor(
            CreateTarget(identity, () => firstKillCount++),
            fixtureKey,
            plan);
        var secondExecutor = new ControlledProcessFixtureActionExecutor(
            CreateTarget(identity, () => secondKillCount++),
            fixtureKey,
            plan);
        using var preparation = Assert.IsAssignableFrom<IControlledProcessActionPreparation>(
            firstExecutor.Prepare(plan, session).Value);

        var result = secondExecutor.Execute(
            plan,
            CreateApplyingSession(session, preparation),
            preparation);

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("PREPARATION_OWNER_MISMATCH", result.Error?.Code);
        Assert.Equal(0, firstKillCount);
        Assert.Equal(0, secondKillCount);
    }

    [Fact]
    public void PreparationFromAnotherSessionCannotAuthorizeMutation()
    {
        var fixtureKey = Guid.Parse("f207a4c4-9daf-4bd2-8a79-a80d198f60be");
        var identity = new ProcessIdentity(42, CreatedAt);
        var plan = CreatePlan(identity, fixtureKey);
        var originalSession = CreateApprovedSession(plan);
        var otherSession = CreateApprovedSession(plan);
        var killCount = 0;
        var executor = new ControlledProcessFixtureActionExecutor(
            CreateTarget(identity, () => killCount++),
            fixtureKey,
            plan);
        using var preparation = Assert.IsAssignableFrom<IControlledProcessActionPreparation>(
            executor.Prepare(plan, originalSession).Value);

        var result = executor.Execute(
            plan,
            CreateApplyingSession(otherSession, preparation),
            preparation);

        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("INVALID_APPLY_PREPARATION", result.Error?.Code);
        Assert.Equal(0, killCount);
    }

    private static ControlledProcessTarget CreateTarget(
        ProcessIdentity identity,
        Action kill) =>
        new(
            identity,
            isOwnedLaunchContext: () => true,
            isRunning: () => true,
            readIdentity: () => identity,
            kill);

    private static ChangePlan CreatePlan(ProcessIdentity identity, Guid fixtureKey)
    {
        var result = new CreateControlledProcessChangePlanUseCase()
            .Execute(identity, fixtureKey.ToString("D"));
        return Assert.IsType<ChangePlan>(result.Value);
    }

    private static Session CreateApprovedSession(ChangePlan plan)
    {
        var planned = new Session(
            Guid.NewGuid(),
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

    private static Session CreateApplyingSession(
        Session approved,
        IControlledProcessActionPreparation preparation) =>
        new(
            approved.SessionId,
            SessionState.Applying,
            approved.CreatedAt,
            preparation.Snapshot.CapturedAt,
            approved.PlanId,
            approved.Operations,
            [preparation.Snapshot],
            [],
            null,
            RecoveryState.NotRequired);

    private static Operation CopyOperation(
        Operation source,
        string? currentState = null) =>
        new(
            source.OperationId,
            source.Description,
            currentState ?? source.CurrentState,
            source.TargetState,
            source.Risk,
            source.PrivilegeRequirement,
            source.RestartRequirement,
            source.Reversibility,
            source.ExecutionResources);

    private static ChangePlan CopyPlan(ChangePlan source, Operation operation) =>
        new(
            source.PlanId,
            source.FeatureIds,
            [operation],
            source.Preconditions,
            source.Risk,
            source.Warnings,
            source.PrivilegeRequirements,
            source.RestartRequirements,
            source.RecoveryInformation);

    private static async Task WaitUntilAsync(Func<bool> condition)
    {
        var startedAt = System.Diagnostics.Stopwatch.GetTimestamp();
        while (!condition())
        {
            if (System.Diagnostics.Stopwatch.GetElapsedTime(startedAt) >= TimeSpan.FromSeconds(5))
            {
                Assert.Fail("The controlled fixture did not reach the expected state in time.");
            }

            await Task.Delay(TimeSpan.FromMilliseconds(25));
        }
    }
}
