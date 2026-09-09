param([Parameter(Mandatory)] [string]$OutputPath)

$ErrorActionPreference = 'Stop'
$first = $null
$second = $null
$firstStopped = $false
$secondRunning = $false
$secondStopped = $false

try {
    $plan = [ordered]@{
        target = 'prototype-owned pwsh child process'
        changes = @('Terminate owned fixture', 'Start equivalent owned fixture')
        adminRequired = $false
        restartRequired = 'Process'
        reversible = $true
    }
    $dryRun = [ordered]@{ mutated = $false; targetExistsBeforeApply = $false }

    $arguments = @('-NoProfile', '-NonInteractive', '-Command', 'Start-Sleep -Seconds 30')
    $first = Start-Process -FilePath (Join-Path $PSHOME 'pwsh.exe') -ArgumentList $arguments -PassThru -WindowStyle Hidden
    if ($first.HasExited) { throw 'First fixture exited before lifecycle test' }
    $firstId = $first.Id
    Stop-Process -Id $firstId -Force
    $first.WaitForExit(5000) | Out-Null
    $firstStopped = $first.HasExited

    $second = Start-Process -FilePath (Join-Path $PSHOME 'pwsh.exe') -ArgumentList $arguments -PassThru -WindowStyle Hidden
    Start-Sleep -Milliseconds 150
    $secondId = $second.Id
    $secondRunning = -not $second.HasExited

    Stop-Process -Id $secondId -Force
    $second.WaitForExit(5000) | Out-Null
    $secondStopped = $second.HasExited

    $result = [ordered]@{
        featureId = 'C-PROCESSES-007'
        capturedAt = (Get-Date).ToString('o')
        scope = 'prototype-owned-process-only'
        plan = $plan
        dryRun = $dryRun
        snapshot = [ordered]@{ originalProcessAbsent = $true }
        apply = [ordered]@{ firstPid = $firstId; secondPid = $secondId }
        verify = [ordered]@{ firstStopped = $firstStopped; secondRunning = $secondRunning }
        rollback = [ordered]@{ secondStopped = $secondStopped; restoredOriginalAbsence = $secondStopped }
    }
}
finally {
    foreach ($process in @($first, $second)) {
        if ($null -ne $process -and -not $process.HasExited) { Stop-Process -Id $process.Id -Force -ErrorAction SilentlyContinue }
    }
}

$directory = Split-Path -Parent $OutputPath
if ($directory) { New-Item -ItemType Directory -Path $directory -Force | Out-Null }
[IO.File]::WriteAllText($OutputPath, (($result | ConvertTo-Json -Depth 7).Replace("`r`n", "`n")), [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
