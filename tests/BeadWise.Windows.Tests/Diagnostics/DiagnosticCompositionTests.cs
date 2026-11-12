using BeadWise.Application.Diagnostics;
using BeadWise.Core.Results;
using BeadWise.Windows.Diagnostics;

namespace BeadWise.Windows.Tests.Diagnostics;

public sealed class DiagnosticCompositionTests
{
    [Fact]
    [Trait("Environment", "RealWindows")]
    public async Task ProducesTypedRealInventoryAndMetadataLogsWithoutUi()
    {
        var root = Path.Combine(Path.GetTempPath(), "beadwise-inventory-" + Guid.NewGuid().ToString("N"));
        try
        {
            var sources = new WindowsDiagnosticSources(root);
            var useCase = new GetDiagnosticInventoryUseCase(sources.System, sources.Processes,
                sources.Audio, sources.Displays, sources.Drivers, sources.Log);
            var result = await useCase.ExecuteAsync();

            Assert.True(result.Status is ResultStatus.Success or ResultStatus.Partial);
            var report = result.Value!;
            Assert.NotNull(report.System.Value);
            Assert.True(report.System.Value.WindowsBuild.HasValue);
            Assert.NotNull(report.Processes.Value);
            Assert.Contains(report.Processes.Value, process => process.ProcessId == Environment.ProcessId);
            Assert.NotNull(report.Audio.Value);
            Assert.NotNull(report.Displays.Value);
            Assert.NotNull(report.Drivers.Value);
            Assert.NotEmpty(report.Drivers.Value);
            var text = File.ReadAllText(Path.Combine(root, "logs", "execution.jsonl"));
            Assert.Contains(report.RunId.ToString(), text);
            Assert.DoesNotContain("ExecutablePath", text);
            Assert.DoesNotContain("DriverVersion", text);
            Assert.DoesNotContain("TechnicalDetails", text);
            Assert.False(Directory.Exists(Path.Combine(root, "sessions")));
        }
        finally { if (Directory.Exists(root)) Directory.Delete(root, true); }
    }
}
