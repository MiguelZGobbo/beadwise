using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Snapshots;

namespace BeadWise.Windows.Sessions;

internal sealed class SessionDocument
{
    public required Guid SessionId { get; init; }

    public required SessionState State { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public required DateTimeOffset UpdatedAt { get; init; }

    public required Guid? PlanId { get; init; }

    public required List<OperationDocument> Operations { get; init; }

    public required List<SnapshotDocument> Snapshots { get; init; }

    public required List<ResultDocument> Results { get; init; }

    public required ErrorDocument? Failure { get; init; }

    public required RecoveryState RecoveryState { get; init; }
}

internal sealed class OperationDocument
{
    public required Guid OperationId { get; init; }

    public required string Description { get; init; }

    public required string CurrentState { get; init; }

    public required string TargetState { get; init; }

    public required Risk Risk { get; init; }

    public required PrivilegeRequirement PrivilegeRequirement { get; init; }

    public required RestartRequirement RestartRequirement { get; init; }

    public required Reversibility Reversibility { get; init; }

    public required List<ExecutionResourceDocument> ExecutionResources { get; init; }
}

internal sealed class ExecutionResourceDocument
{
    public required string ResourceType { get; init; }

    public required string ResourceKey { get; init; }
}

internal sealed class SnapshotDocument
{
    public required Guid SnapshotId { get; init; }

    public required Guid SessionId { get; init; }

    public required Guid OperationId { get; init; }

    public required DateTimeOffset CapturedAt { get; init; }

    public required string StateKind { get; init; }

    public required string? OriginalState { get; init; }

    public required ExistenceSemantics ExistenceSemantics { get; init; }
}

internal sealed class ResultDocument
{
    public required ResultStatus Status { get; init; }

    public required string? Value { get; init; }

    public required ErrorDocument? Error { get; init; }

    public required List<string> Warnings { get; init; }
}

internal sealed class ErrorDocument
{
    public required string Code { get; init; }

    public required ErrorCategory Category { get; init; }

    public required string? TechnicalDetails { get; init; }

    public required bool Recoverable { get; init; }
}
