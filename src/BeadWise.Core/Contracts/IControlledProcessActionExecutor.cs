using BeadWise.Core.Operations;
using BeadWise.Core.Planning;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;

namespace BeadWise.Core.Contracts;

public interface IControlledProcessActionExecutor
{
    Result<IControlledProcessActionPreparation> Prepare(
        ChangePlan plan,
        Session approvedSession);

    Result<Operation> Execute(
        ChangePlan plan,
        Session applyingSession,
        IControlledProcessActionPreparation preparation);
}
