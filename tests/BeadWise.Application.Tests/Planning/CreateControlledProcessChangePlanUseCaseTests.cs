using BeadWise.Application.Planning;
using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;

namespace BeadWise.Application.Tests.Planning;

public sealed class CreateControlledProcessChangePlanUseCaseTests
{
    private static readonly ProcessIdentity TargetIdentity = new(
        42,
        new DateTimeOffset(2026, 9, 14, 12, 30, 0, TimeSpan.FromHours(-3)));

    [Fact]
    public void CreatesControlledProcessPlanWithRequiredMetadata()
    {
        var useCase = new CreateControlledProcessChangePlanUseCase();

        var result = useCase.Execute(TargetIdentity, "fixture-alpha");

        Assert.Equal(ResultStatus.Success, result.Status);
        var plan = Assert.IsType<Core.Planning.ChangePlan>(result.Value);
        Assert.NotEqual(Guid.Empty, plan.PlanId);
        Assert.Equal(["C-PROCESSES-001", "C-PROCESSES-007"], plan.FeatureIds);
        Assert.Equal(Risk.Medium, plan.Risk);
        Assert.Equal([PrivilegeRequirement.None], plan.PrivilegeRequirements);
        Assert.Equal([RestartRequirement.None], plan.RestartRequirements);
        Assert.NotEmpty(plan.Preconditions);
        Assert.NotEmpty(plan.Warnings);
        Assert.NotNull(plan.RecoveryInformation);

        var operation = Assert.Single(plan.Operations);
        Assert.NotEqual(Guid.Empty, operation.OperationId);
        Assert.Equal("Stop controlled process fixture", operation.Description);
        Assert.Equal("Running", operation.CurrentState);
        Assert.Equal("NotRunning", operation.TargetState);
        Assert.Equal(Risk.Medium, operation.Risk);
        Assert.Equal(PrivilegeRequirement.None, operation.PrivilegeRequirement);
        Assert.Equal(RestartRequirement.None, operation.RestartRequirement);
        Assert.Equal(Reversibility.Recoverable, operation.Reversibility);
        Assert.NotEqual(Reversibility.Reversible, operation.Reversibility);

        var resource = Assert.Single(operation.ExecutionResources);
        Assert.Equal("Process", resource.ResourceType);
        Assert.Equal(
            "fixture-alpha|pid=42|created=2026-09-14T15:30:00.0000000+00:00",
            resource.ResourceKey);
    }

    [Fact]
    public void ExposesReadOnlyPlanCollections()
    {
        var result = new CreateControlledProcessChangePlanUseCase()
            .Execute(TargetIdentity, "fixture-alpha");
        var plan = Assert.IsType<Core.Planning.ChangePlan>(result.Value);

        Assert.Throws<NotSupportedException>(
            () => ((IList<string>)plan.FeatureIds).Add("C-PROCESSES-999"));
        Assert.Throws<NotSupportedException>(
            () => ((IList<Operation>)plan.Operations).Clear());
        Assert.Throws<NotSupportedException>(
            () => ((IList<string>)plan.Preconditions).Clear());
        Assert.Throws<NotSupportedException>(
            () => ((IList<string>)plan.Warnings).Clear());
    }

    [Theory]
    [InlineData(0, "fixture-alpha")]
    [InlineData(-1, "fixture-alpha")]
    [InlineData(42, null)]
    [InlineData(42, "")]
    [InlineData(42, "   ")]
    public void ReturnsStructuredFailureForInvalidTarget(int processId, string? fixtureKey)
    {
        var identity = new ProcessIdentity(processId, TargetIdentity.CreationTime);

        var result = new CreateControlledProcessChangePlanUseCase()
            .Execute(identity, fixtureKey);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Null(result.Value);
        Assert.Equal(ErrorCategory.InvalidPlan, result.Error?.Category);
    }

    [Fact]
    public void ReturnsStructuredFailureWhenCreationTimeIsUnavailable()
    {
        var identity = new ProcessIdentity(42, default);

        var result = new CreateControlledProcessChangePlanUseCase()
            .Execute(identity, "fixture-alpha");

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Null(result.Value);
        Assert.Equal("INVALID_CONTROLLED_PROCESS_TARGET", result.Error?.Code);
        Assert.Equal(ErrorCategory.InvalidPlan, result.Error?.Category);
    }
}
