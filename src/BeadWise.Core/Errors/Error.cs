namespace BeadWise.Core.Errors;

public sealed record Error
{
    public Error(
        string code,
        ErrorCategory category,
        string? technicalDetails = null,
        bool recoverable = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        Code = code;
        Category = category;
        TechnicalDetails = technicalDetails;
        Recoverable = recoverable;
    }

    public string Code { get; }

    public ErrorCategory Category { get; }

    public string? TechnicalDetails { get; }

    public bool Recoverable { get; }
}
