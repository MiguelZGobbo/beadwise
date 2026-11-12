using System.ComponentModel;
using System.Globalization;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Planning;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;
using BeadWise.Windows.Processes;

namespace BeadWise.Windows.Tests.Fixtures;

// This adapter is deliberately part of the harness: it alone owns the fixed
// fixture launcher and the live Process objects. Persisted data grants no authority.
internal sealed class ControlledFixtureRecovery : IControlledFixtureRecovery, IAsyncDisposable
{
    private readonly ControlledProcessFixture _original;
    private readonly ChangePlan _approvedPlan;
    private readonly SemaphoreSlim _gate = new(1, 1);
    private readonly FixtureStartupSlot _startupSlot = new();
    private ControlledProcessFixture _current;
    private int _closing;
    private bool _disposed;

    private ControlledFixtureRecovery(ControlledProcessFixture original, ChangePlan approvedPlan)
    {
        _original = original;
        _current = original;
        _approvedPlan = approvedPlan;
    }

    internal static ControlledFixtureRecovery ForOwner(ControlledProcessFixture original, ChangePlan approvedPlan)
    {
        ArgumentNullException.ThrowIfNull(original);
        ArgumentNullException.ThrowIfNull(approvedPlan);
        return original.GetOrCreateRecovery(approvedPlan, () => new ControlledFixtureRecovery(original, approvedPlan));
    }

    internal bool IsBoundTo(ControlledProcessFixture original, ChangePlan plan) =>
        ReferenceEquals(_original, original) && _approvedPlan.HasSameLogicalContentAs(plan);

    public ProcessIdentity OriginalIdentity => _original.Identity;
    public ProcessIdentity? ReplacementIdentity => ReferenceEquals(_current, _original) ? null : _current.Identity;
    public bool IsRunning => _current.IsRunning;

    public async Task<Result<ProcessIdentity>> RestoreAsync(ChangePlan plan, CancellationToken cancellationToken)
    {
        await _gate.WaitAsync(cancellationToken);
        var replacementStarted = false;
        try
        {
            var error = Validate(plan);
            if (error is not null) return Result<ProcessIdentity>.Blocked(error);
            if (!await _startupSlot.CanStartAsync())
                return Result<ProcessIdentity>.Blocked(new Error("RECOVERY_CLEANUP_PENDING", ErrorCategory.Blocked,
                    "A previously launched fixture has not been confirmed exited.", recoverable: true));
            var originalState = ReadOwned(_original.CreateTarget());
            if (originalState.Error is not null) return Result<ProcessIdentity>.Blocked(originalState.Error);
            var currentState = ReferenceEquals(_current, _original)
                ? originalState : ReadOwned(_current.CreateTarget());
            if (currentState.Error is not null) return Result<ProcessIdentity>.Blocked(currentState.Error);
            if (!ReferenceEquals(_current, _original) && originalState.Running)
                return Result<ProcessIdentity>.Blocked(new Error("TARGET_CHANGED", ErrorCategory.TargetChanged));
            if (currentState.Running) return Result<ProcessIdentity>.Success(_current.Identity);

            cancellationToken.ThrowIfCancellationRequested();
            var previous = _current;
            var replacement = await ControlledProcessFixture.StartAsync(new ControlledProcessFixtureOptions
            {
                RetainStartupCleanup = _startupSlot.Retain
            }, cancellationToken);
            _current = replacement;
            replacementStarted = true;
            if (!ReferenceEquals(previous, _original)) await previous.DisposeAsync();
            var replacementState = ReadOwned(replacement.CreateTarget());
            if (replacementState.Error is not null || !replacementState.Running)
                return Result<ProcessIdentity>.Partial(replacement.Identity,
                    replacementState.Error ?? new Error("RECOVERY_FIXTURE_EXITED", ErrorCategory.RecoveryFailed));
            return Result<ProcessIdentity>.Success(replacement.Identity);
        }
        catch (Exception exception) when (exception is Win32Exception or UnauthorizedAccessException or
            InvalidOperationException or ObjectDisposedException or IOException or TimeoutException)
        {
            var error = new Error("RECOVERY_FIXTURE_UNAVAILABLE",
                exception is TimeoutException ? ErrorCategory.Timeout : ErrorCategory.Unavailable,
                exception.GetType().FullName, recoverable: true);
            return replacementStarted
                ? Result<ProcessIdentity>.Partial(_current.Identity, error)
                : Result<ProcessIdentity>.Failed(error);
        }
        finally
        {
            _gate.Release();
        }
    }

    public async Task<Result<bool>> VerifyAsync(ChangePlan plan, ProcessIdentity identity,
        CancellationToken cancellationToken)
    {
        await _gate.WaitAsync(cancellationToken);
        try
        {
            var error = Validate(plan);
            if (error is not null) return Result<bool>.Blocked(error);
            var originalState = ReadOwned(_original.CreateTarget());
            if (originalState.Error is not null) return Result<bool>.Blocked(originalState.Error);
            var currentState = ReadOwned(_current.CreateTarget());
            if (currentState.Error is not null) return Result<bool>.Blocked(currentState.Error);
            if (_current.Identity != identity)
                return Result<bool>.Blocked(new Error("TARGET_CHANGED", ErrorCategory.TargetChanged));
            return Result<bool>.Success(currentState.Running);
        }
        catch (Exception exception) when (exception is Win32Exception or UnauthorizedAccessException or
            InvalidOperationException or ObjectDisposedException)
        {
            return Result<bool>.Failed(new Error("RECOVERY_VERIFY_UNAVAILABLE", ErrorCategory.Unavailable,
                exception.GetType().FullName, recoverable: true));
        }
        finally
        {
            _gate.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        Interlocked.Exchange(ref _closing, 1);
        await _gate.WaitAsync();
        try
        {
            if (_disposed) return;
            if (!await _startupSlot.CleanupForDisposalAsync())
                throw new InvalidOperationException("The owned startup process could not be confirmed exited.");
            if (!ReferenceEquals(_current, _original)) await _current.DisposeAsync();
            _disposed = true;
        }
        finally
        {
            _gate.Release();
        }
    }

    private Error? Validate(ChangePlan? plan)
    {
        if (Volatile.Read(ref _closing) != 0)
            return new Error("RECOVERY_CONTEXT_DISPOSED", ErrorCategory.Blocked);
        if (!_approvedPlan.HasSameLogicalContentAs(plan) || plan!.Operations.Count != 1)
            return new Error("INVALID_RECOVERY_PLAN", ErrorCategory.InvalidPlan);
        var resources = plan.Operations[0].ExecutionResources;
        var expected = string.Create(CultureInfo.InvariantCulture,
            $"{_original.FixtureKey:D}|pid={OriginalIdentity.ProcessId}|created={OriginalIdentity.CreationTime.ToUniversalTime():O}");
        if (resources.Count != 1 || resources[0].ResourceType != "Process" || resources[0].ResourceKey != expected)
            return new Error("FIXTURE_OWNERSHIP_MISMATCH", ErrorCategory.Blocked);
        return null;
    }

    internal static (bool Running, Error? Error) ReadOwned(ControlledProcessTarget target)
    {
        if (!target.IsOwnedLaunchContext())
            return (false, new Error("FIXTURE_OWNERSHIP_MISMATCH", ErrorCategory.Blocked));
        var running = target.IsRunning();
        if (running && target.ReadIdentity() != target.ExpectedIdentity)
            return (false, new Error("TARGET_CHANGED", ErrorCategory.TargetChanged));
        return (running, null);
    }
}
