param(
    [Parameter(Mandatory)] [ValidateSet('Capture', 'Compare')] [string]$Mode,
    [string]$RegistryPath,
    [string]$BaselinePath,
    [string]$CurrentPath,
    [Parameter(Mandatory)] [string]$OutputPath
)

$ErrorActionPreference = 'Stop'

if ($Mode -eq 'Capture') {
    if ([string]::IsNullOrWhiteSpace($RegistryPath)) { throw 'RegistryPath is required for Capture' }
    $exists = Test-Path -LiteralPath $RegistryPath
    $values = @()
    if ($exists) {
        $key = Get-Item -LiteralPath $RegistryPath
        $values = @($key.GetValueNames() | Sort-Object | ForEach-Object {
            [ordered]@{ name = $_; kind = $key.GetValueKind($_).ToString(); value = [string]$key.GetValue($_, $null, 'DoNotExpandEnvironmentNames') }
        })
    }
    $result = [ordered]@{
        schemaVersion = 1
        featureId = 'C-SYSTEM-007'
        capturedAt = (Get-Date).ToString('o')
        source = 'Registry'
        registryPath = $RegistryPath
        exists = $exists
        values = $values
    }
} else {
    if (-not (Test-Path -LiteralPath $BaselinePath) -or -not (Test-Path -LiteralPath $CurrentPath)) { throw 'BaselinePath and CurrentPath are required for Compare' }
    $baseline = Get-Content -Raw -LiteralPath $BaselinePath | ConvertFrom-Json
    $current = Get-Content -Raw -LiteralPath $CurrentPath | ConvertFrom-Json
    if ($baseline.registryPath -ne $current.registryPath) { throw 'Snapshots refer to different registry paths' }
    $changes = @()
    if ($baseline.exists -ne $current.exists) {
        $changes += [ordered]@{ kind = 'KeyExistenceChanged'; before = $baseline.exists; after = $current.exists }
    }
    if (($baseline.values | ConvertTo-Json -Compress) -ne ($current.values | ConvertTo-Json -Compress)) {
        $changes += [ordered]@{ kind = 'ValuesChanged'; before = @($baseline.values); after = @($current.values) }
    }
    $result = [ordered]@{
        schemaVersion = 1
        featureId = 'C-SYSTEM-007'
        comparedAt = (Get-Date).ToString('o')
        source = 'Registry'
        registryPath = $baseline.registryPath
        changed = $changes.Count -gt 0
        changes = $changes
    }
}

$directory = Split-Path -Parent $OutputPath
if ($directory) { New-Item -ItemType Directory -Path $directory -Force | Out-Null }
[IO.File]::WriteAllText($OutputPath, (($result | ConvertTo-Json -Depth 8).Replace("`r`n", "`n")), [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
