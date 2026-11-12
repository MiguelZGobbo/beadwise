using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Planning;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Logging;

namespace BeadWise.Application.Execution;

public sealed class VerifyChangePlanUseCase
{
    private static readonly TimeSpan PollInterval = TimeSpan.FromMilliseconds(100);
    private static readonly TimeSpan VerificationTimeout = TimeSpan.FromSeconds(5);
    private readonly IControlledProcessStateSource _source;
    private readonly ISessionStore _sessionStore;
    private readonly TimeProvider _timeProvider;
    private readonly IExecutionLog _log;

    public VerifyChangePlanUseCase(
        IControlledProcessStateSource source,
        ISessionStore sessionStore,
        TimeProvider? timeProvider = null,
        IExecutionLog? log = null)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(sessionStore);
        _source = source;
        _sessionStore = sessionStore;
        _timeProvider = timeProvider ?? TimeProvider.System;
        _log = log ?? NullExecutionLog.Instance;
    }

    public async Task<Result<Session>> ExecuteAsync(
        ChangePlan? plan,
        Session? session,
        CancellationToken cancellationToken = default)
    {
        var validationError = VerifyChangePlanValidation.Validate(plan, session);
        if (validationError is not null)
        {
            return Result<Session>.Failed(validationError);
        }

        var verifying = session!;
        if (verifying.State is SessionState.Applying)
        {
            verifying = verifying.TransitionTo(SessionState.Verifying, _timeProvider.GetUtcNow());
            var saved = _sessionStore.Save(verifying);
            if (saved.Status is not ResultStatus.Success)
            {
                return saved;
            }
        }

        _log.WriteForPlan(ExecutionEvent.VerifyStarted, plan!, verifying.SessionId);
        var outcome = await ObserveUntilExitAsync(plan!, cancellationToken).ConfigureAwait(false);
        var result = PersistOutcome(verifying, outcome);
        _log.WriteForPlan(result.Status switch
        {
            ResultStatus.Success => ExecutionEvent.VerifyCompleted,
            ResultStatus.Cancelled => ExecutionEvent.Cancelled,
            _ => ExecutionEvent.Failure
        }, plan!, verifying.SessionId, result.Error);
        return result;
    }

    private async Task<Result<string>> ObserveUntilExitAsync(ChangePlan plan, CancellationToken cancellationToken)
    {
        var startedAt = _timeProvider.GetTimestamp();
        try
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (_timeProvider.GetElapsedTime(startedAt) > VerificationTimeout)
                {
                    return TimedOut();
                }

                var result = _source.Observe(plan);
                cancellationToken.ThrowIfCancellationRequested();
                if (_timeProvider.GetElapsedTime(startedAt) > VerificationTimeout)
                {
                    return TimedOut();
                }

                var observation = result.Value;
                if (result.Status is not ResultStatus.Success ||
                    observation is null || !observation.HasValue ||
                    observation.Availability is not ObservationAvailability.Available)
                {
                    return Result<string>.Failed(result.Error ?? observation?.Error ?? new Error(
                        "VERIFY_OBSERVATION_UNAVAILABLE", ErrorCategory.VerifyFailed,
                        "The controlled process state could not be established.", recoverable: true));
                }

                if (!observation.Value)
                {
                    return Result<string>.Success($"VERIFY_SUCCEEDED:{plan.Operations[0].OperationId:D}");
                }

                var remaining = VerificationTimeout - _timeProvider.GetElapsedTime(startedAt);
                if (remaining <= TimeSpan.Zero)
                {
                    return TimedOut();
                }

                await Task.Delay(remaining < PollInterval ? remaining : PollInterval,
                    _timeProvider, cancellationToken).ConfigureAwait(false);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            return Result<string>.Cancelled(new Error(
                "VERIFY_CANCELLED", ErrorCategory.Cancelled,
                "Verification was cancelled; the process outcome is not confirmed.", recoverable: true));
        }
    }

    private static Result<string> TimedOut() => Result<string>.Failed(new Error(
        "VERIFY_TIMEOUT", ErrorCategory.Timeout,
        "The controlled process exit was not confirmed within the verification window.", recoverable: true));

    private Result<Session> PersistOutcome(Session verifying, Result<string> outcome)
    {
        var recorded = new Session(verifying.SessionId, verifying.State, verifying.CreatedAt,
            _timeProvider.GetUtcNow(), verifying.PlanId, verifying.Operations, verifying.Snapshots,
            [.. verifying.Results, outcome], verifying.Failure, verifying.RecoveryState, verifying.RecoveryEvidence);
        if (outcome.Status is ResultStatus.Success)
        {
            recorded = recorded.TransitionTo(SessionState.Completed, recorded.UpdatedAt);
        }
        else if (outcome.Status is ResultStatus.Failed)
        {
            recorded = recorded.TransitionTo(SessionState.Failed, recorded.UpdatedAt, outcome.Error);
        }

        var saved = _sessionStore.Save(recorded);
        if (saved.Status is not ResultStatus.Success)
        {
            return Result<Session>.Partial(recorded, new Error(
                "VERIFY_RESULT_PERSIST_FAILED", ErrorCategory.PartialFailure,
                saved.Error?.Code, recoverable: true));
        }

        return outcome.Status switch
        {
            ResultStatus.Success => Result<Session>.Success(recorded),
            ResultStatus.Cancelled => Result<Session>.Cancelled(outcome.Error!),
            _ => Result<Session>.Failed(outcome.Error!)
        };
    }
}
