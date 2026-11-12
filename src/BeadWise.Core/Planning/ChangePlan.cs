using BeadWise.Core.Operations;

namespace BeadWise.Core.Planning;

public sealed class ChangePlan
{
    public ChangePlan(
        Guid planId,
        IEnumerable<string> featureIds,
        IEnumerable<Operation> operations,
        IEnumerable<string> preconditions,
        Risk risk,
        IEnumerable<string> warnings,
        IEnumerable<PrivilegeRequirement> privilegeRequirements,
        IEnumerable<RestartRequirement> restartRequirements,
        string? recoveryInformation = null)
    {
        if (planId == Guid.Empty)
        {
            throw new ArgumentException("A plan identity cannot be empty.", nameof(planId));
        }

        ArgumentNullException.ThrowIfNull(featureIds);
        ArgumentNullException.ThrowIfNull(operations);
        ArgumentNullException.ThrowIfNull(preconditions);
        ArgumentNullException.ThrowIfNull(warnings);
        ArgumentNullException.ThrowIfNull(privilegeRequirements);
        ArgumentNullException.ThrowIfNull(restartRequirements);

        PlanId = planId;
        FeatureIds = Array.AsReadOnly(featureIds.ToArray());
        Operations = Array.AsReadOnly(operations.ToArray());
        Preconditions = Array.AsReadOnly(preconditions.ToArray());
        Risk = risk;
        Warnings = Array.AsReadOnly(warnings.ToArray());
        PrivilegeRequirements = Array.AsReadOnly(privilegeRequirements.ToArray());
        RestartRequirements = Array.AsReadOnly(restartRequirements.ToArray());
        RecoveryInformation = recoveryInformation;
    }

    public Guid PlanId { get; }

    public IReadOnlyList<string> FeatureIds { get; }

    public IReadOnlyList<Operation> Operations { get; }

    public IReadOnlyList<string> Preconditions { get; }

    public Risk Risk { get; }

    public IReadOnlyList<string> Warnings { get; }

    public IReadOnlyList<PrivilegeRequirement> PrivilegeRequirements { get; }

    public IReadOnlyList<RestartRequirement> RestartRequirements { get; }

    public string? RecoveryInformation { get; }

    public bool HasSameLogicalContentAs(ChangePlan? other) =>
        other is not null &&
        PlanId == other.PlanId &&
        FeatureIds.SequenceEqual(other.FeatureIds) &&
        Operations.Count == other.Operations.Count &&
        Operations.Zip(other.Operations).All(pair => OperationsMatch(pair.First, pair.Second)) &&
        Preconditions.SequenceEqual(other.Preconditions) &&
        Risk == other.Risk &&
        Warnings.SequenceEqual(other.Warnings) &&
        PrivilegeRequirements.SequenceEqual(other.PrivilegeRequirements) &&
        RestartRequirements.SequenceEqual(other.RestartRequirements) &&
        RecoveryInformation == other.RecoveryInformation;

    private static bool OperationsMatch(Operation left, Operation right) =>
        left.OperationId == right.OperationId &&
        left.Description == right.Description &&
        left.CurrentState == right.CurrentState &&
        left.TargetState == right.TargetState &&
        left.Risk == right.Risk &&
        left.PrivilegeRequirement == right.PrivilegeRequirement &&
        left.RestartRequirement == right.RestartRequirement &&
        left.Reversibility == right.Reversibility &&
        left.ExecutionResources.SequenceEqual(right.ExecutionResources);
}
