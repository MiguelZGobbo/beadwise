param(
    [string]$PrototypeRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'

$script = Join-Path $PrototypeRoot 'scripts\vendor-api-proof.ps1'
$result = Join-Path $PrototypeRoot 'results\vendor-api-proof.json'

if (-not (Test-Path -LiteralPath $script)) {
    throw "Missing proof script: $script"
}

& $script -OutputPath $result | Out-Null

$json = Get-Content -Raw -LiteralPath $result | ConvertFrom-Json
$gpuDetect = $json.steps | Where-Object name -eq 'GPU_DETECT' | Select-Object -First 1
$vendorAccess = $json.steps | Where-Object name -eq 'VENDOR_API_ACCESS' | Select-Object -First 1

if (-not $gpuDetect -or $gpuDetect.status -ne 'PASS') {
    throw 'GPU_DETECT did not pass.'
}
if (-not $vendorAccess -or $vendorAccess.status -notin @('PASS', 'BLOCKED')) {
    throw 'VENDOR_API_ACCESS must be PASS or BLOCKED with concrete evidence.'
}

Write-Output "PASS: GPU vendor API availability recorded as $($vendorAccess.status)"
