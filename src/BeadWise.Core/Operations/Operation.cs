namespace BeadWise.Core.Operations;

public sealed class Operation
{
    public Operation(
        Guid operationId,
        string description,
        string currentState,
        string targetState,
        Risk risk,
        PrivilegeRequirement privilegeRequirement,
        RestartRequirement restartRequirement,
        Reversibility reversibility,
        IEnumerable<ExecutionResource> executionResources)
    {
        if (operationId == Guid.Empty)
        {
            throw new ArgumentException("An operation identity cannot be empty.", nameof(operationId));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentNullException.ThrowIfNull(currentState);
        ArgumentNullException.ThrowIfNull(targetState);
        ArgumentNullException.ThrowIfNull(executionResources);

        OperationId = operationId;
        Description = description;
        CurrentState = currentState;
        TargetState = targetState;
        Risk = risk;
        PrivilegeRequirement = privilegeRequirement;
        RestartRequirement = restartRequirement;
        Reversibility = reversibility;
        ExecutionResources = Array.AsReadOnly(executionResources.ToArray());
    }

    public Guid OperationId { get; }

    public string Description { get; }

    public string CurrentState { get; }

    public string TargetState { get; }

    public Risk Risk { get; }

    public PrivilegeRequirement PrivilegeRequirement { get; }

    public RestartRequirement RestartRequirement { get; }

    public Reversibility Reversibility { get; }

    public IReadOnlyList<ExecutionResource> ExecutionResources { get; }
}
