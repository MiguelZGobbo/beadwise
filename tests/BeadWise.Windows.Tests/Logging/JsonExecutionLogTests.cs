using BeadWise.Core.Logging;
using BeadWise.Windows.Logging;

namespace BeadWise.Windows.Tests.Logging;

public sealed class JsonExecutionLogTests
{
    [Fact]
    public void RejectsFreeTextInMetadataAndDoesNotThrowOnUnavailableStorage()
    {
        var root = Path.Combine(Path.GetTempPath(), "beadwise-log-test-" + Guid.NewGuid().ToString("N"));
        try
        {
            var log = new JsonExecutionLog(root);
            log.Write(new ExecutionLogEntry(DateTimeOffset.UtcNow, ExecutionEvent.Failure,
                FeatureId: "C:\\Users\\private\\process.exe", ErrorCode: "device USB\\personal-id"));
            var text = File.ReadAllText(Path.Combine(root, "execution.jsonl"));
            Assert.DoesNotContain("private", text);
            Assert.DoesNotContain("personal-id", text);
            var blockedRoot = Path.Combine(root, "file");
            File.WriteAllText(blockedRoot, "occupied");
            new JsonExecutionLog(blockedRoot).Write(new ExecutionLogEntry(DateTimeOffset.UtcNow, ExecutionEvent.Failure));
        }
        finally { if (Directory.Exists(root)) Directory.Delete(root, true); }
    }

    [Fact]
    public void RotatesWithinThreeFilesAndStoresOnlyStructuredMetadata()
    {
        var root = Path.Combine(Path.GetTempPath(), "beadwise-log-test-" + Guid.NewGuid().ToString("N"));
        try
        {
            var log = new JsonExecutionLog(root, maximumFileBytes: 512);
            for (var i = 0; i < 30; i++)
                log.Write(new ExecutionLogEntry(DateTimeOffset.UtcNow, ExecutionEvent.OperationStarted,
                    Guid.NewGuid(), Guid.NewGuid(), "C-PROCESSES-007", Guid.NewGuid()));
            var files = Directory.GetFiles(root, "*.jsonl");
            Assert.Equal(3, files.Length);
            Assert.All(files, file => Assert.InRange(new FileInfo(file).Length, 1, 512));
            Assert.Contains("OperationStarted", File.ReadAllText(Path.Combine(root, "execution.jsonl")));
        }
        finally { if (Directory.Exists(root)) Directory.Delete(root, true); }
    }
}
