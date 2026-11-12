namespace BeadWise.Windows.Tests.Fixtures;

// Access is serialized by the recovery context's gate.
internal sealed class FixtureStartupSlot
{
    private PendingFixtureCleanup? _pending;

    public void Retain(PendingFixtureCleanup pending)
    {
        ArgumentNullException.ThrowIfNull(pending);
        if (_pending is not null) throw new InvalidOperationException("A startup cleanup is already pending.");
        _pending = pending;
    }

    public Task<bool> CanStartAsync() => ClearAsync(force: false);

    public Task<bool> CleanupForDisposalAsync() => ClearAsync(force: true);

    private async Task<bool> ClearAsync(bool force)
    {
        if (_pending is null) return true;
        if (!await _pending.TryCleanupAsync(force)) return false;
        _pending = null;
        return true;
    }
}

internal sealed class PendingFixtureCleanup(
    Func<bool> hasExited,
    Func<Task> stop,
    Func<Task> forceStop,
    Action release)
{
    private bool _released;

    public async Task<bool> TryCleanupAsync(bool force)
    {
        if (_released) return true;
        try
        {
            if (!hasExited())
            {
                try { await stop(); }
                catch (Exception exception) when (exception is not OutOfMemoryException) { if (!force) return false; }
            }
            if (!hasExited() && force) await forceStop();
            if (!hasExited()) return false;
            release();
            _released = true;
            return true;
        }
        catch (Exception exception) when (exception is not OutOfMemoryException)
        {
            // Failure to establish exit must keep the owned handle and block another launch.
            return false;
        }
    }
}
