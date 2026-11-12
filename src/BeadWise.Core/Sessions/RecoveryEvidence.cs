using BeadWise.Core.Errors;
using BeadWise.Core.Processes;

namespace BeadWise.Core.Sessions;

public sealed record RecoveryEvidence
{
    public RecoveryEvidence(
        RecoveryReason reason,
        ProcessIdentity originalIdentity,
        ProcessIdentity? recoveredIdentity,
        Error? originalError,
        Error? recoveryError)
    {
        if (!Enum.IsDefined(reason)) throw new ArgumentOutOfRangeException(nameof(reason));
        ValidateIdentity(originalIdentity, nameof(originalIdentity));
        if (recoveredIdentity is { } identity) ValidateIdentity(identity, nameof(recoveredIdentity));
        if (reason is RecoveryReason.FailureRecovery && originalError is null)
            throw new ArgumentNullException(nameof(originalError));
        if (reason is RecoveryReason.ExplicitFixtureReset && originalError is not null)
            throw new ArgumentException("An explicit reset has no originating failure.", nameof(originalError));

        Reason = reason;
        OriginalIdentity = originalIdentity;
        RecoveredIdentity = recoveredIdentity;
        OriginalError = originalError;
        RecoveryError = recoveryError;
    }

    public RecoveryReason Reason { get; }
    public ProcessIdentity OriginalIdentity { get; }
    public ProcessIdentity? RecoveredIdentity { get; }
    public Error? OriginalError { get; }
    public Error? RecoveryError { get; }

    private static void ValidateIdentity(ProcessIdentity identity, string name)
    {
        if (identity.ProcessId <= 0 || identity.CreationTime == default)
            throw new ArgumentException("A process identity requires a positive PID and creation time.", name);
    }
}
