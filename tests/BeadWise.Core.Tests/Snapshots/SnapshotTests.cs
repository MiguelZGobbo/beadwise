using BeadWise.Core.Snapshots;

namespace BeadWise.Core.Tests.Snapshots;

public sealed class SnapshotTests
{
    private static readonly Guid SnapshotId = Guid.Parse("7466e4ac-b1bc-4b7c-846a-85cc24eefebe");
    private static readonly Guid SessionId = Guid.Parse("7b766a4a-461f-44e2-bc6f-b66962f3ee06");
    private static readonly Guid OperationId = Guid.Parse("cb984403-4faf-4182-876b-710404f0b6bf");
    private static readonly DateTimeOffset CapturedAt = new(2026, 9, 9, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void MissingOriginalStateDiffersFromExistingNullState()
    {
        var missing = new Snapshot(
            SnapshotId,
            SessionId,
            OperationId,
            CapturedAt,
            "ProcessState",
            null,
            ExistenceSemantics.DidNotExist);
        var existingNull = new Snapshot(
            SnapshotId,
            SessionId,
            OperationId,
            CapturedAt,
            "ProcessState",
            null,
            ExistenceSemantics.Existed);

        Assert.NotEqual(missing, existingNull);
        Assert.Equal(ExistenceSemantics.DidNotExist, missing.ExistenceSemantics);
        Assert.Equal(ExistenceSemantics.Existed, existingNull.ExistenceSemantics);
    }

    [Fact]
    public void MissingOriginalStateRejectsAStoredValue()
    {
        var create = () => new Snapshot(
            SnapshotId,
            SessionId,
            OperationId,
            CapturedAt,
            "ProcessState",
            "Running",
            ExistenceSemantics.DidNotExist);

        Assert.Throws<ArgumentException>(create);
    }

    [Theory]
    [InlineData("snapshot")]
    [InlineData("session")]
    [InlineData("operation")]
    public void RejectsEmptyRequiredIdentity(string identity)
    {
        var create = () => new Snapshot(
            identity == "snapshot" ? Guid.Empty : SnapshotId,
            identity == "session" ? Guid.Empty : SessionId,
            identity == "operation" ? Guid.Empty : OperationId,
            CapturedAt,
            "ProcessState",
            null,
            ExistenceSemantics.Existed);

        Assert.Throws<ArgumentException>(create);
    }
}
