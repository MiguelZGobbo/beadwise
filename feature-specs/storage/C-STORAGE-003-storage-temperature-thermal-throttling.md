# Feature Specification — C-STORAGE-003 — Storage temperature & thermal throttling

## 3. Identificação

Nome: Storage temperature & thermal throttling  
ID: C-STORAGE-003  
Tipo: Diagnostic, Monitoring, Recommendation  
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
Monitorar temperatura de storage e inferir throttling somente quando houver evidência observável, sem thresholds universais inventados.

**Base conceitual do Discovery:** [14] Storage Temperature Monitoring — `CANDIDATE`; [15] Storage Thermal Throttling Detection — `CANDIDATE`; [16] Storage Cooling Advisor — `IDEA`; [180] External Storage Thermal Analysis — `IDEA`

### Objetivo
Expor temperatura e contexto térmico; impedir benchmark agressivo quando o dispositivo já está quente; sinalizar possível thermal throttling por correlação.

### Problema que resolve
SSD/NVMe pode reduzir desempenho por proteção térmica, mas limite e comportamento variam por controlador e fabricante.

### Benefício esperado
Stability / Diagnostic

## 5. Aplicabilidade

### Quando se aplica?
Dispositivos com temperature property, reliability counter ou NVMe health log; throttling exige telemetria suficiente.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não usar 70°C/80°C ou qualquer valor fixo como “limite universal”.
- Não alterar thermal thresholds do dispositivo.
- Não atribuir toda queda de throughput a throttling térmico.

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
Conditional

### Critérios de recomendação
Temperatura excede threshold reportado pelo próprio dispositivo/driver, critical warning térmico, ou queda repetível de desempenho correlacionada com subida térmica e recuperação após cooldown.

### Motivo apresentado ao usuário
Mostrar temperatura, threshold de origem quando houver, e evidência de correlação; recomendar fluxo de ar/cooldown somente como orientação.

### Grau de confiança
High quando device reporta warning/threshold; Medium para correlação controlada; Low para temperatura isolada.

## 7. Estado atual

### O que precisa ser detectado?
- STORAGE_TEMPERATURE_DATA_DESCRIPTOR via IOCTL_STORAGE_QUERY_PROPERTY.
- Get-StorageReliabilityCounter Temperature quando disponível.
- NVMe SMART/Health log temperature e warning fields.
- Durante benchmark, amostrar temperatura e throughput/latency para correlação.

### Como detectar?
Sensor read-only com sample interval adaptativo durante monitoring/benchmark; throttling é uma classificação por evidência, não por temperatura fixa.

### Fonte da verdade
Thresholds e warnings reportados pelo dispositivo/protocolo têm precedência; sem threshold, apenas reportar temperatura absoluta sem rotulá-la “alta” universalmente.

### Estados possíveis
- Normal (per device threshold)
- Warning
- Critical warning
- Possible throttling
- Confirmed-by-test throttling
- Unsupported
- Unknown

## 8. Estado alvo
Diagnóstico/guard; nenhuma alteração persistente.

## 9. Implementação técnica

### Método principal
Sensor read-only com sample interval adaptativo durante monitoring/benchmark; throttling é uma classificação por evidência, não por temperatura fixa.

### Tecnologias utilizadas
- [ ] .NET API
- [x] Win32
- [ ] Registry
- [ ] PowerShell
- [ ] CMD / executable
- [x] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- StorageDeviceTemperatureProperty
- Get-StorageReliabilityCounter
- NVMe SMART/Health log

### Alternativas avaliadas
Vendor tools podem expor sensores extras, mas não podem ser dependência universal.

### Abordagem escolhida
Windows/protocol telemetry primeiro; vendor API como adapter opcional futuro.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Dependendo do handle/protocol query.

### O que acontece se não houver permissão?
Expor temperatura alternativa se disponível; senão ADMIN_REQUIRED/PARTIAL_DATA.

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
Feature: C-STORAGE-003
Current state: resultado estruturado de Detect
Target state: Diagnóstico/guard; nenhuma alteração persistente.
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
Diagnóstico/guard; nenhuma alteração persistente.

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
- Polling excessivo pode gerar overhead mínimo.
- Sensor pode representar controlador e não NAND.

### Pior cenário plausível
Falso throttling; mitigado por confidence e exigência de correlação.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Windows 10/11; varia por NVMe/SATA/USB bridge.

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
- Benchmark infrastructure para confirmação opcional

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Ferramentas vendor podem mostrar sensor diferente.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- TEMPERATURE_UNSUPPORTED
- SENSOR_AMBIGUOUS
- PARTIAL_DATA

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
Séries temporais curtas durante sessão/benchmark; histórico longo opcional.

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
Amostragem em NVMe com sensor conhecido e teste de carga controlado; verificar que classificação não dispara apenas por valor absoluto.

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
Local planejado: `/prototypes/storage/c-storage-003/`

### Resultado
DETECT documentado; thermal-throttling correlation test PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- nvme: https://learn.microsoft.com/en-us/windows/win32/fileio/working-with-nvme-devices
- reliability: https://learn.microsoft.com/en-us/powershell/module/storage/get-storagereliabilitycounter?view=windowsserver2025-ps

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.

## 32. Benefício real
Reasonable

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
Yes

### Como?
Temperatura, throughput, latency e tempo de recuperação no mesmo ensaio.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Temperature
- Device threshold/warning when available
- Throttling confidence
- Cooling/cooldown guidance

### Ações disponíveis
- Details

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- temperatureC
- thresholds
- thermalState
- throttlingState
- confidence
- samples

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Quais campos térmicos são expostos por SATA/AHCI sem SMART vendor parsing?

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
