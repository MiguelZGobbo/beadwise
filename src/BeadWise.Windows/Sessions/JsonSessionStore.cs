using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;

namespace BeadWise.Windows.Sessions;

public sealed class JsonSessionStore : ISessionStore
{
    private const string NotFoundErrorCode = "SESSION_NOT_FOUND";
    private const string InvalidContentErrorCode = "SESSION_CONTENT_INVALID";
    private const string IoErrorCode = "SESSION_IO_ERROR";
    private static readonly JsonSerializerOptions SerializerOptions = CreateSerializerOptions();
    private readonly string _sessionsDirectory;

    public JsonSessionStore(string storeRoot)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storeRoot);
        _sessionsDirectory = Path.Combine(Path.GetFullPath(storeRoot), "sessions");
    }

    public Result<Session> Save(Session session)
    {
        ArgumentNullException.ThrowIfNull(session);

        string? temporaryPath = null;

        try
        {
            Directory.CreateDirectory(_sessionsDirectory);
            var document = SessionDocumentMapper.ToDocument(session);
            var json = JsonSerializer.Serialize(document, SerializerOptions);
            var destinationPath = GetSessionPath(session.SessionId);
            temporaryPath = Path.Combine(
                _sessionsDirectory,
                $".{session.SessionId:N}.{Guid.NewGuid():N}.tmp");

            File.WriteAllText(temporaryPath, json, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            File.Move(temporaryPath, destinationPath, overwrite: true);
            temporaryPath = null;

            return Result<Session>.Success(session);
        }
        catch (IOException exception)
        {
            return IoFailure(exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            return IoFailure(exception);
        }
        finally
        {
            TryDeleteTemporaryFile(temporaryPath);
        }
    }

    public Result<Session> Load(Guid sessionId)
    {
        var sessionPath = GetSessionPath(sessionId);

        try
        {
            var json = File.ReadAllText(sessionPath, Encoding.UTF8);
            var document = JsonSerializer.Deserialize<SessionDocument>(json, SerializerOptions)
                ?? throw new JsonException("The session document is empty.");
            var session = SessionDocumentMapper.FromDocument(document);

            return Result<Session>.Success(session);
        }
        catch (JsonException exception)
        {
            return InvalidContentFailure(exception);
        }
        catch (ArgumentException exception)
        {
            return InvalidContentFailure(exception);
        }
        catch (InvalidOperationException exception)
        {
            return InvalidContentFailure(exception);
        }
        catch (FileNotFoundException)
        {
            return NotFoundFailure(sessionId);
        }
        catch (DirectoryNotFoundException)
        {
            return NotFoundFailure(sessionId);
        }
        catch (IOException exception)
        {
            return IoFailure(exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            return IoFailure(exception);
        }
    }

    private string GetSessionPath(Guid sessionId) =>
        Path.Combine(_sessionsDirectory, $"{sessionId:D}.json");

    private static Result<Session> NotFoundFailure(Guid sessionId) =>
        Result<Session>.Failed(
            new Error(
                NotFoundErrorCode,
                ErrorCategory.Unavailable,
                $"Session {sessionId:D} was not found."));

    private static Result<Session> InvalidContentFailure(Exception exception) =>
        Result<Session>.Failed(
            new Error(
                InvalidContentErrorCode,
                ErrorCategory.Unavailable,
                exception.Message));

    private static Result<Session> IoFailure(Exception exception) =>
        Result<Session>.Failed(
            new Error(
                IoErrorCode,
                ErrorCategory.Unavailable,
                exception.Message,
                recoverable: true));

    private static void TryDeleteTemporaryFile(string? temporaryPath)
    {
        if (temporaryPath is null)
        {
            return;
        }

        try
        {
            File.Delete(temporaryPath);
        }
        catch (IOException)
        {
            // Preserve the structured I/O failure that triggered cleanup.
        }
        catch (UnauthorizedAccessException)
        {
            // Preserve the structured I/O failure that triggered cleanup.
        }
    }

    private static JsonSerializerOptions CreateSerializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        options.Converters.Add(
            new JsonStringEnumConverter(namingPolicy: null, allowIntegerValues: false));
        return options;
    }
}
