using System.ComponentModel;
using BeadWise.Application.Execution;
using BeadWise.Application.Planning;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
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
public sealed class ControlledProcessFixtureStateSourceTests
{
    private static readonly DateTimeOffset Now = new(2026, 11, 12, 12, 0, 0, TimeSpan.Zero);
    private static readonly ProcessIdentity Identity = new(42, Now);
    private static readonly Guid FixtureKey = Guid.Parse("7a8e87bf-4fdf-4e66-8db0-a0d2a1732e35");

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ObservesRunningStateWithoutMutation(bool running)
    {
        var plan = CreatePlan(Identity, FixtureKey);
        var target = CreateTarget(() => running);
        var source = new ControlledProcessFixtureStateSource(target, FixtureKey, plan, new FixedTimeProvider());
        var result = source.Observe(plan);
        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.Equal(ObservationAvailability.Available, result.Value!.Availability);
        Assert.Equal(running, result.Value.Value);
        Assert.Equal(Now, result.Value.Timestamp);
        Assert.Equal("System.Diagnostics.Process", result.Value.Source);
    }

    [Theory]
    [InlineData("disposed", "VERIFY_TARGET_UNAVAILABLE", ErrorCategory.Unavailable)]
    [InlineData("invalid", "VERIFY_TARGET_UNAVAILABLE", ErrorCategory.Unavailable)]
    [InlineData("access", "VERIFY_ACCESS_DENIED", ErrorCategory.AccessDenied)]
    [InlineData("win32-access", "VERIFY_ACCESS_DENIED", ErrorCategory.AccessDenied)]
    [InlineData("win32", "VERIFY_TARGET_UNAVAILABLE", ErrorCategory.Unavailable)]
    [InlineData("unsupported", "VERIFY_UNSUPPORTED", ErrorCategory.Unsupported)]
    public void ReadFailureCannotBeMistakenForExit(string kind, string code, ErrorCategory category)
    {
        var plan = CreatePlan(Identity, FixtureKey);
        var target = CreateTarget(() => throw kind switch
        {
            "disposed" => new ObjectDisposedException("process"),
            "invalid" => new InvalidOperationException(),
            "access" => new UnauthorizedAccessException(),
            "win32-access" => new Win32Exception(5),
            "win32" => new Win32Exception(6),
            _ => new PlatformNotSupportedException()
        });
        var result = new ControlledProcessFixtureStateSource(target, FixtureKey, plan).Observe(plan);
        Assert.NotEqual(ResultStatus.Success, result.Status);
        Assert.Null(result.Value);
        Assert.Equal(code, result.Error!.Code);
        Assert.Equal(category, result.Error.Category);
    }

    [Fact]
    public void ChangedIdentityIsRejected()
    {
        var plan = CreatePlan(Identity, FixtureKey);
        var target = new ControlledProcessTarget(Identity, () => true, () => true,
            () => Identity with { CreationTime = Now.AddSeconds(1) }, MutationForbidden);
        var result = new ControlledProcessFixtureStateSource(target, FixtureKey, plan).Observe(plan);
        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("TARGET_CHANGED", result.Error!.Code);
    }

    [Fact]
    public void LostOwnershipIsRejectedBeforeReadingState()
    {
        var plan = CreatePlan(Identity, FixtureKey);
        var target = new ControlledProcessTarget(Identity, () => false,
            () => throw new InvalidOperationException("State must not be read."), () => Identity, MutationForbidden);
        var result = new ControlledProcessFixtureStateSource(target, FixtureKey, plan).Observe(plan);
        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("FIXTURE_OWNERSHIP_MISMATCH", result.Error!.Code);
    }

    [Theory]
    [InlineData("fixture-key")]
    [InlineData("pid")]
    [InlineData("creation-time")]
    public void PlanMustBelongToCapturedFixture(string mismatch)
    {
        var plan = CreatePlan(mismatch switch
        {
            "pid" => Identity with { ProcessId = 43 },
            "creation-time" => Identity with { CreationTime = Now.AddSeconds(1) },
            _ => Identity
        }, mismatch == "fixture-key" ? Guid.NewGuid() : FixtureKey);
        var result = new ControlledProcessFixtureStateSource(CreateTarget(() => false), FixtureKey, plan).Observe(plan);
        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("FIXTURE_OWNERSHIP_MISMATCH", result.Error!.Code);
    }

    [Fact]
    public void ChangedLogicalPlanIsRejectedBeforeReadingState()
    {
        var plan = CreatePlan(Identity, FixtureKey);
        var changed = new ChangePlan(plan.PlanId, plan.FeatureIds, plan.Operations, plan.Preconditions,
            plan.Risk, ["Different warning"], plan.PrivilegeRequirements, plan.RestartRequirements, plan.RecoveryInformation);
        var source = new ControlledProcessFixtureStateSource(
            CreateTarget(() => throw new InvalidOperationException("State must not be read.")), FixtureKey, plan);
        var result = source.Observe(changed);
        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("INVALID_VERIFY_PLAN", result.Error!.Code);
    }

    [Fact]
    public async Task ApplyVerifyAndJsonReloadConfirmOnlyTheOriginalFixtureExited()
    {
        var root = Path.Combine(Path.GetTempPath(), "BeadWise.Tests", Guid.NewGuid().ToString("N"));
        try
        {
            await using var first = await ControlledFixture.StartAsync();
            await using var second = await ControlledFixture.StartAsync();
            var plan = CreatePlan(first.Identity, first.FixtureKey);
            var createdAt = DateTimeOffset.UtcNow;
            var approved = new Session(Guid.NewGuid(), SessionState.Approved, createdAt, createdAt, plan.PlanId,
                plan.Operations, [], [], null, RecoveryState.NotRequired);
            var source = first.CreateStateSource(plan);
            Assert.True(source.Observe(plan).Value!.Value);
            var store = new JsonSessionStore(root);
            var applied = new ApplyChangePlanUseCase(first.CreateActionExecutor(plan), store).Execute(plan, approved);
            Assert.Equal(ResultStatus.Success, applied.Status);
            Assert.Equal(SessionState.Applying, applied.Value!.State);

            var verified = await new VerifyChangePlanUseCase(source, store).ExecuteAsync(plan, applied.Value);

            Assert.Equal(ResultStatus.Success, verified.Status);
            Assert.False(source.Observe(plan).Value!.Value);
            Assert.True(second.IsRunning);
            var loaded = store.Load(approved.SessionId);
            Assert.Equal(ResultStatus.Success, loaded.Status);
            Assert.Equal(SessionState.Completed, loaded.Value!.State);
            Assert.Equal(applied.Value.Snapshots, loaded.Value.Snapshots);
            Assert.Equal(plan.PlanId, loaded.Value.PlanId);
            Assert.Equal(2, loaded.Value.Results.Count);
            Assert.Equal($"APPLY_DISPATCHED:{plan.Operations[0].OperationId:D}", loaded.Value.Results[0].Value);
            Assert.Equal($"VERIFY_SUCCEEDED:{plan.Operations[0].OperationId:D}", loaded.Value.Results[1].Value);
            Assert.All(loaded.Value.Results, result => Assert.Equal(ResultStatus.Success, result.Status));
        }
        finally
        {
            if (Directory.Exists(root)) Directory.Delete(root, recursive: true);
        }
    }

    [Fact]
    public async Task DisposedRealFixtureDoesNotCountAsConfirmedExit()
    {
        var fixture = await ControlledFixture.StartAsync();
        var plan = CreatePlan(fixture.Identity, fixture.FixtureKey);
        var source = fixture.CreateStateSource(plan);
        await fixture.DisposeAsync();
        var result = source.Observe(plan);
        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal("VERIFY_TARGET_UNAVAILABLE", result.Error!.Code);
    }

    private static void MutationForbidden() => Assert.Fail("Verify must not terminate a process.");
    private static ControlledProcessTarget CreateTarget(Func<bool> isRunning) =>
        new(Identity, () => true, isRunning, () => Identity, MutationForbidden);
    private static ChangePlan CreatePlan(ProcessIdentity identity, Guid key) =>
        new CreateControlledProcessChangePlanUseCase().Execute(identity, key.ToString("D")).Value!;
    private sealed class FixedTimeProvider : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => Now;
    }
}
