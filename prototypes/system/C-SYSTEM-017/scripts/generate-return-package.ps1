param(
    [string]$RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..\..\..')).Path,
    [string]$OutputPath = (Join-Path (Split-Path -Parent $PSScriptRoot) 'results\PHASE-2-RETURN-PACKAGE.md'),
    [string]$CommitBase
)

$ErrorActionPreference = 'Stop'

function Get-MatchValue {
    param([string]$Content, [string]$Pattern, [string]$Default = 'NOT_TESTED')
    $match = [regex]::Match($Content, $Pattern)
    if ($match.Success) { return $match.Groups[1].Value.Trim() }
    $Default
}

function Escape-Cell {
    param([string]$Value)
    ((($Value -replace '\|', '\|') -replace '\r?\n', ' ').Trim())
}

$specRoot = Join-Path $RepositoryRoot 'feature-specs'
$evidencePath = Join-Path $RepositoryRoot 'prototypes\system\C-SYSTEM-017\results\domain-evidence.json'
$environmentPath = Join-Path $RepositoryRoot 'prototypes\system\C-SYSTEM-001\results\environment.json'
$evidence = Get-Content -Raw -LiteralPath $evidencePath | ConvertFrom-Json
$environment = Get-Content -Raw -LiteralPath $environmentPath | ConvertFrom-Json
if ([string]::IsNullOrWhiteSpace($CommitBase)) {
    $CommitBase = git -C $RepositoryRoot rev-parse HEAD
}
$specs = @()

foreach ($file in Get-ChildItem -LiteralPath $specRoot -File -Recurse -Filter '*.md' | Sort-Object FullName) {
    $content = Get-Content -Raw -LiteralPath $file.FullName
    $id = Get-MatchValue $content '(?m)^ID:\s+(C-[A-Z]+-\d+)\s*$' 'MISSING'
    $status = Get-MatchValue $content '(?m)^Status:\s+(\S+)' 'MISSING'
    $proof = [ordered]@{}
    foreach ($operation in @('DETECT', 'PLAN', 'DRY-RUN', 'APPLY', 'VERIFY', 'ROLLBACK', 'RESTORE VERIFY')) {
        $proof[$operation] = Get-MatchValue $content ("(?m)^" + [regex]::Escape($operation) + ":\s+([^\r\n]+)")
    }
    $section36 = Get-MatchValue $content '(?s)## 36\. Questões em aberto\s+(.*?)(?=\r?\n## 37\.)' 'Sem seção 36 detectada.'
    $firstQuestion = Get-MatchValue $section36 '(?m)^-\s+(.+)$' $section36
    $specs += [pscustomobject]@{
        Domain = $file.Directory.Name
        Id = $id
        Status = $status
        RelativePath = $file.FullName.Substring($RepositoryRoot.Length + 1).Replace('\', '/')
        Proof = $proof
        FirstOpenQuestion = $firstQuestion
        Content = $content
    }
}

$builder = [Text.StringBuilder]::new()
function Add-Line {
    param([string]$Line = '')
    [void]$builder.Append($Line)
    [void]$builder.Append("`n")
}

Add-Line '# Fase 2 — Pacote de retorno da campanha de prova técnica'
Add-Line
Add-Line "Gerado em: $(([datetimeoffset]$evidence.capturedAt).ToString('o'))"
Add-Line "Commit-base: ``$($CommitBase.Trim())``"
Add-Line 'Escopo: 234 Feature Specs em 20 domínios; prototypes isolados, sem backend final.'
Add-Line
Add-Line '## A–D. Artefatos e estado do Git'
Add-Line
Add-Line '- Feature Specs finais: `/feature-specs/`'
Add-Line '- Prototypes e resultados: `/prototypes/`'
Add-Line '- Diff auditável: `git diff -- feature-specs prototypes` (working tree contra o commit-base acima).'
Add-Line '- `docs/02A-PROTOTYPING-TECHNICAL-PROOF.md` já estava não rastreado antes da campanha e foi preservado como entrada operacional.'
Add-Line
Add-Line '### git status --short capturado'
Add-Line
Add-Line '```text'
Add-Line (((git -C $RepositoryRoot status --short) | ForEach-Object { $_.TrimEnd() }) -join "`n")
Add-Line '```'
Add-Line
Add-Line '### git log --oneline -5 capturado'
Add-Line
Add-Line '```text'
Add-Line (((git -C $RepositoryRoot log -5 --oneline) | ForEach-Object { $_.TrimEnd() }) -join "`n")
Add-Line '```'
Add-Line
Add-Line '## E. Inventário final de status'
Add-Line
foreach ($group in $specs | Group-Object Status | Sort-Object Name) {
    Add-Line "- $($group.Name): $($group.Count) — $((@($group.Group.Id | Sort-Object) -join ', '))"
}
foreach ($status in @('PROTOTYPING', 'PROVEN', 'APPROVED', 'BLOCKED', 'DEFERRED', 'REJECTED')) {
    if (-not ($specs.Status -contains $status)) { Add-Line "- ${status}: 0"
    }
}
Add-Line
Add-Line '## F. Matriz do ambiente realmente testado'
Add-Line
Add-Line "- Windows: $($environment.windowsEdition), versão $($environment.windowsVersion), build $($environment.windowsBuild)"
Add-Line "- Arquitetura: $($environment.architecture)"
Add-Line "- CPU: $((@($environment.cpu.name) -join '; '))"
Add-Line "- GPU/driver: $((@($environment.gpu | ForEach-Object { "$($_.name) / $($_.driverVersion)" }) -join '; '))"
Add-Line "- Motherboard: $((@($environment.motherboard | ForEach-Object { "$($_.manufacturer) $($_.product) $($_.version)" }) -join '; '))"
Add-Line "- Form factor: $($environment.formFactor)"
Add-Line "- Admin: $($environment.isAdministrator)"
Add-Line "- Bateria presente: $($environment.hasBattery)"
Add-Line "- Hypervisor presente: $($environment.hypervisorPresent)"
Add-Line '- VM: não usada'
Add-Line '- Chipset: não inferido; fonte autoritativa não foi provada nesta campanha.'
Add-Line
Add-Line '## G. Matriz de prova por Feature ID'
Add-Line
Add-Line '| Feature ID | Status | DETECT | PLAN | DRY-RUN | APPLY | VERIFY | SNAPSHOT | ROLLBACK | VERIFY_ROLLBACK | MEASURE | REBOOT_TEST |'
Add-Line '|---|---|---|---|---|---|---|---|---|---|---|---|'
foreach ($spec in $specs) {
    $snapshot = if ($spec.Proof['ROLLBACK'] -eq 'N/A') { 'N/A' } else { 'NOT_TESTED' }
    Add-Line "| $($spec.Id) | $($spec.Status) | $($spec.Proof['DETECT']) | $($spec.Proof['PLAN']) | $($spec.Proof['DRY-RUN']) | $($spec.Proof['APPLY']) | $($spec.Proof['VERIFY']) | $snapshot | $($spec.Proof['ROLLBACK']) | $($spec.Proof['RESTORE VERIFY']) | NOT_TESTED | NOT_TESTED |"
}
Add-Line
Add-Line '## H. Falhas encontradas'
Add-Line
foreach ($domain in $evidence.domains.PSObject.Properties) {
    foreach ($probe in $domain.Value.probes | Where-Object status -ne 'PASS') {
        Add-Line "- ``$($probe.id)`` — $($probe.status): $(Escape-Cell $probe.error)"
    }
}
Add-Line '- A premissa de que o check read-only do component store executaria sem elevação foi refutada neste host: DISM retornou 740.'
Add-Line '- O inventário amplo via fontes genéricas (CIM/PnP/cmdlets) mostrou-se apenas evidência parcial para várias specs; não substitui APIs mais próximas do subsistema.'
Add-Line '- Revisão cruzada encontrou contratos herdados de Apply/Rollback em features declaradas read-only; foram corrigidos em C-CPU-011, C-DRIVERS-003 e C-DRIVERS-009.'
Add-Line
Add-Line '## I. Features que mudaram de decisão'
Add-Line
Add-Line '- `C-STORAGE-019`: SPECIFIED → RESEARCH. O cabeçalho contradizia o gate final e a estratégia de fonte de dados de arquitetura/endurance permanece BLOCKED/PENDING.'
Add-Line '- Nenhuma promoção para PROVEN/APPROVED foi feita; as provas parciais não satisfazem esses gates.'
Add-Line
Add-Line '## J. Questões ainda abertas que impedem PROVEN/APPROVED'
Add-Line
Add-Line '| Feature ID | Status | Primeira questão/gate aberto preservado na spec |'
Add-Line '|---|---|---|'
foreach ($spec in $specs) {
    Add-Line "| $($spec.Id) | $($spec.Status) | $(Escape-Cell $spec.FirstOpenQuestion) |"
}
Add-Line
Add-Line '## K. Dependências de hardware/ambiente ainda não testadas'
Add-Line
Add-Line '- Intel CPU e topologias híbridas não testadas.'
Add-Line '- Intel GPU e NVIDIA GPU não testadas; apenas AMD Radeon RX 570 presente.'
Add-Line '- Laptop, bateria, Modern Standby e caminhos AC/DC não testados.'
Add-Line '- ARM64 não testado.'
Add-Line '- VM/hypervisor, RDP e virtual display não testados.'
Add-Line '- HDR, VRR, multi-monitor, DDC/CI e hotplug não testados.'
Add-Line '- Wi-Fi, WWAN/hotspot, VPN corporativa, proxy gerenciado e policy corporativa não testados.'
Add-Line '- BitLocker/device encryption, Storage Spaces/RAID e cenários de filesystem corrompido não testados.'
Add-Line '- USB4/Thunderbolt/dock, Bluetooth coexistence e falhas USB induzidas não testados.'
Add-Line '- Áudio com múltiplos endpoints, Bluetooth/USB audio, spatial sound e sleep/resume não testados.'
Add-Line '- Reboot/logoff e retomada pós-reboot não executados.'
Add-Line
Add-Line '## L. Evidência útil para a Fase 3 (fatos, não decisões)'
Add-Line
$techPatterns = [ordered]@{
    'Registry declarado' = '- \[x\] Registry'
    'PowerShell declarado' = '- \[x\] PowerShell'
    'WMI/CIM declarado' = '- \[x\] WMI / CIM'
    'Win32 declarado' = '- \[x\] Win32'
    'Vendor API declarado' = '- \[x\] Vendor API'
}
foreach ($item in $techPatterns.GetEnumerator()) {
    $count = @($specs | Where-Object Content -match $item.Value).Count
    Add-Line "- $($item.Key): $count specs."
}
$sharedCount = @($specs | Where-Object Content -match '(?m)^Shared Capability:\s+Yes').Count
$snapshotCount = @($specs | Where-Object Content -match '(?s)## 14\. Snapshot.*?### É necessário\?\s+Yes').Count
$rebootPersistenceCount = @($specs | Where-Object Content -match '(?s)### O snapshot precisa sobreviver reboot\?\s+Yes').Count
Add-Line "- Shared Capability declarada: $sharedCount specs."
Add-Line "- Snapshot explicitamente necessário: $snapshotCount specs."
Add-Line "- Snapshot explicitamente persistente após reboot: $rebootPersistenceCount specs."
Add-Line '- Observado nos prototypes: PowerShell/CIM/cmdlets nativos foram suficientes para o bundle inicial; nenhum P/Invoke ou vendor API foi necessário para esses dois prototypes.'
Add-Line '- Observado: 20 domínios puderam emitir pelo menos um probe read-only estruturado; isso não demonstra cobertura completa de qualquer domínio.'
Add-Line '- Observado: 1 de 27 probes exigiu elevação (DISM /CheckHealth); os outros 26 executaram na sessão não elevada.'
Add-Line '- Observado: nenhuma prova desta campanha exigiu reboot, snapshot mutável, rollback multi-etapas ou processo contínuo.'
Add-Line
Add-Line '## Revisão cruzada global'
Add-Line
Add-Line '- Nenhum status PROVEN/APPROVED existe sem evidência.'
Add-Line '- O bundle comum registra provenance por probe, mas não foi usado como substituto de fonte de verdade específica.'
Add-Line '- Permanecem abertos: ownership de inventários compartilhados fora do domínio System; fonte autoritativa por campo; semântica uniforme de Admin/Restart/System Restore; sobreposições Gaming↔Display/Audio/Network, GPU↔Processes/RAM e Network↔Security/Energy.'
Add-Line '- As mutações destrutivas/alto risco não foram improvisadas no Windows real; seus resultados permanecem NOT_TESTED/RESEARCH conforme a spec.'
Add-Line '- Nenhum código do prototype foi promovido para backend final, solution, UI, DI, IPC ou contratos finais.'

$outputDirectory = Split-Path -Parent $OutputPath
New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
[IO.File]::WriteAllText($OutputPath, $builder.ToString(), [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
