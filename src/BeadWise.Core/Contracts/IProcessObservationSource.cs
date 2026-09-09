using BeadWise.Core.Processes;
using BeadWise.Core.Results;

namespace BeadWise.Core.Contracts;

public interface IProcessObservationSource
{
    Result<IReadOnlyList<ProcessObservation>> Observe();
}
