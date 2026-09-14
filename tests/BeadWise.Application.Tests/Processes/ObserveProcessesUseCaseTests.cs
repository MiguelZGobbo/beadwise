using BeadWise.Application.Processes;
using BeadWise.Core.Contracts;
using BeadWise.Core.Errors;
using BeadWise.Core.Observations;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;

namespace BeadWise.Application.Tests.Processes;

public sealed class ObserveProcessesUseCaseTests
{
    private static readonly DateTimeOffset Timestamp = new(2026, 9, 14, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void ExecutePreservesSuccessfulResultAndProcesses()
    {
        IReadOnlyList<ProcessObservation> processes = [CreateProcess(42)];
        var expected = Result<IReadOnlyList<ProcessObservation>>.Success(processes);
        var source = new FakeProcessObservationSource(expected);
        var useCase = new ObserveProcessesUseCase(source);

        var actual = useCase.Execute();

        Assert.Same(expected, actual);
        Assert.Same(processes, actual.Value);
        Assert.Equal(ResultStatus.Success, actual.Status);
    }

    [Fact]
    public void ExecutePreservesPartialResultErrorAndWarnings()
    {
        IReadOnlyList<ProcessObservation> processes = [CreateProcess(42)];
        var error = new Error(
            "PROCESS_OBSERVATION_PARTIAL",
            ErrorCategory.PartialFailure,
            recoverable: true);
        var expected = Result<IReadOnlyList<ProcessObservation>>.Partial(
            processes,
            error,
            ["Executable path unavailable."]);
        var source = new FakeProcessObservationSource(expected);
        var useCase = new ObserveProcessesUseCase(source);

        var actual = useCase.Execute();

        Assert.Same(expected, actual);
        Assert.Equal(ResultStatus.Partial, actual.Status);
        Assert.Same(processes, actual.Value);
        Assert.Same(error, actual.Error);
        Assert.Equal(["Executable path unavailable."], actual.Warnings);
    }

    [Theory]
    [InlineData(ResultStatus.Failed)]
    [InlineData(ResultStatus.Blocked)]
    [InlineData(ResultStatus.Unsupported)]
    [InlineData(ResultStatus.Cancelled)]
    public void ExecutePreservesTerminalResult(ResultStatus status)
    {
        var error = new Error("PROCESS_OBSERVATION_STOPPED", CategoryFor(status));
        var expected = TerminalResult(status, error);
        var source = new FakeProcessObservationSource(expected);
        var useCase = new ObserveProcessesUseCase(source);

        var actual = useCase.Execute();

        Assert.Same(expected, actual);
        Assert.Equal(status, actual.Status);
        Assert.Same(error, actual.Error);
        Assert.Null(actual.Value);
    }

    [Fact]
    public void ExecuteAcceptsEmptySuccessfulCollection()
    {
        IReadOnlyList<ProcessObservation> processes = Array.Empty<ProcessObservation>();
        var expected = Result<IReadOnlyList<ProcessObservation>>.Success(processes);
        var source = new FakeProcessObservationSource(expected);
        var useCase = new ObserveProcessesUseCase(source);

        var actual = useCase.Execute();

        Assert.Same(expected, actual);
        Assert.Empty(actual.Value!);
    }

    [Fact]
    public void ExecuteCallsSourceOnce()
    {
        IReadOnlyList<ProcessObservation> processes = [CreateProcess(42)];
        var source = new FakeProcessObservationSource(
            Result<IReadOnlyList<ProcessObservation>>.Success(processes));
        var useCase = new ObserveProcessesUseCase(source);

        _ = useCase.Execute();

        Assert.Equal(1, source.CallCount);
    }

    [Fact]
    public void ConstructorRejectsMissingSource()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => new ObserveProcessesUseCase(null!));

        Assert.Equal("source", exception.ParamName);
    }

    private static ProcessObservation CreateProcess(int processId)
    {
        const string source = "test-source";
        var identity = Observation<ProcessIdentity>.Available(
            new ProcessIdentity(processId, Timestamp),
            source,
            Timestamp);

        return new ProcessObservation(
            processId,
            identity,
            Observation<string>.Available("testhost", source, Timestamp),
            Observation<string>.Available("C:\\testhost.exe", source, Timestamp));
    }

    private static ErrorCategory CategoryFor(ResultStatus status) => status switch
    {
        ResultStatus.Failed => ErrorCategory.Unavailable,
        ResultStatus.Blocked => ErrorCategory.Blocked,
        ResultStatus.Unsupported => ErrorCategory.Unsupported,
        ResultStatus.Cancelled => ErrorCategory.Cancelled,
        _ => throw new ArgumentOutOfRangeException(nameof(status))
    };

    private static Result<IReadOnlyList<ProcessObservation>> TerminalResult(
        ResultStatus status,
        Error error) => status switch
        {
            ResultStatus.Failed => Result<IReadOnlyList<ProcessObservation>>.Failed(error),
            ResultStatus.Blocked => Result<IReadOnlyList<ProcessObservation>>.Blocked(error),
            ResultStatus.Unsupported => Result<IReadOnlyList<ProcessObservation>>.Unsupported(error),
            ResultStatus.Cancelled => Result<IReadOnlyList<ProcessObservation>>.Cancelled(error),
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };

    private sealed class FakeProcessObservationSource(
        Result<IReadOnlyList<ProcessObservation>> result) : IProcessObservationSource
    {
        public int CallCount { get; private set; }

        public Result<IReadOnlyList<ProcessObservation>> Observe()
        {
            CallCount++;
            return result;
        }
    }
}
