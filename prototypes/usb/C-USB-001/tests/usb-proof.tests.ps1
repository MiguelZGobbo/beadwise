$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$script = Join-Path $root 'scripts\collect-usb-proof.ps1'
$out = Join-Path $env:TEMP 'beadwise-usb-proof.json'
try {
    & $script -OutputPath $out
    $r = Get-Content -Raw $out | ConvertFrom-Json
    foreach ($id in 'C-USB-001','C-USB-002','C-USB-005','C-USB-010','C-USB-015') { if ($id -notin @($r.probes.featureId)) { throw "missing $id" } }
    if (@($r.probes | Where-Object status -eq 'NOT_TESTED').Count) { throw 'NOT_TESTED cannot replace unsupported' }
    if (@($r.rawInstanceIds).Count) { throw 'raw PnP identifiers leaked' }
    'PASS: USB/HID/Bluetooth inventory and safeguard fixtures ran without device mutation'
} finally { Remove-Item $out -Force -ErrorAction SilentlyContinue }
