using BeadWise.Core.Errors;
using BeadWise.Core.Observations;

namespace BeadWise.Core.Tests.Observations;

public sealed class ObservationTests
{
    private static readonly DateTimeOffset Timestamp = new(2026, 9, 9, 12, 0, 0, TimeSpan.Zero);
    private static readonly Error AccessDenied = new(
        "ACCESS_DENIED",
        ErrorCategory.AccessDenied,
        "The source could not read every field.",
        recoverable: true);

    [Fact]
    public void AvailableCarriesValueAndProvenance()
    {
        var observation = Observation<string>.Available("running", "process-api", Timestamp);

        Assert.Equal(ObservationAvailability.Available, observation.Availability);
        Assert.True(observation.HasValue);
        Assert.Equal("running", observation.Value);
        Assert.Equal("process-api", observation.Source);
        Assert.Equal(Timestamp, observation.Timestamp);
        Assert.Null(observation.Error);
    }

    [Fact]
    public void PartialRemainsPartial()
    {
        var observation = Observation<string>.Partial("partial", "process-api", Timestamp, AccessDenied);

        Assert.Equal(ObservationAvailability.Partial, observation.Availability);
        Assert.Equal("partial", observation.Value);
        Assert.Equal(AccessDenied, observation.Error);
    }

    [Fact]
    public void UnsupportedRemainsUnsupported()
    {
        var observation = Observation<string>.Unsupported("process-api", Timestamp);

        Assert.Equal(ObservationAvailability.Unsupported, observation.Availability);
        Assert.False(observation.HasValue);
        Assert.Throws<InvalidOperationException>(() => { _ = observation.Value; });
    }

    [Fact]
    public void UnknownRemainsUnknown()
    {
        var observation = Observation<string>.Unknown("process-api", Timestamp);

        Assert.Equal(ObservationAvailability.Unknown, observation.Availability);
        Assert.False(observation.HasValue);
        Assert.Throws<InvalidOperationException>(() => { _ = observation.Value; });
    }

    [Fact]
    public void ErrorCarriesStructuredError()
    {
        var observation = Observation<string>.Failure("process-api", Timestamp, AccessDenied);

        Assert.Equal(ObservationAvailability.Error, observation.Availability);
        Assert.Equal(AccessDenied, observation.Error);
        Assert.False(observation.HasValue);
        Assert.Throws<InvalidOperationException>(() => { _ = observation.Value; });
    }

    [Theory]
    [InlineData(ObservationAvailability.Partial)]
    [InlineData(ObservationAvailability.Unsupported)]
    [InlineData(ObservationAvailability.Unknown)]
    [InlineData(ObservationAvailability.Error)]
    public void UnavailableValueTypeDoesNotExposeDefaultValue(ObservationAvailability availability)
    {
        var observation = availability switch
        {
            ObservationAvailability.Partial => Observation<int>.PartialWithoutValue(
                "process-api",
                Timestamp,
                AccessDenied),
            ObservationAvailability.Unsupported => Observation<int>.Unsupported("process-api", Timestamp),
            ObservationAvailability.Unknown => Observation<int>.Unknown("process-api", Timestamp),
            ObservationAvailability.Error => Observation<int>.Failure("process-api", Timestamp, AccessDenied),
            _ => throw new ArgumentOutOfRangeException(nameof(availability))
        };

        Assert.False(observation.HasValue);
        Assert.Throws<InvalidOperationException>(() => { _ = observation.Value; });
    }
}
