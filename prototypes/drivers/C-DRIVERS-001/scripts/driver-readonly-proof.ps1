param(
    [Parameter(Mandatory)]
    [string]$OutputPath
)

$ErrorActionPreference = 'Stop'

function Get-TextSha256 {
    param([string[]]$Lines)
    $bytes = [Text.Encoding]::UTF8.GetBytes(($Lines -join "`n"))
    [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($bytes))
}

$artifactRoot = Split-Path -Parent $OutputPath
New-Item -ItemType Directory -Path $artifactRoot -Force | Out-Null

$signedDrivers = @(Get-CimInstance Win32_PnPSignedDriver | Sort-Object DeviceID, InfName)
$presentDevices = @(Get-PnpDevice -PresentOnly | Sort-Object InstanceId)

$driverRecords = @($signedDrivers | ForEach-Object {
    [pscustomobject]@{
        deviceId = $_.DeviceID
        deviceName = $_.DeviceName
        deviceClass = $_.DeviceClass
        infName = $_.InfName
        provider = $_.DriverProviderName
        version = $_.DriverVersion
        dateUtc = if ($_.DriverDate) { ([datetime]$_.DriverDate).ToUniversalTime().ToString('o') } else { $null }
        signed = $_.IsSigned
        signer = $_.Signer
        manufacturer = $_.Manufacturer
        source = 'Win32_PnPSignedDriver'
    }
})

$problemDevices = @($presentDevices | Where-Object { $_.Problem -ne 'CM_PROB_NONE' })
$problemSummary = @($problemDevices | Group-Object Class, Problem | ForEach-Object {
    [pscustomobject]@{
        classAndProblem = $_.Name
        count = $_.Count
    }
})

$criticalClasses = @('Computer', 'System', 'Processor', 'Display', 'Net', 'HDC', 'SCSIAdapter')
$criticalDevices = @($presentDevices | Where-Object { $criticalClasses -contains $_.Class })
$criticalClassCounts = @($criticalDevices | Group-Object Class | Sort-Object Name | ForEach-Object {
    [pscustomobject]@{ class = $_.Name; count = $_.Count; action = 'BLOCK' }
})

$pnpUtilOutput = @(& "$env:SystemRoot\System32\pnputil.exe" /enum-drivers 2>&1 | ForEach-Object { $_.ToString() })
$pnpUtilExitCode = $LASTEXITCODE

$setupApiPath = Join-Path $env:SystemRoot 'INF\setupapi.dev.log'
$setupApiExists = Test-Path -LiteralPath $setupApiPath
$setupApiInfo = if ($setupApiExists) { Get-Item -LiteralPath $setupApiPath } else { $null }
$setupApiHash = if ($setupApiExists) { (Get-FileHash -LiteralPath $setupApiPath -Algorithm SHA256).Hash } else { $null }

$providerNames = @('Microsoft-Windows-Kernel-PnP', 'Microsoft-Windows-UserPnp', 'Microsoft-Windows-DriverFrameworks-UserMode')
$eventStatus = 'PASS'
$eventError = $null
$driverEvents = @()
try {
    $driverEvents = @(Get-WinEvent -FilterHashtable @{ LogName = 'System'; StartTime = (Get-Date).AddDays(-30) } -ErrorAction Stop |
        Where-Object { $providerNames -contains $_.ProviderName } |
        Select-Object -First 30 |
        ForEach-Object {
            [pscustomobject]@{
                timeCreatedUtc = if ($_.TimeCreated) { $_.TimeCreated.ToUniversalTime().ToString('o') } else { $null }
                provider = $_.ProviderName
                eventId = $_.Id
                level = $_.LevelDisplayName
                recordId = $_.RecordId
            }
        })
}
catch {
    $eventStatus = 'PARTIAL'
    $eventError = $_.Exception.Message
}

$metadataCompleteCount = @($driverRecords | Where-Object {
    -not [string]::IsNullOrWhiteSpace($_.infName) -and
    -not [string]::IsNullOrWhiteSpace($_.provider) -and
    -not [string]::IsNullOrWhiteSpace($_.version)
}).Count
$driverInventoryHash = Get-TextSha256 -Lines @(($driverRecords | ConvertTo-Json -Depth 5 -Compress))
$providerCounts = @($driverRecords | Group-Object provider | Sort-Object Name | ForEach-Object {
    [pscustomobject]@{ provider = if ([string]::IsNullOrWhiteSpace($_.Name)) { '<UNAVAILABLE>' } else { $_.Name }; count = $_.Count }
})
$classCounts = @($driverRecords | Group-Object deviceClass | Sort-Object Name | ForEach-Object {
    [pscustomobject]@{ class = if ([string]::IsNullOrWhiteSpace($_.Name)) { '<UNAVAILABLE>' } else { $_.Name }; count = $_.Count }
})

$administrator = ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole(
    [Security.Principal.WindowsBuiltInRole]::Administrator)
$os = Get-CimInstance Win32_OperatingSystem

$result = [ordered]@{
    schemaVersion = 1
    capturedAtUtc = (Get-Date).ToUniversalTime().ToString('o')
    capabilities = @('C-DRIVERS-001', 'C-DRIVERS-002', 'C-DRIVERS-005', 'C-DRIVERS-007', 'C-DRIVERS-008')
    mutation = 'NONE'
    environment = [ordered]@{
        os = $os.Caption
        version = $os.Version
        build = $os.BuildNumber
        administrator = $administrator
    }
    inventory = [ordered]@{
        source = 'Win32_PnPSignedDriver + Get-PnpDevice'
        signedDriverCount = $driverRecords.Count
        presentDeviceCount = $presentDevices.Count
        recordsWithInfProviderVersion = $metadataCompleteCount
        canonicalRecordsSha256 = $driverInventoryHash
        providerCounts = $providerCounts
        classCounts = $classCounts
        rawIdentifiersPersisted = $false
    }
    deviceHealth = [ordered]@{
        source = 'Get-PnpDevice'
        healthyCount = @($presentDevices | Where-Object Problem -eq 'CM_PROB_NONE').Count
        problemCount = $problemDevices.Count
        observedProblemSummary = $problemSummary
        actualProblemFixtureAvailable = ($problemDevices.Count -gt 0)
        interpretationFixtures = @(
            [pscustomobject]@{ input = 'CM_PROB_NONE'; normalized = 'HEALTHY'; provenance = 'synthetic-rule-fixture' },
            [pscustomobject]@{ input = 'CM_PROB_DISABLED'; normalized = 'PROBLEM'; provenance = 'synthetic-rule-fixture' },
            [pscustomobject]@{ input = $null; normalized = 'UNKNOWN'; provenance = 'synthetic-rule-fixture' }
        )
    }
    pnputil = [ordered]@{
        command = 'pnputil.exe /enum-drivers'
        mode = 'read-only'
        exitCode = $pnpUtilExitCode
        outputLineCount = $pnpUtilOutput.Count
        outputSha256 = Get-TextSha256 -Lines $pnpUtilOutput
        parsed = $false
        limitation = 'Localized CLI text was not treated as the structured source of truth.'
    }
    safeguards = [ordered]@{
        policy = 'Conservative dry-run: every present device in a boot/connectivity/display/system class is blocked.'
        criticalClasses = $criticalClasses
        criticalDeviceCount = $criticalDevices.Count
        criticalClassCounts = $criticalClassCounts
        nonBlockedCriticalCount = 0
        forceFlagPresent = $false
        executableActionAttempted = $false
    }
    history = [ordered]@{
        causalityClaimed = $false
        statement = 'Timestamps are correlation context only; no driver regression was inferred from temporal proximity.'
        setupApiLog = [ordered]@{
            path = $setupApiPath
            exists = $setupApiExists
            lengthBytes = if ($setupApiInfo) { $setupApiInfo.Length } else { 0 }
            lastWriteUtc = if ($setupApiInfo) { $setupApiInfo.LastWriteTimeUtc.ToString('o') } else { $null }
            sha256 = $setupApiHash
        }
        eventLog = [ordered]@{
            status = $eventStatus
            error = $eventError
            recordCount = $driverEvents.Count
            records = $driverEvents
        }
        exportManifestOnly = $true
        rollbackProven = $false
    }
}

$result | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath $OutputPath -Encoding utf8
Write-Output $OutputPath
