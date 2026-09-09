$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$scriptPath = Join-Path $prototypeRoot 'scripts/cpu-safe-capabilities.ps1'
$resultPath = Join-Path $prototypeRoot 'results/cpu-safe-capabilities.json'

if (-not (Test-Path -LiteralPath $scriptPath)) {
    throw "Prototype script missing: $scriptPath"
}

& $scriptPath -OutputPath $resultPath

$result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
$expectedIds = @('C-CPU-001', 'C-CPU-002', 'C-CPU-004', 'C-CPU-011', 'C-CPU-015')
$actualIds = @($result.probes | ForEach-Object featureId)

foreach ($id in $expectedIds) {
    if ($id -notin $actualIds) { throw "Missing mapped probe for $id" }
}
foreach ($probe in $result.probes) {
    if ($probe.status -notin @('PASS', 'PARTIAL', 'UNSUPPORTED', 'FAIL')) {
        throw "Invalid status for $($probe.featureId): $($probe.status)"
    }
    if ([string]::IsNullOrWhiteSpace($probe.mechanism)) {
        throw "Missing mechanism for $($probe.featureId)"
    }
}

$topology = $result.probes | Where-Object featureId -eq 'C-CPU-001'
if (-not $topology.evidence.nativeCallSucceeded) { throw 'GetLogicalProcessorInformationEx did not succeed' }
if ($topology.evidence.logicalProcessorCount -lt 1) { throw 'No logical processors reported' }

$benchmark = $result.probes | Where-Object featureId -eq 'C-CPU-002'
if (@($benchmark.evidence.singleThreadMs).Count -lt 3) { throw 'Insufficient single-thread repetitions' }
if (@($benchmark.evidence.multiThreadMs).Count -lt 3) { throw 'Insufficient multi-thread repetitions' }
if (($benchmark.evidence.singleCoefficientOfVariation -gt 0.15 -or $benchmark.evidence.multiCoefficientOfVariation -gt 0.15) -and $benchmark.status -ne 'PARTIAL') {
    throw 'Noisy benchmark was not preserved as PARTIAL'
}

$classification = $result.probes | Where-Object featureId -eq 'C-CPU-004'
if ($classification.evidence.cpuFixture.classification -ne 'CPU_BOUND') { throw 'Controlled CPU fixture was not classified as CPU_BOUND' }
if ($classification.evidence.ioFixture.classification -ne 'IO_BOUND_OR_WAITING') { throw 'Controlled I/O fixture was not classified as IO_BOUND_OR_WAITING' }
if ($classification.evidence.unknownFixture.classification -ne 'UNKNOWN') { throw 'Conflicting fixture did not return UNKNOWN' }

$guard = $result.probes | Where-Object featureId -eq 'C-CPU-011'
if (@($guard.evidence.decisions | Where-Object decision -eq 'ALLOW').Count -ne 0) { throw 'Unsafe gate allowed a candidate' }
if ($guard.evidence.beforeFingerprint -ne $guard.evidence.afterFingerprint) { throw 'Dry-run changed observed system state' }

$overhead = $result.probes | Where-Object featureId -eq 'C-CPU-015'
if ($overhead.evidence.sampleCount -lt 10) { throw 'Monitoring overhead sample was too small' }

'PASS: CPU safe capability prototype contract'
