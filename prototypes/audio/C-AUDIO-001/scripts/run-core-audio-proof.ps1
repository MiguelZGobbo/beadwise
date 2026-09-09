param(
    [string]$PrototypeRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path,
    [string]$OutputPath = (Join-Path $PrototypeRoot 'results\core-audio-proof.json')
)

$ErrorActionPreference = 'Stop'

function Get-StableHash {
    param([string]$Value)
    $sha = [System.Security.Cryptography.SHA256]::Create()
    try {
        $bytes = [Text.Encoding]::UTF8.GetBytes($Value)
        (($sha.ComputeHash($bytes) | ForEach-Object { $_.ToString('x2') }) -join '').Substring(0, 32).ToUpperInvariant()
    }
    finally {
        $sha.Dispose()
    }
}

function Get-EnumeratorPrefix {
    param([string]$InstanceId)
    if ([string]::IsNullOrWhiteSpace($InstanceId)) { return 'UNKNOWN' }
    ($InstanceId -split '\\', 2)[0].ToUpperInvariant()
}

function Summarize-PnpDevices {
    param([object[]]$Devices)

    $rows = foreach ($device in @($Devices)) {
        [pscustomobject]@{
            class = [string]$device.Class
            status = [string]$device.Status
            problem = if ($null -eq $device.Problem) { 'UNKNOWN' } else { [string]$device.Problem }
            enumerator = Get-EnumeratorPrefix ([string]$device.InstanceId)
            instanceIdHash = Get-StableHash ([string]$device.InstanceId)
        }
    }

    $byClass = @($rows | Group-Object class | Sort-Object Name | ForEach-Object { [ordered]@{ name = $_.Name; count = $_.Count } })
    $byStatus = @($rows | Group-Object status | Sort-Object Name | ForEach-Object { [ordered]@{ name = $_.Name; count = $_.Count } })
    $byProblem = @($rows | Group-Object problem | Sort-Object Name | ForEach-Object { [ordered]@{ name = $_.Name; count = $_.Count } })
    $byEnumerator = @($rows | Group-Object enumerator | Sort-Object Name | ForEach-Object { [ordered]@{ name = $_.Name; count = $_.Count } })

    [ordered]@{
        count = @($rows).Count
        byClass = $byClass
        byStatus = $byStatus
        byProblem = $byProblem
        byEnumerator = $byEnumerator
        hashedRows = @($rows | Select-Object class, status, problem, enumerator, instanceIdHash)
    }
}

$project = Join-Path $PrototypeRoot 'src\CoreAudioProbe\CoreAudioProbe.csproj'
if (-not (Test-Path -LiteralPath $project)) {
    throw "Missing Core Audio probe project: $project"
}

$output = dotnet run --project $project --configuration Release
if ($LASTEXITCODE -ne 0) { throw "dotnet run failed with $LASTEXITCODE" }

$result = ($output | Out-String).Trim() | ConvertFrom-Json
$audioEndpointDevices = @(Get-PnpDevice -Class AudioEndpoint -ErrorAction SilentlyContinue)
$mediaDevices = @(Get-PnpDevice -Class Media -ErrorAction SilentlyContinue)

$result.api += @(
    'Get-PnpDevice -Class AudioEndpoint',
    'Get-PnpDevice -Class Media'
)
$result | Add-Member -NotePropertyName pnpTransportSummary -NotePropertyValue ([ordered]@{
    queried = $true
    rawInstanceIdsPersisted = $false
    audioEndpoint = Summarize-PnpDevices $audioEndpointDevices
    media = Summarize-PnpDevices $mediaDevices
})
$result | Add-Member -NotePropertyName transportCoverage -NotePropertyValue ([ordered]@{
    queried = $true
    enumeratorsObserved = @(
        @($audioEndpointDevices + $mediaDevices | ForEach-Object { Get-EnumeratorPrefix ([string]$_.InstanceId) } | Sort-Object -Unique)
    )
    hasUsbCandidate = @($mediaDevices | Where-Object { (Get-EnumeratorPrefix ([string]$_.InstanceId)) -eq 'USB' }).Count -gt 0
    hasBluetoothCandidate = @($mediaDevices | Where-Object { (Get-EnumeratorPrefix ([string]$_.InstanceId)) -in @('BTHENUM','BTHHFENUM') }).Count -gt 0
    hasRootOrVirtualCandidate = @($mediaDevices | Where-Object { (Get-EnumeratorPrefix ([string]$_.InstanceId)) -in @('ROOT','SWD') }).Count -gt 0
    endpointToParentExactCorrelation = 'NOT_TESTED'
})
$microphoneConsentValues = @()
foreach ($root in @(
    'HKCU:\Software\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\microphone',
    'HKLM:\Software\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\microphone'
)) {
    if (-not (Test-Path -LiteralPath $root)) { continue }
    foreach ($item in @(Get-ChildItem -LiteralPath $root -ErrorAction SilentlyContinue)) {
        $value = (Get-ItemProperty -LiteralPath $item.PSPath -Name Value -ErrorAction SilentlyContinue).Value
        if ([string]::IsNullOrWhiteSpace([string]$value)) { $value = 'UNSPECIFIED' }
        $microphoneConsentValues += [string]$value
    }
}
$result | Add-Member -NotePropertyName microphonePrivacySummary -NotePropertyValue ([ordered]@{
    queried = $true
    rawAppOrPathPersisted = $false
    valueCounts = @($microphoneConsentValues | Group-Object | Sort-Object Name | ForEach-Object { [ordered]@{ value = $_.Name; count = $_.Count } })
})

$result.limitations = @(
    'No endpoint state, role, volume, mute, route, service, device, or privacy setting was changed.',
    'Capture endpoint mute/volume were queried read-only; no capture stream was opened and no user audio was retained.',
    'Microphone consent-store values were summarized without app IDs or paths.',
    'PnP transport classes were queried read-only and stored as counts/hashes; exact endpoint-to-parent correlation remains NOT_TESTED.',
    'Hotplug, permission-blocked, disabled, sleep/resume, and session fixtures remain untested unless naturally present.'
)

New-Item -ItemType Directory -Path (Split-Path -Parent $OutputPath) -Force | Out-Null
[IO.File]::WriteAllText($OutputPath, (($result | ConvertTo-Json -Depth 20) + "`n"), [Text.UTF8Encoding]::new($false))
$OutputPath
