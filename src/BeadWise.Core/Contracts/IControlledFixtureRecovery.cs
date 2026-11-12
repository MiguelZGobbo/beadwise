using BeadWise.Core.Planning;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;

namespace BeadWise.Core.Contracts;

/// <summary>
/// A live harness-owned context. Implementations retain authority in memory and must
/// serialize attempts, honour cancellation, and never reconstruct authority from persisted data.
/// </summary>
public interface IControlledFixtureRecovery
{
    ProcessIdentity OriginalIdentity { get; }

    Task<Result<ProcessIdentity>> RestoreAsync(ChangePlan plan, CancellationToken cancellationToken);

    Task<Result<bool>> VerifyAsync(ChangePlan plan, ProcessIdentity identity, CancellationToken cancellationToken);
}
