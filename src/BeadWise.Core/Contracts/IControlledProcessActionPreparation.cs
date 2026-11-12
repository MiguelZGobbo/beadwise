using BeadWise.Core.Snapshots;

namespace BeadWise.Core.Contracts;

public interface IControlledProcessActionPreparation : IDisposable
{
    Guid PreparationId { get; }

    Guid PlanId { get; }

    Guid OperationId { get; }

    Snapshot Snapshot { get; }
}
