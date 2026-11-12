using BeadWise.Application.Execution;
using BeadWise.Core.Operations;
using BeadWise.Core.Results;

namespace BeadWise.Application.Tests.Execution;

public sealed class UnprivilegedBoundaryTests
{
    [Theory]
    [InlineData(PrivilegeRequirement.Administrator)]
    [InlineData((PrivilegeRequirement)999)]
    public void NeverAuthorizesAdministrativeOrUnknownRequirements(PrivilegeRequirement requirement)
    {
        var result = new UnprivilegedBoundary().Authorize([PrivilegeRequirement.None, requirement]);
        Assert.Equal(ResultStatus.Blocked, result.Status);
        Assert.Equal("ADMIN_REQUIRED", result.Error?.Code);
    }

    [Fact]
    public void AllowsOnlyOrdinaryOperations() =>
        Assert.True(new UnprivilegedBoundary().Authorize([PrivilegeRequirement.None]).Value);
}
