# BeadWise / PC Optimizer V2 — Gaming Discovery

## First Pass — Broad Discovery

Status da área: FIRST PASS COMPLETE

Objetivo desta passada: descobrir capacidades relevantes para jogos no Windows sem duplicar desnecessariamente capabilities já levantadas em Sistema, Energia, CPU, RAM, Storage e GPU.

Princípio desta área:

> Gaming deve funcionar principalmente como consumidor e orquestrador contextual de capabilities compartilhadas.

Fluxo conceitual relevante:

**Detect Game → Understand Session → Measure → Diagnose → Recommend → Apply Contextually → Measure Again → Keep/Rollback**

Regras específicas desta passada:

- não assumir que qualquer configuração é universalmente melhor para jogos;
- distinguir FPS de frame pacing e latência;
- distinguir frames renderizados de frames gerados/interpolados;
- não transformar cada opção de driver ou jogo em uma capability separada;
- preferir configuração por jogo quando o comportamento pode variar entre títulos;
- não duplicar telemetria bruta de CPU, GPU, RAM ou Storage;
- respeitar anti-cheat, segurança e compatibilidade;
- benchmark before/after deve ser preferido para alterações context-dependent.

---

# 1. Game Library Discovery

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Game Discovery

Primary Product Area: Shared

Possible Consumers:

- Optimization
- Benchmark
- Monitoring
- My PC
- Tools

Final UI Placement: TBD

Descrição: Detectar jogos instalados no computador e construir um inventário lógico das bibliotecas disponíveis.

Possível objetivo: Criar a entidade base "Game" para que outras capabilities possam operar por título em vez de tratar qualquer executável como jogo.

Possível benefício: Permitir perfis, monitoramento, benchmark e recomendações específicos por jogo com menos falsos positivos.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- nome do jogo;
- executável principal conhecido ou provável;
- caminho de instalação;
- launcher/store de origem;
- versão quando disponível;
- identificadores da loja quando disponíveis;
- presença de múltiplas instalações;
- localização de configurações quando identificável com segurança;
- associação entre launcher e título.

Observações:

- não depender exclusivamente de catálogo hardcoded;
- diferentes lojas e jogos podem exigir estratégias diferentes de descoberta;
- launchers não devem ser confundidos automaticamente com o processo real do jogo.

---

# 2. Game Process Identification

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Game Runtime

Primary Product Area: Shared

Possible Consumers:

- Monitoring
- Benchmark
- Optimization
- Diagnostics

Final UI Placement: TBD

Descrição: Identificar qual processo ou conjunto de processos representa efetivamente uma sessão de jogo.

Possível objetivo: Evitar aplicar monitoramento, prioridade, perfil ou benchmark ao launcher, updater ou processo auxiliar errado.

Possível benefício: Aumentar a precisão de todas as capabilities per-game.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- processo principal;
- parent/child process relationship;
- launcher;
- bootstrapper;
- anti-cheat process;
- crash reporter;
- helper processes;
- renderer process quando separado;
- mudanças de PID durante inicialização;
- executáveis alternativos DX11/DX12/Vulkan quando existentes.

---

# 3. Game Session Detection

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Game Session

Primary Product Area: Shared

Possible Consumers:

- Optimization
- Monitoring
- Benchmark
- Profiles

Final UI Placement: TBD

Descrição: Detectar início, duração e término de uma sessão real de jogo.

Possível objetivo: Criar um lifecycle confiável para ativar e restaurar comportamentos contextuais.

Possível benefício: Permitir automação somente enquanto o jogo estiver rodando.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis estados:

- game launching;
- game active;
- game foreground;
- game background/minimized;
- game closing;
- game exited;
- launcher remaining after game exit.

Possíveis consumidores:

- iniciar captura de frametime;
- aplicar perfil contextual;
- suspender determinada otimização ao minimizar;
- restaurar configurações ao sair;
- registrar resultado da sessão.

---

# 4. Game Runtime & Graphics API Detection

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Game Runtime

Primary Product Area: Shared

Possible Consumers:

- Monitoring
- Benchmark
- Graphics Diagnostics
- Optimization

Final UI Placement: TBD

Descrição: Identificar o runtime e a API gráfica efetivamente utilizados pelo jogo durante a execução.

Possível objetivo: Contextualizar telemetria, apresentação, compatibilidade de recursos e limitações de medição.

Possível benefício: Evitar interpretações erradas entre jogos que utilizam pipelines diferentes.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- Direct3D 9;
- Direct3D 10;
- Direct3D 11;
- Direct3D 12;
- Vulkan;
- OpenGL;
- DXGI quando aplicável;
- arquitetura x86/x64;
- runtime detectado pela telemetria de apresentação;
- troca de API por parâmetro de inicialização quando observável.

Observação:

Algumas métricas de apresentação podem possuir precisão ou disponibilidade diferente dependendo da API e da instrumentação disponível.

---

# 5. Game Capability Profile

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Game Capability

Primary Product Area: Gaming

Possible Consumers:

- Optimization
- Monitoring
- Benchmark
- Recommendation Engine

Final UI Placement: TBD

Descrição: Construir um perfil consolidado das capacidades relevantes disponíveis para determinado jogo no hardware, driver, display e Windows atuais.

Possível objetivo: Saber quais recomendações realmente podem ser oferecidas naquele contexto.

Possível benefício: Evitar opções incompatíveis, irrelevantes ou impossíveis.

Aplicabilidade: Gaming

Shared Capability: Partially

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis capacidades:

- HDR;
- Auto HDR;
- VRR;
- tearing support;
- frame generation;
- temporal/spatial upscaling;
- NVIDIA Reflex;
- AMD Anti-Lag;
- Intel Xe Low Latency;
- DLSS;
- FSR;
- XeSS;
- DirectStorage;
- per-game Windows graphics options;
- supported display modes;
- hybrid graphics path.

Observação:

A disponibilidade pode depender simultaneamente de jogo, GPU, driver, display, versão do Windows e configuração interna do título.

---

# 6. Windows Gaming Configuration Audit

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Gaming

Primary Product Area: Optimization

Possible Consumers:

- Gaming
- Diagnostics
- Benchmark
- Recommendation Engine

Final UI Placement: TBD

Descrição: Consolidar o estado das principais configurações do Windows que podem afetar a experiência em jogos.

Possível objetivo: Oferecer uma visão única do ambiente gaming do Windows antes de recomendar alterações.

Possível benefício: Detectar configurações incompatíveis, desabilitadas ou potencialmente relevantes sem assumir um preset universal.

Aplicabilidade: Gaming / Specific Windows Version

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis verificações:

- Game Mode;
- Xbox Game Bar;
- Captures/background recording;
- Hardware-Accelerated GPU Scheduling;
- Optimizations for Windowed Games;
- HDR;
- Auto HDR;
- variable refresh rate;
- graphics preference por aplicativo;
- refresh rate ativo;
- múltiplos monitores relevantes.

---

# 7. Game Mode Evaluation

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Gaming

Primary Product Area: Optimization

Possible Consumers:

- Gaming
- Benchmark
- Recommendation Engine

Final UI Placement: TBD

Descrição: Detectar o estado do Game Mode e avaliar se existe motivo para alterá-lo no contexto atual.

Possível objetivo: Tratar Game Mode como mecanismo real do Windows, mas não como tweak universal.

Possível benefício: Permitir recomendação baseada em contexto e comparação antes/depois em sistemas onde houver suspeita de comportamento diferente.

Aplicabilidade: Gaming / Specific Windows Version

Shared Capability: No

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Benchmark First

Possíveis ações:

- detectar estado;
- explicar finalidade;
- recomendar manter;
- testar ON × OFF quando houver motivo;
- restaurar estado anterior.

Não fazer:

- marcar OFF como problema automaticamente;
- afirmar ganho universal de FPS.

---

# 8. Gaming Capture & Recording Impact Diagnostics

Status: CANDIDATE

Type: Diagnostic / Optimization

Technical Domain: Capture & Recording

Primary Product Area: Optimization

Possible Consumers:

- Gaming
- Monitoring
- Benchmark

Final UI Placement: TBD

Descrição: Detectar captura, gravação e instant replay ativos durante jogos e estimar se estão contribuindo para consumo relevante de recursos.

Possível objetivo: Diferenciar gravação desejada pelo usuário de overhead indesejado em segundo plano.

Possível benefício: Encontrar casos reais em que captura interfere em GPU, CPU, memória, encoder ou storage.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis fontes:

- Xbox Game Bar;
- Windows Captures;
- gravação em segundo plano;
- NVIDIA capture/overlay;
- AMD recording;
- OBS;
- Steam recording;
- outros recorders detectáveis.

Possíveis dados:

- processo;
- encoder em uso quando observável;
- uso de GPU Video Encode;
- CPU/GPU overhead;
- escrita em disco;
- gravação ativa;
- replay buffer;
- resolução/bitrate quando acessível.

---

# 9. Overlay & Hook Interference Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Game Compatibility

Primary Product Area: Gaming

Possible Consumers:

- Stability
- Monitoring
- Optimization
- Repair

Final UI Placement: TBD

Descrição: Detectar overlays, injectors, monitoring tools e hooks conhecidos ativos junto ao jogo.

Possível objetivo: Investigar conflitos, crashes, stutter ou incompatibilidades sem assumir que overlays são ruins por definição.

Possível benefício: Facilitar troubleshooting de problemas difíceis causados por interação entre múltiplas ferramentas.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis categorias:

- Steam overlay;
- Discord overlay;
- Xbox Game Bar;
- NVIDIA overlay;
- AMD overlay;
- RTSS;
- MSI Afterburner;
- capture tools;
- RGB/peripheral overlays;
- performance overlays;
- accessibility overlays.

Observação:

A capability deve informar correlação/contexto. A presença de um overlay não prova causalidade.

---

# 10. Per-Game Windows Graphics Configuration

Status: CANDIDATE

Type: Configuration / Optimization

Technical Domain: Windows Graphics

Primary Product Area: Optimization

Possible Consumers:

- Gaming
- GPU
- Display

Final UI Placement: TBD

Descrição: Detectar e gerenciar configurações gráficas do Windows que podem ser definidas por aplicativo.

Possível objetivo: Preferir alterações específicas por jogo em vez de modificar globalmente o comportamento do sistema.

Possível benefício: Reduzir efeitos colaterais em outros aplicativos.

Aplicabilidade: Gaming / Specific Windows Version

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis configurações:

- GPU preference;
- Optimizations for Windowed Games;
- Auto HDR opt-out quando suportado;
- outras graphics preferences expostas pelo Windows.

Observação:

A preferência "High performance GPU" é especialmente relevante em sistemas híbridos, mas deve considerar topologia e objetivo energético.

---

# 11. Game Presentation Path Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Presentation Pipeline

Primary Product Area: Monitoring

Possible Consumers:

- Gaming
- Benchmark
- GPU Diagnostics
- Display Diagnostics

Final UI Placement: TBD

Descrição: Identificar como os frames do jogo são apresentados ao Windows e ao display.

Possível objetivo: Explicar diferenças entre fullscreen, borderless, windowed e diferentes presentation models sem recorrer a mitos antigos sobre "fullscreen sempre mais rápido".

Possível benefício: Diagnosticar latência, tearing, VRR e frame pacing com mais contexto.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- presentation runtime;
- present mode;
- flip model;
- legacy blt model quando observável;
- hardware independent flip;
- composed flip;
- tearing allowed;
- display path;
- dropped/discarded presents quando observável.

---

# 12. Windowed & Borderless Optimization Evaluation

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Presentation Pipeline

Primary Product Area: Optimization

Possible Consumers:

- Gaming
- Benchmark
- Display

Final UI Placement: TBD

Descrição: Avaliar o uso de Optimizations for Windowed Games em jogos compatíveis.

Possível objetivo: Tratar corretamente o mecanismo moderno do Windows que pode migrar jogos DX10/DX11 em windowed/borderless do modelo blt para flip model.

Possível benefício: Possível redução de frame latency e habilitação de recursos modernos de apresentação quando suportados.

Aplicabilidade: Gaming / Windows 11 / Compatible Games

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Benchmark First

Possíveis verificações:

- estado global;
- override por aplicativo;
- API do jogo;
- modo de janela;
- Auto HDR dependency;
- VRR availability;
- presentation mode observado.

Não fazer:

- recomendar desativar "fullscreen optimizations" globalmente;
- assumir que borderless é necessariamente inferior a exclusive fullscreen.

---

# 13. Fullscreen / Borderless / Windowed Mode Comparison

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Presentation Pipeline

Primary Product Area: Benchmark

Possible Consumers:

- Gaming
- Optimization
- Monitoring

Final UI Placement: TBD

Descrição: Comparar modos de apresentação disponíveis no próprio jogo quando houver motivo para investigar comportamento diferente.

Possível objetivo: Determinar empiricamente qual modo oferece melhor combinação de performance, latência, estabilidade e funcionalidades naquele título.

Possível benefício: Substituir recomendações genéricas por medição real.

Aplicabilidade: Gaming / Optional

Shared Capability: No

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Benchmark First

Possíveis comparações:

- exclusive fullscreen;
- borderless fullscreen;
- windowed;
- FPS;
- frametime;
- latency;
- VRR behavior;
- HDR behavior;
- alt-tab behavior;
- frame pacing.

---

# 14. Refresh Rate & Game Output Validation

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Display Gaming

Primary Product Area: Gaming

Possible Consumers:

- Display
- Benchmark
- Monitoring
- Optimization

Final UI Placement: TBD

Descrição: Validar se jogo, Windows e monitor estão operando na taxa de atualização esperada.

Possível objetivo: Encontrar configurações simples que limitam a experiência apesar de hardware capaz.

Possível benefício: Detectar monitor de alta frequência funcionando inadvertidamente em frequência inferior ou jogo limitado por modo de display.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis dados:

- refresh rate do desktop;
- refresh rate solicitado pelo jogo;
- refresh rate ativo;
- resolução;
- display alvo;
- múltiplos monitores;
- fullscreen mode;
- display mode switches.

---

# 15. Variable Refresh Rate Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Display Gaming

Primary Product Area: Gaming

Possible Consumers:

- Display
- GPU
- Benchmark
- Optimization

Final UI Placement: TBD

Descrição: Detectar disponibilidade e contexto de uso de taxa de atualização variável.

Possível objetivo: Identificar se VRR pode operar corretamente com a combinação atual de GPU, display, apresentação e frame rate.

Possível benefício: Melhor diagnóstico de tearing, stutter percebido e configuração de sincronização.

Aplicabilidade: Gaming / Supported Hardware

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- VRR capability;
- G-SYNC/Compatible;
- FreeSync/Adaptive-Sync;
- Windows VRR configuration quando aplicável;
- tearing support;
- refresh range quando disponível;
- present mode;
- VSync relationship;
- FPS cap relationship.

Observação:

Não reduzir a análise a "VRR ON = melhor". O comportamento depende de frame rate, display e política de sincronização.

---

# 16. VSync / Tearing / Frame Cap Strategy

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Frame Delivery

Primary Product Area: Optimization

Possible Consumers:

- Gaming
- Display
- Benchmark
- Latency

Final UI Placement: TBD

Descrição: Analisar conjuntamente VSync, tearing, FPS cap e VRR em vez de tratar cada configuração isoladamente.

Possível objetivo: Explicar trade-offs entre tearing, latência, consumo e estabilidade de frame pacing.

Possível benefício: Permitir recomendações coerentes para diferentes objetivos do usuário.

Aplicabilidade: Gaming

Shared Capability: Partially

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Benchmark First

Possíveis fatores:

- VSync in-game;
- driver VSync;
- tearing allowed;
- FPS limiter;
- refresh rate;
- VRR range;
- frame generation;
- CPU/GPU bound state;
- latency target;
- power/temperature target.

Não fazer:

- impor VSync OFF universal;
- impor cap mágico como regra para todo monitor;
- assumir que "uncapped FPS" é sempre ideal.

---

# 17. FPS Limiter Detection & Coordination

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Frame Rate Control

Primary Product Area: Gaming

Possible Consumers:

- Optimization
- Benchmark
- Monitoring
- Energy

Final UI Placement: TBD

Descrição: Detectar limitadores de FPS ativos e evitar múltiplos limiters conflitantes.

Possível objetivo: Saber quem está controlando a taxa de frames antes de diagnosticar performance.

Possível benefício: Identificar caps inesperados, reduzir confusão e permitir estratégias de frame pacing/energia mais previsíveis.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis fontes:

- limiter interno do jogo;
- driver;
- RTSS;
- launcher/tool externo;
- VSync;
- refresh-dependent caps;
- power-saving features;
- vendor-specific frame limiting.

---

# 18. Game Upscaling Capability & Configuration Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Rendering Technologies

Primary Product Area: Gaming

Possible Consumers:

- Optimization
- Benchmark
- GPU

Final UI Placement: TBD

Descrição: Detectar tecnologias de upscaling/reconstruction suportadas e ajudar a interpretar seu uso.

Possível objetivo: Diferenciar resolução de renderização, resolução de saída e modo de reconstrução ao avaliar desempenho.

Possível benefício: Explicar ganhos de FPS e trade-offs de qualidade sem atribuir toda diferença ao hardware.

Aplicabilidade: Gaming / Supported Games

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis tecnologias:

- NVIDIA DLSS Super Resolution;
- AMD FSR Upscaling;
- Intel XeSS Super Resolution;
- outros métodos temporais;
- spatial upscaling;
- native AA modes;
- dynamic resolution scaling.

Possíveis dados:

- tecnologia ativa;
- quality preset;
- render resolution estimada/conhecida;
- output resolution;
- sharpening quando exposto;
- dynamic resolution;
- capability compatibility.

---

# 19. Frame Generation Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Rendering Technologies

Primary Product Area: Monitoring

Possible Consumers:

- Gaming
- Benchmark
- Latency
- GPU

Final UI Placement: TBD

Descrição: Detectar quando frame generation/interpolation participa da apresentação e distinguir frames produzidos pela aplicação de frames gerados.

Possível objetivo: Evitar métricas enganosas em que "FPS" sozinho mistura trabalho real do jogo com frames sintetizados.

Possível benefício: Tornar comparação de performance e latência mais tecnicamente honesta.

Aplicabilidade: Gaming / Supported Games or Drivers

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- application/rendered FPS;
- presented FPS;
- displayed FPS;
- generated frame type;
- frame generation technology;
- frame generation multiplier quando identificável;
- base frame rate;
- final displayed frame rate.

Regra:

**FPS com Frame Generation != necessariamente maior taxa de frames renderizados pelo jogo.**

---

# 20. Low-Latency Technology Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Gaming Latency

Primary Product Area: Gaming

Possible Consumers:

- Benchmark
- Optimization
- Monitoring

Final UI Placement: TBD

Descrição: Detectar tecnologias de redução de latência específicas do jogo ou fabricante e contextualizar seu uso.

Possível objetivo: Diferenciar mecanismos integrados à engine/driver de tweaks genéricos de scheduler.

Possível benefício: Oferecer recomendações com base em tecnologia realmente suportada.

Aplicabilidade: Gaming / Supported Hardware and Games

Shared Capability: Partially

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis tecnologias:

- NVIDIA Reflex;
- NVIDIA Reflex 2;
- AMD Radeon Anti-Lag / Anti-Lag 2;
- Intel Xe Low Latency;
- driver low-latency mechanisms;
- in-game latency modes.

Observação:

Não assumir equivalência perfeita entre todas as tecnologias nem criar preset universal entre vendors.

---

# 21. Game FPS & Frame Time Monitoring

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Game Performance

Primary Product Area: Monitoring

Possible Consumers:

- Gaming
- Benchmark
- Optimization

Final UI Placement: TBD

Descrição: Medir entrega de frames durante gameplay com foco em FPS e tempo individual entre frames.

Possível objetivo: Criar a base quantitativa para análise real de fluidez.

Possível benefício: Detectar variação que médias de FPS escondem.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis métricas:

- application FPS;
- presented FPS;
- displayed FPS;
- frame time;
- displayed frame time;
- presented frame time;
- average FPS;
- min/max com interpretação cuidadosa;
- percentile-based statistics;
- capture duration;
- frame count.

Observação:

Com frame generation, múltiplas definições de FPS podem divergir e devem ser apresentadas separadamente.

---

# 22. 1% Low / 0.1% Low & Percentile Performance

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Game Performance

Primary Product Area: Benchmark

Possible Consumers:

- Gaming
- Monitoring
- Optimization

Final UI Placement: TBD

Descrição: Resumir a cauda de desempenho da sessão para representar quedas e inconsistências que a média não mostra.

Possível objetivo: Comparar experiência sustentada entre configurações.

Possível benefício: Encontrar regressões que uma média de FPS pode ocultar.

Aplicabilidade: Gaming

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis estatísticas:

- 1% low;
- 0.1% low;
- frame-time percentiles;
- P95/P99/P99.9 frame time;
- worst-frame clusters;
- sample duration;
- warm-up exclusion;
- outlier policy documentada.

Observação:

A metodologia precisa ser consistente entre before/after para evitar comparações inválidas.

---

# 23. Frame Pacing & Stutter Analysis

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Game Performance

Primary Product Area: Monitoring

Possible Consumers:

- Gaming
- Benchmark
- Optimization
- Diagnostics

Final UI Placement: TBD

Descrição: Analisar irregularidade temporal na entrega de frames em vez de apenas contar FPS.

Possível objetivo: Detectar stutter, hitching e pacing inconsistente.

Possível benefício: Diagnosticar situações em que o jogo possui FPS médio alto, mas experiência ruim.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis análises:

- frame-time variance;
- percentile spikes;
- repeated spikes;
- hitch frequency;
- hitch duration;
- long-frame threshold contextual;
- consecutive slow frames;
- pacing distribution;
- displayed-frame irregularity;
- dropped/discarded frames quando observáveis.

Regra:

**High average FPS != smooth gameplay.**

---

# 24. Gaming Latency Measurement

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Gaming Latency

Primary Product Area: Benchmark

Possible Consumers:

- Gaming
- Monitoring
- Optimization
- Input Diagnostics

Final UI Placement: TBD

Descrição: Medir componentes de latência do pipeline de jogo quando a instrumentação disponível permitir.

Possível objetivo: Separar "FPS alto" de "resposta rápida".

Possível benefício: Permitir avaliação de tecnologias low-latency e alterações de sincronização com dados.

Aplicabilidade: Gaming / Capability-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis métricas:

- PC latency;
- GPU latency;
- display latency;
- render-present latency;
- click-to-photon quando disponível;
- all-input-to-photon quando disponível;
- instrumented latency;
- simulation-to-display components;
- present-to-display timing.

Observação:

Disponibilidade e precisão variam com API, hardware, driver e método de instrumentação.

---

# 25. CPU vs GPU Game Bottleneck Classification

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Gaming Bottleneck

Primary Product Area: Gaming

Possible Consumers:

- Optimization
- Benchmark
- Monitoring
- Recommendation Engine

Final UI Placement: TBD

Descrição: Classificar de forma contextual se a sessão parece limitada principalmente por CPU, GPU ou por nenhuma das duas de forma simples.

Possível objetivo: Direcionar recomendações para a área correta.

Possível benefício: Evitar aplicar "GPU optimization" em jogo claramente CPU-bound ou vice-versa.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais consumidos de outras capabilities:

- CPU frame time;
- GPU busy time;
- GPU utilization;
- CPU utilization por thread/core;
- clocks;
- power/thermal limiters;
- frame rate cap;
- VSync;
- frame generation;
- resolution;
- graphics settings;
- frame-time behavior.

Regra:

- GPU abaixo de 100% não prova CPU bottleneck;
- CPU total abaixo de 100% não elimina CPU bottleneck.

---

# 26. Memory Pressure During Gaming Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Gaming Memory

Primary Product Area: Gaming

Possible Consumers:

- RAM
- GPU
- Storage
- Monitoring

Final UI Placement: TBD

Descrição: Correlacionar pressão de RAM e memória gráfica com stutter e comportamento da sessão.

Possível objetivo: Usar capabilities existentes de RAM e GPU memory dentro do contexto temporal do jogo.

Possível benefício: Detectar cenários de paging, residency pressure ou budget pressure sem simplificar para "RAM/VRAM cheia".

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis correlações:

- commit pressure;
- hard faults/paging;
- working set;
- VRAM physical use;
- WDDM budget;
- residency;
- shared GPU memory;
- frame-time spikes;
- asset-loading moments;
- game process memory growth.

---

# 27. Storage-Induced Stutter Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Gaming Storage

Primary Product Area: Gaming

Possible Consumers:

- Storage
- Monitoring
- Benchmark

Final UI Placement: TBD

Descrição: Correlacionar atividade de armazenamento e asset streaming com hitches observados durante gameplay.

Possível objetivo: Identificar quando stutter pode estar ligado a I/O, asset loading ou storage contention.

Possível benefício: Diferenciar gargalo de armazenamento de problema de CPU/GPU.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- game disk reads;
- I/O latency;
- queue behavior;
- throughput;
- storage active time;
- competing processes;
- decompression workload;
- DirectStorage usage quando identificável;
- RAM pressure;
- frame-time spikes sincronizados com I/O.

---

# 28. Shader Compilation & Pipeline Stutter Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Game Runtime

Primary Product Area: Gaming

Possible Consumers:

- Monitoring
- Storage
- GPU
- Repair

Final UI Placement: TBD

Descrição: Identificar padrões compatíveis com shader/pipeline compilation stutter e distinguir isso de problemas persistentes de hardware/configuração.

Possível objetivo: Evitar recomendar tweaks agressivos para um fenômeno que pode ser específico da engine ou de primeira execução.

Possível benefício: Melhorar explicação de stutters que diminuem após cache/aquecimento da sessão.

Aplicabilidade: Gaming / Context-dependent

Shared Capability: Partially

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- spikes recorrentes na primeira visita a cenas/efeitos;
- redução dos spikes após repetição;
- shader cache rebuild recente;
- driver update recente;
- game update recente;
- compilação explícita no startup quando detectável;
- CPU spikes correlacionados;
- storage activity correlacionada.

Não fazer:

- limpar shader cache rotineiramente como "otimização";
- prometer correção universal de shader compilation stutter.

---

# 29. DirectStorage Gaming Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Game Runtime / Storage

Primary Product Area: Gaming

Possible Consumers:

- Storage
- Benchmark
- Monitoring

Final UI Placement: TBD

Descrição: Detectar suporte e, quando tecnicamente possível, uso de DirectStorage no contexto de jogos.

Possível objetivo: Explicar se o caminho moderno de carregamento/streaming pode participar do workload.

Possível benefício: Melhor contextualização de asset streaming e diferenças de storage/CPU.

Aplicabilidade: Gaming / Supported Games and Systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- OS capability;
- storage capability;
- GPU capability quando relevante;
- game support;
- runtime presence;
- decompression path quando observável;
- relationship com storage telemetry.

Não fazer:

- tratar DirectStorage como Registry tweak;
- prometer que "ativá-lo" globalmente melhora qualquer jogo.

---

# 30. Background Workload Impact During Gaming

Status: CANDIDATE

Type: Diagnostic / Optimization

Technical Domain: Game Runtime Environment

Primary Product Area: Optimization

Possible Consumers:

- Processes
- Startup
- System
- Gaming
- Monitoring

Final UI Placement: TBD

Descrição: Detectar workloads de terceiros ou do sistema competindo por recursos durante uma sessão de jogo.

Possível objetivo: Encontrar interferência real em vez de desabilitar processos arbitrariamente.

Possível benefício: Identificar CPU, RAM, storage, GPU ou network contention relevante durante gameplay.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis categorias:

- updates;
- cloud sync;
- browser workloads;
- launchers;
- indexing;
- antivirus scans;
- RGB/peripheral software;
- recording;
- backup;
- game downloads;
- compilation/build workloads;
- virtual machines;
- user applications.

Regra:

Processo em background != processo desnecessário.

A recomendação deve depender de impacto observado e função do processo.

---

# 31. Anti-Cheat & Game Security Compatibility Awareness

Status: CANDIDATE

Type: Diagnostic / Safeguard

Technical Domain: Game Compatibility

Primary Product Area: Shared

Possible Consumers:

- Optimization
- Monitoring
- Profiles
- Benchmark
- Tools

Final UI Placement: TBD

Descrição: Detectar presença de anti-cheat e tratar a sessão como ambiente com restrições adicionais de instrumentação e alteração.

Possível objetivo: Impedir que otimizações, overlays ou técnicas de coleta arrisquem incompatibilidade, bloqueio ou comportamento semelhante a tampering.

Possível benefício: Aumentar segurança do usuário e confiabilidade do produto.

Aplicabilidade: Gaming / Games with Anti-Cheat

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Automatic

Possíveis comportamentos:

- identificar anti-cheat conhecido quando possível;
- evitar injection/hooking invasivo;
- preferir telemetria externa documentada;
- marcar operações incompatíveis;
- não modificar arquivos protegidos;
- não alterar executável do jogo;
- informar limitações de captura;
- respeitar termos e mecanismos de segurança do título.

---

# 32. Game Crash & Hang Diagnostics

Status: CANDIDATE

Type: Diagnostic / Repair Guidance

Technical Domain: Game Stability

Primary Product Area: Gaming

Possible Consumers:

- Repair
- GPU
- System
- Drivers
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Correlacionar crashes, hangs e fechamentos inesperados de jogos com evidências do Windows, driver e hardware.

Possível objetivo: Diferenciar falha do jogo de driver crash, WHEA, memória, storage ou corrupção.

Possível benefício: Produzir troubleshooting mais útil do que simplesmente recomendar reinstalação.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis fontes:

- Application Error;
- Windows Error Reporting;
- Reliability history;
- Event Log;
- GPU TDR;
- driver reset;
- WHEA;
- out-of-memory indicators;
- game crash logs quando acessíveis;
- recent driver/game update;
- dump availability.

---

# 33. Per-Game Optimization Profile

Status: CANDIDATE

Type: Optimization / Configuration

Technical Domain: Game Profiles

Primary Product Area: Optimization

Possible Consumers:

- Gaming
- Energy
- GPU
- Processes
- Display
- Monitoring

Final UI Placement: TBD

Descrição: Armazenar e aplicar um conjunto contextual de preferências para um jogo específico.

Possível objetivo: Orquestrar capabilities já existentes sem transformar cada configuração em uma "gaming tweak" independente.

Possível benefício: Permitir comportamentos diferentes por título e reduzir alterações globais desnecessárias.

Aplicabilidade: Gaming / Optional

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: User Choice

Possíveis componentes:

- power mode/plan contextual;
- GPU preference;
- capture behavior;
- FPS limiter strategy;
- selected background apps;
- display/refresh preference quando seguro;
- vendor profile configuration quando API confiável existir;
- monitoring preset;
- benchmark preset.

Possíveis estados:

**Game starts → Apply profile → Verify → Monitor → Game exits → Restore**

Regra:

Não incluir automaticamente affinity, priority, HAGS ou outras alterações apenas porque tecnicamente podem ser aplicadas.

---

# 34. Per-Game Profile Conflict Detection

Status: CANDIDATE

Type: Diagnostic / Safeguard

Technical Domain: Game Profiles

Primary Product Area: Optimization

Possible Consumers:

- Gaming
- GPU
- Energy
- Display

Final UI Placement: TBD

Descrição: Detectar quando múltiplas camadas tentam configurar o mesmo comportamento para um jogo.

Possível objetivo: Evitar perfis conflitantes entre Windows, driver, jogo e ferramentas externas.

Possível benefício: Reduzir comportamento imprevisível e facilitar troubleshooting.

Aplicabilidade: Gaming / Optional

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Automatic

Possíveis conflitos:

- in-game FPS cap × driver cap × RTSS;
- in-game VSync × driver override;
- Windows GPU preference × vendor preference;
- multiple overlays;
- global vendor profile × per-game profile;
- Game profile × active power policy;
- user manual change while profile is active.

---

# 35. Gaming Before × After Benchmark

Status: CANDIDATE

Type: Benchmark

Technical Domain: Gaming Benchmark

Primary Product Area: Benchmark

Possible Consumers:

- Optimization
- Gaming
- Recommendation Engine

Final UI Placement: TBD

Descrição: Comparar uma sessão controlada antes e depois de determinada mudança relevante.

Possível objetivo: Determinar se a otimização teve efeito mensurável naquele jogo e sistema.

Possível benefício: Evitar manter alterações que não ajudam ou pioram a experiência.

Aplicabilidade: Gaming / Benchmarkable Scenarios

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Benchmark First

Possíveis métricas:

- rendered/application FPS;
- displayed FPS;
- average FPS;
- 1% low;
- 0.1% low;
- frame-time percentiles;
- stutter/hitch rate;
- latency;
- CPU/GPU frame timing;
- power;
- temperature;
- noise proxy quando disponível;
- game stability.

Controle desejável:

- mesma cena/benchmark;
- mesma resolução;
- mesmos settings;
- mesmo FPS cap;
- mesmo FG/upscaling state;
- warm-up comparável;
- similar thermal state;
- background activity registrada.

---

# 36. Gaming Change Rollback

Status: CANDIDATE

Type: Repair / Safeguard

Technical Domain: Game Profiles

Primary Product Area: Shared

Possible Consumers:

- Optimization
- Gaming
- Benchmark
- Settings

Final UI Placement: TBD

Descrição: Registrar e restaurar alterações aplicadas especificamente para jogos.

Possível objetivo: Garantir que testes e perfis não deixem mudanças persistentes inesperadas.

Possível benefício: Tornar experimentation/benchmark seguro e auditável.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis dados:

- game/profile ID;
- setting;
- original value;
- applied value;
- scope global/per-game;
- timestamp;
- session;
- verification result;
- benchmark result;
- restore result.

Possíveis operações:

- undo individual;
- restore profile;
- restore on game exit;
- rollback after failed validation;
- restore original state after benchmark.

---

# 37. Gaming Recommendation Explanation

Status: CANDIDATE

Type: Diagnostic / Quality of Life

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- Optimization
- Gaming
- Benchmark
- Monitoring

Final UI Placement: TBD

Descrição: Explicar por que uma recomendação de gaming foi produzida e quais evidências sustentam a sugestão.

Possível objetivo: Impedir recomendações opacas do tipo "Gaming Boost".

Possível benefício: Aumentar confiança e permitir decisão informada.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possível explicação:

- problema observado;
- evidência;
- capability envolvida;
- condição que torna a recomendação aplicável;
- benefício esperado;
- risco;
- método de validação;
- reversibilidade;
- alternativa "não alterar".

Exemplo conceitual:

> O jogo apresenta pacing irregular enquanto um processo de gravação utiliza o encoder da GPU. A gravação não é um problema por definição; a recomendação aparece porque o impacto coincidiu com os spikes medidos nesta sessão.

---

# 38. Game Optimization Outcome Classification

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Recommendation Validation

Primary Product Area: Optimization

Possible Consumers:

- Gaming
- Benchmark
- History

Final UI Placement: TBD

Descrição: Classificar o resultado de uma alteração ou perfil com base na medição posterior.

Possível objetivo: Permitir que "não melhorou" e "piorou" sejam resultados legítimos.

Possível benefício: Impedir que o produto trate toda alteração aplicada como sucesso.

Aplicabilidade: Gaming / Benchmarkable Changes

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis resultados:

- Improved;
- Neutral / Within Variance;
- Regressed;
- Inconclusive;
- Stability Regression;
- Not Comparable;
- User Preference Only.

Possíveis decisões:

- keep;
- rollback;
- ask user;
- repeat benchmark;
- mark as context-dependent;
- suppress future recommendation for that game/system when apropriado.

---

# Itens propositalmente NÃO separados nesta passada

Os seguintes assuntos foram investigados, mas não viraram capabilities independentes porque são detalhes ou consumidores das capabilities acima:

- cada launcher individual;
- cada overlay individual;
- cada tecnologia DLSS/FSR/XeSS como uma feature diferente;
- cada métrica do PresentMon;
- cada present mode;
- cada tipo de frame;
- cada setting de NVIDIA/AMD/Intel;
- cada FPS limiter;
- cada processo em background;
- cada tipo de anti-cheat;
- CPU priority;
- affinity;
- CPU Sets;
- HAGS;
- GPU power management;
- VRAM telemetry;
- RAM usage;
- disk queue;
- GPU temperature;
- CPU temperature.

Esses últimos já pertencem a áreas compartilhadas e Gaming deve apenas consumi-los no contexto da sessão.

---

# Candidatos fortes para futura consolidação

Após todas as áreas serem concluídas, os 38 itens desta primeira passada provavelmente podem ser agrupados em aproximadamente:

- Game Discovery & Session Context
- Game Capability & Runtime Inventory
- Windows Gaming Configuration
- Presentation & Display Diagnostics
- Synchronization & Frame Rate Control
- Rendering Technology Diagnostics
- Gaming Performance Monitoring
- Stutter & Latency Diagnostics
- Gaming Bottleneck Diagnostics
- Runtime Interference Diagnostics
- Game Stability Diagnostics
- Per-Game Profiles
- Gaming Benchmark & Validation
- Gaming Rollback & Explain Why

Isso é apenas uma previsão para a fase futura de consolidação.

**Não consolidar agora.**

---

# Hipóteses / tópicos para Second Pass — Gap Audit

A segunda passada deverá procurar principalmente lacunas em:

- launcher/store discovery;
- game config/save awareness;
- cloud gaming e remote play;
- multi-GPU / hybrid presentation edge cases;
- multi-monitor gaming;
- HDR gaming interactions;
- refresh switching;
- frame generation measurement edge cases;
- input device latency correlation;
- audio/DPC interference durante jogos;
- network-induced gameplay issues sem duplicar Network;
- online-game telemetry boundaries;
- shader compilation versus asset streaming versus traversal stutter;
- game update / driver update regressions;
- modded games;
- compatibility layers;
- emulators;
- VR gaming;
- accessibility interactions;
- laptops / battery gaming;
- thermal soak durante sessões longas;
- benchmark repeatability;
- benchmark scene detection;
- crash/hang recovery;
- safe interaction with anti-cheat;
- game-specific known-issue knowledge;
- recommendation confidence.

---

# Principais conclusões da First Pass

1. **Gaming é majoritariamente uma camada de contexto.**  
   CPU, GPU, RAM, Storage, Energy e System fornecem sinais e mecanismos; Gaming correlaciona isso com uma sessão e um título.

2. **FPS isolado é insuficiente.**  
   Frametime, pacing, percentiles, latency e stutter são necessários para avaliar experiência.

3. **Frame Generation exige métricas separadas.**  
   Frames renderizados pela aplicação, frames apresentados e frames efetivamente exibidos podem divergir.

4. **Fullscreen exclusivo não deve ser tratado automaticamente como superior.**  
   O Windows moderno possui flip-model e Optimizations for Windowed Games para cenários compatíveis.

5. **VSync, VRR e FPS cap devem ser analisados em conjunto.**

6. **Tecnologias low-latency devem ser capability-aware.**  
   Reflex, Anti-Lag e Xe Low Latency não justificam tweaks genéricos de scheduler.

7. **Background process != problema.**  
   Deve existir impacto medido antes de recomendar fechamento/desativação.

8. **Overlays não são ruins por definição.**  
   São candidatos a investigação quando há correlação com problema.

9. **Anti-cheat deve atuar como safeguard de arquitetura e instrumentação.**

10. **Per-game profile é uma capability de orquestração, não um pacote de tweaks universais.**

11. **Before × After é especialmente importante em Gaming.**

12. **Nothing to optimize é um resultado válido.**

---

# Fontes técnicas de referência desta passada

Prioridade dada a documentação oficial e projetos técnicos primários.

## Microsoft

- Optimizations for windowed games in Windows 11  
  https://support.microsoft.com/en-us/windows/hardware/display-graphics/optimizations-for-windowed-games-in-windows-11

- DXGI flip model  
  https://learn.microsoft.com/windows/win32/direct3ddxgi/dxgi-flip-model

- For best performance, use DXGI flip model  
  https://learn.microsoft.com/windows/win32/direct3ddxgi/for-best-performance--use-dxgi-flip-model

- Variable refresh rate displays  
  https://learn.microsoft.com/windows/win32/direct3ddxgi/variable-refresh-rate-displays

- Auto HDR  
  https://support.microsoft.com/en-us/windows/hardware/display-graphics/use-auto-hdr-for-better-gaming-in-windows

## Intel / PresentMon

- PresentMon  
  https://github.com/GameTechDev/PresentMon

- PresentMon Capture metrics  
  https://github.com/GameTechDev/PresentMon/blob/main/README-CaptureApplication.md

## NVIDIA

- NVIDIA Reflex  
  https://www.nvidia.com/geforce/technologies/reflex/

- NVIDIA DLSS  
  https://developer.nvidia.com/rtx/dlss

## AMD

- AMD FidelityFX Super Resolution  
  https://www.amd.com/en/products/graphics/technologies/fidelityfx/super-resolution.html

## Intel Gaming

- Intel XeSS / Xe Low Latency  
  https://game.intel.com/xess-gaming/

---

# Status final

**Gaming — First Pass: COMPLETE**

Próximo passo da área:

**Second Pass — Consolidated Gap Audit**

# BeadWise / PC Optimizer V2 — Gaming Discovery

## Second Pass — Consolidated Gap Audit

Status da área: SECOND PASS COMPLETE

Base analisada:

- Gaming First Pass;
- 01-DISCOVERY;
- inventário funcional V1 apenas como fonte secundária;
- documentação técnica atual da Microsoft e fabricantes quando necessário.

Objetivo desta passada:

Encontrar perguntas relevantes para Gaming que ainda não eram respondidas pelas capabilities da First Pass.

Regra desta passada:

> adicionar apenas lacunas reais; não reabrir cada métrica, vendor setting ou capability compartilhada como item novo.

As capabilities abaixo continuam a numeração da First Pass.

---

# 39. Game Installation & Runtime Dependency Health

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Game Runtime

Primary Product Area: Repair

Possible Consumers:

- Gaming
- Diagnostics
- Game Library
- Stability

Final UI Placement: TBD

Descrição: Verificar se os componentes de runtime e dependências comuns necessários para executar determinado jogo aparentam estar disponíveis e íntegros.

Possível objetivo: Diferenciar falhas de performance de falhas de runtime/compatibilidade.

Possível benefício: Diagnosticar jogos que não iniciam, fecham imediatamente ou apresentam erros de componentes sem recorrer primeiro a tweaks de desempenho.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis verificações:

- DirectX/runtime requirements;
- Visual C++ runtime dependencies;
- .NET quando aplicável;
- Gaming Services quando aplicável;
- launcher/runtime dependencies;
- missing DLL/runtime symptoms;
- architecture x86/x64;
- required Windows optional components quando conhecidos;
- reboot pending;
- failed runtime installation indicators.

Observação:

Não transformar isso em instalador indiscriminado de runtimes. Reparos automáticos devem depender de método oficial e diagnóstico claro.

---

# 40. Game File Integrity & Repair Orchestration

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Game Installation

Primary Product Area: Repair

Possible Consumers:

- Gaming
- Game Library
- Stability

Final UI Placement: TBD

Descrição: Identificar quando uma falha pode justificar validação de integridade do jogo e orientar ou acionar o mecanismo oficial do launcher quando tecnicamente suportado.

Possível objetivo: Tratar corrupção ou arquivos ausentes como hipótese separada de problemas do Windows ou hardware.

Possível benefício: Evitar reinstalações completas desnecessárias e separar repair de optimization.

Aplicabilidade: Gaming / Supported Launchers

Shared Capability: Partially

Risco inicial: Low

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis integrações:

- Steam verify integrity;
- Xbox/Microsoft Store repair quando suportado;
- Epic verify;
- outros launchers com mecanismo oficial conhecido;
- detection de instalação incompleta;
- update interrompido;
- manifest inconsistency quando exposta com segurança.

Não fazer:

- modificar arquivos do jogo diretamente;
- substituir DLLs por fontes de terceiros;
- tratar qualquer crash como corrupção.

---

# 41. Game Update & Environment Change Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Game Stability

Primary Product Area: Gaming

Possible Consumers:

- Stability
- Drivers
- Windows Update
- Benchmark
- Recommendation Engine

Final UI Placement: TBD

Descrição: Registrar mudanças relevantes no ambiente e correlacioná-las com regressões percebidas em um jogo.

Possível objetivo: Responder à pergunta "o que mudou desde quando este jogo funcionava bem?".

Possível benefício: Melhor diagnóstico de regressões sem atribuí-las automaticamente a uma única causa.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis eventos correlacionáveis:

- game update;
- launcher update;
- GPU driver update;
- chipset/other driver update;
- Windows update;
- BIOS update quando detectável;
- profile/configuration change;
- graphics settings change;
- mod installation/change;
- shader cache rebuild;
- new overlay/monitoring software;
- hardware configuration change.

Regra:

Correlação temporal não prova causalidade.

---

# 42. Game Configuration & Save Awareness

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: Game Data

Primary Product Area: Tools

Possible Consumers:

- Gaming
- Profiles
- Repair
- Backup

Final UI Placement: TBD

Descrição: Identificar arquivos de configuração e save data de jogos quando houver método confiável, sem assumir que todos seguem a mesma estrutura.

Possível objetivo: Permitir backup contextual, comparação de configuração e recuperação antes de alterações relevantes.

Possível benefício: Reduzir risco ao experimentar settings ou reparar instalações.

Aplicabilidade: Gaming / Supported Games

Shared Capability: Partially

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: User Choice

Possíveis dados:

- configuration path;
- save path;
- cloud-save awareness;
- last modification time;
- known graphics config files;
- profile/config backup;
- configuration restore;
- detection de reset de settings após update.

Safeguards:

- não editar formato desconhecido;
- não sobrescrever cloud save automaticamente;
- não assumir que AppData/Documents sempre contém dados descartáveis;
- backup antes de alteração.

---

# 43. Automatic Super Resolution Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Gaming Graphics

Primary Product Area: Gaming

Possible Consumers:

- Optimization
- GPU
- Display
- Benchmark

Final UI Placement: TBD

Descrição: Detectar suporte e configuração do Automatic Super Resolution (Auto SR) do Windows em dispositivos compatíveis.

Possível objetivo: Incorporar ao diagnóstico uma tecnologia de upscaling gerenciada pelo próprio Windows, distinta de DLSS/FSR/XeSS integrados ao jogo.

Possível benefício: Evitar interpretar incorretamente resolução, qualidade ou ganho de FPS quando Auto SR está ativo.

Aplicabilidade: Specific Hardware / Specific Windows Version / Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- device capability;
- Auto SR availability;
- global state;
- per-game state;
- supported game;
- render/output resolution relationship;
- per-game maximum resolution behavior;
- HDR interaction;
- notification/status from Windows.

Observações:

- Auto SR é uma capability do Windows, não um Registry tweak;
- suporte depende de hardware e software específicos;
- documentação atual da Microsoft indica que Auto SR não suporta HDR no mesmo cenário e pode alterar a resolução por jogo.

---

# 44. Multi-Monitor Gaming Context Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Display Gaming

Primary Product Area: Gaming

Possible Consumers:

- Display
- GPU
- Presentation Diagnostics
- Benchmark

Final UI Placement: TBD

Descrição: Entender qual display está sendo usado pelo jogo e quais diferenças entre monitores podem influenciar apresentação e comportamento.

Possível objetivo: Evitar analisar refresh rate, HDR, VRR ou GPU path usando o monitor errado.

Possível benefício: Melhor diagnóstico em setups com dois ou mais displays heterogêneos.

Aplicabilidade: Gaming / Multi-Monitor

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- display onde o jogo está apresentado;
- primary display;
- active resolution;
- active refresh rate;
- HDR capability/state por display;
- VRR capability por display;
- connection path;
- GPU associated with display;
- window migration between displays;
- mixed refresh-rate setup;
- clone/extend state.

---

# 45. Laptop & Handheld Gaming Context

Status: CANDIDATE

Type: Diagnostic / Optimization

Technical Domain: Mobile Gaming

Primary Product Area: Gaming

Possible Consumers:

- Energy
- Battery
- GPU
- Thermal
- Profiles

Final UI Placement: TBD

Descrição: Tratar notebooks e handheld PCs como contexto diferente de desktops durante gaming.

Possível objetivo: Considerar bateria, fonte conectada, limites térmicos e performance-per-watt antes de sugerir ajustes.

Possível benefício: Evitar que "maximum performance" gere mais calor, ruído e consumo com pouco ou nenhum ganho útil.

Aplicabilidade: Laptop / Handheld / Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis sinais:

- AC vs battery;
- battery state;
- battery saver/energy mode;
- charger capability;
- OEM performance mode quando detectável;
- CPU/GPU power limits;
- hybrid graphics;
- display refresh;
- battery drain rate;
- temperature;
- sustained performance;
- performance-per-watt.

Regra:

Maximum power consumption != maximum useful gaming performance.

---

# 46. Sustained Gaming Performance & Thermal Soak Analysis

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Sustained Performance

Primary Product Area: Benchmark

Possible Consumers:

- Gaming
- Thermal
- CPU
- GPU
- Energy

Final UI Placement: TBD

Descrição: Comparar desempenho no início e após período prolongado de carga para detectar regressão sustentada por temperatura, energia ou outros limites.

Possível objetivo: Identificar problemas que benchmarks curtos não revelam.

Possível benefício: Detectar thermal soak, power-limit behavior e perda gradual de clocks/FPS durante sessões longas.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Benchmark First

Possíveis dados consumidos:

- FPS over time;
- frametime over time;
- CPU/GPU clocks;
- CPU/GPU temperature;
- hotspot;
- power;
- throttling/limiter reasons;
- fan behavior quando disponível;
- performance-per-watt;
- session duration.

Possíveis resultados:

- stable;
- thermally constrained;
- power constrained;
- progressive degradation;
- inconclusive.

---

# 47. Gaming Benchmark Repeatability & Run Quality

Status: CANDIDATE

Type: Benchmark / Safeguard

Technical Domain: Benchmark Methodology

Primary Product Area: Benchmark

Possible Consumers:

- Gaming
- Optimization
- Recommendation Engine

Final UI Placement: TBD

Descrição: Avaliar se duas execuções de benchmark são comparáveis antes de calcular melhoria ou regressão.

Possível objetivo: Evitar percentuais before × after baseados em runs inconsistentes.

Possível benefício: Aumentar confiança estatística das decisões automáticas.

Aplicabilidade: Gaming / Benchmarkable Scenarios

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis controles:

- same game build;
- same scene/test;
- same resolution;
- same graphics preset;
- same upscaling state;
- same frame generation state;
- same VSync/FPS cap;
- comparable thermal state;
- comparable background activity;
- comparable session duration;
- adequate sample size;
- warm-up exclusion;
- variance between repeated baseline runs.

Possíveis estados:

- Comparable;
- High Variance;
- Configuration Drift;
- Thermal State Mismatch;
- Scene Mismatch;
- Insufficient Sample;
- Invalid Run.

---

# 48. Benchmark Scene & Workload Consistency

Status: CANDIDATE

Type: Benchmark

Technical Domain: Benchmark Methodology

Primary Product Area: Benchmark

Possible Consumers:

- Gaming
- Optimization

Final UI Placement: TBD

Descrição: Determinar se a carga observada antes e depois representa aproximadamente o mesmo workload.

Possível objetivo: Evitar comparar uma cena leve com uma cena pesada e chamar a diferença de otimização.

Possível benefício: Melhorar validade de benchmark em jogos sem benchmark interno determinístico.

Aplicabilidade: Gaming / Benchmarkable Scenarios

Shared Capability: No

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Automatic

Possíveis estratégias:

- prefer built-in benchmark;
- identify benchmark process/state;
- fixed capture window;
- workload fingerprints;
- GPU/CPU load similarity;
- scene duration;
- frame-count similarity;
- user-assisted route when automation is unreliable.

Observação:

Nem todo jogo permitirá benchmark reproduzível. "Não comparável" deve ser resultado válido.

---

# 49. Online Gaming Network Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Online Gaming

Primary Product Area: Gaming

Possible Consumers:

- Network
- Monitoring
- Diagnostics

Final UI Placement: TBD

Descrição: Correlacionar qualidade da conexão com sintomas percebidos durante jogos online sem duplicar o domínio Network.

Possível objetivo: Distinguir stutter/rendering problems de lag, jitter ou packet loss.

Possível benefício: Direcionar troubleshooting para rede apenas quando os sinais apontarem para isso.

Aplicabilidade: Online Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais consumidos de Network:

- latency;
- jitter;
- packet loss;
- route changes;
- adapter link state;
- Wi-Fi quality;
- retransmission/connection indicators quando disponíveis;
- competing network traffic.

Possíveis correlações:

- gameplay hitch sem frametime spike local;
- disconnect;
- rubber-banding report;
- packet-loss window;
- network saturation.

Regra:

Ping baixo != experiência online necessariamente boa.

---

# 50. Cloud Gaming & Remote Play Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Remote Gaming

Primary Product Area: Gaming

Possible Consumers:

- Network
- Display
- Input
- Monitoring

Final UI Placement: TBD

Descrição: Detectar quando a experiência de jogo é baseada em streaming/remote play e adaptar interpretação de métricas.

Possível objetivo: Evitar aplicar diagnósticos de renderização local como se o jogo estivesse sendo processado pela GPU local.

Possível benefício: Separar problemas de encode/decode, rede e display de gargalos tradicionais CPU/GPU do jogo.

Aplicabilidade: Cloud Gaming / Remote Play

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis contextos:

- cloud gaming client;
- Steam Remote Play;
- Xbox remote/cloud session;
- vendor streaming client;
- local host/client topology quando identificável.

Possíveis métricas relevantes:

- network latency;
- jitter;
- packet loss;
- decode utilization;
- video decode latency;
- stream resolution;
- stream frame rate;
- bitrate;
- display refresh;
- input-to-stream behavior.

---

# 51. VR / XR Gaming Compatibility & Performance Context

Status: CANDIDATE

Type: Diagnostic

Technical Domain: VR / XR Gaming

Primary Product Area: Gaming

Possible Consumers:

- GPU
- Display
- USB
- Performance Monitoring
- Drivers

Final UI Placement: TBD

Descrição: Reconhecer sessões VR/XR como workload com requisitos e pipelines diferentes de jogos em monitor tradicional.

Possível objetivo: Evitar aplicar interpretações convencionais de FPS, display e presentation path a VR.

Possível benefício: Melhor diagnóstico de frame timing, headset connectivity, runtime e compatibilidade.

Aplicabilidade: VR / XR Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- active XR runtime;
- headset detected;
- headset refresh rate;
- render resolution;
- reprojection/motion smoothing state quando exposto;
- dropped/reprojected frames quando disponível;
- GPU/CPU timing;
- USB/connectivity state;
- runtime compatibility;
- SteamVR/OpenXR context.

Observação importante:

Windows Mixed Reality não deve ser assumido como plataforma corrente universal. O WMR foi removido do Windows 11 24H2 e dispositivos legados possuem janela de suporte limitada em versões antigas do Windows.

---

# 52. Emulator & Compatibility-Layer Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Gaming Runtime

Primary Product Area: Gaming

Possible Consumers:

- CPU
- GPU
- Monitoring
- Benchmark
- Compatibility

Final UI Placement: TBD

Descrição: Identificar quando o "jogo" está sendo executado através de emulador ou camada intermediária relevante.

Possível objetivo: Evitar interpretar o processo, API ou gargalo como se fosse um jogo Windows nativo simples.

Possível benefício: Melhor contextualização de CPU bottleneck, shader compilation, frame pacing e múltiplos processos.

Aplicabilidade: Gaming / Emulator / Compatibility Layer

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis contextos:

- console emulators;
- Android gaming environments;
- virtualized game runtime;
- translation/compatibility layer;
- wrapper graphics API;
- launcher-hosted runtime.

Regra:

A presença de emulação não significa problema; apenas muda o modelo de diagnóstico.

---

# 53. Modded Game & Injection Awareness

Status: CANDIDATE

Type: Diagnostic / Safeguard

Technical Domain: Game Compatibility

Primary Product Area: Gaming

Possible Consumers:

- Stability
- Repair
- Anti-Cheat Safeguards
- Benchmark

Final UI Placement: TBD

Descrição: Reconhecer quando mods, wrappers ou injectors conhecidos podem alterar o comportamento do jogo ou invalidar troubleshooting padrão.

Possível objetivo: Evitar atribuir crashes, pacing ou incompatibilidades ao Windows quando o runtime foi alterado.

Possível benefício: Melhorar qualidade do diagnóstico e proteger dados customizados do usuário.

Aplicabilidade: Modded Gaming

Shared Capability: Partially

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- mod loader;
- graphics wrapper;
- custom DLL/plugin;
- ReShade-like injection;
- script extender;
- altered configuration;
- known mod manager process.

Safeguards:

- não apagar mods automaticamente;
- não "reparar" sobrescrevendo instalação customizada sem consentimento;
- avisar antes de file-integrity repair;
- considerar anti-cheat conflicts.

---

# 54. Gaming Audio / Input Interference Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Gaming Latency & Stability

Primary Product Area: Gaming

Possible Consumers:

- Audio
- USB / Peripherals
- DPC Diagnostics
- Monitoring

Final UI Placement: TBD

Descrição: Correlacionar eventos de áudio, input e driver latency com problemas percebidos durante gameplay sem duplicar as capabilities próprias dessas áreas.

Possível objetivo: Encontrar casos em que hitch, crackling ou input anomalies acompanham DPC/ISR ou device behavior.

Possível benefício: Direcionar investigação para driver/dispositivo em vez de aplicar ajustes genéricos de gaming.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais consumidos:

- DPC latency;
- ISR latency;
- driver latency;
- audio glitch/dropout;
- USB disconnect/reconnect;
- HID events;
- polling irregularity quando mensurável;
- frame-time spikes;
- device power transitions.

---

# 55. Gaming Accessibility & Assistive-Technology Safeguards

Status: CANDIDATE

Type: Safeguard / Diagnostic

Technical Domain: Accessibility

Primary Product Area: Shared

Possible Consumers:

- Optimization
- Gaming
- Profiles
- Input
- Display

Final UI Placement: TBD

Descrição: Detectar quando uma alteração de gaming pode interferir em recursos de acessibilidade ou ferramentas assistivas necessárias ao usuário.

Possível objetivo: Impedir que uma "otimização" desative comportamento funcional importante.

Possível benefício: Tornar perfis e recomendações mais seguros e respeitosos com configurações existentes.

Aplicabilidade: Gaming / Accessibility

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Automatic

Possíveis cuidados:

- overlays/accessibility tools;
- input remapping;
- Sticky/Filter/Toggle Keys context;
- controller accessibility devices;
- speech/input tools;
- visual aids;
- color/HDR implications;
- caption/overlay tools;
- background services required by assistive software.

Regra:

Configuração incomum != configuração errada.

---

# 56. Game-Specific Known-Issue Context

Status: CANDIDATE

Type: Diagnostic / Knowledge

Technical Domain: Game Compatibility

Primary Product Area: Gaming

Possible Consumers:

- Repair
- Drivers
- Recommendation Engine
- Benchmark

Final UI Placement: TBD

Descrição: Associar diagnóstico local a problemas conhecidos e documentados para determinado jogo, driver ou versão do Windows.

Possível objetivo: Evitar sugerir alterações locais quando o problema já é conhecido como bug do jogo, driver ou plataforma.

Possível benefício: Reduzir troubleshooting inútil e recomendações potencialmente danosas.

Aplicabilidade: Gaming / Connected Knowledge Required

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis fontes futuras:

- publisher/developer support;
- Microsoft known issues;
- GPU vendor release notes;
- launcher status;
- official game patch notes;
- confirmed incident/status pages.

Safeguard:

Comunidade, fóruns e Reddit podem gerar hipóteses, mas não devem virar "known issue" confirmado sem evidência adequada.

---

# 57. Gaming Recommendation Confidence

Status: CANDIDATE

Type: Diagnostic / Recommendation Infrastructure

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- Optimization
- Gaming
- Benchmark
- Explain Why

Final UI Placement: TBD

Descrição: Expressar o grau de confiança de uma recomendação de gaming com base na qualidade das evidências disponíveis.

Possível objetivo: Diferenciar diagnóstico sólido de hipótese plausível.

Possível benefício: Evitar apresentar correlação fraca como otimização comprovada.

Aplicabilidade: Gaming

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis fatores:

- direct measurement;
- repeatability;
- benchmark quality;
- known capability support;
- correlation strength;
- confounding factors;
- vendor/game documentation;
- hardware applicability;
- stability evidence;
- previous result on same system/game.

Possíveis saídas:

- High Confidence;
- Medium Confidence;
- Low Confidence;
- Experimental;
- Insufficient Evidence.

---

# Lacunas avaliadas mas NÃO transformadas em novas capabilities

## HDR Gaming

Já está suficientemente coberto por:

- Game Capability Profile;
- Windows Gaming Configuration Audit;
- Per-Game Windows Graphics Configuration;
- Multi-Monitor Gaming Context;
- Auto SR Diagnostics.

Não criar uma capability duplicada apenas para "HDR gaming".

## Hybrid Graphics

Já pertence principalmente a GPU/Display e é consumido por:

- Game Capability Profile;
- Per-Game Windows Graphics Configuration;
- Laptop & Handheld Gaming Context.

## Shader cache

Já coberto dentro de Shader Compilation & Pipeline Stutter Diagnostics.

Não criar:

- "Clear Shader Cache";
- "Shader Cache Size";
- "Rebuild Shader Cache"

como features independentes.

## Game process priority / affinity / CPU Sets

Não são gaps de Gaming.

São mecanismos compartilhados de CPU/Scheduler e, caso futuramente usados por game profile, devem passar pelos mesmos evidence gates já definidos.

## Per-game power plan

Não criar como capability independente.

É um possível componente de Per-Game Optimization Profile consumindo Energy.

## Network tweaks para jogos

Não criar "Gaming TCP Optimization", "Nagle Disable", "Gaming DNS" ou similares nesta área.

Network fará discovery próprio.

Gaming precisa apenas correlacionar Network com a sessão.

## Mouse polling / USB tweaks

Também não são capabilities novas de Gaming.

Pertencem a USB/Input e Gaming apenas consome o diagnóstico contextual.

## MPO / HAGS / ReBAR

Já foram tratados em GPU/Windows Graphics.

Não duplicar aqui.

---

# Resultado do Gap Audit

Capabilities adicionadas nesta passada: **19**

Numeração adicionada:

**39–57**

Total bruto da área Gaming após as duas passadas:

- First Pass: 38 capabilities
- Second Pass: 19 capabilities
- Total bruto atual: **57 capabilities**

Isso NÃO significa 57 features finais.

A expectativa continua sendo consolidar fortemente depois que todas as áreas principais terminarem.

---

# Principais grupos que começam a emergir

Sem consolidar formalmente ainda, Gaming parece caminhar para algo próximo de:

1. Game Discovery & Identity
2. Session & Runtime Context
3. Game Capability Inventory
4. Windows Gaming Configuration
5. Presentation & Display
6. Frame Rate / Synchronization
7. Rendering Technologies
8. Performance Monitoring
9. Stutter & Latency
10. Bottleneck Correlation
11. Runtime Interference
12. Game Stability & Repair
13. Game Profiles
14. Gaming Benchmark
15. Mobile / Sustained Gaming
16. Online / Remote Gaming
17. VR / Special Runtime Contexts
18. Safeguards & Compatibility
19. Recommendation Evidence

Esses grupos são apenas sinalização para a futura fase de consolidação.

---

# Principais conclusões da Second Pass

1. **Auto SR adiciona uma camada nova de upscaling gerenciada pelo Windows** e precisa ser distinguido de DLSS/FSR/XeSS.

2. **Benchmark repeatability é uma capability própria**, não mero detalhe da tela de benchmark.

3. **Uma melhora before × after só é válida se os runs forem comparáveis.**

4. **Gaming em notebook/handheld precisa considerar performance-per-watt e sustained performance**, não somente peak FPS.

5. **Thermal soak pode revelar regressões que benchmarks curtos escondem.**

6. **Online gaming precisa correlacionar Network sem duplicar Network.**

7. **Cloud gaming/Remote Play muda completamente o modelo de gargalo.**

8. **VR/XR precisa de contexto específico de runtime e frame delivery.**

9. **Windows Mixed Reality é legado/removido nas versões modernas do Windows e não deve ser tratado como base genérica de VR.**

10. **Mods e injectors precisam de awareness, não de remoção automática.**

11. **Game repair deve preferir mecanismos oficiais de verificação do launcher.**

12. **Game update, driver update e Windows update devem participar de regression correlation.**

13. **Accessibility é safeguard de gaming profile.**

14. **Known issues podem impedir que o produto prescreva tweaks para bugs já documentados.**

15. **Recommendation Confidence deve impedir que hipótese fraca apareça como certeza.**

---

# Fontes técnicas relevantes desta passada

## Microsoft — Automatic Super Resolution

https://support.microsoft.com/en-us/windows/ai/ai-features/automatic-super-resolution

## Microsoft — Windows 11 Feature Requirements / DirectStorage

https://learn.microsoft.com/en-us/windows/whats-new/windows-11-requirements

## Microsoft — Optimizations for Windowed Games

https://support.microsoft.com/en-us/windows/hardware/display-graphics/optimizations-for-windowed-games-in-windows-11

## Microsoft — Windows Removed Features / Windows Mixed Reality

https://learn.microsoft.com/en-us/windows/whats-new/removed-features

## Microsoft — Windows Deprecated Features

https://learn.microsoft.com/en-us/windows/whats-new/deprecated-features

---

# Status final da área

**Gaming — INITIAL DISCOVERY COMPLETE**

Passadas concluídas:

- First Pass — Broad Discovery
- Second Pass — Consolidated Gap Audit
