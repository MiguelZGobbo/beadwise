$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$script=Join-Path $root 'scripts\test-restart-manager.ps1'
$out=Join-Path $env:TEMP 'beadwise-restart-manager-proof.json'
try{
 & $script -OutputPath $out
 $r=Get-Content -Raw $out|ConvertFrom-Json
 if($r.featureId -ne 'C-SYSTEM-018'){throw 'feature mapping missing'}
 if($r.status -ne 'PASS'){throw "Restart Manager proof was $($r.status)"}
 if(-not $r.evidence.lockOwnerMatched){throw 'owned lock process was not identified'}
 if(-not $r.evidence.unlockVerified -or -not $r.evidence.restartVerified){throw 'controlled cleanup/restart was not verified'}
 if($r.evidence.externalProcessTerminated){throw 'external process was touched'}
 'PASS: Restart Manager identified and safely cycled only the prototype-owned lock process'
}finally{Remove-Item $out -Force -ErrorAction SilentlyContinue}
