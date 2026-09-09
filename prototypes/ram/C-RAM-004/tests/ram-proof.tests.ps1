$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$script = Join-Path $root 'scripts\collect-ram-proof.ps1'
$out = Join-Path $env:TEMP 'beadwise-ram-proof.json'
try {
    & $script -OutputPath $out
    $r = Get-Content -Raw $out | ConvertFrom-Json
    foreach ($id in 'C-RAM-001','C-RAM-004','C-RAM-006','C-RAM-009','C-RAM-010','C-RAM-012','C-RAM-014','C-RAM-016','C-RAM-017') { if ($id -notin @($r.probes.featureId)) { throw "missing $id" } }
    if (($r.probes | Where-Object featureId -eq 'C-RAM-004').evidence.allocatedBytes -gt 268435456) { throw 'workload exceeded safety cap' }
    if (($r.probes | Where-Object featureId -eq 'C-RAM-016').status -notin 'PARTIAL','UNSUPPORTED','FAIL') { throw 'large-page result was inferred' }
    'PASS: bounded memory, native NUMA/large-page and localized counter probes ran'
} finally { Remove-Item $out -Force -ErrorAction SilentlyContinue }
