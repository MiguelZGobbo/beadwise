namespace BeadWise.Core.Errors;

public enum ErrorCategory
{
    AdminRequired,
    AccessDenied,
    Unsupported,
    Unavailable,
    Blocked,
    Timeout,
    Cancelled,
    ApplyFailed,
    VerifyFailed,
    RecoveryFailed,
    PartialFailure,
    TargetChanged,
    InvalidPlan
}
