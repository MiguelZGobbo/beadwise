param(
    [Parameter(Mandatory)] [string]$ResultPath,
    [Parameter(Mandatory)] [string]$EtlPath
)

$ErrorActionPreference = 'Stop'
$started = $false
$status = 'FAIL'
$startOutput = ''
$stopOutput = ''
$failure = $null

try {
    $stateOutput = (& wpr.exe -status 2>&1 | Out-String).Trim()
    if ($stateOutput -notmatch 'not recording') {
        $status = 'BLOCKED_EXISTING_SESSION'
        $failure = 'An ETW/WPR recording was already active; the prototype did not interfere with it.'
    } else {
        $startOutput = (& wpr.exe -start CPU -filemode 2>&1 | Out-String).Trim()
        $startExit = $LASTEXITCODE
        if ($startExit -ne 0) {
            $status = if ($startOutput -match 'access|elevat|administrator|0x80070005|0xc5585011') { 'BLOCKED_ADMIN' } else { 'FAIL' }
            $failure = "WPR start failed with exit code $startExit"
        } else {
            $started = $true
            $until = [DateTime]::UtcNow.AddMilliseconds(500)
            $accumulator = 0.0
            while ([DateTime]::UtcNow -lt $until) { $accumulator += [Math]::Sqrt(($accumulator % 1000) + 1) }
            $stopOutput = (& wpr.exe -stop $EtlPath 2>&1 | Out-String).Trim()
            $stopExit = $LASTEXITCODE
            $started = $false
            if ($stopExit -eq 0 -and (Test-Path -LiteralPath $EtlPath) -and (Get-Item -LiteralPath $EtlPath).Length -gt 0) {
                $status = 'PASS'
            } else {
                $status = 'FAIL'
                $failure = "WPR stop failed with exit code $stopExit or produced no ETL"
            }
        }
    }
}
catch {
    $status = 'FAIL'
    $failure = $_.Exception.Message
}
finally {
    if ($started) { & wpr.exe -cancel 2>&1 | Out-Null }
}

$result = [ordered]@{
    featureId = 'C-CPU-003'
    mechanism = 'ETW_WPR_CPU'
    status = $status
    capturedAt = (Get-Date).ToString('o')
    etlBytes = if (Test-Path -LiteralPath $EtlPath) { (Get-Item -LiteralPath $EtlPath).Length } else { 0 }
    failure = $failure
    startOutput = $startOutput
    stopOutput = $stopOutput
}
$directory = Split-Path -Parent $ResultPath
if ($directory) { New-Item -ItemType Directory -Path $directory -Force | Out-Null }
[IO.File]::WriteAllText($ResultPath, (($result | ConvertTo-Json -Depth 5).Replace("`r`n", "`n")), [Text.UTF8Encoding]::new($false))
Write-Output $ResultPath
