$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$script = Join-Path $root 'scripts\collect-update-proof.ps1'
$out = Join-Path $env:TEMP 'beadwise-update-proof.json'
try {
    & $script -OutputPath $out
    $r = Get-Content -Raw $out | ConvertFrom-Json
    foreach ($id in 'C-UPDATE-001','C-UPDATE-003','C-UPDATE-006','C-UPDATE-008','C-UPDATE-010') { if ($id -notin @($r.probes.featureId)) { throw "missing $id" } }
    if (($r.probes | Where-Object featureId -eq 'C-UPDATE-008').evidence.falseCorrelationRejected -ne $true) { throw 'false-correlation fixture failed' }
    if ($r.mutationPolicy -ne 'read-only') { throw 'update probe must be read-only' }
    'PASS: Update Agent history, readiness and policy probes ran read-only'
} finally { Remove-Item $out -Force -ErrorAction SilentlyContinue }
