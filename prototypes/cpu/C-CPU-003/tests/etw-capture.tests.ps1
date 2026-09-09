$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$probe = Join-Path $prototypeRoot 'scripts\capture-cpu-etw.ps1'
$resultPath = Join-Path $env:TEMP 'beadwise-c-cpu-003-etw-test.json'
$etlPath = Join-Path $env:TEMP 'beadwise-c-cpu-003-etw-test.etl'

try {
    & $probe -ResultPath $resultPath -EtlPath $etlPath
    $result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
    if ($result.featureId -ne 'C-CPU-003') { throw 'Wrong featureId' }
    if ($result.status -eq 'NOT_TESTED') { throw 'ETW attempt was not executed' }
    if ($result.startOutput -match '0xc5585011' -and $result.status -ne 'BLOCKED_ADMIN') { throw 'Known non-admin WPR failure was not classified as BLOCKED_ADMIN' }
    if ($result.status -eq 'PASS' -and (-not (Test-Path -LiteralPath $etlPath) -or (Get-Item $etlPath).Length -le 0)) { throw 'PASS without a non-empty ETL' }
    Write-Output "OBSERVED: ETW status=$($result.status)"
}
finally {
    Remove-Item -LiteralPath $resultPath,$etlPath -Force -ErrorAction SilentlyContinue
}
