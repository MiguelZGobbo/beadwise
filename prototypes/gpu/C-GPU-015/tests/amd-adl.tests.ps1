$ErrorActionPreference = 'Stop'
$prototypeRoot = Split-Path -Parent $PSScriptRoot
$probe = Join-Path $prototypeRoot 'scripts\probe-amd-adl.ps1'
$resultPath = Join-Path $env:TEMP 'beadwise-c-gpu-015-adl-test.json'
try {
    & $probe -OutputPath $resultPath
    $result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
    if ($result.featureId -ne 'C-GPU-015') { throw 'Wrong featureId' }
    if ($result.libraryAvailable -ne $true) { throw 'AMD ADL library was not available despite installed AMD driver' }
    if ($result.createStatus -ne 0) { throw "ADL context creation failed: $($result.createStatus)" }
    if ($result.enumerationStatus -ne 0) { throw "ADL adapter enumeration failed: $($result.enumerationStatus)" }
    if ($result.adapterCount -lt 1) { throw 'ADL returned no adapters on the AMD host' }
    if ($result.destroyStatus -ne 0) { throw "ADL context destroy failed: $($result.destroyStatus)" }
    Write-Output 'PASS: AMD ADL context and adapter enumeration succeeded read-only'
}
finally {
    Remove-Item -LiteralPath $resultPath -Force -ErrorAction SilentlyContinue
}
