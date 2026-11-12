using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Planning;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Logging;

namespace BeadWise.Application.Execution;

public sealed class ApplyChangePlanUseCase
{
    private readonly IControlledProcessActionExecutor _executor;
    private readonly ISessionStore _sessionStore;
    private readonly TimeProvider _timeProvider;
    private readonly IPrivilegeBoundary _privilegeBoundary;
    private readonly IExecutionLog _log;

    public ApplyChangePlanUseCase(
        IControlledProcessActionExecutor executor,
        ISessionStore sessionStore,
        TimeProvider? timeProvider = null,
        IPrivilegeBoundary? privilegeBoundary = null,
        IExecutionLog? log = null)
    {
        ArgumentNullException.ThrowIfNull(executor);
        ArgumentNullException.ThrowIfNull(sessionStore);

        _executor = executor;
        _sessionStore = sessionStore;
        _timeProvider = timeProvider ?? TimeProvider.System;
        _privilegeBoundary = privilegeBoundary ?? new UnprivilegedBoundary();
        _log = log ?? NullExecutionLog.Instance;
    }

    public Result<Session> Execute(ChangePlan? plan, Session? approvedSession)
    {
        var validationError = Validate(plan, approvedSession);
        if (validationError is not null)
        {
            return Result<Session>.Failed(validationError);
        }

        var approvedPlan = plan!;
        var session = approvedSession!;
        var operation = approvedPlan.Operations[0];
        var authorization = _privilegeBoundary.Authorize(
            approvedPlan.PrivilegeRequirements.Concat(approvedPlan.Operations.Select(item => item.PrivilegeRequirement))
                .Distinct().ToArray());
        if (authorization.Status is not ResultStatus.Success || authorization.Value != true)
        {
            var error = authorization.Error ?? new Error("PRIVILEGE_DENIED", ErrorCategory.Blocked);
            _log.WriteForPlan(ExecutionEvent.Failure, approvedPlan, session.SessionId, error);
            return Result<Session>.Blocked(error);
        }
        var prepared = _executor.Prepare(approvedPlan, session);
        if (prepared.Status is not ResultStatus.Success || prepared.Value is null)
        {
            prepared.Value?.Dispose();
            _log.WriteForPlan(ExecutionEvent.Failure, approvedPlan, session.SessionId, prepared.Error);
            return MapFailure<IControlledProcessActionPreparation>(prepared);
        }

        using var preparation = prepared.Value;
        if (preparation.PlanId != approvedPlan.PlanId ||
            preparation.OperationId != operation.OperationId ||
            preparation.Snapshot.SessionId != session.SessionId ||
            preparation.Snapshot.OperationId != operation.OperationId)
        {
            return Result<Session>.Failed(
                new Error(
                    "INVALID_APPLY_PREPARATION",
                    ErrorCategory.InvalidPlan,
                    "The prepared authorization does not match the approved plan and session."));
        }

        var applyingSession = CopySession(
            session.TransitionTo(SessionState.Applying, _timeProvider.GetUtcNow()),
            snapshots: [.. session.Snapshots, preparation.Snapshot]);
        var preApplySave = _sessionStore.Save(applyingSession);
        if (preApplySave.Status is not ResultStatus.Success)
        {
            _log.WriteForPlan(ExecutionEvent.Failure, approvedPlan, session.SessionId, preApplySave.Error);
            return MapFailure<Session>(preApplySave);
        }

        _log.WriteForPlan(ExecutionEvent.SnapshotCaptured, approvedPlan, session.SessionId);
        _log.WriteForPlan(ExecutionEvent.SessionPersisted, approvedPlan, session.SessionId);
        _log.WriteForPlan(ExecutionEvent.OperationStarted, approvedPlan, session.SessionId);
        var applyResult = _executor.Execute(approvedPlan, applyingSession, preparation);
        _log.WriteForPlan(applyResult.Status is ResultStatus.Success ? ExecutionEvent.OperationCompleted : ExecutionEvent.Failure,
            approvedPlan, session.SessionId, applyResult.Error);
        var operationResult = applyResult.Status is ResultStatus.Success
            ? Result<string>.Success($"APPLY_DISPATCHED:{operation.OperationId:D}")
            : MapOperationFailure(applyResult);
        var recordedSession = CopySession(
            applyingSession,
            results: [.. applyingSession.Results, operationResult]);

        if (applyResult.Status is ResultStatus.Failed or
            ResultStatus.Blocked or
            ResultStatus.Unsupported or
            ResultStatus.Cancelled)
        {
            recordedSession = recordedSession.TransitionTo(
                SessionState.Failed,
                _timeProvider.GetUtcNow(),
                applyResult.Error ?? new Error(
                    "APPLY_FAILED",
                    ErrorCategory.ApplyFailed,
                    "Apply ended without the required structured error."));
        }

        var postApplySave = _sessionStore.Save(recordedSession);
        if (postApplySave.Status is not ResultStatus.Success)
        {
            _log.WriteForPlan(ExecutionEvent.Failure, approvedPlan, session.SessionId, postApplySave.Error);
            return Result<Session>.Partial(
                recordedSession,
                new Error(
                    "APPLY_RESULT_PERSIST_FAILED",
                    ErrorCategory.PartialFailure,
                    postApplySave.Error?.Code,
                    recoverable: true));
        }

        _log.WriteForPlan(ExecutionEvent.SessionPersisted, approvedPlan, session.SessionId);
        return applyResult.Status switch
        {
            ResultStatus.Success => Result<Session>.Success(recordedSession),
            ResultStatus.Partial => Result<Session>.Partial(
                recordedSession,
                applyResult.Error,
                applyResult.Warnings),
            ResultStatus.Blocked => Result<Session>.Blocked(applyResult.Error!),
            ResultStatus.Unsupported => Result<Session>.Unsupported(applyResult.Error!),
            ResultStatus.Cancelled => Result<Session>.Cancelled(applyResult.Error!),
            _ => Result<Session>.Failed(applyResult.Error!)
        };
    }

    private static Error? Validate(ChangePlan? plan, Session? session)
    {
        if (plan is null || plan.Operations.Count != 1)
        {
            return new Error(
                "INVALID_APPLY_PLAN",
                ErrorCategory.InvalidPlan,
                "Apply requires exactly one existing operation.");
        }

        if (session is null || session.State is not SessionState.Approved)
        {
            return new Error(
                "SESSION_NOT_APPROVED",
                ErrorCategory.InvalidPlan,
                "Apply requires an approved session.");
        }

        if (session.PlanId != plan.PlanId ||
            session.Operations.Count != plan.Operations.Count ||
            !session.Operations[0].HasSameLogicalContentAs(plan.Operations[0]))
        {
            return new Error(
                "SESSION_PLAN_MISMATCH",
                ErrorCategory.InvalidPlan,
                "The approved session does not contain the supplied logical plan.");
        }

        return null;
    }

    private static Session CopySession(
        Session source,
        IEnumerable<Core.Snapshots.Snapshot>? snapshots = null,
        IEnumerable<Result<string>>? results = null) =>
        new(
            source.SessionId,
            source.State,
            source.CreatedAt,
            source.UpdatedAt,
            source.PlanId,
            source.Operations,
            snapshots ?? source.Snapshots,
            results ?? source.Results,
            source.Failure,
            source.RecoveryState,
            source.RecoveryEvidence);

    private static Result<string> MapOperationFailure(Result<Operation> source) =>
        source.Status switch
        {
            ResultStatus.Failed => Result<string>.Failed(source.Error!),
            ResultStatus.Blocked => Result<string>.Blocked(source.Error!),
            ResultStatus.Unsupported => Result<string>.Unsupported(source.Error!),
            ResultStatus.Cancelled => Result<string>.Cancelled(source.Error!),
            _ => Result<string>.Partial(null, source.Error, source.Warnings)
        };

    private static Result<Session> MapFailure<T>(Result<T> source) =>
        source.Status switch
        {
            ResultStatus.Blocked => Result<Session>.Blocked(source.Error!),
            ResultStatus.Unsupported => Result<Session>.Unsupported(source.Error!),
            ResultStatus.Cancelled => Result<Session>.Cancelled(source.Error!),
            ResultStatus.Partial => Result<Session>.Partial(null, source.Error, source.Warnings),
            _ => Result<Session>.Failed(source.Error!)
        };
}
