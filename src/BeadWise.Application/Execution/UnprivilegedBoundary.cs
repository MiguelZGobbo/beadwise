using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Results;

namespace BeadWise.Application.Execution;

/// <summary>The phase-12 host never dispatches privileged operations, even from an elevated shell.</summary>
public sealed class UnprivilegedBoundary : IPrivilegeBoundary
{
    public Result<bool> Authorize(IReadOnlyList<PrivilegeRequirement> requirements)
    {
        ArgumentNullException.ThrowIfNull(requirements);
        return requirements.All(requirement => requirement is PrivilegeRequirement.None)
            ? Result<bool>.Success(true)
            : Result<bool>.Blocked(new Error("ADMIN_REQUIRED", ErrorCategory.AdminRequired));
    }
}
