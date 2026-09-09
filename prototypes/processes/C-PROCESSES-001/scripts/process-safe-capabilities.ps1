param(
    [Parameter(Mandatory = $true)]
    [string] $OutputPath
)

$ErrorActionPreference = 'Stop'

function Start-OwnedPowerShell {
    param([string] $ScriptPath)
    $pwshPath = (Get-Process -Id $PID).Path
    return Start-Process -FilePath $pwshPath -ArgumentList @('-NoProfile', '-File', ('"{0}"' -f $ScriptPath)) -PassThru -WindowStyle Hidden
}

function Stop-OwnedProcess {
    param([Diagnostics.Process] $Process)
    if ($Process -and -not $Process.HasExited) {
        Stop-Process -Id $Process.Id -Force -ErrorAction SilentlyContinue
        $Process.WaitForExit(5000) | Out-Null
    }
}

$scratchRoot = Join-Path ([IO.Path]::GetTempPath()) ("beadwise-process-proof-" + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $scratchRoot | Out-Null
$cpuScript = Join-Path $scratchRoot 'cpu-child.ps1'
$sleepScript = Join-Path $scratchRoot 'sleep-child.ps1'
$lifecycleScript = Join-Path $scratchRoot 'lifecycle-child.ps1'
@'
$deadline = [DateTime]::UtcNow.AddSeconds(4)
$value = 1L
while ([DateTime]::UtcNow -lt $deadline) { $value = (($value * 1103515245L) + 12345L) -band 0x7fffffff }
Start-Sleep -Milliseconds 500
'@ | Set-Content -LiteralPath $cpuScript -Encoding utf8
'Start-Sleep -Seconds 30' | Set-Content -LiteralPath $sleepScript -Encoding utf8
'Start-Sleep -Milliseconds 250; exit 23' | Set-Content -LiteralPath $lifecycleScript -Encoding utf8

$ownedProcesses = [Collections.Generic.List[Diagnostics.Process]]::new()
$probes = [Collections.Generic.List[object]]::new()

try {
    $inventoryChild = Start-OwnedPowerShell -ScriptPath $sleepScript
    $ownedProcesses.Add($inventoryChild)
    Start-Sleep -Milliseconds 250
    $inventoryCim = Get-CimInstance -ClassName Win32_Process -Filter "ProcessId = $($inventoryChild.Id)"
    $parentMatched = $inventoryCim -and [int]$inventoryCim.ParentProcessId -eq $PID
    $inventoryPid = $inventoryChild.Id
    Stop-OwnedProcess -Process $inventoryChild
    Start-Sleep -Milliseconds 100
    $exitObserved = -not [bool](Get-Process -Id $inventoryPid -ErrorAction SilentlyContinue)
    $probes.Add([ordered]@{
        featureId = 'C-PROCESSES-001'; status = $(if ($parentMatched -and $exitObserved) { 'PASS' } else { 'FAIL' });
        mechanism = 'Win32_Process CIM PID/PPID correlation over prototype-owned child';
        evidence = [ordered]@{ pid = $inventoryPid; parentPid = $inventoryCim.ParentProcessId; expectedParentPid = $PID; parentMatched = [bool]$parentMatched; exitObserved = $exitObserved; executableName = [IO.Path]::GetFileName($inventoryCim.ExecutablePath) }
        limitations = @('Protected-process full metadata and packaged-app grouping remain untested.')
    })

    $trendChild = Start-OwnedPowerShell -ScriptPath $cpuScript
    $ownedProcesses.Add($trendChild)
    $samples = [Collections.Generic.List[object]]::new()
    $previousCpu = $null
    $previousAt = $null
    1..5 | ForEach-Object {
        Start-Sleep -Milliseconds 300
        $sampledAt = [DateTimeOffset]::Now
        $process = Get-Process -Id $trendChild.Id -ErrorAction Stop
        $process.Refresh()
        $cpuSeconds = [double]$process.CPU
        $deltaCpu = if ($null -ne $previousCpu) { $cpuSeconds - $previousCpu } else { $null }
        $deltaWall = if ($null -ne $previousAt) { ($sampledAt - $previousAt).TotalSeconds } else { $null }
        $samples.Add([ordered]@{
            sampledAt = $sampledAt.ToString('o'); cpuSeconds = $cpuSeconds; workingSetBytes = [long]$process.WorkingSet64;
            cpuDeltaSeconds = $deltaCpu; wallDeltaSeconds = $deltaWall;
            normalizedCpuPercent = $(if ($null -ne $deltaCpu -and $deltaWall -gt 0) { 100 * $deltaCpu / $deltaWall / [Environment]::ProcessorCount } else { $null })
        })
        $previousCpu = $cpuSeconds
        $previousAt = $sampledAt
    }
    $trendStatus = if (@($samples | Where-Object { $null -ne $_.cpuDeltaSeconds -and $_.cpuDeltaSeconds -gt 0 }).Count -gt 0) { 'PASS' } else { 'PARTIAL' }
    $probes.Add([ordered]@{
        featureId = 'C-PROCESSES-002'; status = $trendStatus;
        mechanism = 'Get-Process temporal CPU/working-set deltas on bounded owned workload';
        evidence = [ordered]@{ pid = $trendChild.Id; logicalProcessorCount = [Environment]::ProcessorCount; samples = $samples }
        limitations = @('Five short samples; no sustained load, GPU attribution, or long-duration ETW session.')
    })
    Stop-OwnedProcess -Process $trendChild

    $lifecycleChild = Start-OwnedPowerShell -ScriptPath $lifecycleScript
    $ownedProcesses.Add($lifecycleChild)
    $startedPid = $lifecycleChild.Id
    $detectedRunning = [bool](Get-Process -Id $startedPid -ErrorAction SilentlyContinue)
    $lifecycleChild.WaitForExit(5000) | Out-Null
    $exitCode = $lifecycleChild.ExitCode
    $goneAfterExit = -not [bool](Get-Process -Id $startedPid -ErrorAction SilentlyContinue)
    $probes.Add([ordered]@{
        featureId = 'C-PROCESSES-004'; status = $(if ($detectedRunning -and $goneAfterExit -and $exitCode -eq 23) { 'PASS' } else { 'FAIL' });
        mechanism = 'Prototype-owned child start/detect/exit lifecycle';
        evidence = [ordered]@{ pid = $startedPid; detectedRunning = $detectedRunning; exitObserved = $goneAfterExit; exitCode = $exitCode; expectedExitCode = 23 }
        limitations = @('Console lifecycle only; GUI responsiveness, crash dumps, and protected apps remain untested.')
    })

    $policyChild = Start-OwnedPowerShell -ScriptPath $sleepScript
    $ownedProcesses.Add($policyChild)
    Start-Sleep -Milliseconds 200
    $policyChild.Refresh()
    $originalPriority = $policyChild.PriorityClass
    $originalAffinity = $policyChild.ProcessorAffinity.ToInt64()
    $lowestAffinity = $originalAffinity -band (-$originalAffinity)
    $policyError = $null
    $applied = $false
    $restored = $false
    try {
        $policyChild.PriorityClass = [Diagnostics.ProcessPriorityClass]::BelowNormal
        $policyChild.ProcessorAffinity = [IntPtr]::new($lowestAffinity)
        $policyChild.Refresh()
        $applied = $policyChild.PriorityClass -eq [Diagnostics.ProcessPriorityClass]::BelowNormal -and $policyChild.ProcessorAffinity.ToInt64() -eq $lowestAffinity
    }
    catch {
        $policyError = $_.Exception.Message
    }
    finally {
        try {
            $policyChild.PriorityClass = $originalPriority
            $policyChild.ProcessorAffinity = [IntPtr]::new($originalAffinity)
            $policyChild.Refresh()
            $restored = $policyChild.PriorityClass -eq $originalPriority -and $policyChild.ProcessorAffinity.ToInt64() -eq $originalAffinity
        }
        catch {
            $policyError = @($policyError, $_.Exception.Message) -join ' | '
        }
    }
    $probes.Add([ordered]@{
        featureId = 'C-PROCESSES-005'; status = $(if ($applied -and $restored) { 'PASS' } elseif ($restored) { 'PARTIAL' } else { 'FAIL' });
        mechanism = 'System.Diagnostics.Process priority/affinity on prototype-owned child with restoration';
        evidence = [ordered]@{ pid = $policyChild.Id; originalPriority = $originalPriority.ToString(); requestedPriority = 'BelowNormal'; originalAffinity = $originalAffinity; requestedAffinity = $lowestAffinity; applied = $applied; restored = $restored; error = $policyError }
        limitations = @('No mutation of user/protected processes; PowerThrottling, memory priority, and CPU Sets remain untested.')
    })

    $policyChild.Refresh()
    $threadCount = @($policyChild.Threads).Count
    $handleCount = $policyChild.HandleCount
    $protectedOutcome = [ordered]@{ pid = 4; status = 'UNAVAILABLE'; detail = $null }
    try {
        $systemProcess = Get-Process -Id 4 -ErrorAction Stop
        try {
            $protectedOutcome.status = 'READABLE'
            $protectedOutcome.detail = [ordered]@{ threadCount = @($systemProcess.Threads).Count; handleCount = $systemProcess.HandleCount }
        }
        catch [System.ComponentModel.Win32Exception] {
            $protectedOutcome.status = 'ACCESS_DENIED'
            $protectedOutcome.detail = $_.Exception.Message
        }
    }
    catch {
        $protectedOutcome.status = 'UNAVAILABLE'
        $protectedOutcome.detail = $_.Exception.Message
    }
    $probes.Add([ordered]@{
        featureId = 'C-PROCESSES-006'; status = $(if ($threadCount -gt 0 -and $handleCount -gt 0) { 'PARTIAL' } else { 'FAIL' });
        mechanism = 'Get-Process thread/handle inspection on owned child + read-only protected-process outcome';
        evidence = [ordered]@{ pid = $policyChild.Id; threadCount = $threadCount; handleCount = $handleCount; protectedProcessQuery = $protectedOutcome }
        limitations = @('No induced contention, deadlock, or kernel handle leak; no AccessDenied claim unless observed.')
    })
}
finally {
    foreach ($owned in $ownedProcesses) { Stop-OwnedProcess -Process $owned }
    Remove-Item -LiteralPath $scratchRoot -Recurse -Force -ErrorAction SilentlyContinue
}

$document = [ordered]@{
    generatedAt = [DateTimeOffset]::Now.ToString('o')
    safety = [ordered]@{ userProcessMutation = $false; onlyOwnedChildProcesses = $true; temporaryDirectoryRemoved = (-not (Test-Path -LiteralPath $scratchRoot)) }
    probes = $probes
}
$parent = Split-Path -Parent $OutputPath
if ($parent) { New-Item -ItemType Directory -Path $parent -Force | Out-Null }
$document | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath $OutputPath -Encoding utf8
