# BeadWise / PC Optimizer V2 — Audio Discovery

## First Pass — Broad Discovery

Status da área: FIRST PASS COMPLETE

Objetivo desta passada:

Descobrir capabilities realmente úteis relacionadas a áudio no Windows, priorizando:

- identificação correta de dispositivos;
- roteamento;
- qualidade;
- estabilidade;
- compatibilidade;
- drivers;
- formatos;
- enhancements;
- spatial audio;
- Bluetooth/USB/HDMI audio;
- latência;
- DPC/dropouts;
- captura/microfone;
- reparo seguro.

Esta área adota explicitamente o filtro de UX definido para a V2:

> Não degradar qualidade, espacialidade, conveniência, recursos modernos ou comportamento esperado do Windows em troca de ganhos pequenos, teóricos ou difíceis de perceber.

Outro princípio:

> Audio optimization deve corrigir problemas reais ou configurar corretamente o hardware — não transformar preferências sonoras em tweaks universais.

Fluxo conceitual:

**Detect Device → Identify Route → Validate Format/Capability → Detect Problem → Correlate → Recommend → Configure/Repair Contextually → Validate → Rollback**

---

# Critério de relevância reforçado

Uma capability entra nesta área quando responde a uma pergunta útil como:

- Qual dispositivo está realmente reproduzindo/capturando áudio?
- O dispositivo padrão está correto?
- Existe driver ou endpoint com problema?
- O formato atual é compatível e apropriado?
- Há distorção, estalo, dropout ou silêncio?
- Enhancements estão interferindo?
- Spatial Audio é suportado/está configurado?
- O problema é do USB, Bluetooth, HDMI, driver, DPC ou aplicação?
- Uma aplicação está roteada para o dispositivo errado?
- Exclusive mode está causando conflito?
- O microfone está bloqueado por permissão?
- Existe uma mudança com benefício real e reversível?

Não criar capability apenas porque existe:

- sample rate maior;
- bit depth maior;
- enhancement;
- APO;
- codec;
- vendor control;
- buffer size;
- exclusive-mode flag;
- Registry value;
- “latency tweak”.

---

# 1. Audio Endpoint Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Devices

Primary Product Area: My PC

Possible Consumers:

- Audio
- Repair
- Drivers
- Monitoring

Final UI Placement: TBD

Descrição: Identificar endpoints de reprodução e captura conhecidos pelo Windows.

Possível objetivo: Criar a base para todas as capabilities condicionais da área de áudio.

Possível benefício: Evitar diagnóstico ou alteração no dispositivo errado.

Aplicabilidade: All PCs with Audio

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- playback endpoints;
- capture endpoints;
- friendly name;
- device state;
- default role;
- form factor;
- associated hardware;
- interface/device ID;
- active/disabled/unplugged state.

---

# 2. Physical Audio Device & Endpoint Relationship

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Topology

Primary Product Area: Shared

Possible Consumers:

- Audio
- Drivers
- USB / Peripherals
- Display

Final UI Placement: TBD

Descrição: Relacionar endpoints de software do Windows ao hardware físico que os fornece.

Possível objetivo: Diferenciar, por exemplo, uma GPU HDMI Audio, codec onboard, headset USB e rádio Bluetooth.

Possível benefício: Melhorar root-cause diagnostics.

Aplicabilidade: All PCs with Audio

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis contextos:

- onboard codec;
- USB audio;
- HDMI/DisplayPort audio;
- Bluetooth audio;
- webcam microphone;
- dock audio;
- virtual audio device;
- external interface.

---

# 3. Default Playback Device Validation

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Audio Routing

Primary Product Area: Audio

Possible Consumers:

- Repair
- UX
- Gaming
- Media

Final UI Placement: TBD

Descrição: Identificar qual dispositivo de saída é padrão e detectar situações em que o áudio está sendo enviado para um endpoint inesperado.

Possível objetivo: Resolver casos de “sem som” causados apenas por roteamento incorreto.

Possível benefício: Correção simples, perceptível e de baixo risco.

Aplicabilidade: All PCs with Multiple Audio Outputs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis situações:

- monitor sem alto-falantes selecionado;
- HDMI/DP audio selecionado;
- Bluetooth headset não selecionado;
- USB interface conectada;
- endpoint antigo mantido como default.

---

# 4. Default Capture Device Validation

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Audio Routing

Primary Product Area: Audio

Possible Consumers:

- Communication
- Repair
- Privacy

Final UI Placement: TBD

Descrição: Validar dispositivo padrão de entrada/microfone.

Possível objetivo: Diagnosticar apps capturando do microfone errado.

Possível benefício: Melhor experiência em calls, streaming e gravação.

Aplicabilidade: PCs with Multiple Capture Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

---

# 5. Per-App Audio Routing Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Audio Routing

Primary Product Area: Audio

Possible Consumers:

- Gaming
- Communication
- Media
- UX

Final UI Placement: TBD

Descrição: Identificar aplicativos roteados para dispositivos de entrada/saída específicos.

Possível objetivo: Detectar quando uma aplicação ignora o endpoint padrão por possuir roteamento persistido.

Possível benefício: Resolver “só este app não tem som” ou captura no dispositivo errado.

Aplicabilidade: Multi-Endpoint PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- application;
- output endpoint;
- input endpoint;
- volume;
- mute state;
- session state;
- persisted routing override.

---

# 6. Audio Session & Volume Mixer Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Sessions

Primary Product Area: Audio

Possible Consumers:

- Repair
- UX
- Monitoring

Final UI Placement: TBD

Descrição: Inspecionar sessões de áudio e níveis individuais de aplicações.

Possível objetivo: Diferenciar ausência de áudio global de app mutado, volume baixo ou sessão problemática.

Possível benefício: Resolver problemas simples sem reinstalar driver ou alterar formatos.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- active audio sessions;
- process/application;
- volume;
- mute;
- peak meter;
- endpoint;
- session state.

---

# 7. Audio Device Health & Problem Code Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Device Health

Primary Product Area: Repair

Possible Consumers:

- Drivers
- Hardware Diagnostics
- Audio

Final UI Placement: TBD

Descrição: Detectar dispositivos de áudio com erros de Plug and Play, driver ou inicialização.

Possível objetivo: Separar falha de configuração de falha de dispositivo/driver.

Possível benefício: Direcionar troubleshooting para a camada correta.

Aplicabilidade: All PCs with Audio

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis sinais:

- device problem code;
- failed start;
- disabled device;
- missing driver;
- failed installation;
- restart required;
- endpoint unavailable.

---

# 8. Audio Driver Inventory & Provider Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Drivers

Primary Product Area: Drivers

Possible Consumers:

- Audio
- Repair
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Identificar drivers envolvidos no caminho de áudio e seus providers/versões.

Possível objetivo: Diferenciar Microsoft inbox driver, vendor driver, GPU audio driver e componentes de terceiros.

Possível benefício: Melhor diagnóstico de regressões e recursos ausentes.

Aplicabilidade: All PCs with Audio

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- device;
- provider;
- driver version;
- date;
- associated endpoint;
- recent update;
- rollback availability;
- generic/vendor context.

---

# 9. Audio Driver Regression Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Stability

Primary Product Area: Audio

Possible Consumers:

- Drivers
- Windows Update
- Repair
- History

Final UI Placement: TBD

Descrição: Correlacionar início de falhas de áudio com updates de driver, Windows, GPU ou software relacionado.

Possível objetivo: Responder “o que mudou desde quando o áudio funcionava?”.

Possível benefício: Evitar tweaks aleatórios quando existe regressão temporal clara.

Aplicabilidade: Audio Problems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis mudanças:

- audio driver update;
- GPU driver update;
- Windows update;
- chipset/USB driver update;
- vendor audio suite update;
- Bluetooth driver update;
- firmware change.

Regra:

Correlação temporal != causalidade comprovada.

---

# 10. Audio Format Capability Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Format

Primary Product Area: Audio

Possible Consumers:

- Quality
- Repair
- Pro Audio

Final UI Placement: TBD

Descrição: Identificar formatos suportados por endpoint e configuração padrão atual.

Possível objetivo: Saber quais combinações de sample rate/bit depth são realmente aceitas.

Possível benefício: Evitar selecionar formato não suportado ou tratar número maior como automaticamente melhor.

Aplicabilidade: All Audio Endpoints

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- default sample rate;
- default bit depth;
- supported formats quando expostos;
- channel count;
- shared-mode mix format;
- endpoint capability.

---

# 11. Default Audio Format Validation

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Audio Format

Primary Product Area: Audio

Possible Consumers:

- Repair
- Quality
- Media

Final UI Placement: TBD

Descrição: Validar se o formato padrão está funcionando corretamente com o dispositivo e workload.

Possível objetivo: Detectar formatos incompatíveis ou responsáveis por distorção/ausência de áudio.

Possível benefício: Correção real de crackling, silêncio ou baixa qualidade em casos específicos.

Aplicabilidade: All Audio Endpoints

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: User Choice

Possíveis ações:

- test current format;
- compare supported alternatives;
- change format temporarily;
- verify output;
- restore original.

Regra:

Não forçar 44.1 kHz, 48 kHz, 96 kHz, 192 kHz ou qualquer bit depth como universalmente superior.

---

# 12. Shared Mode Audio Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: WASAPI

Primary Product Area: Audio

Possible Consumers:

- Applications
- Gaming
- Media
- Communication

Final UI Placement: TBD

Descrição: Diagnosticar comportamento normal de áudio em WASAPI shared mode, onde o Windows Audio Engine mistura múltiplos streams.

Possível objetivo: Entender resampling, mix format e convivência entre aplicações.

Possível benefício: Evitar recomendar exclusive mode quando shared mode atende melhor ao uso cotidiano.

Aplicabilidade: All Windows Audio

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- endpoint mix format;
- active sessions;
- resampling context;
- audio engine path;
- APO processing context;
- multiple simultaneous clients.

Observação:

Shared mode é o comportamento normal para coexistência de aplicativos no Windows.

---

# 13. Exclusive Mode Capability & Conflict Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: WASAPI

Primary Product Area: Audio

Possible Consumers:

- Pro Audio
- Media
- Repair

Final UI Placement: TBD

Descrição: Detectar disponibilidade de exclusive mode e investigar conflitos quando uma aplicação assume acesso exclusivo ao endpoint.

Possível objetivo: Diferenciar baixa latência/pro-audio legítima de situação onde outros apps perdem áudio.

Possível benefício: Melhor troubleshooting sem tratar exclusive mode como “mais performance” universal.

Aplicabilidade: Supported Audio Endpoints

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis situações:

- app opens exclusive stream;
- other apps cannot play;
- format negotiation fails;
- pro-audio workload;
- RTC/special application use;
- exclusive mode allowed/disallowed.

Regra:

**Exclusive mode não deve ser ativado como otimização universal.**

---

# 14. Audio Enhancements Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Processing

Primary Product Area: Audio

Possible Consumers:

- Quality
- Repair
- Vendor Software

Final UI Placement: TBD

Descrição: Identificar enhancements/APOs ativos no endpoint.

Possível objetivo: Saber quais processamentos estão alterando o áudio antes de diagnosticar qualidade.

Possível benefício: Evitar desativação indiscriminada de recursos sonoros desejados.

Aplicabilidade: Supported Audio Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis categorias:

- loudness processing;
- room correction;
- bass enhancement;
- virtual surround;
- voice processing;
- OEM/vendor effects;
- system effects;
- APO chain context.

---

# 15. Audio Enhancement Troubleshooting

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Audio Processing

Primary Product Area: Repair

Possible Consumers:

- Audio
- Quality

Final UI Placement: TBD

Descrição: Testar temporariamente áudio com enhancements desativados quando há distorção, crackling ou comportamento anormal.

Possível objetivo: Identificar processamento como possível causa.

Possível benefício: Resolver falhas reais sem declarar todo enhancement como ruim.

Aplicabilidade: Audio Problems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Benchmark First

Possíveis etapas:

- capture current state;
- disable effects temporarily;
- replay/test;
- compare;
- restore if no benefit;
- keep only with user approval if problem resolves.

Regra:

**Não desativar todos os enhancements preventivamente.**

---

# 16. Audio Processing Object / APO Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Processing

Primary Product Area: Shared

Possible Consumers:

- Drivers
- Repair
- Vendor Software
- Audio

Final UI Placement: TBD

Descrição: Reconhecer Audio Processing Objects e componentes de processamento integrados ao pipeline de áudio.

Possível objetivo: Explicar onde enhancements, spatial processing, OEM effects e outras transformações podem ocorrer.

Possível benefício: Melhor root-cause diagnostics de distorção ou incompatibilidade.

Aplicabilidade: Audio Endpoints

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

---

# 17. Spatial Audio Capability Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Spatial Audio

Primary Product Area: Audio

Possible Consumers:

- Gaming
- Media
- Accessibility

Final UI Placement: TBD

Descrição: Detectar suporte e estado de Spatial Sound no endpoint.

Possível objetivo: Diferenciar estéreo convencional de spatial rendering suportado pelo Windows/dispositivo.

Possível benefício: Preservar recurso de imersão quando desejado e diagnosticar configuração incorreta.

Aplicabilidade: Supported Audio Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis contextos:

- Windows Sonic;
- vendor spatial solution;
- headphone virtualization;
- home theater/spatial endpoint;
- current state;
- device compatibility.

Regra:

Spatial Audio não deve ser desligado por performance sem evidência de problema.

---

# 18. Channel Layout & Speaker Configuration Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Audio Channels

Primary Product Area: Audio

Possible Consumers:

- Media
- Gaming
- Home Theater

Final UI Placement: TBD

Descrição: Validar configuração de canais em relação ao dispositivo físico.

Possível objetivo: Detectar sistema surround tratado como estéreo ou configuração multichannel incorreta.

Possível benefício: Recuperar canais ausentes e posicionamento adequado.

Aplicabilidade: Multi-Channel Audio Systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- mono/stereo;
- 5.1;
- 7.1;
- speaker mapping;
- endpoint channel capability;
- test tones;
- center/subwoofer/surround presence.

---

# 19. HDMI / DisplayPort Audio Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Audio

Primary Product Area: Shared

Possible Consumers:

- Display
- GPU
- Audio
- Repair

Final UI Placement: TBD

Descrição: Relacionar endpoints HDMI/DisplayPort ao monitor/TV e GPU correspondentes.

Possível objetivo: Diagnosticar saída selecionada incorretamente ou desaparecimento de áudio após mudança de display.

Possível benefício: Resolver problemas comuns em setups com monitores, TVs e GPUs.

Aplicabilidade: HDMI / DisplayPort Audio

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis sinais:

- associated display;
- GPU audio driver;
- display connected/disconnected;
- default endpoint change;
- supported channel/format context;
- monitor without speakers selected.

---

# 20. USB Audio Path Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Audio

Primary Product Area: Shared

Possible Consumers:

- USB / Peripherals
- Audio
- DPC Diagnostics
- Repair

Final UI Placement: TBD

Descrição: Correlacionar comportamento do endpoint de áudio com topologia e estabilidade USB.

Possível objetivo: Separar audio-engine problem de disconnect/hub/controller USB.

Possível benefício: Diagnosticar interfaces, DACs, headsets e microfones USB.

Aplicabilidade: USB Audio Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis correlações:

- USB disconnect/reconnect;
- hub sharing;
- negotiated speed;
- sleep/resume;
- driver reset;
- audio dropout;
- DPC spikes.

---

# 21. Bluetooth Audio Capability Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Bluetooth Audio

Primary Product Area: Audio

Possible Consumers:

- USB / Peripherals
- Bluetooth
- Battery
- Repair

Final UI Placement: TBD

Descrição: Identificar perfil/capability de áudio Bluetooth e comportamento do endpoint conectado.

Possível objetivo: Diferenciar limitações do transporte Bluetooth de problemas do Windows Audio Engine.

Possível benefício: Melhor troubleshooting de headsets e speakers wireless.

Aplicabilidade: Bluetooth Audio Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- device;
- connected state;
- profile/context;
- output endpoint;
- microphone endpoint;
- battery when available;
- radio;
- reconnect events;
- audio quality mode when exposed.

---

# 22. Bluetooth Audio Quality / Communication Mode Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Bluetooth Audio

Primary Product Area: Audio

Possible Consumers:

- Communication
- Gaming
- Repair

Final UI Placement: TBD

Descrição: Detectar mudanças de qualidade associadas ao uso simultâneo de reprodução e microfone em headsets Bluetooth.

Possível objetivo: Explicar por que qualidade sonora pode mudar ao entrar em call ou ativar microfone.

Possível benefício: Evitar tratar comportamento do profile/transporte como defeito.

Aplicabilidade: Bluetooth Headsets

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

---

# 23. Microphone Permission & Privacy Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Audio Privacy

Primary Product Area: Privacy

Possible Consumers:

- Audio
- Repair
- Communication

Final UI Placement: TBD

Descrição: Verificar permissões do Windows para acesso ao microfone e apps específicos.

Possível objetivo: Detectar microfone funcional bloqueado por política/permissão.

Possível benefício: Resolver “microfone não funciona neste app” sem reinstalar driver.

Aplicabilidade: PCs with Microphone

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis verificações:

- microphone access;
- app permission;
- desktop app permission;
- policy-managed state;
- device enabled;
- default input.

---

# 24. Microphone Input Level Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Audio Capture

Primary Product Area: Audio

Possible Consumers:

- Communication
- Recording
- Gaming

Final UI Placement: TBD

Descrição: Avaliar nível de entrada, mute e sinal capturado pelo microfone.

Possível objetivo: Diferenciar hardware silencioso de gain/volume inadequado.

Possível benefício: Melhorar calls e gravações sem aplicar processamento desnecessário.

Aplicabilidade: Capture Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- input volume;
- mute;
- signal level;
- endpoint;
- clipping suspicion;
- too-low signal;
- application routing.

---

# 25. Microphone Processing / Enhancement Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Capture Processing

Primary Product Area: Audio

Possible Consumers:

- Communication
- Recording
- Repair

Final UI Placement: TBD

Descrição: Identificar processamento aplicado à captura de voz.

Possível objetivo: Diagnosticar voz abafada, pumping, clipping ou artefatos causados por processing.

Possível benefício: Melhor qualidade de comunicação quando existe problema mensurável/perceptível.

Aplicabilidade: Capture Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis processamentos:

- noise suppression;
- echo cancellation;
- automatic gain;
- beamforming;
- vendor effects;
- communication processing.

Regra:

Não desativar noise suppression/echo cancellation preventivamente se eles melhoram a experiência do usuário.

---

# 26. Audio Dropout / Glitch Monitoring

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Audio Stability

Primary Product Area: Monitoring

Possible Consumers:

- Repair
- DPC Diagnostics
- USB / Peripherals
- Drivers

Final UI Placement: TBD

Descrição: Registrar eventos compatíveis com dropout, crackling, glitch ou interrupção de áudio.

Possível objetivo: Criar evidência temporal para problemas intermitentes.

Possível benefício: Correlacionar falhas com DPC, driver, USB, Bluetooth, CPU load ou device transition.

Aplicabilidade: Audio Problems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Automatic

Possíveis sinais:

- stream underrun/glitch evidence quando disponível;
- device disconnect;
- audio service event;
- session interruption;
- driver reset;
- buffer starvation suspicion;
- timestamp;
- affected endpoint.

---

# 27. DPC / ISR Audio Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Latency

Primary Product Area: Audio

Possible Consumers:

- Hardware Diagnostics
- USB
- Network
- Drivers
- Gaming

Final UI Placement: TBD

Descrição: Correlacionar crackling/dropouts com DPC/ISR e driver latency sem transformar qualquer DPC alto em causa automática.

Possível objetivo: Identificar driver ou subsistema que impede entrega temporal consistente de áudio.

Possível benefício: Direcionar troubleshooting para rede, USB, storage ou driver correto.

Aplicabilidade: Audio Stability Problems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- DPC latency;
- ISR latency;
- driver attribution;
- audio dropout timestamps;
- CPU load;
- interrupt bursts;
- device activity.

Regra:

Correlação temporal forte aumenta confiança, mas DPC isolado não prova causalidade.

---

# 28. Audio Engine / Service Health Diagnostics

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Windows Audio Services

Primary Product Area: Repair

Possible Consumers:

- Audio
- System

Final UI Placement: TBD

Descrição: Verificar estado dos componentes/serviços centrais responsáveis por endpoints e reprodução/captura.

Possível objetivo: Diagnosticar ausência global de áudio por serviço parado ou estado inconsistente.

Possível benefício: Reparar problema de baixo impacto antes de reinstalar drivers.

Aplicabilidade: Windows Audio Problems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis componentes:

- Windows Audio;
- Windows Audio Endpoint Builder;
- endpoint enumeration;
- service state;
- startup configuration;
- dependency state.

---

# 29. Targeted Audio Service Restart

Status: CANDIDATE

Type: Repair

Technical Domain: Windows Audio Services

Primary Product Area: Repair

Possible Consumers:

- Audio

Final UI Placement: TBD

Descrição: Reiniciar serviços de áudio quando diagnóstico indicar falha temporária de engine/endpoint.

Possível objetivo: Recuperar áudio sem reboot completo.

Possível benefício: Reparação rápida e de baixo impacto.

Aplicabilidade: Recoverable Audio Service Problems

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Safeguards:

- warn that active audio sessions will stop;
- capture service state;
- restart only relevant services;
- verify endpoint recovery.

---

# 30. Audio Endpoint Re-enumeration / Device Restart

Status: CANDIDATE

Type: Repair

Technical Domain: Audio Device Repair

Primary Product Area: Repair

Possible Consumers:

- Drivers
- USB
- Bluetooth

Final UI Placement: TBD

Descrição: Reiniciar/re-enumerar dispositivo de áudio específico quando apropriado.

Possível objetivo: Recuperar endpoint travado sem resetar todos os dispositivos de som.

Possível benefício: Intervenção mais precisa e menos disruptiva.

Aplicabilidade: Recoverable Endpoint Failures

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Safeguards:

- exact endpoint/device identity;
- warn about call/recording interruption;
- preserve default routing;
- verify endpoint returns;
- avoid global reset when unnecessary.

---

# 31. Audio Repair Escalation

Status: CANDIDATE

Type: Repair / Safeguard

Technical Domain: Audio Repair

Primary Product Area: Repair

Possible Consumers:

- Audio
- Drivers

Final UI Placement: TBD

Descrição: Organizar troubleshooting em ordem crescente de impacto.

Possível objetivo: Evitar reinstalar driver como primeira ação para qualquer problema de áudio.

Possível benefício: Resolver problemas com menor risco e menor desconforto ao usuário.

Aplicabilidade: Audio Problems

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possível sequência:

1. verify endpoint/routing/volume;
2. verify permissions;
3. test current format;
4. test enhancements only if symptom suggests;
5. restart audio services;
6. targeted endpoint restart;
7. driver diagnosis/rollback;
8. reinstall driver only when justified;
9. hardware/transport investigation.

---

# 32. Communication Activity / Ducking Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Communications Audio

Primary Product Area: Audio

Possible Consumers:

- Communication
- Gaming
- UX

Final UI Placement: TBD

Descrição: Detectar políticas do Windows/aplicações que reduzem volume de outros sons durante chamadas.

Possível objetivo: Explicar “o jogo/música fica baixo quando entro em call”.

Possível benefício: Evitar diagnosticar volume reduction deliberado como falha.

Aplicabilidade: Communication Workloads

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

---

# 33. Virtual Audio Device Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Virtual Audio

Primary Product Area: Audio

Possible Consumers:

- Streaming
- Recording
- Gaming
- Repair

Final UI Placement: TBD

Descrição: Identificar endpoints criados por software virtual de routing, mixing ou capture.

Possível objetivo: Evitar confundir dispositivo virtual com hardware físico ou remover fluxo configurado pelo usuário.

Possível benefício: Melhor troubleshooting em setups de streaming, DAWs e comunicação.

Aplicabilidade: Virtual Audio Users

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis contextos:

- virtual cable;
- virtual mixer;
- loopback endpoint;
- streaming software;
- broadcast mixer;
- application-specific virtual device.

Regra:

Virtual audio device != bloat.

---

# 34. Audio Loopback / Capture Path Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Capture

Primary Product Area: Shared

Possible Consumers:

- Recording
- Streaming
- Monitoring

Final UI Placement: TBD

Descrição: Reconhecer captura de loopback/system audio e distinguir de captura física por microfone.

Possível objetivo: Contextualizar gravação/streaming e evitar troubleshooting do input errado.

Possível benefício: Melhor suporte a workflows de criação de conteúdo.

Aplicabilidade: Recording / Streaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

---

# 35. Audio Resource Usage Diagnostics

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Audio Processing

Primary Product Area: Monitoring

Possible Consumers:

- Processes
- Audio
- Performance

Final UI Placement: TBD

Descrição: Identificar uso relevante de recursos associado à engine de áudio, enhancements ou suites de terceiros quando houver suspeita de impacto.

Possível objetivo: Medir antes de recomendar remover processamento.

Possível benefício: Filtrar casos em que software de áudio realmente causa uso anormal.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- audiodg CPU usage;
- vendor service CPU/RAM;
- APO correlation;
- active audio sessions;
- processing load;
- dropouts under load.

Regra:

Uso de recursos pequeno não justifica perder enhancement/UX.

---

# 36. Audio Quality Comparison Tool

Status: CANDIDATE

Type: Tool / Benchmark

Technical Domain: Audio Quality

Primary Product Area: Tools

Possible Consumers:

- Enhancements
- Format
- Spatial Audio

Final UI Placement: TBD

Descrição: Permitir comparação A/B controlada entre duas configurações quando o benefício é subjetivo.

Possível objetivo: Tratar qualidade sonora como experiência do usuário, não como score sintético simples.

Possível benefício: Escolher formato/enhancement/spatial configuration com base em preferência real.

Aplicabilidade: Optional

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: User Choice

Possíveis comparações:

- enhancement ON/OFF;
- spatial ON/OFF;
- format A/B quando apropriado;
- vendor processing preset.

Regra:

Não declarar configuração subjetivamente “melhor” apenas por especificação maior.

---

# 37. Audio Recommendation Explanation

Status: CANDIDATE

Type: Diagnostic / Quality of Life

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- Audio
- Optimization
- Repair
- Drivers

Final UI Placement: TBD

Descrição: Explicar por que determinada recomendação de áudio existe e qual efeito é esperado.

Possível objetivo: Impedir recomendações opacas como “Optimize Sound”.

Possível benefício: Preservar qualidade, recursos desejados e escolhas do usuário.

Aplicabilidade: All PCs with Audio

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possível explicação:

- affected endpoint;
- symptom;
- evidence;
- route/format/driver/processing context;
- proposed action;
- expected improvement;
- quality trade-off;
- latency trade-off;
- rollback;
- confidence.

---

# 38. Audio Configuration Backup & Rollback

Status: CANDIDATE

Type: Safeguard / Repair

Technical Domain: Audio Configuration

Primary Product Area: Shared

Possible Consumers:

- Audio
- Repair
- History

Final UI Placement: TBD

Descrição: Registrar alterações realizadas pelo BeadWise e restaurar estado anterior quando possível.

Possível objetivo: Tornar testes de formato, enhancement, routing e other settings reversíveis.

Possível benefício: Evitar perda de configuração personalizada.

Aplicabilidade: Audio Configuration Changes

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis dados:

- endpoint;
- original setting;
- new setting;
- routing;
- default role;
- format;
- enhancement state;
- spatial state;
- timestamp;
- validation result;
- rollback result.

---

# 39. Audio Change Outcome Validation

Status: CANDIDATE

Type: Diagnostic / Validation

Technical Domain: Recommendation Validation

Primary Product Area: Audio

Possible Consumers:

- Repair
- Optimization
- History

Final UI Placement: TBD

Descrição: Verificar se a mudança realmente resolveu o sintoma ou melhorou a experiência.

Possível objetivo: Impedir que “configuração alterada com sucesso” seja tratada como melhoria comprovada.

Possível benefício: Reverter mudanças neutras ou prejudiciais.

Aplicabilidade: Audio Changes

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis resultados:

- Resolved;
- Improved;
- No Benefit;
- Quality Regression;
- Stability Regression;
- Inconclusive;
- User Preferred Original;
- Rolled Back.

---

# Itens filtrados / rejeitados nesta First Pass

## Disable all audio enhancements

Status conceitual: REJECTED como otimização padrão

Motivo:

Enhancements podem fornecer funções úteis e desejadas. A Microsoft recomenda desativá-los como troubleshooting quando há distorção/crackling, não como limpeza preventiva.

---

## Force 44.1 kHz globally

Status conceitual: REJECTED

Motivo:

Não existe um sample rate universalmente ideal para todos os dispositivos e workloads.

---

## Force 48 kHz globally

Status conceitual: REJECTED

Motivo:

48 kHz é comum em vídeo/jogos/comunicações, mas não justifica sobrescrever todo endpoint indiscriminadamente.

---

## Force 96/192 kHz for “better sound”

Status conceitual: REJECTED como optimization

Motivo:

Sample rate maior não garante melhora audível e pode aumentar processamento/bandwidth ou causar incompatibilidade.

---

## Force 24/32-bit everywhere

Status conceitual: REJECTED como regra universal

Motivo:

Bit depth precisa ser suportado e apropriado ao pipeline. Número maior por si só não prova benefício perceptível.

---

## Enable Exclusive Mode for lower latency

Status conceitual: REJECTED como regra universal

Motivo:

Exclusive mode impede compartilhamento normal do endpoint e é mais apropriado para workloads específicos como pro-audio/RTC.

---

## Disable Exclusive Mode globally

Status conceitual: REJECTED

Motivo:

Pode quebrar workflows profissionais legítimos. Diagnosticar conflito específico.

---

## Disable Spatial Audio for performance

Status conceitual: REJECTED

Motivo:

É recurso de qualidade/imersão. Só deve ser alterado por preferência ou troubleshooting.

---

## Disable Windows Audio service “if not needed”

Status conceitual: REJECTED

Motivo:

Destrói funcionalidade central do Windows e não representa otimização razoável.

---

## Kill audiodg.exe

Status conceitual: REJECTED

Motivo:

Audiodg é parte da engine/processamento de áudio do Windows. Uso anormal deve ser diagnosticado, não tratado como processo inútil.

---

## Registry “audio latency packs”

Status conceitual: REJECTED / UNVERIFIED

Motivo:

Não aplicar valores mágicos de buffer, scheduling, MMCSS ou Registry sem evidência técnica, applicability gates e benchmark.

---

## Set audio process to Realtime priority

Status conceitual: REJECTED

Motivo:

Realtime priority pode prejudicar estabilidade/scheduling do sistema e não é performance grátis.

---

## Disable Bluetooth audio power saving globally

Status conceitual: REJECTED

Motivo:

Diagnosticar transport/radio/device específico.

---

# Itens propositalmente não duplicados

## USB Selective Suspend

Pertence a USB / Energy.

Audio apenas consome a correlação quando um dispositivo USB sofre dropout.

---

## Full Bluetooth stack tuning

Pertence a USB / Peripherals / Bluetooth context.

Audio trata perfil/endpoint/quality.

---

## DPC/ISR implementation completa

É capability compartilhada de Hardware Diagnostics/System.

Audio consome os sinais e correlaciona com dropouts.

---

## HDMI/DisplayPort bandwidth

Pertence a Display/GPU.

Audio apenas usa o path para identificar endpoint/capabilities.

---

## Application performance optimization

Pertence a Processes/System.

Audio só mede impacto relevante ligado ao pipeline.

---

# Principais grupos futuros

Sem consolidar formalmente ainda:

- Audio Device & Endpoint Inventory
- Routing & Sessions
- Driver & Device Health
- Format / WASAPI
- Enhancements / APOs
- Spatial / Channels
- HDMI / USB / Bluetooth Audio
- Capture / Microphone
- Stability / Dropouts / DPC
- Windows Audio Services
- Virtual Audio / Streaming
- Repair
- Explain Why / Rollback / Validation

---

# Hipóteses / tópicos para Second Pass — Gap Audit

A segunda passada deverá procurar principalmente lacunas em:

- Bluetooth LE Audio;
- LC3 / modern Bluetooth audio context;
- microphone arrays / beamforming;
- communications processing modes;
- raw audio mode;
- low-latency shared mode;
- IAudioClient3 / engine periods;
- hardware offload audio;
- event-driven buffering;
- pro-audio / ASIO boundary;
- MIDI devices;
- audio interface buffer/driver awareness;
- sample-rate mismatch/resampling depth;
- HDMI EDID audio capabilities;
- eARC/ARC awareness;
- USB Audio Class nuances;
- endpoint jack detection;
- unplugged/disabled endpoint history;
- Nahimic/Sonic Studio/Dolby/DTS/vendor suites;
- APO crash/isolation;
- virtual mixer conflicts;
- loopback feedback;
- exclusive-mode conflict detection;
- audio sleep/resume failure;
- Bluetooth codec/profile changes;
- wireless headset battery/quality relation;
- noise suppression / voice-focus features;
- audio privacy indicators;
- microphone test methodology;
- speaker/channel test tool;
- accessibility/hearing-device context;
- recommendation confidence.

---

# Principais conclusões da First Pass

1. **Audio optimization deve preservar qualidade e recursos desejados.**

2. **Endpoint/routing incorreto é causa real e frequente de “sem som”.**

3. **Sample rate/bit depth maior não significa automaticamente som melhor.**

4. **Shared mode é o comportamento normal do Windows para coexistência entre apps.**

5. **Exclusive mode é contextual e não deve ser ativado globalmente.**

6. **Enhancements devem ser diagnosticados, não desativados preventivamente.**

7. **Spatial Audio é recurso de qualidade, não overhead a remover por padrão.**

8. **USB/Bluetooth/HDMI audio precisam ser correlacionados com seus transports físicos.**

9. **Microphone permissions podem parecer falha de hardware.**

10. **Bluetooth headset pode mudar de qualidade quando o microfone/communication path entra em uso.**

11. **DPC/ISR só deve ser considerado causa quando houver correlação temporal e evidência suficiente.**

12. **Windows Audio services podem ser reiniciados como repair contextual, não desativados.**

13. **Virtual audio devices devem ser preservados quando fazem parte do workflow do usuário.**

14. **Qualidade sonora subjetiva pode exigir A/B e escolha do usuário, não score automático.**

15. **Registry latency packs, Realtime priority e universal format tweaks não entram no produto.**

16. **Nothing to optimize continua sendo resultado válido.**

---

# Fontes técnicas de referência desta passada

## Microsoft — User-Mode Audio Components / WASAPI Shared vs Exclusive

https://learn.microsoft.com/windows/win32/coreaudio/user-mode-audio-components

## Microsoft — Fix Sound or Audio Problems

https://support.microsoft.com/windows/hardware/audio/fix-sound-or-audio-problems-in-windows

## Microsoft — Fix Distorted or Crackling Audio

https://support.microsoft.com/windows/hardware/audio/fix-distorted-or-crackling-audio-in-windows

## Microsoft — Core Audio APIs

https://learn.microsoft.com/windows/win32/coreaudio/core-audio-apis-in-windows-vista

---

# Status final

**Audio — First Pass: COMPLETE**

Capabilities brutas desta passada: **39**

Próximo passo da área:

**Second Pass — Consolidated Gap Audit**

# BeadWise / PC Optimizer V2 — Audio Discovery

## Second Pass — Consolidated Gap Audit

Esta passada adiciona apenas gaps reais da First Pass. O foco continua sendo qualidade, estabilidade e compatibilidade, preservando shared mode, enhancements, spatial audio e outros recursos quando funcionam corretamente.

---

# 40. Low-Latency Shared Mode Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: WASAPI / Low Latency

Primary Product Area: Audio

Possible Consumers:

- Pro Audio
- Gaming
- Communication

Final UI Placement: TBD

Descrição: Detectar periodicidades suportadas pelo Audio Engine em shared mode e distinguir baixa latência suportada de tweaks arbitrários.

Possível objetivo: Saber se o endpoint/driver suporta engine periods menores através de interfaces modernas do Windows.

Possível benefício: Permitir diagnóstico de latência sem recorrer a prioridade Realtime ou Registry packs.

Aplicabilidade: Supported Windows Audio Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- IAudioClient3 availability;
- current shared-mode engine period;
- supported period range;
- mix format;
- endpoint capability;

---

# 41. Raw Audio Processing Capability

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Audio Processing

Primary Product Area: Audio

Possible Consumers:

- Pro Audio
- Recording
- Repair

Final UI Placement: TBD

Descrição: Detectar suporte a raw processing e permitir bypass de processamento somente quando o workload realmente necessita.

Possível objetivo: Separar pipeline processado de captura/reprodução raw.

Possível benefício: Atender cenários profissionais e troubleshooting sem desativar enhancements globalmente.

Aplicabilidade: Supported Endpoints

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- RawProcessingSupported;
- stream category;
- APO bypass context;
- format compatibility;

---

# 42. Audio Engine Period / Buffer Context

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Latency

Primary Product Area: Audio

Possible Consumers:

- Benchmark
- Pro Audio
- Drivers

Final UI Placement: TBD

Descrição: Interpretar buffer/engine period como propriedade do endpoint, driver e modo de operação, não como número mágico.

Possível objetivo: Identificar quando a latência é limitada pelo endpoint/driver.

Possível benefício: Evitar ajustes irreais de buffer fora das APIs suportadas.

Aplicabilidade: Latency-sensitive Audio

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- default engine period;
- minimum supported period;
- shared/exclusive context;
- underrun correlation;

---

# 43. Hardware Audio Offload Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Offload

Primary Product Area: Audio

Possible Consumers:

- Battery
- Media
- Drivers

Final UI Placement: TBD

Descrição: Detectar quando endpoint/driver suporta processamento ou playback offloaded em hardware.

Possível objetivo: Contextualizar energia e compatibilidade sem assumir offload como mais rápido ou melhor.

Possível benefício: Preservar eficiência em dispositivos que se beneficiam do recurso.

Aplicabilidade: Supported Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- offload capability;
- stream category support;
- power context;
- driver support;

---

# 44. Bluetooth LE Audio Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Bluetooth Audio

Primary Product Area: Audio

Possible Consumers:

- Bluetooth
- Battery
- Accessibility

Final UI Placement: TBD

Descrição: Reconhecer dispositivos e caminhos modernos de Bluetooth LE Audio quando expostos pelo Windows.

Possível objetivo: Distinguir LE Audio de profiles Bluetooth clássicos.

Possível benefício: Melhorar diagnóstico de compatibilidade, qualidade e múltiplos endpoints modernos.

Aplicabilidade: Supported Windows / Bluetooth Hardware

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- LE Audio capability;
- device support;
- render/capture endpoints;
- driver/radio dependency;

---

# 45. Microphone Array & Beamforming Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Capture

Primary Product Area: Audio

Possible Consumers:

- Communication
- Laptop
- Accessibility

Final UI Placement: TBD

Descrição: Identificar arrays de microfone e processamento espacial de captura quando presentes.

Possível objetivo: Evitar tratar múltiplos elementos físicos como microfones independentes simples.

Possível benefício: Melhor troubleshooting de voz, direção e noise suppression.

Aplicabilidade: Microphone Arrays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- array geometry when exposed;
- beamforming capability;
- processing mode;
- vendor APO context;

---

# 46. Audio Processing Mode Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Processing

Primary Product Area: Audio

Possible Consumers:

- Communication
- Media
- Gaming

Final UI Placement: TBD

Descrição: Identificar categorias/modos de processamento usados pelo endpoint ou aplicação.

Possível objetivo: Contextualizar diferenças entre media, communications, speech e raw modes.

Possível benefício: Explicar mudanças de qualidade sem forçar um único modo global.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- audio category;
- processing mode;
- communications context;
- raw mode;

---

# 47. USB Audio Class Context

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB Audio

Primary Product Area: Shared

Possible Consumers:

- USB / Peripherals
- Drivers
- Audio

Final UI Placement: TBD

Descrição: Identificar interfaces USB Audio Class e sua relação com driver inbox ou vendor.

Possível objetivo: Distinguir limitações de classe/driver de falhas genéricas USB.

Possível benefício: Melhor suporte a DACs, interfaces e headsets USB.

Aplicabilidade: USB Audio Devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- USB audio class context;
- driver provider;
- endpoint mapping;
- device topology;

---

# 48. HDMI Audio Capability from Display Path

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Audio

Primary Product Area: Shared

Possible Consumers:

- Display
- GPU
- Audio

Final UI Placement: TBD

Descrição: Consumir capabilities de áudio anunciadas pelo display/path para validar canais e formatos disponíveis.

Possível objetivo: Explicar por que TV/monitor oferece ou não determinados modos.

Possível benefício: Evitar configurações impossíveis no endpoint HDMI/DP.

Aplicabilidade: HDMI / DisplayPort Audio

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- display endpoint;
- supported channels;
- format capability;
- hotplug state;

---

# 49. Audio Jack Detection & Connector State

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Hardware

Primary Product Area: Audio

Possible Consumers:

- Repair
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Detectar estado de jack/connector quando o codec e driver expõem essa informação.

Possível objetivo: Diferenciar fone desconectado, jack reassigned e endpoint indisponível.

Possível benefício: Reduzir troubleshooting desnecessário de driver.

Aplicabilidade: Supported Analog Audio Hardware

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- jack presence;
- connector type;
- endpoint state;
- retasking context;

---

# 50. Vendor Audio Suite Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Software

Primary Product Area: Shared

Possible Consumers:

- Startup
- Processes
- Drivers
- Audio

Final UI Placement: TBD

Descrição: Reconhecer suites como Dolby, DTS, Nahimic, Sonic Studio e equivalentes como componentes funcionais, não bloat automático.

Possível objetivo: Correlacionar efeitos/serviços com qualidade ou falhas.

Possível benefício: Preservar UX e remover apenas quando impacto/problema é demonstrado.

Aplicabilidade: Vendor-enhanced Audio

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- suite/service;
- APO relationship;
- startup components;
- resource usage;
- crash correlation;

---

# 51. Virtual Mixer / Feedback Loop Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Virtual Audio

Primary Product Area: Audio

Possible Consumers:

- Streaming
- Recording
- Repair

Final UI Placement: TBD

Descrição: Detectar routing virtual que cria feedback, duplicação ou loopback indesejado.

Possível objetivo: Separar problema de hardware de topologia virtual mal configurada.

Possível benefício: Melhor troubleshooting para streamers e criadores.

Aplicabilidade: Virtual Audio Workflows

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- virtual endpoints;
- loopback path;
- duplicate routing;
- feedback suspicion;

---

# 52. Audio Sleep / Resume Failure Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Audio Stability

Primary Product Area: Audio

Possible Consumers:

- Energy
- USB
- Bluetooth
- Drivers

Final UI Placement: TBD

Descrição: Correlacionar endpoint ausente, sem som ou crackling com sleep/resume.

Possível objetivo: Encontrar falhas temporais de driver/transport.

Possível benefício: Evitar reinstalação ampla quando o problema ocorre somente após resume.

Aplicabilidade: Sleep-capable PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- sleep/resume time;
- endpoint disappearance;
- driver reset;
- USB/Bluetooth reconnect;
- service recovery;

---

# 53. Speaker / Channel Test Tool

Status: CANDIDATE

Type: Tool / Diagnostic

Technical Domain: Audio Quality

Primary Product Area: Tools

Possible Consumers:

- Audio
- Home Theater

Final UI Placement: TBD

Descrição: Reproduzir sinais controlados por canal para validar speaker mapping e funcionamento.

Possível objetivo: Confirmar fisicamente cada canal do sistema.

Possível benefício: Detectar center/surround/subwoofer ausente ou mapeado incorretamente.

Aplicabilidade: Stereo / Multichannel Systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- left/right test;
- multichannel sequence;
- level observation;
- channel mapping;

---

# 54. Microphone Test & Clipping Validation

Status: CANDIDATE

Type: Tool / Diagnostic

Technical Domain: Audio Capture

Primary Product Area: Tools

Possible Consumers:

- Communication
- Recording

Final UI Placement: TBD

Descrição: Executar teste local de captura com medição simples de nível e clipping.

Possível objetivo: Validar microfone sem depender de aplicativo externo.

Possível benefício: Distinguir mute, ganho baixo, clipping e routing incorreto.

Aplicabilidade: Microphones

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- signal level;
- peak level;
- clipping indication;
- input endpoint;
- short playback option;

---

# 55. Audio Recommendation Confidence

Status: CANDIDATE

Type: Diagnostic / Recommendation Infrastructure

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- Audio
- Repair
- Drivers

Final UI Placement: TBD

Descrição: Classificar força da evidência para recomendações de áudio.

Possível objetivo: Separar causa confirmada de hipótese de driver, processing ou transport.

Possível benefício: Reduzir mudanças de qualidade baseadas em correlação fraca.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- direct endpoint evidence;
- A/B result;
- dropout correlation;
- driver event;
- user confirmation;

---

# Itens filtrados / rejeitados

## ASIO auto-tuning

Não entrar como tuning automático. ASIO é ecossistema de drivers/aplicações profissionais e configurações devem permanecer no software/driver próprio.

## Force minimum engine period

REJECTED como regra universal. Períodos menores podem aumentar risco de underrun e dependem do endpoint/driver.

## Disable all vendor APOs

REJECTED. Só testar bypass quando há sintoma e possibilidade de restauração.

## Bluetooth codec forcing

Não criar tweak genérico; disponibilidade e seleção dependem do stack, dispositivo e driver.

---

# Principais conclusões

1. Baixa latência moderna deve usar capabilities suportadas pelo Audio Engine, não Registry packs.
2. Raw mode é contextual e não substitui shared mode no uso comum.
3. Bluetooth LE Audio é contexto novo, mas não justifica tuning universal.
4. Vendor suites e APOs devem ser preservados quando fornecem funcionalidade desejada.
5. Audio após sleep/resume merece correlação temporal própria.
6. Ferramentas de teste simples agregam mais valor que elevar sample rate arbitrariamente.
7. Audio — INITIAL DISCOVERY COMPLETE.

---

# Fontes técnicas de referência

## Microsoft — Low Latency Audio / IAudioClient3

https://learn.microsoft.com/windows-hardware/drivers/audio/low-latency-audio

## Microsoft — Core Audio / WASAPI

https://learn.microsoft.com/windows/win32/coreaudio/core-audio-apis-in-windows-vista

## Microsoft — Bluetooth Audio

https://learn.microsoft.com/windows-hardware/drivers/bluetooth/

---
