# Consolidated Discovery — REPAIR

> Source: `repair.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **29**
- Consolidated active capability groups: **10**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-REPAIR-001 — System repair triage & orchestration

Status: CANDIDATE

Type: Diagnostic, Repair / Diagnostic, Tool / Diagnostic

Technical Domain: REPAIR

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] System Repair Triage — `CANDIDATE`
- [8] Built-in Troubleshooter Orchestration — `CANDIDATE`
- [29] Repair Session Bundling — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-REPAIR-002 — System file & component-store repair

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: REPAIR

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [2] System File Integrity Assessment — `CANDIDATE`
- [3] Component Store Health & Repair — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-REPAIR-003 — Boot, startup & recovery-environment repair

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Repair, Diagnostic / Tool, Repair

Technical Domain: REPAIR

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [4] Boot Failure Triage — `CANDIDATE`
- [5] Startup Repair Orchestration — `CANDIDATE`
- [6] Windows Recovery Environment Health — `CANDIDATE`
- [22] Safe Mode / Clean-Boot Diagnostic Path — `CANDIDATE`
- [26] Offline Repair Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-REPAIR-004 — Pending operation & restart resolution

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: REPAIR

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [7] Restart / Pending Operation Resolution — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-REPAIR-005 — Application & Microsoft Store repair

Status: CANDIDATE

Type: Repair, Repair / Configuration

Technical Domain: REPAIR

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [9] Application Repair / Reset Awareness — `CANDIDATE`
- [10] Microsoft Store / Packaged App Repair — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-REPAIR-006 — Subsystem repair paths

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: REPAIR

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [11] Network Stack Repair Triage — `CANDIDATE`
- [12] Audio Device Repair Triage — `CANDIDATE`
- [15] File System Error Repair Triage — `CANDIDATE`
- [16] Time & Clock Repair Triage — `CANDIDATE`
- [17] Search / Index Repair Triage — `CANDIDATE`
- [18] Printing Repair Triage — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-REPAIR-007 — Device & driver repair path

Status: CANDIDATE

Type: Repair

Technical Domain: REPAIR

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [13] Device Re-enumeration / Restart Guidance — `CANDIDATE`
- [14] Driver Rollback / Reinstall Repair Path — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-REPAIR-008 — Repair verification, history & known-good correlation

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Safety, Measurement

Technical Domain: REPAIR

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [19] Repair Before/After Verification — `CANDIDATE`
- [20] Repair History & Audit Trail — `CANDIDATE`
- [23] Known-Good State Correlation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-REPAIR-009 — Repair escalation, source availability & safeguards

Status: CANDIDATE

Type: Diagnostic, Safety, Safety / Recommendation

Technical Domain: REPAIR

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [21] Repair Escalation & Stop Conditions — `CANDIDATE`
- [24] Repair Source Availability Assessment — `CANDIDATE`
- [25] BitLocker-Aware Repair Safeguard — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-REPAIR-010 — Repair confidence & rollback readiness

Status: CANDIDATE

Type: Recommendation / Diagnostic, Safety / Diagnostic

Technical Domain: REPAIR

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [27] Repair Confidence & Evidence Summary — `CANDIDATE`
- [28] Repair Rollback Readiness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
