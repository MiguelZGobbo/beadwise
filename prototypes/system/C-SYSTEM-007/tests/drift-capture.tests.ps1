$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$probe = Join-Path $prototypeRoot 'scripts\capture-registry-drift.ps1'
$sandboxPath = 'HKCU:\Software\BeadWise\Phase2Proof\C-SYSTEM-007'
$baselinePath = Join-Path $env:TEMP 'beadwise-c-system-007-baseline.json'
$currentPath = Join-Path $env:TEMP 'beadwise-c-system-007-current.json'
$diffPath = Join-Path $env:TEMP 'beadwise-c-system-007-diff.json'

try {
    Remove-Item -LiteralPath $sandboxPath -Recurse -Force -ErrorAction SilentlyContinue
    & $probe -Mode Capture -RegistryPath $sandboxPath -OutputPath $baselinePath
    New-Item -Path $sandboxPath -Force | Out-Null
    New-ItemProperty -Path $sandboxPath -Name 'ProofValue' -PropertyType String -Value 'after' -Force | Out-Null
    & $probe -Mode Capture -RegistryPath $sandboxPath -OutputPath $currentPath
    & $probe -Mode Compare -BaselinePath $baselinePath -CurrentPath $currentPath -OutputPath $diffPath

    $baseline = Get-Content -Raw -LiteralPath $baselinePath | ConvertFrom-Json
    $diff = Get-Content -Raw -LiteralPath $diffPath | ConvertFrom-Json
    if ($baseline.exists -ne $false) { throw 'Absent initial state was not captured' }
    if ($diff.changed -ne $true) { throw 'Registry drift was not detected' }
    if ($diff.changes.Count -lt 1) { throw 'Expected at least one normalized change' }

    Write-Output 'PASS: absent state, registry drift and provenance were captured'
}
finally {
    Remove-Item -LiteralPath $sandboxPath -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item -LiteralPath $baselinePath,$currentPath,$diffPath -Force -ErrorAction SilentlyContinue
}
