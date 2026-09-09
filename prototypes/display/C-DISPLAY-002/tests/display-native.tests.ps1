param(
    [string]$PrototypeRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'
$project = Join-Path $PrototypeRoot 'src\DisplayNativeProbe\DisplayNativeProbe.csproj'
$resultPath = Join-Path $PrototypeRoot 'results\display-native-proof.json'

if (-not (Test-Path -LiteralPath $project)) {
    throw "Missing display probe project: $project"
}

$output = dotnet run --project $project --configuration Release
if ($LASTEXITCODE -ne 0) { throw "dotnet run failed with $LASTEXITCODE" }

New-Item -ItemType Directory -Path (Split-Path -Parent $resultPath) -Force | Out-Null
[IO.File]::WriteAllText($resultPath, (($output | Out-String).Trim() + "`n"), [Text.UTF8Encoding]::new($false))
$result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
$rawResult = Get-Content -Raw -LiteralPath $resultPath

foreach ($capability in @('C-DISPLAY-001', 'C-DISPLAY-002')) {
    if ($result.capabilities -notcontains $capability) { throw "Missing capability mapping: $capability" }
}
if ($result.api -notcontains 'GetDisplayConfigBufferSizes') { throw 'CCD API was not exercised.' }
if ($result.api -notcontains 'ChangeDisplaySettingsEx(CDS_TEST)') { throw 'Non-persistent validation API was not exercised.' }
if ($result.api -notcontains 'HKLM SYSTEM CurrentControlSet Enum DISPLAY EDID read') { throw 'EDID registry read was not exercised.' }
if ([int]$result.activePathCount -lt 1) { throw 'CCD returned no active display paths.' }
if (@($result.displays).Count -lt 1) { throw 'No attached desktop displays were enumerated.' }
if ($true -ne [bool]$result.edidSummary.queried) { throw 'EDID summary was not queried.' }
if ($false -ne [bool]$result.edidSummary.rawEdidPersisted) { throw 'EDID raw persistence flag is not explicit.' }

foreach ($display in @($result.displays)) {
    if ([string]::IsNullOrWhiteSpace($display.deviceIdHash)) { throw 'Display device identity hash missing.' }
    if ($false -ne [bool]$display.deviceIdPersisted) { throw 'Display raw identity persistence flag is not explicit.' }
    if ([int]$display.currentMode.width -lt 1 -or [int]$display.currentMode.height -lt 1) { throw 'Invalid current display mode.' }
    if ([int]$display.enumeratedModeCount -lt 1) { throw 'No supported modes enumerated.' }
    if ([int]$display.currentValidationCode -ne 0) { throw "Current mode failed CDS_TEST: $($display.currentValidationCode)" }
    if ($display.stateUnchanged -ne $true) { throw 'CDS_TEST changed effective display state.' }
}
if ($rawResult -match '"deviceId"\s*:') { throw 'Raw display device ID was persisted.' }
if ($rawResult -match 'PCI\\VEN_') { throw 'Raw PnP display identity was persisted.' }
if ($rawResult -match '00FFFFFF') { throw 'Raw EDID bytes appear to have been persisted.' }

Write-Output 'PASS: CCD inventory and CDS_TEST mode validation completed without changing display state'
