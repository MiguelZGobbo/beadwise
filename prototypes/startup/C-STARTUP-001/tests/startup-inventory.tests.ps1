$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$probe = Join-Path $prototypeRoot 'scripts\collect-startup-inventory.ps1'
$runPath = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Run'
$proofName = 'BeadWisePhase2Proof'
$proofCommand = "$env:SystemRoot\System32\cmd.exe /c exit 0"
$resultPath = Join-Path $env:TEMP 'beadwise-c-startup-001-test.json'
$original = try { Get-ItemPropertyValue -LiteralPath $runPath -Name $proofName -ErrorAction Stop } catch { $null }
$originalExisted = $null -ne $original

try {
    New-ItemProperty -LiteralPath $runPath -Name $proofName -PropertyType String -Value $proofCommand -Force | Out-Null
    & $probe -OutputPath $resultPath -IncludeCommands
    $result = Get-Content -Raw -LiteralPath $resultPath | ConvertFrom-Json
    $entry = @($result.entries | Where-Object { $_.sourceKind -eq 'RegistryRun' -and $_.name -eq $proofName })
    if ($entry.Count -ne 1) { throw 'Controlled HKCU Run entry was not detected exactly once' }
    if ($entry[0].scope -ne 'CurrentUser') { throw 'Registry source scope was not preserved' }
    if ($entry[0].command -ne $proofCommand) { throw 'Registry command was not preserved' }
    if (@($result.sources | Where-Object status -eq 'PASS').Count -lt 2) { throw 'Expected multiple source collectors' }

    Write-Output 'PASS: controlled startup fixture was detected and normalized'
}
finally {
    if ($originalExisted) {
        New-ItemProperty -LiteralPath $runPath -Name $proofName -PropertyType String -Value $original -Force | Out-Null
    } else {
        Remove-ItemProperty -LiteralPath $runPath -Name $proofName -Force -ErrorAction SilentlyContinue
    }
    Remove-Item -LiteralPath $resultPath -Force -ErrorAction SilentlyContinue
}
