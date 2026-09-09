using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Processes;

namespace BeadWise.Core.Tests.Processes;

public sealed class ProcessObservationTests
{
    private static readonly DateTimeOffset Timestamp = new(2026, 9, 9, 12, 0, 0, TimeSpan.Zero);
    private static readonly Error Unavailable = new(
        "PROCESS_CREATION_TIME_UNAVAILABLE",
        ErrorCategory.Unavailable,
        recoverable: true);

    [Fact]
    public void ProcessWithoutCreationTimePreservesPidWithoutFabricatingIdentity()
    {
        var identity = Observation<ProcessIdentity>.PartialWithoutValue(
            "System.Diagnostics.Process",
            Timestamp,
            Unavailable);
        var name = Observation<string>.Available("testhost", "System.Diagnostics.Process", Timestamp);
        var path = Observation<string>.PartialWithoutValue(
            "System.Diagnostics.Process",
            Timestamp,
            Unavailable);

        var process = new ProcessObservation(42, identity, name, path);

        Assert.Equal(42, process.ProcessId);
        Assert.False(process.Identity.HasValue);
        Assert.Equal(ObservationAvailability.Partial, process.Identity.Availability);
        Assert.Equal("testhost", process.ProcessName.Value);
        Assert.False(process.ExecutablePath.HasValue);
    }

    [Fact]
    public void IdentityForDifferentPidIsRejected()
    {
        var identity = Observation<ProcessIdentity>.Available(
            new ProcessIdentity(43, Timestamp),
            "System.Diagnostics.Process",
            Timestamp);
        var name = Observation<string>.Available("testhost", "System.Diagnostics.Process", Timestamp);
        var path = Observation<string>.Available("C:\\testhost.exe", "System.Diagnostics.Process", Timestamp);

        var exception = Assert.Throws<ArgumentException>(
            () => new ProcessObservation(42, identity, name, path));

        Assert.Equal("identity", exception.ParamName);
    }
}
