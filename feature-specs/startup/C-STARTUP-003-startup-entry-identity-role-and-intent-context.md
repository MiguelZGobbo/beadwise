# Feature Specification — C-STARTUP-003 — Startup entry identity, role & intent context

## 3. Identificação

Nome: Startup entry identity, role & intent context  
ID: C-STARTUP-003  
Tipo: Diagnostic, Diagnostic / UX  
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
Capability de produto para detectar e contextualizar **startup entry identity, role & intent context**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

**Base conceitual do Discovery:** [6] Startup Entry Publisher & Signature Context — `CANDIDATE`; [7] Startup Functional Role Classification — `CANDIDATE`; [8] Startup User-Intent Awareness — `CANDIDATE`; [13] Launcher Auto-Start Diagnostics — `CANDIDATE`; [14] Cloud Sync Startup Diagnostics — `CANDIDATE`; [15] Hardware Helper Startup Diagnostics — `CANDIDATE`

### Objetivo
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

### Problema que resolve
Transforma a intenção do Discovery em estado técnico verificável sem presumir que “executa no logon/boot” significa problema ou que “alto impacto” significa automaticamente seguro para desativar.

### Benefício esperado
Diagnostic / Quality of Life

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
High for signer/file metadata; Medium/Low for role inference; user intent is authoritative when explicitly provided.

## 7. Estado atual

### O que precisa ser detectado?
- Canonical target/executable or task action from C-STARTUP-001.
- Authenticode verification with `WinVerifyTrust(WINTRUST_ACTION_GENERIC_VERIFY_V2)`; record signed/unsigned/verification error separately from publisher trust judgment.
- File version metadata (`CompanyName`, `ProductName`, `FileDescription`, version) through Version APIs when present; absence is valid.
- Source metadata: Task Scheduler author/description/principal; service display name/description for context; package identity/display name for packaged entries.
- User-intent signal maintained by BeadWise (explicit “I need this at login”, “optional”, “unknown”), never inferred solely from filename/publisher.
- Functional role classification is evidence-based and confidence-scored (cloud sync, launcher, hardware helper, security, accessibility, updater, unknown); `Unknown` is preferable to a guessed role.

### Como detectar?
Resolve identity without loading/executing the target. Verify signature and version metadata directly from file. Apply deterministic role rules first; optional curated knowledge may enrich but must be versioned and never override direct evidence/user intent.

### Fonte da verdade
Entry target/source from C-STARTUP-001; Authenticode result from WinVerifyTrust; version resource from Version API; explicit user intent from BeadWise persistence. Role classification is an interpretation layer and never becomes a source of truth.

### Estados possíveis
- Identified + verified signer
- Identified + unsigned
- Identified + signature invalid/untrusted
- Metadata partial
- Role classified High/Medium/Low confidence
- User intent explicit/unknown
- Target missing
- Unknown/Error

## 8. Estado alvo
Each startup entry has explainable identity, signer/version metadata where available, role with confidence/evidence, and explicit/unknown user intent; no safety decision is made solely from the role label.

## 9. Implementação técnica

### Método principal
Resolve identity without loading/executing the target. Verify signature and version metadata directly from file. Apply deterministic role rules first; optional curated knowledge may enrich but must be versioned and never override direct evidence/user intent.

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
- WinVerifyTrust + WINTRUST_ACTION_GENERIC_VERIFY_V2
- GetFileVersionInfoSizeEx/GetFileVersionInfoEx/VerQueryValue
- Task Scheduler registration/action metadata
- Package identity APIs/manifest metadata when applicable

### Alternativas avaliadas
File path/name heuristics alone are insufficient. Online reputation is not required for baseline functionality and would introduce privacy/network dependencies.

### Abordagem escolhida
Usar superfícies públicas/documentadas por source, representar cobertura parcial explicitamente e manter Detect → Interpret → Recommend → Apply separados.

## 10. Permissões

### Requer administrador?
No

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
Feature: C-STARTUP-003
Current state: structured Detect result
Target state: Each startup entry has explainable identity, signer/version metadata where available, role with confidence/evidence, and explicit/unknown user intent; no safety decision is made solely from the role label.
Changes:
N/A — read-only diagnostic/safeguard; `Changes = []`.
Admin required: No
Restart required: Nada
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
Each startup entry has explainable identity, signer/version metadata where available, role with confidence/evidence, and explicit/unknown user intent; no safety decision is made solely from the role label.

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
WinVerifyTrust + WINTRUST_ACTION_GENERIC_VERIFY_V2, GetFileVersionInfoSizeEx/GetFileVersionInfoEx/VerQueryValue, Task Scheduler registration/action metadata, Package identity APIs/manifest metadata when applicable

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
Local: `/prototypes/startup/c-startup-003/` — TBD.

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
- Documented behavior — https://learn.microsoft.com/windows/win32/api/wintrust/nf-wintrust-winverifytrust
- Documented behavior — https://learn.microsoft.com/windows/win32/api/winver/nf-winver-getfileversioninfoexw
- Documented behavior — https://learn.microsoft.com/windows/win32/taskschd/task-scheduler-2-0-interfaces

Observed behavior: N/A nesta revisão; nenhuma execução real foi alegada.

**Observed behavior (campanha 2026-09-08):** probes compartilhados read-only executados neste host: `startup.inventory.cim`=PASS. Evidência: `/prototypes/system/C-SYSTEM-017/results/domain-evidence.json`. Estes sinais são parciais e não satisfazem, sozinhos, o gate DETECT completo desta feature.

## 32. Benefício real
Reasonable

## 33. Impacto mensurável

### Pode ser medido?
No

### Como?
N/A — validate classification accuracy/coverage separately; no performance claim.

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
- Define and version the initial role taxonomy/rule evidence without turning vendor names into unconditional allow/deny lists.

## 37. Critério para PROVEN
- [ ] Detect validado em todas as sources declaradas supported
- [ ] Partial/Unsupported preservados corretamente
- [ ] Dry-run sem mutação
- [ ] Apply/Snapshot/Rollback confirmados N/A
- [ ] Compatibilidade mínima testada
- [ ] Questões críticas acima resolvidas ou explicitamente bloqueantes

## 38. Critério para APPROVED
Somente após PROVEN + utilidade/risco/manutenção/UX/redundância revisados. Em particular, não aprovar mecanismo privado só para alcançar paridade visual com Task Manager/Autoruns.
