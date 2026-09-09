param(
    [Parameter(Mandatory)] [string]$OutputPath,
    [ValidateRange(1, 60)] [int]$SampleCount = 3,
    [ValidateRange(50, 60000)] [int]$IntervalMilliseconds = 500
)

$ErrorActionPreference = 'Stop'

if (-not ('BeadWise.Pdh.Native' -as [type])) {
    Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;

namespace BeadWise.Pdh {
    [StructLayout(LayoutKind.Sequential)]
    public struct FormattedCounterValue {
        public uint Status;
        public double Value;
    }

    public static class Native {
        [DllImport("pdh.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern uint PdhOpenQueryW(string dataSource, UIntPtr userData, out IntPtr query);

        [DllImport("pdh.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern uint PdhAddEnglishCounterW(IntPtr query, string path, UIntPtr userData, out IntPtr counter);

        [DllImport("pdh.dll")]
        public static extern uint PdhCollectQueryData(IntPtr query);

        [DllImport("pdh.dll")]
        public static extern uint PdhGetFormattedCounterValue(IntPtr counter, uint format, out uint type, out FormattedCounterValue value);

        [DllImport("pdh.dll")]
        public static extern uint PdhCloseQuery(IntPtr query);

        public static uint OpenEnglishCounter(string path, out IntPtr query, out IntPtr counter) {
            counter = IntPtr.Zero;
            uint status = PdhOpenQueryW(null, UIntPtr.Zero, out query);
            if (status != 0) return status;
            status = PdhAddEnglishCounterW(query, path, UIntPtr.Zero, out counter);
            if (status != 0) { PdhCloseQuery(query); query = IntPtr.Zero; }
            return status;
        }

        public static uint ReadDouble(IntPtr counter, out FormattedCounterValue value) {
            uint type;
            return PdhGetFormattedCounterValue(counter, 0x00000200, out type, out value);
        }
    }
}
'@
}

function Assert-PdhSuccess([uint32]$Status, [string]$Operation) {
    if ($Status -ne 0) { throw "$Operation failed with PDH status 0x$($Status.ToString('X8'))" }
}

$query = [IntPtr]::Zero
$counter = [IntPtr]::Zero
$samples = @()
try {
    Assert-PdhSuccess ([BeadWise.Pdh.Native]::OpenEnglishCounter('\Processor(_Total)\% Processor Time', [ref]$query, [ref]$counter)) 'OpenEnglishCounter'
    Assert-PdhSuccess ([BeadWise.Pdh.Native]::PdhCollectQueryData($query)) 'PdhCollectQueryData(initial)'

    for ($index = 0; $index -lt $SampleCount; $index++) {
        Start-Sleep -Milliseconds $IntervalMilliseconds
        Assert-PdhSuccess ([BeadWise.Pdh.Native]::PdhCollectQueryData($query)) 'PdhCollectQueryData'
        $value = [BeadWise.Pdh.FormattedCounterValue]::new()
        Assert-PdhSuccess ([BeadWise.Pdh.Native]::ReadDouble($counter, [ref]$value)) 'PdhGetFormattedCounterValue'
        $samples += [ordered]@{ capturedAt = (Get-Date).ToString('o'); status = $value.Status; value = $value.Value }
    }
}
finally {
    if ($query -ne [IntPtr]::Zero) { [void][BeadWise.Pdh.Native]::PdhCloseQuery($query) }
}

$result = [ordered]@{
    featureId = 'C-CPU-003'
    mechanism = 'PDH_PdhAddEnglishCounterW'
    counterPath = '\Processor(_Total)\% Processor Time'
    samples = $samples
}
$directory = Split-Path -Parent $OutputPath
if ($directory) { New-Item -ItemType Directory -Path $directory -Force | Out-Null }
[IO.File]::WriteAllText($OutputPath, (($result | ConvertTo-Json -Depth 5).Replace("`r`n", "`n")), [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
