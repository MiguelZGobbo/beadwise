using BeadWise.Application.Diagnostics;
using BeadWise.Core.Contracts;
using BeadWise.Core.Diagnostics;
using BeadWise.Core.Errors;
using BeadWise.Core.Processes;
using BeadWise.Core.Results;

namespace BeadWise.Application.Tests.Diagnostics;

public sealed class GetDiagnosticInventoryUseCaseTests
{
    [Fact]
    public async Task CollectsSequentiallyAndRetainsSuccessWhenOneCollectorThrows()
    {
        var source = new Sources();
        var result = await source.UseCase().ExecuteAsync();
        Assert.Equal(["system", "processes", "audio", "displays", "drivers"], source.Calls);
        Assert.Equal(ResultStatus.Partial, result.Status);
        Assert.Equal(ResultStatus.Failed, result.Value!.Audio.Status);
        Assert.Equal(ResultStatus.Success, result.Value.Displays.Status);
        Assert.Empty(result.Value.Displays.Value!);
        Assert.DoesNotContain("private path", result.Value.Audio.Error!.TechnicalDetails ?? "");
    }

    [Fact]
    public async Task CancellationBetweenCollectorsStopsFurtherWork()
    {
        using var cancellation = new CancellationTokenSource();
        var source = new Sources { AfterSystem = cancellation.Cancel };
        var result = await source.UseCase().ExecuteAsync(cancellation.Token);
        Assert.Equal(ResultStatus.Cancelled, result.Status);
        Assert.Equal(["system"], source.Calls);
    }

    [Fact]
    public async Task AlreadyCancelledDoesNotCollect()
    {
        var source = new Sources();
        var result = await source.UseCase().ExecuteAsync(new CancellationToken(true));
        Assert.Equal(ResultStatus.Cancelled, result.Status);
        Assert.Empty(source.Calls);
    }

    [Fact]
    public async Task SuccessfulEmptyCollectionsAreNotInventedFailures()
    {
        var source = new Sources { AllAvailable = true };
        var result = await source.UseCase().ExecuteAsync();
        Assert.Equal(ResultStatus.Success, result.Status);
        Assert.NotEqual(Guid.Empty, result.Value!.RunId);
        Assert.True(result.Value.CompletedAt >= result.Value.StartedAt);
        Assert.Empty(result.Value.Audio.Value!);
    }

    private sealed class Sources : ISystemInventorySource, IProcessObservationSource,
        IAudioInventorySource, IDisplayInventorySource, IDriverInventorySource
    {
        public List<string> Calls { get; } = [];
        public Action? AfterSystem { get; init; }
        public bool AllAvailable { get; init; }
        public GetDiagnosticInventoryUseCase UseCase() => new(this, this, this, this, this);
        Result<SystemInventory> ISystemInventorySource.Observe()
        {
            Calls.Add("system");
            AfterSystem?.Invoke();
            if (AllAvailable)
            {
                var now = DateTimeOffset.UtcNow;
                BeadWise.Core.Observations.Observation<T> Known<T>(T value) =>
                    BeadWise.Core.Observations.Observation<T>.Available(value, "test", now);
                return Result<SystemInventory>.Success(new SystemInventory(Known("Windows"), Known("10"),
                    Known("26200"), Known("X64"), Known<IReadOnlyList<ProcessorInfo>>([]),
                    Known<IReadOnlyList<GpuInfo>>([]), Known<IReadOnlyList<BaseBoardInfo>>([]),
                    Known("Desktop"), Known(false), Known(false), Known(false)));
            }
            return Result<SystemInventory>.Unsupported(new Error("SYSTEM_UNSUPPORTED", ErrorCategory.Unsupported));
        }
        Result<IReadOnlyList<ProcessObservation>> IProcessObservationSource.Observe()
        {
            Calls.Add("processes");
            return Result<IReadOnlyList<ProcessObservation>>.Success([]);
        }
        Result<IReadOnlyList<AudioEndpointObservation>> IAudioInventorySource.Observe()
        {
            Calls.Add("audio");
            if (AllAvailable) return Result<IReadOnlyList<AudioEndpointObservation>>.Success([]);
            throw new InvalidOperationException("private path");
        }
        Result<IReadOnlyList<DisplayObservation>> IDisplayInventorySource.Observe()
        {
            Calls.Add("displays");
            return Result<IReadOnlyList<DisplayObservation>>.Success([]);
        }
        Result<IReadOnlyList<DriverObservation>> IDriverInventorySource.Observe()
        {
            Calls.Add("drivers");
            return Result<IReadOnlyList<DriverObservation>>.Success([]);
        }
    }
}
