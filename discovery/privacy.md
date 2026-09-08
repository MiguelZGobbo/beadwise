# BeadWise V2 — Discovery — Privacy — First Pass

**Status da área:** INITIAL DISCOVERY — FIRST PASS

**Regra canônica:** `01-DISCOVERY.md`  
**Filosofia:** Detect → Understand → Recommend → Apply → Measure → Keep / Rollback  
**Filtro:** preservar UX, segurança, estabilidade e reversibilidade; evitar granularidade por comando, chave, sensor ou parâmetro.

# 1. Windows Privacy Baseline Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Privacy

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Settings
- Recommendations

Final UI Placement: TBD

Descrição: Inventariar configurações de privacidade relevantes por categoria sem presumir que “mais desligado = melhor PC”.

Possível objetivo: Dar visão compreensível do que está habilitado e por quê.

Possível benefício: Privacy, Diagnostic

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- diagnostic data
- advertising/personalization
- location
- activity history
- app permissions
- speech/inking where applicable

---

# 2. Diagnostic Data & Feedback Controls

Status: CANDIDATE

Type: Configuration / Diagnostic

Technical Domain: Windows Privacy

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Settings

Final UI Placement: TBD

Descrição: Mostrar nível/opções de dados diagnósticos suportados e recursos relacionados.

Possível objetivo: Permitir escolha informada sem vender ganho de performance.

Possível benefício: Privacy

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- required/optional diagnostic settings
- feedback frequency where exposed
- policy-managed state
- Diagnostic Data Viewer availability

---

# 3. Tailored Experiences / Personalized Recommendations

Status: CANDIDATE

Type: Configuration

Technical Domain: Windows Privacy

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Settings

Final UI Placement: TBD

Descrição: Controlar uso de diagnostic/activity signals para experiências personalizadas onde o Windows oferece opção.

Possível objetivo: Dar escolha de personalização, não otimização falsa.

Possível benefício: Privacy, Preference

Aplicabilidade: Supported Windows versions

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- setting state
- OS-version naming
- managed state
- functional effect

---

# 4. Advertising ID & Windows Recommendations

Status: CANDIDATE

Type: Configuration

Technical Domain: Windows Privacy

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Settings

Final UI Placement: TBD

Descrição: Expor advertising ID e recomendações/ofertas com descrição precisa.

Possível objetivo: Permitir preferência de publicidade/personalização.

Possível benefício: Privacy, Preference

Aplicabilidade: Windows PCs

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- advertising ID setting
- recommendations/offers settings
- per-user scope

---

# 5. Location Privacy & App Access

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Location / Privacy

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Settings

Final UI Placement: TBD

Descrição: Mostrar serviço de localização, app access e dependências funcionais.

Possível objetivo: Permitir controle sem quebrar timezone automático, Find my device ou apps sem aviso.

Possível benefício: Privacy, Quality of Life

Aplicabilidade: Location-capable PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- location service
- recent app access where available
- desktop app access
- dependent Windows features

---

# 6. Camera Permission Inventory

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Privacy / Devices

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Security

Final UI Placement: TBD

Descrição: Mostrar quais categorias/apps possuem acesso à câmera e estado global.

Possível objetivo: Aumentar controle sem desabilitar hardware globalmente.

Possível benefício: Privacy, Security

Aplicabilidade: Devices with cameras

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- device presence
- global access
- app permissions
- recent access where available

---

# 7. Microphone Permission Inventory

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Privacy / Audio

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Security
- Audio

Final UI Placement: TBD

Descrição: Mostrar acesso ao microfone por apps e desktop apps.

Possível objetivo: Permitir controle contextual sem quebrar comunicação/recording.

Possível benefício: Privacy, Security

Aplicabilidade: Devices with microphones

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- global access
- app permissions
- desktop app access
- recent use where available

---

# 8. App Permission Inventory

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows App Permissions

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Settings

Final UI Placement: TBD

Descrição: Consolidar permissões por capability do Windows em vez de criar feature por permissão.

Possível objetivo: Dar visão de acesso a recursos sensíveis.

Possível benefício: Privacy, Security

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- contacts
- calendar
- call history
- messaging
- radios
- documents/pictures/videos
- other supported permissions

---

# 9. Activity History Controls

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Privacy

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Settings

Final UI Placement: TBD

Descrição: Mostrar armazenamento local de activity history e comportamento dependente da versão.

Possível objetivo: Permitir escolha sem reproduzir orientação obsoleta de versões antigas.

Possível benefício: Privacy

Aplicabilidade: Supported Windows versions

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- local history state
- OS-version behavior
- clear history action
- deprecated cloud send behavior

---

# 10. Speech / Online Speech Recognition Privacy

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Speech / Privacy

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Accessibility
- Settings

Final UI Placement: TBD

Descrição: Explicar opções de speech recognition e dependências funcionais.

Possível objetivo: Preservar acessibilidade/voice UX ao oferecer escolha de privacidade.

Possível benefício: Privacy, Accessibility

Aplicabilidade: Systems using speech features

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- online speech state
- local alternatives
- feature dependency

---

# 11. Inking & Typing Personalization Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Input / Privacy

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Settings

Final UI Placement: TBD

Descrição: Expor personalization/dictionary options relacionadas a input quando aplicável.

Possível objetivo: Permitir preferência sem alegar ganho de performance.

Possível benefício: Privacy, Preference

Aplicabilidade: Supported systems

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- personalization state
- dictionary data
- clear/reset option

---

# 12. Search / Cloud Content Privacy Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Search / Privacy

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Search

Final UI Placement: TBD

Descrição: Separar busca local, cloud/account integration e history/preferences.

Possível objetivo: Dar controle de privacidade sem quebrar index/search local por associação.

Possível benefício: Privacy, Quality of Life

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: User Choice

Possíveis informações / dados / checks:

- cloud search integration
- search history/settings
- account context
- local indexing unaffected

---

# 13. Find My Device Privacy Trade-off

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Location / Recovery

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Recovery
- Security

Final UI Placement: TBD

Descrição: Mostrar dependência de localização/conta e valor de recuperação.

Possível objetivo: Evitar recomendar desligamento automático de recurso útil.

Possível benefício: Privacy, Recovery

Aplicabilidade: Supported devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- feature state
- location dependency
- account/device support

---

# 14. Privacy Policy / Managed State Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Privacy / Policy

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Settings

Final UI Placement: TBD

Descrição: Distinguir escolhas do usuário de políticas organizacionais ou edições Windows.

Possível objetivo: Evitar tentar sobrescrever settings gerenciados.

Possível benefício: Privacy, Compatibility

Aplicabilidade: Managed PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- policy source
- setting locked
- organization management indicators

---

# 15. Privacy Recommendation Explanation

Status: CANDIDATE

Type: Recommendation

Technical Domain: Shared / Privacy

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Recommendations

Final UI Placement: TBD

Descrição: Explicar dado/recurso afetado, efeito funcional e se há impacto de UX.

Possível objetivo: Evitar “privacy hardening pack” sem contexto.

Possível benefício: Privacy, Quality of Life

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- what data/access changes
- feature impact
- scope
- reversibility
- recommendation confidence

---

# Itens rejeitados / filtrados

- “Disable all telemetry for performance” — REJECTED.
- Desabilitar serviços Windows de telemetria indiscriminadamente — REJECTED.
- Remover componentes do Windows para privacidade — REJECTED.
- Bloquear endpoints em hosts/firewall packs sem semântica — REJECTED.
- Desativar localização globalmente sem explicar dependências — REJECTED.
- Desativar câmera/microfone no Device Manager para privacidade — REJECTED como padrão.
- Tratar recomendações/ofertas como consumo relevante de recursos — REJECTED como argumento de performance.

# Principais conclusões

Privacy é uma área de **consentimento, transparência e preferência**, não uma otimização de desempenho.
O Windows muda nomenclatura e comportamento entre versões; capability detection é obrigatória.
Recursos de acessibilidade, localização, busca e recovery precisam de aviso de impacto.

# Fontes técnicas relevantes

- Microsoft Support — Diagnostics, feedback, and privacy in Windows
- Microsoft Support — General privacy settings / Recommendations & offers
- Microsoft Support — Windows activity history and your privacy
- Microsoft Support — Windows location service and privacy

# Status final

**Privacy — First Pass: INITIAL DISCOVERY COMPLETE**

Capabilities CANDIDATE: **15**

# Próximo passo

**Privacy — Second Pass / Consolidated Gap Audit**

# BeadWise V2 — Discovery — Privacy — Second Pass / Consolidated Gap Audit

**Regra canônica:** `01-DISCOVERY.md`  
**Filosofia:** Detect → Understand → Recommend → Apply → Measure → Keep / Rollback  
**Filtro:** preservar UX, segurança, estabilidade e reversibilidade; evitar granularidade por comando, chave, sensor ou parâmetro.

# 16. Recent Sensitive Resource Access Review

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Privacy / Devices

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Security

Final UI Placement: TBD

Descrição: Consolidar sinais de uso recente de localização/câmera/microfone quando o Windows expõe esses dados.

Possível objetivo: Ajudar usuário a revisar acesso real, não apenas permissões teóricas.

Possível benefício: Privacy, Security

Aplicabilidade: Supported Windows versions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- resource
- app/process identity
- last access where exposed
- permission state

---

# 17. Desktop App vs Packaged App Permission Distinction

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Privacy Model

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Settings

Final UI Placement: TBD

Descrição: Explicar diferenças de controle entre apps empacotados e desktop apps.

Possível objetivo: Evitar prometer granularidade que o Windows não oferece.

Possível benefício: Privacy, Diagnostic

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- app model
- permission surface
- global desktop-app control
- limitations

---

# 18. Per-User Privacy Scope Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Privacy / Profiles

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Settings

Final UI Placement: TBD

Descrição: Distinguir settings por usuário de settings por dispositivo/policy.

Possível objetivo: Evitar mudança em escopo errado.

Possível benefício: Privacy, Compatibility

Aplicabilidade: Multi-user PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- scope
- current user
- device-wide setting
- policy

---

# 19. Privacy Change History

Status: CANDIDATE

Type: Diagnostic / Safety

Technical Domain: Privacy / History

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Settings

Final UI Placement: TBD

Descrição: Registrar mudanças feitas pelo BeadWise e possibilitar restauração de preferências.

Possível objetivo: Dar reversibilidade e confiança.

Possível benefício: Privacy, Quality of Life

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- previous value
- new value
- timestamp
- reason
- rollback

---

# 20. Privacy Recommendation Confidence

Status: CANDIDATE

Type: Recommendation

Technical Domain: Privacy / Decision Support

Primary Product Area: Privacy

Possible Consumers:

- Privacy
- Recommendations

Final UI Placement: TBD

Descrição: Marcar quando uma sugestão é preferência, privacy hardening ou necessidade funcional.

Possível objetivo: Evitar tom alarmista.

Possível benefício: Privacy, Quality of Life

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- evidence
- functional trade-off
- scope
- confidence

---

# Itens avaliados e não promovidos

- Cada permissão (Contacts, Calendar etc.): fica dentro de App Permission Inventory.
- Clipboard history: pode ser explorado em Personalization/Privacy futura Feature Spec; não justificou capability isolada aqui.
- Browser privacy: fora do núcleo Windows e melhor por app/plugin específico.
- DNS/privacy/VPN: Network/Security.
- Recall-specific controls: dependem fortemente de hardware/versão e devem entrar como subcapability/version-aware privacy se suportados, não como regra universal.

# Status final

**Privacy — INITIAL DISCOVERY COMPLETE**

First Pass: 15  
Second Pass: +5  
Total bruto: **20 capabilities**