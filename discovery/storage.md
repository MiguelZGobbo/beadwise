# Discovery Catalog — Armazenamento
## Primeira passada — levantamento amplo

> Área dedicada a dispositivos de armazenamento, volumes, filesystems, integridade, performance, manutenção e diagnóstico.
>
> Regra central:
>
> **HDD, SATA SSD, NVMe, Storage Spaces e dispositivos virtuais não devem receber o mesmo tratamento.**
>
> Antes de qualquer ação:
>
> **detectar tipo → capacidade → interface → saúde → volume → filesystem → workload → só então recomendar.**

---

# 1. Physical Storage Inventory

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Hardware

Primary Product Area:
My PC

Possible Consumers:
- Optimization
- Benchmark
- Repair

Final UI Placement:
TBD

Descrição:
Inventariar todos os dispositivos físicos de armazenamento visíveis ao Windows.

Possível objetivo:
Criar mapa confiável dos discos da máquina.

Possível benefício:
Base para todas as capacidades posteriores.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

Possíveis dados:
- model;
- manufacturer;
- serial;
- firmware;
- capacity;
- media type;
- bus type;
- health;
- operational status.

---

# 2. Storage Media Type Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Hardware

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Benchmark
- Maintenance

Final UI Placement:
TBD

Descrição:
Classificar corretamente cada dispositivo.

Possíveis tipos:
- HDD;
- SATA SSD;
- NVMe SSD;
- USB storage;
- SD;
- Storage Space;
- virtual disk;
- unknown.

Possível objetivo:
Evitar manutenção inadequada.

Possível benefício:
Fundamental para Optimize/Defrag/TRIM.

Evidence Level:
Established

Action Mode:
Automatic

---

# 3. Storage Bus Type Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Hardware

Primary Product Area:
My PC

Possible Consumers:
- Benchmark
- Performance Diagnostics

Descrição:
Detectar barramento/interface utilizado.

Possíveis exemplos:
- SATA;
- NVMe;
- SAS;
- USB;
- RAID;
- Storage Spaces;
- virtual.

Possível objetivo:
Dar contexto à velocidade máxima e comportamento esperado.

Evidence Level:
Established

Action Mode:
Automatic

---

# 4. Storage Model / Firmware Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Hardware

Primary Product Area:
My PC

Possible Consumers:
- Drivers
- Firmware
- Repair

Descrição:
Identificar modelo e firmware do dispositivo.

Possível objetivo:
Permitir diagnósticos específicos e futuras verificações de firmware.

Possível benefício:
Troubleshooting e compatibilidade.

Evidence Level:
Established

Action Mode:
Automatic

---

# 5. Storage Firmware Currency Check

Status:
IDEA

Type:
Diagnostic / Recommendation

Technical Domain:
Storage Firmware

Primary Product Area:
My PC

Possible Consumers:
- Repair
- Drivers

Descrição:
Comparar firmware instalado com versões conhecidas do fabricante quando houver fonte confiável.

Possível objetivo:
Detectar correções de estabilidade ou compatibilidade.

Possível benefício:
Resolver problemas que software tweaks não corrigem.

Aplicabilidade:
Specific Drives

Risco inicial:
Medium

Evidence Level:
Requires vendor data

Action Mode:
Manual Guidance

---

# 6. Physical Disk Health Status

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Reliability

Primary Product Area:
My PC

Possible Consumers:
- Repair
- Monitoring

Descrição:
Consultar health status fornecido pelos storage providers do Windows.

Possíveis estados:
- Healthy;
- Warning;
- Unhealthy;
- Unknown.

Possível objetivo:
Detectar problemas graves rapidamente.

Evidence Level:
Established

Action Mode:
Automatic

---

# 7. Storage Reliability Counters

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Reliability

Primary Product Area:
Shared

Possible Consumers:
- Monitoring
- Repair
- My PC

Descrição:
Consultar métricas de confiabilidade disponíveis para cada dispositivo.

Possíveis informações:
- temperature;
- errors;
- wear;
- power-on time;
- outras métricas expostas pelo provider.

Possível benefício:
Criar health diagnostics mais ricos que simples SMART pass/fail.

Evidence Level:
Established Windows capability

Action Mode:
Automatic

---

# 8. SMART Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Reliability

Primary Product Area:
My PC

Possible Consumers:
- Repair
- Monitoring

Descrição:
Detectar suporte a SMART ou mecanismo equivalente.

Possível objetivo:
Adaptar o diagnóstico ao dispositivo.

Evidence Level:
Established

Action Mode:
Automatic

---

# 9. SMART Attribute Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Reliability

Primary Product Area:
Storage

Possible Consumers:
- Repair
- Monitoring

Descrição:
Interpretar atributos SMART relevantes quando acessíveis.

Possíveis exemplos:
- reallocated sectors;
- pending sectors;
- uncorrectable sectors;
- power-on hours;
- temperature;
- wear indicators;
- error counts.

Risco inicial:
Low

Evidence Level:
Vendor-dependent interpretation

Action Mode:
Diagnostic

Importante:
IDs e significados SMART podem variar por fabricante.

---

# 10. SMART Warning Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Reliability

Primary Product Area:
Repair

Descrição:
Detectar atributos ou estados indicando possível falha futura.

Possível objetivo:
Alertar antes de perda de dados.

Possível benefício:
Alta relevância.

Action Mode:
Diagnostic / Urgent Recommendation

---

# 11. NVMe Health Information

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVMe

Primary Product Area:
Storage

Possible Consumers:
- My PC
- Monitoring
- Repair

Descrição:
Consultar informações de saúde específicas de NVMe quando disponíveis.

Possíveis dados:
- temperature;
- available spare;
- percentage used;
- data read/written;
- power cycles;
- power-on hours;
- unsafe shutdowns;
- media/data integrity errors;
- error log count.

Aplicabilidade:
NVMe SSD

Evidence Level:
NVMe-standard / platform-dependent access

Action Mode:
Diagnostic

---

# 12. NVMe Wear Percentage

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVMe Reliability

Primary Product Area:
My PC

Descrição:
Interpretar indicador de desgaste/percentage used.

Possível objetivo:
Estimar estado de endurance.

Possível benefício:
Diferenciar SSD antigo saudável de unidade próxima ao limite de vida estimado.

Importante:
100% de “percentage used” não deve ser tratado cegamente como morte imediata sem interpretar padrão/fabricante.

---

# 13. SSD Remaining Life Estimate

Status:
IDEA

Type:
Diagnostic

Technical Domain:
SSD Reliability

Primary Product Area:
My PC

Descrição:
Produzir estimativa amigável de vida útil a partir dos indicadores disponíveis.

Possível objetivo:
Facilitar interpretação de wear.

Risco inicial:
Medium

Evidence Level:
Vendor-dependent

Action Mode:
Diagnostic

Importante:
Não prometer data exata de falha.

---

# 14. Storage Temperature Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
Storage Thermal

Primary Product Area:
Monitoring

Possible Consumers:
- Storage
- Cooling

Descrição:
Monitorar temperatura quando disponível.

Possível objetivo:
Detectar SSD/NVMe quente.

Possível benefício:
Troubleshooting de throttling e confiabilidade.

Evidence Level:
Hardware-dependent

Action Mode:
Monitoring

---

# 15. Storage Thermal Throttling Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Thermal

Primary Product Area:
Storage

Possible Consumers:
- Benchmark
- Cooling

Descrição:
Detectar degradação de throughput associada à temperatura.

Possível objetivo:
Explicar SSD rápido que perde performance durante cópia prolongada.

Possível benefício:
Diagnóstico real.

Evidence Level:
Vendor/hardware dependent

Action Mode:
Diagnostic

---

# 16. Storage Cooling Advisor

Status:
IDEA

Type:
Recommendation

Technical Domain:
Storage Thermal

Primary Product Area:
Storage

Descrição:
Orientar quando unidade apresenta temperaturas consistentemente altas.

Possíveis recomendações:
- verificar airflow;
- dissipador NVMe;
- posição sob GPU;
- thermal pad;
- limpeza.

Action Mode:
Manual Guidance

---

# 17. Physical Error Counter Monitoring

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Reliability

Primary Product Area:
Repair

Descrição:
Monitorar erros reportados pelo dispositivo/provider.

Possível objetivo:
Detectar degradação antes de filesystem corruption.

Evidence Level:
Established capability / device dependent

Action Mode:
Diagnostic

---

# 18. Storage Error Trend

Status:
IDEA

Type:
Monitoring

Technical Domain:
Storage Reliability

Primary Product Area:
Monitoring

Descrição:
Registrar evolução de erros ao longo do tempo.

Possível objetivo:
Distinguir erro isolado de degradação progressiva.

Possível benefício:
Predictive diagnostics.

Action Mode:
Monitoring

---

# 19. Unsafe Shutdown Count

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVMe / Reliability

Primary Product Area:
My PC

Descrição:
Detectar quantidade de desligamentos não limpos quando unidade expuser a informação.

Possível objetivo:
Dar contexto a problemas de filesystem ou power-loss behavior.

Evidence Level:
NVMe-dependent

Action Mode:
Diagnostic

---

# 20. Disk Power-On Hours

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Reliability

Primary Product Area:
My PC

Descrição:
Mostrar tempo total aproximado de operação quando disponível.

Possível objetivo:
Dar contexto à idade operacional.

Evidence Level:
Device dependent

Action Mode:
Diagnostic

---

# 21. Disk Power Cycle Count

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Reliability

Primary Product Area:
My PC

Descrição:
Mostrar número de ciclos de energia quando disponível.

Possível benefício:
Contexto de uso/idade.

---

# 22. Storage Capacity Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage

Primary Product Area:
My PC

Descrição:
Detectar capacidade física e lógica.

Evidence Level:
Established

Action Mode:
Automatic

---

# 23. Volume Inventory

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Volumes

Primary Product Area:
Storage

Possible Consumers:
- Cleanup
- Repair
- BitLocker

Descrição:
Inventariar volumes existentes.

Possíveis informações:
- drive letter;
- label;
- filesystem;
- capacity;
- free space;
- mount point;
- health.

Evidence Level:
Established

Action Mode:
Automatic

---

# 24. Partition Layout Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Partitioning

Primary Product Area:
My PC

Descrição:
Mapear partições e volumes por disco físico.

Possível objetivo:
Entender layout de sistema.

Possíveis categorias:
- EFI;
- MSR;
- Windows;
- Recovery;
- OEM;
- data;
- unallocated.

Action Mode:
Diagnostic

---

# 25. GPT / MBR Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Partitioning

Primary Product Area:
My PC

Descrição:
Detectar estilo de partição.

Possível objetivo:
Dar contexto a UEFI, Secure Boot e limitações de volume.

Action Mode:
Diagnostic

---

# 26. Unexpected Unallocated Space Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Partitioning

Primary Product Area:
Storage

Descrição:
Detectar espaço significativo não alocado.

Possível objetivo:
Encontrar capacidade não utilizada por erro/configuração.

Risco inicial:
Low

Action Mode:
Diagnostic / Manual Guidance

---

# 27. Low Free Space Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Capacity

Primary Product Area:
Optimization

Possible Consumers:
- Cleanup
- My PC

Descrição:
Detectar volumes com espaço livre reduzido.

Possível objetivo:
Prevenir problemas de updates, cache, pagefile e performance.

Evidence Level:
Established

Action Mode:
Automatic Diagnostic

---

# 28. Free Space Trend

Status:
IDEA

Type:
Monitoring

Technical Domain:
Storage Capacity

Primary Product Area:
Monitoring

Descrição:
Registrar perda de espaço ao longo do tempo.

Possível objetivo:
Encontrar crescimento anormal.

Possíveis culpados:
- logs;
- cache;
- games;
- crash dumps;
- temp;
- restore points;
- application data.

---

# 29. “What Is Using My Disk Space?” Analysis

Status:
CANDIDATE

Type:
Diagnostic Tool

Technical Domain:
Storage Capacity

Primary Product Area:
Tools

Possible Consumers:
- Cleanup

Descrição:
Analisar consumo de espaço por diretório/categoria.

Possível objetivo:
Explicar onde capacidade está sendo utilizada.

Possível benefício:
Mais útil que simplesmente apagar temporários.

Action Mode:
User-triggered Diagnostic

---

# 30. Large File Discovery

Status:
CANDIDATE

Type:
Tool

Technical Domain:
Storage Capacity

Primary Product Area:
Tools

Descrição:
Identificar arquivos muito grandes.

Possível objetivo:
Auxiliar gestão de espaço.

Importante:
Nunca apagar automaticamente.

Action Mode:
User Choice

---

# 31. Duplicate File Discovery

Status:
IDEA

Type:
Tool

Technical Domain:
Storage Capacity

Primary Product Area:
Tools

Descrição:
Identificar conteúdo duplicado com comparação confiável.

Possível objetivo:
Recuperar espaço.

Risco inicial:
Medium

Action Mode:
User Choice Only

Dúvidas:
- hash completo;
- hard links;
- OneDrive/cloud placeholders;
- backups.

---

# 32. Filesystem Type Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Filesystem

Primary Product Area:
Storage

Descrição:
Identificar filesystem utilizado.

Possíveis exemplos:
- NTFS;
- ReFS;
- FAT32;
- exFAT.

Possível objetivo:
Selecionar reparos/manutenção compatíveis.

---

# 33. Filesystem Health Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Filesystem

Primary Product Area:
Repair

Descrição:
Detectar sinais de problemas do filesystem.

Possíveis sinais:
- dirty volume;
- NTFS errors;
- corruption;
- Event Log errors;
- failed operations.

Action Mode:
Diagnostic

---

# 34. CHKDSK Online Scan

Status:
CANDIDATE

Type:
Diagnostic / Repair

Technical Domain:
Filesystem

Primary Product Area:
Repair

Descrição:
Executar verificação online apropriada quando necessário.

Possível objetivo:
Detectar corrupção sem iniciar imediatamente reparo offline.

Action Mode:
Problem-triggered Diagnostic

---

# 35. CHKDSK Repair Scheduling

Status:
CANDIDATE

Type:
Repair

Technical Domain:
Filesystem

Primary Product Area:
Repair

Descrição:
Agendar reparo em reboot quando necessário.

Risco inicial:
Medium

Action Mode:
Explicit User Choice

---

# 36. Filesystem Error History

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Filesystem / Event Logs

Primary Product Area:
Repair

Descrição:
Consultar histórico de erros relacionados a NTFS/storage.

Possível objetivo:
Descobrir problemas intermitentes.

---

# 37. TRIM Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
SSD Maintenance

Primary Product Area:
Storage

Descrição:
Detectar suporte e estado relacionado a TRIM.

Possível objetivo:
Confirmar que SSD recebe informação de blocos liberados.

Evidence Level:
Established

Action Mode:
Automatic Diagnostic

---

# 38. ReTRIM Capability

Status:
CANDIDATE

Type:
Maintenance

Technical Domain:
SSD Maintenance

Primary Product Area:
Optimization

Descrição:
Permitir ReTrim usando mecanismo suportado do Windows quando aplicável.

Possível objetivo:
Reemitir notificações de blocos liberados.

Aplicabilidade:
SSD/TRIM/thin-provisioned storage

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Contextual Maintenance

---

# 39. TRIM Misconfiguration Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
SSD Maintenance

Primary Product Area:
Repair

Descrição:
Detectar SSD compatível onde TRIM parece indisponível/desabilitado.

Possível objetivo:
Encontrar configuração anormal.

Action Mode:
Diagnostic First

---

# 40. Volume Optimization Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Maintenance

Primary Product Area:
Optimization

Descrição:
Analisar volume antes de executar Optimize.

Possível objetivo:
Deixar o Windows decidir ou informar o tipo apropriado de manutenção.

Evidence Level:
Established

Action Mode:
Automatic Diagnostic

---

# 41. Media-Aware Optimize Volume

Status:
CANDIDATE

Type:
Maintenance

Technical Domain:
Storage Maintenance

Primary Product Area:
Optimization

Descrição:
Executar operação apropriada por tipo de mídia usando mecanismos suportados do Windows.

Possíveis comportamentos:
- HDD → Analyze / Defrag;
- SSD com TRIM → ReTrim;
- tiered Storage Space → TierOptimize;
- thin provisioned storage → SlabConsolidate/ReTrim.

Evidence Level:
Established Windows behavior

Action Mode:
Contextual Automatic/User Choice

---

# 42. HDD Fragmentation Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
HDD

Primary Product Area:
Storage

Descrição:
Analisar fragmentação em mídia rotacional.

Possível objetivo:
Determinar se defrag tem benefício.

Evidence Level:
Established

Action Mode:
Diagnostic First

---

# 43. HDD Defragmentation

Status:
CANDIDATE

Type:
Maintenance

Technical Domain:
HDD

Primary Product Area:
Optimization

Descrição:
Executar defrag onde mídia e estado justificarem.

Applicability:
HDD

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Contextual

---

# 44. SSD Manual Defrag Guard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
SSD Maintenance

Primary Product Area:
Shared

Descrição:
Impedir que o BeadWise trate SSD como HDD e simplesmente execute defrag manual agressivo.

Possível objetivo:
Usar mecanismo correto por mídia.

Evidence Level:
Established platform behavior

Action Mode:
Internal Safeguard

---

# 45. Storage Maintenance Schedule Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Maintenance

Primary Product Area:
Storage

Descrição:
Detectar se otimização periódica do Windows está habilitada e funcionando.

Possível objetivo:
Evitar executar tarefas redundantes.

Action Mode:
Diagnostic

---

# 46. Redundant Optimization Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Storage Maintenance

Primary Product Area:
Optimization

Descrição:
Detectar quando o Windows já realizou manutenção recentemente.

Possível objetivo:
Evitar “Optimize” que não faz nada útil.

Action Mode:
Automatic Safeguard

---

# 47. Storage Write Cache Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Performance

Primary Product Area:
Storage

Descrição:
Detectar políticas de cache de escrita quando acessíveis.

Possível objetivo:
Dar contexto a performance e segurança.

Risco inicial:
Low

Evidence Level:
Platform/device-dependent

Action Mode:
Diagnostic

---

# 48. Write Cache Policy Modification

Status:
RESEARCHING

Type:
Configuration

Technical Domain:
Storage Performance

Primary Product Area:
Advanced / TBD

Descrição:
Investigar alteração de políticas de write caching.

Possível benefício:
Possível performance.

Risco inicial:
High

Evidence Level:
Device/context-dependent

Action Mode:
Manual/Advanced Only

Importante:
Pode aumentar risco de perda de dados em power failure.

---

# 49. Storage Flush Behavior Context

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Storage Performance / Reliability

Primary Product Area:
Storage

Descrição:
Dar contexto a cache/flush e proteção contra perda de energia.

Possível objetivo:
Evitar otimizações que sacrifiquem integridade.

Action Mode:
Diagnostic

---

# 50. SATA Link Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
SATA

Primary Product Area:
Storage

Descrição:
Detectar quando possível geração/velocidade negociada de SATA.

Possível objetivo:
Encontrar SSD rápido limitado por interface/porta inadequada.

Action Mode:
Diagnostic

---

# 51. SATA Link Bottleneck Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
SATA Performance

Primary Product Area:
Storage

Descrição:
Detectar unidade capaz de throughput superior ao link atual.

Possível benefício:
Hardware guidance.

Action Mode:
Manual Guidance

---

# 52. NVMe PCIe Link Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVMe / PCIe

Primary Product Area:
Shared

Possible Consumers:
- Storage
- PCIe
- Benchmark

Descrição:
Detectar geração e largura do link PCIe quando possível.

Possível objetivo:
Descobrir NVMe Gen4/5 operando em link inferior.

Action Mode:
Diagnostic

---

# 53. NVMe Link Bottleneck Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVMe / PCIe

Primary Product Area:
Storage

Descrição:
Comparar capacidades da unidade com link negociado.

Possível objetivo:
Identificar slot/chipset/configuração limitando performance.

Action Mode:
Diagnostic

---

# 54. NVMe Shared-Lane / Platform Constraint Context

Status:
IDEA

Type:
Diagnostic

Technical Domain:
NVMe / Platform

Primary Product Area:
Storage

Descrição:
Reconhecer que alguns slots compartilham lanes com GPU/SATA/outros dispositivos.

Possível objetivo:
Explicar comportamento inesperado.

Risco inicial:
Low

Evidence Level:
Motherboard-specific

Action Mode:
Manual Guidance

---

# 55. Storage Controller Identification

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Controller

Primary Product Area:
My PC

Descrição:
Identificar controller/driver associado ao dispositivo.

Possíveis exemplos:
- Standard NVM Express Controller;
- Intel RST;
- AMD RAID;
- vendor NVMe driver;
- USB bridge.

Possível benefício:
Diagnóstico de compatibilidade/performance.

---

# 56. Storage Driver Version Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Drivers

Primary Product Area:
Shared

Possible Consumers:
- Storage
- Drivers

Descrição:
Detectar versão do driver/controlador.

Possível objetivo:
Investigar problemas de performance/compatibilidade.

Action Mode:
Diagnostic

---

# 57. Storage Driver Problem Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Drivers

Primary Product Area:
Repair

Descrição:
Relacionar erros de dispositivo, Event Log ou throughput anormal a driver/controller.

Action Mode:
Diagnostic

---

# 58. Storage I/O Throughput Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
Storage Performance

Primary Product Area:
Monitoring

Descrição:
Monitorar leitura/escrita por dispositivo/volume.

Possível objetivo:
Detectar saturação e processos consumidores.

Action Mode:
Monitoring

---

# 59. Storage I/O by Process

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage / Processes

Primary Product Area:
Monitoring

Possible Consumers:
- Processes
- Optimization

Descrição:
Relacionar atividade de disco a processos.

Possível objetivo:
Responder “o que está usando meu disco?”

Action Mode:
Monitoring

---

# 60. Disk Active Time Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Performance

Primary Product Area:
Storage

Descrição:
Analisar períodos de alta ocupação.

Possível objetivo:
Detectar saturação mesmo com throughput aparentemente baixo.

Importante:
100% active time não significa necessariamente que dispositivo está entregando throughput máximo.

---

# 61. Storage Latency Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
Storage Performance

Primary Product Area:
Monitoring

Descrição:
Monitorar latência de leitura/escrita.

Possível objetivo:
Detectar dispositivo responsivo versus congestionado.

Action Mode:
Monitoring

---

# 62. High Storage Latency Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Performance

Primary Product Area:
Optimization

Descrição:
Detectar latência anormal durante workload.

Possível objetivo:
Encontrar congestionamento, thermal throttling, driver ou hardware problem.

Action Mode:
Diagnostic

---

# 63. Storage Queue Depth Monitoring

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Performance

Primary Product Area:
Storage

Descrição:
Observar fila de operações pendentes.

Possível objetivo:
Dar contexto a saturação.

Action Mode:
Diagnostic

---

# 64. Storage Saturation Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Performance

Primary Product Area:
Optimization

Descrição:
Combinar:
- queue;
- latency;
- active time;
- throughput;
- process I/O.

Possível objetivo:
Determinar se armazenamento é gargalo real.

Action Mode:
Diagnostic Only

---

# 65. Sequential Read Benchmark

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Descrição:
Medir leitura sequencial.

Action Mode:
User-triggered Benchmark

---

# 66. Sequential Write Benchmark

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Descrição:
Medir escrita sequencial.

Risco inicial:
Medium

Importante:
Benchmarks de escrita geram desgaste e devem limitar quantidade de dados.

---

# 67. Random Read Benchmark

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Descrição:
Medir acessos aleatórios.

Possível benefício:
Mais representativo de vários workloads de sistema.

---

# 68. Random Write Benchmark

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Risco inicial:
Medium

Descrição:
Medir escrita aleatória de forma controlada.

---

# 69. Storage Latency Benchmark

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Descrição:
Medir latência em condições controladas.

Possível benefício:
Complementar MB/s.

---

# 70. Queue-Depth Benchmark

Status:
IDEA

Type:
Benchmark

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Descrição:
Avaliar comportamento em diferentes níveis de concorrência.

Possível benefício:
Distinguir desktop responsiveness de throughput enterprise-like.

---

# 71. Low Queue Depth Benchmark

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Descrição:
Dar destaque a workload de baixa fila mais próximo de várias tarefas desktop/gaming.

Possível objetivo:
Evitar reduzir “SSD rápido” ao QD32 sequencial.

---

# 72. Storage Benchmark Wear Safeguard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
Storage Benchmark

Primary Product Area:
Shared

Descrição:
Limitar writes e frequência de benchmark em SSDs.

Possível objetivo:
Não desgastar dispositivo por medições desnecessárias.

Action Mode:
Internal Safeguard

---

# 73. Storage Benchmark Thermal Readiness

Status:
CANDIDATE

Type:
Benchmark Safeguard

Technical Domain:
Storage Thermal

Primary Product Area:
Benchmark

Descrição:
Registrar temperatura inicial antes de teste.

Possível objetivo:
Diferenciar performance fria de throttling térmico.

Action Mode:
Automatic

---

# 74. Sustained Storage Performance Test

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Descrição:
Medir throughput por período maior.

Possível objetivo:
Detectar:
- SLC cache exhaustion;
- thermal throttling;
- sustained write behavior.

Risco inicial:
Medium

Action Mode:
Explicit User-triggered

---

# 75. SSD Cache Exhaustion Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
SSD Performance

Primary Product Area:
Storage

Descrição:
Detectar queda grande de write performance após cache pseudo-SLC ser preenchido.

Possível objetivo:
Explicar diferença entre benchmark curto e cópia grande.

Evidence Level:
Device/workload dependent

Action Mode:
Diagnostic

---

# 76. SSD Sustained Write Characterization

Status:
IDEA

Type:
Benchmark / Diagnostic

Technical Domain:
SSD Performance

Primary Product Area:
Benchmark

Descrição:
Caracterizar performance depois que caches internos deixam de mascarar velocidade nativa.

Possível benefício:
Transparência real do dispositivo.

---

# 77. Storage Benchmark Before × After

Status:
CANDIDATE

Type:
Validation

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Descrição:
Comparar performance antes/depois de manutenção/configuração.

Possível objetivo:
Validar benefício real.

---

# 78. Storage Performance Baseline vs Expected

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Storage Benchmark

Primary Product Area:
My PC

Descrição:
Comparar resultado com faixa esperada para o mesmo modelo/interface.

Possível objetivo:
Detectar unidade operando muito abaixo do esperado.

Risco inicial:
Medium

Evidence Level:
Requires reliable reference dataset

---

# 79. Filesystem Allocation Unit Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Filesystem

Primary Product Area:
Storage

Descrição:
Detectar cluster/allocation unit size.

Possível objetivo:
Contexto para workloads específicos.

Action Mode:
Diagnostic Only

---

# 80. Filesystem Compression Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Filesystem

Primary Product Area:
Storage

Descrição:
Detectar NTFS compression onde relevante.

Possível objetivo:
Explicar CPU/storage tradeoffs e consumo de espaço.

Action Mode:
Diagnostic

---

# 81. CompactOS State Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Windows Storage

Primary Product Area:
Storage

Descrição:
Detectar uso de CompactOS.

Possível objetivo:
Dar contexto ao espaço do Windows e overhead de compressão.

Action Mode:
Diagnostic

---

# 82. CompactOS Configuration

Status:
RESEARCHING

Type:
Configuration

Technical Domain:
Windows Storage

Primary Product Area:
Storage

Descrição:
Investigar quando compactar/descompactar Windows faz sentido.

Risco inicial:
Medium

Action Mode:
Contextual / Manual

---

# 83. Sparse File Awareness

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Filesystem

Primary Product Area:
Advanced Tools

Descrição:
Distinguir tamanho lógico de espaço físico ocupado em arquivos sparse.

Possível objetivo:
Evitar cálculo errado de uso.

---

# 84. Hard Link Awareness

Status:
CANDIDATE

Type:
Safety / Diagnostic

Technical Domain:
Filesystem

Primary Product Area:
Cleanup

Descrição:
Evitar considerar hard links como arquivos duplicados independentes.

Possível objetivo:
Impedir cálculos/limpezas incorretos.

Action Mode:
Internal Safeguard

---

# 85. Cloud Placeholder Awareness

Status:
CANDIDATE

Type:
Safety / Diagnostic

Technical Domain:
Cloud Files

Primary Product Area:
Cleanup

Descrição:
Distinguir arquivos disponíveis online, locais e placeholders.

Possível objetivo:
Evitar afirmar que arquivo consome armazenamento físico quando não consome integralmente.

Action Mode:
Internal

---

# 86. Windows.old Detection

Status:
CANDIDATE

Type:
Diagnostic / Cleanup

Technical Domain:
Windows Storage

Primary Product Area:
Cleanup

Descrição:
Detectar instalação anterior do Windows.

Possível objetivo:
Informar espaço recuperável e implicações de rollback.

Risco inicial:
Medium

Action Mode:
User Choice

Importante:
Remover pode eliminar opção de retornar à versão anterior.

---

# 87. Update Cache Detection

Status:
CANDIDATE

Type:
Diagnostic / Cleanup

Technical Domain:
Windows Update Storage

Primary Product Area:
Cleanup

Descrição:
Detectar arquivos temporários/cache relacionados a updates.

Action Mode:
Contextual Cleanup

---

# 88. Delivery Optimization Cache Detection

Status:
CANDIDATE

Type:
Diagnostic / Cleanup

Technical Domain:
Windows Storage

Primary Product Area:
Cleanup

Descrição:
Detectar cache de Delivery Optimization.

Action Mode:
Contextual Cleanup

---

# 89. DirectX Shader Cache Detection

Status:
CANDIDATE

Type:
Diagnostic / Cleanup

Technical Domain:
Storage / Gaming

Primary Product Area:
Cleanup

Descrição:
Detectar cache de shaders gerenciado pelo Windows.

Possível objetivo:
Permitir limpeza em troubleshooting, não como rotina de performance.

Risco inicial:
Low

Action Mode:
Problem-triggered / User Choice

Importante:
Apagar cache pode causar recompilação e stutter temporário.

---

# 90. Browser / Application Cache Inventory

Status:
CANDIDATE

Type:
Diagnostic / Cleanup

Technical Domain:
Storage

Primary Product Area:
Cleanup

Descrição:
Inventariar caches conhecidos.

Possível objetivo:
Mostrar espaço potencial antes de apagar.

Action Mode:
User Choice

---

# 91. Crash Dump Storage Analysis

Status:
CANDIDATE

Type:
Diagnostic / Cleanup

Technical Domain:
Storage / Repair

Primary Product Area:
Cleanup

Descrição:
Detectar:
- memory dumps;
- minidumps;
- application dumps.

Possível objetivo:
Recuperar espaço sem remover evidência diagnóstica necessária.

Action Mode:
User Choice

---

# 92. Log Growth Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage

Primary Product Area:
Optimization

Descrição:
Detectar logs crescendo anormalmente.

Possível objetivo:
Encontrar software quebrado consumindo armazenamento.

Action Mode:
Diagnostic First

---

# 93. Temporary File Classification

Status:
CANDIDATE

Type:
Cleanup

Technical Domain:
Storage

Primary Product Area:
Cleanup

Descrição:
Classificar temporários por origem e segurança.

Possíveis níveis:
- Safe;
- Usually Safe;
- Application-specific;
- Review Required.

Action Mode:
User Choice / Safe Automatic where justified

---

# 94. Cleanup Preview

Status:
CANDIDATE

Type:
Safety / Product Behavior

Technical Domain:
Cleanup

Primary Product Area:
Cleanup

Descrição:
Mostrar exatamente categorias e espaço antes da exclusão.

Possível benefício:
Transparência e segurança.

Action Mode:
Automatic

---

# 95. Cleanup Verification

Status:
CANDIDATE

Type:
Validation

Technical Domain:
Cleanup

Primary Product Area:
Cleanup

Descrição:
Recalcular espaço depois da operação.

Possível objetivo:
Mostrar ganho real.

---

# 96. Cleanup Rollback Capability

Status:
RESEARCHING

Type:
Safety

Technical Domain:
Cleanup

Primary Product Area:
Cleanup

Descrição:
Investigar quais categorias podem ser restauradas após remoção.

Possível objetivo:
Maior segurança.

Dúvidas:
Nem todo cleanup é reversível.

---

# 97. User Data Protection Guard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
Cleanup

Primary Product Area:
Shared

Descrição:
Nunca apagar automaticamente:
- Downloads;
- Documents;
- Desktop;
- pictures;
- personal archives;
- unidentified large files.

Action Mode:
Internal Safeguard

---

# 98. System Restore Storage Usage

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage / Recovery

Primary Product Area:
Storage

Descrição:
Detectar espaço consumido por System Restore/Shadow Copies.

Possível objetivo:
Explicar uso de armazenamento aparentemente invisível.

Action Mode:
Diagnostic

---

# 99. Shadow Storage Pressure Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Storage / Recovery

Primary Product Area:
Storage

Descrição:
Detectar configuração muito grande ou muito pequena de shadow storage.

Possível benefício:
Contextual.

Action Mode:
Diagnostic First

---

# 100. BitLocker State Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Security

Primary Product Area:
Shared

Possible Consumers:
- Storage
- Security
- Recovery

Descrição:
Detectar estado do BitLocker por volume.

Possíveis dados:
- protected;
- encryption percentage;
- protection status;
- conversion state.

Action Mode:
Automatic

---

# 101. BitLocker Encryption-in-Progress Detection

Status:
CANDIDATE

Type:
Diagnostic / Benchmark Safeguard

Technical Domain:
Storage Security

Primary Product Area:
Shared

Descrição:
Detectar processo de encrypt/decrypt em andamento.

Possível objetivo:
Evitar benchmark durante conversão e explicar atividade de disco.

Action Mode:
Automatic

---

# 102. BitLocker Performance Context

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage / Security

Primary Product Area:
Storage

Descrição:
Reconhecer pequeno overhead possível de criptografia sem recomendar desativação.

Possível objetivo:
Interpretar benchmarks corretamente.

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 103. Disable BitLocker for Performance

Status:
REJECTED

Type:
Optimization

Descrição:
Desativar criptografia apenas para ganho marginal de storage performance.

Motivo:
Reduz segurança por benefício geralmente pequeno e dependente do workload.

---

# 104. Storage Spaces Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Spaces

Primary Product Area:
My PC

Descrição:
Detectar discos pertencentes a Storage Pools/Virtual Disks.

Possível objetivo:
Evitar tratar cada disco físico isoladamente.

Action Mode:
Automatic

---

# 105. Storage Pool Health

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Spaces

Primary Product Area:
Storage

Descrição:
Consultar estado de saúde do pool.

Possível objetivo:
Encontrar warning/unhealthy states.

Action Mode:
Diagnostic

---

# 106. Virtual Disk Health

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Spaces

Primary Product Area:
Storage

Descrição:
Consultar integridade dos virtual disks.

Action Mode:
Diagnostic

---

# 107. Storage Spaces Physical Disk Health

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Spaces

Primary Product Area:
Storage

Descrição:
Relacionar problema do virtual disk ao disco físico correspondente.

Action Mode:
Diagnostic

---

# 108. Storage Pool Repair Guidance

Status:
IDEA

Type:
Repair Guidance

Technical Domain:
Storage Spaces

Primary Product Area:
Repair

Descrição:
Orientar quando pool está degradado.

Risco inicial:
High

Action Mode:
Manual Guidance

---

# 109. Thin Provisioning Awareness

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Virtual Storage

Primary Product Area:
Storage

Descrição:
Detectar volumes/discos thin-provisioned.

Possível objetivo:
Selecionar manutenção adequada como slab consolidation/ReTrim.

Action Mode:
Automatic

---

# 110. Storage Tier Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Spaces

Primary Product Area:
Storage

Descrição:
Detectar armazenamento em tiers.

Action Mode:
Diagnostic

---

# 111. Tier Optimization

Status:
CANDIDATE

Type:
Maintenance

Technical Domain:
Storage Spaces

Primary Product Area:
Optimization

Descrição:
Executar otimização de tiers quando apropriado.

Evidence Level:
Established Windows operation

Action Mode:
Contextual

---

# 112. DirectStorage Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Gaming / Storage

Primary Product Area:
Shared

Possible Consumers:
- Gaming
- Storage
- My PC

Descrição:
Verificar pré-requisitos relevantes para DirectStorage.

Possíveis requisitos:
- NVMe SSD;
- controlador NVM Express compatível;
- GPU DX12;
- Shader Model 6;
- jogo compatível.

Action Mode:
Diagnostic

---

# 113. DirectStorage Readiness Summary

Status:
CANDIDATE

Type:
Diagnostic / Product Behavior

Technical Domain:
Gaming / Storage

Primary Product Area:
My PC

Descrição:
Mostrar se máquina parece pronta para jogos que utilizam DirectStorage.

Possíveis resultados:
- Ready;
- Storage limitation;
- GPU limitation;
- driver/controller limitation;
- unsupported.

---

# 114. “Enable DirectStorage” Myth Guard

Status:
CANDIDATE

Type:
Safety / Explainability

Technical Domain:
Gaming / Storage

Primary Product Area:
Shared

Descrição:
Evitar apresentar DirectStorage como Registry tweak universal.

Possível objetivo:
Explicar que aplicação/jogo precisa utilizá-lo.

Action Mode:
Internal

---

# 115. Storage Event Log Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage / Event Logs

Primary Product Area:
Repair

Descrição:
Analisar eventos relacionados a:
- disk;
- storage port;
- NVMe;
- NTFS;
- controller resets;
- timeout;
- bad block;
- surprise removal.

Possível objetivo:
Detectar problemas intermitentes.

Action Mode:
Diagnostic

---

# 116. Storage Timeout Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Reliability

Primary Product Area:
Repair

Descrição:
Identificar resets/timeouts recorrentes.

Possível objetivo:
Encontrar driver, cable, controller ou failing drive.

Action Mode:
Diagnostic

---

# 117. Surprise Removal Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage / USB

Primary Product Area:
Repair

Descrição:
Detectar desconexões inesperadas.

Possível objetivo:
Investigar cabo/energia/USB/storage.

Action Mode:
Diagnostic

---

# 118. SATA Cable / Link Error Advisor

Status:
IDEA

Type:
Diagnostic / Guidance

Technical Domain:
SATA

Primary Product Area:
Repair

Descrição:
Interpretar determinados contadores/eventos como possível problema de link/cabo.

Risco inicial:
Low

Action Mode:
Manual Guidance

Importante:
Não afirmar “cabo ruim” sem evidência suficiente.

---

# 119. Storage Driver Reset Correlation

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Storage / Drivers

Primary Product Area:
Repair

Descrição:
Correlacionar stutters/freezes com resets do controller/storage.

Possível benefício:
Diagnóstico muito valioso.

Action Mode:
Diagnostic

---

# 120. Storage Activity / Stutter Correlation

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Gaming / Storage

Primary Product Area:
Monitoring

Possible Consumers:
- FPS
- Stutter Analyzer

Descrição:
Correlacionar:
- disk latency;
- queue;
- read spikes;
- page faults;
- frametime spikes.

Possível objetivo:
Investigar stutter relacionado a streaming de assets/paging.

Action Mode:
Diagnostic Only

---

# 121. Game Storage Location Analysis

Status:
IDEA

Type:
Diagnostic / Recommendation

Technical Domain:
Gaming / Storage

Primary Product Area:
Gaming

Descrição:
Identificar tipo de armazenamento onde determinado jogo está instalado.

Possível objetivo:
Detectar jogo moderno instalado em HDD quando workload se beneficia fortemente de SSD.

Action Mode:
Recommendation

---

# 122. Move Game to Faster Storage Advisor

Status:
IDEA

Type:
Recommendation

Technical Domain:
Gaming / Storage

Primary Product Area:
Gaming

Descrição:
Recomendar mover jogo/aplicação para unidade mais adequada quando gargalo for demonstrável.

Risco inicial:
Low

Action Mode:
Manual Guidance

---

# 123. Storage Benchmark During Background I/O Guard

Status:
CANDIDATE

Type:
Benchmark Safeguard

Technical Domain:
Storage

Primary Product Area:
Benchmark

Descrição:
Detectar:
- Windows Update;
- antivirus scan;
- downloads;
- indexing;
- copy operation.

Possível objetivo:
Evitar benchmark contaminado.

Action Mode:
Automatic

---

# 124. Storage Benchmark Free-Space Safeguard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
Storage Benchmark

Primary Product Area:
Benchmark

Descrição:
Verificar espaço disponível antes de criar arquivos de teste.

Action Mode:
Automatic

---

# 125. Storage Benchmark File Cleanup

Status:
CANDIDATE

Type:
Safety

Technical Domain:
Storage Benchmark

Primary Product Area:
Benchmark

Descrição:
Remover arquivos temporários de benchmark de forma segura após teste.

Action Mode:
Automatic

---

# 126. Storage Benchmark Result Interpretation

Status:
CANDIDATE

Type:
Product Behavior

Technical Domain:
Storage Benchmark

Primary Product Area:
Benchmark

Descrição:
Não reduzir armazenamento a uma única nota.

Possíveis dimensões:
- sequential read;
- sequential write;
- random read;
- random write;
- latency;
- sustained performance;
- thermal behavior.

Action Mode:
Automatic

---

# 127. Storage Health Summary

Status:
CANDIDATE

Type:
Diagnostic / Product Behavior

Technical Domain:
Storage

Primary Product Area:
My PC

Descrição:
Consolidar estado de cada unidade.

Possíveis resultados:

Healthy

Warning:
High temperature

Warning:
Low free space

Critical:
SMART / reliability errors

Performance issue:
PCIe link below expected

Maintenance:
ReTrim recommended

---

# 128. Storage Optimization Myth Guard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
Storage

Primary Product Area:
Shared

Descrição:
Impedir otimizações clássicas sem contexto.

Regras:

- não desfragmentar toda mídia indiscriminadamente;
- não limpar caches úteis apenas para mostrar espaço;
- não desabilitar BitLocker por performance;
- não alterar write cache sem explicar risco;
- não executar benchmark destrutivo repetidamente;
- não apagar dados pessoais;
- não assumir que MB/s sequencial define experiência real.

Action Mode:
Internal

---

# Itens deliberadamente NÃO tratados como otimização universal

## Defrag em todos os discos

Não.

O Windows escolhe operação apropriada por tipo de mídia.

---

## TRIM manual diariamente

Não necessariamente.

O Windows já possui manutenção agendada.

---

## Desabilitar write cache flushing

Não.

Pode aumentar risco de corrupção/perda de dados.

---

## Desativar BitLocker para ganhar desempenho

Não.

---

## Apagar shader cache rotineiramente

Não.

Pode gerar recompilação e stutter.

---

## Limpar Prefetch/SysMain cache

Não adicionar como “storage optimization” sem evidência.

---

## Apagar Windows.old automaticamente

Não.

Pode remover capacidade de rollback.

---

## Apagar Restore Points para liberar espaço

Não automaticamente.

---

## “NVMe optimization registry pack”

Não.

Investigar configurações individualmente, nunca pacote mágico.

---

## Desabilitar indexing apenas porque existe SSD

Não.

Já classificamos Search de forma contextual em Sistema.

---

# Princípios que emergem da área Armazenamento

1. Mídia precisa ser identificada antes de manutenção.

2. Health e performance são conceitos diferentes.

3. SMART “OK” não garante ausência de problema.

4. SMART attributes podem ser vendor-specific.

5. NVMe oferece telemetria mais rica que simples SMART tradicional.

6. Temperatura pode limitar performance.

7. Performance burst e sustentada podem ser muito diferentes em SSD.

8. SLC cache pode mascarar performance nativa.

9. Sequential MB/s não representa toda experiência.

10. Low queue depth é importante para desktop/gaming.

11. Latency e queue também importam.

12. 100% active time não significa automaticamente throughput máximo.

13. Link PCIe pode limitar NVMe.

14. Firmware/controlador podem causar problemas.

15. HDD e SSD devem receber manutenção diferente.

16. ReTrim não é defrag tradicional.

17. O Windows já conhece diferentes operações de Optimize Volume.

18. Executar otimização redundante não gera benefício.

19. Espaço livre baixo pode causar problemas indiretos.

20. Descobrir onde o espaço está sendo usado é melhor que limpar cegamente.

21. Cleanup precisa distinguir cache, temporário e dado do usuário.

22. Cache útil não deve ser tratado automaticamente como lixo.

23. BitLocker é security feature, não “bloat”.

24. Storage Spaces exige visão de pool + virtual disk + physical disk.

25. DirectStorage é capability de software/hardware, não tweak de Registry.

26. Benchmark deve respeitar desgaste e temperatura.

27. Um disco lento pode estar saudável.

28. Um disco rápido pode estar falhando.

29. Um SSD pode estar abaixo do esperado por link/interface e não por Windows.

30. Storage optimization deve ser fortemente baseada em diagnóstico.

---

# Questões para segunda passada

- Como obter NVMe SMART/Health Log diretamente?
- Windows Storage APIs expõem todos os campos NVMe relevantes?
- Como detectar NVMe critical warnings?
- Como interpretar Available Spare / threshold?
- Como detectar controller busy time?
- Como obter host reads/writes?
- Como mostrar TBW estimado?
- Vale calcular write amplification? Provavelmente não sem dados internos.
- Como detectar NAND type? TLC/QLC?
- Isso é relevante ou depende demais de database externa?
- Como detectar DRAM-less SSD?
- Host Memory Buffer pode ser observado?
- HMB pertence ao discovery?
- Como detectar SLC cache dinamicamente?
- Benchmark sustentado pode estimar cache size?
- Como evitar desgaste fazendo isso?
- Como detectar PCIe Gen/lanes do NVMe de maneira confiável?
- Como distinguir capability máxima de negotiated link?
- APST NVMe pertence a Storage ou Energy?
- Autonomous Power State Transition merece diagnóstico?
- NVMe power states podem causar latência relevante?
- Deveríamos medir wake latency?
- SATA ALPM pertence a Storage/Energy?
- HIPM/DIPM ainda merecem investigação?
- Devemos detectar NCQ?
- AHCI vs RAID/RST tem implicações úteis?
- Intel VMD deve ser detectado?
- Como detectar RAID arrays sem interpretar discos isoladamente?
- Storage Spaces client Windows tem todas as capacidades documentadas?
- ReFS é relevante em desktop?
- Integrity Streams devem ser detectados?
- NTFS compression pode ser usada como otimização de espaço?
- CompactOS vale a pena em PCs com SSD grande?
- Filesystem fragmentation em SSD ainda possui cenários legítimos?
- O Windows executa defrag ocasional de SSD por motivos específicos?
- Devemos simplesmente confiar em Optimize-Volume?
- TRIM pass-through funciona sob RAID?
- Como detectar TRIM efetivamente chegando ao dispositivo?
- `fsutil behavior query DisableDeleteNotify` é suficiente ou só informa filesystem behavior?
- Podemos verificar TRIM end-to-end?
- Storage Sense deve ser aprofundado?
- Cleanup recommendations do Windows podem ser consumidas?
- Reserved Storage merece capability própria?
- Windows Update Reserved Storage é relevante?
- Delivery Optimization cache size pode ser configurado?
- Prefetch/SysMain terá pesquisa própria em Sistema/Storage?
- USN Journal size merece diagnóstico?
- Event Logs gigantes podem consumir armazenamento?
- WinSxS permanece em Sistema/Repair ou Storage?
- Shadow Copies podem crescer anormalmente?
- VSS snapshots órfãos?
- Restore point storage allocation?
- BitLocker software vs hardware encryption detection?
- Hardware encryption continua recomendável no Windows moderno?
- Como medir BitLocker overhead de forma justa?
- Device Encryption vs BitLocker?
- eDrive/self-encrypting drives?
- Storage encryption pertence mais a Security?
- DirectStorage runtime capability possui API para detectar suporte?
- BypassIO merece capability?
- DirectStorage compression/GDeflate pertence à GPU/Gaming?
- Como medir jogo carregando assets?
- Devemos ter game loading benchmark?
- Pode existir “storage-induced stutter” score?
- Como correlacionar page faults, storage read latency e frametime?
- NVMe temperature sampling overhead?
- SSD thermal throttle flags são padronizados?
- Como detectar PCIe correctable errors relacionados ao SSD?
- WHEA pode ajudar?
- Como tratar USB SSDs?
- UASP vs BOT deve ser detectado?
- USB negotiated speed?
- External SSD thermal/performance diagnostics?
- Removable drive write caching?
- Safe removal?
- Battery-backed RAID cache?
- Overprovisioning pode ser detectado?
- Free space influencia SSD GC/performance?
- Deveríamos recomendar manter espaço livre em SSD?
- Qual percentual sem cair em número mágico?
- TRIM + garbage collection são coisas diferentes.
- Storage benchmark deve usar incompressible data?
- Compressible synthetic data pode inflar alguns controladores antigos?
- Cache do Windows deve ser bypassado?
- Unbuffered I/O?
- Alignment?
- Direct I/O?
- Qual tamanho de arquivo evita medir RAM cache?
- Random test deve usar QD1 como padrão desktop?
- Before/After cleanup realmente precisa benchmark?
- Limpeza raramente aumenta performance — devemos medir espaço, não FPS.

---

# Avaliação inicial

A primeira passada encontrou aproximadamente 128 capacidades, subcapacidades e princípios.

Os grupos mais fortes são:

## Hardware
- HDD/SSD/NVMe;
- interface;
- controller;
- firmware;
- PCIe/SATA link.

## Reliability
- SMART;
- reliability counters;
- NVMe health;
- temperature;
- wear;
- errors.

## Volumes / Filesystems
- partitions;
- filesystems;
- CHKDSK;
- free space.

## Maintenance
- TRIM;
- ReTrim;
- Optimize Volume;
- HDD defrag;
- Storage Spaces maintenance.

## Performance
- throughput;
- latency;
- queue;
- sustained behavior;
- thermal throttling;
- cache exhaustion.

## Cleanup
- temporary data;
- caches;
- Windows.old;
- dumps;
- large files;
- user-data safeguards.

## Security / Recovery
- BitLocker;
- Shadow Storage;
- Restore storage.

## Gaming
- DirectStorage;
- game location;
- storage/stutter correlation.

## Benchmark
- sequential;
- random;
- latency;
- QD;
- sustained;
- before/after.

Estado:

FIRST PASS COMPLETE

Próximo passo:

SECOND PASS — STORAGE GAP AUDIT

# Discovery Catalog — Armazenamento
## Segunda passada — auditoria de lacunas

> Este bloco complementa os itens 1–128 da primeira passada.
>
> Foco:
>
> - NVMe power management;
> - APST / runtime power states;
> - Host Memory Buffer;
> - DRAM-less SSDs;
> - VMD / RST / RAID;
> - TRIM end-to-end;
> - VSS / Shadow Storage;
> - Reserved Storage;
> - BypassIO;
> - DirectStorage stack readiness;
> - USB SSD / UASP;
> - free-space / overprovisioning;
> - metodologia de benchmark.

---

# 129. NVMe Power State Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVMe / Power

Primary Product Area:
Storage

Possible Consumers:
- Energy
- Laptop
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar capacidades de power states expostas pelo dispositivo NVMe.

Possível objetivo:
Compreender comportamento de idle e wake latency.

Possível benefício:
Dar contexto a consumo e latência.

Aplicabilidade:
NVMe SSD

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
NVMe-standard / Windows-supported

Action Mode:
Diagnostic

Possíveis informações:
- operational states;
- non-operational states;
- entry latency;
- exit latency;
- relative performance.

---

# 130. NVMe Runtime Power Management Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVMe / Power

Primary Product Area:
Storage

Possible Consumers:
- Energy
- Laptop
- Benchmark

Descrição:
Analisar comportamento de transição do NVMe para estados de menor consumo.

Possível objetivo:
Explicar trade-off entre idle power e wake latency.

Possível benefício:
Diagnóstico de notebook e responsiveness.

Evidence Level:
Established Windows mechanism

Action Mode:
Diagnostic

---

# 131. NVMe APST Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVMe / Power

Primary Product Area:
Storage

Descrição:
Detectar suporte ao Autonomous Power State Transition.

Possível objetivo:
Conhecer capabilities do controller.

Evidence Level:
Established NVMe feature

Action Mode:
Diagnostic

---

# 132. NVMe APST State Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
NVMe Power

Primary Product Area:
Storage

Descrição:
Investigar leitura do estado efetivo de APST.

Possível objetivo:
Distinguir capacidade de configuração real.

Risco inicial:
Low

Evidence Level:
Windows/NVMe mechanisms established; observability needs validation

Action Mode:
Diagnostic

---

# 133. NVMe APST Tuning

Status:
QUESTIONABLE

Type:
Advanced Configuration

Technical Domain:
NVMe / Power

Primary Product Area:
Advanced / Energy

Descrição:
Investigar tecnicamente mudanças em power state transitions.

Possível benefício:
Trade-off entre consumo e wake latency.

Risco inicial:
High

Evidence Level:
Mechanism established / benefit hardware-specific

Action Mode:
Benchmark First / Advanced Only

Importante:
Não transformar “disable NVMe power saving” em otimização universal.

---

# 134. NVMe Power-State Latency Analysis

Status:
IDEA

Type:
Diagnostic / Benchmark

Technical Domain:
NVMe Power

Primary Product Area:
Storage

Possible Consumers:
- Gaming
- Energy

Descrição:
Comparar entry/exit latency de estados energéticos com comportamento real de I/O.

Possível objetivo:
Investigar casos raros de latência após idle.

Action Mode:
Diagnostic / Benchmark

---

# 135. NVMe Power Management Myth Guard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
NVMe Power

Primary Product Area:
Shared

Descrição:
Evitar regra:

“Maximum Performance NVMe power state = melhor PC.”

Possível objetivo:
Preservar gerenciamento energético correto quando não existe problema mensurável.

Action Mode:
Internal Safeguard

---

# 136. Host Memory Buffer Capability Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
NVMe

Primary Product Area:
Storage

Possible Consumers:
- My PC
- Benchmark

Descrição:
Investigar capacidade de identificar NVMe Host Memory Buffer.

Possível objetivo:
Dar contexto a SSDs que utilizam RAM do host para estruturas de mapeamento.

Possível benefício:
Melhor hardware information.

Aplicabilidade:
Compatible NVMe SSDs

Risco inicial:
Low

Evidence Level:
NVMe feature / Windows observability requires research

Action Mode:
Diagnostic

---

# 137. DRAM-less SSD Context Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
SSD Architecture

Primary Product Area:
My PC

Possible Consumers:
- Storage
- Benchmark

Descrição:
Investigar capacidade de identificar unidades sem DRAM dedicada.

Possível objetivo:
Contextualizar comportamento de determinadas unidades.

Possível benefício:
Explicar diferenças de sustained/random performance.

Evidence Level:
Usually requires vendor/model data

Action Mode:
Diagnostic

Importante:
“DRAM-less” não significa automaticamente SSD ruim.

---

# 138. SSD Architecture Metadata Layer

Status:
IDEA

Type:
Shared Diagnostic Capability

Technical Domain:
Storage Hardware

Primary Product Area:
Shared

Possible Consumers:
- My PC
- Benchmark

Descrição:
Permitir enriquecer informações do dispositivo através de base específica de modelos.

Possíveis informações:
- NAND type;
- controller;
- DRAM;
- HMB;
- rated endurance;
- interface maximum.

Risco inicial:
Medium

Evidence Level:
Requires maintained external dataset

Action Mode:
Diagnostic

---

# 139. SSD Rated Endurance Context

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
SSD Reliability

Primary Product Area:
My PC

Descrição:
Relacionar host writes e indicadores de wear a endurance especificada pelo fabricante quando houver informação confiável.

Possível objetivo:
Dar contexto ao desgaste.

Importante:
Não prever “dias restantes”.

Action Mode:
Diagnostic

---

# 140. Host Reads / Writes Monitoring

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVMe / SSD

Primary Product Area:
My PC

Possible Consumers:
- Monitoring
- Reliability

Descrição:
Mostrar quantidade acumulada de dados lidos/escritos quando dispositivo expuser contador.

Possível benefício:
Contexto de utilização e wear.

Action Mode:
Diagnostic

---

# 141. NVMe Controller Busy Time

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVMe

Primary Product Area:
Storage

Descrição:
Consultar tempo acumulado em que controller esteve ocupado quando disponível.

Possível objetivo:
Dar contexto ao histórico de utilização.

Action Mode:
Diagnostic

---

# 142. NVMe Critical Warning Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVMe Reliability

Primary Product Area:
Repair

Descrição:
Interpretar Critical Warning do health log NVMe.

Possível objetivo:
Alertar imediatamente sobre condições importantes.

Possíveis categorias:
- spare below threshold;
- temperature issue;
- reliability degraded;
- media read-only;
- volatile backup failure quando aplicável.

Action Mode:
Urgent Diagnostic

---

# 143. NVMe Available Spare Monitoring

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVMe Reliability

Primary Product Area:
Storage

Descrição:
Monitorar spare disponível e threshold fornecido pelo controller.

Possível objetivo:
Identificar degradação.

Action Mode:
Diagnostic

---

# 144. NVMe Media/Data Integrity Error Trend

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
NVMe Reliability

Primary Product Area:
Monitoring

Descrição:
Registrar evolução de media/data integrity errors.

Possível objetivo:
Detectar degradação progressiva.

Action Mode:
Diagnostic / Monitoring

---

# 145. Storage Controller Stack Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Drivers

Primary Product Area:
Shared

Possible Consumers:
- Storage
- Drivers
- DirectStorage

Descrição:
Identificar caminho de software/controlador usado pela unidade.

Possíveis estados:
- Microsoft StorNVMe;
- vendor NVMe driver;
- Intel RST/VMD;
- AMD RAID;
- USB bridge;
- Storage Spaces;
- virtual storage.

Possível objetivo:
Evitar assumir que todo NVMe é acessado diretamente pelo mesmo driver.

Action Mode:
Automatic

---

# 146. Intel VMD Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Intel Storage

Primary Product Area:
Storage

Possible Consumers:
- Drivers
- My PC

Descrição:
Detectar NVMe gerenciado por Intel Volume Management Device.

Possível objetivo:
Explicar topologia e dependência de drivers RST.

Aplicabilidade:
Supported Intel Platforms

Evidence Level:
Established Intel technology

Action Mode:
Diagnostic

---

# 147. Intel RST Storage Context

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Intel Storage

Primary Product Area:
Storage

Descrição:
Identificar unidades/arrays sob Intel Rapid Storage Technology.

Possível objetivo:
Evitar tratar dispositivo gerenciado por RST como NVMe standalone.

Action Mode:
Diagnostic

---

# 148. VMD/RST Driver Health

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Drivers

Primary Product Area:
Repair

Descrição:
Verificar instalação e estado de controllers VMD/RST.

Possível objetivo:
Detectar drivers ausentes ou dispositivos com erro.

Action Mode:
Diagnostic

---

# 149. VMD Disable Optimization

Status:
REJECTED

Type:
Optimization

Technical Domain:
Storage

Descrição:
Desativar VMD/RST apenas para suposto ganho de desempenho.

Motivo:
Pode alterar completamente a forma como o sistema acessa o disco, quebrar boot ou arrays RAID.

Action Mode:
None

---

# 150. RAID Configuration Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage / RAID

Primary Product Area:
My PC

Descrição:
Identificar quando discos participam de RAID gerenciado por hardware/firmware/software reconhecível.

Possível objetivo:
Não diagnosticar cada mídia fora do contexto do array.

Action Mode:
Automatic

---

# 151. RAID Health Context

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Reliability

Primary Product Area:
Storage

Descrição:
Diferenciar saúde do array de saúde de cada membro quando dados estiverem disponíveis.

Possível objetivo:
Detectar degraded arrays.

Action Mode:
Diagnostic

---

# 152. RAID Modification Guard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
Storage

Primary Product Area:
Shared

Descrição:
Nunca alterar modo RAID/AHCI/VMD automaticamente.

Possível objetivo:
Evitar perda de boot ou acesso aos dados.

Action Mode:
Internal Safeguard

---

# 153. TRIM Filesystem State Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
SSD Maintenance

Primary Product Area:
Storage

Descrição:
Consultar se delete notifications/TRIM estão habilitadas no filesystem.

Possível objetivo:
Encontrar configuração Windows-side anormal.

Action Mode:
Diagnostic

---

# 154. TRIM End-to-End Confidence

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
SSD Maintenance

Primary Product Area:
Storage

Descrição:
Investigar até onde é possível provar que TRIM chegou efetivamente ao dispositivo.

Possível objetivo:
Distinguir:
“Windows está configurado para emitir TRIM”
de
“dispositivo recebeu/processou TRIM”.

Evidence Level:
Requires technical validation

Action Mode:
Diagnostic

---

# 155. TRIM Pass-Through Context

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Storage Stack

Primary Product Area:
Storage

Descrição:
Investigar suporte a TRIM quando existem camadas intermediárias.

Possíveis cenários:
- RAID;
- VMD;
- Storage Spaces;
- USB bridge;
- virtual disk.

Possível objetivo:
Evitar conclusão apenas pelo filesystem flag.

---

# 156. ReTRIM Need Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
SSD Maintenance

Primary Product Area:
Optimization

Descrição:
Determinar se ReTrim faz sentido com base no tipo de volume e manutenção recente.

Possível objetivo:
Evitar executar manualmente sem necessidade.

Action Mode:
Automatic Diagnostic

---

# 157. Reserved Storage State Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Windows Storage

Primary Product Area:
Storage

Possible Consumers:
- Windows Update
- My PC

Descrição:
Detectar estado do Reserved Storage do Windows.

Possível objetivo:
Explicar espaço aparentemente não disponível e dar contexto a updates.

Evidence Level:
Established

Action Mode:
Automatic

---

# 158. Reserved Storage Explanation

Status:
CANDIDATE

Type:
Explainability

Technical Domain:
Windows Storage

Primary Product Area:
My PC

Descrição:
Explicar que Reserved Storage existe para aumentar a chance de updates terem espaço suficiente.

Possível objetivo:
Evitar tratá-lo como “Windows roubando espaço”.

Action Mode:
Automatic Explanation

---

# 159. Disable Reserved Storage for Optimization

Status:
QUESTIONABLE

Type:
Configuration

Technical Domain:
Windows Storage

Primary Product Area:
Advanced / TBD

Descrição:
Registrar capacidade, mas não recomendar como cleanup comum.

Possível benefício:
Espaço adicional em cenário restrito.

Risco inicial:
Medium

Evidence Level:
Supported management feature / questionable optimization benefit

Action Mode:
Contextual / Manual

Importante:
Pode reduzir margem disponível para Windows Update.

---

# 160. VSS Snapshot Inventory

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
VSS / Recovery

Primary Product Area:
Storage

Possible Consumers:
- Recovery
- Cleanup
- Repair

Descrição:
Listar shadow copies existentes.

Possível objetivo:
Explicar consumo de espaço e proteger pontos de recuperação.

Action Mode:
Diagnostic

---

# 161. Shadow Storage Allocation Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
VSS

Primary Product Area:
Storage

Descrição:
Detectar volume que hospeda shadow storage e limite configurado.

Possível objetivo:
Explicar consumo invisível de espaço.

Action Mode:
Diagnostic

---

# 162. Shadow Storage Usage Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
VSS

Primary Product Area:
Storage

Descrição:
Comparar espaço reservado/utilizado por snapshots ao volume.

Possível objetivo:
Detectar pressão real.

Action Mode:
Diagnostic

---

# 163. Shadow Storage Resize Advisor

Status:
RESEARCHING

Type:
Configuration / Recommendation

Technical Domain:
VSS

Primary Product Area:
Storage

Descrição:
Avaliar alterações de limite apenas quando houver problema concreto.

Risco inicial:
High

Evidence Level:
Supported operation with destructive side effects

Action Mode:
Explicit User Choice

Importante:
Reduzir o limite pode causar exclusão de cópias de sombra existentes.

---

# 164. Delete Restore Points for Space

Status:
QUESTIONABLE

Type:
Cleanup

Technical Domain:
Recovery

Primary Product Area:
Cleanup

Descrição:
Permitir exclusão apenas com contexto explícito de recovery.

Risco inicial:
High

Action Mode:
Explicit User Choice

Importante:
Não classificar restore points como lixo.

---

# 165. VSS Writer Health

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
VSS

Primary Product Area:
Repair

Descrição:
Verificar writers VSS registrados e estados de erro.

Possível objetivo:
Diagnosticar problemas de backup/System Restore.

Action Mode:
Diagnostic

---

# 166. VSS Provider Inventory

Status:
IDEA

Type:
Diagnostic

Technical Domain:
VSS

Primary Product Area:
Repair

Descrição:
Identificar providers instalados.

Possível objetivo:
Troubleshooting de conflitos entre backup software e VSS.

Action Mode:
Diagnostic

---

# 167. Orphaned / Excessive Snapshot Diagnosis

Status:
IDEA

Type:
Diagnostic

Technical Domain:
VSS

Primary Product Area:
Storage

Descrição:
Investigar conjuntos de snapshots incomuns ou consumo excessivo.

Possível objetivo:
Encontrar software de backup problemático.

Risco inicial:
Low

Action Mode:
Diagnostic First

---

# 168. BypassIO Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Windows Storage / Gaming

Primary Product Area:
Shared

Possible Consumers:
- Gaming
- DirectStorage
- Storage

Descrição:
Verificar se BypassIO é suportado em determinado volume/path.

Possível objetivo:
Avaliar stack de armazenamento relevante para jogos modernos.

Applicability:
Windows 11

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 169. BypassIO Blocking Driver Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage Drivers

Primary Product Area:
Gaming

Possible Consumers:
- Drivers
- Repair

Descrição:
Identificar primeiro driver/minifilter impedindo BypassIO.

Possível objetivo:
Explicar por que caminho otimizado não está disponível.

Possível benefício:
Diagnóstico específico e acionável.

Action Mode:
Diagnostic

---

# 170. BypassIO Partial Support Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Storage / DirectStorage

Primary Product Area:
Gaming

Descrição:
Distinguir suporte total, parcial ou ausente.

Possível objetivo:
Evitar resultado binário simplista.

Action Mode:
Diagnostic

---

# 171. BypassIO Filter Compatibility Advisor

Status:
IDEA

Type:
Diagnostic / Recommendation

Technical Domain:
Storage Filters

Primary Product Area:
Gaming

Descrição:
Explicar quando antivirus, encryption ou outro minifilter limita partes do caminho BypassIO.

Possível objetivo:
Identificar causa sem sugerir imediatamente remover proteção.

Action Mode:
Diagnostic First

Importante:
Driver bloquear BypassIO não significa que deve ser removido.

---

# 172. Disable Security Filter for BypassIO

Status:
REJECTED

Type:
Optimization

Descrição:
Desativar antivirus, encryption ou security filters automaticamente para liberar BypassIO.

Motivo:
Sacrifica proteção para obter potencial ganho de I/O.

---

# 173. DirectStorage Stack Readiness

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Gaming / Storage

Primary Product Area:
Gaming

Descrição:
Avaliar conjunto completo de condições relevantes para DirectStorage.

Possíveis camadas:
- OS;
- filesystem;
- BypassIO;
- storage driver;
- storage type;
- GPU;
- game support.

Action Mode:
Diagnostic

---

# 174. DirectStorage Limiter Explanation

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Gaming / Storage

Primary Product Area:
Gaming

Descrição:
Explicar qual componente impede determinada capacidade.

Exemplo:

DirectStorage:
Supported

BypassIO:
Partial

Blocking component:
Storage minifilter X

Action Mode:
Diagnostic

---

# 175. External Storage Transport Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
USB Storage

Primary Product Area:
Storage

Possible Consumers:
- USB
- My PC

Descrição:
Identificar armazenamento externo e protocolo de transporte quando possível.

Possível objetivo:
Diferenciar SSD rápido limitado por USB/protocolo.

Action Mode:
Diagnostic

---

# 176. USB Storage Negotiated Speed

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
USB / Storage

Primary Product Area:
Storage

Descrição:
Detectar velocidade de conexão quando exposta.

Possível objetivo:
Encontrar SSD externo conectado a porta/cabo lento.

Action Mode:
Diagnostic

---

# 177. UASP Capability Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
USB Storage

Primary Product Area:
Storage

Descrição:
Investigar identificação de dispositivos usando USB Attached SCSI Protocol em vez de transporte legado.

Possível objetivo:
Dar contexto a queueing/performance de SSD externo.

Evidence Level:
Established USB storage concept / Windows identification needs validation

Action Mode:
Diagnostic

---

# 178. External SSD Interface Bottleneck

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
USB Storage

Primary Product Area:
Storage

Descrição:
Comparar desempenho da unidade ao limite prático da conexão externa.

Possível objetivo:
Explicar unidade NVMe externa aparentemente lenta.

Possíveis causas:
- USB 2;
- USB 5 Gbps;
- USB 10/20 Gbps;
- bridge;
- cable;
- hub.

Action Mode:
Diagnostic

---

# 179. USB Storage Bridge Context

Status:
IDEA

Type:
Diagnostic

Technical Domain:
USB Storage

Primary Product Area:
Storage

Descrição:
Reconhecer que enclosure/bridge pode impedir telemetria SMART/NVMe ou limitar comandos.

Possível objetivo:
Explicar ausência de health data.

Action Mode:
Diagnostic

---

# 180. External Storage Thermal Analysis

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Storage Thermal

Primary Product Area:
Storage

Descrição:
Monitorar SSD externo quando temperatura estiver disponível.

Possível objetivo:
Detectar enclosure causando throttling.

Action Mode:
Diagnostic

---

# 181. SSD Free-Space Performance Context

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
SSD Performance

Primary Product Area:
Storage

Descrição:
Considerar espaço disponível ao interpretar desempenho sustentado e garbage collection.

Possível objetivo:
Investigar SSD muito cheio.

Possível benefício:
Contextual.

Action Mode:
Diagnostic

Importante:
Não criar regra universal como “sempre deixe exatamente 20% livre”.

---

# 182. Low SSD Free-Space Warning

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
SSD Capacity

Primary Product Area:
Optimization

Descrição:
Alertar quando espaço extremamente baixo coincide com problemas práticos.

Possíveis impactos:
- updates;
- caches;
- pagefile;
- temp;
- application writes;
- SSD management.

Action Mode:
Diagnostic

---

# 183. SSD Overprovisioning Awareness

Status:
IDEA

Type:
Diagnostic

Technical Domain:
SSD

Primary Product Area:
Storage

Descrição:
Reconhecer diferença entre espaço não utilizado pelo usuário e overprovisioning interno do fabricante.

Possível objetivo:
Evitar promessas simplistas de que deixar partição vazia equivale exatamente a OP interno.

Action Mode:
Explainability

---

# 184. Manual Overprovisioning Recommendation

Status:
QUESTIONABLE

Type:
Optimization

Technical Domain:
SSD

Primary Product Area:
Advanced / TBD

Descrição:
Reservar espaço não particionado deliberadamente.

Possível benefício:
Possível efeito em workloads específicos.

Evidence Level:
Device/workload dependent

Action Mode:
Research / Advanced Only

Importante:
Não criar percentual mágico.

---

# 185. Storage Benchmark Cache-Control Strategy

Status:
CANDIDATE

Type:
Benchmark Infrastructure

Technical Domain:
Storage Benchmark

Primary Product Area:
Shared

Descrição:
Garantir que benchmark de armazenamento não seja predominantemente benchmark de RAM/cache do Windows.

Possível objetivo:
Medir dispositivo real.

Action Mode:
Internal

Possíveis técnicas:
- arquivo suficientemente grande;
- appropriate buffering flags;
- flush/control between runs.

---

# 186. Buffered vs Unbuffered Storage Benchmark

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Descrição:
Distinguir testes que incluem cache do sistema daqueles destinados ao dispositivo.

Possível objetivo:
Responder perguntas diferentes de forma explícita.

Action Mode:
Benchmark

---

# 187. Storage Benchmark Test-File Sizing

Status:
CANDIDATE

Type:
Benchmark Safeguard

Technical Domain:
Storage Benchmark

Primary Product Area:
Benchmark

Descrição:
Selecionar tamanho de arquivo com base em:
- RAM;
- free disk space;
- objetivo do teste;
- provável SSD cache.

Possível objetivo:
Evitar resultados artificiais.

Action Mode:
Automatic

---

# 188. Compressibility-Safe Benchmark Data

Status:
CANDIDATE

Type:
Benchmark Safeguard

Technical Domain:
Storage Benchmark

Primary Product Area:
Benchmark

Descrição:
Usar dados apropriados para evitar que compressão interna ou filesystem distorça resultados quando objetivo é medir throughput bruto.

Action Mode:
Internal

---

# 189. Storage Benchmark Alignment

Status:
CANDIDATE

Type:
Benchmark Safeguard

Technical Domain:
Storage Benchmark

Primary Product Area:
Benchmark

Descrição:
Garantir alinhamento adequado para operações de teste.

Possível objetivo:
Evitar penalidade gerada pelo próprio benchmark.

Action Mode:
Internal

---

# 190. Storage Benchmark Queue Profile

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Descrição:
Definir explicitamente queue depth e thread count.

Possível objetivo:
Evitar comparar QD1 com resultados QD32 como se fossem equivalentes.

Action Mode:
Benchmark

---

# 191. Desktop Storage Responsiveness Profile

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Descrição:
Criar teste voltado a:
- low queue depth;
- small random reads;
- latency.

Possível objetivo:
Representar melhor uso cotidiano.

Action Mode:
Benchmark

---

# 192. Game Asset Streaming Storage Profile

Status:
IDEA

Type:
Benchmark

Technical Domain:
Gaming / Storage

Primary Product Area:
Benchmark

Descrição:
Criar workload sintético mais próximo de leitura concorrente de assets.

Possível objetivo:
Avaliar storage para jogos além de sequential MB/s.

Action Mode:
Benchmark

Dúvidas:
Como representar gameplay real sem afirmar equivalência excessiva?

---

# 193. Sustained Write Wear Budget

Status:
CANDIDATE

Type:
Safety

Technical Domain:
SSD Benchmark

Primary Product Area:
Shared

Descrição:
Controlar quantidade acumulada de dados escritos pelos benchmarks.

Possível objetivo:
Evitar desgaste desnecessário.

Action Mode:
Internal

Possível comportamento:
- track test writes;
- warn before heavy test;
- default to lightweight tests.

---

# 194. Benchmark Thermal Recovery

Status:
CANDIDATE

Type:
Benchmark Safeguard

Technical Domain:
Storage Thermal

Primary Product Area:
Benchmark

Descrição:
Permitir temperatura voltar a faixa comparável entre testes A/B.

Possível objetivo:
Evitar viés térmico.

Action Mode:
Automatic

---

# 195. Read-Only Storage Benchmark Mode

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
Storage Performance

Primary Product Area:
Benchmark

Descrição:
Oferecer perfil de teste sem gravações significativas.

Possível objetivo:
Avaliação rápida com mínimo desgaste.

Action Mode:
User-triggered

---

# 196. Storage Benchmark Result Confidence

Status:
CANDIDATE

Type:
Product Behavior

Technical Domain:
Storage Benchmark

Primary Product Area:
Benchmark

Descrição:
Avaliar variabilidade entre runs.

Possível objetivo:
Não declarar melhoria dentro do ruído.

Action Mode:
Automatic

---

# 197. Storage Optimization Validation

Status:
CANDIDATE

Type:
Validation

Technical Domain:
Storage

Primary Product Area:
Optimization

Descrição:
Validar alteração usando a métrica apropriada.

Exemplos:

Cleanup:
validate reclaimed space.

ReTrim:
validate operation completed.

Driver change:
validate latency/performance/stability.

PCIe fix:
validate negotiated link.

Possível objetivo:
Não executar benchmark irrelevante após toda ação.

Action Mode:
Automatic

---

# 198. Cleanup Performance Myth Guard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
Cleanup

Primary Product Area:
Shared

Descrição:
Não afirmar que remover arquivos temporários gera FPS ou aceleração significativa sem evidência.

Possível objetivo:
Separar:
- recuperar espaço;
de
- aumentar performance.

Action Mode:
Internal

---

# 199. Storage Change Risk Classification

Status:
CANDIDATE

Type:
Shared Capability

Technical Domain:
Storage

Primary Product Area:
Shared

Descrição:
Classificar ações segundo potencial de perda de dados ou boot.

Possíveis níveis:

Low:
- diagnostic;
- ReTrim supported by Windows.

Medium:
- delete cache;
- CHKDSK repair.

High:
- write cache changes;
- VSS resize;
- RAID configuration;
- partition operations.

Critical:
- operations capable of making OS unbootable/data inaccessible.

Action Mode:
Internal Safeguard

---

# 200. Storage Diagnostic Summary

Status:
CANDIDATE

Type:
Product Behavior

Technical Domain:
Storage

Primary Product Area:
My PC

Descrição:
Consolidar health, capacidade, configuração e performance de cada unidade.

Exemplo:

Samsung NVMe — 1 TB

Health:
Healthy

Temperature:
Normal

Wear:
8%

Interface:
PCIe 4.0 x4

Current link:
PCIe 4.0 x4

TRIM:
Available

BypassIO:
Supported

Free space:
21%

Maintenance:
No action required

ou:

Warning:
Drive healthy, but PCIe link is below expected.

ou:

Critical:
Media/Data Integrity errors increasing.

Action Mode:
Automatic

---

# Consolidações com primeira passada

## NVMe Power

Deve ser compartilhado com Energia.

Storage possui:
- device capabilities;
- power states;
- latency;
- impact.

Energy decide:
- políticas globais;
- power plan context.

---

## RAID/VMD/RST

Não são “otimizações”.

São contexto da storage stack.

---

## TRIM

Separar claramente:

1. filesystem está configurado para emitir delete notifications;
2. stack permite pass-through;
3. unidade suporta mecanismo;
4. manutenção pode executar ReTrim.

Não reduzir tudo a:
`DisableDeleteNotify = 0 → tudo perfeito`.

---

## VSS

Restore Points/Shadow Copies não são lixo.

Armazenamento apenas:
- mede;
- explica;
- detecta problemas.

Recovery decide política de proteção.

---

## BypassIO

Não é um “tweak”.

É capability da storage/filesystem/filter stack.

BeadWise deve diagnosticar por que está disponível ou bloqueado.

---

## DirectStorage

Usa informações de:
- Storage;
- GPU;
- Gaming;
- Drivers.

Não deve existir como botão “Enable DirectStorage”.

---

## SSD externals

USB terá pesquisa própria.

Storage mantém:
- dispositivo;
- bridge;
- transport;
- throughput;
- health.

---

# Itens rejeitados ou fortemente despriorizados

## Disable NVMe power saving

Não como padrão.

---

## APST OFF = gaming optimization

Não.

---

## Intel VMD OFF = faster NVMe

Rejeitado como tweak automático.

---

## AHCI sempre melhor que RST

Não assumir.

---

## RAID OFF para gaming

Não.

---

## Reserved Storage OFF

Não como cleanup padrão.

---

## Delete all Shadow Copies

Não.

---

## Reduce VSS to tiny size

Não.

---

## Remove BitLocker to get full BypassIO

Não.

---

## Disable antivirus filters for DirectStorage

Não.

---

## Force overprovisioning = 20%

Não.

---

## Benchmark SSD com centenas de GB toda vez

Não.

---

## Usar sequential QD32 como “gaming SSD score”

Não.

---

# Princípios adicionais da segunda passada

1. NVMe possui múltiplos estados de energia com latências diferentes.

2. Power management de NVMe é parcialmente coordenado pelo Windows storage driver.

3. Estado mais profundo nem sempre é melhor para responsiveness.

4. Estado de máxima performance nem sempre é melhor para eficiência.

5. HMB é feature do dispositivo, não RAM stealing problem.

6. DRAM-less não é automaticamente ruim.

7. Storage stack precisa ser conhecida antes de diagnóstico.

8. VMD/RST pode esconder topologia direta do NVMe.

9. RAID precisa ser analisado como array.

10. Mudar controller mode pode impedir boot.

11. TRIM configurado no filesystem não prova sozinho funcionamento end-to-end.

12. Reserved Storage existe por motivo operacional.

13. Shadow Copies possuem valor de recuperação.

14. Redimensionar shadow storage pode destruir snapshots.

15. BypassIO reduz overhead de CPU em reads.

16. BypassIO pode ser bloqueado por minifilters.

17. Um filter blocking BypassIO não é automaticamente software ruim.

18. Security deve vencer ganho marginal de I/O quando houver conflito.

19. DirectStorage readiness é uma cadeia de capabilities.

20. SSD externo pode estar limitado pelo enclosure, cabo ou porta.

21. Ausência de SMART em USB não significa ausência de health support do SSD — o bridge pode impedir acesso.

22. Espaço livre influencia vários comportamentos, mas percentuais mágicos devem ser evitados.

23. Benchmark precisa controlar cache de RAM.

24. Benchmark precisa declarar queue depth.

25. QD1 e QD32 medem workloads diferentes.

26. Benchmark de escrita tem custo físico.

27. Sustained test deve ser explícito.

28. Temperatura precisa ser controlada em comparação A/B.

29. Cleanup recupera espaço; não significa automaticamente maior performance.

30. Storage é uma das áreas onde segurança de dados deve ter prioridade máxima.

---

# Resultado da segunda passada

Primeira passada:
~128 itens.

Segunda passada:
~72 capacidades/subcapacidades adicionais.

Total bruto:
~200 itens.

Esse número é propositalmente alto nesta fase e deverá cair bastante durante consolidação.

---

# Grupos consolidados que emergem

## Hardware Identification
- drive;
- media;
- bus;
- firmware;
- controller;
- physical topology.

## NVMe
- health log;
- wear;
- errors;
- power states;
- PCIe link;
- HMB/context.

## SATA / RAID / VMD
- controller;
- negotiated interface;
- array context;
- driver health.

## Reliability
- SMART;
- NVMe health;
- errors;
- thermal;
- trend monitoring.

## Filesystem
- NTFS/ReFS/etc.;
- CHKDSK;
- TRIM;
- fragmentation;
- compression.

## Maintenance
- ReTrim;
- defrag;
- tier optimization;
- schedule.

## Capacity / Cleanup
- free space;
- largest consumers;
- caches;
- Windows.old;
- Reserved Storage.

## Recovery Storage
- VSS;
- shadow storage;
- restore-point space.

## Gaming
- DirectStorage;
- BypassIO;
- game storage;
- stutter correlation.

## External Storage
- USB transport;
- negotiated speed;
- UASP;
- enclosure bottlenecks.

## Benchmark
- sequential;
- random;
- low-QD;
- latency;
- sustained;
- cache control;
- thermal control;
- wear safeguards.

---

# Áreas que assumem aprofundamentos futuros

## Energy
- NVMe power states;
- runtime idle;
- power/latency policy.

## Gaming
- DirectStorage;
- BypassIO;
- asset streaming;
- loading benchmarks.

## USB
- UASP;
- negotiated speed;
- bridge behavior.

## Drivers
- storage filters;
- controller drivers;
- VMD/RST;
- resets.

## Security
- BitLocker;
- encryption mode;
- security filters.

## Recovery
- VSS;
- Restore Points;
- Shadow Storage.

## PCIe
- NVMe lanes;
- generation;
- WHEA;
- link errors.

---

# Avaliação de maturidade

A segunda passada encontrou principalmente:

- subcapacidades NVMe;
- contexto de storage stack;
- recovery storage;
- DirectStorage infrastructure;
- external storage;
- benchmark methodology.

Poucos novos domínios completos apareceram.

Novas pesquisas amplas agora provavelmente retornarão:

- propriedades NVMe individuais;
- comandos específicos;
- detalhes de protocolos;
- capacidades pertencentes às próximas áreas.

Estado sugerido:

**INITIAL DISCOVERY COMPLETE**