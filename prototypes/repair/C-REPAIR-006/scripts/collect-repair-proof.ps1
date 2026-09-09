param([Parameter(Mandatory)][string]$OutputPath)
$ErrorActionPreference='Stop'
$dispatch=@(
 [ordered]@{symptom='component-store';owner='C-REPAIR-002';genericRepairAllowed=$false},
 [ordered]@{symptom='store-app';owner='C-REPAIR-005';genericRepairAllowed=$false},
 [ordered]@{symptom='device-driver';owner='C-REPAIR-007';genericRepairAllowed=$false},
 [ordered]@{symptom='subsystem';owner='C-REPAIR-006';genericRepairAllowed=$false}
)
$history=@(
 [ordered]@{time=1;operation='detect';result='FAIL';stateHash='a'},
 [ordered]@{time=2;operation='repair';result='PARTIAL';stateHash='b'},
 [ordered]@{time=3;operation='verify';result='PASS';stateHash='c'}
)
$sources=[ordered]@{dismAvailable=Test-Path "$env:SystemRoot\System32\dism.exe";sfcAvailable=Test-Path "$env:SystemRoot\System32\sfc.exe";installationMediaAvailable=$false;networkSourceConfigured=$false}
$gates=@(
 [ordered]@{admin=$false;snapshot='PASS';pendingReboot=$false;result='NO'},
 [ordered]@{admin=$true;snapshot='FAIL';pendingReboot=$false;result='NO'},
 [ordered]@{admin=$true;snapshot='PASS';pendingReboot=$true;result='PARTIAL'},
 [ordered]@{admin=$true;snapshot='PASS';pendingReboot=$false;result='FULL'}
)
$probes=@(
 [ordered]@{featureId='C-REPAIR-006';premise='Subsystem symptoms dispatch to an explicit owner and never to a generic repair pack.';status=if(@($dispatch|Where-Object genericRepairAllowed).Count -eq 0){'PASS'}else{'FAIL'};evidence=[ordered]@{cases=$dispatch};limitations=@('contract fixture; delegated repair implementations not executed')},
 [ordered]@{featureId='C-REPAIR-008';premise='Repair history distinguishes detect, partial apply and verified known-good state.';status=if($history[-1].operation -eq 'verify' -and $history[-1].result -eq 'PASS'){'PASS'}else{'FAIL'};evidence=[ordered]@{timeline=$history;causationInferred=$false};limitations=@('fixture history; no real incident')},
 [ordered]@{featureId='C-REPAIR-009';premise='Repair-source availability is detected before escalation and missing media remains explicit.';status='PARTIAL';evidence=$sources;limitations=@('no controlled installation media or network repair source')},
 [ordered]@{featureId='C-REPAIR-010';premise='Admin, snapshot and reboot gates degrade rollback readiness to FULL/PARTIAL/NO explicitly.';status=if(@($gates.result|Sort-Object -Unique).Count -eq 3){'PASS'}else{'FAIL'};evidence=[ordered]@{cases=$gates;repairAttempted=$false};limitations=@('fixture gates; no real repair integration')}
)
$result=[ordered]@{schemaVersion=1;capturedAt=(Get-Date).ToString('o');mutationPolicy='read-only plus fixtures';commandsExecuted=@();probes=$probes};$dir=Split-Path -Parent $OutputPath;if($dir){New-Item -ItemType Directory -Path $dir -Force|Out-Null};[IO.File]::WriteAllText($OutputPath,(($result|ConvertTo-Json -Depth 9).Replace("`r`n","`n")),[Text.UTF8Encoding]::new($false));$OutputPath
