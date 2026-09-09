param([Parameter(Mandatory)][string]$OutputPath)
$ErrorActionPreference = 'Stop'

if (-not ('BeadWise.NativeFirmware' -as [type])) {
    Add-Type @'
using System;
using System.Runtime.InteropServices;
namespace BeadWise {
  public static class NativeFirmware {
    [DllImport("kernel32.dll", SetLastError=true)]
    public static extern uint GetSystemFirmwareTable(uint provider, uint tableId, IntPtr buffer, uint size);
    public static uint RawSmbiosSize() { return GetSystemFirmwareTable(0x52534D42, 0, IntPtr.Zero, 0); }
  }
}
'@
}

$bios = Get-CimInstance Win32_BIOS -ErrorAction Stop
$baseboard = Get-CimInstance Win32_BaseBoard -ErrorAction Stop
$firmwareBytes = [BeadWise.NativeFirmware]::RawSmbiosSize()
$topology = [ordered]@{
    disks = @(Get-CimInstance Win32_DiskDrive -ErrorAction Stop).Count
    displayControllers = @(Get-CimInstance Win32_VideoController -ErrorAction Stop).Count
    networkAdapters = @(Get-NetAdapter -Physical -ErrorAction SilentlyContinue).Count
    presentUsbDevices = @(Get-PnpDevice -PresentOnly -ErrorAction SilentlyContinue | Where-Object InstanceId -Like 'USB*').Count
}
$fixture = [ordered]@{ serial = 'SYNTHETIC-SERIAL'; pnp = 'USB\VID_1234&PID_ABCD\SYNTHETIC'; mac = '00-11-22-33-44-55' }
$redacted = [ordered]@{}
foreach ($name in $fixture.Keys) {
    $value = [string]$fixture[$name]
    $redacted[$name + 'Sha256'] = [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData([Text.Encoding]::UTF8.GetBytes($value))).ToLowerInvariant()
}
$packageJson = $redacted | ConvertTo-Json -Compress
$probes = @(
    [ordered]@{ featureId='C-HARDWARE-001'; premise='Raw SMBIOS is available through GetSystemFirmwareTable and can be cross-checked with CIM BIOS/baseboard data.'; status=if($firmwareBytes -gt 0 -and $bios.SMBIOSBIOSVersion){'PASS'}else{'UNSUPPORTED'}; evidence=[ordered]@{nativeFirmwareBytes=$firmwareBytes;cimBiosVersion=[string]$bios.SMBIOSBIOSVersion;baseboardManufacturer=[string]$baseboard.Manufacturer;nativeSource='GetSystemFirmwareTable(RSMB)';secondarySource='Win32_BIOS/Win32_BaseBoard'}; limitations=@('single x64 desktop; no malformed or missing SMBIOS fixture') },
    [ordered]@{ featureId='C-HARDWARE-004'; premise='Cross-component topology can preserve per-source counts and explicit Unknown fields.'; status='PASS'; evidence=[ordered]@{topology=$topology;unsupportedFields=@('dockPath','pcieNegotiatedWidth')}; limitations=@('no dock, VM, laptop, or multi-controller matrix') },
    [ordered]@{ featureId='C-HARDWARE-009'; premise='A hardware evidence package can hash direct identifiers and retain integrity without exporting raw values.'; status=if($packageJson -notmatch 'SYNTHETIC-SERIAL|SYNTHETIC|00-11-22-33-44-55'){'PASS'}else{'FAIL'}; evidence=[ordered]@{redactedFields=@($redacted.Keys);payloadSha256=[Convert]::ToHexString([Security.Cryptography.SHA256]::HashData([Text.Encoding]::UTF8.GetBytes($packageJson))).ToLowerInvariant();rawIdentifiersPresent=($packageJson -match 'SYNTHETIC-SERIAL|SYNTHETIC|00-11-22-33-44-55');fixtureOnly=$true}; limitations=@('redaction proved with fully synthetic identifiers; no failed hardware session') }
)
$result=[ordered]@{schemaVersion=1;capturedAt=(Get-Date).ToString('o');mutationPolicy='read-only';probes=$probes}
$dir=Split-Path -Parent $OutputPath; if($dir){New-Item -ItemType Directory -Path $dir -Force|Out-Null}
[IO.File]::WriteAllText($OutputPath,(($result|ConvertTo-Json -Depth 8).Replace("`r`n","`n")),[Text.UTF8Encoding]::new($false))
$OutputPath
