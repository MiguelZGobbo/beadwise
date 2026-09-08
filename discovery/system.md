# Consolidated Discovery — SYSTEM

> Source: `system.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **70**
- Consolidated active capability groups: **19**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-SYSTEM-001 — System baseline, identity & capability detection

Status: CANDIDATE

Type: Diagnostic, Shared Capability

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] System Baseline / Estado Inicial do Sistema — `CANDIDATE`
- [2] Windows Identification — `CANDIDATE`
- [3] System Capability Detection — `CANDIDATE`
- [47] System Defaults Reference — `RESEARCHING`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-002 — System health, bottleneck & slow-PC diagnostic session

Status: CANDIDATE

Type: Diagnostic, Diagnostic Tool, Product Behavior, Shared Capability

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [4] System Health Scan — `CANDIDATE`
- [6] System Bottleneck Overview — `IDEA`
- [31] “Why Is My PC Slow?” Diagnostic Session — `IDEA`
- [48] System Diagnostic Framework — `CANDIDATE`
- [49] “Nothing To Optimize” Result — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-003 — System recommendation & explanation layer

Status: CANDIDATE

Type: Diagnostic / Optimization, Diagnostic / Product Behavior

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [5] System Recommendation Engine — `CANDIDATE`
- [50] Explain Why — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-004 — Process/resource overview & anomalies

Status: CANDIDATE

Type: Advanced Diagnostic, Diagnostic

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [7] Process Resource Overview — `CANDIDATE`
- [8] Abnormal Resource Consumption Detection — `IDEA`
- [60] Memory Allocation Overview — `CANDIDATE`
- [62] Per-Process Virtual Memory Analysis — `IDEA`
- [63] Resource Usage History — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-005 — Startup & boot overview diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Benchmark, Diagnostic / Configuration, Diagnostic / Optimization

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [9] Automatic Startup Inventory — `CANDIDATE`
- [10] Startup Impact Analysis — `CANDIDATE`
- [11] Boot Performance Diagnosis — `CANDIDATE`
- [34] Boot / Shutdown History — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-006 — Services & scheduled-task health

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration, Diagnostic / Repair

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [12] Windows Services Inventory — `CANDIDATE`
- [13] Service Dependency Analysis — `CANDIDATE`
- [14] Service Failure Detection — `CANDIDATE`
- [15] Scheduled Tasks Inventory — `CANDIDATE`
- [16] Scheduled Task Failure Detection — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-007 — System change history, drift & anomaly detection

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Cleanup, Diagnostic / Safety

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [17] Orphaned System Entries Detection — `IDEA`
- [35] System Change History — `CANDIDATE`
- [36] Configuration Drift Detection — `IDEA`
- [37] System Configuration Anomaly Detection — `IDEA`
- [38] Third-Party Optimizer Residue Detection — `IDEA`
- [46] System State Comparison — `CANDIDATE`
- [61] Memory Snapshot Comparison — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-008 — Windows feature & core-component health

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [18] Optional Windows Features Inventory — `CANDIDATE`
- [19] Windows Feature Dependency Safeguard — `IDEA`
- [40] Windows Core Component Availability Check — `IDEA`
- [69] Environment Configuration Health — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-009 — Windows image, system-file & component-store health

Status: CANDIDATE

Type: Diagnostic / Maintenance, Diagnostic / Repair, Maintenance / Cleanup

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [20] Windows Image Health — `CANDIDATE`
- [21] Protected System Files Integrity — `CANDIDATE`
- [22] Component Store Analysis — `CANDIDATE`
- [23] Component Store Maintenance — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-010 — Search/index health & repair

Status: CANDIDATE

Type: Diagnostic, Repair

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [24] Windows Search / Index Health — `CANDIDATE`
- [25] Windows Search Repair — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-011 — Event, crash, WHEA & hang diagnostics

Status: CANDIDATE

Type: Advanced Diagnostic Tool, Diagnostic

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [26] Event Log Health Analysis — `CANDIDATE`
- [27] Hardware Error History via WHEA — `CANDIDATE`
- [28] Application Crash / Hang History — `CANDIDATE`
- [29] System Crash / BSOD History — `IDEA`
- [58] Process Hang Detection — `CANDIDATE`
- [59] Triggered Process Dump Capture — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-012 — Performance/file-registry tracing & failed-I/O diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic Tool

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [30] Performance Trace Capture — `CANDIDATE`
- [51] File / Registry Activity Trace — `CANDIDATE`
- [52] Repeated Failed I/O Detection — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-013 — Clean-boot & shell/module diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Tool, Shared Diagnostic Capability

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [32] Clean Boot Diagnostic Assistant — `IDEA`
- [39] Explorer / Shell Extension Diagnosis — `IDEA`
- [55] Loaded Module Inventory — `CANDIDATE`
- [56] Unsigned / Untrusted Module Detection — `IDEA`
- [57] Executable Trust Metadata — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-014 — Pending reboot/file-operation diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [33] Pending Reboot Detection — `CANDIDATE`
- [68] Pending File Operation Detection — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-015 — System maintenance & benchmark readiness

Status: IDEA

Type: Diagnostic

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [41] System Maintenance State — `IDEA`
- [42] Benchmark Readiness Check — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-016 — Accessibility safeguard

Status: CANDIDATE

Type: Safety / Diagnostic

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [43] Accessibility Configuration Safeguard — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-017 — System diagnostic evidence correlation & export

Status: IDEA

Type: Diagnostic, Diagnostic Tool, Tool

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [44] System Diagnostic Export — `IDEA`
- [45] Diagnostic Evidence Correlation — `IDEA`
- [70] System Diagnostic Capture Package — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-018 — File/resource lock diagnosis & cleanup

Status: CANDIDATE

Type: Diagnostic Tool, Repair / Tool

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [53] Open File / Resource Lock Diagnosis — `CANDIDATE`
- [54] Locked File Cleanup Assistant — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SYSTEM-019 — Sleep/wake & power-efficiency diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: SYSTEM

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [64] Sleep Blocker Detection — `CANDIDATE`
- [65] Wake Source Diagnosis — `CANDIDATE`
- [66] Sleep Transition History — `CANDIDATE`
- [67] Power Efficiency Diagnostic — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
