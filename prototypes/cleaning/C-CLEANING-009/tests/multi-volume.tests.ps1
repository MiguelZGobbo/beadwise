$ErrorActionPreference = 'Stop'
$prototypeRoot = Split-Path -Parent $PSScriptRoot
$script = Join-Path $prototypeRoot 'scripts\inspect-multi-volume.ps1'
$output = Join-Path $env:TEMP 'beadwise-cleaning-multi-volume-test.json'
try {
    & $script -OutputPath $output
    $raw = Get-Content -Raw -LiteralPath $output
    $result = $raw | ConvertFrom-Json
    if ($result.status -ne 'PASS') { throw "Unexpected status: $($result.status)" }
    if ($result.mutationCount -ne 0) { throw 'Read-only proof reported mutation' }
    if (@($result.actualVolumes).Count -lt 1) { throw 'No actual volume was inventoried' }
    if (@($result.actualVolumes | Where-Object { $_.root -eq $env:SystemDrive + '\' }).Count -ne 1) { throw 'System volume missing' }
    if (@($result.normalizationFixtures | Select-Object -ExpandProperty classification) -notcontains 'OFFLINE') { throw 'Offline normalization case missing' }
    if (@($result.normalizationFixtures | Select-Object -ExpandProperty classification) -notcontains 'NETWORK') { throw 'Network normalization case missing' }
    if ($raw -match '(?i)C:\\Users\\[^\[\s]') { throw 'User profile path leaked' }
    Write-Output 'PASS: multi-volume inventory and classification ran read-only'
}
finally {
    Remove-Item -LiteralPath $output -Force -ErrorAction SilentlyContinue
}
