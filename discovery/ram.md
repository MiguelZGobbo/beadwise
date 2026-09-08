# Consolidated Discovery — RAM

> Source: `ram.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **140**
- Consolidated active capability groups: **17**
- Explicitly rejected source items kept outside active catalog: **4**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-RAM-001 — Memory capacity, modules & topology inventory

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Product Logic, Diagnostic / Recommendation, Product Behavior

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Physical Memory Capacity Detection — `CANDIDATE`
- [2] OS-Usable Memory Detection — `CANDIDATE`
- [3] Installed vs Usable RAM Difference Analysis — `CANDIDATE`
- [4] Memory Module Inventory — `CANDIDATE`
- [5] Memory Slot Population Detection — `CANDIDATE`
- [6] Asymmetric Module Configuration Detection — `CANDIDATE`
- [7] Memory Type Detection — `CANDIDATE`
- [8] Memory Data Rate Detection — `CANDIDATE`
- [9] Memory Clock vs Effective Data Rate Explanation — `CANDIDATE`
- [21] Memory Channel Topology Detection — `CANDIDATE`
- [22] Suboptimal DIMM Slot Placement Detection — `CANDIDATE`
- [23] Channel Imbalance Detection — `IDEA`
- [24] Rank Detection — `RESEARCHING`
- [88] DDR5 Architecture Awareness — `CANDIDATE`
- [89] DDR5 Subchannel Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-002 — SPD, JEDEC, XMP/EXPO profile diagnostics

Status: CANDIDATE

Type: Benchmark / Stability, Configuration Guidance, Diagnostic, Diagnostic / Recommendation, Product Infrastructure, Safety, Shared Diagnostic Capability

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [10] SPD Information Detection — `RESEARCHING`
- [11] JEDEC Profile Detection — `CANDIDATE`
- [12] Intel XMP Profile Detection — `CANDIDATE`
- [13] AMD EXPO Profile Detection — `CANDIDATE`
- [14] Memory Profile Active-State Detection — `CANDIDATE`
- [15] Memory Running Below Rated Profile Detection — `CANDIDATE`
- [16] XMP / EXPO Compatibility Check — `CANDIDATE`
- [17] XMP / EXPO Manual Guidance — `CANDIDATE`
- [18] XMP / EXPO Post-Activation Validation — `CANDIDATE`
- [19] Memory Timing Detection — `CANDIDATE`
- [20] Memory Timing Profile Comparison — `IDEA`
- [80] XMP / EXPO Stability Regression Detection — `CANDIDATE`
- [98] SPD Access Capability Detection — `CANDIDATE`
- [99] SPD Read Fallback Strategy — `CANDIDATE`
- [100] SMBus Access Conflict Safeguard — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-003 — ECC & physical-memory error diagnostics

Status: CANDIDATE

Type: Advanced Diagnostic, Diagnostic

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [25] ECC Capability Detection — `CANDIDATE`
- [26] ECC Error Monitoring — `CANDIDATE`
- [27] Predictive Bad Memory Page Detection — `IDEA`
- [127] Memory Fault Source Classification — `IDEA`
- [136] Memory Error Address Pattern Analysis — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-004 — Memory usage, pressure & commit diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Visualization, Monitoring

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [28] Total Physical Memory Usage — `CANDIDATE`
- [29] Available Memory Monitoring — `CANDIDATE`
- [30] Memory Pressure Detection — `CANDIDATE`
- [31] System Commit Charge Monitoring — `CANDIDATE`
- [32] Commit Limit Detection — `CANDIDATE`
- [33] Commit Pressure Detection — `CANDIDATE`
- [34] Commit Peak History — `CANDIDATE`
- [43] Private Memory / Commit by Process — `CANDIDATE`
- [63] Memory Composition View — `CANDIDATE`
- [66] Out-of-Memory Risk Detection — `CANDIDATE`
- [67] Low-Memory Incident Capture — `IDEA`
- [68] Memory Pressure Timeline — `IDEA`
- [106] Resource Exhaustion Event Detection — `CANDIDATE`
- [107] Low Virtual Memory Incident Attribution — `CANDIDATE`
- [108] Resource Exhaustion Timeline Correlation — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-005 — Pagefile & crash-dump configuration diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Recommendation, Recommendation

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [35] Pagefile Presence Detection — `CANDIDATE`
- [36] Pagefile Configuration Detection — `CANDIDATE`
- [37] Pagefile Requirement Analysis — `CANDIDATE`
- [38] System-Managed Pagefile Recommendation — `CANDIDATE`
- [39] Disabled Pagefile Risk Detection — `CANDIDATE`
- [40] Pagefile Placement Analysis — `RESEARCHING`
- [41] Crash Dump Memory Requirement Analysis — `CANDIDATE`
- [122] ClearPageFileAtShutdown Detection — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-006 — Per-process memory & leak diagnostics

Status: CANDIDATE

Type: Diagnostic, Monitoring

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [42] Working Set Monitoring — `CANDIDATE`
- [44] Process Memory Growth Detection — `CANDIDATE`
- [45] Memory Leak Candidate Detection — `CANDIDATE`
- [64] Memory Usage by Process Ranking — `CANDIDATE`
- [65] Memory Usage Anomaly Detection — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-007 — Kernel pool & driver leak diagnostics

Status: CANDIDATE

Type: Advanced Diagnostic, Advanced Diagnostic Tool, Diagnostic

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [46] Paged Pool Monitoring — `CANDIDATE`
- [47] Nonpaged Pool Monitoring — `CANDIDATE`
- [48] Kernel Pool Leak Detection — `CANDIDATE`
- [109] Driver Pool Tag Monitoring — `CANDIDATE`
- [110] Pool Tag Growth Detection — `CANDIDATE`
- [111] Pool Tag to Driver Mapping — `CANDIDATE`
- [112] Kernel Memory Leak Diagnostic Session — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-008 — Standby/cache/compression diagnostics

Status: CANDIDATE

Type: Advanced Diagnostic, Configuration, Diagnostic, Optimization, Product Behavior

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [49] Standby Memory Observation — `CANDIDATE`
- [50] Standby Cache Explanation — `CANDIDATE`
- [51] Standby List Clear — `QUESTIONABLE`
- [53] Memory Compression State Detection — `CANDIDATE`
- [54] Memory Compression Impact Analysis — `RESEARCHING`
- [55] Disable Memory Compression — `QUESTIONABLE`
- [61] System Cache Size Observation — `CANDIDATE`
- [62] Modified Page List Observation — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-009 — Memory-manager tweak/default-state safeguards

Status: CANDIDATE

Type: Diagnostic, Repair / Diagnostic, Safety, Safety / Product Principle

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [52] RAM Cleaner Myth Guard — `CANDIDATE`
- [87] Memory Optimization Myth Guard — `CANDIDATE`
- [117] DisablePagingExecutive State Detection — `CANDIDATE`
- [119] Legacy Memory Registry Tweak Detection — `CANDIDATE`
- [120] LargeSystemCache State Detection — `CANDIDATE`
- [124] Memory Manager Default-State Advisor — `IDEA`
- [125] Memory Working Set Trim Detection — `RESEARCHING`
- [126] Working Set Trim Abuse Guard — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-010 — Paging & hard-fault diagnostics

Status: CANDIDATE

Type: Advanced Diagnostic, Diagnostic, Safety / Diagnostic Logic

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [56] Paging Activity Monitoring — `CANDIDATE`
- [57] Pages Output Rate — `CANDIDATE`
- [58] Pages/sec Misinterpretation Guard — `CANDIDATE`
- [59] Hard Fault Monitoring — `CANDIDATE`
- [60] Hard Fault / Stutter Correlation — `IDEA`
- [128] Page Fault ETW Trace — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-011 — Memory benchmark suite

Status: CANDIDATE

Type: Benchmark, Benchmark Safeguard

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [69] Memory Bandwidth Benchmark — `CANDIDATE`
- [70] Memory Latency Benchmark — `CANDIDATE`
- [71] Memory Bandwidth Scaling Test — `IDEA`
- [72] Cache vs Main Memory Benchmark Separation — `CANDIDATE`
- [81] Memory Benchmark Before × After — `CANDIDATE`
- [97] Memory Controller Ratio Benchmark — `IDEA`
- [130] Loaded Memory Latency Test — `IDEA`
- [131] Random Memory Access Benchmark — `CANDIDATE`
- [132] Memory Benchmark Topology Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-012 — NUMA & reserved-memory diagnostics

Status: CANDIDATE

Type: Advanced Diagnostic, Diagnostic

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [73] NUMA Memory Topology — `CANDIDATE`
- [74] Remote NUMA Memory Access Analysis — `IDEA`
- [75] Integrated GPU Memory Reservation Analysis — `CANDIDATE`
- [76] Hardware Reserved Memory Anomaly Detection — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-013 — Memory stability testing & validation

Status: CANDIDATE

Type: Benchmark / Product Logic, Diagnostic / Benchmark, Diagnostic Guidance, Product Behavior, Safety, Tool / Diagnostic Guidance, Validation

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [77] Memory Stability Test — `CANDIDATE`
- [78] Windows Memory Diagnostic Integration — `CANDIDATE`
- [79] Memory Test WHEA Monitoring — `CANDIDATE`
- [82] Memory Performance / Stability Combined Result — `CANDIDATE`
- [133] Memory Stability Error Counter — `IDEA`
- [134] In-Windows Memory Stress Test — `CANDIDATE`
- [135] Offline Memory Test Recommendation — `CANDIDATE`
- [137] Memory Stability Confidence — `CANDIDATE`
- [138] Memory Configuration Change Validation — `CANDIDATE`
- [139] Memory Optimization Rejection on Error — `CANDIDATE`
- [140] Memory Optimization Meaningful Gain Validation — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-014 — Memory capacity adequacy & upgrade guidance

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Explainability, Diagnostic / Product Behavior, Diagnostic / Recommendation

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [83] Memory Upgrade Need Detection — `IDEA`
- [84] Memory Capacity Adequacy Classification — `IDEA`
- [85] Memory Configuration Health Summary — `CANDIDATE`
- [86] “High RAM Usage” Explanation Engine — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-015 — Memory-controller, voltage & training context

Status: CANDIDATE

Type: Advanced Tuning, Diagnostic, Diagnostic / Benchmark

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [90] Memory Controller Ratio Detection — `RESEARCHING`
- [91] Intel Memory Gear Mode Detection — `CANDIDATE`
- [92] Intel Gear Mode Performance Context — `CANDIDATE`
- [93] Intel Gear Mode Modification — `QUESTIONABLE`
- [94] AMD MCLK Detection — `RESEARCHING`
- [95] AMD UCLK Detection — `RESEARCHING`
- [96] AMD Fabric Clock Context — `RESEARCHING`
- [101] DDR5 PMIC Information — `IDEA`
- [102] Memory Voltage Detection — `RESEARCHING`
- [103] Memory Training State Context — `IDEA`
- [104] Memory Training Failure / Fallback Detection — `RESEARCHING`
- [105] Memory Context Restore Awareness — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-016 — Large-page capability & application guidance

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration Guidance

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [113] Large Page Capability Detection — `CANDIDATE`
- [114] Large Page Application Usage Detection — `RESEARCHING`
- [116] Large Page Application Guidance — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-RAM-017 — Memory-bound workload diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: RAM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [129] Memory-Bound Workload Detection — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

# Rejected / removed from active capability catalog

> These items remain recorded for traceability but do not proceed as active capabilities.

## R-RAM-115 — Force Large Pages Optimization

Original status: `REJECTED`

Decision: Keep rejected; do not promote to Feature Spec unless new evidence materially changes the assessment.

## R-RAM-118 — DisablePagingExecutive Optimization

Original status: `REJECTED`

Decision: Keep rejected; do not promote to Feature Spec unless new evidence materially changes the assessment.

## R-RAM-121 — LargeSystemCache Gaming Optimization

Original status: `REJECTED`

Decision: Keep rejected; do not promote to Feature Spec unless new evidence materially changes the assessment.

## R-RAM-123 — Clear Pagefile for Performance

Original status: `REJECTED`

Decision: Keep rejected; do not promote to Feature Spec unless new evidence materially changes the assessment.
