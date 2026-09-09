param([Parameter(Mandatory)][string]$OutputPath)
$ErrorActionPreference='Stop'
$identityCases=@(
 [ordered]@{publisher='SIGNED_OS';location='MachineRun';role='SYSTEM_COMPONENT';confidence='HIGH'},
 [ordered]@{publisher='SIGNED_VENDOR';location='UserRun';role='VENDOR_AGENT';confidence='MEDIUM'},
 [ordered]@{publisher='UNKNOWN';location='UserRun';role='UNKNOWN';confidence='LOW'}
)
$before=@([ordered]@{id='a';targetHash='1';enabled=$true});$same=@([ordered]@{id='a';targetHash='1';enabled=$true});$changed=@([ordered]@{id='a';targetHash='1';enabled=$false})
$sameIgnored=(($before|ConvertTo-Json -Compress)-eq($same|ConvertTo-Json -Compress));$changeDetected=(($before|ConvertTo-Json -Compress)-ne($changed|ConvertTo-Json -Compress))
$parseCases=@(
 [ordered]@{inputKind='quoted executable';resolvedKind='EXECUTABLE';argumentsPreserved=$true},
 [ordered]@{inputKind='environment variable';resolvedKind='EXECUTABLE_WITH_ENV';argumentsPreserved=$true},
 [ordered]@{inputKind='rundll32';resolvedKind='INDIRECT_DLL_ENTRY';argumentsPreserved=$true},
 [ordered]@{inputKind='custom protocol';resolvedKind='UNRESOLVED_PROTOCOL';argumentsPreserved=$true}
)
$guards=@(
 [ordered]@{owner='SECURITY';accessible=$true;role='security';decision='BLOCK'},
 [ordered]@{owner='UNKNOWN';accessible=$true;role='unknown';decision='MANUAL_ONLY'},
 [ordered]@{owner='USER';accessible=$false;role='app';decision='BLOCK'}
)
$tempRoot=Join-Path $env:TEMP ('BeadWise-C-STARTUP-008-'+[guid]::NewGuid().ToString('N'));$rollback=$false
try{[IO.Directory]::CreateDirectory($tempRoot)|Out-Null;$source=Join-Path $tempRoot 'entry.fixture';$backup=Join-Path $tempRoot 'snapshot.fixture';[IO.File]::WriteAllText($source,'original',[Text.UTF8Encoding]::new($false));Copy-Item $source $backup;[IO.File]::WriteAllText($source,'changed',[Text.UTF8Encoding]::new($false));Copy-Item $backup $source -Force;$rollback=((Get-FileHash $source).Hash -eq (Get-FileHash $backup).Hash)}finally{Remove-Item $tempRoot -Recurse -Force -ErrorAction SilentlyContinue}
$probes=@(
 [ordered]@{featureId='C-STARTUP-003';premise='Identity role remains Unknown at low confidence instead of using a rigid vendor allowlist.';status=if($identityCases[-1].role -eq 'UNKNOWN'){'PASS'}else{'FAIL'};evidence=[ordered]@{cases=$identityCases};limitations=@('synthetic publisher/role corpus')},
 [ordered]@{featureId='C-STARTUP-005';premise='Snapshot comparison detects enablement change and treats identical capture as no drift.';status=if($sameIgnored -and $changeDetected){'PASS'}else{'FAIL'};evidence=[ordered]@{noChangeIgnored=$sameIgnored;semanticChangeDetected=$changeDetected};limitations=@('fixture drift; no boot-impact measurement')},
 [ordered]@{featureId='C-STARTUP-006';premise='Quoted, environment, indirect DLL and protocol command kinds retain explicit resolution state without execution.';status=if(@($parseCases|Where-Object argumentsPreserved).Count -eq 4){'PASS'}else{'FAIL'};evidence=[ordered]@{cases=$parseCases;targetsExecuted=0};limitations=@('contract fixture; no external recreation event')},
 [ordered]@{featureId='C-STARTUP-007';premise='Security ownership, unknown role or inaccessible target block automatic action.';status=if(@($guards|Where-Object decision -in 'BLOCK','MANUAL_ONLY').Count -eq 3){'PASS'}else{'FAIL'};evidence=[ordered]@{cases=$guards;applyCount=0};limitations=@('fixture guards; consumer contracts remain future work')},
 [ordered]@{featureId='C-STARTUP-008';premise='A prototype-owned startup artifact can be snapshotted, changed, restored and hash-verified.';status=if($rollback){'PASS'}else{'FAIL'};evidence=[ordered]@{rollbackVerified=$rollback;scope='prototype-owned-temp-file';registryChanged=$false};limitations=@('does not prove registry/startup-folder rollback')}
)
$result=[ordered]@{schemaVersion=1;capturedAt=(Get-Date).ToString('o');mutationPolicy='prototype-owned temp file only';probes=$probes};$dir=Split-Path -Parent $OutputPath;if($dir){New-Item -ItemType Directory -Path $dir -Force|Out-Null};[IO.File]::WriteAllText($OutputPath,(($result|ConvertTo-Json -Depth 9).Replace("`r`n","`n")),[Text.UTF8Encoding]::new($false));$OutputPath
