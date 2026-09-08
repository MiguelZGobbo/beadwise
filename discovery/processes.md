# Final Discovery — PROCESSES

> Canonical rules: `01-DISCOVERY.md`
> Scope boundary: `02-FEATURE-SPEC-TEMPLATE.md` is used only to keep implementation research out of Discovery.

## Final review result

- Raw discovery items represented: **44**
- Consolidated capability groups: **9**
- Active non-duplicate groups: **8**
- `READY_FOR_SPEC`: **7**
- `CANDIDATE`: **0**
- `QUESTIONABLE`: **1**
- `DUPLICATE`: **1**

> A status decision is conceptual only. `READY_FOR_SPEC` does not imply implementation, API, Registry, rollback, benchmark proof, test or compatibility method already resolved.

## C-PROCESSES-001 — Process inventory, tree & app grouping

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: PROCESSES

Primary Product Area: TBD

Possible Consumers:
- Monitoring
- Diagnostics
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **process inventory, tree & app grouping**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [1] Process Inventory — `CANDIDATE`
- [2] Process Tree & Parent/Child Mapping — `CANDIDATE`
- [3] Application-to-Process Grouping — `CANDIDATE`
- [32] Process Command-Line & Launch Context Diagnostics — `CANDIDATE`
- [33] Process Session / User Isolation Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-PROCESSES-002 — Per-process resource usage & trend diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Monitoring

Technical Domain: PROCESSES

Primary Product Area: TBD

Possible Consumers:
- Monitoring
- Diagnostics
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **per-process resource usage & trend diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [4] Process CPU Usage Diagnostics — `CANDIDATE`
- [5] Process Memory Usage Diagnostics — `CANDIDATE`
- [6] Process Disk I/O Diagnostics — `CANDIDATE`
- [7] Process Network Usage Diagnostics — `CANDIDATE`
- [8] Process GPU Usage Diagnostics — `CANDIDATE`
- [9] Process Resource Trend Monitoring — `CANDIDATE`
- [23] Process CPU-Time vs Utilization Context — `CANDIDATE`
- [36] Process Power / Energy Attribution — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-PROCESSES-003 — Process anomaly & root-cause correlation

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Safeguard

Technical Domain: PROCESSES

Primary Product Area: TBD

Possible Consumers:
- Monitoring
- Diagnostics
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para correlacionar e contextualizar **process anomaly & root-cause correlation**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Ajudar a distinguir coincidência de causa provável e explicar mudanças ao longo do tempo, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando histórico, comparação ou múltiplas evidências forem necessárias para interpretar um problema.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [10] Foreground vs Background Context — `CANDIDATE`
- [28] Process Resource Anomaly Detection — `CANDIDATE`
- [37] Process Wake / Sleep Prevention Diagnostics — `CANDIDATE`
- [44] Process Known-Role Knowledge Boundary — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-PROCESSES-004 — Process responsiveness, crash & lifecycle diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Monitoring, Safeguard

Technical Domain: PROCESSES

Primary Product Area: TBD

Possible Consumers:
- Monitoring
- Diagnostics
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **process responsiveness, crash & lifecycle diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [11] Process Responsiveness / Hang Diagnostics — `CANDIDATE`
- [12] Process Crash History Correlation — `CANDIDATE`
- [13] Process Start / Exit Monitoring — `CANDIDATE`
- [30] Process Change / Action History — `CANDIDATE`
- [39] Process Leak Trend Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-PROCESSES-005 — Process scheduling, priority & policy diagnostics

Status: READY_FOR_SPEC

Type: Configuration / Safeguard, Diagnostic

Technical Domain: PROCESSES

Primary Product Area: TBD

Possible Consumers:
- Monitoring
- Diagnostics
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **process scheduling, priority & policy diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [14] Process Priority Class Diagnostics — `CANDIDATE`
- [15] Process Priority Adjustment Safeguard — `CANDIDATE`
- [16] Background Processing Mode Awareness — `CANDIDATE`
- [17] Efficiency / Eco Mode Awareness — `CANDIDATE`
- [18] Process Affinity / CPU Set State Diagnostics — `CANDIDATE`
- [19] Process I/O Priority Awareness — `CANDIDATE`
- [20] Process Memory Priority Awareness — `CANDIDATE`
- [21] Job Object Membership Awareness — `CANDIDATE`
- [35] Process Mitigation Policy Awareness — `CANDIDATE`
- [38] Process Working-Set Trim Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-PROCESSES-006 — Process handles, threads & contention diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: PROCESSES

Primary Product Area: TBD

Possible Consumers:
- Monitoring
- Diagnostics
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **process handles, threads & contention diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [22] Process Handle / Thread Count Diagnostics — `CANDIDATE`
- [40] Process Contention Correlation — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-PROCESSES-007 — Process action workflow & safeguards

Status: READY_FOR_SPEC

Type: Diagnostic / Configuration, Diagnostic / Safeguard, Repair / Tool, Safeguard

Technical Domain: PROCESSES

Primary Product Area: TBD

Possible Consumers:
- Monitoring
- Diagnostics
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **process action workflow & safeguards**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Evita alterações populares, agressivas ou cosméticas que não apresentem benefício técnico proporcional.

Aplicabilidade:
quando uma configuração ou recomendação puder ser confundida com otimização universal.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [24] Process Suspend / Resume Awareness — `CANDIDATE`
- [25] Safe Process Termination — `CANDIDATE`
- [26] Process Restart Workflow — `CANDIDATE`
- [27] Critical Process Safeguard — `CANDIDATE`
- [34] Protected / Restricted Process Awareness — `CANDIDATE`
- [42] Process Action Dry-Run / Impact Preview — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-PROCESSES-008 — Process history, outcome & recommendation layer

Status: DUPLICATE

Type: Diagnostic / Quality of Life, Diagnostic / Recommendation Infrastructure, Diagnostic / Validation

Technical Domain: PROCESSES

Primary Product Area: TBD

Possible Consumers:
- Monitoring
- Diagnostics
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para correlacionar e contextualizar **process history, outcome & recommendation layer**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Ajudar a distinguir coincidência de causa provável e explicar mudanças ao longo do tempo, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando histórico, comparação ou múltiplas evidências forem necessárias para interpretar um problema.

Dúvidas:
Ownership consolidado em C-SYSTEM-003 + history/outcome inside C-PROCESSES-002/C-PROCESSES-003. Esta entrada não deve gerar Feature Spec independente.

Duplicate of:
C-SYSTEM-003 + history/outcome inside C-PROCESSES-002/C-PROCESSES-003

Origem / itens consolidados:
- [29] Process Recommendation Explanation — `CANDIDATE`
- [31] Process Outcome Validation — `CANDIDATE`
- [43] Process Recommendation Confidence — `CANDIDATE`

Decisão de revisão:
Não manter como capability ativa independente. O comportamento é coberto por C-SYSTEM-003 + history/outcome inside C-PROCESSES-002/C-PROCESSES-003; esta entrada permanece apenas para registrar a decisão de ownership.

## C-PROCESSES-009 — Per-app process policy profiles

Status: QUESTIONABLE

Type: Configuration

Technical Domain: PROCESSES

Primary Product Area: TBD

Possible Consumers:
- Monitoring
- Diagnostics
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **per-app process policy profiles**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
O benefício real é suficiente para justificar uma capability independente? Quais limites impedem que se transforme em tweak pack, preferência ou tuning de risco desproporcional?

Origem / itens consolidados:
- [41] Per-App Process Policy Profile — `CANDIDATE`

Decisão de revisão:
Não avançar ainda. O grupo contém risco de tuning agressivo, benefício incerto ou comportamento que pode virar preferência/tweak pack; precisa de justificativa conceitual adicional antes de Feature Spec.
