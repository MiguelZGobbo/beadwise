using BeadWise.Core.Errors;
using BeadWise.Core.Planning;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Snapshots;

namespace BeadWise.Application.Execution;

internal static class VerifyChangePlanValidation
{
    public static Error? Validate(ChangePlan? plan, Session? session)
    {
        if (plan is null || plan.Operations.Count != 1 ||
            plan.Operations[0].CurrentState != "Running" || plan.Operations[0].TargetState != "NotRunning")
        {
            return Invalid("INVALID_VERIFY_PLAN", "Verify requires one controlled process stop operation.");
        }

        if (session is null || session.State is not (SessionState.Applying or SessionState.Verifying))
        {
            return Invalid("SESSION_NOT_VERIFYING", "Verify requires an applying or verifying session.");
        }

        var operation = plan.Operations[0];
        if (session.PlanId != plan.PlanId || session.Operations.Count != 1 ||
            !operation.HasSameLogicalContentAs(session.Operations[0]))
        {
            return Invalid("SESSION_PLAN_MISMATCH", "The session does not contain the supplied operation.");
        }

        if (!session.Snapshots.Any(snapshot =>
            snapshot.SessionId == session.SessionId && snapshot.OperationId == operation.OperationId &&
            snapshot.StateKind == "ControlledProcessState" &&
            snapshot.ExistenceSemantics == ExistenceSemantics.Existed &&
            !string.IsNullOrWhiteSpace(snapshot.OriginalState) && snapshot.CapturedAt <= session.UpdatedAt))
        {
            return Invalid("VERIFY_SNAPSHOT_MISSING", "Verify requires the corresponding pre-apply snapshot.");
        }

        if (!session.Results.Any(result => result.Status is ResultStatus.Success &&
            result.Value == $"APPLY_DISPATCHED:{operation.OperationId:D}"))
        {
            return Invalid("VERIFY_APPLY_RESULT_MISSING", "Verify requires a successful dispatch for this operation.");
        }

        return null;
    }

    private static Error Invalid(string code, string details) => new(code, ErrorCategory.InvalidPlan, details);
}
