param([Parameter(Mandatory)][string]$OutputPath)

$ErrorActionPreference = 'Stop'

$volumes = @(Get-Volume | Where-Object DriveLetter | Sort-Object DriveLetter | ForEach-Object {
    $root = "$($_.DriveLetter):\"
    [ordered]@{
        root = $root
        driveType = [string]$_.DriveType
        fileSystem = if ($_.FileSystemType) { [string]$_.FileSystemType } else { 'UNKNOWN' }
        health = if ($_.HealthStatus) { [string]$_.HealthStatus } else { 'UNKNOWN' }
        operational = @($_.OperationalStatus | ForEach-Object { [string]$_ })
        sizeBytes = [uint64]$_.Size
        freeBytes = [uint64]$_.SizeRemaining
        candidateLabels = @(
            if ($root -eq "$env:SystemDrive\") { 'SYSTEM_TEMP' }
            if ($root -eq "$(Split-Path -Qualifier $PSScriptRoot)\") { 'PROTOTYPE_ROOT' }
        )
    }
})

$result = [ordered]@{
    schemaVersion = 1
    featureId = 'C-CLEANING-009'
    capturedAt = (Get-Date).ToString('o')
    status = if ($volumes.Count -gt 0) { 'PASS' } else { 'FAIL' }
    mutationCount = 0
    source = 'Get-Volume read-only'
    actualVolumes = $volumes
    normalizationFixtures = @(
        [ordered]@{ input = 'fixed-online'; classification = 'FIXED'; eligibleForAutomaticCleanup = $false }
        [ordered]@{ input = 'removable-present'; classification = 'REMOVABLE'; eligibleForAutomaticCleanup = $false }
        [ordered]@{ input = 'network-path'; classification = 'NETWORK'; eligibleForAutomaticCleanup = $false }
        [ordered]@{ input = 'known-volume-not-mounted'; classification = 'OFFLINE'; eligibleForAutomaticCleanup = $false }
    )
    limitations = @(
        'No cleanup Apply was performed.'
        'Offline, removable and network states are normalization fixtures, not claims about attached hardware.'
        'Mount-point and reparse traversal remain outside this bounded proof.'
    )
}

$directory = Split-Path -Parent $OutputPath
if ($directory) { New-Item -ItemType Directory -Path $directory -Force | Out-Null }
[IO.File]::WriteAllText($OutputPath, (($result | ConvertTo-Json -Depth 8).Replace("`r`n", "`n")), [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
