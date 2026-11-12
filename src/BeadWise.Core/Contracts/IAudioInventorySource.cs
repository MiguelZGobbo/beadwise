using BeadWise.Core.Diagnostics;
using BeadWise.Core.Results;

namespace BeadWise.Core.Contracts;

public interface IAudioInventorySource
{
    Result<IReadOnlyList<AudioEndpointObservation>> Observe();
}
