# Consolidated Discovery — HARDWARE

> Source: `hardware.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **27**
- Consolidated active capability groups: **9**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-HARDWARE-001 — Hardware inventory & firmware context

Status: CANDIDATE

Type: Diagnostic

Technical Domain: HARDWARE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Hardware Inventory Baseline — `CANDIDATE`
- [13] Firmware / BIOS Context Inventory — `CANDIDATE`
- [14] Hardware Sensor Availability Map — `CANDIDATE`
- [17] Hardware Change Detection — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-HARDWARE-002 — Cross-device health & error correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: HARDWARE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [2] Device Manager Health Assessment — `CANDIDATE`
- [3] Hardware Error Event Correlation — `CANDIDATE`
- [4] Unexpected Shutdown / Hardware Failure Correlation — `CANDIDATE`
- [24] Intermittent Device Failure Trend — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-HARDWARE-003 — Component hardware stability assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: HARDWARE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [5] Memory Hardware Error Assessment — `CANDIDATE`
- [6] Storage Hardware Health Assessment — `CANDIDATE`
- [7] GPU Hardware Stability Assessment — `CANDIDATE`
- [8] CPU Hardware Stability Assessment — `CANDIDATE`
- [10] USB / Peripheral Hardware Reliability Assessment — `CANDIDATE`
- [11] Network Adapter Hardware Health Assessment — `CANDIDATE`
- [12] Display / Monitor Hardware Detection Health — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-HARDWARE-004 — PCIe, storage-path & topology sanity diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: HARDWARE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [9] PCIe Link Health & Error Awareness — `CANDIDATE`
- [22] Memory Configuration Topology Sanity Check — `CANDIDATE`
- [23] Storage Connection / Path Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-HARDWARE-005 — Thermal & power-delivery fault evidence

Status: CANDIDATE

Type: Diagnostic

Technical Domain: HARDWARE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [15] Hardware Thermal Stress Evidence — `CANDIDATE`
- [16] Power Delivery / PSU Symptom Correlation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-HARDWARE-006 — Hardware stress-test orchestration

Status: CANDIDATE

Type: Benchmark / Diagnostic

Technical Domain: HARDWARE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [18] Hardware Stress Test Orchestration — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-HARDWARE-007 — Bottleneck vs hardware-fault classification

Status: CANDIDATE

Type: Diagnostic

Technical Domain: HARDWARE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [19] Cross-Component Bottleneck vs Fault Classification — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-HARDWARE-008 — Hardware diagnostic confidence & escalation

Status: CANDIDATE

Type: Diagnostic / Tool, Manual Guidance, Recommendation

Technical Domain: HARDWARE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [20] Hardware Diagnostic Confidence & Escalation — `CANDIDATE`
- [21] Hardware Vendor Diagnostic Integration Awareness — `CANDIDATE`
- [26] Physical Check Guidance — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-HARDWARE-009 — Hardware diagnostic session & evidence bundle

Status: CANDIDATE

Type: Diagnostic / Measurement, Tool / Diagnostic

Technical Domain: HARDWARE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [25] Hardware Diagnostic Session Baseline — `CANDIDATE`
- [27] Hardware Diagnostic Evidence Bundle — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
