$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$collector = Join-Path $prototypeRoot 'scripts\collect-environment.ps1'
$testOutput = Join-Path $env:TEMP 'beadwise-c-system-001-test.json'

try {
    & $collector -OutputPath $testOutput
    $result = Get-Content -Raw -LiteralPath $testOutput | ConvertFrom-Json

    $requiredFields = @(
        'capturedAt',
        'windowsEdition',
        'windowsVersion',
        'windowsBuild',
        'architecture',
        'cpu',
        'gpu',
        'motherboard',
        'formFactor',
        'isAdministrator',
        'hasBattery',
        'hypervisorPresent'
    )

    foreach ($field in $requiredFields) {
        if ($null -eq $result.PSObject.Properties[$field]) {
            throw "Missing required field: $field"
        }
    }

    if ([string]::IsNullOrWhiteSpace($result.windowsBuild)) {
        throw 'windowsBuild must not be empty'
    }

    if ($result.cpu.Count -lt 1) {
        throw 'At least one CPU must be reported'
    }

    if ($result.gpu.Count -lt 1) {
        throw 'At least one GPU must be reported'
    }

    Write-Output 'PASS: environment evidence contract is complete'
}
finally {
    Remove-Item -LiteralPath $testOutput -Force -ErrorAction SilentlyContinue
}
