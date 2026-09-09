# Feature Specification — C-STORAGE-002 — Storage health, SMART/NVMe & wear diagnostics

## 3. Identificação

Nome: Storage health, SMART/NVMe & wear diagnostics
ID: C-STORAGE-002
Tipo: Diagnostic, Monitoring, Safety
Technical Domain: STORAGE
Primary Product Area: TBD
Also Used By: My PC, Monitoring, Optimization, Cleaning, Benchmark, Gaming
Shared Capability: No
Final UI Placement: TBD
Status: BLOCKED
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Coletar saúde e endurance de storage sem transformar atributos SMART vendor-specific em diagnósticos universais.

**Base conceitual do Discovery:** [6] Physical Disk Health Status — `CANDIDATE`; [7] Storage Reliability Counters — `CANDIDATE`; [8] SMART Capability Detection — `CANDIDATE`; [9] SMART Attribute Analysis — `CANDIDATE`; [10] SMART Warning Detection — `CANDIDATE`; [11] NVMe Health Information — `CANDIDATE`; [12] NVMe Wear Percentage — `CANDIDATE`; [13] SSD Remaining Life Estimate — `IDEA`; [17] Physical Error Counter Monitoring — `CANDIDATE`; [18] Storage Error Trend — `IDEA`; [19] Unsafe Shutdown Count — `CANDIDATE`; [20] Disk Power-On Hours — `CANDIDATE`; [21] Disk Power Cycle Count — `CANDIDATE`; [72] Storage Benchmark Wear Safeguard — `CANDIDATE`; [107] Storage Spaces Physical Disk Health — `CANDIDATE`; [115] Storage Event Log Analysis — `CANDIDATE`; [140] Host Reads / Writes Monitoring — `CANDIDATE`; [141] NVMe Controller Busy Time — `CANDIDATE`; [142] NVMe Critical Warning Detection — `CANDIDATE`; [143] NVMe Available Spare Monitoring — `CANDIDATE`; [144] NVMe Media/Data Integrity Error Trend — `CANDIDATE`; [193] Sustained Write Wear Budget — `CANDIDATE`

### Objetivo
Detectar warnings, desgaste, erros e tendências com semântica segura por protocolo, priorizando contadores normalizados do Windows e o NVMe SMART/Health log.

### Problema que resolve
Falhas de armazenamento podem se manifestar antes de uma perda total, mas métricas SMART brutas variam por fabricante e podem ser mal interpretadas.

### Benefício esperado
Stability / Diagnostic

## 5. Aplicabilidade

### Quando se aplica?
Discos para os quais Windows/driver expõe HealthStatus, reliability counters ou health log de protocolo.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não calcular “vida restante” a partir de atributos SMART não documentados pelo fabricante.
- Não afirmar falha iminente com base em um único contador sem semântica definida.
- Não executar escrita de teste destrutiva para validar saúde.

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
CriticalWarning != 0; HealthStatus unhealthy/warning; erros de mídia/dados aumentam; spare abaixo do threshold reportado; wear claramente elevado; eventos recorrentes correlacionados.

### Motivo apresentado ao usuário
Explicar qual sinal foi observado, origem e por que merece backup/diagnóstico; evitar “seu SSD vai morrer em X dias”.

### Grau de confiança
High para NVMe Critical Warning/Percentage Used e HealthStatus explícito; Medium para tendências; Low para atributos ATA vendor-specific sem tabela oficial.

## 7. Estado atual

### O que precisa ser detectado?
- Get-StorageReliabilityCounter para temperatura, erros, wear e tempo quando disponível.
- Get-PhysicalDisk HealthStatus/OperationalStatus.
- NVMe SMART/Health Information Log via IOCTL_STORAGE_QUERY_PROPERTY + NVMeDataTypeLogPage.
- Event Log/Storport apenas como evidência complementar de tendência.

### Como detectar?
Coletor somente leitura com adapters por protocolo. Armazenar séries temporais apenas para métricas estáveis e incluir source/confidence.

### Fonte da verdade
Ordem: protocolo/health log documentado e contadores normalizados do Windows; estado do Storage subsystem; eventos. SMART ATA vendor-specific só com definição autoritativa por modelo/vendor.

### Estados possíveis
- Healthy
- Warning
- Critical
- Degraded trend
- Unsupported
- Partial
- Unknown/Error

## 8. Estado alvo
Diagnóstico atualizado; sem mudança automática.

## 9. Implementação técnica

### Método principal
Coletor somente leitura com adapters por protocolo. Armazenar séries temporais apenas para métricas estáveis e incluir source/confidence.

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
- Get-StorageReliabilityCounter
- Get-PhysicalDisk
- IOCTL_STORAGE_QUERY_PROPERTY + NVMe SMART/Health Log

### Alternativas avaliadas
WMI MSStorageDriver_FailurePredict* é legado e inconsistente em NVMe/USB; pode ser fallback explícito.

### Abordagem escolhida
APIs Storage + protocolo NVMe documentado minimizam heurística.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Consulta protocol-specific a handle físico pode exigir elevação; CIM pode retornar subset sem admin.

### O que acontece se não houver permissão?
PARTIAL_DATA com motivo ADMIN_REQUIRED para métricas privilegiadas.

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
Feature: C-STORAGE-002
Current state: resultado estruturado de Detect
Target state: Diagnóstico atualizado; sem mudança automática.
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
Diagnóstico atualizado; sem mudança automática.

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
- Alarmes falsos se vendor SMART for generalizado.
- Leitura através de bridge pode ser indisponível ou traduzida.

### Pior cenário plausível
Falso senso de segurança ou falso alarme; UI deve exibir Unknown/Partial e não mascarar ausência de telemetria.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Windows 10/11; NVMe protocol queries exigem suporte de driver/transport; ATA/SATA variam.

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
- Storage Management API
- NVMe protocol-specific query

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- RAID/HBA/USB bridge pode ocultar SMART/health.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- HEALTH_UNSUPPORTED
- PROTOCOL_QUERY_FAILED
- PARTIAL_DATA
- ACCESS_DENIED

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
Histórico compacto por device stable-id: timestamp, metric, value, source, confidence.

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
Testar NVMe nativo e SATA/USB; comparar Get-StorageReliabilityCounter com NVMe log; validar ausência graciosa em dispositivo sem suporte.

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
Local planejado: `/prototypes/storage/c-storage-002/`

### Resultado
DETECT documentado; cross-device hardware validation PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- reliability: https://learn.microsoft.com/en-us/powershell/module/storage/get-storagereliabilitycounter?view=windowsserver2025-ps
- nvme: https://learn.microsoft.com/en-us/windows/win32/fileio/working-with-nvme-devices
- storport: https://learn.microsoft.com/en-us/windows-hardware/drivers/storage/storport-event-log-extensions

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior (campanha 2026-09-08):** probes compartilhados read-only executados neste host: `storage.inventory-and-health`=PASS. Evidência: `/prototypes/system/C-SYSTEM-017/results/domain-evidence.json`. Estes sinais são parciais e não satisfazem, sozinhos, o gate DETECT completo desta feature.

<!-- PHASE2-SAFE-PROOF-20260908:START -->
**Observed safe proof (auditoria final, 2026-09-08):**
- Capability/premissa exercitada: Reliability counters return measured fields or preserve access/unsupported failure per disk.
- Resultado observado deste probe: `FAIL`.
- Evidência reproduzível: `/prototypes/storage/C-STORAGE-001/results/storage-proof.json` (script e teste no mesmo prototype).
- Limites preservados: no time series or vendor ATA attribute map
- Este resultado substitui `NOT_TESTED` somente para a premissa acima; não satisfaz por si só todos os cenários mínimos nem promove a Feature Spec a `PROVEN`/`APPROVED`.
<!-- PHASE2-SAFE-PROOF-20260908:END -->
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — leitura real de reliability/SMART/NVMe é essencial para sustentar health/wear, e a fonte tentou executar.
- Evidência realmente executada: Prototype seguro adicional executado: Reliability counters return measured fields or preserve access/unsupported failure per disk. Resultado observado: `FAIL`. Evidência: `/prototypes/storage/C-STORAGE-001/results/storage-proof.json`. A evidência anterior foi substituída por esta observação mais recente.
- Execução segura neste host / teste: A leitura não elevada foi segura e executada; repetir com acesso ao provider exige ambiente/autoridade diferente.
- Impedimento ou limitação restante: Bloqueio objetivo atual da API/provider necessário; não há SMART/NVMe reliability válido neste host/sessão.
- Disposição: `BLOCKED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

## 32. Benefício real
Proven

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
Yes

### Como?
Tendência de contadores/health flags ao longo do tempo; não converter em expectativa de vida temporal sem modelo oficial.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Health summary
- Warnings
- Temperature (se disponível)
- Wear/percentage used
- Media/data errors
- Unsafe shutdowns
- Telemetry confidence

### Ações disponíveis
- Details
- Export diagnostics

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- healthState
- signals[]
- wearPercent
- temperatureC
- mediaErrors
- unsafeShutdowns
- source
- confidence
- lastUpdated

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Política de atributos ATA vendor-specific por fabricante.
- Retenção ideal de séries temporais sem banco inflado.

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
