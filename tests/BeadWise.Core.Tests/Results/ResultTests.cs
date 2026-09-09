using BeadWise.Core.Errors;
using BeadWise.Core.Results;

namespace BeadWise.Core.Tests.Results;

public sealed class ResultTests
{
    private static readonly Error Failure = new(
        "TEST_FAILURE",
        ErrorCategory.ApplyFailed,
        "Failure created by a unit test.",
        recoverable: true);

    [Fact]
    public void SuccessCarriesValueWithoutFatalError()
    {
        var result = Result<string>.Success("value");

        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.Equal("value", result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void PartialRemainsDistinctFromSuccess()
    {
        var result = Result<string>.Partial("partial value", Failure, ["some data was unavailable"]);

        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.NotEqual(ResultStatus.Success, result.Status);
        Assert.Equal(Failure, result.Error);
        Assert.Equal(["some data was unavailable"], result.Warnings);
    }

    [Fact]
    public void FailedCarriesStructuredError()
    {
        var result = Result<string>.Failed(Failure);

        Assert.Equal(ResultStatus.Failed, result.Status);
        Assert.Equal(Failure, result.Error);
        Assert.Null(result.Value);
    }

    [Theory]
    [InlineData(ResultStatus.Blocked)]
    [InlineData(ResultStatus.Unsupported)]
    [InlineData(ResultStatus.Cancelled)]
    public void NonSuccessTerminalStatusCarriesStructuredError(ResultStatus status)
    {
        var error = status switch
        {
            ResultStatus.Blocked => new Error("BLOCKED", ErrorCategory.Blocked),
            ResultStatus.Unsupported => new Error("UNSUPPORTED", ErrorCategory.Unsupported),
            ResultStatus.Cancelled => new Error("CANCELLED", ErrorCategory.Cancelled),
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };
        var result = status switch
        {
            ResultStatus.Blocked => Result<string>.Blocked(error),
            ResultStatus.Unsupported => Result<string>.Unsupported(error),
            ResultStatus.Cancelled => Result<string>.Cancelled(error),
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };

        Assert.Equal(status, result.Status);
        Assert.Equal(error, result.Error);
        Assert.Null(result.Value);
    }
}
