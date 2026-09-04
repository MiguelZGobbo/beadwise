# Discovery Catalog — GPU
## Primeira passada — levantamento amplo

> Área dedicada ao hardware gráfico, drivers, recursos do Windows, memória gráfica, telemetria, desempenho, estabilidade e configurações específicas de fabricante.
>
> Regra central:
>
> **GPU detection ≠ GPU tuning.**
>
> O BeadWise deve primeiro entender:
>
> - qual GPU existe;
> - qual está sendo usada;
> - qual driver/WDDM está ativo;
> - quais recursos são suportados;
> - quais limites estão ativos;
> - se existe gargalo real;
> - se há erro/instabilidade;
>
> e somente depois considerar qualquer alteração.
>
> Outra regra:
>
> **Configuração disponível no driver não significa otimização universal.**

---

# 1. GPU Adapter Inventory

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Hardware

Primary Product Area:
My PC

Possible Consumers:
- Optimization
- Benchmark
- Monitoring
- Gaming

Final UI Placement:
TBD

Descrição:
Inventariar todos os adaptadores gráficos presentes no sistema.

Possíveis tipos:
- integrated GPU;
- discrete GPU;
- external GPU;
- software adapter;
- remote/virtual adapter.

Possível objetivo:
Criar mapa confiável da arquitetura gráfica.

Evidence Level:
Established

Action Mode:
Automatic

---

# 2. GPU Vendor Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Hardware

Primary Product Area:
Shared

Descrição:
Identificar fabricante do adaptador.

Possíveis vendors:
- NVIDIA;
- AMD;
- Intel;
- Microsoft/software;
- outros.

Possível objetivo:
Selecionar capabilities e telemetria específicas.

Action Mode:
Automatic

---

# 3. GPU Hardware ID Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Hardware

Primary Product Area:
Shared

Descrição:
Obter hardware/vendor/device/subsystem IDs.

Possível objetivo:
Identificação precisa do modelo e board partner quando possível.

Action Mode:
Automatic

---

# 4. GPU Model Identification

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Hardware

Primary Product Area:
My PC

Descrição:
Mostrar nome comercial do adaptador.

Possível benefício:
CPU-Z/GPU-Z-like hardware information.

Action Mode:
Automatic

---

# 5. GPU Architecture Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
GPU Hardware

Primary Product Area:
My PC

Descrição:
Identificar arquitetura/microarquitetura quando possível.

Exemplos:
- Ada;
- Blackwell;
- RDNA;
- Xe;
- outras.

Possível objetivo:
Dar contexto a recursos e performance.

Evidence Level:
Vendor/model database dependent

Action Mode:
Diagnostic

---

# 6. GPU Integrated / Discrete Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Hardware

Primary Product Area:
Shared

Descrição:
Classificar adaptador como integrado ou discreto.

Possível objetivo:
Base para hybrid graphics.

Evidence Level:
Established

Action Mode:
Automatic

---

# 7. Detachable / External GPU Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Hardware

Primary Product Area:
My PC

Descrição:
Detectar adaptadores destacáveis/externos.

Possível objetivo:
Suportar eGPU e dock scenarios.

Action Mode:
Automatic

---

# 8. Physical GPU Count

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Topology

Primary Product Area:
My PC

Descrição:
Distinguir adaptador lógico de adaptadores físicos agrupados quando aplicável.

Action Mode:
Automatic

---

# 9. GPU Engine Enumeration

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Engines

Primary Product Area:
Monitoring

Descrição:
Enumerar engines expostos pelo adaptador.

Possíveis engines:
- 3D;
- compute;
- copy;
- video decode;
- video encode;
- outros.

Possível objetivo:
Evitar um único percentual genérico de GPU.

Action Mode:
Automatic

---

# 10. Dedicated VRAM Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Memory

Primary Product Area:
My PC

Descrição:
Detectar memória dedicada do adaptador.

Action Mode:
Automatic

---

# 11. Dedicated System Graphics Memory Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Memory

Primary Product Area:
My PC

Descrição:
Detectar memória do sistema reservada exclusivamente ao adaptador quando aplicável.

Action Mode:
Automatic

---

# 12. Shared System Graphics Memory Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Memory

Primary Product Area:
My PC

Descrição:
Detectar limite de memória do sistema compartilhável com GPU.

Possível objetivo:
Explicar comportamento de iGPU e spillover de VRAM.

Action Mode:
Automatic

---

# 13. VRAM Usage Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Memory

Primary Product Area:
Monitoring

Possible Consumers:
- Gaming
- Benchmark

Descrição:
Monitorar uso de memória gráfica dedicada.

Action Mode:
Monitoring

---

# 14. Shared GPU Memory Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Memory

Primary Product Area:
Monitoring

Descrição:
Monitorar uso de memória do sistema pela GPU.

Possível objetivo:
Detectar pressure/spillover.

Action Mode:
Monitoring

---

# 15. VRAM Pressure Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Memory

Primary Product Area:
Gaming

Descrição:
Detectar workload se aproximando ou excedendo orçamento útil de memória gráfica.

Possível objetivo:
Diagnosticar stutter, streaming e quedas de performance.

Evidence Level:
Context-dependent

Action Mode:
Diagnostic

---

# 16. VRAM Spillover Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Memory

Primary Product Area:
Gaming

Descrição:
Detectar aumento relevante de memória compartilhada quando VRAM está pressionada.

Possível objetivo:
Dar contexto a performance degradada.

Action Mode:
Diagnostic

---

# 17. Per-Process GPU Memory Usage

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Memory / Processes

Primary Product Area:
Monitoring

Descrição:
Relacionar VRAM/shared memory a processos.

Action Mode:
Monitoring

---

# 18. GPU Utilization Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Performance

Primary Product Area:
Monitoring

Descrição:
Monitorar utilização global e/ou engines relevantes.

Action Mode:
Monitoring

---

# 19. Per-Engine GPU Utilization

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Engines

Primary Product Area:
Monitoring

Descrição:
Separar 3D, compute, copy, video encode/decode etc.

Possível objetivo:
Explicar porque “GPU 100%” pode significar coisas diferentes.

Action Mode:
Monitoring

---

# 20. GPU Usage by Process

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU / Processes

Primary Product Area:
Monitoring

Descrição:
Mostrar quais processos utilizam engines da GPU.

Action Mode:
Monitoring

---

# 21. GPU Clock Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Sensors

Primary Product Area:
Monitoring

Descrição:
Monitorar core/graphics clock quando disponível.

Action Mode:
Monitoring

---

# 22. GPU Memory Clock Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Sensors

Primary Product Area:
Monitoring

Descrição:
Monitorar clock da memória gráfica.

Action Mode:
Monitoring

---

# 23. Effective GPU Clock Analysis

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
GPU Performance

Primary Product Area:
GPU

Descrição:
Investigar métricas que diferenciem clock reportado de desempenho efetivamente sustentado.

Possível objetivo:
Detectar throttling ou clock behavior anormal.

Action Mode:
Diagnostic

---

# 24. GPU Temperature Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Thermal

Primary Product Area:
Monitoring

Descrição:
Monitorar temperatura principal da GPU.

Action Mode:
Monitoring

---

# 25. GPU Hotspot Temperature

Status:
RESEARCHING

Type:
Monitoring

Technical Domain:
GPU Thermal

Primary Product Area:
GPU

Descrição:
Monitorar hotspot/junction quando fabricante expuser.

Possível objetivo:
Dar contexto térmico mais profundo que temperatura média.

Action Mode:
Monitoring

---

# 26. GPU Memory Temperature

Status:
RESEARCHING

Type:
Monitoring

Technical Domain:
VRAM Thermal

Primary Product Area:
GPU

Descrição:
Monitorar temperatura da memória quando sensor existir.

Action Mode:
Monitoring

---

# 27. GPU Fan Speed Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Cooling

Primary Product Area:
Monitoring

Descrição:
Monitorar RPM/percentual quando disponível.

Action Mode:
Monitoring

---

# 28. GPU Fan Stop State

Status:
IDEA

Type:
Diagnostic

Technical Domain:
GPU Cooling

Primary Product Area:
GPU

Descrição:
Reconhecer zero-RPM/fan-stop legítimo.

Possível objetivo:
Não alertar erroneamente que fan parado significa defeito.

Action Mode:
Diagnostic

---

# 29. GPU Board Power Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Power

Primary Product Area:
Monitoring

Descrição:
Monitorar consumo total/estimado da placa quando disponível.

Action Mode:
Monitoring

---

# 30. GPU Voltage Monitoring

Status:
RESEARCHING

Type:
Monitoring

Technical Domain:
GPU Power

Primary Product Area:
Advanced GPU

Descrição:
Monitorar tensão quando API/vendor permitir.

Action Mode:
Diagnostic

---

# 31. GPU Power Limit Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Power

Primary Product Area:
GPU

Descrição:
Detectar limite de potência configurado e/ou atuação do limitador.

Possível objetivo:
Explicar clocks/performance.

Action Mode:
Diagnostic

---

# 32. GPU Thermal Limit Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Thermal

Primary Product Area:
GPU

Descrição:
Detectar condição em que temperatura limita frequência/performance.

Action Mode:
Diagnostic

---

# 33. GPU Voltage / Reliability Limiter Context

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
GPU Performance

Primary Product Area:
GPU

Descrição:
Interpretar motivos de limitação expostos por APIs específicas do fabricante.

Possível objetivo:
Explicar por que GPU não aumenta clock apesar de utilização elevada.

Action Mode:
Diagnostic

---

# 34. GPU Performance Limiter Classification

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Performance

Primary Product Area:
GPU

Descrição:
Combinar sinais disponíveis para classificar provável limite ativo.

Possíveis resultados:
- GPU utilization limited;
- CPU limited;
- power limited;
- thermal limited;
- VRAM limited;
- voltage/reliability limited;
- frame-cap limited;
- unknown.

Action Mode:
Diagnostic

---

# 35. GPU Driver Version Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Drivers

Primary Product Area:
My PC

Descrição:
Detectar versão do driver gráfico.

Action Mode:
Automatic

---

# 36. GPU Driver Provider Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Drivers

Primary Product Area:
My PC

Descrição:
Identificar provider/vendor do driver.

Action Mode:
Automatic

---

# 37. WDDM Version Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
WDDM

Primary Product Area:
Shared

Descrição:
Detectar versão do Windows Display Driver Model.

Possível objetivo:
Determinar disponibilidade de capabilities modernas.

Action Mode:
Automatic

---

# 38. Graphics Driver Model Capability Check

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
WDDM

Primary Product Area:
GPU

Descrição:
Determinar features condicionais à versão WDDM.

Action Mode:
Automatic

---

# 39. GPU Driver Age Context

Status:
IDEA

Type:
Diagnostic

Technical Domain:
GPU Drivers

Primary Product Area:
Drivers

Descrição:
Mostrar idade aproximada do driver sem assumir automaticamente que mais novo = melhor.

Action Mode:
Diagnostic

---

# 40. GPU Driver Update Recommendation

Status:
CANDIDATE

Type:
Recommendation

Technical Domain:
GPU Drivers

Primary Product Area:
Drivers

Descrição:
Recomendar update quando houver versão problemática, requisito de jogo ou correção relevante.

Evidence Level:
Context-dependent

Action Mode:
Manual Guidance

---

# 41. GPU Driver Regression Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
GPU Drivers

Primary Product Area:
Drivers

Descrição:
Relacionar queda de performance/crashes a mudança de driver.

Possível objetivo:
Detectar regressões antes/depois.

Action Mode:
Diagnostic

---

# 42. GPU Driver Clean Install Advisor

Status:
RESEARCHING

Type:
Repair / Guidance

Technical Domain:
GPU Drivers

Primary Product Area:
Repair

Descrição:
Recomendar reinstalação limpa somente quando houver evidência de driver corrompido/conflito.

Action Mode:
Problem-triggered

Importante:
Não transformar DDU/reinstall em ritual de otimização periódica.

---

# 43. Display Driver Device Error Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Drivers

Primary Product Area:
Repair

Descrição:
Detectar problemas PnP/device state relacionados à GPU.

Action Mode:
Diagnostic

---

# 44. Graphics Driver Crash History

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Stability

Primary Product Area:
Repair

Descrição:
Consultar histórico de crashes/resets relevantes.

Action Mode:
Diagnostic

---

# 45. TDR Event Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
WDDM / Stability

Primary Product Area:
GPU

Descrição:
Detectar Timeout Detection and Recovery.

Possível objetivo:
Identificar driver/GPU que deixou de responder e foi reinicializado pelo Windows.

Action Mode:
Diagnostic

---

# 46. TDR Frequency Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Stability

Primary Product Area:
Repair

Descrição:
Distinguir evento isolado de TDR recorrente.

Action Mode:
Diagnostic

---

# 47. TDR Context Correlation

Status:
IDEA

Type:
Diagnostic

Technical Domain:
GPU Stability

Primary Product Area:
GPU

Descrição:
Relacionar TDR com:
- jogo;
- driver;
- temperatura;
- clock;
- power;
- overclock;
- WHEA.

Action Mode:
Diagnostic Only

---

# 48. TdrDelay Tweak Guard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
GPU Stability

Primary Product Area:
Shared

Descrição:
Impedir que aumentar `TdrDelay` seja apresentado como correção universal para GPU crash.

Possível objetivo:
Não mascarar hardware/driver que está deixando de responder.

Action Mode:
Internal

---

# 49. GPU Crash Dump / Live Dump Awareness

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
GPU Drivers

Primary Product Area:
Tools

Descrição:
Investigar dumps/telemetria disponibilizados pelo Windows para debugging de GPU.

Action Mode:
Advanced Diagnostic

---

# 50. DirectX Runtime Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Graphics APIs

Primary Product Area:
My PC

Descrição:
Detectar capacidades relevantes de DirectX.

Action Mode:
Automatic

---

# 51. Direct3D Feature Level Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Direct3D

Primary Product Area:
My PC

Descrição:
Detectar feature levels suportados.

Possível objetivo:
Compatibilidade de jogos/apps.

Action Mode:
Automatic

---

# 52. DirectX 12 Capability

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Direct3D

Primary Product Area:
My PC

Descrição:
Identificar suporte apropriado a D3D12.

Action Mode:
Automatic

---

# 53. Shader Model Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Direct3D

Primary Product Area:
My PC

Descrição:
Detectar Shader Model relevante.

Possível objetivo:
Compatibilidade com recursos modernos.

Action Mode:
Automatic

---

# 54. DirectX Ultimate Feature Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Direct3D

Primary Product Area:
My PC

Descrição:
Investigar capabilities como:
- ray tracing;
- mesh shaders;
- sampler feedback;
- variable rate shading.

Action Mode:
Diagnostic

---

# 55. Vulkan Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Graphics APIs

Primary Product Area:
My PC

Descrição:
Detectar runtime/versão/capabilities Vulkan quando presentes.

Action Mode:
Diagnostic

---

# 56. OpenGL Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Graphics APIs

Primary Product Area:
My PC

Descrição:
Detectar versão/capabilities OpenGL expostas pelo driver.

Action Mode:
Diagnostic

---

# 57. Graphics Runtime Health

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Graphics Runtime

Primary Product Area:
Repair

Descrição:
Consolidar estado de DirectX/Vulkan/OpenGL/driver para troubleshooting.

Action Mode:
Diagnostic

---

# 58. Hardware-Accelerated GPU Scheduling Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Windows Graphics

Primary Product Area:
GPU

Descrição:
Detectar disponibilidade e estado do HAGS.

Action Mode:
Automatic

---

# 59. HAGS Performance Test

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
Windows Graphics

Primary Product Area:
Benchmark

Descrição:
Comparar workload com HAGS ON/OFF quando a plataforma realmente permitir teste controlado.

Possível objetivo:
Determinar impacto específico naquela máquina/jogo.

Evidence Level:
Context-dependent

Action Mode:
Benchmark First

---

# 60. HAGS Universal Optimization Guard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
Windows Graphics

Primary Product Area:
Shared

Descrição:
Não assumir HAGS ON ou OFF como configuração universalmente superior.

Action Mode:
Internal

---

# 61. Per-App GPU Preference Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Windows Graphics

Primary Product Area:
Gaming

Descrição:
Detectar preferência gráfica de um aplicativo quando disponível.

Possíveis estados:
- system/default;
- minimum power;
- high performance.

Action Mode:
Diagnostic

---

# 62. Per-App GPU Preference Configuration

Status:
CANDIDATE

Type:
Configuration

Technical Domain:
Windows Graphics

Primary Product Area:
Gaming Profiles

Descrição:
Permitir selecionar GPU preferida por aplicativo em sistemas multi-GPU.

Action Mode:
User Choice

---

# 63. Wrong-GPU Execution Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Hybrid Graphics

Primary Product Area:
Gaming

Descrição:
Detectar jogo/workload pesado executando na GPU inadequada.

Possível benefício:
Pode explicar enorme perda de performance.

Action Mode:
Diagnostic

---

# 64. Hybrid Graphics Topology

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Hybrid Graphics

Primary Product Area:
My PC

Descrição:
Detectar combinação iGPU + dGPU.

Action Mode:
Automatic

---

# 65. Hybrid Graphics Runtime Usage

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
Hybrid Graphics

Primary Product Area:
Monitoring

Descrição:
Mostrar qual GPU está efetivamente processando cada workload.

Action Mode:
Monitoring

---

# 66. dGPU Idle Residency Diagnosis

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Hybrid Graphics / Energy

Primary Product Area:
GPU

Descrição:
Detectar dGPU permanecendo ativa sem workload relevante.

Possível benefício:
Especialmente importante para notebooks.

Action Mode:
Diagnostic

---

# 67. External Display GPU Routing Context

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Hybrid Graphics

Primary Product Area:
GPU

Descrição:
Reconhecer que monitor externo pode estar fisicamente conectado a GPU específica.

Possível objetivo:
Explicar dGPU ativa ou comportamento de performance.

Action Mode:
Diagnostic

---

# 68. MUX Capability Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
Hybrid Graphics / Laptop

Primary Product Area:
GPU

Descrição:
Detectar presença de hardware MUX quando possível.

Possível objetivo:
Explicar modos hybrid/discrete-only.

Action Mode:
Diagnostic

---

# 69. MUX Mode Advisor

Status:
IDEA

Type:
Recommendation

Technical Domain:
Hybrid Graphics

Primary Product Area:
Gaming

Descrição:
Orientar mudança de modo somente quando hardware/OEM suportar e objetivo justificar.

Possível benefício:
Performance × autonomia trade-off.

Risco inicial:
Medium

Action Mode:
Manual Guidance

---

# 70. Resizable BAR Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
PCIe / GPU

Primary Product Area:
GPU

Possible Consumers:
- BIOS
- Gaming

Descrição:
Detectar suporte de GPU/plataforma a Resizable BAR.

Action Mode:
Automatic

---

# 71. Resizable BAR Active-State Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
PCIe / GPU

Primary Product Area:
GPU

Descrição:
Determinar se ReBAR está realmente ativo.

Action Mode:
Automatic

---

# 72. Resizable BAR Platform Readiness

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
PCIe / BIOS

Primary Product Area:
GPU

Descrição:
Verificar pré-requisitos como:
- GPU;
- CPU;
- motherboard/firmware;
- UEFI;
- Above 4G Decoding;
- driver.

Action Mode:
Diagnostic

---

# 73. Resizable BAR Manual Guidance

Status:
CANDIDATE

Type:
Configuration Guidance

Technical Domain:
BIOS / GPU

Primary Product Area:
Optimization

Descrição:
Orientar ativação quando plataforma suporta e existe razão para fazê-lo.

Action Mode:
Manual Guidance

---

# 74. Intel Arc ReBAR Requirement Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Intel GPU

Primary Product Area:
GPU

Descrição:
Dar tratamento especial a Intel Arc quando ReBAR estiver ausente.

Possível objetivo:
Alertar que Arc pode funcionar, mas desempenho ótimo depende fortemente de ReBAR.

Action Mode:
Diagnostic / Recommendation

---

# 75. ReBAR Benefit Context

Status:
CANDIDATE

Type:
Explainability

Technical Domain:
GPU / PCIe

Primary Product Area:
GPU

Descrição:
Explicar que ReBAR pode melhorar desempenho, mas benefício depende de GPU/jogo/plataforma.

Action Mode:
Automatic Explanation

---

# 76. PCIe Link Generation Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU / PCIe

Primary Product Area:
GPU

Descrição:
Detectar geração do link utilizado pela GPU quando possível.

Action Mode:
Diagnostic

---

# 77. PCIe Link Width Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU / PCIe

Primary Product Area:
GPU

Descrição:
Detectar x1/x4/x8/x16 etc.

Action Mode:
Diagnostic

---

# 78. GPU PCIe Link Bottleneck Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU / PCIe

Primary Product Area:
GPU

Descrição:
Comparar capacidade da placa e link negociado.

Possível objetivo:
Encontrar GPU instalada em slot/link inadequado.

Action Mode:
Diagnostic

---

# 79. PCIe Link Dynamic-State Awareness

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU / PCIe

Primary Product Area:
GPU

Descrição:
Não interpretar link reduzido em idle como problema sem verificar estado sob carga.

Action Mode:
Internal Safeguard

---

# 80. GPU PCIe Error Correlation

Status:
IDEA

Type:
Diagnostic

Technical Domain:
GPU / PCIe / WHEA

Primary Product Area:
GPU

Descrição:
Relacionar WHEA/PCIe errors ao adaptador gráfico.

Action Mode:
Diagnostic

---

# 81. GPU Bottleneck Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Performance

Primary Product Area:
Gaming

Descrição:
Determinar se GPU é provável limitador do workload.

Possíveis evidências:
- sustained 3D utilization;
- CPU behavior;
- frame rate;
- frametime;
- power/thermal state;
- frame cap.

Action Mode:
Diagnostic

---

# 82. CPU-Limited GPU Underutilization Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / GPU

Primary Product Area:
Gaming

Descrição:
Detectar GPU abaixo de utilização potencial porque CPU/main thread limita frames.

Action Mode:
Diagnostic

---

# 83. Frame Cap / VSync Limited Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
GPU / Gaming

Primary Product Area:
Gaming

Descrição:
Evitar classificar GPU abaixo de 100% como problema quando FPS está intencionalmente limitado.

Action Mode:
Diagnostic

---

# 84. GPU Performance Headroom

Status:
IDEA

Type:
Diagnostic

Technical Domain:
GPU Performance

Primary Product Area:
Gaming

Descrição:
Estimar se existe capacidade não utilizada por causa de outro limitador.

Action Mode:
Diagnostic

---

# 85. GPU Frametime Correlation

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU / Gaming

Primary Product Area:
Monitoring

Descrição:
Relacionar frametime spikes a:
- utilization;
- clocks;
- thermal;
- power;
- VRAM;
- driver events.

Action Mode:
Diagnostic

---

# 86. VRAM Pressure / Frametime Correlation

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Memory / Gaming

Primary Product Area:
Gaming

Descrição:
Investigar stutters coincidindo com VRAM pressure/shared-memory spillover.

Action Mode:
Diagnostic

---

# 87. Shader Compilation Stutter Context

Status:
IDEA

Type:
Diagnostic

Technical Domain:
GPU / Shaders

Primary Product Area:
Gaming

Descrição:
Distinguir possível shader compilation stutter de GPU incapacity.

Action Mode:
Diagnostic

---

# 88. Shader Cache State Awareness

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Driver / Shaders

Primary Product Area:
GPU

Descrição:
Detectar/configurar cache apenas quando interface suportada permitir.

Action Mode:
Diagnostic

---

# 89. Shader Cache Cleanup Guard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
GPU / Cleanup

Primary Product Area:
Shared

Descrição:
Não limpar shader caches rotineiramente como otimização.

Possível objetivo:
Evitar recompilação e stutter após limpeza.

Action Mode:
Internal

---

# 90. Shader Cache Size Tuning

Status:
RESEARCHING

Type:
Configuration

Technical Domain:
GPU Driver

Primary Product Area:
GPU

Descrição:
Investigar configurações de limite de cache por vendor.

Possível objetivo:
Avaliar quando default é insuficiente.

Evidence Level:
Vendor/context-dependent

Action Mode:
Diagnostic / Benchmark First

Importante:
Não assumir “10 GB” como valor universal.

---

# 91. GPU API Workload Identification

Status:
IDEA

Type:
Monitoring

Technical Domain:
Graphics APIs

Primary Product Area:
Monitoring

Descrição:
Identificar quando possível se aplicação usa:
- D3D11;
- D3D12;
- Vulkan;
- OpenGL.

Possível benefício:
Contexto de performance/troubleshooting.

Action Mode:
Diagnostic

---

# 92. GPU Compute Workload Detection

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Compute

Primary Product Area:
Monitoring

Descrição:
Distinguir workloads compute de 3D.

Action Mode:
Monitoring

---

# 93. Video Decode Utilization

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Video

Primary Product Area:
Monitoring

Descrição:
Monitorar engine de decode.

Action Mode:
Monitoring

---

# 94. Video Encode Utilization

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Video

Primary Product Area:
Monitoring

Descrição:
Monitorar encode.

Possível objetivo:
Diagnosticar streaming/recording impact.

Action Mode:
Monitoring

---

# 95. GPU Copy Engine Utilization

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
GPU Engines

Primary Product Area:
Monitoring

Descrição:
Monitorar copy/DMA engines.

Action Mode:
Monitoring

---

# 96. GPU Benchmark Readiness

Status:
CANDIDATE

Type:
Benchmark Safeguard

Technical Domain:
GPU Benchmark

Primary Product Area:
Benchmark

Descrição:
Verificar:
- temperature;
- background GPU usage;
- power mode;
- driver state;
- frame caps;
- battery/AC.

Action Mode:
Automatic

---

# 97. GPU Synthetic Benchmark

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
GPU Performance

Primary Product Area:
Benchmark

Descrição:
Executar workload gráfico controlado.

Possível objetivo:
Medir performance reproduzível.

Action Mode:
User-triggered

---

# 98. GPU Sustained Performance Benchmark

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
GPU Performance

Primary Product Area:
Benchmark

Descrição:
Executar workload suficientemente longo para detectar thermal/power behavior.

Action Mode:
User-triggered

---

# 99. GPU Stability Test

Status:
CANDIDATE

Type:
Benchmark / Diagnostic

Technical Domain:
GPU Stability

Primary Product Area:
Benchmark

Descrição:
Aplicar workload controlado visando detectar instabilidade.

Possível objetivo:
Validar clocks/configurações.

Risco inicial:
Medium

Action Mode:
Explicit User Choice

---

# 100. GPU WHEA/TDR Monitoring During Test

Status:
CANDIDATE

Type:
Validation

Technical Domain:
GPU Stability

Primary Product Area:
Benchmark

Descrição:
Monitorar WHEA, TDR e crashes durante teste.

Possível objetivo:
Não considerar configuração estável apenas porque benchmark terminou.

Action Mode:
Automatic during test

---

# 101. GPU Temperature Stability Validation

Status:
CANDIDATE

Type:
Validation

Technical Domain:
GPU Thermal

Primary Product Area:
Benchmark

Descrição:
Registrar temperatura e throttling durante benchmark.

Action Mode:
Automatic

---

# 102. GPU Performance Variability

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
GPU Performance

Primary Product Area:
Benchmark

Descrição:
Medir variação entre runs.

Possível objetivo:
Evitar concluir melhoria dentro do ruído.

Action Mode:
Automatic

---

# 103. GPU Before × After Validation

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
GPU

Primary Product Area:
Optimization

Descrição:
Comparar qualquer alteração relevante.

Possíveis métricas:
- FPS;
- frametime;
- score;
- power;
- temperature;
- clocks;
- stability.

Action Mode:
Automatic Validation

---

# 104. GPU Performance-per-Watt Benchmark

Status:
IDEA

Type:
Benchmark

Technical Domain:
GPU / Energy

Primary Product Area:
Benchmark

Descrição:
Comparar performance por energia consumida.

Possível objetivo:
Encontrar eficiência, não apenas score máximo.

Action Mode:
Benchmark

---

# 105. GPU Thermal Efficiency Comparison

Status:
IDEA

Type:
Benchmark

Technical Domain:
GPU Thermal

Primary Product Area:
Benchmark

Descrição:
Comparar ganho de performance contra temperatura/potência adicional.

Action Mode:
Benchmark

---

# 106. NVIDIA Capability Layer

Status:
CANDIDATE

Type:
Shared Capability

Technical Domain:
NVIDIA GPU

Primary Product Area:
Shared

Descrição:
Criar camada específica para capacidades NVIDIA.

Possíveis consumers:
- sensors;
- driver profiles;
- power;
- clocks;
- VRAM;
- tuning.

Action Mode:
Internal

---

# 107. AMD GPU Capability Layer

Status:
CANDIDATE

Type:
Shared Capability

Technical Domain:
AMD GPU

Primary Product Area:
Shared

Descrição:
Criar camada específica para capacidades AMD.

Action Mode:
Internal

---

# 108. Intel GPU Capability Layer

Status:
CANDIDATE

Type:
Shared Capability

Technical Domain:
Intel GPU

Primary Product Area:
Shared

Descrição:
Criar camada específica para capabilities Intel.

Action Mode:
Internal

---

# 109. Vendor Sensor API Abstraction

Status:
CANDIDATE

Type:
Infrastructure

Technical Domain:
GPU Sensors

Primary Product Area:
Shared

Descrição:
Unificar métricas vindas de APIs distintas.

Possível objetivo:
Expor:
- temperature;
- clock;
- power;
- fan;
- VRAM;
- limits

em modelo consistente.

Action Mode:
Internal

---

# 110. NVIDIA Driver Profile Inventory

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVIDIA Driver

Primary Product Area:
GPU

Descrição:
Investigar configurações globais/per-app relevantes do driver.

Action Mode:
Diagnostic

---

# 111. NVIDIA Profile Change Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
NVIDIA Driver

Primary Product Area:
GPU

Descrição:
Detectar valores customizados em relação a defaults conhecidos.

Possível objetivo:
Encontrar tweak packs antigos.

Action Mode:
Diagnostic

---

# 112. NVIDIA Threaded Optimization State

Status:
RESEARCHING

Type:
Diagnostic / Configuration

Technical Domain:
NVIDIA OpenGL Driver

Primary Product Area:
GPU

Descrição:
Investigar significado atual e aplicabilidade da opção Threaded Optimization.

Possível objetivo:
Classificar corretamente ideia antiga “Threaded Optimization = Off”.

Action Mode:
Research First

Importante:
Não definir OFF como default de otimização sem evidência.

---

# 113. NVIDIA Shader Cache Size State

Status:
RESEARCHING

Type:
Diagnostic / Configuration

Technical Domain:
NVIDIA Driver

Primary Product Area:
GPU

Descrição:
Investigar valores suportados, comportamento e default atual.

Possível objetivo:
Avaliar hipótese “10 GB”.

Action Mode:
Research / Contextual

---

# 114. NVIDIA Power Management Mode

Status:
RESEARCHING

Type:
Diagnostic / Configuration

Technical Domain:
NVIDIA Driver

Primary Product Area:
GPU

Descrição:
Investigar políticas de power management globais/per-app.

Possível objetivo:
Evitar “Prefer Maximum Performance” global sem necessidade.

Action Mode:
Benchmark First

---

# 115. NVIDIA Low Latency Mode

Status:
RESEARCHING

Type:
Diagnostic / Configuration

Technical Domain:
NVIDIA Driver

Primary Product Area:
Gaming

Descrição:
Investigar aplicabilidade por API/jogo e interação com Reflex.

Action Mode:
Contextual / Benchmark First

---

# 116. NVIDIA Reflex Awareness

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
NVIDIA Gaming

Primary Product Area:
Gaming

Descrição:
Reconhecer jogos que possuem solução própria de baixa latência quando detectável.

Possível objetivo:
Evitar conflito com tuning externo.

Action Mode:
Diagnostic

---

# 117. NVIDIA Frame Rate Limiter Context

Status:
IDEA

Type:
Diagnostic / Configuration

Technical Domain:
NVIDIA Driver

Primary Product Area:
Gaming

Descrição:
Considerar limiter do driver como possível razão para GPU underutilization.

Action Mode:
Diagnostic

---

# 118. AMD Driver Profile Inventory

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
AMD Driver

Primary Product Area:
GPU

Descrição:
Investigar estado de configurações relevantes do Adrenalin.

Action Mode:
Diagnostic

---

# 119. AMD Anti-Lag Context

Status:
RESEARCHING

Type:
Diagnostic / Configuration

Technical Domain:
AMD Gaming

Primary Product Area:
Gaming

Descrição:
Investigar versões e aplicabilidade de Anti-Lag/Anti-Lag 2.

Action Mode:
Contextual

---

# 120. AMD Radeon Chill Context

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
AMD Gaming

Primary Product Area:
Gaming

Descrição:
Reconhecer Chill/frame-control como possível razão de FPS/utilização menores.

Action Mode:
Diagnostic

---

# 121. AMD Enhanced Sync Context

Status:
RESEARCHING

Type:
Diagnostic / Configuration

Technical Domain:
AMD Display/Gaming

Primary Product Area:
Gaming

Descrição:
Investigar interação com VSync/VRR.

Action Mode:
Contextual

---

# 122. AMD Smart Access Memory Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
AMD / ReBAR

Primary Product Area:
GPU

Descrição:
Detectar estado relevante de Smart Access Memory/ReBAR.

Action Mode:
Diagnostic

---

# 123. Intel Arc Driver Profile Inventory

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
Intel GPU

Primary Product Area:
GPU

Descrição:
Investigar configurações gráficas/per-app relevantes.

Action Mode:
Diagnostic

---

# 124. Intel Arc Performance Readiness

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Intel GPU

Primary Product Area:
GPU

Descrição:
Verificar combinação de:
- ReBAR;
- driver;
- firmware/platform;
- PCIe link.

Possível objetivo:
Encontrar limitações importantes antes de tweaks.

Action Mode:
Diagnostic

---

# 125. GPU Driver Profile Default Restore

Status:
CANDIDATE

Type:
Repair

Technical Domain:
GPU Driver

Primary Product Area:
Repair

Descrição:
Permitir restaurar defaults de perfil quando configurações antigas provocarem problemas.

Risco inicial:
Medium

Action Mode:
User Choice

---

# 126. GPU Tweak Residue Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
GPU Driver

Primary Product Area:
Repair

Descrição:
Detectar customizações suspeitas deixadas por antigas ferramentas/tweak packs.

Possível objetivo:
Restaurar baseline suportada.

Action Mode:
Diagnostic First

---

# 127. GPU Overclock State Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
GPU Tuning

Primary Product Area:
GPU

Descrição:
Detectar indícios de clock/power configurados fora de stock.

Possível objetivo:
Dar contexto a crashes/temperatura.

Action Mode:
Diagnostic

---

# 128. GPU Undervolt State Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
GPU Tuning

Primary Product Area:
GPU

Descrição:
Detectar tuning de tensão quando vendor API permitir.

Action Mode:
Diagnostic

---

# 129. GPU Overclock / Undervolt Modification

Status:
QUESTIONABLE

Type:
Advanced Tuning

Technical Domain:
GPU Tuning

Primary Product Area:
Advanced / TBD

Descrição:
Registrar possibilidade de tuning, mas separar completamente de Optimize automático.

Risco inicial:
High

Action Mode:
Advanced / Explicit User Choice / Stability Required

---

# 130. GPU Fan Curve Modification

Status:
QUESTIONABLE

Type:
Advanced Tuning

Technical Domain:
GPU Cooling

Primary Product Area:
Advanced

Descrição:
Configurar curva de fans quando vendor permitir.

Possível benefício:
Trade-off térmica/ruído.

Risco inicial:
Medium

Action Mode:
Explicit User Choice

---

# 131. GPU Power Limit Modification

Status:
QUESTIONABLE

Type:
Advanced Tuning

Technical Domain:
GPU Power

Primary Product Area:
Advanced

Descrição:
Alterar limite de potência.

Risco inicial:
High

Action Mode:
Advanced Only

---

# 132. GPU Tuning Stability Validation

Status:
CANDIDATE

Type:
Safety

Technical Domain:
GPU Stability

Primary Product Area:
Benchmark

Descrição:
Qualquer tuning avançado precisa ser seguido por teste de:
- artifacts;
- crashes;
- TDR;
- thermal;
- power;
- performance variance.

Action Mode:
Mandatory Validation

---

# 133. GPU Artifact Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
GPU Stability

Primary Product Area:
Benchmark

Descrição:
Investigar meios de detectar rendering corruption durante stability workloads.

Action Mode:
Diagnostic

---

# 134. GPU Optimization Rejection on TDR

Status:
CANDIDATE

Type:
Safety

Technical Domain:
GPU

Primary Product Area:
Optimization

Descrição:
Rejeitar/rollback configuração que introduz novos TDRs.

Action Mode:
Automatic Safeguard

---

# 135. GPU Optimization Rejection on Thermal Cost

Status:
IDEA

Type:
Validation

Technical Domain:
GPU

Primary Product Area:
Optimization

Descrição:
Não considerar pequeno ganho automaticamente melhor quando power/temperature aumentam desproporcionalmente.

Action Mode:
Contextual

---

# 136. GPU Multi-Metric Optimization Result

Status:
CANDIDATE

Type:
Product Behavior

Technical Domain:
GPU

Primary Product Area:
Optimization

Descrição:
Avaliar alterações por:
- FPS;
- 1% lows;
- frametime;
- power;
- temperature;
- VRAM;
- stability.

Action Mode:
Automatic

---

# 137. GPU Health Summary

Status:
CANDIDATE

Type:
Diagnostic / Product Behavior

Technical Domain:
GPU

Primary Product Area:
My PC

Descrição:
Consolidar hardware, driver, sensors e findings.

Exemplo:

GPU:
NVIDIA ...

Driver:
...

VRAM:
...

Resizable BAR:
Enabled

PCIe:
Expected

Temperature:
Normal

Power limiting:
No

TDR history:
None

Status:
Healthy

---

# 138. GPU Performance Summary

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Performance

Primary Product Area:
Optimization

Descrição:
Responder:

“Minha GPU está entregando o que deveria neste workload?”

Possíveis resultados:
- GPU-limited as expected;
- CPU-limited;
- thermal constrained;
- power constrained;
- VRAM pressure;
- wrong GPU selected;
- frame cap active;
- unknown.

---

# 139. GPU Configuration Explain Why

Status:
CANDIDATE

Type:
Explainability

Technical Domain:
GPU

Primary Product Area:
Shared

Descrição:
Toda recomendação de GPU precisa explicar:
- current state;
- expected impact;
- evidence;
- vendor/platform scope;
- risk;
- benchmark requirement.

---

# Itens que NÃO devem virar otimização automática

## HAGS ON para todo mundo

Não.

---

## HAGS OFF para todo mundo

Também não.

---

## ReBAR sempre OFF ou sempre ON sem capability check

Não.

Quando suportado, tende a ser recurso desejável, mas implementação/benefício é específico da plataforma.

---

## “Prefer Maximum Performance” global

Não como padrão.

Pode aumentar consumo/temperatura sem melhorar workload.

---

## NVIDIA Threaded Optimization = Off

Não classificar como regra até investigação específica.

---

## Shader Cache Size = 10 GB

Não classificar como valor mágico.

---

## Limpar shader cache após cada otimização

Não.

---

## High Performance GPU para todos os aplicativos

Não.

---

## Desativar iGPU em notebook

Não.

---

## Force P-State máximo

Não.

---

## Overclock automático

Não.

---

## Power Limit máximo

Não.

---

## TdrDelay alto para corrigir crashes

Não.

---

## DDU em toda atualização de driver

Não.

---

# Princípios que emergem da área GPU

1. O sistema pode ter múltiplas GPUs.

2. Adaptador lógico e físico não são necessariamente a mesma coisa.

3. GPU possui múltiplos engines.

4. Um único percentual não descreve toda atividade gráfica.

5. VRAM dedicada e memória compartilhada devem ser distinguidas.

6. Memória compartilhada não equivale a “VRAM extra grátis”.

7. VRAM pressure pode afetar frametime.

8. Driver e WDDM são parte importante das capabilities.

9. HAGS precisa ser capability + benchmark, não dogma.

10. GPU preference por aplicativo é mecanismo legítimo.

11. Em multi-GPU, selecionar GPU errada pode produzir grande perda de performance.

12. Hybrid graphics é também uma questão energética.

13. ReBAR é capability PCIe/firmware, não Registry tweak.

14. Intel Arc merece atenção especial a ReBAR.

15. PCIe link deve ser testado sob carga antes de declarar problema.

16. GPU 99–100% pode ser comportamento ideal se ela é o bottleneck desejado.

17. GPU abaixo de 100% não significa automaticamente problema.

18. FPS cap, VSync e CPU podem reduzir utilização da GPU.

19. Temperatura, potência e clocks precisam ser analisados juntos.

20. TDR é recuperação do Windows, não “otimização”.

21. Mudar TDR timeout pode esconder o problema.

22. Shader cache é útil.

23. Limpar cache pode piorar temporariamente stutter.

24. Driver profiles precisam ser tratados por vendor.

25. Configuração global e per-game precisam ser separadas.

26. NVIDIA, AMD e Intel exigirão extensões específicas.

27. Overclock/undervolt pertence a tuning avançado.

28. Ganho de performance precisa sobreviver a stability test.

29. TDR/artifact invalida tuning.

30. Melhor GPU configuration pode variar por jogo/API.

---

# Questões para segunda passada

- Qual é a melhor combinação DXCore/DXGI/D3DKMT para inventário?
- Como obter utilization de engines sem depender de Performance Counters lentos?
- ETW/DxgKrnl pode alimentar overlay?
- Como Task Manager obtém GPU utilization?
- Qual overhead?
- Dedicated video memory budget pode ser obtido por DXGI?
- Como medir budget vs usage?
- Existe conceito de eviction pressure útil?
- Podemos detectar residency/evictions?
- Como identificar VRAM oversubscription?
- Como correlacionar evictions a frametime?
- NVIDIA NVML é redistribuível/adequada ao app?
- NVAPI seria necessária para driver profiles?
- Quais capabilities exigem NVAPI e quais NVML?
- AMD ADLX pode cobrir sensors + settings?
- Intel possui API equivalente?
- Como manter camada vendor sem quebrar após drivers novos?
- Como tratar laptops com OEM custom driver?
- Como detectar GPU hotspot entre vendors?
- Como detectar memory junction temperature?
- Como detectar throttling reasons de forma vendor-neutral?
- NVIDIA PerfCap Reason?
- AMD limiter telemetry equivalente?
- Intel limiter telemetry?
- Como medir GPU effective clock?
- GPU power readings possuem precisão suficiente?
- Como detectar board power vs chip power?
- Fan curve APIs possuem risco?
- Como detectar overclock aplicado por MSI Afterburner/vendor software?
- Como detectar conflicting tuning tools?
- NVIDIA app / Control Panel profiles podem ser lidos?
- AMD Adrenalin profiles podem ser lidos?
- Intel Graphics Software profiles podem ser lidos?
- Threaded Optimization ainda afeta apenas OpenGL?
- Qual é o default moderno?
- Shader Cache Size default atual da NVIDIA?
- Unlimited pode causar problemas de espaço?
- NVIDIA Low Latency Mode funciona em DX12/Vulkan?
- Quando Reflex substitui Low Latency Mode?
- AMD Anti-Lag 2 precisa de integração do jogo?
- Como tratar Radeon Boost?
- Radeon Chill?
- Enhanced Sync?
- AFMF/frame generation no driver?
- NVIDIA Smooth Motion/driver frame generation em versões atuais?
- Intel frame generation equivalent?
- Frame generation pertence a GPU ou Gaming?
- DLSS/FSR/XeSS capabilities pertencem à GPU?
- Ray tracing capability?
- Mesh shaders?
- VRS?
- Sampler Feedback?
- DirectML/AI capabilities?
- NPU deve ser área distinta?
- CUDA capability pertence a My PC?
- OpenCL?
- ROCm?
- OneAPI?
- GPU compute benchmark?
- Video encode/decode codec capability?
- AV1 hardware encode/decode?
- H.264/H.265?
- Media engine benchmark?
- GPU scheduling priority?
- Graphics preemption granularity é útil ao diagnóstico?
- Compute preemption?
- Hardware scheduler queue?
- HAGS state pode ser lido via API oficial?
- Como trocar HAGS de forma suportada?
- Reboot é obrigatório?
- Qual efeito de HAGS em frame generation?
- Como tratar MPO?
- Multi-Plane Overlay pertence a Display/GPU?
- MPO disable tweak deve provavelmente ser rejeitado como universal?
- Hardware overlays podem causar glitches em casos específicos?
- Flip model?
- Fullscreen exclusive vs borderless?
- Windowed optimizations pertence a Gaming.
- GPU preference do Windows sobrescreve NVIDIA/AMD setting?
- Qual precedência?
- MUX/Advanced Optimus/SmartAccess Graphics?
- Como detectar discrete-only mode?
- External display routing?
- Thunderbolt eGPU?
- eGPU bandwidth diagnostics?
- PCIe generation/width via Windows?
- ReBAR current BAR size?
- Above 4G Decoding state?
- ReBAR whitelist ainda existe em NVIDIA?
- Como muda por geração?
- WHEA PCIe errors podem apontar GPU?
- Live Kernel Events 141/117?
- Reliability Monitor registra GPU resets úteis?
- Event IDs mais confiáveis?
- TDR vs engine timeout?
- Black screen without TDR?
- Driver hang?
- Device removed errors?
- DXGI_ERROR_DEVICE_REMOVED diagnostics?
- DRED pode ser útil?
- Direct3D 12 Device Removed Extended Data?
- Aplicação pode fornecer DRED, mas BeadWise externo consegue?
- GPU crash dumps?
- OC stability tester precisa detectar artifacts?
- Como fazer artifact test sem falsos positivos?
- Benchmark interno ou integrar workloads existentes?
- Precisamos de benchmark raster + compute separados?
- Ray tracing benchmark separado?
- VRAM benchmark?
- PCIe bandwidth test CPU↔GPU?
- Resizable BAR benchmark?
- GPU memory bandwidth benchmark?
- Memory clock/VRAM errors?
- ECC VRAM em workstation cards?
- GPU memory ECC health?
- GPU reliability em workstation/server?
- Como comparar score com GPUs equivalentes?
- Laptop GPU TGP varia muito; comparação precisa considerar power limit.
- Devemos detectar TGP?
- NVIDIA Max-Q?
- Dynamic Boost?
- AMD SmartShift?
- GPU performance em bateria?
- Battery Boost?
- WhisperMode?
- OEM modes?
- MSHybrid?
- GPU thermal limit vs laptop chassis limit?
- Como comparar notebook com desktop do mesmo nome?
- Melhor detectar PCI IDs + TGP para baseline.

---

# Avaliação inicial

A primeira passada encontrou aproximadamente 139 capacidades, subcapacidades e princípios.

Os blocos mais fortes são:

## Hardware
- adapters;
- topology;
- engines;
- VRAM;
- PCIe;
- ReBAR.

## Monitoring
- utilization;
- clocks;
- temperatures;
- power;
- fans;
- VRAM.

## Drivers
- version;
- WDDM;
- crashes;
- TDR;
- regression.

## Windows Graphics
- HAGS;
- GPU preference;
- hybrid graphics.

## Graphics APIs
- DirectX;
- feature level;
- Shader Model;
- Vulkan;
- OpenGL.

## Performance
- bottleneck;
- limiter;
- frametime;
- VRAM pressure.

## Vendor Layers
- NVIDIA;
- AMD;
- Intel.

## Stability
- TDR;
- WHEA;
- stress;
- artifacts.

## Benchmark
- synthetic;
- sustained;
- performance-per-watt;
- before/after.

## Advanced Tuning
- clocks;
- voltage;
- power limit;
- fan curve.

Estado:

FIRST PASS COMPLETE

Próximo passo:

**SECOND PASS — GPU GAP AUDIT**

# Discovery Catalog — GPU
## Segunda passada — auditoria de lacunas consolidada

> Este bloco complementa os itens 1–139 da primeira passada de GPU.
>
> Nesta segunda passada, detalhes técnicos não recebem automaticamente um item próprio.
>
> Exemplos:
>
> - temperatura, power, clock e limiter reasons específicos NVIDIA/AMD/Intel pertencem à capability de telemetria/vendor;
> - vários eventos de VRAM pertencem a Video Memory Diagnostics;
> - diferentes mecanismos de apresentação pertencem a Presentation Pipeline Diagnostics.
>
> Objetivo:
>
> **máxima cobertura com menor inflação artificial do catálogo.**

---

# 140. GPU Video Memory Budget Diagnostics

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Memory / WDDM

Primary Product Area:
GPU

Possible Consumers:
- Gaming
- Monitoring
- Benchmark

Final UI Placement:
TBD

Descrição:
Analisar memória gráfica usando o orçamento gerenciado pelo Windows, e não apenas capacidade física total da VRAM.

Possível objetivo:
Distinguir:

- VRAM física;
- budget disponível ao workload;
- uso atual;
- reservation;
- memória compartilhada.

Possível benefício:
Diagnóstico muito mais correto de pressão de VRAM.

Aplicabilidade:
WDDM / DXGI-capable GPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic / Monitoring

Possíveis métricas internas:
- Budget;
- CurrentUsage;
- AvailableForReservation;
- CurrentReservation.

Importante:
Usar “8 GB VRAM” isoladamente é insuficiente para determinar quanto um workload pode utilizar naquele momento.

O próprio DXGI alerta que ultrapassar o orçamento concedido pelo sistema pode resultar em penalidade de desempenho ou stutter. citeturn515314search4

---

# 141. GPU Memory Residency Pressure Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Memory

Primary Product Area:
Gaming

Possible Consumers:
- Stutter Analyzer
- Benchmark
- Monitoring

Final UI Placement:
TBD

Descrição:
Investigar situações em que o working set gráfico excede ou pressiona o orçamento fornecido pelo Windows.

Possível objetivo:
Identificar:
- oversubscription;
- trimming;
- possível movimentação entre memória dedicada e compartilhada;
- stutter associado à pressão de memória.

Possível benefício:
Separar “VRAM quase cheia” de pressão real do WDDM.

Applicability:
GPU workloads

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established architecture / detailed correlation requires measurement

Action Mode:
Diagnostic

Consolida:
- VRAM Pressure Detection;
- VRAM Spillover Detection;
- parte de VRAM/Frametime Correlation.

---

# 142. GPU Scheduler / Engine Trace

Status:
CANDIDATE

Type:
Advanced Diagnostic

Technical Domain:
WDDM / ETW

Primary Product Area:
Tools

Possible Consumers:
- GPU
- Gaming
- Latency
- Benchmark

Final UI Placement:
TBD

Descrição:
Capturar atividade de scheduling e execução das GPU engines durante sessão específica.

Possível objetivo:
Investigar problemas que polling simples de utilização não consegue explicar.

Possíveis análises:
- engines utilizadas;
- work submission;
- scheduling;
- waits;
- queue behavior;
- CPU↔GPU synchronization;
- context switches gráficos.

Possível benefício:
Base para troubleshooting profundo de stutter e baixa utilização.

Applicability:
Advanced Diagnostics

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established Windows graphics tracing model

Action Mode:
User-triggered Diagnostic

---

# 143. Graphics Presentation Pipeline Diagnostics

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
DXGI / DWM / Presentation

Primary Product Area:
Shared

Possible Consumers:
- Gaming
- FPS Monitoring
- Display
- Latency

Final UI Placement:
TBD

Descrição:
Identificar o caminho utilizado para apresentar frames ao display.

Possível objetivo:
Dar contexto a diferenças entre:
- legacy blt presentation;
- flip model;
- DirectFlip;
- Independent Flip;
- composed presentation;
- borderless;
- exclusive fullscreen.

Possível benefício:
Diagnosticar latência/apresentação sem reduzir tudo a “fullscreen é melhor”.

Aplicabilidade:
Windows graphics workloads

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Observação:
O Windows moderno possui otimizações de flip/DirectFlip capazes, em condições adequadas, de evitar parte da composição tradicional do desktop. citeturn131994search6

---

# 144. Present Mode / Frame Presentation Monitoring

Status:
CANDIDATE

Type:
Monitoring / Diagnostic

Technical Domain:
Graphics Presentation

Primary Product Area:
Monitoring

Possible Consumers:
- FPS
- Latency
- Gaming

Final UI Placement:
TBD

Descrição:
Observar comportamento de apresentação dos frames durante gameplay.

Possível objetivo:
Explicar diferenças entre FPS produzido e frames efetivamente apresentados.

Possível benefício:
Base para análise futura de:
- latency;
- frame pacing;
- VSync;
- tearing;
- compositor;
- flip modes.

Aplicabilidade:
Gaming sessions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established graphics concepts

Action Mode:
Monitoring

---

# 145. Multi-Plane Overlay Capability / Health

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Windows Composition / MPO

Primary Product Area:
GPU

Possible Consumers:
- Display
- Video
- Repair

Final UI Placement:
TBD

Descrição:
Reconhecer suporte e contexto de Multi-Plane Overlay.

Possível objetivo:
Dar contexto a problemas específicos de composição, reprodução de vídeo ou display.

Possível benefício:
Troubleshooting direcionado.

Applicability:
Supported WDDM GPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established Windows feature

Action Mode:
Diagnostic

Importante:
MPO permite ao hardware compor múltiplos planos sem exigir que toda composição seja feita da forma tradicional pelo CPU/GPU. citeturn515314search2

---

# 146. MPO Disable Tweak Guard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
Windows Graphics

Primary Product Area:
Shared

Descrição:
Impedir que “Disable MPO” seja oferecido como otimização genérica.

Possível objetivo:
Tratar workaround apenas quando problema compatível foi diagnosticado.

Possível benefício:
Evitar desativar recurso da arquitetura de composição sem necessidade.

Action Mode:
Internal Safeguard

---

# 147. VRR / Tearing Presentation Capability

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
DXGI / Display

Primary Product Area:
Shared

Possible Consumers:
- GPU
- Gaming
- Display

Final UI Placement:
TBD

Descrição:
Detectar capacidade de apresentação com tearing/VRR relevante ao pipeline gráfico.

Possível objetivo:
Dar contexto a:
- VRR;
- VSync;
- tearing;
- borderless presentation.

Possível benefício:
Evitar recomendações incorretas de sincronização.

Evidence Level:
Established

Action Mode:
Diagnostic

Observação:
No DXGI moderno, suporte a tearing é pré-requisito para VRR no caminho correspondente. citeturn131994search0turn131994search2

---

# 148. GPU Device Removed Diagnostics

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Direct3D / GPU Stability

Primary Product Area:
Repair

Possible Consumers:
- GPU
- Gaming
- Stability

Final UI Placement:
TBD

Descrição:
Reconhecer falhas nas quais dispositivo gráfico foi removido/resetado durante execução.

Possível objetivo:
Distinguir:
- device removed;
- device reset;
- driver crash;
- TDR;
- application rendering fault.

Possível benefício:
Diagnóstico de crashes gráficos mais preciso.

Applicability:
Direct3D workloads

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 149. DRED Crash Context

Status:
CANDIDATE

Type:
Advanced Diagnostic

Technical Domain:
Direct3D 12 / GPU Crash

Primary Product Area:
Tools

Possible Consumers:
- GPU
- Repair
- Gaming

Final UI Placement:
TBD

Descrição:
Aproveitar Device Removed Extended Data quando disponível em workloads sob controle ou diagnóstico compatível.

Possível objetivo:
Obter contexto como:
- GPU breadcrumbs;
- page fault information;
- operações anteriores à remoção do device.

Possível benefício:
Investigar crashes D3D12 com muito mais profundidade.

Applicability:
D3D12 diagnostic scenarios

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established D3D12 capability

Action Mode:
Advanced Diagnostic

DRED fornece breadcrumbs e dados de page fault após cenários como `DXGI_ERROR_DEVICE_REMOVED`. citeturn131994search4

Limitação:
Uma aplicação externa não possui automaticamente todos os dados internos de DRED de qualquer jogo.

Portanto:
capability precisa respeitar limites técnicos reais.

---

# 150. GPU Crash Incident Correlation

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Stability

Primary Product Area:
Repair

Possible Consumers:
- Gaming
- Drivers
- Benchmark

Final UI Placement:
TBD

Descrição:
Construir timeline de incidentes gráficos combinando:

- TDR;
- device removed;
- LiveKernelEvent/Windows reliability evidence quando disponível;
- driver update;
- GPU temperature;
- power;
- clocks;
- overclock state;
- WHEA.

Possível objetivo:
Responder:

“Por que meu driver de vídeo está crashando?”

Possível benefício:
Finding muito mais útil que apenas mostrar Event ID.

Risco inicial:
Low

Evidence Level:
Correlation-based

Action Mode:
Diagnostic

---

# 151. Vendor Performance Telemetry Layer

Status:
CANDIDATE

Type:
Shared Capability

Technical Domain:
GPU Vendor APIs

Primary Product Area:
Shared

Possible Consumers:
- Monitoring
- GPU
- Benchmark
- Cooling
- Energy

Final UI Placement:
TBD

Descrição:
Criar camada comum capaz de consumir telemetria adicional oferecida por NVIDIA, AMD e Intel.

Possível objetivo:
Complementar as APIs vendor-neutral do Windows.

Modelo conceitual comum:
- utilization;
- clocks;
- temperature;
- memory temperature;
- power;
- fan;
- performance state;
- active limiters;
- vendor-specific telemetry.

Possível benefício:
Uma única feature de diagnóstico, não dezenas de features por sensor.

Applicability:
Vendor-supported GPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established vendor APIs / per-vendor support differs

Action Mode:
Internal / Monitoring

Exemplo:
A NVIDIA NVML expõe utilização, temperatura, fans e diversos estados, além de causas específicas para redução de clock por potência, temperatura, idle e outras condições. citeturn131994search7turn596212search1

---

# 152. GPU Limiter Diagnostics

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Performance

Primary Product Area:
GPU

Possible Consumers:
- Benchmark
- Gaming
- Cooling

Final UI Placement:
TBD

Descrição:
Consolidar telemetria genérica e específica de fabricante para explicar por que a GPU não aumenta performance.

Possíveis classes:
- utilization/workload limited;
- CPU limited;
- frame-cap limited;
- power limited;
- thermal limited;
- voltage/reliability limited;
- idle;
- application clock limit;
- platform power limit;
- unknown.

Possível objetivo:
Transformar sensores dispersos em finding compreensível.

Action Mode:
Diagnostic

Consolida vários itens da primeira passada:
- GPU Power Limit Detection;
- GPU Thermal Limit Detection;
- Voltage/Reliability Context;
- Performance Limiter Classification.

---

# 153. Laptop GPU Power Envelope Context

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Mobile GPU / Power

Primary Product Area:
GPU

Possible Consumers:
- Laptop
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Tratar GPUs móveis considerando envelope de potência da implementação específica.

Possível objetivo:
Evitar comparar duas GPUs com o mesmo nome comercial como se necessariamente tivessem a mesma performance.

Possíveis fatores:
- configured power envelope;
- thermal capacity;
- OEM performance mode;
- Dynamic Boost/SmartShift-like mechanisms;
- AC/battery state.

Possível benefício:
Benchmark e diagnóstico muito mais corretos em notebooks.

Evidence Level:
Vendor/OEM-dependent

Action Mode:
Diagnostic

---

# 154. Hybrid Graphics Routing Diagnostics

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Hybrid Graphics

Primary Product Area:
GPU

Possible Consumers:
- Gaming
- Laptop
- Display
- Energy

Final UI Placement:
TBD

Descrição:
Consolidar a análise de como renderização e apresentação trafegam em sistemas híbridos.

Possíveis configurações:
- iGPU-only;
- hybrid/MSHybrid;
- dGPU render through iGPU;
- dGPU direct output;
- discrete-only;
- external display attached to one adapter;
- hardware MUX.

Possível objetivo:
Explicar performance, consumo e comportamento do display.

Action Mode:
Diagnostic

Consolida:
- Hybrid Graphics Topology;
- External Display Routing;
- MUX Detection;
- dGPU Idle Residency context.

---

# 155. Graphics Configuration Conflict Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Configuration

Primary Product Area:
Repair

Possible Consumers:
- GPU
- Gaming Profiles

Final UI Placement:
TBD

Descrição:
Detectar quando múltiplas camadas parecem controlar o mesmo comportamento gráfico.

Possíveis atores:
- Windows Graphics settings;
- NVIDIA/AMD/Intel profile;
- OEM utility;
- game settings;
- overlay/tuning software.

Possível objetivo:
Explicar configurações que parecem “não pegar”.

Possível benefício:
Troubleshooting e redução de tweak stacking.

Action Mode:
Diagnostic First

---

# 156. GPU Driver Profile Baseline / Diff

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Vendor Driver Profiles

Primary Product Area:
GPU

Possible Consumers:
- Repair
- Gaming Profiles
- Optimization

Final UI Placement:
TBD

Descrição:
Comparar configurações relevantes de driver com baseline/default conhecido para aquele driver.

Possível objetivo:
Encontrar customizações herdadas de:
- tweak packs;
- ferramentas antigas;
- perfis manuais;
- software de terceiros.

Possível benefício:
Muito mais útil do que analisar cada setting isoladamente.

Action Mode:
Diagnostic

Importante:
“Non-default” não significa automaticamente “bad”.

---

# 157. GPU Configuration Provenance

Status:
IDEA

Type:
Diagnostic

Technical Domain:
GPU Configuration

Primary Product Area:
Repair

Descrição:
Quando possível, identificar provável origem de determinada modificação.

Exemplos:
- Windows;
- driver vendor;
- OEM;
- application profile;
- third-party tuner;
- unknown.

Possível objetivo:
Explicar conflitos e permitir rollback mais seguro.

Action Mode:
Diagnostic

---

# 158. GPU Tuning Tool Conflict Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Tuning

Primary Product Area:
GPU

Possible Consumers:
- Stability
- Repair

Final UI Placement:
TBD

Descrição:
Detectar múltiplas ferramentas tentando controlar clocks, voltage, fan ou power simultaneamente.

Possíveis exemplos conceituais:
- OEM tuning utility;
- vendor software;
- third-party overclock utility;
- BeadWise advanced tuning.

Possível objetivo:
Prevenir comportamento imprevisível.

Risco inicial:
Low

Evidence Level:
Product diagnostic concept

Action Mode:
Diagnostic First

---

# 159. GPU Media Capability Inventory

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Media Engines

Primary Product Area:
My PC

Possible Consumers:
- Streaming
- Recording
- Video

Final UI Placement:
TBD

Descrição:
Inventariar capacidades de encode/decode por hardware.

Possíveis codecs:
- H.264;
- HEVC;
- AV1;
- VP9;
- outros conforme hardware.

Possível objetivo:
Responder se workload de vídeo pode usar hardware acceleration.

Possível benefício:
Streaming/recording diagnostics.

Action Mode:
Diagnostic

Consolida:
- Video Encode Utilization;
- Video Decode Utilization;
- codec capability questions.

---

# 160. GPU Compute Capability Inventory

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU Compute

Primary Product Area:
My PC

Possible Consumers:
- Benchmark
- Productivity
- AI Tools

Final UI Placement:
TBD

Descrição:
Inventariar capacidades compute relevantes sem transformar cada API em feature.

Possíveis interfaces/ecossistemas:
- DirectCompute;
- DirectML;
- CUDA quando aplicável;
- OpenCL;
- Vulkan compute;
- vendor-specific capabilities.

Possível objetivo:
Hardware information e compatibility.

Action Mode:
Diagnostic

---

# 161. Modern Rendering Feature Inventory

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Graphics Features

Primary Product Area:
My PC

Possible Consumers:
- Gaming
- Benchmark

Final UI Placement:
TBD

Descrição:
Consolidar recursos gráficos modernos da GPU.

Possíveis exemplos:
- ray tracing;
- mesh shaders;
- variable rate shading;
- sampler feedback;
- relevant D3D feature levels;
- Shader Model.

Possível objetivo:
Não criar uma feature por capability Direct3D.

Action Mode:
Automatic Diagnostic

---

# 162. Upscaling / Frame Generation Capability Context

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Gaming Graphics

Primary Product Area:
Gaming

Possible Consumers:
- GPU
- Benchmark

Final UI Placement:
TBD

Descrição:
Registrar capacidades/ecossistemas suportados pela GPU relacionados a:
- temporal upscaling;
- frame generation;
- low-latency companion technologies.

Possíveis famílias:
- DLSS;
- FSR;
- XeSS;
- vendor/driver frame generation quando aplicável.

Possível objetivo:
Permitir que Gaming interprete corretamente configurações disponíveis.

Importante:
Pertence principalmente à futura área Gaming.

GPU deve apenas fornecer capabilities.

---

# 163. PCIe GPU Transfer Diagnostics

Status:
IDEA

Type:
Benchmark / Diagnostic

Technical Domain:
GPU / PCIe

Primary Product Area:
Benchmark

Possible Consumers:
- GPU
- PCIe
- ReBAR

Final UI Placement:
TBD

Descrição:
Medir transferência CPU↔GPU em cenário controlado quando fizer sentido.

Possível objetivo:
Detectar:
- link inadequado;
- bandwidth anormal;
- plataforma limitada.

Possível benefício:
Validação de PCIe e ReBAR em troubleshooting específico.

Action Mode:
Advanced Benchmark

---

# 164. GPU Memory Performance Benchmark

Status:
IDEA

Type:
Benchmark

Technical Domain:
VRAM

Primary Product Area:
Benchmark

Descrição:
Medir bandwidth/transfer relacionado à memória gráfica quando workload adequado estiver disponível.

Possível objetivo:
Caracterizar subsistema de VRAM.

Possível benefício:
Advanced hardware diagnostics.

Action Mode:
Benchmark

Importante:
Não precisa fazer parte do benchmark padrão para usuário comum.

---

# 165. GPU Benchmark Workload Classification

Status:
CANDIDATE

Type:
Benchmark Architecture

Technical Domain:
GPU Benchmark

Primary Product Area:
Benchmark

Descrição:
Separar workloads que medem características diferentes.

Possíveis perfis:
- rasterization;
- ray tracing;
- compute;
- VRAM;
- sustained thermal;
- gaming-like workload.

Possível objetivo:
Evitar “GPU Score” único sem contexto.

Action Mode:
Internal / Benchmark

---

# 166. GPU Monitoring Overhead Safeguard

Status:
CANDIDATE

Type:
Safety / Infrastructure

Technical Domain:
GPU Telemetry

Primary Product Area:
Shared

Possible Consumers:
- FPS Overlay
- Benchmark
- Monitoring

Final UI Placement:
TBD

Descrição:
Controlar frequência e profundidade das consultas de telemetria.

Possível objetivo:
Garantir que BeadWise não afete o workload observado.

Possível benefício:
Maior confiança em FPS/frametime.

Possível estratégia:
- Lightweight Overlay Mode;
- Standard Monitoring;
- Deep Diagnostic Session.

Action Mode:
Internal

---

# 167. GPU Optimization Evidence Gate

Status:
CANDIDATE

Type:
Product Behavior / Safety

Technical Domain:
GPU Optimization

Primary Product Area:
Shared

Descrição:
Exigir determinado nível de evidência antes que configuração gráfica seja classificada como optimization candidate.

Possíveis níveis:

### Established capability
Exemplo:
jogo usando iGPU errada.

### Strong contextual recommendation
Exemplo:
ReBAR ausente em plataforma onde fabricante considera necessário para desempenho ideal.

### Benchmark-required
Exemplo:
HAGS ON/OFF.

### Experimental
Exemplo:
driver profile tweak sem evidência consistente.

### Reject
Exemplo:
TdrDelay alto para esconder crashes.

Possível objetivo:
Evitar que opções de driver virem automaticamente “tweaks”.

Action Mode:
Internal

---

# 168. GPU Diagnostic Session

Status:
CANDIDATE

Type:
Diagnostic Tool

Technical Domain:
GPU

Primary Product Area:
GPU

Possible Consumers:
- Gaming
- Repair
- Benchmark

Final UI Placement:
TBD

Descrição:
Executar diagnóstico consolidado da GPU durante workload real.

Possíveis dados:
- GPU engines;
- VRAM budget;
- shared memory;
- clocks;
- temperature;
- power;
- limiter;
- PCIe;
- presentation mode;
- CPU utilization;
- frametime;
- TDR/device errors.

Possível objetivo:
Responder:

> “Por que minha GPU/jogo não está entregando o desempenho esperado?”

Possível benefício:
Transformar dezenas de sensores em uma sessão diagnóstica compreensível.

Action Mode:
User-triggered

---

# Consolidações da primeira passada

A segunda investigação mostrou que alguns itens anteriormente separados devem futuramente ser unidos.

## GPU Sensors

Agrupar:
- core temperature;
- hotspot;
- memory temperature;
- fan;
- clocks;
- voltage;
- board power.

Capability provável:

**GPU Sensor Telemetry**

---

## GPU Limiters

Agrupar:
- thermal limit;
- power limit;
- voltage/reliability;
- platform limit;
- vendor PerfCap/limiter states.

Capability provável:

**GPU Limiter Diagnostics**

---

## GPU Memory

Agrupar:
- dedicated VRAM;
- shared memory;
- usage;
- budget;
- residency pressure;
- per-process usage.

Capability provável:

**GPU Memory Diagnostics**

---

## Driver Stability

Agrupar:
- TDR;
- driver crash history;
- device removed;
- reliability events;
- WHEA correlation.

Capability provável:

**GPU Crash & Recovery Diagnostics**

---

## Hybrid Graphics

Agrupar:
- iGPU/dGPU;
- per-app GPU preference;
- wrong GPU;
- external monitor routing;
- MUX;
- dGPU idle.

Capability provável:

**Hybrid Graphics Diagnostics**

---

## Graphics APIs

Agrupar:
- D3D feature levels;
- Shader Model;
- Vulkan;
- OpenGL;
- modern rendering features.

Capability provável:

**Graphics Capability Inventory**

---

## Vendor APIs

Não criar um subsystem de features para cada sensor de cada fabricante.

Modelo:

**Generic Windows GPU Layer**
+
**NVIDIA Adapter**
+
**AMD Adapter**
+
**Intel Adapter**

e todos alimentam um modelo comum.

---

# Revisão das ideias específicas NVIDIA da fase inicial

## Threaded Optimization = Off

Status sugerido:
**QUESTIONABLE / CONTEXT-DEPENDENT**

Não deve virar optimization default.

Investigar como configuração específica de driver/API, provavelmente relevante principalmente a determinados workloads OpenGL/legacy.

---

## Shader Cache Size = 10 GB

Status sugerido:
**QUESTIONABLE**

Não existe razão nesta fase para transformar 10 GB em valor universal.

Capability correta:

**Shader Cache Configuration Diagnostics**

e não:

**Set Shader Cache to 10 GB**

---

## Prefer Maximum Performance

Status sugerido:
**CONTEXT-DEPENDENT**

Pode fazer sentido por aplicação em determinados casos.

Não globalmente.

Deve exigir:
- workload;
- comportamento de clock;
- performance measurement;
- power/thermal cost.

---

## NVIDIA Low Latency Mode

Status sugerido:
**CONTEXT-DEPENDENT**

Deve ser analisado junto com:
- graphics API;
- game;
- Reflex;
- frame queue;
- FPS limiting;
- VSync/VRR.

Pertence principalmente a Gaming/Latency.

---

# Revisão de tweaks gráficos comuns

## Disable MPO globally

Não como optimization.

---

## Disable HAGS globally

Não.

---

## Enable HAGS globally

Também não.

---

## Disable VRR

Não como padrão.

---

## Force maximum GPU clocks

Não.

---

## Force maximum PCIe state

Não.

---

## Disable hybrid graphics

Não.

---

## Force dedicated GPU for every application

Não.

---

## Change TdrDelay to fix GPU crashes

Rejeitar como correção normal.

---

## Clear shader cache repeatedly

Não.

---

# Novos princípios consolidados

1. VRAM física não é a única métrica de memória gráfica relevante.

2. WDDM administra budget de memória gráfica dinamicamente.

3. Pressão de VRAM deve ser medida contra budget, não apenas capacidade nominal.

4. GPU presentation path pode afetar comportamento e latência.

5. Flip model, DirectFlip e composição são contextos diferentes.

6. MPO é feature da arquitetura gráfica, não tweak a ser desligado preventivamente.

7. VRR depende também do caminho de apresentação utilizado pelo software.

8. Device Removed/TDR devem alimentar diagnóstico de estabilidade.

9. DRED é diagnóstico profundo, não feature de otimização.

10. APIs vendor-neutral devem formar a base.

11. APIs NVIDIA/AMD/Intel complementam telemetria onde necessário.

12. Sensores específicos não precisam virar features independentes.

13. Limiter reasons devem ser consolidados em diagnóstico compreensível.

14. Notebook GPU precisa ser interpretada segundo envelope térmico/energético da implementação.

15. Mesmo nome comercial não garante mesma performance em notebook.

16. Hybrid graphics é questão de routing, performance e energia.

17. Múltiplas camadas podem disputar controle de uma configuração gráfica.

18. Driver profile diff é mais útil que lista interminável de tweaks.

19. Configuração diferente do default não é automaticamente errada.

20. GPU compute/media capabilities devem ser inventariadas, não transformadas em dezenas de features.

21. Upscaling/frame generation pertence majoritariamente a Gaming.

22. Benchmark gráfico precisa declarar o que está medindo.

23. Monitoramento precisa ter overhead conhecido.

24. Deep Diagnostics e FPS Overlay não devem usar a mesma profundidade de coleta.

25. Optimization candidate precisa passar por um Evidence Gate.

---

# Resultado da segunda passada

Primeira passada:
~139 itens brutos.

Segunda passada consolidada:
29 capabilities/lacunas adicionadas.

Total bruto:
~168 itens.

Isso é significativamente menor do que seria usando a granularidade anterior.

Mais importante:

vários itens da primeira passada já foram identificados como candidatos claros à consolidação.

---

# Estimativa preliminar de consolidação futura

Os ~168 itens de GPU provavelmente NÃO resultarão em 168 features.

Uma possível redução futura poderia parecer algo como:

## Hardware / My PC
1. GPU Information
2. Graphics Capability Inventory
3. PCIe / ReBAR Status
4. Hybrid Graphics Topology

## Monitoring
5. GPU Performance Monitoring
6. GPU Memory Monitoring
7. GPU Sensor Monitoring

## Diagnostics
8. GPU Bottleneck Diagnostics
9. GPU Limiter Diagnostics
10. GPU Memory Pressure Diagnostics
11. GPU Crash & Recovery Diagnostics
12. Hybrid Graphics Diagnostics
13. Presentation Pipeline Diagnostics
14. Driver/Profile Diagnostics

## Optimization
15. Windows Graphics Configuration
16. Per-App GPU Configuration
17. ReBAR Advisor
18. Driver Profile Recommendations

## Benchmark
19. GPU Performance Benchmark
20. GPU Stability Test
21. GPU Efficiency Benchmark

## Advanced
22. Vendor-Specific Tuning
23. GPU Deep Diagnostic Session

Ou seja:

**~168 discovery items → talvez ~20–25 capabilities/features compostas.**

Isso é exatamente o tipo de compressão que faremos posteriormente.

---

# Áreas que devem assumir aprofundamento

## Gaming
- Reflex;
- Anti-Lag;
- frame generation;
- DLSS/FSR/XeSS;
- VSync;
- VRR;
- frame limiting;
- presentation;
- latency;
- game-specific profiles.

## Display
- MPO;
- HDR;
- refresh;
- VRR;
- color;
- monitor routing.

## PCIe
- link;
- ReBAR;
- WHEA;
- eGPU.

## Energy
- dGPU residency;
- laptop power;
- performance modes.

## Drivers
- update;
- rollback;
- clean install;
- regression;
- profile persistence.

## Latency
- render queue;
- Reflex;
- Low Latency Mode;
- presentation delay.

---

# Avaliação de maturidade

A segunda passada encontrou principalmente:

- GPU memory budgeting;
- presentation pipeline;
- crash diagnostics;
- vendor telemetry;
- mobile GPU context;
- graphics configuration conflicts;
- compute/media capabilities.

Não apareceu nenhum novo grande domínio de GPU que justifique outra pesquisa ampla agora.

Pesquisas adicionais tenderiam a retornar:
- propriedades individuais de APIs;
- settings específicos de driver;
- detalhes que pertencem a Gaming/Display/Latency.

Estado sugerido:

**INITIAL DISCOVERY COMPLETE**