# BeadWise / PC Optimizer V2 — Monitor / Display Discovery

## First Pass — Broad Discovery

Status da área: FIRST PASS COMPLETE

Objetivo desta passada:

Descobrir capabilities realmente úteis relacionadas a monitores, displays e pipeline de exibição no Windows, priorizando:

- identificação correta do display;
- capabilities reais do monitor;
- resolução e refresh rate;
- HDR;
- VRR;
- color management;
- scaling;
- multi-monitor;
- conexão/display path;
- diagnóstico visual;
- estabilidade;
- experiência do usuário.

Esta área adota explicitamente o filtro de UX definido para a V2:

> Não degradar qualidade visual, fluidez, legibilidade, consistência estética ou comportamento esperado do Windows em troca de ganhos pequenos, teóricos ou difíceis de perceber.

Outro princípio:

> Display optimization deve significar usar corretamente as capabilities do hardware e corrigir configurações/problemáticas reais — não empobrecer a interface do Windows.

Fluxo conceitual:

**Detect Display → Identify Capability → Validate Active Mode → Diagnose Path/Quality → Recommend → Configure Contextually → Validate → Keep/Rollback**

---

# Critério de relevância reforçado

Uma capability entra nesta área quando responde a uma pergunta útil como:

- Que monitor é esse?
- O Windows está usando resolução nativa?
- O monitor está realmente em 120/144/165/240 Hz?
- HDR está disponível e configurado corretamente?
- VRR está disponível e utilizável?
- Existe limitação de conexão/cabo/porta?
- Scaling está causando problema de legibilidade?
- Um monitor está conectado à GPU errada?
- Existe configuração inconsistente entre múltiplos monitores?
- O problema é pixel, ghosting, tearing, banding ou calibração?
- Há uma mudança real que melhora qualidade/funcionalidade?

Não criar capability apenas porque existe:

- Registry visual;
- efeito cosmético;
- animação;
- transparência;
- preferência estética;
- ajuste vendor-specific sem contexto;
- valor de gamma arbitrário;
- “gaming preset” universal.

---

# 1. Display Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Hardware

Primary Product Area: My PC

Possible Consumers:

- Optimization
- Gaming
- Monitoring
- Hardware Diagnostics
- Settings

Final UI Placement: TBD

Descrição: Identificar todos os displays ativos e conhecidos pelo Windows.

Possível objetivo: Criar a base de identidade para todas as capabilities condicionais de monitor/display.

Possível benefício: Evitar aplicar configuração ao monitor errado e distinguir setups single/multi-monitor.

Aplicabilidade: All PCs with Display

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- display friendly name;
- manufacturer;
- product/model;
- serial quando exposto;
- internal/external;
- active/inactive;
- primary display;
- connection target;
- associated adapter;
- monitor device instance;
- EDID identity quando disponível.

---

# 2. EDID & Display Capability Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Capability

Primary Product Area: Shared

Possible Consumers:

- Display
- GPU
- Gaming
- HDR
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Ler e interpretar informações de capacidade anunciadas pelo monitor via EDID/related display descriptors quando disponíveis.

Possível objetivo: Diferenciar capacidade física/anunciada do monitor da configuração ativa no Windows.

Possível benefício: Saber quais resoluções, refresh rates e recursos podem ser suportados.

Aplicabilidade: Supported Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- manufacturer/product;
- native/preferred timing;
- supported timings;
- physical size;
- color characteristics;
- HDR/static metadata capability quando exposta;
- serial/name descriptors;
- extension blocks;
- CTA/DisplayID context quando disponível.

Observação:

EDID é informação anunciada pelo display; não garante que toda capability funcione através de qualquer cabo/porta/path.

---

# 3. Active Display Path Mapping

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Topology

Primary Product Area: Shared

Possible Consumers:

- GPU
- Gaming
- USB-C
- Multi-Monitor
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Mapear como cada monitor ativo está conectado à GPU/adaptador e ao Windows display topology.

Possível objetivo: Identificar o caminho real da apresentação.

Possível benefício: Diagnosticar monitor conectado à GPU integrada, dock, USB-C ou outro caminho inesperado.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- source adapter;
- target display;
- source/target IDs;
- clone/extend topology;
- primary display;
- path active state;
- output technology;
- resolution;
- refresh;
- rotation;
- scaling context.

---

# 4. Native Resolution Validation

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Display Mode

Primary Product Area: Display

Possible Consumers:

- Gaming
- Accessibility
- Optimization

Final UI Placement: TBD

Descrição: Comparar resolução ativa com resolução nativa/preferida do monitor.

Possível objetivo: Detectar configurações que reduzem nitidez ou área útil sem necessidade.

Possível benefício: Recuperar qualidade visual de forma clara e perceptível.

Aplicabilidade: Fixed-Pixel Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- active resolution;
- native/preferred resolution;
- scaled mode;
- custom mode indication;
- multiple monitor differences.

Regra:

Resolução abaixo da nativa pode ser escolha legítima do usuário, especialmente por performance ou acessibilidade.

---

# 5. Refresh Rate Capability & Active Rate Validation

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Display Timing

Primary Product Area: Display

Possible Consumers:

- Gaming
- Monitoring
- Optimization

Final UI Placement: TBD

Descrição: Comparar refresh rates suportados com a taxa efetivamente ativa no Windows.

Possível objetivo: Detectar monitor de alta frequência operando acidentalmente em 60 Hz ou outro modo inferior.

Possível benefício: Uma das correções de experiência mais perceptíveis e de baixo risco quando a configuração está errada.

Aplicabilidade: Displays with Multiple Refresh Rates

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- active refresh rate;
- supported refresh rates;
- preferred refresh rate;
- active resolution;
- interlaced/progressive state;
- dynamic refresh availability;
- per-display differences.

---

# 6. Dynamic Refresh Rate Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Display Refresh

Primary Product Area: Display

Possible Consumers:

- Battery
- Energy
- Laptop
- Gaming

Final UI Placement: TBD

Descrição: Detectar suporte e uso de Dynamic Refresh Rate (DRR) em dispositivos compatíveis.

Possível objetivo: Diferenciar refresh fixo de comportamento dinâmico administrado pelo Windows.

Possível benefício: Preservar fluidez e autonomia em notebooks sem forçar refresh máximo continuamente.

Aplicabilidade: Supported Laptops / Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- DRR support;
- DRR active/available;
- current refresh;
- supported dynamic range;
- battery/AC context;
- app/game interaction.

Regra:

Não recomendar refresh máximo fixo em notebook como regra universal se DRR atende melhor ao objetivo.

---

# 7. Display Mode Compatibility Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Timing

Primary Product Area: Display

Possible Consumers:

- HDR
- Gaming
- Repair

Final UI Placement: TBD

Descrição: Detectar combinações de resolução, refresh, HDR e conexão que excedem ou pressionam a capacidade do caminho de vídeo.

Possível objetivo: Explicar casos em que uma função só funciona ao reduzir refresh ou resolução.

Possível benefício: Diagnosticar limitações reais de bandwidth/link sem classificar display/GPU como defeituosos.

Aplicabilidade: High-Bandwidth Display Modes

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- HDR unavailable at current mode;
- refresh option missing at current resolution;
- color depth reduction;
- connection bandwidth limitation;
- output format limitation;
- monitor capability vs active path.

---

# 8. HDR Capability Detection

Status: CANDIDATE

Type: Diagnostic

Technical Domain: HDR

Primary Product Area: Display

Possible Consumers:

- Gaming
- Media
- GPU
- Calibration

Final UI Placement: TBD

Descrição: Detectar se o display e o Windows suportam HDR para jogos, aplicativos e/ou streaming.

Possível objetivo: Diferenciar display HDR verdadeiro de sistema onde HDR está indisponível ou parcialmente suportado.

Possível benefício: Evitar recomendações inválidas e explicar recursos disponíveis.

Aplicabilidade: HDR-capable Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- HDR supported;
- HDR state;
- HDR video support;
- games/apps HDR support;
- Auto HDR availability;
- bit depth/context;
- WDDM/driver requirement;
- internal/external display.

---

# 9. HDR Configuration Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: HDR

Primary Product Area: Display

Possible Consumers:

- Gaming
- Media
- Optimization

Final UI Placement: TBD

Descrição: Avaliar configuração HDR ativa e detectar problemas comuns de brilho, cor ou incompatibilidade.

Possível objetivo: Melhorar qualidade visual sem simplesmente desligar HDR.

Possível benefício: Corrigir HDR lavado, escuro, saturação incorreta ou configuração inconsistente.

Aplicabilidade: HDR Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis verificações:

- HDR enabled;
- SDR/HDR brightness balance;
- supported display mode;
- app-specific behavior;
- connection type;
- graphics driver;
- calibration profile presence;
- multiple display context.

Regra:

HDR ON ou OFF não é universalmente "melhor"; deve respeitar display, conteúdo e preferência.

---

# 10. HDR Calibration Guidance

Status: CANDIDATE

Type: Tool / Manual Guidance

Technical Domain: HDR Calibration

Primary Product Area: Tools

Possible Consumers:

- Display
- Gaming
- Media

Final UI Placement: TBD

Descrição: Orientar calibração HDR usando mecanismos oficiais do Windows em displays compatíveis.

Possível objetivo: Ajustar níveis de preto, pico de brilho e saturação com método apropriado.

Possível benefício: Melhorar fidelidade e consistência visual de forma perceptível.

Aplicabilidade: HDR Displays / Windows 11

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis elementos:

- Windows HDR Calibration app;
- darkest visible detail;
- brightest visible detail;
- maximum display luminance;
- SDR/HDR color saturation;
- calibration profile.

Regra:

Não aplicar valores mágicos de brilho/nits sem medição ou interação do usuário.

---

# 11. SDR Content Brightness in HDR Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: HDR / SDR Integration

Primary Product Area: Display

Possible Consumers:

- UX
- Media
- Gaming

Final UI Placement: TBD

Descrição: Diagnosticar equilíbrio de brilho de conteúdo SDR quando HDR está habilitado.

Possível objetivo: Corrigir desktops/apps excessivamente claros, escuros ou dessaturados em modo HDR.

Possível benefício: Melhor experiência diária sem exigir desativar HDR permanentemente.

Aplicabilidade: HDR Displays

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- HDR state;
- SDR content brightness setting;
- app behavior;
- display type;
- calibration state.

---

# 12. Variable Refresh Rate Capability Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: VRR

Primary Product Area: Display

Possible Consumers:

- Gaming
- GPU
- Benchmark

Final UI Placement: TBD

Descrição: Detectar suporte e contexto de VRR do display/path.

Possível objetivo: Determinar se Adaptive-Sync/FreeSync/G-SYNC-compatible behavior é tecnicamente possível.

Possível benefício: Melhor diagnóstico de tearing, stutter e sincronização.

Aplicabilidade: VRR-capable Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- VRR capability;
- active path;
- refresh range quando disponível;
- GPU support;
- output connection;
- presentation requirements;
- Windows/driver configuration.

---

# 13. VRR Configuration & Validation

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: VRR

Primary Product Area: Display

Possible Consumers:

- Gaming
- Benchmark

Final UI Placement: TBD

Descrição: Validar se VRR está realmente utilizável no cenário atual em vez de apenas detectar suporte nominal.

Possível objetivo: Encontrar capability disponível mas não efetivamente funcional por configuração/path.

Possível benefício: Melhorar fluidez em workloads compatíveis sem alterar qualidade visual.

Aplicabilidade: VRR-capable Gaming Systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Benchmark First

Possíveis fatores:

- display VRR support;
- GPU support;
- active refresh;
- presentation model;
- tearing support;
- VSync relationship;
- FPS range;
- multi-monitor context.

---

# 14. Multi-Monitor Topology Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Multi-Monitor

Primary Product Area: Display

Possible Consumers:

- Gaming
- Productivity
- GPU
- Settings

Final UI Placement: TBD

Descrição: Mapear configurações com múltiplos monitores e diferenças relevantes entre eles.

Possível objetivo: Evitar aplicar pressupostos de um display a outro.

Possível benefício: Diagnosticar refresh, scaling, HDR e path de forma correta por monitor.

Aplicabilidade: Multi-Monitor PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- display count;
- primary;
- extend/clone;
- relative position;
- orientation;
- per-display resolution;
- per-display refresh;
- per-display scaling;
- HDR;
- VRR;
- associated GPU/path.

---

# 15. Mixed Refresh Rate Multi-Monitor Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Multi-Monitor

Primary Product Area: Display

Possible Consumers:

- Gaming
- Monitoring
- GPU

Final UI Placement: TBD

Descrição: Reconhecer setups com monitores operando em refresh rates diferentes.

Possível objetivo: Contextualizar comportamento percebido de animação, vídeo, presentation e GPU load.

Possível benefício: Evitar recomendar reduzir o monitor principal apenas para igualar o secundário.

Aplicabilidade: Multi-Monitor / Mixed Refresh

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Regra:

Refresh rates diferentes não são problema por definição.

---

# 16. Display Scaling & DPI Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: DPI / Scaling

Primary Product Area: Display

Possible Consumers:

- Accessibility
- UX
- Applications

Final UI Placement: TBD

Descrição: Identificar scaling/DPI por monitor e detectar combinações que geram legibilidade ou comportamento inconsistente.

Possível objetivo: Preservar nitidez e conforto visual em displays de densidades diferentes.

Possível benefício: Melhor UX sem sacrificar resolução nativa.

Aplicabilidade: All Modern Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- scale percentage;
- recommended scale;
- per-monitor DPI;
- custom scaling;
- mixed-DPI setup;
- resolution;
- app compatibility symptoms.

Regra:

Não reduzir scaling apenas para "ganhar espaço" ou performance se isso prejudica legibilidade.

---

# 17. Resolution vs Scaling Recommendation Logic

Status: CANDIDATE

Type: Diagnostic / Recommendation

Technical Domain: Display UX

Primary Product Area: Display

Possible Consumers:

- Accessibility
- UX

Final UI Placement: TBD

Descrição: Preferir resolução nativa + scaling apropriado quando o problema é tamanho de interface, em vez de reduzir resolução sem necessidade.

Possível objetivo: Manter nitidez do painel.

Possível benefício: Melhor legibilidade com qualidade visual superior.

Aplicabilidade: Fixed-Pixel Displays

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Regra:

Se o usuário acha elementos pequenos, scaling geralmente é abordagem mais apropriada do que reduzir a resolução nativa.

---

# 18. Display Orientation & Layout Validation

Status: CANDIDATE

Type: Configuration / Diagnostic

Technical Domain: Display Topology

Primary Product Area: Display

Possible Consumers:

- Productivity
- Settings

Final UI Placement: TBD

Descrição: Validar orientação, posição relativa e layout de múltiplos displays.

Possível objetivo: Corrigir cursor/janelas atravessando para o lado errado ou orientação incorreta.

Possível benefício: Melhora direta de UX sem qualquer trade-off de performance.

Aplicabilidade: Multi-Monitor PCs

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

---

# 19. Color Depth & Output Capability Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Color

Primary Product Area: Display

Possible Consumers:

- HDR
- GPU
- Media
- Gaming

Final UI Placement: TBD

Descrição: Identificar bit depth e capacidades de saída relevantes para qualidade de cor.

Possível objetivo: Detectar quando um modo de alta resolução/refresh força redução de profundidade ou formato.

Possível benefício: Explicar banding, HDR limitado ou trade-offs de link bandwidth.

Aplicabilidade: Supported Displays / GPUs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- active bit depth;
- supported bit depth;
- HDR context;
- resolution/refresh interaction;
- output capability;
- link limitation suspicion.

---

# 20. Color Profile / ICC Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Color Management

Primary Product Area: Display

Possible Consumers:

- Photography
- Design
- Media
- HDR

Final UI Placement: TBD

Descrição: Identificar perfis de cor associados ao display e configuração de color management do Windows.

Possível objetivo: Evitar alterações que removam calibração existente ou apliquem perfil ao monitor errado.

Possível benefício: Preservar fidelidade de cor e detectar configuração de perfil incorreta.

Aplicabilidade: All Displays / Especially Color-Critical Workloads

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- associated ICC/ICM profile;
- default profile;
- per-display assignment;
- calibration loader/context;
- advanced color context.

Regra:

Não remover perfil ICC porque "default Windows color is faster".

---

# 21. Windows Color Calibration Guidance

Status: CANDIDATE

Type: Tool / Manual Guidance

Technical Domain: Color Calibration

Primary Product Area: Tools

Possible Consumers:

- Display
- Media
- Design

Final UI Placement: TBD

Descrição: Orientar calibração SDR usando ferramentas apropriadas do Windows quando o usuário desejar corrigir aparência de cor/gamma.

Possível objetivo: Melhorar fidelidade visual de forma controlada.

Possível benefício: Corrigir display claramente mal ajustado sem aplicar presets arbitrários.

Aplicabilidade: Optional

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: Manual Guidance

Regra:

Calibration é preference/accuracy workflow, não performance optimization.

---

# 22. Gamma / Brightness / Contrast Diagnostic Guidance

Status: CANDIDATE

Type: Diagnostic / Manual Guidance

Technical Domain: Display Quality

Primary Product Area: Tools

Possible Consumers:

- Calibration
- Accessibility
- Media

Final UI Placement: TBD

Descrição: Oferecer testes visuais para detectar ajustes obviamente inadequados de gamma, brightness e contrast.

Possível objetivo: Ajudar usuário a configurar o monitor sem aplicar valores universais.

Possível benefício: Melhorar detalhes de sombras, highlights e conforto visual.

Aplicabilidade: Optional

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: Manual Guidance

Não fazer:

- definir gamma/brightness "ideal" universal;
- alterar OSD do monitor sem suporte/documentação confiável.

---

# 23. Display Connection Type Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Connectivity

Primary Product Area: Display

Possible Consumers:

- GPU
- HDR
- High Refresh
- USB-C

Final UI Placement: TBD

Descrição: Identificar tecnologia/path de saída utilizada para cada monitor.

Possível objetivo: Contextualizar bandwidth, HDR, refresh, VRR e adapters/docks.

Possível benefício: Encontrar limitações causadas por HDMI/DisplayPort/USB-C/dock em vez do monitor.

Aplicabilidade: External Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis contextos:

- HDMI;
- DisplayPort;
- internal panel;
- USB-C Alternate Mode;
- USB4/Thunderbolt path;
- wireless display;
- adapter/converter path quando identificável.

---

# 24. Display Link Bottleneck Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Connectivity

Primary Product Area: Display

Possible Consumers:

- HDR
- High Refresh
- Gaming
- Repair

Final UI Placement: TBD

Descrição: Correlacionar recursos indisponíveis ou degradados com limitações do caminho de vídeo.

Possível objetivo: Identificar quando cabo, porta, adapter ou dock impede determinada combinação de resolução/refresh/HDR.

Possível benefício: Evitar trocar monitor/GPU ou reduzir qualidade sem entender o path.

Aplicabilidade: High-Bandwidth External Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sintomas:

- expected refresh missing;
- HDR unavailable;
- lower color depth;
- intermittent signal;
- resolution limitation;
- monitor detected at reduced capability.

---

# 25. Display Signal Loss / Reconnect Monitoring

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Display Stability

Primary Product Area: Monitoring

Possible Consumers:

- GPU
- Drivers
- USB-C
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Registrar eventos de perda/retorno de monitor ou mudança de topologia.

Possível objetivo: Diagnosticar tela piscando, monitor apagando ou sendo reconectado.

Possível benefício: Correlacionar falha com driver reset, cabo/path, dock ou power event.

Aplicabilidade: All Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis eventos:

- display arrival/removal;
- topology change;
- mode change;
- source adapter change;
- driver reset correlation;
- sleep/resume;
- dock reconnect;
- HDR/refresh state change.

---

# 26. Display Driver / GPU Reset Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Stability

Primary Product Area: Display

Possible Consumers:

- GPU
- Drivers
- Repair
- Gaming

Final UI Placement: TBD

Descrição: Correlacionar perda de vídeo/flicker com eventos de driver gráfico ou GPU reset.

Possível objetivo: Diferenciar problema físico de monitor/cabo de falha da pilha gráfica.

Possível benefício: Melhor troubleshooting de black screen/flicker.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- TDR/driver reset;
- display reconnect;
- event log;
- mode reset;
- crash history;
- multi-monitor simultaneous loss.

---

# 27. Dead / Stuck Pixel Diagnostic Test

Status: CANDIDATE

Type: Tool / Diagnostic

Technical Domain: Panel Diagnostics

Primary Product Area: Tools

Possible Consumers:

- Hardware Diagnostics
- Display

Final UI Placement: TBD

Descrição: Exibir padrões de cores sólidas para facilitar identificação de pixels mortos ou presos.

Possível objetivo: Diferenciar defeito de pixel de artefato de software, sujeira ou conteúdo.

Possível benefício: Diagnóstico visual simples e útil.

Aplicabilidade: Flat-Panel Displays

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis padrões:

- black;
- white;
- red;
- green;
- blue;
- gray.

Regra:

Não prometer recuperação de dead pixel.

---

# 28. Stuck Pixel Exercise Tool

Status: CANDIDATE

Type: Tool

Technical Domain: Panel Diagnostics

Primary Product Area: Tools

Possible Consumers:

- Display

Final UI Placement: TBD

Descrição: Oferecer padrão de mudança rápida de cores em região controlada como tentativa opcional para stuck/subpixel behavior.

Possível objetivo: Disponibilizar técnica de baixo risco conhecida para alguns casos de stuck pixel.

Possível benefício: Pode ajudar em casos específicos sem prometer correção.

Aplicabilidade: Stuck Pixel Suspected

Shared Capability: No

Risco inicial: Low

Evidence Level: Unverified / Context-dependent

Action Mode: User Choice

Safeguards:

- warning about limited evidence;
- time-limited session;
- stop control;
- do not present as repair for dead pixels;
- avoid seizure-sensitive flashing without warning.

---

# 29. Backlight Bleed / Uniformity Test

Status: CANDIDATE

Type: Tool / Diagnostic

Technical Domain: Panel Quality

Primary Product Area: Tools

Possible Consumers:

- Hardware Diagnostics
- Display

Final UI Placement: TBD

Descrição: Exibir padrões adequados para inspeção visual de uniformidade, glow e bleed.

Possível objetivo: Ajudar usuário a caracterizar problema físico do painel.

Possível benefício: Melhor distinção entre configuração e limitação/defeito do hardware.

Aplicabilidade: Displays

Shared Capability: No

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Manual Guidance

Observação:

Interpretação depende de ambiente, tecnologia do painel e percepção; o BeadWise não deve diagnosticar automaticamente defeito físico só por foto/sensor inexistente.

---

# 30. Banding / Gradient Test

Status: CANDIDATE

Type: Tool / Diagnostic

Technical Domain: Display Quality

Primary Product Area: Tools

Possible Consumers:

- Color
- HDR
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Exibir gradients controlados para observar banding e tonal transitions.

Possível objetivo: Diferenciar problema de content/compression de limitação de bit depth, profile ou painel.

Possível benefício: Auxiliar diagnóstico de cor.

Aplicabilidade: Displays

Shared Capability: No

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Manual Guidance

---

# 31. Ghosting / Motion Clarity Test

Status: CANDIDATE

Type: Tool / Diagnostic

Technical Domain: Motion Quality

Primary Product Area: Tools

Possible Consumers:

- Gaming
- Display

Final UI Placement: TBD

Descrição: Exibir movimento controlado para inspeção de ghosting, overshoot e motion clarity.

Possível objetivo: Ajudar usuário a avaliar response-time/overdrive behavior visualmente.

Possível benefício: Detectar OSD overdrive excessivo ou painel com comportamento inadequado para preferência do usuário.

Aplicabilidade: Displays

Shared Capability: No

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Manual Guidance

Regra:

Não inventar "response time em ms" sem instrumentação apropriada.

---

# 32. Tearing Visual Validation

Status: CANDIDATE

Type: Tool / Diagnostic

Technical Domain: Motion / Synchronization

Primary Product Area: Tools

Possible Consumers:

- Gaming
- VRR
- Benchmark

Final UI Placement: TBD

Descrição: Exibir conteúdo em movimento apropriado para observar tearing e comportamento de sincronização.

Possível objetivo: Complementar diagnóstico de VRR/VSync com percepção direta.

Possível benefício: Validar se configuração de sincronização está produzindo o resultado desejado.

Aplicabilidade: Gaming Displays

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: Manual Guidance

---

# 33. Display Power / Sleep Behavior Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Power

Primary Product Area: Energy

Possible Consumers:

- Display
- Sleep/Wake
- Laptop
- Repair

Final UI Placement: TBD

Descrição: Correlacionar monitores que não acordam, piscam ou mudam de modo com sleep/resume e políticas de energia.

Possível objetivo: Diagnosticar display wake failures sem desativar políticas de economia globalmente.

Possível benefício: Melhor estabilidade preservando comportamento normal do Windows.

Aplicabilidade: Sleep-capable PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

---

# 34. Internal Display Brightness Capability Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Display Brightness

Primary Product Area: Display

Possible Consumers:

- Battery
- Energy
- Accessibility

Final UI Placement: TBD

Descrição: Detectar controles de brilho disponíveis para displays internos e preservar comportamento adaptativo quando apropriado.

Possível objetivo: Separar brilho de qualidade/performance e respeitar autonomia/conforto.

Possível benefício: Melhor UX e bateria em notebooks.

Aplicabilidade: Internal Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Regra:

Não elevar brilho como “performance setting”.

---

# 35. Content-Adaptive Brightness Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Display Power / UX

Primary Product Area: Display

Possible Consumers:

- Battery
- Energy
- Accessibility

Final UI Placement: TBD

Descrição: Detectar comportamento de brilho adaptativo/content-adaptive em dispositivos compatíveis.

Possível objetivo: Diferenciar mudança automática de brilho de falha do painel.

Possível benefício: Permitir escolha informada entre consistência visual e economia de energia.

Aplicabilidade: Supported Laptops / Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Regra:

Não desativar automaticamente por estética/performance; é preferência contextual.

---

# 36. Wireless Display Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Wireless Display

Primary Product Area: Display

Possible Consumers:

- Network
- Media
- Latency

Final UI Placement: TBD

Descrição: Reconhecer quando um display utiliza caminho wireless em vez de saída física.

Possível objetivo: Ajustar expectativas de latency, quality e stability.

Possível benefício: Evitar diagnosticar wireless display como se fosse HDMI/DisplayPort local.

Aplicabilidade: Wireless Display

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

---

# 37. Display Recommendation Explanation

Status: CANDIDATE

Type: Diagnostic / Quality of Life

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- Display
- Optimization
- Gaming
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Explicar por que uma recomendação de display existe e qual efeito visual/funcional é esperado.

Possível objetivo: Impedir recomendações opacas do tipo "Optimize Display".

Possível benefício: Preservar autonomia do usuário e evitar perda de qualidade visual.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possível explicação:

- current state;
- supported capability;
- mismatch/problem;
- expected improvement;
- visual trade-off;
- performance trade-off;
- power trade-off;
- rollback.

---

# 38. Display Configuration Backup & Rollback

Status: CANDIDATE

Type: Safeguard / Repair

Technical Domain: Display Configuration

Primary Product Area: Shared

Possible Consumers:

- Display
- Optimization
- Settings

Final UI Placement: TBD

Descrição: Registrar alterações realizadas pelo BeadWise em resolução, refresh, HDR ou outras configurações suportadas e restaurar estado anterior quando possível.

Possível objetivo: Evitar que usuário fique preso em configuração visual inadequada.

Possível benefício: Experimentação segura.

Aplicabilidade: Display Configuration Changes

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis dados:

- display identity;
- original mode;
- new mode;
- HDR state;
- refresh;
- resolution;
- scaling when changed;
- timestamp;
- validation result;
- rollback result.

---

# 39. Display Change Outcome Validation

Status: CANDIDATE

Type: Diagnostic / Validation

Technical Domain: Recommendation Validation

Primary Product Area: Display

Possible Consumers:

- Optimization
- Gaming
- History

Final UI Placement: TBD

Descrição: Verificar se uma alteração de display produziu o efeito esperado e manteve imagem estável.

Possível objetivo: Diferenciar configuração aplicada de configuração realmente melhor.

Possível benefício: Reverter mudanças que causam sinal instável, qualidade inferior ou desconforto.

Aplicabilidade: Display Changes

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis resultados:

- Improved;
- Valid / Expected;
- No Benefit;
- Signal Regression;
- Visual Regression;
- Incompatible;
- User Rejected;
- Rolled Back.

---

# Itens filtrados / rejeitados nesta First Pass

## Disable Windows animations for performance

Status conceitual: REJECTED como otimização padrão

Motivo:

Reduzir animações pode alterar sensação de fluidez e acabamento do Windows. Em hardware moderno, o benefício normalmente não justifica degradar UX sem evidência clara.

Pode permanecer apenas como preferência de acessibilidade/usuário fora do fluxo padrão de performance.

---

## Disable transparency effects for performance

Status conceitual: REJECTED como otimização padrão

Motivo:

É elemento visual do Windows e o ganho potencial costuma ser pequeno demais para justificar perda estética.

---

## "Adjust for best performance" visual preset

Status conceitual: REJECTED como recomendação padrão

Motivo:

Desativa vários elementos de UX de uma vez, incluindo animações e efeitos, sem diagnóstico individual de benefício.

---

## Force 60 Hz for lower GPU usage

Status conceitual: REJECTED como otimização universal

Motivo:

Reduzir refresh pode degradar diretamente fluidez e responsividade. Só faz sentido por autonomia, troubleshooting de bandwidth/HDR ou preferência explícita.

---

## Force maximum refresh rate everywhere

Status conceitual: REJECTED como regra universal

Motivo:

Em desktops pode ser desejável, mas notebooks podem se beneficiar de DRR/refresh menor em bateria. Além disso, alguns modos altos impõem trade-offs de HDR/color depth/link bandwidth.

---

## Disable HDR to gain performance

Status conceitual: REJECTED como regra universal

Motivo:

HDR é recurso de qualidade visual. Qualquer impacto precisa ser medido e decisão deve ser contextual, especialmente em gaming.

---

## Disable VRR

Status conceitual: REJECTED como regra universal

Motivo:

VRR é capability moderna útil. Problemas devem ser diagnosticados, não resolvidos desligando o recurso por padrão.

---

## Arbitrary gamma Registry tweaks

Status conceitual: REJECTED

Motivo:

Color calibration deve utilizar mecanismos adequados e interação visual do usuário.

---

## Remove ICC profiles

Status conceitual: REJECTED

Motivo:

Pode destruir calibração de cor válida e prejudicar workloads criativos.

---

## Lower resolution for "more Windows performance"

Status conceitual: REJECTED como recomendação de desktop

Motivo:

Reduz qualidade e nitidez. Em jogos, resolução é decisão do game/profile; no desktop, resolução nativa deve ser preferida salvo motivo real.

---

## MPO disable as display optimization

Status conceitual: REJECTED como regra universal

Motivo:

Já tratado na área GPU/Presentation como troubleshooting contextual. Não duplicar como tweak de display.

---

# Itens propositalmente não duplicados

## HAGS

Pertence a GPU / Windows Graphics.

Display apenas consome efeitos observáveis.

---

## Fullscreen / Borderless / Present Model

Pertence a Gaming/GPU Presentation.

Display fornece capability e active mode context.

---

## GPU color settings vendor-specific

Não criar uma feature por painel NVIDIA/AMD/Intel nesta área.

Podem ser considerados posteriormente dentro de driver/profile diagnostics quando API confiável existir.

---

## Performance benchmark de GPU

Pertence a GPU/Benchmark.

Display apenas garante contexto de resolução/refresh/HDR.

---

## Screen recording / capture

Pertence a Gaming/System/GPU.

---

# Principais grupos futuros

Sem consolidar formalmente ainda:

- Display Identity & Capability
- Active Display Path
- Resolution & Refresh
- HDR
- VRR
- Multi-Monitor
- DPI / Scaling
- Color Management
- Connectivity / Link Capability
- Stability
- Panel Quality Tests
- Power / Brightness
- Wireless Display
- Explain Why / Rollback / Validation

---

# Hipóteses / tópicos para Second Pass — Gap Audit

A segunda passada deverá procurar principalmente lacunas em:

- Advanced Color / wide gamut;
- Auto Color Management;
- SDR color management in HDR mode;
- ICC vs advanced color interactions;
- HDR certification/luminance metadata;
- DSC awareness;
- HDMI/DisplayPort version inference limitations;
- chroma subsampling / pixel format awareness;
- clone-mode limitations;
- mixed HDR multi-monitor;
- mixed DPI edge cases;
- orientation sensors;
- laptop internal panel features;
- OLED-specific behavior;
- burn-in prevention safeguards;
- monitor DDC/CI capability;
- brightness control for external monitors;
- monitor firmware awareness;
- docking/display hotplug;
- display over USB4/Thunderbolt;
- wireless display latency;
- remote desktop/display virtualization;
- virtual displays;
- privacy screen capability;
- HDCP/protected content diagnostics;
- colorimeter integration boundaries;
- refresh-rate measurement/validation methodology;
- accessibility interactions;
- seizure-sensitive flashing safeguards;
- display recommendation confidence.

---

# Principais conclusões da First Pass

1. **Display optimization deve preservar qualidade visual e UX.**

2. **Resolution native + appropriate scaling é preferível a reduzir resolução para melhorar legibilidade.**

3. **Monitor de alta frequência operando em 60 Hz é um problema real e perceptível que vale detectar.**

4. **DRR pode ser melhor que refresh máximo fixo em notebooks.**

5. **HDR deve ser configurado/calibrado, não simplesmente desativado.**

6. **VRR é capability moderna e deve ser validada, não tratada como tweak.**

7. **Multi-monitor exige configuração por display.**

8. **Mixed refresh não é problema automaticamente.**

9. **Color profiles devem ser preservados.**

10. **Display path e conexão podem limitar refresh/HDR/color depth.**

11. **Dead pixel e stuck pixel são problemas diferentes.**

12. **Pixel repair não pode prometer resultado garantido.**

13. **Ghosting/motion clarity devem ser avaliados visualmente sem inventar response time.**

14. **Animações/transparência do Windows não entram no fluxo padrão de otimização de display.**

15. **Qualidade visual é parte do valor do sistema, não overhead a ser removido por padrão.**

16. **Nothing to optimize é resultado válido.**

---

# Fontes técnicas de referência desta passada

## Microsoft — HDR Settings in Windows

https://support.microsoft.com/windows/hardware/display-graphics/hdr-settings-in-windows

## Microsoft — Windows HDR Calibration

https://support.microsoft.com/windows/hardware/display-graphics/calibrate-your-hdr-display-using-the-windows-hdr-calibration-app

## Microsoft — Variable Refresh Rate Displays

https://learn.microsoft.com/windows/win32/direct3ddxgi/variable-refresh-rate-displays

## Microsoft — Display Configuration APIs

https://learn.microsoft.com/windows-hardware/drivers/display/connecting-and-configuring-displays

## Microsoft — CCD / DisplayConfig

https://learn.microsoft.com/windows-hardware/drivers/display/ccd-apis

---

# Status final

**Monitor / Display — First Pass: COMPLETE**

Capabilities brutas desta passada: **39**

Próximo passo da área:

**Second Pass — Consolidated Gap Audit**

# BeadWise / PC Optimizer V2 — Monitor / Display Discovery

## Second Pass — Consolidated Gap Audit

Status da área: SECOND PASS COMPLETE

Base analisada:

- Monitor / Display First Pass;
- 01-DISCOVERY;
- inventário funcional V1 apenas como fonte secundária;
- documentação técnica atual da Microsoft para Advanced Color, ICC/ACM, DDC/CI e display management.

Objetivo desta passada:

Encontrar perguntas relevantes de Monitor / Display ainda não respondidas pelas 39 capabilities da First Pass.

Regra desta passada:

> adicionar somente lacunas reais e com valor perceptível de qualidade, compatibilidade, diagnóstico ou segurança.

Filtro mantido:

> não degradar UI/UX, fluidez, HDR, cor, scaling, legibilidade ou recursos modernos do Windows em troca de ganhos marginais.

As capabilities abaixo continuam a numeração da First Pass.

---

# 40. Advanced Color Capability & State Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Advanced Color

Primary Product Area: Display

Possible Consumers:

- HDR
- Color Management
- Media
- Design

Final UI Placement: TBD

Descrição: Detectar suporte e estado de Advanced Color no Windows, abrangendo HDR, wide color gamut e high bit depth quando aplicáveis.

Possível objetivo: Diferenciar display convencional de pipeline de cor avançado gerenciado pelo Windows.

Possível benefício: Evitar diagnóstico incorreto de ICC, bit depth e gamut quando Advanced Color está ativo.

Aplicabilidade: Supported Displays / Windows 11

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- Advanced Color supported;
- Advanced Color active;
- HDR state;
- wide color gamut capability;
- high bit-depth context;
- SDR/HDR display type;
- per-display state.

Observação:

Advanced Color é um conceito do sistema operacional, não apenas uma propriedade física do monitor.

---

# 41. Auto Color Management Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Color Management

Primary Product Area: Display

Possible Consumers:

- Design
- Media
- HDR
- Color Profiles

Final UI Placement: TBD

Descrição: Detectar quando o gerenciamento automático de cores do Windows está ativo e considerar seu impacto na apresentação de aplicações.

Possível objetivo: Evitar recomendações legadas de color management que entram em conflito com o pipeline moderno do Windows.

Possível benefício: Preservar precisão de cor e reduzir alterações desnecessárias em perfis.

Aplicabilidade: Supported Windows / Displays

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- ACM available;
- ACM active;
- display gamut context;
- application compatibility context;
- profile behavior;
- Advanced Color state.

Regra:

Não desativar Auto Color Management por “performance”.

---

# 42. ICC Profile Compatibility Under Advanced Color

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Color Management

Primary Product Area: Display

Possible Consumers:

- Design
- Photography
- Media
- Repair

Final UI Placement: TBD

Descrição: Diagnosticar interação entre perfis ICC tradicionais e Advanced Color/Auto Color Management.

Possível objetivo: Detectar casos em que uma aplicação color-managed apresenta comportamento diferente quando Advanced Color está ativo.

Possível benefício: Evitar remover perfis válidos ou culpar o monitor quando a diferença vem do modelo de color management.

Aplicabilidade: Color-Managed Workloads

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- active ICC/ICM profile;
- Advanced Color state;
- ICC compatibility helper context;
- application color-management behavior;
- gamut restriction/sRGB compatibility context;
- per-display profile assignment.

---

# 43. HDR / Wide-Gamut Multi-Monitor Consistency Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Multi-Monitor Color

Primary Product Area: Display

Possible Consumers:

- HDR
- Color Management
- Media

Final UI Placement: TBD

Descrição: Detectar diferenças de HDR, gamut, bit depth e color profile entre monitores de um mesmo setup.

Possível objetivo: Explicar por que a mesma janela/conteúdo aparenta mudar ao mover-se entre displays.

Possível benefício: Melhor diagnóstico de inconsistência visual sem tentar igualar monitores fisicamente diferentes.

Aplicabilidade: Multi-Monitor / Mixed Display Capability

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis comparações:

- HDR state per display;
- gamut capability;
- bit depth;
- ICC profile;
- Advanced Color state;
- scaling;
- brightness context.

Regra:

Monitores diferentes não precisam produzir aparência idêntica; diferenças físicas podem ser legítimas.

---

# 44. Display Stream Compression Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Transport

Primary Product Area: Display

Possible Consumers:

- High Refresh
- HDR
- USB-C
- GPU

Final UI Placement: TBD

Descrição: Reconhecer quando combinações de alta resolução, alta taxa de atualização e color depth podem depender de Display Stream Compression (DSC) ou outras capacidades do link.

Possível objetivo: Contextualizar modos que parecem exceder bandwidth bruto simples da conexão.

Possível benefício: Evitar classificar um modo válido como impossível ou recomendar redução de qualidade sem necessidade.

Aplicabilidade: High-Bandwidth Display Modes

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- DSC capability quando identificável;
- high-resolution/high-refresh mode;
- color depth;
- link type;
- GPU/display capability;
- dock/USB-C path.

Observação:

A disponibilidade prática pode depender de GPU, display, cabo, dock e firmware.

---

# 45. Chroma Subsampling & Pixel Format Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Output Format

Primary Product Area: Display

Possible Consumers:

- HDR
- Media
- Gaming
- Text Quality

Final UI Placement: TBD

Descrição: Identificar quando o output de vídeo utiliza formatos que podem reduzir chroma resolution ou alterar apresentação de texto/cor.

Possível objetivo: Explicar trade-offs de bandwidth em combinações de resolução/refresh/HDR.

Possível benefício: Diagnosticar texto borrado ou perda de detalhe cromático sem reduzir resolução automaticamente.

Aplicabilidade: Supported GPU / Display Paths

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis contextos:

- RGB;
- YCbCr;
- full/limited range quando identificável;
- 4:4:4;
- 4:2:2;
- 4:2:0;
- high refresh;
- HDR;
- TV vs monitor context.

Regra:

Não forçar formato global sem saber o tipo de display e workload.

---

# 46. Clone Mode Capability Limitation Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Multi-Monitor

Primary Product Area: Display

Possible Consumers:

- Presentation
- Gaming
- HDR

Final UI Placement: TBD

Descrição: Detectar limitações impostas quando dois ou mais displays usam modo duplicado/clone.

Possível objetivo: Explicar redução de resolução, refresh ou capability quando displays precisam compartilhar um modo compatível.

Possível benefício: Evitar troubleshooting de hardware quando a limitação é consequência do topology mode.

Aplicabilidade: Clone / Duplicate Display Setups

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis sinais:

- common timing requirement;
- lowest shared refresh;
- resolution compromise;
- HDR availability mismatch;
- scaling behavior;
- target capability difference.

---

# 47. External Monitor DDC/CI Capability Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Monitor Control

Primary Product Area: Display

Possible Consumers:

- Tools
- UX
- Power

Final UI Placement: TBD

Descrição: Detectar se um monitor externo expõe controles via DDC/CI e quais VCP features podem ser acessadas com segurança.

Possível objetivo: Permitir controle centralizado apenas em monitores que realmente suportam o mecanismo.

Possível benefício: Melhor UX para brilho, contraste, volume, input e power sem depender do OSD físico.

Aplicabilidade: DDC/CI-capable External Displays

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Automatic

Possíveis controles:

- brightness;
- contrast;
- volume;
- input source;
- color temperature;
- power state;
- rotation quando exposta;
- custom VCPs.

Safeguard:

Leitura de capability DDC/CI pode ser inconsistente em alguns monitores. Probing deve ser conservador e possuir exclusion/blacklist strategy se necessário.

---

# 48. External Monitor Brightness Control

Status: CANDIDATE

Type: Configuration / Tool

Technical Domain: Monitor Control

Primary Product Area: Tools

Possible Consumers:

- Display
- UX
- Energy

Final UI Placement: TBD

Descrição: Ajustar brilho de monitores externos via DDC/CI quando suportado.

Possível objetivo: Oferecer experiência semelhante ao controle de brilho de painel interno.

Possível benefício: Melhora clara de UX, especialmente em setups multi-monitor.

Aplicabilidade: Supported External Displays

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis recursos:

- per-monitor brightness;
- linked brightness;
- restore previous value;
- profile-based brightness;
- monitor-specific ranges.

Regra:

Não alterar brilho automaticamente por performance.

---

# 49. Monitor Input Source Control

Status: CANDIDATE

Type: Tool / Configuration

Technical Domain: Monitor Control

Primary Product Area: Tools

Possible Consumers:

- Multi-PC
- Docking
- Productivity

Final UI Placement: TBD

Descrição: Alterar input source do monitor via DDC/CI quando suportado.

Possível objetivo: Facilitar alternância entre PCs/consoles/docks.

Possível benefício: Quality-of-life real sem impacto negativo na experiência visual.

Aplicabilidade: Supported External Displays

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

---

# 50. Monitor Firmware Awareness

Status: CANDIDATE

Type: Diagnostic / Manual Guidance

Technical Domain: Monitor Firmware

Primary Product Area: Display

Possible Consumers:

- Repair
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Identificar firmware do monitor quando disponível e considerar update apenas em problemas conhecidos ou funções específicas.

Possível objetivo: Incluir firmware como possível causa de compatibilidade sem transformar update em rotina.

Possível benefício: Resolver bugs reais de HDR, VRR, USB hub, KVM ou input switching em modelos suportados.

Aplicabilidade: Supported Displays

Shared Capability: No

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Manual Guidance

Safeguards:

- exact model required;
- official vendor source only;
- update only when relevant;
- avoid generic firmware recommendations.

---

# 51. Dock / USB-C / USB4 Display Hotplug Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Connectivity

Primary Product Area: Display

Possible Consumers:

- USB / Peripherals
- Docking
- GPU
- Repair

Final UI Placement: TBD

Descrição: Correlacionar display arrival/removal com conexão de docks, USB-C, USB4 ou Thunderbolt.

Possível objetivo: Encontrar causa comum de monitor sumindo ao conectar/desconectar dock.

Possível benefício: Evitar tratar cada display reset como falha independente.

Aplicabilidade: Docked / USB-C Display Systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- upstream dock event;
- USB4/Thunderbolt reconnect;
- DisplayPort Alt Mode path;
- topology rebuild;
- power negotiation;
- display reconnect;
- Ethernet/audio/USB simultaneous changes.

---

# 52. Remote Desktop / Virtual Display Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Virtual Display

Primary Product Area: Shared

Possible Consumers:

- Display
- Remote Access
- Benchmark
- Gaming

Final UI Placement: TBD

Descrição: Detectar quando a sessão usa display virtual, Remote Desktop ou outro output não diretamente ligado a um monitor físico.

Possível objetivo: Evitar interpretar resolução, refresh ou color capability virtual como capacidade física do monitor local.

Possível benefício: Melhor contexto para troubleshooting remoto e benchmarks.

Aplicabilidade: Remote / Virtual Display Sessions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis contextos:

- Remote Desktop;
- virtual display adapter;
- headless virtual display;
- streaming/remote-session display;
- synthetic monitor.

---

# 53. Privacy Screen / Integrated Display Feature Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Integrated Display Features

Primary Product Area: Display

Possible Consumers:

- Laptop
- Privacy
- Accessibility

Final UI Placement: TBD

Descrição: Reconhecer recursos especiais de painéis internos, como privacy screen, quando expostos pelo OEM/Windows.

Possível objetivo: Evitar diagnosticar alteração deliberada de visibilidade/brilho como defeito de painel.

Possível benefício: Melhor experiência em notebooks com recursos de display especializados.

Aplicabilidade: Supported Laptops

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

---

# 54. OLED Display Safeguards

Status: CANDIDATE

Type: Diagnostic / Safeguard

Technical Domain: OLED Display

Primary Product Area: Display

Possible Consumers:

- Personalization
- Energy
- UX

Final UI Placement: TBD

Descrição: Identificar displays OLED e preservar mecanismos que reduzem exposição prolongada a conteúdo estático.

Possível objetivo: Evitar recomendações que aumentem desgaste do painel em troca de estética ou conveniência marginal.

Possível benefício: Melhor longevidade do display sem sacrificar uso normal.

Aplicabilidade: OLED Displays

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis considerações:

- display technology when reliably identifiable;
- idle timeout;
- display sleep;
- screensaver context;
- OEM pixel-shift/panel-care features;
- static-content exposure.

Regra:

Não desativar mecanismos de proteção OLED por performance.

---

# 55. Protected Content / HDCP Context Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Protected Media

Primary Product Area: Display

Possible Consumers:

- Media
- Repair
- Streaming

Final UI Placement: TBD

Descrição: Considerar proteção de conteúdo no diagnóstico de vídeo que funciona em desktop mas falha em streaming protegido.

Possível objetivo: Diferenciar problema de display link/HDCP de falha genérica de GPU ou browser/app.

Possível benefício: Melhor troubleshooting de tela preta, conteúdo bloqueado ou resolução reduzida em serviços protegidos.

Aplicabilidade: Protected Media Workloads

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- protected content only failure;
- connection adapter/converter;
- dock path;
- display capability;
- driver state;
- multi-monitor path.

---

# 56. Refresh Rate Measurement Validation

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Display Timing

Primary Product Area: Benchmark

Possible Consumers:

- Display
- Gaming
- Monitoring

Final UI Placement: TBD

Descrição: Diferenciar refresh configurado pelo Windows de comportamento temporal observado na apresentação.

Possível objetivo: Evitar declarar que "monitor está entregando X Hz" apenas porque o modo está configurado para X Hz.

Possível benefício: Melhor validação de display timing e troubleshooting de presentation issues.

Aplicabilidade: Displays / Benchmark

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis dados:

- configured refresh;
- display timing;
- presentation cadence;
- frame delivery;
- VRR context;
- dropped/repeated frames quando observáveis.

Regra:

Configuração nominal != medição física direta da resposta do painel.

---

# 57. Display Test Flashing Safety

Status: CANDIDATE

Type: Safeguard

Technical Domain: Display Testing Safety

Primary Product Area: Shared

Possible Consumers:

- Stuck Pixel Tool
- Motion Tests
- Accessibility

Final UI Placement: TBD

Descrição: Aplicar safeguards a testes que usam flashes, padrões rápidos ou alto contraste.

Possível objetivo: Reduzir risco de desconforto ou sensibilidade fotossensível.

Possível benefício: Tornar ferramentas visuais mais seguras.

Aplicabilidade: Display Test Tools

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Automatic

Possíveis safeguards:

- explicit warning;
- user-initiated start;
- immediate stop;
- reduced-size test region;
- time limit;
- avoid autoplay;
- no full-screen rapid flashing by default.

---

# 58. Display Recommendation Confidence

Status: CANDIDATE

Type: Diagnostic / Recommendation Infrastructure

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- Display
- Optimization
- Gaming
- Repair

Final UI Placement: TBD

Descrição: Expressar a força da evidência por trás de uma recomendação de display.

Possível objetivo: Diferenciar capability mismatch comprovado de hipótese de cabo, firmware ou monitor.

Possível benefício: Evitar alterações de qualidade visual baseadas em inferência fraca.

Aplicabilidade: Display Diagnostics

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis confidence levels:

- High;
- Medium;
- Low;
- Inconclusive.

Possíveis fatores:

- EDID/capability evidence;
- active path;
- configuration mismatch;
- repeated signal failure;
- driver correlation;
- user visual confirmation;
- benchmark validation;
- inferred cable/path limitation.

---

# Lacunas avaliadas mas NÃO transformadas em novas capabilities

## HDMI version detection

Não criar capability isolada.

A versão "HDMI 2.0/2.1" nem sempre é inferível de forma confiável a partir do Windows e marketing de porta/cabo não corresponde sempre a feature set simples.

Diagnosticar capability efetiva do path é mais importante.

---

## DisplayPort version detection

Mesma decisão.

Preferir recursos efetivamente disponíveis, timings, DSC e path capability a um rótulo de versão simplificado.

---

## Cable quality score

Não criar.

O Windows geralmente não possui telemetria suficiente para dizer "este cabo é ruim" de forma direta.

Pode existir suspeita baseada em:

- signal drops;
- capability fallback;
- link instability;
- reduced modes.

Mas deve permanecer inferência.

---

## Automatic overdrive tuning

REJECTED.

Overdrive é comportamento específico do monitor e normalmente controlado via OSD/DDC/CI vendor-specific.

A escolha depende de refresh e percepção de overshoot/ghosting.

---

## Force RGB Full universally

REJECTED.

TVs, monitors e paths diferentes podem esperar ranges/formatos diferentes.

A decisão deve ser contextual.

---

## Force 10-bit output universally

REJECTED.

Pode reduzir refresh ou exigir bandwidth adicional. Só faz sentido quando display/path/workload suportam e se beneficiam.

---

## OLED brightness reduction as performance optimization

REJECTED.

Brilho/longevidade são questões de UX/energia/panel care, não performance do PC.

---

## Screensaver mandatory for all displays

REJECTED.

Em LCD/LED convencional não é necessário como regra. Display sleep é normalmente mais apropriado para economia de energia.

OLED pode justificar safeguards contextuais.

---

## Disable Advanced Color

REJECTED como optimization.

Pode reduzir fidelidade, HDR/WCG e gerenciamento automático de cores.

---

## Disable Auto Color Management

REJECTED como optimization.

É recurso moderno de fidelidade de cor do Windows.

---

# Resultado do Gap Audit

Capabilities adicionadas nesta passada: **19**

Numeração adicionada:

**40–58**

Total bruto da área Monitor / Display após as duas passadas:

- First Pass: 39 capabilities
- Second Pass: 19 capabilities
- Total bruto atual: **58 capabilities**

Isso NÃO significa 58 features finais.

A consolidação futura deverá reduzir fortemente o catálogo.

---

# Principais grupos que começam a emergir

Sem consolidar formalmente ainda, Monitor / Display parece caminhar para:

1. Display Identity & Capability
2. Display Path & Topology
3. Resolution / Refresh
4. HDR / Advanced Color
5. VRR
6. Multi-Monitor
7. DPI / Scaling
8. Color Management
9. Output Format / Link Capability
10. External Monitor Control
11. USB-C / Dock / Remote Display
12. Panel Quality / OLED Safeguards
13. Stability / Protected Content
14. Visual Test Tools
15. Explain Why / Confidence / Rollback

Não consolidar formalmente agora.

---

# Principais conclusões da Second Pass

1. **Advanced Color e Auto Color Management são capabilities modernas do Windows e devem ser preservadas.**

2. **Perfis ICC mudam de comportamento quando Advanced Color está ativo**, então color management precisa ser diagnosticado com contexto.

3. **Mixed HDR/wide-gamut multi-monitor não pode ser tratado como um único espaço visual.**

4. **DSC pode explicar modos de alta resolução/refresh que não caberiam em uma análise simplista de bandwidth.**

5. **Chroma/pixel format pode afetar qualidade de texto e cor**, mas não deve ser forçado globalmente.

6. **Clone mode pode limitar capacidades ao menor denominador comum.**

7. **DDC/CI permite controles úteis de monitor**, mas probing precisa ser conservador.

8. **Brilho externo e input source são capabilities de UX com valor real.**

9. **Monitor firmware é contextual**, não rotina de manutenção.

10. **Docks/USB-C/USB4 podem explicar mudanças simultâneas de display e outros periféricos.**

11. **Remote/virtual displays precisam ser distinguidos de hardware físico.**

12. **OLED exige safeguards de longevidade**, nunca "desabilitar proteção para ganhar performance".

13. **Protected content failures não são equivalentes a display/GPU failure genérico.**

14. **Refresh configurado não é a mesma coisa que medição física direta do painel.**

15. **Ferramentas com flashing precisam de safeguards de acessibilidade.**

16. **Recommendation confidence deve impedir que suspeita de cabo/path vire certeza.**

17. **A filosofia de UX continua eliminando tweaks que degradam imagem sem benefício convincente.**

18. **Nothing to optimize continua sendo resultado válido.**

---

# Fontes técnicas relevantes desta passada

## Microsoft — Advanced Color / HDR / Auto Color Management

https://learn.microsoft.com/windows/win32/direct3darticles/high-dynamic-range

## Microsoft — ICC Profile Behavior with Advanced Color

https://learn.microsoft.com/windows/win32/wcs/advanced-color-icc-profiles

## Microsoft — Hardware Display Color Calibration Pipeline

https://learn.microsoft.com/windows/win32/wcs/display-calibration-mhc

## Microsoft PowerToys — Power Display / DDC-CI

https://learn.microsoft.com/windows/powertoys/power-display

## Microsoft Support — Screen Saver / OLED Context

https://support.microsoft.com/windows/set-up-a-screen-saver-in-windows

---

# Status final da área

**Monitor / Display — INITIAL DISCOVERY COMPLETE**

Passadas concluídas:

- First Pass — Broad Discovery
- Second Pass — Consolidated Gap Audit