using BeadWise.Core.Diagnostics;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Results;
using BeadWise.Windows.Diagnostics;

namespace BeadWise.Windows.Tests.Diagnostics;

public sealed class WindowsAudioInventorySourceTests
{
    [Fact]
    public void FailedDefaultQueryKeepsKnownRolesButDoesNotClaimCompleteRoles()
    {
        var failure = new Error("AUDIO_DEFAULT_UNAVAILABLE", ErrorCategory.Unavailable, recoverable: true);
        var snapshot = new AudioNativeSnapshot(
            [new(AudioDirection.Render, Ok("endpoint-alpha"), Ok("Speakers"), Ok(1u))],
            [new(AudioDirection.Render, AudioRole.Console, Ok("endpoint-alpha")),
             new(AudioDirection.Render, AudioRole.Multimedia, Ok("other")),
             new(AudioDirection.Render, AudioRole.Communications, Result<string>.Failed(failure))],
            []);
        var source = new WindowsAudioInventorySource(() => snapshot);

        var result = source.Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        var endpoint = Assert.Single(result.Value!);
        Assert.Equal("Speakers", endpoint.Name.Value);
        Assert.Equal(AudioEndpointState.Active, endpoint.State.Value);
        Assert.Equal(ObservationAvailability.Partial, endpoint.DefaultRoles.Availability);
        Assert.Equal([AudioRole.Console], endpoint.DefaultRoles.Value);
        Assert.Equal(ErrorCategory.Unavailable, endpoint.DefaultRoles.Error?.Category);
        Assert.NotEqual("endpoint-alpha", endpoint.Identity.Value);
        Assert.Matches("^[0-9A-F]{64}$", endpoint.Identity.Value);
        Assert.Equal(endpoint.Identity.Value, Assert.Single(source.Observe().Value!).Identity.Value);
    }

    [Fact]
    public void FailedIdentityNameAndUnknownStateRemainExplicitlyUnavailable()
    {
        var denied = new Error("AUDIO_FIELD_ACCESS_DENIED", ErrorCategory.AccessDenied, recoverable: true);
        var snapshot = new AudioNativeSnapshot(
            [new(AudioDirection.Capture, Result<string>.Failed(denied), Result<string>.Failed(denied), Ok(0x20u))],
            [new(AudioDirection.Capture, AudioRole.Console, Ok("other")),
             new(AudioDirection.Capture, AudioRole.Multimedia, Ok("other")),
             new(AudioDirection.Capture, AudioRole.Communications, Ok("other"))],
            []);

        var result = new WindowsAudioInventorySource(() => snapshot).Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        var endpoint = Assert.Single(result.Value!);
        Assert.False(endpoint.Identity.HasValue);
        Assert.Equal(ErrorCategory.AccessDenied, endpoint.Identity.Error?.Category);
        Assert.False(endpoint.Name.HasValue);
        Assert.False(endpoint.State.HasValue);
        Assert.False(endpoint.DefaultRoles.HasValue);
    }

    [Fact]
    public void EnumerationFailurePreservesEndpointsFromOtherDirection()
    {
        var unavailable = new Error("AUDIO_ENUMERATION_UNAVAILABLE", ErrorCategory.Unavailable, recoverable: true);
        var snapshot = new AudioNativeSnapshot(
            [new(AudioDirection.Render, Ok("render"), Ok("Speaker"), Ok(8u))],
            [new(AudioDirection.Render, AudioRole.Console, Ok("other")),
             new(AudioDirection.Render, AudioRole.Multimedia, Ok("other")),
             new(AudioDirection.Render, AudioRole.Communications, Ok("other"))],
            [unavailable]);

        var result = new WindowsAudioInventorySource(() => snapshot).Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal(AudioEndpointState.Unplugged, Assert.Single(result.Value!).State.Value);
        Assert.Equal(ErrorCategory.Unavailable, result.Error?.Category);
        Assert.Equal("AUDIO_ENUMERATION_UNAVAILABLE", result.Error?.Code);
    }

    [Fact]
    public void ReaderAccessFailureIsAStructuredFailure()
    {
        var source = new WindowsAudioInventorySource(() => throw new UnauthorizedAccessException());
        var result = source.Observe();
        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal(ErrorCategory.AccessDenied, result.Error?.Category);
    }

    [Fact]
    public void NoDefaultEndpointsAreKnownEmptyRolesForDisabledDevices()
    {
        var snapshot = new AudioNativeSnapshot(
            [new(AudioDirection.Render, Ok("disabled"), Ok("Speaker"), Ok(2u))],
            [new(AudioDirection.Render, AudioRole.Console, Ok("")),
             new(AudioDirection.Render, AudioRole.Multimedia, Ok("")),
             new(AudioDirection.Render, AudioRole.Communications, Ok(""))], []);

        var result = new WindowsAudioInventorySource(() => snapshot).Observe();

        Assert.Equal(ResultStatus.Success, result.Status);
        var endpoint = Assert.Single(result.Value!);
        Assert.Equal(AudioEndpointState.Disabled, endpoint.State.Value);
        Assert.Equal(ObservationAvailability.Available, endpoint.DefaultRoles.Availability);
        Assert.Empty(endpoint.DefaultRoles.Value);
    }
    [Fact]
    public void DefaultMissingFromItsDirectionMakesOnlyThatDirectionsRolesPartial()
    {
        var snapshot = new AudioNativeSnapshot(
            [new(AudioDirection.Render, Ok("render-1"), Ok("Speaker"), Ok(1u)),
             new(AudioDirection.Render, Ok("render-2"), Ok("Headphones"), Ok(1u)),
             new(AudioDirection.Capture, Ok("capture-1"), Ok("Microphone"), Ok(1u)),
             new(AudioDirection.Capture, Ok("missing-render"), Ok("Other microphone"), Ok(1u))],
            [new(AudioDirection.Render, AudioRole.Console, Ok("render-1")),
             new(AudioDirection.Render, AudioRole.Multimedia, Ok("missing-render")),
             new(AudioDirection.Render, AudioRole.Communications, Ok("")),
             new(AudioDirection.Capture, AudioRole.Console, Ok("capture-1")),
             new(AudioDirection.Capture, AudioRole.Multimedia, Ok("capture-1")),
             new(AudioDirection.Capture, AudioRole.Communications, Ok("capture-1"))], []);

        var result = new WindowsAudioInventorySource(() => snapshot).Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal("AUDIO_DEFAULT_NOT_ENUMERATED", result.Error?.Code);
        var speaker = Assert.Single(result.Value!, item => item.Name.Value == "Speaker");
        Assert.Equal(ObservationAvailability.Partial, speaker.DefaultRoles.Availability);
        Assert.Equal([AudioRole.Console], speaker.DefaultRoles.Value);
        var headphones = Assert.Single(result.Value!, item => item.Name.Value == "Headphones");
        Assert.Equal(ObservationAvailability.Partial, headphones.DefaultRoles.Availability);
        Assert.Empty(headphones.DefaultRoles.Value);
        var microphone = Assert.Single(result.Value!, item => item.Name.Value == "Microphone");
        Assert.Equal(ObservationAvailability.Available, microphone.DefaultRoles.Availability);
        Assert.Equal([AudioRole.Console, AudioRole.Multimedia, AudioRole.Communications], microphone.DefaultRoles.Value);
    }

    [Fact]
    public void DefaultMissingFromAnEmptyEnumerationMakesTheSectionPartial()
    {
        var snapshot = new AudioNativeSnapshot([], [new(AudioDirection.Render, AudioRole.Console, Ok("new-device"))], []);

        var result = new WindowsAudioInventorySource(() => snapshot).Observe();

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Empty(result.Value!);
        Assert.Equal("AUDIO_DEFAULT_NOT_ENUMERATED", result.Error?.Code);
    }

    private static Result<T> Ok<T>(T value) where T : notnull => Result<T>.Success(value);
}
