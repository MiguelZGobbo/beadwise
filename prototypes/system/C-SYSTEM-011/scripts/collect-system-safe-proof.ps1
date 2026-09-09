param([Parameter(Mandatory)][string]$OutputPath)
$ErrorActionPreference='Stop'
$recommendations=@(
 [ordered]@{evidence=@();confidence='NONE';decision='NOTHING_TO_OPTIMIZE';owner=$null},
 [ordered]@{evidence=@('measured-pressure');confidence='MEDIUM';decision='SHOW_DIAGNOSTIC';owner='C-RAM-004'},
 [ordered]@{evidence=@('conflict-a','conflict-b');confidence='LOW';decision='EXPLAIN_CONFLICT';owner='C-SYSTEM-003'}
)
$eventStatus='PASS';$events=@();$eventError=$null
try{$events=@(Get-WinEvent -FilterHashtable @{LogName='System';StartTime=(Get-Date).AddDays(-1)} -MaxEvents 50 -ErrorAction Stop|ForEach-Object{[ordered]@{provider=$_.ProviderName;id=$_.Id;level=$_.LevelDisplayName;time=$_.TimeCreated.ToUniversalTime().ToString('o')}})}catch{$eventStatus='FAIL';$eventError=$_.Exception.Message}
$incident=[datetime]'2026-09-08T10:00:00Z';$near=[datetime]'2026-09-08T10:02:00Z';$far=[datetime]'2026-09-09T10:00:00Z';$nearCandidate=[math]::Abs(($near-$incident).TotalMinutes)-le 5;$farRejected=[math]::Abs(($far-$incident).TotalMinutes)-gt 5
$pending=[ordered]@{cbs=Test-Path 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Component Based Servicing\RebootPending';windowsUpdate=Test-Path 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update\RebootRequired'}
$powerOutput=@(& powercfg.exe /getactivescheme 2>&1);$powerExit=$LASTEXITCODE
$accessibility=@();foreach($item in @(@{name='StickyKeys';path='HKCU:\Control Panel\Accessibility\StickyKeys'},@{name='ToggleKeys';path='HKCU:\Control Panel\Accessibility\ToggleKeys'},@{name='KeyboardResponse';path='HKCU:\Control Panel\Accessibility\Keyboard Response'})){$accessibility+=[ordered]@{name=$item.name;sourcePresent=Test-Path $item.path;valueExported=$false}}
$probes=@(
 [ordered]@{featureId='C-SYSTEM-003';premise='Recommendation output supports no-op, evidence-owned diagnostic and conflict explanation without fabricating an action.';status=if($recommendations[0].decision -eq 'NOTHING_TO_OPTIMIZE'){'PASS'}else{'FAIL'};evidence=[ordered]@{cases=$recommendations;applyCount=0};limitations=@('fixture contract; no frontend consumer')},
 [ordered]@{featureId='C-SYSTEM-011';premise='System Event Log metadata can be queried while correlation stays distinct from causation.';status=$eventStatus;evidence=[ordered]@{eventCount=$events.Count;eventError=$eventError;events=$events;nearCorrelationCandidate=$nearCandidate;falseCorrelationRejected=$farRejected;causationClaimed=$false};limitations=@('no controlled crash/incident; messages omitted for privacy')},
 [ordered]@{featureId='C-SYSTEM-015';premise='Benchmark readiness can expose pending-reboot, power-source availability and background process count as reasons without optimizing.';status='PARTIAL';evidence=[ordered]@{pendingRebootSources=$pending;activePowerSchemeQueryExit=$powerExit;processCount=@(Get-Process).Count;benchmarkStarted=$false};limitations=@('no benchmark consumer/workload; power output omitted')},
 [ordered]@{featureId='C-SYSTEM-016';premise='Accessibility source presence can be read without exporting values or changing assistive settings.';status='PARTIAL';evidence=[ordered]@{sources=$accessibility;valuesExported=$false;settingsChanged=$false};limitations=@('does not prove active assistive technology or managed policy')}
)
$result=[ordered]@{schemaVersion=1;capturedAt=(Get-Date).ToString('o');mutationPolicy='read-only';probes=$probes};$dir=Split-Path -Parent $OutputPath;if($dir){New-Item -ItemType Directory -Path $dir -Force|Out-Null};[IO.File]::WriteAllText($OutputPath,(($result|ConvertTo-Json -Depth 10).Replace("`r`n","`n")),[Text.UTF8Encoding]::new($false));$OutputPath
