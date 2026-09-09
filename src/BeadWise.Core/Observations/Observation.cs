using BeadWise.Core.Errors;

namespace BeadWise.Core.Observations;

public sealed class Observation<T>
{
    private readonly T? _value;

    private Observation(
        T? value,
        bool hasValue,
        string source,
        DateTimeOffset timestamp,
        ObservationAvailability availability,
        Error? error)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(source);

        _value = value;
        HasValue = hasValue;
        Source = source;
        Timestamp = timestamp;
        Availability = availability;
        Error = error;
    }

    public bool HasValue { get; }

    public T Value => HasValue
        ? _value!
        : throw new InvalidOperationException("This observation does not contain a value.");

    public string Source { get; }

    public DateTimeOffset Timestamp { get; }

    public ObservationAvailability Availability { get; }

    public Error? Error { get; }

    public static Observation<T> Available(T value, string source, DateTimeOffset timestamp)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new Observation<T>(value, true, source, timestamp, ObservationAvailability.Available, null);
    }

    public static Observation<T> Partial(
        T? value,
        string source,
        DateTimeOffset timestamp,
        Error? error = null)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new Observation<T>(value, true, source, timestamp, ObservationAvailability.Partial, error);
    }

    public static Observation<T> PartialWithoutValue(
        string source,
        DateTimeOffset timestamp,
        Error? error = null) =>
        new(default, false, source, timestamp, ObservationAvailability.Partial, error);

    public static Observation<T> Unsupported(
        string source,
        DateTimeOffset timestamp,
        Error? error = null) =>
        new(default, false, source, timestamp, ObservationAvailability.Unsupported, error);

    public static Observation<T> Unknown(string source, DateTimeOffset timestamp) =>
        new(default, false, source, timestamp, ObservationAvailability.Unknown, null);

    public static Observation<T> Failure(
        string source,
        DateTimeOffset timestamp,
        Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new Observation<T>(default, false, source, timestamp, ObservationAvailability.Error, error);
    }
}
