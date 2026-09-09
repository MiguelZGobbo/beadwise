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
}
