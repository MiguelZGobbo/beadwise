# Final Discovery — ENERGY

> Canonical rules: `01-DISCOVERY.md`
> Scope boundary: `02-FEATURE-SPEC-TEMPLATE.md` is used only to keep implementation research out of Discovery.

## Final review result

- Raw discovery items represented: **100**
- Consolidated capability groups: **13**
- Active non-duplicate groups: **12**
- `READY_FOR_SPEC`: **11**
- `CANDIDATE`: **1**
- `QUESTIONABLE`: **0**
- `DUPLICATE`: **1**

> A status decision is conceptual only. `READY_FOR_SPEC` does not imply implementation, API, Registry, rollback, benchmark proof, test or compatibility method already resolved.

## C-ENERGY-001 — Power scheme inventory, switching & state

Status: READY_FOR_SPEC

Type: Configuration / Optimization, Diagnostic, Diagnostic / Configuration, Diagnostic / Product Behavior, Diagnostic / Repair, Monitoring, Safety / Configuration

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para detectar e contextualizar **power scheme inventory, switching & state**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais estados são apenas informativos e quais alterações, se alguma, têm benefício contextual suficiente para serem investigadas tecnicamente na Feature Spec?

Origem / itens consolidados:
- [1] Power Scheme Inventory — `CANDIDATE`
- [2] Power Scheme Switching — `CANDIDATE`
- [3] Power Scheme Backup / Restore — `CANDIDATE`
- [4] Power Scheme Configuration Diff — `IDEA`
- [5] Broken / Anomalous Power Scheme Detection — `IDEA`
- [6] Custom BeadWise Power Profile — `RESEARCHING`
- [7] Ultimate Performance Availability — `CANDIDATE`
- [61] User-Configured Power Mode Detection — `CANDIDATE`
- [62] AC Power Mode Detection — `CANDIDATE`
- [63] DC Power Mode Detection — `CANDIDATE`
- [64] Effective Power Mode Detection — `CANDIDATE`
- [65] Power Mode Change Monitoring — `IDEA`
- [66] Power Scheme × Power Mode Interaction Analysis — `CANDIDATE`
- [67] OEM Power Overlay Detection — `RESEARCHING`
- [68] Effective Power Configuration Explanation — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-ENERGY-002 — Dynamic workload power profiles

Status: CANDIDATE

Type: Configuration, Diagnostic, Optimization / Automation

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **dynamic workload power profiles**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
A proposta é conceitualmente plausível, mas ainda precisa de evidência adicional de utilidade/escopo antes de avançar para Feature Spec.

Origem / itens consolidados:
- [8] AC × Battery Policy Detection — `CANDIDATE`
- [9] AC/Battery Profile Switching — `IDEA`
- [59] Dynamic Workload Power Profile — `IDEA`

Decisão de revisão:
Manter no Discovery, mas não avançar ainda. O conceito precisa amadurecer quanto a benefício/escopo, sem antecipar investigação técnica de implementação.

## C-ENERGY-003 — Processor power policy diagnostics & tuning

Status: READY_FOR_SPEC

Type: Configuration / Optimization, Diagnostic, Diagnostic / Configuration, Optimization / Configuration, Optimization / Recommendation

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **processor power policy diagnostics & tuning**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
Quais estados são apenas informativos e quais alterações, se alguma, têm benefício contextual suficiente para serem investigadas tecnicamente na Feature Spec?

Origem / itens consolidados:
- [10] Processor Minimum Performance State — `CANDIDATE`
- [11] Processor Maximum Performance State — `CANDIDATE`
- [12] Processor Boost Mode Detection — `CANDIDATE`
- [13] Processor Boost Policy Tuning — `RESEARCHING`
- [14] Energy Performance Preference — EPP Detection — `CANDIDATE`
- [15] EPP Tuning — `RESEARCHING`
- [16] CPPC / Hardware Autonomous Performance Detection — `CANDIDATE`
- [17] Core Parking State Detection — `CANDIDATE`
- [18] Core Parking Policy Tuning — `RESEARCHING`
- [19] Processor Idle State Diagnostics — `IDEA`
- [53] Power Throttling State Analysis — `RESEARCHING`
- [69] EcoQoS Capability Detection — `CANDIDATE`
- [70] Process EcoQoS State Detection — `RESEARCHING`
- [71] Background Work EcoQoS Advisor — `IDEA`
- [72] Power Throttling Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-ENERGY-004 — Energy efficiency & performance-per-watt analysis

Status: READY_FOR_SPEC

Type: Benchmark, Benchmark / Diagnostic, Diagnostic

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **energy efficiency & performance-per-watt analysis**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [20] Idle Power Consumption Diagnosis — `CANDIDATE`
- [42] Energy Efficiency Scan — `CANDIDATE`
- [55] Performance-per-Watt Benchmark — `IDEA`
- [56] Thermal / Power Efficiency Comparison — `IDEA`
- [57] Power-Limited Performance Detection — `CANDIDATE`
- [58] Performance Regression Caused by Energy Policy — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-ENERGY-005 — Sleep, wake, hibernate & fast-startup diagnostics

Status: READY_FOR_SPEC

Type: Configuration, Configuration / Storage, Diagnostic, Diagnostic / Configuration, Diagnostic / Repair

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para diagnosticar **sleep, wake, hibernate & fast-startup diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [21] Power Requests Detection — `CANDIDATE`
- [22] Power Request Override Advisor — `RESEARCHING`
- [23] Available Sleep States Detection — `CANDIDATE`
- [24] Modern Standby Detection — `CANDIDATE`
- [25] Modern Standby Quality Analysis — `CANDIDATE`
- [26] Sleep Transition Diagnostics — `CANDIDATE`
- [27] Last Wake Source Detection — `CANDIDATE`
- [28] Wake-Capable Device Inventory — `CANDIDATE`
- [29] Device Wake Permission Management — `CANDIDATE`
- [30] Wake Timer Detection — `CANDIDATE`
- [31] Wake Timer Policy Management — `RESEARCHING`
- [32] Hibernate Availability / Configuration — `CANDIDATE`
- [33] Hibernation File Configuration — `RESEARCHING`
- [34] Fast Startup State Detection — `CANDIDATE`
- [35] Fast Startup Configuration — `RESEARCHING`
- [41] Sleep Timeout Policy — `CANDIDATE`
- [78] Modern Standby DRIPS Residency Analysis — `CANDIDATE`
- [79] Poor DRIPS Residency Detection — `CANDIDATE`
- [80] Modern Standby Offender Detection — `CANDIDATE`
- [81] Modern Standby Driver Problem Detection — `CANDIDATE`
- [82] Standby Drain Session Comparison — `IDEA`
- [83] Standby Energy Regression Detection — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-ENERGY-006 — Device power-management diagnostics

Status: READY_FOR_SPEC

Type: Configuration / Optimization, Diagnostic, Diagnostic / Repair

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar **device power-management diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais estados são apenas informativos e quais alterações, se alguma, têm benefício contextual suficiente para serem investigadas tecnicamente na Feature Spec?

Origem / itens consolidados:
- [36] USB Selective Suspend State — `CANDIDATE`
- [37] USB Selective Suspend Troubleshooter — `CANDIDATE`
- [38] PCIe Link State Power Management Detection — `CANDIDATE`
- [39] PCIe ASPM Policy Tuning — `RESEARCHING`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-ENERGY-007 — Display energy-management diagnostics

Status: READY_FOR_SPEC

Type: Configuration, Configuration / Recommendation, Diagnostic, Diagnostic / Configuration, Diagnostic / Recommendation

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **display energy-management diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [40] Display Power Policy — `CANDIDATE`
- [88] Display Refresh Rate Energy Analysis — `CANDIDATE`
- [89] Dynamic Refresh Rate Detection — `CANDIDATE`
- [90] Dynamic Refresh Rate Recommendation — `CANDIDATE`
- [91] Battery Refresh Rate Profile — `IDEA`
- [92] Adaptive Brightness Capability Detection — `CANDIDATE`
- [93] Content Adaptive Brightness Detection — `CANDIDATE`
- [94] Display Power Consumption Advisor — `IDEA`
- [95] OLED Energy Context — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-ENERGY-008 — Energy recommendation confidence

Status: DUPLICATE

Type: Diagnostic Safeguard, Product Behavior

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **energy recommendation confidence**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Ownership consolidado em C-SYSTEM-003. Esta entrada não deve gerar Feature Spec independente.

Duplicate of:
C-SYSTEM-003

Origem / itens consolidados:
- [43] Energy Diagnostic Readiness Check — `IDEA`
- [100] Energy Recommendation Confidence — `IDEA`

Decisão de revisão:
Não manter como capability ativa independente. O comportamento é coberto por C-SYSTEM-003; esta entrada permanece apenas para registrar a decisão de ownership.

## C-ENERGY-009 — Energy state history, conflict & outcome validation

Status: READY_FOR_SPEC

Type: Benchmark / Validation, Diagnostic, Monitoring / Diagnostic

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para correlacionar e contextualizar **energy state history, conflict & outcome validation**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Ajudar a distinguir coincidência de causa provável e explicar mudanças ao longo do tempo, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando histórico, comparação ou múltiplas evidências forem necessárias para interpretar um problema.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [44] Historical Energy Usage — `CANDIDATE`
- [60] Power Profile Before × After Validation — `CANDIDATE`
- [98] Energy Optimization Conflict Detection — `IDEA`
- [99] Energy State Timeline — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-ENERGY-010 — Application/process energy attribution & policy

Status: READY_FOR_SPEC

Type: Configuration, Diagnostic, Diagnostic / Configuration, Optimization

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **application/process energy attribution & policy**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
Quais estados são apenas informativos e quais alterações, se alguma, têm benefício contextual suficiente para serem investigadas tecnicamente na Feature Spec?

Origem / itens consolidados:
- [45] Energy Consumption by Application — `IDEA`
- [54] Per-Process Energy Efficiency Policy — `RESEARCHING`
- [84] Hybrid Graphics Capability Detection — `CANDIDATE`
- [85] Application GPU Power Preference — `CANDIDATE`
- [86] Unnecessary dGPU Activation Detection — `CANDIDATE`
- [87] GPU Power Preference Recommendation — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-ENERGY-011 — Battery capability, health & drain diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Monitoring

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **battery capability, health & drain diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [46] Battery Presence / Capability Detection — `CANDIDATE`
- [47] Battery Health Overview — `CANDIDATE`
- [48] Battery Usage Report — `CANDIDATE`
- [49] Abnormal Battery Drain Detection — `IDEA`
- [50] Charging / Discharging Rate Monitoring — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-ENERGY-012 — Energy Saver diagnostics & configuration

Status: READY_FOR_SPEC

Type: Configuration, Diagnostic

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **energy saver diagnostics & configuration**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [51] Energy Saver State Detection — `CANDIDATE`
- [52] Energy Saver Configuration — `CANDIDATE`
- [96] Energy Saver Background Activity Impact — `CANDIDATE`
- [97] Energy Saver Automatic Activation Policy — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-ENERGY-013 — Timer-resolution energy diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Benchmark, Product Behavior / Safety

Technical Domain: ENERGY

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics
- My PC

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **timer-resolution energy diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [73] Timer Resolution Request Detection — `CANDIDATE`
- [74] Excessive Timer Resolution Diagnostic — `CANDIDATE`
- [75] Timer Resolution Energy Correlation — `IDEA`
- [76] Timer Resolution Myth Guard — `CANDIDATE`
- [77] Occluded Process Timer Throttling Detection — `RESEARCHING`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.
