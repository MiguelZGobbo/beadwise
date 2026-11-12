using System.Runtime.ExceptionServices;

namespace BeadWise.Windows.Tests.Fixtures;

internal sealed class FixtureDisposalCoordinator(
    object gate,
    Func<Task> disposeRecovery,
    Func<Task> stopOriginal,
    Action releaseOriginal)
{
    private Task? _attempt;
    private bool _originalReleased;
    private ExceptionDispatchInfo? _originalFailure;

    public Task DisposeAsync()
    {
        lock (gate)
        {
            if (_attempt is null || _attempt.IsFaulted || _attempt.IsCanceled)
                _attempt = DisposeCoreAsync();
            return _attempt;
        }
    }

    private async Task DisposeCoreAsync()
    {
        try
        {
            await disposeRecovery();
        }
        finally
        {
            await DisposeOriginalOnceAsync();
        }
    }

    private async Task DisposeOriginalOnceAsync()
    {
        if (_originalReleased)
        {
            _originalFailure?.Throw();
            return;
        }

        try
        {
            try { await stopOriginal(); }
            finally { releaseOriginal(); }
        }
        catch (Exception exception) when (exception is not OutOfMemoryException)
        {
            _originalFailure = ExceptionDispatchInfo.Capture(exception);
            throw;
        }
        finally
        {
            _originalReleased = true;
        }
    }
}
