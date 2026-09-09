param(
    [Parameter(Mandatory = $true)]
    [string] $OutputPath
)

$ErrorActionPreference = 'Stop'

$source = @'
using System.Runtime.InteropServices;

public static class Phase2TimerResolution
{
    [DllImport("ntdll.dll")]
    public static extern int NtQueryTimerResolution(
        out uint maximumResolution,
        out uint minimumResolution,
        out uint currentResolution);
}
'@
if (-not ('Phase2TimerResolution' -as [type])) {
    Add-Type -TypeDefinition $source -Language CSharp
}

$samples = [Collections.Generic.List[object]]::new()
1..20 | ForEach-Object {
    $maximum = 0u
    $minimum = 0u
    $current = 0u
    $status = [Phase2TimerResolution]::NtQueryTimerResolution([ref]$maximum, [ref]$minimum, [ref]$current)
    $samples.Add([ordered]@{
        ntStatus = $status
        maximum100ns = $maximum
        minimum100ns = $minimum
        current100ns = $current
        currentMilliseconds = $current / 10000.0
    })
    Start-Sleep -Milliseconds 20
}
$successfulSamples = @($samples | Where-Object ntStatus -eq 0)
$currentValues = @($successfulSamples | ForEach-Object current100ns | Sort-Object -Unique)

$requestsOutput = & powercfg.exe /requests 2>&1
$requestsExitCode = $LASTEXITCODE
$scratchRoot = Join-Path ([IO.Path]::GetTempPath()) ("beadwise-energy-proof-" + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $scratchRoot | Out-Null
$energyReportPath = Join-Path $scratchRoot 'energy-report.html'
try {
    $energyOutput = & powercfg.exe /energy /duration 1 /output $energyReportPath 2>&1
    $energyExitCode = $LASTEXITCODE
    $energyReportCreated = Test-Path -LiteralPath $energyReportPath
    $energyReportBytes = if ($energyReportCreated) { (Get-Item -LiteralPath $energyReportPath).Length } else { 0 }
}
finally {
    Remove-Item -LiteralPath $scratchRoot -Recurse -Force -ErrorAction SilentlyContinue
}

$querySucceeded = $successfulSamples.Count -eq $samples.Count -and $samples.Count -gt 0
$probeStatus = if ($querySucceeded) { 'PARTIAL' } else { 'FAIL' }
$probe = [ordered]@{
    featureId = 'C-ENERGY-013'
    status = $probeStatus
    mechanism = 'NtQueryTimerResolution + read-only powercfg /requests + bounded powercfg /energy attempt'
    evidence = [ordered]@{
        ntQueryTimerResolution = [ordered]@{
            callSucceeded = $querySucceeded
            sampleCount = $samples.Count
            maximum100ns = $(if ($successfulSamples.Count) { $successfulSamples[0].maximum100ns } else { $null })
            minimum100ns = $(if ($successfulSamples.Count) { $successfulSamples[0].minimum100ns } else { $null })
            current100ns = $(if ($successfulSamples.Count) { $successfulSamples[0].current100ns } else { $null })
            distinctCurrentValues100ns = $currentValues
            samples = $samples
        }
        powercfgRequests = [ordered]@{ exitCode = $requestsExitCode; rawOutput = ($requestsOutput | Out-String).Trim() }
        powercfgEnergy = [ordered]@{ exitCode = $energyExitCode; reportCreated = $energyReportCreated; reportBytes = $energyReportBytes; rawOutput = ($energyOutput | Out-String).Trim(); temporaryReportRemoved = (-not (Test-Path -LiteralPath $scratchRoot)) }
        attribution = [ordered]@{
            status = 'UNSUPPORTED'
            reason = 'No stable structured per-process timer-request attribution API was exercised; localized powercfg text is retained only as raw evidence.'
        }
        mutationAttempted = $false
    }
    limitations = @(
        'Detection of the effective timer resolution passed, but per-process attribution remains unsupported.'
        'No timeBeginPeriod/timeEndPeriod request was issued and no energy-impact claim is inferred.'
    )
}

$document = [ordered]@{
    generatedAt = [DateTimeOffset]::Now.ToString('o')
    safety = [ordered]@{ persistentMutation = $false; timerResolutionMutation = $false; temporaryDirectoryRemoved = (-not (Test-Path -LiteralPath $scratchRoot)) }
    probes = @($probe)
}
$parent = Split-Path -Parent $OutputPath
if ($parent) { New-Item -ItemType Directory -Path $parent -Force | Out-Null }
$document | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath $OutputPath -Encoding utf8
