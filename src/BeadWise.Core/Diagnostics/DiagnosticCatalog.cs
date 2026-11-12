namespace BeadWise.Core.Diagnostics;

public sealed record FeatureSliceDescriptor(string FeatureId, string Behavior, string Limitations,
    bool ControlledTestOnly = false);

/// <summary>Implementation coverage, not approval of a complete feature or universal hardware support.</summary>
public static class DiagnosticCatalog
{
    public static IReadOnlyList<FeatureSliceDescriptor> Slices { get; } = Array.AsReadOnly(new[]
    {
        new FeatureSliceDescriptor("C-SYSTEM-001", "System identity and hardware context",
            "No chipset inference, service/task diagnostics, or universal compatibility claim."),
        new FeatureSliceDescriptor("C-PROCESSES-001", "PID, creation time, name and available executable path",
            "No tree, app grouping, metrics or completeness guarantee for protected processes."),
        new FeatureSliceDescriptor("C-PROCESSES-007", "Owned test fixture apply, verify and recovery",
            "No actions on user processes; replacement does not restore original memory.", true),
        new FeatureSliceDescriptor("C-AUDIO-001", "Endpoint inventory, direction and default roles",
            "No capture stream, configuration changes or exact endpoint-parent mapping."),
        new FeatureSliceDescriptor("C-AUDIO-003", "Observed endpoint state masks",
            "Not a complete device-health diagnosis; absence does not prove driver failure."),
        new FeatureSliceDescriptor("C-DISPLAY-001", "Available display and adapter identity",
            "No full CCD topology, EDID, HDR, VRR or hotplug validation."),
        new FeatureSliceDescriptor("C-DISPLAY-002", "Current and enumerated supported display modes",
            "No mode testing, application or rollback."),
        new FeatureSliceDescriptor("C-DRIVERS-001", "Present devices and available signed-driver metadata",
            "No install, remove, disable, restart, export or recommendation by version."),
        new FeatureSliceDescriptor("C-DRIVERS-002", "Observed device status and problem codes",
            "No causal diagnosis or repair; real faulty-device coverage depends on test hardware.")
    });
}
