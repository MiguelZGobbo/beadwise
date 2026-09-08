# Final Discovery — DISPLAY

> Canonical rules: `01-DISCOVERY.md`
> Scope boundary: `02-FEATURE-SPEC-TEMPLATE.md` is used only to keep implementation research out of Discovery.

## Final review result

- Raw discovery items represented: **58**
- Consolidated capability groups: **13**
- Active non-duplicate groups: **12**
- `READY_FOR_SPEC`: **12**
- `CANDIDATE`: **0**
- `QUESTIONABLE`: **0**
- `DUPLICATE`: **1**

> A status decision is conceptual only. `READY_FOR_SPEC` does not imply implementation, API, Registry, rollback, benchmark proof, test or compatibility method already resolved.

## C-DISPLAY-001 — Display inventory, EDID & path mapping

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **display inventory, edid & path mapping**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [1] Display Inventory — `CANDIDATE`
- [2] EDID & Display Capability Inventory — `CANDIDATE`
- [3] Active Display Path Mapping — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-DISPLAY-002 — Resolution, refresh rate & mode validation

Status: READY_FOR_SPEC

Type: Configuration / Diagnostic, Diagnostic, Diagnostic / Benchmark, Diagnostic / Configuration, Diagnostic / Recommendation

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **resolution, refresh rate & mode validation**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [4] Native Resolution Validation — `CANDIDATE`
- [5] Refresh Rate Capability & Active Rate Validation — `CANDIDATE`
- [6] Dynamic Refresh Rate Awareness — `CANDIDATE`
- [7] Display Mode Compatibility Diagnostics — `CANDIDATE`
- [12] Variable Refresh Rate Capability Diagnostics — `CANDIDATE`
- [15] Mixed Refresh Rate Multi-Monitor Awareness — `CANDIDATE`
- [17] Resolution vs Scaling Recommendation Logic — `CANDIDATE`
- [18] Display Orientation & Layout Validation — `CANDIDATE`
- [56] Refresh Rate Measurement Validation — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-DISPLAY-003 — HDR & advanced color diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Configuration, Tool / Manual Guidance

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar **hdr & advanced color diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [8] HDR Capability Detection — `CANDIDATE`
- [9] HDR Configuration Diagnostics — `CANDIDATE`
- [10] HDR Calibration Guidance — `CANDIDATE`
- [11] SDR Content Brightness in HDR Diagnostics — `CANDIDATE`
- [40] Advanced Color Capability & State Diagnostics — `CANDIDATE`
- [41] Auto Color Management Awareness — `CANDIDATE`
- [42] ICC Profile Compatibility Under Advanced Color — `CANDIDATE`
- [43] HDR / Wide-Gamut Multi-Monitor Consistency Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-DISPLAY-004 — VRR, tearing & motion diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic / Configuration, Tool / Diagnostic

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **vrr, tearing & motion diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [13] VRR Configuration & Validation — `CANDIDATE`
- [31] Ghosting / Motion Clarity Test — `CANDIDATE`
- [32] Tearing Visual Validation — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-DISPLAY-005 — Multi-monitor topology & compatibility

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **multi-monitor topology & compatibility**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [14] Multi-Monitor Topology Diagnostics — `CANDIDATE`
- [46] Clone Mode Capability Limitation Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-DISPLAY-006 — Scaling, DPI & color profile diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Configuration, Diagnostic / Manual Guidance, Tool / Manual Guidance

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar **scaling, dpi & color profile diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [16] Display Scaling & DPI Diagnostics — `CANDIDATE`
- [19] Color Depth & Output Capability Diagnostics — `CANDIDATE`
- [20] Color Profile / ICC Awareness — `CANDIDATE`
- [21] Windows Color Calibration Guidance — `CANDIDATE`
- [22] Gamma / Brightness / Contrast Diagnostic Guidance — `CANDIDATE`
- [45] Chroma Subsampling & Pixel Format Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-DISPLAY-007 — Display link, connection & hotplug diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Monitoring

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **display link, connection & hotplug diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [23] Display Connection Type Diagnostics — `CANDIDATE`
- [24] Display Link Bottleneck Diagnostics — `CANDIDATE`
- [25] Display Signal Loss / Reconnect Monitoring — `CANDIDATE`
- [26] Display Driver / GPU Reset Correlation — `CANDIDATE`
- [36] Wireless Display Awareness — `CANDIDATE`
- [44] Display Stream Compression Awareness — `CANDIDATE`
- [51] Dock / USB-C / USB4 Display Hotplug Correlation — `CANDIDATE`
- [55] Protected Content / HDCP Context Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-DISPLAY-008 — Panel visual test tools

Status: READY_FOR_SPEC

Type: Tool, Tool / Diagnostic

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **panel visual test tools**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [27] Dead / Stuck Pixel Diagnostic Test — `CANDIDATE`
- [28] Stuck Pixel Exercise Tool — `CANDIDATE`
- [29] Backlight Bleed / Uniformity Test — `CANDIDATE`
- [30] Banding / Gradient Test — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-DISPLAY-009 — Display power & brightness behavior

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Configuration, Diagnostic / Safeguard

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar **display power & brightness behavior**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [33] Display Power / Sleep Behavior Diagnostics — `CANDIDATE`
- [34] Internal Display Brightness Capability Awareness — `CANDIDATE`
- [35] Content-Adaptive Brightness Awareness — `CANDIDATE`
- [54] OLED Display Safeguards — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-DISPLAY-010 — Display recommendation layer

Status: DUPLICATE

Type: Diagnostic / Quality of Life, Diagnostic / Recommendation Infrastructure

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **display recommendation layer**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

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
- [37] Display Recommendation Explanation — `CANDIDATE`
- [58] Display Recommendation Confidence — `CANDIDATE`

Decisão de revisão:
Não manter como capability ativa independente. O comportamento é coberto por C-SYSTEM-003; esta entrada permanece apenas para registrar a decisão de ownership.

## C-DISPLAY-011 — Display change safety, rollback & validation

Status: READY_FOR_SPEC

Type: Diagnostic / Validation, Safeguard, Safeguard / Repair

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para avaliar **display change safety, rollback & validation**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [38] Display Configuration Backup & Rollback — `CANDIDATE`
- [39] Display Change Outcome Validation — `CANDIDATE`
- [57] Display Test Flashing Safety — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-DISPLAY-012 — External monitor control & firmware awareness

Status: READY_FOR_SPEC

Type: Configuration / Tool, Diagnostic, Diagnostic / Manual Guidance, Tool / Configuration

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para detectar e contextualizar **external monitor control & firmware awareness**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [47] External Monitor DDC/CI Capability Diagnostics — `CANDIDATE`
- [48] External Monitor Brightness Control — `CANDIDATE`
- [49] Monitor Input Source Control — `CANDIDATE`
- [50] Monitor Firmware Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-DISPLAY-013 — Virtual/remote display awareness

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: DISPLAY

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Configuration

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **virtual/remote display awareness**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [52] Remote Desktop / Virtual Display Awareness — `CANDIDATE`
- [53] Privacy Screen / Integrated Display Feature Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.
