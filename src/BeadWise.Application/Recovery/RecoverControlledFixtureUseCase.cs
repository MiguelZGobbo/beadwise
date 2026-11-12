using BeadWise.Application.Execution;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Logging;
using BeadWise.Core.Planning;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Snapshots;
using System.Runtime.CompilerServices;

namespace BeadWise.Application.Recovery;

public sealed class RecoverControlledFixtureUseCase
{
    private static readonly ConditionalWeakTable<IControlledFixtureRecovery, SemaphoreSlim> AttemptGates = new();
    private readonly IControlledFixtureRecovery _recovery;
    private readonly ISessionStore _store;
    private readonly TimeProvider _clock;
    private readonly TimeSpan _timeout;
    private readonly IExecutionLog _log;
    private readonly IPrivilegeBoundary _privilegeBoundary;

    public RecoverControlledFixtureUseCase(IControlledFixtureRecovery recovery, ISessionStore store,
        TimeProvider? timeProvider = null, TimeSpan? recoveryTimeout = null, IExecutionLog? log = null,
        IPrivilegeBoundary? privilegeBoundary = null)
    {
        ArgumentNullException.ThrowIfNull(recovery);
        ArgumentNullException.ThrowIfNull(store);
        _recovery = recovery;
        _store = store;
        _clock = timeProvider ?? TimeProvider.System;
        _timeout = recoveryTimeout ?? TimeSpan.FromSeconds(5);
        if (_timeout <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(recoveryTimeout));
        _log = log ?? NullExecutionLog.Instance;
        _privilegeBoundary = privilegeBoundary ?? new UnprivilegedBoundary();
    }

    public async Task<Result<Session>> ExecuteAsync(ChangePlan? plan, Session? session, RecoveryReason reason,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested) return Cancelled();
        var validationError = Validate(plan, session);
        if (validationError is not null) return Result<Session>.Failed(validationError);

        var gate = AttemptGates.GetValue(_recovery, static _ => new SemaphoreSlim(1, 1));
        try
        {
            await gate.WaitAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            return Cancelled();
        }

        try
        {
            if (cancellationToken.IsCancellationRequested) return Cancelled();
            return await ExecuteLockedAsync(plan!, session!, reason, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            gate.Release();
        }
    }

    private async Task<Result<Session>> ExecuteLockedAsync(ChangePlan plan, Session requested,
        RecoveryReason reason, CancellationToken cancellationToken)
    {
        var latest = _store.Load(requested.SessionId);
        if (latest.Status is not ResultStatus.Success || latest.Value is null)
            return MapSaveFailure(latest);
        var session = latest.Value;
        if (session.SessionId != requested.SessionId)
            return Result<Session>.Failed(new Error("RECOVERY_SESSION_MISMATCH", ErrorCategory.InvalidPlan));
        var validationError = Validate(plan, session);
        if (validationError is not null) return Result<Session>.Failed(validationError);

        Session recovering;
        try
        {
            recovering = session.BeginRecovery(reason, _recovery.OriginalIdentity, _clock.GetUtcNow());
        }
        catch (Exception exception) when (exception is InvalidOperationException or ArgumentException)
        {
            return Result<Session>.Failed(new Error("INVALID_RECOVERY_REQUEST", ErrorCategory.InvalidPlan,
                exception.Message));
        }

        var authorization = _privilegeBoundary.Authorize(plan.PrivilegeRequirements
            .Concat(plan.Operations.Select(operation => operation.PrivilegeRequirement)).Distinct().ToArray());
        if (authorization.Status is not ResultStatus.Success || !authorization.Value)
        {
            var error = authorization.Error ?? new Error("PRIVILEGE_DENIED", ErrorCategory.Blocked);
            _log.WriteForPlan(ExecutionEvent.Failure, plan, recovering.SessionId, error);
            return Result<Session>.Blocked(error);
        }

        if (cancellationToken.IsCancellationRequested) return Cancelled();
        if (session.State is SessionState.Recovered)
        {
            var replay = await VerifyRecoveredAsync(plan, session, cancellationToken).ConfigureAwait(false);
            if (replay is not null) return replay;
        }
        if (cancellationToken.IsCancellationRequested) return Cancelled();
        var saved = _store.Save(recovering);
        if (saved.Status is not ResultStatus.Success)
        {
            _log.WriteForPlan(ExecutionEvent.Failure, plan, recovering.SessionId, saved.Error);
            return MapSaveFailure(saved);
        }
        _log.WriteForPlan(ExecutionEvent.SessionPersisted, plan, recovering.SessionId);
        _log.WriteForPlan(ExecutionEvent.RecoveryStarted, plan!, recovering.SessionId);

        using var timeout = new CancellationTokenSource(_timeout, _clock);
        using var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeout.Token);
        var startedAt = _clock.GetTimestamp();
        Task<Result<ProcessIdentity>>? restoreTask = null;
        var insideAdapter = false;
        try
        {
            linked.Token.ThrowIfCancellationRequested();
            insideAdapter = true;
            restoreTask = _recovery.RestoreAsync(plan!, linked.Token);
            var restored = await restoreTask.WaitAsync(linked.Token).ConfigureAwait(false);
            insideAdapter = false;
            if (restored.Value is { } identity && (restored.Status is ResultStatus.Success or ResultStatus.Partial))
                recovering = recovering.RecordRecoveryIdentity(identity, _clock.GetUtcNow());
            if (_clock.GetElapsedTime(startedAt) > _timeout)
                return PersistFailure(plan!, recovering, TimeoutError(), ResultStatus.Failed, false);
            if (restored.Status is not ResultStatus.Success)
                return PersistFailure(plan!, recovering, restored.Error ?? RecoveryError("RECOVERY_RESTORE_FAILED"),
                    restored.Status, restored.Status is ResultStatus.Partial);

            var recoveredIdentity = restored.Value;
            _log.WriteForPlan(ExecutionEvent.VerifyStarted, plan!, recovering.SessionId);
            insideAdapter = true;
            var verification = await _recovery.VerifyAsync(plan!, recoveredIdentity, linked.Token)
                .WaitAsync(linked.Token).ConfigureAwait(false);
            insideAdapter = false;
            if (_clock.GetElapsedTime(startedAt) > _timeout)
                return PersistFailure(plan!, recovering, TimeoutError(), ResultStatus.Failed, false);
            if (verification.Status is not ResultStatus.Success || !verification.Value)
                return PersistFailure(plan!, recovering, verification.Error ?? RecoveryError("RECOVERY_VERIFY_FAILED"),
                    verification.Status is ResultStatus.Success ? ResultStatus.Failed : verification.Status, false);

            _log.WriteForPlan(ExecutionEvent.VerifyCompleted, plan!, recovering.SessionId);
            var completed = WithResult(recovering, Result<string>.Success(
                $"RECOVERY_VERIFIED:{plan!.Operations[0].OperationId:D}"))
                .CompleteRecovery(recoveredIdentity, _clock.GetUtcNow());
            var persisted = _store.Save(completed);
            if (persisted.Status is not ResultStatus.Success)
            {
                var partial = PersistencePartial(completed, persisted.Error);
                _log.WriteForPlan(ExecutionEvent.Failure, plan, completed.SessionId, partial.Error);
                return partial;
            }
            _log.WriteForPlan(ExecutionEvent.SessionPersisted, plan, completed.SessionId);
            _log.WriteForPlan(ExecutionEvent.RecoveryCompleted, plan, completed.SessionId);
            return Result<Session>.Success(completed);
        }
        catch (OperationCanceledException) when (linked.IsCancellationRequested)
        {
            var cancelled = cancellationToken.IsCancellationRequested;
            var error = new Error(cancelled ? "RECOVERY_CANCELLED" : "RECOVERY_TIMEOUT",
                cancelled ? ErrorCategory.Cancelled : ErrorCategory.Timeout,
                "Recovery did not finish within the allowed operation window.", recoverable: true);
            return PersistFailure(plan!, recovering, error,
                cancelled ? ResultStatus.Cancelled : ResultStatus.Failed,
                restoreTask is { IsCompleted: false });
        }
        catch (Exception exception) when (insideAdapter && exception is not OutOfMemoryException)
        {
            var cancelled = exception is OperationCanceledException;
            var error = new Error(cancelled ? "RECOVERY_CANCELLED" : "RECOVERY_ADAPTER_FAILED",
                cancelled ? ErrorCategory.Cancelled : ErrorCategory.RecoveryFailed,
                exception.GetType().FullName, recoverable: true);
            return PersistFailure(plan!, recovering, error,
                cancelled ? ResultStatus.Cancelled : ResultStatus.Failed,
                recovering.RecoveryEvidence!.RecoveredIdentity is null);
        }
        catch (ArgumentException exception)
        {
            return PersistFailure(plan!, recovering, new Error("RECOVERY_IDENTITY_INVALID",
                ErrorCategory.RecoveryFailed, exception.Message), ResultStatus.Failed, false);
        }
    }

    private async Task<Result<Session>?> VerifyRecoveredAsync(ChangePlan plan, Session session,
        CancellationToken cancellationToken)
    {
        var identity = session.RecoveryEvidence?.RecoveredIdentity;
        if (identity is null)
            return Result<Session>.Partial(session, RecoveryError("RECOVERY_EVIDENCE_MISSING"));
        using var timeout = new CancellationTokenSource(_timeout, _clock);
        using var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeout.Token);
        var startedAt = _clock.GetTimestamp();
        try
        {
            _log.WriteForPlan(ExecutionEvent.VerifyStarted, plan, session.SessionId);
            var result = await _recovery.VerifyAsync(plan, identity.Value, linked.Token)
                .WaitAsync(linked.Token).ConfigureAwait(false);
            if (cancellationToken.IsCancellationRequested) return Cancelled();
            if (result.Status is ResultStatus.Cancelled)
                return Result<Session>.Cancelled(result.Error ??
                    new Error("RECOVERY_CANCELLED", ErrorCategory.Cancelled, recoverable: true));
            if (_clock.GetElapsedTime(startedAt) > _timeout)
                return Result<Session>.Partial(session, TimeoutError());
            if (result.Status is ResultStatus.Success)
            {
                _log.WriteForPlan(ExecutionEvent.VerifyCompleted, plan, session.SessionId);
                return result.Value ? Result<Session>.Success(session) : null;
            }
            return Result<Session>.Partial(session, result.Error ?? RecoveryError("RECOVERY_VERIFY_FAILED"));
        }
        catch (OperationCanceledException) when (linked.IsCancellationRequested)
        {
            return cancellationToken.IsCancellationRequested
                ? Cancelled()
                : Result<Session>.Partial(session, TimeoutError());
        }
        catch (Exception exception) when (exception is not OutOfMemoryException)
        {
            return Result<Session>.Partial(session, new Error("RECOVERY_ADAPTER_FAILED",
                ErrorCategory.RecoveryFailed, exception.GetType().FullName, recoverable: true));
        }
    }

    private Result<Session> PersistFailure(ChangePlan plan, Session recovering, Error error,
        ResultStatus status, bool uncertain)
    {
        var outcome = status switch
        {
            ResultStatus.Cancelled => Result<string>.Cancelled(error),
            ResultStatus.Blocked => Result<string>.Blocked(error),
            ResultStatus.Unsupported => Result<string>.Unsupported(error),
            ResultStatus.Partial => Result<string>.Partial(null, error),
            _ => Result<string>.Failed(error)
        };
        var failed = WithResult(recovering, outcome).TransitionTo(SessionState.Failed, _clock.GetUtcNow(), error);
        var saved = _store.Save(failed);
        _log.WriteForPlan(ExecutionEvent.Failure, plan, failed.SessionId, error);
        if (saved.Status is not ResultStatus.Success) return PersistencePartial(failed, saved.Error);
        if (uncertain) return Result<Session>.Partial(failed, error);
        return status switch
        {
            ResultStatus.Partial => Result<Session>.Partial(failed, error),
            ResultStatus.Cancelled => Result<Session>.Cancelled(error),
            ResultStatus.Blocked => Result<Session>.Blocked(error),
            ResultStatus.Unsupported => Result<Session>.Unsupported(error),
            _ => Result<Session>.Failed(error)
        };
    }

    private static Error? Validate(ChangePlan? plan, Session? session)
    {
        if (plan is null || plan.Operations.Count != 1 ||
            plan.Operations[0].Description != "Stop controlled process fixture" ||
            plan.Operations[0].CurrentState != "Running" || plan.Operations[0].TargetState != "NotRunning")
            return new Error("INVALID_RECOVERY_PLAN", ErrorCategory.InvalidPlan);
        if (session is null || session.PlanId != plan.PlanId || session.Operations.Count != 1 ||
            !session.Operations[0].HasSameLogicalContentAs(plan.Operations[0]))
            return new Error("RECOVERY_SESSION_PLAN_MISMATCH", ErrorCategory.InvalidPlan);
        var operation = plan.Operations[0];
        if (operation.ExecutionResources.Count != 1)
            return new Error("INVALID_RECOVERY_PLAN", ErrorCategory.InvalidPlan);
        var resource = operation.ExecutionResources[0];
        var expectedState = $"Running|resource={resource.ResourceType}:{resource.ResourceKey}|action={operation.OperationId:D}";
        if (!session.Snapshots.Any(snapshot => snapshot.SessionId == session.SessionId &&
            snapshot.OperationId == plan.Operations[0].OperationId && snapshot.StateKind == "ControlledProcessState" &&
            snapshot.ExistenceSemantics is ExistenceSemantics.Existed && snapshot.OriginalState == expectedState &&
            snapshot.CapturedAt <= session.UpdatedAt))
            return new Error("RECOVERY_SNAPSHOT_MISSING", ErrorCategory.InvalidPlan);
        return null;
    }

    private static Session WithResult(Session session, Result<string> result) => new(
        session.SessionId, session.State, session.CreatedAt, session.UpdatedAt, session.PlanId,
        session.Operations, session.Snapshots, [.. session.Results, result], session.Failure,
        session.RecoveryState, session.RecoveryEvidence);

    private static Error RecoveryError(string code) => new(code, ErrorCategory.RecoveryFailed,
        "The controlled fixture could not be restored and verified.", recoverable: true);

    private static Error TimeoutError() => new("RECOVERY_TIMEOUT", ErrorCategory.Timeout,
        "Recovery did not finish within the allowed operation window.", recoverable: true);

    private static Result<Session> Cancelled() => Result<Session>.Cancelled(new Error("RECOVERY_CANCELLED",
        ErrorCategory.Cancelled, "Recovery was cancelled before an attempt began.", recoverable: true));

    private static Result<Session> PersistencePartial(Session session, Error? error) =>
        Result<Session>.Partial(session, new Error("RECOVERY_RESULT_PERSIST_FAILED",
            ErrorCategory.PartialFailure, error?.Code, recoverable: true));

    private static Result<Session> MapSaveFailure(Result<Session> source) => source.Status switch
    {
        ResultStatus.Blocked => Result<Session>.Blocked(source.Error!),
        ResultStatus.Unsupported => Result<Session>.Unsupported(source.Error!),
        ResultStatus.Cancelled => Result<Session>.Cancelled(source.Error!),
        ResultStatus.Partial => Result<Session>.Partial(null, source.Error, source.Warnings),
        _ => Result<Session>.Failed(source.Error ?? new Error("RECOVERY_PERSIST_FAILED", ErrorCategory.Unavailable))
    };
}
