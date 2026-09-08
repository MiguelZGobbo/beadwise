# Consolidated Discovery — USB

> Source: `usb.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **55**
- Consolidated active capability groups: **15**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-USB-001 — USB controller, topology & device inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] USB Controller Inventory — `CANDIDATE`
- [2] USB Topology Mapping — `CANDIDATE`
- [3] USB Device Identification — `CANDIDATE`
- [39] Composite USB Device Diagnostics — `CANDIDATE`
- [40] Multiple Identical Peripheral Identity Diagnostics — `CANDIDATE`
- [41] Serial / COM Peripheral Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-002 — USB speed, generation & bandwidth diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [4] USB Connection Speed & Generation Validation — `CANDIDATE`
- [20] USB Hub Capability & Load Diagnostics — `CANDIDATE`
- [21] USB Bandwidth Contention Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-003 — USB device health, enumeration & disconnect diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Monitoring, Diagnostic / Repair

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [5] USB Device Health & Problem Code Diagnostics — `CANDIDATE`
- [6] USB Enumeration Failure Diagnostics — `CANDIDATE`
- [7] USB Disconnect / Reconnect Monitoring — `CANDIDATE`
- [8] USB Port / Hub Fault Correlation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-004 — USB power, suspend, sleep & wake diagnostics

Status: CANDIDATE

Type: Configuration / Repair, Diagnostic, Diagnostic / Configuration

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [9] USB Selective Suspend Diagnostics — `CANDIDATE`
- [10] Device-Specific USB Power Exception — `CANDIDATE`
- [11] USB Sleep / Resume Diagnostics — `CANDIDATE`
- [12] USB Wake Capability Diagnostics — `CANDIDATE`
- [22] USB Power Delivery / Power Availability Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-005 — HID, mouse, keyboard, controller & input diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Benchmark

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [13] HID Device Inventory — `CANDIDATE`
- [14] Mouse & Keyboard Input Health Diagnostics — `CANDIDATE`
- [15] HID Report Rate / Polling Diagnostics — `CANDIDATE`
- [16] Input Latency Context Diagnostics — `CANDIDATE`
- [17] Controller / Gamepad Diagnostics — `CANDIDATE`
- [50] Virtual Controller & Input Remapping Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-006 — Peripheral driver, firmware & software-conflict diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Manual Guidance

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [18] Peripheral Driver & Software Dependency Diagnostics — `CANDIDATE`
- [19] Peripheral Firmware Awareness — `CANDIDATE`
- [32] USB Driver Regression Correlation — `CANDIDATE`
- [51] Peripheral Software Conflict Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-007 — USB-C, USB4, Thunderbolt & docking diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Repair Guidance

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [23] USB-C Capability Diagnostics — `CANDIDATE`
- [24] USB-C / Alternate Mode Failure Diagnostics — `CANDIDATE`
- [25] USB4 Capability & Topology Diagnostics — `CANDIDATE`
- [47] Thunderbolt / PCIe Tunneling Awareness — `CANDIDATE`
- [49] Docking Station Composite Context Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-008 — External storage & surprise-removal awareness

Status: CANDIDATE

Type: Diagnostic / Safeguard

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [26] External Storage Safe Removal & Surprise Removal Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-009 — USB audio, webcam & capture-device awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [27] USB Audio Peripheral Awareness — `CANDIDATE`
- [28] Webcam / Capture Device USB Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-010 — Bluetooth adapter, device & coexistence diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Monitoring

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [29] Bluetooth Adapter USB Awareness — `CANDIDATE`
- [42] Bluetooth Radio & Capability Inventory — `CANDIDATE`
- [43] Bluetooth Device Inventory & Profile Awareness — `CANDIDATE`
- [44] Bluetooth Connection Stability Diagnostics — `CANDIDATE`
- [45] Bluetooth / Wi-Fi Coexistence Diagnostics — `CANDIDATE`
- [46] Bluetooth Battery & Low-Power Context — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-011 — Ghost, duplicate & resource-conflict diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [30] Peripheral Duplicate / Ghost Device Diagnostics — `CANDIDATE`
- [31] Peripheral Conflict & Resource Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-012 — Peripheral restart, repair & outcome validation

Status: CANDIDATE

Type: Diagnostic / Repair Validation, Repair, Repair / Safeguard

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [33] Targeted Peripheral Restart / Re-enumeration — `CANDIDATE`
- [34] Peripheral Repair Escalation — `CANDIDATE`
- [55] Peripheral Repair Outcome Validation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-013 — USB/peripheral history, trace & root-cause diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Recommendation Infrastructure, Diagnostic / Tool

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [35] USB / Peripheral Change History — `CANDIDATE`
- [38] USB ETW Trace Diagnostics — `CANDIDATE`
- [54] Peripheral Diagnostic Confidence & Root-Cause Layering — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-014 — USB/peripheral change backup & recommendation layer

Status: CANDIDATE

Type: Diagnostic / Quality of Life, Safeguard / Repair

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [36] USB / Peripheral Recommendation Explanation — `CANDIDATE`
- [37] USB / Peripheral Change Backup & Rollback — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-USB-015 — External peripheral security safeguards

Status: CANDIDATE

Type: Diagnostic / Safeguard, Safeguard, Safeguard / Diagnostic

Technical Domain: USB

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [48] Kernel DMA Protection & External Peripheral Safeguard — `CANDIDATE`
- [52] Accessibility Peripheral Safeguards — `CANDIDATE`
- [53] Critical Input Device Repair Safeguard — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
