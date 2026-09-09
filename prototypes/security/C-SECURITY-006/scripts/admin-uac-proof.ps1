param(
    [string]$OutputPath = (Join-Path (Split-Path -Parent $PSScriptRoot) 'results\admin-uac-proof.json')
)

$ErrorActionPreference = 'Stop'

$steps = [Collections.Generic.List[object]]::new()

function Add-Step {
    param([string]$Name, [string]$Status, [string]$Detail)
    $steps.Add([ordered]@{ name = $Name; status = $Status; detail = $Detail })
}

$identity = [Security.Principal.WindowsIdentity]::GetCurrent()
$principal = [Security.Principal.WindowsPrincipal]::new($identity)
$isAdmin = $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
Add-Step 'TOKEN_DETECT' 'PASS' "IsAdministrator=$isAdmin"

$netSessionOutput = (& net.exe session 2>&1 | Out-String).Trim()
$netSessionExitCode = $LASTEXITCODE

if ($isAdmin) {
    if ($netSessionExitCode -eq 0) {
        Add-Step 'ADMIN_REQUIRED_SIGNAL' 'PASS' 'Elevated token can execute net session.'
    }
    else {
        Add-Step 'ADMIN_REQUIRED_SIGNAL' 'FAIL' "Admin token but net session failed with exit code $netSessionExitCode."
    }
}
else {
    if ($netSessionExitCode -ne 0 -and $netSessionOutput -match '5|access|acesso|denied|negado') {
        Add-Step 'ADMIN_REQUIRED_SIGNAL' 'PASS' "Non-elevated token received admin-required/access-denied signal from net session: exit code $netSessionExitCode."
    }
    else {
        Add-Step 'ADMIN_REQUIRED_SIGNAL' 'FAIL' "Expected access-denied signal from non-elevated token; exit code $netSessionExitCode."
    }
}

$result = [ordered]@{
    schemaVersion = 1
    capturedAt = (Get-Date).ToString('o')
    featureId = 'C-SECURITY-006'
    isAdministrator = $isAdmin
    identityPersisted = $false
    adminProbe = [ordered]@{
        command = 'net session'
        exitCode = $netSessionExitCode
        output = $netSessionOutput
    }
    steps = @($steps)
}

$outputDirectory = Split-Path -Parent $OutputPath
New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
$json = ($result | ConvertTo-Json -Depth 8).Replace("`r`n", "`n")
[IO.File]::WriteAllText($OutputPath, $json, [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
