using BeadWise.Core.Contracts;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;

namespace BeadWise.Application.Processes;

public sealed class ObserveProcessesUseCase
{
    private readonly IProcessObservationSource _source;

    public ObserveProcessesUseCase(IProcessObservationSource source)
    {
        ArgumentNullException.ThrowIfNull(source);
        _source = source;
    }

    public Result<IReadOnlyList<ProcessObservation>> Execute() =>
        _source.Observe();
}
