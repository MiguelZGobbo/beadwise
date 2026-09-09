# Feature Specification — C-STORAGE-017 — Storage optimization outcome validation & risk guard

## 3. Identificação

Nome: Storage optimization outcome validation & risk guard
ID: C-STORAGE-017
Tipo: Diagnostic / Product Behavior, Product Behavior, Safety, Shared Capability, Validation
Technical Domain: STORAGE
Primary Product Area: TBD
Also Used By: My PC, Monitoring, Optimization, Cleaning, Benchmark, Gaming
Shared Capability: No
Final UI Placement: TBD
Status: DEFERRED
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Camada de validação e risco que impede otimizações de storage de serem apresentadas como benéficas sem evidência e pós-verificação.

**Base conceitual do Discovery:** [127] Storage Health Summary — `CANDIDATE`; [128] Storage Optimization Myth Guard — `CANDIDATE`; [197] Storage Optimization Validation — `CANDIDATE`; [199] Storage Change Risk Classification — `CANDIDATE`; [200] Storage Diagnostic Summary — `CANDIDATE`

### Objetivo
Normalizar health summary, risk, expected benefit, before/after e myth guards para qualquer mudança de storage.

### Problema que resolve
Mecanismos legítimos podem ser usados fora de contexto; sem guard comum cada feature pode inventar thresholds e critérios de sucesso.

### Benefício esperado
Safety / Validation

## 5. Aplicabilidade

### Quando se aplica?
Toda recommendation/apply de domínio Storage.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não substitui owner técnico de cada capability.
- Não autoriza mudança que a spec dona não aprovou.

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
N/A; é policy engine interna.

### Motivo apresentado ao usuário
N/A

### Grau de confiança
N/A

## 7. Estado atual

### O que precisa ser detectado?
- Consumir outputs das specs donas: inventory, health, thermal, capacity, BitLocker, VSS etc.
- Validar que recommendation tem criterion/evidence e measurement quando claim exigir.

### Como detectar?
Rules declarativas versionadas; sem valores universais escondidos. Cada regra referencia capability/source e reason code.

### Fonte da verdade
Dados continuam owned pelas services específicas; guard não cria fonte paralela.

### Estados possíveis
- Allowed
- Allowed with warning
- Manual-only
- Blocked
- Insufficient evidence
- Not applicable

## 8. Estado alvo
Nenhum ChangePlan de storage executa sem guard decision e provenance.

## 9. Implementação técnica

### Método principal
Rules declarativas versionadas; sem valores universais escondidos. Cada regra referencia capability/source e reason code.

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
Checks espalhados em cada UI/feature aumentariam inconsistência.

### Abordagem escolhida
Policy layer compartilhada.

## 10. Permissões

### Requer administrador?
No

### Quando ocorre a elevação?
N/A

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
Feature: C-STORAGE-017
Current state: resultado estruturado de Detect
Target state: Nenhum ChangePlan de storage executa sem guard decision e provenance.
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
Nenhum ChangePlan de storage executa sem guard decision e provenance.

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
- Policy excessivamente conservadora pode bloquear ação útil.

### Pior cenário plausível
Feature insegura passa por regra incompleta; requer testes de contrato e default-deny para operação não classificada.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Agnóstica; depende da compatibilidade de inputs.

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
- Storage domain services
- ChangePlan architecture

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Nenhuma feature pode contornar o guard por UI direta.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- RISK_UNCLASSIFIED
- EVIDENCE_INSUFFICIENT
- DEPENDENCY_UNKNOWN
- CHANGE_BLOCKED

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
Versão das regras + decisions para audit log.

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
Testes unitários com matriz: safe/no-op/unsupported/high-risk/questionable mechanism; integração com pelo menos C-STORAGE-006/010/011.

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
Local planejado: `/prototypes/storage/c-storage-017/`

### Resultado
Policy contract can be proven unit/integration; prototype PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- optimize: https://learn.microsoft.com/en-us/powershell/module/storage/optimize-volume?view=windowsserver2025-ps
- writecache: https://learn.microsoft.com/en-us/windows-hardware/drivers/storage/querying-for-the-write-cache-property
- reserved_dism: https://learn.microsoft.com/en-us/windows-hardware/manufacture/desktop/dism-storage-reserve?view=windows-11

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.

<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — outcome/risk guard com operações reais e reason codes.
- Evidência realmente executada: NENHUMA.
- Execução segura neste host / teste: Sim para fixtures de baseline/after/unknown e bloqueio; sem Apply storage real.
- Impedimento ou limitação restante: Taxonomia global de risco/razões ainda depende da fase arquitetural posterior.
- Disposição: `DEFERRED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

## 32. Benefício real
Proven

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
Yes

### Como?
Taxa de decisions/rollback/verify failures como qualidade do produto; não como ganho de storage.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Internal; consumidor recebe reason/risk/confidence.

### Ações disponíveis
- N/A

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- decision
- risk
- reasonCodes
- evidenceQuality
- reversible
- measurementAvailable

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Taxonomia global de risk/reason codes deve alinhar com arquitetura fora do domínio.

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
