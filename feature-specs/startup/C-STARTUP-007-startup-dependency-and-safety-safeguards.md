# Feature Specification — C-STARTUP-007 — Startup dependency & safety safeguards

## 3. Identificação

Nome: Startup dependency & safety safeguards  
ID: C-STARTUP-007  
Tipo: Diagnostic, Safeguard  
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
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **startup dependency & safety safeguards**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

**Base conceitual do Discovery:** [16] Security Software Startup Safeguard — `CANDIDATE`; [17] Accessibility Startup Safeguard — `CANDIDATE`; [18] Auto-Start Service Context — `CANDIDATE`; [19] Trigger-Start Service Awareness — `CANDIDATE`; [20] Scheduled Task Startup Context — `CANDIDATE`; [22] Automatic Maintenance Awareness — `CANDIDATE`; [23] Startup Entry Dependency Diagnostics — `CANDIDATE`; [36] RunOnce Transient-State Awareness — `CANDIDATE`

### Objetivo
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

### Problema que resolve
Transforma a intenção do Discovery em estado técnico verificável sem presumir que “executa no logon/boot” significa problema ou que “alto impacto” significa automaticamente seguro para desativar.

### Benefício esperado
Stability / Safety

## 5. Aplicabilidade

### Quando se aplica?
quando uma configuração ou recomendação puder ser confundida com otimização universal.

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
High for explicit blockers; conservative Unknown blocks automation.

## 7. Estado atual

### O que precisa ser detectado?
- Security software/startup protection context from the Security domain when available; until then, signer/role alone cannot establish that disabling is safe.
- Accessibility/user-intent context: if entry is explicitly associated with assistive technology or user marks it required, automatic recommendation is blocked.
- Services: query `QueryServiceConfig` and `QueryServiceConfig2(SERVICE_CONFIG_TRIGGER_INFO)` so trigger-start services are distinguished from unconditional automatic start. Do not recommend service changes merely because a service may run near startup.
- Scheduled tasks: inspect trigger type, maintenance settings, principal, conditions and actions; Boot/Logon does not imply “bloat”.
- RunOnce entries are transient setup/maintenance state; Microsoft documents RunOnce for transient conditions, so default policy is **do not optimize/disable** unless a specific repair diagnosis owns the action.
- Dependency confidence: explicit known dependency/user intent blocks; unknown dependency on a medium/high-risk entry must prevent automatic recommendation.

### Como detectar?
Implement a guard pipeline that runs after detection and before scoring. Blockers have precedence over performance impact. Guards do not change system state.

### Fonte da verdade
Explicit subsystem configuration and cross-domain safety signals. Unknown dependency/safety is not equivalent to safe.

### Estados possíveis
- Safe-to-consider
- Blocked: security
- Blocked: accessibility/user intent
- Blocked: transient RunOnce
- Blocked: service/task semantics
- Blocked: managed/maintenance
- Dependency unknown — manual only

## 8. Estado alvo
Every potential startup change receives a guard decision with explicit blocker/reason; unsafe or uncertain entries cannot become automatic recommendations.

## 9. Implementação técnica

### Método principal
Implement a guard pipeline that runs after detection and before scoring. Blockers have precedence over performance impact. Guards do not change system state.

### Tecnologias utilizadas
- [x] .NET API
- [x] Win32
- [ ] Registry
- [ ] PowerShell
- [ ] CMD / executable
- [ ] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [x] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- SCM QueryServiceConfig/QueryServiceConfig2(SERVICE_CONFIG_TRIGGER_INFO)
- Task Scheduler trigger/maintenance/principal interfaces
- C-STARTUP-003 role/user-intent output
- C-SECURITY-* and future accessibility owner outputs when available

### Alternativas avaliadas
Allowlist/denylist by vendor name alone rejected. “High impact => safe to disable” rejected.

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
Feature: C-STARTUP-007
Current state: structured Detect result
Target state: Every potential startup change receives a guard decision with explicit blocker/reason; unsafe or uncertain entries cannot become automatic recommendations.
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
Every potential startup change receives a guard decision with explicit blocker/reason; unsafe or uncertain entries cannot become automatic recommendations.

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
C-STARTUP-001

### Serviços
Task Scheduler/SCM apenas quando a fonte em análise exigir.

### APIs
SCM QueryServiceConfig/QueryServiceConfig2(SERVICE_CONFIG_TRIGGER_INFO), Task Scheduler trigger/maintenance/principal interfaces, C-STARTUP-003 role/user-intent output, C-SECURITY-* and future accessibility owner outputs when available

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
Local: `/prototypes/startup/c-startup-007/` — TBD.

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
- Documented behavior — https://learn.microsoft.com/windows/win32/setupapi/run-and-runonce-registry-keys
- Documented behavior — https://learn.microsoft.com/windows/win32/services/service-trigger-events
- Documented behavior — https://learn.microsoft.com/windows/win32/taskschd/task-scheduler-2-0-interfaces

Observed behavior: N/A nesta revisão; nenhuma execução real foi alegada.

**Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.

## 32. Benefício real
Reasonable

## 33. Impacto mensurável

### Pode ser medido?
No

### Como?
N/A — validate false-positive/false-negative guard cases in test corpus.

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
- Define the cross-domain contract with Security and System accessibility capability once those specs are completed; until then corresponding guard may return `Unknown/manual only`.

## 37. Critério para PROVEN
- [ ] Detect validado em todas as sources declaradas supported
- [ ] Partial/Unsupported preservados corretamente
- [ ] Dry-run sem mutação
- [ ] Apply/Snapshot/Rollback confirmados N/A
- [ ] Compatibilidade mínima testada
- [ ] Questões críticas acima resolvidas ou explicitamente bloqueantes

## 38. Critério para APPROVED
Somente após PROVEN + utilidade/risco/manutenção/UX/redundância revisados. Em particular, não aprovar mecanismo privado só para alcançar paridade visual com Task Manager/Autoruns.
