param(
    [Parameter(Mandatory)] [string]$OutputPath,
    [switch]$IncludeCommands
)

$ErrorActionPreference = 'Stop'
$entries = @()
$sources = @()

function Add-SourceResult([string]$Name, [scriptblock]$Collector) {
    try {
        $script:entries += @(& $Collector)
        $script:sources += [ordered]@{ name = $Name; status = 'PASS'; error = $null }
    } catch {
        $script:sources += [ordered]@{ name = $Name; status = 'FAIL'; error = $_.Exception.Message }
    }
}

Add-SourceResult 'RegistryRun' {
    $locations = @(
        @{ Path = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Run'; Scope = 'CurrentUser' },
        @{ Path = 'HKLM:\Software\Microsoft\Windows\CurrentVersion\Run'; Scope = 'LocalMachine' }
    )
    foreach ($location in $locations) {
        if (-not (Test-Path -LiteralPath $location.Path)) { continue }
        $key = Get-Item -LiteralPath $location.Path
        foreach ($name in $key.GetValueNames()) {
            $command = [string]$key.GetValue($name, $null, 'DoNotExpandEnvironmentNames')
            [ordered]@{
                sourceKind = 'RegistryRun'
                scope = $location.Scope
                name = $name
                command = if ($IncludeCommands) { $command } else { $null }
                commandSha256 = if ($IncludeCommands) { $null } else { [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData([Text.Encoding]::UTF8.GetBytes($command))).ToLowerInvariant() }
                transient = $false
                provenance = $location.Path
            }
        }
    }
}

Add-SourceResult 'StartupFolder' {
    $folders = @(
        @{ Path = [Environment]::GetFolderPath('Startup'); Scope = 'CurrentUser' },
        @{ Path = [Environment]::GetFolderPath('CommonStartup'); Scope = 'AllUsers' }
    )
    foreach ($folder in $folders) {
        if ([string]::IsNullOrWhiteSpace($folder.Path) -or -not (Test-Path -LiteralPath $folder.Path)) { continue }
        foreach ($item in Get-ChildItem -LiteralPath $folder.Path -File -ErrorAction Stop) {
            [ordered]@{ sourceKind = 'StartupFolder'; scope = $folder.Scope; name = $item.Name; command = $null; commandSha256 = $null; transient = $false; provenance = $folder.Path }
        }
    }
}

$result = [ordered]@{
    schemaVersion = 1
    featureId = 'C-STARTUP-001'
    capturedAt = (Get-Date).ToString('o')
    mutationPolicy = 'read-only'
    sources = $sources
    entries = $entries
}
$directory = Split-Path -Parent $OutputPath
if ($directory) { New-Item -ItemType Directory -Path $directory -Force | Out-Null }
[IO.File]::WriteAllText($OutputPath, (($result | ConvertTo-Json -Depth 7).Replace("`r`n", "`n")), [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
