$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$scriptPath = Join-Path $prototypeRoot 'scripts/process-safe-capabilities.ps1'
$resultPath = Join-Path $prototypeRoot 'results/process-safe-capabilities.json'

if (-not (Test-Path -LiteralPath $scriptPath)) {
    throw "Prototype script missing: $scriptPath"
}

& $scriptPath -OutputPath $resultPath

$result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
$serializedResult = $result | ConvertTo-Json -Depth 12
if ($serializedResult -match [regex]::Escape($env:USERPROFILE)) { throw 'Result leaked the user profile path' }
$expectedIds = @('C-PROCESSES-001', 'C-PROCESSES-002', 'C-PROCESSES-004', 'C-PROCESSES-005', 'C-PROCESSES-006')
foreach ($id in $expectedIds) {
    $probe = $result.probes | Where-Object featureId -eq $id
    if (-not $probe) { throw "Missing mapped probe for $id" }
    if ($probe.status -notin @('PASS', 'PARTIAL', 'UNSUPPORTED', 'FAIL')) { throw "Invalid status for $id" }
}

$inventory = $result.probes | Where-Object featureId -eq 'C-PROCESSES-001'
if ($inventory.evidence.executable -match '^[A-Za-z]:\\Users\\') { throw 'Result leaked an absolute user-profile executable path' }
if (-not $inventory.evidence.parentMatched) { throw 'Owned child parent mapping failed' }
if (-not $inventory.evidence.exitObserved) { throw 'Exited child was not observed as gone' }

$trend = $result.probes | Where-Object featureId -eq 'C-PROCESSES-002'
if (@($trend.evidence.samples).Count -lt 3) { throw 'Insufficient temporal process samples' }

$lifecycle = $result.probes | Where-Object featureId -eq 'C-PROCESSES-004'
if ($lifecycle.evidence.exitCode -ne 23) { throw 'Controlled lifecycle exit code was not preserved' }

$policy = $result.probes | Where-Object featureId -eq 'C-PROCESSES-005'
if (-not $policy.evidence.restored) { throw 'Owned child scheduling state was not restored' }

$contention = $result.probes | Where-Object featureId -eq 'C-PROCESSES-006'
if ($contention.evidence.threadCount -lt 1) { throw 'Owned child threads were not observable' }

'PASS: process safe capability prototype contract'
