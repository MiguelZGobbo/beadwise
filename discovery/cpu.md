# Discovery Catalog — CPU
## Primeira passada — levantamento amplo

> Área dedicada à identificação, topologia, utilização, desempenho, scheduling, frequência, throttling e capacidades do processador.
>
> Esta área não deve assumir que:
>
> - mais frequência sempre é melhor;
> - todos os núcleos devem permanecer ativos;
> - afinidade manual melhora jogos;
> - prioridade High melhora qualquer processo;
> - desabilitar SMT melhora desempenho;
> - forçar scheduler é melhor que deixar o Windows decidir.
>
> A prioridade é:
>
> **detectar → compreender → medir → recomendar somente quando houver evidência.**

---

# 1. CPU Identification

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU

Primary Product Area:
My PC

Possible Consumers:
- Optimization
- Benchmark
- Monitoring
- Energy

Final UI Placement:
TBD

Descrição:
Identificar precisamente o processador instalado.

Possível objetivo:
Servir como base para todas as capacidades condicionais relacionadas à CPU.

Possível benefício:
Evitar aplicar recomendações genéricas em arquiteturas diferentes.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

Possíveis dados:
- manufacturer;
- family;
- model;
- stepping;
- architecture;
- processor name;
- socket;
- microarchitecture quando identificável;
- CPUID information;
- logical processors;
- physical cores;
- processor groups;
- NUMA topology.

---

# 2. CPU Topology Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU Topology

Primary Product Area:
Shared

Possible Consumers:
- CPU
- Benchmark
- Scheduler
- Monitoring

Final UI Placement:
TBD

Descrição:
Mapear a organização física e lógica do processador.

Possível objetivo:
Compreender a relação entre cores, threads, SMT, grupos e nós NUMA.

Possível benefício:
Base para análises corretas de scheduling e afinidade.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

Possíveis informações:
- packages;
- physical cores;
- logical processors;
- SMT siblings;
- processor groups;
- NUMA nodes;
- efficiency classes;
- heterogeneous core types quando expostos.

---

# 3. Physical Core / Logical Processor Mapping

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU Topology

Primary Product Area:
CPU

Possible Consumers:
- Scheduler
- Benchmark
- Monitoring

Final UI Placement:
TBD

Descrição:
Relacionar cada logical processor ao core físico correspondente.

Possível objetivo:
Diferenciar núcleo físico de SMT thread.

Possível benefício:
Evitar interpretações erradas de utilização e afinidade.

Aplicabilidade:
SMT-capable CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

---

# 4. SMT / Hyper-Threading Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU

Primary Product Area:
My PC

Possible Consumers:
- CPU
- Benchmark
- Virtualization

Final UI Placement:
TBD

Descrição:
Detectar suporte e estado de SMT/Hyper-Threading.

Possível objetivo:
Entender quantidade de threads disponíveis e comportamento de compartilhamento de recursos.

Possível benefício:
Dar contexto correto a benchmarks e scheduling.

Aplicabilidade:
Supported CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic Only

---

# 5. SMT Performance Comparison

Status:
IDEA

Type:
Benchmark

Technical Domain:
CPU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU
- Workstation

Final UI Placement:
TBD

Descrição:
Comparar workloads sensíveis a SMT quando houver possibilidade segura de realizar teste.

Possível objetivo:
Descobrir se uma workload específica se beneficia ou não de SMT.

Possível benefício:
Avaliação contextual.

Aplicabilidade:
Specific Hardware / Workloads

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Workload-dependent

Action Mode:
Benchmark First / Manual BIOS Guidance

Importante:
Não assumir SMT OFF como gaming optimization universal.

---

# 6. Hybrid CPU Architecture Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / Scheduler

Primary Product Area:
Shared

Possible Consumers:
- CPU
- Energy
- Gaming
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar processadores com classes de núcleo diferentes.

Possível objetivo:
Entender plataformas com performance cores e efficiency cores ou outras arquiteturas heterogêneas.

Possível benefício:
Permitir análise correta do scheduler.

Aplicabilidade:
Hybrid CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

Possíveis informações:
- efficiency class;
- number of performance cores;
- number of efficiency cores;
- logical processors per class.

---

# 7. CPU Efficiency Class Mapping

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Scheduler
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Mapear logical processors por classe de eficiência/desempenho quando o Windows expuser essa informação.

Possível objetivo:
Saber quais processadores são preferíveis para determinados workloads.

Possível benefício:
Base para CPU Sets e análise de scheduling heterogêneo.

Aplicabilidade:
Heterogeneous CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established API concept

Action Mode:
Diagnostic

---

# 8. CPU Cache Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU

Primary Product Area:
My PC

Possible Consumers:
- Benchmark
- CPU

Final UI Placement:
TBD

Descrição:
Detectar hierarquia de cache.

Possível objetivo:
Exibir especificações e permitir análise de topologia/cache-sensitive workloads.

Possível benefício:
Diagnóstico e hardware information.

Aplicabilidade:
All CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

Possíveis dados:
- L1;
- L2;
- L3;
- size;
- cache sharing;
- cache per core/cluster quando identificável.

---

# 9. CPU Instruction Set Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU

Primary Product Area:
My PC

Possible Consumers:
- Benchmark
- Compatibility

Final UI Placement:
TBD

Descrição:
Detectar principais instruction sets suportados pelo processador.

Possível objetivo:
Avaliar compatibilidade e selecionar workloads otimizados.

Possível benefício:
Permitir benchmark e diagnósticos apropriados.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

Possíveis exemplos:
- SSE family;
- AVX;
- AVX2;
- AVX-512 quando disponível;
- AES;
- virtualization extensions;
- architecture-specific capabilities.

---

# 10. CPU Virtualization Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / Virtualization

Primary Product Area:
My PC

Possible Consumers:
- Security
- Virtualization
- Optimization

Final UI Placement:
TBD

Descrição:
Detectar suporte de virtualização da CPU e se está habilitado no firmware quando possível.

Possível objetivo:
Explicar disponibilidade de Hyper-V, WSL2, VBS e virtual machines.

Possível benefício:
Diagnóstico de compatibilidade.

Aplicabilidade:
Supported CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 11. Current CPU Utilization

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
CPU

Primary Product Area:
Monitoring

Possible Consumers:
- CPU
- Optimization
- Benchmark

Final UI Placement:
TBD

Descrição:
Medir utilização atual da CPU.

Possível objetivo:
Representar carga global.

Possível benefício:
Diagnóstico básico.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Monitoring

---

# 12. Per-Core CPU Utilization

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
CPU

Primary Product Area:
Monitoring

Possible Consumers:
- CPU
- Gaming
- Benchmark

Final UI Placement:
TBD

Descrição:
Medir utilização de cada logical processor/core.

Possível objetivo:
Detectar workloads limitados por poucos threads.

Possível benefício:
Diferenciar “CPU 40%” de “um core totalmente saturado”.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Monitoring

---

# 13. CPU Utility vs CPU Time

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU Performance

Primary Product Area:
Shared

Possible Consumers:
- Monitoring
- Benchmark
- Optimization

Final UI Placement:
TBD

Descrição:
Distinguir tempo ocupado do processador de quantidade efetiva de trabalho executada levando em conta estado de desempenho/frequência.

Possível objetivo:
Evitar interpretação incorreta de métricas de CPU modernas.

Possível benefício:
Diagnóstico mais correto de throttling e workload.

Aplicabilidade:
Supported Windows Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Observação:
Windows diferencia Processor Time de Processor Utility.

---

# 14. Single-Core Saturation Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU Bottleneck

Primary Product Area:
Optimization

Possible Consumers:
- Gaming
- Benchmark
- Monitoring

Final UI Placement:
TBD

Descrição:
Detectar workloads limitados por um ou poucos threads mesmo quando utilização global parece baixa.

Possível objetivo:
Identificar gargalo de CPU thread-bound.

Possível benefício:
Explicar FPS limitado por CPU sem CPU total em 100%.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept

Action Mode:
Diagnostic

---

# 15. CPU Bottleneck Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU Performance

Primary Product Area:
Optimization

Possible Consumers:
- Gaming
- Benchmark
- Monitoring

Final UI Placement:
TBD

Descrição:
Combinar diferentes métricas para determinar possível saturação de CPU.

Possível objetivo:
Identificar quando CPU é provável limitador do workload.

Possível benefício:
Direcionar otimizações corretamente.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent

Action Mode:
Diagnostic Only

Possíveis evidências:
- sustained utilization;
- per-core saturation;
- queue length;
- frame timing;
- GPU underutilization;
- context switching;
- clock state.

Importante:
Não inferir gargalo apenas por percentual global de CPU.

---

# 16. Processor Queue Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Optimization
- Monitoring
- Benchmark

Final UI Placement:
TBD

Descrição:
Analisar quantidade de trabalho aguardando CPU.

Possível objetivo:
Encontrar saturação que simples porcentagem de utilização não explica completamente.

Possível benefício:
Melhor diagnóstico de CPU contention.

Applicability:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 17. Context Switch Rate Monitoring

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU Scheduler

Primary Product Area:
Monitoring

Possible Consumers:
- CPU
- Latency
- Processes

Final UI Placement:
TBD

Descrição:
Monitorar taxa de context switches.

Possível objetivo:
Detectar workloads com scheduling extremamente ativo ou comportamento anormal.

Possível benefício:
Contexto para performance/latency diagnostics.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Importante:
Número alto sozinho não constitui problema.

---

# 18. User Time / Kernel Time Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU

Primary Product Area:
CPU

Possible Consumers:
- Processes
- Repair
- Monitoring

Final UI Placement:
TBD

Descrição:
Separar tempo de CPU gasto em user mode e kernel/privileged mode.

Possível objetivo:
Distinguir aplicação pesada de atividade de kernel/driver.

Possível benefício:
Direcionar troubleshooting.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 19. Interrupt CPU Time Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / Interrupts

Primary Product Area:
CPU

Possible Consumers:
- DPC/ISR
- Drivers
- Monitoring

Final UI Placement:
TBD

Descrição:
Detectar consumo relevante de CPU causado por interrupts.

Possível objetivo:
Encontrar drivers ou hardware gerando carga excessiva.

Possível benefício:
Diferenciar aplicação pesada de interrupt storm.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 20. DPC CPU Time Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / DPC

Primary Product Area:
Shared

Possible Consumers:
- Latency
- Drivers
- Audio
- Network

Final UI Placement:
TBD

Descrição:
Medir CPU consumida em Deferred Procedure Calls.

Possível objetivo:
Detectar atividade de driver que afeta desempenho ou latência.

Possível benefício:
Direcionar investigação para DPC/ISR.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 21. CPU Clock Detection

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
CPU

Primary Product Area:
Monitoring

Possible Consumers:
- CPU
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Observar frequência atual da CPU.

Possível objetivo:
Verificar comportamento de boost e idle.

Possível benefício:
Diagnosticar frequência abaixo do esperado.

Aplicabilidade:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established / data-source dependent

Action Mode:
Monitoring

---

# 22. Per-Core Clock Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
CPU

Primary Product Area:
Monitoring

Possible Consumers:
- CPU
- Benchmark
- Gaming

Final UI Placement:
TBD

Descrição:
Observar clocks por núcleo ou logical processor quando hardware/API permitir.

Possível objetivo:
Entender boost assimétrico e scheduling.

Possível benefício:
Diagnóstico mais preciso.

Aplicabilidade:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Hardware-dependent

Action Mode:
Monitoring

---

# 23. Effective Clock Monitoring

Status:
CANDIDATE

Type:
Monitoring / Diagnostic

Technical Domain:
CPU

Primary Product Area:
Monitoring

Possible Consumers:
- CPU
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Investigar frequência efetivamente útil do núcleo, distinguindo clock reportado de tempo realmente executando.

Possível objetivo:
Evitar conclusões erradas baseadas apenas no clock instantâneo.

Possível benefício:
Melhor análise de idle, throttling e boost.

Aplicabilidade:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Vendor/hardware dependent

Action Mode:
Diagnostic

---

# 24. Base / Boost Frequency Validation

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU

Primary Product Area:
CPU

Possible Consumers:
- My PC
- Benchmark

Final UI Placement:
TBD

Descrição:
Comparar frequência observada com características esperadas do processador.

Possível objetivo:
Detectar CPU que não atinge comportamento de boost esperado.

Possível benefício:
Encontrar problemas térmicos, energéticos ou firmware.

Aplicabilidade:
Supported CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent

Action Mode:
Diagnostic

Importante:
Boost máximo de especificação não precisa aparecer em todos os cores simultaneamente.

---

# 25. Boost Behavior Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU Performance

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Analisar como frequência reage a workloads single-thread e multi-thread.

Possível objetivo:
Verificar comportamento normal do algoritmo de boost.

Possível benefício:
Identificar limitações.

Aplicabilidade:
Modern CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Hardware-dependent

Action Mode:
Diagnostic / Benchmark

---

# 26. CPU Temperature Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
CPU Thermal

Primary Product Area:
Monitoring

Possible Consumers:
- CPU
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Monitorar sensores térmicos disponíveis.

Possível objetivo:
Relacionar temperatura a frequência e throttling.

Possível benefício:
Diagnóstico térmico.

Aplicabilidade:
Sensor-capable Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Hardware-dependent

Action Mode:
Monitoring

---

# 27. Thermal Throttling Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU Thermal

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Cooling
- Monitoring

Final UI Placement:
TBD

Descrição:
Detectar redução de performance associada a limite térmico.

Possível objetivo:
Distinguir insuficiência da CPU de limitação causada por temperatura.

Possível benefício:
Encontrar problema de refrigeração.

Aplicabilidade:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept / Vendor telemetry dependent

Action Mode:
Diagnostic

---

# 28. CPU Power Throttling Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / Power

Primary Product Area:
CPU

Possible Consumers:
- Energy
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar situações em que frequência/desempenho é limitado por potência ou corrente.

Possível objetivo:
Separar power limit de thermal limit.

Possível benefício:
Diagnóstico correto de clocks reduzidos.

Aplicabilidade:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Hardware/vendor dependent

Action Mode:
Diagnostic

---

# 29. CPU Current / Electrical Limit Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
CPU Power

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Investigar sinais de limitação elétrica além de simples package power.

Possível objetivo:
Entender CPUs que atingem current/VRM limits.

Possível benefício:
Diagnóstico de desempenho sustentado.

Aplicabilidade:
Vendor-specific Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Vendor-dependent

Action Mode:
Diagnostic Only

---

# 30. CPU Package Power Monitoring

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
CPU / Power

Primary Product Area:
Monitoring

Possible Consumers:
- Energy
- Benchmark
- CPU

Final UI Placement:
TBD

Descrição:
Monitorar consumo energético estimado do pacote quando disponível.

Possível objetivo:
Relacionar desempenho, temperatura e potência.

Possível benefício:
Performance-per-watt e power throttling diagnostics.

Aplicabilidade:
Sensor-capable Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Vendor/hardware dependent

Action Mode:
Monitoring

---

# 31. CPU Performance-per-Watt Analysis

Status:
IDEA

Type:
Benchmark

Technical Domain:
CPU / Energy

Primary Product Area:
Benchmark

Possible Consumers:
- CPU
- Energy

Final UI Placement:
TBD

Descrição:
Avaliar desempenho produzido por unidade aproximada de energia.

Possível objetivo:
Encontrar sweet spot de eficiência.

Possível benefício:
Especialmente útil para laptops e sistemas térmicos limitados.

Applicability:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Benchmark

---

# 32. CPU Sustained Performance Test

Status:
CANDIDATE

Type:
Benchmark / Diagnostic

Technical Domain:
CPU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU
- Cooling

Final UI Placement:
TBD

Descrição:
Executar workload suficientemente longo para observar performance sustentada.

Possível objetivo:
Diferenciar burst boost de desempenho contínuo.

Possível benefício:
Detectar thermal/power throttling.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Measurement-based

Action Mode:
User-triggered Benchmark

---

# 33. CPU Short Burst Performance Test

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
CPU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU

Final UI Placement:
TBD

Descrição:
Medir responsividade/performance de curta duração.

Possível objetivo:
Avaliar comportamento de boost inicial.

Possível benefício:
Comparar com sustained performance.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Benchmark

---

# 34. Single-Thread Benchmark

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
CPU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU
- Gaming

Final UI Placement:
TBD

Descrição:
Medir desempenho predominantemente single-thread.

Possível objetivo:
Avaliar capacidade de execução serial.

Possível benefício:
Especialmente relevante a workloads sensíveis a poucos threads.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established benchmarking concept

Action Mode:
Benchmark

---

# 35. Multi-Thread Benchmark

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
CPU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU
- Workstation

Final UI Placement:
TBD

Descrição:
Medir throughput multi-thread.

Possível objetivo:
Avaliar utilização conjunta da CPU.

Possível benefício:
Workstation/content workloads.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Benchmark

---

# 36. Scaling Efficiency Analysis

Status:
IDEA

Type:
Benchmark / Diagnostic

Technical Domain:
CPU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU

Final UI Placement:
TBD

Descrição:
Comparar ganho entre 1, 2, 4, 8 e mais threads.

Possível objetivo:
Avaliar eficiência de escalabilidade do workload e CPU.

Possível benefício:
Distinguir limitação de software de limitação de hardware.

Aplicabilidade:
All PCs

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Benchmark

---

# 37. CPU Scheduler Observation

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Gaming
- Benchmark
- Processes

Final UI Placement:
TBD

Descrição:
Observar como threads são distribuídas entre logical processors durante workload.

Possível objetivo:
Entender comportamento real antes de tentar alterar scheduler/affinity.

Possível benefício:
Detectar casos anormais.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic Only

---

# 38. Hybrid Scheduler Behavior Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Scheduler / Hybrid CPU

Primary Product Area:
CPU

Possible Consumers:
- Gaming
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Analisar se workloads relevantes estão sendo distribuídos entre classes de core de maneira coerente.

Possível objetivo:
Diagnosticar comportamento inesperado em CPUs heterogêneas.

Possível benefício:
Explicar aplicações críticas executando em efficiency cores ou background workloads ocupando performance cores.

Aplicabilidade:
Hybrid CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established mechanism / interpretation context-dependent

Action Mode:
Diagnostic

---

# 39. Heterogeneous Scheduling Policy Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Scheduler / Power

Primary Product Area:
CPU

Possible Consumers:
- Energy
- Gaming

Final UI Placement:
TBD

Descrição:
Detectar políticas que influenciam preferência por classes de processador.

Possível objetivo:
Compreender configuração atual do Windows.

Possível benefício:
Base para troubleshooting.

Aplicabilidade:
Heterogeneous CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Possíveis políticas conceituais:
- all processors;
- performance processors;
- prefer performance;
- efficient processors;
- prefer efficient;
- automatic.

---

# 40. Heterogeneous Scheduling Policy Tuning

Status:
RESEARCHING

Type:
Configuration / Optimization

Technical Domain:
Scheduler / Hybrid CPU

Primary Product Area:
CPU

Possible Consumers:
- Gaming
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Investigar ajustes de políticas de scheduling heterogêneo.

Possível objetivo:
Avaliar workloads específicos que apresentem scheduling comprovadamente ruim.

Possível benefício:
Potencialmente melhorar comportamento em cenário específico.

Aplicabilidade:
Hybrid CPUs

Shared Capability:
Yes

Risco inicial:
High

Evidence Level:
Established mechanism / Unproven generic benefit

Action Mode:
Benchmark First

Importante:
Automatic deve permanecer referência inicial.

---

# 41. Process Affinity Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Scheduler

Primary Product Area:
Processes

Possible Consumers:
- CPU
- Gaming
- Tools

Final UI Placement:
TBD

Descrição:
Detectar máscaras de afinidade aplicadas a processos.

Possível objetivo:
Encontrar processos artificialmente restritos a determinados CPUs.

Possível benefício:
Diagnosticar ferramentas antigas/tweaks prejudiciais.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 42. Unexpected Affinity Restriction Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Scheduler

Primary Product Area:
Optimization

Possible Consumers:
- CPU
- Gaming
- Repair

Final UI Placement:
TBD

Descrição:
Detectar processo cujo acesso à CPU está limitado de maneira incomum.

Possível objetivo:
Encontrar configurações herdadas de launcher, tweak tool ou software específico.

Possível benefício:
Restaurar acesso normal aos processadores quando apropriado.

Aplicabilidade:
Affected Processes

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established mechanism

Action Mode:
Diagnostic First

---

# 43. Process Affinity Configuration

Status:
RESEARCHING

Type:
Configuration

Technical Domain:
Scheduler

Primary Product Area:
Tools

Possible Consumers:
- Gaming Profiles
- Processes

Final UI Placement:
TBD

Descrição:
Permitir configurar afinidade de processo em situações específicas.

Possível objetivo:
Suportar troubleshooting ou workloads especiais.

Possível benefício:
Context-dependent.

Aplicabilidade:
Specific Workloads

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Established mechanism / Benefit workload-specific

Action Mode:
Explicit User Choice / Benchmark First

Importante:
Não tratar “desabilitar core 0” ou regras semelhantes como otimização universal.

---

# 44. CPU Sets Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Processes
- Gaming
- Advanced Tools

Final UI Placement:
TBD

Descrição:
Detectar CPU Sets disponíveis e atribuições existentes.

Possível objetivo:
Entender mecanismo moderno de controle de CPUs além da affinity mask tradicional.

Possível benefício:
Melhor suporte a arquiteturas modernas e sistemas multi-group.

Aplicabilidade:
Supported Windows Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 45. CPU Sets Configuration

Status:
RESEARCHING

Type:
Configuration

Technical Domain:
Scheduler

Primary Product Area:
Tools

Possible Consumers:
- Gaming Profiles
- Advanced Tools

Final UI Placement:
TBD

Descrição:
Investigar uso de CPU Sets para orientar determinados processos a subconjuntos de CPUs.

Possível objetivo:
Permitir controle menos rígido que afinidade tradicional em alguns cenários.

Possível benefício:
Workload-specific.

Aplicabilidade:
Advanced / Specific Workloads

Shared Capability:
Yes

Risco inicial:
High

Evidence Level:
Established mechanism / Benefit requires proof

Action Mode:
Benchmark First

---

# 46. Affinity vs CPU Set Conflict Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Repair
- Advanced Tools

Final UI Placement:
TBD

Descrição:
Detectar situações em que affinity masks e CPU Sets entram em conflito.

Possível objetivo:
Explicar scheduling inesperado.

Possível benefício:
Diagnóstico de configurações avançadas quebradas.

Aplicabilidade:
Advanced Configurations

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established mechanism

Action Mode:
Diagnostic

---

# 47. Processor Group Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU Topology

Primary Product Area:
Shared

Possible Consumers:
- CPU
- Workstation
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar processor groups usados pelo Windows em máquinas com grande número de logical processors.

Possível objetivo:
Permitir comportamento correto em CPUs com mais de 64 logical processors.

Possível benefício:
Evitar ferramentas e benchmarks que enxergam apenas parte do processador.

Aplicabilidade:
High-core-count Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

---

# 48. Multi-Processor-Group Application Diagnosis

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Workstation
- Benchmark

Final UI Placement:
TBD

Descrição:
Investigar aplicações que não utilizam adequadamente CPUs muito grandes.

Possível objetivo:
Identificar limitação de software em sistemas >64 logical processors.

Possível benefício:
Explicar baixa escalabilidade.

Aplicabilidade:
High-core-count PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent

Action Mode:
Diagnostic

Observação:
Desde Windows 11, processos normalmente podem abranger múltiplos processor groups por padrão.

---

# 49. NUMA Topology Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / Memory Topology

Primary Product Area:
My PC

Possible Consumers:
- CPU
- RAM
- Workstation

Final UI Placement:
TBD

Descrição:
Detectar nós NUMA e CPUs associados.

Possível objetivo:
Dar contexto a máquinas multi-socket ou topologias complexas.

Possível benefício:
Diagnóstico de memory locality e workload scaling.

Aplicabilidade:
NUMA Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

---

# 50. NUMA Locality Diagnostic

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
CPU / Memory

Primary Product Area:
Tools

Possible Consumers:
- RAM
- Workstation
- Benchmark

Final UI Placement:
TBD

Descrição:
Investigar workloads com acesso remoto excessivo entre nós NUMA.

Possível objetivo:
Identificar perda de desempenho por memory locality.

Possível benefício:
Workstations/high-end systems.

Aplicabilidade:
NUMA Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept

Action Mode:
Diagnostic

---

# 51. Process Priority Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Scheduler

Primary Product Area:
Processes

Possible Consumers:
- CPU
- Gaming

Final UI Placement:
TBD

Descrição:
Detectar priority class aplicada aos processos.

Possível objetivo:
Identificar processos manualmente modificados ou configurados de forma incomum.

Possível benefício:
Diagnóstico de scheduling.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 52. Process Priority Configuration

Status:
RESEARCHING

Type:
Configuration

Technical Domain:
Scheduler

Primary Product Area:
Tools

Possible Consumers:
- Gaming Profiles
- Processes

Final UI Placement:
TBD

Descrição:
Permitir alterar priority class em casos específicos.

Possível objetivo:
Aumentar ou reduzir preferência relativa de scheduling.

Possível benefício:
Context-dependent.

Aplicabilidade:
Specific Workloads

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Established mechanism / Benefit context-dependent

Action Mode:
Benchmark First / User Choice

Importante:
High Priority não cria CPU adicional.

---

# 53. Realtime Priority Safeguard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
Scheduler

Primary Product Area:
Shared

Possible Consumers:
- Processes
- Tools

Final UI Placement:
TBD

Descrição:
Impedir ou desencorajar uso de Realtime priority para otimização comum.

Possível objetivo:
Evitar starvation de componentes essenciais.

Possível benefício:
Segurança e estabilidade.

Aplicabilidade:
Application-wide

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established risk

Action Mode:
Internal Safeguard

---

# 54. Thread Priority Observation

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Scheduler

Primary Product Area:
Advanced Tools

Possible Consumers:
- CPU
- Processes

Final UI Placement:
TBD

Descrição:
Observar prioridades de threads individuais durante investigação avançada.

Possível objetivo:
Diagnosticar comportamento anormal.

Possível benefício:
Troubleshooting profundo.

Aplicabilidade:
Advanced Scenarios

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 55. CPU Scheduler Trace

Status:
CANDIDATE

Type:
Advanced Diagnostic

Technical Domain:
ETW / Scheduler

Primary Product Area:
Tools

Possible Consumers:
- CPU
- Latency
- Benchmark

Final UI Placement:
TBD

Descrição:
Capturar scheduling, context switches e execução de threads via tracing.

Possível objetivo:
Investigar problemas que polling não consegue revelar.

Possível benefício:
Diagnóstico de scheduler baseado em comportamento real.

Aplicabilidade:
Advanced Troubleshooting

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
User-triggered Diagnostic

---

# 56. CPU Hardware Counter Profiling

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
CPU PMU

Primary Product Area:
Tools

Possible Consumers:
- Benchmark
- Advanced Diagnostics

Final UI Placement:
TBD

Descrição:
Investigar uso de hardware performance counters da CPU.

Possível objetivo:
Obter métricas microarquiteturais mais profundas.

Possível benefício:
Diagnosticar workloads CPU-bound com maior precisão.

Aplicabilidade:
Advanced / Supported CPUs

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established technology

Action Mode:
Advanced Diagnostic

Possíveis métricas futuras:
- instructions retired;
- cycles;
- cache misses;
- branch misses;
- IPC.

Dúvidas:
- APIs/permissões disponíveis no Windows?
- Complexidade excessiva para produto principal?

---

# 57. IPC / Instructions-per-Cycle Analysis

Status:
IDEA

Type:
Advanced Diagnostic / Benchmark

Technical Domain:
CPU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU
- Advanced Tools

Final UI Placement:
TBD

Descrição:
Avaliar eficiência de execução do workload em relação aos ciclos utilizados.

Possível objetivo:
Distinguir CPU ocupada de CPU realizando trabalho eficientemente.

Possível benefício:
Diagnóstico de bottleneck microarquitetural.

Aplicabilidade:
Supported PMU Hardware

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Advanced Benchmark

---

# 58. Cache Miss Analysis

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
CPU Cache

Primary Product Area:
Tools

Possible Consumers:
- Benchmark
- RAM

Final UI Placement:
TBD

Descrição:
Avaliar cache misses em workloads específicos.

Possível objetivo:
Identificar workloads memory/cache-bound.

Possível benefício:
Diferenciar falta de CPU de limitação de memória/cache.

Aplicabilidade:
Advanced Hardware Profiling

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established concept

Action Mode:
Diagnostic

---

# 59. Branch Misprediction Analysis

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
CPU

Primary Product Area:
Tools

Possible Consumers:
- Benchmark

Final UI Placement:
TBD

Descrição:
Investigar branch misprediction através de hardware counters quando disponível.

Possível objetivo:
Análise microarquitetural avançada.

Possível benefício:
Principalmente benchmarking técnico.

Aplicabilidade:
Advanced

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Advanced Diagnostic

---

# 60. CPU Benchmark Stability

Status:
CANDIDATE

Type:
Benchmark / Diagnostic

Technical Domain:
CPU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU
- Cooling
- Stability

Final UI Placement:
TBD

Descrição:
Verificar variação de performance durante múltiplas execuções.

Possível objetivo:
Detectar comportamento inconsistente.

Possível benefício:
Revelar thermal throttling, background interference ou instability.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Benchmark

---

# 61. CPU Performance Variability Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
CPU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU
- Monitoring

Final UI Placement:
TBD

Descrição:
Detectar variação acima do esperado entre execuções comparáveis.

Possível objetivo:
Identificar sistema instável ou condições inconsistentes.

Possível benefício:
Evitar benchmark enganoso.

Applicability:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Diagnostic

---

# 62. CPU Background Interference Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / Processes

Primary Product Area:
Benchmark

Possible Consumers:
- Optimization
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar processos externos consumindo CPU durante medição.

Possível objetivo:
Garantir benchmark limpo.

Possível benefício:
Resultados mais reprodutíveis.

Aplicabilidade:
Benchmark Sessions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established principle

Action Mode:
Automatic Safeguard

---

# 63. CPU Idle Baseline

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU

Primary Product Area:
CPU

Possible Consumers:
- Energy
- Optimization

Final UI Placement:
TBD

Descrição:
Medir nível normal de atividade da CPU em idle.

Possível objetivo:
Detectar background activity excessiva.

Possível benefício:
Diagnóstico de temperatura/consumo elevados em repouso.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Diagnostic

---

# 64. CPU Idle Anomaly Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU

Primary Product Area:
Optimization

Possible Consumers:
- Energy
- Processes

Final UI Placement:
TBD

Descrição:
Detectar CPU mantendo atividade significativa durante períodos que deveriam estar ociosos.

Possível objetivo:
Encontrar processos, drivers ou timers interferindo no idle.

Possível benefício:
Menor consumo, temperatura e ruído.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent

Action Mode:
Diagnostic

---

# 65. CPU Core Parking Observation

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / Energy

Primary Product Area:
CPU

Possible Consumers:
- Energy
- Benchmark

Final UI Placement:
TBD

Descrição:
Observar quais logical processors permanecem parked/unparked quando informação estiver disponível.

Possível objetivo:
Relacionar configuração de parking ao comportamento real.

Possível benefício:
Evitar conclusões baseadas apenas no valor configurado.

Aplicabilidade:
Supported Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established mechanism

Action Mode:
Diagnostic

---

# 66. CPU Core Parking Benchmark

Status:
RESEARCHING

Type:
Benchmark

Technical Domain:
CPU / Energy

Primary Product Area:
Benchmark

Possible Consumers:
- Gaming
- Energy

Final UI Placement:
TBD

Descrição:
Comparar workload com diferentes políticas de parking apenas quando aplicável.

Possível objetivo:
Verificar se existe benefício real.

Possível benefício:
Substituir tweak dogmático por teste A/B.

Aplicabilidade:
Hardware/workload dependent

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Measurement-based

Action Mode:
Benchmark First

---

# 67. CPU Scheduling Regression Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Gaming
- Windows Update
- Drivers

Final UI Placement:
TBD

Descrição:
Detectar alteração significativa de comportamento de scheduling depois de update/driver/configuração.

Possível objetivo:
Relacionar regressões a mudanças recentes.

Possível benefício:
Troubleshooting.

Aplicabilidade:
Affected PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Diagnostic

---

# 68. CPU Performance Before × After

Status:
CANDIDATE

Type:
Benchmark / Validation

Technical Domain:
CPU

Primary Product Area:
Benchmark

Possible Consumers:
- Optimization
- Energy

Final UI Placement:
TBD

Descrição:
Comparar métricas da CPU antes e depois de qualquer alteração relevante.

Possível objetivo:
Validar benefício.

Possível benefício:
Evitar manter tweak sem resultado.

Aplicabilidade:
Optimization Sessions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement principle

Action Mode:
Automatic Validation

Possíveis métricas:
- single-thread;
- multi-thread;
- sustained throughput;
- effective clock;
- temperature;
- power;
- variability.

---

# 69. CPU Performance Baseline vs Expected Hardware

Status:
IDEA

Type:
Diagnostic

Technical Domain:
CPU Benchmark

Primary Product Area:
My PC

Possible Consumers:
- Benchmark
- Optimization

Final UI Placement:
TBD

Descrição:
Comparar desempenho observado com faixa esperada para processador equivalente.

Possível objetivo:
Detectar CPU significativamente abaixo do comportamento típico.

Possível benefício:
Revelar problema térmico, power policy ou configuração.

Aplicabilidade:
Hardware with Reference Data

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Requires reliable comparison dataset

Action Mode:
Diagnostic

Dúvidas:
- Precisaremos de base própria?
- Resultados devem considerar RAM, cooling, BIOS e motherboard.

---

# 70. CPU Performance Limiter Classification

Status:
IDEA

Type:
Diagnostic

Technical Domain:
CPU

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Optimization

Final UI Placement:
TBD

Descrição:
Classificar provável razão para desempenho abaixo do esperado.

Possível objetivo:
Responder “por que minha CPU está lenta?”

Possível benefício:
Diagnóstico acionável.

Aplicabilidade:
Affected PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Correlation-based

Action Mode:
Diagnostic

Possíveis classes:
- thermal limited;
- power limited;
- utilization limited;
- thread-bound workload;
- background interference;
- energy-policy limited;
- scheduler anomaly;
- unknown.

---

# 71. CPU Firmware / Microcode Context Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
CPU / Firmware

Primary Product Area:
My PC

Possible Consumers:
- Drivers
- BIOS
- Stability

Final UI Placement:
TBD

Descrição:
Investigar capacidade de identificar revisão de microcode e contexto de firmware.

Possível objetivo:
Auxiliar análise de problemas corrigidos por BIOS/Windows updates.

Possível benefício:
Troubleshooting avançado.

Aplicabilidade:
Hardware-dependent

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Platform-dependent

Action Mode:
Diagnostic

Dúvidas:
- Qual informação é exposta de forma estável?
- Como mapear microcode para versões corrigidas sem base externa enorme?

---

# 72. CPU Firmware Update Recommendation

Status:
IDEA

Type:
Diagnostic / Recommendation

Technical Domain:
CPU / BIOS

Primary Product Area:
My PC

Possible Consumers:
- BIOS
- Stability

Final UI Placement:
TBD

Descrição:
Relacionar determinadas condições da CPU a BIOS/firmware potencialmente desatualizado.

Possível objetivo:
Direcionar usuário ao fabricante quando atualização pode corrigir estabilidade/desempenho.

Possível benefício:
Solucionar problemas que tweaks do Windows não podem corrigir.

Aplicabilidade:
Specific Hardware

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Requires vendor data

Action Mode:
Manual Guidance

---

# 73. CPU Security Mitigation Context

Status:
IDEA

Type:
Diagnostic

Technical Domain:
CPU / Security

Primary Product Area:
Shared

Possible Consumers:
- Security
- Benchmark

Final UI Placement:
TBD

Descrição:
Reconhecer que determinadas proteções contra vulnerabilidades de CPU podem afetar comportamento/performance em certos workloads.

Possível objetivo:
Evitar atribuir toda diferença de performance a configuração errada.

Possível benefício:
Contextualização técnica.

Aplicabilidade:
Specific CPUs / Windows Builds

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept

Action Mode:
Diagnostic Only

Importante:
Não transformar “desativar mitigations” em otimização padrão.

---

# 74. Hypervisor Presence Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / Virtualization

Primary Product Area:
Shared

Possible Consumers:
- CPU
- Security
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar se o Windows está operando com hypervisor ativo.

Possível objetivo:
Dar contexto a VBS, Hyper-V e comportamento de virtualização.

Possível benefício:
Interpretar benchmarks e disponibilidade de recursos corretamente.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

---

# 75. Hypervisor Scheduler Context

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Virtualization / CPU Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Virtualization
- Benchmark
- Security

Final UI Placement:
TBD

Descrição:
Considerar scheduler do hypervisor ao analisar máquinas com virtualização ativa.

Possível objetivo:
Evitar tratar scheduling do host como se não houvesse camada de virtualização.

Possível benefício:
Diagnóstico avançado.

Aplicabilidade:
Hyper-V Hosts

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 76. CPU “Use All Cores” Myth Guard

Status:
CANDIDATE

Type:
Safety / Product Behavior

Technical Domain:
CPU

Primary Product Area:
Shared

Possible Consumers:
- Optimization

Final UI Placement:
TBD

Descrição:
Impedir recomendações do tipo “habilitar todos os núcleos pelo msconfig”.

Possível objetivo:
Evitar tweak baseado em interpretação errada.

Possível benefício:
Preservar configuração correta do Windows.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Internal Safeguard

Observação:
Windows já utiliza os processadores disponíveis; opções de boot podem limitar, não “desbloquear”, núcleos.

---

# 77. Scheduler Registry Tweak Guard

Status:
CANDIDATE

Type:
Safety / Research

Technical Domain:
CPU Scheduler

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Latency
- Gaming

Final UI Placement:
TBD

Descrição:
Tratar alterações como `Win32PrioritySeparation` e valores semelhantes como hipóteses até demonstração técnica.

Possível objetivo:
Evitar números mágicos de scheduler.

Possível benefício:
Reduzir placebo e regressão.

Aplicabilidade:
Application-wide

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Unverified generic benefit

Action Mode:
Research / Benchmark First

---

# 78. CPU Affinity Myth Guard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
Scheduler

Primary Product Area:
Shared

Possible Consumers:
- Gaming
- Optimization

Final UI Placement:
TBD

Descrição:
Evitar recomendação genérica de restringir jogos ou aplicações a subconjuntos arbitrários de cores.

Possível objetivo:
Preservar liberdade do scheduler quando não existe evidência de problema.

Possível benefício:
Evitar perda de desempenho.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established principle

Action Mode:
Internal Safeguard

---

# 79. Priority Tweak Myth Guard

Status:
CANDIDATE

Type:
Safety

Technical Domain:
Scheduler

Primary Product Area:
Shared

Possible Consumers:
- Gaming
- Processes

Final UI Placement:
TBD

Descrição:
Impedir a suposição de que elevar todos os jogos para High/Realtime gera desempenho gratuito.

Possível objetivo:
Evitar starvation e comportamento imprevisível.

Possível benefício:
Estabilidade.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established scheduler behavior

Action Mode:
Internal

---

# 80. CPU Diagnostic Summary

Status:
CANDIDATE

Type:
Diagnostic / Product Behavior

Technical Domain:
CPU

Primary Product Area:
My PC

Possible Consumers:
- Optimization
- Benchmark

Final UI Placement:
TBD

Descrição:
Consolidar estado geral da CPU em uma explicação compreensível.

Possível objetivo:
Transformar dezenas de métricas em poucas conclusões úteis.

Possível benefício:
Melhor experiência.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product concept

Action Mode:
Automatic

Possíveis resultados:

CPU:
Normal

ou:

CPU:
Performance abaixo do esperado

Possible reason:
Thermal throttling detected.

ou:

CPU:
Single-thread bottleneck detected during game session.

---

# Consolidações importantes

## Core Parking

Já apareceu profundamente em Energia.

Em CPU, manter apenas:
- observation;
- topology relationship;
- workload benchmark;
- scheduler context.

Não duplicar toda a configuração de PPM.

---

## EPP / CPPC / Boost

Configuração fica principalmente em Energia.

CPU consome essas capacidades para:
- diagnóstico;
- performance analysis;
- throttling;
- benchmark.

---

## DPC / ISR

CPU deve medir o consumo e reconhecer anomalias.

Investigação profunda de driver/interrupt affinity fica para área DPC/Latency.

---

## Affinity / CPU Sets

Ambos são mecanismos reais, mas conceitualmente diferentes.

Affinity:
restrição tradicional e mais rígida.

CPU Sets:
mecanismo mais moderno para controlar preferência/disponibilidade de CPUs.

Devem permanecer separados no discovery.

---

# Itens clássicos que NÃO devem ser otimizações automáticas

## “Usar todos os cores”

Rejeitar como tweak.

---

## Desativar Core 0 para jogos

Não assumir.

---

## Usar somente P-Cores para todo jogo

Não assumir.

Alguns workloads podem se beneficiar; outros podem perder throughput ou comportamento correto.

---

## Desativar E-Cores

Não como padrão.

Requer BIOS e altera significativamente topologia/capacidade.

---

## SMT OFF

Não como padrão.

Resultado depende do workload.

---

## High Priority para jogos

Não como padrão.

Aumenta preferência relativa, não capacidade da CPU.

---

## Realtime Priority

Evitar para otimização comum.

---

## CPU Affinity fixa

Não como padrão.

O scheduler possui mais contexto dinâmico que uma regra estática.

---

## `Win32PrioritySeparation = valor mágico`

Não.

Somente pesquisar se surgir caso concreto e mensurável.

---

## “CPU usage menor = melhor”

Não.

Pode simplesmente significar workload limitado em outro ponto.

---

## “100% CPU = sempre problema”

Não.

Durante workload multi-thread pesado pode ser exatamente o comportamento esperado.

---

# Princípios que emergem da área CPU

1. CPU total não conta toda a história.

2. Saturação de um thread pode limitar workload com utilização global baixa.

3. Physical cores e logical processors precisam ser distinguidos.

4. CPUs heterogêneas exigem conhecimento de classes de núcleo.

5. Scheduler moderno deve ser observado antes de ser alterado.

6. CPU affinity não é uma otimização universal.

7. CPU Sets e affinity são mecanismos diferentes.

8. Desde Windows 11, processor groups são tratados de forma mais transparente pelo sistema.

9. Frequência reportada não é necessariamente frequência efetiva.

10. Boost máximo anunciado não precisa aparecer simultaneamente em todos os cores.

11. Performance deve ser analisada junto com temperatura e potência.

12. Thermal throttling e power throttling são problemas diferentes.

13. CPU “ocupada” e CPU realizando trabalho efetivo são conceitos diferentes.

14. Context switches, interrupts e DPC devem ser usados como evidência contextual.

15. Performance sustentada é diferente de burst performance.

16. Benchmarks single-thread e multi-thread respondem perguntas diferentes.

17. SMT é workload-dependent.

18. Configurações de scheduler devem exigir evidência muito forte.

19. Registry tweaks de scheduler não devem ser confiados sem validação.

20. O melhor resultado pode ser concluir que a CPU já está se comportando corretamente.

---

# Questões para segunda passada

- Como detectar corretamente P-core/E-core em Intel e equivalentes em outras arquiteturas?
- `EfficiencyClass` do Windows é suficiente?
- Quais informações modernas `GetSystemCpuSetInformation` fornece?
- Podemos observar CPU Sets usados por processos de terceiros?
- Qual relação prática entre CPU Sets e Thread Director?
- O Windows expõe preferred/ideal processor de threads de maneira útil?
- Devemos analisar thread migration?
- Migração excessiva entre P/E cores pode ser detectada?
- Existe sinal confiável de scheduler anomaly?
- Como diferenciar logical processor utilization de real execution capacity?
- Qual API é melhor para clocks?
- WMI é insuficiente para clocks dinâmicos?
- Performance counters são adequados somente para baixa frequência?
- ETW deve alimentar gráficos rápidos?
- Podemos usar PMU diretamente?
- Hardware counter profiling exige administrador?
- Como coletar IPC de Intel/AMD/ARM de maneira portável?
- Podemos detectar L3 cache topology corretamente em CPUs chiplet?
- Como tratar AMD CCD/CCX?
- Como detectar preferred cores?
- CPPC preferred core information é acessível?
- Ryzen X3D com múltiplos CCDs merece lógica específica?
- Como detectar cache-vs-frequency CCD scheduling?
- Intel Thread Director expõe alguma informação útil ao user space?
- Windows Game Mode influencia scheduling heterogêneo?
- EcoQoS influencia escolha P-core/E-core?
- Como avaliar jogos com anti-cheat sem instrumentação invasiva?
- Podemos identificar main game thread?
- Como detectar thread-bound gameplay?
- Qual relação entre processor queue e CPUs modernas com muitos cores?
- Thresholds fixos de queue são ruins em high-core-count systems?
- Como medir thermal throttling em Intel, AMD e ARM?
- Como medir power limits de maneira vendor-neutral?
- PL1/PL2/Tau devem ser diagnosticados?
- PPT/TDC/EDC devem ser diagnosticados?
- Essas métricas pertencem a CPU ou BIOS/tuning?
- Podemos detectar motherboard impondo power limits abaixo do esperado?
- Devemos detectar Intel baseline/default profiles?
- Devemos identificar AMD Eco Mode/PBO?
- Overclock/undervolt pertence a CPU ou Advanced Tuning?
- Como diferenciar tuning permitido de risco?
- O BeadWise deveria oferecer stability testing de CPU?
- Devemos detectar WHEA durante benchmark automaticamente?
- Como avaliar clock stretching?
- Como detectar CPU frequency residency?
- Como medir C-state residency?
- Qual profundidade de CPU-Z devemos reproduzir em My PC?
- Vale mostrar raw CPUID para Advanced mode?
- Devemos comparar benchmark com processador equivalente online?
- Como normalizar comparação por RAM/cooling/power limit?
- Podemos criar score sem virar benchmark artificial?
- Quais workloads representam uso real?

---

# Avaliação inicial

A primeira passada encontrou aproximadamente 80 capacidades, subcapacidades e princípios.

Os blocos mais fortes são:

## Hardware / topology
- identificação;
- cores/threads;
- SMT;
- heterogeneous cores;
- cache;
- NUMA;
- processor groups.

## Runtime monitoring
- utilization;
- per-core utilization;
- clocks;
- effective clocks;
- temperatures;
- power.

## Performance diagnostics
- bottleneck;
- single-thread saturation;
- queue;
- kernel/user time;
- DPC/interrupts;
- throttling.

## Scheduler
- heterogeneous scheduling;
- affinity;
- CPU Sets;
- priorities;
- ETW tracing.

## Benchmark
- single-thread;
- multi-thread;
- scaling;
- sustained;
- performance-per-watt;
- before/after.

## Advanced
- PMU;
- IPC;
- cache misses;
- firmware context.

## Myth guards
- “use all cores”;
- Core 0;
- affinity;
- priority;
- scheduler Registry tweaks.

Estado:

FIRST PASS COMPLETE

Próximo passo:

SECOND PASS — CPU GAP AUDIT

# Discovery Catalog — CPU
## Segunda passada — auditoria de lacunas

> Este bloco complementa os itens 1–80 da primeira passada de CPU.
>
> A segunda investigação concentra-se em:
>
> - arquiteturas híbridas;
> - preferred cores;
> - AMD CCD/CCX/X3D;
> - Intel power limits;
> - AMD PPT/TDC/EDC;
> - estabilidade;
> - WHEA;
> - clock stretching;
> - hardware performance counters;
> - profiling avançado.
>
> Regra central:
>
> **Telemetria disponível não implica que o BeadWise deva alterar o parâmetro correspondente.**

---

# 81. CPU Vendor-Specific Capability Layer

Status:
CANDIDATE

Type:
Shared Capability

Technical Domain:
CPU

Primary Product Area:
Shared

Possible Consumers:
- CPU
- Benchmark
- Monitoring
- Energy
- BIOS

Final UI Placement:
TBD

Descrição:
Permitir que capacidades genéricas de CPU recebam extensões específicas para Intel, AMD e outras arquiteturas quando necessário.

Possível objetivo:
Evitar reduzir todos os processadores ao menor denominador comum.

Possível benefício:
Maior profundidade de diagnóstico sem comprometer suporte genérico.

Aplicabilidade:
Application-wide

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product architecture concept

Action Mode:
Internal

Exemplo:
Generic:
CPU Power Limit Detected

AMD:
PPT / TDC / EDC

Intel:
PL1 / PL2 / Tau

---

# 82. AMD CCD Topology Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
AMD CPU Topology

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Gaming
- RAM

Final UI Placement:
TBD

Descrição:
Detectar organização da CPU AMD em CCDs quando aplicável.

Possível objetivo:
Compreender topologias chiplet que não podem ser representadas apenas como cores/threads.

Possível benefício:
Base para análise de cache, scheduling e desempenho entre CCDs.

Aplicabilidade:
Supported AMD CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Hardware-dependent

Action Mode:
Diagnostic

Possíveis informações:
- number of CCDs;
- active CCDs;
- cores per CCD;
- cache relationships.

Dúvidas:
- Qual interface fornece topologia de CCD de forma suportada?
- CPUID será suficiente em todas as gerações?

---

# 83. AMD CCX Topology Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
AMD CPU Topology

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Advanced Tools

Final UI Placement:
TBD

Descrição:
Investigar identificação de CCXs quando essa estrutura permanecer relevante para a arquitetura específica.

Possível objetivo:
Dar contexto a cache/locality.

Possível benefício:
Análise avançada de topologia.

Aplicabilidade:
Specific AMD Generations

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Architecture-dependent

Action Mode:
Diagnostic

Importante:
Não assumir que toda geração Ryzen possui a mesma organização CCD/CCX.

---

# 84. AMD 3D V-Cache Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
AMD CPU / Cache

Primary Product Area:
Shared

Possible Consumers:
- CPU
- Gaming
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar processadores com 3D V-Cache e características relevantes da topologia.

Possível objetivo:
Permitir diagnósticos específicos de CPUs X3D.

Possível benefício:
Melhor compreensão de cache e scheduling em modelos onde CCDs podem possuir características diferentes.

Aplicabilidade:
AMD X3D CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established hardware capability

Action Mode:
Automatic Diagnostic

---

# 85. AMD X3D Scheduling Support Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
AMD CPU / Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Gaming
- Drivers
- Optimization

Final UI Placement:
TBD

Descrição:
Verificar presença dos componentes de chipset relevantes para comportamento de scheduling em plataformas X3D compatíveis.

Possível objetivo:
Detectar instalação incompleta ou inadequada de drivers necessários ao funcionamento esperado da plataforma.

Possível benefício:
Corrigir desempenho sem aplicar tweaks manuais de affinity.

Aplicabilidade:
Relevant AMD X3D Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established vendor software

Action Mode:
Diagnostic

Possíveis componentes:
- AMD PPM Provisioning driver;
- AMD 3D V-Cache Performance Optimizer driver.

Dúvidas:
- Quais CPUs realmente necessitam de cada componente?
- Como comportamento mudou entre gerações?

---

# 86. X3D Scheduling Health

Status:
IDEA

Type:
Diagnostic

Technical Domain:
AMD CPU / Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Gaming
- Benchmark

Final UI Placement:
TBD

Descrição:
Investigar se jogos/workloads estão sendo direcionados de forma coerente em CPUs X3D multi-CCD.

Possível objetivo:
Encontrar problemas de driver ou classificação do workload sem impor affinity fixa.

Possível benefício:
Diagnóstico de desempenho específico para X3D.

Aplicabilidade:
Multi-CCD X3D Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Vendor/platform dependent

Action Mode:
Diagnostic First

Importante:
Não substituir automaticamente o mecanismo oficial AMD por pinning manual de cores.

---

# 87. CCD Workload Distribution Analysis

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
AMD CPU / Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Gaming
- Benchmark

Final UI Placement:
TBD

Descrição:
Observar distribuição de threads entre CCDs durante workload.

Possível objetivo:
Identificar comportamento de migração/locality potencialmente relevante.

Possível benefício:
Troubleshooting avançado.

Aplicabilidade:
Multi-CCD AMD CPUs

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Diagnostic

Dúvidas:
- Como distinguir migração normal de problema?
- Qual resolução de tracing é necessária?

---

# 88. Preferred / Favored Core Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
CPU / Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Gaming
- Energy

Final UI Placement:
TBD

Descrição:
Investigar identificação de cores considerados mais capazes pelo hardware/firmware.

Possível objetivo:
Compreender escolhas do scheduler e comportamento de boost single-thread.

Possível benefício:
Explicar por que determinados cores recebem threads prioritárias.

Aplicabilidade:
Supported CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Platform-dependent

Action Mode:
Diagnostic

Importante:
“Melhor core” não significa que o BeadWise deva fixar aplicações nele.

---

# 89. Preferred Core Scheduling Observation

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Gaming

Final UI Placement:
TBD

Descrição:
Observar se workloads leves/single-thread utilizam cores favorecidos quando esperado.

Possível objetivo:
Investigar boost ou scheduling anormal.

Possível benefício:
Diagnóstico sem intervenção.

Aplicabilidade:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent

Action Mode:
Diagnostic

---

# 90. Thread Migration Analysis

Status:
CANDIDATE

Type:
Advanced Diagnostic

Technical Domain:
Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Gaming
- Latency
- Benchmark

Final UI Placement:
TBD

Descrição:
Analisar migração de threads entre logical processors ao longo do tempo.

Possível objetivo:
Dar contexto a cache locality e scheduling heterogêneo.

Possível benefício:
Troubleshooting de workloads sensíveis.

Aplicabilidade:
Advanced Scenarios

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established scheduling phenomenon

Action Mode:
Diagnostic

---

# 91. Cross-Core-Class Migration Analysis

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
Hybrid Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Gaming
- Benchmark

Final UI Placement:
TBD

Descrição:
Observar migração entre classes distintas de processador em CPUs heterogêneas.

Possível objetivo:
Investigar thread alternando entre cores de performance e eficiência.

Possível benefício:
Detectar comportamento potencialmente relacionado a variabilidade de performance.

Aplicabilidade:
Hybrid CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Diagnostic

Importante:
Migração não significa automaticamente erro.

---

# 92. CPU Ideal Processor Observation

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
Scheduler

Primary Product Area:
Tools

Possible Consumers:
- CPU
- Processes

Final UI Placement:
TBD

Descrição:
Investigar preferência de processor associada às threads quando disponível.

Possível objetivo:
Compreender decisões do dispatcher além de affinity.

Possível benefício:
Troubleshooting avançado.

Aplicabilidade:
Advanced

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established scheduler concept

Action Mode:
Diagnostic

---

# 93. Intel Hybrid Scheduling Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Intel CPU / Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Gaming
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Detectar plataformas Intel híbridas e capacidades expostas ao Windows para classificar processadores.

Possível objetivo:
Permitir análise adequada de P-cores/E-cores.

Possível benefício:
Evitar heurísticas baseadas apenas em core ID.

Aplicabilidade:
Intel Hybrid CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established platform capability

Action Mode:
Automatic

---

# 94. Hardware-Guided Scheduling Context

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
Intel / Scheduler

Primary Product Area:
CPU

Possible Consumers:
- Gaming
- Energy

Final UI Placement:
TBD

Descrição:
Investigar informações que hardware moderno fornece ao Windows para orientar decisões de scheduling.

Possível objetivo:
Compreender até onde o próprio hardware/firmware já orienta thread placement.

Possível benefício:
Evitar substituir mecanismos sofisticados por afinidade fixa.

Aplicabilidade:
Supported Hybrid CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept / Public observability requires research

Action Mode:
Diagnostic / Research

---

# 95. CPU Scheduling Intervention Threshold

Status:
CANDIDATE

Type:
Product Behavior

Technical Domain:
Scheduler

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Gaming

Final UI Placement:
TBD

Descrição:
Definir princípio de que qualquer intervenção de scheduling exige evidência de comportamento ruim e benefício mensurável.

Possível objetivo:
Impedir transformação de ferramentas avançadas em tweaks padrão.

Possível benefício:
Estabilidade e redução de placebo.

Aplicabilidade:
Application-wide

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product principle

Action Mode:
Internal Safeguard

Regra conceitual:

Observe
→ detect anomaly
→ reproduce
→ benchmark alternative
→ only then consider change.

---

# 96. Intel Package Power Limit Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Intel CPU / Power

Primary Product Area:
CPU

Possible Consumers:
- Energy
- Benchmark
- Cooling

Final UI Placement:
TBD

Descrição:
Detectar limites de potência relevantes em processadores Intel quando telemetria suportada estiver disponível.

Possível objetivo:
Explicar desempenho sustentado limitado por power policy.

Possível benefício:
Distinguir CPU lenta de plataforma configurada para determinado envelope de potência.

Aplicabilidade:
Supported Intel CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established hardware mechanism

Action Mode:
Diagnostic

Possíveis parâmetros:
- PL1;
- PL2;
- PL3 quando relevante;
- PL4 quando relevante;
- Tau.

---

# 97. Intel PL1 Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Intel CPU / Power

Primary Product Area:
CPU

Possible Consumers:
- Energy
- Benchmark

Final UI Placement:
TBD

Descrição:
Interpretar limite médio/sustentado de package power.

Possível objetivo:
Relacionar desempenho sustentado ao envelope térmico da plataforma.

Possível benefício:
Diagnosticar redução de frequência após burst.

Aplicabilidade:
Supported Intel CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 98. Intel PL2 Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Intel CPU / Power

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Interpretar limite de potência de curta duração/turbo.

Possível objetivo:
Dar contexto ao desempenho burst.

Possível benefício:
Explicar diferenças entre short benchmark e sustained benchmark.

Aplicabilidade:
Supported Intel CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 99. Intel Turbo Time Window Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Intel CPU / Power

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Cooling

Final UI Placement:
TBD

Descrição:
Considerar Tau e comportamento temporal de package power ao analisar turbo.

Possível objetivo:
Explicar por que frequência inicial não é sustentada indefinidamente.

Possível benefício:
Análise correta de benchmarks.

Aplicabilidade:
Supported Intel Platforms

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established mechanism

Action Mode:
Diagnostic

---

# 100. Intel Platform Power Constraint Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Intel CPU / Platform

Primary Product Area:
CPU

Possible Consumers:
- Laptop
- Benchmark
- Cooling

Final UI Placement:
TBD

Descrição:
Detectar quando limites da plataforma parecem inferiores à capacidade nominal potencial da CPU.

Possível objetivo:
Diferenciar comportamento esperado de laptop/OEM de problema de CPU.

Possível benefício:
Evitar recomendar tweak quando limite é intencional do sistema térmico.

Aplicabilidade:
OEM / Laptop Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Platform-dependent

Action Mode:
Diagnostic

---

# 101. Intel Dynamic Tuning Context

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Intel CPU / OEM Power

Primary Product Area:
Shared

Possible Consumers:
- CPU
- Energy
- Laptop

Final UI Placement:
TBD

Descrição:
Reconhecer que Intel Dynamic Tuning pode alterar dinamicamente energia e desempenho conforme condições da plataforma.

Possível objetivo:
Evitar concluir que limites diferentes do esperado são necessariamente configuração errada.

Possível benefício:
Melhor suporte a notebooks/OEM.

Aplicabilidade:
Supported Platforms

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 102. Intel Power Limit Modification

Status:
QUESTIONABLE

Type:
Advanced Tuning

Technical Domain:
Intel CPU

Primary Product Area:
Advanced / TBD

Possible Consumers:
- Benchmark
- Enthusiast Tools

Final UI Placement:
TBD

Descrição:
Investigar tecnicamente possibilidade de modificar PL1/PL2/Tau.

Possível objetivo:
Conhecer capacidade existente.

Possível benefício:
Possível alteração de desempenho sustentado.

Aplicabilidade:
Unlocked/Supported Platforms

Shared Capability:
No

Risco inicial:
High

Evidence Level:
Mechanism established / Benefit and safety platform-specific

Action Mode:
Manual Guidance / Advanced Experimental

Importante:
Não pertence à otimização automática.

Modificar esses limites pode exceder capacidade térmica ou de entrega de energia da plataforma.

---

# 103. AMD PPT Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
AMD CPU / Power

Primary Product Area:
CPU

Possible Consumers:
- Energy
- Benchmark
- Cooling

Final UI Placement:
TBD

Descrição:
Detectar uso relativo ou limite PPT quando a plataforma expuser telemetria.

Possível objetivo:
Determinar se CPU está limitada pelo orçamento de socket power.

Possível benefício:
Diagnóstico de power-limited performance.

Aplicabilidade:
Supported AMD CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established vendor metric

Action Mode:
Diagnostic

---

# 104. AMD TDC Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
AMD CPU / Power

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Cooling

Final UI Placement:
TBD

Descrição:
Monitorar relação com limite de corrente sustentada.

Possível objetivo:
Identificar restrição elétrica durante workloads prolongados.

Possível benefício:
Melhor classificação de limiter.

Aplicabilidade:
Supported AMD CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 105. AMD EDC Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
AMD CPU / Power

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Cooling

Final UI Placement:
TBD

Descrição:
Monitorar relação com limite de corrente de pico.

Possível objetivo:
Identificar restrição elétrica em cargas transientes.

Possível benefício:
Diagnóstico detalhado de boost.

Aplicabilidade:
Supported AMD CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 106. AMD Limiter Classification

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
AMD CPU

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Optimization

Final UI Placement:
TBD

Descrição:
Combinar PPT, TDC, EDC, temperatura e frequência para explicar provável limitação ativa.

Possível objetivo:
Responder por que Precision Boost deixou de aumentar frequência.

Possível benefício:
Diagnóstico acionável.

Aplicabilidade:
Supported AMD CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Vendor telemetry based

Action Mode:
Diagnostic

Possíveis resultados:
- thermal constrained;
- PPT constrained;
- TDC constrained;
- EDC constrained;
- workload limited;
- no obvious limiter.

---

# 107. AMD Eco Mode Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
AMD CPU / Power

Primary Product Area:
CPU

Possible Consumers:
- Energy
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar uso de modo energético reduzido quando disponível.

Possível objetivo:
Explicar comportamento de consumo/desempenho.

Possível benefício:
Evitar confundir CPU em Eco Mode com problema de performance.

Aplicabilidade:
Supported AMD CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established vendor feature

Action Mode:
Diagnostic

---

# 108. AMD Precision Boost Overdrive State Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
AMD CPU / Tuning

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- BIOS
- Cooling

Final UI Placement:
TBD

Descrição:
Identificar estado de PBO quando possível.

Possível objetivo:
Dar contexto a limites e frequências acima dos parâmetros stock.

Possível benefício:
Diagnóstico correto de máquinas já modificadas.

Aplicabilidade:
Supported AMD CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 109. PBO Modification

Status:
QUESTIONABLE

Type:
Advanced Tuning

Technical Domain:
AMD CPU

Primary Product Area:
Advanced / TBD

Possible Consumers:
- Benchmark

Final UI Placement:
TBD

Descrição:
Registrar possibilidade conceitual de alterar PBO/PPT/TDC/EDC.

Possível objetivo:
Conhecer capacidade de tuning existente.

Possível benefício:
Possível aumento de desempenho sustentado ou ajuste de eficiência.

Aplicabilidade:
Supported AMD CPUs

Shared Capability:
No

Risco inicial:
High

Evidence Level:
Vendor-supported tuning / outcome hardware-specific

Action Mode:
Manual Guidance / Advanced Only

Importante:
Não deve integrar fluxo normal de Optimize.

---

# 110. CPU Tuning Mode Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / Tuning

Primary Product Area:
My PC

Possible Consumers:
- CPU
- Benchmark
- Stability

Final UI Placement:
TBD

Descrição:
Detectar indícios de CPU operando fora de configuração stock quando tecnicamente possível.

Possível objetivo:
Dar contexto a temperaturas, clocks, instabilidade e benchmark.

Possível benefício:
Evitar diagnosticar uma máquina modificada como stock.

Aplicabilidade:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Vendor-dependent

Action Mode:
Diagnostic

Possíveis estados conceituais:
- Default;
- OEM tuned;
- Eco;
- performance tuned;
- overclocked;
- undervolted;
- unknown.

---

# 111. CPU Stability Test

Status:
CANDIDATE

Type:
Benchmark / Diagnostic

Technical Domain:
CPU Stability

Primary Product Area:
Benchmark

Possible Consumers:
- CPU
- Cooling
- Repair

Final UI Placement:
TBD

Descrição:
Executar workload controlado destinado a identificar instabilidade da CPU/plataforma.

Possível objetivo:
Validar que desempenho não foi obtido às custas de erros.

Possível benefício:
Diferenciar benchmark alto de sistema realmente estável.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Established testing concept

Action Mode:
User-triggered

Possíveis componentes:
- multi-thread load;
- varying load;
- burst transitions;
- sustained load;
- optional memory-heavy CPU load.

---

# 112. WHEA Monitoring During CPU Test

Status:
CANDIDATE

Type:
Diagnostic / Validation

Technical Domain:
CPU / WHEA

Primary Product Area:
Shared

Possible Consumers:
- Benchmark
- Stability
- CPU

Final UI Placement:
TBD

Descrição:
Monitorar novos eventos WHEA durante benchmark ou stability test.

Possível objetivo:
Detectar erros de hardware mesmo quando o sistema não trava.

Possível benefício:
Validar estabilidade de maneira mais forte.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established Windows mechanism

Action Mode:
Automatic during tests

Possível resultado:
Test completed
Performance: +4.1%
WHEA corrected errors: 7

Conclusão:
Configuration should not be considered stable.

---

# 113. Corrected Hardware Error Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / WHEA

Primary Product Area:
Shared

Possible Consumers:
- CPU
- Stability
- My PC

Final UI Placement:
TBD

Descrição:
Detectar erros de hardware corrigidos registrados pelo Windows.

Possível objetivo:
Encontrar instabilidade que ainda não atingiu nível fatal.

Possível benefício:
Alertar antes de crashes mais graves.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Importante:
Erro WHEA não significa automaticamente que a CPU física está defeituosa; outras fontes incluem memória, PCIe e chipset.

---

# 114. CPU WHEA Source Classification

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
WHEA

Primary Product Area:
CPU

Possible Consumers:
- RAM
- PCIe
- Hardware Diagnostics

Final UI Placement:
TBD

Descrição:
Interpretar registros WHEA tentando identificar subsistema relacionado.

Possível objetivo:
Distinguir:
- processor;
- cache;
- memory;
- interconnect;
- PCIe;
- other hardware.

Possível benefício:
Direcionar investigação.

Aplicabilidade:
Systems with WHEA Events

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established record format / interpretation complexity

Action Mode:
Diagnostic

Importante:
Evitar declarar causa física definitiva quando registro não permitir.

---

# 115. Stability Regression Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
CPU Stability

Primary Product Area:
Benchmark

Possible Consumers:
- Optimization
- BIOS

Final UI Placement:
TBD

Descrição:
Comparar estabilidade antes/depois de alteração.

Possível objetivo:
Rejeitar uma otimização que cria novos erros.

Possível benefício:
Evitar trocar estabilidade por pontuação.

Aplicabilidade:
Optimization/Tuning Sessions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Automatic Validation

---

# 116. Clock Stretching Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
CPU Performance

Primary Product Area:
CPU

Possible Consumers:
- Benchmark
- Advanced Tuning

Final UI Placement:
TBD

Descrição:
Investigar situações em que clock reportado parece elevado, mas trabalho efetivo realizado é menor.

Possível objetivo:
Evitar interpretar frequência nominal/instantânea como performance real.

Possível benefício:
Especialmente útil em cenários de undervolt ou limitação interna.

Aplicabilidade:
Hardware-dependent

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Vendor/measurement dependent

Action Mode:
Diagnostic

Possíveis sinais:
- reported clock high;
- effective clock lower;
- performance unexpectedly low;
- increased clock without proportional workload throughput.

Dúvidas:
- Como detectar de forma vendor-neutral?
- Quais sensores fornecem effective clock confiável?

---

# 117. Reported Clock × Effective Clock Comparison

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU

Primary Product Area:
Monitoring

Possible Consumers:
- CPU
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Comparar frequência reportada a medida mais próxima possível de frequência efetiva.

Possível objetivo:
Detectar discrepâncias relevantes.

Possível benefício:
Melhor entendimento de boost, idle e throttling.

Aplicabilidade:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-source dependent

Action Mode:
Diagnostic

---

# 118. Frequency Residency Analysis

Status:
IDEA

Type:
Diagnostic

Technical Domain:
CPU / Power

Primary Product Area:
CPU

Possible Consumers:
- Energy
- Benchmark

Final UI Placement:
TBD

Descrição:
Medir quanto tempo CPU permanece em diferentes estados de desempenho.

Possível objetivo:
Compreender comportamento durante workload e idle.

Possível benefício:
Contexto melhor que amostras instantâneas.

Aplicabilidade:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept

Action Mode:
Diagnostic

---

# 119. CPU Performance State Transition Analysis

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
CPU / Power

Primary Product Area:
CPU

Possible Consumers:
- Energy
- Latency

Final UI Placement:
TBD

Descrição:
Analisar mudanças de estado de desempenho ao longo de workload.

Possível objetivo:
Investigar responsividade de boost/downclock.

Possível benefício:
Troubleshooting avançado de performance.

Aplicabilidade:
Advanced

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Diagnostic

---

# 120. PMU Capability Enumeration

Status:
CANDIDATE

Type:
Advanced Diagnostic

Technical Domain:
CPU PMU

Primary Product Area:
Shared

Possible Consumers:
- CPU
- Benchmark
- Advanced Tools

Final UI Placement:
TBD

Descrição:
Enumerar eventos de Performance Monitoring Unit que o Windows realmente suporta na máquina.

Possível objetivo:
Evitar hardcode de eventos específicos de uma arquitetura.

Possível benefício:
Criar profiling adaptativo.

Aplicabilidade:
Supported CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Possível mecanismo:
Windows Performance Recorder / hardware counter facilities.

---

# 121. Instructions Retired Measurement

Status:
CANDIDATE

Type:
Advanced Diagnostic

Technical Domain:
PMU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU

Final UI Placement:
TBD

Descrição:
Medir quantidade de instruções concluídas durante determinado workload quando PMU suportar.

Possível objetivo:
Base para IPC e eficiência de execução.

Possível benefício:
Profiling avançado.

Aplicabilidade:
Supported PMU

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established hardware event

Action Mode:
Advanced Benchmark

---

# 122. CPU Cycle Measurement

Status:
CANDIDATE

Type:
Advanced Diagnostic

Technical Domain:
PMU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU

Final UI Placement:
TBD

Descrição:
Medir ciclos de CPU durante workload.

Possível objetivo:
Permitir métricas derivadas como IPC.

Possível benefício:
Análise microarquitetural.

Aplicabilidade:
Supported PMU

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Advanced Benchmark

---

# 123. IPC Analysis

Status:
CANDIDATE

Type:
Advanced Diagnostic

Technical Domain:
CPU PMU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU
- RAM

Final UI Placement:
TBD

Descrição:
Calcular Instructions Per Cycle quando fontes confiáveis estiverem disponíveis.

Possível objetivo:
Diferenciar frequência elevada de execução eficiente.

Possível benefício:
Diagnosticar workload memory-bound, cache-bound ou pipeline-limited.

Aplicabilidade:
Advanced Benchmark

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Advanced

---

# 124. PMU Sampling Profiling

Status:
IDEA

Type:
Advanced Diagnostic Tool

Technical Domain:
PMU / ETW

Primary Product Area:
Tools

Possible Consumers:
- CPU
- Benchmark

Final UI Placement:
TBD

Descrição:
Usar overflow/sample de hardware counters para identificar regiões de execução associadas a eventos específicos.

Possível objetivo:
Profiling semelhante a ferramentas de performance profissionais.

Possível benefício:
Investigação extremamente profunda.

Aplicabilidade:
Advanced Users

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established Windows capability

Action Mode:
Advanced Tool

Dúvidas:
Pode ser complexo demais para o escopo principal do produto.

---

# 125. PMU + Context Switch Correlation

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
PMU / ETW

Primary Product Area:
Tools

Possible Consumers:
- CPU
- Scheduler

Final UI Placement:
TBD

Descrição:
Associar valores de hardware counters a eventos ETW como context switches.

Possível objetivo:
Entender característica do workload por thread/processo.

Possível benefício:
Profiling avançado sem instrumentar diretamente a aplicação.

Aplicabilidade:
Advanced

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established Windows capability

Action Mode:
Advanced Diagnostic

---

# 126. Cache Performance Counter Analysis

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
CPU Cache / PMU

Primary Product Area:
Tools

Possible Consumers:
- RAM
- Benchmark

Final UI Placement:
TBD

Descrição:
Investigar cache-related PMU events disponíveis para plataforma.

Possível objetivo:
Identificar workloads com alta pressão de cache.

Possível benefício:
Diferenciar gargalo de compute de gargalo de memória/cache.

Aplicabilidade:
Advanced

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Vendor-dependent

Action Mode:
Advanced Diagnostic

---

# 127. Hardware Counter Portability Guard

Status:
CANDIDATE

Type:
Safety / Architecture

Technical Domain:
PMU

Primary Product Area:
Shared

Possible Consumers:
- Benchmark

Final UI Placement:
TBD

Descrição:
Assumir explicitamente que eventos PMU variam por fabricante e microarquitetura.

Possível objetivo:
Evitar benchmarks que dependam de um contador inexistente em outra CPU.

Possível benefício:
Portabilidade.

Aplicabilidade:
Application-wide

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Internal Safeguard

---

# 128. CPU Performance Source Selection

Status:
CANDIDATE

Type:
Shared Capability

Technical Domain:
CPU Telemetry

Primary Product Area:
Shared

Possible Consumers:
- Monitoring
- Benchmark
- CPU

Final UI Placement:
TBD

Descrição:
Selecionar fonte de telemetria apropriada conforme frequência e finalidade.

Possível objetivo:
Evitar usar Performance Counters para algo que exige profiling de alta frequência.

Possível benefício:
Menor overhead e maior precisão.

Aplicabilidade:
Application-wide

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established Windows guidance

Action Mode:
Internal

Possível regra conceitual:

Low-frequency administration:
Performance Counters

High-frequency profiling:
ETW / direct APIs / PMU

Sensor telemetry:
Hardware/vendor layer

---

# 129. CPU Telemetry Sampling Cost Analysis

Status:
IDEA

Type:
Diagnostic Infrastructure

Technical Domain:
Monitoring

Primary Product Area:
Shared

Possible Consumers:
- Overlay
- Benchmark
- Monitoring

Final UI Placement:
TBD

Descrição:
Medir overhead causado pela própria coleta de métricas.

Possível objetivo:
Garantir que BeadWise não prejudique o workload que tenta medir.

Possível benefício:
Maior confiança em FPS e benchmarks.

Aplicabilidade:
Application-wide

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement principle

Action Mode:
Internal Validation

---

# 130. CPU Monitoring Low-Overhead Mode

Status:
CANDIDATE

Type:
Monitoring

Technical Domain:
CPU Telemetry

Primary Product Area:
Monitoring

Possible Consumers:
- FPS Overlay
- Gaming

Final UI Placement:
TBD

Descrição:
Utilizar conjunto mínimo de métricas durante gaming quando overhead importar.

Possível objetivo:
Evitar coleta diagnóstica profunda durante gameplay normal.

Possível benefício:
Overlay mais leve.

Aplicabilidade:
Gaming Sessions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product design principle

Action Mode:
Automatic

---

# 131. CPU Deep Diagnostic Mode

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
CPU

Primary Product Area:
Tools

Possible Consumers:
- Repair
- Benchmark

Final UI Placement:
TBD

Descrição:
Ativar coleta mais profunda somente durante sessões de troubleshooting.

Possível objetivo:
Separar monitoramento cotidiano de profiling pesado.

Possível benefício:
Maior profundidade sem overhead permanente.

Aplicabilidade:
On-demand

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product concept

Action Mode:
User-triggered

---

# 132. CPU Benchmark Warm-Up

Status:
CANDIDATE

Type:
Benchmark Safeguard

Technical Domain:
CPU

Primary Product Area:
Benchmark

Possible Consumers:
- CPU

Final UI Placement:
TBD

Descrição:
Executar preparação curta antes de determinadas medições.

Possível objetivo:
Reduzir variação causada por estado inicial inconsistente.

Possível benefício:
Melhor repetibilidade.

Aplicabilidade:
Benchmark Sessions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement principle

Action Mode:
Automatic

---

# 133. CPU Benchmark Cooldown Control

Status:
CANDIDATE

Type:
Benchmark Safeguard

Technical Domain:
CPU / Thermal

Primary Product Area:
Benchmark

Possible Consumers:
- CPU
- Cooling

Final UI Placement:
TBD

Descrição:
Controlar temperatura inicial entre testes comparativos.

Possível objetivo:
Evitar que o segundo perfil seja injustamente comparado com CPU já aquecida.

Possível benefício:
Before × After mais confiável.

Aplicabilidade:
Benchmark Sessions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement principle

Action Mode:
Automatic

---

# 134. CPU Test Repetition / Variance

Status:
CANDIDATE

Type:
Benchmark

Technical Domain:
CPU

Primary Product Area:
Benchmark

Possible Consumers:
- Optimization

Final UI Placement:
TBD

Descrição:
Executar múltiplas repetições e medir variabilidade.

Possível objetivo:
Evitar concluir melhoria com diferença dentro do ruído.

Possível benefício:
Validação estatística básica.

Aplicabilidade:
Benchmark Sessions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement principle

Action Mode:
Automatic

---

# 135. Minimum Meaningful CPU Improvement

Status:
IDEA

Type:
Product Behavior

Technical Domain:
Benchmark

Primary Product Area:
Optimization

Possible Consumers:
- CPU

Final UI Placement:
TBD

Descrição:
Definir limiar de confiança para afirmar que uma alteração melhorou desempenho.

Possível objetivo:
Evitar celebrar +0,3% quando variação natural é maior.

Possível benefício:
Credibilidade.

Aplicabilidade:
Optimization Validation

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Statistical/measurement dependent

Action Mode:
Internal

Dúvidas:
- Usar confidence interval?
- Desvio entre runs?
- Threshold absoluto não deve ser universal.

---

# 136. CPU Optimization Rejection on Instability

Status:
CANDIDATE

Type:
Safety / Validation

Technical Domain:
CPU

Primary Product Area:
Optimization

Possible Consumers:
- Benchmark
- Stability

Final UI Placement:
TBD

Descrição:
Considerar qualquer alteração inválida caso aumente performance mas introduza erro de estabilidade.

Possível objetivo:
Priorizar funcionamento correto sobre benchmark.

Possível benefício:
Segurança.

Aplicabilidade:
All Optimization Sessions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product principle

Action Mode:
Automatic Safeguard

Exemplo:

Performance:
+5.2%

WHEA:
3 new corrected hardware errors

Result:
REJECT / ROLLBACK

---

# 137. CPU Optimization Rejection on Excessive Thermal Cost

Status:
IDEA

Type:
Validation

Technical Domain:
CPU / Thermal

Primary Product Area:
Optimization

Possible Consumers:
- Energy
- Benchmark

Final UI Placement:
TBD

Descrição:
Avaliar se pequeno ganho de desempenho exige aumento desproporcional de temperatura/potência.

Possível objetivo:
Evitar otimização tecnicamente pior.

Possível benefício:
Performance útil em vez de score máximo.

Aplicabilidade:
Optimization Sessions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product policy / measurement-based

Action Mode:
Contextual

---

# 138. CPU Optimization Multi-Metric Result

Status:
CANDIDATE

Type:
Product Behavior

Technical Domain:
CPU

Primary Product Area:
Optimization

Possible Consumers:
- Benchmark

Final UI Placement:
TBD

Descrição:
Avaliar alterações usando múltiplas dimensões.

Possível objetivo:
Não resumir tudo a um score.

Possível benefício:
Recomendação mais racional.

Possíveis dimensões:
- performance;
- latency;
- power;
- temperature;
- stability;
- variance.

Resultado conceitual:

Performance:
+2.1%

Power:
+18%

Temperature:
+11°C

Stability:
Stable

Classification:
Optional / Low-value tradeoff

---

# Consolidações com primeira passada

## Intel Thread Director

Não criar uma feature chamada simplesmente “Thread Director”.

O que importa para o BeadWise é:

- detectar CPU heterogênea;
- identificar classes de processador;
- observar scheduling;
- reconhecer hardware-guided scheduling;
- não substituir automaticamente decisões do sistema.

---

## Preferred cores

Não virar tweak de affinity.

Entram apenas como:
- diagnóstico;
- explicação de boost;
- scheduling context.

---

## AMD X3D

Não criar perfil manual “force cache CCD” por padrão.

Primeiro verificar:
- CPU;
- chipset driver;
- PPM components;
- 3D V-Cache optimizer;
- scheduling real;
- benchmark.

---

## PL1 / PL2 / PPT / TDC / EDC

São métricas excelentes para diagnóstico.

Modificá-las pertence a:
**Advanced Tuning**

e não:
**Optimize PC**

---

## PMU

Não transformar todos os contadores em features.

A capacidade real é:
**Hardware Counter Profiling**

As métricas específicas são ferramentas internas desse profiling.

---

# Itens deliberadamente NÃO recomendados como otimização automática

## Desativar E-cores

Não.

---

## Fixar jogo em P-cores

Não por padrão.

---

## Fixar jogo no CCD X3D

Não por padrão.

---

## Desativar segundo CCD

Não.

---

## Alterar PL1/PL2 para máximo da placa-mãe

Não.

---

## Remover limites PPT/TDC/EDC

Não.

---

## Ativar PBO automaticamente

Não.

---

## Aplicar Curve Optimizer automaticamente nesta área

Não.

Se for investigado futuramente, pertence a tuning avançado com stability validation muito mais rigorosa.

---

## Aumentar clock e considerar benchmark concluído

Não.

Performance nova precisa sobreviver a:
- repetição;
- temperatura;
- potência;
- WHEA;
- stability test.

---

# Princípios adicionais surgidos na segunda passada

1. A CPU deve ter camada genérica e extensões por fabricante.

2. Topologia moderna pode incluir mais que core/thread.

3. CCD/cache topology pode importar para scheduling e performance.

4. X3D exige compreensão de driver e scheduler antes de qualquer intervenção.

5. Preferred core é informação, não convite para affinity fixa.

6. Migração de thread deve ser observada antes de julgada.

7. Hardware moderno participa das decisões de scheduling.

8. PL1/PL2/Tau representam limites distintos.

9. PPT/TDC/EDC representam restrições distintas.

10. Power limiter precisa ser classificado, não apenas detectado como “throttling”.

11. OEM thermal/power policy pode ser comportamento intencional.

12. Detectar configuração modificada é diferente de recomendar modificação.

13. PBO e power limit tuning pertencem a uma categoria de risco superior.

14. Benchmark de CPU precisa monitorar estabilidade.

15. WHEA corrigido durante teste é informação relevante.

16. Ausência de crash não é prova suficiente de estabilidade.

17. Clock reportado e performance efetiva podem divergir.

18. PMU permite análise profunda, mas é hardware-specific.

19. ETW e PMU podem ser combinados.

20. Performance Counters não devem ser usados como solução universal de telemetria rápida.

21. O custo de monitoramento do próprio BeadWise precisa ser medido.

22. Overlay e Deep Diagnostics devem usar níveis diferentes de coleta.

23. Before × After exige controle térmico e repetição.

24. Ganho menor que a variabilidade não é ganho demonstrado.

25. Mais desempenho com instabilidade não é otimização.

26. Mais desempenho com custo térmico/energético extremo pode não ser otimização.

---

# Resultado da segunda passada

Primeira passada:
~80 itens.

Segunda passada:
~58 capacidades/subcapacidades/princípios adicionais.

Total bruto:
~138 itens.

Isso NÃO significa 138 features de CPU.

Durante especificação provavelmente serão consolidadas em grupos muito menores.

---

# Possíveis grupos finais emergentes

## CPU Information
- identification;
- topology;
- instruction sets;
- cache;
- CCD;
- hybrid architecture.

## CPU Monitoring
- utilization;
- clocks;
- temperature;
- package power;
- effective frequency.

## CPU Performance
- single-thread saturation;
- bottleneck;
- queue;
- sustained/burst behavior.

## CPU Scheduler
- thread placement;
- migration;
- CPU Sets;
- affinity;
- heterogeneous scheduling.

## Intel Diagnostics
- hybrid context;
- PL1;
- PL2;
- Tau;
- DTT.

## AMD Diagnostics
- CCD;
- X3D;
- PPT;
- TDC;
- EDC;
- Eco Mode;
- PBO state.

## Stability
- stress;
- WHEA;
- regression;
- repeated benchmarks.

## Advanced Profiling
- ETW;
- PMU;
- cycles;
- instructions;
- IPC;
- cache events.

## Validation
- repeatability;
- cooldown;
- variance;
- thermal cost;
- stability cost;
- rollback.

---

# Áreas que devem receber detalhes posteriormente

## Energy
Já possui PPM, EPP, parking e power modes.

## RAM
NUMA/cache/memory-bound diagnosis continuará lá.

## Latency
DPC, ISR, timer e scheduler latency serão aprofundados.

## BIOS
PBO, Curve Optimizer, SMT, E-cores, power limits e firmware controls.

## Cooling
Thermal behavior e sustained performance.

## Benchmark
Metodologia estatística e workloads.

## Gaming
Main-thread detection, CPU/GPU bottleneck e X3D scheduling.

---

# Avaliação de maturidade

A segunda passada já começou a encontrar majoritariamente:

- vendor-specific telemetry;
- submétricas;
- tuning avançado;
- profiling profissional;
- capacidades pertencentes a outras áreas.

Não apareceu nenhum grande domínio de CPU que esteja claramente ausente.

Estado sugerido:

**INITIAL DISCOVERY COMPLETE**