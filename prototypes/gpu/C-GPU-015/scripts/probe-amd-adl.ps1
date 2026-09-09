param([Parameter(Mandatory)] [string]$OutputPath)

$ErrorActionPreference = 'Stop'

if (-not ('BeadWise.Adl.Probe' -as [type])) {
    Add-Type -TypeDefinition @'
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BeadWise.Adl {
    public static class Probe {
        private delegate IntPtr MemoryAllocate(int size);
        private static readonly List<IntPtr> allocations = new List<IntPtr>();
        private static readonly MemoryAllocate allocator = Allocate;

        [DllImport("atiadlxx.dll", ExactSpelling = true)]
        private static extern int ADL_Main_Control_Create(MemoryAllocate callback, int enumerateConnectedAdapters);

        [DllImport("atiadlxx.dll", ExactSpelling = true)]
        private static extern int ADL_Main_Control_Destroy();

        [DllImport("atiadlxx.dll", ExactSpelling = true)]
        private static extern int ADL_Adapter_NumberOfAdapters_Get(ref int numberOfAdapters);

        private static IntPtr Allocate(int size) {
            IntPtr pointer = Marshal.AllocHGlobal(size);
            allocations.Add(pointer);
            return pointer;
        }

        public static int[] Run() {
            int create = ADL_Main_Control_Create(allocator, 1);
            int enumerate = -1;
            int count = 0;
            int destroy = -1;
            try {
                if (create == 0) enumerate = ADL_Adapter_NumberOfAdapters_Get(ref count);
            } finally {
                if (create == 0) destroy = ADL_Main_Control_Destroy();
                foreach (IntPtr pointer in allocations) Marshal.FreeHGlobal(pointer);
                allocations.Clear();
            }
            return new [] { create, enumerate, count, destroy };
        }
    }
}
'@
}

$available = Test-Path -LiteralPath (Join-Path $env:SystemRoot 'System32\atiadlxx.dll')
$values = if ($available) { [BeadWise.Adl.Probe]::Run() } else { @(-1, -1, 0, -1) }
$result = [ordered]@{
    featureId = 'C-GPU-015'
    mechanism = 'AMD_ADL_classic'
    capturedAt = (Get-Date).ToString('o')
    libraryAvailable = $available
    createStatus = $values[0]
    enumerationStatus = $values[1]
    adapterCount = $values[2]
    destroyStatus = $values[3]
    mutationPolicy = 'read-only'
}
$directory = Split-Path -Parent $OutputPath
if ($directory) { New-Item -ItemType Directory -Path $directory -Force | Out-Null }
[IO.File]::WriteAllText($OutputPath, (($result | ConvertTo-Json -Depth 4).Replace("`r`n", "`n")), [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
