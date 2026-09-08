# Consolidated Discovery — STORAGE

> Source: `storage.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **200**
- Consolidated active capability groups: **21**
- Explicitly rejected source items kept outside active catalog: **3**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-STORAGE-001 — Storage hardware inventory & interface context

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Recommendation

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Physical Storage Inventory — `CANDIDATE`
- [2] Storage Media Type Detection — `CANDIDATE`
- [3] Storage Bus Type Detection — `CANDIDATE`
- [4] Storage Model / Firmware Detection — `CANDIDATE`
- [5] Storage Firmware Currency Check — `IDEA`
- [50] SATA Link Detection — `CANDIDATE`
- [51] SATA Link Bottleneck Detection — `IDEA`
- [52] NVMe PCIe Link Detection — `CANDIDATE`
- [55] Storage Controller Identification — `CANDIDATE`
- [145] Storage Controller Stack Detection — `CANDIDATE`
- [146] Intel VMD Detection — `CANDIDATE`
- [147] Intel RST Storage Context — `CANDIDATE`
- [148] VMD/RST Driver Health — `CANDIDATE`
- [150] RAID Configuration Detection — `CANDIDATE`
- [175] External Storage Transport Detection — `CANDIDATE`
- [176] USB Storage Negotiated Speed — `CANDIDATE`
- [177] UASP Capability Detection — `RESEARCHING`
- [179] USB Storage Bridge Context — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-002 — Storage health, SMART/NVMe & wear diagnostics

Status: CANDIDATE

Type: Diagnostic, Monitoring, Safety

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [6] Physical Disk Health Status — `CANDIDATE`
- [7] Storage Reliability Counters — `CANDIDATE`
- [8] SMART Capability Detection — `CANDIDATE`
- [9] SMART Attribute Analysis — `CANDIDATE`
- [10] SMART Warning Detection — `CANDIDATE`
- [11] NVMe Health Information — `CANDIDATE`
- [12] NVMe Wear Percentage — `CANDIDATE`
- [13] SSD Remaining Life Estimate — `IDEA`
- [17] Physical Error Counter Monitoring — `CANDIDATE`
- [18] Storage Error Trend — `IDEA`
- [19] Unsafe Shutdown Count — `CANDIDATE`
- [20] Disk Power-On Hours — `CANDIDATE`
- [21] Disk Power Cycle Count — `CANDIDATE`
- [72] Storage Benchmark Wear Safeguard — `CANDIDATE`
- [107] Storage Spaces Physical Disk Health — `CANDIDATE`
- [115] Storage Event Log Analysis — `CANDIDATE`
- [140] Host Reads / Writes Monitoring — `CANDIDATE`
- [141] NVMe Controller Busy Time — `CANDIDATE`
- [142] NVMe Critical Warning Detection — `CANDIDATE`
- [143] NVMe Available Spare Monitoring — `CANDIDATE`
- [144] NVMe Media/Data Integrity Error Trend — `CANDIDATE`
- [193] Sustained Write Wear Budget — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-003 — Storage temperature & thermal throttling

Status: CANDIDATE

Type: Diagnostic, Monitoring, Recommendation

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [14] Storage Temperature Monitoring — `CANDIDATE`
- [15] Storage Thermal Throttling Detection — `CANDIDATE`
- [16] Storage Cooling Advisor — `IDEA`
- [180] External Storage Thermal Analysis — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-004 — Capacity, volumes, partitions & free-space diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic Tool, Monitoring, Optimization, Tool

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [22] Storage Capacity Detection — `CANDIDATE`
- [23] Volume Inventory — `CANDIDATE`
- [24] Partition Layout Detection — `CANDIDATE`
- [25] GPT / MBR Detection — `CANDIDATE`
- [26] Unexpected Unallocated Space Detection — `IDEA`
- [27] Low Free Space Detection — `CANDIDATE`
- [28] Free Space Trend — `IDEA`
- [29] “What Is Using My Disk Space?” Analysis — `CANDIDATE`
- [30] Large File Discovery — `CANDIDATE`
- [31] Duplicate File Discovery — `IDEA`
- [181] SSD Free-Space Performance Context — `CANDIDATE`
- [182] Low SSD Free-Space Warning — `CANDIDATE`
- [183] SSD Overprovisioning Awareness — `IDEA`
- [184] Manual Overprovisioning Recommendation — `QUESTIONABLE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-005 — Filesystem health, CHKDSK & allocation diagnostics

Status: CANDIDATE

Type: Configuration, Diagnostic, Diagnostic / Repair, Repair, Safety / Diagnostic

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [32] Filesystem Type Detection — `CANDIDATE`
- [33] Filesystem Health Detection — `CANDIDATE`
- [34] CHKDSK Online Scan — `CANDIDATE`
- [35] CHKDSK Repair Scheduling — `CANDIDATE`
- [36] Filesystem Error History — `CANDIDATE`
- [79] Filesystem Allocation Unit Detection — `IDEA`
- [80] Filesystem Compression Detection — `CANDIDATE`
- [81] CompactOS State Detection — `CANDIDATE`
- [82] CompactOS Configuration — `RESEARCHING`
- [83] Sparse File Awareness — `IDEA`
- [84] Hard Link Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-006 — TRIM, ReTRIM & media-aware optimization

Status: CANDIDATE

Type: Diagnostic, Maintenance, Safety

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [37] TRIM Capability Detection — `CANDIDATE`
- [38] ReTRIM Capability — `CANDIDATE`
- [39] TRIM Misconfiguration Detection — `CANDIDATE`
- [40] Volume Optimization Analysis — `CANDIDATE`
- [41] Media-Aware Optimize Volume — `CANDIDATE`
- [42] HDD Fragmentation Analysis — `CANDIDATE`
- [43] HDD Defragmentation — `CANDIDATE`
- [44] SSD Manual Defrag Guard — `CANDIDATE`
- [45] Storage Maintenance Schedule Detection — `CANDIDATE`
- [46] Redundant Optimization Detection — `IDEA`
- [153] TRIM Filesystem State Detection — `CANDIDATE`
- [154] TRIM End-to-End Confidence — `RESEARCHING`
- [155] TRIM Pass-Through Context — `IDEA`
- [156] ReTRIM Need Analysis — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-007 — Storage write-cache & flush-policy diagnostics

Status: CANDIDATE

Type: Configuration, Diagnostic

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [47] Storage Write Cache Detection — `CANDIDATE`
- [48] Write Cache Policy Modification — `RESEARCHING`
- [49] Storage Flush Behavior Context — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-008 — Storage driver, controller & link health

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Guidance

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [53] NVMe Link Bottleneck Detection — `CANDIDATE`
- [54] NVMe Shared-Lane / Platform Constraint Context — `IDEA`
- [56] Storage Driver Version Detection — `CANDIDATE`
- [57] Storage Driver Problem Detection — `CANDIDATE`
- [116] Storage Timeout Detection — `CANDIDATE`
- [117] Surprise Removal Detection — `CANDIDATE`
- [118] SATA Cable / Link Error Advisor — `IDEA`
- [119] Storage Driver Reset Correlation — `IDEA`
- [178] External SSD Interface Bottleneck — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-009 — Storage I/O performance & saturation diagnostics

Status: CANDIDATE

Type: Benchmark, Diagnostic, Monitoring

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [58] Storage I/O Throughput Monitoring — `CANDIDATE`
- [59] Storage I/O by Process — `CANDIDATE`
- [60] Disk Active Time Analysis — `CANDIDATE`
- [61] Storage Latency Monitoring — `CANDIDATE`
- [62] High Storage Latency Detection — `CANDIDATE`
- [63] Storage Queue Depth Monitoring — `CANDIDATE`
- [64] Storage Saturation Detection — `CANDIDATE`
- [71] Low Queue Depth Benchmark — `CANDIDATE`
- [120] Storage Activity / Stutter Correlation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-010 — Storage benchmark suite & methodology

Status: CANDIDATE

Type: Benchmark, Benchmark / Diagnostic, Benchmark Infrastructure, Benchmark Safeguard, Diagnostic, Product Behavior, Safety, Validation

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [65] Sequential Read Benchmark — `CANDIDATE`
- [66] Sequential Write Benchmark — `CANDIDATE`
- [67] Random Read Benchmark — `CANDIDATE`
- [68] Random Write Benchmark — `CANDIDATE`
- [69] Storage Latency Benchmark — `CANDIDATE`
- [70] Queue-Depth Benchmark — `IDEA`
- [73] Storage Benchmark Thermal Readiness — `CANDIDATE`
- [74] Sustained Storage Performance Test — `CANDIDATE`
- [75] SSD Cache Exhaustion Detection — `CANDIDATE`
- [76] SSD Sustained Write Characterization — `IDEA`
- [77] Storage Benchmark Before × After — `CANDIDATE`
- [78] Storage Performance Baseline vs Expected — `IDEA`
- [123] Storage Benchmark During Background I/O Guard — `CANDIDATE`
- [124] Storage Benchmark Free-Space Safeguard — `CANDIDATE`
- [125] Storage Benchmark File Cleanup — `CANDIDATE`
- [126] Storage Benchmark Result Interpretation — `CANDIDATE`
- [185] Storage Benchmark Cache-Control Strategy — `CANDIDATE`
- [186] Buffered vs Unbuffered Storage Benchmark — `CANDIDATE`
- [187] Storage Benchmark Test-File Sizing — `CANDIDATE`
- [188] Compressibility-Safe Benchmark Data — `CANDIDATE`
- [189] Storage Benchmark Alignment — `CANDIDATE`
- [190] Storage Benchmark Queue Profile — `CANDIDATE`
- [191] Desktop Storage Responsiveness Profile — `CANDIDATE`
- [192] Game Asset Streaming Storage Profile — `IDEA`
- [194] Benchmark Thermal Recovery — `CANDIDATE`
- [195] Read-Only Storage Benchmark Mode — `CANDIDATE`
- [196] Storage Benchmark Result Confidence — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-011 — Cleanup/storage-space artifacts & safeguards

Status: CANDIDATE

Type: Cleanup, Diagnostic, Diagnostic / Cleanup, Safety, Safety / Diagnostic, Safety / Product Behavior, Validation

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [85] Cloud Placeholder Awareness — `CANDIDATE`
- [86] Windows.old Detection — `CANDIDATE`
- [87] Update Cache Detection — `CANDIDATE`
- [88] Delivery Optimization Cache Detection — `CANDIDATE`
- [89] DirectX Shader Cache Detection — `CANDIDATE`
- [90] Browser / Application Cache Inventory — `CANDIDATE`
- [91] Crash Dump Storage Analysis — `CANDIDATE`
- [92] Log Growth Detection — `CANDIDATE`
- [93] Temporary File Classification — `CANDIDATE`
- [94] Cleanup Preview — `CANDIDATE`
- [95] Cleanup Verification — `CANDIDATE`
- [96] Cleanup Rollback Capability — `RESEARCHING`
- [97] User Data Protection Guard — `CANDIDATE`
- [198] Cleanup Performance Myth Guard — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-012 — System restore, VSS & shadow-storage diagnostics

Status: CANDIDATE

Type: Cleanup, Configuration / Recommendation, Diagnostic

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [98] System Restore Storage Usage — `CANDIDATE`
- [99] Shadow Storage Pressure Detection — `IDEA`
- [160] VSS Snapshot Inventory — `CANDIDATE`
- [161] Shadow Storage Allocation Detection — `CANDIDATE`
- [162] Shadow Storage Usage Analysis — `CANDIDATE`
- [163] Shadow Storage Resize Advisor — `RESEARCHING`
- [164] Delete Restore Points for Space — `QUESTIONABLE`
- [165] VSS Writer Health — `CANDIDATE`
- [166] VSS Provider Inventory — `IDEA`
- [167] Orphaned / Excessive Snapshot Diagnosis — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-013 — BitLocker storage context & safeguards

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Benchmark Safeguard

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [100] BitLocker State Detection — `CANDIDATE`
- [101] BitLocker Encryption-in-Progress Detection — `CANDIDATE`
- [102] BitLocker Performance Context — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-014 — Storage Spaces, pools, virtual disks & tiers

Status: CANDIDATE

Type: Diagnostic, Maintenance, Repair Guidance

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [104] Storage Spaces Detection — `CANDIDATE`
- [105] Storage Pool Health — `CANDIDATE`
- [106] Virtual Disk Health — `CANDIDATE`
- [108] Storage Pool Repair Guidance — `IDEA`
- [109] Thin Provisioning Awareness — `CANDIDATE`
- [110] Storage Tier Detection — `CANDIDATE`
- [111] Tier Optimization — `CANDIDATE`
- [151] RAID Health Context — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-015 — DirectStorage & BypassIO readiness diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Product Behavior, Diagnostic / Recommendation, Safety / Explainability

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [112] DirectStorage Capability Detection — `CANDIDATE`
- [113] DirectStorage Readiness Summary — `CANDIDATE`
- [114] “Enable DirectStorage” Myth Guard — `CANDIDATE`
- [168] BypassIO Capability Detection — `CANDIDATE`
- [169] BypassIO Blocking Driver Detection — `CANDIDATE`
- [170] BypassIO Partial Support Detection — `CANDIDATE`
- [171] BypassIO Filter Compatibility Advisor — `IDEA`
- [173] DirectStorage Stack Readiness — `CANDIDATE`
- [174] DirectStorage Limiter Explanation — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-016 — Game storage placement guidance

Status: IDEA

Type: Diagnostic / Recommendation, Recommendation

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [121] Game Storage Location Analysis — `IDEA`
- [122] Move Game to Faster Storage Advisor — `IDEA`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-017 — Storage optimization validation, risk & summary

Status: CANDIDATE

Type: Diagnostic / Product Behavior, Product Behavior, Safety, Shared Capability, Validation

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [127] Storage Health Summary — `CANDIDATE`
- [128] Storage Optimization Myth Guard — `CANDIDATE`
- [197] Storage Optimization Validation — `CANDIDATE`
- [199] Storage Change Risk Classification — `CANDIDATE`
- [200] Storage Diagnostic Summary — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-018 — NVMe power-management diagnostics

Status: CANDIDATE

Type: Advanced Configuration, Diagnostic, Diagnostic / Benchmark, Safety

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [129] NVMe Power State Capability Detection — `CANDIDATE`
- [130] NVMe Runtime Power Management Analysis — `CANDIDATE`
- [131] NVMe APST Capability Detection — `CANDIDATE`
- [132] NVMe APST State Detection — `RESEARCHING`
- [133] NVMe APST Tuning — `QUESTIONABLE`
- [134] NVMe Power-State Latency Analysis — `IDEA`
- [135] NVMe Power Management Myth Guard — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-019 — SSD architecture & endurance context

Status: CANDIDATE

Type: Diagnostic, Shared Diagnostic Capability

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [136] Host Memory Buffer Capability Detection — `RESEARCHING`
- [137] DRAM-less SSD Context Detection — `RESEARCHING`
- [138] SSD Architecture Metadata Layer — `IDEA`
- [139] SSD Rated Endurance Context — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-020 — Storage unsafe/low-value tweak guards

Status: CANDIDATE

Type: Configuration, Safety

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [152] RAID Modification Guard — `CANDIDATE`
- [159] Disable Reserved Storage for Optimization — `QUESTIONABLE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-STORAGE-021 — Reserved storage awareness

Status: CANDIDATE

Type: Diagnostic, Explainability

Technical Domain: STORAGE

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Unknown

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [157] Reserved Storage State Detection — `CANDIDATE`
- [158] Reserved Storage Explanation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

# Rejected / removed from active capability catalog

> These items remain recorded for traceability but do not proceed as active capabilities.

## R-STORAGE-103 — Disable BitLocker for Performance

Original status: `REJECTED`

Decision: Keep rejected; do not promote to Feature Spec unless new evidence materially changes the assessment.

## R-STORAGE-149 — VMD Disable Optimization

Original status: `REJECTED`

Decision: Keep rejected; do not promote to Feature Spec unless new evidence materially changes the assessment.

## R-STORAGE-172 — Disable Security Filter for BypassIO

Original status: `REJECTED`

Decision: Keep rejected; do not promote to Feature Spec unless new evidence materially changes the assessment.
