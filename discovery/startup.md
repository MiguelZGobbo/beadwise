# Final Discovery — STARTUP

> Canonical rules: `01-DISCOVERY.md`
> Scope boundary: `02-FEATURE-SPEC-TEMPLATE.md` is used only to keep implementation research out of Discovery.

## Final review result

- Raw discovery items represented: **39**
- Consolidated capability groups: **9**
- Active non-duplicate groups: **8**
- `READY_FOR_SPEC`: **8**
- `CANDIDATE`: **0**
- `QUESTIONABLE`: **0**
- `DUPLICATE`: **1**

> A status decision is conceptual only. `READY_FOR_SPEC` does not imply implementation, API, Registry, rollback, benchmark proof, test or compatibility method already resolved.

## C-STARTUP-001 — Startup entry inventory & source coverage

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Safeguard

Technical Domain: STARTUP

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para detectar e contextualizar **startup entry inventory & source coverage**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [1] Startup Entry Inventory — `CANDIDATE`
- [2] Run / RunOnce Inventory — `CANDIDATE`
- [3] Startup Folder Inventory — `CANDIDATE`
- [29] Autoruns Extended Location Awareness — `CANDIDATE`
- [30] Explorer Shell Extension Startup Context — `CANDIDATE`
- [31] Winlogon Extension Safeguard — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STARTUP-002 — Startup impact, timing & responsiveness diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Benchmark, Diagnostic / Tool

Technical Domain: STARTUP

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **startup impact, timing & responsiveness diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [4] Startup App Impact Diagnostics — `CANDIDATE`
- [5] Boot / Logon Phase Timing — `CANDIDATE`
- [32] Boot Trace / WPA Escalation — `CANDIDATE`
- [33] Fast Startup vs Full Boot Benchmark Context — `CANDIDATE`
- [34] Post-Logon Responsiveness Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STARTUP-003 — Startup entry identity, role & intent context

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / UX

Technical Domain: STARTUP

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **startup entry identity, role & intent context**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [6] Startup Entry Publisher & Signature Context — `CANDIDATE`
- [7] Startup Functional Role Classification — `CANDIDATE`
- [8] Startup User-Intent Awareness — `CANDIDATE`
- [13] Launcher Auto-Start Diagnostics — `CANDIDATE`
- [14] Cloud Sync Startup Diagnostics — `CANDIDATE`
- [15] Hardware Helper Startup Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STARTUP-004 — Startup disable scoring & control

Status: READY_FOR_SPEC

Type: Configuration, Diagnostic / Recommendation

Technical Domain: STARTUP

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **startup disable scoring & control**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [9] Startup Disable Candidate Scoring — `CANDIDATE`
- [10] Startup Disable / Enable — `CANDIDATE`
- [21] Delayed Start Opportunity Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STARTUP-005 — Startup drift, regression & before/after validation

Status: READY_FOR_SPEC

Type: Benchmark, Diagnostic

Technical Domain: STARTUP

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para correlacionar e contextualizar **startup drift, regression & before/after validation**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Ajudar a distinguir coincidência de causa provável e explicar mudanças ao longo do tempo, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando histórico, comparação ou múltiplas evidências forem necessárias para interpretar um problema.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [11] Startup Before × After Validation — `CANDIDATE`
- [25] Startup Configuration Drift — `CANDIDATE`
- [38] Startup Regression Correlation — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STARTUP-006 — Broken/recreated startup entry diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Cleaning, Diagnostic / Repair

Technical Domain: STARTUP

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **broken/recreated startup entry diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [12] Startup Re-Creation Detection — `CANDIDATE`
- [24] Broken Startup Entry Diagnostics — `CANDIDATE`
- [35] Startup App Failure Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STARTUP-007 — Startup dependency & safety safeguards

Status: READY_FOR_SPEC

Type: Diagnostic, Safeguard

Technical Domain: STARTUP

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **startup dependency & safety safeguards**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Evita alterações populares, agressivas ou cosméticas que não apresentem benefício técnico proporcional.

Aplicabilidade:
quando uma configuração ou recomendação puder ser confundida com otimização universal.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [16] Security Software Startup Safeguard — `CANDIDATE`
- [17] Accessibility Startup Safeguard — `CANDIDATE`
- [18] Auto-Start Service Context — `CANDIDATE`
- [19] Trigger-Start Service Awareness — `CANDIDATE`
- [20] Scheduled Task Startup Context — `CANDIDATE`
- [22] Automatic Maintenance Awareness — `CANDIDATE`
- [23] Startup Entry Dependency Diagnostics — `CANDIDATE`
- [36] RunOnce Transient-State Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STARTUP-008 — Startup backup, rollback & optimization sessions

Status: READY_FOR_SPEC

Type: Optimization / Safeguard, Safeguard

Technical Domain: STARTUP

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **startup backup, rollback & optimization sessions**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [26] Startup Change Backup & Rollback — `CANDIDATE`
- [37] Startup Optimization Session Profile — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-STARTUP-009 — Startup recommendation layer

Status: DUPLICATE

Type: Diagnostic / Quality of Life, Diagnostic / Recommendation Infrastructure

Technical Domain: STARTUP

Primary Product Area: TBD

Possible Consumers:
- Optimization
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **startup recommendation layer**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

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
- [27] Startup Recommendation Explanation — `CANDIDATE`
- [28] Startup Recommendation Confidence — `CANDIDATE`
- [39] Startup Recommendation Confidence — `CANDIDATE`

Decisão de revisão:
Não manter como capability ativa independente. O comportamento é coberto por C-SYSTEM-003; esta entrada permanece apenas para registrar a decisão de ownership.
