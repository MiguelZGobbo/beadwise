# BeadWise V2 — Discovery — Security — First Pass

**Status da área:** INITIAL DISCOVERY — FIRST PASS

**Regra canônica:** `01-DISCOVERY.md`  
**Filosofia:** Detect → Understand → Recommend → Apply → Measure → Keep / Rollback  
**Filtro:** preservar UX, segurança, estabilidade e reversibilidade; evitar granularidade por comando, chave, sensor ou parâmetro.

# 1. Windows Security Baseline Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Security

Primary Product Area: Security

Possible Consumers:

- Security
- My PC
- Recommendations

Final UI Placement: TBD

Descrição: Inventariar estado das principais proteções sem assumir que todo desvio é falha.

Possível objetivo: Dar visão consolidada de postura de segurança.

Possível benefício: Security, Diagnostic

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- Defender/AV state
- firewall profiles
- UAC
- Secure Boot
- TPM
- core isolation
- SmartScreen
- update status

---

# 2. Antivirus / Defender Protection State

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Microsoft Defender / AV

Primary Product Area: Security

Possible Consumers:

- Security
- My PC

Final UI Placement: TBD

Descrição: Detectar provedor AV ativo e principais estados de proteção sem conflitar com terceiros.

Possível objetivo: Identificar ausência ou degradação de proteção.

Possível benefício: Security

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- registered AV providers
- real-time protection
- service health
- definitions/platform status
- third-party ownership

---

# 3. Firewall Profile & Protection State

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Firewall

Primary Product Area: Security

Possible Consumers:

- Security
- Network

Final UI Placement: TBD

Descrição: Avaliar proteção por perfil de rede e detectar firewall desabilitado ou regras amplas suspeitas.

Possível objetivo: Evitar exposição de rede sem vender firewall como performance cost.

Possível benefício: Security

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- Domain/Private/Public profile
- firewall enabled
- default inbound policy
- network profile

---

# 4. Secure Boot Capability & State

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Firmware / Boot Security

Primary Product Area: Security

Possible Consumers:

- Security
- My PC

Final UI Placement: TBD

Descrição: Distinguir hardware capaz de Secure Boot de estado efetivamente habilitado.

Possível objetivo: Identificar proteção de boot ausente ou incompatibilidade.

Possível benefício: Security, Diagnostic

Aplicabilidade: UEFI systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- UEFI mode
- Secure Boot capability/state
- unsupported legacy boot
- recent firmware changes

---

# 5. TPM Capability & Health

Status: CANDIDATE

Type: Diagnostic

Technical Domain: TPM / Platform Security

Primary Product Area: Security

Possible Consumers:

- Security
- My PC

Final UI Placement: TBD

Descrição: Identificar versão, readiness e estado do TPM sem limpar TPM como correção genérica.

Possível objetivo: Avaliar base de confiança usada por Windows Hello, BitLocker e outras proteções.

Possível benefício: Security, Diagnostic

Aplicabilidade: TPM-capable systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- TPM present/version
- ready state
- manufacturer
- attestation/health indicators where available

---

# 6. Core Isolation / Memory Integrity State

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: VBS / HVCI

Primary Product Area: Security

Possible Consumers:

- Security
- My PC

Final UI Placement: TBD

Descrição: Avaliar VBS/HVCI e incompatibilidades conhecidas sem desativá-los por FPS.

Possível objetivo: Detectar proteção desabilitada ou bloqueada por driver incompatível.

Possível benefício: Security, Compatibility

Aplicabilidade: Supported PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- VBS state
- memory integrity
- incompatible drivers
- hardware support

---

# 7. Kernel DMA Protection State

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Platform Security / DMA

Primary Product Area: Security

Possible Consumers:

- Security
- USB
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Detectar Kernel DMA Protection e limitações de plataforma.

Possível objetivo: Preservar defesa contra DMA em portas externas de alta velocidade.

Possível benefício: Security

Aplicabilidade: Supported hardware

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- protection state
- IOMMU support
- Thunderbolt/USB4 context

---

# 8. SmartScreen / Reputation Protection State

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Security

Primary Product Area: Security

Possible Consumers:

- Security
- Applications

Final UI Placement: TBD

Descrição: Avaliar proteções de reputação e download sem tratá-las como bloat.

Possível objetivo: Detectar redução voluntária/inadvertida de proteção.

Possível benefício: Security

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- app/browser reputation protection
- download checks
- policy-managed state

---

# 9. Smart App Control Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Application Control

Primary Product Area: Security

Possible Consumers:

- Security
- My PC

Final UI Placement: TBD

Descrição: Detectar disponibilidade/estado do Smart App Control e explicar limitações de ativação.

Possível objetivo: Dar visibilidade a proteção moderna sem forçar mudança.

Possível benefício: Security

Aplicabilidade: Supported Windows 11 systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- availability
- state
- evaluation mode
- OS requirements

---

# 10. UAC Protection State

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Security

Primary Product Area: Security

Possible Consumers:

- Security
- System

Final UI Placement: TBD

Descrição: Avaliar UAC e detectar configurações excessivamente permissivas sem promover UAC-off.

Possível objetivo: Preservar consentimento/elevation boundaries.

Possível benefício: Security

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- UAC enabled
- prompt behavior
- policy-managed state

---

# 11. Exploit Protection State

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Security / Mitigations

Primary Product Area: Security

Possible Consumers:

- Security
- Gaming

Final UI Placement: TBD

Descrição: Inventariar mitigations globais/per-app e detectar alterações relevantes.

Possível objetivo: Evitar desativar Exploit Protection como tweak de FPS.

Possível benefício: Security, Compatibility

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- system mitigations
- per-app overrides
- known compatibility context

---

# 12. BitLocker / Device Encryption State

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Storage Encryption

Primary Product Area: Security

Possible Consumers:

- Security
- Recovery
- My PC

Final UI Placement: TBD

Descrição: Detectar criptografia, proteção e recovery readiness sem recomendar desativação por performance.

Possível objetivo: Dar visibilidade a proteção de dados e riscos de recovery.

Possível benefício: Security, Recovery

Aplicabilidade: Supported systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- encryption state
- protection suspended
- recovery method availability
- volume scope

---

# 13. Windows Hello / Sign-in Protection Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Identity

Primary Product Area: Security

Possible Consumers:

- Security
- My PC

Final UI Placement: TBD

Descrição: Avaliar opções modernas de sign-in sem coletar credenciais.

Possível objetivo: Identificar proteção biométrica/PIN disponível e problemas de configuração.

Possível benefício: Security, Quality of Life

Aplicabilidade: Supported systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- Hello capability
- PIN/biometric availability
- policy state

---

# 14. LSA Protection / Credential Guard Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Credential Security

Primary Product Area: Security

Possible Consumers:

- Security
- My PC

Final UI Placement: TBD

Descrição: Detectar proteções de credenciais em sistemas compatíveis.

Possível objetivo: Dar visibilidade sem incentivar disable para compatibilidade/performance.

Possível benefício: Security

Aplicabilidade: Supported systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- LSA protection
- Credential Guard/VBS relation
- compatibility

---

# 15. Security Update Exposure Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Security / Windows Update

Primary Product Area: Security

Possible Consumers:

- Security
- Windows Update

Final UI Placement: TBD

Descrição: Identificar sistema sem patches relevantes ou fora de suporte.

Possível objetivo: Priorizar risco real em vez de tweaks cosméticos.

Possível benefício: Security, Stability

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- OS support status
- pending security updates
- update failures
- reboot pending

---

# 16. Security Protection Tamper / Policy Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Security / Policy

Primary Product Area: Security

Possible Consumers:

- Security
- System

Final UI Placement: TBD

Descrição: Distinguir proteção alterada pelo usuário, organização, terceiro ou possível tamper.

Possível objetivo: Evitar mudanças que brigam com políticas ou AV de terceiros.

Possível benefício: Security, Diagnostic

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- managed state
- security provider ownership
- tamper protection state where available
- policy source

---

# 17. Potentially Unsafe Security Tweaks Detection

Status: CANDIDATE

Type: Diagnostic / Recommendation

Technical Domain: Security / Configuration

Primary Product Area: Security

Possible Consumers:

- Security
- Optimization

Final UI Placement: TBD

Descrição: Detectar tweaks conhecidos que reduzem proteções em nome de performance.

Possível objetivo: Reverter filosofia de optimizer tradicional que sacrifica segurança.

Possível benefício: Security, Stability

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- disabled Defender/firewall/UAC
- signature enforcement changes
- HVCI disabled due tweak
- SmartScreen disabled
- Exploit Protection overrides

---

# 18. Security Recommendation Explanation

Status: CANDIDATE

Type: Recommendation

Technical Domain: Shared / Security

Primary Product Area: Security

Possible Consumers:

- Security
- Recommendations

Final UI Placement: TBD

Descrição: Explicar impacto, compatibilidade e urgência de cada achado.

Possível objetivo: Evitar score alarmista e recomendações sem contexto.

Possível benefício: Security, Quality of Life

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- risk level
- why it matters
- managed state
- user impact
- recommended action

---

# Itens rejeitados / filtrados

- Desativar Defender por FPS — REJECTED.
- Desativar Firewall por latência — REJECTED.
- Desativar UAC — REJECTED.
- Desativar Memory Integrity/Core Isolation genericamente — REJECTED.
- Desativar Kernel DMA Protection — REJECTED.
- Desativar Exploit Protection — REJECTED.
- Desativar Secure Boot/TPM — REJECTED.
- Desabilitar driver signature enforcement — REJECTED.
- Tratar suite de segurança de terceiro como bloat automaticamente — REJECTED.

# Principais conclusões

Security deve ser uma área de **postura, compatibilidade e diagnóstico**, não de “performance tuning”.
O produto deve respeitar provedores de segurança de terceiros e políticas organizacionais.
Qualquer sugestão de mudança precisa explicar claramente o trade-off.

# Fontes técnicas relevantes

- Microsoft Support — Windows Security
- Microsoft Learn — Core isolation / VBS / HVCI
- Microsoft Learn — Secure Boot / TPM / Kernel DMA Protection
- Microsoft Support — SmartScreen and reputation-based protection

# Status final

**Security — First Pass: INITIAL DISCOVERY COMPLETE**

Capabilities CANDIDATE: **18**

# Próximo passo

**Security — Second Pass / Consolidated Gap Audit**

# BeadWise V2 — Discovery — Security — Second Pass / Consolidated Gap Audit

**Regra canônica:** `01-DISCOVERY.md`  
**Filosofia:** Detect → Understand → Recommend → Apply → Measure → Keep / Rollback  
**Filtro:** preservar UX, segurança, estabilidade e reversibilidade; evitar granularidade por comando, chave, sensor ou parâmetro.

# 19. Security Provider Conflict Detection

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Security Providers

Primary Product Area: Security

Possible Consumers:

- Security
- Drivers

Final UI Placement: TBD

Descrição: Detectar múltiplos providers/filters potencialmente conflitantes sem classificá-los automaticamente como problema.

Possível objetivo: Investigar conflitos de AV/EDR/firewall que geram falhas reais.

Possível benefício: Security, Stability

Aplicabilidade: Systems with multiple security products

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- registered providers
- filter drivers
- service state
- recent installation
- symptom correlation

---

# 20. Protection Feature Compatibility Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Security / Compatibility

Primary Product Area: Security

Possible Consumers:

- Security
- Gaming
- Drivers

Final UI Placement: TBD

Descrição: Correlacionar falhas reais com proteções modernas antes de sugerir qualquer exceção.

Possível objetivo: Evitar disable global quando uma incompatibilidade é específica.

Possível benefício: Security, Compatibility

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- feature state
- affected app/driver
- known issue
- per-app alternative
- update path

---

# 21. Security Recovery Readiness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Security / Recovery

Primary Product Area: Security

Possible Consumers:

- Security
- Recovery

Final UI Placement: TBD

Descrição: Verificar disponibilidade de recovery keys e caminhos de recuperação para proteções que podem bloquear acesso após mudança de hardware/firmware.

Possível objetivo: Reduzir risco operacional.

Possível benefício: Security, Recovery

Aplicabilidade: Encrypted/secured systems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- BitLocker recovery readiness
- account/key backup state where safely detectable
- TPM/Secure Boot changes

---

# 22. Unsupported OS Security Risk

Status: CANDIDATE

Type: Diagnostic / Recommendation

Technical Domain: Windows Lifecycle

Primary Product Area: Security

Possible Consumers:

- Security
- Windows Update

Final UI Placement: TBD

Descrição: Detectar versão fora de suporte e separar isso de mero atraso em patches.

Possível objetivo: Apontar risco estrutural que tweaking não corrige.

Possível benefício: Security, Stability

Aplicabilidade: Unsupported Windows versions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- edition/version
- support lifecycle
- ESU context where applicable

---

# 23. Security Baseline Drift History

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Security / History

Primary Product Area: Security

Possible Consumers:

- Security
- Recommendations

Final UI Placement: TBD

Descrição: Registrar mudanças relevantes de proteções ao longo do tempo.

Possível objetivo: Detectar regressões após tweak, app, driver ou policy.

Possível benefício: Security, Diagnostic

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- previous state
- current state
- change timestamp
- likely source

---

# 24. Security Recommendation Confidence

Status: CANDIDATE

Type: Recommendation

Technical Domain: Security / Decision Support

Primary Product Area: Security

Possible Consumers:

- Security
- Recommendations

Final UI Placement: TBD

Descrição: Separar finding estabelecido de compatibilidade contextual ou preferência.

Possível objetivo: Evitar alertas absolutos em cenários legítimos.

Possível benefício: Security, Quality of Life

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- evidence
- managed state
- compatibility
- severity
- confidence

---

# Itens avaliados e não promovidos

- Cada firewall rule: detalhe de diagnóstico.
- Cada Defender preference: granularidade excessiva.
- Cada exploit mitigation: detalhe de Feature Spec.
- Cada TPM property: dado interno de TPM Capability & Health.
- “Security score” único: QUESTIONABLE; pode induzir falsa precisão.

# Status final

**Security — INITIAL DISCOVERY COMPLETE**

First Pass: 18  
Second Pass: +6  
Total bruto: **24 capabilities**