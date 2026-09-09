namespace BeadWise.Core.Snapshots;

public sealed record Snapshot
{
    public Snapshot(
        Guid snapshotId,
        Guid sessionId,
        Guid operationId,
        DateTimeOffset capturedAt,
        string stateKind,
        string? originalState,
        ExistenceSemantics existenceSemantics)
    {
        if (snapshotId == Guid.Empty)
        {
            throw new ArgumentException("A snapshot identity cannot be empty.", nameof(snapshotId));
        }

        if (sessionId == Guid.Empty)
        {
            throw new ArgumentException("A session identity cannot be empty.", nameof(sessionId));
        }

        if (operationId == Guid.Empty)
        {
            throw new ArgumentException("An operation identity cannot be empty.", nameof(operationId));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(stateKind);

        if (existenceSemantics is ExistenceSemantics.DidNotExist && originalState is not null)
        {
            throw new ArgumentException(
                "An original value cannot be stored when the state did not exist.",
                nameof(originalState));
        }

        SnapshotId = snapshotId;
        SessionId = sessionId;
        OperationId = operationId;
        CapturedAt = capturedAt;
        StateKind = stateKind;
        OriginalState = originalState;
        ExistenceSemantics = existenceSemantics;
    }

    public Guid SnapshotId { get; }

    public Guid SessionId { get; }

    public Guid OperationId { get; }

    public DateTimeOffset CapturedAt { get; }

    public string StateKind { get; }

    public string? OriginalState { get; }

    public ExistenceSemantics ExistenceSemantics { get; }
}
