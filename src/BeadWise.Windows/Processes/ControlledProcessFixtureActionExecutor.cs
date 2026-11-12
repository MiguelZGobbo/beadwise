using System.ComponentModel;
using System.Globalization;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Operations;
using BeadWise.Core.Planning;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;
using BeadWise.Core.Sessions;
using BeadWise.Core.Snapshots;

namespace BeadWise.Windows.Processes;

internal sealed class ControlledProcessFixtureActionExecutor : IControlledProcessActionExecutor
{
    private static readonly string[] ExpectedFeatureIds =
        ["C-PROCESSES-001", "C-PROCESSES-007"];
    private readonly Guid _executorInstanceId = Guid.NewGuid();
    private readonly ControlledProcessTarget _target;
    private readonly Guid _fixtureKey;
    private readonly ChangePlan _approvedPlan;
    private readonly TimeProvider _timeProvider;

    public ControlledProcessFixtureActionExecutor(
        ControlledProcessTarget target,
        Guid fixtureKey,
        ChangePlan approvedPlan,
        TimeProvider? timeProvider = null)
    {
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(approvedPlan);

        _target = target;
        _fixtureKey = fixtureKey;
        _approvedPlan = approvedPlan;
        _timeProvider = timeProvider ?? TimeProvider.System;
    }

    public Result<IControlledProcessActionPreparation> Prepare(
        ChangePlan plan,
        Session approvedSession)
    {
        var requestError = ValidateRequest(plan, approvedSession, SessionState.Approved);
        if (requestError is not null)
        {
            return Result<IControlledProcessActionPreparation>.Blocked(requestError);
        }

        var targetError = RevalidateTarget("TARGET_NOT_RUNNING");
        if (targetError is not null)
        {
            return TargetFailure<IControlledProcessActionPreparation>(targetError);
        }

        var operation = plan.Operations[0];
        var resource = operation.ExecutionResources[0];
        var snapshot = new Snapshot(
            Guid.NewGuid(),
            approvedSession.SessionId,
            operation.OperationId,
            _timeProvider.GetUtcNow(),
            "ControlledProcessState",
            $"Running|resource={resource.ResourceType}:{resource.ResourceKey}|action={operation.OperationId:D}",
            ExistenceSemantics.Existed);
        IControlledProcessActionPreparation preparation = new Preparation(
            _executorInstanceId,
            plan.PlanId,
            operation.OperationId,
            snapshot);

        return Result<IControlledProcessActionPreparation>.Success(preparation);
    }

    public Result<Operation> Execute(
        ChangePlan plan,
        Session applyingSession,
        IControlledProcessActionPreparation preparation)
    {
        if (preparation is not Preparation ownedPreparation ||
            ownedPreparation.ExecutorInstanceId != _executorInstanceId)
        {
            return Result<Operation>.Blocked(
                new Error(
                    "PREPARATION_OWNER_MISMATCH",
                    ErrorCategory.Blocked,
                    "The preparation was not issued by this executor instance."));
        }

        if (!ownedPreparation.TryConsume())
        {
            return Result<Operation>.Blocked(
                new Error(
                    "PREPARATION_ALREADY_USED",
                    ErrorCategory.Blocked,
                    "The preparation is invalid, disposed, or already consumed."));
        }

        var requestError = ValidateRequest(plan, applyingSession, SessionState.Applying);
        if (requestError is not null ||
            ownedPreparation.PlanId != plan.PlanId ||
            ownedPreparation.OperationId != plan.Operations[0].OperationId ||
            ownedPreparation.Snapshot.SessionId != applyingSession.SessionId ||
            !ReferenceEquals(ownedPreparation.Snapshot, preparation.Snapshot) ||
            !applyingSession.Snapshots.Contains(ownedPreparation.Snapshot))
        {
            return Result<Operation>.Blocked(
                requestError ?? new Error(
                    "INVALID_APPLY_PREPARATION",
                    ErrorCategory.InvalidPlan,
                    "The preparation does not match this apply execution."));
        }

        var targetError = RevalidateTarget("TARGET_EXITED_DURING_APPLY");
        if (targetError is not null)
        {
            return TargetFailure<Operation>(targetError);
        }

        try
        {
            _target.Kill();
            return Result<Operation>.Success(plan.Operations[0]);
        }
        catch (UnauthorizedAccessException exception)
        {
            return AccessDenied(exception);
        }
        catch (Win32Exception exception) when (exception.NativeErrorCode == 5)
        {
            return AccessDenied(exception);
        }
        catch (InvalidOperationException exception)
        {
            return Result<Operation>.Failed(
                new Error(
                    "TARGET_EXITED_DURING_APPLY",
                    ErrorCategory.Unavailable,
                    exception.GetType().FullName,
                    recoverable: true));
        }
        catch (Exception exception) when (
            exception is Win32Exception or NotSupportedException or ObjectDisposedException)
        {
            return Result<Operation>.Failed(
                new Error(
                    "APPLY_FAILED",
                    ErrorCategory.ApplyFailed,
                    exception.GetType().FullName,
                    recoverable: true));
        }
    }

    private Error? ValidateRequest(
        ChangePlan plan,
        Session session,
        SessionState expectedSessionState)
    {
        if (!_approvedPlan.HasSameLogicalContentAs(plan) ||
            session.PlanId != plan.PlanId ||
            session.State != expectedSessionState ||
            plan.Operations.Count != 1 ||
            session.Operations.Count != 1 ||
            !OperationsMatch(session.Operations[0], plan.Operations[0]))
        {
            return new Error(
                "INVALID_APPLY_PLAN",
                ErrorCategory.InvalidPlan,
                "The supplied plan or session differs from the executor's approved plan.");
        }

        var operation = plan.Operations[0];
        if (!plan.FeatureIds.SequenceEqual(ExpectedFeatureIds) ||
            operation.Description != "Stop controlled process fixture" ||
            operation.CurrentState != "Running" ||
            operation.TargetState != "NotRunning" ||
            operation.Risk != Risk.Medium ||
            operation.PrivilegeRequirement != PrivilegeRequirement.None ||
            operation.RestartRequirement != RestartRequirement.None ||
            operation.Reversibility != Reversibility.Recoverable ||
            operation.ExecutionResources.Count != 1)
        {
            return new Error(
                "CONTROLLED_OPERATION_NOT_AUTHORIZED",
                ErrorCategory.Blocked,
                "Only the controlled fixture stop operation is authorized.");
        }

        var resource = operation.ExecutionResources[0];
        if (resource.ResourceType != "Process" ||
            resource.ResourceKey != BuildExpectedResourceKey())
        {
            return new Error(
                "FIXTURE_OWNERSHIP_MISMATCH",
                ErrorCategory.Blocked,
                "The execution resource does not belong to the controlled fixture.");
        }

        return null;
    }

    private Error? RevalidateTarget(string exitedErrorCode)
    {
        try
        {
            if (!_target.IsOwnedLaunchContext())
            {
                return new Error(
                    "FIXTURE_OWNERSHIP_MISMATCH",
                    ErrorCategory.Blocked,
                    "The target is not the process instance launched by the controlled harness.");
            }

            if (!_target.IsRunning())
            {
                return new Error(
                    exitedErrorCode,
                    ErrorCategory.Unavailable,
                    "The controlled target is no longer running.",
                    recoverable: true);
            }

            if (_target.ReadIdentity() != _target.ExpectedIdentity)
            {
                return new Error(
                    "TARGET_CHANGED",
                    ErrorCategory.TargetChanged,
                    "The process identity changed after planning.");
            }

            return null;
        }
        catch (Exception exception) when (
            exception is InvalidOperationException or ObjectDisposedException)
        {
            return new Error(
                exitedErrorCode,
                ErrorCategory.Unavailable,
                exception.GetType().FullName,
                recoverable: true);
        }
        catch (UnauthorizedAccessException exception)
        {
            return new Error(
                "APPLY_ACCESS_DENIED",
                ErrorCategory.AccessDenied,
                exception.GetType().FullName);
        }
        catch (Win32Exception exception)
        {
            return new Error(
                exception.NativeErrorCode == 5 ? "APPLY_ACCESS_DENIED" : "APPLY_FAILED",
                exception.NativeErrorCode == 5 ? ErrorCategory.AccessDenied : ErrorCategory.ApplyFailed,
                exception.GetType().FullName,
                recoverable: exception.NativeErrorCode != 5);
        }
    }

    private string BuildExpectedResourceKey() =>
        string.Create(
            CultureInfo.InvariantCulture,
            $"{_fixtureKey:D}|pid={_target.ExpectedIdentity.ProcessId}|created={_target.ExpectedIdentity.CreationTime.ToUniversalTime():O}");

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

    private static Result<T> TargetFailure<T>(Error error) =>
        error.Category == ErrorCategory.TargetChanged || error.Category == ErrorCategory.Blocked
            ? Result<T>.Blocked(error)
            : Result<T>.Failed(error);

    private static Result<Operation> AccessDenied(Exception exception) =>
        Result<Operation>.Failed(
            new Error(
                "APPLY_ACCESS_DENIED",
                ErrorCategory.AccessDenied,
                exception.GetType().FullName));

    private sealed class Preparation(
        Guid executorInstanceId,
        Guid planId,
        Guid operationId,
        Snapshot snapshot) : IControlledProcessActionPreparation
    {
        private int _state;

        public Guid ExecutorInstanceId { get; } = executorInstanceId;

        public Guid PreparationId { get; } = Guid.NewGuid();

        public Guid PlanId { get; } = planId;

        public Guid OperationId { get; } = operationId;

        public Snapshot Snapshot { get; } = snapshot;

        public bool TryConsume() =>
            Interlocked.CompareExchange(ref _state, 1, 0) == 0;

        public void Dispose() =>
            Interlocked.CompareExchange(ref _state, 2, 0);
    }
}
