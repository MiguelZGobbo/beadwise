using BeadWise.Application.Execution;
using BeadWise.Application.Planning;
using BeadWise.Application.Recovery;
using BeadWise.Core.Errors;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Windows.Processes;
using BeadWise.Windows.Sessions;

namespace BeadWise.Windows.Tests.Fixtures;

[Collection(ControlledProcessCollection.Name)]
public sealed class ControlledFixtureRecoveryTests
{
    [Fact]
    public void LostOwnershipIsRejectedBeforeProcessStateCanBeRead()
    {
        var identity = new ProcessIdentity(42, DateTimeOffset.UtcNow);
        var target = new ControlledProcessTarget(identity, () => false,
            () => throw new InvalidOperationException("must not read"), () => identity, () => Assert.Fail("must not mutate"));
        var result = ControlledFixtureRecovery.ReadOwned(target);
        Assert.Equal("FIXTURE_OWNERSHIP_MISMATCH", result.Error!.Code);
    }

    [Fact]
    public void AStillRunningFixtureWithChangedIdentityIsRejected()
    {
        var identity = new ProcessIdentity(42, DateTimeOffset.UtcNow);
        var target = new ControlledProcessTarget(identity, () => true, () => true,
            () => identity with { CreationTime = identity.CreationTime.AddSeconds(1) },
            () => Assert.Fail("must not mutate"));
        var result = ControlledFixtureRecovery.ReadOwned(target);
        Assert.Equal("TARGET_CHANGED", result.Error!.Code);
    }

    [Fact]
    public void UnavailableStateIsNeverInterpretedAsConfirmedExit()
    {
        var identity = new ProcessIdentity(42, DateTimeOffset.UtcNow);
        var target = new ControlledProcessTarget(identity, () => true,
            () => throw new ObjectDisposedException("process"), () => identity, () => Assert.Fail("must not mutate"));
        Assert.Throws<ObjectDisposedException>(() => ControlledFixtureRecovery.ReadOwned(target));
    }

    [Fact]
    public async Task FullPipelineRecreatesOnlyTheOwnedFixtureAndRepeatedRecoveryReusesIt()
    {
        var root = Path.Combine(Path.GetTempPath(), "BeadWise.Tests", Guid.NewGuid().ToString("N"));
        try
        {
            await using var original = await ControlledProcessFixture.StartAsync();
            await using var unrelated = await ControlledProcessFixture.StartAsync();
            var plan = new CreateControlledProcessChangePlanUseCase()
                .Execute(original.Identity, original.FixtureKey.ToString("D")).Value!;
            var now = DateTimeOffset.UtcNow;
            var approved = new Session(Guid.NewGuid(), SessionState.Approved, now, now, plan.PlanId,
                plan.Operations, [], [], null, RecoveryState.NotRequired);
            var store = new JsonSessionStore(root);
            var applied = new ApplyChangePlanUseCase(original.CreateActionExecutor(plan), store).Execute(plan, approved);
            Assert.Equal(ResultStatus.Success, applied.Status);
            var verified = await new VerifyChangePlanUseCase(original.CreateStateSource(plan), store)
                .ExecuteAsync(plan, applied.Value);
            Assert.Equal(ResultStatus.Success, verified.Status);

            await using var recovery = original.CreateRecovery(plan);
            var useCase = new RecoverControlledFixtureUseCase(recovery, store);
            var sameRecovery = original.CreateRecovery(plan);
            Assert.Same(recovery, sameRecovery);
            var secondUseCase = new RecoverControlledFixtureUseCase(sameRecovery, store);
            var attempts = await Task.WhenAll(
                useCase.ExecuteAsync(plan, verified.Value, RecoveryReason.ExplicitFixtureReset),
                secondUseCase.ExecuteAsync(plan, verified.Value, RecoveryReason.ExplicitFixtureReset));
            Assert.All(attempts, attempt => Assert.Equal(ResultStatus.Success, attempt.Status));
            var restored = attempts[0];
            Assert.Equal(ResultStatus.Success, restored.Status);
            var evidence = restored.Value!.RecoveryEvidence!;
            Assert.Equal(evidence.RecoveredIdentity, attempts[1].Value!.RecoveryEvidence!.RecoveredIdentity);
            Assert.Equal(original.Identity, evidence.OriginalIdentity);
            Assert.NotEqual(original.Identity, evidence.RecoveredIdentity);
            Assert.True(unrelated.IsRunning);
            Assert.False(original.IsRunning);
            Assert.True(recovery.IsRunning);
            Assert.Equal(verified.Value!.Snapshots, restored.Value.Snapshots);
            var loaded = store.Load(approved.SessionId).Value!;
            Assert.Equal(evidence, loaded.RecoveryEvidence);

            var repeated = await useCase.ExecuteAsync(plan, loaded, RecoveryReason.ExplicitFixtureReset);
            Assert.Equal(ResultStatus.Success, repeated.Status);
            Assert.Equal(evidence.RecoveredIdentity, repeated.Value!.RecoveryEvidence!.RecoveredIdentity);
            Assert.True(unrelated.IsRunning);
        }
        finally
        {
            if (Directory.Exists(root)) Directory.Delete(root, recursive: true);
        }
    }

    [Fact]
    public async Task RunningOriginalFixtureIsReusedForFailureRecovery()
    {
        await using var original = await ControlledProcessFixture.StartAsync();
        var plan = new CreateControlledProcessChangePlanUseCase()
            .Execute(original.Identity, original.FixtureKey.ToString("D")).Value!;
        await using var recovery = original.CreateRecovery(plan);

        var result = await recovery.RestoreAsync(plan, CancellationToken.None);
        var observed = await recovery.VerifyAsync(plan, original.Identity, CancellationToken.None);

        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.Equal(original.Identity, result.Value);
        Assert.True(observed.Value);
        Assert.True(original.IsRunning);
    }

    [Fact]
    public async Task DisposedOriginalCannotAuthorizeReplacement()
    {
        var original = await ControlledProcessFixture.StartAsync();
        var plan = new CreateControlledProcessChangePlanUseCase()
            .Execute(original.Identity, original.FixtureKey.ToString("D")).Value!;
        await using var recovery = original.CreateRecovery(plan);
        await original.DisposeAsync();

        var result = await recovery.RestoreAsync(plan, CancellationToken.None);
        Assert.NotEqual(ResultStatus.Success, result.Status);
        Assert.Null(recovery.ReplacementIdentity);
    }

    [Fact]
    public async Task DifferentPlanCannotAuthorizeFixtureRecovery()
    {
        await using var original = await ControlledProcessFixture.StartAsync();
        var factory = new CreateControlledProcessChangePlanUseCase();
        var plan = factory.Execute(original.Identity, original.FixtureKey.ToString("D")).Value!;
        var other = factory.Execute(original.Identity, original.FixtureKey.ToString("D")).Value!;
        await using var recovery = original.CreateRecovery(plan);

        Assert.Throws<InvalidOperationException>(() => original.CreateRecovery(other));

        var result = await recovery.RestoreAsync(other, CancellationToken.None);
        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("INVALID_RECOVERY_PLAN", result.Error!.Code);
        Assert.True(original.IsRunning);
    }

    [Fact]
    public async Task DisposingOriginalAlsoDisposesItsCachedReplacement()
    {
        var original = await ControlledProcessFixture.StartAsync();
        var plan = new CreateControlledProcessChangePlanUseCase()
            .Execute(original.Identity, original.FixtureKey.ToString("D")).Value!;
        var recovery = original.CreateRecovery(plan);
        var now = DateTimeOffset.UtcNow;
        var approved = new Session(Guid.NewGuid(), SessionState.Approved, now, now, plan.PlanId,
            plan.Operations, [], [], null, RecoveryState.NotRequired);
        var root = Path.Combine(Path.GetTempPath(), "BeadWise.Tests", Guid.NewGuid().ToString("N"));
        try
        {
            var store = new JsonSessionStore(root);
            var applied = new ApplyChangePlanUseCase(original.CreateActionExecutor(plan), store).Execute(plan, approved);
            Assert.Equal(ResultStatus.Success, applied.Status);
            var verified = await new VerifyChangePlanUseCase(original.CreateStateSource(plan), store)
                .ExecuteAsync(plan, applied.Value);
            Assert.Equal(ResultStatus.Success, verified.Status);
            var restored = await new RecoverControlledFixtureUseCase(recovery, store)
                .ExecuteAsync(plan, verified.Value, RecoveryReason.ExplicitFixtureReset);
            Assert.Equal(ResultStatus.Success, restored.Status);
            Assert.True(recovery.IsRunning);

            await original.DisposeAsync();
            await original.DisposeAsync();
            await recovery.DisposeAsync();
            Assert.False(recovery.IsRunning);
            Assert.Throws<ObjectDisposedException>(() => original.CreateRecovery(plan));
        }
        finally
        {
            await original.DisposeAsync();
            if (Directory.Exists(root)) Directory.Delete(root, recursive: true);
        }
    }
}
