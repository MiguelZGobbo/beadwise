using BeadWise.Core.Observations;
using BeadWise.Core.Planning;
using BeadWise.Core.Results;

namespace BeadWise.Core.Contracts;

public interface IControlledProcessStateSource
{
    Result<Observation<bool>> Observe(ChangePlan plan);
}
