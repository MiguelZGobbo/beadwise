param(
    [string]$OutputPath = (Join-Path (Split-Path -Parent $PSScriptRoot) 'results\process-restart-proof.json')
)

$ErrorActionPreference = 'Stop'

$marker = "BeadWisePhase2-$([guid]::NewGuid().ToString('N'))"
$steps = [Collections.Generic.List[object]]::new()

function Add-Step {
    param([string]$Name, [string]$Status, [string]$Detail)
    $steps.Add([ordered]@{ name = $Name; status = $Status; detail = $Detail })
}

function Start-ProofProcess {
    param([string]$Marker)

    Start-Process -FilePath pwsh -ArgumentList @(
        '-NoProfile',
        '-Command',
        "`$env:BEADWISE_PHASE2_PROCESS_MARKER='$Marker'; Start-Sleep -Seconds 300"
    ) -WindowStyle Hidden -PassThru
}

$firstProcess = $null
$secondProcess = $null
try {
    $firstProcess = Start-ProofProcess -Marker $marker
    Start-Sleep -Milliseconds 750
    $detected = Get-Process -Id $firstProcess.Id -ErrorAction SilentlyContinue
    if ($detected) {
        Add-Step 'DETECT' 'PASS' "Controlled process is running with PID $($firstProcess.Id)."
    }
    else {
        Add-Step 'DETECT' 'FAIL' 'Controlled process was not detected after start.'
    }

    $plan = [ordered]@{
        featureId = 'C-PROCESSES-007'
        targetPid = $firstProcess.Id
        action = 'Terminate controlled child process and start a replacement'
        protectedProcess = $false
        adminRequired = $false
        restartRequired = 'Process restart only'
        rollback = 'Terminate replacement process during cleanup'
    }
    Add-Step 'PLAN' 'PASS' 'Plan targets only a controlled child process.'

    Stop-Process -Id $firstProcess.Id -Force -ErrorAction Stop
    Add-Step 'APPLY_TERMINATE' 'PASS' "Stopped controlled process PID $($firstProcess.Id)."

    Start-Sleep -Milliseconds 750
    $stopped = $null -eq (Get-Process -Id $firstProcess.Id -ErrorAction SilentlyContinue)
    if ($stopped) {
        Add-Step 'VERIFY_STOPPED' 'PASS' 'Controlled process is no longer running.'
    }
    else {
        Add-Step 'VERIFY_STOPPED' 'FAIL' 'Controlled process still appears in the process table.'
    }

    $secondProcess = Start-ProofProcess -Marker $marker
    Add-Step 'RESTART' 'PASS' "Started replacement process PID $($secondProcess.Id)."

    Start-Sleep -Milliseconds 750
    $running = Get-Process -Id $secondProcess.Id -ErrorAction SilentlyContinue
    if ($running) {
        Add-Step 'VERIFY_RUNNING' 'PASS' 'Replacement process is running.'
    }
    else {
        Add-Step 'VERIFY_RUNNING' 'FAIL' 'Replacement process was not detected.'
    }
}
finally {
    foreach ($process in @($firstProcess, $secondProcess)) {
        if ($null -ne $process) {
            Stop-Process -Id $process.Id -Force -ErrorAction SilentlyContinue
        }
    }
}

$result = [ordered]@{
    schemaVersion = 1
    capturedAt = (Get-Date).ToString('o')
    featureId = 'C-PROCESSES-007'
    marker = $marker
    steps = @($steps)
}

$outputDirectory = Split-Path -Parent $OutputPath
New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
$json = ($result | ConvertTo-Json -Depth 8).Replace("`r`n", "`n")
[IO.File]::WriteAllText($OutputPath, $json, [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
