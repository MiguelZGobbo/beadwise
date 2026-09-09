$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$probe = Join-Path $prototypeRoot 'scripts\restart-owned-process.ps1'
$resultPath = Join-Path $env:TEMP 'beadwise-c-processes-007-test.json'

try {
    & $probe -OutputPath $resultPath
    $result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
    if ($result.featureId -ne 'C-PROCESSES-007') { throw 'Wrong featureId' }
    if ($result.plan.changes.Count -ne 2) { throw 'Plan did not model terminate and restart' }
    if ($result.dryRun.mutated -ne $false) { throw 'Dry-run reported mutation' }
    if ($result.apply.firstPid -eq $result.apply.secondPid) { throw 'Restart did not create a distinct process' }
    if ($result.verify.firstStopped -ne $true -or $result.verify.secondRunning -ne $true) { throw 'Observed lifecycle did not match target' }
    if ($result.rollback.secondStopped -ne $true) { throw 'Cleanup did not stop the restarted fixture' }
    Write-Output 'PASS: prototype-owned process terminate/restart/cleanup lifecycle verified'
}
finally {
    Remove-Item -LiteralPath $resultPath -Force -ErrorAction SilentlyContinue
}
