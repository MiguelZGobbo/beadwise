using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using BeadWise.Core.Contracts;
using BeadWise.Core.Diagnostics;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Results;

namespace BeadWise.Windows.Diagnostics;

public sealed class WindowsAudioInventorySource : IAudioInventorySource
{
    public const string ObservationSource = "Windows.CoreAudio.MMDevice";
    private readonly Func<AudioNativeSnapshot> _read;

    public WindowsAudioInventorySource() : this(AudioNativeReader.Read) { }
    internal WindowsAudioInventorySource(Func<AudioNativeSnapshot> read) =>
        _read = read ?? throw new ArgumentNullException(nameof(read));

    public Result<IReadOnlyList<AudioEndpointObservation>> Observe()
    {
        AudioNativeSnapshot snapshot;
        try { snapshot = _read(); }
        catch (Exception exception) when (AudioNativeReader.IsExpectedFailure(exception))
        {
            var error = AudioNativeReader.ErrorFromException("AUDIO_ENUMERATION", exception);
            return error.Category == ErrorCategory.Unsupported
                ? Result<IReadOnlyList<AudioEndpointObservation>>.Unsupported(error)
                : Result<IReadOnlyList<AudioEndpointObservation>>.Failed(error);
        }

        var timestamp = DateTimeOffset.UtcNow;
        var missingDefaultDirections = snapshot.Defaults
            .Where(item => item.Identity.Status == ResultStatus.Success && !string.IsNullOrEmpty(item.Identity.Value)
                && !snapshot.Endpoints.Any(endpoint => endpoint.Direction == item.Direction
                    && endpoint.Identity.Status == ResultStatus.Success
                    && StringComparer.Ordinal.Equals(endpoint.Identity.Value, item.Identity.Value)))
            .Select(item => item.Direction).ToHashSet();
        var missingDefaultError = new Error("AUDIO_DEFAULT_NOT_ENUMERATED", ErrorCategory.Unavailable,
            "A default endpoint was not present in the enumeration for its direction.", recoverable: true);
        var errors = snapshot.Errors.Concat(missingDefaultDirections.Count == 0 ? [] : [missingDefaultError]).ToArray();
        var partial = errors.Length != 0;
        var endpoints = new List<AudioEndpointObservation>(snapshot.Endpoints.Count);
        foreach (var endpoint in snapshot.Endpoints)
        {
            var identity = ReadString(endpoint.Identity, timestamp, "AUDIO_IDENTITY", hash: true);
            var name = ReadString(endpoint.Name, timestamp, "AUDIO_NAME", hash: false);
            var state = ReadState(endpoint.State, timestamp);
            var roles = ReadRoles(endpoint, snapshot.Defaults,
                missingDefaultDirections.Contains(endpoint.Direction) ? missingDefaultError : null, timestamp);
            partial |= identity.Availability != ObservationAvailability.Available
                || name.Availability != ObservationAvailability.Available
                || state.Availability != ObservationAvailability.Available
                || roles.Availability != ObservationAvailability.Available;
            endpoints.Add(new AudioEndpointObservation(identity, name, endpoint.Direction, state, roles));
        }

        IReadOnlyList<AudioEndpointObservation> value = endpoints.AsReadOnly();
        return partial
            ? Result<IReadOnlyList<AudioEndpointObservation>>.Partial(value,
                errors.FirstOrDefault() ?? new Error("AUDIO_INVENTORY_PARTIAL", ErrorCategory.PartialFailure,
                    "One or more audio fields or enumerations were unavailable.", recoverable: true),
                errors.Select(error => error.Code).Distinct(StringComparer.Ordinal))
            : Result<IReadOnlyList<AudioEndpointObservation>>.Success(value);
    }

    private static Observation<string> ReadString(Result<string> result, DateTimeOffset timestamp, string code, bool hash)
    {
        if (result.Status == ResultStatus.Success && !string.IsNullOrWhiteSpace(result.Value))
        {
            var value = hash ? HashIdentity(result.Value) : result.Value;
            return Observation<string>.Available(value, ObservationSource, timestamp);
        }
        return Observation<string>.PartialWithoutValue(ObservationSource, timestamp,
            result.Error ?? new Error($"{code}_UNAVAILABLE", ErrorCategory.Unavailable, recoverable: true));
    }

    private static Observation<AudioEndpointState> ReadState(Result<uint> result, DateTimeOffset timestamp)
    {
        const uint known = 0x0f;
        if (result.Status != ResultStatus.Success)
            return Observation<AudioEndpointState>.PartialWithoutValue(ObservationSource, timestamp, result.Error);
        var state = result.Value;
        if (state == 0 || (state & ~known) != 0)
            return Observation<AudioEndpointState>.PartialWithoutValue(ObservationSource, timestamp,
                new Error("AUDIO_STATE_UNAVAILABLE", ErrorCategory.Unavailable, recoverable: true));
        return Observation<AudioEndpointState>.Available((AudioEndpointState)state, ObservationSource, timestamp);
    }

    private static Observation<IReadOnlyList<AudioRole>> ReadRoles(
        AudioNativeEndpoint endpoint, IReadOnlyList<AudioNativeDefault> defaults, Error? missingDefaultError,
        DateTimeOffset timestamp)
    {
        if (endpoint.Identity.Status != ResultStatus.Success || string.IsNullOrWhiteSpace(endpoint.Identity.Value))
            return Observation<IReadOnlyList<AudioRole>>.PartialWithoutValue(ObservationSource, timestamp,
                endpoint.Identity.Error ?? new Error("AUDIO_IDENTITY_UNAVAILABLE", ErrorCategory.Unavailable, recoverable: true));

        var found = new List<AudioRole>();
        Error? error = null;
        foreach (var role in Enum.GetValues<AudioRole>())
        {
            var matches = defaults.Where(item => item.Direction == endpoint.Direction && item.Role == role).ToArray();
            if (matches.Length != 1 || matches[0].Identity.Status != ResultStatus.Success
                || matches[0].Identity.Value is null)
            {
                error ??= matches.FirstOrDefault()?.Identity.Error
                    ?? new Error("AUDIO_DEFAULT_UNAVAILABLE", ErrorCategory.Unavailable, recoverable: true);
            }
            else if (StringComparer.Ordinal.Equals(endpoint.Identity.Value, matches[0].Identity.Value))
            {
                found.Add(role);
            }
        }
        error ??= missingDefaultError;
        IReadOnlyList<AudioRole> value = found.AsReadOnly();
        return error is null
            ? Observation<IReadOnlyList<AudioRole>>.Available(value, ObservationSource, timestamp)
            : Observation<IReadOnlyList<AudioRole>>.Partial(value, ObservationSource, timestamp, error);
    }

    internal static string HashIdentity(string identity) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(identity)));
}

internal sealed record AudioNativeEndpoint(AudioDirection Direction, Result<string> Identity, Result<string> Name, Result<uint> State);
internal sealed record AudioNativeDefault(AudioDirection Direction, AudioRole Role, Result<string> Identity);
internal sealed record AudioNativeSnapshot(IReadOnlyList<AudioNativeEndpoint> Endpoints,
    IReadOnlyList<AudioNativeDefault> Defaults, IReadOnlyList<Error> Errors);
