using System.ComponentModel;
using System.Globalization;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Planning;
using BeadWise.Core.Results;

namespace BeadWise.Windows.Processes;

internal sealed class ControlledProcessFixtureStateSource : IControlledProcessStateSource
{
    private readonly ControlledProcessTarget _target;
    private readonly Guid _fixtureKey;
    private readonly ChangePlan _approvedPlan;
    private readonly TimeProvider _timeProvider;

    public ControlledProcessFixtureStateSource(
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

    public Result<Observation<bool>> Observe(ChangePlan plan)
    {
        var error = ValidatePlan(plan);
        if (error is not null)
        {
            return Result<Observation<bool>>.Blocked(error);
        }

        try
        {
            if (!_target.IsOwnedLaunchContext())
            {
                return Result<Observation<bool>>.Blocked(new Error(
                    "FIXTURE_OWNERSHIP_MISMATCH", ErrorCategory.Blocked,
                    "The target is not the fixture instance launched by the harness."));
            }

            var running = _target.IsRunning();
            // An exited process may no longer expose its creation time. Its retained
            // launch context and captured identity still bind this read to that instance.
            if (running && _target.ReadIdentity() != _target.ExpectedIdentity)
            {
                return Result<Observation<bool>>.Blocked(new Error(
                    "TARGET_CHANGED", ErrorCategory.TargetChanged,
                    "The observed process identity differs from the approved target."));
            }

            return Result<Observation<bool>>.Success(Observation<bool>.Available(
                running, WindowsProcessObservationSource.ObservationSource, _timeProvider.GetUtcNow()));
        }
        catch (Exception exception) when (exception is Win32Exception or UnauthorizedAccessException
            or InvalidOperationException or ObjectDisposedException or NotSupportedException)
        {
            return ReadFailure(exception);
        }
    }

    private Error? ValidatePlan(ChangePlan? plan)
    {
        if (!_approvedPlan.HasSameLogicalContentAs(plan) || plan!.Operations.Count != 1 ||
            plan.Operations[0].Description != "Stop controlled process fixture" ||
            plan.Operations[0].CurrentState != "Running" || plan.Operations[0].TargetState != "NotRunning")
        {
            return new Error("INVALID_VERIFY_PLAN", ErrorCategory.InvalidPlan,
                "Verification requires the approved controlled process stop plan.");
        }

        var resources = plan.Operations[0].ExecutionResources;
        var expectedResourceKey = string.Create(CultureInfo.InvariantCulture,
            $"{_fixtureKey:D}|pid={_target.ExpectedIdentity.ProcessId}|created={_target.ExpectedIdentity.CreationTime.ToUniversalTime():O}");
        if (resources.Count != 1 || resources[0].ResourceType != "Process" || resources[0].ResourceKey != expectedResourceKey)
        {
            return new Error("FIXTURE_OWNERSHIP_MISMATCH", ErrorCategory.Blocked,
                "The approved operation does not refer to the captured fixture identity.");
        }

        return null;
    }

    private static Result<Observation<bool>> ReadFailure(Exception exception)
    {
        var (code, category) = exception switch
        {
            UnauthorizedAccessException or Win32Exception { NativeErrorCode: 5 } =>
                ("VERIFY_ACCESS_DENIED", ErrorCategory.AccessDenied),
            NotSupportedException => ("VERIFY_UNSUPPORTED", ErrorCategory.Unsupported),
            _ => ("VERIFY_TARGET_UNAVAILABLE", ErrorCategory.Unavailable)
        };
        var error = new Error(code, category, exception.GetType().FullName,
            recoverable: category is ErrorCategory.Unavailable);
        return category is ErrorCategory.Unsupported
            ? Result<Observation<bool>>.Unsupported(error)
            : Result<Observation<bool>>.Failed(error);
    }
}
