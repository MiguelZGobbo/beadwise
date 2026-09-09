param(
    [string]$PrototypeRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'
$runner = Join-Path $PrototypeRoot 'scripts\run-core-audio-proof.ps1'
$resultPath = Join-Path $PrototypeRoot 'results\core-audio-proof.json'

if (-not (Test-Path -LiteralPath $runner)) {
    throw "Missing Core Audio proof runner: $runner"
}

& $runner -PrototypeRoot $PrototypeRoot -OutputPath $resultPath | Out-Null
$result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
$rawResult = Get-Content -Raw -LiteralPath $resultPath

foreach ($capability in @('C-AUDIO-001', 'C-AUDIO-003', 'C-AUDIO-006', 'C-AUDIO-007', 'C-AUDIO-010')) {
    if ($result.capabilities -notcontains $capability) { throw "Missing capability mapping: $capability" }
}
if ($result.mutation -ne 'NONE') { throw 'Core Audio proof reported a mutation.' }
if ($result.api -notcontains 'IMMDeviceEnumerator.EnumAudioEndpoints') { throw 'MMDevice enumeration API was not exercised.' }
if ($result.api -notcontains 'IAudioEndpointVolume.GetMute/GetMasterVolumeLevelScalar') { throw 'Capture mute/volume API was not exercised.' }
if ($result.api -notcontains 'Get-PnpDevice -Class Media') { throw 'PnP media transport query was not exercised.' }
if ([int]$result.summary.endpointCount -lt 1) { throw 'Core Audio returned no endpoints.' }
if ([int]$result.summary.renderCount -lt 1) { throw 'Core Audio returned no render endpoint.' }
if ([int]$result.summary.captureCount -lt 1) { throw 'Core Audio returned no capture endpoint.' }
if (@($result.endpoints | Where-Object { [string]::IsNullOrWhiteSpace($_.endpointIdHash) }).Count -ne 0) { throw 'Endpoint without stable identity hash.' }
if (@($result.endpoints | Where-Object { $_.friendlyNameStatus -ne 'PASS' }).Count -ne 0) { throw 'Endpoint without property-store friendly-name evidence.' }
if (@($result.defaultEndpoints).Count -lt 1) { throw 'No default endpoint role was resolved.' }
if (@($result.captureDiagnostics).Count -lt 1) { throw 'No capture endpoint diagnostic was recorded.' }
if (@($result.captureDiagnostics | Where-Object { $_.activationStatus -eq 'PASS' -and $_.muteQueryStatus -eq 'PASS' }).Count -lt 1) { throw 'No capture endpoint mute query succeeded.' }
if (@($result.captureDiagnostics | Where-Object { $_.activationStatus -eq 'PASS' -and $_.volumeQueryStatus -eq 'PASS' }).Count -lt 1) { throw 'No capture endpoint volume query succeeded.' }
if ($true -ne [bool]$result.microphonePrivacySummary.queried) { throw 'Microphone privacy summary was not queried.' }
if ($false -ne [bool]$result.microphonePrivacySummary.rawAppOrPathPersisted) { throw 'Microphone privacy summary raw persistence flag is not explicit.' }
if ($true -ne [bool]$result.pnpTransportSummary.queried) { throw 'PnP transport summary was not queried.' }
if ([int]$result.pnpTransportSummary.audioEndpoint.count -lt 1) { throw 'No AudioEndpoint PnP devices were summarized.' }
if ([int]$result.pnpTransportSummary.media.count -lt 1) { throw 'No Media PnP devices were summarized.' }
if ($false -ne [bool]$result.pnpTransportSummary.rawInstanceIdsPersisted) { throw 'PnP raw identity persistence flag is not explicit.' }
if (@($result.pnpTransportSummary.media.hashedRows | Where-Object { [string]::IsNullOrWhiteSpace($_.instanceIdHash) }).Count -ne 0) { throw 'PnP row without stable identity hash.' }
if ($rawResult -match '"(id|friendlyName|endpointId)"\s*:') { throw 'Raw endpoint identity or name was persisted.' }
if ($rawResult -match 'BTHENUM\\|BTHHFENUM\\|HDAUDIO\\|USB\\|SWD\\MMDEVAPI\\|PCI\\VEN_') { throw 'Raw PnP/device identity was persisted.' }
if ($rawResult.Length -gt 30000) { throw "Evidence artifact is larger than necessary: $($rawResult.Length) characters." }

Write-Output 'PASS: Core Audio and PnP transport states, identity hashes, property availability, and default roles were enumerated read-only'
