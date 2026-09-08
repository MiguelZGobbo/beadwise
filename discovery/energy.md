# Consolidated Discovery — ENERGY

> Source: `energy.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **100**
- Consolidated active capability groups: **13**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-ENERGY-001 — Power scheme inventory, switching & state

Status: CANDIDATE

Type: Configuration / Optimization, Diagnostic, Diagnostic / Configuration, Diagnostic / Product Behavior, Diagnostic / Repair, Monitoring, Safety / Configuration

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Power Scheme Inventory — `CANDIDATE`
- [2] Power Scheme Switching — `CANDIDATE`
- [3] Power Scheme Backup / Restore — `CANDIDATE`
- [4] Power Scheme Configuration Diff — `IDEA`
- [5] Broken / Anomalous Power Scheme Detection — `IDEA`
- [6] Custom BeadWise Power Profile — `RESEARCHING`
- [7] Ultimate Performance Availability — `CANDIDATE`
- [61] User-Configured Power Mode Detection — `CANDIDATE`
- [62] AC Power Mode Detection — `CANDIDATE`
- [63] DC Power Mode Detection — `CANDIDATE`
- [64] Effective Power Mode Detection — `CANDIDATE`
- [65] Power Mode Change Monitoring — `IDEA`
- [66] Power Scheme × Power Mode Interaction Analysis — `CANDIDATE`
- [67] OEM Power Overlay Detection — `RESEARCHING`
- [68] Effective Power Configuration Explanation — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-ENERGY-002 — Dynamic workload power profiles

Status: CANDIDATE

Type: Configuration, Diagnostic, Optimization / Automation

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [8] AC × Battery Policy Detection — `CANDIDATE`
- [9] AC/Battery Profile Switching — `IDEA`
- [59] Dynamic Workload Power Profile — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-ENERGY-003 — Processor power policy diagnostics & tuning

Status: CANDIDATE

Type: Configuration / Optimization, Diagnostic, Diagnostic / Configuration, Optimization / Configuration, Optimization / Recommendation

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [10] Processor Minimum Performance State — `CANDIDATE`
- [11] Processor Maximum Performance State — `CANDIDATE`
- [12] Processor Boost Mode Detection — `CANDIDATE`
- [13] Processor Boost Policy Tuning — `RESEARCHING`
- [14] Energy Performance Preference — EPP Detection — `CANDIDATE`
- [15] EPP Tuning — `RESEARCHING`
- [16] CPPC / Hardware Autonomous Performance Detection — `CANDIDATE`
- [17] Core Parking State Detection — `CANDIDATE`
- [18] Core Parking Policy Tuning — `RESEARCHING`
- [19] Processor Idle State Diagnostics — `IDEA`
- [53] Power Throttling State Analysis — `RESEARCHING`
- [69] EcoQoS Capability Detection — `CANDIDATE`
- [70] Process EcoQoS State Detection — `RESEARCHING`
- [71] Background Work EcoQoS Advisor — `IDEA`
- [72] Power Throttling Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-ENERGY-004 — Energy efficiency & performance-per-watt analysis

Status: CANDIDATE

Type: Benchmark, Benchmark / Diagnostic, Diagnostic

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [20] Idle Power Consumption Diagnosis — `CANDIDATE`
- [42] Energy Efficiency Scan — `CANDIDATE`
- [55] Performance-per-Watt Benchmark — `IDEA`
- [56] Thermal / Power Efficiency Comparison — `IDEA`
- [57] Power-Limited Performance Detection — `CANDIDATE`
- [58] Performance Regression Caused by Energy Policy — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-ENERGY-005 — Sleep, wake, hibernate & fast-startup diagnostics

Status: CANDIDATE

Type: Configuration, Configuration / Storage, Diagnostic, Diagnostic / Configuration, Diagnostic / Repair

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [21] Power Requests Detection — `CANDIDATE`
- [22] Power Request Override Advisor — `RESEARCHING`
- [23] Available Sleep States Detection — `CANDIDATE`
- [24] Modern Standby Detection — `CANDIDATE`
- [25] Modern Standby Quality Analysis — `CANDIDATE`
- [26] Sleep Transition Diagnostics — `CANDIDATE`
- [27] Last Wake Source Detection — `CANDIDATE`
- [28] Wake-Capable Device Inventory — `CANDIDATE`
- [29] Device Wake Permission Management — `CANDIDATE`
- [30] Wake Timer Detection — `CANDIDATE`
- [31] Wake Timer Policy Management — `RESEARCHING`
- [32] Hibernate Availability / Configuration — `CANDIDATE`
- [33] Hibernation File Configuration — `RESEARCHING`
- [34] Fast Startup State Detection — `CANDIDATE`
- [35] Fast Startup Configuration — `RESEARCHING`
- [41] Sleep Timeout Policy — `CANDIDATE`
- [78] Modern Standby DRIPS Residency Analysis — `CANDIDATE`
- [79] Poor DRIPS Residency Detection — `CANDIDATE`
- [80] Modern Standby Offender Detection — `CANDIDATE`
- [81] Modern Standby Driver Problem Detection — `CANDIDATE`
- [82] Standby Drain Session Comparison — `IDEA`
- [83] Standby Energy Regression Detection — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-ENERGY-006 — Device power-management diagnostics

Status: CANDIDATE

Type: Configuration / Optimization, Diagnostic, Diagnostic / Repair

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [36] USB Selective Suspend State — `CANDIDATE`
- [37] USB Selective Suspend Troubleshooter — `CANDIDATE`
- [38] PCIe Link State Power Management Detection — `CANDIDATE`
- [39] PCIe ASPM Policy Tuning — `RESEARCHING`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-ENERGY-007 — Display energy-management diagnostics

Status: CANDIDATE

Type: Configuration, Configuration / Recommendation, Diagnostic, Diagnostic / Configuration, Diagnostic / Recommendation

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [40] Display Power Policy — `CANDIDATE`
- [88] Display Refresh Rate Energy Analysis — `CANDIDATE`
- [89] Dynamic Refresh Rate Detection — `CANDIDATE`
- [90] Dynamic Refresh Rate Recommendation — `CANDIDATE`
- [91] Battery Refresh Rate Profile — `IDEA`
- [92] Adaptive Brightness Capability Detection — `CANDIDATE`
- [93] Content Adaptive Brightness Detection — `CANDIDATE`
- [94] Display Power Consumption Advisor — `IDEA`
- [95] OLED Energy Context — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-ENERGY-008 — Energy recommendation confidence

Status: IDEA

Type: Diagnostic Safeguard, Product Behavior

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [43] Energy Diagnostic Readiness Check — `IDEA`
- [100] Energy Recommendation Confidence — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-ENERGY-009 — Energy state history, conflict & outcome validation

Status: CANDIDATE

Type: Benchmark / Validation, Diagnostic, Monitoring / Diagnostic

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [44] Historical Energy Usage — `CANDIDATE`
- [60] Power Profile Before × After Validation — `CANDIDATE`
- [98] Energy Optimization Conflict Detection — `IDEA`
- [99] Energy State Timeline — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-ENERGY-010 — Application/process energy attribution & policy

Status: CANDIDATE

Type: Configuration, Diagnostic, Diagnostic / Configuration, Optimization

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [45] Energy Consumption by Application — `IDEA`
- [54] Per-Process Energy Efficiency Policy — `RESEARCHING`
- [84] Hybrid Graphics Capability Detection — `CANDIDATE`
- [85] Application GPU Power Preference — `CANDIDATE`
- [86] Unnecessary dGPU Activation Detection — `CANDIDATE`
- [87] GPU Power Preference Recommendation — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-ENERGY-011 — Battery capability, health & drain diagnostics

Status: CANDIDATE

Type: Diagnostic, Monitoring

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [46] Battery Presence / Capability Detection — `CANDIDATE`
- [47] Battery Health Overview — `CANDIDATE`
- [48] Battery Usage Report — `CANDIDATE`
- [49] Abnormal Battery Drain Detection — `IDEA`
- [50] Charging / Discharging Rate Monitoring — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-ENERGY-012 — Energy Saver diagnostics & configuration

Status: CANDIDATE

Type: Configuration, Diagnostic

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [51] Energy Saver State Detection — `CANDIDATE`
- [52] Energy Saver Configuration — `CANDIDATE`
- [96] Energy Saver Background Activity Impact — `CANDIDATE`
- [97] Energy Saver Automatic Activation Policy — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-ENERGY-013 — Timer-resolution energy diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Benchmark, Product Behavior / Safety

Technical Domain: ENERGY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [73] Timer Resolution Request Detection — `CANDIDATE`
- [74] Excessive Timer Resolution Diagnostic — `CANDIDATE`
- [75] Timer Resolution Energy Correlation — `IDEA`
- [76] Timer Resolution Myth Guard — `CANDIDATE`
- [77] Occluded Process Timer Throttling Detection — `RESEARCHING`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
