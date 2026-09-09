param(
    [string]$OutputPath = (Join-Path (Split-Path -Parent $PSScriptRoot) 'results\vendor-api-proof.json')
)

$ErrorActionPreference = 'Stop'

$steps = [Collections.Generic.List[object]]::new()

function Add-Step {
    param([string]$Name, [string]$Status, [string]$Detail)
    $steps.Add([ordered]@{ name = $Name; status = $Status; detail = $Detail })
}

$gpus = @(Get-CimInstance Win32_VideoController | Select-Object Name, AdapterCompatibility, DriverVersion, PNPDeviceID)
if ($gpus.Count -gt 0) {
    Add-Step 'GPU_DETECT' 'PASS' (($gpus | ForEach-Object { $_.Name }) -join '; ')
}
else {
    Add-Step 'GPU_DETECT' 'FAIL' 'Win32_VideoController returned no GPUs.'
}

$commands = @('nvidia-smi', 'amd-smi', 'rocm-smi', 'PresentMon.exe') | ForEach-Object {
    $command = Get-Command $_ -ErrorAction SilentlyContinue
    [ordered]@{
        name = $_
        found = $null -ne $command
        source = if ($command) { $command.Source } else { $null }
    }
}

$dllCandidates = @(
    'C:\Windows\System32\atiadlxx.dll',
    'C:\Windows\SysWOW64\atiadlxy.dll',
    'C:\Program Files\AMD\CNext\CNext\atiadlxx.dll',
    'C:\Program Files\AMD\CNext\CNext\ADLX.dll',
    'C:\Program Files\NVIDIA Corporation\NVSMI\nvidia-smi.exe',
    'C:\Windows\System32\nvml.dll'
) | ForEach-Object {
    [ordered]@{
        path = $_
        exists = Test-Path -LiteralPath $_
    }
}

$availableCommands = @($commands | Where-Object found)
$availableDlls = @($dllCandidates | Where-Object exists)
$adlExportProbe = [ordered]@{
    attempted = $false
    library = 'C:\Windows\System32\atiadlxx.dll'
    loaded = $false
    exports = @()
    error = $null
}

if (Test-Path -LiteralPath $adlExportProbe.library) {
    $adlExportProbe.attempted = $true
    try {
        $handle = [Runtime.InteropServices.NativeLibrary]::Load($adlExportProbe.library)
        $adlExportProbe.loaded = $true
        $adlExportProbe.exports = @('ADL_Main_Control_Create', 'ADL_Adapter_NumberOfAdapters_Get') | ForEach-Object {
            $address = [IntPtr]::Zero
            [ordered]@{
                name = $_
                found = [Runtime.InteropServices.NativeLibrary]::TryGetExport($handle, $_, [ref]$address)
            }
        }
        [Runtime.InteropServices.NativeLibrary]::Free($handle)
    }
    catch {
        $adlExportProbe.error = $_.Exception.Message
    }
}

$adlExportsFound = @($adlExportProbe.exports | Where-Object found).Count
if ($adlExportProbe.loaded -and $adlExportsFound -ge 2) {
    Add-Step 'VENDOR_API_ACCESS' 'PASS' 'atiadlxx.dll loaded and ADL entrypoint exports were found.'
}
elseif ($availableCommands.Count -gt 0 -or $availableDlls.Count -gt 0) {
    Add-Step 'VENDOR_API_ACCESS' 'BLOCKED' 'A vendor CLI/DLL candidate exists, but the tested ADL entrypoints were not proven available.'
}
else {
    Add-Step 'VENDOR_API_ACCESS' 'BLOCKED' 'AMD GPU is present, but no recognized ADLX/ADL/NVML/vendor CLI candidate was found in the probed locations.'
}

$result = [ordered]@{
    schemaVersion = 1
    capturedAt = (Get-Date).ToString('o')
    featureId = 'C-GPU-015'
    gpus = $gpus
    commands = @($commands)
    dllCandidates = @($dllCandidates)
    adlExportProbe = $adlExportProbe
    sources = @(
        'https://gpuopen.com/adlx/'
        'https://docs.nvidia.com/deploy/nvml-api/nvml-api-reference.html'
        'https://game.intel.com/story/intel-presentmon/'
    )
    steps = @($steps)
}

$outputDirectory = Split-Path -Parent $OutputPath
New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
$json = ($result | ConvertTo-Json -Depth 8).Replace("`r`n", "`n")
[IO.File]::WriteAllText($OutputPath, $json, [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
