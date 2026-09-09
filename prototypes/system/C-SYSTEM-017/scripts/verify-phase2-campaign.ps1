param(
    [string]$RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..\..\..')).Path,
    [string]$OutputPath = (Join-Path (Split-Path -Parent $PSScriptRoot) 'results\final-verification.json'),
    [switch]$RunTests
)

$ErrorActionPreference = 'Stop'
$specFiles = @(Get-ChildItem -LiteralPath (Join-Path $RepositoryRoot 'feature-specs') -Recurse -File -Filter '*.md')
$specRows = foreach ($file in $specFiles) {
    $content = [IO.File]::ReadAllText($file.FullName)
    [pscustomobject]@{
        id = [regex]::Match($content, '(?m)^ID:\s*(C-[A-Z]+-\d+)\s*$').Groups[1].Value
        status = [regex]::Match($content, '(?m)^Status:\s*([A-Z_]+)').Groups[1].Value
        hasAudit = $content.Contains('PHASE2-FINAL-SPECIFIED-AUDIT:START')
    }
}

$tests = @(Get-ChildItem -LiteralPath (Join-Path $RepositoryRoot 'prototypes') -Recurse -File -Filter '*.tests.ps1' | Sort-Object FullName)
$failedTests = @()
if ($RunTests) {
    foreach ($test in $tests) {
        & pwsh -NoLogo -NoProfile -NonInteractive -File $test.FullName *> $null
        if ($LASTEXITCODE -ne 0) { $failedTests += $test.FullName.Substring($RepositoryRoot.Length + 1).Replace('\', '/') }
    }
}

$resultFiles = @(Get-ChildItem -LiteralPath (Join-Path $RepositoryRoot 'prototypes') -Recurse -File | Where-Object { $_.FullName -match '[\\/]results[\\/]' })
$privacyPattern = '(?i)([A-Z]:\\Users\\(?!\[REDACTED\]|<redacted>|REDACTED|user\\)|[\w.+-]+@[\w.-]+\.[A-Z]{2,}|\b(?:[0-9A-F]{2}[:-]){5}[0-9A-F]{2}\b|S-1-5-21-(?:\d+-){3}\d+)'
$privacyMatches = @()
foreach ($file in $resultFiles) {
    if ($file.Extension -notin @('.json', '.md', '.txt', '.csv')) { continue }
    $matches = [regex]::Matches([IO.File]::ReadAllText($file.FullName), $privacyPattern)
    if ($matches.Count -gt 0) { $privacyMatches += $file.FullName.Substring($RepositoryRoot.Length + 1).Replace('\', '/') }
}

$matrixPath = Join-Path $RepositoryRoot 'PHASE-2-PROOF-MATRIX.md'
$matrix = if (Test-Path -LiteralPath $matrixPath) { [IO.File]::ReadAllText($matrixPath) } else { '' }
$coverage = [regex]::Match($matrix, 'Cobertura explícita de prova segura entre specs `SPECIFIED`: (\d+)/(\d+)')
$statuses = [ordered]@{}
foreach ($name in @('RESEARCH','SPECIFIED','PROTOTYPING','PROVEN','APPROVED','BLOCKED','DEFERRED','REJECTED')) {
    $statuses[$name] = @($specRows | Where-Object status -eq $name).Count
}
$conditions = [ordered]@{
    specCount = ($specRows.Count -eq 234)
    uniqueIds = (@($specRows.id | Sort-Object -Unique).Count -eq 234)
    specifiedAudited = (@($specRows | Where-Object status -eq 'SPECIFIED' | Where-Object hasAudit).Count -eq $statuses.SPECIFIED)
    specifiedSafeProofCovered = ($coverage.Success -and [int]$coverage.Groups[1].Value -eq $statuses.SPECIFIED -and [int]$coverage.Groups[2].Value -eq $statuses.SPECIFIED)
    matrixRows = ([regex]::Matches($matrix, '(?m)^\| C-[A-Z]+-\d+ \|').Count -eq 234)
    noProvenOrApproved = ($statuses.PROVEN -eq 0 -and $statuses.APPROVED -eq 0)
    noPrivacyMatches = ($privacyMatches.Count -eq 0)
    testsExecuted = [bool]$RunTests
    testsPassed = ([bool]$RunTests -and $failedTests.Count -eq 0)
}
$verificationStatus = if (@($conditions.Values | Where-Object { -not $_ }).Count -eq 0) { 'PASS' } else { 'FAIL' }
$result = [ordered]@{
    schemaVersion = 1
    capturedAt = (Get-Date).ToString('o')
    status = $verificationStatus
    conditions = $conditions
    specCount = $specRows.Count
    statusDistribution = $statuses
    testCount = $tests.Count
    failedTests = $failedTests
    resultFileCount = $resultFiles.Count
    privacyMatches = $privacyMatches
}
$directory = Split-Path -Parent $OutputPath
if ($directory) { New-Item -ItemType Directory -Path $directory -Force | Out-Null }
[IO.File]::WriteAllText($OutputPath, (($result | ConvertTo-Json -Depth 8).Replace("`r`n", "`n")), [Text.UTF8Encoding]::new($false))
Write-Output "${verificationStatus}: specs=$($specRows.Count), tests=$($tests.Count), failed=$($failedTests.Count), privacy=$($privacyMatches.Count)"
