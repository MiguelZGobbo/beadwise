# BeadWise / PC Optimizer V2 — USB / Peripherals Discovery

## First Pass — Broad Discovery

Status da área: FIRST PASS COMPLETE

Objetivo desta passada:

Descobrir capabilities realmente úteis relacionadas a USB e periféricos no Windows, com foco em:

- identificação;
- compatibilidade;
- estabilidade;
- desconexões;
- energia;
- drivers;
- input;
- USB-C / USB4;
- diagnóstico;
- reparo seguro.

Esta área adota um filtro mais rígido que as primeiras áreas do discovery.

Princípio adicional da V2:

> Não sacrificar UI/UX, conforto, comportamento esperado ou recursos modernos do Windows por ganhos pequenos, teóricos ou difíceis de perceber.

Outro princípio específico:

> USB/peripherals optimization deve priorizar corrigir problemas reais. Configurações padrão de energia, HID e controladores não devem ser tratadas como gargalos até existir evidência.

Fluxo conceitual:

**Detect → Identify Topology → Understand Capability → Detect Problem → Correlate → Recommend → Repair/Configure Contextually → Validate → Rollback**

---

# Critério de relevância reforçado

Uma capability entra nesta área quando responde a uma pergunta útil como:

- Que dispositivo é esse?
- Em qual porta/hub/controlador ele está conectado?
- Ele está negociando a velocidade/capacidade esperada?
- Existe erro de driver ou enumeração?
- Ele está desconectando?
- O problema acontece após sleep/resume?
- Existe limitação de USB-C/USB4/Alternate Mode?
- O input está funcionando de forma consistente?
- Existe conflito ou limitação de energia?
- O periférico precisa de software/driver específico?
- Uma mudança pode realmente resolver um problema medido?

Não criar capability apenas porque existe:

- Registry value;
- checkbox no Device Manager;
- descriptor USB;
- endpoint;
- polling parameter;
- vendor-specific option;
- power-state flag;
- interrupt setting.

---

# 1. USB Controller Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Hardware

Primary Product Area: My PC

Possible Consumers:

- Hardware Diagnostics
- USB / Peripherals
- Drivers
- Repair

Final UI Placement: TBD

Descrição: Identificar os controladores USB presentes no sistema e suas capacidades principais.

Possível objetivo: Criar a raiz do mapa de conectividade USB.

Possível benefício: Diferenciar limitações do controlador, driver e dispositivo conectado.

Aplicabilidade: All PCs with USB

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- host controller;
- controller type;
- PCI/device identity;
- vendor;
- driver;
- USB generation capability quando identificável;
- xHCI context;
- controller status;
- problem code;
- associated root hubs.

---

# 2. USB Topology Mapping

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Topology

Primary Product Area: Shared

Possible Consumers:

- USB / Peripherals
- Hardware Diagnostics
- Repair
- My PC

Final UI Placement: TBD

Descrição: Mapear a árvore real de controladores, root hubs, hubs intermediários, portas e dispositivos conectados.

Possível objetivo: Entender fisicamente/logicamente onde cada periférico está conectado.

Possível benefício: Diagnosticar hubs saturados, dispositivos atrás do mesmo hub, portas problemáticas e caminhos inesperados.

Aplicabilidade: All PCs with USB

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- controller;
- root hub;
- external/internal hub;
- port number;
- device;
- parent/child relationship;
- connection status;
- device descriptors;
- interface descriptors;
- endpoint summaries;
- current configuration.

Observação:

A Microsoft disponibiliza o USBView como exemplo oficial de enumeração de controladores, hubs e dispositivos.

---

# 3. USB Device Identification

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Devices

Primary Product Area: My PC

Possible Consumers:

- USB / Peripherals
- Drivers
- Repair
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Identificar precisamente os dispositivos USB conectados e suas interfaces.

Possível objetivo: Diferenciar corretamente mouse, teclado, áudio, storage, Bluetooth adapter, webcam, controller, serial device e dispositivos vendor-specific.

Possível benefício: Permitir recomendações específicas sem tratar todos os USB da mesma forma.

Aplicabilidade: All PCs with USB

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- VID;
- PID;
- manufacturer;
- product name;
- serial number quando exposto;
- device class;
- interface class;
- composite device;
- instance ID;
- container ID;
- driver;
- hardware IDs;
- compatible IDs.

---

# 4. USB Connection Speed & Generation Validation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Performance

Primary Product Area: USB / Peripherals

Possible Consumers:

- Storage
- Hardware Diagnostics
- Repair

Final UI Placement: TBD

Descrição: Comparar capacidade do dispositivo com a velocidade efetivamente negociada na conexão.

Possível objetivo: Detectar periférico USB 3.x operando em caminho USB 2.0 ou outra limitação de link.

Possível benefício: Encontrar gargalos reais causados por porta, hub, cabo ou controlador inadequado.

Aplicabilidade: Supported USB Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- device USB capability;
- negotiated connection speed;
- SuperSpeed/SuperSpeedPlus context;
- hub capability;
- controller capability;
- cable/port limitation suspicion;
- fallback to lower generation.

Regra:

Velocidade negociada abaixo da capacidade é diagnóstico; não significa automaticamente defeito.

---

# 5. USB Device Health & Problem Code Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Device Health

Primary Product Area: Repair

Possible Consumers:

- Drivers
- Hardware Diagnostics
- USB / Peripherals

Final UI Placement: TBD

Descrição: Detectar dispositivos USB com estado de erro reportado pelo Plug and Play / Device Manager.

Possível objetivo: Transformar códigos genéricos de Device Manager em diagnóstico compreensível.

Possível benefício: Direcionar usuário para driver, reconnect, restart ou hardware em vez de tweaks aleatórios.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis estados/códigos:

- not configured;
- failed start;
- insufficient resources;
- restart required;
- reinstall required;
- disabled;
- device not present;
- failed install;
- driver/problem status.

---

# 6. USB Enumeration Failure Diagnostics

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: USB Enumeration

Primary Product Area: Repair

Possible Consumers:

- USB / Peripherals
- Drivers
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Detectar dispositivos que falham ao enumerar corretamente no Windows.

Possível objetivo: Diferenciar falha de identificação, descriptor, driver, porta, cabo ou hardware.

Possível benefício: Melhor troubleshooting para "Unknown USB Device" e dispositivos que aparecem/desaparecem.

Aplicabilidade: USB Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis sinais:

- unknown device;
- failed descriptor request;
- missing hardware identity;
- failed start;
- repeated enumeration;
- reconnect loop;
- problem code;
- Event Log correlation.

---

# 7. USB Disconnect / Reconnect Monitoring

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: USB Stability

Primary Product Area: Monitoring

Possible Consumers:

- Repair
- Hardware Diagnostics
- USB / Peripherals

Final UI Placement: TBD

Descrição: Registrar eventos de conexão e desconexão de dispositivos USB ao longo do tempo.

Possível objetivo: Diagnosticar falhas intermitentes que não aparecem em um snapshot.

Possível benefício: Correlacionar perda de mouse, teclado, headset, controller ou storage com porta, hub, energia ou driver.

Aplicabilidade: USB Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis eventos:

- connect;
- disconnect;
- surprise removal;
- reconnect;
- enumeration failure;
- driver restart;
- device instance change;
- timestamp;
- affected controller/hub/port.

---

# 8. USB Port / Hub Fault Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Topology

Primary Product Area: USB / Peripherals

Possible Consumers:

- Hardware Diagnostics
- Repair

Final UI Placement: TBD

Descrição: Correlacionar falhas recorrentes com uma porta, hub ou controlador específico.

Possível objetivo: Distinguir "meu mouse desconecta" de "qualquer dispositivo nessa porta/hub desconecta".

Possível benefício: Identificar problema de porta/hub/controller sem culpar cada periférico individualmente.

Aplicabilidade: USB Devices

Shared Capability: No

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis correlações:

- multiple devices failing on same port;
- failures behind same hub;
- controller reset;
- repeated reconnects;
- speed fallback;
- power events;
- sleep/resume correlation.

---

# 9. USB Selective Suspend Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Power Management

Primary Product Area: Energy

Possible Consumers:

- USB / Peripherals
- Battery
- Repair
- Sleep/Wake

Final UI Placement: TBD

Descrição: Diagnosticar comportamento de suspensão seletiva USB sem tratá-la como tweak de performance.

Possível objetivo: Investigar casos em que determinado dispositivo falha ao suspender ou retomar.

Possível benefício: Corrigir desconexões ou wake/resume problems preservando economia de energia do restante do sistema.

Aplicabilidade: USB Devices

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- selective suspend policy;
- device idle capability;
- device power state;
- driver power-management support;
- suspend/resume events;
- failure correlation;
- device-specific behavior.

Regra:

**Não desabilitar USB Selective Suspend globalmente como otimização.**

A Microsoft recomenda fortemente manter selective suspend habilitado.

---

# 10. Device-Specific USB Power Exception

Status: CANDIDATE

Type: Configuration / Repair

Technical Domain: USB Power Management

Primary Product Area: Repair

Possible Consumers:

- Energy
- USB / Peripherals

Final UI Placement: TBD

Descrição: Permitir exceção de gerenciamento de energia apenas para um dispositivo específico quando houver evidência de que suspensão/retomada causa falha.

Possível objetivo: Resolver problema localizado sem sacrificar power management de toda a árvore USB.

Possível benefício: Menor impacto colateral do que desativar selective suspend globalmente.

Aplicabilidade: Problematic USB Devices

Shared Capability: No

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: User Choice

Preconditions:

- repeated problem observed;
- suspend/resume correlation;
- device-specific scope available;
- original state captured;
- rollback supported.

Não fazer:

- aplicar a todos os hubs;
- aplicar a todos os HID devices;
- aplicar preventivamente.

---

# 11. USB Sleep / Resume Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Power & Stability

Primary Product Area: USB / Peripherals

Possible Consumers:

- Energy
- Sleep/Wake
- Repair
- Drivers

Final UI Placement: TBD

Descrição: Correlacionar falhas USB com sleep, hibernate, Modern Standby ou resume.

Possível objetivo: Detectar dispositivos que funcionam normalmente até uma transição de energia.

Possível benefício: Direcionar troubleshooting para power management/driver em vez de reinstalação indiscriminada.

Aplicabilidade: Sleep-capable PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- pre-sleep device state;
- resume timestamp;
- device missing after resume;
- failed re-enumeration;
- reconnect event;
- driver restart;
- wake capability;
- recovery time.

---

# 12. USB Wake Capability Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: USB Power Management

Primary Product Area: Energy

Possible Consumers:

- USB / Peripherals
- Sleep/Wake

Final UI Placement: TBD

Descrição: Identificar quais dispositivos podem acordar o sistema e quais estão configurados para fazê-lo.

Possível objetivo: Diagnosticar wake inesperado ou permitir wake intencional por mouse/teclado.

Possível benefício: Melhor controle de sleep sem desativar dispositivos ou recursos desnecessariamente.

Aplicabilidade: Supported Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- wake-capable;
- wake-armed;
- last wake source;
- device class;
- sleep-state dependency.

---

# 13. HID Device Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: HID

Primary Product Area: My PC

Possible Consumers:

- Input
- Gaming
- USB / Peripherals
- Accessibility

Final UI Placement: TBD

Descrição: Identificar dispositivos Human Interface Device e suas coleções principais.

Possível objetivo: Criar uma visão coerente de mouse, teclado, controllers e outros HID sem depender apenas de nomes de produto.

Possível benefício: Base para input diagnostics e compatibilidade.

Aplicabilidade: HID Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- HID transport;
- top-level collections;
- usage page;
- usage;
- input reports;
- output reports;
- feature reports;
- device identity;
- driver;
- USB/Bluetooth/I2C transport quando identificável.

---

# 14. Mouse & Keyboard Input Health Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Input

Primary Product Area: USB / Peripherals

Possible Consumers:

- Gaming
- Hardware Diagnostics
- Repair

Final UI Placement: TBD

Descrição: Validar que mouse e teclado estão entregando entrada consistentemente sem perdas ou desconexões observáveis.

Possível objetivo: Diagnosticar sintomas como travamento momentâneo, tecla que não responde ou mouse que reconecta.

Possível benefício: Separar falha física, USB, driver e software.

Aplicabilidade: Mouse / Keyboard

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis sinais:

- input activity;
- disconnect/reconnect;
- HID queue behavior quando acessível;
- missed-report suspicion;
- device problem state;
- transport stability;
- sleep/resume behavior.

---

# 15. HID Report Rate / Polling Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Input Latency

Primary Product Area: USB / Peripherals

Possible Consumers:

- Gaming
- Benchmark
- Input Diagnostics

Final UI Placement: TBD

Descrição: Medir ou estimar a taxa efetiva com que um dispositivo HID entrega relatórios ao sistema quando tecnicamente possível.

Possível objetivo: Detectar configuração muito abaixo do esperado ou instabilidade de reporting.

Possível benefício: Verificar se um mouse de alta taxa está realmente entregando relatórios de forma consistente.

Aplicabilidade: Supported HID Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis dados:

- observed report interval;
- effective report frequency;
- interval distribution;
- stability/jitter;
- transport;
- device advertised capability quando disponível.

Regra:

**Higher polling rate != universally better PC performance.**

Taxas muito altas podem aumentar CPU/interrupt workload. Alteração de polling deve permanecer preferencialmente no software/firmware oficial do periférico.

---

# 16. Input Latency Context Diagnostics

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Input Latency

Primary Product Area: Benchmark

Possible Consumers:

- Gaming
- USB / Peripherals
- Display
- CPU

Final UI Placement: TBD

Descrição: Tratar latência de input como pipeline composto, sem atribuir toda a latência à porta USB ou polling rate.

Possível objetivo: Separar device reporting, software/game processing, frame production e display.

Possível benefício: Evitar "USB latency tweaks" que atacam a camada errada.

Aplicabilidade: Gaming / Input-sensitive Workloads

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis componentes:

- device report interval;
- OS/input processing;
- game/application processing;
- CPU/GPU pipeline;
- frame queue;
- display latency;
- total measured latency quando instrumentação permitir.

---

# 17. Controller / Gamepad Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Game Controllers

Primary Product Area: USB / Peripherals

Possible Consumers:

- Gaming
- Bluetooth
- Repair

Final UI Placement: TBD

Descrição: Identificar e diagnosticar gamepads/controllers conectados por USB ou outros transportes.

Possível objetivo: Verificar conexão, reconhecimento e funcionamento básico sem criar profiles específicos por fabricante nesta fase.

Possível benefício: Diagnosticar controller não detectado, reconectando ou duplicado.

Aplicabilidade: Gaming Controllers

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- device identity;
- transport;
- HID/XInput context quando identificável;
- connection state;
- driver;
- reconnect events;
- battery quando disponível;
- duplicate/virtual controller indication.

---

# 18. Peripheral Driver & Software Dependency Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Peripheral Software

Primary Product Area: Drivers

Possible Consumers:

- USB / Peripherals
- Repair
- Startup
- Processes

Final UI Placement: TBD

Descrição: Identificar quando um periférico funciona com driver genérico do Windows e quando depende de software/driver adicional para funcionalidades específicas.

Possível objetivo: Evitar recomendar instalação de suites OEM quando não são necessárias, mas também detectar recursos ausentes por falta de driver/software.

Possível benefício: Reduzir bloat sem perder funcionalidades reais.

Aplicabilidade: Peripheral Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis categorias:

- Microsoft inbox driver;
- vendor driver;
- optional configuration software;
- firmware updater;
- macro/RGB software;
- audio control software;
- device service;
- startup component.

Regra:

Driver genérico funcionando corretamente não é automaticamente inferior.

---

# 19. Peripheral Firmware Awareness

Status: CANDIDATE

Type: Diagnostic / Manual Guidance

Technical Domain: Peripheral Firmware

Primary Product Area: USB / Peripherals

Possible Consumers:

- Drivers
- Repair
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Identificar firmware version ou disponibilidade de atualização quando o fabricante expõe método confiável.

Possível objetivo: Considerar firmware em problemas de compatibilidade/estabilidade.

Possível benefício: Resolver bugs reais sem tratar firmware update como rotina obrigatória.

Aplicabilidade: Supported Devices

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Manual Guidance

Safeguards:

- prefer official vendor updater;
- verify exact model;
- do not flash generic firmware;
- preserve power during update;
- only recommend when relevant.

---

# 20. USB Hub Capability & Load Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Hubs

Primary Product Area: USB / Peripherals

Possible Consumers:

- Hardware Diagnostics
- Repair
- USB Power

Final UI Placement: TBD

Descrição: Identificar hubs e dispositivos compartilhando o mesmo caminho USB.

Possível objetivo: Investigar limitações de bandwidth, energia ou estabilidade em hubs congestionados.

Possível benefício: Explicar por que periférico funciona diretamente na placa-mãe mas falha em determinado hub/dock.

Aplicabilidade: USB Hubs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- hub identity;
- internal/external;
- upstream controller;
- port count;
- connected devices;
- speed capabilities;
- bus-powered/self-powered quando identificável;
- bandwidth-sensitive devices;
- recurring failures.

---

# 21. USB Bandwidth Contention Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Performance

Primary Product Area: USB / Peripherals

Possible Consumers:

- Audio
- Webcam
- Storage
- Capture Devices

Final UI Placement: TBD

Descrição: Identificar quando múltiplos dispositivos de alta demanda compartilham um caminho USB e podem competir por capacidade disponível.

Possível objetivo: Diagnosticar webcams, capture cards, storage e interfaces de áudio que falham somente quando usados simultaneamente.

Possível benefício: Direcionar rearranjo de portas/hubs em vez de alterar Registry ou drivers sem motivo.

Aplicabilidade: High-bandwidth USB Workloads

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis contextos:

- capture card;
- webcam;
- external SSD;
- audio interface;
- multiple high-speed devices;
- same upstream hub/controller;
- transfer errors/dropouts.

---

# 22. USB Power Delivery / Power Availability Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Power

Primary Product Area: USB / Peripherals

Possible Consumers:

- USB-C
- Laptop
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Diagnosticar quando um dispositivo, dock ou carregador possui capacidade de energia incompatível ou insuficiente.

Possível objetivo: Diferenciar falha de dados de limitação de alimentação.

Possível benefício: Explicar charging slowly, dispositivo instável ou recurso limitado.

Aplicabilidade: Supported USB / USB-C Hardware

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- charging state;
- power role;
- available power quando exposto;
- insufficient charger indication;
- dock power context;
- port capability;
- Windows USB-C notifications.

---

# 23. USB-C Capability Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Type-C

Primary Product Area: USB / Peripherals

Possible Consumers:

- Display
- Charging
- USB4
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Identificar capabilities reais da porta/conexão USB Type-C.

Possível objetivo: Evitar assumir que todo conector USB-C oferece as mesmas funções.

Possível benefício: Explicar ausência de vídeo, charging, USB4 ou velocidade esperada.

Aplicabilidade: USB-C Systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis capacidades:

- USB data;
- charging;
- power role;
- DisplayPort Alternate Mode;
- USB4;
- Thunderbolt interoperability quando aplicável;
- UCSI context;
- cable/port compatibility symptoms.

Regra:

USB-C descreve conector; não garante mesma velocidade, charging ou alternate modes.

---

# 24. USB-C / Alternate Mode Failure Diagnostics

Status: CANDIDATE

Type: Diagnostic / Repair Guidance

Technical Domain: USB Type-C

Primary Product Area: Repair

Possible Consumers:

- Display
- USB4
- Laptop

Final UI Placement: TBD

Descrição: Interpretar notificações e sintomas do Windows relacionados a limitações de USB-C, USB4, DisplayPort Alternate Mode e charging.

Possível objetivo: Transformar mensagens como "functionality might be limited" em causa provável compreensível.

Possível benefício: Evitar troubleshooting errado de GPU/display quando a limitação está na porta/cabo/device mode.

Aplicabilidade: USB-C Systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis cenários:

- USB4 not supported by host;
- alternate mode unsupported;
- cable capability mismatch;
- charging limitation;
- unsupported device combination;
- display connection limitation;
- Billboard device indication.

---

# 25. USB4 Capability & Topology Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB4

Primary Product Area: USB / Peripherals

Possible Consumers:

- Display
- Storage
- Docking
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Identificar sistemas e dispositivos USB4 e contextualizar tunneling/capabilities disponíveis.

Possível objetivo: Diagnosticar docks, displays e storage de alta velocidade sem tratar USB4 como USB tradicional simples.

Possível benefício: Melhor troubleshooting de setups modernos de notebook/dock.

Aplicabilidade: USB4 Systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- USB4 host capability;
- USB4 device/router;
- connection state;
- topology;
- supported tunneling/capabilities quando expostos;
- USB-C compatibility;
- Windows notification state.

---

# 26. External Storage Safe Removal & Surprise Removal Awareness

Status: CANDIDATE

Type: Diagnostic / Safeguard

Technical Domain: USB Storage

Primary Product Area: Shared

Possible Consumers:

- Storage
- USB / Peripherals
- Repair

Final UI Placement: TBD

Descrição: Detectar dispositivos de armazenamento removíveis e tratar desconexões/surprise removal com cautela.

Possível objetivo: Evitar confundir remoção física com falha de storage e reduzir risco de corrupção.

Possível benefício: Melhor diagnóstico de drives externos instáveis.

Aplicabilidade: USB Storage

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- removable storage identity;
- active connection;
- surprise removal;
- reconnect;
- I/O failure correlation;
- safe-removal context;
- filesystem/storage health handoff.

Observação:

Benchmark e health de storage permanecem na área Storage.

---

# 27. USB Audio Peripheral Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Audio

Primary Product Area: Shared

Possible Consumers:

- Audio
- USB / Peripherals
- DPC Diagnostics

Final UI Placement: TBD

Descrição: Identificar interfaces de áudio USB e correlacionar problemas de conexão com o caminho USB.

Possível objetivo: Separar falha de áudio de falha física/USB.

Possível benefício: Diagnosticar dropouts causados por disconnect, hub ou driver sem duplicar toda a área Audio.

Aplicabilidade: USB Audio Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis sinais:

- USB audio device;
- connection path;
- driver;
- reconnect events;
- device problem code;
- hub sharing;
- DPC correlation handoff.

---

# 28. Webcam / Capture Device USB Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Video

Primary Product Area: USB / Peripherals

Possible Consumers:

- Hardware Diagnostics
- Audio/Video
- USB Bandwidth

Final UI Placement: TBD

Descrição: Identificar webcams e capture devices e correlacionar falhas com USB speed, hub e bandwidth context.

Possível objetivo: Diagnosticar vídeo travando, dispositivo sumindo ou modo de alta resolução indisponível.

Possível benefício: Encontrar gargalo físico de conexão antes de alterar software.

Aplicabilidade: USB Video Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

---

# 29. Bluetooth Adapter USB Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB / Bluetooth

Primary Product Area: Shared

Possible Consumers:

- Bluetooth
- USB / Peripherals
- Drivers

Final UI Placement: TBD

Descrição: Reconhecer quando o rádio Bluetooth depende de adaptador USB interno ou externo.

Possível objetivo: Correlacionar falhas Bluetooth com disconnect, USB power ou driver do adaptador físico.

Possível benefício: Evitar diagnosticar Bluetooth isoladamente quando o hardware base está desaparecendo do USB.

Aplicabilidade: USB Bluetooth Adapters

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Observação:

Discovery aprofundado de Bluetooth deverá pertencer a USB/Peripherals ou futura subcapability de peripherals, sem duplicar o domínio USB.

---

# 30. Peripheral Duplicate / Ghost Device Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Device Management

Primary Product Area: USB / Peripherals

Possible Consumers:

- Drivers
- Repair
- Controllers

Final UI Placement: TBD

Descrição: Identificar múltiplas instâncias históricas ou dispositivos não presentes sem tratá-los automaticamente como lixo.

Possível objetivo: Diagnosticar duplicação de controllers, portas COM, interfaces ou device instances.

Possível benefício: Auxiliar troubleshooting sem implementar "ghost device cleaner" agressivo.

Aplicabilidade: Peripheral Devices

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Regra:

Dispositivo não presente != item inútil.

O Windows mantém device instances por razões válidas e remoção automática indiscriminada deve ser evitada.

---

# 31. Peripheral Conflict & Resource Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Plug and Play

Primary Product Area: Repair

Possible Consumers:

- Drivers
- Hardware Diagnostics
- USB / Peripherals

Final UI Placement: TBD

Descrição: Detectar conflitos, falha de inicialização ou recursos insuficientes reportados pelo Plug and Play.

Possível objetivo: Diferenciar erro de driver, hardware e resource allocation.

Possível benefício: Melhor orientação para Code 10/12/31 e problemas semelhantes.

Aplicabilidade: Peripheral Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

---

# 32. USB Driver Regression Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Drivers

Primary Product Area: Drivers

Possible Consumers:

- Repair
- USB / Peripherals
- Windows Update
- History

Final UI Placement: TBD

Descrição: Correlacionar início de falhas de dispositivo com update/rollback de driver ou atualização do Windows.

Possível objetivo: Responder "isso começou depois de qual mudança?".

Possível benefício: Evitar trocar porta/cabo ou alterar power management quando a regressão é de software.

Aplicabilidade: USB / Peripheral Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis mudanças:

- device driver update;
- USB controller driver update;
- chipset driver update;
- Windows update;
- vendor software update;
- firmware update.

Regra:

Correlação temporal != prova de causalidade.

---

# 33. Targeted Peripheral Restart / Re-enumeration

Status: CANDIDATE

Type: Repair

Technical Domain: Device Repair

Primary Product Area: Repair

Possible Consumers:

- USB / Peripherals
- Drivers

Final UI Placement: TBD

Descrição: Reiniciar ou reenumerar um dispositivo específico quando apropriado, evitando resets globais da árvore USB.

Possível objetivo: Recuperar dispositivo travado com mínimo impacto em outros periféricos.

Possível benefício: Alternativa menos agressiva a reboot ou reset geral de controladores.

Aplicabilidade: Recoverable Device Failures

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Safeguards:

- identify exact device;
- warn if storage/audio/input critical;
- avoid critical input loss;
- record state;
- verify return;
- rollback/reboot guidance when required.

---

# 34. Peripheral Repair Escalation

Status: CANDIDATE

Type: Repair / Safeguard

Technical Domain: Device Repair

Primary Product Area: Repair

Possible Consumers:

- USB / Peripherals
- Drivers
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Organizar troubleshooting em ordem crescente de impacto.

Possível objetivo: Evitar reinstalar drivers, controladores ou remover devices como primeira tentativa.

Possível benefício: Resolver problemas com menor risco e menor desconforto ao usuário.

Aplicabilidade: Peripheral Problems

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possível sequência:

1. refresh state;
2. verify cable/port/topology;
3. reconnect device;
4. targeted device restart;
5. alternate port/hub recommendation;
6. driver diagnosis;
7. vendor software/firmware guidance quando relevante;
8. reinstall/rollback guidance;
9. controller/system-level repair apenas quando justificado.

---

# 35. USB / Peripheral Change History

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Device History

Primary Product Area: Shared

Possible Consumers:

- Repair
- Drivers
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Registrar mudanças relevantes no conjunto de periféricos e configuração.

Possível objetivo: Criar contexto temporal para problemas intermitentes.

Possível benefício: Correlacionar falha com novo hub, novo dispositivo, driver ou mudança de porta.

Aplicabilidade: Peripheral Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis eventos:

- device first seen;
- connect/disconnect;
- driver update;
- device moved to different port;
- negotiated speed change;
- firmware change quando disponível;
- problem code appearance;
- repair action.

---

# 36. USB / Peripheral Recommendation Explanation

Status: CANDIDATE

Type: Diagnostic / Quality of Life

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- USB / Peripherals
- Repair
- Optimization
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Explicar por que uma recomendação de USB/periférico foi produzida e por que alternativas mais agressivas não foram escolhidas.

Possível objetivo: Evitar mensagens genéricas como "Optimize USB".

Possível benefício: Tornar troubleshooting compreensível e previsível.

Aplicabilidade: All PCs with Peripherals

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possível explicação:

- symptom;
- affected device;
- topology;
- evidence;
- likely layer;
- proposed action;
- expected effect;
- UX/power trade-off;
- rollback;
- confidence.

---

# 37. USB / Peripheral Change Backup & Rollback

Status: CANDIDATE

Type: Safeguard / Repair

Technical Domain: Device Configuration

Primary Product Area: Shared

Possible Consumers:

- Repair
- USB / Peripherals
- History

Final UI Placement: TBD

Descrição: Registrar alterações feitas pelo BeadWise em configurações de dispositivos e restaurar estado anterior quando possível.

Possível objetivo: Garantir reversibilidade de exceções de energia, wake ou outras configurações.

Possível benefício: Reduzir risco de perda de funcionalidade após tentativa de reparo.

Aplicabilidade: Configurable Peripheral Changes

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis dados:

- device instance;
- setting;
- original value;
- new value;
- reason;
- timestamp;
- validation result;
- rollback result.

---

# Itens filtrados / rejeitados nesta First Pass

A nova regra de relevância eliminou vários itens antes mesmo de entrarem no catálogo.

## Disable USB Selective Suspend globally

Status conceitual: REJECTED

Motivo:

A suspensão seletiva é mecanismo normal de gerenciamento de energia do Windows e a Microsoft recomenda fortemente não desabilitá-la globalmente.

Uma exceção só deve existir para dispositivo problemático e com evidência.

---

## Disable power saving on every USB Root Hub

Status conceitual: REJECTED

Motivo:

É uma abordagem ampla demais, pode aumentar consumo e impedir estados de baixo consumo sem garantir qualquer melhora perceptível.

Diagnosticar dispositivo/hub específico.

---

## Force maximum mouse polling rate

Status conceitual: REJECTED como otimização automática

Motivo:

Polling rate é característica do periférico/firmware/driver e taxas maiores podem aumentar CPU/interrupt workload.

O BeadWise pode medir e informar, mas deve preferir software oficial para alteração.

---

## Registry tweaks for USB latency

Status conceitual: REJECTED / UNVERIFIED

Motivo:

Não criar tweaks baseados em valores de Registry sem documentação, applicability gate e benefício mensurável.

---

## Disable HID power management globally

Status conceitual: REJECTED

Motivo:

Mouses, teclados e outros HID possuem power-management normal. Alteração ampla pode aumentar consumo e criar efeitos colaterais.

---

## Force interrupt affinity / MSI for every USB controller

Status conceitual: REJECTED como regra universal

Motivo:

Pertence a diagnóstico avançado de interrupts/CPU e não há justificativa para configurar controladores USB globalmente sem evidência.

---

## Increase HID input buffers universally

Status conceitual: REJECTED como tweak

Motivo:

O HID class driver já possui buffering e mecanismos próprios. Ajustar buffers só faria sentido em diagnóstico/implementação específica com evidência de perda de reports.

---

## Disable USB 3 power features

Status conceitual: REJECTED como preset

Motivo:

Recursos de energia fazem parte do design do barramento/controlador. Não sacrificar eficiência por ganho teórico.

---

## Remove all ghost devices

Status conceitual: REJECTED

Motivo:

Device instances não presentes podem ser legítimas. Limpeza indiscriminada pode prejudicar drivers, configurações ou dispositivos reconectados futuramente.

---

## Generic "USB Boost"

Status conceitual: REJECTED

Motivo:

Não existe uma única configuração que torne todo USB "mais rápido".

Performance depende de dispositivo, controlador, hub, cabo, protocolo, carga e driver.

---

# Itens propositalmente não duplicados

## USB Storage Benchmark / SMART

Pertence a Storage.

USB apenas fornece topology/speed/connection context.

## Audio Latency / Sample Rate

Pertence a Audio.

USB apenas fornece caminho físico e estabilidade.

## Bluetooth protocol tuning

Não duplicar aqui.

O USB side do adaptador pode ser diagnosticado; comportamento Bluetooth deverá ser tratado como periferia/transport específico.

## GPU/display over USB-C

Display/HDR/refresh permanecem em Display/GPU.

USB-C apenas informa capability/path/alternate mode.

## DPC/ISR diagnostics completos

Pertencem a System/CPU/Audio/Hardware Diagnostics.

USB pode fornecer correlação com dispositivo/controlador.

## Mouse acceleration / Enhance Pointer Precision

Não é USB performance.

É configuração de input/UX e, pela nova filosofia, não deve ser desativada automaticamente.

Pode existir futuramente apenas como preferência explícita do usuário.

---

# Principais grupos futuros

Sem consolidar formalmente ainda:

- USB Hardware & Topology
- Device Identity & Health
- Connection Speed & Capability
- Stability / Disconnects
- Power & Sleep
- HID / Input
- Controllers
- Driver / Firmware / Vendor Software
- Hubs / Bandwidth
- USB-C / USB4 / Power Delivery
- Peripheral-specific Context
- Repair
- History / Explain Why / Rollback

---

# Hipóteses / tópicos para Second Pass — Gap Audit

A segunda passada deverá procurar principalmente lacunas em:

- USB event logging depth;
- USB ETW tracing;
- USB4 router details;
- Thunderbolt / PCIe tunneling awareness;
- docking stations;
- composite device edge cases;
- device interface conflicts;
- serial/COM peripherals;
- printers/scanners;
- webcams;
- biometric devices;
- Bluetooth deeper integration;
- wireless dongles;
- multiple identical devices;
- input duplicate devices;
- controller virtualization;
- input remapping software;
- peripheral software conflicts;
- RGB/peripheral suites;
- accessibility devices;
- device security;
- DMA-capable peripherals;
- USB storage removal/write caching handoff;
- cable capability inference limitations;
- charging/battery peripherals;
- firmware regression correlation;
- benchmark/input measurement methodology;
- safe behavior when mouse/keyboard is the device being restarted;
- recommendation confidence.

---

# Principais conclusões da First Pass

1. **USB optimization deve priorizar problemas reais, não tweaks.**

2. **Selective Suspend deve permanecer habilitado por padrão.**

3. **Exceções de power management devem ser específicas por dispositivo e baseadas em evidência.**

4. **USB topology é capability central**, porque porta, hub e controlador importam para estabilidade e velocidade.

5. **Connection speed validation pode encontrar gargalos reais** sem alterar nenhuma configuração.

6. **HID polling deve ser medido, não maximizado automaticamente.**

7. **Input latency não é sinônimo de USB polling latency.**

8. **USB-C não implica automaticamente USB4, charging ou DisplayPort.**

9. **Hubs podem introduzir bandwidth/power/stability constraints**, mas não são problema por definição.

10. **Peripheral software deve ser tratado como dependência funcional**, não como bloat automaticamente.

11. **Firmware update é intervenção contextual**, não manutenção rotineira.

12. **Ghost devices não devem ser removidos indiscriminadamente.**

13. **Targeted device restart é preferível a reset global quando possível.**

14. **A nova filosofia UX/performance elimina muitos "tweaks" antes mesmo da Feature Spec.**

15. **Nothing to optimize é resultado válido.**

---

# Fontes técnicas de referência desta passada

## Microsoft — USB Selective Suspend

https://learn.microsoft.com/windows-hardware/drivers/usbcon/usb-selective-suspend

## Microsoft — Selective Suspend in USB WDF Drivers

https://learn.microsoft.com/windows-hardware/drivers/usbcon/selective-suspend-in-usb-drivers-wdf

## Microsoft — HID Selective Suspend

https://learn.microsoft.com/windows-hardware/drivers/hid/selective-suspend-for-hid-over-usb-devices

## Microsoft — HID Architecture / Drivers

https://learn.microsoft.com/windows-hardware/drivers/hid/

## Microsoft — USBView

https://learn.microsoft.com/windows-hardware/drivers/debugger/usbview

## Microsoft — USBView Sample

https://learn.microsoft.com/samples/microsoft/windows-driver-samples/usbview-sample-application/

## Microsoft — Device Manager Problem Codes

https://learn.microsoft.com/windows-hardware/drivers/debugger/device-manager-problem-codes

## Microsoft — USB Type-C Troubleshooting Notifications

https://learn.microsoft.com/windows-hardware/drivers/usbcon/usb-type-c-troubleshooting-notifications

## Microsoft — USB Type-C FAQ / UCSI

https://learn.microsoft.com/windows-hardware/drivers/usbcon/faq--usb-type-c-connector-on-a-windows-system

---

# Status final

**USB / Peripherals — First Pass: COMPLETE**

Capabilities brutas desta passada: **37**

Próximo passo da área:

**Second Pass — Consolidated Gap Audit**

# BeadWise / PC Optimizer V2 — USB / Peripherals Discovery

## Second Pass — Consolidated Gap Audit

Status da área: SECOND PASS COMPLETE

Base analisada:

- USB / Peripherals First Pass;
- 01-DISCOVERY;
- inventário funcional V1 apenas como fonte secundária;
- documentação técnica atual da Microsoft para USB, Bluetooth, USB4/Thunderbolt e segurança de DMA.

Objetivo desta passada:

Encontrar perguntas relevantes ainda não respondidas pelas 37 capabilities da First Pass.

Regra desta passada:

> adicionar somente lacunas reais e com valor perceptível de diagnóstico, estabilidade, compatibilidade ou segurança.

Filtro adicional mantido:

> não sacrificar UI/UX, autonomia, recursos modernos do Windows ou segurança por ganhos pequenos, teóricos ou não mensuráveis.

As capabilities abaixo continuam a numeração da First Pass.

---

# 38. USB ETW Trace Diagnostics

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: USB Tracing

Primary Product Area: Tools

Possible Consumers:

- USB / Peripherals
- Repair
- Drivers
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Capturar rastreamento detalhado da pilha USB através de Event Tracing for Windows quando snapshots e eventos normais não forem suficientes.

Possível objetivo: Investigar falhas complexas de enumeração, transferência, controller, hub, endpoint ou power state.

Possível benefício: Obter evidência técnica de baixo nível sem recorrer imediatamente a software de terceiros ou alterações de sistema.

Aplicabilidade: Advanced USB Diagnostics

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis providers/contextos:

- USBXHCI;
- UCX;
- USBHUB3;
- Kernel I/O tracing;
- power-related events;
- state-machine events;
- controller commands/completion;
- endpoint activity.

Safeguards:

- captura curta e direcionada;
- limitar tamanho do arquivo;
- evitar coleta contínua;
- explicar que traces avançados podem exigir interpretação técnica;
- remover/parar trace após conclusão.

Observação:

ETW é uma infraestrutura nativa do Windows e a pilha USB possui providers próprios para diagnóstico de host controller, hubs e endpoints.

---

# 39. Composite USB Device Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Composite Devices

Primary Product Area: USB / Peripherals

Possible Consumers:

- Drivers
- Audio
- Input
- Repair

Final UI Placement: TBD

Descrição: Identificar dispositivos físicos que expõem múltiplas interfaces USB e diagnosticar falhas parciais.

Possível objetivo: Entender casos em que parte do periférico funciona e outra parte não.

Possível benefício: Evitar tratar teclado + áudio + HID + vendor interface do mesmo dispositivo como periféricos independentes sem relação.

Aplicabilidade: Composite USB Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- parent physical device;
- interface collection;
- interface class;
- function driver;
- composite parent;
- failed interface;
- working interfaces;
- shared VID/PID/container identity.

Exemplos de contexto:

- headset com áudio + HID;
- teclado com HID + RGB/vendor interface;
- webcam com vídeo + microfone;
- controller com múltiplas interfaces.

---

# 40. Multiple Identical Peripheral Identity Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Device Identity

Primary Product Area: USB / Peripherals

Possible Consumers:

- Input
- Controllers
- Repair
- Profiles

Final UI Placement: TBD

Descrição: Diferenciar múltiplas unidades do mesmo modelo conectadas simultaneamente.

Possível objetivo: Evitar que configuração, diagnóstico ou restart seja aplicado ao dispositivo físico errado.

Possível benefício: Melhor suporte para simuladores, múltiplos controllers, instrumentos USB e equipamentos idênticos.

Aplicabilidade: Multiple Identical Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis identificadores:

- serial number;
- container ID;
- instance ID;
- topology path;
- controller/hub/port;
- friendly name;
- connection history.

---

# 41. Serial / COM Peripheral Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Serial Devices

Primary Product Area: USB / Peripherals

Possible Consumers:

- Hardware Diagnostics
- Drivers
- Tools
- Repair

Final UI Placement: TBD

Descrição: Identificar dispositivos USB-to-serial e portas COM associadas.

Possível objetivo: Diagnosticar periféricos industriais, microcontroladores, consoles, GPS e outros equipamentos que dependem de serial virtual.

Possível benefício: Detectar driver ausente, COM renumerada ou device instance problemática.

Aplicabilidade: Serial / USB-to-Serial Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- COM port;
- USB device identity;
- bridge chipset/vendor;
- driver;
- connection state;
- assigned port number;
- duplicate/historical instances;
- problem code.

Não fazer:

- alterar baud rate ou protocolo genericamente;
- remover portas COM históricas sem motivo.

---

# 42. Bluetooth Radio & Capability Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Bluetooth

Primary Product Area: My PC

Possible Consumers:

- USB / Peripherals
- Audio
- Input
- Drivers

Final UI Placement: TBD

Descrição: Identificar rádios Bluetooth e capacidades suportadas pelo Windows/driver.

Possível objetivo: Criar baseline real para periféricos Bluetooth sem assumir que todo adaptador oferece os mesmos profiles e recursos.

Possível benefício: Evitar recomendações incompatíveis e diferenciar limitação do rádio de problema do periférico.

Aplicabilidade: PCs with Bluetooth

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- radio identity;
- internal vs USB adapter;
- driver;
- supported Bluetooth generation quando exposta;
- Classic Bluetooth capability;
- Bluetooth LE;
- GATT;
- HID over GATT;
- audio profile support;
- controller state.

---

# 43. Bluetooth Device Inventory & Profile Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Bluetooth Peripherals

Primary Product Area: USB / Peripherals

Possible Consumers:

- Audio
- Input
- Battery
- Repair

Final UI Placement: TBD

Descrição: Identificar dispositivos Bluetooth emparelhados/conectados e os tipos de serviço/profile relevantes.

Possível objetivo: Diferenciar fone, teclado, mouse, controller, sensor e outros dispositivos Bluetooth.

Possível benefício: Permitir diagnóstico específico sem tratar Bluetooth apenas como "connected/disconnected".

Aplicabilidade: Bluetooth Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- device identity;
- paired state;
- connected state;
- Classic/LE context;
- HID over GATT;
- audio profile context;
- GATT services quando apropriado;
- battery when exposed;
- associated radio.

---

# 44. Bluetooth Connection Stability Diagnostics

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Bluetooth Stability

Primary Product Area: USB / Peripherals

Possible Consumers:

- Audio
- Input
- Repair
- Drivers

Final UI Placement: TBD

Descrição: Monitorar desconexões, reconexões e mudanças de estado de periféricos Bluetooth.

Possível objetivo: Diagnosticar falhas intermitentes semelhantes às já tratadas para USB físico.

Possível benefício: Diferenciar problema de bateria, rádio, driver, distância/interferência ou dispositivo.

Aplicabilidade: Bluetooth Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- connection/disconnection timestamps;
- reconnect frequency;
- radio reset;
- device battery;
- USB disconnect do Bluetooth adapter;
- sleep/resume correlation;
- driver event;
- audio/input dropout correlation.

---

# 45. Bluetooth / Wi-Fi Coexistence Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Wireless Coexistence

Primary Product Area: USB / Peripherals

Possible Consumers:

- Network
- Audio
- Input
- Gaming

Final UI Placement: TBD

Descrição: Investigar situações em que Bluetooth e Wi-Fi compartilham espectro ou hardware e apresentam degradação simultânea.

Possível objetivo: Correlacionar stutter de áudio/input Bluetooth com atividade Wi-Fi sem presumir interferência automaticamente.

Possível benefício: Direcionar troubleshooting wireless quando há evidência temporal consistente.

Aplicabilidade: Bluetooth + Wi-Fi Systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- Bluetooth dropout;
- Wi-Fi traffic/load;
- 2.4 GHz usage;
- combo radio;
- shared adapter;
- latency/jitter correlation;
- reconnect events.

Regra:

Coexistência não deve gerar tweak automático de banda/canal sem diagnóstico.

---

# 46. Bluetooth Battery & Low-Power Context

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Bluetooth Power

Primary Product Area: USB / Peripherals

Possible Consumers:

- Battery
- Input
- Audio

Final UI Placement: TBD

Descrição: Exibir bateria e estado de energia de periféricos Bluetooth quando o dispositivo expõe essas informações.

Possível objetivo: Diferenciar falha de conexão de bateria baixa ou comportamento de economia de energia do próprio periférico.

Possível benefício: Diagnóstico simples e perceptível antes de alterar drivers ou configurações.

Aplicabilidade: Supported Bluetooth Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- battery percentage;
- charging state quando disponível;
- low-battery indication;
- last reported battery;
- connection state.

---

# 47. Thunderbolt / PCIe Tunneling Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Thunderbolt / USB4

Primary Product Area: USB / Peripherals

Possible Consumers:

- Security
- GPU
- Storage
- Docking
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Reconhecer quando um dispositivo externo utiliza PCIe tunneling através de Thunderbolt/USB4 em vez de funcionar como dispositivo USB comum.

Possível objetivo: Contextualizar eGPU, docks avançados, storage PCIe e outros periféricos DMA-capable.

Possível benefício: Evitar diagnóstico incorreto e considerar segurança/driver/topologia adequadamente.

Aplicabilidade: Thunderbolt / USB4 Systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- USB4/Thunderbolt path;
- PCIe tunneling capability;
- external PCIe device;
- driver;
- topology;
- hot-plug context;
- DMA protection context.

---

# 48. Kernel DMA Protection & External Peripheral Safeguard

Status: CANDIDATE

Type: Diagnostic / Safeguard

Technical Domain: Peripheral Security

Primary Product Area: Security

Possible Consumers:

- USB / Peripherals
- Thunderbolt / USB4
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Detectar suporte/estado de Kernel DMA Protection em sistemas com periféricos externos DMA-capable.

Possível objetivo: Preservar proteção contra acesso DMA não autorizado em dispositivos Thunderbolt/USB4/PCIe hot-plug.

Possível benefício: Impedir que uma tentativa de "compatibility/performance optimization" reduza proteção importante do Windows.

Aplicabilidade: Supported Systems

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- Kernel DMA Protection state;
- IOMMU support;
- DMA remapping support;
- external DMA-capable device;
- driver compatibility;
- blocked-until-unlock behavior.

Regra:

**Nunca recomendar desativar Kernel DMA Protection por desempenho.**

Observação:

Dispositivos USB convencionais não fazem DMA da mesma forma; o risco aqui se aplica principalmente a periféricos PCIe hot-plug, incluindo Thunderbolt e USB4.

---

# 49. Docking Station Composite Context Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Docking

Primary Product Area: USB / Peripherals

Possible Consumers:

- Display
- Network
- Audio
- USB
- Charging

Final UI Placement: TBD

Descrição: Tratar docks como conjunto integrado de funções em vez de múltiplos dispositivos aparentemente desconectados.

Possível objetivo: Correlacionar falha simultânea de Ethernet, áudio, USB, display ou charging com o dock/upstream link.

Possível benefício: Encontrar causa comum mais rapidamente.

Aplicabilidade: Docking Stations

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis componentes:

- upstream USB-C/USB4/Thunderbolt link;
- USB hub;
- Ethernet;
- audio;
- display path;
- storage/card reader;
- power delivery;
- firmware;
- disconnect event.

---

# 50. Virtual Controller & Input Remapping Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Input Virtualization

Primary Product Area: USB / Peripherals

Possible Consumers:

- Gaming
- Input
- Repair

Final UI Placement: TBD

Descrição: Identificar quando software cria controllers ou dispositivos de input virtuais para remapping, compatibility ou emulação.

Possível objetivo: Explicar duplicate input, double input e device-selection problems.

Possível benefício: Evitar remover ou reinstalar hardware físico quando o conflito vem da camada virtual.

Aplicabilidade: Gaming / Input Remapping Systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis contextos:

- virtual gamepad;
- remapping layer;
- controller emulation;
- physical + virtual duplicate;
- game seeing wrong device;
- virtual HID driver;
- mapping software.

Não fazer:

- remover virtual device automaticamente;
- classificar remapping software como bloat.

---

# 51. Peripheral Software Conflict Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Peripheral Software

Primary Product Area: USB / Peripherals

Possible Consumers:

- Processes
- Startup
- Drivers
- Gaming
- Audio

Final UI Placement: TBD

Descrição: Correlacionar múltiplas suites de periféricos, RGB, macros, monitoring ou vendor services com conflitos observados.

Possível objetivo: Identificar casos em que dois softwares tentam controlar o mesmo dispositivo ou criam hooks/services concorrentes.

Possível benefício: Diagnosticar comportamento instável sem remover software indiscriminadamente.

Aplicabilidade: Peripheral Software Users

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- multiple vendor suites;
- duplicate services;
- device claimed by multiple tools;
- virtual device creation;
- overlay/remapping;
- startup components;
- crash correlation;
- high resource use tied to peripheral software.

Regra:

Software de periférico != bloat automaticamente.

---

# 52. Accessibility Peripheral Safeguards

Status: CANDIDATE

Type: Safeguard / Diagnostic

Technical Domain: Accessibility Devices

Primary Product Area: Shared

Possible Consumers:

- USB / Peripherals
- Input
- Optimization
- Repair

Final UI Placement: TBD

Descrição: Identificar periféricos e software de acessibilidade que não devem ser desativados, reiniciados ou removidos durante otimização genérica.

Possível objetivo: Preservar dispositivos essenciais ao usuário.

Possível benefício: Evitar perda de input, comunicação ou autonomia causada por ações automáticas.

Aplicabilidade: Accessibility Hardware / Software

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Automatic

Possíveis categorias:

- adaptive controllers;
- assistive keyboards;
- alternative pointing devices;
- switch interfaces;
- communication devices;
- accessibility HID;
- required helper software.

Regra:

A presença de configuração ou software incomum nunca deve ser tratada como desperdício sem contexto.

---

# 53. Critical Input Device Repair Safeguard

Status: CANDIDATE

Type: Safeguard

Technical Domain: Device Repair

Primary Product Area: Repair

Possible Consumers:

- USB / Peripherals
- Input

Final UI Placement: TBD

Descrição: Impedir que uma ação automática deixe o usuário sem mouse e teclado funcionais.

Possível objetivo: Tornar restart/re-enumeration/driver repair seguro para dispositivos de entrada críticos.

Possível benefício: Evitar lockout operacional durante troubleshooting.

Aplicabilidade: Mouse / Keyboard / Critical Input

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Automatic

Preconditions possíveis:

- detect alternative input path;
- identify local vs remote session;
- determine whether device is sole keyboard/mouse;
- require confirmation for disruptive operation;
- avoid simultaneous restart of all input devices;
- define timeout/recovery path.

Regra:

Convenience do repair nunca deve superar a capacidade do usuário de continuar controlando o PC.

---

# 54. Peripheral Diagnostic Confidence & Root-Cause Layering

Status: CANDIDATE

Type: Diagnostic / Recommendation Infrastructure

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- USB / Peripherals
- Repair
- Drivers
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Classificar quão forte é a evidência de que o problema está no dispositivo, cabo, porta, hub, controlador, driver, software ou energia.

Possível objetivo: Evitar diagnóstico simplista como "USB problem" quando várias camadas podem produzir o mesmo sintoma.

Possível benefício: Melhorar qualidade das recomendações e reduzir intervenções desnecessárias.

Aplicabilidade: Peripheral Diagnostics

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis camadas:

- device;
- cable;
- connector/port;
- hub/dock;
- host controller;
- transport;
- driver;
- vendor software;
- power management;
- application;
- unknown.

Possíveis confidence levels:

- High;
- Medium;
- Low;
- Inconclusive.

---

# 55. Peripheral Repair Outcome Validation

Status: CANDIDATE

Type: Diagnostic / Repair Validation

Technical Domain: Recommendation Validation

Primary Product Area: Repair

Possible Consumers:

- USB / Peripherals
- History
- Drivers

Final UI Placement: TBD

Descrição: Verificar se o problema que justificou uma intervenção realmente deixou de ocorrer após o reparo.

Possível objetivo: Impedir que "comando executado sem erro" seja considerado sucesso suficiente.

Possível benefício: Detectar reparos neutros, regressões ou necessidade de escalar troubleshooting.

Aplicabilidade: Peripheral Repair Actions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis validações:

- device enumerates;
- driver starts;
- expected speed restored;
- no reconnect loop;
- survives sleep/resume quando relevante;
- input stable;
- no problem code;
- required interfaces available.

Possíveis resultados:

- Resolved;
- Improved;
- Unchanged;
- Regressed;
- Inconclusive;
- Requires Hardware Check.

---

# Lacunas avaliadas mas NÃO transformadas em novas capabilities

## Printers / Scanners

Não justificar uma capability detalhada específica nesta fase.

O BeadWise pode detectar device/driver/problem code através das capabilities genéricas de peripheral diagnostics.

Troubleshooting completo de spooler/impressão pertence mais naturalmente a Repair/System se futuramente entrar no escopo.

---

## Biometric Devices

Não criar tuning próprio.

Dispositivos biométricos devem ser preservados e diagnosticados através de Device Identity / Health / Driver / Security context.

---

## RGB Optimization

Não existe justificativa técnica para "otimizar RGB".

Software RGB pode participar de Process/Startup/Peripheral Software Conflict Diagnostics, mas aparência e iluminação são preferências do usuário.

---

## Disable RGB Software

REJECTED como recomendação automática.

Pode existir consumo mensurável, mas perder iluminação, macros, fan control ou profiles pode degradar UX.

Somente recomendar quando:

- impacto relevante for medido;
- função do software for conhecida;
- usuário puder escolher.

---

## Bluetooth "Latency Tweaks"

REJECTED como categoria genérica.

Bluetooth possui profiles, codec/audio path, HID behavior, rádio e interferência próprios.

Não aplicar Registry packs ou "gaming tweaks" universais.

---

## Disable Bluetooth Power Saving Globally

REJECTED.

Mesma filosofia do USB: diagnosticar device/radio específico.

---

## Force Maximum Polling on Wireless Devices

REJECTED como otimização automática.

Wireless input envolve firmware, rádio, receiver e bateria, não apenas host polling.

---

## Thunderbolt Security Disable

REJECTED.

Kernel DMA Protection e DMA remapping são proteções de segurança e não devem ser sacrificados por performance.

---

## USB Cable Benchmark

Não criar capability isolada.

O Windows geralmente não consegue identificar diretamente todas as propriedades físicas de um cabo.

Connection capability/speed e sintomas podem permitir inferência, mas não certeza universal.

---

## Peripheral Battery Optimization

Não criar tuning genérico.

Battery status é útil; comportamento energético do periférico normalmente pertence ao próprio firmware/device.

---

# Resultado do Gap Audit

Capabilities adicionadas nesta passada: **18**

Numeração adicionada:

**38–55**

Total bruto da área USB / Peripherals após as duas passadas:

- First Pass: 37 capabilities
- Second Pass: 18 capabilities
- Total bruto atual: **55 capabilities**

Isso NÃO significa 55 features finais.

A consolidação futura deverá reduzir bastante esse número.

---

# Principais grupos que começam a emergir

Sem consolidar formalmente ainda, USB / Peripherals parece caminhar para algo próximo de:

1. USB Hardware & Topology
2. Device Identity & Health
3. Connection Capability & Speed
4. Stability / Disconnects
5. Power / Sleep / Wake
6. HID & Input
7. Bluetooth
8. Controllers & Input Virtualization
9. Driver / Firmware / Vendor Software
10. Hubs / Docks / Bandwidth
11. USB-C / USB4 / Thunderbolt
12. Peripheral Security
13. Advanced Tracing
14. Repair & Safeguards
15. History / Explain Why / Validation

Esses grupos são apenas sinalização.

Não consolidar formalmente agora.

---

# Principais conclusões da Second Pass

1. **USB ETW é uma ferramenta real de diagnóstico avançado**, não um tweak.

2. **Dispositivos compostos precisam ser tratados como uma entidade física com múltiplas interfaces.**

3. **Múltiplos periféricos idênticos precisam de identidade baseada em serial/topologia/instance, não apenas nome.**

4. **Bluetooth precisa ser entendido como transport/peripheral ecosystem próprio**, não apenas como adaptador USB.

5. **Bluetooth stability pode depender de rádio, bateria, driver, sleep/resume e coexistência com Wi-Fi.**

6. **Wi-Fi/Bluetooth coexistence deve ser diagnosticada por correlação, não presumida.**

7. **Thunderbolt/USB4 pode transportar PCIe e dispositivos DMA-capable**, portanto segurança faz parte da capability.

8. **Kernel DMA Protection nunca deve ser sacrificada por performance.**

9. **Docks precisam ser tratados como sistemas compostos**, porque uma única falha upstream pode afetar Ethernet, USB, áudio, vídeo e charging ao mesmo tempo.

10. **Virtual controllers/remapping podem explicar double input e conflitos sem qualquer falha do hardware físico.**

11. **Peripheral suites não são bloat automaticamente.** Elas podem fornecer macros, RGB, firmware, fan control e outros recursos desejados.

12. **Accessibility devices exigem safeguards fortes.**

13. **Nunca reiniciar automaticamente o único mouse/teclado disponível sem recovery path.**

14. **Root cause deve ser classificado por camada**, porque os mesmos sintomas podem vir de device, cable, hub, driver ou software.

15. **Repair precisa validar o sintoma original**, e não apenas retorno de comando.

16. **A nova regra de UX eliminou vários candidatos que tecnicamente poderiam ser alterados, mas não justificam interferência na experiência do usuário.**

17. **Nothing to optimize continua sendo resultado válido.**

---

# Fontes técnicas relevantes desta passada

## Microsoft — USB Event Tracing for Windows

https://learn.microsoft.com/windows-hardware/drivers/usbcon/usb-event-tracing-for-windows

## Microsoft — Capture USB ETW Trace with Logman

https://learn.microsoft.com/windows-hardware/drivers/usbcon/how-to-capture-a-usb-event-trace

## Microsoft — Event Tracing for Windows

https://learn.microsoft.com/windows/win32/etw/about-event-tracing

## Microsoft — Bluetooth Support in Windows

https://learn.microsoft.com/windows-hardware/drivers/bluetooth/general-bluetooth-support-in-windows

## Microsoft — Kernel DMA Protection

https://learn.microsoft.com/windows/security/hardware-security/kernel-dma-protection-for-thunderbolt

---

# Status final da área

**USB / Peripherals — INITIAL DISCOVERY COMPLETE**

Passadas concluídas:

- First Pass — Broad Discovery
- Second Pass — Consolidated Gap Audit