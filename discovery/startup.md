# Consolidated Discovery — STARTUP

> Source: `startup.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **39**
- Consolidated active capability groups: **9**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-STARTUP-001 — Startup entry inventory & source coverage

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Safeguard

Technical Domain: STARTUP

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Startup Entry Inventory — `CANDIDATE`
- [2] Run / RunOnce Inventory — `CANDIDATE`
- [3] Startup Folder Inventory — `CANDIDATE`
- [29] Autoruns Extended Location Awareness — `CANDIDATE`
- [30] Explorer Shell Extension Startup Context — `CANDIDATE`
- [31] Winlogon Extension Safeguard — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STARTUP-002 — Startup impact, timing & responsiveness diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Benchmark, Diagnostic / Tool

Technical Domain: STARTUP

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [4] Startup App Impact Diagnostics — `CANDIDATE`
- [5] Boot / Logon Phase Timing — `CANDIDATE`
- [32] Boot Trace / WPA Escalation — `CANDIDATE`
- [33] Fast Startup vs Full Boot Benchmark Context — `CANDIDATE`
- [34] Post-Logon Responsiveness Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STARTUP-003 — Startup entry identity, role & intent context

Status: CANDIDATE

Type: Diagnostic, Diagnostic / UX

Technical Domain: STARTUP

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [6] Startup Entry Publisher & Signature Context — `CANDIDATE`
- [7] Startup Functional Role Classification — `CANDIDATE`
- [8] Startup User-Intent Awareness — `CANDIDATE`
- [13] Launcher Auto-Start Diagnostics — `CANDIDATE`
- [14] Cloud Sync Startup Diagnostics — `CANDIDATE`
- [15] Hardware Helper Startup Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STARTUP-004 — Startup disable scoring & control

Status: CANDIDATE

Type: Configuration, Diagnostic / Recommendation

Technical Domain: STARTUP

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [9] Startup Disable Candidate Scoring — `CANDIDATE`
- [10] Startup Disable / Enable — `CANDIDATE`
- [21] Delayed Start Opportunity Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STARTUP-005 — Startup drift, regression & before/after validation

Status: CANDIDATE

Type: Benchmark, Diagnostic

Technical Domain: STARTUP

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [11] Startup Before × After Validation — `CANDIDATE`
- [25] Startup Configuration Drift — `CANDIDATE`
- [38] Startup Regression Correlation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STARTUP-006 — Broken/recreated startup entry diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Cleaning, Diagnostic / Repair

Technical Domain: STARTUP

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [12] Startup Re-Creation Detection — `CANDIDATE`
- [24] Broken Startup Entry Diagnostics — `CANDIDATE`
- [35] Startup App Failure Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STARTUP-007 — Startup dependency & safety safeguards

Status: CANDIDATE

Type: Diagnostic, Safeguard

Technical Domain: STARTUP

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [16] Security Software Startup Safeguard — `CANDIDATE`
- [17] Accessibility Startup Safeguard — `CANDIDATE`
- [18] Auto-Start Service Context — `CANDIDATE`
- [19] Trigger-Start Service Awareness — `CANDIDATE`
- [20] Scheduled Task Startup Context — `CANDIDATE`
- [22] Automatic Maintenance Awareness — `CANDIDATE`
- [23] Startup Entry Dependency Diagnostics — `CANDIDATE`
- [36] RunOnce Transient-State Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STARTUP-008 — Startup backup, rollback & optimization sessions

Status: CANDIDATE

Type: Optimization / Safeguard, Safeguard

Technical Domain: STARTUP

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [26] Startup Change Backup & Rollback — `CANDIDATE`
- [37] Startup Optimization Session Profile — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STARTUP-009 — Startup recommendation layer

Status: CANDIDATE

Type: Diagnostic / Quality of Life, Diagnostic / Recommendation Infrastructure

Technical Domain: STARTUP

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [27] Startup Recommendation Explanation — `CANDIDATE`
- [28] Startup Recommendation Confidence — `CANDIDATE`
- [39] Startup Recommendation Confidence — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
