param(
    [string]$PrototypeRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'
$proofScript = Join-Path $PrototypeRoot 'scripts\cleanup-sandbox-proof.ps1'
$resultPath = Join-Path $PrototypeRoot 'results\cleanup-sandbox-proof.json'

if (-not (Test-Path -LiteralPath $proofScript)) {
    throw "Missing proof script: $proofScript"
}

& $proofScript -OutputPath $resultPath | Out-Null
$result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
$rawResult = Get-Content -Raw -LiteralPath $resultPath

$expectedCapabilities = @(
    'C-CLEANING-001',
    'C-CLEANING-002',
    'C-CLEANING-005',
    'C-CLEANING-006',
    'C-CLEANING-007'
)

foreach ($capability in $expectedCapabilities) {
    if ($result.capabilities -notcontains $capability) {
        throw "Missing capability mapping: $capability"
    }
}

foreach ($stepName in @('FIXTURE', 'DETECT', 'PLAN', 'DRY_RUN', 'APPLY', 'VERIFY', 'CLEANUP')) {
    $step = $result.steps | Where-Object name -eq $stepName | Select-Object -First 1
    if (-not $step) { throw "Missing step: $stepName" }
    if ($step.status -ne 'PASS') { throw "Step $stepName was $($step.status): $($step.detail)" }
}

if ($result.sandbox.existsAfterCleanup) { throw 'Prototype-owned sandbox survived cleanup.' }
if ($result.dryRun.hashBefore -ne $result.dryRun.hashAfter) { throw 'Dry-run mutated the sandbox.' }
if ($result.verify.deletedCount -ne 2) { throw "Expected 2 eligible deletions, got $($result.verify.deletedCount)." }
if ($result.verify.retainedLocked -ne $true) { throw 'Locked artifact was not retained.' }
if ($result.verify.retainedProtected -ne $true) { throw 'Protected artifact was not retained.' }
if ($result.verify.retainedReparse -ne $true) { throw 'Reparse point was not retained.' }
if ($result.verify.outsideSentinelIntact -ne $true) { throw 'Traversal escaped the authorized sandbox.' }
if ($result.verify.actualFreedBytes -ne $result.plan.eligibleBytes) { throw 'Verified bytes do not match the plan.' }
if ($result.plan.authorizationRootKind -ne 'prototype-owned-user-temp') { throw 'Authorization boundary was not represented without a raw path.' }
if ($rawResult -match '(?i)C:\\Users\\') { throw 'Evidence persisted a user-profile path.' }

Write-Output 'PASS: isolated cleanup plan, dry-run, apply, verification and cleanup are deterministic'
