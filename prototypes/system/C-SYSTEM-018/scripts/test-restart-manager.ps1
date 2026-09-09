param([Parameter(Mandatory)][string]$OutputPath)
$ErrorActionPreference='Stop'

if(-not('BeadWise.RestartManagerProbe' -as [type])){
Add-Type @'
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
namespace BeadWise {
 [StructLayout(LayoutKind.Sequential)] public struct RM_UNIQUE_PROCESS { public int ProcessId; public System.Runtime.InteropServices.ComTypes.FILETIME ProcessStartTime; }
 public enum RM_APP_TYPE { Unknown=0, MainWindow=1, OtherWindow=2, Service=3, Explorer=4, Console=5, Critical=1000 }
 [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)] public struct RM_PROCESS_INFO {
   public RM_UNIQUE_PROCESS Process;
   [MarshalAs(UnmanagedType.ByValTStr, SizeConst=256)] public string AppName;
   [MarshalAs(UnmanagedType.ByValTStr, SizeConst=64)] public string ServiceShortName;
   public RM_APP_TYPE ApplicationType; public uint AppStatus; public uint TSSessionId;
   [MarshalAs(UnmanagedType.Bool)] public bool Restartable;
 }
 public static class RestartManagerProbe {
  [DllImport("rstrtmgr.dll", CharSet=CharSet.Unicode)] static extern int RmStartSession(out uint handle, int flags, StringBuilder key);
  [DllImport("rstrtmgr.dll", CharSet=CharSet.Unicode)] static extern int RmRegisterResources(uint handle, uint files, string[] fileNames, uint apps, RM_UNIQUE_PROCESS[] processes, uint services, string[] serviceNames);
  [DllImport("rstrtmgr.dll")] static extern int RmGetList(uint handle, out uint needed, ref uint count, [In,Out] RM_PROCESS_INFO[] info, ref uint rebootReasons);
  [DllImport("rstrtmgr.dll")] static extern int RmEndSession(uint handle);
  public static int[] QueryFile(string path) {
   uint handle; var key=new StringBuilder(33); int rc=RmStartSession(out handle,0,key); if(rc!=0) throw new Win32Exception(rc);
   try {
    rc=RmRegisterResources(handle,1,new[]{path},0,null,0,null); if(rc!=0) throw new Win32Exception(rc);
    uint needed=0,count=0,reboot=0; rc=RmGetList(handle,out needed,ref count,null,ref reboot);
    if(rc==0) return new int[0]; if(rc!=234) throw new Win32Exception(rc);
    var info=new RM_PROCESS_INFO[needed]; count=needed; rc=RmGetList(handle,out needed,ref count,info,ref reboot); if(rc!=0) throw new Win32Exception(rc);
    var result=new List<int>(); for(int i=0;i<count;i++) result.Add(info[i].Process.ProcessId); return result.ToArray();
   } finally { RmEndSession(handle); }
  }
 }
}
'@
}

function Start-OwnedLocker([string]$Path,[string]$Ready,[string]$Helper){
 $info=[Diagnostics.ProcessStartInfo]::new((Get-Command pwsh).Source);$info.UseShellExecute=$false;$info.CreateNoWindow=$true
 foreach($arg in '-NoProfile','-File',$Helper,'-FilePath',$Path,'-ReadyPath',$Ready){$null=$info.ArgumentList.Add($arg)}
 $process=[Diagnostics.Process]::Start($info)
 $deadline=(Get-Date).AddSeconds(8);while(-not(Test-Path $Ready)){if($process.HasExited){throw "lock helper exited $($process.ExitCode)"};if((Get-Date)-gt $deadline){throw 'lock helper timeout'};Start-Sleep -Milliseconds 50}
 return $process
}
function Stop-OwnedLocker([Diagnostics.Process]$Process){if($Process -and -not $Process.HasExited){$Process.Kill($true);$Process.WaitForExit(5000)|Out-Null}}

$tempRoot=Join-Path $env:TEMP ('BeadWise-C-SYSTEM-018-'+[guid]::NewGuid().ToString('N'));[IO.Directory]::CreateDirectory($tempRoot)|Out-Null
$file=Join-Path $tempRoot 'owned-lock.txt';$ready1=Join-Path $tempRoot 'ready-1';$ready2=Join-Path $tempRoot 'ready-2';$helper=Join-Path $PSScriptRoot 'hold-owned-lock.ps1';[IO.File]::WriteAllText($file,'owned',[Text.UTF8Encoding]::new($false))
$first=$null;$second=$null;$status='FAIL';$errorMessage=$null;$matched=$false;$unlock=$false;$restart=$false;$owners=@()
try{
 $first=Start-OwnedLocker $file $ready1 $helper;$owners=@([BeadWise.RestartManagerProbe]::QueryFile($file));$matched=$first.Id -in $owners
 Stop-OwnedLocker $first;$first=$null
 $stream=[IO.File]::Open($file,[IO.FileMode]::Open,[IO.FileAccess]::ReadWrite,[IO.FileShare]::None);$stream.Dispose();$unlock=$true
 $second=Start-OwnedLocker $file $ready2 $helper;$ownersAfterRestart=@([BeadWise.RestartManagerProbe]::QueryFile($file));$restart=($second.Id -in $ownersAfterRestart -and $second.Id -ne ($owners|Select-Object -First 1))
 if($matched -and $unlock -and $restart){$status='PASS'}
}catch{$errorMessage=$_.Exception.Message}
finally{Stop-OwnedLocker $first;Stop-OwnedLocker $second;Remove-Item -LiteralPath $tempRoot -Recurse -Force -ErrorAction SilentlyContinue}
$result=[ordered]@{schemaVersion=1;featureId='C-SYSTEM-018';premise='Restart Manager identifies the owner of a prototype-owned file lock; cleanup and restart target only that child process.';status=$status;capturedAt=(Get-Date).ToString('o');mutationPolicy='prototype-owned temp file and child processes only';evidence=[ordered]@{lockOwnerMatched=$matched;unlockVerified=$unlock;restartVerified=$restart;ownerCount=$owners.Count;externalProcessTerminated=$false;api='RmStartSession/RmRegisterResources/RmGetList/RmEndSession';error=$errorMessage};limitations=@('does not exercise services, critical processes, kernel or antivirus locks; RmShutdown/RmRestart intentionally not invoked')}
$dir=Split-Path -Parent $OutputPath;if($dir){New-Item -ItemType Directory -Path $dir -Force|Out-Null};[IO.File]::WriteAllText($OutputPath,(($result|ConvertTo-Json -Depth 7).Replace("`r`n","`n")),[Text.UTF8Encoding]::new($false));$OutputPath
