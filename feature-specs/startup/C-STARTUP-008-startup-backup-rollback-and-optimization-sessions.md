# Feature Specification — C-STARTUP-008 — Startup backup, rollback & optimization sessions

## 3. Identificação

Nome: Startup backup, rollback & optimization sessions
ID: C-STARTUP-008
Tipo: Optimization / Safeguard, Safeguard
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
Capability de produto para avaliar **startup backup, rollback & optimization sessions**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

**Base conceitual do Discovery:** [26] Startup Change Backup & Rollback — `CANDIDATE`; [37] Startup Optimization Session Profile — `CANDIDATE`

### Objetivo
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

### Problema que resolve
Transforma a intenção do Discovery em estado técnico verificável sem presumir que “executa no logon/boot” significa problema ou que “alto impacto” significa automaticamente seguro para desativar.

### Benefício esperado
Stability / Quality of Life

## 5. Aplicabilidade

### Quando se aplica?
quando o cenário descrito pela capability estiver presente.

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
High for BeadWise-owned session state; depends on source-specific operation support from C-STARTUP-004.

## 7. Estado atual

### O que precisa ser detectado?
- All planned source-specific changes and their exact pre-change values/objects from C-STARTUP-004.
- Snapshot must include source kind, scope/user SID where relevant, Registry key/value name/type/data and existence, shortcut path/content hash/target metadata, task path/enabled state, and any other field the chosen operation can modify.
- Optimization session metadata: capability/version, Windows build, user, start/end timestamps, planned operations, per-operation apply/verify status and before/after measurement references.
- Rollback eligibility: refuse automatic rollback if the target object changed externally since Apply unless the operation defines a safe merge/conflict policy.

### Como detectar?
Use transactional orchestration over source-specific operations. Persist snapshot before the first mutation. Apply sequentially with per-operation verification. On dependent failure, rollback prior applied operations in reverse order. Session survives app crash/reboot when unfinished.

### Fonte da verdade
Persisted BeadWise ChangePlan + exact snapshot captured immediately before Apply; current source state is used to detect rollback conflicts.

### Estados possíveis
- Plan only
- Snapshot captured
- Applied + verified
- Partial failure
- Keep
- Rollback available
- Rollback conflict
- Rolled back + verified
- Rollback failed

## 8. Estado alvo
Every BeadWise startup mutation is attributable to a session and can restore exact prior state when the source operation is classified reversible; no “restore defaults” substitution.

## 9. Implementação técnica

### Método principal
Use transactional orchestration over source-specific operations. Persist snapshot before the first mutation. Apply sequentially with per-operation verification. On dependent failure, rollback prior applied operations in reverse order. Session survives app crash/reboot when unfinished.

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
- Source-specific operations from C-STARTUP-004
- Durable local persistence with schema/version and integrity checks
- Current-state re-detection from C-STARTUP-001 before rollback

### Alternativas avaliadas
System Restore as primary rollback rejected. “Default value” rollback rejected. In-memory-only snapshot rejected for mutations.

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
Normalmente a configuração pode ser re-detectada imediatamente; o efeito em startup só é medido no próximo logon/boot comparável.

### Rollback também exige reinicialização?
Configuração: normalmente não. Efeito percebido só é reavaliado no próximo logon/boot.

## 12. Change Plan

```text
Feature: C-STARTUP-008
Current state: structured Detect result
Target state: Every BeadWise startup mutation is attributable to a session and can restore exact prior state when the source operation is classified reversible; no “restore defaults” substitution.
Changes:
1. Re-detect the exact source object.
2. Re-run safeguards and policy/ownership checks.
3. Capture/persist exact pre-change state.
4. Execute only a source-specific operation explicitly marked supported.
5. Re-detect/verify.
6. On failure, rollback dependent operations when safe.
Admin required: Conditional
Restart required: Nada
Risk: Low
Reversible: Fully for operations admitted as reversible; unsupported operations must not enter Apply
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
Yes

### O que precisa ser salvo antes da alteração?
Estado exato do objeto sob ownership da operação, inclusive existência/ausência, tipo, dados, caminho/source, scope/user, metadata necessária à restauração e hash/version para detectar conflito externo.

### Estado inexistente também deve ser registrado
Yes — ausência é estado e rollback deve restaurá-la.

## 15. Apply

### Sequência de execução
1. Re-detect the exact source object.
2. Re-run safeguards and policy/ownership checks.
3. Capture/persist exact pre-change state.
4. Execute only a source-specific operation explicitly marked supported.
5. Re-detect/verify.
6. On failure, rollback dependent operations when safe.

### Atomicidade
Por operação; sessão composta deve parar em falha dependente e reverter operações já aplicadas em ordem inversa quando seguro. Operações independentes só continuam se o plano disser explicitamente.

## 16. Verify

### Como comprovar que funcionou?
Reexecutar a mesma fonte autoritativa; para performance, usar a metodologia da capability de medição. Exit code não basta.

### Critério de sucesso
Every BeadWise startup mutation is attributable to a session and can restore exact prior state when the source operation is classified reversible; no “restore defaults” substitution.

### Resultado parcial
Yes — cobertura/source/attribution incompleta deve permanecer `Partial`.

## 17. Rollback

### É reversível?
Fully for operations admitted as reversible; unsupported operations must not enter Apply

### Método de rollback
Restaurar o snapshot exato usando o mesmo owner/source. Se houver conflito externo desde Apply, não sobrescrever silenciosamente; retornar `ROLLBACK_CONFLICT`.

### O rollback restaura:
`estado original`, nunca “default”.

### Ordem de reversão
Inversa para operações dependentes.

## 18. Verify Rollback

### Critério de sucesso
Re-detect é semanticamente igual ao snapshot para todos os campos alterados.

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
- Mudança indevida pode remover funcionalidade esperada no logon ou ser recriada pelo aplicativo.

### Pior cenário plausível
Perda temporária de funcionalidade de startup e conflito com software que recria a entrada; rollback específico deve recuperar quando a operação era suportada.

### Recuperação manual possível?
Yes/Partial

### Procedimento manual
Restaurar snapshot/source original ou reabilitar pela superfície oficial do owner; para Settings-owned, orientar o usuário na página Startup Apps.

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
Source-specific operations from C-STARTUP-004, Durable local persistence with schema/version and integrity checks, Current-state re-detection from C-STARTUP-001 before rollback

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
Conditional — Apply deve virar no-op quando o mesmo estado alvo já existe. Operações baseadas em delete/move precisam validar identidade para não repetir efeitos.

## 25. Falhas possíveis
- `SOURCE_UNSUPPORTED`
- `PARTIAL_COVERAGE`
- `ENTRY_NOT_FOUND`
- `TARGET_UNRESOLVED`
- `ACCESS_DENIED`
- `ADMIN_REQUIRED`
- `POLICY_BLOCKED`
- `DEPENDENCY_UNKNOWN`
- `APPLY_FAILED`
- `VERIFY_FAILED`
- `ROLLBACK_CONFLICT`
- `ROLLBACK_FAILED`

## 26. Logging
Registrar capability/source, stable entry identity, provenance, confidence, detection result, guard/recommendation reason e measurement/session IDs. Para mutação: snapshot reference, exact operation, Apply/Verify/Rollback. Não registrar command lines que possam conter secrets sem redaction.

## 27. Persistência

### O estado precisa sobreviver ao fechamento do app?
Yes — history/session is intrinsic to this capability.

### O snapshot precisa sobreviver reboot?
Yes para sessão ainda não finalizada; a aplicação pode fechar ou o usuário reiniciar antes de Keep/Rollback.

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
Testar estado customizado, ausência original, external drift/conflict e crash entre Apply e Verify.

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
- [ ] Apply/Verify
- [ ] Partial failure
- [ ] Rollback
- [ ] Rollback conflict
- [ ] Estado inicial personalizado/ausente

## 30. Prova técnica

### Script/protótipo
Local: `/prototypes/startup/c-startup-008/` — TBD.

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

## 31. Evidências
- Documented behavior — https://learn.microsoft.com/windows/win32/setupapi/run-and-runonce-registry-keys
- Documented behavior — https://learn.microsoft.com/windows/win32/taskschd/task-scheduler-2-0-interfaces
- Documented behavior — https://learn.microsoft.com/uwp/api/windows.applicationmodel.startuptask?view=winrt-28000

Observed behavior: N/A nesta revisão; nenhuma execução real foi alegada.

**Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.

<!-- PHASE2-SAFE-PROOF-20260908:START -->
**Observed safe proof (auditoria final, 2026-09-08):**
- Capability/premissa exercitada: A prototype-owned startup artifact can be snapshotted, changed, restored and hash-verified.
- Resultado observado deste probe: `PASS`
- Evidência reproduzível: `/prototypes/startup/C-STARTUP-006/results/startup-contract.json` (script e teste no mesmo prototype).
- Limites preservados: does not prove registry/startup-folder rollback
- Este resultado substitui `NOT_TESTED` somente para a premissa acima; não satisfaz por si só todos os cenários mínimos nem promove a Feature Spec a `PROVEN`/`APPROVED`.
<!-- PHASE2-SAFE-PROOF-20260908:END -->

<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — snapshot/backup/rollback e inexistência de valor.
- Evidência realmente executada: Prototype seguro adicional executado: A prototype-owned startup artifact can be snapshotted, changed, restored and hash-verified. Resultado observado: `PASS`. Evidência: `/prototypes/startup/C-STARTUP-006/results/startup-contract.json`. A evidência anterior foi substituída por esta observação mais recente.
- Execução segura neste host / teste: Sim, serial — chave HKCU temporária + arquivo Startup owned; snapshot→dry-run→apply→verify→rollback→verify rollback.
- Impedimento ou limitação restante: Formato durável/encriptação final é Fase 3, mas não bloqueia o contrato de prova.
- Disposição: `SPECIFIED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

## 32. Benefício real
Situational

## 33. Impacto mensurável

### Pode ser medido?
Partial

### Como?
Use C-STARTUP-005 to associate the session with before/after startup metrics; rollback correctness is state equality, not performance.

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
- Select durable storage format/encryption/integrity mechanism later at architecture level without changing the snapshot contract defined here.

## 37. Critério para PROVEN
- [ ] Detect validado em todas as sources declaradas supported
- [ ] Partial/Unsupported preservados corretamente
- [ ] Dry-run sem mutação
- [ ] Apply/Verify source-specific
- [ ] Snapshot/Rollback/Verify Rollback
- [ ] Conflict/recreation test
- [ ] Compatibilidade mínima testada
- [ ] Questões críticas acima resolvidas ou explicitamente bloqueantes

## 38. Critério para APPROVED
Somente após PROVEN + utilidade/risco/manutenção/UX/redundância revisados. Em particular, não aprovar mecanismo privado só para alcançar paridade visual com Task Manager/Autoruns.
