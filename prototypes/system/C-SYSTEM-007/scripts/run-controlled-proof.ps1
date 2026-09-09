param([Parameter(Mandatory)] [string]$ResultDirectory)

$ErrorActionPreference = 'Stop'
$collector = Join-Path $PSScriptRoot 'capture-registry-drift.ps1'
$sandboxPath = 'HKCU:\Software\BeadWise\Phase2Proof\C-SYSTEM-007'
$baselinePath = Join-Path $ResultDirectory 'baseline.json'
$currentPath = Join-Path $ResultDirectory 'current.json'
$diffPath = Join-Path $ResultDirectory 'drift.json'

New-Item -ItemType Directory -Path $ResultDirectory -Force | Out-Null
try {
    Remove-Item -LiteralPath $sandboxPath -Recurse -Force -ErrorAction SilentlyContinue
    & $collector -Mode Capture -RegistryPath $sandboxPath -OutputPath $baselinePath
    New-Item -Path $sandboxPath -Force | Out-Null
    New-ItemProperty -Path $sandboxPath -Name 'ProofValue' -PropertyType String -Value 'after' -Force | Out-Null
    & $collector -Mode Capture -RegistryPath $sandboxPath -OutputPath $currentPath
    & $collector -Mode Compare -BaselinePath $baselinePath -CurrentPath $currentPath -OutputPath $diffPath
}
finally {
    Remove-Item -LiteralPath $sandboxPath -Recurse -Force -ErrorAction SilentlyContinue
}

Write-Output $diffPath
