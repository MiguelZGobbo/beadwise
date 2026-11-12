using BeadWise.Core.Errors;

namespace BeadWise.Core.Logging;

public enum ExecutionEvent
{
    ObservationStarted, ObservationCompleted, PlanBuilt, DryRunCompleted,
    SessionPersisted, SnapshotCaptured, OperationStarted, OperationCompleted,
    VerifyStarted, VerifyCompleted, RecoveryStarted, RecoveryCompleted, Failure,
    DiagnosticStarted, DiagnosticCompleted, Cancelled
}

/// <summary>Metadata only: deliberately has no free-text payload or native identifiers.</summary>
public sealed record ExecutionLogEntry(
    DateTimeOffset Timestamp,
    ExecutionEvent Event,
    Guid? SessionId = null,
    Guid? PlanId = null,
    string? FeatureId = null,
    Guid? OperationId = null,
    Guid? DiagnosticRunId = null,
    string? ErrorCode = null,
    ErrorCategory? ErrorCategory = null)
{
    public string Severity => Event is ExecutionEvent.Failure ? "Error"
        : ErrorCategory is not null ? "Warning" : "Information";
}
