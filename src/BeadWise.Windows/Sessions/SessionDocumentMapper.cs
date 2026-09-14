using System.Text.Json;
using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Snapshots;

namespace BeadWise.Windows.Sessions;

internal static class SessionDocumentMapper
{
    public static SessionDocument ToDocument(Session session) =>
        new()
        {
            SessionId = session.SessionId,
            State = session.State,
            CreatedAt = session.CreatedAt,
            UpdatedAt = session.UpdatedAt,
            PlanId = session.PlanId,
            Operations = session.Operations.Select(ToDocument).ToList(),
            Snapshots = session.Snapshots.Select(ToDocument).ToList(),
            Results = session.Results.Select(ToDocument).ToList(),
            Failure = session.Failure is null ? null : ToDocument(session.Failure),
            RecoveryState = session.RecoveryState
        };

    public static Session FromDocument(SessionDocument document) =>
        new(
            document.SessionId,
            document.State,
            document.CreatedAt,
            document.UpdatedAt,
            document.PlanId,
            Required(document.Operations, nameof(document.Operations)).Select(FromDocument),
            Required(document.Snapshots, nameof(document.Snapshots)).Select(FromDocument),
            Required(document.Results, nameof(document.Results)).Select(FromDocument),
            document.Failure is null ? null : FromDocument(document.Failure),
            document.RecoveryState);

    private static OperationDocument ToDocument(Operation operation) =>
        new()
        {
            OperationId = operation.OperationId,
            Description = operation.Description,
            CurrentState = operation.CurrentState,
            TargetState = operation.TargetState,
            Risk = operation.Risk,
            PrivilegeRequirement = operation.PrivilegeRequirement,
            RestartRequirement = operation.RestartRequirement,
            Reversibility = operation.Reversibility,
            ExecutionResources = operation.ExecutionResources.Select(ToDocument).ToList()
        };

    private static Operation FromDocument(OperationDocument document) =>
        new(
            document.OperationId,
            document.Description,
            document.CurrentState,
            document.TargetState,
            document.Risk,
            document.PrivilegeRequirement,
            document.RestartRequirement,
            document.Reversibility,
            Required(document.ExecutionResources, nameof(document.ExecutionResources)).Select(FromDocument));

    private static ExecutionResourceDocument ToDocument(ExecutionResource resource) =>
        new()
        {
            ResourceType = resource.ResourceType,
            ResourceKey = resource.ResourceKey
        };

    private static ExecutionResource FromDocument(ExecutionResourceDocument document) =>
        new(document.ResourceType, document.ResourceKey);

    private static SnapshotDocument ToDocument(Snapshot snapshot) =>
        new()
        {
            SnapshotId = snapshot.SnapshotId,
            SessionId = snapshot.SessionId,
            OperationId = snapshot.OperationId,
            CapturedAt = snapshot.CapturedAt,
            StateKind = snapshot.StateKind,
            OriginalState = snapshot.OriginalState,
            ExistenceSemantics = snapshot.ExistenceSemantics
        };

    private static Snapshot FromDocument(SnapshotDocument document) =>
        new(
            document.SnapshotId,
            document.SessionId,
            document.OperationId,
            document.CapturedAt,
            document.StateKind,
            document.OriginalState,
            document.ExistenceSemantics);

    private static ResultDocument ToDocument(Result<string> result) =>
        new()
        {
            Status = result.Status,
            Value = result.Value,
            Error = result.Error is null ? null : ToDocument(result.Error),
            Warnings = result.Warnings.ToList()
        };

    private static Result<string> FromDocument(ResultDocument document)
    {
        var warnings = Required(document.Warnings, nameof(document.Warnings));
        var error = document.Error is null ? null : FromDocument(document.Error);

        return document.Status switch
        {
            ResultStatus.Success when document.Value is not null && error is null =>
                Result<string>.Success(document.Value, warnings),
            ResultStatus.Partial =>
                Result<string>.Partial(document.Value, error, warnings),
            ResultStatus.Failed when HasTerminalShape(document, error, warnings) =>
                Result<string>.Failed(error!),
            ResultStatus.Blocked when HasTerminalShape(document, error, warnings) =>
                Result<string>.Blocked(error!),
            ResultStatus.Unsupported when HasTerminalShape(document, error, warnings) =>
                Result<string>.Unsupported(error!),
            ResultStatus.Cancelled when HasTerminalShape(document, error, warnings) =>
                Result<string>.Cancelled(error!),
            _ => throw new JsonException($"Result status {document.Status} has inconsistent content.")
        };
    }

    private static bool HasTerminalShape(
        ResultDocument document,
        Error? error,
        IReadOnlyList<string> warnings) =>
        document.Value is null && error is not null && warnings.Count == 0;

    private static ErrorDocument ToDocument(Error error) =>
        new()
        {
            Code = error.Code,
            Category = error.Category,
            TechnicalDetails = error.TechnicalDetails,
            Recoverable = error.Recoverable
        };

    private static Error FromDocument(ErrorDocument document) =>
        new(
            document.Code,
            document.Category,
            document.TechnicalDetails,
            document.Recoverable);

    private static IReadOnlyList<T> Required<T>(IReadOnlyList<T>? values, string propertyName) =>
        values ?? throw new JsonException($"Property {propertyName} cannot be null.");
}
