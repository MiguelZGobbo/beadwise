# Feature Specification — C-STORAGE-008 — Storage driver, controller & link health

## 3. Identificação

Nome: Storage driver, controller & link health
ID: C-STORAGE-008
Tipo: Diagnostic, Diagnostic / Guidance
Technical Domain: STORAGE
Primary Product Area: TBD
Also Used By: My PC, Monitoring, Optimization, Cleaning, Benchmark, Gaming
Shared Capability: No
Final UI Placement: TBD
Status: RESEARCH
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Correlacionar driver/controlador/link com timeouts, resets, surprise removal e gargalos de interface sem diagnosticar hardware por um único evento.

**Base conceitual do Discovery:** [53] NVMe Link Bottleneck Detection — `CANDIDATE`; [54] NVMe Shared-Lane / Platform Constraint Context — `IDEA`; [56] Storage Driver Version Detection — `CANDIDATE`; [57] Storage Driver Problem Detection — `CANDIDATE`; [116] Storage Timeout Detection — `CANDIDATE`; [117] Surprise Removal Detection — `CANDIDATE`; [118] SATA Cable / Link Error Advisor — `IDEA`; [119] Storage Driver Reset Correlation — `IDEA`; [178] External SSD Interface Bottleneck — `CANDIDATE`

### Objetivo
Identificar falhas persistentes na pilha de storage e contexto de link que expliquem stutter, disconnects ou desempenho incompatível.

### Problema que resolve
Erros de cabo, bridge, driver, controller ou link podem parecer “SSD lento” e levar a otimizações erradas.

### Benefício esperado
Stability / Diagnostic

## 5. Aplicabilidade

### Quando se aplica?
Dispositivos locais e externos com driver/event telemetry disponível.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não afirmar “cabo SATA ruim” apenas por baixo desempenho.
- Não atualizar driver automaticamente nesta capability.
- Não inferir lane-sharing sem topologia PCIe verificável.

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
Eventos repetidos de timeout/reset/surprise removal correlacionados ao mesmo device/controller, ou link observavelmente abaixo de capacidade em contexto suportado.

### Motivo apresentado ao usuário
Apresentar sequência temporal e camada provável (device/controller/transport), com confidence.

### Grau de confiança
Medium por padrão; High apenas quando driver/event explicitamente identifica falha.

## 7. Estado atual

### O que precisa ser detectado?
- Inventário de controller/driver version.
- Event Log/Storport channels para falhas/reset/timeout.
- Pnp/SetupAPI para surprise removal/device state.
- Interface details somente onde APIs documentadas expõem.

### Como detectar?
Event correlation service por stable device/controller ID e janela temporal; regras exigem repetição/contexto.

### Fonte da verdade
Eventos do storage stack + PnP state; benchmark isolado é evidência auxiliar, não causa.

### Estados possíveis
- Healthy
- Repeated timeout/reset
- Surprise removal
- Driver issue
- Possible link bottleneck
- Partial visibility
- Unknown

## 8. Estado alvo
Diagnóstico e guidance; sem Apply.

## 9. Implementação técnica

### Método principal
Event correlation service por stable device/controller ID e janela temporal; regras exigem repetição/contexto.

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
- [x] Other

### Comandos / APIs / chaves
- Windows Event Log / Storport channels
- PnP device properties

### Alternativas avaliadas
Vendor diagnostics como complemento opcional.

### Abordagem escolhida
Telemetria nativa e correlação reduzem falsa causalidade.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Alguns logs/properties podem exigir privilégios.

### O que acontece se não houver permissão?
Expor subset e PARTIAL_DATA.

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
Feature: C-STORAGE-008
Current state: resultado estruturado de Detect
Target state: Diagnóstico e guidance; sem Apply.
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
Diagnóstico e guidance; sem Apply.

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
- Falso positivo por eventos transitórios durante sleep/update.

### Pior cenário plausível
Usuário troca cabo/driver sem necessidade; recommendation deve ser guidance com confidence.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Windows 10/11; Storport-specific evidence depende da pilha usada.

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
- Event Log access

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Vendor RAID drivers usam providers/event IDs próprios.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- EVENT_SOURCE_UNAVAILABLE
- DEVICE_CORRELATION_FAILED
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
Yes

### O snapshot precisa sobreviver reboot?
No

### Arquivos/dados necessários
Eventos agregados/counters; não duplicar log bruto indefinidamente.

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
Induzir desconexão segura em storage externo de teste e verificar correlação; usar logs históricos para timeout sem causar falha destrutiva.

### Rollback
N/A para feature somente leitura; testar que não existe path de mutação.

### VM
Partial

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
Local planejado: `/prototypes/storage/c-storage-008/`

### Resultado
Event mechanisms documented; correlation validation PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- storport: https://learn.microsoft.com/en-us/windows-hardware/drivers/storage/storport-event-log-extensions
- physicaldisk: https://learn.microsoft.com/en-us/powershell/module/storage/get-physicaldisk?view=windowsserver2025-ps

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior (campanha 2026-09-08):** probes compartilhados read-only executados neste host: `storage.inventory-and-health`=PASS. Evidência: `/prototypes/system/C-SYSTEM-017/results/domain-evidence.json`. Estes sinais são parciais e não satisfazem, sozinhos, o gate DETECT completo desta feature.

<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — controller/driver/link e eventos.
- Evidência realmente executada: E:storage.inventory-and-health PASS (parcial).
- Execução segura neste host / teste: Sim — correlacionar disks/PnP/`Win32_PnPSignedDriver` e Event Log.
- Impedimento ou limitação restante: Catálogo robusto StorAHCI/StorNVMe/vendor event IDs não definido.
- Disposição: `RESEARCH`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

<!-- PHASE2-FINAL-RESEARCH-AUDIT:START -->
**Reavaliação final de RESEARCH (2026-09-08):**
- O status `RESEARCH` foi revisto e preservado porque ainda há incerteza técnica solucionável descrita nas seções 30, 31 e 36; documentação ou probe compartilhado parcial não foi convertido em `PASS`.
- Nenhuma prova prática isolada nesta máquina elimina essa incerteza sem antes definir fonte, contrato, fixture, hardware ou dependência indicada pela própria spec.
- Próximo gate: concluir a investigação registrada, então decidir se cabe prototype seguro, `SPECIFIED`, `BLOCKED`, `DEFERRED` ou `REJECTED`; não promover diretamente a `PROVEN`/`APPROVED`.
<!-- PHASE2-FINAL-RESEARCH-AUDIT:END -->

## 32. Benefício real
Reasonable

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
Yes

### Como?
Contagem/taxa de eventos por device e janela; latency/throughput apenas contexto.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Driver/controller
- Recent errors
- Reset/timeout trend
- Transport context
- Confidence
- Suggested checks

### Ações disponíveis
- Details
- Open troubleshooting

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- driver
- controller
- events[]
- healthState
- confidence
- guidance

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Catálogo robusto de event IDs/provider por StorAHCI/StorNVMe/vendor drivers.

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
