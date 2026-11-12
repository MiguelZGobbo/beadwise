using BeadWise.Core.Processes;
using BeadWise.Core.Results;

namespace BeadWise.Core.Diagnostics;

public sealed record DiagnosticInventoryReport(
    Guid RunId, DateTimeOffset StartedAt, DateTimeOffset CompletedAt,
    Result<SystemInventory> System,
    Result<IReadOnlyList<ProcessObservation>> Processes,
    Result<IReadOnlyList<AudioEndpointObservation>> Audio,
    Result<IReadOnlyList<DisplayObservation>> Displays,
    Result<IReadOnlyList<DriverObservation>> Drivers);
