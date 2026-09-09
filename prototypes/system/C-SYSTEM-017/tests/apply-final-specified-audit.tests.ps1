$ErrorActionPreference = 'Stop'
$prototypeRoot = Split-Path -Parent $PSScriptRoot
$updater = Join-Path $prototypeRoot 'scripts\apply-final-specified-audit.ps1'
$testRoot = Join-Path $env:TEMP ('beadwise-final-audit-' + [guid]::NewGuid().ToString('N'))
$specRoot = Join-Path $testRoot 'feature-specs\test'
$report = Join-Path $testRoot 'audit.md'
try {
    New-Item -ItemType Directory -Path $specRoot -Force | Out-Null
    [IO.File]::WriteAllText((Join-Path $specRoot 'fixture.md'), "# Fixture`nID: C-TEST-001`nStatus: SPECIFIED  `n`n<!-- PHASE2-SAFE-PROOF-20260908:START -->`n**Observed safe proof:** bounded fixture.`n- Resultado observado deste probe: ``PASS``.`n- Evidência reproduzível: ``/prototypes/test/C-TEST-001/results/proof.json``.`n<!-- PHASE2-SAFE-PROOF-20260908:END -->`n`n## 32. Benefício real`nReasonable`n", [Text.UTF8Encoding]::new($false))
    [IO.File]::WriteAllText((Join-Path $specRoot 'research.md'), "# Research fixture`nID: C-TEST-002`nStatus: RESEARCH`n`n## 32. Benefício real`nUnknown`n", [Text.UTF8Encoding]::new($false))
    [IO.File]::WriteAllText($report, "| ID | Essential | Evidence | Safe/test | Limitation | Status |`n|---|---|---|---|---|---|`n| C-TEST-001 | Sim — prova direta. | NENHUMA. | Sim — fixture segura. | No feature prototype exists. | BLOCKED |`n", [Text.UTF8Encoding]::new($false))

    & $updater -FeatureSpecsRoot $specRoot -AuditReports @($report)
    $updated = Get-Content -Raw -LiteralPath (Join-Path $specRoot 'fixture.md')
    if ($updated -notmatch '(?m)^Status: BLOCKED') { throw 'Recommended status was not applied' }
    if ($updated -notmatch 'Prova prática adicional essencial.*prova direta') { throw 'Essential proof decision missing' }
    if ($updated -notmatch 'Execução segura.*fixture segura') { throw 'Safe execution decision missing' }
    if ($updated -notmatch 'Prototype seguro adicional executado.*bounded fixture.*proof\.json') { throw 'Observed safe proof was not folded into the final audit evidence' }
    if ($updated -match 'No feature prototype exists') { throw 'Obsolete no-prototype limitation survived observed proof' }
    if ([regex]::Matches($updated, 'PHASE2-FINAL-SPECIFIED-AUDIT:START').Count -ne 1) { throw 'Audit block is not idempotent' }
    $researchUpdated = Get-Content -Raw -LiteralPath (Join-Path $specRoot 'research.md')
    if ($researchUpdated -notmatch 'PHASE2-FINAL-RESEARCH-AUDIT:START') { throw 'RESEARCH spec did not receive a local final reevaluation note' }

    & $updater -FeatureSpecsRoot $specRoot -AuditReports @($report)
    $updatedAgain = Get-Content -Raw -LiteralPath (Join-Path $specRoot 'fixture.md')
    if ([regex]::Matches($updatedAgain, 'PHASE2-FINAL-SPECIFIED-AUDIT:START').Count -ne 1) { throw 'Second run duplicated audit block' }
    $researchAgain = Get-Content -Raw -LiteralPath (Join-Path $specRoot 'research.md')
    if ([regex]::Matches($researchAgain, 'PHASE2-FINAL-RESEARCH-AUDIT:START').Count -ne 1) { throw 'Second run duplicated RESEARCH audit block' }
    Write-Output 'PASS: final SPECIFIED audit updater is complete and idempotent'
}
finally {
    Remove-Item -LiteralPath $testRoot -Recurse -Force -ErrorAction SilentlyContinue
}
