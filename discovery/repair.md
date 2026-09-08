# BeadWise V2 — Discovery — Repair — First Pass

**Status da área:** INITIAL DISCOVERY — FIRST PASS

**Regra canônica:** `01-DISCOVERY.md`  
**Filosofia:** Detect → Understand → Recommend → Apply → Measure → Keep / Rollback  
**Filtro:** preservar UX, segurança, estabilidade e reversibilidade; evitar granularidade por comando, chave, sensor ou parâmetro.

# 1. System Repair Triage

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows / Repair

Primary Product Area: Repair

Possible Consumers:

- Repair
- System
- Recommendations

Final UI Placement: TBD

Descrição: Classificar sintomas antes de executar qualquer reparo, distinguindo corrupção do sistema, boot, update, rede, apps, storage e drivers.

Possível objetivo: Evitar sequência cega de comandos e direcionar o mecanismo correto.

Possível benefício: Repair, Diagnostic, Stability

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- sintoma principal
- último funcionamento conhecido
- mudanças recentes
- eventos/crashes relacionados
- reboot pendente
- capacidade de recuperação disponível

---

# 2. System File Integrity Assessment

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Windows Servicing

Primary Product Area: Repair

Possible Consumers:

- Repair
- System

Final UI Placement: TBD

Descrição: Avaliar integridade de arquivos protegidos do Windows e usar mecanismos suportados quando houver corrupção.

Possível objetivo: Detectar e reparar corrupção real, não executar SFC como ritual de otimização.

Possível benefício: Repair, Stability

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- resultado SFC
- CBS evidence
- corrupção detectada vs ausência de problema
- reboot requirement
- resultado após reparo

---

# 3. Component Store Health & Repair

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: DISM / Windows Servicing

Primary Product Area: Repair

Possible Consumers:

- Repair
- Windows Update

Final UI Placement: TBD

Descrição: Avaliar saúde da imagem/component store e restaurar componentes por DISM quando houver evidência.

Possível objetivo: Resolver corrupção que impede SFC, updates ou componentes Windows.

Possível benefício: Repair, Stability

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- CheckHealth/ScanHealth state
- RestoreHealth applicability
- source availability
- servicing pending
- resultado pós-reparo

---

# 4. Boot Failure Triage

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Boot / WinRE

Primary Product Area: Repair

Possible Consumers:

- Repair
- Recovery

Final UI Placement: TBD

Descrição: Classificar falhas de boot entre BCD/boot files, system files, update, storage, driver ou hardware.

Possível objetivo: Evitar reparo de boot genérico quando a causa é outra.

Possível benefício: Repair, Diagnostic

Aplicabilidade: Systems with boot issues

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- WinRE availability
- boot stage
- Startup Repair logs
- BCD visibility
- recent update/driver
- disk health signals

---

# 5. Startup Repair Orchestration

Status: CANDIDATE

Type: Repair

Technical Domain: WinRE / Boot

Primary Product Area: Repair

Possible Consumers:

- Repair
- Recovery

Final UI Placement: TBD

Descrição: Orientar ou acionar Startup Repair quando o padrão de falha é compatível com o mecanismo oficial do Windows.

Possível objetivo: Corrigir problemas comuns que impedem inicialização.

Possível benefício: Repair

Aplicabilidade: Systems unable to boot normally

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- WinRE access
- attempt history
- repair result
- fallback options

---

# 6. Windows Recovery Environment Health

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: WinRE / Recovery

Primary Product Area: Repair

Possible Consumers:

- Repair
- Recovery

Final UI Placement: TBD

Descrição: Verificar se WinRE existe, está habilitado e utilizável antes de depender dele para reparos.

Possível objetivo: Garantir que ferramentas de recuperação estejam disponíveis quando necessárias.

Possível benefício: Repair, Recovery

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- WinRE status
- recovery image/path
- partition accessibility
- BitLocker implications

---

# 7. Restart / Pending Operation Resolution

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Windows Servicing

Primary Product Area: Repair

Possible Consumers:

- Repair
- Windows Update

Final UI Placement: TBD

Descrição: Detectar operações pendentes cujo reparo real é concluir reinicialização/servicing.

Possível objetivo: Evitar diagnósticos falsos enquanto o Windows ainda precisa finalizar uma alteração.

Possível benefício: Repair, Stability

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- pending reboot indicators
- pending servicing
- installer activity
- uptime
- update state

---

# 8. Built-in Troubleshooter Orchestration

Status: CANDIDATE

Type: Repair / Diagnostic

Technical Domain: Windows Get Help

Primary Product Area: Repair

Possible Consumers:

- Repair
- Network
- Audio
- Windows Update

Final UI Placement: TBD

Descrição: Direcionar solucionadores oficiais relevantes sem tratar todos como scripts universais.

Possível objetivo: Aproveitar diagnóstico suportado e registrar descobertas.

Possível benefício: Repair, Diagnostic

Aplicabilidade: Supported Windows versions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- problema alvo
- troubleshooter disponível
- findings
- ações propostas
- resultado

---

# 9. Application Repair / Reset Awareness

Status: CANDIDATE

Type: Repair / Configuration

Technical Domain: Windows Apps

Primary Product Area: Repair

Possible Consumers:

- Repair
- Applications

Final UI Placement: TBD

Descrição: Detectar quando um app suporta Repair/Reset por mecanismos do Windows e explicar diferença entre ambos.

Possível objetivo: Corrigir apps sem recorrer diretamente a reinstalação ou exclusão manual de dados.

Possível benefício: Repair, Quality of Life

Aplicabilidade: Supported applications

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- package/app identity
- repair availability
- reset consequence
- user data impact

---

# 10. Microsoft Store / Packaged App Repair

Status: CANDIDATE

Type: Repair

Technical Domain: AppX / Microsoft Store

Primary Product Area: Repair

Possible Consumers:

- Repair
- Applications

Final UI Placement: TBD

Descrição: Diagnosticar falhas comuns de apps empacotados/Store usando mecanismos suportados, sem registrar DLLs ou apagar caches indiscriminadamente.

Possível objetivo: Restaurar funcionamento de apps/Store com baixo risco.

Possível benefício: Repair, Compatibility

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: User Choice

Possíveis informações / dados / checks:

- Store/package health
- license/account context
- update status
- repair/reset availability

---

# 11. Network Stack Repair Triage

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Network

Primary Product Area: Repair

Possible Consumers:

- Repair
- Network

Final UI Placement: TBD

Descrição: Determinar se reset de componentes de rede é realmente necessário após diagnóstico de DNS, DHCP, Winsock, adapters e filters.

Possível objetivo: Usar resets apenas quando a falha está na pilha local.

Possível benefício: Repair, Connectivity

Aplicabilidade: Network issues

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- adapter state
- IP config
- DNS reachability
- gateway
- filter drivers
- VPN/proxy context

---

# 12. Audio Device Repair Triage

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Audio

Primary Product Area: Repair

Possible Consumers:

- Repair
- Audio

Final UI Placement: TBD

Descrição: Correlacionar dispositivo, endpoint, serviço, driver, formato e app antes de reset/reinstall.

Possível objetivo: Resolver áudio quebrado sem desativar enhancements/spatial audio preventivamente.

Possível benefício: Repair, Compatibility

Aplicabilidade: Audio issues

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- endpoint state
- default device
- driver errors
- service state
- exclusive lock
- recent changes

---

# 13. Device Re-enumeration / Restart Guidance

Status: CANDIDATE

Type: Repair

Technical Domain: PnP / Devices

Primary Product Area: Repair

Possible Consumers:

- Repair
- Drivers
- USB

Final UI Placement: TBD

Descrição: Usar disable/enable, rescan ou reconnect somente para devices apropriados e sem automatizar em hardware crítico.

Possível objetivo: Recuperar dispositivos presos/transientes.

Possível benefício: Repair, Compatibility

Aplicabilidade: Specific device issues

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- device class
- critical/protected status
- PnP error
- hotplug support
- driver state

---

# 14. Driver Rollback / Reinstall Repair Path

Status: CANDIDATE

Type: Repair

Technical Domain: Drivers / PnP

Primary Product Area: Repair

Possible Consumers:

- Repair
- Drivers

Final UI Placement: TBD

Descrição: Selecionar rollback, reinstall ou vendor package quando evidência aponta regressão/corrupção de driver.

Possível objetivo: Restaurar dispositivo sem universal driver updater.

Possível benefício: Repair, Stability, Compatibility

Aplicabilidade: Driver-related issues

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- recent driver change
- rollback availability
- known issue
- device class
- restore/recovery plan

---

# 15. File System Error Repair Triage

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Storage / Filesystem

Primary Product Area: Repair

Possible Consumers:

- Repair
- Storage

Final UI Placement: TBD

Descrição: Avaliar filesystem errors antes de decidir por scan/repair e respeitar necessidade de offline repair.

Possível objetivo: Corrigir estrutura lógica sem tratar toda lentidão como erro de disco.

Possível benefício: Repair, Stability

Aplicabilidade: Storage issues

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- filesystem type
- dirty/error signals
- volume role
- SMART/storage health
- offline requirement

---

# 16. Time & Clock Repair Triage

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Windows Time

Primary Product Area: Repair

Possible Consumers:

- Repair
- System
- Network

Final UI Placement: TBD

Descrição: Diagnosticar relógio incorreto, timezone, sync source e serviço antes de resetar configurações.

Possível objetivo: Corrigir falhas que afetam certificados, login, apps e rede.

Possível benefício: Repair, Compatibility

Aplicabilidade: Clock/time issues

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- timezone
- sync status
- time source
- clock drift
- domain context

---

# 17. Search / Index Repair Triage

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Windows Search

Primary Product Area: Repair

Possible Consumers:

- Repair
- System

Final UI Placement: TBD

Descrição: Distinguir indexação em andamento, escopo, serviço e corrupção antes de reconstruir índice.

Possível objetivo: Resolver busca quebrada sem destruir índice saudável.

Possível benefício: Repair, Quality of Life

Aplicabilidade: Search issues

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- index status
- scope
- service state
- recent changes
- rebuild necessity

---

# 18. Printing Repair Triage

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Printing

Primary Product Area: Repair

Possible Consumers:

- Repair
- Devices

Final UI Placement: TBD

Descrição: Classificar fila presa, spooler, driver, porta ou conectividade antes de limpar spool.

Possível objetivo: Resolver impressão sem apagar estado desnecessariamente.

Possível benefício: Repair, Compatibility

Aplicabilidade: Printing issues

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- queue state
- spooler
- printer connectivity
- driver status
- stuck jobs

---

# 19. Repair Before/After Verification

Status: CANDIDATE

Type: Measurement

Technical Domain: Shared / Repair

Primary Product Area: Shared

Possible Consumers:

- Repair
- Optimization

Final UI Placement: TBD

Descrição: Reexecutar checks relevantes após ação para provar se o sintoma foi resolvido.

Possível objetivo: Evitar declarar sucesso apenas porque um comando terminou sem erro.

Possível benefício: Diagnostic, Repair, Quality of Life

Aplicabilidade: All repair actions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- baseline
- action result
- symptom retest
- new errors
- reboot requirement

---

# 20. Repair History & Audit Trail

Status: CANDIDATE

Type: Diagnostic / Safety

Technical Domain: Shared / Repair

Primary Product Area: Shared

Possible Consumers:

- Repair
- Recovery
- Settings

Final UI Placement: TBD

Descrição: Registrar ações, resultados e alterações realizadas em cada sessão de reparo.

Possível objetivo: Dar rastreabilidade e facilitar rollback/escalation.

Possível benefício: Safety, Diagnostic

Aplicabilidade: All repair actions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- timestamp
- action
- reason
- result
- files/settings affected
- rollback availability

---

# 21. Repair Escalation & Stop Conditions

Status: CANDIDATE

Type: Safety / Recommendation

Technical Domain: Shared / Repair

Primary Product Area: Repair

Possible Consumers:

- Repair
- Recovery

Final UI Placement: TBD

Descrição: Definir quando parar automações e recomendar backup, recovery, reinstall ou suporte especializado.

Possível objetivo: Evitar loops destrutivos de reparo.

Possível benefício: Safety, Stability

Aplicabilidade: Complex/unresolved issues

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- repeated failure
- hardware suspicion
- data-loss risk
- recovery availability
- unsupported state

---

# Itens rejeitados / filtrados

- **“Run SFC/DISM/CHKDSK tudo de uma vez” — REJECTED:** reparo sem diagnóstico confunde causa e efeito.
- **Registry repair packs — REJECTED:** risco alto e sem semântica de problema.
- **DLL re-registration packs — REJECTED:** não usar como cura genérica.
- **Network reset como primeira ação — REJECTED:** último recurso contextual.
- **Reinstall de driver em massa — REJECTED:** contradiz área Drivers.
- **Clear caches para reparar qualquer coisa — REJECTED:** só quando há relação causal.
- **Desabilitar segurança para “testar” — REJECTED como fluxo padrão:** troubleshooting deve preservar proteções salvo orientação técnica específica e temporária.
- **Factory reset / reinstall como “repair automático” — REJECTED:** escalation de alto impacto.

# Principais conclusões

1. Repair deve começar por **triagem**, não por comandos.
2. SFC, DISM, Startup Repair e troubleshooters são mecanismos, não features isoladas por parâmetro.
3. O produto precisa provar resultado com **before/after verification**.
4. Reparo deve reconhecer quando a causa provável é **hardware**, não insistir em software.
5. Escalation e stop conditions são parte essencial da segurança.

# Fontes técnicas relevantes

- Microsoft Support — Startup Repair
  https://support.microsoft.com/en-us/windows/experience/startup-boot/startup-repair
- Microsoft Learn — Guidance for troubleshooting Windows Update issues
  https://learn.microsoft.com/en-us/troubleshoot/windows-client/installing-updates-features-roles/troubleshoot-windows-update-issues
- Microsoft Support — Windows troubleshooters / Get Help
  https://support.microsoft.com/windows/windows-troubleshooters

# Status final

**Repair — First Pass: INITIAL DISCOVERY COMPLETE**

Capabilities CANDIDATE: **21**

# Próximo passo

**Repair — Second Pass / Consolidated Gap Audit**

# BeadWise V2 — Discovery — Repair — Second Pass / Consolidated Gap Audit

**Regra canônica:** `01-DISCOVERY.md`  
**Filosofia:** Detect → Understand → Recommend → Apply → Measure → Keep / Rollback  
**Filtro:** preservar UX, segurança, estabilidade e reversibilidade; evitar granularidade por comando, chave, sensor ou parâmetro.

# 22. Safe Mode / Clean-Boot Diagnostic Path

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: Windows Startup / Troubleshooting

Primary Product Area: Repair

Possible Consumers:

- Repair
- Startup
- Processes

Final UI Placement: TBD

Descrição: Usar Safe Mode ou clean boot como isolamento diagnóstico, não como otimização permanente.

Possível objetivo: Separar problema de driver/service/app de problema core do Windows.

Possível benefício: Diagnostic, Repair

Aplicabilidade: Troubleshooting scenarios

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- boot mode
- services/startup scope
- symptom difference
- restoration steps

---

# 23. Known-Good State Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: System / History

Primary Product Area: Repair

Possible Consumers:

- Repair
- Recovery
- Drivers
- Windows Update

Final UI Placement: TBD

Descrição: Correlacionar início do problema com update, driver, app ou configuração recente.

Possível objetivo: Aumentar qualidade da hipótese antes de alterar o sistema.

Possível benefício: Diagnostic

Aplicabilidade: All repair scenarios

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- install/update history
- driver history
- restore points
- crash timeline

---

# 24. Repair Source Availability Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Windows Servicing

Primary Product Area: Repair

Possible Consumers:

- Repair
- Windows Update

Final UI Placement: TBD

Descrição: Detectar quando reparo depende de fonte externa/local compatível e quando Windows Update pode servir de source.

Possível objetivo: Evitar RestoreHealth falhar por source inadequado.

Possível benefício: Repair, Diagnostic

Aplicabilidade: Servicing corruption

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- edition/build/language
- source version
- network availability
- policy

---

# 25. BitLocker-Aware Repair Safeguard

Status: CANDIDATE

Type: Safety

Technical Domain: Security / Recovery

Primary Product Area: Shared

Possible Consumers:

- Repair
- Recovery
- Storage

Final UI Placement: TBD

Descrição: Considerar BitLocker e recovery keys antes de operações de boot, partition ou offline repair.

Possível objetivo: Evitar lockout e perda de acesso.

Possível benefício: Safety, Recovery

Aplicabilidade: Encrypted systems

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- BitLocker state
- key availability
- TPM changes
- WinRE access

---

# 26. Offline Repair Awareness

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: WinRE / Servicing

Primary Product Area: Repair

Possible Consumers:

- Repair
- Recovery

Final UI Placement: TBD

Descrição: Reconhecer quando o Windows online não é o ambiente adequado e orientar repair offline.

Possível objetivo: Permitir reparo de sistemas que não inicializam ou têm arquivos bloqueados.

Possível benefício: Repair

Aplicabilidade: Severe system issues

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- online/offline state
- target Windows installation
- drive letter mapping
- encryption

---

# 27. Repair Confidence & Evidence Summary

Status: CANDIDATE

Type: Recommendation / Diagnostic

Technical Domain: Shared / Repair

Primary Product Area: Repair

Possible Consumers:

- Repair
- Recommendations

Final UI Placement: TBD

Descrição: Apresentar hipótese, evidências e confiança antes de ações invasivas.

Possível objetivo: Tornar troubleshooting explicável.

Possível benefício: Diagnostic, Safety

Aplicabilidade: All repair scenarios

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- evidence sources
- alternative causes
- confidence
- risk
- next action

---

# 28. Repair Rollback Readiness

Status: CANDIDATE

Type: Safety / Diagnostic

Technical Domain: Recovery / Repair

Primary Product Area: Shared

Possible Consumers:

- Repair
- Recovery

Final UI Placement: TBD

Descrição: Verificar se existe caminho de retorno antes de mudanças de risco médio/alto.

Possível objetivo: Evitar reparos irreversíveis sem proteção.

Possível benefício: Safety, Recovery

Aplicabilidade: Risky repair actions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- restore point applicability
- backup
- driver rollback
- uninstall update
- manual undo

---

# 29. Repair Session Bundling

Status: CANDIDATE

Type: Tool / Diagnostic

Technical Domain: Shared / Repair

Primary Product Area: Repair

Possible Consumers:

- Repair
- Support

Final UI Placement: TBD

Descrição: Empacotar logs e resultados relevantes de uma sessão sem coletar dados excessivos.

Possível objetivo: Facilitar troubleshooting posterior ou suporte técnico.

Possível benefício: Diagnostic, Quality of Life

Aplicabilidade: Unresolved issues

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- selected logs
- privacy review
- hardware/software summary
- actions attempted
- error codes

---

# Itens avaliados e não promovidos

- Reset individual de cada serviço: detalhe de Feature Spec.
- Limpar cada cache específico: coberto por Cleaning/Repair contextual.
- Cada comando BCDBoot/Bootrec: implementação futura de Boot Repair, não capability.
- Cada Windows troubleshooter: consumidor de Built-in Troubleshooter Orchestration.
- Reinstall in-place: escalation/recovery, não item de reparo automático inicial.

# Conclusões consolidadas

Repair termina com **29 capabilities brutas**: 21 no First Pass + 8 gaps reais.

A área deve funcionar como um **motor de troubleshooting explicável**, capaz de isolar causa, escolher mecanismo suportado, verificar resultado e parar quando o risco aumenta.

# Status final

**Repair — INITIAL DISCOVERY COMPLETE**