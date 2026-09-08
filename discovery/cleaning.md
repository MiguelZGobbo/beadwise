# Final Discovery — CLEANING

> Canonical rules: `01-DISCOVERY.md`
> Scope boundary: `02-FEATURE-SPEC-TEMPLATE.md` is used only to keep implementation research out of Discovery.

## Final review result

- Raw discovery items represented: **36**
- Consolidated capability groups: **10**
- Active non-duplicate groups: **9**
- `READY_FOR_SPEC`: **9**
- `CANDIDATE`: **0**
- `QUESTIONABLE`: **0**
- `DUPLICATE`: **1**

> A status decision is conceptual only. `READY_FOR_SPEC` does not imply implementation, API, Registry, rollback, benchmark proof, test or compatibility method already resolved.

## C-CLEANING-001 — Cleanup opportunity assessment

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Recommendation, Recommendation

Technical Domain: CLEANING

Primary Product Area: Cleaning

Possible Consumers:
- Cleaning
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **cleanup opportunity assessment**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Recuperação de espaço ou redução de acúmulo desnecessário com proteção de dados do usuário.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [1] Storage Pressure & Recoverable Space Assessment — `CANDIDATE`
- [2] Cleanup Inventory & Category Classification — `CANDIDATE`
- [33] Cleanup Opportunity Ranking — `CANDIDATE`
- [34] Critical Free-Space Requirement Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CLEANING-002 — Safe temporary & rebuildable data cleanup

Status: READY_FOR_SPEC

Type: Configuration / Optimization, Diagnostic, Maintenance / Optimization, Optimization, Optimization / Repair-adjacent

Technical Domain: CLEANING

Primary Product Area: Cleaning

Possible Consumers:
- Cleaning
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para avaliar **safe temporary & rebuildable data cleanup**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Recuperação de espaço ou redução de acúmulo desnecessário com proteção de dados do usuário.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [3] Safe Temporary File Cleanup — `CANDIDATE`
- [4] Recycle Bin Space Management — `CANDIDATE`
- [10] Previous Windows Installation Cleanup — `CANDIDATE`
- [11] Windows Update Temporary Data Cleanup — `CANDIDATE`
- [12] Delivery Optimization Cache Cleanup — `CANDIDATE`
- [13] Windows Component Store Cleanup — `CANDIDATE`
- [14] App-Owned Temporary Data Cleanup — `CANDIDATE`
- [15] Rebuildable Cache Classification — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CLEANING-003 — User file review & space recovery

Status: READY_FOR_SPEC

Type: Configuration / Optimization, Diagnostic / Tool, Recommendation / Tool

Technical Domain: CLEANING

Primary Product Area: Cleaning

Possible Consumers:
- Cleaning
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **user file review & space recovery**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Recuperação de espaço ou redução de acúmulo desnecessário com proteção de dados do usuário.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [5] Downloads Folder Review — `CANDIDATE`
- [6] Large & Unused File Discovery — `CANDIDATE`
- [7] Duplicate File Discovery — `CANDIDATE`
- [9] Cloud Content Dehydration / Free-Up-Space — `CANDIDATE`
- [19] User Data Archive / Move Recommendation — `CANDIDATE`
- [32] Installer / Archive Redundancy Correlation — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CLEANING-004 — Application & installation residue review

Status: READY_FOR_SPEC

Type: Diagnostic / Configuration, Diagnostic / Tool

Technical Domain: CLEANING

Primary Product Area: Cleaning

Possible Consumers:
- Cleaning
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para avaliar **application & installation residue review**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [8] Unused Application Discovery & Uninstall Guidance — `CANDIDATE`
- [18] Installation Residue Review — `CANDIDATE`
- [31] Orphaned User Profile Assessment — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CLEANING-005 — Logs, dumps & diagnostic artifact retention

Status: READY_FOR_SPEC

Type: Diagnostic / Configuration

Technical Domain: CLEANING

Primary Product Area: Cleaning

Possible Consumers:
- Cleaning
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para diagnosticar **logs, dumps & diagnostic artifact retention**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [16] Crash Dump & Diagnostic Artifact Retention — `CANDIDATE`
- [17] Log Retention & Cleanup Assessment — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CLEANING-006 — Cleanup safety, provenance & exclusions

Status: READY_FOR_SPEC

Type: Diagnostic / Safety, Safety / Configuration, Safety / Diagnostic

Technical Domain: CLEANING

Primary Product Area: Cleaning

Possible Consumers:
- Cleaning
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para avaliar **cleanup safety, provenance & exclusions**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Recuperação de espaço ou redução de acúmulo desnecessário com proteção de dados do usuário.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [20] Active-Use & File-Lock Safeguard — `CANDIDATE`
- [21] Cleanup Ownership & Provenance Validation — `CANDIDATE`
- [23] Cleanup Exclusions & Protected Locations — `CANDIDATE`
- [29] Reparse Point / Hardlink / Sparse File Accounting — `CANDIDATE`
- [30] Per-User / Multi-Profile Cleanup Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CLEANING-007 — Cleanup preview, execution safety & verification

Status: READY_FOR_SPEC

Type: Measurement / Diagnostic, Safety / Reliability, Safety / Tool

Technical Domain: CLEANING

Primary Product Area: Cleaning

Possible Consumers:
- Cleaning
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **cleanup preview, execution safety & verification**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Recuperação de espaço ou redução de acúmulo desnecessário com proteção de dados do usuário.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [22] Cleanup Preview / Dry Run — `CANDIDATE`
- [27] Cleanup Result Verification & History — `CANDIDATE`
- [35] Cleanup Cancellation / Partial-Failure Safety — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CLEANING-008 — Cleanup policy & Storage Sense awareness

Status: READY_FOR_SPEC

Type: Configuration, Diagnostic / Configuration, Optimization / Recommendation

Technical Domain: CLEANING

Primary Product Area: Cleaning

Possible Consumers:
- Cleaning
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para detectar e contextualizar **cleanup policy & storage sense awareness**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Recuperação de espaço ou redução de acúmulo desnecessário com proteção de dados do usuário.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [24] Age / Context-Aware Cleanup Policy — `CANDIDATE`
- [25] Low-Disk Triggered Cleanup Recommendation — `CANDIDATE`
- [26] Storage Sense Configuration Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CLEANING-009 — Multi-drive cleanup awareness

Status: READY_FOR_SPEC

Type: Diagnostic / Tool

Technical Domain: CLEANING

Primary Product Area: Cleaning

Possible Consumers:
- Cleaning
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **multi-drive cleanup awareness**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Recuperação de espaço ou redução de acúmulo desnecessário com proteção de dados do usuário.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [28] Multi-Drive Cleanup Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-CLEANING-010 — Cleanup recommendation confidence

Status: DUPLICATE

Type: Recommendation / Diagnostic

Technical Domain: CLEANING

Primary Product Area: Cleaning

Possible Consumers:
- Cleaning
- Optimization

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **cleanup recommendation confidence**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Recuperação de espaço ou redução de acúmulo desnecessário com proteção de dados do usuário.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Ownership consolidado em C-SYSTEM-003. Esta entrada não deve gerar Feature Spec independente.

Duplicate of:
C-SYSTEM-003

Origem / itens consolidados:
- [36] Cleanup Recommendation Confidence — `CANDIDATE`

Decisão de revisão:
Não manter como capability ativa independente. O comportamento é coberto por C-SYSTEM-003; esta entrada permanece apenas para registrar a decisão de ownership.
