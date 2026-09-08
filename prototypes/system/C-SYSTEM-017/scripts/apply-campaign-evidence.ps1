param(
    [Parameter(Mandatory)]
    [string]$FeatureSpecsRoot,

    [Parameter(Mandatory)]
    [string]$EvidencePath
)

$ErrorActionPreference = 'Stop'

$evidence = Get-Content -Raw -LiteralPath $EvidencePath | ConvertFrom-Json
$probeMap = @{}

foreach ($domain in $evidence.domains.PSObject.Properties) {
    foreach ($probe in $domain.Value.probes) {
        foreach ($featureId in $probe.featureIds) {
            if (-not $probeMap.ContainsKey($featureId)) {
                $probeMap[$featureId] = [Collections.Generic.List[object]]::new()
            }
            $probeMap[$featureId].Add($probe)
        }
    }
}

$campaignDate = ([datetimeoffset]$evidence.capturedAt).ToString('yyyy-MM-dd')
$filesChanged = 0

foreach ($file in Get-ChildItem -LiteralPath $FeatureSpecsRoot -File -Recurse -Filter '*.md') {
    $original = Get-Content -Raw -LiteralPath $file.FullName
    $content = $original -replace '\bNOT_RUN\b', 'NOT_TESTED'
    $idMatch = [regex]::Match($content, '(?m)^ID:\s+(C-[A-Z]+-\d+)\s*$')
    $statusMatch = [regex]::Match($content, '(?m)^Status:\s+(\S+)')

    if (-not $idMatch.Success) {
        continue
    }

    $featureId = $idMatch.Groups[1].Value
    $status = if ($statusMatch.Success) { $statusMatch.Groups[1].Value } else { 'UNKNOWN' }

    if ($probeMap.ContainsKey($featureId)) {
        $probeDescriptions = @($probeMap[$featureId] | ForEach-Object { "``$($_.id)``=$($_.status)" }) -join ', '
        $campaignNote = "**Observed behavior (campanha $campaignDate):** probes compartilhados read-only executados neste host: $probeDescriptions. Evidência: ``/prototypes/system/C-SYSTEM-017/results/domain-evidence.json``. Estes sinais são parciais e não satisfazem, sozinhos, o gate `DETECT` completo desta feature."
    }
    else {
        $campaignNote = "**Observed behavior (campanha $campaignDate):** nenhuma prova específica desta capability foi executada neste host; resultado ``NOT_TESTED``. A necessidade de prototype foi reavaliada e o status ``$status`` foi preservado porque os gates aplicáveis continuam abertos."
    }

    $commonObserved = '(?m)^\*\*Observed behavior:\*\* N/A nesta revisão documental; nenhuma execução real foi alegada\.\r?$'
    $storageObserved = '(?m)^- \*\*Observed behavior:\*\* TBD até execução dos protótipos/testes\.\r?$'

    if ([regex]::IsMatch($content, $commonObserved)) {
        $content = [regex]::Replace($content, $commonObserved, $campaignNote)
    }
    elseif ([regex]::IsMatch($content, $storageObserved)) {
        $content = [regex]::Replace($content, $storageObserved, '- ' + $campaignNote)
    }
    elseif ($content -notmatch [regex]::Escape("Observed behavior (campanha $campaignDate)")) {
        $section32 = [regex]::Match($content, '(?m)^## 32\.')
        if ($section32.Success) {
            $newline = if ($content.Contains("`r`n")) { "`r`n" } else { "`n" }
            $content = $content.Insert($section32.Index, "$campaignNote$newline$newline")
        }
    }

    if ($content -ne $original) {
        [IO.File]::WriteAllText($file.FullName, $content, [Text.UTF8Encoding]::new($false))
        $filesChanged++
    }
}

Write-Output "Updated $filesChanged Feature Specs."
