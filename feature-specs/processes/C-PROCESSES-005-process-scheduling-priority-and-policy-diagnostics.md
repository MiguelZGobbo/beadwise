# Feature Specification — C-PROCESSES-005 — Process scheduling, priority & policy diagnostics

## 3. Identificação

Nome: Process scheduling, priority & policy diagnostics
ID: C-PROCESSES-005
Tipo: Configuration / Safeguard, Diagnostic
Technical Domain: PROCESSES
Primary Product Area: TBD
Also Used By: Monitoring, Diagnostics, Optimization
Shared Capability: No
Final UI Placement: TBD
Status: SPECIFIED
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Capability de produto para avaliar e, quando justificado, permitir configurar **process scheduling, priority & policy diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

**Base conceitual do Discovery:** [14] Process Priority Class Diagnostics — `CANDIDATE`; [15] Process Priority Adjustment Safeguard — `CANDIDATE`; [16] Background Processing Mode Awareness — `CANDIDATE`; [17] Efficiency / Eco Mode Awareness — `CANDIDATE`; [18] Process Affinity / CPU Set State Diagnostics — `CANDIDATE`; [19] Process I/O Priority Awareness — `CANDIDATE`; [20] Process Memory Priority Awareness — `CANDIDATE`; [21] Job Object Membership Awareness — `CANDIDATE`; [35] Process Mitigation Policy Awareness — `CANDIDATE`; [38] Process Working-Set Trim Awareness — `CANDIDATE`

### Objetivo
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

### Problema que resolve
Evita decisões baseadas em suposição sobre **Process scheduling, priority & policy diagnostics**, modelando estado real, contexto, limitações e falhas antes de qualquer recomendação ou ação.

### Benefício esperado
Diagnostic / Quality of Life

## 5. Aplicabilidade

### Quando se aplica?
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

### Condições necessárias
- Fonte técnica documentada disponível para o estado que será interpretado.
- Compatibilidade de Windows/hardware/driver validada antes de concluir suporte.
- Estado gerenciado por política, virtualização ou fabricante deve ser representado explicitamente.

### Quando NÃO deve ser aplicada?
- Quando a fonte de verdade necessária não estiver disponível: retornar `Unknown/Unsupported`, não inferir.
- Quando política corporativa ou software de fabricante for owner explícito do estado e a operação proposta puder conflitar.
- Não converter diferença de default em problema sem evidência contextual.
- Não executar mecanismo consolidado no Discovery que esteja `QUESTIONABLE`/não autorizado apenas por estar tecnicamente acessível.

## 5.1 Uso dentro do produto

### Consumidores
- Monitoring
- Diagnostics
- Optimization

### Uso direto pelo usuário
Partial

### Capacidade compartilhada
N/A — o Discovery classifica `Shared Capability: No`; consumidores podem reutilizar o output sem transferir ownership.

## 6. Recomendação

### A feature deve ser recomendável automaticamente?
Conditional

### Critérios de recomendação
Somente quando Detect confirmar aplicabilidade, o estado não for meramente uma preferência/default diferente, a mudança tiver benefício contextual plausível, não estiver bloqueada por política/owner externo e o ChangePlan for verificável/reversível conforme esta spec.

### Motivo apresentado ao usuário
Explicar o estado observado, a evidência, o contexto que torna a mudança relevante e o que exatamente será alterado; nunca prometer ganho universal.

### Grau de confiança
High somente para estados diretamente retornados por fonte documentada e coerente; Medium/Low para correlação/inferência parcial; Not Applicable quando fora de suporte.

## 7. Estado atual

### O que precisa ser detectado?
- Process identity must be stable as `PID + creation time`; a reused PID must never receive a stale plan.
- Priority class: `GetPriorityClass`.
- CPU affinity: `GetProcessAffinityMask`; CPU-set-specific state only through documented CPU Set APIs on builds where available.
- Memory priority and power throttling/EcoQoS: `GetProcessInformation` with the corresponding `PROCESS_INFORMATION_CLASS` when supported.
- Job membership: `IsProcessInJob` / documented Job Object queries when required for safeguards.
- Mitigation policy: `GetProcessMitigationPolicy` for read-only context.
- Working-set limits/state through documented Process Working Set APIs when diagnostic context requires them.
- **I/O priority:** no generic public Win32 mutation contract was established in this review; do not use `NtQueryInformationProcess/NtSetInformationProcess` or private information classes as product contract. Represent as `Unsupported/Unknown` unless a supported surface is later proven.

### Como detectar?
Open the target with the minimum query rights, capture `PID + creation time`, query only documented process APIs, and revalidate identity immediately before Apply. Access denied to protected/system processes is an explicit state, not evidence that a value is absent.

### Fonte da verdade
The documented Win32 getter paired with each supported setter is authoritative for mutable fields. ETW/Performance Counters can enrich workload context but do not replace the actual scheduling-policy getter.

### Estados possíveis
- Supported / current value known
- Supported / change candidate
- Restricted/protected process
- Process exited / identity changed
- Partial data
- Unsupported field
- Unknown / Error

## 8. Estado alvo
Only an explicitly selected, supported change for **Process scheduling, priority & policy diagnostics**, built from current state and context, reaches its declared post-condition; otherwise no change is performed.

## 9. Implementação técnica

### Método principal
Split the capability into **documented mutable fields** and **diagnostic-only fields**. A ChangePlan may contain only operations that have a supported getter/setter pair and an exact pre-change snapshot.

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
- [ ] Other

### Comandos / APIs / chaves
Supported mutable subset:
- `GetPriorityClass` / `SetPriorityClass`
- `GetProcessAffinityMask` / `SetProcessAffinityMask` when affinity adjustment is explicitly selected
- `GetProcessInformation` / `SetProcessInformation` for supported classes such as `ProcessMemoryPriority` and `ProcessPowerThrottling`

Read-only/safeguard context:
- `IsProcessInJob`
- `GetProcessMitigationPolicy`
- documented working-set APIs
- process identity/timing APIs

Explicit exclusions:
- no undocumented `NtSetInformationProcess`/private information classes for I/O priority or hidden scheduling knobs;
- no automatic REALTIME priority recommendation;
- no working-set trimming presented as a performance optimization.

### Alternativas avaliadas
- Native/NT private information classes: rejected as production contract because compatibility/support guarantees are insufficient.
- Registry tweaks and scheduler folklore: rejected.
- Vendor/game-specific tuning: out of ownership; may be contextual evidence only.

### Abordagem escolhida
Use exact documented state transitions and make unsupported subfields visible instead of forcing one generic scheduling-tuning abstraction.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Somente no momento em que a operação específica exigir acesso elevado; Detect deve degradar para dados parciais quando possível antes de solicitar elevação.

### O que acontece se não houver permissão?
Retornar erro estruturado (`ADMIN_REQUIRED`/`ACCESS_DENIED`) por operação/campo e preservar os dados de leitura que puderem ser obtidos com segurança.

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
No for the supported per-process operations. Verify immediately with the matching getter.

### Rollback também exige reinicialização?
No. Rollback is immediate while the original process identity still exists.

## 12. Change Plan

```text
Feature: C-PROCESSES-005
Current state: PID + creation time + exact supported policy fields
Target state: only the explicitly selected documented field/value
Changes:
1. Revalidate PID + creation time.
2. Verify target process is not restricted by the action policy.
3. Capture exact getter values for every field to be changed.
4. Apply one documented setter per ChangePlan operation.
5. Re-read each field and compare actual vs target.
6. On failure, restore already changed fields from the snapshot when process identity still matches.
Admin required: Conditional
Restart required: No
Risk: Medium (High/Realtime class is never auto-recommended; REALTIME is blocked by product safeguard)
Reversible: Fully for supported fields while the same process instance exists
```

A plan must never silently include I/O-priority/private NT operations or other unsupported fields.

## 13. Dry-run

### É suportado?
Yes

### O que o dry-run deve fazer?
- Executar Detect real e validar prerequisites/managed state.
- Construir exatamente o mesmo ChangePlan que Apply consumiria.
- Mostrar valores/objetos atuais e alvo, risco, admin, restart e reversibilidade.
- Para read-only, retornar `Changes = []`.

### O que o dry-run NÃO pode fazer?
Não persistir configuração, reiniciar processo/serviço/dispositivo, iniciar reparo destrutivo, instalar/remover pacote/driver ou executar workload que altere materialmente o estado.

### Limitações
Efeitos de desempenho, estabilidade, hardware/vendor e operações que exigem reboot só podem ser comprovados por teste real controlado.

## 14. Snapshot

### É necessário?
Yes

### O que precisa ser salvo antes da alteração?
- stable process identity (`PID + creation time`);
- exact priority class when changed;
- exact affinity mask/CPU-set state when changed;
- exact supported `GetProcessInformation` structure/value when changed;
- field-by-field support/access result.

### Estado inexistente também deve ser registrado
Yes. Unsupported/unqueryable is not converted to a default value and makes that field ineligible for Apply.

## 15. Apply

### Sequência de execução
1. Revalidate process identity.
2. Reject stale/exited/protected targets and unsupported fields.
3. Capture the final snapshot.
4. Apply only the user-selected documented field.
5. Read the field back with the matching getter.
6. If Verify fails and the same process instance exists, restore the snapshot for fields already changed.

### Atomicidade
Prefer one field per user-visible operation. If a plan intentionally contains multiple independent supported fields, failure stops subsequent operations and rolls back already changed fields in reverse order. A process exit changes the result to `TARGET_EXITED`; it must not cause changes to a new process that reused the PID.

## 16. Verify

### Como comprovar que funcionou?
Reexecutar a mesma detecção autoritativa e comparar estado real com o alvo/critério definido; exit code ou ausência de exceção nunca bastam sozinhos.

### Critério de sucesso
Only an explicitly selected, supported change for **Process scheduling, priority & policy diagnostics**, built from current state and context, reaches its declared post-condition; otherwise no change is performed.

### Resultado parcial
Yes — quando apenas parte independente do estado puder ser lida/validada. Nunca promover `Partial` silenciosamente a sucesso total.

## 17. Rollback

### É reversível?
Fully for the supported mutable subset **while the same process instance still exists**. If the target exits/restarts, the old per-process state no longer exists and rollback becomes `Not Applicable/Stale`, not an attempt to modify a new PID owner.

### Método de rollback
Revalidate `PID + creation time`, then call the same documented setter for each changed field using the exact getter value captured before Apply.

### O rollback restaura:
`estado original capturado`.

### Ordem de reversão
Reverse Apply order for multi-field plans.

## 18. Verify Rollback

### Critério de sucesso
Detect pós-rollback é semanticamente equivalente ao snapshot original para todos os campos sob ownership da operação.

### O que fazer se rollback falhar?
Registrar `ROLLBACK_FAILED`, preservar snapshot/audit, bloquear repetição automática potencialmente destrutiva e fornecer retry/ação manual/System Restore somente se tecnicamente apropriado.

## 19. System Restore

### A feature exige ponto de restauração?
Not Required

### Motivo
The supported changes are per-process runtime state and do not persist system configuration. System Restore is not an appropriate rollback mechanism.

## 20. Risco

### Classificação
High

### Possíveis efeitos negativos
- Misclassification when a source exposes incomplete/virtualized state.
- False confidence if one signal is treated as definitive without provenance/context.
- User-customized or policy-managed state could be overwritten if snapshot/managed-state guards fail.
- Partial application may leave mixed state if atomicity handling is incorrect.

### Pior cenário plausível
Mudança incorreta/partial failure causar regressão no subsistema ou exigir recuperação manual; a severidade concreta depende da operação escolhida.

### Recuperação manual possível?
Partial

### Procedimento manual
Usar o snapshot e a interface oficial correspondente para restaurar o estado; escalar para ferramenta nativa/WinRE/System Restore apenas conforme o tipo de falha.

## 21. Compatibilidade

### Windows
- Windows 10 22H2: Legacy/conditional; fora do suporte padrão, tratar ESU/escopo de produto separadamente e confirmar APIs específicas se o produto decidir suportá-lo.
- Windows 11 23H2: Conditional; Home/Pro fora de suporte, Enterprise/Education ainda dependem do lifecycle da edição; detectar edição/build.
- Windows 11 24H2: Target de teste; considerar fim de suporte próximo para Home/Pro em 2026-10-13.
- Windows 11 25H2: Target de teste.
- Windows 11 26H1: Target condicional; build/hardware branch deve ser detectado, não assumido equivalente a 24H2/25H2.

### Arquitetura
- x64: Target principal de teste.
- ARM64: Conditional; somente APIs/paths comprovados.
- Other: Unsupported/TBD.

### Hardware
CPU vendor: Conditional/N/A
GPU vendor: Conditional/N/A
Laptop/Desktop: Detectar; não assumir equivalência em energia/firmware.
Device class: conforme a capability.

## 22. Dependências

### Outras features
Usar outputs de capabilities relacionadas por ID quando definidos no Discovery; não duplicar detectores apenas por existirem múltiplos consumidores.

### Serviços
Somente serviços nativos necessários às APIs escolhidas; detectar indisponibilidade e retornar estado estruturado.

### APIs
CreateToolhelp32Snapshot / Process32First/Next or EnumProcesses, OpenProcess / QueryFullProcessImageName / GetProcessTimes, GetProcessMemoryInfo, PDH / Performance Counters, ETW, Wait Chain Traversal API

### Componentes do Windows
Componentes do subsistema PROCESSES e infraestrutura comum de Event Log/ETW/CIM quando aplicável.

### Drivers
Conditional — necessários apenas quando a fonte/ação depende de dispositivo ou vendor.

### Internet
Conditional — somente para catálogo/lifecycle/vendor/update externo explicitamente previsto; detecção local não deve depender da Internet sem necessidade.

### Aplicações externas
N/A por padrão; ferramentas vendor/terceiros só entram como dependência explícita de prova/escalation.

## 23. Conflitos
Pode conflitar com política corporativa/MDM, software de fabricante, Windows Update, antivírus/security tooling, tuning software e personalizações do usuário. Antes de alterar, Detect deve identificar ownership/policy quando disponível; estado gerenciado não deve ser sobrescrito silenciosamente.

## 24. Idempotência

Yes — Apply repetido deve convergir para o mesmo estado alvo e virar no-op quando já aplicado; confirmar por operação concreta.

## 25. Falhas possíveis
- `ADMIN_REQUIRED`
- `ACCESS_DENIED`
- `UNSUPPORTED_WINDOWS_VERSION`
- `HARDWARE_UNSUPPORTED`
- `SETTING_NOT_FOUND` / `SOURCE_UNAVAILABLE`
- `POLICY_BLOCKED`
- `PARTIAL_DATA`
- `DETECTION_FAILED`
- `APPLY_FAILED`
- `VERIFY_FAILED`
- `ROLLBACK_FAILED`
- `RESTART_REQUIRED`

## 26. Logging
Registrar: Detection started/completed, source/provenance, normalized current state, confidence, plan generated, policy/compatibility guards, admin/restart requirements, e — para mutações — snapshot/apply/verify/rollback lifecycle. Não registrar conteúdo pessoal/sensível além do mínimo técnico necessário.

## 27. Persistência

### O estado precisa sobreviver ao fechamento do app?
Yes para histórico/audit quando a capability participar de correlação, before/after ou mudança; caso contrário somente cache efêmero.

### O snapshot precisa sobreviver reboot?
Yes quando ChangePlan indicar reboot ou rollback pós-reboot; caso contrário pode ser persistido até Verify/Keep finalizar.

### Arquivos/dados necessários
Schema versionado de resultado, provenance, timestamps, capability ID/status; para mutações, ChangePlan + snapshot + apply/verify/rollback result.

## 28. Testes

### Teste unitário
- Normalização/parser de fontes.
- Regras de estados e recomendação.
- Geração de ChangePlan.
- Comparação state vs target/snapshot.
- Compatibilidade, policy e idempotência.

### Dry-run
Usar fixtures de Supported/Unsupported/Managed/Partial e confirmar zero efeito persistente.

### Integração real
Executar Detect em Windows suportado e comparar com ferramenta/API nativa; para mutações, aplicar somente em ambiente controlado com snapshot.

### Rollback
Testar estado customizado, estado ausente e falha intermediária; confirmar igualdade semântica com snapshot.

### VM
Partial — adequada para lógica/OS/configuração; inadequada para conclusões de hardware/telemetria física.

### Hardware real
Required

## 29. Cenários mínimos de teste
- [ ] Feature já está no estado desejado / diagnóstico normal
- [ ] Feature precisa ser alterada ou apresenta condição relevante
- [ ] Feature não é suportada
- [ ] Falta permissão
- [ ] Fonte retorna dados parciais/unknown
- [ ] Apply falha
- [ ] Verify falha
- [ ] Rollback funciona
- [ ] Rollback falha
- [ ] Estado inicial personalizado
- [ ] Configuração inexistente
- [ ] Execução repetida
- [ ] Estado gerenciado por policy/vendor quando aplicável

## 30. Prova técnica

### Script/protótipo
Local: `/prototypes/processes/c-processes-005/` — **TBD / ainda não executado nesta fase documental**.

### Resultado
```text
DETECT: NOT_TESTED
PLAN: NOT_TESTED
DRY-RUN: NOT_TESTED
APPLY: NOT_TESTED
VERIFY: NOT_TESTED
ROLLBACK: NOT_TESTED
RESTORE VERIFY: NOT_TESTED
```

### Ambiente utilizado
```text
Windows version: TBD
Hardware: TBD
Admin: TBD
Date: TBD
```

A spec não deve receber `PROVEN` antes dessa prova quando os itens forem aplicáveis.

## 31. Evidências

- Documented behavior — https://learn.microsoft.com/windows/win32/procthread/process-and-thread-functions
- Documented behavior — https://learn.microsoft.com/windows/win32/debug/wait-chain-traversal
- Documented behavior — https://learn.microsoft.com/windows/win32/etw/about-event-tracing
- Documented behavior — https://learn.microsoft.com/windows/win32/wer/windows-error-reporting

**Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.
- Documented behavior — https://learn.microsoft.com/windows/win32/api/processthreadsapi/nf-processthreadsapi-getpriorityclass
- Documented behavior — https://learn.microsoft.com/windows/win32/api/processthreadsapi/nf-processthreadsapi-setpriorityclass
- Documented behavior — https://learn.microsoft.com/windows/win32/api/processthreadsapi/nf-processthreadsapi-getprocessinformation
- Documented behavior — https://learn.microsoft.com/windows/win32/api/processthreadsapi/nf-processthreadsapi-setprocessinformation
- Documented behavior — https://learn.microsoft.com/windows/win32/api/winbase/nf-winbase-getprocessaffinitymask
- Documented behavior — https://learn.microsoft.com/windows/win32/api/winbase/nf-winbase-setprocessaffinitymask

<!-- PHASE2-SAFE-PROOF:START -->
**Prova prática segura executada (2026-09-08):** `C-PROCESSES-005=PASS` em `/prototypes/processes/C-PROCESSES-001/results/process-safe-capabilities.json`. Somente no filho próprio, priority `Normal→BelowNormal` e affinity `65535→1` foram aplicadas, reconsultadas e restauradas; `restored=true`. Nenhum processo do usuário/serviço/protegido foi mutado. PowerThrottling, memory priority e CPU Sets não foram exercitados.
<!-- PHASE2-SAFE-PROOF:END -->

<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — prioridade/affinity/PowerThrottling e negação de acesso.
- Evidência realmente executada: Prototype seguro adicional executado: `C-PROCESSES-005=PASS` em `/prototypes/processes/C-PROCESSES-001/results/process-safe-capabilities.json`. Somente no filho próprio, priority `Normal→BelowNormal` e affinity `65535→1` foram aplicadas, reconsultadas e restauradas; `restored=true`. Nenhum processo do usuário/serviço/protegido foi mutado. PowerThrottling, memory priority e CPU Sets não foram exercitados. Evidência: `/prototypes/processes/C-PROCESSES-001/results/process-safe-capabilities.json`. A evidência anterior foi substituída por esta observação mais recente.
- Execução segura neste host / teste: Sim, serial — aplicar/restaurar prioridade e affinity apenas em processo filho próprio; reconsultar via APIs públicas.
- Impedimento ou limitação restante: Memory priority/CPU sets variam por build; não testar outros processos.
- Disposição: `SPECIFIED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

## 32. Benefício real
Situational

A classificação permanece condicionada ao contexto descrito pelo Discovery e, quando houver alegação de desempenho/latência/energia, à medição controlada.

## 33. Impacto mensurável

### Pode ser medido?
No

### Como?
N/A — value is primarily diagnostic/configurational; success is state correctness rather than a performance delta.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Name / description
- Current state + provenance/confidence quando relevante
- Recommended state somente quando existe recomendação válida
- Reason / limitations
- Risk
- Admin / restart
- Reversible
- Managed/unsupported/partial state

### Ações disponíveis
Details sempre; Apply/Skip/Rollback somente quando houver ChangePlan mutável suportado. Não expor controles de implementação sem significado para o usuário.

## 35. Dados necessários pelo frontend

### Data exposed directly to UI
`id`, `name`, `description`, `currentState`, `status`, `confidence`, `recommended`, `recommendationReason`, `risk`, `requiresAdmin`, `requiresRestart`, `reversible`, `managed`, `limitations`.

### Data consumed internally
Source/provenance IDs, raw technical identifiers needed for correlation/apply/verify, compatibility flags, policy owner, timestamps, ChangePlan/snapshot handles. Raw sensitive data must not be exposed without need.

## 36. Questões em aberto

- Execute the Windows prototype/test matrix for priority class, affinity, memory priority and power-throttling on supported Windows 11 builds; documentary definition is sufficient for `SPECIFIED`, not `PROVEN`.
- Confirm exact minimum build for every optional CPU Set / ProcessInformation class included by the implementation before enabling it.
- Determine which cross-process targets permit each setter under normal user/admin rights and encode access-denied behavior.
- Keep I/O priority and any private NT scheduling class `Unsupported` until Microsoft exposes/documented a suitable contract; do not block the supported subset on that absence.

## 37. Critério para PROVEN
- [ ] Detect validado contra fonte nativa/documentada
- [ ] ChangePlan representa exatamente as operações quando aplicável
- [ ] Dry-run confirmou zero mutação
- [ ] Apply/Verify testados
- [ ] Snapshot/Rollback/Verify Rollback testados
- [ ] Compatibilidade mínima executada
- [ ] Falhas/Partial/Unsupported exercitados
- [ ] Nenhuma questão crítica da seção 36 permanece aberta

## 38. Critério para APPROVED
Somente após `PROVEN` e revisão de utilidade, confiabilidade, escopo, risco, manutenção, UX, não duplicação e ausência de dependência frágil injustificada. `READY_FOR_SPEC` do Discovery não implica `APPROVED`.
