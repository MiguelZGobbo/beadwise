using BeadWise.Core.Observations;

namespace BeadWise.Core.Diagnostics;

public sealed record DriverObservation(
    Observation<string> Identity,
    Observation<string> Name,
    Observation<string> ClassName,
    Observation<string> Provider,
    Observation<string> Version,
    Observation<bool> Signed,
    Observation<uint> ProblemCode,
    Observation<uint> DeviceStatus);
