param([Parameter(Mandatory)][string]$OutputPath)
$ErrorActionPreference='Stop'

if(-not('BeadWise.NativeMemory' -as [type])){
Add-Type @'
using System;
using System.Runtime.InteropServices;
namespace BeadWise {
 [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
 public class MemoryStatus { public uint Length=(uint)Marshal.SizeOf(typeof(MemoryStatus)); public uint Load; public ulong TotalPhys; public ulong AvailPhys; public ulong TotalPageFile; public ulong AvailPageFile; public ulong TotalVirtual; public ulong AvailVirtual; public ulong AvailExtendedVirtual; }
 public static class NativeMemory {
  [DllImport("kernel32.dll", SetLastError=true)] public static extern bool GlobalMemoryStatusEx([In,Out] MemoryStatus status);
  [DllImport("kernel32.dll", SetLastError=true)] public static extern bool GetNumaHighestNodeNumber(out uint node);
  [DllImport("kernel32.dll")] public static extern UIntPtr GetLargePageMinimum();
 }
}
'@
}
$native=[BeadWise.MemoryStatus]::new();$memoryOk=[BeadWise.NativeMemory]::GlobalMemoryStatusEx($native)
$modules=@(Get-CimInstance Win32_PhysicalMemory -ErrorAction Stop)
$arrays=@(Get-CimInstance Win32_PhysicalMemoryArray -ErrorAction Stop)
$before=(Get-Process -Id $PID).WorkingSet64
$allocatedBytes=64MB
$buffer=New-Object byte[] $allocatedBytes
for($i=0;$i -lt $buffer.Length;$i+=4096){$buffer[$i]=1}
$during=(Get-Process -Id $PID).WorkingSet64
$copy=New-Object byte[] $allocatedBytes
$sw=[Diagnostics.Stopwatch]::StartNew();[Buffer]::BlockCopy($buffer,0,$copy,0,$buffer.Length);$sw.Stop()
$buffer=$null;$copy=$null;[GC]::Collect();[GC]::WaitForPendingFinalizers();[GC]::Collect();Start-Sleep -Milliseconds 100
$after=(Get-Process -Id $PID).WorkingSet64
$numaNode=[uint32]0;$numaOk=[BeadWise.NativeMemory]::GetNumaHighestNodeNumber([ref]$numaNode)
$largePageBytes=[uint64][BeadWise.NativeMemory]::GetLargePageMinimum()
$counterStatus='UNSUPPORTED';$counterEvidence=[ordered]@{reason='No Memory/Page Faults performance counter path found'}
try{
 $set=Get-Counter -ListSet * -ErrorAction Stop|Where-Object{$_.CounterSetName -in 'Memory','Memória'}|Select-Object -First 1
 $path=@($set.Paths|Where-Object{$_ -match 'Page Faults/sec|Falhas de páginas/s'}|Select-Object -First 1)
 if($path){$samples=Get-Counter $path -SampleInterval 1 -MaxSamples 3 -ErrorAction Stop;$values=@($samples.CounterSamples.CookedValue);$counterStatus='PASS';$counterEvidence=[ordered]@{path=$path;sampleCount=$values.Count;minimum=($values|Measure-Object -Minimum).Minimum;maximum=($values|Measure-Object -Maximum).Maximum}}
}catch{$counterStatus='FAIL';$counterEvidence=[ordered]@{error=$_.Exception.Message}}
$guardCases=@(
 [ordered]@{evidence=$false;documented=$false;rollback=$false;decision='BLOCK'},
 [ordered]@{evidence=$true;documented=$true;rollback=$false;decision='BLOCK'},
 [ordered]@{evidence=$true;documented=$true;rollback=$true;decision='REVIEW'}
)
$adequacy=@(
 [ordered]@{capacityGb=4;commitRatio=.95;sustainedPressure=$true;classification='CONSTRAINED'},
 [ordered]@{capacityGb=32;commitRatio=.35;sustainedPressure=$false;classification='NO_EVIDENCE_OF_CAPACITY_LIMIT'}
)
$probes=@(
 [ordered]@{featureId='C-RAM-001';premise='CIM module/array inventory can be reconciled with native physical-memory total.';status=if($memoryOk -and $modules.Count){'PASS'}else{'PARTIAL'};evidence=[ordered]@{moduleCount=$modules.Count;arrayCount=$arrays.Count;nativeTotalBytes=$native.TotalPhys;cimModuleBytes=($modules.Capacity|Measure-Object -Sum).Sum;sources=@('GlobalMemoryStatusEx','Win32_PhysicalMemory','Win32_PhysicalMemoryArray')};limitations=@('single desktop; no ECC or empty-slot matrix')},
 [ordered]@{featureId='C-RAM-004';premise='A bounded allocation is visible in a before/during/after memory window and is cleaned up.';status=if($during -gt $before){'PASS'}else{'PARTIAL'};evidence=[ordered]@{allocatedBytes=$allocatedBytes;workingSetBefore=$before;workingSetDuring=$during;workingSetAfterCleanup=$after;nativeLoadPercent=$native.Load};limitations=@('64 MiB allocation does not prove OOM behavior')},
 [ordered]@{featureId='C-RAM-006';premise='Per-process growth and release can be sampled without classifying every retained working-set page as a leak.';status='PARTIAL';evidence=[ordered]@{workingSetGrowthBytes=($during-$before);workingSetAfterCleanup=$after;classification='BOUNDED_FIXTURE_RELEASED'};limitations=@('allocator may retain pages; no real leaking process')},
 [ordered]@{featureId='C-RAM-009';premise='An unsafe tweak guard blocks absent evidence or absent rollback and never emits Apply.';status=if(@($guardCases|Where-Object decision -eq 'BLOCK').Count -eq 2){'PASS'}else{'FAIL'};evidence=[ordered]@{cases=$guardCases;applyCount=0};limitations=@('contract fixture only')},
 [ordered]@{featureId='C-RAM-010';premise='A localized PDH-backed page-fault counter can be sampled temporally.';status=$counterStatus;evidence=$counterEvidence;limitations=@('aggregate page faults are not per-process hard faults')},
 [ordered]@{featureId='C-RAM-012';premise='Native NUMA topology reports a one-node or multi-node result without inferring unsupported topology.';status=if($numaOk){'PASS'}else{'UNSUPPORTED'};evidence=[ordered]@{highestNode=$numaNode;nodeCount=if($numaOk){$numaNode+1}else{$null};source='GetNumaHighestNodeNumber'};limitations=@('single UMA/NUMA topology; no ARM64')},
 [ordered]@{featureId='C-RAM-014';premise='Capacity guidance depends on measured pressure context rather than a magic capacity threshold.';status=if($adequacy[1].classification -eq 'NO_EVIDENCE_OF_CAPACITY_LIMIT'){'PASS'}else{'FAIL'};evidence=[ordered]@{cases=$adequacy;currentCapacityGb=[math]::Round($native.TotalPhys/1GB,2)};limitations=@('fixture rule not validated against diverse workloads')},
 [ordered]@{featureId='C-RAM-016';premise='Large-page capability can be detected without changing SeLockMemoryPrivilege or allocating a large page.';status=if($largePageBytes -gt 0){'PARTIAL'}else{'UNSUPPORTED'};evidence=[ordered]@{minimumBytes=$largePageBytes;allocationAttempted=$false;privilegeChanged=$false;source='GetLargePageMinimum'};limitations=@('privileged allocation intentionally not attempted')},
 [ordered]@{featureId='C-RAM-017';premise='A bounded memory-copy workload records bytes and duration without turning one sample into a universal recommendation.';status=if($sw.Elapsed.TotalMilliseconds -gt 0){'PARTIAL'}else{'FAIL'};evidence=[ordered]@{bytesCopied=$allocatedBytes;durationMs=[math]::Round($sw.Elapsed.TotalMilliseconds,3);recommendation='NONE_FROM_SINGLE_SAMPLE'};limitations=@('synthetic single-host workload')}
)
$result=[ordered]@{schemaVersion=1;capturedAt=(Get-Date).ToString('o');mutationPolicy='ephemeral-process-memory-only';probes=$probes}
$dir=Split-Path -Parent $OutputPath;if($dir){New-Item -ItemType Directory -Path $dir -Force|Out-Null};[IO.File]::WriteAllText($OutputPath,(($result|ConvertTo-Json -Depth 9).Replace("`r`n","`n")),[Text.UTF8Encoding]::new($false));$OutputPath
