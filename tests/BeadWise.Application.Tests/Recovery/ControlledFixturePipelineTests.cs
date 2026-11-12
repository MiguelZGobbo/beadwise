using BeadWise.Application.Execution;
using BeadWise.Application.Planning;
using BeadWise.Application.Recovery;
using BeadWise.Core.Contracts;
using BeadWise.Core.Observations;
using BeadWise.Core.Operations;
using BeadWise.Core.Planning;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Snapshots;

namespace BeadWise.Application.Tests.Recovery;

public sealed class ControlledFixturePipelineTests
{
    [Fact]
    public async Task PreviewApplyVerifyAndRecoveryShareThePlanAndPreserveTheSnapshot()
    {
        var fixture = new FakeFixture();
        var store = new MemoryStore();
        var plan = new CreateControlledProcessChangePlanUseCase()
            .Execute(fixture.OriginalIdentity, "af440a71-5983-4056-944a-2fbc9863d739").Value!;
        var preview = new PreviewChangePlanUseCase().Execute(plan);
        Assert.Same(plan, preview.Value);
        Assert.True(fixture.Running);
        var now = DateTimeOffset.UtcNow;
        var approved = new Session(Guid.NewGuid(), SessionState.Approved, now, now, plan.PlanId,
            plan.Operations, [], [], null, RecoveryState.NotRequired);

        var applied = new ApplyChangePlanUseCase(fixture, store).Execute(preview.Value, approved);
        Assert.Equal(ResultStatus.Success, applied.Status);
        Assert.False(fixture.Running);
        var verified = await new VerifyChangePlanUseCase(fixture, store).ExecuteAsync(plan, applied.Value);
        Assert.Equal(ResultStatus.Success, verified.Status);
        var snapshot = Assert.Single(verified.Value!.Snapshots);

        var useCase = new RecoverControlledFixtureUseCase(fixture, store);
        var recovered = await useCase.ExecuteAsync(plan, verified.Value, RecoveryReason.ExplicitFixtureReset);
        Assert.Equal(ResultStatus.Success, recovered.Status);
        Assert.True(fixture.Running);
        Assert.Equal(snapshot, Assert.Single(recovered.Value!.Snapshots));
        Assert.Equal(fixture.OriginalIdentity, recovered.Value.RecoveryEvidence!.OriginalIdentity);
        Assert.Equal(fixture.CurrentIdentity, recovered.Value.RecoveryEvidence.RecoveredIdentity);
        Assert.NotEqual(fixture.OriginalIdentity, fixture.CurrentIdentity);

        var repeated = await useCase.ExecuteAsync(plan, store.Current, RecoveryReason.ExplicitFixtureReset);
        Assert.Equal(ResultStatus.Success, repeated.Status);
        Assert.Equal(1, fixture.Creations);
        Assert.Equal(fixture.CurrentIdentity, repeated.Value!.RecoveryEvidence!.RecoveredIdentity);
    }

    private sealed class FakeFixture : IControlledProcessActionExecutor, IControlledProcessStateSource,
        IControlledFixtureRecovery
    {
        public ProcessIdentity OriginalIdentity { get; } = new(42, DateTimeOffset.UtcNow.AddMinutes(-1));
        public ProcessIdentity CurrentIdentity { get; private set; }
        public bool Running { get; private set; } = true;
        public int Creations { get; private set; }

        public FakeFixture() => CurrentIdentity = OriginalIdentity;

        public Result<IControlledProcessActionPreparation> Prepare(ChangePlan plan, Session session)
        {
            var operation = plan.Operations[0];
            var resource = operation.ExecutionResources[0];
            var snapshot = new Snapshot(Guid.NewGuid(), session.SessionId, operation.OperationId,
                DateTimeOffset.UtcNow, "ControlledProcessState",
                $"Running|resource={resource.ResourceType}:{resource.ResourceKey}|action={operation.OperationId:D}",
                ExistenceSemantics.Existed);
            return Result<IControlledProcessActionPreparation>.Success(new Preparation(plan.PlanId, operation.OperationId, snapshot));
        }

        public Result<Operation> Execute(ChangePlan plan, Session session, IControlledProcessActionPreparation preparation)
        {
            Running = false;
            return Result<Operation>.Success(plan.Operations[0]);
        }

        public Result<Observation<bool>> Observe(ChangePlan plan) => Result<Observation<bool>>.Success(
            Observation<bool>.Available(Running, "fake controlled fixture", DateTimeOffset.UtcNow));

        public Task<Result<ProcessIdentity>> RestoreAsync(ChangePlan plan, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Running)
            {
                CurrentIdentity = new ProcessIdentity(43, DateTimeOffset.UtcNow);
                Creations++;
                Running = true;
            }
            return Task.FromResult(Result<ProcessIdentity>.Success(CurrentIdentity));
        }

        public Task<Result<bool>> VerifyAsync(ChangePlan plan, ProcessIdentity identity, CancellationToken cancellationToken) =>
            Task.FromResult(Result<bool>.Success(Running && identity == CurrentIdentity));
    }

    private sealed class Preparation(Guid planId, Guid operationId, Snapshot snapshot) : IControlledProcessActionPreparation
    {
        public Guid PreparationId { get; } = Guid.NewGuid();
        public Guid PlanId { get; } = planId;
        public Guid OperationId { get; } = operationId;
        public Snapshot Snapshot { get; } = snapshot;
        public void Dispose() { }
    }

    private sealed class MemoryStore : ISessionStore
    {
        public Session? Current { get; private set; }
        public Result<Session> Save(Session session)
        {
            Current = session;
            return Result<Session>.Success(session);
        }
        public Result<Session> Load(Guid sessionId) => Result<Session>.Success(Current!);
    }
}
