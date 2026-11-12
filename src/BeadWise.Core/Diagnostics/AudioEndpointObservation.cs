using BeadWise.Core.Observations;

namespace BeadWise.Core.Diagnostics;

public enum AudioDirection { Render, Capture }
public enum AudioRole { Console, Multimedia, Communications }
[Flags]
public enum AudioEndpointState { Active = 1, Disabled = 2, NotPresent = 4, Unplugged = 8 }

public sealed record AudioEndpointObservation(
    Observation<string> Identity,
    Observation<string> Name,
    AudioDirection Direction,
    Observation<AudioEndpointState> State,
    Observation<IReadOnlyList<AudioRole>> DefaultRoles);
