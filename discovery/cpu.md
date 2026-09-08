# Final Discovery — CPU

> Canonical rules: `01-DISCOVERY.md`
> Scope boundary: `02-FEATURE-SPEC-TEMPLATE.md` is used only to keep implementation research out of Discovery.

## Final review result

- Raw discovery items represented: **138**
- Consolidated capability groups: **15**
- Active non-duplicate groups: **14**
- `READY_FOR_SPEC`: **13**
- `CANDIDATE`: **0**
- `QUESTIONABLE`: **1**
- `DUPLICATE`: **1**

> A status decision is conceptual only. `READY_FOR_SPEC` does not imply implementation, API, Registry, rollback, benchmark proof, test or compatibility method already resolved.

## C-CPU-001 — CPU identity, capabilities & topology

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **cpu identity, capabilities & topology**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [1] CPU Identification — `CANDIDATE`
- [2] CPU Topology Detection — `CANDIDATE`
- [3] Physical Core / Logical Processor Mapping — `CANDIDATE`
- [4] SMT / Hyper-Threading Detection — `CANDIDATE`
- [6] Hybrid CPU Architecture Detection — `CANDIDATE`
- [7] CPU Efficiency Class Mapping — `CANDIDATE`
- [8] CPU Cache Detection — `CANDIDATE`
- [9] CPU Instruction Set Detection — `CANDIDATE`
- [10] CPU Virtualization Capability Detection — `CANDIDATE`
- [47] Processor Group Detection — `CANDIDATE`
- [49] NUMA Topology Detection — `CANDIDATE`
- [82] AMD CCD Topology Detection — `CANDIDATE`
- [83] AMD CCX Topology Detection — `RESEARCHING`
- [84] AMD 3D V-Cache Capability Detection — `CANDIDATE`
- [88] Preferred / Favored Core Detection — `RESEARCHING`
- [93] Intel Hybrid Scheduling Capability Detection — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CPU-002 — CPU benchmark suite

Status: READY_FOR_SPEC

Type: Benchmark, Benchmark / Diagnostic, Benchmark / Validation, Benchmark Safeguard, Product Behavior

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para medir e comparar **cpu benchmark suite**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Fornecer uma medição reproduzível e interpretável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Medição comparável para compreender desempenho e validar se uma mudança realmente produziu efeito.

Aplicabilidade:
quando for necessário quantificar desempenho, regressão ou resultado de uma alteração.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [5] SMT Performance Comparison — `IDEA`
- [34] Single-Thread Benchmark — `CANDIDATE`
- [35] Multi-Thread Benchmark — `CANDIDATE`
- [60] CPU Benchmark Stability — `CANDIDATE`
- [68] CPU Performance Before × After — `CANDIDATE`
- [132] CPU Benchmark Warm-Up — `CANDIDATE`
- [133] CPU Benchmark Cooldown Control — `CANDIDATE`
- [134] CPU Test Repetition / Variance — `CANDIDATE`
- [135] Minimum Meaningful CPU Improvement — `IDEA`
- [138] CPU Optimization Multi-Metric Result — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CPU-003 — CPU utilization & saturation diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Monitoring

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **cpu utilization & saturation diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [11] Current CPU Utilization — `CANDIDATE`
- [12] Per-Core CPU Utilization — `CANDIDATE`
- [13] CPU Utility vs CPU Time — `CANDIDATE`
- [14] Single-Core Saturation Detection — `CANDIDATE`
- [16] Processor Queue Analysis — `CANDIDATE`
- [17] Context Switch Rate Monitoring — `CANDIDATE`
- [18] User Time / Kernel Time Analysis — `CANDIDATE`
- [19] Interrupt CPU Time Detection — `CANDIDATE`
- [20] DPC CPU Time Detection — `CANDIDATE`
- [62] CPU Background Interference Detection — `CANDIDATE`
- [63] CPU Idle Baseline — `CANDIDATE`
- [64] CPU Idle Anomaly Detection — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CPU-004 — CPU bottleneck & performance classification

Status: READY_FOR_SPEC

Type: Benchmark, Benchmark / Diagnostic, Diagnostic

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar **cpu bottleneck & performance classification**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [15] CPU Bottleneck Detection — `CANDIDATE`
- [32] CPU Sustained Performance Test — `CANDIDATE`
- [33] CPU Short Burst Performance Test — `CANDIDATE`
- [36] Scaling Efficiency Analysis — `IDEA`
- [61] CPU Performance Variability Detection — `IDEA`
- [69] CPU Performance Baseline vs Expected Hardware — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CPU-005 — CPU clocks, boost & performance-state diagnostics

Status: READY_FOR_SPEC

Type: Advanced Diagnostic, Diagnostic, Monitoring, Monitoring / Diagnostic

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **cpu clocks, boost & performance-state diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais estados são apenas informativos e quais alterações, se alguma, têm benefício contextual suficiente para serem investigadas tecnicamente na Feature Spec?

Origem / itens consolidados:
- [21] CPU Clock Detection — `CANDIDATE`
- [22] Per-Core Clock Monitoring — `CANDIDATE`
- [23] Effective Clock Monitoring — `CANDIDATE`
- [24] Base / Boost Frequency Validation — `CANDIDATE`
- [25] Boost Behavior Analysis — `CANDIDATE`
- [116] Clock Stretching Detection — `RESEARCHING`
- [117] Reported Clock × Effective Clock Comparison — `CANDIDATE`
- [118] Frequency Residency Analysis — `IDEA`
- [119] CPU Performance State Transition Analysis — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CPU-006 — CPU thermal, power & limiter diagnostics

Status: READY_FOR_SPEC

Type: Benchmark, Diagnostic, Monitoring

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **cpu thermal, power & limiter diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais estados são apenas informativos e quais alterações, se alguma, têm benefício contextual suficiente para serem investigadas tecnicamente na Feature Spec?

Origem / itens consolidados:
- [26] CPU Temperature Monitoring — `CANDIDATE`
- [27] Thermal Throttling Detection — `CANDIDATE`
- [28] CPU Power Throttling Detection — `CANDIDATE`
- [29] CPU Current / Electrical Limit Detection — `RESEARCHING`
- [30] CPU Package Power Monitoring — `CANDIDATE`
- [31] CPU Performance-per-Watt Analysis — `IDEA`
- [70] CPU Performance Limiter Classification — `IDEA`
- [96] Intel Package Power Limit Detection — `CANDIDATE`
- [97] Intel PL1 Analysis — `CANDIDATE`
- [98] Intel PL2 Analysis — `CANDIDATE`
- [99] Intel Turbo Time Window Analysis — `CANDIDATE`
- [100] Intel Platform Power Constraint Detection — `IDEA`
- [103] AMD PPT Detection — `CANDIDATE`
- [104] AMD TDC Detection — `CANDIDATE`
- [105] AMD EDC Detection — `CANDIDATE`
- [106] AMD Limiter Classification — `CANDIDATE`
- [107] AMD Eco Mode Detection — `CANDIDATE`
- [108] AMD Precision Boost Overdrive State Detection — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CPU-007 — CPU scheduler & workload placement diagnostics

Status: READY_FOR_SPEC

Type: Advanced Diagnostic, Configuration, Configuration / Optimization, Diagnostic, Product Behavior, Safety

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para diagnosticar **cpu scheduler & workload placement diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais estados são apenas informativos e quais alterações, se alguma, têm benefício contextual suficiente para serem investigadas tecnicamente na Feature Spec?

Origem / itens consolidados:
- [37] CPU Scheduler Observation — `CANDIDATE`
- [38] Hybrid Scheduler Behavior Analysis — `CANDIDATE`
- [39] Heterogeneous Scheduling Policy Detection — `CANDIDATE`
- [40] Heterogeneous Scheduling Policy Tuning — `RESEARCHING`
- [41] Process Affinity Detection — `CANDIDATE`
- [42] Unexpected Affinity Restriction Detection — `CANDIDATE`
- [43] Process Affinity Configuration — `RESEARCHING`
- [44] CPU Sets Detection — `CANDIDATE`
- [45] CPU Sets Configuration — `RESEARCHING`
- [46] Affinity vs CPU Set Conflict Detection — `IDEA`
- [48] Multi-Processor-Group Application Diagnosis — `IDEA`
- [50] NUMA Locality Diagnostic — `IDEA`
- [51] Process Priority Detection — `CANDIDATE`
- [52] Process Priority Configuration — `RESEARCHING`
- [54] Thread Priority Observation — `IDEA`
- [55] CPU Scheduler Trace — `CANDIDATE`
- [67] CPU Scheduling Regression Detection — `IDEA`
- [75] Hypervisor Scheduler Context — `IDEA`
- [78] CPU Affinity Myth Guard — `CANDIDATE`
- [85] AMD X3D Scheduling Support Detection — `CANDIDATE`
- [86] X3D Scheduling Health — `IDEA`
- [87] CCD Workload Distribution Analysis — `IDEA`
- [89] Preferred Core Scheduling Observation — `IDEA`
- [90] Thread Migration Analysis — `CANDIDATE`
- [91] Cross-Core-Class Migration Analysis — `IDEA`
- [92] CPU Ideal Processor Observation — `IDEA`
- [94] Hardware-Guided Scheduling Context — `RESEARCHING`
- [95] CPU Scheduling Intervention Threshold — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CPU-008 — CPU tuning controls & safeguards

Status: QUESTIONABLE

Type: Advanced Tuning, Benchmark, Diagnostic, Safety

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **cpu tuning controls & safeguards**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Evita alterações populares, agressivas ou cosméticas que não apresentem benefício técnico proporcional.

Aplicabilidade:
quando uma configuração ou recomendação puder ser confundida com otimização universal.

Dúvidas:
O benefício real é suficiente para justificar uma capability independente? Quais limites impedem que se transforme em tweak pack, preferência ou tuning de risco desproporcional?

Origem / itens consolidados:
- [53] Realtime Priority Safeguard — `CANDIDATE`
- [65] CPU Core Parking Observation — `CANDIDATE`
- [66] CPU Core Parking Benchmark — `RESEARCHING`
- [102] Intel Power Limit Modification — `QUESTIONABLE`
- [109] PBO Modification — `QUESTIONABLE`
- [110] CPU Tuning Mode Detection — `CANDIDATE`

Decisão de revisão:
Não avançar ainda. O grupo contém risco de tuning agressivo, benefício incerto ou comportamento que pode virar preferência/tweak pack; precisa de justificativa conceitual adicional antes de Feature Spec.

## C-CPU-009 — CPU PMU & hardware-counter diagnostics

Status: READY_FOR_SPEC

Type: Advanced Diagnostic, Advanced Diagnostic / Benchmark, Advanced Diagnostic Tool, Safety / Architecture, Shared Capability

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **cpu pmu & hardware-counter diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [56] CPU Hardware Counter Profiling — `IDEA`
- [57] IPC / Instructions-per-Cycle Analysis — `IDEA`
- [58] Cache Miss Analysis — `IDEA`
- [59] Branch Misprediction Analysis — `IDEA`
- [120] PMU Capability Enumeration — `CANDIDATE`
- [121] Instructions Retired Measurement — `CANDIDATE`
- [122] CPU Cycle Measurement — `CANDIDATE`
- [123] IPC Analysis — `CANDIDATE`
- [124] PMU Sampling Profiling — `IDEA`
- [125] PMU + Context Switch Correlation — `IDEA`
- [126] Cache Performance Counter Analysis — `IDEA`
- [127] Hardware Counter Portability Guard — `CANDIDATE`
- [128] CPU Performance Source Selection — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CPU-010 — CPU firmware, microcode & security context

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Recommendation

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para detectar e contextualizar **cpu firmware, microcode & security context**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor compreensão da postura de segurança sem desativar proteções por ganho marginal.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [71] CPU Firmware / Microcode Context Detection — `RESEARCHING`
- [72] CPU Firmware Update Recommendation — `IDEA`
- [73] CPU Security Mitigation Context — `IDEA`
- [74] Hypervisor Presence Detection — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CPU-011 — CPU optimization myth & unsafe-tweak guards

Status: READY_FOR_SPEC

Type: Safety, Safety / Product Behavior, Safety / Research

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **cpu optimization myth & unsafe-tweak guards**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Evita alterações populares, agressivas ou cosméticas que não apresentem benefício técnico proporcional.

Aplicabilidade:
quando uma configuração ou recomendação puder ser confundida com otimização universal.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [76] CPU “Use All Cores” Myth Guard — `CANDIDATE`
- [77] Scheduler Registry Tweak Guard — `CANDIDATE`
- [79] Priority Tweak Myth Guard — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CPU-012 — CPU diagnostic summary & evidence gate

Status: DUPLICATE

Type: Diagnostic / Product Behavior

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para correlacionar e contextualizar **cpu diagnostic summary & evidence gate**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Ajudar a distinguir coincidência de causa provável e explicar mudanças ao longo do tempo, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando histórico, comparação ou múltiplas evidências forem necessárias para interpretar um problema.

Dúvidas:
Ownership consolidado em C-SYSTEM-002 / C-SYSTEM-003. Esta entrada não deve gerar Feature Spec independente.

Duplicate of:
C-SYSTEM-002 / C-SYSTEM-003

Origem / itens consolidados:
- [80] CPU Diagnostic Summary — `CANDIDATE`

Decisão de revisão:
Não manter como capability ativa independente. O comportamento é coberto por C-SYSTEM-002 / C-SYSTEM-003; esta entrada permanece apenas para registrar a decisão de ownership.

## C-CPU-013 — CPU vendor-specific capability abstraction

Status: READY_FOR_SPEC

Type: Diagnostic, Shared Capability

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **cpu vendor-specific capability abstraction**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [81] CPU Vendor-Specific Capability Layer — `CANDIDATE`
- [101] Intel Dynamic Tuning Context — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CPU-014 — CPU stability & hardware-error validation

Status: READY_FOR_SPEC

Type: Benchmark / Diagnostic, Diagnostic, Diagnostic / Validation, Safety / Validation, Validation

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar **cpu stability & hardware-error validation**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [111] CPU Stability Test — `CANDIDATE`
- [112] WHEA Monitoring During CPU Test — `CANDIDATE`
- [113] Corrected Hardware Error Detection — `CANDIDATE`
- [114] CPU WHEA Source Classification — `RESEARCHING`
- [115] Stability Regression Detection — `IDEA`
- [136] CPU Optimization Rejection on Instability — `CANDIDATE`
- [137] CPU Optimization Rejection on Excessive Thermal Cost — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CPU-015 — CPU monitoring modes & overhead safeguards

Status: READY_FOR_SPEC

Type: Advanced Diagnostic, Diagnostic Infrastructure, Monitoring

Technical Domain: CPU

Primary Product Area: TBD

Possible Consumers:
- My PC
- Monitoring
- Optimization
- Benchmark
- Gaming

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **cpu monitoring modes & overhead safeguards**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Evita alterações populares, agressivas ou cosméticas que não apresentem benefício técnico proporcional.

Aplicabilidade:
quando uma configuração ou recomendação puder ser confundida com otimização universal.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [129] CPU Telemetry Sampling Cost Analysis — `IDEA`
- [130] CPU Monitoring Low-Overhead Mode — `CANDIDATE`
- [131] CPU Deep Diagnostic Mode — `IDEA`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.
