# Final Discovery — PRIVACY

> Canonical rules: `01-DISCOVERY.md`
> Scope boundary: `02-FEATURE-SPEC-TEMPLATE.md` is used only to keep implementation research out of Discovery.

## Final review result

- Raw discovery items represented: **20**
- Consolidated capability groups: **6**
- Active non-duplicate groups: **6**
- `READY_FOR_SPEC`: **6**
- `CANDIDATE`: **0**
- `QUESTIONABLE`: **0**
- `DUPLICATE`: **0**

> A status decision is conceptual only. `READY_FOR_SPEC` does not imply implementation, API, Registry, rollback, benchmark proof, test or compatibility method already resolved.

## C-PRIVACY-001 — Windows privacy baseline & managed-state awareness

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: PRIVACY

Primary Product Area: Privacy

Possible Consumers:
- Privacy
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **windows privacy baseline & managed-state awareness**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Mais transparência e controle sobre coleta, permissões e personalização; deve ser apresentado como privacidade/configuração, nunca como promessa de performance.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais limitações e estados gerenciados precisam ser considerados na Feature Spec? Como manter a apresentação como escolha de privacidade, sem chamar preferência de otimização?

Origem / itens consolidados:
- [1] Windows Privacy Baseline Inventory — `CANDIDATE`
- [14] Privacy Policy / Managed State Awareness — `CANDIDATE`
- [18] Per-User Privacy Scope Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-PRIVACY-002 — Diagnostics, personalization & advertising controls

Status: READY_FOR_SPEC

Type: Configuration, Configuration / Diagnostic, Diagnostic / Configuration

Technical Domain: PRIVACY

Primary Product Area: Privacy

Possible Consumers:
- Privacy
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **diagnostics, personalization & advertising controls**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Mais transparência e controle sobre coleta, permissões e personalização; deve ser apresentado como privacidade/configuração, nunca como promessa de performance.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
Quais limitações e estados gerenciados precisam ser considerados na Feature Spec? Como manter a apresentação como escolha de privacidade, sem chamar preferência de otimização?

Origem / itens consolidados:
- [2] Diagnostic Data & Feedback Controls — `CANDIDATE`
- [3] Tailored Experiences / Personalized Recommendations — `CANDIDATE`
- [4] Advertising ID & Windows Recommendations — `CANDIDATE`
- [11] Inking & Typing Personalization Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-PRIVACY-003 — Location & sensitive-device permissions

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: PRIVACY

Primary Product Area: Privacy

Possible Consumers:
- Privacy
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar **location & sensitive-device permissions**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Mais transparência e controle sobre coleta, permissões e personalização; deve ser apresentado como privacidade/configuração, nunca como promessa de performance.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais limitações e estados gerenciados precisam ser considerados na Feature Spec? Como manter a apresentação como escolha de privacidade, sem chamar preferência de otimização?

Origem / itens consolidados:
- [5] Location Privacy & App Access — `CANDIDATE`
- [6] Camera Permission Inventory — `CANDIDATE`
- [7] Microphone Permission Inventory — `CANDIDATE`
- [8] App Permission Inventory — `CANDIDATE`
- [16] Recent Sensitive Resource Access Review — `CANDIDATE`
- [17] Desktop App vs Packaged App Permission Distinction — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-PRIVACY-004 — Activity, speech & search/cloud privacy

Status: READY_FOR_SPEC

Type: Diagnostic / Configuration

Technical Domain: PRIVACY

Primary Product Area: Privacy

Possible Consumers:
- Privacy
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar **activity, speech & search/cloud privacy**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Mais transparência e controle sobre coleta, permissões e personalização; deve ser apresentado como privacidade/configuração, nunca como promessa de performance.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais limitações e estados gerenciados precisam ser considerados na Feature Spec? Como manter a apresentação como escolha de privacidade, sem chamar preferência de otimização?

Origem / itens consolidados:
- [9] Activity History Controls — `CANDIDATE`
- [10] Speech / Online Speech Recognition Privacy — `CANDIDATE`
- [12] Search / Cloud Content Privacy Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-PRIVACY-005 — Find My Device privacy trade-off

Status: READY_FOR_SPEC

Type: Diagnostic / Configuration

Technical Domain: PRIVACY

Primary Product Area: Privacy

Possible Consumers:
- Privacy
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **find my device privacy trade-off**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Mais transparência e controle sobre coleta, permissões e personalização; deve ser apresentado como privacidade/configuração, nunca como promessa de performance.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais limitações e estados gerenciados precisam ser considerados na Feature Spec? Como manter a apresentação como escolha de privacidade, sem chamar preferência de otimização?

Origem / itens consolidados:
- [13] Find My Device Privacy Trade-off — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-PRIVACY-006 — Privacy change history & drift awareness

Status: READY_FOR_SPEC

Type: Diagnostic / Safety, Recommendation

Technical Domain: PRIVACY

Primary Product Area: Privacy

Possible Consumers:
- Privacy
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **privacy change history & drift awareness**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Mais transparência e controle sobre coleta, permissões e personalização; deve ser apresentado como privacidade/configuração, nunca como promessa de performance.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais limitações e estados gerenciados precisam ser considerados na Feature Spec? Como manter a apresentação como escolha de privacidade, sem chamar preferência de otimização?

Origem / itens consolidados:
- [15] Privacy Recommendation Explanation — `CANDIDATE`
- [19] Privacy Change History — `CANDIDATE`
- [20] Privacy Recommendation Confidence — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.
