using BeadWise.Core.Contracts;
using BeadWise.Core.Diagnostics;
using BeadWise.Core.Errors;
using BeadWise.Core.Logging;
using BeadWise.Core.Results;

namespace BeadWise.Application.Diagnostics;

public sealed class GetDiagnosticInventoryUseCase
{
    private readonly ISystemInventorySource _system;
    private readonly IProcessObservationSource _processes;
    private readonly IAudioInventorySource _audio;
    private readonly IDisplayInventorySource _displays;
    private readonly IDriverInventorySource _drivers;
    private readonly IExecutionLog _log;

    public GetDiagnosticInventoryUseCase(ISystemInventorySource system, IProcessObservationSource processes,
        IAudioInventorySource audio, IDisplayInventorySource displays, IDriverInventorySource drivers,
        IExecutionLog? log = null)
    {
        _system = system ?? throw new ArgumentNullException(nameof(system));
        _processes = processes ?? throw new ArgumentNullException(nameof(processes));
        _audio = audio ?? throw new ArgumentNullException(nameof(audio));
        _displays = displays ?? throw new ArgumentNullException(nameof(displays));
        _drivers = drivers ?? throw new ArgumentNullException(nameof(drivers));
        _log = log ?? NullExecutionLog.Instance;
    }

    public async Task<Result<DiagnosticInventoryReport>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var id = Guid.NewGuid();
        var started = DateTimeOffset.UtcNow;
        _log.Write(new ExecutionLogEntry(started, ExecutionEvent.DiagnosticStarted, DiagnosticRunId: id));
        try
        {
            var system = await Collect(_system.Observe, "C-SYSTEM-001", id, cancellationToken).ConfigureAwait(false);
            var processes = await Collect(_processes.Observe, "C-PROCESSES-001", id, cancellationToken).ConfigureAwait(false);
            var audio = await Collect(_audio.Observe, "C-AUDIO-001", id, cancellationToken).ConfigureAwait(false);
            var displays = await Collect(_displays.Observe, "C-DISPLAY-001", id, cancellationToken).ConfigureAwait(false);
            var drivers = await Collect(_drivers.Observe, "C-DRIVERS-001", id, cancellationToken).ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            var report = new DiagnosticInventoryReport(id, started, DateTimeOffset.UtcNow,
                system, processes, audio, displays, drivers);
            var complete = new[] { system.Status, processes.Status, audio.Status, displays.Status, drivers.Status }
                .All(status => status is ResultStatus.Success);
            var error = complete ? null : new Error("DIAGNOSTIC_INVENTORY_PARTIAL", ErrorCategory.PartialFailure);
            _log.Write(new ExecutionLogEntry(report.CompletedAt, ExecutionEvent.DiagnosticCompleted,
                DiagnosticRunId: id, ErrorCode: error?.Code, ErrorCategory: error?.Category));
            return complete ? Result<DiagnosticInventoryReport>.Success(report)
                : Result<DiagnosticInventoryReport>.Partial(report, error);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            var error = new Error("DIAGNOSTIC_CANCELLED", ErrorCategory.Cancelled);
            _log.Write(new ExecutionLogEntry(DateTimeOffset.UtcNow, ExecutionEvent.Cancelled,
                DiagnosticRunId: id, ErrorCode: error.Code, ErrorCategory: error.Category));
            return Result<DiagnosticInventoryReport>.Cancelled(error);
        }
    }

    private async Task<Result<T>> Collect<T>(Func<Result<T>> observe, string feature,
        Guid runId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _log.Write(new ExecutionLogEntry(DateTimeOffset.UtcNow, ExecutionEvent.ObservationStarted,
            FeatureId: feature, DiagnosticRunId: runId));
        Result<T> result;
        try
        {
            // Wait for each read to finish: cancellation never leaves abandoned native collectors.
            result = await Task.Run(observe, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { throw; }
        catch (Exception exception) when (exception is not OutOfMemoryException)
        {
            var category = exception is UnauthorizedAccessException ? ErrorCategory.AccessDenied
                : exception is PlatformNotSupportedException ? ErrorCategory.Unsupported : ErrorCategory.Unavailable;
            var error = new Error("DIAGNOSTIC_COLLECTOR_FAILED", category);
            result = category is ErrorCategory.Unsupported ? Result<T>.Unsupported(error) : Result<T>.Failed(error);
        }
        _log.Write(new ExecutionLogEntry(DateTimeOffset.UtcNow, ExecutionEvent.ObservationCompleted,
            FeatureId: feature, DiagnosticRunId: runId, ErrorCode: result.Error?.Code,
            ErrorCategory: result.Error?.Category));
        return result;
    }
}
