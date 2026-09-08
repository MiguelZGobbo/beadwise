param(
    [string]$OutputPath = (Join-Path (Split-Path -Parent $PSScriptRoot) 'results\domain-evidence.json'),
    [switch]$Fast
)

$ErrorActionPreference = 'Stop'

function Invoke-EvidenceProbe {
    param(
        [string]$Id,
        [string[]]$FeatureIds,
        [scriptblock]$Operation
    )

    $startedAt = Get-Date
    try {
        $data = & $Operation
        [ordered]@{
            id = $Id
            featureIds = $FeatureIds
            status = 'PASS'
            durationMs = [int]((Get-Date) - $startedAt).TotalMilliseconds
            data = $data
            error = $null
        }
    }
    catch [System.PlatformNotSupportedException] {
        [ordered]@{
            id = $Id
            featureIds = $FeatureIds
            status = 'UNSUPPORTED'
            durationMs = [int]((Get-Date) - $startedAt).TotalMilliseconds
            data = $null
            error = $_.Exception.Message
        }
    }
    catch {
        [ordered]@{
            id = $Id
            featureIds = $FeatureIds
            status = 'FAIL'
            durationMs = [int]((Get-Date) - $startedAt).TotalMilliseconds
            data = $null
            error = $_.Exception.Message
        }
    }
}

function New-DomainEvidence {
    param([object[]]$Probes)
    [ordered]@{ probes = @($Probes) }
}

$domains = [ordered]@{}

$domains.audio = New-DomainEvidence @(
    Invoke-EvidenceProbe 'audio.devices.cim' @('C-AUDIO-001', 'C-AUDIO-003', 'C-AUDIO-006') {
        @(Get-CimInstance Win32_SoundDevice | Select-Object Name, Manufacturer, Status, StatusInfo)
    }
    Invoke-EvidenceProbe 'audio.services' @('C-AUDIO-003', 'C-AUDIO-009', 'C-AUDIO-013') {
        @(Get-Service AudioSrv, AudioEndpointBuilder | Select-Object Name, Status, StartType)
    }
)

$domains.cleaning = New-DomainEvidence @(
    Invoke-EvidenceProbe 'cleaning.rebuildable-roots' @('C-CLEANING-001', 'C-CLEANING-002', 'C-CLEANING-006', 'C-CLEANING-009') {
        $roots = @($env:TEMP, "$env:WINDIR\Temp") | Select-Object -Unique
        @($roots | ForEach-Object {
            [ordered]@{ path = $_; exists = Test-Path -LiteralPath $_ }
        })
    }
)

$domains.cpu = New-DomainEvidence @(
    Invoke-EvidenceProbe 'cpu.identity.cim' @('C-CPU-001', 'C-CPU-013') {
        @(Get-CimInstance Win32_Processor | Select-Object Name, Manufacturer, Architecture, NumberOfCores, NumberOfLogicalProcessors, MaxClockSpeed, VirtualizationFirmwareEnabled)
    }
    Invoke-EvidenceProbe 'cpu.utilization.sample' @('C-CPU-003', 'C-CPU-015') {
        @(Get-CimInstance Win32_PerfFormattedData_PerfOS_Processor -Filter "Name='_Total'" | Select-Object PercentProcessorTime, PercentUserTime, PercentPrivilegedTime)
    }
)

$domains.display = New-DomainEvidence @(
    Invoke-EvidenceProbe 'display.inventory.cim' @('C-DISPLAY-001', 'C-DISPLAY-002', 'C-DISPLAY-005', 'C-DISPLAY-007', 'C-DISPLAY-013') {
        [ordered]@{
            controllers = @(Get-CimInstance Win32_VideoController | Select-Object Name, CurrentHorizontalResolution, CurrentVerticalResolution, CurrentRefreshRate, DriverVersion)
            monitors = @(Get-CimInstance Win32_DesktopMonitor | Select-Object Name, Status, ScreenWidth, ScreenHeight)
        }
    }
)

$domains.drivers = New-DomainEvidence @(
    Invoke-EvidenceProbe 'drivers.problem-devices.pnp' @('C-DRIVERS-001', 'C-DRIVERS-002', 'C-DRIVERS-007') {
        $devices = @(Get-PnpDevice -PresentOnly -ErrorAction Stop)
        [ordered]@{
            presentCount = $devices.Count
            problemDevices = @($devices | Where-Object Status -ne 'OK' | Select-Object Class, FriendlyName, Status, Problem)
        }
    }
)

$domains.energy = New-DomainEvidence @(
    Invoke-EvidenceProbe 'energy.active-scheme' @('C-ENERGY-001', 'C-ENERGY-003', 'C-ENERGY-007') {
        [ordered]@{ activeScheme = (& powercfg.exe /getactivescheme | Out-String).Trim(); exitCode = $LASTEXITCODE }
    }
    Invoke-EvidenceProbe 'energy.available-sleep-states' @('C-ENERGY-005', 'C-ENERGY-011', 'C-ENERGY-012') {
        [ordered]@{ output = (& powercfg.exe /a | Out-String).Trim(); exitCode = $LASTEXITCODE }
    }
)

$domains.gaming = New-DomainEvidence @(
    Invoke-EvidenceProbe 'gaming.running-process-context' @('C-GAMING-001', 'C-GAMING-006', 'C-GAMING-012') {
        [ordered]@{
            foregroundProcessAvailable = (Get-Process | Where-Object MainWindowHandle -ne 0 | Measure-Object).Count -gt 0
            processCount = (Get-Process | Measure-Object).Count
        }
    }
)

$domains.gpu = New-DomainEvidence @(
    Invoke-EvidenceProbe 'gpu.identity-and-memory.cim' @('C-GPU-001', 'C-GPU-002', 'C-GPU-003', 'C-GPU-015') {
        @(Get-CimInstance Win32_VideoController | Select-Object Name, AdapterRAM, DriverVersion, VideoModeDescription, CurrentRefreshRate)
    }
)

$domains.hardware = New-DomainEvidence @(
    Invoke-EvidenceProbe 'hardware.inventory.cim' @('C-HARDWARE-001', 'C-HARDWARE-004', 'C-HARDWARE-009') {
        [ordered]@{
            system = @(Get-CimInstance Win32_ComputerSystem | Select-Object Manufacturer, Model, PCSystemType, HypervisorPresent)
            baseboard = @(Get-CimInstance Win32_BaseBoard | Select-Object Manufacturer, Product, Version)
            bios = @(Get-CimInstance Win32_BIOS | Select-Object Manufacturer, SMBIOSBIOSVersion, ReleaseDate)
        }
    }
)

$domains.network = New-DomainEvidence @(
    Invoke-EvidenceProbe 'network.adapters-and-path' @('C-NETWORK-001', 'C-NETWORK-005', 'C-NETWORK-006', 'C-NETWORK-007', 'C-NETWORK-011', 'C-NETWORK-016') {
        @(Get-NetAdapter -IncludeHidden | Select-Object Name, InterfaceDescription, Status, LinkSpeed, ifIndex, Virtual)
    }
    Invoke-EvidenceProbe 'network.loopback-reachability' @('C-NETWORK-002', 'C-NETWORK-003', 'C-NETWORK-008') {
        [ordered]@{ success = Test-Connection -TargetName 127.0.0.1 -Count 2 -Quiet }
    }
    Invoke-EvidenceProbe 'network.dns-localhost' @('C-NETWORK-004') {
        @(Resolve-DnsName localhost -ErrorAction Stop | Select-Object Name, Type, IPAddress)
    }
)

$domains.privacy = New-DomainEvidence @(
    Invoke-EvidenceProbe 'privacy.managed-policy-presence' @('C-PRIVACY-001', 'C-PRIVACY-002', 'C-PRIVACY-003', 'C-PRIVACY-004', 'C-PRIVACY-005') {
        $paths = @(
            'HKLM:\SOFTWARE\Policies\Microsoft\Windows\DataCollection',
            'HKLM:\SOFTWARE\Policies\Microsoft\Windows\AppPrivacy',
            'HKLM:\SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors'
        )
        @($paths | ForEach-Object { [ordered]@{ path = $_; exists = Test-Path -LiteralPath $_ } })
    }
)

$domains.processes = New-DomainEvidence @(
    Invoke-EvidenceProbe 'processes.inventory' @('C-PROCESSES-001', 'C-PROCESSES-002', 'C-PROCESSES-004', 'C-PROCESSES-006') {
        $processes = @(Get-Process)
        [ordered]@{
            count = $processes.Count
            sampledProcessesWithCpuData = @($processes | Where-Object { $null -ne $_.CPU }).Count
        }
    }
)

$domains.ram = New-DomainEvidence @(
    Invoke-EvidenceProbe 'ram.inventory-and-pressure' @('C-RAM-001', 'C-RAM-004', 'C-RAM-005', 'C-RAM-010', 'C-RAM-012', 'C-RAM-014') {
        [ordered]@{
            modules = @(Get-CimInstance Win32_PhysicalMemory | Select-Object Manufacturer, Capacity, Speed, ConfiguredClockSpeed, DeviceLocator, PartNumber)
            operatingSystem = @(Get-CimInstance Win32_OperatingSystem | Select-Object TotalVisibleMemorySize, FreePhysicalMemory, TotalVirtualMemorySize, FreeVirtualMemory)
            pageFiles = @(Get-CimInstance Win32_PageFileUsage | Select-Object Name, AllocatedBaseSize, CurrentUsage, PeakUsage)
        }
    }
)

$domains.repair = New-DomainEvidence @(
    Invoke-EvidenceProbe 'repair.component-store-check' @('C-REPAIR-001', 'C-REPAIR-002', 'C-REPAIR-010') {
        if ($Fast) { return [ordered]@{ skippedInFastMode = $true } }
        $output = & dism.exe /Online /Cleanup-Image /CheckHealth 2>&1 | Out-String
        if ($LASTEXITCODE -ne 0) { throw "DISM exit code $LASTEXITCODE`: $output" }
        [ordered]@{ exitCode = $LASTEXITCODE; output = $output.Trim() }
    }
)

$domains.security = New-DomainEvidence @(
    Invoke-EvidenceProbe 'security.defender-status' @('C-SECURITY-001', 'C-SECURITY-002', 'C-SECURITY-007') {
        Get-MpComputerStatus | Select-Object AMServiceEnabled, AntivirusEnabled, AntispywareEnabled, BehaviorMonitorEnabled, RealTimeProtectionEnabled, AntivirusSignatureVersion, AntivirusSignatureLastUpdated
    }
    Invoke-EvidenceProbe 'security.firewall-profiles' @('C-SECURITY-001', 'C-SECURITY-002') {
        @(Get-NetFirewallProfile | Select-Object Name, Enabled, DefaultInboundAction, DefaultOutboundAction)
    }
)

$domains.startup = New-DomainEvidence @(
    Invoke-EvidenceProbe 'startup.inventory.cim' @('C-STARTUP-001', 'C-STARTUP-003', 'C-STARTUP-006') {
        $entries = @(Get-CimInstance Win32_StartupCommand)
        [ordered]@{ count = $entries.Count; locations = @($entries.Location | Sort-Object -Unique) }
    }
)

$domains.storage = New-DomainEvidence @(
    Invoke-EvidenceProbe 'storage.inventory-and-health' @('C-STORAGE-001', 'C-STORAGE-002', 'C-STORAGE-004', 'C-STORAGE-008', 'C-STORAGE-014', 'C-STORAGE-019') {
        [ordered]@{
            physicalDisks = @(Get-PhysicalDisk | Select-Object FriendlyName, MediaType, BusType, HealthStatus, OperationalStatus, Size)
            disks = @(Get-Disk | Select-Object Number, FriendlyName, BusType, PartitionStyle, OperationalStatus, HealthStatus, Size)
            volumes = @(Get-Volume | Select-Object DriveLetter, FileSystemType, HealthStatus, OperationalStatus, Size, SizeRemaining)
        }
    }
)

$domains.system = New-DomainEvidence @(
    Invoke-EvidenceProbe 'system.identity.cim' @('C-SYSTEM-001', 'C-SYSTEM-015', 'C-SYSTEM-017') {
        $os = Get-CimInstance Win32_OperatingSystem
        [ordered]@{ caption = $os.Caption; version = $os.Version; build = $os.BuildNumber; architecture = $os.OSArchitecture }
    }
    Invoke-EvidenceProbe 'system.pending-reboot-signals' @('C-SYSTEM-014', 'C-UPDATE-001', 'C-REPAIR-004') {
        [ordered]@{
            componentBasedServicing = Test-Path 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Component Based Servicing\RebootPending'
            windowsUpdate = Test-Path 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update\RebootRequired'
            pendingFileRenameOperations = $null -ne (Get-ItemProperty -Path 'HKLM:\SYSTEM\CurrentControlSet\Control\Session Manager' -Name PendingFileRenameOperations -ErrorAction SilentlyContinue)
        }
    }
)

$domains.update = New-DomainEvidence @(
    Invoke-EvidenceProbe 'update.services-and-history' @('C-UPDATE-001', 'C-UPDATE-002', 'C-UPDATE-003', 'C-UPDATE-009') {
        [ordered]@{
            services = @(Get-Service wuauserv, bits, usosvc | Select-Object Name, Status, StartType)
            hotfixCount = @(Get-HotFix).Count
            newestHotfix = @(Get-HotFix | Sort-Object InstalledOn -Descending | Select-Object -First 1 HotFixID, InstalledOn, Description)
        }
    }
)

$domains.usb = New-DomainEvidence @(
    Invoke-EvidenceProbe 'usb.present-devices.pnp' @('C-USB-001', 'C-USB-002', 'C-USB-003', 'C-USB-005', 'C-USB-006', 'C-USB-007', 'C-USB-009', 'C-USB-010', 'C-USB-011', 'C-USB-015') {
        $devices = @(Get-PnpDevice -PresentOnly | Where-Object { $_.InstanceId -like 'USB*' })
        [ordered]@{
            count = $devices.Count
            byClass = @($devices | Group-Object Class | Sort-Object Name | Select-Object Name, Count)
            problems = @($devices | Where-Object Status -ne 'OK' | Select-Object Class, FriendlyName, Status, Problem)
        }
    }
)

$bundle = [ordered]@{
    schemaVersion = 1
    capturedAt = (Get-Date).ToString('o')
    mode = if ($Fast) { 'fast' } else { 'full' }
    mutationPolicy = 'read-only'
    domains = $domains
}

$outputDirectory = Split-Path -Parent $OutputPath
if ($outputDirectory) {
    New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
}

$json = $bundle | ConvertTo-Json -Depth 12
[IO.File]::WriteAllText($OutputPath, $json, [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
