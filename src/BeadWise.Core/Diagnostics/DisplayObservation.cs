using BeadWise.Core.Observations;

namespace BeadWise.Core.Diagnostics;

public sealed record DisplayMode(int Width, int Height, uint Frequency, uint BitsPerPixel);

public enum DisplayObservationKind { Monitor, Adapter }

public sealed record DisplayObservation(
    Observation<string> Identity,
    Observation<string> AdapterName,
    Observation<string> Name,
    Observation<DisplayMode> CurrentMode,
    Observation<IReadOnlyList<DisplayMode>> SupportedModes,
    DisplayObservationKind Kind = DisplayObservationKind.Monitor);
