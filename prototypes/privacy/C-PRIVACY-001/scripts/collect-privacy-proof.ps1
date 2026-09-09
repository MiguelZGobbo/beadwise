param([Parameter(Mandatory)][string]$OutputPath)
$ErrorActionPreference='Stop'
$sources=@(
 [ordered]@{name='TelemetryPolicy';scope='MachinePolicy';path='HKLM:\SOFTWARE\Policies\Microsoft\Windows\DataCollection';value='AllowTelemetry'},
 [ordered]@{name='AdvertisingPolicy';scope='UserPolicy';path='HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo';value='DisabledByGroupPolicy'},
 [ordered]@{name='AppPrivacyPolicy';scope='MachinePolicy';path='HKLM:\SOFTWARE\Policies\Microsoft\Windows\AppPrivacy';value='LetAppsAccessAccountInfo'}
)
$observed=@();foreach($s in $sources){$exists=Test-Path $s.path;$hasValue=$false;if($exists){$hasValue=$null -ne (Get-ItemProperty $s.path -Name $s.value -ErrorAction SilentlyContinue)};$observed+=[ordered]@{name=$s.name;scope=$s.scope;keyPresent=$exists;valuePresent=$hasValue;effectiveState=if($hasValue){'POLICY_PRESENT_EFFECTIVE_VALUE_NOT_INFERRED'}else{'NO_POLICY_VALUE_OBSERVED'}}}
$before=[ordered]@{telemetry='NOT_CONFIGURED';advertising='USER_ENABLED';capturedAt='t1'};$volatile=[ordered]@{telemetry='NOT_CONFIGURED';advertising='USER_ENABLED';capturedAt='t2'};$changed=[ordered]@{telemetry='MANAGED_BASIC';advertising='USER_ENABLED';capturedAt='t3'}
$normalize={param($v) "$($v.telemetry)|$($v.advertising)"};$volatileIgnored=(& $normalize $before)-eq(& $normalize $volatile);$changeDetected=(& $normalize $before)-ne(& $normalize $changed)
$probes=@(
 [ordered]@{featureId='C-PRIVACY-001';premise='Policy presence, scope and effective-state uncertainty can be reported without exporting values or identity.';status='PARTIAL';evidence=[ordered]@{sources=$observed;rawValuesExported=$false;accountExported=$false};limitations=@('no MDM/GPO conflict host; effective runtime behavior not independently queried')},
 [ordered]@{featureId='C-PRIVACY-006';premise='Privacy drift ignores capture time while detecting a semantic managed-state change without sensitive values.';status=if($volatileIgnored -and $changeDetected){'PASS'}else{'FAIL'};evidence=[ordered]@{volatileOnlyIgnored=$volatileIgnored;semanticChangeDetected=$changeDetected;fixtureContainsIdentity=$false};limitations=@('synthetic transition; real registry remains read-only')}
)
$result=[ordered]@{schemaVersion=1;capturedAt=(Get-Date).ToString('o');mutationPolicy='read-only';probes=$probes};$dir=Split-Path -Parent $OutputPath;if($dir){New-Item -ItemType Directory -Path $dir -Force|Out-Null};[IO.File]::WriteAllText($OutputPath,(($result|ConvertTo-Json -Depth 8).Replace("`r`n","`n")),[Text.UTF8Encoding]::new($false));$OutputPath
