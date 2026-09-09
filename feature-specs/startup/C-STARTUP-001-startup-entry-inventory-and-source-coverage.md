# Feature Specification — C-STARTUP-001 — Startup entry inventory & source coverage

## 3. Identificação

Nome: Startup entry inventory & source coverage
ID: C-STARTUP-001
Tipo: Diagnostic, Diagnostic / Safeguard
Technical Domain: STARTUP
Primary Product Area: TBD
Also Used By: Optimization, Diagnostics
Shared Capability: No
Final UI Placement: TBD
Status: SPECIFIED
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Capability de produto para detectar e contextualizar **startup entry inventory & source coverage**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

**Base conceitual do Discovery:** [1] Startup Entry Inventory — `CANDIDATE`; [2] Run / RunOnce Inventory — `CANDIDATE`; [3] Startup Folder Inventory — `CANDIDATE`; [29] Autoruns Extended Location Awareness — `CANDIDATE`; [30] Explorer Shell Extension Startup Context — `CANDIDATE`; [31] Winlogon Extension Safeguard — `CANDIDATE`

### Objetivo
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

### Problema que resolve
Transforma a intenção do Discovery em estado técnico verificável sem presumir que “executa no logon/boot” significa problema ou que “alto impacto” significa automaticamente seguro para desativar.

### Benefício esperado
Diagnostic / Compatibility

## 5. Aplicabilidade

### Quando se aplica?
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

### Condições necessárias
- Windows desktop suportado e fonte de startup identificada.
- Fonte/owner da entrada precisa ser conhecida antes de interpretar ou alterar.
- Quando houver medição/comparação, contexto de boot e método de coleta devem ser registrados.

### Quando NÃO deve ser aplicada?
- Estado/source desconhecido não deve ser convertido em recomendação.
- RunOnce, service trigger, scheduled maintenance, security/accessibility e managed state exigem safeguards específicos.
- Nenhum mecanismo privado/undocumented pode ser usado só para imitar Task Manager/optimizer apps.

## 5.1 Uso dentro do produto

### Consumidores
- Optimization
- Diagnostics

### Uso direto pelo usuário
Partial

### Capacidade compartilhada
N/A — o Discovery classifica `Shared Capability: No`; reutilização de output não muda ownership.

## 6. Recomendação

### A feature deve ser recomendável automaticamente?
No

### Critérios de recomendação
N/A — esta capability fornece diagnóstico/safeguard/contexto.

### Motivo apresentado ao usuário
Mostrar fonte da entrada, identidade/role, evidência de impacto quando existente, motivo/guard e limitação. Distinguir “opcional” de “problemático”.

### Grau de confiança
High for documented Tier-1 sources; Medium/Low for extended locations.

## 7. Estado atual

### O que precisa ser detectado?
- Tier 1 canonical startup sources: HKCU/HKLM `Software\Microsoft\Windows\CurrentVersion\Run` and `RunOnce`, including WOW64 view where applicable.
- `FOLDERID_Startup` and `FOLDERID_CommonStartup` resolved through Known Folder API; enumerate shortcuts/files and resolve shortcut targets without executing them.
- Task Scheduler entries with Boot/Logon triggers through Task Scheduler 2.0 COM, recording path, author, principal, enabled state, triggers and actions.
- Auto-start services only as *context/safeguard*, not as normal startup-app entries; query start type and trigger-start configuration so a trigger-start service is not misclassified as boot bloat.
- Packaged `windows.startupTask` entries: detect package manifest/registered state where a documented enumeration path for third-party packages is available; do not assume `StartupTask` can manage arbitrary other packages.
- Tier 2 extended auto-start locations (Explorer shell extensions, Winlogon-related extensibility, other Autoruns-style locations): maintain a versioned coverage matrix and mark each location `documented`, `observed-only`, or `unsupported`.

### Como detectar?
Build source-specific collectors and normalize into one `StartupEntry` model. Do not collapse different source semantics into a single Registry-centric model. Tier-2 coverage remains explicit until each location has a supported read path.

### Fonte da verdade
For Tier 1, the configuration store/API that owns the entry (Registry Run/RunOnce, Known Folder contents, Task Scheduler registration, SCM configuration). Autoruns is evidence that extended auto-start locations exist, not a runtime source of truth for BeadWise.

### Estados possíveis
- Enabled entry
- Disabled-by-owning-surface when directly observable
- Transient RunOnce
- Trigger-start/context-only
- Broken target
- Extended-location observed
- Partial coverage
- Unsupported/Unknown/Error

## 8. Estado alvo
A deduplicated inventory in which every entry has `sourceKind`, owner scope (user/machine), raw identity, resolved target when safe, enabled/transient flags, provenance and coverage confidence; no modification.

## 9. Implementação técnica

### Método principal
Build source-specific collectors and normalize into one `StartupEntry` model. Do not collapse different source semantics into a single Registry-centric model. Tier-2 coverage remains explicit until each location has a supported read path.

### Tecnologias utilizadas
- [x] .NET API
- [x] Win32
- [x] Registry
- [ ] PowerShell
- [ ] CMD / executable
- [ ] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [x] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- RegOpenKeyEx/RegEnumValue for documented Run/RunOnce keys
- SHGetKnownFolderPath with FOLDERID_Startup/FOLDERID_CommonStartup
- IShellLink/IPersistFile for `.lnk` resolution
- Task Scheduler 2.0 COM (`ITaskService`, `IRegisteredTask`, trigger/action interfaces)
- SCM `QueryServiceConfig` + `QueryServiceConfig2(SERVICE_CONFIG_TRIGGER_INFO)` for service context

### Alternativas avaliadas
Do not shell out to Autoruns as the product inventory engine. Do not enumerate undocumented registry locations and silently call them supported.

### Abordagem escolhida
Usar superfícies públicas/documentadas por source, representar cobertura parcial explicitamente e manter Detect → Interpret → Recommend → Apply separados.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Somente para fontes/operações machine-wide que realmente exigirem acesso elevado; inventário per-user deve funcionar sem elevação quando possível.

### O que acontece se não houver permissão?
Retornar `ADMIN_REQUIRED`/`ACCESS_DENIED` por source/operação e preservar o restante do resultado como `Partial`.

## 11. Reinicialização

### Requer:
- [x] Nada
- [ ] Reinício do aplicativo
- [ ] Reinício de processo
- [ ] Reinício de serviço
- [ ] Logoff
- [ ] Reboot do Windows
- [ ] Desconhecido

### A alteração só pode ser validada depois da reinicialização?
N/A para leitura. Para benchmark de boot, uma nova inicialização é parte da medição, não requisito de persistência.

### Rollback também exige reinicialização?
No.

## 12. Change Plan

```text
Feature: C-STARTUP-001
Current state: structured Detect result
Target state: A deduplicated inventory in which every entry has `sourceKind`, owner scope (user/machine), raw identity, resolved target when safe, enabled/transient flags, provenance and coverage confidence; no modification.
Changes:
N/A — read-only diagnostic/safeguard; `Changes = []`.
Admin required: Conditional
Restart required: Nada
Risk: High
Reversible: N/A
```

## 13. Dry-run

### É suportado?
Yes

### O que o dry-run deve fazer?
Detect real, prerequisites, safeguard/policy evaluation e o mesmo ChangePlan da execução. Para read-only, `Changes=[]`. Para mutação, mostrar source, objeto/valor atual, operação, rollback e se o efeito só pode ser medido no próximo logon/boot.

### O que o dry-run NÃO pode fazer?
Não alterar Registry/task/file/service, não solicitar enable de startup task, não mover shortcut e não iniciar/reiniciar automaticamente o PC.

### Limitações
Impacto real e regressões só podem ser confirmados em sessão posterior controlada.

## 14. Snapshot

### É necessário?
No

### O que precisa ser salvo antes da alteração?
N/A — read-only.

### Estado inexistente também deve ser registrado
N/A.

## 15. Apply

### Sequência de execução
N/A — capability read-only/safeguard.

### Atomicidade
N/A.

## 16. Verify

### Como comprovar que funcionou?
Reexecutar a mesma fonte autoritativa; para performance, usar a metodologia da capability de medição. Exit code não basta.

### Critério de sucesso
A deduplicated inventory in which every entry has `sourceKind`, owner scope (user/machine), raw identity, resolved target when safe, enabled/transient flags, provenance and coverage confidence; no modification.

### Resultado parcial
Yes — cobertura/source/attribution incompleta deve permanecer `Partial`.

## 17. Rollback

### É reversível?
N/A

### Método de rollback
N/A — nenhuma alteração.

### O rollback restaura:
`estado original`, nunca “default”.

### Ordem de reversão
Inversa para operações dependentes.

## 18. Verify Rollback

### Critério de sucesso
N/A.

### O que fazer se rollback falhar?
Registrar erro, preservar snapshot/session, permitir retry seguro e fornecer ação manual. System Restore não é fallback automático para simples startup entries.

## 19. System Restore

### A feature exige ponto de restauração?
Not Required

### Motivo
Startup entries devem usar rollback específico. System Restore não substitui snapshot e seria desproporcional para mudanças source-specific reversíveis.

## 20. Risco

### Classificação
High

### Possíveis efeitos negativos
- Cobertura incompleta levar a falsa sensação de inventário total.
- Identidade/role/impact mal atribuídos levar a recomendação inadequada.
- Diagnóstico incorreto pode influenciar um consumidor, embora esta capability não altere o sistema.

### Pior cenário plausível
Classificação incorreta induzir uma decisão ruim em capability consumidora.

### Recuperação manual possível?
N/A

### Procedimento manual
N/A.

## 21. Compatibilidade

### Windows
- Windows 10 22H2: Legacy/conditional; fora do suporte padrão, decidir suporte de produto/ESU separadamente.
- Windows 11 23H2: Conditional por edição/lifecycle.
- Windows 11 24H2: Target de teste; Home/Pro chega ao fim de suporte em 2026-10-13.
- Windows 11 25H2: Target de teste.
- Windows 11 26H1: Target condicional; não presumir equivalência de build/event schema.

### Arquitetura
x64: target principal.
ARM64: expected for APIs Win32/WinRT documentadas, mas scripts/tooling e executáveis-alvo precisam de teste.
Other: TBD.

### Hardware
N/A, exceto efeitos de storage/CPU no benchmark e helpers ligados a hardware.

## 22. Dependências

### Outras features
N/A dentro de Startup; fornece base para as demais.

### Serviços
Task Scheduler/SCM apenas quando a fonte em análise exigir.

### APIs
RegOpenKeyEx/RegEnumValue for documented Run/RunOnce keys, SHGetKnownFolderPath with FOLDERID_Startup/FOLDERID_CommonStartup, IShellLink/IPersistFile for `.lnk` resolution, Task Scheduler 2.0 COM (`ITaskService`, `IRegisteredTask`, trigger/action interfaces), SCM `QueryServiceConfig` + `QueryServiceConfig2(SERVICE_CONFIG_TRIGGER_INFO)` for service context

### Componentes do Windows
Registry, Shell Known Folders, Task Scheduler, SCM, ETW/WPT conforme a capability.

### Drivers
N/A.

### Internet
No para baseline; online reputation/catalog não é requisito.

### Aplicações externas
WPR/WPA/ADK é dependência de **prova/escalation** em C-STARTUP-002, não do inventário básico.

## 23. Conflitos
Software que recria auto-start, installers/updaters, política corporativa, Task Scheduler, security software e alterações manuais do usuário. BeadWise deve detectar drift/conflito e não assumir ownership.

## 24. Idempotência
Yes — read-only.

## 25. Falhas possíveis
- `SOURCE_UNSUPPORTED`
- `PARTIAL_COVERAGE`
- `ENTRY_NOT_FOUND`
- `TARGET_UNRESOLVED`
- `ACCESS_DENIED`
- `ADMIN_REQUIRED`
- `POLICY_BLOCKED`
- `DEPENDENCY_UNKNOWN`


## 26. Logging
Registrar capability/source, stable entry identity, provenance, confidence, detection result, guard/recommendation reason e measurement/session IDs. Para mutação: snapshot reference, exact operation, Apply/Verify/Rollback. Não registrar command lines que possam conter secrets sem redaction.

## 27. Persistência

### O estado precisa sobreviver ao fechamento do app?
Conditional — current inventory can be ephemeral; user intent/history/drift identifiers must persist when consumed.

### O snapshot precisa sobreviver reboot?
N/A.

### Arquivos/dados necessários
Schema versionado para StartupEntry, source/provenance, user intent, measurements/history; ChangePlan/snapshot/session para alterações.

## 28. Testes

### Teste unitário
Parsers/normalization, stable identity, role/scoring/guards, ChangePlan, snapshot equality, command-line edge cases e state transitions.

### Dry-run
Fixtures para cada source, partial coverage, permission denial, RunOnce, trigger-start service, task maintenance, user-required entry e Settings-owned packaged startup.

### Integração real
Windows 11 24H2/25H2 e, quando possível, 26H1; criar entradas de teste em Run, Startup folder e scheduled task sem usar software real crítico.

### Rollback
N/A.

### VM
Safe para Registry/Startup folder/tasks/rollback e WPR lógico; boot-performance absoluto não representa hardware real.

### Hardware real
No

## 29. Cenários mínimos de teste
- [ ] Estado normal
- [ ] Source não suportado/partial
- [ ] Falta permissão
- [ ] RunOnce/transient
- [ ] Scheduled task Boot/Logon
- [ ] Trigger-start service
- [ ] Entry com target ausente/ambíguo
- [ ] Entry com assinatura válida/inválida/ausente
- [ ] Execução repetida


## 30. Prova técnica

### Script/protótipo
Local: `/prototypes/startup/C-STARTUP-001/` — executado na auditoria final da Fase 2.

### Resultado
```text
DETECT: PASS
PLAN: N/A
DRY-RUN: N/A
APPLY: N/A
VERIFY: PASS
ROLLBACK: N/A
RESTORE VERIFY: N/A
```

### Ambiente utilizado
```text
Windows version: Windows 11 Pro 10.0.26200 (build 26200), x64
Hardware: Desktop; AMD Ryzen 7 5700; Radeon RX 570 Series; ASUS PRIME B450M-GAMING/BR
Admin: No
Date: 2026-09-08
```

## 31. Evidências

- Documented behavior — https://learn.microsoft.com/windows/win32/setupapi/run-and-runonce-registry-keys
- Documented behavior — https://learn.microsoft.com/windows/win32/w8cookbook/startup-apps
- Documented behavior — https://learn.microsoft.com/windows/win32/shell/knownfolderid
- Documented behavior — https://learn.microsoft.com/windows/win32/taskschd/task-scheduler-2-0-interfaces
- Documented behavior — https://learn.microsoft.com/windows/win32/services/service-trigger-events
- Documented behavior — https://learn.microsoft.com/uwp/api/windows.applicationmodel.startuptask?view=winrt-28000
- Documented behavior — https://learn.microsoft.com/sysinternals/downloads/autoruns

**Observed behavior (campanha 2026-09-08):** probes compartilhados read-only executados neste host: `startup.inventory.cim`=PASS. Evidência: `/prototypes/system/C-SYSTEM-017/results/domain-evidence.json`. Estes sinais são parciais e não satisfazem, sozinhos, o gate DETECT completo desta feature.

**Observed behavior (auditoria final 2026-09-08):** o prototype próprio `C-STARTUP-001` criou uma entrada controlada `HKCU\...\Run\BeadWisePhase2Proof`, executou inventário Registry Run + Startup folders, confirmou detecção exata, escopo `CurrentUser`, preservação de comando quando solicitada e restaurou/removou a entrada em `finally` (`PASS: controlled startup fixture was detected and normalized`). Evidência: `/prototypes/startup/C-STARTUP-001/results/startup-inventory.json`. A prova cobre HKCU/HKLM Run e Startup folders; RunOnce/WOW64, Task Scheduler, services, packaged startup tasks e logon real continuam `NOT_TESTED`, portanto o status permanece `SPECIFIED`.

<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Não para o gate final da Fase 2 — inventário de Startup folders e Registry Run com fixture HKCU controlada foi executado e limpo; RunOnce/WOW64, Task Scheduler, services, packaged startup tasks e logon real ficam como limitações para PROVEN/arquitetura futura.
- Evidência realmente executada: Fixture HKCU Run inofensiva detectada com scope/provenance e restaurada em finally; inventário real persistido com command lines redigidas.
- Execução segura neste host / teste: Teste serializado executado sem logoff/reboot; valor original foi restaurado ou removido conforme snapshot.
- Impedimento ou limitação restante: RunOnce/WOW64, shortcuts adversariais, Task Scheduler, SCM, packaged startup tasks e logon real não foram provados e não devem ser inferidos a partir da fixture HKCU Run.
- Disposição: `SPECIFIED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

## 32. Benefício real
Reasonable

## 33. Impacto mensurável

### Pode ser medido?
No

### Como?
N/A — correctness is coverage/identity/provenance, not a performance delta.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
Nome/role/publisher, source, enabled/transient state, impact evidence quando disponível, recommendation/guard reason, confidence, risk, admin, rollback e “managed/manual-only” quando aplicável.

### Ações disponíveis
Details sempre. Apply/Skip/Rollback apenas em C-STARTUP-004/008 quando o source-specific plan for suportado; Settings guidance quando BeadWise não possuir controle público seguro.

## 35. Dados necessários pelo frontend

### Data exposed directly to UI
`id`, `displayName`, `publisher`, `role`, `sourceKind`, `scope`, `currentState`, `impact`, `confidence`, `recommended`, `guardReason`, `risk`, `requiresAdmin`, `reversible`, `manualOnly`.

### Data consumed internally
Raw source identity, registry/task/file/package identifiers, signer verification details, measurement/session IDs, provenance, policy/dependency flags, ChangePlan/snapshot references. Redact secrets from command lines.

## 36. Questões em aberto

- Cobertura mínima suportada definida: Run/RunOnce (HKCU/HKLM incluindo views relevantes) e Startup folders; outras extensões (scheduled tasks, services, shell/winlogon) devem ser classificadas pela fonte e nunca achatadas como se fossem equivalentes.
- Inventário é read-only e deve preservar provenance, user scope e publisher/path quando disponíveis.
- Entradas protegidas/sistema não viram automaticamente candidatas a disable.
- `PROVEN` exige fixtures reais em cada fonte suportada e deduplicação por identidade sem perder provenance.

## 37. Critério para PROVEN
- [ ] Detect validado em todas as sources declaradas supported
- [ ] Partial/Unsupported preservados corretamente
- [ ] Dry-run sem mutação
- [ ] Apply/Snapshot/Rollback confirmados N/A
- [ ] Compatibilidade mínima testada
- [ ] Questões críticas acima resolvidas ou explicitamente bloqueantes

## 38. Critério para APPROVED
Somente após PROVEN + utilidade/risco/manutenção/UX/redundância revisados. Em particular, não aprovar mecanismo privado só para alcançar paridade visual com Task Manager/Autoruns.
