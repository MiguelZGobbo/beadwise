param(
    [string]$PrototypeRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'

$script = Join-Path $PrototypeRoot 'scripts\etw-wpr-proof.ps1'
$result = Join-Path $PrototypeRoot 'results\etw-wpr-proof.json'

if (-not (Test-Path -LiteralPath $script)) {
    throw "Missing proof script: $script"
}

& $script -OutputPath $result | Out-Null

$json = Get-Content -Raw -LiteralPath $result | ConvertFrom-Json
foreach ($step in @('WPR_PRESENT', 'WPR_STATUS', 'WPR_PROFILES')) {
    $entry = $json.steps | Where-Object name -eq $step | Select-Object -First 1
    if (-not $entry) {
        throw "Missing step $step"
    }
    if ($entry.status -ne 'PASS') {
        throw "Step $step was $($entry.status): $($entry.detail)"
    }
}

$trace = $json.steps | Where-Object name -eq 'TRACE_START_STOP' | Select-Object -First 1
if (-not $trace) {
    throw 'Missing TRACE_START_STOP result.'
}
if ($trace.status -notin @('PASS', 'FAIL')) {
    throw "Unexpected TRACE_START_STOP status $($trace.status)"
}

Write-Output "PASS: WPR preflight recorded trace capability as $($trace.status)"
