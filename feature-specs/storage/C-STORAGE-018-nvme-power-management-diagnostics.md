# Feature Specification — C-STORAGE-018 — NVMe power-management diagnostics

## 3. Identificação

Nome: NVMe power-management diagnostics  
ID: C-STORAGE-018  
Tipo: Advanced Configuration, Diagnostic, Diagnostic / Benchmark, Safety  
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
Diagnosticar power states e política NVMe/StorNVMe; APST tuning permanece QUESTIONABLE e não será aplicado como otimização.

**Base conceitual do Discovery:** [129] NVMe Power State Capability Detection — `CANDIDATE`; [130] NVMe Runtime Power Management Analysis — `CANDIDATE`; [131] NVMe APST Capability Detection — `CANDIDATE`; [132] NVMe APST State Detection — `RESEARCHING`; [133] NVMe APST Tuning — `QUESTIONABLE`; [134] NVMe Power-State Latency Analysis — `IDEA`; [135] NVMe Power Management Myth Guard — `CANDIDATE`

### Objetivo
Explicar trade-off energia/latência e detectar configuração/power capabilities relevantes sem forçar valores de timeout/latency tolerance.

### Problema que resolve
Tweaks de APST e power state usam valores arbitrários e podem aumentar consumo/temperatura ou causar incompatibilidades.

### Benefício esperado
Diagnostic / Energy

## 5. Aplicabilidade

### Quando se aplica?
NVMe sob StorNVMe ou stack que exponha power state capabilities; comportamento vendor driver pode divergir.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- NVMe APST Tuning permanece fora do Apply.
- Não alterar Primary/Secondary NVMe Idle Timeout ou transition latency tolerance automaticamente.
- Não desabilitar power management para “gaming latency” sem prova workload-specific.

## 5.1 Uso dentro do produto

### Consumidores
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

### Uso direto pelo usuário
Partial

### Capacidade compartilhada
N/A — o Discovery classifica `Shared Capability: No`. Outros consumidores podem reutilizar o output desta capability, mas o ownership permanece aqui e nenhuma infraestrutura compartilhada adicional é criada por esta spec.

## 6. Recomendação

### A feature deve ser recomendável automaticamente?
No

### Critérios de recomendação
N/A; somente diagnóstico/myth guard. Eventual tuning exige spec separada/retorno ao Discovery.

### Motivo apresentado ao usuário
Explicar que Windows escolhe estado usando latência de entrada/saída e policy; mais desempenho não significa manter NVMe sempre ativo.

### Grau de confiança
High para power settings do StorNVMe e Identify power state descriptors; Medium para estado runtime/APST semantics se não diretamente exposto.

## 7. Estado atual

### O que precisa ser detectado?
- NVMe Identify Controller/feature queries quando disponíveis.
- Power plan settings do StorNVMe (Primary/Secondary Idle Timeout e latency tolerance).
- Driver/controller identity para saber se StorNVMe policy se aplica.

### Como detectar?
Read-only protocol + powercfg/API inspection. Separar capability de controller de policy atual.

### Fonte da verdade
NVMe protocol capabilities + Windows power policy documentada; não inferir APST “enabled” de um Registry key não documentado.

### Estados possíveis
- StorNVMe managed
- Vendor-driver managed
- Power states available
- Policy values readable
- Runtime/APST state unknown
- Unsupported

## 8. Estado alvo
Diagnóstico; Apply N/A.

## 9. Implementação técnica

### Método principal
Read-only protocol + powercfg/API inspection. Separar capability de controller de policy atual.

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
- NVMe Identify/Get Features (read-only)
- PowerCfg/API read power settings

### Alternativas avaliadas
Registry tweaks online são rejeitados por fragilidade.

### Abordagem escolhida
Documentação StorNVMe + NVMe protocol.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Protocol query/power policy detail pode exigir admin.

### O que acontece se não houver permissão?
PARTIAL_DATA.

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
Feature: C-STORAGE-018
Current state: resultado estruturado de Detect
Target state: Diagnóstico; Apply N/A.
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
Diagnóstico; Apply N/A.

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
- Expor settings ocultos pode incentivar tuning manual perigoso; UI deve ser explainability-first.

### Pior cenário plausível
Usuário altera política por conta própria e causa instabilidade/consumo; não oferecer “one click”.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Windows 10/11; StorNVMe-specific settings não representam vendor drivers.

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

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- OEM/vendor NVMe drivers e Modern Standby policies.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- NON_STORNVME_DRIVER
- POWER_SETTING_UNAVAILABLE
- PROTOCOL_QUERY_FAILED

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
No

### O snapshot precisa sobreviver reboot?
No

### Arquivos/dados necessários
N/A

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
Testar StorNVMe em desktop/laptop; verificar leitura dos GUIDs e Identify power state descriptors; nenhum set.

### Rollback
N/A para feature somente leitura; testar que não existe path de mutação.

### VM
Not Suitable

### Hardware real
Required

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
Local planejado: `/prototypes/storage/c-storage-018/`

### Resultado
Power policy documented; runtime APST observability remains PENDING/limited.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- nvme_power: https://learn.microsoft.com/en-us/windows-hardware/design/component-guidelines/power-management-for-storage-hardware-devices-nvme
- disk_power: https://learn.microsoft.com/en-us/windows-hardware/customize/power-settings/disk-settings
- nvme: https://learn.microsoft.com/en-us/windows/win32/fileio/working-with-nvme-devices

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior:** TBD até execução dos protótipos/testes.

## 32. Benefício real
Reasonable

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
Partial

### Como?
Energy/latency only through controlled workload; diagnostics alone do not claim gain.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Driver
- Power-state capability
- Current policy
- Trade-off explanation
- Tuning not recommended automatically

### Ações disponíveis
- Details

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- driverType
- powerStates
- policy
- runtimeStateConfidence
- mythGuard

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Existe API documentada confiável para estado APST runtime em Windows além de feature/protocol query e driver behavior?

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
