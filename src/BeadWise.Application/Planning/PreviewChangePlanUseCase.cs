using BeadWise.Core.Errors;
using BeadWise.Core.Planning;
using BeadWise.Core.Results;

namespace BeadWise.Application.Planning;

public sealed class PreviewChangePlanUseCase
{
    private const string InvalidPlanErrorCode = "INVALID_CHANGE_PLAN";

    public Result<ChangePlan> Execute(ChangePlan? plan)
    {
        if (plan is null || !IsStructurallyExecutable(plan))
        {
            return Result<ChangePlan>.Failed(
                new Error(
                    InvalidPlanErrorCode,
                    ErrorCategory.InvalidPlan,
                    "The change plan is incomplete or internally inconsistent."));
        }

        return Result<ChangePlan>.Success(plan);
    }

    private static bool IsStructurallyExecutable(ChangePlan plan)
    {
        if (plan.FeatureIds.Count == 0 ||
            plan.FeatureIds.Any(string.IsNullOrWhiteSpace) ||
            plan.Operations.Count == 0 ||
            plan.Preconditions.Count == 0 ||
            plan.Preconditions.Any(string.IsNullOrWhiteSpace))
        {
            return false;
        }

        foreach (var operation in plan.Operations)
        {
            if (operation.ExecutionResources.Count == 0 ||
                !plan.PrivilegeRequirements.Contains(operation.PrivilegeRequirement) ||
                !plan.RestartRequirements.Contains(operation.RestartRequirement))
            {
                return false;
            }
        }

        return true;
    }
}
