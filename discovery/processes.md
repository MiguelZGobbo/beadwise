# BeadWise / PC Optimizer V2 — Processes Discovery

## First Pass — Broad Discovery

Processes deve transformar atividade do sistema em diagnóstico compreensível, não em uma lista de coisas para encerrar. O BeadWise deve priorizar attribution, tendência, função e correlação com sintomas antes de qualquer ação.

---

# 1. Process Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Processes

Primary Product Area: My PC

Possible Consumers:

- Monitoring
- Optimization
- Repair

Final UI Placement: TBD

Descrição: Inventariar processos ativos com identidade e contexto suficientes para análise.

Possível objetivo: Criar base para attribution de recursos e comportamento.

Possível benefício: Entender o que está rodando sem marcar processos desconhecidos como inúteis.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- PID;
- name;
- path;
- publisher;
- user/session;
- start time;
- parent process;

---

# 2. Process Tree & Parent/Child Mapping

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Process Topology

Primary Product Area: Processes

Possible Consumers:

- Repair
- Security
- Gaming

Final UI Placement: TBD

Descrição: Mapear relações parent/child para interpretar launchers, helpers, browsers e workloads multiprocess.

Possível objetivo: Entender unidade lógica de aplicação.

Possível benefício: Evitar encerrar helper essencial isoladamente.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- parent PID;
- children;
- process group/context;
- tree lifetime;

---

# 3. Application-to-Process Grouping

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Process Attribution

Primary Product Area: Processes

Possible Consumers:

- Monitoring
- UX

Final UI Placement: TBD

Descrição: Agrupar múltiplos processos pertencentes ao mesmo app/produto.

Possível objetivo: Mostrar impacto por aplicação em vez de fragmentar em dezenas de subprocessos.

Possível benefício: Melhor compreensão do usuário.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- product/company;
- executable set;
- aggregate CPU/RAM/disk/network/GPU;

---

# 4. Process CPU Usage Diagnostics

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: CPU Usage

Primary Product Area: Monitoring

Possible Consumers:

- CPU
- Optimization

Final UI Placement: TBD

Descrição: Medir CPU por processo e ao longo do tempo.

Possível objetivo: Encontrar workloads realmente consumidores.

Possível benefício: Attribution de gargalo real.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- CPU percentage;
- CPU time;
- per-core context when needed;
- sustained vs burst;

---

# 5. Process Memory Usage Diagnostics

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Memory Usage

Primary Product Area: Monitoring

Possible Consumers:

- RAM
- Optimization

Final UI Placement: TBD

Descrição: Medir working set, private memory e commit por processo com terminologia correta.

Possível objetivo: Evitar simplificar tudo como 'RAM usada'.

Possível benefício: Encontrar processos com consumo anormal ou crescimento.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- working set;
- private bytes;
- commit;
- shared memory context;
- trend;

---

# 6. Process Disk I/O Diagnostics

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Storage I/O

Primary Product Area: Monitoring

Possible Consumers:

- Storage
- Startup
- Optimization

Final UI Placement: TBD

Descrição: Atribuir reads/writes e I/O rate aos processos.

Possível objetivo: Identificar contenção real de storage.

Possível benefício: Explicar lentidão causada por update, sync, scan ou workload.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- read/write bytes;
- I/O rate;
- operation count;
- sustained/burst;
- target storage context;

---

# 7. Process Network Usage Diagnostics

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Network Usage

Primary Product Area: Monitoring

Possible Consumers:

- Network
- Privacy
- Gaming

Final UI Placement: TBD

Descrição: Atribuir tráfego de rede por processo/aplicação.

Possível objetivo: Identificar downloads/uploads relevantes.

Possível benefício: Encontrar saturação e background traffic.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- send/receive rate;
- connections;
- endpoint context;
- total transfer;

---

# 8. Process GPU Usage Diagnostics

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: GPU Usage

Primary Product Area: Monitoring

Possible Consumers:

- GPU
- Gaming

Final UI Placement: TBD

Descrição: Atribuir engine/GPU usage a processos quando disponível.

Possível objetivo: Encontrar app usando GPU em background ou workload gráfico real.

Possível benefício: Melhor diagnóstico de consumo e performance.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- GPU engine;
- utilization;
- dedicated/shared memory context;
- video encode/decode;

---

# 9. Process Resource Trend Monitoring

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Resource Trends

Primary Product Area: Monitoring

Possible Consumers:

- CPU
- RAM
- Storage
- Network

Final UI Placement: TBD

Descrição: Observar recursos ao longo do tempo em vez de snapshot único.

Possível objetivo: Distinguir spike legítimo de consumo sustentado.

Possível benefício: Reduzir falsos positivos.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- time series;
- baseline;
- spike;
- sustained load;
- session duration;

---

# 10. Foreground vs Background Context

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Process Context

Primary Product Area: Processes

Possible Consumers:

- Optimization
- Gaming

Final UI Placement: TBD

Descrição: Identificar se processo está interagindo com usuário ou executando trabalho em background.

Possível objetivo: Contextualizar consumo antes de recomendar ação.

Possível benefício: Evitar penalizar workload foreground útil.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- foreground window;
- session;
- background state;
- user interaction;

---

# 11. Process Responsiveness / Hang Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Process Health

Primary Product Area: Repair

Possible Consumers:

- Processes
- Crash Diagnostics

Final UI Placement: TBD

Descrição: Detectar app não respondendo e diferenciar hang transitório de crash.

Possível objetivo: Orientar wait/close/restart com contexto.

Possível benefício: Melhor troubleshooting de apps travados.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- window responsiveness;
- CPU state;
- wait duration;
- crash/hang events;

---

# 12. Process Crash History Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Process Stability

Primary Product Area: Repair

Possible Consumers:

- System
- Drivers

Final UI Placement: TBD

Descrição: Correlacionar processo com Application Error/WER/Reliability history.

Possível objetivo: Identificar aplicações instáveis.

Possível benefício: Foco em problema real em vez de consumo normal.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- faulting app;
- fault module;
- crash count;
- version;
- recent update;

---

# 13. Process Start / Exit Monitoring

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Process Lifecycle

Primary Product Area: Monitoring

Possible Consumers:

- Startup
- Gaming
- Repair

Final UI Placement: TBD

Descrição: Registrar criação e término para entender workloads intermitentes.

Possível objetivo: Correlacionar spikes e eventos com processo que já saiu.

Possível benefício: Melhor diagnóstico temporal.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- start time;
- exit time;
- exit code when available;
- parent;
- resource burst;

---

# 14. Process Priority Class Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Scheduling

Primary Product Area: Processes

Possible Consumers:

- CPU
- Gaming

Final UI Placement: TBD

Descrição: Ler priority class com contexto e sinalizar estados extremos sem sugerir aumento automático.

Possível objetivo: Identificar configurações anormais.

Possível benefício: Evitar prioridades mal configuradas.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- Idle;
- Below Normal;
- Normal;
- Above Normal;
- High;
- Realtime;

---

# 15. Process Priority Adjustment Safeguard

Status: CANDIDATE

Type: Configuration / Safeguard

Technical Domain: Scheduling

Primary Product Area: Processes

Possible Consumers:

- Optimization
- Gaming

Final UI Placement: TBD

Descrição: Permitir ajustes somente em casos específicos, temporários e reversíveis.

Possível objetivo: Evitar starvation e regressões.

Possível benefício: Experimentos controlados quando há evidência.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: High

Evidence Level: Context-dependent

Action Mode: Benchmark First

Possíveis informações:

- original priority;
- target priority;
- workload reason;
- validation;
- restore on exit;

---

# 16. Background Processing Mode Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Scheduling

Primary Product Area: Processes

Possible Consumers:

- Optimization
- System

Final UI Placement: TBD

Descrição: Reconhecer que Windows possui modo próprio para workloads de background reduzindo prioridades de recursos.

Possível objetivo: Evitar reinventar prioridade apenas de CPU.

Possível benefício: Contextualizar aplicativos que cooperam com scheduler.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- background mode;
- CPU/I/O/memory scheduling context;
- foreground interaction;

---

# 17. Efficiency / Eco Mode Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Efficiency

Primary Product Area: Processes

Possible Consumers:

- Energy
- Battery
- Optimization

Final UI Placement: TBD

Descrição: Reconhecer mecanismos modernos de eficiência para processos sem tratá-los como penalidade universal.

Possível objetivo: Separar performance máxima de eficiência e responsividade.

Possível benefício: Melhor battery/performance-per-watt quando apropriado.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- efficiency state when observable;
- foreground/background;
- power context;
- performance effect;

---

# 18. Process Affinity / CPU Set State Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Scheduling

Primary Product Area: Shared

Possible Consumers:

- CPU
- Gaming

Final UI Placement: TBD

Descrição: Detectar restrições existentes de affinity/CPU Sets sem usar pinning como otimização genérica.

Possível objetivo: Encontrar configurações que limitam processo.

Possível benefício: Diagnosticar regressões reais.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- affinity mask/context;
- CPU Set restrictions;
- processor groups;
- heterogeneous CPU context;

---

# 19. Process I/O Priority Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: I/O Scheduling

Primary Product Area: Processes

Possible Consumers:

- Storage
- Optimization

Final UI Placement: TBD

Descrição: Contextualizar prioridade de I/O sem forçar High indiscriminadamente.

Possível objetivo: Entender contenção entre workloads.

Possível benefício: Melhor troubleshooting de background I/O.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- I/O priority when accessible;
- background mode;
- disk contention;

---

# 20. Process Memory Priority Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Memory Scheduling

Primary Product Area: Processes

Possible Consumers:

- RAM
- Optimization

Final UI Placement: TBD

Descrição: Reconhecer memory priority como mecanismo avançado, não tweak de RAM.

Possível objetivo: Evitar alterar sem evidência.

Possível benefício: Diagnóstico correto de políticas existentes.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- memory priority when observable;
- working set behavior;
- background context;

---

# 21. Job Object Membership Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Process Control

Primary Product Area: Processes

Possible Consumers:

- Containers
- Applications
- Gaming

Final UI Placement: TBD

Descrição: Reconhecer processos agrupados/controlados por Job Objects.

Possível objetivo: Evitar interpretar limites ou término conjunto como comportamento estranho.

Possível benefício: Melhor suporte a launchers, sandboxes e apps gerenciados.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- job membership when observable;
- group limits;
- process tree context;
- resource accounting;

---

# 22. Process Handle / Thread Count Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Process Health

Primary Product Area: Processes

Possible Consumers:

- Repair
- Leak Diagnostics

Final UI Placement: TBD

Descrição: Observar crescimento anormal de handles/threads sem usar thresholds mágicos.

Possível objetivo: Encontrar leaks ou runaway behavior.

Possível benefício: Diagnóstico de estabilidade.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- handle count;
- thread count;
- trend;
- baseline;
- app lifetime;

---

# 23. Process CPU-Time vs Utilization Context

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Process Metrics

Primary Product Area: Processes

Possible Consumers:

- Monitoring

Final UI Placement: TBD

Descrição: Distinguir CPU total acumulada de utilização instantânea.

Possível objetivo: Evitar ranking enganoso de processos antigos.

Possível benefício: Métricas mais compreensíveis.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- user time;
- kernel time;
- current utilization;
- process age;

---

# 24. Process Suspend / Resume Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Process Lifecycle

Primary Product Area: Processes

Possible Consumers:

- Tools
- Optimization

Final UI Placement: TBD

Descrição: Reconhecer processos suspensos pelo sistema/app model e permitir ação manual apenas quando segura.

Possível objetivo: Não matar processo legítimo apenas por estar suspenso.

Possível benefício: Preservar gestão moderna do Windows.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- suspended state;
- app model context;
- resource usage;
- resume trigger;

---

# 25. Safe Process Termination

Status: CANDIDATE

Type: Repair / Tool

Technical Domain: Process Lifecycle

Primary Product Area: Tools

Possible Consumers:

- Repair
- Monitoring

Final UI Placement: TBD

Descrição: Encerrar processo específico com confirmação e classificação de criticidade.

Possível objetivo: Recuperar app travado sem reboot.

Possível benefício: Intervenção útil e localizada.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- process identity;
- unsaved-work risk;
- system criticality;
- child processes;
- result;

---

# 26. Process Restart Workflow

Status: CANDIDATE

Type: Repair / Tool

Technical Domain: Process Lifecycle

Primary Product Area: Repair

Possible Consumers:

- Processes
- UX

Final UI Placement: TBD

Descrição: Encerrar e reabrir app/processo quando existe caminho confiável para restaurá-lo.

Possível objetivo: Resolver estado temporário com mínimo impacto.

Possível benefício: Alternativa a reboot.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- launch command/app identity;
- state risk;
- restart result;

---

# 27. Critical Process Safeguard

Status: CANDIDATE

Type: Safeguard

Technical Domain: Process Safety

Primary Product Area: Shared

Possible Consumers:

- Processes
- Repair

Final UI Placement: TBD

Descrição: Bloquear término/alteração perigosa de processos críticos do Windows e segurança.

Possível objetivo: Evitar crash, logoff ou perda de controle.

Possível benefício: Estabilidade e segurança.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- critical/system process;
- security role;
- session role;
- protected process context;

---

# 28. Process Resource Anomaly Detection

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Process Analysis

Primary Product Area: Processes

Possible Consumers:

- Optimization
- Repair

Final UI Placement: TBD

Descrição: Detectar comportamento fora do baseline do próprio processo/sessão, não simplesmente uso alto.

Possível objetivo: Encontrar consumo inesperado.

Possível benefício: Recomendações mais úteis.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- sustained anomaly;
- baseline;
- resource type;
- foreground/background;
- recent change;

---

# 29. Process Recommendation Explanation

Status: CANDIDATE

Type: Diagnostic / Quality of Life

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- Processes
- Optimization

Final UI Placement: TBD

Descrição: Explicar por que um processo merece atenção e qual função pode ser perdida.

Possível objetivo: Eliminar 'end task to boost PC' genérico.

Possível benefício: Decisão informada.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- resource evidence;
- process identity;
- role;
- expected effect;
- risk;
- alternative;

---

# 30. Process Change / Action History

Status: CANDIDATE

Type: Safeguard

Technical Domain: Process History

Primary Product Area: Shared

Possible Consumers:

- Processes
- Repair

Final UI Placement: TBD

Descrição: Registrar prioridades, suspensões, término/restart e resultados aplicados pelo BeadWise.

Possível objetivo: Auditoria e troubleshooting.

Possível benefício: Transparência.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- process/app;
- action;
- old/new state;
- timestamp;
- outcome;

---

# 31. Process Outcome Validation

Status: CANDIDATE

Type: Diagnostic / Validation

Technical Domain: Recommendation Validation

Primary Product Area: Processes

Possible Consumers:

- Optimization
- History

Final UI Placement: TBD

Descrição: Verificar se ação sobre processo realmente melhorou o sintoma relevante.

Possível objetivo: Não considerar 'processo fechado' como otimização comprovada.

Possível benefício: Evitar mudanças inúteis.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- resource before/after;
- foreground responsiveness;
- gaming/app metric;
- side effects;
- user feedback;

---

# Itens filtrados / rejeitados

## Set game/app priority High by default

REJECTED. High priority pode preemptar workloads normais e não é performance grátis.

## Realtime priority

REJECTED para fluxo normal. Pode prejudicar serviços do sistema e até responsividade do mouse.

## Kill all background processes

REJECTED. Background não significa inútil.

## End processes using most RAM

REJECTED. Uso de RAM não prova desperdício.

## End processes using most CPU

REJECTED sem contexto; workload ativo pode ser legítimo.

## Force affinity to fewer/best cores

REJECTED como otimização universal.

## Suspend random background apps

REJECTED; Windows/app model já gerencia muitos estados.

## Process cleaner / RAM booster

REJECTED.

---

# Principais conclusões

1. Processos precisam ser agrupados por aplicação e contexto, não tratados só por PID.
2. Uso alto não é problema por definição; duração, foreground e função importam.
3. Realtime/High priority não são performance grátis.
4. Background mode e efficiency mechanisms já existem no Windows e devem ser respeitados.
5. Affinity/CPU Sets continuam diagnósticos compartilhados, não tweaks universais.
6. Terminate/restart são ferramentas de reparo, não otimização de rotina.
7. Critical-process safeguards são obrigatórios.
8. Trends e anomalies são melhores que snapshots.
9. Processes First Pass deve explicar, não caçar processos.

---

# Fontes técnicas de referência

## Microsoft — SetPriorityClass

https://learn.microsoft.com/windows/win32/api/processthreadsapi/nf-processthreadsapi-setpriorityclass

## Microsoft — Job Objects

https://learn.microsoft.com/windows/win32/procthread/job-objects

## Microsoft — Processes and Threads

https://learn.microsoft.com/windows/win32/procthread/processes-and-threads

---

# Status final

**Processes — FIRST PASS COMPLETE**

Próximo passo: **Processes — Second Pass**

# BeadWise / PC Optimizer V2 — Processes Discovery

## Second Pass — Consolidated Gap Audit

A segunda passada fecha lacunas de identidade, segurança, energia, leaks, contention e per-app policy, sem transformar o BeadWise em um task killer agressivo.

---

# 32. Process Command-Line & Launch Context Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Process Identity

Primary Product Area: Processes

Possible Consumers:

- Startup
- Repair
- Security

Final UI Placement: TBD

Descrição: Usar command line e launch context para distinguir instâncias do mesmo executável.

Possível objetivo: Entender modo/arquivo/profile que originou o processo.

Possível benefício: Melhor attribution.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- command line when permitted;
- parent;
- working context;
- app identity;

---

# 33. Process Session / User Isolation Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Process Context

Primary Product Area: Processes

Possible Consumers:

- Security
- Remote Sessions

Final UI Placement: TBD

Descrição: Distinguir processos por usuário, session ID e serviços.

Possível objetivo: Evitar encerrar processo de outra sessão ou serviço do sistema.

Possível benefício: Segurança operacional.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- user;
- session;
- interactive/service context;
- remote/local;

---

# 34. Protected / Restricted Process Awareness

Status: CANDIDATE

Type: Diagnostic / Safeguard

Technical Domain: Process Security

Primary Product Area: Security

Possible Consumers:

- Processes
- Repair

Final UI Placement: TBD

Descrição: Reconhecer processos protegidos/restritos que não devem ser tratados como apps comuns.

Possível objetivo: Evitar ações inválidas ou inseguras.

Possível benefício: Melhor estabilidade e segurança.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- protected context;
- access limitations;
- security role;

---

# 35. Process Mitigation Policy Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Process Security

Primary Product Area: Security

Possible Consumers:

- Processes
- Compatibility

Final UI Placement: TBD

Descrição: Reconhecer mitigations de processo como segurança/compatibilidade, não overhead para desativar.

Possível objetivo: Evitar 'performance tuning' de Exploit Protection.

Possível benefício: Preservar segurança.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- DEP/ASLR/CFG context;
- policy source;
- compatibility symptom;

---

# 36. Process Power / Energy Attribution

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Energy Usage

Primary Product Area: Processes

Possible Consumers:

- Battery
- Energy

Final UI Placement: TBD

Descrição: Correlacionar CPU/GPU/wake activity com impacto energético por aplicação.

Possível objetivo: Encontrar apps realmente caros em notebook.

Possível benefício: Recomendações perceptíveis de autonomia.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- resource usage;
- background activity;
- battery context;
- sustained energy impact;

---

# 37. Process Wake / Sleep Prevention Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Power Behavior

Primary Product Area: Processes

Possible Consumers:

- Energy
- Sleep/Wake

Final UI Placement: TBD

Descrição: Identificar processo/app associado a requests que impedem sleep/display-off quando observável.

Possível objetivo: Explicar PC que não dorme sem desativar serviços aleatórios.

Possível benefício: Melhor autonomia/UX.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- power request context;
- process;
- media/activity role;
- duration;

---

# 38. Process Working-Set Trim Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Memory Management

Primary Product Area: Processes

Possible Consumers:

- RAM

Final UI Placement: TBD

Descrição: Reconhecer trimming como função do memory manager e rejeitar limpeza manual genérica de working sets.

Possível objetivo: Evitar RAM cleaners disfarçados.

Possível benefício: Preservar cache/performance.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- working set;
- memory pressure;
- system trimming context;

---

# 39. Process Leak Trend Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Leak Diagnostics

Primary Product Area: Processes

Possible Consumers:

- RAM
- Repair

Final UI Placement: TBD

Descrição: Detectar crescimento contínuo de private bytes, handles ou threads ao longo da vida do app.

Possível objetivo: Distinguir app pesado de leak plausível.

Possível benefício: Melhor recomendação de restart/update.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- private bytes trend;
- handle trend;
- thread trend;
- runtime;
- plateau vs unbounded growth;

---

# 40. Process Contention Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Resource Contention

Primary Product Area: Processes

Possible Consumers:

- CPU
- Storage
- Network
- Gaming

Final UI Placement: TBD

Descrição: Correlacionar processo com queda de responsividade de outro workload.

Possível objetivo: Saber se background workload realmente compete por recurso.

Possível benefício: Recomendação baseada em efeito, não presença.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- resource saturation;
- victim workload;
- timing correlation;
- foreground impact;

---

# 41. Per-App Process Policy Profile

Status: CANDIDATE

Type: Configuration

Technical Domain: Process Profiles

Primary Product Area: Optimization

Possible Consumers:

- Gaming
- Battery
- Processes

Final UI Placement: TBD

Descrição: Guardar políticas contextuais aprovadas pelo usuário para apps específicos, somente quando há benefício medido.

Possível objetivo: Evitar mudanças globais de priority/efficiency.

Possível benefício: Controle granular e reversível.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Benchmark First

Possíveis informações:

- app identity;
- policy;
- apply on start;
- restore on exit;
- validation;

---

# 42. Process Action Dry-Run / Impact Preview

Status: CANDIDATE

Type: Safeguard

Technical Domain: Process Actions

Primary Product Area: Processes

Possible Consumers:

- Repair
- Optimization

Final UI Placement: TBD

Descrição: Mostrar dependências, children, sessão e risco antes de terminate/restart/policy change.

Possível objetivo: Evitar perda inesperada de função ou dados.

Possível benefício: UX segura.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- process tree;
- unsaved work risk;
- criticality;
- restart path;
- affected app;

---

# 43. Process Recommendation Confidence

Status: CANDIDATE

Type: Diagnostic / Recommendation Infrastructure

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- Processes
- Optimization

Final UI Placement: TBD

Descrição: Classificar confiança com base em trend, contention e função conhecida.

Possível objetivo: Evitar chamar comportamento normal de problema.

Possível benefício: Menos falsos positivos.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- resource trend;
- foreground/background;
- role certainty;
- symptom correlation;
- before/after evidence;

---

# 44. Process Known-Role Knowledge Boundary

Status: CANDIDATE

Type: Diagnostic / Safeguard

Technical Domain: Process Identity

Primary Product Area: Processes

Possible Consumers:

- Security
- UX

Final UI Placement: TBD

Descrição: Distinguir identificação confiável de inferência por nome de executável.

Possível objetivo: Não classificar processo apenas por filename.

Possível benefício: Evitar recomendações perigosas.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- signed publisher;
- installed product;
- path;
- service/app association;
- confidence;

---

# Itens filtrados / rejeitados

## Automatic priority optimizer

REJECTED. Prioridade deve continuar majoritariamente sob controle do scheduler/aplicação.

## Auto-suspend high-background-usage apps

REJECTED.

## Trim working sets to free RAM

REJECTED; é RAM cleaner.

## Disable Exploit Protection mitigations for FPS

REJECTED.

## Kill process by filename blacklist

REJECTED; identidade e contexto são necessários.

---

# Principais conclusões

1. Session, protection and mitigation context são safeguards essenciais.
2. Energy attribution pode gerar ganhos perceptíveis em notebooks sem degradar UX.
3. Leak detection precisa de trend, não snapshot.
4. Contention correlation é mais útil que rank de uso absoluto.
5. Policies per-app só entram com medição e rollback.
6. Working-set trimming permanece rejeitado.
7. Processes — INITIAL DISCOVERY COMPLETE.

---

# Fontes técnicas de referência

## Microsoft — SetPriorityClass

https://learn.microsoft.com/windows/win32/api/processthreadsapi/nf-processthreadsapi-setpriorityclass

## Microsoft — Job Objects

https://learn.microsoft.com/windows/win32/procthread/job-objects

## Microsoft — Processes and Threads

https://learn.microsoft.com/windows/win32/procthread/processes-and-threads

---