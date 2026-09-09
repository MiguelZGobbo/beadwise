$ErrorActionPreference = 'Stop'
$prototypeRoot = Split-Path -Parent $PSScriptRoot
$exporter = Join-Path $prototypeRoot 'scripts\export-sanitized-evidence.ps1'
$testRoot = Join-Path $env:TEMP ('beadwise-export-test-' + [guid]::NewGuid().ToString('N'))
$inputPath = Join-Path $testRoot 'input.json'
$outputPath = Join-Path $testRoot 'output.json'
$manifestPath = Join-Path $testRoot 'manifest.json'
try {
    New-Item -ItemType Directory -Path $testRoot -Force | Out-Null
    $fixture = '{"schemaVersion":1,"capturedAt":"2026-09-08T00:00:00Z","domains":{"test":{"probes":[{"id":"test.one","featureIds":["C-TEST-001"],"status":"PASS","data":{"email":"secret@example.com","commandLine":"token=SECRET","macAddress":"00-11-22-33-44-55","registryPath":"HKU\\S-1-5-21-111111111-222222222-333333333-1001\\Software\\Vendor","safe":"kept"},"error":null}]}}}'
    [IO.File]::WriteAllText($inputPath, $fixture, [Text.UTF8Encoding]::new($false))
    & $exporter -InputPath $inputPath -OutputPath $outputPath -ManifestPath $manifestPath
    $raw = Get-Content -Raw -LiteralPath $outputPath
    $export = $raw | ConvertFrom-Json
    $manifest = Get-Content -Raw -LiteralPath $manifestPath | ConvertFrom-Json
    if ($raw -match 'secret@example|token=SECRET|00-11-22|S-1-5-21-111111111') { throw 'Sensitive fixture value leaked' }
    if ($export.domains.test.probes[0].data.safe -ne 'kept') { throw 'Non-sensitive field was removed' }
    if ([string]::IsNullOrWhiteSpace($export.domains.test.probes[0].correlationId)) { throw 'Correlation ID missing' }
    $actualHash = [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData([IO.File]::ReadAllBytes($outputPath))).ToLowerInvariant()
    if ($manifest.sha256 -ne $actualHash) { throw 'Manifest hash does not match export bytes' }
    if ($manifest.reloadVerified -ne $true) { throw 'Export reload was not verified' }
    Write-Output 'PASS: export redaction, correlation ID, integrity and reload verified'
}
finally {
    Remove-Item -LiteralPath $testRoot -Recurse -Force -ErrorAction SilentlyContinue
}
