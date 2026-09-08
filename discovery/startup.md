# BeadWise / PC Optimizer V2 — Startup Discovery

## First Pass — Broad Discovery

Startup é uma área de UX e performance. O objetivo é reduzir contenção real no boot/logon sem tornar o Windows menos pronto após entrar na sessão. Itens só devem ser recomendados para desativação quando impacto, função, dependências e intenção do usuário justificarem.

---

# 1. Startup Entry Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Startup

Primary Product Area: Startup

Possible Consumers:

- Optimization
- My PC
- Repair

Final UI Placement: TBD

Descrição: Inventariar itens de auto-start relevantes sem limitar a análise ao painel Startup Apps.

Possível objetivo: Criar visão unificada do que inicia em boot/logon.

Possível benefício: Encontrar impacto e persistência reais sem contagem simplista.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- entry name;
- source;
- user/machine scope;
- command/path;
- publisher;
- enabled state;

---

# 2. Run / RunOnce Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Logon Startup

Primary Product Area: Startup

Possible Consumers:

- Optimization
- Repair

Final UI Placement: TBD

Descrição: Identificar entradas Run/RunOnce de usuário e máquina.

Possível objetivo: Cobrir mecanismos clássicos de logon documentados pelo Windows.

Possível benefício: Diagnóstico transparente de auto-start.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- HKCU/HKLM;
- Run/RunOnce;
- command;
- publisher;
- scope;

---

# 3. Startup Folder Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Logon Startup

Primary Product Area: Startup

Possible Consumers:

- Optimization
- Repair

Final UI Placement: TBD

Descrição: Inventariar atalhos e executáveis nas pastas Startup do usuário e públicas.

Possível objetivo: Detectar auto-start simples e facilmente reversível.

Possível benefício: Melhor cobertura do caminho de logon.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- user/public folder;
- shortcut target;
- publisher;
- enabled/effective state;

---

# 4. Startup App Impact Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Startup Performance

Primary Product Area: Optimization

Possible Consumers:

- Benchmark
- Processes

Final UI Placement: TBD

Descrição: Avaliar impacto observado de apps no startup usando CPU/I/O e medições do Windows quando disponíveis.

Possível objetivo: Priorizar itens que realmente afetam responsividade.

Possível benefício: Evitar desabilitar vários itens de baixo impacto.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- startup impact;
- CPU time;
- disk I/O;
- startup duration;
- run frequency;

---

# 5. Boot / Logon Phase Timing

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Startup Performance

Primary Product Area: Benchmark

Possible Consumers:

- System
- Optimization

Final UI Placement: TBD

Descrição: Separar tempo de boot, logon e pós-logon em fases para localizar atraso.

Possível objetivo: Saber se o problema é firmware/OS, serviço, startup app ou post-logon.

Possível benefício: Direcionar otimização corretamente.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- boot phase;
- sign-in;
- desktop responsive;
- post-logon duration;
- repeatability;

---

# 6. Startup Entry Publisher & Signature Context

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Startup Identity

Primary Product Area: Startup

Possible Consumers:

- Security
- Optimization

Final UI Placement: TBD

Descrição: Identificar publisher/assinatura do executável associado ao auto-start.

Possível objetivo: Diferenciar componente legítimo conhecido de entrada obscura.

Possível benefício: Melhor decisão sem chamar item desconhecido de inútil.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- publisher;
- signature;
- path;
- product/company;

---

# 7. Startup Functional Role Classification

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Startup Semantics

Primary Product Area: Startup

Possible Consumers:

- Recommendation Engine
- Optimization

Final UI Placement: TBD

Descrição: Classificar função provável do item antes de recomendar desativação.

Possível objetivo: Preservar funcionalidades que o usuário espera imediatamente após logon.

Possível benefício: Melhor UX.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- security;
- hardware helper;
- cloud sync;
- communication;
- launcher;
- updater;
- tray utility;
- accessibility;
- unknown;

---

# 8. Startup User-Intent Awareness

Status: CANDIDATE

Type: Diagnostic / UX

Technical Domain: Startup Semantics

Primary Product Area: Shared

Possible Consumers:

- Startup
- Settings

Final UI Placement: TBD

Descrição: Registrar se o usuário depende ou deseja que determinado app esteja pronto no logon.

Possível objetivo: Evitar otimizar contra preferência explícita.

Possível benefício: Preservar UX e rotina.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- always needed;
- occasionally needed;
- not needed at login;
- unknown;

---

# 9. Startup Disable Candidate Scoring

Status: CANDIDATE

Type: Diagnostic / Recommendation

Technical Domain: Startup Optimization

Primary Product Area: Optimization

Possible Consumers:

- Startup

Final UI Placement: TBD

Descrição: Combinar impacto, função, frequência e intenção do usuário para gerar candidatos reais.

Possível objetivo: Priorizar poucas ações de alto valor.

Possível benefício: Reduzir clutter de recomendações.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- impact;
- functional role;
- user intent;
- relaunch behavior;
- dependency;
- confidence;

---

# 10. Startup Disable / Enable

Status: CANDIDATE

Type: Configuration

Technical Domain: Startup

Primary Product Area: Optimization

Possible Consumers:

- Startup
- Rollback

Final UI Placement: TBD

Descrição: Desabilitar ou reabilitar item usando mecanismo apropriado e preservando estado.

Possível objetivo: Reduzir custo de logon quando benefício é justificável.

Possível benefício: Boot/logon mais responsivo em casos reais.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- entry source;
- original state;
- new state;
- verification;
- re-enable;

---

# 11. Startup Before × After Validation

Status: CANDIDATE

Type: Benchmark

Technical Domain: Startup Performance

Primary Product Area: Benchmark

Possible Consumers:

- Optimization
- History

Final UI Placement: TBD

Descrição: Comparar boot/logon antes e depois de mudanças.

Possível objetivo: Confirmar que a desativação trouxe melhora além da variância.

Possível benefício: Manter apenas mudanças úteis.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Benchmark First

Possíveis informações:

- same boot type;
- startup timing;
- desktop responsiveness;
- background load;
- repeat runs;

---

# 12. Startup Re-Creation Detection

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Startup Persistence

Primary Product Area: Startup

Possible Consumers:

- Repair
- Optimization

Final UI Placement: TBD

Descrição: Detectar apps que recriam sua entrada após serem desabilitados.

Possível objetivo: Diferenciar falha do BeadWise de comportamento do próprio app.

Possível benefício: Orientar ajuste no software correto.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- entry removed/disabled;
- reappeared;
- process responsible;
- app setting context;

---

# 13. Launcher Auto-Start Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Startup Applications

Primary Product Area: Startup

Possible Consumers:

- Gaming
- Optimization

Final UI Placement: TBD

Descrição: Tratar launchers como apps de função conhecida e impacto variável.

Possível objetivo: Permitir desligar auto-start quando usuário não precisa deles sempre.

Possível benefício: Reduzir post-logon load sem remover launcher.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- launcher;
- impact;
- update behavior;
- background services;
- user intent;

---

# 14. Cloud Sync Startup Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Startup Applications

Primary Product Area: Startup

Possible Consumers:

- Storage
- Network
- Optimization

Final UI Placement: TBD

Descrição: Identificar cloud sync no startup e seu custo inicial sem marcá-lo como bloat.

Possível objetivo: Preservar sincronização quando necessária e medir contenção real.

Possível benefício: Decisão contextual.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- provider;
- startup impact;
- sync activity;
- user dependency;
- network/storage load;

---

# 15. Hardware Helper Startup Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Startup Applications

Primary Product Area: Startup

Possible Consumers:

- USB / Peripherals
- Audio
- Drivers

Final UI Placement: TBD

Descrição: Reconhecer suites de mouse, teclado, áudio, RGB e outros helpers.

Possível objetivo: Evitar perder macros, fan control, profiles ou firmware support.

Possível benefício: Preservar recursos do hardware.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- device dependency;
- features provided;
- resource impact;
- can run on-demand;

---

# 16. Security Software Startup Safeguard

Status: CANDIDATE

Type: Safeguard

Technical Domain: Startup Security

Primary Product Area: Security

Possible Consumers:

- Startup
- Optimization

Final UI Placement: TBD

Descrição: Proteger componentes de antivírus, endpoint protection e segurança de desativação genérica.

Possível objetivo: Não trocar segurança por pequenos ganhos de boot.

Possível benefício: Preservar proteção.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- security role;
- provider;
- critical services;
- user-managed state;

---

# 17. Accessibility Startup Safeguard

Status: CANDIDATE

Type: Safeguard

Technical Domain: Startup Accessibility

Primary Product Area: Shared

Possible Consumers:

- Startup
- Accessibility

Final UI Placement: TBD

Descrição: Identificar software assistivo que precisa iniciar com o usuário.

Possível objetivo: Evitar perda de acessibilidade.

Possível benefício: Preservar controle e autonomia.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- assistive role;
- startup dependency;
- criticality;

---

# 18. Auto-Start Service Context

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Services

Primary Product Area: Shared

Possible Consumers:

- Startup
- System

Final UI Placement: TBD

Descrição: Relacionar serviços auto-start ao boot sem duplicar discovery de Services.

Possível objetivo: Distinguir startup app de serviço persistente.

Possível benefício: Melhor attribution de boot cost.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- service;
- start type;
- trigger-start awareness;
- publisher;
- process host;

---

# 19. Trigger-Start Service Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Services

Primary Product Area: Startup

Possible Consumers:

- Energy
- System

Final UI Placement: TBD

Descrição: Reconhecer serviços que só iniciam mediante evento e não consomem recursos continuamente.

Possível objetivo: Evitar recomendação de desativação por aparecerem configurados no sistema.

Possível benefício: Menos falso positivo.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- trigger presence;
- current state;
- start type;
- trigger event;

---

# 20. Scheduled Task Startup Context

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Scheduled Tasks

Primary Product Area: Shared

Possible Consumers:

- Startup
- System

Final UI Placement: TBD

Descrição: Identificar tarefas que disparam no logon/boot sem transformar toda tarefa agendada em startup app.

Possível objetivo: Cobrir persistência e impacto temporal.

Possível benefício: Melhor diagnóstico.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- trigger type;
- task path;
- publisher;
- last/next run;
- duration;

---

# 21. Delayed Start Opportunity Diagnostics

Status: CANDIDATE

Type: Diagnostic / Recommendation

Technical Domain: Startup Optimization

Primary Product Area: Optimization

Possible Consumers:

- Startup

Final UI Placement: TBD

Descrição: Identificar software útil no logon que poderia iniciar mais tarde sem prejudicar UX.

Possível objetivo: Reduzir contenção inicial sem desativar funcionalidade.

Possível benefício: Compromisso melhor entre responsividade e conveniência.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Manual Guidance

Possíveis informações:

- startup impact;
- required-at-logon;
- delay tolerance;
- existing app setting;

---

# 22. Automatic Maintenance Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Scheduled Maintenance

Primary Product Area: Startup

Possible Consumers:

- System
- Energy

Final UI Placement: TBD

Descrição: Reconhecer trabalhos periódicos que o Windows já agenda para momentos de menor impacto.

Possível objetivo: Evitar mover manutenção saudável para startup ou desativá-la.

Possível benefício: Preservar manutenção e eficiência.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- maintenance task;
- idle/AC behavior;
- schedule;
- resource context;

---

# 23. Startup Entry Dependency Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Startup Semantics

Primary Product Area: Startup

Possible Consumers:

- Repair
- Optimization

Final UI Placement: TBD

Descrição: Identificar dependências óbvias entre startup item e feature/aplicação.

Possível objetivo: Evitar desabilitar helper necessário a função importante.

Possível benefício: Menos regressões.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- parent app;
- service dependency;
- hardware feature;
- user workflow;

---

# 24. Broken Startup Entry Diagnostics

Status: CANDIDATE

Type: Diagnostic / Cleaning

Technical Domain: Startup Integrity

Primary Product Area: Startup

Possible Consumers:

- Repair
- Cleaning

Final UI Placement: TBD

Descrição: Detectar entradas apontando para arquivo inexistente ou instalação removida.

Possível objetivo: Separar item quebrado de item apenas desnecessário.

Possível benefício: Limpeza segura com valor real.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- missing target;
- invalid shortcut;
- uninstalled product;
- source location;

---

# 25. Startup Configuration Drift

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Startup History

Primary Product Area: Startup

Possible Consumers:

- History
- Repair

Final UI Placement: TBD

Descrição: Registrar mudanças de auto-start ao longo do tempo.

Possível objetivo: Responder o que começou a iniciar recentemente.

Possível benefício: Correlacionar regressões de boot.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- new entry;
- removed entry;
- enabled state change;
- publisher;
- timestamp;

---

# 26. Startup Change Backup & Rollback

Status: CANDIDATE

Type: Safeguard

Technical Domain: Startup Configuration

Primary Product Area: Shared

Possible Consumers:

- Optimization
- History

Final UI Placement: TBD

Descrição: Preservar estado original de cada item alterado.

Possível objetivo: Permitir re-enable rápido.

Possível benefício: Reversibilidade.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- source;
- entry;
- old state;
- new state;
- timestamp;
- rollback;

---

# 27. Startup Recommendation Explanation

Status: CANDIDATE

Type: Diagnostic / Quality of Life

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- Startup
- Optimization

Final UI Placement: TBD

Descrição: Explicar impacto, função e trade-off de cada sugestão.

Possível objetivo: Evitar botão 'disable all'.

Possível benefício: Decisão informada.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- measured impact;
- role;
- expected benefit;
- lost convenience;
- rollback;
- confidence;

---

# 28. Startup Recommendation Confidence

Status: CANDIDATE

Type: Diagnostic / Recommendation Infrastructure

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- Startup

Final UI Placement: TBD

Descrição: Classificar confiança de que um item realmente vale sair do startup.

Possível objetivo: Não converter suspeita em recomendação forte.

Possível benefício: Menos ações irrelevantes.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- measurement quality;
- functional role certainty;
- user intent;
- dependency certainty;
- before/after evidence;

---

# Itens filtrados / rejeitados

## Disable all third-party startup

REJECTED. Software de segurança, sincronização, hardware e rotina pode ter motivo legítimo para iniciar.

## Count-based startup score

REJECTED. Dez itens leves podem impactar menos que um único item pesado.

## Disable Microsoft entries by default

REJECTED. Assinatura/vendor sozinho não decide utilidade.

## Disable hardware suites automatically

REJECTED. Pode perder macros, perfis, fan control ou outras funções.

## Disable cloud sync for performance

REJECTED como regra universal.

## Move everything to delayed start

REJECTED. Delay também altera UX e ordem funcional.

---

# Principais conclusões

1. Startup deve ser otimizado por impacto medido, função e intenção do usuário.
2. Task Manager já mede impacto por CPU/I/O; o produto deve usar essa filosofia, não contagem de itens.
3. Serviços, tasks e apps de logon são mecanismos diferentes.
4. Trigger-start e Automatic Maintenance evitam custo contínuo e devem ser preservados.
5. Desabilitar item útil pode piorar UX mais do que melhorar boot.
6. Broken entries são candidatos melhores a cleanup do que software legítimo de baixo impacto.
7. Before × After é importante porque boot possui variância.
8. Startup First Pass deve ser conservadora e explicável.

---

# Fontes técnicas de referência

## Microsoft — Startup Apps

https://learn.microsoft.com/windows/win32/w8cookbook/startup-apps

## Microsoft — Run / RunOnce

https://learn.microsoft.com/windows/win32/setupapi/run-and-runonce-registry-keys

## Microsoft Sysinternals — Autoruns

https://learn.microsoft.com/sysinternals/downloads/autoruns

## Microsoft — Automatic Maintenance

https://learn.microsoft.com/windows/win32/taskschd/task-maintenence

## Microsoft — Startup and Shutdown Experience

https://learn.microsoft.com/windows-hardware/test/weg/delivering-a-great-startup-and-shutdown-experience

---

# Status final

**Startup — FIRST PASS COMPLETE**

Próximo passo: **Startup — Second Pass**

# BeadWise / PC Optimizer V2 — Startup Discovery

## Second Pass — Consolidated Gap Audit

A segunda passada fecha gaps de boot tracing, post-logon responsiveness, mecanismos avançados de auto-start e safeguards de logon, mantendo o catálogo focado em impacto real.

---

# 29. Autoruns Extended Location Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Auto-Start Extensibility

Primary Product Area: Startup

Possible Consumers:

- Advanced Diagnostics
- Security

Final UI Placement: TBD

Descrição: Reconhecer locais adicionais de auto-start além de Run/Startup Folder, sem transformar todos em candidatos de otimização.

Possível objetivo: Cobrir shell extensions, Winlogon, services e outros mecanismos na investigação avançada.

Possível benefício: Encontrar persistência obscura quando há problema real.

Aplicabilidade: Advanced Diagnostics

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- location category;
- publisher;
- signature;
- path;
- Microsoft/third-party context;

---

# 30. Explorer Shell Extension Startup Context

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Shell Extensions

Primary Product Area: Startup

Possible Consumers:

- Explorer
- Repair
- Processes

Final UI Placement: TBD

Descrição: Identificar extensões carregadas pelo Explorer como custo/contexto distinto de startup app.

Possível objetivo: Diagnosticar shell lento/crash sem desabilitar extensões em massa.

Possível benefício: Troubleshooting preciso.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- extension;
- publisher;
- load context;
- Explorer issue correlation;

---

# 31. Winlogon Extension Safeguard

Status: CANDIDATE

Type: Diagnostic / Safeguard

Technical Domain: Logon

Primary Product Area: Startup

Possible Consumers:

- Security
- Repair

Final UI Placement: TBD

Descrição: Tratar extensões e providers de logon como componentes sensíveis.

Possível objetivo: Impedir otimização casual de mecanismo crítico de autenticação/logon.

Possível benefício: Segurança e boot reliability.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- provider;
- signature;
- criticality;
- logon role;

---

# 32. Boot Trace / WPA Escalation

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: Boot Performance

Primary Product Area: Benchmark

Possible Consumers:

- Startup
- System

Final UI Placement: TBD

Descrição: Escalar para trace de boot/WPA quando métricas simples não explicam atraso.

Possível objetivo: Encontrar CPU/I/O/process phase bottlenecks reais.

Possível benefício: Análise profunda sem tweaks por tentativa.

Aplicabilidade: Advanced Slow-Boot Cases

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- boot trace;
- CPU timeline;
- disk I/O;
- process phase;
- service/startup attribution;

---

# 33. Fast Startup vs Full Boot Benchmark Context

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Boot Methodology

Primary Product Area: Startup

Possible Consumers:

- Benchmark
- System

Final UI Placement: TBD

Descrição: Distinguir Fast Startup, Restart e full boot ao comparar tempos.

Possível objetivo: Evitar benchmarks incomparáveis.

Possível benefício: Medição correta.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- boot type;
- shutdown/restart path;
- hiberboot state;
- repeatability;

---

# 34. Post-Logon Responsiveness Diagnostics

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Startup UX

Primary Product Area: Startup

Possible Consumers:

- Processes
- Benchmark

Final UI Placement: TBD

Descrição: Medir quando o desktop fica realmente responsivo após logon, não apenas quando aparece.

Possível objetivo: Capturar contenção que o tempo de boot bruto esconde.

Possível benefício: Melhor alinhamento com percepção do usuário.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- desktop shown;
- CPU/disk busy period;
- app launch responsiveness;
- post-logon stabilization;

---

# 35. Startup App Failure Diagnostics

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: Startup Integrity

Primary Product Area: Startup

Possible Consumers:

- Repair

Final UI Placement: TBD

Descrição: Detectar app que deveria iniciar mas falha, ao invés de focar apenas em desativar itens.

Possível objetivo: Tratar startup como feature funcional também.

Possível benefício: Corrigir missing tray/helper esperado.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- entry enabled;
- process absent;
- crash/event;
- missing target;
- policy block;

---

# 36. RunOnce Transient-State Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: RunOnce

Primary Product Area: Startup

Possible Consumers:

- Repair
- Installations

Final UI Placement: TBD

Descrição: Reconhecer RunOnce como mecanismo transitório de setup/recovery e evitar limpeza agressiva.

Possível objetivo: Não interromper instalação ou conclusão pós-reboot.

Possível benefício: Confiabilidade.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- entry age;
- install/update correlation;
- admin logon dependency;
- transient context;

---

# 37. Startup Optimization Session Profile

Status: CANDIDATE

Type: Optimization / Safeguard

Technical Domain: Startup Changes

Primary Product Area: Optimization

Possible Consumers:

- History
- Benchmark

Final UI Placement: TBD

Descrição: Agrupar mudanças de uma sessão de startup optimization para benchmark/rollback conjunto.

Possível objetivo: Avaliar pacote de mudanças de forma controlada.

Possível benefício: Rollback simples.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- changed entries;
- baseline timing;
- post-change timing;
- keep/rollback;

---

# 38. Startup Regression Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Startup History

Primary Product Area: Startup

Possible Consumers:

- Drivers
- Windows Update
- Processes

Final UI Placement: TBD

Descrição: Correlacionar boot pior com software/update recém-instalado.

Possível objetivo: Encontrar mudança temporal relevante.

Possível benefício: Evitar desativação ampla.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- new startup item;
- software install;
- driver/update;
- timing regression;

---

# 39. Startup Recommendation Confidence

Status: CANDIDATE

Type: Diagnostic / Recommendation Infrastructure

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- Startup

Final UI Placement: TBD

Descrição: Refinar confiança com base em medições repetidas e função conhecida.

Possível objetivo: Evitar recomendação forte quando boot trace é inconclusivo.

Possível benefício: Melhor precisão.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- repeated impact;
- role certainty;
- dependency;
- user intent;
- variance;

---

# Itens filtrados / rejeitados

## Disable shell extensions globally

REJECTED.

## Delete RunOnce entries as cleanup

REJECTED salvo entrada comprovadamente órfã.

## Disable Fast Startup as optimization

REJECTED como regra universal; é contexto de benchmark/troubleshooting.

## Boot-time world record presets

REJECTED; responsividade e funcionalidade pós-logon importam mais.

---

# Principais conclusões

1. Boot method precisa ser controlado em benchmark.
2. Post-logon responsiveness é mais útil que cronômetro até a área de trabalho.
3. Autoruns amplia cobertura, mas locais avançados não devem virar lista de coisas para desligar.
4. RunOnce merece proteção por ser frequentemente transitório.
5. Startup também precisa reparar itens esperados que deixaram de iniciar.
6. Startup — INITIAL DISCOVERY COMPLETE.

---

# Fontes técnicas de referência

## Microsoft — Startup Apps

https://learn.microsoft.com/windows/win32/w8cookbook/startup-apps

## Microsoft — Run / RunOnce

https://learn.microsoft.com/windows/win32/setupapi/run-and-runonce-registry-keys

## Microsoft Sysinternals — Autoruns

https://learn.microsoft.com/sysinternals/downloads/autoruns

## Microsoft — Automatic Maintenance

https://learn.microsoft.com/windows/win32/taskschd/task-maintenence

## Microsoft — Startup and Shutdown Experience

https://learn.microsoft.com/windows-hardware/test/weg/delivering-a-great-startup-and-shutdown-experience

---