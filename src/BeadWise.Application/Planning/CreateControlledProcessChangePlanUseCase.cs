using System.Globalization;
using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Planning;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;

namespace BeadWise.Application.Planning;

public sealed class CreateControlledProcessChangePlanUseCase
{
    private const string InvalidTargetErrorCode = "INVALID_CONTROLLED_PROCESS_TARGET";

    public Result<ChangePlan> Execute(
        ProcessIdentity targetIdentity,
        string? controlledFixtureKey)
    {
        if (targetIdentity.ProcessId <= 0 ||
            targetIdentity.CreationTime == default ||
            string.IsNullOrWhiteSpace(controlledFixtureKey))
        {
            return Result<ChangePlan>.Failed(
                new Error(
                    InvalidTargetErrorCode,
                    ErrorCategory.InvalidPlan,
                    "A controlled target requires a positive process ID, creation time, and fixture key."));
        }

        var operation = new Operation(
            Guid.NewGuid(),
            "Stop controlled process fixture",
            "Running",
            "NotRunning",
            Risk.Medium,
            PrivilegeRequirement.None,
            RestartRequirement.None,
            Reversibility.Recoverable,
            [new ExecutionResource("Process", BuildResourceKey(targetIdentity, controlledFixtureKey))]);

        var plan = new ChangePlan(
            Guid.NewGuid(),
            ["C-PROCESSES-001", "C-PROCESSES-007"],
            [operation],
            [
                "The target has a stable process identity.",
                "The target is owned by the controlled test fixture.",
                "The observed target state is Running."
            ],
            Risk.Medium,
            [
                "Only a controlled test fixture may be targeted.",
                "Stopping the process loses its in-memory state."
            ],
            [PrivilegeRequirement.None],
            [RestartRequirement.None],
            "Recovery may create a new controlled fixture process, not restore the original process instance.");

        return Result<ChangePlan>.Success(plan);
    }

    private static string BuildResourceKey(
        ProcessIdentity targetIdentity,
        string controlledFixtureKey) =>
        string.Create(
            CultureInfo.InvariantCulture,
            $"{controlledFixtureKey}|pid={targetIdentity.ProcessId}|created={targetIdentity.CreationTime.ToUniversalTime():O}");
}
