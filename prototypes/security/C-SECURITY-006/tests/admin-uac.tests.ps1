param(
    [string]$PrototypeRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'

$script = Join-Path $PrototypeRoot 'scripts\admin-uac-proof.ps1'
$result = Join-Path $PrototypeRoot 'results\admin-uac-proof.json'

if (-not (Test-Path -LiteralPath $script)) {
    throw "Missing proof script: $script"
}

& $script -OutputPath $result | Out-Null

$json = Get-Content -Raw -LiteralPath $result | ConvertFrom-Json
$raw = Get-Content -Raw -LiteralPath $result
if ($json.PSObject.Properties.Name -contains 'identity' -or $json.PSObject.Properties.Name -contains 'whoamiGroups') {
    throw 'Persistent evidence contains identity/group details'
}
if ($raw -match '@') { throw 'Persistent evidence may contain an account identifier' }
foreach ($step in @('TOKEN_DETECT', 'ADMIN_REQUIRED_SIGNAL')) {
    $entry = $json.steps | Where-Object name -eq $step | Select-Object -First 1
    if (-not $entry) {
        throw "Missing step $step"
    }
    if ($entry.status -ne 'PASS') {
        throw "Step $step was $($entry.status): $($entry.detail)"
    }
}

Write-Output 'PASS: admin/UAC non-elevated behavior is structured'
