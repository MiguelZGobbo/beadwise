# Final Discovery — STORAGE

> Canonical rules: `01-DISCOVERY.md`
> Scope boundary: `02-FEATURE-SPEC-TEMPLATE.md` is used only to keep implementation research out of Discovery.

## Final review result

- Raw discovery items represented: **200**
- Consolidated capability groups: **21**
- Active non-duplicate groups: **21**
- `READY_FOR_SPEC`: **21**
- `CANDIDATE`: **0**
- `QUESTIONABLE`: **0**
- `DUPLICATE`: **0**

> A status decision is conceptual only. `READY_FOR_SPEC` does not imply implementation, API, Registry, rollback, benchmark proof, test or compatibility method already resolved.

## C-STORAGE-001 — Storage hardware inventory & interface context

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Recommendation

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para detectar e contextualizar **storage hardware inventory & interface context**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
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

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-002 — Storage health, SMART/NVMe & wear diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Monitoring, Safety

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar **storage health, smart/nvme & wear diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
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

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-003 — Storage temperature & thermal throttling

Status: READY_FOR_SPEC

Type: Diagnostic, Monitoring, Recommendation

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para diagnosticar **storage temperature & thermal throttling**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [14] Storage Temperature Monitoring — `CANDIDATE`
- [15] Storage Thermal Throttling Detection — `CANDIDATE`
- [16] Storage Cooling Advisor — `IDEA`
- [180] External Storage Thermal Analysis — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-004 — Capacity, volumes, partitions & free-space diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic Tool, Monitoring, Optimization, Tool

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar **capacity, volumes, partitions & free-space diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Recuperação de espaço ou redução de acúmulo desnecessário com proteção de dados do usuário.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
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

Limite conceitual:
O `READY_FOR_SPEC` vale para a responsabilidade principal descrita nesta capability. Subitens de origem marcados `QUESTIONABLE` continuam questionáveis e **não** são aprovados implicitamente; qualquer alteração agressiva neles representada permanece fora do escopo ativo até justificativa própria.

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-005 — Filesystem health, CHKDSK & allocation diagnostics

Status: READY_FOR_SPEC

Type: Configuration, Diagnostic, Diagnostic / Repair, Repair, Safety / Diagnostic

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar **filesystem health, chkdsk & allocation diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
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

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-006 — TRIM, ReTRIM & media-aware optimization

Status: READY_FOR_SPEC

Type: Diagnostic, Maintenance, Safety

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **trim, retrim & media-aware optimization**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
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

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-007 — Storage write-cache & flush-policy diagnostics

Status: READY_FOR_SPEC

Type: Configuration, Diagnostic

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **storage write-cache & flush-policy diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [47] Storage Write Cache Detection — `CANDIDATE`
- [48] Write Cache Policy Modification — `RESEARCHING`
- [49] Storage Flush Behavior Context — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-008 — Storage driver, controller & link health

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Guidance

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **storage driver, controller & link health**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [53] NVMe Link Bottleneck Detection — `CANDIDATE`
- [54] NVMe Shared-Lane / Platform Constraint Context — `IDEA`
- [56] Storage Driver Version Detection — `CANDIDATE`
- [57] Storage Driver Problem Detection — `CANDIDATE`
- [116] Storage Timeout Detection — `CANDIDATE`
- [117] Surprise Removal Detection — `CANDIDATE`
- [118] SATA Cable / Link Error Advisor — `IDEA`
- [119] Storage Driver Reset Correlation — `IDEA`
- [178] External SSD Interface Bottleneck — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-009 — Storage I/O performance & saturation diagnostics

Status: READY_FOR_SPEC

Type: Benchmark, Diagnostic, Monitoring

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para diagnosticar **storage i/o performance & saturation diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [58] Storage I/O Throughput Monitoring — `CANDIDATE`
- [59] Storage I/O by Process — `CANDIDATE`
- [60] Disk Active Time Analysis — `CANDIDATE`
- [61] Storage Latency Monitoring — `CANDIDATE`
- [62] High Storage Latency Detection — `CANDIDATE`
- [63] Storage Queue Depth Monitoring — `CANDIDATE`
- [64] Storage Saturation Detection — `CANDIDATE`
- [71] Low Queue Depth Benchmark — `CANDIDATE`
- [120] Storage Activity / Stutter Correlation — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-010 — Storage benchmark suite & methodology

Status: READY_FOR_SPEC

Type: Benchmark, Benchmark / Diagnostic, Benchmark Infrastructure, Benchmark Safeguard, Diagnostic, Product Behavior, Safety, Validation

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para medir e comparar **storage benchmark suite & methodology**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Fornecer uma medição reproduzível e interpretável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Medição comparável para compreender desempenho e validar se uma mudança realmente produziu efeito.

Aplicabilidade:
quando for necessário quantificar desempenho, regressão ou resultado de uma alteração.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
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

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-011 — Cleanup/storage-space artifacts & safeguards

Status: READY_FOR_SPEC

Type: Cleanup, Diagnostic, Diagnostic / Cleanup, Safety, Safety / Diagnostic, Safety / Product Behavior, Validation

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **cleanup/storage-space artifacts & safeguards**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Recuperação de espaço ou redução de acúmulo desnecessário com proteção de dados do usuário.

Aplicabilidade:
quando uma configuração ou recomendação puder ser confundida com otimização universal.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
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

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-012 — System restore, VSS & shadow-storage diagnostics

Status: READY_FOR_SPEC

Type: Cleanup, Configuration / Recommendation, Diagnostic

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para diagnosticar **system restore, vss & shadow-storage diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
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

Limite conceitual:
O `READY_FOR_SPEC` vale para a responsabilidade principal descrita nesta capability. Subitens de origem marcados `QUESTIONABLE` continuam questionáveis e **não** são aprovados implicitamente; qualquer alteração agressiva neles representada permanece fora do escopo ativo até justificativa própria.

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-013 — BitLocker storage context & safeguards

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Benchmark Safeguard

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para detectar e contextualizar **bitlocker storage context & safeguards**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Evita alterações populares, agressivas ou cosméticas que não apresentem benefício técnico proporcional.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [100] BitLocker State Detection — `CANDIDATE`
- [101] BitLocker Encryption-in-Progress Detection — `CANDIDATE`
- [102] BitLocker Performance Context — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-014 — Storage Spaces, pools, virtual disks & tiers

Status: READY_FOR_SPEC

Type: Diagnostic, Maintenance, Repair Guidance

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para avaliar **storage spaces, pools, virtual disks & tiers**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Recuperação de espaço ou redução de acúmulo desnecessário com proteção de dados do usuário.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [104] Storage Spaces Detection — `CANDIDATE`
- [105] Storage Pool Health — `CANDIDATE`
- [106] Virtual Disk Health — `CANDIDATE`
- [108] Storage Pool Repair Guidance — `IDEA`
- [109] Thin Provisioning Awareness — `CANDIDATE`
- [110] Storage Tier Detection — `CANDIDATE`
- [111] Tier Optimization — `CANDIDATE`
- [151] RAID Health Context — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-015 — DirectStorage & BypassIO readiness diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Product Behavior, Diagnostic / Recommendation, Safety / Explainability

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para diagnosticar **directstorage & bypassio readiness diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [112] DirectStorage Capability Detection — `CANDIDATE`
- [113] DirectStorage Readiness Summary — `CANDIDATE`
- [114] “Enable DirectStorage” Myth Guard — `CANDIDATE`
- [168] BypassIO Capability Detection — `CANDIDATE`
- [169] BypassIO Blocking Driver Detection — `CANDIDATE`
- [170] BypassIO Partial Support Detection — `CANDIDATE`
- [171] BypassIO Filter Compatibility Advisor — `IDEA`
- [173] DirectStorage Stack Readiness — `CANDIDATE`
- [174] DirectStorage Limiter Explanation — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-016 — Game storage placement guidance

Status: READY_FOR_SPEC

Type: Diagnostic / Recommendation, Recommendation

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **game storage placement guidance**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [121] Game Storage Location Analysis — `IDEA`
- [122] Move Game to Faster Storage Advisor — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-017 — Storage optimization outcome validation & risk guard

Status: READY_FOR_SPEC

Type: Diagnostic / Product Behavior, Product Behavior, Safety, Shared Capability, Validation

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **storage optimization outcome validation & risk guard**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Evita alterações populares, agressivas ou cosméticas que não apresentem benefício técnico proporcional.

Aplicabilidade:
quando uma configuração ou recomendação puder ser confundida com otimização universal.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [127] Storage Health Summary — `CANDIDATE`
- [128] Storage Optimization Myth Guard — `CANDIDATE`
- [197] Storage Optimization Validation — `CANDIDATE`
- [199] Storage Change Risk Classification — `CANDIDATE`
- [200] Storage Diagnostic Summary — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-018 — NVMe power-management diagnostics

Status: READY_FOR_SPEC

Type: Advanced Configuration, Diagnostic, Diagnostic / Benchmark, Safety

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para diagnosticar **nvme power-management diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [129] NVMe Power State Capability Detection — `CANDIDATE`
- [130] NVMe Runtime Power Management Analysis — `CANDIDATE`
- [131] NVMe APST Capability Detection — `CANDIDATE`
- [132] NVMe APST State Detection — `RESEARCHING`
- [133] NVMe APST Tuning — `QUESTIONABLE`
- [134] NVMe Power-State Latency Analysis — `IDEA`
- [135] NVMe Power Management Myth Guard — `CANDIDATE`

Limite conceitual:
O `READY_FOR_SPEC` vale para a responsabilidade principal descrita nesta capability. Subitens de origem marcados `QUESTIONABLE` continuam questionáveis e **não** são aprovados implicitamente; qualquer alteração agressiva neles representada permanece fora do escopo ativo até justificativa própria.

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-019 — SSD architecture & endurance context

Status: READY_FOR_SPEC

Type: Diagnostic, Shared Diagnostic Capability

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para detectar e contextualizar **ssd architecture & endurance context**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [136] Host Memory Buffer Capability Detection — `RESEARCHING`
- [137] DRAM-less SSD Context Detection — `RESEARCHING`
- [138] SSD Architecture Metadata Layer — `IDEA`
- [139] SSD Rated Endurance Context — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-020 — Storage unsafe & low-value optimization guard

Status: READY_FOR_SPEC

Type: Configuration, Safety

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **storage unsafe & low-value optimization guard**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Evita alterações populares, agressivas ou cosméticas que não apresentem benefício técnico proporcional.

Aplicabilidade:
quando uma configuração ou recomendação puder ser confundida com otimização universal.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [152] RAID Modification Guard — `CANDIDATE`
- [159] Disable Reserved Storage for Optimization — `QUESTIONABLE`

Limite conceitual:
O `READY_FOR_SPEC` vale para a responsabilidade principal descrita nesta capability. Subitens de origem marcados `QUESTIONABLE` continuam questionáveis e **não** são aprovados implicitamente; qualquer alteração agressiva neles representada permanece fora do escopo ativo até justificativa própria.

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STORAGE-021 — Reserved storage awareness

Status: READY_FOR_SPEC

Type: Diagnostic, Explainability

Technical Domain: STORAGE

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Unknown

Descrição:
Capability de produto para detectar e contextualizar **reserved storage awareness**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [157] Reserved Storage State Detection — `CANDIDATE`
- [158] Reserved Storage Explanation — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.
