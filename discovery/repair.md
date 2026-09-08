# Final Discovery — REPAIR

> Canonical rules: `01-DISCOVERY.md`
> Scope boundary: `02-FEATURE-SPEC-TEMPLATE.md` is used only to keep implementation research out of Discovery.

## Final review result

- Raw discovery items represented: **29**
- Consolidated capability groups: **10**
- Active non-duplicate groups: **10**
- `READY_FOR_SPEC`: **10**
- `CANDIDATE`: **0**
- `QUESTIONABLE`: **0**
- `DUPLICATE`: **0**

> A status decision is conceptual only. `READY_FOR_SPEC` does not imply implementation, API, Registry, rollback, benchmark proof, test or compatibility method already resolved.

## C-REPAIR-001 — System repair triage & orchestration

Status: READY_FOR_SPEC

Type: Diagnostic, Repair / Diagnostic, Tool / Diagnostic

Technical Domain: REPAIR

Primary Product Area: Repair

Possible Consumers:
- Repair
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **system repair triage & orchestration**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior chance de resolver uma falha real de forma guiada e verificável.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [1] System Repair Triage — `CANDIDATE`
- [8] Built-in Troubleshooter Orchestration — `CANDIDATE`
- [29] Repair Session Bundling — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-REPAIR-002 — System file & component-store repair

Status: READY_FOR_SPEC

Type: Diagnostic / Repair

Technical Domain: REPAIR

Primary Product Area: Repair

Possible Consumers:
- Repair
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **system file & component-store repair**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior chance de resolver uma falha real de forma guiada e verificável.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [2] System File Integrity Assessment — `CANDIDATE`
- [3] Component Store Health & Repair — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-REPAIR-003 — Boot, startup & recovery-environment repair

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Repair, Diagnostic / Tool, Repair

Technical Domain: REPAIR

Primary Product Area: Repair

Possible Consumers:
- Repair
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **boot, startup & recovery-environment repair**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior chance de resolver uma falha real de forma guiada e verificável.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [4] Boot Failure Triage — `CANDIDATE`
- [5] Startup Repair Orchestration — `CANDIDATE`
- [6] Windows Recovery Environment Health — `CANDIDATE`
- [22] Safe Mode / Clean-Boot Diagnostic Path — `CANDIDATE`
- [26] Offline Repair Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-REPAIR-004 — Pending operation & restart resolution

Status: READY_FOR_SPEC

Type: Diagnostic / Repair

Technical Domain: REPAIR

Primary Product Area: Repair

Possible Consumers:
- Repair
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **pending operation & restart resolution**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [7] Restart / Pending Operation Resolution — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-REPAIR-005 — Application & Microsoft Store repair

Status: READY_FOR_SPEC

Type: Repair, Repair / Configuration

Technical Domain: REPAIR

Primary Product Area: Repair

Possible Consumers:
- Repair
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **application & microsoft store repair**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior chance de resolver uma falha real de forma guiada e verificável.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [9] Application Repair / Reset Awareness — `CANDIDATE`
- [10] Microsoft Store / Packaged App Repair — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-REPAIR-006 — Subsystem repair paths

Status: READY_FOR_SPEC

Type: Diagnostic / Repair

Technical Domain: REPAIR

Primary Product Area: Repair

Possible Consumers:
- Repair
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **subsystem repair paths**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior chance de resolver uma falha real de forma guiada e verificável.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [11] Network Stack Repair Triage — `CANDIDATE`
- [12] Audio Device Repair Triage — `CANDIDATE`
- [15] File System Error Repair Triage — `CANDIDATE`
- [16] Time & Clock Repair Triage — `CANDIDATE`
- [17] Search / Index Repair Triage — `CANDIDATE`
- [18] Printing Repair Triage — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-REPAIR-007 — Device & driver repair path

Status: READY_FOR_SPEC

Type: Repair

Technical Domain: REPAIR

Primary Product Area: Repair

Possible Consumers:
- Repair
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **device & driver repair path**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior chance de resolver uma falha real de forma guiada e verificável.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [13] Device Re-enumeration / Restart Guidance — `CANDIDATE`
- [14] Driver Rollback / Reinstall Repair Path — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-REPAIR-008 — Repair verification, history & known-good correlation

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Safety, Measurement

Technical Domain: REPAIR

Primary Product Area: Repair

Possible Consumers:
- Repair
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **repair verification, history & known-good correlation**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior chance de resolver uma falha real de forma guiada e verificável.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [19] Repair Before/After Verification — `CANDIDATE`
- [20] Repair History & Audit Trail — `CANDIDATE`
- [23] Known-Good State Correlation — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-REPAIR-009 — Repair escalation, source availability & safeguards

Status: READY_FOR_SPEC

Type: Diagnostic, Safety, Safety / Recommendation

Technical Domain: REPAIR

Primary Product Area: Repair

Possible Consumers:
- Repair
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **repair escalation, source availability & safeguards**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior chance de resolver uma falha real de forma guiada e verificável.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [21] Repair Escalation & Stop Conditions — `CANDIDATE`
- [24] Repair Source Availability Assessment — `CANDIDATE`
- [25] BitLocker-Aware Repair Safeguard — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-REPAIR-010 — Repair confidence & rollback readiness

Status: READY_FOR_SPEC

Type: Recommendation / Diagnostic, Safety / Diagnostic

Technical Domain: REPAIR

Primary Product Area: Repair

Possible Consumers:
- Repair
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **repair confidence & rollback readiness**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior chance de resolver uma falha real de forma guiada e verificável.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [27] Repair Confidence & Evidence Summary — `CANDIDATE`
- [28] Repair Rollback Readiness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.
