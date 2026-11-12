using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Results;
using BeadWise.Core.Snapshots;
using BeadWise.Core.Processes;

namespace BeadWise.Core.Sessions;

public sealed class Session
{
    public Session(
        Guid sessionId,
        SessionState state,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt,
        Guid? planId,
        IEnumerable<Operation> operations,
        IEnumerable<Snapshot> snapshots,
        IEnumerable<Result<string>> results,
        Error? failure,
        RecoveryState recoveryState,
        RecoveryEvidence? recoveryEvidence = null)
    {
        if (sessionId == Guid.Empty)
        {
            throw new ArgumentException("A session identity cannot be empty.", nameof(sessionId));
        }

        if (planId == Guid.Empty)
        {
            throw new ArgumentException("A plan identity cannot be empty.", nameof(planId));
        }

        ArgumentNullException.ThrowIfNull(operations);
        ArgumentNullException.ThrowIfNull(snapshots);
        ArgumentNullException.ThrowIfNull(results);

        if (updatedAt < createdAt)
        {
            throw new ArgumentOutOfRangeException(
                nameof(updatedAt),
                updatedAt,
                "A session update cannot predate its creation.");
        }

        ValidateState(state, failure, recoveryState, recoveryEvidence);

        SessionId = sessionId;
        State = state;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        PlanId = planId;
        Operations = Array.AsReadOnly(operations.ToArray());
        Snapshots = Array.AsReadOnly(snapshots.ToArray());
        Results = Array.AsReadOnly(results.ToArray());
        Failure = failure;
        RecoveryState = recoveryState;
        RecoveryEvidence = recoveryEvidence;
    }

    private static void ValidateState(
        SessionState state,
        Error? failure,
        RecoveryState recoveryState,
        RecoveryEvidence? evidence)
    {
        if (!Enum.IsDefined(state)) throw new ArgumentOutOfRangeException(nameof(state));
        if (!Enum.IsDefined(recoveryState)) throw new ArgumentOutOfRangeException(nameof(recoveryState));
        var explicitReset = evidence?.Reason is RecoveryReason.ExplicitFixtureReset;
        if (state is SessionState.Failed && failure is null &&
            !(explicitReset && recoveryState is RecoveryState.Failed && evidence!.RecoveryError is not null))
        {
            throw new ArgumentNullException(nameof(failure), "A failed session requires a structured error.");
        }

        if (state is SessionState.Recovering or SessionState.Recovered && failure is null && !explicitReset)
        {
            throw new ArgumentNullException(nameof(failure), "A recovery session requires its originating error.");
        }

        if (failure is not null && state is not (SessionState.Failed or SessionState.Recovering or SessionState.Recovered))
        {
            throw new ArgumentException("Only failed or recovery states can carry a failure.", nameof(failure));
        }

        var recoveryStateIsValid = state switch
        {
            SessionState.Failed => recoveryState is RecoveryState.Required or RecoveryState.Failed,
            SessionState.Recovering => recoveryState is RecoveryState.InProgress,
            SessionState.Recovered => recoveryState is RecoveryState.Completed,
            _ => recoveryState is RecoveryState.NotRequired
        };

        if (!recoveryStateIsValid)
        {
            throw new ArgumentException(
                $"Recovery state {recoveryState} is inconsistent with session state {state}.",
                nameof(recoveryState));
        }

        if (evidence is not null)
        {
            var evidenceMatchesState = state switch
            {
                SessionState.Recovering => evidence.RecoveryError is null,
                SessionState.Recovered => evidence.RecoveredIdentity is not null && evidence.RecoveryError is null,
                SessionState.Failed => recoveryState is RecoveryState.Failed && evidence.RecoveryError is not null,
                _ => false
            };
            if (!evidenceMatchesState || evidence.OriginalError != failure)
            {
                throw new ArgumentException("Recovery evidence is inconsistent with the session.", nameof(evidence));
            }
        }
    }

    public Guid SessionId { get; }

    public SessionState State { get; }

    public DateTimeOffset CreatedAt { get; }

    public DateTimeOffset UpdatedAt { get; }

    public Guid? PlanId { get; }

    public IReadOnlyList<Operation> Operations { get; }

    public IReadOnlyList<Snapshot> Snapshots { get; }

    public IReadOnlyList<Result<string>> Results { get; }

    public Error? Failure { get; }

    public RecoveryState RecoveryState { get; }

    public RecoveryEvidence? RecoveryEvidence { get; }

    public static Session Create(Guid sessionId, DateTimeOffset createdAt) =>
        new(
            sessionId,
            SessionState.Created,
            createdAt,
            createdAt,
            null,
            [],
            [],
            [],
            null,
            RecoveryState.NotRequired);

    public Session TransitionTo(
        SessionState nextState,
        DateTimeOffset updatedAt,
        Error? failure = null)
    {
        if (!CanTransition(State, nextState))
        {
            throw new InvalidOperationException($"Cannot transition a session from {State} to {nextState}.");
        }

        if (State is SessionState.Failed && nextState is SessionState.Recovering && RecoveryEvidence is not null)
            throw new InvalidOperationException("Use BeginRecovery to start a new recovery attempt.");

        if (updatedAt < UpdatedAt)
        {
            throw new ArgumentOutOfRangeException(
                nameof(updatedAt),
                updatedAt,
                "A session update cannot predate its current state.");
        }

        if (nextState is SessionState.Failed && failure is null)
        {
            throw new ArgumentNullException(nameof(failure), "A failed session requires a structured error.");
        }

        if (nextState is not SessionState.Failed && failure is not null)
        {
            throw new ArgumentException(
                "A failure can only be supplied when transitioning to Failed.",
                nameof(failure));
        }

        var nextFailure = nextState is SessionState.Failed && State is not SessionState.Recovering ? failure : Failure;
        var nextRecoveryState = nextState switch
        {
            SessionState.Recovering => RecoveryState.InProgress,
            SessionState.Recovered => RecoveryState.Completed,
            SessionState.Failed when State is SessionState.Recovering => RecoveryState.Failed,
            SessionState.Failed => RecoveryState.Required,
            _ => RecoveryState
        };

        var evidence = RecoveryEvidence;
        if (State is SessionState.Recovering && nextState is SessionState.Failed && evidence is not null)
        {
            evidence = new RecoveryEvidence(evidence.Reason, evidence.OriginalIdentity,
                evidence.RecoveredIdentity, evidence.OriginalError, failure);
        }

        return new Session(
            SessionId,
            nextState,
            CreatedAt,
            updatedAt,
            PlanId,
            Operations,
            Snapshots,
            Results,
            nextFailure,
            nextRecoveryState,
            evidence);
    }

    public Session BeginRecovery(RecoveryReason reason, ProcessIdentity originalIdentity, DateTimeOffset updatedAt)
    {
        if (updatedAt < UpdatedAt) throw new ArgumentOutOfRangeException(nameof(updatedAt));
        var permitted = reason switch
        {
            RecoveryReason.FailureRecovery => State is SessionState.Failed && Failure is not null &&
                (RecoveryEvidence is null || RecoveryEvidence.Reason is RecoveryReason.FailureRecovery),
            RecoveryReason.ExplicitFixtureReset => State is SessionState.Completed ||
                (State is SessionState.Failed && RecoveryEvidence?.Reason is RecoveryReason.ExplicitFixtureReset),
            _ => false
        };
        if (State is SessionState.Recovering or SessionState.Recovered)
        {
            permitted = RecoveryEvidence?.Reason == reason;
        }
        if (!permitted || (RecoveryEvidence is not null && RecoveryEvidence.OriginalIdentity != originalIdentity))
            throw new InvalidOperationException("The requested recovery is inconsistent with the session.");

        var evidence = new RecoveryEvidence(reason, originalIdentity, null, Failure, null);
        return new Session(SessionId, SessionState.Recovering, CreatedAt, updatedAt, PlanId,
            Operations, Snapshots, Results, Failure, RecoveryState.InProgress, evidence);
    }

    public Session CompleteRecovery(ProcessIdentity recoveredIdentity, DateTimeOffset updatedAt)
    {
        if (State is not SessionState.Recovering || RecoveryEvidence is null)
            throw new InvalidOperationException("Recovery has not been started with evidence.");
        if (updatedAt < UpdatedAt) throw new ArgumentOutOfRangeException(nameof(updatedAt));
        var evidence = new RecoveryEvidence(RecoveryEvidence.Reason, RecoveryEvidence.OriginalIdentity,
            recoveredIdentity, RecoveryEvidence.OriginalError, null);
        return new Session(SessionId, SessionState.Recovered, CreatedAt, updatedAt, PlanId,
            Operations, Snapshots, Results, Failure, RecoveryState.Completed, evidence);
    }

    public Session RecordRecoveryIdentity(ProcessIdentity recoveredIdentity, DateTimeOffset updatedAt)
    {
        if (State is not SessionState.Recovering || RecoveryEvidence is null)
            throw new InvalidOperationException("Recovery has not been started with evidence.");
        if (updatedAt < UpdatedAt) throw new ArgumentOutOfRangeException(nameof(updatedAt));
        var evidence = new RecoveryEvidence(RecoveryEvidence.Reason, RecoveryEvidence.OriginalIdentity,
            recoveredIdentity, RecoveryEvidence.OriginalError, null);
        return new Session(SessionId, State, CreatedAt, updatedAt, PlanId,
            Operations, Snapshots, Results, Failure, RecoveryState, evidence);
    }

    private static bool CanTransition(SessionState currentState, SessionState nextState) =>
        currentState switch
        {
            SessionState.Created => nextState is SessionState.Planned or SessionState.Failed,
            SessionState.Planned => nextState is SessionState.Approved or SessionState.Failed,
            SessionState.Approved => nextState is SessionState.Applying or SessionState.Failed,
            SessionState.Applying => nextState is SessionState.Verifying or SessionState.Failed,
            SessionState.Verifying => nextState is SessionState.Completed or SessionState.Failed,
            SessionState.Failed => nextState is SessionState.Recovering,
            SessionState.Recovering => nextState is SessionState.Recovered or SessionState.Failed,
            SessionState.Completed or SessionState.Recovered => false,
            _ => false
        };
}
