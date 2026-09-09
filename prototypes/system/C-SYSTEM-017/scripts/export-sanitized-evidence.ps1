param(
    [Parameter(Mandatory)] [string]$InputPath,
    [Parameter(Mandatory)] [string]$OutputPath,
    [Parameter(Mandatory)] [string]$ManifestPath
)

$ErrorActionPreference = 'Stop'

function ConvertTo-SafeValue([object]$Value, [string]$PropertyName = '') {
    if ($PropertyName -match '(?i)email|identity|sid|mac(address)?|command(line)?|pnp(device)?id|serial(number)?|user(name)?') { return '[REDACTED]' }
    if ($null -eq $Value) { return $null }
    if ($Value -is [string]) {
        $safe = $Value -replace '(?i)\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}\b', '[REDACTED_EMAIL]'
        $safe = $safe -replace '(?i)C:\\Users\\[^\\\s]+', 'C:\Users\[REDACTED]'
        $safe = $safe -replace '(?i)\b(?:[0-9A-F]{2}[:-]){5}[0-9A-F]{2}\b', '[REDACTED_MAC]'
        $safe = $safe -replace '(?i)\bS-1-5-21-(?:\d+-){3}\d+\b', '[REDACTED_SID]'
        return $safe
    }
    if ($Value -is [Collections.IDictionary]) {
        $copy = [ordered]@{}
        foreach ($key in $Value.Keys) { $copy[$key] = ConvertTo-SafeValue $Value[$key] ([string]$key) }
        return $copy
    }
    if ($Value -is [Collections.IEnumerable] -and $Value -isnot [string]) {
        return @($Value | ForEach-Object { ConvertTo-SafeValue $_ })
    }
    if ($Value -is [psobject] -and $Value.PSObject.Properties.Count -gt 0) {
        $copy = [ordered]@{}
        foreach ($property in $Value.PSObject.Properties) { $copy[$property.Name] = ConvertTo-SafeValue $property.Value $property.Name }
        return $copy
    }
    return $Value
}

function Get-Sha256Text([string]$Text) {
    [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData([Text.Encoding]::UTF8.GetBytes($Text))).ToLowerInvariant()
}

$source = Get-Content -Raw -LiteralPath $InputPath | ConvertFrom-Json
$safe = ConvertTo-SafeValue $source
foreach ($domain in $safe.domains.Keys) {
    foreach ($probe in @($safe.domains[$domain].probes)) {
        $identity = "$domain|$($probe.id)|$((@($probe.featureIds) | Sort-Object) -join ',')"
        $probe.correlationId = (Get-Sha256Text $identity).Substring(0, 24)
    }
}
$safe.export = [ordered]@{ redactionPolicy = 'denylisted identifiers plus string patterns'; correlationScheme = 'sha256(domain|probe|sorted-featureIds), first 24 hex'; sourceSchemaVersion = $source.schemaVersion }

$outputDirectory = Split-Path -Parent $OutputPath
if ($outputDirectory) { New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null }
$json = ($safe | ConvertTo-Json -Depth 20).Replace("`r`n", "`n")
[IO.File]::WriteAllText($OutputPath, $json, [Text.UTF8Encoding]::new($false))
$reloadVerified = $false
try { $null = Get-Content -Raw -LiteralPath $OutputPath | ConvertFrom-Json; $reloadVerified = $true } catch { $reloadVerified = $false }
$hash = [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData([IO.File]::ReadAllBytes($OutputPath))).ToLowerInvariant()
$manifest = [ordered]@{ schemaVersion = 1; featureId = 'C-SYSTEM-017'; capturedAt = (Get-Date).ToString('o'); sha256 = $hash; bytes = (Get-Item -LiteralPath $OutputPath).Length; reloadVerified = $reloadVerified }
$manifestDirectory = Split-Path -Parent $ManifestPath
if ($manifestDirectory) { New-Item -ItemType Directory -Path $manifestDirectory -Force | Out-Null }
[IO.File]::WriteAllText($ManifestPath, (($manifest | ConvertTo-Json -Depth 5).Replace("`r`n", "`n")), [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
