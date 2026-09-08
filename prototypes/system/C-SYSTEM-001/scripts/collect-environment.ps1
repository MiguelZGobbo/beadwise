param(
    [string]$OutputPath = (Join-Path (Split-Path -Parent $PSScriptRoot) 'results\environment.json')
)

$ErrorActionPreference = 'Stop'

function Get-FormFactor {
    param([int]$PcSystemType)

    switch ($PcSystemType) {
        1 { 'Desktop' }
        2 { 'Laptop' }
        3 { 'Workstation' }
        4 { 'EnterpriseServer' }
        5 { 'SOHOServer' }
        6 { 'AppliancePC' }
        7 { 'PerformanceServer' }
        8 { 'Slate' }
        default { 'Unknown' }
    }
}

$operatingSystem = Get-CimInstance -ClassName Win32_OperatingSystem
$computerSystem = Get-CimInstance -ClassName Win32_ComputerSystem
$processors = @(Get-CimInstance -ClassName Win32_Processor)
$videoControllers = @(Get-CimInstance -ClassName Win32_VideoController)
$baseBoards = @(Get-CimInstance -ClassName Win32_BaseBoard)
$batteries = @(Get-CimInstance -ClassName Win32_Battery -ErrorAction SilentlyContinue)
$identity = [Security.Principal.WindowsIdentity]::GetCurrent()
$principal = [Security.Principal.WindowsPrincipal]::new($identity)
$isAdministrator = $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)

$result = [ordered]@{
    capturedAt = (Get-Date).ToString('o')
    windowsEdition = $operatingSystem.Caption
    windowsVersion = $operatingSystem.Version
    windowsBuild = [string]$operatingSystem.BuildNumber
    architecture = $operatingSystem.OSArchitecture
    cpu = @($processors | ForEach-Object {
        [ordered]@{
            name = $_.Name.Trim()
            manufacturer = $_.Manufacturer
            cores = $_.NumberOfCores
            logicalProcessors = $_.NumberOfLogicalProcessors
        }
    })
    gpu = @($videoControllers | ForEach-Object {
        [ordered]@{
            name = $_.Name
            driverVersion = $_.DriverVersion
        }
    })
    motherboard = @($baseBoards | ForEach-Object {
        [ordered]@{
            manufacturer = $_.Manufacturer
            product = $_.Product
            version = $_.Version
        }
    })
    formFactor = Get-FormFactor -PcSystemType $computerSystem.PCSystemType
    isAdministrator = $isAdministrator
    hasBattery = $batteries.Count -gt 0
    hypervisorPresent = [bool]$computerSystem.HypervisorPresent
}

$outputDirectory = Split-Path -Parent $OutputPath
if ($outputDirectory) {
    New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
}

$json = ($result | ConvertTo-Json -Depth 6).Replace("`r`n", "`n")
[IO.File]::WriteAllText($OutputPath, $json, [Text.UTF8Encoding]::new($false))
Write-Output $json
