using BeadWise.Core.Diagnostics;
using BeadWise.Core.Results;

namespace BeadWise.Core.Contracts;

public interface IDisplayInventorySource
{
    Result<IReadOnlyList<DisplayObservation>> Observe();
}
