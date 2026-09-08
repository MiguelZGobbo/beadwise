# Consolidated Discovery — DISPLAY

> Source: `display.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **58**
- Consolidated active capability groups: **13**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-DISPLAY-001 — Display inventory, EDID & path mapping

Status: CANDIDATE

Type: Diagnostic

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Display Inventory — `CANDIDATE`
- [2] EDID & Display Capability Inventory — `CANDIDATE`
- [3] Active Display Path Mapping — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DISPLAY-002 — Resolution, refresh rate & mode validation

Status: CANDIDATE

Type: Configuration / Diagnostic, Diagnostic, Diagnostic / Benchmark, Diagnostic / Configuration, Diagnostic / Recommendation

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [4] Native Resolution Validation — `CANDIDATE`
- [5] Refresh Rate Capability & Active Rate Validation — `CANDIDATE`
- [6] Dynamic Refresh Rate Awareness — `CANDIDATE`
- [7] Display Mode Compatibility Diagnostics — `CANDIDATE`
- [12] Variable Refresh Rate Capability Diagnostics — `CANDIDATE`
- [15] Mixed Refresh Rate Multi-Monitor Awareness — `CANDIDATE`
- [17] Resolution vs Scaling Recommendation Logic — `CANDIDATE`
- [18] Display Orientation & Layout Validation — `CANDIDATE`
- [56] Refresh Rate Measurement Validation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DISPLAY-003 — HDR & advanced color diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration, Tool / Manual Guidance

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [8] HDR Capability Detection — `CANDIDATE`
- [9] HDR Configuration Diagnostics — `CANDIDATE`
- [10] HDR Calibration Guidance — `CANDIDATE`
- [11] SDR Content Brightness in HDR Diagnostics — `CANDIDATE`
- [40] Advanced Color Capability & State Diagnostics — `CANDIDATE`
- [41] Auto Color Management Awareness — `CANDIDATE`
- [42] ICC Profile Compatibility Under Advanced Color — `CANDIDATE`
- [43] HDR / Wide-Gamut Multi-Monitor Consistency Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DISPLAY-004 — VRR, tearing & motion diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration, Tool / Diagnostic

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [13] VRR Configuration & Validation — `CANDIDATE`
- [31] Ghosting / Motion Clarity Test — `CANDIDATE`
- [32] Tearing Visual Validation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DISPLAY-005 — Multi-monitor topology & compatibility

Status: CANDIDATE

Type: Diagnostic

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [14] Multi-Monitor Topology Diagnostics — `CANDIDATE`
- [46] Clone Mode Capability Limitation Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DISPLAY-006 — Scaling, DPI & color profile diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration, Diagnostic / Manual Guidance, Tool / Manual Guidance

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [16] Display Scaling & DPI Diagnostics — `CANDIDATE`
- [19] Color Depth & Output Capability Diagnostics — `CANDIDATE`
- [20] Color Profile / ICC Awareness — `CANDIDATE`
- [21] Windows Color Calibration Guidance — `CANDIDATE`
- [22] Gamma / Brightness / Contrast Diagnostic Guidance — `CANDIDATE`
- [45] Chroma Subsampling & Pixel Format Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DISPLAY-007 — Display link, connection & hotplug diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Monitoring

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [23] Display Connection Type Diagnostics — `CANDIDATE`
- [24] Display Link Bottleneck Diagnostics — `CANDIDATE`
- [25] Display Signal Loss / Reconnect Monitoring — `CANDIDATE`
- [26] Display Driver / GPU Reset Correlation — `CANDIDATE`
- [36] Wireless Display Awareness — `CANDIDATE`
- [44] Display Stream Compression Awareness — `CANDIDATE`
- [51] Dock / USB-C / USB4 Display Hotplug Correlation — `CANDIDATE`
- [55] Protected Content / HDCP Context Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DISPLAY-008 — Panel visual test tools

Status: CANDIDATE

Type: Tool, Tool / Diagnostic

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [27] Dead / Stuck Pixel Diagnostic Test — `CANDIDATE`
- [28] Stuck Pixel Exercise Tool — `CANDIDATE`
- [29] Backlight Bleed / Uniformity Test — `CANDIDATE`
- [30] Banding / Gradient Test — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DISPLAY-009 — Display power & brightness behavior

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration, Diagnostic / Safeguard

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [33] Display Power / Sleep Behavior Diagnostics — `CANDIDATE`
- [34] Internal Display Brightness Capability Awareness — `CANDIDATE`
- [35] Content-Adaptive Brightness Awareness — `CANDIDATE`
- [54] OLED Display Safeguards — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DISPLAY-010 — Display recommendation layer

Status: CANDIDATE

Type: Diagnostic / Quality of Life, Diagnostic / Recommendation Infrastructure

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [37] Display Recommendation Explanation — `CANDIDATE`
- [58] Display Recommendation Confidence — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DISPLAY-011 — Display change safety, rollback & validation

Status: CANDIDATE

Type: Diagnostic / Validation, Safeguard, Safeguard / Repair

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [38] Display Configuration Backup & Rollback — `CANDIDATE`
- [39] Display Change Outcome Validation — `CANDIDATE`
- [57] Display Test Flashing Safety — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DISPLAY-012 — External monitor control & firmware awareness

Status: CANDIDATE

Type: Configuration / Tool, Diagnostic, Diagnostic / Manual Guidance, Tool / Configuration

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [47] External Monitor DDC/CI Capability Diagnostics — `CANDIDATE`
- [48] External Monitor Brightness Control — `CANDIDATE`
- [49] Monitor Input Source Control — `CANDIDATE`
- [50] Monitor Firmware Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-DISPLAY-013 — Virtual/remote display awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: DISPLAY

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [52] Remote Desktop / Virtual Display Awareness — `CANDIDATE`
- [53] Privacy Screen / Integrated Display Feature Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
