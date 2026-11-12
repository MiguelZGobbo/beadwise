using BeadWise.Core.Diagnostics;
using BeadWise.Core.Results;

namespace BeadWise.Core.Contracts;

public interface IDriverInventorySource
{
    Result<IReadOnlyList<DriverObservation>> Observe();
}
