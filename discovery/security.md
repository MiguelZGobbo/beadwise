# Final Discovery — SECURITY

> Canonical rules: `01-DISCOVERY.md`
> Scope boundary: `02-FEATURE-SPEC-TEMPLATE.md` is used only to keep implementation research out of Discovery.

## Final review result

- Raw discovery items represented: **24**
- Consolidated capability groups: **10**
- Active non-duplicate groups: **9**
- `READY_FOR_SPEC`: **9**
- `CANDIDATE`: **0**
- `QUESTIONABLE`: **0**
- `DUPLICATE`: **1**

> A status decision is conceptual only. `READY_FOR_SPEC` does not imply implementation, API, Registry, rollback, benchmark proof, test or compatibility method already resolved.

## C-SECURITY-001 — Windows security baseline

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: SECURITY

Primary Product Area: Security

Possible Consumers:
- Security
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **windows security baseline**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior visibilidade sobre a postura de segurança e conflitos relevantes, sem reduzir proteções do Windows em troca de ganhos marginais.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais estados e compatibilidades serão confirmados tecnicamente na Feature Spec? Como garantir que nenhuma recomendação reduza proteção apenas por desempenho marginal?

Origem / itens consolidados:
- [1] Windows Security Baseline Inventory — `CANDIDATE`
- [23] Security Baseline Drift History — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-SECURITY-002 — Endpoint protection & firewall state

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: SECURITY

Primary Product Area: Security

Possible Consumers:
- Security
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar **endpoint protection & firewall state**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior visibilidade sobre a postura de segurança e conflitos relevantes, sem reduzir proteções do Windows em troca de ganhos marginais.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais estados e compatibilidades serão confirmados tecnicamente na Feature Spec? Como garantir que nenhuma recomendação reduza proteção apenas por desempenho marginal?

Origem / itens consolidados:
- [2] Antivirus / Defender Protection State — `CANDIDATE`
- [3] Firewall Profile & Protection State — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-SECURITY-003 — Platform boot & hardware-backed security

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: SECURITY

Primary Product Area: Security

Possible Consumers:
- Security
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **platform boot & hardware-backed security**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior visibilidade sobre a postura de segurança e conflitos relevantes, sem reduzir proteções do Windows em troca de ganhos marginais.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais estados e compatibilidades serão confirmados tecnicamente na Feature Spec? Como garantir que nenhuma recomendação reduza proteção apenas por desempenho marginal?

Origem / itens consolidados:
- [4] Secure Boot Capability & State — `CANDIDATE`
- [5] TPM Capability & Health — `CANDIDATE`
- [7] Kernel DMA Protection State — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-SECURITY-004 — Virtualization-based & credential protections

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: SECURITY

Primary Product Area: Security

Possible Consumers:
- Security
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar **virtualization-based & credential protections**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior visibilidade sobre a postura de segurança e conflitos relevantes, sem reduzir proteções do Windows em troca de ganhos marginais.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais estados e compatibilidades serão confirmados tecnicamente na Feature Spec? Como garantir que nenhuma recomendação reduza proteção apenas por desempenho marginal?

Origem / itens consolidados:
- [6] Core Isolation / Memory Integrity State — `CANDIDATE`
- [14] LSA Protection / Credential Guard Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-SECURITY-005 — Reputation, application & exploit protection

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: SECURITY

Primary Product Area: Security

Possible Consumers:
- Security
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar **reputation, application & exploit protection**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior visibilidade sobre a postura de segurança e conflitos relevantes, sem reduzir proteções do Windows em troca de ganhos marginais.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais estados e compatibilidades serão confirmados tecnicamente na Feature Spec? Como garantir que nenhuma recomendação reduza proteção apenas por desempenho marginal?

Origem / itens consolidados:
- [8] SmartScreen / Reputation Protection State — `CANDIDATE`
- [9] Smart App Control Awareness — `CANDIDATE`
- [11] Exploit Protection State — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-SECURITY-006 — UAC, encryption & sign-in protection

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: SECURITY

Primary Product Area: Security

Possible Consumers:
- Security
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar **uac, encryption & sign-in protection**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior visibilidade sobre a postura de segurança e conflitos relevantes, sem reduzir proteções do Windows em troca de ganhos marginais.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais estados e compatibilidades serão confirmados tecnicamente na Feature Spec? Como garantir que nenhuma recomendação reduza proteção apenas por desempenho marginal?

Origem / itens consolidados:
- [10] UAC Protection State — `CANDIDATE`
- [12] BitLocker / Device Encryption State — `CANDIDATE`
- [13] Windows Hello / Sign-in Protection Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-SECURITY-007 — Security update & support exposure

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Recommendation

Technical Domain: SECURITY

Primary Product Area: Security

Possible Consumers:
- Security
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **security update & support exposure**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior visibilidade sobre a postura de segurança e conflitos relevantes, sem reduzir proteções do Windows em troca de ganhos marginais.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais estados e compatibilidades serão confirmados tecnicamente na Feature Spec? Como garantir que nenhuma recomendação reduza proteção apenas por desempenho marginal?

Origem / itens consolidados:
- [15] Security Update Exposure Assessment — `CANDIDATE`
- [22] Unsupported OS Security Risk — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-SECURITY-008 — Security policy, tamper & unsafe-tweak diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Recommendation

Technical Domain: SECURITY

Primary Product Area: Security

Possible Consumers:
- Security
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **security policy, tamper & unsafe-tweak diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior visibilidade sobre a postura de segurança e conflitos relevantes, sem reduzir proteções do Windows em troca de ganhos marginais.

Aplicabilidade:
quando uma configuração ou recomendação puder ser confundida com otimização universal.

Dúvidas:
Quais estados e compatibilidades serão confirmados tecnicamente na Feature Spec? Como garantir que nenhuma recomendação reduza proteção apenas por desempenho marginal?

Origem / itens consolidados:
- [16] Security Protection Tamper / Policy Awareness — `CANDIDATE`
- [17] Potentially Unsafe Security Tweaks Detection — `CANDIDATE`
- [19] Security Provider Conflict Detection — `CANDIDATE`
- [20] Protection Feature Compatibility Assessment — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-SECURITY-009 — Security recommendation layer

Status: DUPLICATE

Type: Recommendation

Technical Domain: SECURITY

Primary Product Area: Security

Possible Consumers:
- Security
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **security recommendation layer**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior visibilidade sobre a postura de segurança e conflitos relevantes, sem reduzir proteções do Windows em troca de ganhos marginais.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Ownership consolidado em C-SYSTEM-003. Esta entrada não deve gerar Feature Spec independente.

Duplicate of:
C-SYSTEM-003

Origem / itens consolidados:
- [18] Security Recommendation Explanation — `CANDIDATE`
- [24] Security Recommendation Confidence — `CANDIDATE`

Decisão de revisão:
Não manter como capability ativa independente. O comportamento é coberto por C-SYSTEM-003; esta entrada permanece apenas para registrar a decisão de ownership.

## C-SECURITY-010 — Security recovery readiness & drift history

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: SECURITY

Primary Product Area: Security

Possible Consumers:
- Security
- Diagnostics

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **security recovery readiness & drift history**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior visibilidade sobre a postura de segurança e conflitos relevantes, sem reduzir proteções do Windows em troca de ganhos marginais.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais estados e compatibilidades serão confirmados tecnicamente na Feature Spec? Como garantir que nenhuma recomendação reduza proteção apenas por desempenho marginal?

Origem / itens consolidados:
- [21] Security Recovery Readiness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.
