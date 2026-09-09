param([Parameter(Mandatory)][string]$FilePath,[Parameter(Mandatory)][string]$ReadyPath)
$ErrorActionPreference='Stop'
$stream=$null
try{
 $stream=[IO.File]::Open($FilePath,[IO.FileMode]::Open,[IO.FileAccess]::ReadWrite,[IO.FileShare]::None)
 [IO.File]::WriteAllText($ReadyPath,'LOCKED',[Text.UTF8Encoding]::new($false))
 while($true){Start-Sleep -Milliseconds 250}
}finally{if($stream){$stream.Dispose()}}
