using BeadWise.Core.Contracts;
using BeadWise.Core.Logging;
using BeadWise.Windows.Logging;
using BeadWise.Windows.Processes;

namespace BeadWise.Windows.Diagnostics;

/// <summary>Native source composition reusable by any host; constructing it performs no collection.</summary>
public sealed class WindowsDiagnosticSources
{
    public WindowsDiagnosticSources(string storageRoot)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storageRoot);
        Log = new JsonExecutionLog(Path.Combine(Path.GetFullPath(storageRoot), "logs"));
    }

    public ISystemInventorySource System { get; } = new WindowsSystemInventorySource();
    public IProcessObservationSource Processes { get; } = new WindowsProcessObservationSource();
    public IAudioInventorySource Audio { get; } = new WindowsAudioInventorySource();
    public IDisplayInventorySource Displays { get; } = new WindowsDisplayInventorySource();
    public IDriverInventorySource Drivers { get; } = new WindowsDriverInventorySource();
    public IExecutionLog Log { get; }
}
