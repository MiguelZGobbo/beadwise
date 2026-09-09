$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$probe = Join-Path $prototypeRoot 'scripts\sample-cpu-pdh.ps1'
$resultPath = Join-Path $env:TEMP 'beadwise-c-cpu-003-test.json'

try {
    & $probe -OutputPath $resultPath -SampleCount 2 -IntervalMilliseconds 100
    $result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json

    if ($result.featureId -ne 'C-CPU-003') { throw 'Wrong featureId' }
    if ($result.mechanism -ne 'PDH_PdhAddEnglishCounterW') { throw 'PDH English counter API was not used' }
    if ($result.samples.Count -ne 2) { throw 'Expected exactly two samples' }
    if (@($result.samples | Where-Object { $_.status -ne 0 }).Count -ne 0) { throw 'PDH returned an invalid formatted value' }
    if (@($result.samples | Where-Object { $_.value -lt 0 -or $_.value -gt 100 }).Count -ne 0) { throw 'CPU sample outside 0..100' }

    Write-Output 'PASS: PDH English counter sampling works independently of OS display language'
}
finally {
    Remove-Item -LiteralPath $resultPath -Force -ErrorAction SilentlyContinue
}
