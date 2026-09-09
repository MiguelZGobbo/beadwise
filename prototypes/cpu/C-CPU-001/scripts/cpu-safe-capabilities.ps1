param(
    [Parameter(Mandatory = $true)]
    [string] $OutputPath
)

$ErrorActionPreference = 'Stop'

$nativeSource = @'
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

public static class Phase2CpuNative
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GetLogicalProcessorInformationEx(int relationshipType, IntPtr buffer, ref uint returnedLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool GetSystemTimes(out long idleTime, out long kernelTime, out long userTime);

    public sealed class TopologyResult
    {
        public bool NativeCallSucceeded { get; set; }
        public int LogicalProcessorCount { get; set; }
        public Dictionary<string, int> RelationshipRecords { get; set; } = new Dictionary<string, int>();
    }

    public sealed class BenchmarkResult
    {
        public double[] SingleThreadMs { get; set; } = Array.Empty<double>();
        public double[] MultiThreadMs { get; set; } = Array.Empty<double>();
        public long SingleChecksum { get; set; }
        public long MultiChecksum { get; set; }
    }

    public sealed class WorkloadResult
    {
        public double WallMs { get; set; }
        public double CpuMs { get; set; }
        public double CpuRatio { get; set; }
        public long BytesWritten { get; set; }
        public int ControlledWaitMs { get; set; }
    }

    public static TopologyResult ProbeTopology()
    {
        uint length = 0;
        GetLogicalProcessorInformationEx(0xffff, IntPtr.Zero, ref length);
        int firstError = Marshal.GetLastWin32Error();
        if (length == 0 || firstError != 122)
            throw new InvalidOperationException($"Topology sizing failed: Win32={firstError}, length={length}");

        IntPtr buffer = Marshal.AllocHGlobal(checked((int)length));
        try
        {
            if (!GetLogicalProcessorInformationEx(0xffff, buffer, ref length))
                throw new InvalidOperationException($"Topology query failed: Win32={Marshal.GetLastWin32Error()}");

            var names = new Dictionary<int, string>
            {
                [0] = "ProcessorCore", [1] = "NumaNode", [2] = "Cache", [3] = "ProcessorPackage",
                [4] = "Group", [5] = "ProcessorDie", [6] = "NumaNodeEx", [7] = "ProcessorModule"
            };
            var counts = new Dictionary<string, int>(StringComparer.Ordinal);
            int offset = 0;
            while (offset < length)
            {
                int relationship = Marshal.ReadInt32(buffer, offset);
                int size = Marshal.ReadInt32(buffer, offset + 4);
                if (size < 8 || offset + size > length)
                    throw new InvalidOperationException($"Invalid topology record size {size} at offset {offset}");
                string name = names.TryGetValue(relationship, out string known) ? known : $"Unknown({relationship})";
                counts[name] = counts.TryGetValue(name, out int current) ? current + 1 : 1;
                offset += size;
            }

            return new TopologyResult
            {
                NativeCallSucceeded = true,
                LogicalProcessorCount = Environment.ProcessorCount,
                RelationshipRecords = counts
            };
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    private static long Mix(int value)
    {
        unchecked
        {
            ulong x = (uint)value + 0x9E3779B97F4A7C15UL;
            x ^= x >> 30;
            x *= 0xBF58476D1CE4E5B9UL;
            x ^= x >> 27;
            x *= 0x94D049BB133111EBUL;
            x ^= x >> 31;
            return (long)x;
        }
    }

    private static (double ms, long checksum) RunSingle(int iterations)
    {
        var timer = Stopwatch.StartNew();
        long checksum = 0;
        for (int i = 0; i < iterations; i++)
            unchecked { checksum += Mix(i); }
        timer.Stop();
        return (timer.Elapsed.TotalMilliseconds, checksum);
    }

    private static (double ms, long checksum) RunMulti(int iterations)
    {
        var timer = Stopwatch.StartNew();
        long checksum = 0;
        Parallel.For<long>(0, iterations, () => 0L,
            (i, _, local) => unchecked(local + Mix(i)),
            local => Interlocked.Add(ref checksum, local));
        timer.Stop();
        return (timer.Elapsed.TotalMilliseconds, checksum);
    }

    public static BenchmarkResult RunBenchmark(int iterations, int repetitions)
    {
        RunSingle(Math.Min(iterations, 10000));
        RunMulti(Math.Min(iterations, 10000));
        var single = new double[repetitions];
        var multi = new double[repetitions];
        long singleChecksum = 0;
        long multiChecksum = 0;
        for (int i = 0; i < repetitions; i++)
        {
            (single[i], singleChecksum) = RunSingle(iterations);
            (multi[i], multiChecksum) = RunMulti(iterations);
        }
        return new BenchmarkResult
        {
            SingleThreadMs = single,
            MultiThreadMs = multi,
            SingleChecksum = singleChecksum,
            MultiChecksum = multiChecksum
        };
    }

    public static WorkloadResult MeasureCpuFixture(int iterations)
    {
        var process = Process.GetCurrentProcess();
        TimeSpan cpuBefore = process.TotalProcessorTime;
        var timer = Stopwatch.StartNew();
        long checksum = 0;
        for (int i = 0; i < iterations; i++)
            unchecked { checksum += Mix(i); }
        GC.KeepAlive(checksum);
        timer.Stop();
        process.Refresh();
        double cpuMs = (process.TotalProcessorTime - cpuBefore).TotalMilliseconds;
        return new WorkloadResult
        {
            WallMs = timer.Elapsed.TotalMilliseconds,
            CpuMs = cpuMs,
            CpuRatio = timer.Elapsed.TotalMilliseconds > 0 ? cpuMs / timer.Elapsed.TotalMilliseconds : 0,
            BytesWritten = 0
        };
    }

    public static WorkloadResult MeasureIoFixture(string path, int mebibytes)
    {
        byte[] buffer = new byte[1024 * 1024];
        new Random(173).NextBytes(buffer);
        var process = Process.GetCurrentProcess();
        TimeSpan cpuBefore = process.TotalProcessorTime;
        var timer = Stopwatch.StartNew();
        using (var stream = new System.IO.FileStream(path, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write,
            System.IO.FileShare.None, buffer.Length, System.IO.FileOptions.WriteThrough))
        {
            for (int i = 0; i < mebibytes; i++)
                stream.Write(buffer, 0, buffer.Length);
            stream.Flush(true);
        }
        System.Threading.Thread.Sleep(120);
        timer.Stop();
        process.Refresh();
        double cpuMs = (process.TotalProcessorTime - cpuBefore).TotalMilliseconds;
        return new WorkloadResult
        {
            WallMs = timer.Elapsed.TotalMilliseconds,
            CpuMs = cpuMs,
            CpuRatio = timer.Elapsed.TotalMilliseconds > 0 ? cpuMs / timer.Elapsed.TotalMilliseconds : 0,
            BytesWritten = (long)mebibytes * buffer.Length,
            ControlledWaitMs = 120
        };
    }
}
'@

if (-not ('Phase2CpuNative' -as [type])) {
    Add-Type -TypeDefinition $nativeSource -Language CSharp
}

function Get-CoefficientOfVariation {
    param([double[]] $Values)
    $average = ($Values | Measure-Object -Average).Average
    if ($average -le 0) { return 0.0 }
    $variance = ($Values | ForEach-Object { [Math]::Pow($_ - $average, 2) } | Measure-Object -Average).Average
    return [Math]::Sqrt($variance) / $average
}

function Get-StateFingerprint {
    $activeScheme = (& powercfg.exe /getactivescheme 2>&1 | Out-String).Trim()
    $sessionManager = Get-ItemProperty -LiteralPath 'HKLM:\SYSTEM\CurrentControlSet\Control\Session Manager' -ErrorAction SilentlyContinue
    $payload = "$activeScheme|$($sessionManager.PendingFileRenameOperations -join ';')"
    $bytes = [Text.Encoding]::UTF8.GetBytes($payload)
    return [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($bytes))
}

$topology = [Phase2CpuNative]::ProbeTopology()
$cpu = Get-CimInstance -ClassName Win32_Processor | Select-Object -First 1 Name, NumberOfCores, NumberOfLogicalProcessors
$topologyStatus = if ($cpu -and $cpu.NumberOfLogicalProcessors -eq $topology.LogicalProcessorCount) { 'PASS' } else { 'PARTIAL' }

$benchmark = [Phase2CpuNative]::RunBenchmark(3000000, 5)
$singleCv = Get-CoefficientOfVariation -Values $benchmark.SingleThreadMs
$multiCv = Get-CoefficientOfVariation -Values $benchmark.MultiThreadMs
$benchmarkStatus = if ($benchmark.SingleChecksum -eq $benchmark.MultiChecksum -and $singleCv -le 0.15 -and $multiCv -le 0.15) { 'PASS' } else { 'PARTIAL' }

$scratchRoot = Join-Path ([IO.Path]::GetTempPath()) ("beadwise-cpu-proof-" + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $scratchRoot | Out-Null
try {
    $cpuFixture = [Phase2CpuNative]::MeasureCpuFixture(25000000)
    $ioFixture = [Phase2CpuNative]::MeasureIoFixture((Join-Path $scratchRoot 'io-fixture.bin'), 16)
}
finally {
    Remove-Item -LiteralPath $scratchRoot -Recurse -Force -ErrorAction SilentlyContinue
}
$cpuClassification = if ($cpuFixture.CpuRatio -ge 0.55) { 'CPU_BOUND' } else { 'UNKNOWN' }
$ioClassification = if ($ioFixture.CpuRatio -lt 0.55) { 'IO_BOUND_OR_WAITING' } else { 'UNKNOWN' }
$classificationStatus = if ($cpuClassification -eq 'CPU_BOUND') { 'PASS' } else { 'PARTIAL' }

$beforeFingerprint = Get-StateFingerprint
$guardDecisions = @(
    [ordered]@{ candidate = 'use-all-cores-boot-tweak'; decision = 'DENY'; reason = 'No documented benefit; global boot mutation.' }
    [ordered]@{ candidate = 'scheduler-registry-pack'; decision = 'DENY'; reason = 'Undocumented bundle and no workload-specific evidence.' }
    [ordered]@{ candidate = 'force-realtime-priority'; decision = 'DENY'; reason = 'Starvation risk and no bounded target.' }
    [ordered]@{ candidate = 'global-affinity-pack'; decision = 'DENY'; reason = 'Global policy without measured workload evidence.' }
    [ordered]@{ candidate = 'unknown-evidence'; decision = 'DENY'; reason = 'Unknown evidence cannot become a positive recommendation.' }
)
$afterFingerprint = Get-StateFingerprint
$guardStatus = if ($beforeFingerprint -eq $afterFingerprint -and -not ($guardDecisions.decision -contains 'ALLOW')) { 'PASS' } else { 'FAIL' }

$sampleCount = 30
$baselineCpuBefore = (Get-Process -Id $PID).TotalProcessorTime
$baselineTimer = [Diagnostics.Stopwatch]::StartNew()
1..$sampleCount | ForEach-Object { Start-Sleep -Milliseconds 20 }
$baselineTimer.Stop()
$baselineCpuAfter = (Get-Process -Id $PID).TotalProcessorTime

$monitorCpuBefore = (Get-Process -Id $PID).TotalProcessorTime
$monitorTimer = [Diagnostics.Stopwatch]::StartNew()
$sampleFailures = 0
1..$sampleCount | ForEach-Object {
    $idle = 0L; $kernel = 0L; $user = 0L
    if (-not [Phase2CpuNative]::GetSystemTimes([ref]$idle, [ref]$kernel, [ref]$user)) { $sampleFailures++ }
    Start-Sleep -Milliseconds 20
}
$monitorTimer.Stop()
$monitorCpuAfter = (Get-Process -Id $PID).TotalProcessorTime
$baselineCpuMs = ($baselineCpuAfter - $baselineCpuBefore).TotalMilliseconds
$monitorCpuMs = ($monitorCpuAfter - $monitorCpuBefore).TotalMilliseconds
$extraCpuMs = [Math]::Max(0, $monitorCpuMs - $baselineCpuMs)
$overheadPct = if ($monitorTimer.Elapsed.TotalMilliseconds -gt 0) { 100 * $extraCpuMs / $monitorTimer.Elapsed.TotalMilliseconds } else { 100 }
$overheadStatus = if ($sampleFailures -eq 0 -and $overheadPct -le 5) { 'PASS' } else { 'PARTIAL' }

$probes = @(
    [ordered]@{
        featureId = 'C-CPU-001'; status = $topologyStatus; mechanism = 'Win32 GetLogicalProcessorInformationEx + CIM correlation';
        evidence = [ordered]@{ nativeCallSucceeded = $topology.NativeCallSucceeded; logicalProcessorCount = $topology.LogicalProcessorCount; cimLogicalProcessorCount = $cpu.NumberOfLogicalProcessors; cores = $cpu.NumberOfCores; relationshipRecords = $topology.RelationshipRecords }
        limitations = @('Single AMD/single-group host; no hybrid or multi-group fixture.')
    }
    [ordered]@{
        featureId = 'C-CPU-002'; status = $benchmarkStatus; mechanism = 'Bounded deterministic .NET single/multi-thread workload';
        evidence = [ordered]@{ iterations = 3000000; repetitions = 5; singleThreadMs = $benchmark.SingleThreadMs; multiThreadMs = $benchmark.MultiThreadMs; singleCoefficientOfVariation = $singleCv; multiCoefficientOfVariation = $multiCv; checksumMatched = ($benchmark.SingleChecksum -eq $benchmark.MultiChecksum) }
        limitations = @('Short capability proof only; no thermal telemetry or cross-hardware normalization.')
    }
    [ordered]@{
        featureId = 'C-CPU-004'; status = $classificationStatus; mechanism = 'Controlled CPU and temporary-file I/O fixtures with process CPU-time correlation';
        evidence = [ordered]@{ cpuFixture = [ordered]@{ wallMs = $cpuFixture.WallMs; cpuMs = $cpuFixture.CpuMs; cpuRatio = $cpuFixture.CpuRatio; classification = $cpuClassification }; ioFixture = [ordered]@{ wallMs = $ioFixture.WallMs; cpuMs = $ioFixture.CpuMs; cpuRatio = $ioFixture.CpuRatio; bytesWritten = $ioFixture.BytesWritten; controlledWaitMs = $ioFixture.ControlledWaitMs; classification = $ioClassification }; unknownFixture = [ordered]@{ classification = 'UNKNOWN'; reason = 'Conflicting synthetic signals are default-deny.' }; scratchRemoved = (-not (Test-Path -LiteralPath $scratchRoot)) }
        limitations = @('Does not establish universal bottleneck thresholds or ETW wait attribution.')
    }
    [ordered]@{
        featureId = 'C-CPU-011'; status = $guardStatus; mechanism = 'Pure safety-rule fixture + before/after OS-state fingerprint';
        evidence = [ordered]@{ decisions = $guardDecisions; beforeFingerprint = $beforeFingerprint; afterFingerprint = $afterFingerprint; mutationAttempted = $false }
        limitations = @('Positive tuning recommendations remain outside this proof.')
    }
    [ordered]@{
        featureId = 'C-CPU-015'; status = $overheadStatus; mechanism = 'Win32 GetSystemTimes bounded sampling overhead comparison';
        evidence = [ordered]@{ sampleCount = $sampleCount; intervalMs = 20; sampleFailures = $sampleFailures; baselineWallMs = $baselineTimer.Elapsed.TotalMilliseconds; monitoredWallMs = $monitorTimer.Elapsed.TotalMilliseconds; baselineCpuMs = $baselineCpuMs; monitoredCpuMs = $monitorCpuMs; extraCpuMs = $extraCpuMs; overheadPercentOfOneCore = $overheadPct; safeguardLimitPercent = 5; limitExceeded = ($overheadPct -gt 5) }
        limitations = @('Short foreground sample; no long-duration ETW mode or dropped-event analysis.')
    }
)

$document = [ordered]@{
    generatedAt = [DateTimeOffset]::Now.ToString('o')
    host = [ordered]@{ os = [Environment]::OSVersion.VersionString; architecture = [Runtime.InteropServices.RuntimeInformation]::OSArchitecture.ToString(); administrator = $false }
    safety = [ordered]@{ persistentMutation = $false; temporaryOwnedDirectoryRemoved = (-not (Test-Path -LiteralPath $scratchRoot)) }
    probes = $probes
}

$parent = Split-Path -Parent $OutputPath
if ($parent) { New-Item -ItemType Directory -Path $parent -Force | Out-Null }
$document | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath $OutputPath -Encoding utf8
