using BeadWise.Core.Operations;

namespace BeadWise.Core.Tests.Operations;

public sealed class OperationTests
{
    [Fact]
    public void PreservesExecutionMetadata()
    {
        var operationId = Guid.Parse("cb984403-4faf-4182-876b-710404f0b6bf");
        var resource = new ExecutionResource("Process", "42:2026-09-09T12:00:00Z");

        var operation = new Operation(
            operationId,
            "Restart controlled process",
            "Running",
            "Replaced",
            Risk.High,
            PrivilegeRequirement.Administrator,
            RestartRequirement.Process,
            Reversibility.Recoverable,
            [resource]);

        Assert.Equal(operationId, operation.OperationId);
        Assert.Equal("Restart controlled process", operation.Description);
        Assert.Equal("Running", operation.CurrentState);
        Assert.Equal("Replaced", operation.TargetState);
        Assert.Equal(Risk.High, operation.Risk);
        Assert.Equal(PrivilegeRequirement.Administrator, operation.PrivilegeRequirement);
        Assert.Equal(RestartRequirement.Process, operation.RestartRequirement);
        Assert.Equal(Reversibility.Recoverable, operation.Reversibility);
        Assert.Equal([resource], operation.ExecutionResources);
    }

    [Fact]
    public void CopiesExecutionResourcesAtConstruction()
    {
        var resources = new List<ExecutionResource>
        {
            new("Process", "42:2026-09-09T12:00:00Z")
        };
        var operation = CreateOperation(resources);

        resources.Add(new ExecutionResource("Process", "43:2026-09-09T12:00:00Z"));

        Assert.Single(operation.ExecutionResources);
    }

    [Fact]
    public void RejectsEmptyOperationIdentity()
    {
        Assert.Throws<ArgumentException>(
            () => new Operation(
                Guid.Empty,
                "Restart controlled process",
                "Running",
                "Replaced",
                Risk.Medium,
                PrivilegeRequirement.None,
                RestartRequirement.Process,
                Reversibility.Recoverable,
                []));
    }

    private static Operation CreateOperation(IEnumerable<ExecutionResource> resources) =>
        new(
            Guid.Parse("cb984403-4faf-4182-876b-710404f0b6bf"),
            "Restart controlled process",
            "Running",
            "Replaced",
            Risk.Medium,
            PrivilegeRequirement.None,
            RestartRequirement.Process,
            Reversibility.Recoverable,
            resources);
}
