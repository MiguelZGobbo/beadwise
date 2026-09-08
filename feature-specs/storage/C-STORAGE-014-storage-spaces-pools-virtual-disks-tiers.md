# Feature Specification — C-STORAGE-014 — Storage Spaces, pools, virtual disks & tiers

## 3. Identificação

Nome: Storage Spaces, pools, virtual disks & tiers  
ID: C-STORAGE-014  
Tipo: Diagnostic, Maintenance, Repair Guidance  
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
Inventariar Storage Spaces/pools/virtual disks/tiers e oferecer maintenance guidance; mudanças estruturais RAID/pool ficam fora de automação agressiva.

**Base conceitual do Discovery:** [104] Storage Spaces Detection — `CANDIDATE`; [105] Storage Pool Health — `CANDIDATE`; [106] Virtual Disk Health — `CANDIDATE`; [108] Storage Pool Repair Guidance — `IDEA`; [109] Thin Provisioning Awareness — `CANDIDATE`; [110] Storage Tier Detection — `CANDIDATE`; [111] Tier Optimization — `CANDIDATE`; [151] RAID Health Context — `CANDIDATE`

### Objetivo
Detectar health, resiliency, thin provisioning, tiering e jobs para explicar degradação e ações nativas seguras.

### Problema que resolve
Storage Spaces possui camadas e estados próprios; tratar virtual disk como disco comum pode gerar diagnóstico e repair incorretos.

### Benefício esperado
Stability / Diagnostic

## 5. Aplicabilidade

### Quando se aplica?
Quando Windows Storage Spaces/Storage subsystem expõe pools/virtual disks.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não criar/remover pool, mudar resiliency ou RAID automaticamente.
- Repair guidance só vira Apply quando operação nativa e pré-condições estiverem claras.

## 5.1 Uso dentro do produto

### Consumidores
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

### Uso direto pelo usuário
Yes

### Capacidade compartilhada
N/A — o Discovery classifica `Shared Capability: No`. Outros consumidores podem reutilizar o output desta capability, mas o ownership permanece aqui e nenhuma infraestrutura compartilhada adicional é criada por esta spec.

## 6. Recomendação

### A feature deve ser recomendável automaticamente?
Conditional

### Critérios de recomendação
Pool/virtual disk health degraded, physical disk unhealthy, thin provisioning pressure, ou rebalance indicado após adição de capacidade.

### Motivo apresentado ao usuário
Explicar objeto afetado, resiliency e operação nativa sugerida; destacar I/O intensivo.

### Grau de confiança
High para HealthStatus/OperationalStatus; Medium para cause diagnosis.

## 7. Estado atual

### O que precisa ser detectado?
- Get-StoragePool
- Get-VirtualDisk
- Get-PhysicalDisk scoped to pool
- Get-StorageTier/StorageJob quando aplicável.

### Como detectar?
Modelar object graph pool→virtual disk→physical disk/tier e jobs; operações mutáveis têm ChangePlan próprio.

### Fonte da verdade
Storage Management provider state.

### Estados possíveis
- Healthy
- Degraded
- Warning
- Detached/Incomplete
- Thin provisioned pressure
- Optimization/rebalance candidate
- Unsupported

## 8. Estado alvo
Diagnóstico. Optimize-StoragePool/TierOptimize somente como manutenção explícita após preflight; repair estrutural permanece guidance inicialmente.

## 9. Implementação técnica

### Método principal
Modelar object graph pool→virtual disk→physical disk/tier e jobs; operações mutáveis têm ChangePlan próprio.

### Tecnologias utilizadas
- [ ] .NET API
- [ ] Win32
- [ ] Registry
- [x] PowerShell
- [ ] CMD / executable
- [x] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- Get-StoragePool
- Get-VirtualDisk
- Get-PhysicalDisk
- Optimize-StoragePool (conditional)
- Optimize-Volume -TierOptimize (conditional)

### Alternativas avaliadas
Storage Spaces UI é insuficiente para backend; CIM é canônico.

### Abordagem escolhida
Storage module corresponde ao provider do Windows.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Apply maintenance/repair requer admin; detect pode funcionar com subset.

### O que acontece se não houver permissão?
ADMIN_REQUIRED somente para ação.

## 11. Reinicialização

### Requer
- [ ] Nada
- [ ] Reinício do aplicativo
- [ ] Reinício de processo
- [ ] Reinício de serviço
- [ ] Logoff
- [ ] Reboot do Windows
- [ ] Desconhecido

**Nota:** Conditional

### A alteração só pode ser validada depois da reinicialização?
Normalmente não para optimize; repair específico TBD.

### Rollback também exige reinicialização?
TBD

## 12. Change Plan

```text
Feature: C-STORAGE-014
Current state: resultado estruturado de Detect
Target state: Diagnóstico. Optimize-StoragePool/TierOptimize somente como manutenção explícita após preflight; repair estrutural permanece guidance inicialmente.
Changes: derivadas somente se a capability tiver Apply aprovado; caso contrário lista vazia
Admin required: Conditional
Restart required: Conditional
Risk: Medium
Reversible: Partially
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
Yes

### O que precisa ser salvo antes da alteração?
Capturar todos os valores/estados que o ChangePlan pretende alterar, incluindo ausência de valor/objeto quando relevante, além do stable-id do alvo e timestamp.

### Estado inexistente também deve ser registrado
Yes — ausência é um estado válido do snapshot.

## 15. Apply

### Sequência de execução
1. Revalidar compatibilidade/preconditions.
2. Revalidar o ChangePlan imediatamente antes da execução.
3. Capturar snapshot necessário.
4. Executar somente as operações aprovadas.
5. Re-detectar estado.
6. Se Verify falhar, seguir estratégia de rollback/falha definida.

### Atomicidade
Cada operação mutável deve declarar se é independente. Em falha parcial, não continuar mudanças dependentes; restaurar mudanças já aplicadas quando rollback determinístico existir.

## 16. Verify

### Como comprovar que funcionou?
Re-detectar pela mesma fonte da verdade e comparar estado real com o target. Exit code 0 isolado nunca é suficiente.

### Critério de sucesso
Diagnóstico. Optimize-StoragePool/TierOptimize somente como manutenção explícita após preflight; repair estrutural permanece guidance inicialmente.

### Resultado parcial
Yes — quando a fonte da verdade expõe subset, quando uma camada abstrai hardware ou quando parte independente do plano falha de maneira representável. Resultado parcial nunca é promovido silenciosamente a sucesso total.

## 17. Rollback

### É reversível?
Partially

### Método de rollback
Partially. Restaurar exatamente o snapshot quando tecnicamente possível. Operações irreversíveis ou de reparo devem ser marcadas como tal e nunca fingir rollback por escrever “default”.

### Ordem de reversão
Para mudanças compostas, ordem inversa quando tecnicamente apropriado; N/A para diagnostics.

## 18. Verify Rollback

### Critério de sucesso
Re-detectar e comparar com snapshot original; divergência gera ROLLBACK_FAILED e orientação manual/escalonamento.

### O que fazer se rollback falhar?
Registrar `ROLLBACK_FAILED`, manter snapshot/audit log, permitir retry somente se seguro e fornecer orientação manual/System Restore apenas quando realmente aplicável.

## 19. System Restore

### A feature exige ponto de restauração?
Not Required

### Motivo
A decisão segue o risco real da operação. Diagnostics/guards não justificam restore point; operações com rollback próprio não devem usar System Restore como mecanismo primário.

## 20. Risco

### Classificação
Medium

### Possíveis efeitos negativos
- Rebalance é I/O intensive.
- Repair em pool degradado pode estressar discos.

### Pior cenário plausível
Falha adicional durante repair/rebalance em storage já degradado; bloquear ação sem health/capacity preflight.

### Recuperação manual possível?
Partial

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Storage Spaces em Windows 10/11; features/tiering variam por edição/configuração.

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
- output de C-STORAGE-001
- output de C-STORAGE-002
- Storage Management provider

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Vendor RAID != Storage Spaces; não misturar.

## 24. Idempotência
Conditional

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- POOL_DEGRADED
- INSUFFICIENT_CAPACITY
- STORAGE_JOB_FAILED
- ADMIN_REQUIRED
- VERIFY_FAILED

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
Yes

### Arquivos/dados necessários
Object graph e ChangePlan; snapshot de estados/allocations relevantes antes de maintenance.

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
VM com Storage Spaces virtual disks; criar pool de teste, observar healthy/degraded e optimize job; não usar dados reais.

### Rollback
Testar snapshot→apply→verify→rollback→verify rollback incluindo estado inicial personalizado e inexistente.

### VM
Safe

### Hardware real
Optional

## 29. Cenários mínimos de teste
- [ ] Feature já está no estado desejado
- [ ] Feature precisa ser alterada
- [ ] Feature não é suportada
- [ ] Falta permissão
- [ ] Apply falha
- [ ] Verify falha
- [ ] Rollback funciona
- [ ] Rollback falha
- [ ] Estado inicial personalizado
- [ ] Configuração inexistente
- [ ] Execução repetida

## 30. Prova técnica

### Script/protótipo
Local planejado: `/prototypes/storage/c-storage-014/`

### Resultado
Detection/Optimize behavior documented; repair scenarios PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- pool: https://learn.microsoft.com/en-us/powershell/module/storage/get-storagepool?view=windowsserver2025-ps
- virtualdisk: https://learn.microsoft.com/en-us/powershell/module/storage/get-virtualdisk?view=windowsserver2025-ps
- poolopt: https://learn.microsoft.com/en-us/powershell/module/storage/optimize-storagepool?view=windowsserver2025-ps
- optimize: https://learn.microsoft.com/en-us/powershell/module/storage/optimize-volume?view=windowsserver2025-ps

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior:** TBD até execução dos protótipos/testes.

## 32. Benefício real
Proven

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
Yes

### Como?
Health/operational state e job completion; perf before/after apenas se usuário solicitar benchmark.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Pool/virtual disk health
- Resiliency
- Capacity/provisioning
- Physical members
- Jobs
- Maintenance risk

### Ações disponíveis
- Details
- Optimize pool (conditional)
- Repair guidance

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- pools[]
- virtualDisks[]
- physicalMembers[]
- tiers[]
- jobs[]
- recommendedActions[]

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Quais repair cmdlets entram no escopo do produto e com quais safeguards por resiliency type?

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
