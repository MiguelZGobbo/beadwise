# Consolidated Discovery — SECURITY

> Source: `security.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **24**
- Consolidated active capability groups: **10**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-SECURITY-001 — Windows security baseline

Status: CANDIDATE

Type: Diagnostic

Technical Domain: SECURITY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Windows Security Baseline Inventory — `CANDIDATE`
- [23] Security Baseline Drift History — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SECURITY-002 — Endpoint protection & firewall state

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: SECURITY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [2] Antivirus / Defender Protection State — `CANDIDATE`
- [3] Firewall Profile & Protection State — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SECURITY-003 — Platform boot & hardware-backed security

Status: CANDIDATE

Type: Diagnostic

Technical Domain: SECURITY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [4] Secure Boot Capability & State — `CANDIDATE`
- [5] TPM Capability & Health — `CANDIDATE`
- [7] Kernel DMA Protection State — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SECURITY-004 — Virtualization-based & credential protections

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: SECURITY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [6] Core Isolation / Memory Integrity State — `CANDIDATE`
- [14] LSA Protection / Credential Guard Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SECURITY-005 — Reputation, application & exploit protection

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: SECURITY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [8] SmartScreen / Reputation Protection State — `CANDIDATE`
- [9] Smart App Control Awareness — `CANDIDATE`
- [11] Exploit Protection State — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SECURITY-006 — UAC, encryption & sign-in protection

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: SECURITY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [10] UAC Protection State — `CANDIDATE`
- [12] BitLocker / Device Encryption State — `CANDIDATE`
- [13] Windows Hello / Sign-in Protection Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SECURITY-007 — Security update & support exposure

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Recommendation

Technical Domain: SECURITY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [15] Security Update Exposure Assessment — `CANDIDATE`
- [22] Unsupported OS Security Risk — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SECURITY-008 — Security policy, tamper & unsafe-tweak diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Recommendation

Technical Domain: SECURITY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [16] Security Protection Tamper / Policy Awareness — `CANDIDATE`
- [17] Potentially Unsafe Security Tweaks Detection — `CANDIDATE`
- [19] Security Provider Conflict Detection — `CANDIDATE`
- [20] Protection Feature Compatibility Assessment — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SECURITY-009 — Security recommendation layer

Status: CANDIDATE

Type: Recommendation

Technical Domain: SECURITY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [18] Security Recommendation Explanation — `CANDIDATE`
- [24] Security Recommendation Confidence — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-SECURITY-010 — Security recovery readiness & drift history

Status: CANDIDATE

Type: Diagnostic

Technical Domain: SECURITY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [21] Security Recovery Readiness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
