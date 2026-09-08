$ErrorActionPreference = 'Stop'

$prototypeRoot = Split-Path -Parent $PSScriptRoot
$updater = Join-Path $prototypeRoot 'scripts\apply-campaign-evidence.ps1'
$testRoot = Join-Path $env:TEMP ('beadwise-evidence-test-' + [guid]::NewGuid().ToString('N'))
$specRoot = Join-Path $testRoot 'feature-specs\test'
$evidencePath = Join-Path $testRoot 'evidence.json'

try {
    New-Item -ItemType Directory -Path $specRoot -Force | Out-Null
    @'
# Fixture
ID: C-TEST-001
Status: SPECIFIED

DETECT: NOT_RUN
VERIFY: NOT_RUN

**Observed behavior:** N/A nesta revisão documental; nenhuma execução real foi alegada.
'@ | Set-Content -LiteralPath (Join-Path $specRoot 'C-TEST-001-fixture.md') -Encoding utf8NoBOM

    @'
{
  "capturedAt": "2026-09-08T20:00:00-03:00",
  "domains": {
    "test": {
      "probes": [
        {
          "id": "test.read-only",
          "featureIds": ["C-TEST-001"],
          "status": "PASS"
        }
      ]
    }
  }
}
'@ | Set-Content -LiteralPath $evidencePath -Encoding utf8NoBOM

    & $updater -FeatureSpecsRoot (Join-Path $testRoot 'feature-specs') -EvidencePath $evidencePath
    $updated = Get-Content -Raw -LiteralPath (Join-Path $specRoot 'C-TEST-001-fixture.md')

    if ($updated -notmatch 'DETECT: NOT_TESTED') { throw 'NOT_RUN was not normalized to NOT_TESTED' }
    if ($updated -notmatch 'test\.read-only.*PASS') { throw 'Probe evidence was not recorded' }
    if ($updated -notmatch 'Status: SPECIFIED') { throw 'Updater changed feature status' }
    if ($updated -match 'DETECT: PASS') { throw 'Partial shared probe was incorrectly promoted to DETECT PASS' }

    Write-Output 'PASS: campaign evidence updater preserves status and avoids false PASS'
}
finally {
    Remove-Item -LiteralPath $testRoot -Recurse -Force -ErrorAction SilentlyContinue
}
