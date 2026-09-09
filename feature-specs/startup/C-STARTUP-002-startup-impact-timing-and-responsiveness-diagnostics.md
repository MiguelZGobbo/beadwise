# Feature Specification — C-STARTUP-002 — Startup impact, timing & responsiveness diagnostics

## 3. Identificação

Nome: Startup impact, timing & responsiveness diagnostics
ID: C-STARTUP-002
Tipo: Diagnostic, Diagnostic / Benchmark, Diagnostic / Tool
Technical Domain: STARTUP
Primary Product Area: TBD
Also Used By: Optimization, Diagnostics
Shared Capability: No
Final UI Placement: TBD
Status: BLOCKED
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Capability de produto para diagnosticar **startup impact, timing & responsiveness diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

**Base conceitual do Discovery:** [4] Startup App Impact Diagnostics — `CANDIDATE`; [5] Boot / Logon Phase Timing — `CANDIDATE`; [32] Boot Trace / WPA Escalation — `CANDIDATE`; [33] Fast Startup vs Full Boot Benchmark Context — `CANDIDATE`; [34] Post-Logon Responsiveness Diagnostics — `CANDIDATE`

### Objetivo
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

### Problema que resolve
Transforma a intenção do Discovery em estado técnico verificável sem presumir que “executa no logon/boot” significa problema ou que “alto impacto” significa automaticamente seguro para desativar.

### Benefício esperado
Performance / Latency / Diagnostic

## 5. Aplicabilidade

### Quando se aplica?
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

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
High for collected ETW/ADK measurements; Medium for attribution when concurrent work overlaps.

## 7. Estado atual

### O que precisa ser detectado?
- Boot mode/context: full boot vs Fast Startup/hiberboot must be identified so samples are not compared as if equivalent.
- Boot/logon phase timestamps and post-logon busy period from ETW/WPR traces when detailed diagnosis is requested.
- Per-startup-process CPU time and disk I/O during the startup window; Windows documents Task Manager impact bands (>1 s CPU or >3 MB disk = High, 300 ms–1 s or 300 KB–3 MB = Medium, below both = Low). These bands may be shown as **Windows impact classification**, not as BeadWise universal thresholds.
- Foreground responsiveness/context during post-logon; do not equate total boot duration with user-perceived readiness.
- Deep escalation trace only on demand because WPR/ETW collection has overhead and may require reboot for boot traces.

### Como detectar?
Use lightweight counters for routine observation and WPR/ETW for controlled benchmark/escalation. Define a startup session boundary and persist environmental metadata. Compare only like-for-like sessions.

### Fonte da verdade
ETW/WPR/Windows Performance Toolkit trace for timing/attribution. Microsoft-documented startup impact thresholds may be used only when the same underlying CPU/disk semantics are reproduced; otherwise label BeadWise metrics separately.

### Estados possíveis
- No regression observed
- High/Medium/Low Windows-compatible impact classification (when method matches)
- Startup bottleneck candidate
- Post-logon contention candidate
- Trace required
- Incomparable sample (boot-mode/context mismatch)
- Partial/Unknown/Error

## 8. Estado alvo
A reproducible startup-performance record with boot mode, measurement window, per-entry/resource attribution and confidence; no automatic tweak is implied.

## 9. Implementação técnica

### Método principal
Use lightweight counters for routine observation and WPR/ETW for controlled benchmark/escalation. Define a startup session boundary and persist environmental metadata. Compare only like-for-like sessions.

### Tecnologias utilizadas
- [x] .NET API
- [x] Win32
- [ ] Registry
- [ ] PowerShell
- [ ] CMD / executable
- [ ] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [x] Other

### Comandos / APIs / chaves
- ETW providers via WPR/Windows Performance Toolkit
- WPA/ETL analysis pipeline for prototype/validation
- Process/disk performance counters as lower-cost sampling path
- Power/boot context signals sufficient to distinguish Fast Startup from full boot — exact API TBD in prototype

### Alternativas avaliadas
Do not scrape Task Manager UI or its displayed impact value. Do not invent a fixed “boot is slow above X seconds” threshold.

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
- [ ] Nada
- [ ] Reinício do aplicativo
- [ ] Reinício de processo
- [ ] Reinício de serviço
- [ ] Logoff
- [x] Reboot do Windows
- [ ] Desconhecido

### A alteração só pode ser validada depois da reinicialização?
N/A para leitura. Para benchmark de boot, uma nova inicialização é parte da medição, não requisito de persistência.

### Rollback também exige reinicialização?
No.

## 12. Change Plan

```text
Feature: C-STARTUP-002
Current state: structured Detect result
Target state: A reproducible startup-performance record with boot mode, measurement window, per-entry/resource attribution and confidence; no automatic tweak is implied.
Changes:
N/A — read-only diagnostic/safeguard; `Changes = []`.
Admin required: Conditional
Restart required: Conditional
Risk: Low
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
A reproducible startup-performance record with boot mode, measurement window, per-entry/resource attribution and confidence; no automatic tweak is implied.

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
Low

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
C-STARTUP-001

### Serviços
Task Scheduler/SCM apenas quando a fonte em análise exigir.

### APIs
ETW providers via WPR/Windows Performance Toolkit, WPA/ETL analysis pipeline for prototype/validation, Process/disk performance counters as lower-cost sampling path, Power/boot context signals sufficient to distinguish Fast Startup from full boot — exact API TBD in prototype

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
Optional

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
Local: `/prototypes/startup/c-startup-002/` — TBD.

### Resultado
```text
DETECT: NOT_TESTED
PLAN: NOT_TESTED
DRY-RUN: NOT_TESTED
APPLY: N/A
VERIFY: NOT_TESTED
ROLLBACK: N/A
RESTORE VERIFY: N/A
```

### Ambiente utilizado
```text
Windows version: TBD
Hardware: TBD
Admin: TBD
Date: TBD
```

## 31. Evidências

- Documented behavior — https://learn.microsoft.com/windows/win32/w8cookbook/startup-apps
- Documented behavior — https://learn.microsoft.com/windows-hardware/test/wpt/windows-performance-recorder
- Documented behavior — https://learn.microsoft.com/windows-hardware/test/wpt/introduction-to-wpr

Observed behavior: N/A nesta revisão; nenhuma execução real foi alegada.

**Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.

<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — impacto de boot/logon repetido e ETW.
- Evidência realmente executada: NENHUMA.
- Execução segura neste host / teste: Não integralmente — apenas verificar WPR/ETW availability sem iniciar boot trace.
- Impedimento ou limitação restante: Prova essencial requer boot/reboot controlado; não executar reboot/boot trace nesta sessão.
- Disposição: `BLOCKED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

## 32. Benefício real
Situational

## 33. Impacto mensurável

### Pode ser medido?
Yes

### Como?
Boot/logon phase durations, per-process CPU time/disk I/O, and post-logon responsiveness under controlled repeated sessions; report variance and boot mode.

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

- A Microsoft documenta impacto de Startup Apps com CPU e disk I/O; essa classificação pode ser exibida quando a fonte está disponível, sem reinterpretar limites como score universal do BeadWise.
- Boot/logon timing avançado deve usar ETW/WPA-style evidence quando necessário; Fast Startup e full boot precisam ser diferenciados.
- Não inferir causalidade de uma única inicialização; exigir repetição/variância para benchmark próprio.
- `PROVEN` requer comparar medição do app com traces/observação em boots controlados.

## 37. Critério para PROVEN
- [ ] Detect validado em todas as sources declaradas supported
- [ ] Partial/Unsupported preservados corretamente
- [ ] Dry-run sem mutação
- [ ] Apply/Snapshot/Rollback confirmados N/A
- [ ] Compatibilidade mínima testada
- [ ] Questões críticas acima resolvidas ou explicitamente bloqueantes

## 38. Critério para APPROVED
Somente após PROVEN + utilidade/risco/manutenção/UX/redundância revisados. Em particular, não aprovar mecanismo privado só para alcançar paridade visual com Task Manager/Autoruns.
