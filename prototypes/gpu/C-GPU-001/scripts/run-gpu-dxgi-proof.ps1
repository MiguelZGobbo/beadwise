param(
    [Parameter(Mandatory = $true)]
    [string] $OutputPath
)

$ErrorActionPreference = 'Stop'
$prototypeRoot = Split-Path -Parent $PSScriptRoot
$projectPath = Join-Path $prototypeRoot 'src/GpuDxgiProbe/GpuDxgiProbe.csproj'

$buildOutput = & dotnet build $projectPath --configuration Release --nologo --verbosity quiet 2>&1
if ($LASTEXITCODE -ne 0) {
    throw "GPU native probe build failed:`n$($buildOutput | Out-String)"
}

$assemblyPath = Join-Path $prototypeRoot 'src/GpuDxgiProbe/bin/Release/net10.0/GpuDxgiProbe.dll'
$nativeJson = & dotnet $assemblyPath
if ($LASTEXITCODE -ne 0) { throw "GPU native probe exited with $LASTEXITCODE" }
$result = $nativeJson | ConvertFrom-Json

$cimControllers = @(Get-CimInstance -ClassName Win32_VideoController -ErrorAction SilentlyContinue | ForEach-Object {
    [ordered]@{ name = $_.Name; pnpDeviceId = $_.PNPDeviceID; adapterRam = $_.AdapterRAM; driverVersion = $_.DriverVersion }
})
$identity = $result.probes | Where-Object featureId -eq 'C-GPU-001'
$identity.evidence | Add-Member -NotePropertyName cimControllers -NotePropertyValue $cimControllers
$identity.evidence | Add-Member -NotePropertyName correlation -NotePropertyValue ([ordered]@{
    dxgiCount = @($identity.evidence.dxgiAdapters).Count
    cimCount = $cimControllers.Count
    exactNameMatches = @($identity.evidence.dxgiAdapters | Where-Object { $adapterName = $_.description; $cimControllers.name -contains $adapterName }).Count
})
if ($cimControllers.Count -eq 0) { $identity.status = 'PARTIAL' }

$parent = Split-Path -Parent $OutputPath
if ($parent) { New-Item -ItemType Directory -Path $parent -Force | Out-Null }
$result | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath $OutputPath -Encoding utf8
