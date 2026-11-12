using BeadWise.Application.Planning;
using BeadWise.Application.Sessions;
using BeadWise.Core.Contracts;
using BeadWise.Core.Logging;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;

namespace BeadWise.Application.Tests.Planning;

public sealed class ExecutionLoggingTests
{
    [Fact]
    public void PlanPreviewAndPersistenceShareCorrelationWithoutCopyingTargetData()
    {
        var log = new RecordingLog();
        var plan = new CreateControlledProcessChangePlanUseCase(log).Execute(
            new ProcessIdentity(42, DateTimeOffset.UtcNow.AddMinutes(-1)), "private-fixture-context").Value!;
        new PreviewChangePlanUseCase(log).Execute(plan);
        var id = Guid.NewGuid();
        new CreateAndPersistSessionUseCase(new Store(), log).Execute(plan, id, DateTimeOffset.UtcNow);

        Assert.Equal(6, log.Entries.Count);
        Assert.All(log.Entries, entry =>
        {
            Assert.Equal(plan.PlanId, entry.PlanId);
            Assert.Equal(plan.Operations[0].OperationId, entry.OperationId);
            Assert.Contains(entry.FeatureId!, plan.FeatureIds);
        });
        Assert.All(log.Entries.Where(entry => entry.Event is ExecutionEvent.SessionPersisted),
            entry => Assert.Equal(id, entry.SessionId));
        Assert.DoesNotContain("private-fixture-context", System.Text.Json.JsonSerializer.Serialize(log.Entries));
    }

    private sealed class RecordingLog : IExecutionLog
    {
        public List<ExecutionLogEntry> Entries { get; } = [];
        public void Write(ExecutionLogEntry entry) => Entries.Add(entry);
    }

    private sealed class Store : ISessionStore
    {
        public Result<Session> Save(Session session) => Result<Session>.Success(session);
        public Result<Session> Load(Guid sessionId) => throw new NotSupportedException();
    }
}
