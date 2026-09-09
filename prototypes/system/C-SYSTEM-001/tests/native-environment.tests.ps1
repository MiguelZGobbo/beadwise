param(
    [string]$PrototypeRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'

$project = Join-Path $PrototypeRoot 'src\NativeEnvironmentProbe\NativeEnvironmentProbe.csproj'
$result = Join-Path $PrototypeRoot 'results\native-environment.json'

if (-not (Test-Path -LiteralPath $project)) {
    throw "Missing native probe project: $project"
}

$output = dotnet run --project $project --configuration Release
if ($LASTEXITCODE -ne 0) {
    throw "dotnet run failed with $LASTEXITCODE"
}

$outputDirectory = Split-Path -Parent $result
New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
[IO.File]::WriteAllText($result, (($output | Out-String).Trim() + "`n"), [Text.UTF8Encoding]::new($false))

$json = Get-Content -Raw -LiteralPath $result | ConvertFrom-Json
if ($json.api -ne 'GetNativeSystemInfo/RtlGetVersion') {
    throw 'Native API marker missing.'
}
if ($json.rtlStatus -ne 0) {
    throw "RtlGetVersion returned status $($json.rtlStatus)"
}
if ([int]$json.processorCount -lt 1) {
    throw 'GetNativeSystemInfo returned an invalid processor count.'
}
if ([int]$json.windowsBuild -lt 22000) {
    throw "Unexpected Windows build from RtlGetVersion: $($json.windowsBuild)"
}

Write-Output 'PASS: native Win32/PInvoke environment probe returned coherent data'
