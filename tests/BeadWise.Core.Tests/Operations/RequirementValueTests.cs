using BeadWise.Core.Operations;

namespace BeadWise.Core.Tests.Operations;

public sealed class RequirementValueTests
{
    [Fact]
    public void RestartRequirementsAreDistinct()
    {
        var expected = new[]
        {
            RestartRequirement.None,
            RestartRequirement.App,
            RestartRequirement.Process,
            RestartRequirement.Service,
            RestartRequirement.Device,
            RestartRequirement.Logoff,
            RestartRequirement.Reboot
        };

        Assert.Equal(expected, Enum.GetValues<RestartRequirement>());
        Assert.Equal(expected.Length, expected.Distinct().Count());
    }

    [Fact]
    public void ReversibilityKindsAreDistinct()
    {
        var expected = new[]
        {
            Reversibility.Reversible,
            Reversibility.Recoverable,
            Reversibility.Irreversible,
            Reversibility.NotApplicable
        };

        Assert.Equal(expected, Enum.GetValues<Reversibility>());
        Assert.Equal(expected.Length, expected.Distinct().Count());
    }

    [Fact]
    public void ExecutionResourceUsesTypeAndKeyForEquality()
    {
        var resource = new ExecutionResource("Process", "42:2026-09-09T12:00:00Z");

        Assert.Equal(resource, new ExecutionResource("Process", "42:2026-09-09T12:00:00Z"));
        Assert.NotEqual(resource, new ExecutionResource("Process", "43:2026-09-09T12:00:00Z"));
    }
}
