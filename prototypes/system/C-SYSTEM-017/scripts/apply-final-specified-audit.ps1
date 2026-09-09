param(
    [Parameter(Mandatory)] [string]$FeatureSpecsRoot,
    [Parameter(Mandatory)] [string[]]$AuditReports,
    [string]$OverridesPath,
    [int]$ExpectedCount = 0
)

$ErrorActionPreference = 'Stop'
$decisions = @{}

foreach ($reportPath in $AuditReports) {
    foreach ($line in Get-Content -LiteralPath $reportPath) {
        if ($line -notmatch '^\|\s*(C-[A-Z]+-\d+)\s*\|') { continue }
        $cells = @($line.Trim().Trim('|').Split('|') | ForEach-Object { $_.Trim() })
        if ($cells.Count -eq 7) {
            $decision = [ordered]@{ id=$cells[0]; essential=$cells[1]; evidence=$cells[2]; safe="$($cells[3]) Teste recomendado: $($cells[4])"; limitation=$cells[5]; status=$cells[6] }
        } elseif ($cells.Count -eq 6) {
            $decision = [ordered]@{ id=$cells[0]; essential=$cells[1]; evidence=$cells[2]; safe=$cells[3]; limitation=$cells[4]; status=$cells[5] }
        } else {
            throw "Unexpected audit table shape ($($cells.Count) cells): $line"
        }
        if ($decision.status -notin @('SPECIFIED','RESEARCH','BLOCKED','DEFERRED','PROVEN')) { throw "Invalid recommended status for $($decision.id): $($decision.status)" }
        $decisions[$decision.id] = $decision
    }
}

if ($OverridesPath) {
    foreach ($override in @(Get-Content -Raw -LiteralPath $OverridesPath | ConvertFrom-Json)) {
        if (-not $decisions.ContainsKey($override.id)) { throw "Override refers to non-audited ID: $($override.id)" }
        $decisions[$override.id] = [ordered]@{ id=$override.id; essential=$override.essential; evidence=$override.evidence; safe=$override.safe; limitation=$override.limitation; status=$override.status }
    }
}
if ($ExpectedCount -gt 0 -and $decisions.Count -ne $ExpectedCount) { throw "Expected $ExpectedCount audit decisions, found $($decisions.Count)" }

$specById = @{}
foreach ($file in Get-ChildItem -LiteralPath $FeatureSpecsRoot -Recurse -File -Filter '*.md') {
    $content = Get-Content -Raw -LiteralPath $file.FullName
    $match = [regex]::Match($content, '(?m)^ID:\s*(C-[A-Z]+-\d+)\s*$')
    if ($match.Success) { $specById[$match.Groups[1].Value] = $file.FullName }
}

$changed = 0
foreach ($decision in $decisions.Values) {
    if (-not $specById.ContainsKey($decision.id)) { throw "Feature Spec not found: $($decision.id)" }
    $path = $specById[$decision.id]
    $content = Get-Content -Raw -LiteralPath $path
    $content = $content.Replace("`r`n", "`n")
    $newline = "`n"
    $content = [regex]::Replace($content, '(?m)^Status:\s*\S+\s*$', "Status: $($decision.status)")
    $content = [regex]::Replace($content, '(?m)^\*\*Final SPECIFIED audit disposition \(2026-09-08\):\*\*.*\r?\n\r?\n?', '')
    $content = [regex]::Replace($content, '(?s)<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->.*?<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->\r?\n\r?\n?', '')
    $evidence = [string]$decision.evidence
    $limitation = [string]$decision.limitation
    $safeProof = [regex]::Match($content, '(?ms)<!-- PHASE2-SAFE-PROOF[^:]*:START -->(.*?)<!-- PHASE2-SAFE-PROOF[^:]*:END -->')
    if ($safeProof.Success) {
        $safeBody = $safeProof.Groups[1].Value
        $summary = [regex]::Match($safeBody, '(?m)^- Capability/premissa exercitada:\s*(.+)$').Groups[1].Value.Trim()
        if (-not $summary) {
            $summary = [regex]::Match($safeBody, '(?m)^\*\*[^*]+:\*\*\s*(.+)$').Groups[1].Value.Trim()
        }
        if (-not $summary) { $summary = 'prova segura descrita na seção 31' }
        $result = [regex]::Match($safeBody, '(?m)^- Resultado observado deste probe:\s*`?([^`\.\r\n]+)').Groups[1].Value.Trim()
        $pathMatch = [regex]::Match($safeBody, '(?i)/prototypes/[^`\s\)]+')
        $evidencePath = if ($pathMatch.Success) { $pathMatch.Value.TrimEnd('.', ',') } else { '/prototypes/ (consulte a seção 31)' }
        $resultText = if ($result) { " Resultado observado: ``$result``." } else { '' }
        $evidence = "Prototype seguro adicional executado: $summary$resultText Evidência: ``$evidencePath``. A evidência anterior foi substituída por esta observação mais recente."
        if ($limitation -match '(?i)\bfeature(?:-specific)? prototype\b|sem prototype') {
            $limitation = 'A prova adicional cobre somente a premissa e o ambiente registrados na seção 31; os cenários restantes do gate e a matriz de compatibilidade permanecem não comprovados.'
        }
    }
    $block = @(
        '<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->',
        '**Auditoria final da necessidade de prova (2026-09-08):**',
        "- Prova prática adicional essencial: $($decision.essential)",
        "- Evidência realmente executada: $evidence",
        "- Execução segura neste host / teste: $($decision.safe)",
        "- Impedimento ou limitação restante: $limitation",
        "- Disposição: ``$($decision.status)``. Nenhum ``PASS`` foi inferido; o status reflete somente a evidência e os bloqueios registrados.",
        '<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->',
        ''
    ) -join $newline
    $section32 = [regex]::Match($content, '(?m)^## 32\.')
    if (-not $section32.Success) { throw "Section 32 not found in $($decision.id)" }
    $content = $content.Insert($section32.Index, $block + $newline)
    [IO.File]::WriteAllText($path, $content, [Text.UTF8Encoding]::new($false))
    $changed++
}

$researchChanged = 0
foreach ($path in $specById.Values) {
    $content = (Get-Content -Raw -LiteralPath $path).Replace("`r`n", "`n")
    if ($content -notmatch '(?m)^Status:\s*RESEARCH\s*$') { continue }
    if ($content.Contains('PHASE2-FINAL-RESEARCH-AUDIT:START')) { continue }
    $researchBlock = @(
        '<!-- PHASE2-FINAL-RESEARCH-AUDIT:START -->',
        '**Reavaliação final de RESEARCH (2026-09-08):**',
        '- O status `RESEARCH` foi revisto e preservado porque ainda há incerteza técnica solucionável descrita nas seções 30, 31 e 36; documentação ou probe compartilhado parcial não foi convertido em `PASS`.',
        '- Nenhuma prova prática isolada nesta máquina elimina essa incerteza sem antes definir fonte, contrato, fixture, hardware ou dependência indicada pela própria spec.',
        '- Próximo gate: concluir a investigação registrada, então decidir se cabe prototype seguro, `SPECIFIED`, `BLOCKED`, `DEFERRED` ou `REJECTED`; não promover diretamente a `PROVEN`/`APPROVED`.',
        '<!-- PHASE2-FINAL-RESEARCH-AUDIT:END -->',
        ''
    ) -join "`n"
    $section32 = [regex]::Match($content, '(?m)^## 32\.')
    if (-not $section32.Success) { throw "Section 32 not found in RESEARCH spec: $path" }
    $content = $content.Insert($section32.Index, $researchBlock + "`n")
    [IO.File]::WriteAllText($path, $content, [Text.UTF8Encoding]::new($false))
    $researchChanged++
}

Write-Output "Applied $changed final SPECIFIED audit decisions; added $researchChanged RESEARCH reevaluation notes."
