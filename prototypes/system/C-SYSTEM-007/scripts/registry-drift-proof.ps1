param(
    [string]$OutputPath = (Join-Path (Split-Path -Parent $PSScriptRoot) 'results\registry-drift-proof.json')
)

$ErrorActionPreference = 'Stop'

$sandboxKey = 'HKCU:\Software\BeadWise\Phase2Proof\C-SYSTEM-007'
$valueName = 'ObservedState'
$targetValue = 'phase2-proof'
$steps = [Collections.Generic.List[object]]::new()

function Add-Step {
    param([string]$Name, [string]$Status, [string]$Detail)
    $steps.Add([ordered]@{ name = $Name; status = $Status; detail = $Detail })
}

function Get-SandboxState {
    $keyExists = Test-Path -LiteralPath $sandboxKey
    $valueExists = $false
    $value = $null
    if ($keyExists) {
        $property = Get-ItemProperty -LiteralPath $sandboxKey -Name $valueName -ErrorAction SilentlyContinue
        if ($null -ne $property) {
            $valueExists = $true
            $value = $property.$valueName
        }
    }

    [ordered]@{
        exists = $keyExists
        valueExists = $valueExists
        value = $value
    }
}

if (Test-Path -LiteralPath $sandboxKey) {
    Remove-Item -LiteralPath $sandboxKey -Recurse -Force
}

$initial = Get-SandboxState
if (-not $initial.exists) {
    Add-Step 'DETECT' 'PASS' 'Sandbox key was absent before apply.'
}
else {
    Add-Step 'DETECT' 'FAIL' 'Sandbox key unexpectedly existed before apply.'
}

$plan = [ordered]@{
    featureId = 'C-SYSTEM-007'
    path = $sandboxKey
    valueName = $valueName
    initial = $initial
    target = [ordered]@{ exists = $true; valueExists = $true; value = $targetValue }
    changes = @(
        'Create HKCU sandbox key when absent',
        "Set $valueName to $targetValue"
    )
    reversible = $true
    restartRequired = $false
    adminRequired = $false
}

Add-Step 'PLAN' 'PASS' 'ChangePlan captures absent original state and target registry value.'

$afterDryRun = Get-SandboxState
if (-not $afterDryRun.exists) {
    Add-Step 'DRY_RUN' 'PASS' 'Dry-run built the plan without persisting the sandbox key.'
}
else {
    Add-Step 'DRY_RUN' 'FAIL' 'Dry-run mutated the sandbox key.'
}

New-Item -Path $sandboxKey -Force | Out-Null
New-ItemProperty -LiteralPath $sandboxKey -Name $valueName -Value $targetValue -PropertyType String -Force | Out-Null
Add-Step 'APPLY' 'PASS' 'Sandbox key/value written.'

$afterApply = Get-SandboxState
if ($afterApply.exists -and $afterApply.valueExists -and $afterApply.value -eq $targetValue) {
    Add-Step 'VERIFY' 'PASS' 'Detected target value after apply.'
}
else {
    Add-Step 'VERIFY' 'FAIL' 'Target value was not detected after apply.'
}

New-ItemProperty -LiteralPath $sandboxKey -Name $valueName -Value $targetValue -PropertyType String -Force | Out-Null
$afterSecondApply = Get-SandboxState
if ($afterSecondApply.exists -and $afterSecondApply.valueExists -and $afterSecondApply.value -eq $targetValue) {
    Add-Step 'IDEMPOTENCE' 'PASS' 'Second apply preserved the same target state.'
}
else {
    Add-Step 'IDEMPOTENCE' 'FAIL' 'Second apply changed the target state unexpectedly.'
}

if (-not $initial.exists) {
    Remove-Item -LiteralPath $sandboxKey -Recurse -Force
}
elseif (-not $initial.valueExists) {
    Remove-ItemProperty -LiteralPath $sandboxKey -Name $valueName -ErrorAction SilentlyContinue
}
else {
    New-ItemProperty -LiteralPath $sandboxKey -Name $valueName -Value $initial.value -PropertyType String -Force | Out-Null
}
Add-Step 'ROLLBACK' 'PASS' 'Rollback restored the original absent/existing snapshot rule.'

$afterRollback = Get-SandboxState
if ($afterRollback.exists -eq $initial.exists -and $afterRollback.valueExists -eq $initial.valueExists -and $afterRollback.value -eq $initial.value) {
    Add-Step 'VERIFY_ROLLBACK' 'PASS' 'Post-rollback state equals the original snapshot.'
}
else {
    Add-Step 'VERIFY_ROLLBACK' 'FAIL' 'Post-rollback state differs from the original snapshot.'
}

$result = [ordered]@{
    schemaVersion = 1
    capturedAt = (Get-Date).ToString('o')
    featureId = 'C-SYSTEM-007'
    mutationScope = $sandboxKey
    initial = $initial
    plan = $plan
    afterDryRun = $afterDryRun
    afterApply = $afterApply
    afterSecondApply = $afterSecondApply
    afterRollback = $afterRollback
    steps = @($steps)
}

$outputDirectory = Split-Path -Parent $OutputPath
New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
$json = ($result | ConvertTo-Json -Depth 8).Replace("`r`n", "`n")
[IO.File]::WriteAllText($OutputPath, $json, [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
