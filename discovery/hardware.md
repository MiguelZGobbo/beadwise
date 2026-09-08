# BeadWise V2 — Discovery — Hardware Diagnostics — First Pass

**Status da área:** INITIAL DISCOVERY — FIRST PASS

**Regra canônica:** `01-DISCOVERY.md`  
**Filosofia:** Detect → Understand → Recommend → Apply → Measure → Keep / Rollback  
**Filtro:** preservar UX, segurança, estabilidade e reversibilidade; evitar granularidade por comando, chave, sensor ou parâmetro.

# 1. Hardware Inventory Baseline

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Hardware

Primary Product Area: My PC

Possible Consumers:

- My PC
- Hardware Diagnostics
- Optimization

Final UI Placement: TBD

Descrição: Construir baseline consolidado de componentes físicos e topologia relevante.

Possível objetivo: Dar contexto para todos os diagnósticos sem duplicar CPU/GPU/RAM/etc.

Possível benefício: Diagnostic

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- CPU
- GPU
- RAM
- motherboard
- firmware
- storage
- network
- display
- battery/devices

---

# 2. Device Manager Health Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: PnP / Devices

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Drivers
- Repair

Final UI Placement: TBD

Descrição: Consolidar devices com status problemático e códigos PnP.

Possível objetivo: Detectar hardware/driver não configurado ou com erro.

Possível benefício: Diagnostic, Repair

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- device status
- problem code
- class
- driver binding
- recent change

---

# 3. Hardware Error Event Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: WHEA / Eventing

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- System
- Stability

Final UI Placement: TBD

Descrição: Correlacionar WHEA e eventos de hardware com tempo, carga e componente.

Possível objetivo: Encontrar evidência de instabilidade física sem diagnosticar por um único evento.

Possível benefício: Diagnostic, Stability

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- WHEA events
- corrected/uncorrected type
- component source
- frequency
- load correlation

---

# 4. Unexpected Shutdown / Hardware Failure Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: System / Hardware

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- System

Final UI Placement: TBD

Descrição: Relacionar Kernel-Power/unexpected shutdown com WHEA, temperatures, PSU suspicion, crash logs e user shutdown context.

Possível objetivo: Evitar declarar ‘fonte ruim’ a partir de Event 41 sozinho.

Possível benefício: Diagnostic, Stability

Aplicabilidade: Systems with sudden shutdowns

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- shutdown events
- bugcheck presence
- WHEA
- thermal data
- power-loss pattern

---

# 5. Memory Hardware Error Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: RAM / WHEA

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- RAM

Final UI Placement: TBD

Descrição: Consolidar sinais de RAM instável, ECC/PFA quando disponível e resultado de testes.

Possível objetivo: Identificar suspeita de memória física sem criar item por timing/error counter.

Possível benefício: Diagnostic, Stability

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- memory test result
- WHEA memory errors
- ECC/PFA data where supported
- XMP/EXPO context
- error reproducibility

---

# 6. Storage Hardware Health Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Storage Hardware

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Storage

Final UI Placement: TBD

Descrição: Consumir SMART/NVMe health e erros de I/O em visão consolidada.

Possível objetivo: Detectar drive degradando sem usar health percentage isolado como sentença.

Possível benefício: Diagnostic, Stability

Aplicabilidade: Storage devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- SMART/NVMe health
- media errors
- critical warnings
- temperature
- reallocated/pending where applicable
- I/O errors

---

# 7. GPU Hardware Stability Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: GPU Hardware

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- GPU
- Gaming

Final UI Placement: TBD

Descrição: Correlacionar crashes, WHEA/PCIe, thermals, clocks e stress behavior.

Possível objetivo: Separar driver/game issue de suspeita de hardware.

Possível benefício: Diagnostic, Stability

Aplicabilidade: GPU systems

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- GPU crash history
- PCIe errors
- temperature
- power behavior
- artifact reports
- stress reproducibility

---

# 8. CPU Hardware Stability Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: CPU Hardware

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- CPU

Final UI Placement: TBD

Descrição: Correlacionar machine checks, thermals, clocks e stress results.

Possível objetivo: Detectar instabilidade física/firmware/tuning sem culpar CPU por uso alto.

Possível benefício: Diagnostic, Stability

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- WHEA/MCE
- thermal throttling
- clock behavior
- OC/undervolt context
- stress reproducibility

---

# 9. PCIe Link Health & Error Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: PCIe

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- GPU
- Storage
- Network

Final UI Placement: TBD

Descrição: Avaliar link width/speed esperado e erros relevantes sem transformar negociação de link em tweak.

Possível objetivo: Detectar seating/link/compatibility issues.

Possível benefício: Diagnostic, Compatibility

Aplicabilidade: PCIe devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- negotiated link
- expected capability
- WHEA/AER-like evidence exposed
- power state context

---

# 10. USB / Peripheral Hardware Reliability Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB / Devices

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- USB

Final UI Placement: TBD

Descrição: Correlacionar disconnects, enumeration failures, hubs, power e device errors.

Possível objetivo: Distinguir periférico, cabo, hub, porta, driver e power issue.

Possível benefício: Diagnostic, Stability

Aplicabilidade: USB/peripheral issues

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- disconnect timeline
- hub topology
- power events
- device error
- port/cable swap evidence

---

# 11. Network Adapter Hardware Health Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Hardware

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Network

Final UI Placement: TBD

Descrição: Consolidar device status, link negotiation, errors e driver/hardware signals.

Possível objetivo: Separar problema físico de problema TCP/DNS.

Possível benefício: Diagnostic, Connectivity

Aplicabilidade: Network adapters

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- link state/speed
- device errors
- interface counters
- driver state
- cable/Wi-Fi signal context

---

# 12. Display / Monitor Hardware Detection Health

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Hardware

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Display

Final UI Placement: TBD

Descrição: Detectar monitor/EDID/topology inconsistencies e disconnects.

Possível objetivo: Investigar display issues sem reduzir resolução/refresh como solução genérica.

Possível benefício: Diagnostic, Compatibility

Aplicabilidade: Display systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- EDID/monitor identity
- connection type
- link state
- native modes
- disconnect history where available

---

# 13. Firmware / BIOS Context Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Firmware

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- My PC
- Drivers

Final UI Placement: TBD

Descrição: Registrar BIOS/UEFI version, mode e relevant platform context sem universal BIOS updater.

Possível objetivo: Dar contexto a compatibility/stability diagnostics.

Possível benefício: Diagnostic, Compatibility

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- vendor/model
- BIOS/UEFI version/date
- UEFI/legacy
- platform security capability
- known-update research hook

---

# 14. Hardware Sensor Availability Map

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Sensors

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Temperature
- Monitoring

Final UI Placement: TBD

Descrição: Mapear sensores realmente disponíveis por componente sem inventar dados ausentes.

Possível objetivo: Saber o que pode ou não ser monitorado com confiança.

Possível benefício: Diagnostic

Aplicabilidade: Hardware-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- sensor source
- component mapping
- availability
- update rate
- confidence

---

# 15. Hardware Thermal Stress Evidence

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Thermal / Hardware

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Temperature
- CPU
- GPU

Final UI Placement: TBD

Descrição: Correlacionar temperaturas e throttling sob carga em vez de julgar snapshot idle.

Possível objetivo: Detectar cooling/thermal problems reais.

Possível benefício: Diagnostic, Stability

Aplicabilidade: Systems with sensors

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Benchmark First

Possíveis informações / dados / checks:

- idle/load temperatures
- throttling flags
- clock reduction
- ambient/context
- sustained duration

---

# 16. Power Delivery / PSU Symptom Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Power / Hardware

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- System

Final UI Placement: TBD

Descrição: Classificar padrões compatíveis com power delivery sem alegar medição de PSU que software não possui.

Possível objetivo: Sinalizar suspeita quando shutdowns/load spikes apontam nessa direção.

Possível benefício: Diagnostic, Stability

Aplicabilidade: Desktops/workstations

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- load-triggered shutdowns
- GPU/CPU power context
- WHEA
- voltage sensor caveats
- physical checks guidance

---

# 17. Hardware Change Detection

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Hardware / History

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- My PC

Final UI Placement: TBD

Descrição: Detectar mudanças de componentes/topologia entre snapshots.

Possível objetivo: Correlacionar problema com upgrade, replacement ou reconnect.

Possível benefício: Diagnostic

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- added/removed device
- firmware change
- memory/storage topology change
- timestamp

---

# 18. Hardware Stress Test Orchestration

Status: CANDIDATE

Type: Benchmark / Diagnostic

Technical Domain: Hardware Testing

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Benchmark

Final UI Placement: TBD

Descrição: Selecionar stress test apropriado por hipótese e impor limites/safeguards.

Possível objetivo: Reproduzir falha de forma controlada, não torturar hardware indiscriminadamente.

Possível benefício: Diagnostic, Stability

Aplicabilidade: Troubleshooting scenarios

Shared Capability: Yes

Risco inicial: High

Evidence Level: Context-dependent

Action Mode: Benchmark First

Possíveis informações / dados / checks:

- target component
- duration
- temperature limits
- error criteria
- stop conditions
- baseline

---

# 19. Cross-Component Bottleneck vs Fault Classification

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Hardware / Performance

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Optimization
- Gaming

Final UI Placement: TBD

Descrição: Distinguir componente apenas limitando performance de componente defeituoso/instável.

Possível objetivo: Evitar chamar 100% usage de problema de hardware.

Possível benefício: Diagnostic, Performance

Aplicabilidade: Performance complaints

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- utilization
- frametime/workload behavior
- errors
- thermal/power limits
- expected performance

---

# 20. Hardware Diagnostic Confidence & Escalation

Status: CANDIDATE

Type: Recommendation

Technical Domain: Hardware Diagnostics

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Repair

Final UI Placement: TBD

Descrição: Atribuir confiança e indicar quando teste físico/vendor/RMA é necessário.

Possível objetivo: Evitar diagnóstico definitivo com dados insuficientes.

Possível benefício: Diagnostic, Safety

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- evidence strength
- reproducibility
- alternative causes
- vendor diagnostic availability
- RMA/manual test recommendation

---

# Itens rejeitados / filtrados

- Um item por sensor — REJECTED.
- “PSU health percentage” por software genérico — REJECTED.
- Diagnosticar fonte apenas por rail readings de sensor — REJECTED.
- Declarar RAM defeituosa por uso alto — REJECTED.
- Declarar GPU defeituosa por 100% usage — REJECTED.
- Declarar SSD saudável apenas por “100% health” — REJECTED.
- BIOS updater universal — REJECTED.
- Stress test extremo automático — REJECTED.
- Benchmark abaixo da média = hardware com defeito — REJECTED.
- Event 41 = fonte defeituosa — REJECTED.

# Principais conclusões

Hardware Diagnostics deve ser uma camada de **correlação de evidências**, consumindo capabilities já descobertas em CPU/GPU/RAM/Storage/USB/Network/Display.
O valor está em diferenciar:
- limitação normal;
- configuração/driver;
- thermal/power limit;
- erro transitório;
- suspeita real de hardware.

# Fontes técnicas relevantes

- Microsoft Support — Device Manager error codes
- Microsoft Learn — WHEA
- Microsoft Support — Device performance & health
- Documentação NVMe/SMART e vendors nas futuras Feature Specs

# Status final

**Hardware Diagnostics — First Pass: INITIAL DISCOVERY COMPLETE**

Capabilities CANDIDATE: **20**

# Próximo passo

**Hardware Diagnostics — Second Pass / Consolidated Gap Audit**

# BeadWise V2 — Discovery — Hardware Diagnostics — Second Pass / Consolidated Gap Audit

**Regra canônica:** `01-DISCOVERY.md`  
**Filosofia:** Detect → Understand → Recommend → Apply → Measure → Keep / Rollback  
**Filtro:** preservar UX, segurança, estabilidade e reversibilidade; evitar granularidade por comando, chave, sensor ou parâmetro.

# 21. Hardware Vendor Diagnostic Integration Awareness

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: Hardware Diagnostics

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Support

Final UI Placement: TBD

Descrição: Reconhecer quando ferramenta oficial do fabricante oferece teste que o Windows não expõe.

Possível objetivo: Escalonar para diagnóstico mais confiável por componente.

Possível benefício: Diagnostic

Aplicabilidade: Specific hardware

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- vendor/model
- official diagnostic availability
- test result import/manual capture

---

# 22. Memory Configuration Topology Sanity Check

Status: CANDIDATE

Type: Diagnostic

Technical Domain: RAM / Hardware

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- RAM

Final UI Placement: TBD

Descrição: Avaliar população de canais, módulos e capacidade aparente sem tratar cada timing como capability.

Possível objetivo: Detectar configuração que pode explicar performance/estabilidade.

Possível benefício: Diagnostic, Performance

Aplicabilidade: Systems with multiple DIMMs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- module count
- channel topology
- reported capacity/speed
- mismatch
- XMP/EXPO context

---

# 23. Storage Connection / Path Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Storage / PCIe / SATA

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Storage

Final UI Placement: TBD

Descrição: Correlacionar drive health com interface/cable/link/controller.

Possível objetivo: Separar drive defeituoso de caminho de conexão problemático.

Possível benefício: Diagnostic, Stability

Aplicabilidade: Storage issues

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- bus type
- link negotiation
- controller
- CRC/interface errors where exposed
- disconnects

---

# 24. Intermittent Device Failure Trend

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Hardware / History

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Devices

Final UI Placement: TBD

Descrição: Detectar padrão temporal de desaparecimento, reconnect, WHEA ou erro PnP.

Possível objetivo: Encontrar falhas intermitentes que snapshot não captura.

Possível benefício: Diagnostic, Stability

Aplicabilidade: Intermittent issues

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- event frequency
- device identity
- load/sleep correlation
- driver/firmware changes

---

# 25. Hardware Diagnostic Session Baseline

Status: CANDIDATE

Type: Diagnostic / Measurement

Technical Domain: Hardware Diagnostics

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Benchmark

Final UI Placement: TBD

Descrição: Capturar baseline antes de teste para comparar erro/thermal/clock state.

Possível objetivo: Aumentar qualidade de stress diagnostics.

Possível benefício: Diagnostic

Aplicabilidade: Stress/diagnostic sessions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- pre-test sensors
- event baseline
- device state
- power mode
- ambient/context

---

# 26. Physical Check Guidance

Status: CANDIDATE

Type: Manual Guidance

Technical Domain: Hardware Support

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Repair

Final UI Placement: TBD

Descrição: Gerar passos físicos seguros quando software não consegue confirmar causa, como reseat/cable/port swap.

Possível objetivo: Fechar lacuna entre telemetria e diagnóstico real.

Possível benefício: Diagnostic, Repair

Aplicabilidade: Hardware suspicion

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- device type
- power-off requirement
- ESD caution
- cable/slot alternatives
- post-check

---

# 27. Hardware Diagnostic Evidence Bundle

Status: CANDIDATE

Type: Tool / Diagnostic

Technical Domain: Hardware Diagnostics

Primary Product Area: Hardware Diagnostics

Possible Consumers:

- Hardware Diagnostics
- Support

Final UI Placement: TBD

Descrição: Exportar resumo estruturado de inventário, erros e testes relevantes sem despejar dados desnecessários.

Possível objetivo: Facilitar suporte/RMA e comparação posterior.

Possível benefício: Diagnostic, Quality of Life

Aplicabilidade: Unresolved hardware issues

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- hardware IDs/model
- firmware
- error timeline
- test results
- privacy filtering

---

# Itens avaliados e não promovidos

- Cada código do Device Manager: knowledge base.
- Cada WHEA event ID: detalhe de classificação.
- Cada SMART attribute: dado interno do Storage Hardware Health Assessment.
- Cada sensor de tensão/fan: Sensor Availability Map / Temperature.
- MemTest, vendor SSD tests, GPU stress tools específicos: implementação/integração futura.
- “PC health score” único: QUESTIONABLE; pode esconder incerteza e diferenças de componentes.

# Auditoria de sobreposição

Hardware Diagnostics não deve duplicar:
- CPU/GPU/RAM/Storage telemetry;
- Temperature/Cooling, que vem na próxima área;
- Benchmark;
- Drivers;
- Repair.

Ele deve **consumir** essas evidências para responder a uma pergunta diferente:
> Há sinal consistente de problema físico, configuração/topologia inadequada ou apenas comportamento normal?

# Status final

**Hardware Diagnostics — INITIAL DISCOVERY COMPLETE**

First Pass: 20  
Second Pass: +7  
Total bruto: **27 capabilities**