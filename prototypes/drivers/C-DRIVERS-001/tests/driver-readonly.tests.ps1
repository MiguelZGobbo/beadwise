param(
    [string]$PrototypeRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'
$proofScript = Join-Path $PrototypeRoot 'scripts\driver-readonly-proof.ps1'
$resultPath = Join-Path $PrototypeRoot 'results\driver-readonly-proof.json'

if (-not (Test-Path -LiteralPath $proofScript)) {
    throw "Missing driver proof script: $proofScript"
}

& $proofScript -OutputPath $resultPath | Out-Null
$result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
$rawResult = Get-Content -Raw -LiteralPath $resultPath

foreach ($capability in @('C-DRIVERS-001', 'C-DRIVERS-002', 'C-DRIVERS-005', 'C-DRIVERS-007', 'C-DRIVERS-008')) {
    if ($result.capabilities -notcontains $capability) { throw "Missing capability mapping: $capability" }
}
if ($result.mutation -ne 'NONE') { throw 'Read-only proof reported a mutation.' }
if ([int]$result.inventory.signedDriverCount -lt 1) { throw 'Signed driver inventory is empty.' }
if ([int]$result.inventory.presentDeviceCount -lt 1) { throw 'Present PnP inventory is empty.' }
if ([int]$result.inventory.recordsWithInfProviderVersion -lt 1) { throw 'No driver has INF/provider/version metadata.' }
if ([int]$result.pnputil.exitCode -ne 0) { throw "PnPUtil read-only inventory failed: $($result.pnputil.exitCode)" }
if ([int]$result.safeguards.criticalDeviceCount -lt 1) { throw 'No current critical device was classified.' }
if ([int]$result.safeguards.nonBlockedCriticalCount -ne 0) { throw 'A critical device was not blocked by the dry-run guard.' }
if ($result.safeguards.forceFlagPresent -ne $false) { throw 'Unsafe /force flag appeared in the plan.' }
if ($result.history.causalityClaimed -ne $false) { throw 'Timeline proximity was incorrectly presented as causality.' }
if ($result.history.setupApiLog.exists -ne $true) { throw 'SetupAPI history source was not found.' }
if ($rawResult -match '"(deviceId|instanceId|deviceName|friendlyName)"\s*:') { throw 'Raw device identity was persisted.' }
if ($rawResult.Length -gt 60000) { throw "Evidence artifact is larger than necessary: $($rawResult.Length) characters." }

Write-Output 'PASS: driver inventory, healthy/problem state, critical-device refusal, and history sources were read without mutation'
