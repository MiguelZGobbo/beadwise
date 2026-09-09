# Feature Specification — C-DRIVERS-003 — Driver update & source awareness

## 3. Identificação

Nome: Driver update & source awareness
ID: C-DRIVERS-003
Tipo: Diagnostic, Diagnostic / Recommendation
Technical Domain: DRIVERS
Primary Product Area: TBD
Also Used By: My PC, Diagnostics, Repair, Update
Shared Capability: No
Final UI Placement: TBD
Status: BLOCKED
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Capability de produto para detectar e contextualizar **driver update & source awareness**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

**Base conceitual do Discovery:** [8] Driver Update Availability Awareness — `CANDIDATE`; [9] OEM vs Inbox Driver Context — `CANDIDATE`; [28] Windows Update Driver Source Correlation — `CANDIDATE`; [29] Firmware-via-Driver Package Awareness — `CANDIDATE`; [35] Driver Update Known-Issue Gate — `CANDIDATE`; [36] Driver Security Exposure Prioritization — `CANDIDATE`

### Objetivo
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

### Problema que resolve
Evita decisões baseadas em suposição sobre **Driver update & source awareness**, modelando estado real, contexto, limitações e falhas antes de qualquer recomendação ou ação.

### Benefício esperado
Diagnostic

## 5. Aplicabilidade

### Quando se aplica?
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

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
Conditional

### Critérios de recomendação
Somente quando Detect confirmar aplicabilidade, o estado não for meramente uma preferência/default diferente, a mudança tiver benefício contextual plausível, não estiver bloqueada por política/owner externo e o ChangePlan for verificável/reversível conforme esta spec.

### Motivo apresentado ao usuário
Explicar o estado observado, a evidência, o contexto que torna a mudança relevante e o que exatamente será alterado; nunca prometer ganho universal.

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
Primary truth is the documented Windows/API state closest to the subsystem being modeled for Driver update & source awareness; secondary sources may enrich context but must carry provenance/confidence. Vendor data is authoritative only for vendor-defined telemetry on explicitly supported hardware.

### Estados possíveis
- Supported / normal
- Supported / attention candidate
- Supported / problem confirmed
- Managed/policy-controlled
- Partial data
- Not Applicable / Unsupported
- Unknown / Error

## 8. Estado alvo
Only an explicitly selected, supported change for **Driver update & source awareness**, built from current state and context, reaches its declared post-condition; otherwise no change is performed.

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
Feature: C-DRIVERS-003
Current state: normalized Detect result + provenance
Target state: Only an explicitly selected, supported change for **Driver update & source awareness**, built from current state and context, reaches its declared post-condition; otherwise no change is performed.
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
1. Validate compatibility and managed state.
2. Re-detect current state.
3. Capture snapshot including absence/existence.
4. Apply the minimal documented change only.
5. Record return/result.
6. Re-detect.
7. If verify fails, enter rollback path when safe.

### Atomicidade
Operações dependentes formam uma unidade lógica: ao falhar, parar e reverter mudanças já aplicadas quando seguro. Mudanças independentes só podem continuar se o ChangePlan as marcar explicitamente como independentes.

## 16. Verify

### Como comprovar que funcionou?
Reexecutar a mesma detecção autoritativa e comparar estado real com o alvo/critério definido; exit code ou ausência de exceção nunca bastam sozinhos.

### Critério de sucesso
Only an explicitly selected, supported change for **Driver update & source awareness**, built from current state and context, reaches its declared post-condition; otherwise no change is performed.

### Resultado parcial
Yes — quando apenas parte independente do estado puder ser lida/validada. Nunca promover `Partial` silenciosamente a sucesso total.

## 17. Rollback

### É reversível?
Unknown

### Método de rollback
Restore the exact captured pre-change state using the same supported surface used for Apply where possible. If the original state was absent, remove the created state rather than writing an assumed default.

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
TBD

### Motivo
A capability possui caminho mutável. A necessidade de System Restore deve ser decidida somente após o mecanismo concreto, risco e capacidade de rollback específico serem comprovados; ele não substitui snapshot/rollback determinístico.

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
Optional

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
Local: `/prototypes/drivers/c-drivers-003/` — **TBD / ainda não executado nesta fase documental**.

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

- Documented behavior — https://learn.microsoft.com/windows-hardware/drivers/install/setupapi
- Documented behavior — https://learn.microsoft.com/windows-hardware/drivers/devtest/pnputil-command-syntax
- Documented behavior — https://learn.microsoft.com/windows-hardware/manufacture/desktop/dism-driver-servicing-command-line-options-s14
- Documented behavior — https://learn.microsoft.com/windows/client-management/mdm/policy-csp-deviceinstallation

**Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.

<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Yes — source awareness/ranking across inbox/OEM/vendor/Windows Update and unknown source.
- Evidência realmente executada: No source-awareness probe; current PnP PASS lacks source fields.
- Execução segura neste host / teste: Read-only inventory is safe. Teste recomendado: Resolve provider/version/date/INF and source/ranking for representative devices; verify “newer” is not automatically “better”.
- Impedimento ou limitação restante: Current host lacks a controlled driver-source matrix and no feature prototype; installation is not safe.
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

- Capability de awareness/recommendation: inventariar provider/version/date/INF e distinguir Windows Update, OEM/vendor e driver store quando observável.
- Não instalar “mais novo” automaticamente nem considerar version number maior sempre melhor.
- Install deve delegar para workflow específico e respeitar driver ranking/signature.
- `PROVEN` exige device com inbox/OEM/updated driver e source desconhecida.

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
