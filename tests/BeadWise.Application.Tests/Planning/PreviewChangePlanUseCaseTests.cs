using BeadWise.Application.Planning;
using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Planning;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;

namespace BeadWise.Application.Tests.Planning;

public sealed class PreviewChangePlanUseCaseTests
{
    private static readonly ProcessIdentity TargetIdentity = new(
        42,
        new DateTimeOffset(2026, 9, 14, 15, 30, 0, TimeSpan.Zero));

    [Fact]
    public void ReturnsTheExistingPlanWithoutRecreatingOrChangingIt()
    {
        var plan = CreateValidPlan();
        var originalPlanId = plan.PlanId;
        var originalOperation = Assert.Single(plan.Operations);
        var originalPreconditions = plan.Preconditions.ToArray();
        var originalWarnings = plan.Warnings.ToArray();

        var result = new PreviewChangePlanUseCase().Execute(plan);

        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.Same(plan, result.Value);
        Assert.Equal(originalPlanId, result.Value?.PlanId);
        Assert.Same(originalOperation, Assert.Single(result.Value!.Operations));
        Assert.Equal(originalPreconditions, result.Value.Preconditions);
        Assert.Equal(originalWarnings, result.Value.Warnings);
        Assert.Equal(plan.PrivilegeRequirements, result.Value.PrivilegeRequirements);
        Assert.Equal(plan.RestartRequirements, result.Value.RestartRequirements);
    }

    [Fact]
    public void DoesNotChangeExternalStateDuringPreview()
    {
        var plan = CreateValidPlan();
        var stateBefore = new[] { "fixture-alpha:running" };

        _ = new PreviewChangePlanUseCase().Execute(plan);

        Assert.Equal(["fixture-alpha:running"], stateBefore);
    }

    [Fact]
    public void ReturnsStructuredFailureForNullPlan()
    {
        var result = new PreviewChangePlanUseCase().Execute(null);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Null(result.Value);
        Assert.Equal("INVALID_CHANGE_PLAN", result.Error?.Code);
        Assert.Equal(ErrorCategory.InvalidPlan, result.Error?.Category);
    }

    [Fact]
    public void ReturnsStructuredFailureForPlanWithoutOperations()
    {
        var plan = new ChangePlan(
            Guid.NewGuid(),
            ["C-PROCESSES-007"],
            [],
            ["Target is controlled"],
            Risk.Medium,
            [],
            [PrivilegeRequirement.None],
            [RestartRequirement.None]);

        var result = new PreviewChangePlanUseCase().Execute(plan);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal(ErrorCategory.InvalidPlan, result.Error?.Category);
    }

    [Fact]
    public void ReturnsStructuredFailureForPlanWithoutExecutionResources()
    {
        var operation = new Operation(
            Guid.NewGuid(),
            "Stop controlled process fixture",
            "Running",
            "NotRunning",
            Risk.Medium,
            PrivilegeRequirement.None,
            RestartRequirement.None,
            Reversibility.Recoverable,
            []);
        var plan = CreatePlan([operation]);

        var result = new PreviewChangePlanUseCase().Execute(plan);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal(ErrorCategory.InvalidPlan, result.Error?.Category);
    }

    [Fact]
    public void ReturnsStructuredFailureWhenPlanRequirementsDoNotCoverOperation()
    {
        var operation = new Operation(
            Guid.NewGuid(),
            "Stop controlled process fixture",
            "Running",
            "NotRunning",
            Risk.Medium,
            PrivilegeRequirement.Administrator,
            RestartRequirement.Process,
            Reversibility.Recoverable,
            [new ExecutionResource("Process", "fixture-alpha")]);
        var plan = CreatePlan([operation]);

        var result = new PreviewChangePlanUseCase().Execute(plan);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal(ErrorCategory.InvalidPlan, result.Error?.Category);
    }

    private static ChangePlan CreateValidPlan()
    {
        var result = new CreateControlledProcessChangePlanUseCase()
            .Execute(TargetIdentity, "fixture-alpha");

        return Assert.IsType<ChangePlan>(result.Value);
    }

    private static ChangePlan CreatePlan(IEnumerable<Operation> operations) =>
        new(
            Guid.NewGuid(),
            ["C-PROCESSES-007"],
            operations,
            ["Target is controlled"],
            Risk.Medium,
            [],
            [PrivilegeRequirement.None],
            [RestartRequirement.None]);
}
