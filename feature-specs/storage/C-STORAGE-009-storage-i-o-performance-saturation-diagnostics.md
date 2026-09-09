# Feature Specification — C-STORAGE-009 — Storage I/O performance & saturation diagnostics

## 3. Identificação

Nome: Storage I/O performance & saturation diagnostics
ID: C-STORAGE-009
Tipo: Benchmark, Diagnostic, Monitoring
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
Medir throughput, IOPS, latency, queue e active time em runtime e distinguir carga normal de saturação contextual.

**Base conceitual do Discovery:** [58] Storage I/O Throughput Monitoring — `CANDIDATE`; [59] Storage I/O by Process — `CANDIDATE`; [60] Disk Active Time Analysis — `CANDIDATE`; [61] Storage Latency Monitoring — `CANDIDATE`; [62] High Storage Latency Detection — `CANDIDATE`; [63] Storage Queue Depth Monitoring — `CANDIDATE`; [64] Storage Saturation Detection — `CANDIDATE`; [71] Low Queue Depth Benchmark — `CANDIDATE`; [120] Storage Activity / Stutter Correlation — `CANDIDATE`

### Objetivo
Explicar quando storage está limitando uma workload real e quais processos estão gerando I/O.

### Problema que resolve
“100% disk” ou fila alta isoladamente não prova defeito; métricas precisam de taxa, latência, tipo de workload e device context.

### Benefício esperado
Performance / Diagnostic

## 5. Aplicabilidade

### Quando se aplica?
Volumes/discos com performance counters/ETW disponíveis.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não usar um threshold fixo universal de ms, queue depth ou active time.
- Não comparar NVMe e HDD pelo mesmo baseline.

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
Latência/queue/saturation anormal em relação ao baseline do próprio device/workload e correlação temporal com impacto do usuário.

### Motivo apresentado ao usuário
Mostrar quais métricas mudaram, qual workload/processo coincidiu e baseline utilizado.

### Grau de confiança
Medium/High quando múltiplos sinais convergem.

## 7. Estado atual

### O que precisa ser detectado?
- PhysicalDisk/LogicalDisk performance counters para bytes/sec, IOPS/latency/queue conforme disponíveis.
- ETW/kernel I/O para atribuição por processo e correlação de stutter.
- output de C-STORAGE-001 para mapear instância ao device.

### Como detectar?
Sampler com janela configurável e overhead medido; baseline robusto por device/workload.

### Fonte da verdade
Performance counters para device-level; ETW para process attribution. Baseline histórico do próprio sistema é preferível a número universal.

### Estados possíveis
- Idle/Normal load
- Busy but healthy
- Saturated
- Latency anomaly
- Process-heavy I/O
- Telemetry partial
- Unknown

## 8. Estado alvo
Diagnóstico; sem alteração.

## 9. Implementação técnica

### Método principal
Sampler com janela configurável e overhead medido; baseline robusto por device/workload.

### Tecnologias utilizadas
- [ ] .NET API
- [x] Win32
- [ ] Registry
- [ ] PowerShell
- [ ] CMD / executable
- [ ] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [x] Other

### Comandos / APIs / chaves
- Performance Counters (PhysicalDisk/LogicalDisk)
- ETW kernel disk/file I/O

### Alternativas avaliadas
Task Manager é referência visual, não API de backend.

### Abordagem escolhida
Counters + ETW fornecem níveis diferentes sem benchmark sintético.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Algumas sessões ETW/kernel podem exigir admin.

### O que acontece se não houver permissão?
Fallback para counters sem process detail.

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
Feature: C-STORAGE-009
Current state: resultado estruturado de Detect
Target state: Diagnóstico; sem alteração.
Changes: derivadas somente se a capability tiver Apply aprovado; caso contrário lista vazia
Admin required: Conditional
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
Diagnóstico; sem alteração.

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
- Overhead de coleta em frequência alta.
- Counters podem ser agregados em virtualização/RAID.

### Pior cenário plausível
Diagnóstico errado por mapping/counter reset; usar provenance e janelas.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Windows 10/11; counter availability depende de stack.

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
- Performance counter/ETW infrastructure

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Outros profilers ETW podem aumentar overhead.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- COUNTER_UNAVAILABLE
- ETW_ACCESS_DENIED
- DEVICE_MAPPING_FAILED

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
Optional

### O snapshot precisa sobreviver reboot?
No

### Arquivos/dados necessários
Downsampled time series; raw ETW não persiste por padrão.

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
Comparar counters com workload DiskSpd conhecido; validar mapping e overhead em idle/load.

### Rollback
N/A para feature somente leitura; testar que não existe path de mutação.

### VM
Safe

### Hardware real
Optional

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
Local planejado: `/prototypes/storage/c-storage-009/`

### Resultado
Metric sources documented; sampling calibration PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- performance: https://learn.microsoft.com/en-us/windows-server/storage/storage-spaces/performance-history-for-drives
- diskspd: https://github.com/microsoft/diskspd/wiki

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.

<!-- PHASE2-SAFE-PROOF-20260908:START -->
**Observed safe proof (auditoria final, 2026-09-08):**
- Capability/premissa exercitada: A localized PDH-backed physical-disk throughput counter can be sampled temporally.
- Resultado observado deste probe: `PASS`.
- Evidência reproduzível: `/prototypes/storage/C-STORAGE-001/results/storage-proof.json` (script e teste no mesmo prototype).
- Limites preservados: three read-only samples; no controlled I/O saturation
- Este resultado substitui `NOT_TESTED` somente para a premissa acima; não satisfaz por si só todos os cenários mínimos nem promove a Feature Spec a `PROVEN`/`APPROVED`.
<!-- PHASE2-SAFE-PROOF-20260908:END -->
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — I/O saturation com percentis/overhead.
- Evidência realmente executada: Prototype seguro adicional executado: A localized PDH-backed physical-disk throughput counter can be sampled temporally. Resultado observado: `PASS`. Evidência: `/prototypes/storage/C-STORAGE-001/results/storage-proof.json`. A evidência anterior foi substituída por esta observação mais recente.
- Execução segura neste host / teste: Sim — amostrar `PhysicalDisk(*)`/`LogicalDisk(*)` PerfCounters; opcional workload own temp-file limitado e cleanup serial.
- Impedimento ou limitação restante: Sem baseline sob carga real; ETW percentil versus counter agregados não decidido.
- Disposição: `SPECIFIED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

## 32. Benefício real
Proven

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
Yes

### Como?
Throughput, IOPS, average/percentile latency when source supports, queue and utilization over time.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Throughput
- Latency
- Queue/utilization
- Top I/O processes
- Saturation state
- Baseline comparison

### Ações disponíveis
- Monitor
- Details

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- samples[]
- saturationState
- topProcesses[]
- baseline
- confidence

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Padronização de percentis via ETW vs counters agregados.

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
