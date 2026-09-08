# BeadWise V2 — Discovery — Windows Update — First Pass

**Status da área:** INITIAL DISCOVERY — FIRST PASS

**Regra canônica:** `01-DISCOVERY.md`  
**Filosofia:** Detect → Understand → Recommend → Apply → Measure → Keep / Rollback  
**Filtro:** preservar UX, segurança, estabilidade e reversibilidade; evitar granularidade por comando, chave, sensor ou parâmetro.

# 1. Windows Update State Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Update

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- My PC
- Security

Final UI Placement: TBD

Descrição: Inventariar estado geral do mecanismo de update.

Possível objetivo: Saber se o sistema está atualizado, pausado, bloqueado ou falhando.

Possível benefício: Security, Stability, Diagnostic

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- last check
- pending updates
- pause state
- service health
- error state
- managed policy

---

# 2. Pending Update Classification

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Update

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Security

Final UI Placement: TBD

Descrição: Classificar updates pendentes por tipo e impacto sem tratar todos como iguais.

Possível objetivo: Priorizar segurança, servicing, drivers e feature updates com contexto.

Possível benefício: Security, Stability

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- quality/security/feature/driver update
- KB
- restart requirement
- size where available

---

# 3. Pending Restart Detection

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Servicing

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Repair

Final UI Placement: TBD

Descrição: Detectar quando reboot é necessário para concluir servicing.

Possível objetivo: Evitar troubleshooting ou novas alterações sobre estado incompleto.

Possível benefício: Stability, Repair

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- reboot pending
- uptime
- update installation state
- servicing indicators

---

# 4. Update Failure Diagnosis

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Windows Update

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Repair

Final UI Placement: TBD

Descrição: Correlacionar códigos de erro, servicing health, rede, espaço e reboot.

Possível objetivo: Tratar causa real de update failure.

Possível benefício: Repair, Stability

Aplicabilidade: Systems with update failures

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- error code
- update KB
- DISM/SFC health
- network
- disk space
- policy
- reboot

---

# 5. Windows Update Troubleshooter Orchestration

Status: CANDIDATE

Type: Repair

Technical Domain: Windows Update / Get Help

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Repair

Final UI Placement: TBD

Descrição: Usar troubleshooter oficial quando apropriado e registrar findings.

Possível objetivo: Resolver falhas comuns com fluxo suportado.

Possível benefício: Repair

Aplicabilidade: Update issues

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- troubleshooter result
- actions suggested
- post-check

---

# 6. Servicing Stack Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Servicing

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Repair

Final UI Placement: TBD

Descrição: Reconhecer dependência do servicing stack e component store.

Possível objetivo: Evitar tratar update como mero download de pacote.

Possível benefício: Stability, Repair

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- servicing stack state
- component store health
- build applicability

---

# 7. Update Eligibility & Applicability Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Update

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- My PC

Final UI Placement: TBD

Descrição: Distinguir update indisponível, incompatível, phased rollout, safeguard hold ou não aplicável.

Possível objetivo: Evitar dizer “update quebrado” quando há bloqueio intencional.

Possível benefício: Diagnostic, Compatibility

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- OS edition/build
- hardware eligibility
- safeguard hold indicators where available
- policy/channel

---

# 8. Update History & Failure Timeline

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Update / History

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Repair

Final UI Placement: TBD

Descrição: Consolidar instalações bem-sucedidas, falhas, uninstall e reboot timeline.

Possível objetivo: Encontrar padrões de regressão.

Possível benefício: Diagnostic, Stability

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- KB history
- status
- date
- retries
- rollback/uninstall history

---

# 9. Update Deferral / Pause Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Update Policy

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Settings

Final UI Placement: TBD

Descrição: Mostrar pausa/deferral configurados e origem da policy.

Possível objetivo: Evitar alterar calendário do usuário ou organização sem consentimento.

Possível benefício: Quality of Life, Security

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- pause until
- deferral policy
- managed state
- active hours

---

# 10. Active Hours & Restart UX

Status: CANDIDATE

Type: Configuration

Technical Domain: Windows Update / UX

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Settings

Final UI Placement: TBD

Descrição: Avaliar active hours e comportamento de restart para reduzir interrupções.

Possível objetivo: Preservar UX enquanto mantém updates.

Possível benefício: Quality of Life, Stability

Aplicabilidade: Windows PCs

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- active hours
- automatic adjustment
- restart notifications
- pending reboot urgency

---

# 11. Metered Connection Update Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Update / Network

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Network

Final UI Placement: TBD

Descrição: Considerar conexão limitada e políticas de download.

Possível objetivo: Explicar por que update pode estar adiado.

Possível benefício: Quality of Life, Network

Aplicabilidade: Metered connections

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- metered state
- download policy
- current network

---

# 12. Delivery Optimization Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Update / Network

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Network
- Cleaning

Final UI Placement: TBD

Descrição: Mostrar Delivery Optimization como mecanismo de distribuição, não como serviço inútil.

Possível objetivo: Diagnosticar download e bandwidth sem “internet tweaks”.

Possível benefício: Network, Quality of Life

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- DO state
- download source behavior
- bandwidth settings/policy
- cache state

---

# 13. Update Storage Requirement Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Update / Storage

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Cleaning
- Storage

Final UI Placement: TBD

Descrição: Relacionar update com espaço necessário e limpeza suportada.

Possível objetivo: Evitar falha por low disk e limpar só o necessário.

Possível benefício: Storage, Repair

Aplicabilidade: Low-space update scenarios

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- free space
- required space estimate
- temporary/recoverable categories
- external storage option

---

# 14. Feature Update Readiness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Update / Compatibility

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- My PC

Final UI Placement: TBD

Descrição: Avaliar readiness para upgrade de versão maior sem forçar bypass de requisitos.

Possível objetivo: Separar incompatibilidade real de mera indisponibilidade.

Possível benefício: Compatibility, Security

Aplicabilidade: Feature upgrade scenarios

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- TPM/Secure Boot/CPU/RAM/storage prerequisites
- edition/build
- known block

---

# 15. Driver Updates via Windows Update Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Update / Drivers

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Drivers

Final UI Placement: TBD

Descrição: Distinguir driver updates distribuídos pelo WU de update genérico.

Possível objetivo: Evitar conflito com estratégia da área Drivers.

Possível benefício: Compatibility

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- optional driver updates
- current driver
- OEM/vendor context
- rollback

---

# 16. Update Uninstall / Rollback Availability

Status: CANDIDATE

Type: Diagnostic / Recovery

Technical Domain: Windows Update / Recovery

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Recovery
- Repair

Final UI Placement: TBD

Descrição: Detectar quando update pode ser desinstalado/rollback após regressão.

Possível objetivo: Restaurar sistema sem apagar update aleatoriamente.

Possível benefício: Recovery, Repair

Aplicabilidade: Post-update regressions

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- KB uninstallability
- feature-update rollback window
- recovery state
- BitLocker considerations

---

# 17. Update Before/After Health Verification

Status: CANDIDATE

Type: Measurement

Technical Domain: Windows Update / Shared

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Repair

Final UI Placement: TBD

Descrição: Verificar health após update/restart.

Possível objetivo: Detectar regressões ou falhas persistentes.

Possível benefício: Stability, Diagnostic

Aplicabilidade: Update sessions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- update state
- reboot completed
- device errors
- service health
- new crashes

---

# 18. Windows Lifecycle / Support Status

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Lifecycle

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Security

Final UI Placement: TBD

Descrição: Detectar se versão ainda recebe suporte normal.

Possível objetivo: Diferenciar PC “atualizado” de OS fora de suporte.

Possível benefício: Security, Diagnostic

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- edition
- version
- support end date
- ESU context where applicable

---

# Itens rejeitados / filtrados

- Desativar Windows Update permanentemente — REJECTED.
- Desativar serviços WU/BITS/DoSvc como “otimização” — REJECTED.
- Bloquear feature updates por Registry pack universal — REJECTED.
- Apagar SoftwareDistribution como primeira ação — REJECTED.
- Instalar todo driver opcional automaticamente — REJECTED.
- Bypass genérico de hardware requirements — REJECTED.
- “Debloat” de servicing components — REJECTED.

# Principais conclusões

Windows Update é um sistema de **servicing**, não um simples downloader.
Pending reboot, component store, espaço, rede, policies e lifecycle precisam ser contextualizados.
A UX deve reduzir interrupção, mas nunca comprometer segurança por promessa de performance.

# Fontes técnicas relevantes

- Microsoft Learn — Guidance for troubleshooting Windows Update issues
- Microsoft Support — Windows Update Troubleshooter
- Microsoft Learn / Support — Windows Update policies, active hours, Delivery Optimization

# Status final

**Windows Update — First Pass: INITIAL DISCOVERY COMPLETE**

Capabilities CANDIDATE: **18**

# Próximo passo

**Windows Update — Second Pass / Consolidated Gap Audit**

# BeadWise V2 — Discovery — Windows Update — Second Pass / Consolidated Gap Audit

**Regra canônica:** `01-DISCOVERY.md`  
**Filosofia:** Detect → Understand → Recommend → Apply → Measure → Keep / Rollback  
**Filtro:** preservar UX, segurança, estabilidade e reversibilidade; evitar granularidade por comando, chave, sensor ou parâmetro.

# 19. Update Source / Catalog Fallback Awareness

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Windows Update

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Repair

Final UI Placement: TBD

Descrição: Reconhecer quando instalação manual oficial via Microsoft Update Catalog é fallback apropriado.

Possível objetivo: Resolver update específico sem baixar pacote de fonte não confiável.

Possível benefício: Repair, Security

Aplicabilidade: Specific update failures

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- KB applicability
- architecture
- build
- catalog availability
- servicing prerequisites

---

# 20. Safeguard Hold Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Update / Compatibility

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Drivers

Final UI Placement: TBD

Descrição: Tratar holds de compatibilidade como proteção temporária e não como erro a ser contornado.

Possível objetivo: Evitar upgrade que cause regressão conhecida.

Possível benefício: Stability, Compatibility

Aplicabilidade: Feature updates

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- hold presence where detectable
- affected hardware/app
- known issue
- resolution status

---

# 21. Managed Update Policy Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Update / Policy

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Settings

Final UI Placement: TBD

Descrição: Distinguir PC consumidor de dispositivo gerenciado por organização.

Possível objetivo: Evitar sobrescrever políticas de update.

Possível benefício: Compatibility, Security

Aplicabilidade: Managed devices

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- MDM/GPO state
- update channel
- WSUS/WUfB context
- policy source

---

# 22. Update Bandwidth / Download Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Update / Network

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Network

Final UI Placement: TBD

Descrição: Distinguir download lento por rede, Delivery Optimization, source, policy ou servidor.

Possível objetivo: Evitar tweak TCP como resposta automática.

Possível benefício: Diagnostic, Network

Aplicabilidade: Slow update downloads

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- throughput
- connection type
- DO state
- metered policy
- error/retry pattern

---

# 23. Update Regression Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Update / History

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Repair
- Drivers

Final UI Placement: TBD

Descrição: Correlacionar início de crash/device issue com update recente sem assumir causalidade.

Possível objetivo: Guiar rollback ou driver investigation com evidência.

Possível benefício: Diagnostic, Repair

Aplicabilidade: Post-update issues

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- timeline
- affected component
- known issue
- rollback availability
- alternative causes

---

# 24. Update Recommendation Confidence

Status: CANDIDATE

Type: Recommendation

Technical Domain: Windows Update / Decision Support

Primary Product Area: Windows Update

Possible Consumers:

- Windows Update
- Recommendations

Final UI Placement: TBD

Descrição: Distinguir update obrigatório/segurança, opcional, preview e feature rollout.

Possível objetivo: Evitar “instale tudo” sem contexto.

Possível benefício: Security, Quality of Life

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- classification
- urgency
- known issues
- restart impact
- confidence

---

# Itens avaliados e não promovidos

- Cada serviço do Windows Update: implementação.
- Cada registry policy: Feature Spec.
- Cada error code: knowledge base interna.
- “Reset Windows Update Components” como capability isolada: fica em Update Failure Diagnosis / Repair e deve ser último recurso contextual.
- Preview updates: classificação interna de Pending Update Classification.

# Status final

**Windows Update — INITIAL DISCOVERY COMPLETE**

First Pass: 18  
Second Pass: +6  
Total bruto: **24 capabilities**