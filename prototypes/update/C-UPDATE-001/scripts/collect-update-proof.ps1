param([Parameter(Mandatory)][string]$OutputPath)
$ErrorActionPreference='Stop'

$historyStatus='UNSUPPORTED';$history=@();$historyError=$null
try{$session=New-Object -ComObject Microsoft.Update.Session -ErrorAction Stop;$searcher=$session.CreateUpdateSearcher();$total=$searcher.GetTotalHistoryCount();$take=[math]::Min($total,20);if($take){$history=@($searcher.QueryHistory(0,$take)|ForEach-Object{[ordered]@{date=$_.Date.ToUniversalTime().ToString('o');operation=[int]$_.Operation;resultCode=[int]$_.ResultCode;hresult=('0x{0:X8}' -f ($_.HResult -band 0xffffffffL))}})};$historyStatus='PASS'}catch{$historyStatus='FAIL';$historyError=$_.Exception.Message}
$pending=[ordered]@{
 componentBasedServicing=Test-Path 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Component Based Servicing\RebootPending'
 windowsUpdate=Test-Path 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update\RebootRequired'
 pendingFileRename=[bool](Get-ItemProperty 'HKLM:\SYSTEM\CurrentControlSet\Control\Session Manager' -Name PendingFileRenameOperations -ErrorAction SilentlyContinue)
}
$os=Get-CimInstance Win32_OperatingSystem -ErrorAction Stop
$services=@(Get-Service usosvc,wuauserv,bits -ErrorAction SilentlyContinue|Select-Object Name,Status,StartType)
$systemVolume=Get-Volume -DriveLetter $env:SystemDrive.TrimEnd(':') -ErrorAction Stop
$policyPaths='HKLM:\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate','HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate'
$policy=@();foreach($path in $policyPaths){if(Test-Path $path){$item=Get-ItemProperty $path;$policy+=[ordered]@{path=$path;present=$true;propertyNames=@($item.PSObject.Properties.Name|Where-Object{$_ -notmatch '^PS'})}}else{$policy+=[ordered]@{path=$path;present=$false;propertyNames=@()}}}
$timeline=@(
 [ordered]@{kind='update';time=[datetime]'2026-09-08T10:00:00Z';id='u1'},
 [ordered]@{kind='incident';time=[datetime]'2026-09-08T10:03:00Z';id='i-near'},
 [ordered]@{kind='incident';time=[datetime]'2026-09-10T10:00:00Z';id='i-far'}
)
$near=[math]::Abs(($timeline[1].time-$timeline[0].time).TotalMinutes)-le 30
$far=[math]::Abs(($timeline[2].time-$timeline[0].time).TotalMinutes)-le 30
$probes=@(
 [ordered]@{featureId='C-UPDATE-001';premise='Update Agent history and pending-restart sources are reported independently.';status=$historyStatus;evidence=[ordered]@{historyCount=$history.Count;historyError=$historyError;pendingSources=$pending;searchInvoked=$false};limitations=@('no currently pending update/restart scenario')},
 [ordered]@{featureId='C-UPDATE-003';premise='Servicing readiness preserves build, service state and source availability without running repair or scan.';status='PASS';evidence=[ordered]@{build=[string]$os.BuildNumber;edition=[string]$os.Caption;services=$services;updateAgentHistoryAvailable=($historyStatus -eq 'PASS')};limitations=@('no managed or failed servicing fixture on this host')},
 [ordered]@{featureId='C-UPDATE-006';premise='System-volume free space can be represented separately from an unknown update payload requirement.';status='PARTIAL';evidence=[ordered]@{freeBytes=[uint64]$systemVolume.SizeRemaining;sizeBytes=[uint64]$systemVolume.Size;payloadRequirementBytes=$null;classification='UNKNOWN_REQUIREMENT'};limitations=@('no pending payload metadata; low-space case is fixture-only')},
 [ordered]@{featureId='C-UPDATE-008';premise='Temporal proximity may be correlated but a far incident must be rejected and neither establishes causation.';status=if($near -and -not $far){'PASS'}else{'FAIL'};evidence=[ordered]@{nearCorrelationCandidate=$near;falseCorrelationRejected=(-not $far);causationClaimed=$false;actualHistoryEntries=$history.Count};limitations=@('no real regression or rollback operation')},
 [ordered]@{featureId='C-UPDATE-010';premise='Managed policy presence is reported separately from effective Update Agent behavior.';status='PARTIAL';evidence=[ordered]@{policySources=$policy;effectiveOwner='UNKNOWN_WITHOUT_MDM_OR_WSUS';updateAgentReachable=($historyStatus -eq 'PASS')};limitations=@('no WSUS/MDM managed host')}
)
$result=[ordered]@{schemaVersion=1;capturedAt=(Get-Date).ToString('o');mutationPolicy='read-only';probes=$probes}
$dir=Split-Path -Parent $OutputPath;if($dir){New-Item -ItemType Directory -Path $dir -Force|Out-Null};[IO.File]::WriteAllText($OutputPath,(($result|ConvertTo-Json -Depth 10).Replace("`r`n","`n")),[Text.UTF8Encoding]::new($false));$OutputPath
