param(
    [string]$PrototypeRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'

$script = Join-Path $PrototypeRoot 'scripts\performance-counter-proof.ps1'
$result = Join-Path $PrototypeRoot 'results\performance-counter-proof.json'

if (-not (Test-Path -LiteralPath $script)) {
    throw "Missing proof script: $script"
}

& $script -OutputPath $result | Out-Null

$json = Get-Content -Raw -LiteralPath $result | ConvertFrom-Json
foreach ($step in @('DISCOVER', 'SAMPLE')) {
    $entry = $json.steps | Where-Object name -eq $step | Select-Object -First 1
    if (-not $entry) {
        throw "Missing step $step"
    }
    if ($entry.status -ne 'PASS') {
        throw "Step $step was $($entry.status): $($entry.detail)"
    }
}
if ([double]$json.sample.cookedValue -lt 0) {
    throw 'Performance counter returned a negative CPU value.'
}

Write-Output 'PASS: localized Performance Counter discovery/sample works'
