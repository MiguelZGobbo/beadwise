$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$scriptPath = Join-Path $prototypeRoot 'scripts/security-readonly.ps1'
$resultPath = Join-Path $prototypeRoot 'results/security-readonly.json'

if (-not (Test-Path -LiteralPath $scriptPath)) {
    throw "Prototype script missing: $scriptPath"
}

& $scriptPath -OutputPath $resultPath

$result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
$expectedIds = @('C-SECURITY-001', 'C-SECURITY-003', 'C-SECURITY-007')
foreach ($id in $expectedIds) {
    $probe = $result.probes | Where-Object featureId -eq $id
    if (-not $probe) { throw "Missing mapped probe for $id" }
    if ($probe.status -notin @('PASS', 'PARTIAL', 'UNSUPPORTED', 'FAIL')) { throw "Invalid status for $id" }
}

$baseline = $result.probes | Where-Object featureId -eq 'C-SECURITY-001'
if (@($baseline.evidence.firewallProfiles).Count -lt 1) { throw 'Firewall effective state missing' }
if ($baseline.evidence.mutationAttempted) { throw 'Read-only proof attempted a mutation' }

$platform = $result.probes | Where-Object featureId -eq 'C-SECURITY-003'
if (-not $platform.evidence.sources.secureBoot -or -not $platform.evidence.sources.tpm -or -not $platform.evidence.sources.deviceGuard) {
    throw 'Platform security source outcomes are incomplete'
}

$lifecycle = $result.probes | Where-Object featureId -eq 'C-SECURITY-007'
if ([string]::IsNullOrWhiteSpace($lifecycle.evidence.osBuild)) { throw 'OS build missing' }
if ($lifecycle.evidence.lifecycle.status -ne 'UNSUPPORTED') { throw 'Unversioned lifecycle metadata was not rejected'
}

'PASS: security read-only prototype contract'
