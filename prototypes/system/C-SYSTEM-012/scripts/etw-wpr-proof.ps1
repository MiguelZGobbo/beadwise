param(
    [string]$OutputPath = (Join-Path (Split-Path -Parent $PSScriptRoot) 'results\etw-wpr-proof.json')
)

$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$artifactsDirectory = Join-Path $prototypeRoot 'artifacts'
$tracePath = Join-Path $artifactsDirectory 'c-system-012-wpr-smoke.etl'
$steps = [Collections.Generic.List[object]]::new()

function Add-Step {
    param([string]$Name, [string]$Status, [string]$Detail)
    $steps.Add([ordered]@{ name = $Name; status = $Status; detail = $Detail })
}

$wpr = Get-Command wpr.exe -ErrorAction SilentlyContinue
if ($null -eq $wpr) {
    Add-Step 'WPR_PRESENT' 'FAIL' 'wpr.exe was not found.'
    $statusOutput = $null
    $profilesOutput = $null
}
else {
    Add-Step 'WPR_PRESENT' 'PASS' $wpr.Source
    $statusOutput = (& wpr.exe -status 2>&1 | Out-String).Trim()
    if ($LASTEXITCODE -eq 0) {
        Add-Step 'WPR_STATUS' 'PASS' $statusOutput
    }
    else {
        Add-Step 'WPR_STATUS' 'FAIL' "wpr -status exit code $LASTEXITCODE"
    }

    $profilesOutput = (& wpr.exe -profiles 2>&1 | Out-String).Trim()
    if ($LASTEXITCODE -eq 0 -and $profilesOutput -match 'GeneralProfile') {
        Add-Step 'WPR_PROFILES' 'PASS' 'GeneralProfile is listed.'
    }
    else {
        Add-Step 'WPR_PROFILES' 'FAIL' "wpr -profiles exit code $LASTEXITCODE"
    }

    New-Item -ItemType Directory -Path $artifactsDirectory -Force | Out-Null
    $started = $false
    try {
        $startOutput = (& wpr.exe -start GeneralProfile -filemode 2>&1 | Out-String).Trim()
        $startExit = $LASTEXITCODE
        if ($startExit -eq 0) {
            $started = $true
            Start-Sleep -Seconds 2
            $stopOutput = (& wpr.exe -stop $tracePath 2>&1 | Out-String).Trim()
            $stopExit = $LASTEXITCODE
            if ($stopExit -eq 0 -and (Test-Path -LiteralPath $tracePath)) {
                Add-Step 'TRACE_START_STOP' 'PASS' "Trace saved to $tracePath"
            }
            else {
                Add-Step 'TRACE_START_STOP' 'FAIL' "wpr -stop exit code $stopExit; output: $stopOutput"
            }
        }
        else {
            Add-Step 'TRACE_START_STOP' 'FAIL' "wpr -start exit code $startExit; output: $startOutput"
        }
    }
    finally {
        if ($started) {
            $statusAfter = (& wpr.exe -status 2>&1 | Out-String).Trim()
            if ($statusAfter -notmatch 'not recording|não está gravando') {
                & wpr.exe -cancel | Out-Null
            }
        }
    }
}

$result = [ordered]@{
    schemaVersion = 1
    capturedAt = (Get-Date).ToString('o')
    featureId = 'C-SYSTEM-012'
    wprPath = if ($wpr) { $wpr.Source } else { $null }
    statusOutput = $statusOutput
    profilesOutput = $profilesOutput
    tracePath = if (Test-Path -LiteralPath $tracePath) { $tracePath } else { $null }
    steps = @($steps)
}

$outputDirectory = Split-Path -Parent $OutputPath
New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
$json = ($result | ConvertTo-Json -Depth 8).Replace("`r`n", "`n")
[IO.File]::WriteAllText($OutputPath, $json, [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
