using System.Diagnostics;
using BeadWise.Core.Observations;
using BeadWise.Windows.Processes;

namespace BeadWise.Windows.Tests.Fixtures;

public sealed class ControlledProcessFixtureTests
{
    [Fact]
    public async Task StartReturnsRunningOwnedProcessWithRealUtcIdentity()
    {
        await using var fixture = await ControlledProcessFixture.StartAsync();

        using var process = Process.GetProcessById(fixture.ProcessId);
        var actualCreationTime = new DateTimeOffset(process.StartTime.ToUniversalTime());

        Assert.True(fixture.IsRunning);
        Assert.NotEqual(Guid.Empty, fixture.FixtureKey);
        Assert.Equal(process.Id, fixture.ProcessId);
        Assert.Equal(process.Id, fixture.Identity.ProcessId);
        Assert.Equal(actualCreationTime, fixture.Identity.CreationTime);
        Assert.Equal(TimeSpan.Zero, fixture.Identity.CreationTime.Offset);
    }

    [Fact]
    public async Task StartWithoutReadyTimesOutAndCleansTheStartedProcess()
    {
        var processesBefore = GetFixtureProcessIds();
        var options = new ControlledProcessFixtureOptions
        {
            StartupTimeout = TimeSpan.FromMilliseconds(250),
            SuppressReadySignal = true
        };

        await Assert.ThrowsAsync<TimeoutException>(
            () => ControlledProcessFixture.StartAsync(options));

        Assert.Equal(processesBefore, GetFixtureProcessIds());
    }

    [Fact]
    public async Task StartupFailureRemainsPrimaryWhenCleanupAlsoFails()
    {
        var processesBefore = GetFixtureProcessIds();
        var options = new ControlledProcessFixtureOptions
        {
            StartupTimeout = TimeSpan.FromMilliseconds(250),
            MaximumLifetime = TimeSpan.FromSeconds(1),
            SuppressReadySignal = true,
            StartupFailureCleanup = () =>
                Task.FromException(new InvalidOperationException("cleanup failed"))
        };

        await Assert.ThrowsAsync<TimeoutException>(
            () => ControlledProcessFixture.StartAsync(options));

        await WaitForFixtureProcessesAsync(processesBefore);
    }

    [Fact]
    public void ReadySignalMustMatchTheExpectedCorrelationTokenAndProcessId()
    {
        var expectedKey = Guid.Parse("9c1cad0c-1d47-4b8f-96cf-5532d0dcf700");

        Assert.True(ControlledProcessFixture.MatchesReadySignal(
            "BEADWISE_FIXTURE_READY|9c1cad0c-1d47-4b8f-96cf-5532d0dcf700|4812",
            expectedKey,
            4812));
        Assert.False(ControlledProcessFixture.MatchesReadySignal(
            "BEADWISE_FIXTURE_READY|50c3ba24-24eb-4906-91ae-0ea68792eaff|4812",
            expectedKey,
            4812));
        Assert.False(ControlledProcessFixture.MatchesReadySignal(
            "BEADWISE_FIXTURE_READY|9c1cad0c-1d47-4b8f-96cf-5532d0dcf700|9001",
            expectedKey,
            4812));
        Assert.False(ControlledProcessFixture.MatchesReadySignal(
            null,
            expectedKey,
            4812));
    }

    [Fact]
    public async Task MaximumLifetimeEndsTheChildWithoutParentShutdown()
    {
        var options = new ControlledProcessFixtureOptions
        {
            MaximumLifetime = TimeSpan.FromMilliseconds(250)
        };
        await using var fixture = await ControlledProcessFixture.StartAsync(options);
        using var process = Process.GetProcessById(fixture.ProcessId);

        await process.WaitForExitAsync().WaitAsync(TimeSpan.FromSeconds(3));

        Assert.False(fixture.IsRunning);
    }

    [Fact]
    public async Task SubMillisecondMaximumLifetimeIsRejectedBeforeStartingAProcess()
    {
        var processesBefore = GetFixtureProcessIds();
        var options = new ControlledProcessFixtureOptions
        {
            MaximumLifetime = TimeSpan.FromTicks(1)
        };

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => ControlledProcessFixture.StartAsync(options));

        Assert.Equal(processesBefore, GetFixtureProcessIds());
    }

    [Fact]
    public async Task RealObservationSourceFindsTheExactFixtureIdentity()
    {
        await using var fixture = await ControlledProcessFixture.StartAsync();

        var result = new WindowsProcessObservationSource().Observe();

        var observation = Assert.Single(
            result.Value!,
            candidate => candidate.ProcessId == fixture.ProcessId);
        Assert.Equal(ObservationAvailability.Available, observation.Identity.Availability);
        Assert.Equal(fixture.Identity, observation.Identity.Value);
    }

    [Fact]
    public async Task MultipleFixturesKeepDistinctCorrelationAndIndependentLifetimes()
    {
        await using var first = await ControlledProcessFixture.StartAsync();
        await using var second = await ControlledProcessFixture.StartAsync();

        Assert.NotEqual(first.FixtureKey, second.FixtureKey);
        Assert.NotEqual(first.Identity, second.Identity);

        await first.DisposeAsync();

        Assert.False(first.IsRunning);
        Assert.True(second.IsRunning);
    }

    [Fact]
    public async Task CooperativeDisposalStopsTheProcessAndIsIdempotent()
    {
        var fixture = await ControlledProcessFixture.StartAsync();
        using var process = Process.GetProcessById(fixture.ProcessId);

        await fixture.DisposeAsync();
        await fixture.DisposeAsync();

        Assert.True(process.HasExited);
        Assert.False(fixture.IsRunning);
    }

    private static int[] GetFixtureProcessIds() =>
        Process.GetProcessesByName("BeadWise.ControlledProcessFixture")
            .Select(process =>
            {
                using (process)
                {
                    return process.Id;
                }
            })
            .Order()
            .ToArray();

    private static async Task WaitForFixtureProcessesAsync(int[] expectedProcessIds)
    {
        var timeout = TimeSpan.FromSeconds(3);
        var startedAt = Stopwatch.GetTimestamp();
        while (GetFixtureProcessIds() is var actualProcessIds
               && !actualProcessIds.SequenceEqual(expectedProcessIds))
        {
            if (Stopwatch.GetElapsedTime(startedAt) >= timeout)
            {
                Assert.Equal(expectedProcessIds, actualProcessIds);
            }

            await Task.Delay(TimeSpan.FromMilliseconds(25));
        }
    }
}
