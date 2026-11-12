using BeadWise.Core.Errors;
using BeadWise.Core.Planning;

namespace BeadWise.Core.Logging;

public static class ExecutionLogExtensions
{
    public static void WriteForPlan(this IExecutionLog log, ExecutionEvent step,
        ChangePlan plan, Guid? sessionId = null, Error? error = null)
    {
        foreach (var feature in plan.FeatureIds)
            log.Write(new ExecutionLogEntry(DateTimeOffset.UtcNow, step, sessionId, plan.PlanId,
                feature, plan.Operations.Count == 1 ? plan.Operations[0].OperationId : null,
                ErrorCode: error?.Code, ErrorCategory: error?.Category));
    }
}
