# Consolidated Discovery — DRIVERS

> Source: `drivers.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **38**
- Consolidated active capability groups: **9**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-DRIVERS-001 — Driver inventory, mapping & metadata

Status: CANDIDATE

Type: Diagnostic

Technical Domain: DRIVERS

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Driver Package Inventory — `CANDIDATE`
- [2] Device-to-Driver Mapping — `CANDIDATE`
- [3] Driver Ranking Awareness — `CANDIDATE`
- [4] Driver Version & Age Diagnostics — `CANDIDATE`
- [5] Driver Signature & Trust Diagnostics — `CANDIDATE`
- [19] Kernel Driver Inventory — `CANDIDATE`
- [21] Device Class Driver Coverage — `CANDIDATE`
- [33] Driver Service Start-Type Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DRIVERS-002 — Driver/device problem diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Manual Guidance

Technical Domain: DRIVERS

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [6] Problem Device Diagnostics — `CANDIDATE`
- [7] SetupAPI Installation Log Diagnostics — `CANDIDATE`
- [18] Driver Conflict / Filter Stack Awareness — `CANDIDATE`
- [20] Driver Crash / Bugcheck Correlation — `CANDIDATE`
- [22] Unknown Device Resolution Guidance — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DRIVERS-003 — Driver update & source awareness

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Recommendation

Technical Domain: DRIVERS

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [8] Driver Update Availability Awareness — `CANDIDATE`
- [9] OEM vs Inbox Driver Context — `CANDIDATE`
- [28] Windows Update Driver Source Correlation — `CANDIDATE`
- [29] Firmware-via-Driver Package Awareness — `CANDIDATE`
- [35] Driver Update Known-Issue Gate — `CANDIDATE`
- [36] Driver Security Exposure Prioritization — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DRIVERS-004 — Driver install, rollback & restart workflow

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Repair, Repair, Repair / Configuration, Safeguard

Technical Domain: DRIVERS

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [10] Driver Rollback Capability — `CANDIDATE`
- [11] Targeted Driver Install via Official Package — `CANDIDATE`
- [15] Driver Restart / Device Restart Orchestration — `CANDIDATE`
- [16] Reboot-Required Driver State — `CANDIDATE`
- [37] Driver Operation Dry-Run / Impact Preview — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DRIVERS-005 — Driver safety & critical-device safeguards

Status: CANDIDATE

Type: Safeguard

Technical Domain: DRIVERS

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [12] Driver Update Safeguard — `CANDIDATE`
- [23] Critical Driver Protection — `CANDIDATE`
- [34] Driver Filter Removal Safeguard — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DRIVERS-006 — Driver store maintenance

Status: CANDIDATE

Type: Diagnostic, Diagnostic / UX, Safeguard / Repair

Technical Domain: DRIVERS

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [13] Driver Store Cleanup Diagnostics — `CANDIDATE`
- [14] Driver Package Removal Safeguard — `CANDIDATE`
- [30] Driver Store Size & Growth Diagnostics — `CANDIDATE`
- [31] Duplicate / Superseded Package Analysis — `CANDIDATE`
- [32] Device Removal vs Driver Package Removal Distinction — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DRIVERS-007 — Driver regression correlation & validation

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Validation

Technical Domain: DRIVERS

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [17] Driver Regression Correlation — `CANDIDATE`
- [25] Driver Change Outcome Validation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DRIVERS-008 — Driver change history, backup & explanation

Status: CANDIDATE

Type: Diagnostic / Quality of Life, Diagnostic / Recommendation Infrastructure, Safeguard

Technical Domain: DRIVERS

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [24] Driver Change Backup & History — `CANDIDATE`
- [26] Driver Recommendation Explanation — `CANDIDATE`
- [38] Driver Recommendation Confidence — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DRIVERS-009 — Driver installation policy awareness

Status: CANDIDATE

Type: Diagnostic / Safeguard

Technical Domain: DRIVERS

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [27] Driver Installation Policy Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
