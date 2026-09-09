param(
    [string]$PrototypeRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'

$script = Join-Path $PrototypeRoot 'scripts\registry-drift-proof.ps1'
$result = Join-Path $PrototypeRoot 'results\registry-drift-proof.json'

if (-not (Test-Path -LiteralPath $script)) {
    throw "Missing proof script: $script"
}

& $script -OutputPath $result | Out-Null

$json = Get-Content -Raw -LiteralPath $result | ConvertFrom-Json

foreach ($step in @('DETECT', 'PLAN', 'DRY_RUN', 'APPLY', 'VERIFY', 'IDEMPOTENCE', 'ROLLBACK', 'VERIFY_ROLLBACK')) {
    $entry = $json.steps | Where-Object name -eq $step | Select-Object -First 1
    if (-not $entry) {
        throw "Missing step $step"
    }
    if ($entry.status -ne 'PASS') {
        throw "Step $step was $($entry.status): $($entry.detail)"
    }
}

if ($json.initial.exists -ne $false) {
    throw 'Expected controlled sandbox key to be absent before apply.'
}

if ($json.afterDryRun.exists -ne $false) {
    throw 'Dry-run mutated the sandbox key.'
}

if ($json.afterRollback.exists -ne $false) {
    throw 'Rollback did not restore the absent-key snapshot.'
}

Write-Output 'PASS: registry drift snapshot/rollback contract is complete'
