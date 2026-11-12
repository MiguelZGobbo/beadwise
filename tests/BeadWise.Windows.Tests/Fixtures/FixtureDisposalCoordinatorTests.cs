namespace BeadWise.Windows.Tests.Fixtures;

public sealed class FixtureDisposalCoordinatorTests
{
    [Fact]
    public async Task FailedRecoveryCleanupCanBeRetriedWithoutReusingDisposedOriginal()
    {
        var recoveryCalls = 0;
        var stopCalls = 0;
        var disposeCalls = 0;
        var coordinator = new FixtureDisposalCoordinator(new object(),
            () => ++recoveryCalls == 1 ? Task.FromException(new IOException("cleanup pending")) : Task.CompletedTask,
            () => { stopCalls++; return Task.CompletedTask; },
            () => disposeCalls++);

        await Assert.ThrowsAsync<IOException>(() => coordinator.DisposeAsync());
        await coordinator.DisposeAsync();
        await coordinator.DisposeAsync();

        Assert.Equal(2, recoveryCalls);
        Assert.Equal(1, stopCalls);
        Assert.Equal(1, disposeCalls);
    }

    [Fact]
    public async Task ConcurrentAndCompletedDisposalsShareTheSameAttempt()
    {
        var finish = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var calls = 0;
        var coordinator = new FixtureDisposalCoordinator(new object(),
            () => { calls++; return finish.Task; }, () => Task.CompletedTask, () => { });
        var first = coordinator.DisposeAsync();
        var second = coordinator.DisposeAsync();
        Assert.Same(first, second);
        finish.SetResult();
        await Task.WhenAll(first, second);
        Assert.Same(first, coordinator.DisposeAsync());
        Assert.Equal(1, calls);
    }

    [Fact]
    public async Task OriginalStopFailureRemainsVisibleOnRetryWithoutReusingTheHandle()
    {
        var stopCalls = 0;
        var disposed = 0;
        var coordinator = new FixtureDisposalCoordinator(new object(), () => Task.CompletedTask,
            () => { stopCalls++; return Task.FromException(new IOException("stop failed")); },
            () => disposed++);
        await Assert.ThrowsAsync<IOException>(() => coordinator.DisposeAsync());
        await Assert.ThrowsAsync<IOException>(() => coordinator.DisposeAsync());
        Assert.Equal(1, stopCalls);
        Assert.Equal(1, disposed);
    }
}
