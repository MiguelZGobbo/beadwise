using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using BeadWise.Core.Logging;

namespace BeadWise.Windows.Logging;

public sealed class JsonExecutionLog : IExecutionLog
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new JsonStringEnumConverter() }
    };
    private static readonly object Gate = new();
    private readonly string _root;
    private readonly long _maximumFileBytes;

    public JsonExecutionLog(string root, long maximumFileBytes = 10 * 1024 * 1024)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(root);
        ArgumentOutOfRangeException.ThrowIfLessThan(maximumFileBytes, 512);
        _root = Path.GetFullPath(root);
        _maximumFileBytes = maximumFileBytes;
    }

    public void Write(ExecutionLogEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);
        var safeEntry = entry with
        {
            FeatureId = IsSafeFeature(entry.FeatureId) ? entry.FeatureId : null,
            ErrorCode = IsSafeCode(entry.ErrorCode) ? entry.ErrorCode : null
        };
        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(safeEntry, Options) + "\n");
        if (bytes.Length > _maximumFileBytes) return;
        lock (Gate)
        {
            try
            {
                Directory.CreateDirectory(_root);
                var current = Path.Combine(_root, "execution.jsonl");
                if (File.Exists(current) && new FileInfo(current).Length + bytes.Length > _maximumFileBytes)
                {
                    var first = Path.Combine(_root, "execution.1.jsonl");
                    var second = Path.Combine(_root, "execution.2.jsonl");
                    if (File.Exists(first)) File.Move(first, second, overwrite: true);
                    File.Move(current, first, overwrite: true);
                }
                using var stream = new FileStream(current, FileMode.Append, FileAccess.Write, FileShare.Read);
                stream.Write(bytes);
            }
            // Logging is diagnostic, never the authority for an operation's durable outcome.
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }
    }

    private static bool IsSafeFeature(string? value) => value is not null &&
        value.Length <= 48 && Regex.IsMatch(value, "\\AC-[A-Z]+-[0-9]{3}\\z");

    private static bool IsSafeCode(string? value) => value is not null &&
        value.Length <= 64 && Regex.IsMatch(value, "\\A[A-Z][A-Z0-9_]*\\z");
}
