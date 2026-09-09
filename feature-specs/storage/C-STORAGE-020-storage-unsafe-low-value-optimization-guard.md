# Feature Specification — C-STORAGE-020 — Storage unsafe & low-value optimization guard

## 3. Identificação

Nome: Storage unsafe & low-value optimization guard
ID: C-STORAGE-020
Tipo: Configuration, Safety
Technical Domain: STORAGE
Primary Product Area: TBD
Also Used By: My PC, Monitoring, Optimization, Cleaning, Benchmark, Gaming
Shared Capability: No
Final UI Placement: TBD
Status: SPECIFIED
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Guard explícito contra mudanças destrutivas/baixo valor, incluindo RAID modification e desativação de Reserved Storage como “otimização”.

**Base conceitual do Discovery:** [152] RAID Modification Guard — `CANDIDATE`; [159] Disable Reserved Storage for Optimization — `QUESTIONABLE`

### Objetivo
Bloquear classes de mudança que excedem o benefício plausível ou alteram segurança/recovery/storage topology sem owner/spec própria.

### Problema que resolve
Optimizer apps frequentemente promovem RAID/Reserved Storage tweaks porque comandos existem, não porque há ganho seguro.

### Benefício esperado
Safety

## 5. Aplicabilidade

### Quando se aplica?
Qualquer recommendation/change de storage que corresponda às classes bloqueadas.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não executar RAID conversion/reconfiguration.
- Disable Reserved Storage for Optimization continua QUESTIONABLE e bloqueado.

## 5.1 Uso dentro do produto

### Consumidores
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

### Uso direto pelo usuário
No

### Capacidade compartilhada
N/A — o Discovery classifica `Shared Capability: No`. Outros consumidores podem reutilizar o output desta capability, mas o ownership permanece aqui e nenhuma infraestrutura compartilhada adicional é criada por esta spec.

## 6. Recomendação

### A feature deve ser recomendável automaticamente?
No

### Critérios de recomendação
N/A

### Motivo apresentado ao usuário
N/A

### Grau de confiança
High

## 7. Estado atual

### O que precisa ser detectado?
- Classificar ChangePlan por operation type e target.
- Consultar Reserved Storage state apenas para explicar contexto, não para recomendar disable.
- Detectar se mudança mexe em RAID/pool topology.

### Como detectar?
Default-deny para operation classes de topology/security/servicing não aprovadas.

### Fonte da verdade
ChangePlan semantics + owner spec; não depende de Registry heuristics.

### Estados possíveis
- Allowed
- Blocked unsafe
- Blocked low-value
- Requires explicit separate spec

## 8. Estado alvo
Operação bloqueada antes de elevação/apply.

## 9. Implementação técnica

### Método principal
Default-deny para operation classes de topology/security/servicing não aprovadas.

### Tecnologias utilizadas
- [x] .NET API
- [ ] Win32
- [ ] Registry
- [ ] PowerShell
- [ ] CMD / executable
- [ ] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- N/A

### Alternativas avaliadas
Aviso visual sem bloqueio é insuficiente para operações destrutivas.

### Abordagem escolhida
Hard guard no backend.

## 10. Permissões

### Requer administrador?
No

### Quando ocorre a elevação?
Bloquear antes de pedir elevação.

### O que acontece se não houver permissão?
N/A

## 11. Reinicialização

### Requer
- [x] Nada
- [ ] Reinício do aplicativo
- [ ] Reinício de processo
- [ ] Reinício de serviço
- [ ] Logoff
- [ ] Reboot do Windows
- [ ] Desconhecido

**Nota:** Nada

### A alteração só pode ser validada depois da reinicialização?
No

### Rollback também exige reinicialização?
No

## 12. Change Plan

```text
Feature: C-STORAGE-020
Current state: resultado estruturado de Detect
Target state: Operação bloqueada antes de elevação/apply.
Changes: derivadas somente se a capability tiver Apply aprovado; caso contrário lista vazia
Admin required: No
Restart required: Nada
Risk: Low
Reversible: N/A
```

O plano é gerado a partir do mesmo resultado de Detect usado pelo dry-run; nenhum target mutável pode ser inventado pela UI.

## 13. Dry-run

### É suportado?
Yes

### O que o dry-run deve fazer?
- Detectar e validar prerequisites.
- Construir o ChangePlan real.
- Exibir estado, evidência, risco, admin/restart e mudanças exatas quando existirem.
- Para feature somente leitura, mostrar que `Changes = []` em vez de simular Apply inexistente.

### O que o dry-run NÃO pode fazer?
Não executar alterações persistentes, não iniciar benchmark write/repair e não alterar política do sistema.

### Limitações
Resultados que dependem de carga, temperatura, hardware/vendor ou reboot só podem ser confirmados na prova/execução correspondente.

## 14. Snapshot

### É necessário?
No

### O que precisa ser salvo antes da alteração?
N/A — feature somente leitura ou guard; nenhum estado do sistema é alterado.

### Estado inexistente também deve ser registrado
N/A.

## 15. Apply

### Sequência de execução
N/A — esta capability não executa alteração persistente. Para consumidores, apenas Detect → interpretar → retornar estado/guard.

### Atomicidade
N/A.

## 16. Verify

### Como comprovar que funcionou?
Executar novamente a mesma detecção e validar schema/coerência; para diagnostics, sucesso significa dados válidos ou estado Unsupported/Partial explicitamente representado.

### Critério de sucesso
Operação bloqueada antes de elevação/apply.

### Resultado parcial
Yes — quando a fonte da verdade expõe subset, quando uma camada abstrai hardware ou quando parte independente do plano falha de maneira representável. Resultado parcial nunca é promovido silenciosamente a sucesso total.

## 17. Rollback

### É reversível?
N/A

### Método de rollback
N/A — nenhuma alteração persistente.

### Ordem de reversão
Para mudanças compostas, ordem inversa quando tecnicamente apropriado; N/A para diagnostics.

## 18. Verify Rollback

### Critério de sucesso
N/A.

### O que fazer se rollback falhar?
Registrar `ROLLBACK_FAILED`, manter snapshot/audit log, permitir retry somente se seguro e fornecer orientação manual/System Restore apenas quando realmente aplicável.

## 19. System Restore

### A feature exige ponto de restauração?
Not Required

### Motivo
A decisão segue o risco real da operação. Diagnostics/guards não justificam restore point; operações com rollback próprio não devem usar System Restore como mecanismo primário.

## 20. Risco

### Classificação
Low

### Possíveis efeitos negativos
- Pode impedir usuário avançado de fazer tuning; produto prioriza segurança e escopo.

### Pior cenário plausível
Bypass do guard por caminho técnico alternativo; testes devem cobrir aliases/operation types.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Agnóstica.

Matriz mínima a validar antes de `PROVEN`:
- Windows 10 22H2: expected/test TBD quando aplicável.
- Windows 11 23H2: expected/test TBD.
- Windows 11 24H2: expected/test TBD.
- Windows 11 25H2: expected/test TBD.

### Arquitetura
- x64: Expected; test TBD.
- ARM64: Expected somente se APIs/binários usados existirem; test TBD.

### Hardware
A compatibilidade de hardware segue as limitações descritas acima; `Unsupported`/`Partial` são estados válidos e nunca devem ser convertidos em erro genérico.

## 22. Dependências

### Outras features
- ChangePlan architecture
- C-STORAGE-017

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Nenhuma feature pode executar operação bloqueada sem nova decisão de Discovery/spec.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- CHANGE_BLOCKED_UNSAFE
- CHANGE_REQUIRES_SEPARATE_SPEC

Códigos definitivos serão alinhados à arquitetura global; estes nomes descrevem semanticamente os casos necessários.

## 26. Logging
- Detection started / completed com stable target id e provenance.
- Current state + confidence/partial flags.
- ChangePlan generated e guard decision.
- Snapshot captured quando aplicável.
- Apply/Verify/Rollback lifecycle quando aplicável.
- Falhas estruturadas e restart/admin requirements.
- Nunca registrar secrets, conteúdo de arquivos do usuário ou recovery keys.

## 27. Persistência

### O estado precisa sobreviver ao fechamento do app?
Yes

### O snapshot precisa sobreviver reboot?
No

### Arquivos/dados necessários
Audit de blocks/reason.

## 28. Testes

### Teste unitário
- Parsing/normalização dos estados e erros.
- Regras de recomendação/guard.
- Geração determinística de ChangePlan.
- Comparação actual vs expected e snapshot quando aplicável.
- Compatibilidade/Unsupported/Partial.

### Dry-run
Gerar exatamente o plano que seria executado e provar que nenhuma alteração persistente ocorre.

### Integração real
Unit tests com RAID change, Reserved Storage disable, write-cache unsafe policy e allowed maintenance.

### Rollback
N/A para feature somente leitura; testar que não existe path de mutação.

### VM
Safe

### Hardware real
No

## 29. Cenários mínimos de teste
- [ ] Feature já está no estado desejado
- [ ] Estado normal/healthy
- [ ] Feature não é suportada
- [ ] Falta permissão
- [ ] Detection falha
- [ ] Verify falha
- [ ] Resultado Partial/Unknown
- [ ] Fonte secundária indisponível
- [ ] Estado inicial personalizado
- [ ] Configuração inexistente
- [ ] Execução repetida

## 30. Prova técnica

### Script/protótipo
Local planejado: `/prototypes/storage/c-storage-020/`

### Resultado
Policy proof by tests PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- reserved_dism: https://learn.microsoft.com/en-us/windows-hardware/manufacture/desktop/dism-storage-reserve?view=windows-11
- pool: https://learn.microsoft.com/en-us/powershell/module/storage/get-storagepool?view=windowsserver2025-ps
- writecache: https://learn.microsoft.com/en-us/windows-hardware/drivers/storage/querying-for-the-write-cache-property

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.

<!-- PHASE2-SAFE-PROOF-20260908:START -->
**Observed safe proof (auditoria final, 2026-09-08):**
- Capability/premissa exercitada: Low-value or unsafe optimization claims are blocked while read-only analysis remains permitted.
- Resultado observado deste probe: `PASS`.
- Evidência reproduzível: `/prototypes/storage/C-STORAGE-001/results/storage-proof.json` (script e teste no mesmo prototype).
- Limites preservados: contract fixture only
- Este resultado substitui `NOT_TESTED` somente para a premissa acima; não satisfaz por si só todos os cenários mínimos nem promove a Feature Spec a `PROVEN`/`APPROVED`.
<!-- PHASE2-SAFE-PROOF-20260908:END -->
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — guard rejeita “tweak” low-value/unsafe.
- Evidência realmente executada: Prototype seguro adicional executado: Low-value or unsafe optimization claims are blocked while read-only analysis remains permitted. Resultado observado: `PASS`. Evidência: `/prototypes/storage/C-STORAGE-001/results/storage-proof.json`. A evidência anterior foi substituída por esta observação mais recente.
- Execução segura neste host / teste: Sim — fixtures de allowlist/risk/reason e decisões de não executar.
- Impedimento ou limitação restante: Não possui Apply permitido; é comportamento esperado.
- Disposição: `SPECIFIED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

## 32. Benefício real
Proven

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
No

### Como?
N/A

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Reason why action is not offered/blocked when surfaced through consumer.

### Ações disponíveis
- N/A

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- blocked
- reasonCode
- explanation

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Manter taxonomia alinhada à política global de ChangePlan.

## 37. Critério para PROVEN
- [ ] Detect funciona nos ambientes aplicáveis.
- [ ] ChangePlan representa corretamente alterações ou lista vazia para diagnostic.
- [ ] Dry-run não altera o sistema.
- [ ] Apply/Verify funcionam quando aplicáveis.
- [ ] Snapshot/Rollback/Verify Rollback funcionam quando aplicáveis.
- [ ] Riscos principais validados.
- [ ] Compatibilidade mínima testada.
- [ ] Falhas conhecidas documentadas.

**Estado atual:** SPECIFIED. Não é `PROVEN` sem a prova técnica registrada.

## 38. Critério para APPROVED
Mesmo após `PROVEN`, requer avaliação de utilidade, confiabilidade, escopo, risco, manutenção, UX, duplicação e fragilidade conforme o template canônico.
