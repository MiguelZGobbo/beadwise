# Feature Specification — C-RAM-005 — Pagefile & crash-dump configuration diagnostics

## 3. Identificação

Nome: Pagefile & crash-dump configuration diagnostics
ID: C-RAM-005
Tipo: Diagnostic, Diagnostic / Recommendation, Recommendation
Technical Domain: RAM
Primary Product Area: TBD
Also Used By: My PC, Monitoring, Optimization, Benchmark, Gaming
Shared Capability: No
Final UI Placement: TBD
Status: BLOCKED
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Capability de produto para avaliar e, quando justificado, permitir configurar **pagefile & crash-dump configuration diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

**Base conceitual do Discovery:** [35] Pagefile Presence Detection — `CANDIDATE`; [36] Pagefile Configuration Detection — `CANDIDATE`; [37] Pagefile Requirement Analysis — `CANDIDATE`; [38] System-Managed Pagefile Recommendation — `CANDIDATE`; [39] Disabled Pagefile Risk Detection — `CANDIDATE`; [40] Pagefile Placement Analysis — `RESEARCHING`; [41] Crash Dump Memory Requirement Analysis — `CANDIDATE`; [122] ClearPageFileAtShutdown Detection — `CANDIDATE`

### Objetivo
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

### Problema que resolve
Evita decisões baseadas em suposição sobre **Pagefile & crash-dump configuration diagnostics**, modelando estado real, contexto, limitações e falhas antes de qualquer recomendação ou ação.

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
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

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
- Whether Windows is using automatic/system-managed pagefile policy (`Win32_ComputerSystem.AutomaticManagedPagefile`).
- Configured pagefile definitions (`Win32_PageFileSetting`) and runtime usage (`Win32_PageFileUsage`), kept distinct because configured state can be deferred until startup.
- Crash-dump configuration through `Win32_OSRecoveryConfiguration` (dump type/path/log/reboot flags).
- Free space on the dump/pagefile volume and whether the chosen dump configuration can be satisfied.

### Como detectar?
Read the supported CIM/WMI classes and normalize configured-vs-runtime state separately. Never infer pagefile sizing from RAM capacity alone.

### Fonte da verdade
The relevant CIMWin32 classes are primary for Windows configuration; runtime pagefile usage is not substituted for the configured setting. Microsoft dump guidance is used for recommendation context.

### Estados possíveis
- System managed
- Custom configured / active
- Custom configured / pending restart
- No pagefile / constrained dump capability
- Dump configured / storage insufficient
- Partial / Unknown / Error

## 8. Estado alvo
Only an explicitly selected, supported change for **Pagefile & crash-dump configuration diagnostics**, built from current state and context, reaches its declared post-condition; otherwise no change is performed.

## 9. Implementação técnica

### Método principal
Treat pagefile and crash-dump settings as two related but separately mutable objects. Recommendations favor system-managed/automatic dump behavior unless a concrete diagnostic requirement justifies a custom configuration.

### Tecnologias utilizadas
- [x] .NET API
- [ ] Win32
- [ ] Registry
- [ ] PowerShell
- [ ] CMD / executable
- [x] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- `Win32_ComputerSystem.AutomaticManagedPagefile`
- `Win32_PageFileSetting`
- `Win32_PageFileUsage`
- `Win32_OSRecoveryConfiguration`

### Alternativas avaliadas
- Writing `Memory Management`/`CrashControl` registry values directly: avoid when the supported WMI configuration class represents the same setting.
- Hard-coded pagefile sizes based on RAM: rejected as magic values.
- Disabling the pagefile as an optimization: rejected; it can reduce commit headroom and break dump requirements.

### Abordagem escolhida
Use supported Windows configuration classes, preserve exact prior state, and treat Microsoft dump requirements as constraints rather than a universal sizing formula.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Somente no momento em que a operação específica exigir acesso elevado; Detect deve degradar para dados parciais quando possível antes de solicitar elevação.

### O que acontece se não houver permissão?
Retornar erro estruturado (`ADMIN_REQUIRED`/`ACCESS_DENIED`) por operação/campo e preservar os dados de leitura que puderem ser obtidos com segurança.

## 11. Reinicialização

### Requer:
- [ ] Nada
- [ ] Reinício do aplicativo
- [ ] Reinício de processo
- [ ] Reinício de serviço
- [ ] Logoff
- [x] Reboot do Windows (for pagefile configuration changes whose WMI state is deferred until startup)
- [ ] Desconhecido

### A alteração só pode ser validada depois da reinicialização?
Pagefile configuration: two-phase verify. First confirm the configured WMI object, then after reboot confirm runtime `Win32_PageFileUsage`/effective state. Crash-dump configuration fields can generally be read back immediately but are only behaviorally proven by a controlled crash test, which is not required for ordinary product verification.

### Rollback também exige reinicialização?
Yes when rollback changes pagefile configuration. Crash-dump-only rollback is read-back verifiable immediately.

## 12. Change Plan

```text
Feature: C-RAM-005
Current state: automatic-managed flag + exact PageFileSetting instances + OSRecoveryConfiguration
Target state: explicit user-selected supported pagefile/dump configuration
Changes:
1. Re-read current state and free-space prerequisites.
2. Snapshot automatic-managed flag, every pagefile setting object, and relevant OSRecoveryConfiguration fields.
3. Apply only the selected configuration object(s).
4. Read back configured state.
5. If pagefile changes require reboot, persist snapshot/plan and mark VerifyPendingReboot.
6. After reboot, verify effective pagefile state; rollback from snapshot on failure/user request.
Admin required: Yes
Restart required: Conditional (pagefile)
Risk: Medium
Reversible: Fully at configuration level; runtime rollback may require reboot
```

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
- `AutomaticManagedPagefile` exact value;
- complete set of `Win32_PageFileSetting` instances including paths and min/max sizes;
- relevant `Win32_OSRecoveryConfiguration` fields and paths;
- free-space/prerequisite assessment;
- whether a setting object was absent.

### Estado inexistente também deve ser registrado
Yes. Rollback recreates only objects that existed originally and deletes objects created solely by Apply.

## 15. Apply

### Sequência de execução
1. Validate admin rights and target volume availability.
2. Re-detect and snapshot exact configuration.
3. Apply the minimum WMI configuration changes.
4. Read back configured state.
5. Mark reboot requirement for deferred pagefile changes.
6. Post-reboot, read runtime/effective state and compare to target.

### Atomicidade
Pagefile and dump changes are logically separate. If a combined plan fails, stop and restore already changed configuration objects from the snapshot. Never silently leave a custom pagefile size because a later dump-field change failed.

## 16. Verify

### Como comprovar que funcionou?
Reexecutar a mesma detecção autoritativa e comparar estado real com o alvo/critério definido; exit code ou ausência de exceção nunca bastam sozinhos.

### Critério de sucesso
Only an explicitly selected, supported change for **Pagefile & crash-dump configuration diagnostics**, built from current state and context, reaches its declared post-condition; otherwise no change is performed.

### Resultado parcial
Yes — quando apenas parte independente do estado puder ser lida/validada. Nunca promover `Partial` silenciosamente a sucesso total.

## 17. Rollback

### É reversível?
Fully at configuration level; effective pagefile restoration can require reboot.

### Método de rollback
Restore the exact automatic-managed flag, pagefile-setting object set, and crash-dump fields captured in the snapshot; remove objects that did not previously exist.

### O rollback restaura:
`estado original capturado`.

### Ordem de reversão
Restore crash-dump fields and pagefile objects/automatic policy according to dependencies; then reboot if the restored pagefile policy is deferred.

## 18. Verify Rollback

### Critério de sucesso
Detect pós-rollback é semanticamente equivalente ao snapshot original para todos os campos sob ownership da operação.

### O que fazer se rollback falhar?
Registrar `ROLLBACK_FAILED`, preservar snapshot/audit, bloquear repetição automática potencialmente destrutiva e fornecer retry/ação manual/System Restore somente se tecnicamente apropriado.

## 19. System Restore

### A feature exige ponto de restauração?
Recommended only for broader repair sessions that combine this change with other risky system changes; not required for this configuration alone.

### Motivo
The settings have explicit WMI-readable snapshots and deterministic configuration rollback. System Restore does not replace that rollback.

## 20. Risco

### Classificação
Medium

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
GlobalMemoryStatusEx, GetPerformanceInfo, GetProcessMemoryInfo, Performance Counters / PDH, GetLogicalProcessorInformationEx, Win32_PhysicalMemory / Win32_PageFileSetting

### Componentes do Windows
Componentes do subsistema RAM e infraestrutura comum de Event Log/ETW/CIM quando aplicável.

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
Local: `/prototypes/ram/c-ram-005/` — **TBD / ainda não executado nesta fase documental**.

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

- Documented behavior — https://learn.microsoft.com/windows/win32/memory/memory-performance-information
- Documented behavior — https://learn.microsoft.com/windows-server/administration/performance-tuning/subsystem/cache-memory-management/troubleshoot
- Documented behavior — https://learn.microsoft.com/windows/win32/api/sysinfoapi/nf-sysinfoapi-getlogicalprocessorinformationex
- Documented behavior — https://learn.microsoft.com/windows/win32/memory/large-page-support

**Observed behavior (campanha 2026-09-08):** probes compartilhados read-only executados neste host: `ram.inventory-and-pressure`=PASS. Evidência: `/prototypes/system/C-SYSTEM-017/results/domain-evidence.json`. Estes sinais são parciais e não satisfazem, sozinhos, o gate DETECT completo desta feature.
- Documented behavior — https://learn.microsoft.com/windows/win32/cimwin32prov/win32-pagefilesetting
- Documented behavior — https://learn.microsoft.com/windows/win32/cimwin32prov/win32-pagefileusage
- Documented behavior — https://learn.microsoft.com/windows/win32/cimwin32prov/win32-osrecoveryconfiguration
- Operational guidance — https://learn.microsoft.com/troubleshoot/windows-server/performance/troubleshoot-stop-errors-best-practices-dump-configuration-recommendations

<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — configuração versus runtime e reboot/rollback.
- Evidência realmente executada: E:ram.inventory-and-pressure PASS (uso atual somente).
- Execução segura neste host / teste: Parcial — leitura de `Win32_PageFileSetting`, usage e crash-dump config.
- Impedimento ou limitação restante: Apply automático/custom, reboot e rollback exigem admin e não devem alterar o pagefile deste host.
- Disposição: `BLOCKED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
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

- Prototype automatic-managed → custom → rollback and custom → automatic-managed on Windows 11, including configured-vs-runtime behavior across reboot.
- Exercise crash-dump types/paths and insufficient-space cases without using destructive crash tests as a routine product test.
- Confirm exact WMI write permissions/error codes on supported Windows editions.
- Recommendations must remain constraint-based (dump requirement, commit pressure, storage availability); no fixed pagefile-size magic values.

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
