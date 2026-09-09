$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$collector = Join-Path $prototypeRoot 'scripts\collect-domain-evidence.ps1'
$testOutput = Join-Path $env:TEMP 'beadwise-c-system-017-test.json'
$expectedDomains = @(
    'audio', 'cleaning', 'cpu', 'display', 'drivers', 'energy', 'gaming',
    'gpu', 'hardware', 'network', 'privacy', 'processes', 'ram', 'repair',
    'security', 'startup', 'storage', 'system', 'update', 'usb'
)

try {
    & $collector -OutputPath $testOutput -Fast
    $rawResult = Get-Content -Raw -LiteralPath $testOutput
    if ($rawResult -match 'S-1-5-21-(?:\d+-){3}\d+') {
        throw 'Domain evidence persisted a raw user SID'
    }
    if ($rawResult -match '(?i)[A-Z]:\\\\Users\\\\(?!\[REDACTED\])[^\\"]+') {
        throw 'Domain evidence persisted a raw user profile path'
    }
    $result = $rawResult | ConvertFrom-Json

    if ($result.schemaVersion -ne 1) {
        throw "Unexpected schemaVersion: $($result.schemaVersion)"
    }

    foreach ($domain in $expectedDomains) {
        $domainEvidence = $result.domains.PSObject.Properties[$domain]
        if ($null -eq $domainEvidence) {
            throw "Missing domain evidence: $domain"
        }
        if ($domainEvidence.Value.probes.Count -lt 1) {
            throw "Domain has no probes: $domain"
        }
        foreach ($probe in $domainEvidence.Value.probes) {
            if ($probe.status -notin @('PASS', 'FAIL', 'UNSUPPORTED', 'NOT_TESTED')) {
                throw "Invalid probe status '$($probe.status)' in domain $domain"
            }
            if ([string]::IsNullOrWhiteSpace($probe.id)) {
                throw "Probe without id in domain $domain"
            }
        }
    }

    Write-Output 'PASS: domain evidence bundle contract is complete'
}
finally {
    Remove-Item -LiteralPath $testOutput -Force -ErrorAction SilentlyContinue
}
