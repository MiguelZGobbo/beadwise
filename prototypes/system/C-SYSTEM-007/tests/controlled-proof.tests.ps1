$ErrorActionPreference = 'Stop'
$prototypeRoot = Split-Path -Parent $PSScriptRoot
$runner = Join-Path $prototypeRoot 'scripts\run-controlled-proof.ps1'
$resultRoot = Join-Path $env:TEMP 'beadwise-c-system-007-controlled'
$sandboxPath = 'HKCU:\Software\BeadWise\Phase2Proof\C-SYSTEM-007'
try {
    & $runner -ResultDirectory $resultRoot
    $diff = Get-Content -Raw -LiteralPath (Join-Path $resultRoot 'drift.json') | ConvertFrom-Json
    if ($diff.changed -ne $true) { throw 'Controlled proof did not persist an observed change' }
    if (Test-Path -LiteralPath $sandboxPath) { throw 'Controlled registry fixture was not rolled back' }
    Write-Output 'PASS: controlled evidence persisted and registry fixture rolled back'
}
finally {
    Remove-Item -LiteralPath $sandboxPath -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item -LiteralPath $resultRoot -Recurse -Force -ErrorAction SilentlyContinue
}
