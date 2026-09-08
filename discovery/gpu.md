# Consolidated Discovery — GPU

> Source: `gpu.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **168**
- Consolidated active capability groups: **17**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-GPU-001 — GPU identity, inventory & architecture

Status: CANDIDATE

Type: Diagnostic

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] GPU Adapter Inventory — `CANDIDATE`
- [2] GPU Vendor Detection — `CANDIDATE`
- [3] GPU Hardware ID Detection — `CANDIDATE`
- [4] GPU Model Identification — `CANDIDATE`
- [5] GPU Architecture Detection — `RESEARCHING`
- [6] GPU Integrated / Discrete Detection — `CANDIDATE`
- [7] Detachable / External GPU Detection — `CANDIDATE`
- [8] Physical GPU Count — `CANDIDATE`
- [9] GPU Engine Enumeration — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-002 — GPU memory capacity, usage & pressure

Status: CANDIDATE

Type: Diagnostic, Monitoring

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
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

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-003 — GPU utilization, clocks & telemetry

Status: CANDIDATE

Type: Diagnostic, Monitoring

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
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

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-004 — GPU thermal, power & limiter diagnostics

Status: CANDIDATE

Type: Advanced Tuning, Diagnostic, Monitoring, Validation

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
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

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-005 — GPU driver health & crash diagnostics

Status: CANDIDATE

Type: Advanced Diagnostic, Diagnostic, Recommendation, Repair / Guidance, Safety, Validation

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
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

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-006 — Graphics API & rendering capability inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
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

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-007 — GPU scheduling & presentation diagnostics

Status: CANDIDATE

Type: Advanced Diagnostic, Benchmark, Diagnostic, Monitoring / Diagnostic, Safety

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [58] Hardware-Accelerated GPU Scheduling Detection — `CANDIDATE`
- [59] HAGS Performance Test — `CANDIDATE`
- [60] HAGS Universal Optimization Guard — `CANDIDATE`
- [142] GPU Scheduler / Engine Trace — `CANDIDATE`
- [143] Graphics Presentation Pipeline Diagnostics — `CANDIDATE`
- [144] Present Mode / Frame Presentation Monitoring — `CANDIDATE`
- [145] Multi-Plane Overlay Capability / Health — `CANDIDATE`
- [146] MPO Disable Tweak Guard — `CANDIDATE`
- [147] VRR / Tearing Presentation Capability — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-008 — Per-app GPU selection & hybrid graphics routing

Status: CANDIDATE

Type: Configuration, Diagnostic, Monitoring, Recommendation

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
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

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-009 — Resizable BAR readiness & context

Status: CANDIDATE

Type: Configuration Guidance, Diagnostic, Explainability

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [70] Resizable BAR Capability Detection — `CANDIDATE`
- [71] Resizable BAR Active-State Detection — `CANDIDATE`
- [72] Resizable BAR Platform Readiness — `CANDIDATE`
- [73] Resizable BAR Manual Guidance — `CANDIDATE`
- [74] Intel Arc ReBAR Requirement Detection — `CANDIDATE`
- [75] ReBAR Benefit Context — `CANDIDATE`
- [122] AMD Smart Access Memory Detection — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-010 — GPU PCIe link & transfer diagnostics

Status: CANDIDATE

Type: Benchmark / Diagnostic, Diagnostic

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [76] PCIe Link Generation Detection — `CANDIDATE`
- [77] PCIe Link Width Detection — `CANDIDATE`
- [78] GPU PCIe Link Bottleneck Detection — `CANDIDATE`
- [79] PCIe Link Dynamic-State Awareness — `CANDIDATE`
- [80] GPU PCIe Error Correlation — `IDEA`
- [163] PCIe GPU Transfer Diagnostics — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-011 — GPU bottleneck, frametime & stutter diagnostics

Status: CANDIDATE

Type: Diagnostic, Monitoring

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
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

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-012 — Shader cache diagnostics & safeguards

Status: CANDIDATE

Type: Configuration, Diagnostic, Diagnostic / Configuration, Safety

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [88] Shader Cache State Awareness — `CANDIDATE`
- [89] Shader Cache Cleanup Guard — `CANDIDATE`
- [90] Shader Cache Size Tuning — `RESEARCHING`
- [113] NVIDIA Shader Cache Size State — `RESEARCHING`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-013 — GPU benchmark suite

Status: CANDIDATE

Type: Benchmark, Benchmark Architecture, Benchmark Safeguard

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [96] GPU Benchmark Readiness — `CANDIDATE`
- [97] GPU Synthetic Benchmark — `CANDIDATE`
- [98] GPU Sustained Performance Benchmark — `CANDIDATE`
- [104] GPU Performance-per-Watt Benchmark — `IDEA`
- [105] GPU Thermal Efficiency Comparison — `IDEA`
- [164] GPU Memory Performance Benchmark — `IDEA`
- [165] GPU Benchmark Workload Classification — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-014 — GPU stability & tuning validation

Status: CANDIDATE

Type: Benchmark, Benchmark / Diagnostic, Diagnostic, Product Behavior, Safety, Validation

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [99] GPU Stability Test — `CANDIDATE`
- [102] GPU Performance Variability — `CANDIDATE`
- [103] GPU Before × After Validation — `CANDIDATE`
- [132] GPU Tuning Stability Validation — `CANDIDATE`
- [133] GPU Artifact Detection — `RESEARCHING`
- [135] GPU Optimization Rejection on Thermal Cost — `IDEA`
- [136] GPU Multi-Metric Optimization Result — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-015 — GPU vendor capability & profile layer

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration, Infrastructure, Shared Capability

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
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

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-016 — GPU tuning state, conflicts & controls

Status: CANDIDATE

Type: Advanced Tuning, Diagnostic, Repair

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
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

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GPU-017 — GPU diagnostic summary & evidence gate

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Product Behavior, Diagnostic Tool, Explainability, Product Behavior / Safety, Safety / Infrastructure

Technical Domain: GPU

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [137] GPU Health Summary — `CANDIDATE`
- [138] GPU Performance Summary — `CANDIDATE`
- [139] GPU Configuration Explain Why — `CANDIDATE`
- [166] GPU Monitoring Overhead Safeguard — `CANDIDATE`
- [167] GPU Optimization Evidence Gate — `CANDIDATE`
- [168] GPU Diagnostic Session — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
