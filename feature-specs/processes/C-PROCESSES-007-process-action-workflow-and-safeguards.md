# Feature Specification — C-PROCESSES-007 — Process action workflow & safeguards

## 3. Identificação

Nome: Process action workflow & safeguards  
ID: C-PROCESSES-007  
Tipo: Diagnostic / Configuration, Diagnostic / Safeguard, Repair / Tool, Safeguard  
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
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **process action workflow & safeguards**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

**Base conceitual do Discovery:** [24] Process Suspend / Resume Awareness — `CANDIDATE`; [25] Safe Process Termination — `CANDIDATE`; [26] Process Restart Workflow — `CANDIDATE`; [27] Critical Process Safeguard — `CANDIDATE`; [34] Protected / Restricted Process Awareness — `CANDIDATE`; [42] Process Action Dry-Run / Impact Preview — `CANDIDATE`

### Objetivo
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

### Problema que resolve
Evita decisões baseadas em suposição sobre **Process action workflow & safeguards**, modelando estado real, contexto, limitações e falhas antes de qualquer recomendação ou ação.

### Benefício esperado
Diagnostic / Quality of Life

## 5. Aplicabilidade

### Quando se aplica?
quando uma configuração ou recomendação puder ser confundida com otimização universal.

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
- Stable process identity (`PID + creation time`) and image/user/session context.
- `IsProcessCritical` for a first-party critical-process safeguard where supported.
- Restart Manager `RM_PROCESS_INFO` (`ApplicationType`, `bRestartable`, service name/session) when a controlled shutdown/restart workflow is being considered.
- Access/protection state: inability to obtain required rights is an explicit `Restricted/AccessDenied` result.
- Unsaved-state risk cannot be inferred generically; require user confirmation for destructive termination.
- Suspend/resume capability is **awareness only** in the production contract unless a supported public process-level API is established later.

### Como detectar?
Use Win32 process APIs and Restart Manager only. Revalidate the unique process instance immediately before action. Never identify a target solely by executable name.

### Fonte da verdade
`IsProcessCritical` and Restart Manager are authoritative for the safeguards they explicitly expose. `bRestartable = false` means the app must not promise Restart Manager recovery. Access denial/protected-process behavior is preserved rather than bypassed.

### Estados possíveis
- Action eligible
- Critical / blocked
- Restricted / access denied
- Restart Manager restartable
- Not restartable / restart unsupported
- Target exited / identity changed
- Unsupported suspend-resume
- Unknown / Error

## 8. Estado alvo
Only an explicitly selected, supported change for **Process action workflow & safeguards**, built from current state and context, reaches its declared post-condition; otherwise no change is performed.

## 9. Implementação técnica

### Método principal
Define each process action separately instead of a generic "process control" mutation.

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
- `IsProcessCritical`
- `OpenProcess` with minimum required rights
- `TerminateProcess` only for an explicit destructive user action after safeguards
- Restart Manager: `RmStartSession`, `RmRegisterResources`, `RmGetList`, and only where appropriate `RmShutdown`/`RmRestart`
- `RM_PROCESS_INFO.bRestartable` and `RM_APP_TYPE`

Explicit exclusions:
- no `NtSuspendProcess`/`NtResumeProcess` production dependency;
- no generic "kill and reconstruct command line" restart claim;
- no termination of critical processes;
- no attempt to bypass protected-process/access controls.

### Alternativas avaliadas
- Undocumented NT suspend/resume: rejected as production contract.
- Recreating arbitrary apps from image path/command line: rejected as exact restart/rollback because app state, elevation, package activation and working directory may differ.
- Restart Manager: selected only for resources/apps where its own contract reports restartability.

### Abordagem escolhida
Safe explicit termination plus conditional Restart Manager orchestration; unsupported operations remain visible rather than emulated unsafely.

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
- [x] Reinício de processo (only when the user explicitly selects a supported restart workflow)
- [ ] Reinício de serviço
- [ ] Logoff
- [ ] Reboot do Windows
- [ ] Desconhecido

### A alteração só pode ser validada depois da reinicialização?
No OS reboot. A restart action is verified by observing termination of the original process identity and, when Restart Manager promises restartability, a new expected application instance.

### Rollback também exige reinicialização?
N/A for termination; terminating a process is not reversible. A restart is the intended action, not rollback.

## 12. Change Plan

```text
Feature: C-PROCESSES-007
Current state: stable process identity + critical/restricted/restartability state
Target state: one explicit supported action
Changes:
1. Revalidate PID + creation time.
2. Block critical/restricted/unsupported target.
3. Explain destructive consequences and restartability.
4. Execute only explicit user-selected terminate OR supported Restart Manager workflow.
5. Verify original process exit and, if promised, restart result.
Admin required: Conditional
Restart required: Process only, conditional
Risk: High
Reversible: No for termination; restart is conditional and not equivalent to rollback
```

Suspend/resume never appears in the ChangePlan under the current supported contract.

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
Yes for audit/safety context, but it does **not** make process termination reversible.

### O que precisa ser salvo antes da alteração?
- PID + creation time;
- image/session/user metadata needed to explain the target;
- critical/restricted state;
- Restart Manager classification and `bRestartable` when used;
- action chosen and confirmation context.

### Estado inexistente também deve ser registrado
N/A. If the process exits before Apply, the plan becomes stale and must be rebuilt.

## 15. Apply

### Sequência de execução
1. Revalidate unique process identity.
2. Query critical/restricted/restartability safeguards again.
3. Require explicit user confirmation for terminate/restart.
4. For termination, use `TerminateProcess` only after guard success.
5. For Restart Manager, operate only on the registered resource/session and only when the API contract reports the app/service as eligible; never promise restart for `bRestartable = false`.
6. Verify the original instance is gone and any promised restart occurred.

### Atomicidade
Termination is intrinsically destructive and cannot be transactionally undone. Restart Manager may affect multiple resource owners; its full affected set must be shown before execution. Partial restart is reported explicitly rather than hidden.

## 16. Verify

### Como comprovar que funcionou?
Reexecutar a mesma detecção autoritativa e comparar estado real com o alvo/critério definido; exit code ou ausência de exceção nunca bastam sozinhos.

### Critério de sucesso
Only an explicitly selected, supported change for **Process action workflow & safeguards**, built from current state and context, reaches its declared post-condition; otherwise no change is performed.

### Resultado parcial
Yes — quando apenas parte independente do estado puder ser lida/validada. Nunca promover `Partial` silenciosamente a sucesso total.

## 17. Rollback

### É reversível?
No for process termination. Conditional restart is a forward recovery action, not restoration of the terminated process's in-memory state.

### Método de rollback
N/A. Do not claim that relaunching an executable restores the original process state.

### O rollback restaura:
N/A.

### Ordem de reversão
N/A.

## 18. Verify Rollback

### Critério de sucesso
Detect pós-rollback é semanticamente equivalente ao snapshot original para todos os campos sob ownership da operação.

### O que fazer se rollback falhar?
Registrar `ROLLBACK_FAILED`, preservar snapshot/audit, bloquear repetição automática potencialmente destrutiva e fornecer retry/ação manual/System Restore somente se tecnicamente apropriado.

## 19. System Restore

### A feature exige ponto de restauração?
Not Required

### Motivo
System Restore cannot restore a terminated process's volatile in-memory/application state and is not an appropriate safeguard for this action.

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
Local: `/prototypes/processes/c-processes-007/` — **TBD / ainda não executado nesta fase documental**.

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
- Documented behavior — https://learn.microsoft.com/windows/win32/api/processthreadsapi/nf-processthreadsapi-isprocesscritical
- Documented behavior — https://learn.microsoft.com/windows/win32/api/processthreadsapi/nf-processthreadsapi-terminateprocess
- Documented behavior — https://learn.microsoft.com/windows/win32/api/restartmanager/
- Documented behavior — https://learn.microsoft.com/windows/win32/api/restartmanager/ns-restartmanager-rm_process_info
- Documented behavior — https://learn.microsoft.com/windows/win32/rstmgr/critical-system-services

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

- Execute Windows tests for critical-process blocking, access denied/protected targets, ordinary termination, target exit between plan/apply, and Restart Manager `bRestartable` true/false cases before `PROVEN`.
- Validate UX/confirmation requirements for destructive termination and multi-process Restart Manager impact.
- Suspend/resume remains intentionally unsupported for Apply until a stable public Windows process-level contract exists; do not substitute undocumented NT APIs.
- Confirm packaged/UWP/application-lifecycle edge cases and service ownership before enabling restart beyond tested classes.

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
