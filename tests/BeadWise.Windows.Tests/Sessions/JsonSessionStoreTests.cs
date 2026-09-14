using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Snapshots;
using BeadWise.Windows.Sessions;

namespace BeadWise.Windows.Tests.Sessions;

public sealed class JsonSessionStoreTests : IDisposable
{
    private static readonly Guid SessionId = Guid.Parse("40f9b37a-f1d4-4486-a049-a08531ae5352");
    private static readonly Guid OtherSessionId = Guid.Parse("8e168b51-eec6-4ffc-89eb-73a086c20928");
    private static readonly Guid PlanId = Guid.Parse("1cb90441-46b1-4937-afd1-4d338e4858e4");
    private static readonly Guid OperationId = Guid.Parse("4503e32c-9a99-4e27-b513-ef245eb0cc7d");
    private static readonly DateTimeOffset CreatedAt = new(2026, 9, 14, 18, 30, 0, TimeSpan.Zero);
    private readonly string _storeRoot = Path.Combine(
        Path.GetTempPath(),
        "BeadWise.Tests",
        Guid.NewGuid().ToString("N"));

    [Fact]
    public void SaveCreatesSessionFileAndLoadRestoresCompleteSession()
    {
        var store = new JsonSessionStore(_storeRoot);
        var original = CreateFailedSession(SessionId);

        var saveResult = store.Save(original);
        var loadResult = store.Load(SessionId);

        Assert.Equal(ResultStatus.Success, saveResult.Status);
        Assert.Same(original, saveResult.Value);
        Assert.True(File.Exists(SessionPath(SessionId)));
        Assert.Equal(ResultStatus.Success, loadResult.Status);
        AssertSessionEquivalent(original, Assert.IsType<Session>(loadResult.Value));
    }

    [Fact]
    public void LoadMissingSessionReturnsStructuredFailure()
    {
        var result = new JsonSessionStore(_storeRoot).Load(SessionId);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Null(result.Value);
        Assert.Equal("SESSION_NOT_FOUND", result.Error?.Code);
        Assert.Equal(ErrorCategory.Unavailable, result.Error?.Category);
    }

    [Fact]
    public void LoadInvalidJsonReturnsStructuredFailure()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(SessionPath(SessionId))!);
        File.WriteAllText(SessionPath(SessionId), "{ invalid json");

        var result = new JsonSessionStore(_storeRoot).Load(SessionId);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Null(result.Value);
        Assert.Equal("SESSION_CONTENT_INVALID", result.Error?.Code);
        Assert.Equal(ErrorCategory.Unavailable, result.Error?.Category);
    }

    [Fact]
    public void LoadIoFailureIsNotReportedAsMissingSession()
    {
        Directory.CreateDirectory(SessionPath(SessionId));

        var result = new JsonSessionStore(_storeRoot).Load(SessionId);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Null(result.Value);
        Assert.Equal("SESSION_IO_ERROR", result.Error?.Code);
        Assert.Equal(ErrorCategory.Unavailable, result.Error?.Category);
    }

    [Fact]
    public void SavingSameSessionAgainReplacesPreviousVersion()
    {
        var store = new JsonSessionStore(_storeRoot);
        var planned = CreateActiveSession(SessionId, SessionState.Planned, CreatedAt);
        var approved = CreateActiveSession(SessionId, SessionState.Approved, CreatedAt.AddMinutes(1));

        _ = store.Save(planned);
        _ = store.Save(approved);
        var result = store.Load(SessionId);

        var loaded = Assert.IsType<Session>(result.Value);
        Assert.Equal(SessionState.Approved, loaded.State);
        Assert.Equal(CreatedAt.AddMinutes(1), loaded.UpdatedAt);
        Assert.Single(Directory.GetFiles(Path.GetDirectoryName(SessionPath(SessionId))!, "*.json"));
    }

    [Fact]
    public void DifferentSessionsRemainIsolated()
    {
        var store = new JsonSessionStore(_storeRoot);
        var first = CreateActiveSession(SessionId, SessionState.Planned, CreatedAt);
        var second = CreateActiveSession(OtherSessionId, SessionState.Approved, CreatedAt.AddMinutes(1));

        _ = store.Save(first);
        _ = store.Save(second);

        Assert.Equal(SessionId, store.Load(SessionId).Value?.SessionId);
        Assert.Equal(SessionState.Planned, store.Load(SessionId).Value?.State);
        Assert.Equal(OtherSessionId, store.Load(OtherSessionId).Value?.SessionId);
        Assert.Equal(SessionState.Approved, store.Load(OtherSessionId).Value?.State);
        Assert.Equal(2, Directory.GetFiles(Path.GetDirectoryName(SessionPath(SessionId))!, "*.json").Length);
    }

    [Fact]
    public void RoundTripDistinguishesMissingStateFromExistingNullState()
    {
        var store = new JsonSessionStore(_storeRoot);
        var original = CreateFailedSession(SessionId);

        _ = store.Save(original);
        var loaded = Assert.IsType<Session>(store.Load(SessionId).Value);

        Assert.Collection(
            loaded.Snapshots,
            missing =>
            {
                Assert.Equal(ExistenceSemantics.DidNotExist, missing.ExistenceSemantics);
                Assert.Null(missing.OriginalState);
            },
            existingNull =>
            {
                Assert.Equal(ExistenceSemantics.Existed, existingNull.ExistenceSemantics);
                Assert.Null(existingNull.OriginalState);
            });
    }

    [Fact]
    public void RoundTripPreservesFailureRecoveryStateResultsAndTimestamps()
    {
        var store = new JsonSessionStore(_storeRoot);
        var original = CreateFailedSession(SessionId);

        _ = store.Save(original);
        var loaded = Assert.IsType<Session>(store.Load(SessionId).Value);

        Assert.Equal(SessionState.Failed, loaded.State);
        Assert.Equal(RecoveryState.Required, loaded.RecoveryState);
        Assert.Equal(CreatedAt, loaded.CreatedAt);
        Assert.Equal(CreatedAt.AddMinutes(2), loaded.UpdatedAt);
        Assert.Equal(original.Failure, loaded.Failure);
        Assert.Equal(
            [
                ResultStatus.Success,
                ResultStatus.Partial,
                ResultStatus.Failed,
                ResultStatus.Blocked,
                ResultStatus.Unsupported,
                ResultStatus.Cancelled
            ],
            loaded.Results.Select(result => result.Status));
        Assert.Equal("Applied", loaded.Results[0].Value);
        Assert.Null(loaded.Results[1].Value);
        Assert.Equal("OPERATION_PARTIAL", loaded.Results[1].Error?.Code);
        Assert.Equal(["Path unavailable."], loaded.Results[1].Warnings);
        Assert.Equal("OPERATION_FAILED", loaded.Results[2].Error?.Code);
        Assert.Equal("OPERATION_BLOCKED", loaded.Results[3].Error?.Code);
        Assert.Equal("OPERATION_UNSUPPORTED", loaded.Results[4].Error?.Code);
        Assert.Equal("OPERATION_CANCELLED", loaded.Results[5].Error?.Code);
    }

    [Fact]
    public void SaveIoFailureReturnsStructuredFailureAndLeavesNoSessionFile()
    {
        Directory.CreateDirectory(_storeRoot);
        var invalidRoot = Path.Combine(_storeRoot, "not-a-directory");
        File.WriteAllText(invalidRoot, "occupied");
        var store = new JsonSessionStore(invalidRoot);

        var result = store.Save(CreateActiveSession(SessionId, SessionState.Planned, CreatedAt));

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Null(result.Value);
        Assert.Equal("SESSION_IO_ERROR", result.Error?.Code);
        Assert.Equal(ErrorCategory.Unavailable, result.Error?.Category);
        Assert.False(File.Exists(Path.Combine(invalidRoot, "sessions", $"{SessionId:D}.json")));
    }

    public void Dispose()
    {
        if (Directory.Exists(_storeRoot))
        {
            Directory.Delete(_storeRoot, recursive: true);
        }
    }

    private string SessionPath(Guid sessionId) =>
        Path.Combine(_storeRoot, "sessions", $"{sessionId:D}.json");

    private static Session CreateFailedSession(Guid sessionId)
    {
        var operation = CreateOperation();
        var partialError = new Error(
            "OPERATION_PARTIAL",
            ErrorCategory.PartialFailure,
            "Executable path was unavailable.",
            recoverable: true);
        var operationFailure = new Error(
            "OPERATION_FAILED",
            ErrorCategory.ApplyFailed,
            "Controlled operation failed.",
            recoverable: true);
        var sessionFailure = new Error(
            "SESSION_FAILED",
            ErrorCategory.ApplyFailed,
            "Session stopped after the controlled operation failed.",
            recoverable: true);

        return new Session(
            sessionId,
            SessionState.Failed,
            CreatedAt,
            CreatedAt.AddMinutes(2),
            PlanId,
            [operation],
            [
                new Snapshot(
                    Guid.Parse("0840e972-608e-4176-a2f0-4d290bdcd045"),
                    sessionId,
                    OperationId,
                    CreatedAt.AddSeconds(10),
                    "ProcessState",
                    null,
                    ExistenceSemantics.DidNotExist),
                new Snapshot(
                    Guid.Parse("9dd0c5e9-1e80-45fb-bd29-163ad770be27"),
                    sessionId,
                    OperationId,
                    CreatedAt.AddSeconds(20),
                    "ProcessState",
                    null,
                    ExistenceSemantics.Existed)
            ],
            [
                Result<string>.Success("Applied"),
                Result<string>.Partial(
                    null,
                    partialError,
                    ["Path unavailable."]),
                Result<string>.Failed(operationFailure),
                Result<string>.Blocked(
                    new Error("OPERATION_BLOCKED", ErrorCategory.Blocked)),
                Result<string>.Unsupported(
                    new Error("OPERATION_UNSUPPORTED", ErrorCategory.Unsupported)),
                Result<string>.Cancelled(
                    new Error("OPERATION_CANCELLED", ErrorCategory.Cancelled))
            ],
            sessionFailure,
            RecoveryState.Required);
    }

    private static Session CreateActiveSession(
        Guid sessionId,
        SessionState state,
        DateTimeOffset updatedAt) =>
        new(
            sessionId,
            state,
            CreatedAt,
            updatedAt,
            PlanId,
            [CreateOperation()],
            [],
            [],
            null,
            RecoveryState.NotRequired);

    private static Operation CreateOperation() =>
        new(
            OperationId,
            "Stop controlled process fixture",
            "Running",
            "NotRunning",
            Risk.Medium,
            PrivilegeRequirement.None,
            RestartRequirement.None,
            Reversibility.Recoverable,
            [new ExecutionResource("Process", "fixture-alpha|pid=42")]);

    private static void AssertSessionEquivalent(Session expected, Session actual)
    {
        Assert.NotSame(expected, actual);
        Assert.Equal(expected.SessionId, actual.SessionId);
        Assert.Equal(expected.State, actual.State);
        Assert.Equal(expected.CreatedAt, actual.CreatedAt);
        Assert.Equal(expected.UpdatedAt, actual.UpdatedAt);
        Assert.Equal(expected.PlanId, actual.PlanId);
        Assert.Equal(expected.Failure, actual.Failure);
        Assert.Equal(expected.RecoveryState, actual.RecoveryState);

        var expectedOperation = Assert.Single(expected.Operations);
        var actualOperation = Assert.Single(actual.Operations);
        Assert.Equal(expectedOperation.OperationId, actualOperation.OperationId);
        Assert.Equal(expectedOperation.Description, actualOperation.Description);
        Assert.Equal(expectedOperation.CurrentState, actualOperation.CurrentState);
        Assert.Equal(expectedOperation.TargetState, actualOperation.TargetState);
        Assert.Equal(expectedOperation.Risk, actualOperation.Risk);
        Assert.Equal(expectedOperation.PrivilegeRequirement, actualOperation.PrivilegeRequirement);
        Assert.Equal(expectedOperation.RestartRequirement, actualOperation.RestartRequirement);
        Assert.Equal(expectedOperation.Reversibility, actualOperation.Reversibility);
        Assert.Equal(expectedOperation.ExecutionResources, actualOperation.ExecutionResources);

        Assert.Equal(expected.Snapshots, actual.Snapshots);
        Assert.Equal(expected.Results.Count, actual.Results.Count);

        for (var index = 0; index < expected.Results.Count; index++)
        {
            var expectedResult = expected.Results[index];
            var actualResult = actual.Results[index];
            Assert.Equal(expectedResult.Status, actualResult.Status);
            Assert.Equal(expectedResult.Value, actualResult.Value);
            Assert.Equal(expectedResult.Error, actualResult.Error);
            Assert.Equal(expectedResult.Warnings, actualResult.Warnings);
        }
    }
}
