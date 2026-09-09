# Feature Specification — C-CPU-002 — CPU benchmark suite

## 3. Identificação

Nome: CPU benchmark suite
ID: C-CPU-002
Tipo: Benchmark, Benchmark / Diagnostic, Benchmark / Validation, Benchmark Safeguard, Product Behavior
Technical Domain: CPU
Primary Product Area: TBD
Also Used By: My PC, Monitoring, Optimization, Benchmark, Gaming
Shared Capability: No
Final UI Placement: TBD
Status: SPECIFIED
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Capability de produto para medir e comparar **cpu benchmark suite**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

**Base conceitual do Discovery:** [5] SMT Performance Comparison — `IDEA`; [34] Single-Thread Benchmark — `CANDIDATE`; [35] Multi-Thread Benchmark — `CANDIDATE`; [60] CPU Benchmark Stability — `CANDIDATE`; [68] CPU Performance Before × After — `CANDIDATE`; [132] CPU Benchmark Warm-Up — `CANDIDATE`; [133] CPU Benchmark Cooldown Control — `CANDIDATE`; [134] CPU Test Repetition / Variance — `CANDIDATE`; [135] Minimum Meaningful CPU Improvement — `IDEA`; [138] CPU Optimization Multi-Metric Result — `CANDIDATE`

### Objetivo
Fornecer uma medição reproduzível e interpretável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

### Problema que resolve
Evita decisões baseadas em suposição sobre **CPU benchmark suite**, modelando estado real, contexto, limitações e falhas antes de qualquer recomendação ou ação.

### Benefício esperado
Diagnostic / Quality of Life

## 5. Aplicabilidade

### Quando se aplica?
quando for necessário quantificar desempenho, regressão ou resultado de uma alteração.

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
No

### Critérios de recomendação
N/A — a capability é primariamente diagnóstica; ela fornece evidência/contexto para consumidores.

### Motivo apresentado ao usuário
Explicar o estado observado, a fonte e as limitações sem transformar diagnóstico em recomendação automática.

### Grau de confiança
High somente para estados diretamente retornados por fonte documentada e coerente; Medium/Low para correlação/inferência parcial; Not Applicable quando fora de suporte.

## 7. Estado atual

### O que precisa ser detectado?
- processor topology through GetLogicalProcessorInformationEx
- identity/capabilities from documented OS/system interfaces plus CPUID only where architecture-specific behavior is explicitly modeled
- CPU utilization through system/per-processor performance counters/ETW
- clock/power/thermal limit interpretation only from supported OS/vendor telemetry
- WHEA for machine-check/hardware error evidence
- Use repeatable workload profiles, warm-up, run metadata and before/after comparability; never infer user benefit from a single run.

### Como detectar?
Coletar os sinais documentados acima, normalizar por identidade estável do objeto relevante, anexar `source/provenance`, timestamp e confidence, e correlacionar somente sinais temporal/semanticamente compatíveis.

### Fonte da verdade
Primary truth is the documented Windows/API state closest to the subsystem being modeled for CPU benchmark suite; secondary sources may enrich context but must carry provenance/confidence. Vendor data is authoritative only for vendor-defined telemetry on explicitly supported hardware.

### Estados possíveis
- Supported / normal
- Supported / attention candidate
- Supported / problem confirmed
- Managed/policy-controlled
- Partial data
- Not Applicable / Unsupported
- Unknown / Error

## 8. Estado alvo
A normalized, provenance-aware assessment of **CPU benchmark suite** with explicit Supported/Partial/Unknown/Not Applicable states and no persistent system change.

## 9. Implementação técnica

### Método principal
Usar as interfaces documentadas do subsistema e manter detecção, interpretação e alteração separadas. PowerShell/CLI pode ser usado em protótipo ou como backend suportado quando for a interface documentada mais adequada, mas parsing textual localizado não deve ser a única fonte se existir API estruturada.

### Tecnologias utilizadas
- [x] .NET API
- [x] Win32
- [ ] Registry
- [ ] PowerShell
- [ ] CMD / executable
- [x] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- GetLogicalProcessorInformationEx
- GetSystemTimes / PDH Processor Information counters
- ETW/WPR
- Power APIs for processor power policy context
- WHEA Event Log
- vendor telemetry adapter conditional

### Alternativas avaliadas
- Registry/CLI não documentado: rejeitado como fonte principal quando API suportada existe.
- Ferramenta de terceiros/vendor: somente complemento quando expõe dado que o Windows não oferece e com adapter explícito de compatibilidade.
- Inferência por nome/default: rejeitada como prova técnica.

### Abordagem escolhida
Prioriza superfícies documentadas, estado efetivo e provenance. Isto reduz dependência de tweak myths e permite distinguir `Unsupported/Unknown` de configuração problemática.

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
No — não há alteração persistente.

### Rollback também exige reinicialização?
No.

## 12. Change Plan

```text
Feature: C-CPU-002
Current state: normalized Detect result + provenance
Target state: A normalized, provenance-aware assessment of **CPU benchmark suite** with explicit Supported/Partial/Unknown/Not Applicable states and no persistent system change.
Changes:
N/A — read-only capability; ChangePlan contains `Changes = []`.
Admin required: Conditional
Restart required: Nada
Risk: Medium
Reversible: N/A
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
No

### O que precisa ser salvo antes da alteração?
N/A — nenhuma alteração persistente.

### Estado inexistente também deve ser registrado
N/A.

## 15. Apply

### Sequência de execução
N/A — this capability does not persistently modify the system.

### Atomicidade
N/A.

## 16. Verify

### Como comprovar que funcionou?
Reexecutar a mesma detecção autoritativa e comparar estado real com o alvo/critério definido; exit code ou ausência de exceção nunca bastam sozinhos.

### Critério de sucesso
A normalized, provenance-aware assessment of **CPU benchmark suite** with explicit Supported/Partial/Unknown/Not Applicable states and no persistent system change.

### Resultado parcial
Yes — quando apenas parte independente do estado puder ser lida/validada. Nunca promover `Partial` silenciosamente a sucesso total.

## 17. Rollback

### É reversível?
N/A

### Método de rollback
N/A — no persistent change.

### O rollback restaura:
`estado original capturado`, não valor default presumido.

### Ordem de reversão
Ordem inversa para mudanças dependentes quando tecnicamente apropriado; dependências externas devem ser respeitadas.

## 18. Verify Rollback

### Critério de sucesso
Detect pós-rollback é semanticamente equivalente ao snapshot original para todos os campos sob ownership da operação.

### O que fazer se rollback falhar?
Registrar `ROLLBACK_FAILED`, preservar snapshot/audit, bloquear repetição automática potencialmente destrutiva e fornecer retry/ação manual/System Restore somente se tecnicamente apropriado.

## 19. System Restore

### A feature exige ponto de restauração?
Not Required

### Motivo
Diagnostics não exigem restore point. Para mudanças, System Restore só pode ser camada adicional quando risco/escopo justificarem; não substitui snapshot/rollback determinístico.

## 20. Risco

### Classificação
Medium

### Possíveis efeitos negativos
- Misclassification when a source exposes incomplete/virtualized state.
- False confidence if one signal is treated as definitive without provenance/context.

### Pior cenário plausível
Diagnóstico incorreto levar consumidor/usuário a uma decisão ruim; a capability em si não altera o sistema.

### Recuperação manual possível?
N/A

### Procedimento manual
N/A.

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
GetLogicalProcessorInformationEx, GetSystemTimes / PDH Processor Information counters, ETW/WPR, Power APIs for processor power policy context, WHEA Event Log, vendor telemetry adapter conditional

### Componentes do Windows
Componentes do subsistema CPU e infraestrutura comum de Event Log/ETW/CIM quando aplicável.

### Drivers
Conditional — necessários apenas quando a fonte/ação depende de dispositivo ou vendor.

### Internet
Conditional — somente para catálogo/lifecycle/vendor/update externo explicitamente previsto; detecção local não deve depender da Internet sem necessidade.

### Aplicações externas
N/A por padrão; ferramentas vendor/terceiros só entram como dependência explícita de prova/escalation.

## 23. Conflitos
Pode conflitar com política corporativa/MDM, software de fabricante, Windows Update, antivírus/security tooling, tuning software e personalizações do usuário. Antes de alterar, Detect deve identificar ownership/policy quando disponível; estado gerenciado não deve ser sobrescrito silenciosamente.

## 24. Idempotência

Yes — leituras repetidas não alteram estado; diferenças devem refletir mudança real/tempo de coleta.

## 25. Falhas possíveis
- `ADMIN_REQUIRED`
- `ACCESS_DENIED`
- `UNSUPPORTED_WINDOWS_VERSION`
- `HARDWARE_UNSUPPORTED`
- `SETTING_NOT_FOUND` / `SOURCE_UNAVAILABLE`
- `POLICY_BLOCKED`
- `PARTIAL_DATA`
- `DETECTION_FAILED`


## 26. Logging
Registrar: Detection started/completed, source/provenance, normalized current state, confidence, plan generated, policy/compatibility guards, admin/restart requirements, e — para mutações — snapshot/apply/verify/rollback lifecycle. Não registrar conteúdo pessoal/sensível além do mínimo técnico necessário.

## 27. Persistência

### O estado precisa sobreviver ao fechamento do app?
Yes para histórico/audit quando a capability participar de correlação, before/after ou mudança; caso contrário somente cache efêmero.

### O snapshot precisa sobreviver reboot?
No.

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
N/A.

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

- [ ] Execução repetida
- [ ] Estado gerenciado por policy/vendor quando aplicável

## 30. Prova técnica

### Script/protótipo
Local: `/prototypes/cpu/c-cpu-002/` — **TBD / ainda não executado nesta fase documental**.

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

A spec não deve receber `PROVEN` antes dessa prova quando os itens forem aplicáveis.

## 31. Evidências

- Documented behavior — https://learn.microsoft.com/windows/win32/api/sysinfoapi/nf-sysinfoapi-getlogicalprocessorinformationex
- Documented behavior — https://learn.microsoft.com/windows/win32/api/sysinfoapi/nf-sysinfoapi-getsystemtimes
- Documented behavior — https://learn.microsoft.com/windows-hardware/drivers/whea/whea-hardware-error-events
- Documented behavior — https://learn.microsoft.com/windows-hardware/customize/power-settings/configure-processor-power-management-options

**Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.
- https://learn.microsoft.com/windows/win32/sysinfo/acquiring-high-resolution-time-stamps
- https://learn.microsoft.com/windows/win32/procthread/processor-groups

<!-- PHASE2-SAFE-PROOF:START -->
**Prova prática segura executada (2026-09-08):** `C-CPU-002=PARTIAL` em `/prototypes/cpu/C-CPU-001/results/cpu-safe-capabilities.json`. Cinco repetições single/multi-thread do mesmo workload determinístico produziram checksum idêntico; a variância multi-thread observada no último run foi 0,404 e excedeu o limite de 15% do prototype, preservada como `PARTIAL`. Não houve stress térmico, normalização cross-hardware nem alegação de ganho.
<!-- PHASE2-SAFE-PROOF:END -->

<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — benchmark determinístico isolado, repetibilidade e salvaguardas de overhead/temperatura precisam de prova antes de PROVEN.
- Evidência realmente executada: Prototype seguro adicional executado: `C-CPU-002=PARTIAL` em `/prototypes/cpu/C-CPU-001/results/cpu-safe-capabilities.json`. Cinco repetições single/multi-thread do mesmo workload determinístico produziram checksum idêntico; a variância multi-thread observada no último run foi 0,404 e excedeu o limite de 15% do prototype, preservada como `PARTIAL`. Não houve stress térmico, normalização cross-hardware nem alegação de ganho. Evidência: `/prototypes/cpu/C-CPU-001/results/cpu-safe-capabilities.json`. A evidência anterior foi substituída por esta observação mais recente.
- Execução segura neste host / teste: Executado como workload curto e determinístico em CPU local, sem stress térmico prolongado nem tuning.
- Impedimento ou limitação restante: A prova cobre somente repetibilidade básica neste host; não há baseline cross-hardware, normalização térmica ou benchmark calibrado para generalização.
- Disposição: `SPECIFIED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

## 32. Benefício real
Reasonable

A classificação permanece condicionada ao contexto descrito pelo Discovery e, quando houver alegação de desempenho/latência/energia, à medição controlada.

## 33. Impacto mensurável

### Pode ser medido?
Partial

### Como?
Use before/after data produced by the same measurement path and workload, with session metadata and no fixed universal threshold unless separately justified.

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

- Metodologia definida para protótipo: single-thread e multi-thread workloads determinísticos, warm-up, cooldown/thermal-state check, repetição e distribuição de resultados; registrar CPU topology, power mode e background load.
- Before/after deve usar mesma build, workload e condições; resultado abaixo da variância não é melhoria comprovada.
- Não incorporar overclock/tuning ao benchmark.
- `PROVEN` só após implementação isolada e correlação/repetibilidade em hardware real.

## 37. Critério para PROVEN
- [ ] Detect validado contra fonte nativa/documentada
- [ ] ChangePlan representa exatamente as operações quando aplicável
- [ ] Dry-run confirmou zero mutação
- [ ] Apply/Snapshot/Rollback confirmados como N/A
- [ ] Compatibilidade mínima executada
- [ ] Falhas/Partial/Unsupported exercitados
- [ ] Nenhuma questão crítica da seção 36 permanece aberta

## 38. Critério para APPROVED
Somente após `PROVEN` e revisão de utilidade, confiabilidade, escopo, risco, manutenção, UX, não duplicação e ausência de dependência frágil injustificada. `READY_FOR_SPEC` do Discovery não implica `APPROVED`.
