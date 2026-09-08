# Feature Specification — C-STORAGE-010 — Storage benchmark suite & methodology

## 3. Identificação

Nome: Storage benchmark suite & methodology  
ID: C-STORAGE-010  
Tipo: Benchmark, Benchmark / Diagnostic, Benchmark Infrastructure, Benchmark Safeguard, Diagnostic, Product Behavior, Safety, Validation  
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
Definir uma suíte sintética reproduzível com DiskSpd, perfis separados por objetivo e safeguards de desgaste, espaço, cache, temperatura e background I/O.

**Base conceitual do Discovery:** [65] Sequential Read Benchmark — `CANDIDATE`; [66] Sequential Write Benchmark — `CANDIDATE`; [67] Random Read Benchmark — `CANDIDATE`; [68] Random Write Benchmark — `CANDIDATE`; [69] Storage Latency Benchmark — `CANDIDATE`; [70] Queue-Depth Benchmark — `IDEA`; [73] Storage Benchmark Thermal Readiness — `CANDIDATE`; [74] Sustained Storage Performance Test — `CANDIDATE`; [75] SSD Cache Exhaustion Detection — `CANDIDATE`; [76] SSD Sustained Write Characterization — `IDEA`; [77] Storage Benchmark Before × After — `CANDIDATE`; [78] Storage Performance Baseline vs Expected — `IDEA`; [123] Storage Benchmark During Background I/O Guard — `CANDIDATE`; [124] Storage Benchmark Free-Space Safeguard — `CANDIDATE`; [125] Storage Benchmark File Cleanup — `CANDIDATE`; [126] Storage Benchmark Result Interpretation — `CANDIDATE`; [185] Storage Benchmark Cache-Control Strategy — `CANDIDATE`; [186] Buffered vs Unbuffered Storage Benchmark — `CANDIDATE`; [187] Storage Benchmark Test-File Sizing — `CANDIDATE`; [188] Compressibility-Safe Benchmark Data — `CANDIDATE`; [189] Storage Benchmark Alignment — `CANDIDATE`; [190] Storage Benchmark Queue Profile — `CANDIDATE`; [191] Desktop Storage Responsiveness Profile — `CANDIDATE`; [192] Game Asset Streaming Storage Profile — `IDEA`; [194] Benchmark Thermal Recovery — `CANDIDATE`; [195] Read-Only Storage Benchmark Mode — `CANDIDATE`; [196] Storage Benchmark Result Confidence — `CANDIDATE`

### Objetivo
Medir características de storage sem esconder parâmetros, permitindo before/after e confidence score comparável no mesmo sistema.

### Problema que resolve
Benchmarks com queue depth, cache ou test file inadequados produzem números bonitos mas pouco representativos e podem escrever demais em SSD.

### Benefício esperado
Performance / Diagnostic

## 5. Aplicabilidade

### Quando se aplica?
Volumes locais com espaço suficiente e saúde/temperatura aceitáveis; write tests são opt-in e condicionais.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não executar write benchmark automaticamente.
- Não usar arquivo de teste minúsculo que caiba totalmente em cache quando objetivo é storage.
- Não comparar resultados de perfis/versões diferentes como equivalentes.
- Não testar raw physical disk com escrita em disco que contém dados.

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
No

### Critérios de recomendação
N/A; benchmark é ação explícita ou etapa de validação de outra feature.

### Motivo apresentado ao usuário
Explicar perfil escolhido e custo/risco.

### Grau de confiança
High apenas quando preflight passa e execução é reproduzível; degradar por thermal/background/cache/space anomalies.

## 7. Estado atual

### O que precisa ser detectado?
- Preflight: inventory, health, free-space, BitLocker transition, temperature, background I/O.
- DiskSpd version pinned e perfil XML/CLI versionado.
- Capturar parâmetros exatos e system metadata.

### Como detectar?
DiskSpd oficial como engine. Perfis separados: read-only responsiveness; sequential read; random read; opt-in write; sustained write apenas avançado com wear budget explícito. Test-file sizing é adaptativo ao objetivo/available space e registrado, não magic constant global.

### Fonte da verdade
DiskSpd output + perfil versionado + telemetry contemporânea. Resultado não representa automaticamente workload real.

### Estados possíveis
- Ready
- Blocked by health
- Blocked by space
- Blocked by encryption transition
- Contaminated by background I/O
- Thermal risk
- Running
- Valid result
- Low-confidence result
- Failed

## 8. Estado alvo
Produzir resultado com parâmetros, métricas, confidence e cleanup verificado; nenhum arquivo residual.

## 9. Implementação técnica

### Método principal
DiskSpd oficial como engine. Perfis separados: read-only responsiveness; sequential read; random read; opt-in write; sustained write apenas avançado com wear budget explícito. Test-file sizing é adaptativo ao objetivo/available space e registrado, não magic constant global.

### Tecnologias utilizadas
- [ ] .NET API
- [ ] Win32
- [ ] Registry
- [ ] PowerShell
- [x] CMD / executable
- [ ] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [x] Other

### Comandos / APIs / chaves
- DiskSpd (version-pinned)
- XML profile support / text or XML result parsing

### Alternativas avaliadas
Implementar benchmark próprio em .NET aumentaria risco de erro de I/O, cache e alinhamento. CrystalDiskMark não oferece contrato de automação/redistribuição necessariamente adequado.

### Abordagem escolhida
DiskSpd é ferramenta Microsoft especializada, configurável e com output automatizável.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Read file target pode não exigir; raw/advanced options podem. Produto deve evitar raw writes.

### O que acontece se não houver permissão?
Usar perfil permitido ou ADMIN_REQUIRED; nunca elevar silenciosamente.

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
Feature: C-STORAGE-010
Current state: resultado estruturado de Detect
Target state: Produzir resultado com parâmetros, métricas, confidence e cleanup verificado; nenhum arquivo residual.
Changes: derivadas somente se a capability tiver Apply aprovado; caso contrário lista vazia
Admin required: Conditional
Restart required: Nada
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
Produzir resultado com parâmetros, métricas, confidence e cleanup verificado; nenhum arquivo residual.

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
- Wear por escrita.
- Ocupação temporária de espaço.
- Thermal throttling.
- Impacto de responsividade.

### Pior cenário plausível
Disco degradado falha sob workload ou arquivo de teste ocupa espaço crítico; preflight bloqueia e write profiles exigem consentimento.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Windows 10/11 x64/ARM64 dependendo de binário DiskSpd disponível; validar distribuição/licença e arquitetura.

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
- output de C-STORAGE-002
- output de C-STORAGE-003
- output de C-STORAGE-004
- output de C-STORAGE-009
- BitLocker context

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Antivírus/indexer/cloud sync podem contaminar resultado.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- PREFLIGHT_BLOCKED
- INSUFFICIENT_SPACE
- THERMAL_BLOCK
- BACKGROUND_IO_HIGH
- BENCHMARK_FAILED
- RESULT_PARSE_FAILED
- CLEANUP_FAILED

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
Perfil/version, target stable-id, file size, cache flags, block size, queue/thread config, timings, metrics, confidence, test-file cleanup state.

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
Protótipo com DiskSpd pinned; validar cleanup após falha, buffered vs unbuffered profile, thermal guard, read-only mode e before/after same profile.

### Rollback
Testar snapshot→apply→verify→rollback→verify rollback incluindo estado inicial personalizado e inexistente.

### VM
Partial

### Hardware real
Required

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
Local planejado: `/prototypes/storage/c-storage-010/`

### Resultado
Engine behavior documented; product profile calibration PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- diskspd: https://github.com/microsoft/diskspd/wiki
- diskspd_params: https://github.com/microsoft/diskspd/wiki/Command-line-and-parameters

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
MB/s, IOPS, latency distribution/percentiles quando habilitado, CPU, thermal/background context.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Profile
- Read/write risk
- Test size
- Expected impact
- Progress
- Metrics
- Confidence
- Conditions that affected result

### Ações disponíveis
- Run
- Cancel
- Details
- Compare

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- profileId
- profileVersion
- preflight
- metrics
- confidence
- context
- cleanupStatus
- comparison

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Perfis finais e parâmetros só podem ser congelados após calibração em HDD/SATA SSD/NVMe de classes diferentes.
- Verificar redistribuição/licenciamento do DiskSpd no produto final.

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
