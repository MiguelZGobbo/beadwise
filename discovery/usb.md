# Final Discovery — USB

> Canonical rules: `01-DISCOVERY.md`
> Scope boundary: `02-FEATURE-SPEC-TEMPLATE.md` is used only to keep implementation research out of Discovery.

## Final review result

- Raw discovery items represented: **55**
- Consolidated capability groups: **15**
- Active non-duplicate groups: **14**
- `READY_FOR_SPEC`: **14**
- `CANDIDATE`: **0**
- `QUESTIONABLE`: **0**
- `DUPLICATE`: **1**

> A status decision is conceptual only. `READY_FOR_SPEC` does not imply implementation, API, Registry, rollback, benchmark proof, test or compatibility method already resolved.

## C-USB-001 — USB controller, topology & device inventory

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **usb controller, topology & device inventory**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [1] USB Controller Inventory — `CANDIDATE`
- [2] USB Topology Mapping — `CANDIDATE`
- [3] USB Device Identification — `CANDIDATE`
- [39] Composite USB Device Diagnostics — `CANDIDATE`
- [40] Multiple Identical Peripheral Identity Diagnostics — `CANDIDATE`
- [41] Serial / COM Peripheral Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-002 — USB speed, generation & bandwidth diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **usb speed, generation & bandwidth diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [4] USB Connection Speed & Generation Validation — `CANDIDATE`
- [20] USB Hub Capability & Load Diagnostics — `CANDIDATE`
- [21] USB Bandwidth Contention Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-003 — USB device health, enumeration & disconnect diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Monitoring, Diagnostic / Repair

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **usb device health, enumeration & disconnect diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [5] USB Device Health & Problem Code Diagnostics — `CANDIDATE`
- [6] USB Enumeration Failure Diagnostics — `CANDIDATE`
- [7] USB Disconnect / Reconnect Monitoring — `CANDIDATE`
- [8] USB Port / Hub Fault Correlation — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-004 — USB power, suspend, sleep & wake diagnostics

Status: READY_FOR_SPEC

Type: Configuration / Repair, Diagnostic, Diagnostic / Configuration

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar **usb power, suspend, sleep & wake diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [9] USB Selective Suspend Diagnostics — `CANDIDATE`
- [10] Device-Specific USB Power Exception — `CANDIDATE`
- [11] USB Sleep / Resume Diagnostics — `CANDIDATE`
- [12] USB Wake Capability Diagnostics — `CANDIDATE`
- [22] USB Power Delivery / Power Availability Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-005 — HID, mouse, keyboard, controller & input diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Benchmark

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar e, quando justificado, permitir configurar **hid, mouse, keyboard, controller & input diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer controle contextual sem tratar preferência como ganho garantido, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
somente quando o estado atual, o objetivo do usuário e o risco tornarem a mudança justificável.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [13] HID Device Inventory — `CANDIDATE`
- [14] Mouse & Keyboard Input Health Diagnostics — `CANDIDATE`
- [15] HID Report Rate / Polling Diagnostics — `CANDIDATE`
- [16] Input Latency Context Diagnostics — `CANDIDATE`
- [17] Controller / Gamepad Diagnostics — `CANDIDATE`
- [50] Virtual Controller & Input Remapping Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-006 — Peripheral driver, firmware & software-conflict diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Manual Guidance

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar **peripheral driver, firmware & software-conflict diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [18] Peripheral Driver & Software Dependency Diagnostics — `CANDIDATE`
- [19] Peripheral Firmware Awareness — `CANDIDATE`
- [32] USB Driver Regression Correlation — `CANDIDATE`
- [51] Peripheral Software Conflict Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-007 — USB-C, USB4, Thunderbolt & docking diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Repair Guidance

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **usb-c, usb4, thunderbolt & docking diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [23] USB-C Capability Diagnostics — `CANDIDATE`
- [24] USB-C / Alternate Mode Failure Diagnostics — `CANDIDATE`
- [25] USB4 Capability & Topology Diagnostics — `CANDIDATE`
- [47] Thunderbolt / PCIe Tunneling Awareness — `CANDIDATE`
- [49] Docking Station Composite Context Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-008 — External storage & surprise-removal awareness

Status: READY_FOR_SPEC

Type: Diagnostic / Safeguard

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **external storage & surprise-removal awareness**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [26] External Storage Safe Removal & Surprise Removal Awareness — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-009 — USB audio, webcam & capture-device awareness

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para detectar e contextualizar **usb audio, webcam & capture-device awareness**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Dar ao produto uma visão confiável do estado e das capacidades relevantes, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Contexto confiável para diagnósticos e recomendações posteriores, reduzindo suposições sobre o sistema.

Aplicabilidade:
quando outras áreas precisarem conhecer o ambiente antes de diagnosticar, recomendar ou agir.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [27] USB Audio Peripheral Awareness — `CANDIDATE`
- [28] Webcam / Capture Device USB Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-010 — Bluetooth adapter, device & coexistence diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Monitoring

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para diagnosticar **bluetooth adapter, device & coexistence diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [29] Bluetooth Adapter USB Awareness — `CANDIDATE`
- [42] Bluetooth Radio & Capability Inventory — `CANDIDATE`
- [43] Bluetooth Device Inventory & Profile Awareness — `CANDIDATE`
- [44] Bluetooth Connection Stability Diagnostics — `CANDIDATE`
- [45] Bluetooth / Wi-Fi Coexistence Diagnostics — `CANDIDATE`
- [46] Bluetooth Battery & Low-Power Context — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-011 — Ghost, duplicate & resource-conflict diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar **ghost, duplicate & resource-conflict diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Transformar sintomas e métricas em uma explicação útil e acionável, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando houver sintomas, degradação, anomalias ou necessidade de entender o estado atual.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [30] Peripheral Duplicate / Ghost Device Diagnostics — `CANDIDATE`
- [31] Peripheral Conflict & Resource Diagnostics — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-012 — Peripheral restart, repair & outcome validation

Status: READY_FOR_SPEC

Type: Diagnostic / Repair Validation, Repair, Repair / Safeguard

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para diagnosticar e conduzir a recuperação de **peripheral restart, repair & outcome validation**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Resolver falhas reais com o menor grau de intervenção necessário, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Maior chance de resolver uma falha real de forma guiada e verificável.

Aplicabilidade:
quando existir um sintoma ou falha compatível com o escopo da capability.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [33] Targeted Peripheral Restart / Re-enumeration — `CANDIDATE`
- [34] Peripheral Repair Escalation — `CANDIDATE`
- [55] Peripheral Repair Outcome Validation — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-013 — USB/peripheral history, trace & root-cause diagnostics

Status: READY_FOR_SPEC

Type: Diagnostic, Diagnostic / Recommendation Infrastructure, Diagnostic / Tool

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Medium

Descrição:
Capability de produto para correlacionar e contextualizar **usb/peripheral history, trace & root-cause diagnostics**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Ajudar a distinguir coincidência de causa provável e explicar mudanças ao longo do tempo, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando histórico, comparação ou múltiplas evidências forem necessárias para interpretar um problema.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [35] USB / Peripheral Change History — `CANDIDATE`
- [38] USB ETW Trace Diagnostics — `CANDIDATE`
- [54] Peripheral Diagnostic Confidence & Root-Cause Layering — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.

## C-USB-014 — USB/peripheral change backup & recommendation layer

Status: DUPLICATE

Type: Diagnostic / Quality of Life, Safeguard / Repair

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: Low

Descrição:
Capability de produto para avaliar **usb/peripheral change backup & recommendation layer**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Oferecer uma capacidade de produto coerente e contextual, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor entendimento do estado atual e decisões mais contextualizadas, evitando mudanças universais sem evidência.

Aplicabilidade:
quando o cenário descrito pela capability estiver presente.

Dúvidas:
Ownership consolidado em C-SYSTEM-003 + change safety inside C-USB-012. Esta entrada não deve gerar Feature Spec independente.

Duplicate of:
C-SYSTEM-003 + change safety inside C-USB-012

Origem / itens consolidados:
- [36] USB / Peripheral Recommendation Explanation — `CANDIDATE`
- [37] USB / Peripheral Change Backup & Rollback — `CANDIDATE`

Decisão de revisão:
Não manter como capability ativa independente. O comportamento é coberto por C-SYSTEM-003 + change safety inside C-USB-012; esta entrada permanece apenas para registrar a decisão de ownership.

## C-USB-015 — External peripheral security safeguards

Status: READY_FOR_SPEC

Type: Diagnostic / Safeguard, Safeguard, Safeguard / Diagnostic

Technical Domain: USB

Primary Product Area: TBD

Possible Consumers:
- My PC
- Diagnostics
- Gaming
- Repair

Final UI Placement: TBD

Shared Capability: No

Risco inicial: High

Descrição:
Capability de produto para identificar e bloquear recomendações inadequadas relacionadas a **external peripheral security safeguards**. Os itens de origem abaixo representam sinais, subcomportamentos ou variações do mesmo objetivo e não devem virar features independentes apenas por terem mecanismos técnicos distintos.

Possível objetivo:
Evitar tweaks sem evidência, riscos desnecessários e perda de qualidade de uso, mantendo a decisão no nível de produto e deixando método exato de detecção/aplicação para a Feature Spec.

Possível benefício:
Melhor compreensão da postura de segurança sem desativar proteções por ganho marginal.

Aplicabilidade:
quando uma configuração ou recomendação puder ser confundida com otimização universal.

Dúvidas:
Quais fontes técnicas, limitações de compatibilidade e condições de aplicabilidade precisam ser confirmadas na Feature Spec?

Origem / itens consolidados:
- [48] Kernel DMA Protection & External Peripheral Safeguard — `CANDIDATE`
- [52] Accessibility Peripheral Safeguards — `CANDIDATE`
- [53] Critical Input Device Repair Safeguard — `CANDIDATE`

Decisão de revisão:
A responsabilidade de produto está clara e não há motivo conceitual óbvio para descarte. As dúvidas restantes são técnicas e pertencem à Feature Spec.
