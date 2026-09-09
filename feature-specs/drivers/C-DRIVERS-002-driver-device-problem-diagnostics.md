# Feature Specification — C-DRIVERS-002 — Driver/device problem diagnostics

## 3. Identificação

Nome: Driver/device problem diagnostics
ID: C-DRIVERS-002
Tipo: Diagnostic, Diagnostic / Manual Guidance
Technical Domain: DRIVERS
Primary Product Area: TBD
Also Used By: My PC, Diagnostics, Repair, Update
Shared Capability: No
Final UI Placement: TBD
Status: SPECIFIED
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Capability de produto para diagnosticar **driver/device problem diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

**Base conceitual do Discovery:** [6] Problem Device Diagnostics — `CANDIDATE`; [7] SetupAPI Installation Log Diagnostics — `CANDIDATE`; [18] Driver Conflict / Filter Stack Awareness — `CANDIDATE`; [20] Driver Crash / Bugcheck Correlation — `CANDIDATE`; [22] Unknown Device Resolution Guidance — `CANDIDATE`

### Objetivo
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

### Problema que resolve
Evita decisões baseadas em suposição sobre **Driver/device problem diagnostics**, modelando estado real, contexto, limitações e falhas antes de qualquer recomendação ou ação.

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
- Diagnostics
- Repair
- Update

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
- driver package/device binding through SetupAPI/CfgMgr32
- device problem codes and present state through Config Manager APIs
- driver store packages through PnPUtil/DISM-supported views
- update source/policy state before offering driver changes
- signed/provider/version metadata without assuming newest = best
- Driver version/provider/date are context, not sufficient evidence that a driver is good/bad; correlate device status and incidents.

### Como detectar?
Coletar os sinais documentados acima, normalizar por identidade estável do objeto relevante, anexar `source/provenance`, timestamp e confidence, e correlacionar somente sinais temporal/semanticamente compatíveis.

### Fonte da verdade
Primary truth is the documented Windows/API state closest to the subsystem being modeled for Driver/device problem diagnostics; secondary sources may enrich context but must carry provenance/confidence. Vendor data is authoritative only for vendor-defined telemetry on explicitly supported hardware.

### Estados possíveis
- Supported / normal
- Supported / attention candidate
- Supported / problem confirmed
- Managed/policy-controlled
- Partial data
- Not Applicable / Unsupported
- Unknown / Error

## 8. Estado alvo
A normalized, provenance-aware assessment of **Driver/device problem diagnostics** with explicit Supported/Partial/Unknown/Not Applicable states and no persistent system change.

## 9. Implementação técnica

### Método principal
Usar as interfaces documentadas do subsistema e manter detecção, interpretação e alteração separadas. PowerShell/CLI pode ser usado em protótipo ou como backend suportado quando for a interface documentada mais adequada, mas parsing textual localizado não deve ser a única fonte se existir API estruturada.

### Tecnologias utilizadas
- [x] .NET API
- [x] Win32
- [ ] Registry
- [ ] PowerShell
- [x] CMD / executable
- [x] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [x] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- SetupDi* APIs
- CM_Get_DevNode_Status / CfgMgr32
- PnPUtil
- DISM driver servicing
- Windows Update Agent/update policy surfaces where applicable

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
Feature: C-DRIVERS-002
Current state: normalized Detect result + provenance
Target state: A normalized, provenance-aware assessment of **Driver/device problem diagnostics** with explicit Supported/Partial/Unknown/Not Applicable states and no persistent system change.
Changes:
N/A — read-only capability; ChangePlan contains `Changes = []`.
Admin required: Conditional
Restart required: Nada
Risk: Low
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
A normalized, provenance-aware assessment of **Driver/device problem diagnostics** with explicit Supported/Partial/Unknown/Not Applicable states and no persistent system change.

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
Low

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
SetupDi* APIs, CM_Get_DevNode_Status / CfgMgr32, PnPUtil, DISM driver servicing, Windows Update Agent/update policy surfaces where applicable

### Componentes do Windows
Componentes do subsistema DRIVERS e infraestrutura comum de Event Log/ETW/CIM quando aplicável.

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
Optional

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
Local: /prototypes/drivers/C-DRIVERS-001/ — executado em hardware Windows real nesta auditoria.

### Resultado
```text
DETECT: PASS (healthy present-device baseline: 190 CM_PROB_NONE)
PLAN: PASS (read-only; Changes = [])
DRY-RUN: PASS (no device action)
APPLY: N/A
VERIFY: PASS (healthy count and provenance)
ROLLBACK: N/A
RESTORE VERIFY: N/A
REAL PROBLEM/ACCESS-DENIED FIXTURE: NOT_TESTED
```

### Ambiente utilizado
```text
Windows version: Windows 11 Pro 10.0.26200 build 26200 x64
Hardware: AMD Ryzen 7 5700; Radeon RX 570; desktop
Admin: No
Date: 2026-09-08
```

A spec não deve receber `PROVEN` antes dessa prova quando os itens forem aplicáveis.

## 31. Evidências
- Documented behavior — https://learn.microsoft.com/windows-hardware/drivers/install/setupapi
- Documented behavior — https://learn.microsoft.com/windows-hardware/drivers/devtest/pnputil-command-syntax
- Documented behavior — https://learn.microsoft.com/windows-hardware/manufacture/desktop/dism-driver-servicing-command-line-options-s14
- Documented behavior — https://learn.microsoft.com/windows/client-management/mdm/policy-csp-deviceinstallation

**Observed behavior (campanha inicial 2026-09-08):** probes compartilhados read-only executados neste host: `drivers.problem-devices.pnp`=PASS. Evidência: `/prototypes/system/C-SYSTEM-017/results/domain-evidence.json`. Estes sinais são parciais e não satisfazem, sozinhos, o gate DETECT completo desta feature. A prova segura adicional posterior, registrada abaixo, é a evidência mais recente para o slice explicitado.

<!-- PHASE2-SAFE-PROOF-2026-09-08:START -->
**Prova segura adicional observada (2026-09-08):** Consulta real encontrou 190 devices presentes com `CM_PROB_NONE` e 0 problemas. Regras sintéticas cobriram normalização `CM_PROB_DISABLED`/missing somente como fixture, não como ocorrência real. Nenhum device foi desabilitado para fabricar falha; problem/access-denied continuam `NOT_TESTED`.

Evidência: /prototypes/drivers/C-DRIVERS-001/results/.
<!-- PHASE2-SAFE-PROOF-2026-09-08:END -->
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Yes — problem-code diagnostics including healthy, problem and access-denied/unknown interpretation.
- Evidência realmente executada: Prototype seguro adicional executado: Consulta real encontrou 190 devices presentes com `CM_PROB_NONE` e 0 problemas. Regras sintéticas cobriram normalização `CM_PROB_DISABLED`/missing somente como fixture, não como ocorrência real. Nenhum device foi desabilitado para fabricar falha; problem/access-denied continuam `NOT_TESTED`. Evidência: `/prototypes/drivers/C-DRIVERS-001/results/`. A evidência anterior foi substituída por esta observação mais recente.
- Execução segura neste host / teste: Healthy read-only detection is safe; inducing a device problem is not. Teste recomendado: Correlate PnP problem codes, status and events; use an isolated/synthetic fixture for problem and inaccessible states; never disable a live device.
- Impedimento ou limitação restante: No safe problem-device fixture; current PASS cannot establish diagnosis behavior.
- Disposição: `SPECIFIED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

## 32. Benefício real
Reasonable

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
- Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?
- Confirm the minimum supported Windows build/edition for every API or property used before APPROVED.

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
