# Feature Specification — C-STORAGE-001 — Storage hardware inventory & interface context

## 3. Identificação

Nome: Storage hardware inventory & interface context
ID: C-STORAGE-001
Tipo: Diagnostic, Diagnostic / Recommendation
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
Inventariar discos físicos, volumes e contexto de interface/controlador de forma canônica, sem inferir desempenho apenas pelo nome comercial do dispositivo.

**Base conceitual do Discovery:** [1] Physical Storage Inventory — `CANDIDATE`; [2] Storage Media Type Detection — `CANDIDATE`; [3] Storage Bus Type Detection — `CANDIDATE`; [4] Storage Model / Firmware Detection — `CANDIDATE`; [5] Storage Firmware Currency Check — `IDEA`; [50] SATA Link Detection — `CANDIDATE`; [51] SATA Link Bottleneck Detection — `IDEA`; [52] NVMe PCIe Link Detection — `CANDIDATE`; [55] Storage Controller Identification — `CANDIDATE`; [145] Storage Controller Stack Detection — `CANDIDATE`; [146] Intel VMD Detection — `CANDIDATE`; [147] Intel RST Storage Context — `CANDIDATE`; [148] VMD/RST Driver Health — `CANDIDATE`; [150] RAID Configuration Detection — `CANDIDATE`; [175] External Storage Transport Detection — `CANDIDATE`; [176] USB Storage Negotiated Speed — `CANDIDATE`; [177] UASP Capability Detection — `RESEARCHING`; [179] USB Storage Bridge Context — `IDEA`

### Objetivo
Fornecer uma identidade estável e capacidades observáveis de cada dispositivo de armazenamento para que diagnósticos e recomendações posteriores usem o mesmo mapa físico/lógico.

### Problema que resolve
Sem correlação entre disco físico, barramento, controlador, volume e caminho externo, outras features podem atribuir métricas, riscos ou recomendações ao dispositivo errado.

### Benefício esperado
Diagnostic / Compatibility

## 5. Aplicabilidade

### Quando se aplica?
Windows 10/11 x64 e ARM64 quando APIs Storage/CIM retornarem objetos; detalhes NVMe/ATA dependem de suporte do driver e do transporte.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não inferir velocidade negociada PCIe/SATA quando a pilha não expuser dado confiável.
- Não declarar firmware desatualizado sem catálogo autoritativo do fabricante.
- Em RAID/VMD/RST, reportar o controlador/virtualização e marcar visibilidade física parcial quando a camada esconder o dispositivo real.

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
N/A — é infraestrutura diagnóstica; consumidores podem gerar recomendações.

### Motivo apresentado ao usuário
N/A

### Grau de confiança
High para propriedades retornadas pela API; Medium/Low para inferências de topologia parcial.

## 7. Estado atual

### O que precisa ser detectado?
- Enumerar MSFT_PhysicalDisk/Get-PhysicalDisk e MSFT_Disk/Get-Disk.
- Correlacionar partições/volumes por Number, UniqueId, DeviceId e caminhos de dispositivo.
- Consultar IOCTL_STORAGE_QUERY_PROPERTY para STORAGE_DEVICE_DESCRIPTOR / adapter/device properties quando detalhes de barramento/protocolo forem necessários.
- Para NVMe, usar protocol-specific Identify somente quando o driver permitir.

### Como detectar?
Implementar um coletor .NET/Win32 que normalize objetos Storage Management e complemente campos ausentes com IOCTLs documentados. Cada campo carrega provenance e confidence.

### Fonte da verdade
Primária: Windows Storage Management API/CIM para inventário lógico/físico. Secundária: IOCTL_STORAGE_QUERY_PROPERTY para propriedades de dispositivo/protocolo. Dados de fabricante externos nunca substituem a identidade reportada pelo sistema.

### Estados possíveis
- Supported + complete
- Supported + partial
- Virtualized/abstracted by RAID/VMD/RST
- External bridge with limited visibility
- Unsupported property
- Unknown/Error

## 8. Estado alvo
Snapshot somente de leitura e internamente consistente; nenhuma alteração de storage.

## 9. Implementação técnica

### Método principal
Implementar um coletor .NET/Win32 que normalize objetos Storage Management e complemente campos ausentes com IOCTLs documentados. Cada campo carrega provenance e confidence.

### Tecnologias utilizadas
- [x] .NET API
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
- Get-PhysicalDisk
- Get-Disk / Get-Partition / Get-Volume
- IOCTL_STORAGE_QUERY_PROPERTY
- NVMe Identify via protocol-specific query (conditional)

### Alternativas avaliadas
PowerShell é útil para protótipo, mas não deve ser a única fonte em runtime. WMI legado Win32_DiskDrive é fallback, não fonte canônica.

### Abordagem escolhida
Storage Management + IOCTLs documentados preservam melhor identidade, suporte moderno e detalhes de protocolo.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Abrir alguns handles físicos/protocol-specific pode exigir privilégios; inventário CIM básico normalmente deve funcionar sem elevação.

### O que acontece se não houver permissão?
Retornar PARTIAL_DATA/ACCESS_DENIED por campo, não falhar o inventário inteiro.

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
Feature: C-STORAGE-001
Current state: resultado estruturado de Detect
Target state: Snapshot somente de leitura e internamente consistente; nenhuma alteração de storage.
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
Snapshot somente de leitura e internamente consistente; nenhuma alteração de storage.

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
- Mapeamento incorreto entre camadas RAID/bridge.
- MediaType pode ser Unknown ou fornecido por camada intermediária.

### Pior cenário plausível
Recomendação posterior aplicada ao disco errado se a correlação for defeituosa; por isso UniqueId/DeviceId e provenance são obrigatórios.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Windows 10/11; detalhes variam por driver, RAID, VMD, USB bridge e Storage Spaces.

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
- Windows Storage Management API/CIM
- SetupAPI/Win32 device handles (quando necessário)
- Driver de storage que exponha a propriedade

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Software de RAID/VMD pode abstrair discos físicos.
- Bridges USB podem mascarar protocolo/modelo/SMART.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- PARTIAL_DATA
- DEVICE_NOT_MAPPED
- PROPERTY_UNSUPPORTED
- ACCESS_DENIED
- DRIVER_ABSTRACTION

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
Cache de inventário versionado com timestamp/provenance; sem dados sensíveis além de identificadores técnicos necessários.

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
Protótipo deve comparar correlação CIM/IOCTL em SATA AHCI, NVMe nativo, USB-UASP e ao menos um cenário RAID/VMD quando disponível.

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
Local planejado: `/prototypes/storage/c-storage-001/`

### Resultado
DETECT/PLAN/DRY-RUN: documentação suficiente; hardware matrix: PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- physicaldisk: https://learn.microsoft.com/en-us/powershell/module/storage/get-physicaldisk?view=windowsserver2025-ps
- ioctl: https://learn.microsoft.com/en-us/windows/win32/api/winioctl/ni-winioctl-ioctl_storage_query_property
- nvme: https://learn.microsoft.com/en-us/windows/win32/fileio/working-with-nvme-devices

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior (campanha 2026-09-08):** probes compartilhados read-only executados neste host: `storage.inventory-and-health`=PASS. Evidência: `/prototypes/system/C-SYSTEM-017/results/domain-evidence.json`. Estes sinais são parciais e não satisfazem, sozinhos, o gate DETECT completo desta feature.

<!-- PHASE2-SAFE-PROOF-20260908:START -->
**Observed safe proof (auditoria final, 2026-09-08):**
- Capability/premissa exercitada: Storage hardware, media, bus and volume identity are available from structured Storage cmdlets.
- Resultado observado deste probe: `PASS`.
- Evidência reproduzível: `/prototypes/storage/C-STORAGE-001/results/storage-proof.json` (script e teste no mesmo prototype).
- Limites preservados: no negotiated PCIe speed or cross-vendor firmware catalogue
- Este resultado substitui `NOT_TESTED` somente para a premissa acima; não satisfaz por si só todos os cenários mínimos nem promove a Feature Spec a `PROVEN`/`APPROVED`.
<!-- PHASE2-SAFE-PROOF-20260908:END -->
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — inventory/PnP/firmware/link provenance.
- Evidência realmente executada: Prototype seguro adicional executado: Storage hardware, media, bus and volume identity are available from structured Storage cmdlets. Resultado observado: `PASS`. Evidência: `/prototypes/storage/C-STORAGE-001/results/storage-proof.json`. A evidência anterior foi substituída por esta observação mais recente.
- Execução segura neste host / teste: Sim — `Get-PhysicalDisk/Get-Disk/Get-PnpDevice` e `Get-StorageReliabilityCounter` quando suportado.
- Impedimento ou limitação restante: PCIe negotiated speed e firmware catalogue cross-vendor ainda sem fonte.
- Disposição: `SPECIFIED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

## 32. Benefício real
Proven

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
No

### Como?
N/A; qualidade é medida por completude/correlação, não performance.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Modelo
- Capacidade
- Media type
- Bus/transport
- Firmware
- Controlador
- Volumes associados
- Confidence/partial visibility

### Ações disponíveis
- Details

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- storageDeviceId
- model
- firmware
- mediaType
- busType
- controller
- capacityBytes
- volumes[]
- visibility
- confidence
- provenance

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Como obter PCIe negotiated width/speed de forma confiável em todos os controladores?
- Quais vendors expõem firmware catalogável sem scraper frágil?

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
