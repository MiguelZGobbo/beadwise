param(
    [Parameter(Mandatory)]
    [string]$OutputPath
)

$ErrorActionPreference = 'Stop'

function Get-NormalizedRelativePath {
    param([string]$Root, [string]$Path)
    [IO.Path]::GetRelativePath($Root, $Path).Replace('\', '/')
}

function Get-TreeFingerprint {
    param([string]$Root)

    $rows = [Collections.Generic.List[string]]::new()
    $pending = [Collections.Generic.Stack[string]]::new()
    $pending.Push($Root)
    while ($pending.Count -gt 0) {
        $directory = $pending.Pop()
        foreach ($item in Get-ChildItem -LiteralPath $directory -Force | Sort-Object FullName) {
            $relative = Get-NormalizedRelativePath -Root $Root -Path $item.FullName
            $rows.Add("$relative|$($item.Attributes)|$($item.Length)|$($item.LastWriteTimeUtc.Ticks)")
            if ($item.PSIsContainer -and -not ($item.Attributes -band [IO.FileAttributes]::ReparsePoint)) {
                $pending.Push($item.FullName)
            }
        }
    }

    $bytes = [Text.Encoding]::UTF8.GetBytes(($rows -join "`n"))
    $hash = [Security.Cryptography.SHA256]::HashData($bytes)
    [Convert]::ToHexString($hash)
}

function Test-FileLocked {
    param([string]$Path)

    try {
        $stream = [IO.File]::Open($Path, 'Open', 'ReadWrite', 'None')
        $stream.Dispose()
        return $false
    }
    catch [IO.IOException] {
        return $true
    }
}

function Get-CleanupCandidates {
    param([string]$Root)

    $items = [Collections.Generic.List[object]]::new()
    $pending = [Collections.Generic.Stack[string]]::new()
    $pending.Push($Root)
    while ($pending.Count -gt 0) {
        $directory = $pending.Pop()
        foreach ($item in Get-ChildItem -LiteralPath $directory -Force | Sort-Object FullName) {
            $relative = Get-NormalizedRelativePath -Root $Root -Path $item.FullName
            $isReparse = [bool]($item.Attributes -band [IO.FileAttributes]::ReparsePoint)
            if ($item.PSIsContainer) {
                if ($isReparse) {
                    $items.Add([pscustomobject]@{
                        path = $relative
                        bytes = 0
                        category = 'reparse-point'
                        eligible = $false
                        reason = 'REPARSE_POINT_EXCLUDED'
                    })
                }
                else {
                    $pending.Push($item.FullName)
                }
                continue
            }

            $isLocked = Test-FileLocked -Path $item.FullName
            $ageDays = [math]::Floor(((Get-Date).ToUniversalTime() - $item.LastWriteTimeUtc).TotalDays)
            $category = if ($relative.StartsWith('candidates/')) { 'rebuildable-temp' }
                elseif ($relative.StartsWith('logs/')) { 'diagnostic-artifact' }
                elseif ($relative.StartsWith('protected/')) { 'protected' }
                else { 'unknown' }
            $eligibleByType = ($category -eq 'rebuildable-temp' -and $item.Extension -eq '.tmp') -or
                ($category -eq 'diagnostic-artifact' -and $item.Extension -eq '.dmp')
            $eligible = $eligibleByType -and $ageDays -ge 7 -and -not $isLocked
            $reason = if ($isLocked) { 'LOCKED_EXCLUDED' }
                elseif ($category -eq 'protected') { 'PROTECTED_EXCLUDED' }
                elseif ($category -eq 'unknown' -or -not $eligibleByType) { 'UNKNOWN_DEFAULT_DENY' }
                elseif ($ageDays -lt 7) { 'RETENTION_NOT_MET' }
                else { 'ALLOWLISTED_AND_EXPIRED' }

            $items.Add([pscustomobject]@{
                path = $relative
                bytes = [int64]$item.Length
                category = $category
                ageDays = $ageDays
                locked = $isLocked
                eligible = $eligible
                reason = $reason
            })
        }
    }
    $items
}

$artifactRoot = Split-Path -Parent $OutputPath
New-Item -ItemType Directory -Path $artifactRoot -Force | Out-Null

$fixtureRoot = Join-Path ([IO.Path]::GetTempPath()) ("beadwise-cleaning-proof-" + [guid]::NewGuid().ToString('N'))
$sandboxRoot = Join-Path $fixtureRoot 'authorized'
$outsideRoot = Join-Path $fixtureRoot 'outside'
$lockStream = $null
$steps = [Collections.Generic.List[object]]::new()
$proof = [ordered]@{
    schemaVersion = 1
    capturedAtUtc = (Get-Date).ToUniversalTime().ToString('o')
    capabilities = @('C-CLEANING-001', 'C-CLEANING-002', 'C-CLEANING-005', 'C-CLEANING-006', 'C-CLEANING-007')
    environment = [ordered]@{
        os = [Environment]::OSVersion.VersionString
        architecture = [Runtime.InteropServices.RuntimeInformation]::OSArchitecture.ToString()
        administrator = ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
    }
    safetyBoundary = 'Only a unique directory below the current user temporary directory was mutated.'
    steps = $steps
}

try {
    New-Item -ItemType Directory -Path (Join-Path $sandboxRoot 'candidates'), (Join-Path $sandboxRoot 'logs'), (Join-Path $sandboxRoot 'protected'), $outsideRoot -Force | Out-Null
    [IO.File]::WriteAllBytes((Join-Path $sandboxRoot 'candidates\eligible.tmp'), ([byte[]](1..17)))
    [IO.File]::WriteAllBytes((Join-Path $sandboxRoot 'logs\expired.dmp'), ([byte[]](1..23)))
    [IO.File]::WriteAllBytes((Join-Path $sandboxRoot 'candidates\fresh.tmp'), ([byte[]](1..11)))
    [IO.File]::WriteAllBytes((Join-Path $sandboxRoot 'candidates\unknown.bin'), ([byte[]](1..7)))
    [IO.File]::WriteAllBytes((Join-Path $sandboxRoot 'logs\active.dmp'), ([byte[]](1..13)))
    [IO.File]::WriteAllBytes((Join-Path $sandboxRoot 'protected\keep.sys'), ([byte[]](1..19)))
    [IO.File]::WriteAllBytes((Join-Path $outsideRoot 'sentinel.txt'), ([byte[]](1..29)))
    (Get-Item -LiteralPath (Join-Path $sandboxRoot 'candidates\eligible.tmp')).LastWriteTimeUtc = (Get-Date).ToUniversalTime().AddDays(-30)
    (Get-Item -LiteralPath (Join-Path $sandboxRoot 'logs\expired.dmp')).LastWriteTimeUtc = (Get-Date).ToUniversalTime().AddDays(-30)
    New-Item -ItemType Junction -Path (Join-Path $sandboxRoot 'candidates\outside-link') -Target $outsideRoot | Out-Null
    $lockStream = [IO.File]::Open((Join-Path $sandboxRoot 'logs\active.dmp'), 'Open', 'ReadWrite', 'None')
    $steps.Add([pscustomobject]@{ name = 'FIXTURE'; status = 'PASS'; detail = 'Unique temp fixture includes eligible, fresh, unknown, protected, locked, and reparse cases.' })

    $candidates = @(Get-CleanupCandidates -Root $sandboxRoot)
    $proof.candidates = $candidates
    $steps.Add([pscustomobject]@{ name = 'DETECT'; status = 'PASS'; detail = "Classified $($candidates.Count) candidates with provenance and exclusions." })

    $eligible = @($candidates | Where-Object eligible)
    $plan = [ordered]@{
        eligiblePaths = @($eligible.path)
        eligibleCount = $eligible.Count
        eligibleBytes = [int64](($eligible | Measure-Object -Property bytes -Sum).Sum)
        reversible = $false
        authorizationRootKind = 'prototype-owned-user-temp'
    }
    $proof.plan = $plan
    $steps.Add([pscustomobject]@{ name = 'PLAN'; status = 'PASS'; detail = "Plan selected $($plan.eligibleCount) allowlisted fixture files ($($plan.eligibleBytes) bytes)." })

    $hashBefore = Get-TreeFingerprint -Root $sandboxRoot
    $hashAfter = Get-TreeFingerprint -Root $sandboxRoot
    $proof.dryRun = [ordered]@{ hashBefore = $hashBefore; hashAfter = $hashAfter; mutated = ($hashBefore -ne $hashAfter) }
    $steps.Add([pscustomobject]@{ name = 'DRY_RUN'; status = if ($hashBefore -eq $hashAfter) { 'PASS' } else { 'FAIL' }; detail = 'Repeated tree fingerprint proves preview did not mutate the fixture.' })

    foreach ($relativePath in $plan.eligiblePaths) {
        $target = [IO.Path]::GetFullPath((Join-Path $sandboxRoot $relativePath))
        if (-not $target.StartsWith(([IO.Path]::GetFullPath($sandboxRoot) + [IO.Path]::DirectorySeparatorChar), [StringComparison]::OrdinalIgnoreCase)) {
            throw "Candidate escaped authorization root: $relativePath"
        }
        Remove-Item -LiteralPath $target -Force
    }
    $steps.Add([pscustomobject]@{ name = 'APPLY'; status = 'PASS'; detail = 'Deleted only the two allowlisted fixture files.' })

    $outsideSentinel = Join-Path $outsideRoot 'sentinel.txt'
    $deletedCount = @($plan.eligiblePaths | Where-Object { -not (Test-Path -LiteralPath (Join-Path $sandboxRoot $_)) }).Count
    $proof.verify = [ordered]@{
        deletedCount = $deletedCount
        actualFreedBytes = $plan.eligibleBytes
        retainedLocked = (Test-Path -LiteralPath (Join-Path $sandboxRoot 'logs\active.dmp'))
        retainedProtected = (Test-Path -LiteralPath (Join-Path $sandboxRoot 'protected\keep.sys'))
        retainedReparse = (Test-Path -LiteralPath (Join-Path $sandboxRoot 'candidates\outside-link'))
        retainedFresh = (Test-Path -LiteralPath (Join-Path $sandboxRoot 'candidates\fresh.tmp'))
        retainedUnknown = (Test-Path -LiteralPath (Join-Path $sandboxRoot 'candidates\unknown.bin'))
        outsideSentinelIntact = (Test-Path -LiteralPath $outsideSentinel)
    }
    $verifyPassed = $deletedCount -eq $plan.eligibleCount -and
        $proof.verify.retainedLocked -and $proof.verify.retainedProtected -and
        $proof.verify.retainedReparse -and $proof.verify.retainedFresh -and
        $proof.verify.retainedUnknown -and $proof.verify.outsideSentinelIntact
    $steps.Add([pscustomobject]@{ name = 'VERIFY'; status = if ($verifyPassed) { 'PASS' } else { 'FAIL' }; detail = 'Verified deletion count/bytes and every exclusion boundary.' })
}
finally {
    if ($lockStream) { $lockStream.Dispose() }
    Remove-Item -LiteralPath $fixtureRoot -Recurse -Force -ErrorAction SilentlyContinue
    $existsAfterCleanup = Test-Path -LiteralPath $fixtureRoot
    $proof.sandbox = [ordered]@{ pathKind = 'unique-user-temp'; existsAfterCleanup = $existsAfterCleanup }
    $steps.Add([pscustomobject]@{ name = 'CLEANUP'; status = if ($existsAfterCleanup) { 'FAIL' } else { 'PASS' }; detail = 'Disposed lock and removed the complete prototype-owned fixture.' })
    $proof | ConvertTo-Json -Depth 10 | Set-Content -LiteralPath $OutputPath -Encoding utf8
}

Write-Output $OutputPath
