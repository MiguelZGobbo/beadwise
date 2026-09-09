$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$scriptPath = Join-Path $prototypeRoot 'scripts/timer-resolution-proof.ps1'
$resultPath = Join-Path $prototypeRoot 'results/timer-resolution-proof.json'

if (-not (Test-Path -LiteralPath $scriptPath)) {
    throw "Prototype script missing: $scriptPath"
}

& $scriptPath -OutputPath $resultPath

$result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
$probe = $result.probes | Where-Object featureId -eq 'C-ENERGY-013'
if (-not $probe) { throw 'Missing mapped probe for C-ENERGY-013' }
if ($probe.status -notin @('PASS', 'PARTIAL', 'UNSUPPORTED', 'FAIL')) { throw 'Invalid timer probe status' }
if (-not $probe.evidence.ntQueryTimerResolution.callSucceeded) { throw 'NtQueryTimerResolution failed' }
if ($probe.evidence.ntQueryTimerResolution.current100ns -lt 1) { throw 'Invalid current timer resolution' }
if ($probe.evidence.attribution.status -ne 'UNSUPPORTED') { throw 'Unsupported attribution was not recorded explicitly' }
if ($probe.evidence.mutationAttempted) { throw 'Timer proof attempted a mutation' }

'PASS: timer resolution read-only prototype contract'
