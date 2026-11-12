using BeadWise.Core.Observations;

namespace BeadWise.Core.Diagnostics;

public sealed record SystemInventory(
    Observation<string> WindowsEdition,
    Observation<string> WindowsVersion,
    Observation<string> WindowsBuild,
    Observation<string> Architecture,
    Observation<IReadOnlyList<ProcessorInfo>> Processors,
    Observation<IReadOnlyList<GpuInfo>> Gpus,
    Observation<IReadOnlyList<BaseBoardInfo>> BaseBoards,
    Observation<string> FormFactor,
    Observation<bool> HasBattery,
    Observation<bool> HypervisorPresent,
    Observation<bool> IsElevated);

public sealed record ProcessorInfo(Observation<string> Name,
    Observation<int> PhysicalCores, Observation<int> LogicalProcessors);

public sealed record GpuInfo(Observation<string> Name, Observation<string> DriverVersion);

public sealed record BaseBoardInfo(Observation<string> Manufacturer, Observation<string> Product);
