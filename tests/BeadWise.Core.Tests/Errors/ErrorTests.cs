using BeadWise.Core.Errors;

namespace BeadWise.Core.Tests.Errors;

public sealed class ErrorTests
{
    [Fact]
    public void PreservesStructuredErrorFields()
    {
        var error = new Error(
            "ACCESS_DENIED",
            ErrorCategory.AccessDenied,
            "The process rejected the requested access.",
            recoverable: true);

        Assert.Equal("ACCESS_DENIED", error.Code);
        Assert.Equal(ErrorCategory.AccessDenied, error.Category);
        Assert.Equal("The process rejected the requested access.", error.TechnicalDetails);
        Assert.True(error.Recoverable);
    }

    [Fact]
    public void RejectsBlankCode()
    {
        Assert.Throws<ArgumentException>(() => new Error(" ", ErrorCategory.Unavailable));
    }

    [Fact]
    public void ContainsEveryCategoryRequiredByTheControlledSlice()
    {
        var expected = new[]
        {
            ErrorCategory.AdminRequired,
            ErrorCategory.AccessDenied,
            ErrorCategory.Unsupported,
            ErrorCategory.Unavailable,
            ErrorCategory.Blocked,
            ErrorCategory.Timeout,
            ErrorCategory.Cancelled,
            ErrorCategory.ApplyFailed,
            ErrorCategory.VerifyFailed,
            ErrorCategory.RecoveryFailed,
            ErrorCategory.PartialFailure,
            ErrorCategory.TargetChanged,
            ErrorCategory.InvalidPlan
        };

        Assert.Equal(expected, Enum.GetValues<ErrorCategory>());
    }
}
