# Consolidated Discovery — PROCESSES

> Source: `processes.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **44**
- Consolidated active capability groups: **9**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-PROCESSES-001 — Process inventory, tree & app grouping

Status: CANDIDATE

Type: Diagnostic

Technical Domain: PROCESSES

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Process Inventory — `CANDIDATE`
- [2] Process Tree & Parent/Child Mapping — `CANDIDATE`
- [3] Application-to-Process Grouping — `CANDIDATE`
- [32] Process Command-Line & Launch Context Diagnostics — `CANDIDATE`
- [33] Process Session / User Isolation Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-PROCESSES-002 — Per-process resource usage & trend diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Monitoring

Technical Domain: PROCESSES

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [4] Process CPU Usage Diagnostics — `CANDIDATE`
- [5] Process Memory Usage Diagnostics — `CANDIDATE`
- [6] Process Disk I/O Diagnostics — `CANDIDATE`
- [7] Process Network Usage Diagnostics — `CANDIDATE`
- [8] Process GPU Usage Diagnostics — `CANDIDATE`
- [9] Process Resource Trend Monitoring — `CANDIDATE`
- [23] Process CPU-Time vs Utilization Context — `CANDIDATE`
- [36] Process Power / Energy Attribution — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-PROCESSES-003 — Process anomaly & root-cause correlation

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Safeguard

Technical Domain: PROCESSES

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [10] Foreground vs Background Context — `CANDIDATE`
- [28] Process Resource Anomaly Detection — `CANDIDATE`
- [37] Process Wake / Sleep Prevention Diagnostics — `CANDIDATE`
- [44] Process Known-Role Knowledge Boundary — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-PROCESSES-004 — Process responsiveness, crash & lifecycle diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Monitoring, Safeguard

Technical Domain: PROCESSES

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [11] Process Responsiveness / Hang Diagnostics — `CANDIDATE`
- [12] Process Crash History Correlation — `CANDIDATE`
- [13] Process Start / Exit Monitoring — `CANDIDATE`
- [30] Process Change / Action History — `CANDIDATE`
- [39] Process Leak Trend Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-PROCESSES-005 — Process scheduling, priority & policy diagnostics

Status: CANDIDATE

Type: Configuration / Safeguard, Diagnostic

Technical Domain: PROCESSES

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [14] Process Priority Class Diagnostics — `CANDIDATE`
- [15] Process Priority Adjustment Safeguard — `CANDIDATE`
- [16] Background Processing Mode Awareness — `CANDIDATE`
- [17] Efficiency / Eco Mode Awareness — `CANDIDATE`
- [18] Process Affinity / CPU Set State Diagnostics — `CANDIDATE`
- [19] Process I/O Priority Awareness — `CANDIDATE`
- [20] Process Memory Priority Awareness — `CANDIDATE`
- [21] Job Object Membership Awareness — `CANDIDATE`
- [35] Process Mitigation Policy Awareness — `CANDIDATE`
- [38] Process Working-Set Trim Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-PROCESSES-006 — Process handles, threads & contention diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: PROCESSES

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [22] Process Handle / Thread Count Diagnostics — `CANDIDATE`
- [40] Process Contention Correlation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-PROCESSES-007 — Process action workflow & safeguards

Status: CANDIDATE

Type: Diagnostic / Configuration, Diagnostic / Safeguard, Repair / Tool, Safeguard

Technical Domain: PROCESSES

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [24] Process Suspend / Resume Awareness — `CANDIDATE`
- [25] Safe Process Termination — `CANDIDATE`
- [26] Process Restart Workflow — `CANDIDATE`
- [27] Critical Process Safeguard — `CANDIDATE`
- [34] Protected / Restricted Process Awareness — `CANDIDATE`
- [42] Process Action Dry-Run / Impact Preview — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-PROCESSES-008 — Process history, outcome & recommendation layer

Status: CANDIDATE

Type: Diagnostic / Quality of Life, Diagnostic / Recommendation Infrastructure, Diagnostic / Validation

Technical Domain: PROCESSES

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [29] Process Recommendation Explanation — `CANDIDATE`
- [31] Process Outcome Validation — `CANDIDATE`
- [43] Process Recommendation Confidence — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-PROCESSES-009 — Per-app process policy profiles

Status: CANDIDATE

Type: Configuration

Technical Domain: PROCESSES

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [41] Per-App Process Policy Profile — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
