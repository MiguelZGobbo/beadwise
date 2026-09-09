param([Parameter(Mandatory)][string]$OutputPath)
$ErrorActionPreference='Stop'

$physical=@(Get-PhysicalDisk -ErrorAction Stop)
$disks=@(Get-Disk -ErrorAction Stop)
$volumes=@(Get-Volume -ErrorAction Stop)
$reliability=@()
foreach($disk in $physical){
 try{$c=$disk|Get-StorageReliabilityCounter -ErrorAction Stop;$reliability+=[ordered]@{name=$disk.FriendlyName;status='PASS';temperature=$c.Temperature;wear=$c.Wear;readErrors=$c.ReadErrorsTotal;writeErrors=$c.WriteErrorsTotal}}
 catch{$reliability+=[ordered]@{name=$disk.FriendlyName;status=if($_.Exception.Message -match 'Access|Acesso'){'FAIL'}else{'UNSUPPORTED'};error=$_.Exception.Message}}
}
$systemDrive=[string]$env:SystemDrive
$scanOutput=@();$scanExit=$null
try{
 $scanInfo=[Diagnostics.ProcessStartInfo]::new('chkdsk.exe',"$systemDrive /scan")
 $scanInfo.UseShellExecute=$false;$scanInfo.CreateNoWindow=$true;$scanInfo.RedirectStandardOutput=$true;$scanInfo.RedirectStandardError=$true
 $scanProcess=[Diagnostics.Process]::new();$scanProcess.StartInfo=$scanInfo;$null=$scanProcess.Start();$scanStdout=$scanProcess.StandardOutput.ReadToEnd();$scanStderr=$scanProcess.StandardError.ReadToEnd();$scanProcess.WaitForExit();$scanExit=$scanProcess.ExitCode
 $scanOutput=@(($scanStdout+"`n"+$scanStderr)-split "`r?`n"|Where-Object{$_})
}catch{$scanExit=$_.Exception.HResult;$scanOutput=@('ADMIN_REQUIRED_OR_ACCESS_DENIED: '+$_.Exception.Message)}
$trimOutput=@(& fsutil.exe behavior query DisableDeleteNotify 2>&1);$trimExit=$LASTEXITCODE
$counterStatus='UNSUPPORTED';$counterEvidence=[ordered]@{reason='Physical Disk counter unavailable'}
try{
 $set=Get-Counter -ListSet * -ErrorAction Stop|Where-Object{$_.CounterSetName -in 'PhysicalDisk','Disco Físico'}|Select-Object -First 1
 $path=@($set.Paths|Where-Object{$_ -match 'Disk Bytes/sec|Bytes de Disco/s'}|Select-Object -First 1)
 if($path){$sample=Get-Counter $path -SampleInterval 1 -MaxSamples 3 -ErrorAction Stop;$values=@($sample.CounterSamples.CookedValue);$counterStatus='PASS';$counterEvidence=[ordered]@{path=$path;sampleCount=$values.Count;maximumBytesPerSecond=($values|Measure-Object -Maximum).Maximum}}
}catch{$counterStatus='FAIL';$counterEvidence=[ordered]@{error=$_.Exception.Message}}

$tempRoot=Join-Path $env:TEMP ('BeadWise-C-STORAGE-011-'+[guid]::NewGuid().ToString('N'))
$rollbackVerified=$false;$snapshotHash=$null;$restoredHash=$null
try{
 $source=Join-Path $tempRoot 'source';$quarantine=Join-Path $tempRoot 'quarantine';[IO.Directory]::CreateDirectory($source)|Out-Null;[IO.Directory]::CreateDirectory($quarantine)|Out-Null
 $artifact=Join-Path $source 'owned.tmp';[IO.File]::WriteAllText($artifact,'beadwise-owned-proof',[Text.UTF8Encoding]::new($false));$snapshotHash=(Get-FileHash $artifact -Algorithm SHA256).Hash
 $destination=Join-Path $quarantine 'owned.tmp';Move-Item -LiteralPath $artifact -Destination $destination
 if(Test-Path $artifact){throw 'quarantine move failed'}
 Move-Item -LiteralPath $destination -Destination $artifact;$restoredHash=(Get-FileHash $artifact -Algorithm SHA256).Hash;$rollbackVerified=($restoredHash -eq $snapshotHash)
}finally{Remove-Item -LiteralPath $tempRoot -Recurse -Force -ErrorAction SilentlyContinue}

$guardCases=@(
 [ordered]@{claim='disable write-cache flushing';documentedBenefit=$false;safeRollback=$false;decision='BLOCK'},
 [ordered]@{claim='disable last-access updates universally';documentedBenefit=$false;safeRollback=$true;decision='BLOCK'},
 [ordered]@{claim='analyze current media';documentedBenefit=$true;safeRollback=$true;decision='READ_ONLY_ANALYZE'}
)
$healthPass=@($reliability|Where-Object status -eq 'PASS').Count;$healthFail=@($reliability|Where-Object status -eq 'FAIL').Count
$probes=@(
 [ordered]@{featureId='C-STORAGE-001';premise='Storage hardware, media, bus and volume identity are available from structured Storage cmdlets.';status=if($physical.Count -and $disks.Count){'PASS'}else{'PARTIAL'};evidence=[ordered]@{physicalDiskCount=$physical.Count;diskCount=$disks.Count;volumeCount=$volumes.Count;mediaTypes=@($physical.MediaType|Sort-Object -Unique);busTypes=@($disks.BusType|ForEach-Object{[string]$_}|Sort-Object -Unique)};limitations=@('no negotiated PCIe speed or cross-vendor firmware catalogue')},
 [ordered]@{featureId='C-STORAGE-002';premise='Reliability counters return measured fields or preserve access/unsupported failure per disk.';status=if($healthPass){'PASS'}elseif($healthFail){'FAIL'}else{'UNSUPPORTED'};evidence=[ordered]@{devices=$reliability};limitations=@('no time series or vendor ATA attribute map')},
 [ordered]@{featureId='C-STORAGE-003';premise='Temperature is reported only when the reliability provider exposes it.';status=if(@($reliability|Where-Object{$_.status -eq 'PASS' -and $null -ne $_.temperature}).Count){'PASS'}elseif($healthFail){'FAIL'}else{'UNSUPPORTED'};evidence=[ordered]@{temperatureSources=@($reliability|ForEach-Object{[ordered]@{name=$_.name;status=$_.status;temperature=$_.temperature}})};limitations=@('no controlled thermal event')},
 [ordered]@{featureId='C-STORAGE-004';premise='Volume capacity/free space can be read without hydrating cloud placeholders.';status=if($volumes.Count){'PASS'}else{'FAIL'};evidence=[ordered]@{volumeCount=$volumes.Count;filesystems=@($volumes.FileSystemType|ForEach-Object{[string]$_}|Sort-Object -Unique);placeholderFilesOpened=0};limitations=@('no cloud-provider placeholder fixture')},
 [ordered]@{featureId='C-STORAGE-005';premise='Online filesystem scan preserves its real exit code and permission failure without invoking repair.';status=if($scanExit -eq 0){'PASS'}else{'FAIL'};evidence=[ordered]@{command='chkdsk <system-drive> /scan';exitCode=$scanExit;repairSwitchUsed=$false;outputTail=@($scanOutput|Select-Object -Last 4)};limitations=@('single NTFS system volume; no ReFS or corrupt filesystem')},
 [ordered]@{featureId='C-STORAGE-006';premise='TRIM policy capability can be queried without issuing retrim.';status=if($trimExit -eq 0){'PASS'}else{'FAIL'};evidence=[ordered]@{command='fsutil behavior query DisableDeleteNotify';exitCode=$trimExit;retrimInvoked=$false;lines=@($trimOutput)};limitations=@('does not prove end-to-end TRIM through RAID/USB')},
 [ordered]@{featureId='C-STORAGE-009';premise='A localized PDH-backed physical-disk throughput counter can be sampled temporally.';status=$counterStatus;evidence=$counterEvidence;limitations=@('three read-only samples; no controlled I/O saturation')},
 [ordered]@{featureId='C-STORAGE-011';premise='An owned temporary artifact can be snapshotted, quarantined, verified and rolled back byte-for-byte.';status=if($rollbackVerified){'PASS'}else{'FAIL'};evidence=[ordered]@{snapshotSha256=$snapshotHash;restoredSha256=$restoredHash;rollbackVerified=$rollbackVerified;scope='prototype-owned-temp-directory'};limitations=@('does not authorize cleanup of any real user/system artifact')},
 [ordered]@{featureId='C-STORAGE-020';premise='Low-value or unsafe optimization claims are blocked while read-only analysis remains permitted.';status=if(@($guardCases|Where-Object decision -eq 'BLOCK').Count -eq 2){'PASS'}else{'FAIL'};evidence=[ordered]@{cases=$guardCases;applyCount=0};limitations=@('contract fixture only')}
)
$result=[ordered]@{schemaVersion=1;capturedAt=(Get-Date).ToString('o');mutationPolicy='read-only-real-storage; owned-temp-file-only';realDiskMutation=$false;probes=$probes}
$dir=Split-Path -Parent $OutputPath;if($dir){New-Item -ItemType Directory -Path $dir -Force|Out-Null};[IO.File]::WriteAllText($OutputPath,(($result|ConvertTo-Json -Depth 10).Replace("`r`n","`n")),[Text.UTF8Encoding]::new($false));$OutputPath
