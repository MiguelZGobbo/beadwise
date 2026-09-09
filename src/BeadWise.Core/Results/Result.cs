using BeadWise.Core.Errors;

namespace BeadWise.Core.Results;

public sealed class Result<T>
{
    private Result(
        ResultStatus status,
        T? value,
        Error? error,
        IEnumerable<string>? warnings)
    {
        Status = status;
        Value = value;
        Error = error;
        Warnings = Array.AsReadOnly(warnings?.ToArray() ?? []);
    }

    public ResultStatus Status { get; }

    public T? Value { get; }

    public Error? Error { get; }

    public IReadOnlyList<string> Warnings { get; }

    public static Result<T> Success(T value, IEnumerable<string>? warnings = null)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new Result<T>(ResultStatus.Success, value, null, warnings);
    }

    public static Result<T> Partial(
        T? value,
        Error? error = null,
        IEnumerable<string>? warnings = null) =>
        new(ResultStatus.Partial, value, error, warnings);

    public static Result<T> Failed(Error error) =>
        FromError(ResultStatus.Failed, error);

    public static Result<T> Blocked(Error error) =>
        FromError(ResultStatus.Blocked, error);

    public static Result<T> Unsupported(Error error) =>
        FromError(ResultStatus.Unsupported, error);

    public static Result<T> Cancelled(Error error) =>
        FromError(ResultStatus.Cancelled, error);

    private static Result<T> FromError(ResultStatus status, Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new Result<T>(status, default, error, null);
    }
}
