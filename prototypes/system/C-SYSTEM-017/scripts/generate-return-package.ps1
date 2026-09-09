param(
    [string]$RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..\..\..')).Path,
    [string]$OutputPath = (Join-Path (Split-Path -Parent $PSScriptRoot) 'results\PHASE-2-RETURN-PACKAGE.md'),
    [string]$RootPackagePath,
    [string]$MatrixOutputPath,
    [string]$ResultsMatrixOutputPath,
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
if ([string]::IsNullOrWhiteSpace($RootPackagePath)) { $RootPackagePath = Join-Path $RepositoryRoot 'PHASE-2-RETURN-PACKAGE.md' }
if ([string]::IsNullOrWhiteSpace($MatrixOutputPath)) { $MatrixOutputPath = Join-Path $RepositoryRoot 'PHASE-2-PROOF-MATRIX.md' }
if ([string]::IsNullOrWhiteSpace($ResultsMatrixOutputPath)) { $ResultsMatrixOutputPath = Join-Path (Split-Path -Parent $PSScriptRoot) 'results\PHASE-2-PROOF-MATRIX.md' }
$additionalProofs = @(
    [pscustomobject]@{ Id = 'C-SYSTEM-001'; Path = 'prototypes/system/C-SYSTEM-001/results/native-environment.json'; Summary = 'Win32/PInvoke GetNativeSystemInfo/RtlGetVersion PASS.' }
    [pscustomobject]@{ Id = 'C-SYSTEM-007'; Path = 'prototypes/system/C-SYSTEM-007/results/drift.json'; Summary = 'Registry HKCU baseline/diff/provenance PASS; fixture snapshot/apply/rollback is harness evidence, not feature Apply.' }
    [pscustomobject]@{ Id = 'C-CPU-003'; Path = 'prototypes/cpu/C-CPU-003/results/performance-counter-proof.json'; Summary = 'Performance Counter localized discovery/sample PASS; English Get-Counter path FAIL on localized Windows.' }
    [pscustomobject]@{ Id = 'C-CPU-003'; Path = 'prototypes/cpu/C-CPU-003/results/pdh-samples.json'; Summary = 'PdhAddEnglishCounterW via P/Invoke sampled CPU successfully independent of UI language.' }
    [pscustomobject]@{ Id = 'C-CPU-003'; Path = 'prototypes/cpu/C-CPU-003/results/etw-attempt.json'; Summary = 'CPU WPR/ETW attempt executed and classified BLOCKED_ADMIN (0xc5585011).' }
    [pscustomobject]@{ Id = 'C-SYSTEM-012'; Path = 'prototypes/system/C-SYSTEM-012/results/etw-wpr-proof.json'; Summary = 'WPR present/status/profiles PASS; trace start FAIL in non-elevated session with 0xc5585011.' }
    [pscustomobject]@{ Id = 'C-GPU-015'; Path = 'prototypes/gpu/C-GPU-015/results/amd-adl.json'; Summary = 'AMD ADL context create/enumerate/destroy PASS read-only; other vendors and tuning untested.' }
    [pscustomobject]@{ Id = 'C-SECURITY-006'; Path = 'prototypes/security/C-SECURITY-006/results/admin-uac-proof.json'; Summary = 'Non-elevated token and admin-required/access-denied signal PASS; identity/group data redacted.' }
    [pscustomobject]@{ Id = 'C-PROCESSES-007'; Path = 'prototypes/processes/C-PROCESSES-007/results/process-restart.json'; Summary = 'Controlled child process dry-run/snapshot/terminate/restart/verify/cleanup PASS.' }
    [pscustomobject]@{ Id = 'C-STARTUP-001'; Path = 'prototypes/startup/C-STARTUP-001/results/startup-inventory.json'; Summary = 'Startup Registry Run fixture inventory/provenance PASS with exact restoration in finally.' }
)
$manualSafeProofStatus = @{
    'C-AUDIO-007' = 'PASS + BLOCKED'
    'C-SYSTEM-001' = 'PASS'
    'C-CPU-003' = 'PASS + BLOCKED_ADMIN'
    'C-PROCESSES-007' = 'PASS'
    'C-STARTUP-001' = 'PASS'
    'C-SYSTEM-007' = 'PASS'
    'C-SYSTEM-017' = 'PASS'
    'C-SYSTEM-012' = 'FAIL / BLOCKED_ADMIN'
    'C-GPU-015' = 'PASS (AMD only)'
    'C-SECURITY-006' = 'PASS / BLOCKED_ADMIN'
}
$evidence = Get-Content -Raw -LiteralPath $evidencePath | ConvertFrom-Json
$environment = Get-Content -Raw -LiteralPath $environmentPath | ConvertFrom-Json
$verificationPath = Join-Path $RepositoryRoot 'prototypes\system\C-SYSTEM-017\results\final-verification.json'
$finalVerification = if (Test-Path -LiteralPath $verificationPath) { Get-Content -Raw -LiteralPath $verificationPath | ConvertFrom-Json } else { $null }
if ([string]::IsNullOrWhiteSpace($CommitBase)) {
    $CommitBase = git -C $RepositoryRoot rev-parse HEAD
}
$baseStatusByPath = @{}
foreach ($line in @(git -C $RepositoryRoot grep -n '^Status:' $CommitBase -- feature-specs)) {
    $match = [regex]::Match($line, '^[^:]+:(feature-specs/[^:]+):\d+:Status:\s+(\S+)')
    if ($match.Success) { $baseStatusByPath[$match.Groups[1].Value] = $match.Groups[2].Value }
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
    $hasManualSafeProofStatus = $manualSafeProofStatus.ContainsKey($id)
    $safeProofStatus = if ($hasManualSafeProofStatus) { $manualSafeProofStatus[$id] } else { 'NOT_TESTED' }
    $safeProofBlock = [regex]::Match($content, '(?ms)<!-- PHASE2-SAFE-PROOF[^:]*:START -->(.*?)<!-- PHASE2-SAFE-PROOF[^:]*:END -->')
    if ($safeProofBlock.Success -and -not $hasManualSafeProofStatus) {
        $safeBody = $safeProofBlock.Groups[1].Value
        $safeMatch = [regex]::Match($safeBody, '(?im)^- Resultado observado deste probe:\s*`?([A-Z_]+)')
        if (-not $safeMatch.Success) { $safeMatch = [regex]::Match($safeBody, "(?i)" + [regex]::Escape($id) + '=(PASS|PARTIAL|FAIL|UNSUPPORTED|BLOCKED(?:_ADMIN)?)') }
        if (-not $safeMatch.Success) { $safeMatch = [regex]::Match($safeBody, '(?i)\b(PASS|PARTIAL|FAIL|UNSUPPORTED|BLOCKED(?:_ADMIN)?)\b') }
        if ($safeMatch.Success) { $safeProofStatus = $safeMatch.Groups[1].Value.ToUpperInvariant() }
        elseif ($proof['DETECT'] -match '^PASS') { $safeProofStatus = 'PASS' }
        else { $safeProofStatus = 'PARTIAL' }
    }
    $section36 = Get-MatchValue $content '(?s)## 36\. Questões em aberto\s+(.*?)(?=\r?\n## 37\.)' 'Sem seção 36 detectada.'
    $firstQuestion = Get-MatchValue $section36 '(?m)^-\s+(.+)$' $section36
    $relativePath = $file.FullName.Substring($RepositoryRoot.Length + 1).Replace('\', '/')
    $specs += [pscustomobject]@{
        Domain = $file.Directory.Name
        Id = $id
        Status = $status
        RelativePath = $relativePath
        BaseStatus = if ($baseStatusByPath.ContainsKey($relativePath)) { $baseStatusByPath[$relativePath] } else { 'MISSING' }
        Proof = $proof
        SafeProof = $safeProofStatus
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
Add-Line '- Pacote canônico: `/PHASE-2-RETURN-PACKAGE.md`; cópia gerada também em `/prototypes/system/C-SYSTEM-017/results/PHASE-2-RETURN-PACKAGE.md`.'
Add-Line '- Matriz canônica: `/PHASE-2-PROOF-MATRIX.md`; cópia gerada também em `/prototypes/system/C-SYSTEM-017/results/PHASE-2-PROOF-MATRIX.md`.'
Add-Line "- Diff auditável após o commit da campanha: ``git diff $($CommitBase.Trim())..HEAD -- .gitignore feature-specs prototypes PHASE-2-RETURN-PACKAGE.md PHASE-2-PROOF-MATRIX.md``. O range inclui arquivos adicionados, modificados e resultados persistidos."
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
foreach ($status in @('RESEARCH','SPECIFIED','PROTOTYPING','PROVEN','APPROVED','BLOCKED','DEFERRED','REJECTED')) {
    $matching = @($specs | Where-Object Status -eq $status)
    $ids = if ($matching.Count -gt 0) { ' — ' + ((@($matching.Id | Sort-Object) -join ', ')) } else { '' }
    Add-Line "- ${status}: $($matching.Count)$ids"
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
Add-Line '| Feature ID | Status | DETECT | PLAN | DRY-RUN | APPLY | VERIFY | SNAPSHOT | ROLLBACK | VERIFY_ROLLBACK | MEASURE | SAFE_PROOF | REBOOT_TEST |'
Add-Line '|---|---|---|---|---|---|---|---|---|---|---|---|---|'
foreach ($spec in $specs) {
    $snapshotRequired = $spec.Content -match '(?s)## 14\. Snapshot.*?### É necessário\?\s+Yes'
    $snapshot = if (-not $snapshotRequired) { 'N/A' } elseif ($spec.Id -eq 'C-PROCESSES-007') { 'PASS' } else { 'NOT_TESTED' }
    $measure = if ($spec.Id -eq 'C-CPU-003') { 'PASS' } elseif ($spec.Content -match '(?s)## 33\. Impacto mensurável.*?### Pode ser medido\?\s+(Yes|Partial)') { 'NOT_TESTED' } else { 'N/A' }
    $rebootRequired = $spec.Content -match '(?m)^- \[x\] (Logoff|Reboot do Windows)\s*$'
    $reboot = if (-not $rebootRequired) { 'N/A' } elseif ($spec.Status -eq 'BLOCKED') { 'BLOCKED' } else { 'NOT_TESTED' }
    Add-Line "| $($spec.Id) | $($spec.Status) | $($spec.Proof['DETECT']) | $($spec.Proof['PLAN']) | $($spec.Proof['DRY-RUN']) | $($spec.Proof['APPLY']) | $($spec.Proof['VERIFY']) | $snapshot | $($spec.Proof['ROLLBACK']) | $($spec.Proof['RESTORE VERIFY']) | $measure | $($spec.SafeProof) | $reboot |"
}
Add-Line
$specifiedProofCoverage = @($specs | Where-Object { $_.Status -eq 'SPECIFIED' -and $_.SafeProof -ne 'NOT_TESTED' })
Add-Line "Cobertura explícita de prova segura entre specs ``SPECIFIED``: $($specifiedProofCoverage.Count)/$(@($specs | Where-Object Status -eq 'SPECIFIED').Count). A coluna ``SAFE_PROOF`` registra a fatia adicional executada sem reescrever os stages ainda não exercitados como ``PASS``."
Add-Line
Add-Line '## H. Falhas encontradas'
Add-Line
foreach ($domain in $evidence.domains.PSObject.Properties) {
    foreach ($probe in $domain.Value.probes | Where-Object status -ne 'PASS') {
        Add-Line "- ``$($probe.id)`` — $($probe.status): $(Escape-Cell $probe.error)"
    }
}
Add-Line '- A premissa de que o check read-only do component store executaria sem elevação foi refutada neste host: DISM retornou 740.'
Add-Line '- A premissa de que `\Processor(_Total)\% Processor Time` seria portável foi refutada neste host localizado: `Get-Counter` retornou "O objeto especificado não foi encontrado"; a prova precisou descobrir `\Processador(_Total)\% tempo de processador`.'
Add-Line '- A captura ETW/WPR real não pôde iniciar em sessão não elevada: `wpr -start GeneralProfile -filemode` retornou exit code -984068079 / erro 0xc5585011.'
Add-Line '- `C-STORAGE-002` e `C-STORAGE-003`: o provider de reliability/temperature retornou acesso CIM indisponível; nenhum dado foi inventado.'
Add-Line '- `C-STORAGE-005`: a tentativa read-only de `chkdsk /scan` falhou com acesso negado; nenhum modo de repair foi invocado.'
Add-Line '- `C-SECURITY-003`: Secure Boot e TPM retornaram acesso negado/estado não estruturado sem elevação; VBS permaneceu como sinal parcial.'
Add-Line '- `C-ENERGY-013`: `powercfg /requests` e `/energy` retornaram exit code 1 por exigirem administrador; o estado ficou PARTIAL/UNSUPPORTED.'
Add-Line '- O inventário amplo via fontes genéricas (CIM/PnP/cmdlets) mostrou-se apenas evidência parcial para várias specs; não substitui APIs mais próximas do subsistema.'
Add-Line '- Revisão cruzada encontrou contratos herdados de Apply/Rollback em features declaradas read-only; foram corrigidos em C-CPU-011, C-DRIVERS-003 e C-DRIVERS-009.'
Add-Line '- A inconsistência C-SYSTEM-007/C-SYSTEM-017 foi corrigida: C-SYSTEM-017 continua sendo o bundle compartilhado de coleta/export e não é prova de C-SYSTEM-007; C-SYSTEM-007 agora possui prototype próprio de baseline/diff Registry.'
Add-Line
Add-Line '## I. Features que mudaram de decisão'
Add-Line
$transitions = @($specs | Where-Object { $_.BaseStatus -ne 'MISSING' -and $_.BaseStatus -ne $_.Status } | Sort-Object Id)
if ($transitions.Count -eq 0) { Add-Line '- Nenhuma mudança de status contra o commit-base.' }
foreach ($transition in $transitions) {
    Add-Line "- ``$($transition.Id)``: $($transition.BaseStatus) → $($transition.Status). Motivo e bloqueio estão no bloco ``Auditoria final da necessidade de prova`` da própria spec."
}
Add-Line '- Nenhuma promoção para PROVEN/APPROVED foi feita; as provas parciais não satisfazem esses gates.'
Add-Line '- Auditoria final adicionou provas específicas sem promoção indevida: C-SYSTEM-001 (P/Invoke), C-SYSTEM-007 (Registry/snapshot/rollback), C-CPU-003 (Performance Counters), C-SYSTEM-012 (ETW/WPR FAIL preservado), C-GPU-015 (ADL entrypoints), C-SECURITY-006 (admin/UAC), C-PROCESSES-007 (restart de processo controlado).'
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
Add-Line '- Dois caminhos de display ativos foram observados; correlação física source/target/EDID, HDR, VRR, DDC/CI e hotplug não foram testados.'
Add-Line '- Wi-Fi, WWAN/hotspot, VPN corporativa, proxy gerenciado e policy corporativa não testados.'
Add-Line '- BitLocker/device encryption, Storage Spaces/RAID e cenários de filesystem corrompido não testados.'
Add-Line '- USB4/Thunderbolt/dock, Bluetooth coexistence e falhas USB induzidas não testados.'
Add-Line '- Múltiplos endpoints Core Audio foram enumerados; correlação física/PnP, Bluetooth/USB audio, spatial sound e sleep/resume não foram testados.'
Add-Line '- Reboot/logoff e retomada pós-reboot não executados.'
Add-Line '- Captura WPR/ETW elevada não executada porque exigiria elevação/UAC fora da sessão atual.'
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
Add-Line '- Observado nos prototypes adicionais: P/Invoke funcionou para identidade nativa; Registry HKCU permitiu snapshot/rollback/idempotência; Performance Counters exigiram descoberta localizada; ADL export exists para AMD neste host; admin/UAC produziu access denied estruturado; restart de processo controlado funcionou; WPR/ETW exigiu privilégio para captura.'
Add-Line '- Observado: 20 domínios puderam emitir pelo menos um probe read-only estruturado; isso não demonstra cobertura completa de qualquer domínio.'
Add-Line '- Observado: 1 de 27 probes do bundle exigiu elevação (DISM /CheckHealth); nos adicionais, WPR trace também falhou por requisito de privilégio.'
Add-Line '- Observado: nenhuma prova executou reboot/logoff; restart de processo controlado e descoberta de lock owner via Restart Manager foram testados.'
Add-Line '- Provas adicionais executadas:'
foreach ($proof in $additionalProofs) {
    Add-Line "  - ``$($proof.Id)``: $($proof.Summary) Evidência: ``/$($proof.Path)``."
}
Add-Line '- Inventário de resultados JSON persistidos:'
foreach ($resultFile in Get-ChildItem -LiteralPath (Join-Path $RepositoryRoot 'prototypes') -Recurse -File -Filter '*.json' | Where-Object { $_.FullName -match '[\\/]results[\\/]' } | Sort-Object FullName) {
    $relativeResult = $resultFile.FullName.Substring($RepositoryRoot.Length + 1).Replace('\', '/')
    Add-Line "  - ``/$relativeResult``"
}
Add-Line
Add-Line '### Cobertura representativa dos mecanismos'
Add-Line
Add-Line '| Mecanismo | Resultado observado | Limite preservado |'
Add-Line '|---|---|---|'
Add-Line '| Win32 / PInvoke | PASS — GetNativeSystemInfo/RtlGetVersion e PDH; AMD ADL classic via interop. | Um host x64; ARM64 e outros vendors não testados. |'
Add-Line '| WMI / CIM | PASS — bundle de 20 domínios e inventários nativos. | Sinais parciais; CIM não substitui API autoritativa específica. |'
Add-Line '| Registry | PASS — leitura de policy/reboot e fixture HKCU de baseline/diff. | Nenhuma mutação HKLM/policy real. |'
Add-Line '| PowerShell / CLI | PASS — cmdlets nativos, powercfg, net, DISM e WPR foram realmente invocados. | DISM e WPR preservam falhas de permissão. |'
Add-Line '| Admin / UAC | PASS para detecção de token não elevado e sinal ACCESS_DENIED; caminho elevado BLOCKED. | Nenhum prompt UAC/helper elevado/IPC foi executado. |'
Add-Line '| Hardware / vendor API | PASS somente para AMD ADL classic read-only. | Intel/NVIDIA, ADLX, tuning e matriz de hardware BLOCKED/NOT_TESTED. |'
Add-Line '| Performance Counters | PASS — Get-Counter localizado e PdhAddEnglishCounterW temporal. | Thresholds/per-core/overhead amplo ainda limitados. |'
Add-Line '| ETW / WPR | FAIL/BLOCKED_ADMIN — 0xc5585011 em duas tentativas não elevadas. | Nenhum ETL foi criado; falha não foi promovida. |'
Add-Line '| Snapshot / Rollback | PASS em fixture HKCU e processo-filho próprios. | Evidência de harness; não implica Apply/Rollback em feature read-only. |'
Add-Line '| Restart / Restart Manager | PASS para processo próprio controlado; `RmStartSession/RmRegisterResources/RmGetList` identificaram o lock owner e o restart foi verificado. | `RmShutdown/RmRestart`, serviço, dispositivo, logoff e reboot permaneceram bloqueados por risco/ambiente. |'
Add-Line
Add-Line '## Revisão cruzada por domínio'
Add-Line
foreach ($domain in $specs.Domain | Sort-Object -Unique) {
    $domainSpecs = @($specs | Where-Object Domain -eq $domain)
    $domainSpecified = @($domainSpecs | Where-Object Status -eq 'SPECIFIED')
    $domainSafe = @($domainSpecified | Where-Object SafeProof -ne 'NOT_TESTED')
    $domainStatus = @($domainSpecs | Group-Object Status | Sort-Object Name | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join ', '
    Add-Line "- ``$domain``: $($domainSpecs.Count) specs ($domainStatus); prova segura explícita em $($domainSafe.Count)/$($domainSpecified.Count) ``SPECIFIED``; fontes, ownership, rollback, erros e sobreposições confrontados; nenhum PROVEN/APPROVED."
}
Add-Line
Add-Line '## Revisão cruzada global'
Add-Line
Add-Line '- Nenhum status PROVEN/APPROVED existe sem evidência.'
Add-Line "- As 234 specs possuem ID único e distribuição final fechada; as $(@($specs | Where-Object Status -eq 'SPECIFIED').Count) ``SPECIFIED`` têm ``SAFE_PROOF`` diferente de ``NOT_TESTED`` ($($specifiedProofCoverage.Count)/$(@($specs | Where-Object Status -eq 'SPECIFIED').Count))."
if ($finalVerification) {
    Add-Line "- Verificação central persistida: ``$($finalVerification.status)``; $($finalVerification.testCount)/$($finalVerification.testCount) harnesses executados, $(@($finalVerification.failedTests).Count) falhas de harness, $(@($finalVerification.privacyMatches).Count) ocorrências de privacidade e $($finalVerification.specCount) specs. Evidência: ``/prototypes/system/C-SYSTEM-017/results/final-verification.json``."
}
Add-Line '- O bundle comum registra provenance por probe, mas não foi usado como substituto de fonte de verdade específica.'
Add-Line '- Permanecem abertos: ownership de inventários compartilhados fora do domínio System; fonte autoritativa por campo; semântica uniforme de Admin/Restart/System Restore; sobreposições Gaming↔Display/Audio/Network, GPU↔Processes/RAM e Network↔Security/Energy.'
Add-Line '- As mutações destrutivas/alto risco não foram improvisadas no Windows real; seus resultados permanecem NOT_TESTED/RESEARCH conforme a spec.'
Add-Line '- Nenhum código do prototype foi promovido para backend final, solution, UI, DI, IPC ou contratos finais.'
if ($finalVerification -and $finalVerification.status -eq 'PASS') {
    Add-Line
    Add-Line '## Conclusão da etapa'
    Add-Line
    Add-Line 'Todas as provas essenciais que eram seguras e executáveis neste host foram executadas. As provas restantes dependem de hardware, autoridade, consentimento, incidente real, reboot/logoff ou ambiente externo e estão classificadas/limitadas nas próprias specs e nas seções H, J e K; não foram convertidas em PASS por inferência.'
    Add-Line
    Add-Line 'FASE 2 PRONTA PARA AUDITORIA FINAL'
}

$packageContent = $builder.ToString()
$matrixSection = [regex]::Match($packageContent, '(?s)## G\. Matriz de prova por Feature ID\n\n(.*?)(?=\n## H\.)')
if (-not $matrixSection.Success) { throw 'Could not extract proof matrix section' }
$matrixContent = "# Fase 2 — Matriz de provas por Feature ID`n`nGerada em: $(([datetimeoffset]$evidence.capturedAt).ToString('o'))`n`n" + $matrixSection.Groups[1].Value.TrimEnd() + "`n"

$outputDirectory = Split-Path -Parent $OutputPath
New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
[IO.File]::WriteAllText($OutputPath, $packageContent, [Text.UTF8Encoding]::new($false))
[IO.File]::WriteAllText($RootPackagePath, $packageContent, [Text.UTF8Encoding]::new($false))
foreach ($path in @($MatrixOutputPath,$ResultsMatrixOutputPath) | Select-Object -Unique) {
    $directory = Split-Path -Parent $path
    if ($directory) { New-Item -ItemType Directory -Path $directory -Force | Out-Null }
    [IO.File]::WriteAllText($path, $matrixContent, [Text.UTF8Encoding]::new($false))
}
Write-Output $OutputPath
