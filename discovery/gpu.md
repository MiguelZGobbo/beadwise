# Final Discovery — GPU

> Canonical rules: `01-DISCOVERY.md`
> Scope boundary: `02-FEATURE-SPEC-TEMPLATE.md` is used only to keep implementation research out of Discovery.

## Final review result

- Raw discovery items represented: **168**
- Consolidated capability groups: **17**
- Active non-duplicate groups: **16**
- `READY_FOR_SPEC`: **15**
- `CANDIDATE`: **0**
- `QUESTIONABLE`: **1**
- `DUPLICATE`: **1**

> A status decision is conceptual only. `READY_FOR_SPEC` does not imply implementation, API, Registry, rollback, benchmark proof, test or compatibility method already resolved.

## C-GPU-001 — GPU identity, inventory & architecture

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para detectar e contextualizar **gpu identity, inventory & architecture**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [1] GPU Adapter Inventory — `CANDIDATE`
- [2] GPU Vendor Detection — `CANDIDATE`
- [3] GPU Hardware ID Detection — `CANDIDATE`
- [4] GPU Model Identification — `CANDIDATE`
- [5] GPU Architecture Detection — `RESEARCHING`
- [6] GPU Integrated / Discrete Detection — `CANDIDATE`
- [7] Detachable / External GPU Detection — `CANDIDATE`
- [8] Physical GPU Count — `CANDIDATE`
- [9] GPU Engine Enumeration — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-002 — GPU memory capacity, usage & pressure

Status: READY_FOR_SPEC

Type: Diagnostic, Monitoring

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **gpu memory capacity, usage & pressure**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [10] Dedicated VRAM Detection — `CANDIDATE`
- [11] Dedicated System Graphics Memory Detection — `CANDIDATE`
- [12] Shared System Graphics Memory Detection — `CANDIDATE`
- [13] VRAM Usage Monitoring — `CANDIDATE`
- [14] Shared GPU Memory Monitoring — `CANDIDATE`
- [15] VRAM Pressure Detection — `CANDIDATE`
- [16] VRAM Spillover Detection — `CANDIDATE`
- [17] Per-Process GPU Memory Usage — `CANDIDATE`
- [86] VRAM Pressure / Frametime Correlation — `CANDIDATE`
- [140] GPU Video Memory Budget Diagnostics — `CANDIDATE`
- [141] GPU Memory Residency Pressure Analysis — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-003 — GPU utilization, clocks & telemetry

Status: READY_FOR_SPEC

Type: Diagnostic, Monitoring

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para diagnosticar **gpu utilization, clocks & telemetry**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais estados são apenas informativos e quais alterações, se alguma, têm benefício contextual suficiente para serem investigadas tecnicamente na Feature Spec?

Origem / itens consolidados:
- [18] GPU Utilization Monitoring — `CANDIDATE`
- [19] Per-Engine GPU Utilization — `CANDIDATE`
- [20] GPU Usage by Process — `CANDIDATE`
- [21] GPU Clock Monitoring — `CANDIDATE`
- [22] GPU Memory Clock Monitoring — `CANDIDATE`
- [23] Effective GPU Clock Analysis — `RESEARCHING`
- [27] GPU Fan Speed Monitoring — `CANDIDATE`
- [28] GPU Fan Stop State — `IDEA`
- [29] GPU Board Power Monitoring — `CANDIDATE`
- [30] GPU Voltage Monitoring — `RESEARCHING`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-004 — GPU thermal, power & limiter diagnostics

Status: READY_FOR_SPEC

Type: Advanced Tuning, Diagnostic, Monitoring, Validation

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para diagnosticar **gpu thermal, power & limiter diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais estados são apenas informativos e quais alterações, se alguma, têm benefício contextual suficiente para serem investigadas tecnicamente na Feature Spec?

Origem / itens consolidados:
- [24] GPU Temperature Monitoring — `CANDIDATE`
- [25] GPU Hotspot Temperature — `RESEARCHING`
- [26] GPU Memory Temperature — `RESEARCHING`
- [31] GPU Power Limit Detection — `CANDIDATE`
- [32] GPU Thermal Limit Detection — `CANDIDATE`
- [33] GPU Voltage / Reliability Limiter Context — `RESEARCHING`
- [34] GPU Performance Limiter Classification — `CANDIDATE`
- [101] GPU Temperature Stability Validation — `CANDIDATE`
- [131] GPU Power Limit Modification — `QUESTIONABLE`
- [152] GPU Limiter Diagnostics — `CANDIDATE`
- [153] Laptop GPU Power Envelope Context — `CANDIDATE`

Limite conceitual:
O `READY_FOR_SPEC` vale para a responsabilidade principal descrita nesta capability. Subitens de origem marcados `QUESTIONABLE` continuam questionáveis e **não** são aprovados implicitamente; qualquer alteração agressiva neles representada permanece fora do escopo ativo até justificativa própria.

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-005 — GPU driver health & crash diagnostics

Status: READY_FOR_SPEC

Type: Advanced Diagnostic, Diagnostic, Recommendation, Repair / Guidance, Safety, Validation

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **gpu driver health & crash diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [35] GPU Driver Version Detection — `CANDIDATE`
- [36] GPU Driver Provider Detection — `CANDIDATE`
- [37] WDDM Version Detection — `CANDIDATE`
- [38] Graphics Driver Model Capability Check — `CANDIDATE`
- [39] GPU Driver Age Context — `IDEA`
- [40] GPU Driver Update Recommendation — `CANDIDATE`
- [41] GPU Driver Regression Detection — `IDEA`
- [42] GPU Driver Clean Install Advisor — `RESEARCHING`
- [43] Display Driver Device Error Detection — `CANDIDATE`
- [44] Graphics Driver Crash History — `CANDIDATE`
- [45] TDR Event Detection — `CANDIDATE`
- [46] TDR Frequency Analysis — `CANDIDATE`
- [47] TDR Context Correlation — `IDEA`
- [48] TdrDelay Tweak Guard — `CANDIDATE`
- [49] GPU Crash Dump / Live Dump Awareness — `IDEA`
- [100] GPU WHEA/TDR Monitoring During Test — `CANDIDATE`
- [134] GPU Optimization Rejection on TDR — `CANDIDATE`
- [148] GPU Device Removed Diagnostics — `CANDIDATE`
- [149] DRED Crash Context — `CANDIDATE`
- [150] GPU Crash Incident Correlation — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-006 — Graphics API & rendering capability inventory

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para detectar e contextualizar **graphics api & rendering capability inventory**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [50] DirectX Runtime Capability Detection — `CANDIDATE`
- [51] Direct3D Feature Level Detection — `CANDIDATE`
- [52] DirectX 12 Capability — `CANDIDATE`
- [53] Shader Model Detection — `CANDIDATE`
- [54] DirectX Ultimate Feature Detection — `IDEA`
- [55] Vulkan Capability Detection — `CANDIDATE`
- [56] OpenGL Capability Detection — `CANDIDATE`
- [57] Graphics Runtime Health — `CANDIDATE`
- [159] GPU Media Capability Inventory — `CANDIDATE`
- [160] GPU Compute Capability Inventory — `CANDIDATE`
- [161] Modern Rendering Feature Inventory — `CANDIDATE`
- [162] Upscaling / Frame Generation Capability Context — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-007 — GPU scheduling & presentation diagnostics

Status: READY_FOR_SPEC

Type: Advanced Diagnostic, Benchmark, Diagnostic, Monitoring / Diagnostic, Safety

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **gpu scheduling & presentation diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [58] Hardware-Accelerated GPU Scheduling Detection — `CANDIDATE`
- [59] HAGS Performance Test — `CANDIDATE`
- [60] HAGS Universal Optimization Guard — `CANDIDATE`
- [142] GPU Scheduler / Engine Trace — `CANDIDATE`
- [143] Graphics Presentation Pipeline Diagnostics — `CANDIDATE`
- [144] Present Mode / Frame Presentation Monitoring — `CANDIDATE`
- [145] Multi-Plane Overlay Capability / Health — `CANDIDATE`
- [146] MPO Disable Tweak Guard — `CANDIDATE`
- [147] VRR / Tearing Presentation Capability — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-008 — Per-app GPU selection & hybrid graphics routing

Status: READY_FOR_SPEC

Type: Configuration, Diagnostic, Monitoring, Recommendation

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **per-app gpu selection & hybrid graphics routing**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [61] Per-App GPU Preference Detection — `CANDIDATE`
- [62] Per-App GPU Preference Configuration — `CANDIDATE`
- [63] Wrong-GPU Execution Detection — `CANDIDATE`
- [64] Hybrid Graphics Topology — `CANDIDATE`
- [65] Hybrid Graphics Runtime Usage — `CANDIDATE`
- [66] dGPU Idle Residency Diagnosis — `IDEA`
- [67] External Display GPU Routing Context — `IDEA`
- [68] MUX Capability Detection — `RESEARCHING`
- [69] MUX Mode Advisor — `IDEA`
- [154] Hybrid Graphics Routing Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-009 — Resizable BAR readiness & context

Status: READY_FOR_SPEC

Type: Configuration Guidance, Diagnostic, Explainability

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para detectar e contextualizar **resizable bar readiness & context**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [70] Resizable BAR Capability Detection — `CANDIDATE`
- [71] Resizable BAR Active-State Detection — `CANDIDATE`
- [72] Resizable BAR Platform Readiness — `CANDIDATE`
- [73] Resizable BAR Manual Guidance — `CANDIDATE`
- [74] Intel Arc ReBAR Requirement Detection — `CANDIDATE`
- [75] ReBAR Benefit Context — `CANDIDATE`
- [122] AMD Smart Access Memory Detection — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-010 — GPU PCIe link & transfer diagnostics

Status: READY_FOR_SPEC

Type: Benchmark / Diagnostic, Diagnostic

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para diagnosticar **gpu pcie link & transfer diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [76] PCIe Link Generation Detection — `CANDIDATE`
- [77] PCIe Link Width Detection — `CANDIDATE`
- [78] GPU PCIe Link Bottleneck Detection — `CANDIDATE`
- [79] PCIe Link Dynamic-State Awareness — `CANDIDATE`
- [80] GPU PCIe Error Correlation — `IDEA`
- [163] PCIe GPU Transfer Diagnostics — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-011 — GPU bottleneck, frametime & stutter diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Monitoring

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para diagnosticar **gpu bottleneck, frametime & stutter diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [81] GPU Bottleneck Detection — `CANDIDATE`
- [82] CPU-Limited GPU Underutilization Detection — `CANDIDATE`
- [83] Frame Cap / VSync Limited Detection — `IDEA`
- [84] GPU Performance Headroom — `IDEA`
- [85] GPU Frametime Correlation — `CANDIDATE`
- [87] Shader Compilation Stutter Context — `IDEA`
- [91] GPU API Workload Identification — `IDEA`
- [92] GPU Compute Workload Detection — `CANDIDATE`
- [93] Video Decode Utilization — `CANDIDATE`
- [94] Video Encode Utilization — `CANDIDATE`
- [95] GPU Copy Engine Utilization — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-012 — Shader cache diagnostics & safeguards

Status: READY_FOR_SPEC

Type: Configuration, Diagnostic, Diagnostic / Configuration, Safety

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **shader cache diagnostics & safeguards**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Evita alterações populares, agressivas ou cosméticas que não apresentem benefício técnico proporcional.

Aplicabilidade:
quando uma configuração ou recomendação puder ser confundida com otimização universal.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [88] Shader Cache State Awareness — `CANDIDATE`
- [89] Shader Cache Cleanup Guard — `CANDIDATE`
- [90] Shader Cache Size Tuning — `RESEARCHING`
- [113] NVIDIA Shader Cache Size State — `RESEARCHING`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-013 — GPU benchmark suite

Status: READY_FOR_SPEC

Type: Benchmark, Benchmark Architecture, Benchmark Safeguard

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para medir e comparar **gpu benchmark suite**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Fornecer uma medição reproduzível e interpretável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Medição comparável para compreender desempenho e validar se uma mudança realmente produziu efeito.

Aplicabilidade:
quando for necessário quantificar desempenho, regressão ou resultado de uma alteração.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [96] GPU Benchmark Readiness — `CANDIDATE`
- [97] GPU Synthetic Benchmark — `CANDIDATE`
- [98] GPU Sustained Performance Benchmark — `CANDIDATE`
- [104] GPU Performance-per-Watt Benchmark — `IDEA`
- [105] GPU Thermal Efficiency Comparison — `IDEA`
- [164] GPU Memory Performance Benchmark — `IDEA`
- [165] GPU Benchmark Workload Classification — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-014 — GPU stability & tuning validation

Status: READY_FOR_SPEC

Type: Benchmark, Benchmark / Diagnostic, Diagnostic, Product Behavior, Safety, Validation

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **gpu stability & tuning validation**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
Quais estados são apenas informativos e quais alterações, se alguma, têm benefício contextual suficiente para serem investigadas tecnicamente na Feature Spec?

Origem / itens consolidados:
- [99] GPU Stability Test — `CANDIDATE`
- [102] GPU Performance Variability — `CANDIDATE`
- [103] GPU Before × After Validation — `CANDIDATE`
- [132] GPU Tuning Stability Validation — `CANDIDATE`
- [133] GPU Artifact Detection — `RESEARCHING`
- [135] GPU Optimization Rejection on Thermal Cost — `IDEA`
- [136] GPU Multi-Metric Optimization Result — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-015 — GPU vendor capability & profile layer

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Configuration, Infrastructure, Shared Capability

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **gpu vendor capability & profile layer**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [106] NVIDIA Capability Layer — `CANDIDATE`
- [107] AMD GPU Capability Layer — `CANDIDATE`
- [108] Intel GPU Capability Layer — `CANDIDATE`
- [109] Vendor Sensor API Abstraction — `CANDIDATE`
- [110] NVIDIA Driver Profile Inventory — `CANDIDATE`
- [111] NVIDIA Profile Change Detection — `IDEA`
- [112] NVIDIA Threaded Optimization State — `RESEARCHING`
- [114] NVIDIA Power Management Mode — `RESEARCHING`
- [115] NVIDIA Low Latency Mode — `RESEARCHING`
- [116] NVIDIA Reflex Awareness — `CANDIDATE`
- [117] NVIDIA Frame Rate Limiter Context — `IDEA`
- [118] AMD Driver Profile Inventory — `CANDIDATE`
- [119] AMD Anti-Lag Context — `RESEARCHING`
- [120] AMD Radeon Chill Context — `CANDIDATE`
- [121] AMD Enhanced Sync Context — `RESEARCHING`
- [123] Intel Arc Driver Profile Inventory — `RESEARCHING`
- [124] Intel Arc Performance Readiness — `CANDIDATE`
- [151] Vendor Performance Telemetry Layer — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-GPU-016 — GPU tuning state, conflicts & controls

Status: QUESTIONABLE

Type: Advanced Tuning, Diagnostic, Repair

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **gpu tuning state, conflicts & controls**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
O benefício real é suficiente para justificar uma capability independente? Quais limites impedem que se transforme em tweak pack, preferência ou tuning de risco desproporcional?

Origem / itens consolidados:
- [125] GPU Driver Profile Default Restore — `CANDIDATE`
- [126] GPU Tweak Residue Detection — `IDEA`
- [127] GPU Overclock State Detection — `RESEARCHING`
- [128] GPU Undervolt State Detection — `RESEARCHING`
- [129] GPU Overclock / Undervolt Modification — `QUESTIONABLE`
- [130] GPU Fan Curve Modification — `QUESTIONABLE`
- [155] Graphics Configuration Conflict Detection — `CANDIDATE`
- [156] GPU Driver Profile Baseline / Diff — `CANDIDATE`
- [157] GPU Configuration Provenance — `IDEA`
- [158] GPU Tuning Tool Conflict Detection — `CANDIDATE`

Decisão de revisão:
Não avançar ainda. O grupo contém risco de tuning agressivo, benefício incerto ou comportamento que pode virar preferência/tweak pack; precisa de justificativa conceitual adicional antes de Feature Spec.

## C-GPU-017 — GPU diagnostic summary & evidence gate

Status: DUPLICATE

Type: Diagnostic, Diagnostic / Product Behavior, Diagnostic Tool, Explainability, Product Behavior / Safety, Safety / Infrastructure

Technical Domain: GPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para correlacionar e contextualizar **gpu diagnostic summary & evidence gate**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Ajudar a distinguir coincidência de causa provável e explicar mudanças ao longo do tempo, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando histórico, comparação ou múltiplas evidências forem necessárias para interpretar um problema.

Dúvidas:
Ownership consolidado em C-SYSTEM-002 / C-SYSTEM-003. Esta entrada não deve gerar Feature Spec independente.

Duplicate of:
C-SYSTEM-002 / C-SYSTEM-003

Origem / itens consolidados:
- [137] GPU Health Summary — `CANDIDATE`
- [138] GPU Performance Summary — `CANDIDATE`
- [139] GPU Configuration Explain Why — `CANDIDATE`
- [166] GPU Monitoring Overhead Safeguard — `CANDIDATE`
- [167] GPU Optimization Evidence Gate — `CANDIDATE`
- [168] GPU Diagnostic Session — `CANDIDATE`

Decisão de revisão:
Não manter como capability ativa independente. O comportamento é coberto por C-SYSTEM-002 / C-SYSTEM-003; esta entrada permanece apenas para registrar a decisão de ownership.
