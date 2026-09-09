using BeadWise.Core.Operations;
using BeadWise.Core.Planning;

namespace BeadWise.Core.Tests.Planning;

public sealed class ChangePlanTests
{
    [Fact]
    public void PreservesIdentityOperationsAndPlanMetadata()
    {
        var planId = Guid.Parse("1ee3b188-2082-4893-96b8-a3603995d96b");
        var operation = CreateOperation();

        var plan = new ChangePlan(
            planId,
            ["C-PROCESSES-001", "C-PROCESSES-007"],
            [operation],
            ["Target identity is current"],
            Risk.High,
            ["The original process memory cannot be restored"],
            [PrivilegeRequirement.Administrator],
            [RestartRequirement.Process],
            "Recreate only the controlled fixture.");

        Assert.Equal(planId, plan.PlanId);
        Assert.Equal(["C-PROCESSES-001", "C-PROCESSES-007"], plan.FeatureIds);
        Assert.Equal([operation], plan.Operations);
        Assert.Equal(["Target identity is current"], plan.Preconditions);
        Assert.Equal(Risk.High, plan.Risk);
        Assert.Equal(["The original process memory cannot be restored"], plan.Warnings);
        Assert.Equal([PrivilegeRequirement.Administrator], plan.PrivilegeRequirements);
        Assert.Equal([RestartRequirement.Process], plan.RestartRequirements);
        Assert.Equal("Recreate only the controlled fixture.", plan.RecoveryInformation);
    }

    [Fact]
    public void CopiesCollectionsAtConstruction()
    {
        var featureIds = new List<string> { "C-PROCESSES-001" };
        var operations = new List<Operation> { CreateOperation() };
        var preconditions = new List<string> { "Target identity is current" };
        var warnings = new List<string> { "Controlled fixture only" };
        var privilegeRequirements = new List<PrivilegeRequirement> { PrivilegeRequirement.None };
        var restartRequirements = new List<RestartRequirement> { RestartRequirement.Process };
        var plan = new ChangePlan(
            Guid.Parse("1ee3b188-2082-4893-96b8-a3603995d96b"),
            featureIds,
            operations,
            preconditions,
            Risk.Medium,
            warnings,
            privilegeRequirements,
            restartRequirements);

        featureIds.Add("C-PROCESSES-007");
        operations.Clear();
        preconditions.Clear();
        warnings.Clear();
        privilegeRequirements.Clear();
        restartRequirements.Clear();

        Assert.Equal(["C-PROCESSES-001"], plan.FeatureIds);
        Assert.Single(plan.Operations);
        Assert.Equal(["Target identity is current"], plan.Preconditions);
        Assert.Equal(["Controlled fixture only"], plan.Warnings);
        Assert.Equal([PrivilegeRequirement.None], plan.PrivilegeRequirements);
        Assert.Equal([RestartRequirement.Process], plan.RestartRequirements);
    }

    [Fact]
    public void RejectsEmptyPlanIdentity()
    {
        Assert.Throws<ArgumentException>(
            () => new ChangePlan(
                Guid.Empty,
                ["C-PROCESSES-001"],
                [CreateOperation()],
                [],
                Risk.Medium,
                [],
                [],
                []));
    }

    private static Operation CreateOperation() =>
        new(
            Guid.Parse("cb984403-4faf-4182-876b-710404f0b6bf"),
            "Restart controlled process",
            "Running",
            "Replaced",
            Risk.Medium,
            PrivilegeRequirement.None,
            RestartRequirement.Process,
            Reversibility.Recoverable,
            [new ExecutionResource("Process", "42:2026-09-09T12:00:00Z")]);
}
