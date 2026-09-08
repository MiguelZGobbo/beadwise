# Consolidated Discovery — AUDIO

> Source: `audio.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **55**
- Consolidated active capability groups: **13**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-AUDIO-001 — Audio device & endpoint inventory

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Audio Endpoint Inventory — `CANDIDATE`
- [2] Physical Audio Device & Endpoint Relationship — `CANDIDATE`
- [3] Default Playback Device Validation — `CANDIDATE`
- [4] Default Capture Device Validation — `CANDIDATE`
- [49] Audio Jack Detection & Connector State — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-AUDIO-002 — Per-app routing, sessions & mixer diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [5] Per-App Audio Routing Diagnostics — `CANDIDATE`
- [6] Audio Session & Volume Mixer Diagnostics — `CANDIDATE`
- [32] Communication Activity / Ducking Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-AUDIO-003 — Audio driver & device health

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Repair

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [7] Audio Device Health & Problem Code Diagnostics — `CANDIDATE`
- [8] Audio Driver Inventory & Provider Diagnostics — `CANDIDATE`
- [9] Audio Driver Regression Correlation — `CANDIDATE`
- [28] Audio Engine / Service Health Diagnostics — `CANDIDATE`
- [50] Vendor Audio Suite Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-AUDIO-004 — Audio format, modes & processing

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [10] Audio Format Capability Inventory — `CANDIDATE`
- [11] Default Audio Format Validation — `CANDIDATE`
- [12] Shared Mode Audio Diagnostics — `CANDIDATE`
- [13] Exclusive Mode Capability & Conflict Diagnostics — `CANDIDATE`
- [14] Audio Enhancements Inventory — `CANDIDATE`
- [15] Audio Enhancement Troubleshooting — `CANDIDATE`
- [16] Audio Processing Object / APO Awareness — `CANDIDATE`
- [25] Microphone Processing / Enhancement Diagnostics — `CANDIDATE`
- [40] Low-Latency Shared Mode Diagnostics — `CANDIDATE`
- [41] Raw Audio Processing Capability — `CANDIDATE`
- [42] Audio Engine Period / Buffer Context — `CANDIDATE`
- [43] Hardware Audio Offload Awareness — `CANDIDATE`
- [46] Audio Processing Mode Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-AUDIO-005 — Spatial, channel & speaker configuration

Status: CANDIDATE

Type: Diagnostic / Configuration, Tool / Diagnostic

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [17] Spatial Audio Capability Diagnostics — `CANDIDATE`
- [18] Channel Layout & Speaker Configuration Diagnostics — `CANDIDATE`
- [53] Speaker / Channel Test Tool — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-AUDIO-006 — Digital, USB & Bluetooth audio paths

Status: CANDIDATE

Type: Diagnostic

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [19] HDMI / DisplayPort Audio Diagnostics — `CANDIDATE`
- [20] USB Audio Path Diagnostics — `CANDIDATE`
- [21] Bluetooth Audio Capability Diagnostics — `CANDIDATE`
- [22] Bluetooth Audio Quality / Communication Mode Diagnostics — `CANDIDATE`
- [44] Bluetooth LE Audio Awareness — `CANDIDATE`
- [47] USB Audio Class Context — `CANDIDATE`
- [48] HDMI Audio Capability from Display Path — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-AUDIO-007 — Microphone diagnostics & validation

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration, Tool / Diagnostic

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [23] Microphone Permission & Privacy Diagnostics — `CANDIDATE`
- [24] Microphone Input Level Diagnostics — `CANDIDATE`
- [45] Microphone Array & Beamforming Awareness — `CANDIDATE`
- [54] Microphone Test & Clipping Validation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-AUDIO-008 — Audio glitch, latency & contention diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Monitoring

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [26] Audio Dropout / Glitch Monitoring — `CANDIDATE`
- [27] DPC / ISR Audio Correlation — `CANDIDATE`
- [35] Audio Resource Usage Diagnostics — `CANDIDATE`
- [51] Virtual Mixer / Feedback Loop Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-AUDIO-009 — Audio repair workflow

Status: CANDIDATE

Type: Repair, Repair / Safeguard

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [29] Targeted Audio Service Restart — `CANDIDATE`
- [30] Audio Endpoint Re-enumeration / Device Restart — `CANDIDATE`
- [31] Audio Repair Escalation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-AUDIO-010 — Virtual audio & capture path awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [33] Virtual Audio Device Awareness — `CANDIDATE`
- [34] Audio Loopback / Capture Path Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-AUDIO-011 — Audio quality comparison & outcome validation

Status: CANDIDATE

Type: Diagnostic / Validation, Tool / Benchmark

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [36] Audio Quality Comparison Tool — `CANDIDATE`
- [39] Audio Change Outcome Validation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-AUDIO-012 — Audio recommendation layer

Status: CANDIDATE

Type: Diagnostic / Quality of Life, Diagnostic / Recommendation Infrastructure, Safeguard / Repair

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [37] Audio Recommendation Explanation — `CANDIDATE`
- [38] Audio Configuration Backup & Rollback — `CANDIDATE`
- [55] Audio Recommendation Confidence — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-AUDIO-013 — Audio sleep/resume diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: AUDIO

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [52] Audio Sleep / Resume Failure Correlation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
