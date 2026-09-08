# Consolidated Discovery — CLEANING

> Source: `cleaning.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **36**
- Consolidated active capability groups: **10**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-CLEANING-001 — Cleanup opportunity assessment

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Recommendation, Recommendation

Technical Domain: CLEANING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Storage Pressure & Recoverable Space Assessment — `CANDIDATE`
- [2] Cleanup Inventory & Category Classification — `CANDIDATE`
- [33] Cleanup Opportunity Ranking — `CANDIDATE`
- [34] Critical Free-Space Requirement Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-CLEANING-002 — Safe temporary & rebuildable data cleanup

Status: CANDIDATE

Type: Configuration / Optimization, Diagnostic, Maintenance / Optimization, Optimization, Optimization / Repair-adjacent

Technical Domain: CLEANING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [3] Safe Temporary File Cleanup — `CANDIDATE`
- [4] Recycle Bin Space Management — `CANDIDATE`
- [10] Previous Windows Installation Cleanup — `CANDIDATE`
- [11] Windows Update Temporary Data Cleanup — `CANDIDATE`
- [12] Delivery Optimization Cache Cleanup — `CANDIDATE`
- [13] Windows Component Store Cleanup — `CANDIDATE`
- [14] App-Owned Temporary Data Cleanup — `CANDIDATE`
- [15] Rebuildable Cache Classification — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-CLEANING-003 — User file review & space recovery

Status: CANDIDATE

Type: Configuration / Optimization, Diagnostic / Tool, Recommendation / Tool

Technical Domain: CLEANING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [5] Downloads Folder Review — `CANDIDATE`
- [6] Large & Unused File Discovery — `CANDIDATE`
- [7] Duplicate File Discovery — `CANDIDATE`
- [9] Cloud Content Dehydration / Free-Up-Space — `CANDIDATE`
- [19] User Data Archive / Move Recommendation — `CANDIDATE`
- [32] Installer / Archive Redundancy Correlation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-CLEANING-004 — Application & installation residue review

Status: CANDIDATE

Type: Diagnostic / Configuration, Diagnostic / Tool

Technical Domain: CLEANING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [8] Unused Application Discovery & Uninstall Guidance — `CANDIDATE`
- [18] Installation Residue Review — `CANDIDATE`
- [31] Orphaned User Profile Assessment — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-CLEANING-005 — Logs, dumps & diagnostic artifact retention

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: CLEANING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [16] Crash Dump & Diagnostic Artifact Retention — `CANDIDATE`
- [17] Log Retention & Cleanup Assessment — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-CLEANING-006 — Cleanup safety, provenance & exclusions

Status: CANDIDATE

Type: Diagnostic / Safety, Safety / Configuration, Safety / Diagnostic

Technical Domain: CLEANING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [20] Active-Use & File-Lock Safeguard — `CANDIDATE`
- [21] Cleanup Ownership & Provenance Validation — `CANDIDATE`
- [23] Cleanup Exclusions & Protected Locations — `CANDIDATE`
- [29] Reparse Point / Hardlink / Sparse File Accounting — `CANDIDATE`
- [30] Per-User / Multi-Profile Cleanup Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-CLEANING-007 — Cleanup preview, execution safety & verification

Status: CANDIDATE

Type: Measurement / Diagnostic, Safety / Reliability, Safety / Tool

Technical Domain: CLEANING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [22] Cleanup Preview / Dry Run — `CANDIDATE`
- [27] Cleanup Result Verification & History — `CANDIDATE`
- [35] Cleanup Cancellation / Partial-Failure Safety — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-CLEANING-008 — Cleanup policy & Storage Sense awareness

Status: CANDIDATE

Type: Configuration, Diagnostic / Configuration, Optimization / Recommendation

Technical Domain: CLEANING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [24] Age / Context-Aware Cleanup Policy — `CANDIDATE`
- [25] Low-Disk Triggered Cleanup Recommendation — `CANDIDATE`
- [26] Storage Sense Configuration Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-CLEANING-009 — Multi-drive cleanup awareness

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: CLEANING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [28] Multi-Drive Cleanup Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-CLEANING-010 — Cleanup recommendation confidence

Status: CANDIDATE

Type: Recommendation / Diagnostic

Technical Domain: CLEANING

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [36] Cleanup Recommendation Confidence — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
