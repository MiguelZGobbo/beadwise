using BeadWise.Core.Operations;
using BeadWise.Core.Results;

namespace BeadWise.Core.Contracts;

public interface IPrivilegeBoundary
{
    Result<bool> Authorize(IReadOnlyList<PrivilegeRequirement> requirements);
}
