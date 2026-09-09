# Feature Specification — C-RAM-008 — Standby/cache/compression diagnostics

## 3. Identificação

Nome: Standby/cache/compression diagnostics
ID: C-RAM-008
Tipo: Advanced Diagnostic, Configuration, Diagnostic, Optimization, Product Behavior
Technical Domain: RAM
Primary Product Area: TBD
Also Used By: My PC, Monitoring, Optimization, Benchmark, Gaming
Shared Capability: No
Final UI Placement: TBD
Status: RESEARCH
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Capability de produto para diagnosticar **standby/cache/compression diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

**Base conceitual do Discovery:** [49] Standby Memory Observation — `CANDIDATE`; [50] Standby Cache Explanation — `CANDIDATE`; [51] Standby List Clear — `QUESTIONABLE`; [53] Memory Compression State Detection — `CANDIDATE`; [54] Memory Compression Impact Analysis — `RESEARCHING`; [55] Disable Memory Compression — `QUESTIONABLE`; [61] System Cache Size Observation — `CANDIDATE`; [62] Modified Page List Observation — `IDEA`; Limite conceitual:; O `READY_FOR_SPEC` vale para a responsabilidade principal descrita nesta capability. Subitens de origem marcados `QUESTIONABLE` continuam questionáveis e **não** são aprovados implicitamente; qualquer alteração agressiva neles representada permanece fora do escopo ativo até justificativa própria.

### Objetivo
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

### Problema que resolve
Evita decisões baseadas em suposição sobre **Standby/cache/compression diagnostics**, modelando estado real, contexto, limitações e falhas antes de qualquer recomendação ou ação.

### Benefício esperado
Diagnostic / Quality of Life

## 5. Aplicabilidade

### Quando se aplica?
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

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
- physical and usable memory through Win32/Win32_ComputerSystem plus GlobalMemoryStatusEx/GetPerformanceInfo
- module/topology metadata through SMBIOS-backed CIM classes where exposed
- commit, working set, paging and pool state through documented performance counters/APIs
- per-process memory through PROCESS_MEMORY_COUNTERS_EX
- NUMA topology through GetLogicalProcessorInformationEx
- crash dump/pagefile configuration through documented system configuration surfaces
- N/A

### Como detectar?
Coletar os sinais documentados acima, normalizar por identidade estável do objeto relevante, anexar `source/provenance`, timestamp e confidence, e correlacionar somente sinais temporal/semanticamente compatíveis.

### Fonte da verdade
Primary truth is the documented Windows/API state closest to the subsystem being modeled for Standby/cache/compression diagnostics; secondary sources may enrich context but must carry provenance/confidence. Vendor data is authoritative only for vendor-defined telemetry on explicitly supported hardware.

### Estados possíveis
- Supported / normal
- Supported / attention candidate
- Supported / problem confirmed
- Managed/policy-controlled
- Partial data
- Not Applicable / Unsupported
- Unknown / Error

## 8. Estado alvo

A read-only assessment of memory compression/cache/standby behavior that explains pressure and avoids treating cached/standby memory as inherently wasted. No standby-list purge or memory-compression toggle is applied as an optimization.

## 9. Implementação técnica

### Método principal
Use documented memory performance information/counters for pressure and cache context. Where available, `Get-MMAgent` may expose MemoryCompression configuration; it is contextual state, not an automatic optimization target.

### Tecnologias utilizadas
- [x] .NET API
- [x] Win32
- [ ] Registry
- [x] PowerShell (structured cmdlet result only where needed)
- [ ] CMD / executable
- [x] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- `GlobalMemoryStatusEx` / `GetPerformanceInfo`
- Memory performance counters
- `Get-MMAgent` for MemoryCompression configuration when available

### Alternativas avaliadas
- Purging standby lists (`EmptyStandbyList`, private memory-manager commands, RAM cleaners): rejected as optimization behavior; reclaimable standby cache is part of Windows memory management.
- Disabling MemoryCompression to "free CPU": not recommended generically; benefit is workload-dependent and Windows manages the feature.
- Repeated cache clearing before benchmarks: allowed only as an explicitly controlled experimental methodology if the benchmark spec requires it, never as normal optimization.

### Abordagem escolhida
Diagnose pressure and explain cache/compression rather than fight the memory manager.

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
- [ ] Reboot do Windows
- [x] Desconhecido

### A alteração só pode ser validada depois da reinicialização?
Conditional — deve ser resolvido por operação concreta; não assumir reboot se a API permitir verificação imediata.

### Rollback também exige reinicialização?
Conditional — igual ao mecanismo revertido; registrar no ChangePlan.

## 12. Change Plan

```text
Feature: C-RAM-008
Current state: normalized Detect result + provenance
Target state: Only an explicitly selected, supported change for **Standby/cache/compression diagnostics**, built from current state and context, reaches its declared post-condition; otherwise no change is performed.
Changes:
1. Re-run Detect immediately before execution.
2. Validate support/managed state/prerequisites.
3. Capture exact snapshot.
4. Execute only the user-selected supported operation.
5. Re-detect and compare actual vs target.
6. On partial failure, stop or rollback already-applied dependent changes according to the operation graph.
Admin required: Conditional
Restart required: Desconhecido
Risk: Medium
Reversible: Unknown
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
Estado efetivo completo dos objetos/propriedades que serão alterados; origem, tipo, existência/ausência, owner/policy, timestamp e qualquer relação necessária para restauração.

### Estado inexistente também deve ser registrado
Yes — ausência é parte do snapshot e rollback deve restaurar ausência quando esse era o estado original.

## 15. Apply

### Sequência de execução
N/A — diagnostic/safeguard only. The BeadWise feature does not purge standby lists or toggle MemoryCompression as an optimization.

### Atomicidade
N/A.

## 16. Verify

### Como comprovar que funcionou?
Reexecutar a mesma detecção autoritativa e comparar estado real com o alvo/critério definido; exit code ou ausência de exceção nunca bastam sozinhos.

### Critério de sucesso
Only an explicitly selected, supported change for **Standby/cache/compression diagnostics**, built from current state and context, reaches its declared post-condition; otherwise no change is performed.

### Resultado parcial
Yes — quando apenas parte independente do estado puder ser lida/validada. Nunca promover `Partial` silenciosamente a sucesso total.

## 17. Rollback

### É reversível?
N/A

### Método de rollback
N/A — no persistent change.

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
Read-only diagnostics.

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
Local: `/prototypes/ram/c-ram-008/` — **TBD / ainda não executado nesta fase documental**.

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

**Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.
- Documented behavior — https://learn.microsoft.com/powershell/module/mmagent/get-mmagent
- Documented behavior — https://learn.microsoft.com/powershell/module/mmagent/enable-mmagent

<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — estado de compression/cache e counter mapping.
- Evidência realmente executada: NENHUMA.
- Execução segura neste host / teste: Sim — `Get-MMAgent`/counters read-only e registrar `Unsupported`.
- Impedimento ou limitação restante: A própria spec não fixa fonte efetiva/correlação com Task Manager; não usar registry privado.
- Disposição: `RESEARCH`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

<!-- PHASE2-FINAL-RESEARCH-AUDIT:START -->
**Reavaliação final de RESEARCH (2026-09-08):**
- O status `RESEARCH` foi revisto e preservado porque ainda há incerteza técnica solucionável descrita nas seções 30, 31 e 36; documentação ou probe compartilhado parcial não foi convertido em `PASS`.
- Nenhuma prova prática isolada nesta máquina elimina essa incerteza sem antes definir fonte, contrato, fixture, hardware ou dependência indicada pela própria spec.
- Próximo gate: concluir a investigação registrada, então decidir se cabe prototype seguro, `SPECIFIED`, `BLOCKED`, `DEFERRED` ou `REJECTED`; não promover diretamente a `PROVEN`/`APPROVED`.
<!-- PHASE2-FINAL-RESEARCH-AUDIT:END -->

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

- Validate the chosen pressure/cache counters on Windows 11 and compare with Task Manager/Performance Monitor before `PROVEN`.
- Treat `Get-MMAgent` availability/version differences as `Partial/Unsupported` rather than falling back to private registry state.
- Keep standby-list purging and universal memory-compression toggles outside Apply unless a future separate experiment demonstrates a narrow, user-meaningful use case.

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
