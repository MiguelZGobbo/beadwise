# Consolidated Discovery — GAMING

> Source: `gaming.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **57**
- Consolidated active capability groups: **14**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-GAMING-001 — Game discovery, identity & session context

Status: CANDIDATE

Type: Diagnostic

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Game Library Discovery — `CANDIDATE`
- [2] Game Process Identification — `CANDIDATE`
- [3] Game Session Detection — `CANDIDATE`
- [4] Game Runtime & Graphics API Detection — `CANDIDATE`
- [5] Game Capability Profile — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-002 — Windows gaming configuration diagnostics

Status: CANDIDATE

Type: Configuration / Optimization, Diagnostic, Diagnostic / Configuration, Diagnostic / Optimization

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [6] Windows Gaming Configuration Audit — `CANDIDATE`
- [7] Game Mode Evaluation — `CANDIDATE`
- [8] Gaming Capture & Recording Impact Diagnostics — `CANDIDATE`
- [10] Per-Game Windows Graphics Configuration — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-003 — Overlay, hook, mod & compatibility interference

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Safeguard

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [9] Overlay & Hook Interference Diagnostics — `CANDIDATE`
- [31] Anti-Cheat & Game Security Compatibility Awareness — `CANDIDATE`
- [52] Emulator & Compatibility-Layer Awareness — `CANDIDATE`
- [53] Modded Game & Injection Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-004 — Presentation mode, refresh, VRR & frame-cap diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Benchmark, Diagnostic / Configuration

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [11] Game Presentation Path Diagnostics — `CANDIDATE`
- [12] Windowed & Borderless Optimization Evaluation — `CANDIDATE`
- [13] Fullscreen / Borderless / Windowed Mode Comparison — `CANDIDATE`
- [14] Refresh Rate & Game Output Validation — `CANDIDATE`
- [15] Variable Refresh Rate Diagnostics — `CANDIDATE`
- [16] VSync / Tearing / Frame Cap Strategy — `CANDIDATE`
- [17] FPS Limiter Detection & Coordination — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-005 — Upscaling, frame generation & latency technologies

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [18] Game Upscaling Capability & Configuration Diagnostics — `CANDIDATE`
- [19] Frame Generation Awareness — `CANDIDATE`
- [20] Low-Latency Technology Diagnostics — `CANDIDATE`
- [43] Automatic Super Resolution Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-006 — Gaming performance monitoring & stutter analysis

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Benchmark, Diagnostic / Monitoring, Diagnostic / Optimization

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [21] Game FPS & Frame Time Monitoring — `CANDIDATE`
- [22] 1% Low / 0.1% Low & Percentile Performance — `CANDIDATE`
- [23] Frame Pacing & Stutter Analysis — `CANDIDATE`
- [24] Gaming Latency Measurement — `CANDIDATE`
- [25] CPU vs GPU Game Bottleneck Classification — `CANDIDATE`
- [26] Memory Pressure During Gaming Diagnostics — `CANDIDATE`
- [27] Storage-Induced Stutter Diagnostics — `CANDIDATE`
- [28] Shader Compilation & Pipeline Stutter Diagnostics — `CANDIDATE`
- [29] DirectStorage Gaming Diagnostics — `CANDIDATE`
- [30] Background Workload Impact During Gaming — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-007 — Game crash, hang & dependency health

Status: CANDIDATE

Type: Diagnostic / Knowledge, Diagnostic / Repair, Diagnostic / Repair Guidance

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [32] Game Crash & Hang Diagnostics — `CANDIDATE`
- [39] Game Installation & Runtime Dependency Health — `CANDIDATE`
- [40] Game File Integrity & Repair Orchestration — `CANDIDATE`
- [56] Game-Specific Known-Issue Context — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-008 — Per-game optimization profiles

Status: CANDIDATE

Type: Diagnostic / Safeguard, Optimization / Configuration

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [33] Per-Game Optimization Profile — `CANDIDATE`
- [34] Per-Game Profile Conflict Detection — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-009 — Gaming benchmark & before/after validation

Status: CANDIDATE

Type: Benchmark, Benchmark / Safeguard, Diagnostic / Benchmark

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [35] Gaming Before × After Benchmark — `CANDIDATE`
- [38] Game Optimization Outcome Classification — `CANDIDATE`
- [46] Sustained Gaming Performance & Thermal Soak Analysis — `CANDIDATE`
- [47] Gaming Benchmark Repeatability & Run Quality — `CANDIDATE`
- [48] Benchmark Scene & Workload Consistency — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-010 — Gaming change rollback & recommendation layer

Status: CANDIDATE

Type: Diagnostic / Quality of Life, Diagnostic / Recommendation Infrastructure, Repair / Safeguard

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [36] Gaming Change Rollback — `CANDIDATE`
- [37] Gaming Recommendation Explanation — `CANDIDATE`
- [57] Gaming Recommendation Confidence — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-011 — Game update/config/save change correlation

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Tool

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [41] Game Update & Environment Change Correlation — `CANDIDATE`
- [42] Game Configuration & Save Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-012 — Gaming hardware/context awareness

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Optimization

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [44] Multi-Monitor Gaming Context Diagnostics — `CANDIDATE`
- [45] Laptop & Handheld Gaming Context — `CANDIDATE`
- [51] VR / XR Gaming Compatibility & Performance Context — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-013 — Online/cloud gaming network correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [49] Online Gaming Network Correlation — `CANDIDATE`
- [50] Cloud Gaming & Remote Play Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-GAMING-014 — Gaming audio/input/accessibility safeguards

Status: CANDIDATE

Type: Diagnostic, Safeguard / Diagnostic

Technical Domain: GAMING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [54] Gaming Audio / Input Interference Correlation — `CANDIDATE`
- [55] Gaming Accessibility & Assistive-Technology Safeguards — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
