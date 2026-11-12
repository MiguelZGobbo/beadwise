namespace BeadWise.Windows.Tests.Fixtures;

public sealed class FixtureStartupSlotTests
{
    [Fact]
    public async Task FailedStartupCleanupBlocksAnotherLaunchUntilExitIsConfirmed()
    {
        var running = true;
        var released = false;
        var launches = 0;
        var pending = new PendingFixtureCleanup(
            () => !running,
            () => Task.FromException(new IOException("cleanup failed")),
            () => throw new InvalidOperationException("retry must not force termination"),
            () => released = true);
        var slot = new FixtureStartupSlot();
        slot.Retain(pending);

        if (await slot.CanStartAsync()) launches++;
        Assert.Equal(0, launches);
        Assert.False(released);
        running = false;
        if (await slot.CanStartAsync()) launches++;
        Assert.Equal(1, launches);
        Assert.True(released);
    }

    [Fact]
    public async Task DisposalCleansRetainedChildBeforeReleasingItsHandle()
    {
        var running = true;
        var forced = false;
        var released = false;
        var slot = new FixtureStartupSlot();
        slot.Retain(new PendingFixtureCleanup(
            () => !running,
            () => Task.FromException(new IOException("cleanup failed")),
            () => { forced = true; running = false; return Task.CompletedTask; },
            () => released = true));

        Assert.True(await slot.CleanupForDisposalAsync());
        Assert.True(forced);
        Assert.True(released);
        Assert.True(await slot.CleanupForDisposalAsync());
    }

    [Fact]
    public async Task UnavailableExitStateKeepsTheOwnedHandleAndBlocksNewLaunches()
    {
        var released = false;
        var slot = new FixtureStartupSlot();
        slot.Retain(new PendingFixtureCleanup(
            () => throw new InvalidOperationException("state unavailable"),
            () => throw new InvalidOperationException("must not stop without state"),
            () => throw new InvalidOperationException("must not force without state"),
            () => released = true));

        Assert.False(await slot.CanStartAsync());
        Assert.False(await slot.CleanupForDisposalAsync());
        Assert.False(released);
    }
}
