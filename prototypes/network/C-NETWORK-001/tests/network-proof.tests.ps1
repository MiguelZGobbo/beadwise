$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$script = Join-Path $root 'scripts\collect-network-proof.ps1'
$out = Join-Path $env:TEMP 'beadwise-network-proof.json'
try {
    & $script -OutputPath $out
    $r = Get-Content -Raw $out | ConvertFrom-Json
    foreach ($id in 'C-NETWORK-001','C-NETWORK-002','C-NETWORK-003','C-NETWORK-005','C-NETWORK-007','C-NETWORK-013') { if ($id -notin @($r.probes.featureId)) { throw "missing $id" } }
    if (@($r.probes | Where-Object status -eq 'NOT_TESTED').Count) { throw 'NOT_TESTED is not evidence' }
    if (-not ($r.probes | Where-Object featureId -eq 'C-NETWORK-013').evidence.volatileOnlyIgnored) { throw 'volatile-only diff was not normalized' }
    'PASS: network probes exercise actual path and deterministic drift'
} finally { Remove-Item $out -Force -ErrorAction SilentlyContinue }
