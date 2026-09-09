$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$script = Join-Path $root 'scripts\collect-storage-proof.ps1'
$out = Join-Path $env:TEMP 'beadwise-storage-proof.json'
try {
    & $script -OutputPath $out
    $r = Get-Content -Raw $out | ConvertFrom-Json
    foreach ($id in 'C-STORAGE-001','C-STORAGE-002','C-STORAGE-003','C-STORAGE-004','C-STORAGE-005','C-STORAGE-006','C-STORAGE-009','C-STORAGE-011','C-STORAGE-020') { if ($id -notin @($r.probes.featureId)) { throw "missing $id" } }
    $rollback = $r.probes | Where-Object featureId -eq 'C-STORAGE-011'
    if (-not $rollback.evidence.rollbackVerified) { throw 'owned temp rollback not verified' }
    if ($r.realDiskMutation) { throw 'real disk mutation detected' }
    'PASS: storage reads and owned-temp rollback executed without real-disk mutation'
} finally { Remove-Item $out -Force -ErrorAction SilentlyContinue }
