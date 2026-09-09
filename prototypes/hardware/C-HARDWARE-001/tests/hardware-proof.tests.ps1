$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$script = Join-Path $root 'scripts\collect-hardware-proof.ps1'
$out = Join-Path $env:TEMP 'beadwise-hardware-proof.json'
try {
    & $script -OutputPath $out
    $r = Get-Content -Raw $out | ConvertFrom-Json
    $ids = @($r.probes.featureId)
    foreach ($id in 'C-HARDWARE-001','C-HARDWARE-004','C-HARDWARE-009') { if ($id -notin $ids) { throw "missing $id" } }
    $inventory = $r.probes | Where-Object featureId -eq 'C-HARDWARE-001'
    if (-not $inventory.evidence.nativeFirmwareBytes -or -not $inventory.evidence.cimBiosVersion) { throw 'firmware/CIM evidence absent' }
    if ($r.mutationPolicy -ne 'read-only') { throw 'mutation policy mismatch' }
    'PASS: hardware probes preserve native/CIM provenance and package privacy'
} finally { Remove-Item $out -Force -ErrorAction SilentlyContinue }
