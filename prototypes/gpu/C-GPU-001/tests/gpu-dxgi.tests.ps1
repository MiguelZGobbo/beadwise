$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$scriptPath = Join-Path $prototypeRoot 'scripts/run-gpu-dxgi-proof.ps1'
$resultPath = Join-Path $prototypeRoot 'results/gpu-dxgi-proof.json'

if (-not (Test-Path -LiteralPath $scriptPath)) {
    throw "Prototype script missing: $scriptPath"
}

& $scriptPath -OutputPath $resultPath

$result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
$expectedIds = @('C-GPU-001', 'C-GPU-002', 'C-GPU-006')
foreach ($id in $expectedIds) {
    $probe = $result.probes | Where-Object featureId -eq $id
    if (-not $probe) { throw "Missing mapped probe for $id" }
    if ($probe.status -notin @('PASS', 'PARTIAL', 'UNSUPPORTED', 'FAIL')) { throw "Invalid status for $id" }
}

$identity = $result.probes | Where-Object featureId -eq 'C-GPU-001'
if (@($identity.evidence.dxgiAdapters).Count -lt 1) { throw 'DXGI returned no adapters' }
if (@($identity.evidence.dxgiAdapters | Where-Object { -not [string]::IsNullOrWhiteSpace($_.description) }).Count -lt 1) {
    throw 'DXGI adapter description missing'
}

$memory = $result.probes | Where-Object featureId -eq 'C-GPU-002'
if (@($memory.evidence.memoryQueries).Count -lt 1) { throw 'No QueryVideoMemoryInfo outcome recorded' }

$api = $result.probes | Where-Object featureId -eq 'C-GPU-006'
if ([string]::IsNullOrWhiteSpace($api.evidence.d3d11.featureLevel)) { throw 'D3D11 feature level missing' }

'PASS: GPU DXGI/D3D capability prototype contract'
