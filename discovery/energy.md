# Discovery Catalog — Energia
## Primeira passada — levantamento amplo

> Área de descoberta dedicada ao gerenciamento de energia, comportamento energético, relação desempenho × consumo, suspensão e políticas de energia do hardware.
>
> Energia não significa apenas “economizar bateria”.
>
> Esta área também investiga:
>
> - desempenho sustentado;
> - boost;
> - clocks;
> - latência de saída de estados de economia;
> - consumo;
> - temperatura;
> - suspensão;
> - comportamento AC × bateria;
> - políticas de dispositivos;
> - equilíbrio entre desempenho e eficiência.
>
> Nenhum valor específico encontrado nesta fase deve ser tratado como otimização universal.

---

# 1. Power Scheme Inventory

Status:
CANDIDATE

Type:
Diagnostic / Configuration

Technical Domain:
Power Management

Primary Product Area:
Optimization

Possible Consumers:
- Optimization
- My PC
- Benchmark
- Laptop

Final UI Placement:
TBD

Descrição:
Detectar os esquemas de energia existentes no Windows e identificar qual está ativo.

Possível objetivo:
Compreender a política energética atual antes de recomendar alterações.

Possível benefício:
Evitar aplicar configurações assumindo incorretamente que determinado plano está ativo.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic Diagnostic

Possíveis informações:
- active scheme;
- Balanced;
- High Performance;
- Power Saver;
- Ultimate Performance;
- OEM schemes;
- custom schemes;
- duplicated schemes;
- scheme GUID;
- individual settings relevantes.

Dúvidas:
- Como diferenciar planos OEM legítimos de planos antigos/residuais?
- Qual granularidade de configurações internas deve aparecer ao usuário?

---

# 2. Power Scheme Switching

Status:
CANDIDATE

Type:
Configuration / Optimization

Technical Domain:
Power Management

Primary Product Area:
Optimization

Possible Consumers:
- Optimization
- Gaming Profiles
- Laptop
- Benchmark

Final UI Placement:
TBD

Descrição:
Permitir trocar entre planos de energia existentes de maneira controlada.

Possível objetivo:
Adequar comportamento energético ao workload ou preferência do usuário.

Possível benefício:
Alterar rapidamente o equilíbrio entre desempenho, consumo, ruído e autonomia.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
User Choice / Contextual

Possíveis usos:
- gaming;
- benchmark;
- battery;
- workstation;
- quiet mode;
- restoration of original state.

Dúvidas:
- Quando trocar plano produz benefício mensurável em hardware moderno?
- Devemos recomendar plano diferente para cada workload?

---

# 3. Power Scheme Backup / Restore

Status:
CANDIDATE

Type:
Safety / Configuration

Technical Domain:
Power Management

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Gaming Profiles
- Benchmark

Final UI Placement:
TBD

Descrição:
Salvar o plano e configurações energéticas originais antes de qualquer modificação.

Possível objetivo:
Garantir reversibilidade.

Possível benefício:
Permitir rollback integral de alterações de energia.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic Safeguard

Possíveis capacidades:
- export scheme;
- import scheme;
- restore original active scheme;
- compare before/after;
- detect external modifications.

---

# 4. Power Scheme Configuration Diff

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Power Management

Primary Product Area:
Optimization

Possible Consumers:
- Optimization
- Repair

Final UI Placement:
TBD

Descrição:
Comparar configurações internas de dois planos de energia.

Possível objetivo:
Explicar precisamente por que planos aparentemente semelhantes apresentam comportamento diferente.

Possível benefício:
Encontrar alterações ocultas realizadas por OEMs, usuários ou tweak tools.

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

Dúvidas:
- Existem centenas de opções; quais são relevantes?
- Como evitar apresentar ruído?

---

# 5. Broken / Anomalous Power Scheme Detection

Status:
IDEA

Type:
Diagnostic / Repair

Technical Domain:
Power Management

Primary Product Area:
Repair

Possible Consumers:
- Optimization
- Repair

Final UI Placement:
TBD

Descrição:
Detectar planos com valores anormais, referências quebradas ou alterações potencialmente prejudiciais.

Possível objetivo:
Encontrar resíduos de antigas ferramentas de otimização ou planos importados.

Possível benefício:
Restaurar comportamento energético previsível.

Aplicabilidade:
Affected PCs

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Requires validation

Action Mode:
Diagnostic First

Dúvidas:
- O que constitui configuração “anormal”?
- Defaults variam por OEM e hardware.

---

# 6. Custom BeadWise Power Profile

Status:
RESEARCHING

Type:
Configuration / Optimization

Technical Domain:
Power Management

Primary Product Area:
Optimization

Possible Consumers:
- Gaming
- Benchmark
- Workstation
- Laptop

Final UI Placement:
TBD

Descrição:
Avaliar se faz sentido criar perfis próprios do BeadWise em vez de simplesmente selecionar planos existentes.

Possível objetivo:
Construir políticas baseadas no hardware e objetivo do usuário.

Possível benefício:
Permitir ajuste contextual mais preciso.

Aplicabilidade:
Specific Scenarios

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Context-dependent

Action Mode:
Benchmark First / User Choice

Dúvidas:
- Um plano próprio supera realmente Balanced em hardware moderno?
- Quais parâmetros seriam alterados?
- Perfis deveriam ser gerados dinamicamente?

---

# 7. Ultimate Performance Availability

Status:
CANDIDATE

Type:
Diagnostic / Configuration

Technical Domain:
Power Management

Primary Product Area:
Optimization

Possible Consumers:
- Workstation
- Benchmark
- Gaming

Final UI Placement:
TBD

Descrição:
Detectar disponibilidade e estado do esquema Ultimate Performance.

Possível objetivo:
Permitir avaliação sem assumir que ele é necessariamente melhor.

Possível benefício:
Oferecer alternativa de comportamento energético para workloads específicos.

Aplicabilidade:
Specific PCs / Workloads

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established feature / Context-dependent benefit

Action Mode:
Optional / Benchmark First

Importante:
Não tratar Ultimate Performance como “mais FPS garantido”.

---

# 8. AC × Battery Policy Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Power Management

Primary Product Area:
Shared

Possible Consumers:
- Laptop
- Optimization
- Benchmark

Final UI Placement:
TBD

Descrição:
Ler separadamente configurações aplicadas quando a máquina está conectada à energia e quando está na bateria.

Possível objetivo:
Evitar recomendar a mesma configuração para dois estados energéticos distintos.

Possível benefício:
Permitir otimização adaptativa em notebooks.

Aplicabilidade:
Laptop / Battery-capable devices

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

---

# 9. AC/Battery Profile Switching

Status:
IDEA

Type:
Configuration

Technical Domain:
Power Management

Primary Product Area:
Laptop

Possible Consumers:
- Optimization
- Gaming Profiles

Final UI Placement:
TBD

Descrição:
Aplicar comportamento diferente dependendo de a máquina estar conectada ou não à tomada.

Possível objetivo:
Priorizar desempenho em AC e autonomia em bateria.

Possível benefício:
Eliminar necessidade de troca manual frequente.

Aplicabilidade:
Laptops

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Established concept

Action Mode:
User Choice

Dúvidas:
- Windows/OEM já controla isso suficientemente bem?
- Devemos evitar conflito com software do fabricante?

---

# 10. Processor Minimum Performance State

Status:
CANDIDATE

Type:
Diagnostic / Configuration

Technical Domain:
Processor Power Management

Primary Product Area:
Energy

Possible Consumers:
- Optimization
- CPU
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar e eventualmente ajustar o limite mínimo permitido para o estado de desempenho do processador.

Possível objetivo:
Investigar impacto sobre idle, responsividade, temperatura e consumo.

Possível benefício:
Permitir comportamento diferente em workloads específicos.

Aplicabilidade:
Supported CPUs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Established setting / Context-dependent benefit

Action Mode:
Benchmark First

Importante:
100% mínimo não deve ser considerado automaticamente “melhor desempenho”.

---

# 11. Processor Maximum Performance State

Status:
CANDIDATE

Type:
Diagnostic / Configuration

Technical Domain:
Processor Power Management

Primary Product Area:
Energy

Possible Consumers:
- CPU
- Laptop
- Optimization

Final UI Placement:
TBD

Descrição:
Detectar e eventualmente configurar limite máximo do estado de desempenho do processador.

Possível objetivo:
Controlar consumo, temperatura ou comportamento de boost em cenários específicos.

Possível benefício:
Pode permitir perfis térmicos ou de autonomia.

Aplicabilidade:
Supported CPUs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Established

Action Mode:
Contextual

---

# 12. Processor Boost Mode Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Processor Power Management

Primary Product Area:
Energy

Possible Consumers:
- CPU
- Optimization
- Laptop
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar a política de boost aplicada pelo Windows.

Possível objetivo:
Explicar comportamento de frequência acima do clock nominal.

Possível benefício:
Diagnosticar máquinas que não boostam corretamente ou perfis excessivamente agressivos.

Aplicabilidade:
Supported CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic Diagnostic

---

# 13. Processor Boost Policy Tuning

Status:
RESEARCHING

Type:
Configuration / Optimization

Technical Domain:
Processor Power Management

Primary Product Area:
Energy

Possible Consumers:
- CPU
- Gaming
- Laptop
- Benchmark

Final UI Placement:
TBD

Descrição:
Investigar diferentes políticas de Processor Performance Boost Mode.

Possível objetivo:
Avaliar equilíbrio entre frequência, responsividade, temperatura e consumo.

Possível benefício:
Otimização contextual de workloads.

Aplicabilidade:
Supported CPUs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Established mechanism / Hardware-dependent result

Action Mode:
Benchmark First

Dúvidas:
- Quais modos são relevantes em Intel/AMD/ARM?
- Como hardware autônomo via CPPC/HWP interpreta cada política?
- Melhor modo depende do workload?

---

# 14. Energy Performance Preference — EPP Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Processor Power Management

Primary Product Area:
Energy

Possible Consumers:
- CPU
- Optimization
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar a preferência atual entre desempenho e eficiência energética do processador quando suportado.

Possível objetivo:
Entender como o Windows sinaliza ao hardware a prioridade performance × energy.

Possível benefício:
Explicar comportamentos que o simples nome do power plan não revela.

Aplicabilidade:
CPPC2 / Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic Diagnostic

---

# 15. EPP Tuning

Status:
RESEARCHING

Type:
Optimization / Configuration

Technical Domain:
Processor Power Management

Primary Product Area:
Energy

Possible Consumers:
- CPU
- Gaming
- Benchmark
- Laptop

Final UI Placement:
TBD

Descrição:
Investigar alteração de Energy Performance Preference para workloads específicos.

Possível objetivo:
Modificar a preferência entre desempenho e eficiência sem necessariamente fixar frequência.

Possível benefício:
Pode alterar responsividade, consumo e boost em hardware compatível.

Aplicabilidade:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Established mechanism / Context-dependent benefit

Action Mode:
Benchmark First

Importante:
EPP 0 não deve significar automaticamente “ótimo”.

---

# 16. CPPC / Hardware Autonomous Performance Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU / Power Management

Primary Product Area:
Shared

Possible Consumers:
- CPU
- Energy
- My PC

Final UI Placement:
TBD

Descrição:
Detectar características relevantes de gerenciamento autônomo de desempenho quando disponíveis.

Possível objetivo:
Saber se frequência e eficiência estão sendo administradas principalmente pelo hardware ou pelo sistema operacional.

Possível benefício:
Evitar aplicar lógica antiga de P-states a plataformas modernas.

Aplicabilidade:
Specific Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 17. Core Parking State Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Processor Power Management

Primary Product Area:
Energy

Possible Consumers:
- CPU
- Optimization
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar comportamento e configuração relacionada ao estacionamento de processadores lógicos.

Possível objetivo:
Permitir estudo do impacto real em diferentes workloads.

Possível benefício:
Explicar comportamento de distribuição de carga e consumo.

Aplicabilidade:
Supported CPUs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established mechanism

Action Mode:
Diagnostic

---

# 18. Core Parking Policy Tuning

Status:
RESEARCHING

Type:
Optimization / Configuration

Technical Domain:
Processor Power Management

Primary Product Area:
Energy

Possible Consumers:
- CPU
- Gaming
- Benchmark

Final UI Placement:
TBD

Descrição:
Investigar ajustes como CPMinCores e CPMaxCores.

Possível objetivo:
Avaliar se determinado workload se beneficia de política diferente da padrão.

Possível benefício:
Potencial alteração de responsividade, consumo e comportamento do scheduler.

Aplicabilidade:
Hardware / Workload dependent

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Established mechanism / Unproven universal benefit

Action Mode:
Benchmark First

Importante:
Desativar Core Parking globalmente não deve ser considerado otimização padrão.

---

# 19. Processor Idle State Diagnostics

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Processor Power Management

Primary Product Area:
Energy

Possible Consumers:
- CPU
- Monitoring
- Benchmark

Final UI Placement:
TBD

Descrição:
Investigar capacidade do processador de entrar em estados de baixo consumo durante idle.

Possível objetivo:
Detectar software ou dispositivos mantendo CPU em estado energético elevado.

Possível benefício:
Reduzir consumo, temperatura e ruído sem afetar desempenho ativo.

Aplicabilidade:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept

Action Mode:
Diagnostic Only

Dúvidas:
- Quais C-states podem ser observados confiavelmente?
- ETW, counters ou sensores de hardware?

---

# 20. Idle Power Consumption Diagnosis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Power / System

Primary Product Area:
Energy

Possible Consumers:
- Optimization
- Laptop
- Monitoring

Final UI Placement:
TBD

Descrição:
Analisar por que uma máquina apresenta consumo anormalmente alto quando aparentemente ociosa.

Possível objetivo:
Encontrar atividade que impede estados de baixo consumo.

Possível benefício:
Menor temperatura, ruído e gasto energético.

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

Possíveis causas:
- CPU activity;
- process timers;
- drivers;
- USB;
- network;
- GPU active state;
- background apps;
- power requests;
- indexing;
- scans.

---

# 21. Power Requests Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Power / Sleep

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Repair
- Optimization

Final UI Placement:
TBD

Descrição:
Detectar aplicações, serviços ou drivers que estejam solicitando que display ou sistema permaneçam ativos.

Possível objetivo:
Explicar por que o PC não dorme ou não desliga tela automaticamente.

Possível benefício:
Diagnóstico de consumo e suspensão.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic Diagnostic

---

# 22. Power Request Override Advisor

Status:
RESEARCHING

Type:
Configuration

Technical Domain:
Power / Sleep

Primary Product Area:
Energy

Possible Consumers:
- Repair
- Laptop

Final UI Placement:
TBD

Descrição:
Avaliar se determinadas solicitações de energia podem ser ignoradas explicitamente.

Possível objetivo:
Corrigir aplicações mal-comportadas que impedem suspensão sem razão legítima.

Possível benefício:
Restaurar sleep automático.

Aplicabilidade:
Specific Problems

Shared Capability:
No

Risco inicial:
High

Evidence Level:
Established mechanism / Context-sensitive safety

Action Mode:
Explicit User Choice

Importante:
Nunca criar override automaticamente apenas porque existe um Power Request.

---

# 23. Available Sleep States Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
ACPI / Power

Primary Product Area:
My PC

Possible Consumers:
- Energy
- Repair
- Laptop

Final UI Placement:
TBD

Descrição:
Detectar quais estados de suspensão o sistema oferece.

Possível objetivo:
Compreender arquitetura energética da máquina.

Possível benefício:
Evitar oferecer soluções incompatíveis com Modern Standby ou S3 tradicional.

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

Possíveis estados:
- Modern Standby / S0 Low Power Idle;
- S1;
- S2;
- S3;
- Hibernate/S4;
- Hybrid Sleep;
- Fast Startup relationships.

---

# 24. Modern Standby Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Sleep / Power

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- My PC
- Repair

Final UI Placement:
TBD

Descrição:
Detectar se a máquina utiliza Modern Standby.

Possível objetivo:
Escolher diagnósticos adequados ao modelo de suspensão.

Possível benefício:
Diferenciar problemas de S0 Low Power Idle de sistemas S3 tradicionais.

Aplicabilidade:
Modern Standby systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

---

# 25. Modern Standby Quality Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Sleep / Power

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Repair
- Monitoring

Final UI Placement:
TBD

Descrição:
Analisar a qualidade das sessões de Modern Standby.

Possível objetivo:
Identificar períodos de atividade excessiva enquanto o usuário acreditava que a máquina estava dormindo.

Possível benefício:
Diagnosticar bateria drenando durante sleep.

Aplicabilidade:
Modern Standby systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic Only

Possíveis informações:
- active time;
- low-power time;
- offending components;
- network activity;
- energy drain;
- sleep sessions.

---

# 26. Sleep Transition Diagnostics

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Sleep / Power

Primary Product Area:
Energy

Possible Consumers:
- Repair
- Laptop

Final UI Placement:
TBD

Descrição:
Analisar se o PC entrou efetivamente no estado de suspensão esperado.

Possível objetivo:
Diagnosticar máquinas que permanecem acordadas com tela desligada ou falham ao dormir.

Possível benefício:
Corrigir consumo desnecessário e comportamento anormal.

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

# 27. Last Wake Source Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Sleep / Wake

Primary Product Area:
Energy

Possible Consumers:
- Repair
- Laptop

Final UI Placement:
TBD

Descrição:
Identificar a fonte responsável por acordar o sistema da última suspensão.

Possível objetivo:
Explicar wake inesperado.

Possível benefício:
Diagnosticar mouse, rede, USB ou outros dispositivos acordando a máquina.

Aplicabilidade:
Sleep-capable PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 28. Wake-Capable Device Inventory

Status:
CANDIDATE

Type:
Diagnostic / Configuration

Technical Domain:
Device Power

Primary Product Area:
Energy

Possible Consumers:
- USB
- Network
- Laptop
- Repair

Final UI Placement:
TBD

Descrição:
Listar dispositivos capazes ou autorizados a acordar o sistema.

Possível objetivo:
Investigar wake inesperado.

Possível benefício:
Permitir configuração seletiva.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic / User Choice

---

# 29. Device Wake Permission Management

Status:
CANDIDATE

Type:
Configuration

Technical Domain:
Device Power

Primary Product Area:
Energy

Possible Consumers:
- USB
- Network
- Laptop

Final UI Placement:
TBD

Descrição:
Permitir habilitar ou desabilitar wake permission por dispositivo.

Possível objetivo:
Evitar wake indesejado sem desativar globalmente funcionalidades de suspensão.

Possível benefício:
Controle granular.

Aplicabilidade:
Supported Devices

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Established

Action Mode:
User Choice

---

# 30. Wake Timer Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Sleep / Wake

Primary Product Area:
Energy

Possible Consumers:
- Repair
- Laptop

Final UI Placement:
TBD

Descrição:
Detectar timers programados para acordar o sistema.

Possível objetivo:
Explicar wake automático aparentemente sem causa.

Possível benefício:
Diagnóstico de updates, manutenção ou software agendado.

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

# 31. Wake Timer Policy Management

Status:
RESEARCHING

Type:
Configuration

Technical Domain:
Sleep / Wake

Primary Product Area:
Energy

Possible Consumers:
- Laptop

Final UI Placement:
TBD

Descrição:
Investigar controle da política de wake timers.

Possível objetivo:
Evitar acordadas desnecessárias em cenários específicos.

Possível benefício:
Economia energética e menor interrupção.

Aplicabilidade:
Specific Scenarios

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Established mechanism / Context-dependent

Action Mode:
User Choice

---

# 32. Hibernate Availability / Configuration

Status:
CANDIDATE

Type:
Diagnostic / Configuration

Technical Domain:
Sleep / Power

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- My PC

Final UI Placement:
TBD

Descrição:
Detectar disponibilidade e configuração da hibernação.

Possível objetivo:
Permitir escolha consciente entre sleep, hibernate e desligamento.

Possível benefício:
Autonomia e preservação de sessão.

Aplicabilidade:
Supported PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic / User Choice

---

# 33. Hibernation File Configuration

Status:
RESEARCHING

Type:
Configuration / Storage

Technical Domain:
Power / Storage

Primary Product Area:
Energy

Possible Consumers:
- Storage
- Laptop

Final UI Placement:
TBD

Descrição:
Investigar configuração e tamanho do arquivo de hibernação.

Possível objetivo:
Equilibrar recursos de energia com uso de armazenamento.

Possível benefício:
Recuperar espaço em cenários específicos sem quebrar Fast Startup ou Hibernate inadvertidamente.

Aplicabilidade:
Specific PCs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Established

Action Mode:
Contextual

Dúvidas:
- Full × reduced hiberfile.
- Relação com Fast Startup.
- Quando realmente faz sentido alterar?

---

# 34. Fast Startup State Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Boot / Power

Primary Product Area:
Energy

Possible Consumers:
- Startup
- Repair
- Benchmark

Final UI Placement:
TBD

Descrição:
Detectar se Fast Startup está habilitado e se o sistema suporta seu funcionamento.

Possível objetivo:
Dar contexto a comportamento de shutdown/boot e troubleshooting.

Possível benefício:
Evitar interpretar hybrid shutdown como boot totalmente limpo.

Aplicabilidade:
Supported PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 35. Fast Startup Configuration

Status:
RESEARCHING

Type:
Configuration

Technical Domain:
Boot / Power

Primary Product Area:
Energy

Possible Consumers:
- Repair
- Startup

Final UI Placement:
TBD

Descrição:
Permitir alteração de Fast Startup quando houver motivo específico.

Possível objetivo:
Resolver compatibilidade ou comportamento de boot em determinadas máquinas.

Possível benefício:
Context-dependent.

Aplicabilidade:
Specific PCs

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Established mechanism / Context-dependent benefit

Action Mode:
Problem-triggered / User Choice

Importante:
Não tratar Fast Startup ON ou OFF como regra universal.

---

# 36. USB Selective Suspend State

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
USB / Device Power

Primary Product Area:
Energy

Possible Consumers:
- USB
- Laptop
- My PC

Final UI Placement:
TBD

Descrição:
Detectar política de suspensão seletiva USB e dispositivos relacionados.

Possível objetivo:
Diagnosticar problemas de energia ou dispositivos que acordam/desconectam.

Possível benefício:
Compreender comportamento energético da pilha USB.

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

# 37. USB Selective Suspend Troubleshooter

Status:
CANDIDATE

Type:
Diagnostic / Repair

Technical Domain:
USB / Device Power

Primary Product Area:
Energy

Possible Consumers:
- USB
- Repair

Final UI Placement:
TBD

Descrição:
Investigar dispositivos específicos com problemas relacionados a suspend/resume.

Possível objetivo:
Resolver desconexões ou problemas de retorno sem desativar a suspensão seletiva globalmente.

Possível benefício:
Preservar eficiência energética enquanto corrige hardware/driver problemático.

Aplicabilidade:
Affected Devices

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Established mechanism

Action Mode:
Diagnostic First

Importante:
Não adotar “Disable USB Selective Suspend” como otimização geral.

---

# 38. PCIe Link State Power Management Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
PCIe / Device Power

Primary Product Area:
Energy

Possible Consumers:
- GPU
- Storage
- PCIe
- Laptop

Final UI Placement:
TBD

Descrição:
Detectar política ASPM/Link State Power Management do plano de energia.

Possível objetivo:
Compreender equilíbrio entre economia e latência de dispositivos PCIe.

Possível benefício:
Ajudar troubleshooting e criação de perfis contextuais.

Aplicabilidade:
PCIe systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 39. PCIe ASPM Policy Tuning

Status:
RESEARCHING

Type:
Configuration / Optimization

Technical Domain:
PCIe / Power

Primary Product Area:
Energy

Possible Consumers:
- GPU
- Storage
- Gaming
- Laptop

Final UI Placement:
TBD

Descrição:
Investigar impacto de diferentes níveis de Link State Power Management.

Possível objetivo:
Avaliar trade-off entre energia, temperatura e wake latency.

Possível benefício:
Contextual.

Aplicabilidade:
Hardware-dependent

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Established mechanism / Context-dependent outcome

Action Mode:
Benchmark First

Importante:
ASPM OFF não deve ser assumido como “menor input lag” sem validação.

---

# 40. Display Power Policy

Status:
CANDIDATE

Type:
Configuration

Technical Domain:
Display / Power

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Display

Final UI Placement:
TBD

Descrição:
Detectar políticas de desligamento de tela e comportamento AC/bateria.

Possível objetivo:
Equilibrar disponibilidade visual e consumo.

Possível benefício:
Autonomia e prevenção de consumo desnecessário.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
User Choice

---

# 41. Sleep Timeout Policy

Status:
CANDIDATE

Type:
Configuration

Technical Domain:
Sleep / Power

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Desktop

Final UI Placement:
TBD

Descrição:
Detectar e configurar tempo até suspensão.

Possível objetivo:
Ajustar comportamento de idle ao perfil do usuário.

Possível benefício:
Energia / convenience.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
User Choice

---

# 42. Energy Efficiency Scan

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Power

Primary Product Area:
Energy

Possible Consumers:
- Optimization
- Laptop
- My PC

Final UI Placement:
TBD

Descrição:
Executar análise de eficiência energética em estado apropriado de idle.

Possível objetivo:
Encontrar problemas comuns de consumo e duração de bateria.

Possível benefício:
Diagnóstico abrangente usando dados observados.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
User-triggered Diagnostic

Possível inspiração/mecanismo:
PowerCfg Energy analysis.

---

# 43. Energy Diagnostic Readiness Check

Status:
IDEA

Type:
Diagnostic Safeguard

Technical Domain:
Power

Primary Product Area:
Energy

Possible Consumers:
- Optimization

Final UI Placement:
TBD

Descrição:
Confirmar que o sistema está suficientemente ocioso antes de executar análise energética.

Possível objetivo:
Evitar resultados contaminados por aplicações ativas.

Possível benefício:
Aumentar qualidade do diagnóstico.

Aplicabilidade:
Energy scan

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established requirement / Product logic

Action Mode:
Automatic

---

# 44. Historical Energy Usage

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Power / SRUM

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Monitoring
- Processes

Final UI Placement:
TBD

Descrição:
Investigar histórico de estimativa de energia armazenado pelo Windows.

Possível objetivo:
Identificar aplicações que consumiram energia ao longo do tempo sem manter BeadWise rodando permanentemente.

Possível benefício:
Diagnóstico histórico.

Aplicabilidade:
Supported Windows Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established mechanism

Action Mode:
Diagnostic

Possível fonte:
SRUM.

---

# 45. Energy Consumption by Application

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Power / Processes

Primary Product Area:
Energy

Possible Consumers:
- Processes
- Laptop
- Monitoring

Final UI Placement:
TBD

Descrição:
Classificar aplicações por impacto energético durante determinado período.

Possível objetivo:
Encontrar software consumindo energia desproporcionalmente.

Possível benefício:
Autonomia, temperatura e redução de background activity.

Aplicabilidade:
Especially Laptops

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent / Established telemetry concept

Action Mode:
Diagnostic

---

# 46. Battery Presence / Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Battery

Primary Product Area:
Shared

Possible Consumers:
- Energy
- Battery
- My PC

Final UI Placement:
TBD

Descrição:
Detectar existência e características básicas da bateria.

Possível objetivo:
Habilitar condicionalmente recursos específicos de notebook.

Possível benefício:
Evitar funcionalidades irrelevantes em desktops.

Aplicabilidade:
Battery-capable PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

---

# 47. Battery Health Overview

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Battery

Primary Product Area:
My PC

Possible Consumers:
- Energy
- Laptop

Final UI Placement:
TBD

Descrição:
Avaliar estado geral da bateria.

Possível objetivo:
Diferenciar baixa autonomia causada por software de degradação física.

Possível benefício:
Diagnóstico mais correto.

Aplicabilidade:
Laptops

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic Only

Possíveis dados:
- design capacity;
- full charge capacity;
- wear estimate;
- usage history;
- charge/discharge history;
- cycle count quando disponível.

---

# 48. Battery Usage Report

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Battery

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- My PC

Final UI Placement:
TBD

Descrição:
Interpretar histórico de uso da bateria e capacidade ao longo do tempo.

Possível objetivo:
Mostrar tendência de degradação e padrões de uso.

Possível benefício:
Explicar redução de autonomia.

Aplicabilidade:
Battery-equipped PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 49. Abnormal Battery Drain Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Battery / Power

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Monitoring

Final UI Placement:
TBD

Descrição:
Detectar consumo significativamente elevado em períodos de uso leve ou standby.

Possível objetivo:
Distinguir bateria degradada de software/hardware mantendo consumo alto.

Possível benefício:
Diagnóstico de autonomia ruim.

Aplicabilidade:
Laptops

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent

Action Mode:
Diagnostic

Dúvidas:
- Como estabelecer baseline por máquina?
- Como separar brilho, workload e degradação?

---

# 50. Charging / Discharging Rate Monitoring

Status:
IDEA

Type:
Monitoring

Technical Domain:
Battery

Primary Product Area:
Monitoring

Possible Consumers:
- Energy
- Laptop

Final UI Placement:
TBD

Descrição:
Monitorar velocidade instantânea ou média de carga/descarga quando hardware expuser dados suficientes.

Possível objetivo:
Entender comportamento energético durante uso real.

Possível benefício:
Detectar consumo elevado e problemas de carregamento.

Aplicabilidade:
Supported Laptops

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Hardware-dependent

Action Mode:
Diagnostic / Monitoring

---

# 51. Energy Saver State Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Windows Energy Saver

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Optimization

Final UI Placement:
TBD

Descrição:
Detectar estado e políticas do Energy Saver nas versões compatíveis do Windows.

Possível objetivo:
Entender se o Windows está aplicando restrições energéticas adicionais.

Possível benefício:
Explicar comportamento diferente de desempenho.

Aplicabilidade:
Windows 11 24H2+

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic Diagnostic

Observação:
Energy Saver substitui Battery Saver em Windows 11 24H2+.

---

# 52. Energy Saver Configuration

Status:
CANDIDATE

Type:
Configuration

Technical Domain:
Windows Energy Saver

Primary Product Area:
Energy

Possible Consumers:
- Laptop

Final UI Placement:
TBD

Descrição:
Gerenciar, quando apropriado, políticas disponíveis do Energy Saver.

Possível objetivo:
Permitir equilíbrio mais consciente entre consumo e desempenho.

Possível benefício:
Autonomia e menor consumo.

Aplicabilidade:
Supported Windows Versions

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
User Choice

---

# 53. Power Throttling State Analysis

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
Process Power Management

Primary Product Area:
Energy

Possible Consumers:
- Processes
- Optimization
- Gaming

Final UI Placement:
TBD

Descrição:
Investigar mecanismos pelos quais o Windows reduz prioridade energética ou desempenho de workloads considerados adequados para execução eficiente.

Possível objetivo:
Explicar aplicações que apresentam comportamento diferente em foreground/background.

Possível benefício:
Diagnóstico de performance ou consumo.

Aplicabilidade:
Supported Windows Versions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept / implementation research needed

Action Mode:
Diagnostic

---

# 54. Per-Process Energy Efficiency Policy

Status:
RESEARCHING

Type:
Configuration

Technical Domain:
Process Power Management

Primary Product Area:
Energy

Possible Consumers:
- Processes
- Gaming Profiles
- Background Apps

Final UI Placement:
TBD

Descrição:
Investigar se determinados processos podem receber políticas energéticas diferentes de forma confiável.

Possível objetivo:
Permitir que background work priorize eficiência sem alterar o PC inteiro.

Possível benefício:
Melhor equilíbrio entre foreground performance e background consumption.

Aplicabilidade:
Specific Processes

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Requires technical validation

Action Mode:
Contextual

---

# 55. Performance-per-Watt Benchmark

Status:
IDEA

Type:
Benchmark

Technical Domain:
Power / Performance

Primary Product Area:
Benchmark

Possible Consumers:
- Energy
- CPU
- GPU
- Laptop

Final UI Placement:
TBD

Descrição:
Comparar desempenho obtido em relação ao consumo energético quando dados confiáveis estiverem disponíveis.

Possível objetivo:
Avaliar eficiência e não somente pontuação absoluta.

Possível benefício:
Encontrar configuração que entrega quase o mesmo desempenho com muito menos consumo/calor.

Aplicabilidade:
Hardware-dependent

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement concept

Action Mode:
Benchmark

Possíveis métricas:
- score/watt;
- FPS/watt;
- workload completion/watt;
- energy consumed per workload.

---

# 56. Thermal / Power Efficiency Comparison

Status:
IDEA

Type:
Benchmark / Diagnostic

Technical Domain:
Power / Thermal

Primary Product Area:
Energy

Possible Consumers:
- CPU
- GPU
- Benchmark

Final UI Placement:
TBD

Descrição:
Comparar diferentes configurações considerando simultaneamente desempenho, temperatura e consumo.

Possível objetivo:
Evitar considerar configuração melhor apenas porque produz maior benchmark.

Possível benefício:
Encontrar sweet spot do hardware.

Aplicabilidade:
Supported Sensor Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Benchmark

Exemplo:
Profile A:
100 FPS / 80 W / 65°C

Profile B:
103 FPS / 130 W / 82°C

O BeadWise não deveria declarar automaticamente B como “melhor”.

---

# 57. Power-Limited Performance Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
CPU/GPU / Power

Primary Product Area:
Shared

Possible Consumers:
- CPU
- GPU
- Benchmark
- Energy

Final UI Placement:
TBD

Descrição:
Detectar quando desempenho está limitado por orçamento energético.

Possível objetivo:
Diferenciar power throttling de thermal throttling ou falta de utilização.

Possível benefício:
Diagnóstico mais preciso de clocks abaixo do esperado.

Aplicabilidade:
Sensor-capable Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept / Vendor-dependent telemetry

Action Mode:
Diagnostic

---

# 58. Performance Regression Caused by Energy Policy

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Power / Performance

Primary Product Area:
Optimization

Possible Consumers:
- Benchmark
- Gaming
- CPU

Final UI Placement:
TBD

Descrição:
Identificar casos em que mudança de política energética realmente reduz desempenho mensurável.

Possível objetivo:
Distinguir mudança teórica de impacto real.

Possível benefício:
Somente recomendar alteração quando existe regressão detectada.

Aplicabilidade:
Benchmarkable Scenarios

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Benchmark First

---

# 59. Dynamic Workload Power Profile

Status:
IDEA

Type:
Optimization / Automation

Technical Domain:
Power Management

Primary Product Area:
Optimization

Possible Consumers:
- Gaming Profiles
- Benchmark
- Workstation

Final UI Placement:
TBD

Descrição:
Alterar temporariamente política energética quando determinado workload inicia e restaurá-la quando termina.

Possível objetivo:
Evitar manter configuração de alto consumo permanentemente.

Possível benefício:
Performance quando necessária, eficiência no restante do tempo.

Aplicabilidade:
Specific Workloads

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Product concept based on established mechanisms

Action Mode:
User Opt-in

Possível fluxo:
Game starts
→ snapshot
→ performance profile
→ game closes
→ restore previous profile.

---

# 60. Power Profile Before × After Validation

Status:
CANDIDATE

Type:
Benchmark / Validation

Technical Domain:
Power / Performance

Primary Product Area:
Optimization

Possible Consumers:
- Benchmark
- CPU
- Gaming

Final UI Placement:
TBD

Descrição:
Medir resultado de qualquer alteração energética relevante.

Possível objetivo:
Evitar afirmar melhoria apenas porque uma configuração parece mais agressiva.

Possível benefício:
Manter apenas alterações com benefício demonstrável.

Aplicabilidade:
Configurable PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement principle

Action Mode:
Automatic Validation

Possíveis métricas:
- performance;
- frametime;
- latency;
- temperature;
- energy;
- clock behavior;
- idle consumption;
- battery drain.

---

# Ideias que NÃO devem virar otimização automática nesta fase

## High Performance para todo mundo

Não assumir.

Pode existir como opção e candidato para benchmark.

---

## Ultimate Performance para todo mundo

Não assumir.

A existência do plano não prova ganho relevante no hardware atual.

---

## Minimum Processor State = 100%

Não assumir.

Pode prejudicar idle, consumo, temperatura e ruído sem aumentar desempenho real.

---

## EPP = 0

Não assumir.

É uma preferência extrema por performance, não uma definição universal de “ótimo”.

---

## Core Parking completamente desativado

Não assumir.

O mecanismo existe justamente para o Windows/hardware gerenciarem capacidade ativa conforme necessidade.

---

## USB Selective Suspend = Disabled

Não usar como tweak global.

Só investigar quando um dispositivo específico demonstra problema.

---

## PCIe ASPM = Off

Não assumir redução útil de latência.

Exige benchmark e aplicabilidade por hardware.

---

## Hibernação = Off

Não assumir.

Afeta Hibernate e pode afetar recursos como Fast Startup.

---

## Fast Startup = Off

Não assumir.

Pode solucionar problemas específicos, mas não é otimização universal.

---

## Power Requests Override automático

Não fazer.

Uma aplicação pode estar impedindo sleep por motivo legítimo.

---

# Possíveis subgrupos que emergem de Energia

## Power Plans
- inventário;
- switching;
- custom profiles;
- backup;
- diff;
- restore.

## Processor Power Management
- min/max performance;
- boost;
- EPP;
- CPPC;
- core parking;
- idle states.

## Sleep / Wake
- available states;
- Modern Standby;
- hibernate;
- wake sources;
- timers;
- Power Requests.

## Device Power
- USB;
- PCIe;
- wake permissions.

## Energy Diagnostics
- energy scan;
- idle analysis;
- historical energy;
- application impact.

## Battery
- health;
- degradation;
- usage;
- drain;
- charging/discharging.

## Dynamic Performance
- workload profiles;
- gaming profiles;
- before/after validation;
- performance-per-watt.

---

# Hipóteses para segunda passada

- Process Power Throttling / EcoQoS merece feature própria?
- O Task Manager Efficiency Mode expõe alguma API útil ao BeadWise?
- Quais opções modernas de Processor Power Management diferem entre Intel HWP, AMD CPPC e ARM?
- Quais parâmetros ocultos de PPM são realmente relevantes?
- Heterogeneous CPU power policy pertence a Energia ou CPU?
- Como detectar residências em C-state de forma portável?
- Podemos medir package power com confiabilidade suficiente para performance-per-watt?
- Quais GPUs expõem board power de forma consistente?
- Como OEM software altera planos do Windows?
- Windows power mode (“Best performance / Balanced / Best power efficiency”) é separado do esquema tradicional em quais máquinas?
- Como overlays de power mode interagem com power schemes?
- Modern Standby possui outros diagnósticos valiosos além de SleepStudy?
- Como diagnosticar “laptop esquenta dentro da mochila”?
- Podemos detectar dispositivo impedindo deep idle sem depender apenas de relatório textual?
- Como medir sleep drain automaticamente?
- É possível identificar timer resolution excessivamente alta como causa de consumo?
- Timers e Dynamic Tick pertencem parcialmente a Energia além da área Latency/CPU?
- Que impacto real HAGS/GPU scheduling pode ter em consumo?
- Devemos detectar display refresh rate elevado durante bateria?
- Variable Refresh Rate pode reduzir consumo em desktop/laptop?
- Como tratar dGPU permanecendo acordada sem workload?
- Podemos detectar software ativando a GPU dedicada desnecessariamente?
- PCIe Active State Power Management pode ser observado além da política configurada?
- Storage power policies devem permanecer em Storage ou ter capability compartilhada?
- Network adapter power management deve ser aprofundado em Network?
- Bluetooth power behavior merece capacidade própria?
- Existem políticas de disk idle relevantes para HDD ainda?
- Como energia se comporta em desktops sem bateria?
- Deveríamos ter um perfil “Quiet / Efficient” além de “Maximum Performance”?
- Qual deve ser o papel de Energy Saver em PCs desktop no Windows 11 24H2+?
- Devemos medir performance por joule em benchmarks?
- Há valor em detectar eficiência anormal comparando hardware semelhante?

---

# Avaliação inicial da área

A primeira passada encontrou aproximadamente 60 capacidades ou hipóteses.

O núcleo mais forte parece ser:

1. compreender a política energética atual;
2. detectar capacidades do hardware;
3. diagnosticar problemas;
4. separar AC e bateria;
5. medir impacto real;
6. evitar valores universais;
7. usar perfis temporários quando fizer sentido;
8. restaurar estado original;
9. avaliar performance, consumo e temperatura conjuntamente.

Princípio provisório:

> **Maximum power consumption ≠ maximum useful performance.**

E:

> **A melhor configuração energética depende do hardware, workload e objetivo do usuário.**

Estado:

FIRST PASS COMPLETE

Próximo passo:

SECOND PASS — ENERGY GAP AUDIT

# Discovery Catalog — Energia
## Segunda passada — auditoria de lacunas

> Este bloco complementa os itens 1–60 levantados na primeira passada de Energia.
>
> O objetivo desta rodada é procurar capacidades ausentes, especialmente em:
>
> - Power Modes modernos do Windows;
> - overlays de energia;
> - EcoQoS / Power Throttling;
> - timer resolution;
> - Modern Standby / DRIPS;
> - dispositivos impedindo low-power states;
> - gráficos híbridos;
> - display;
> - eficiência energética contextual.
>
> Novamente, mecanismo existente não significa benefício universal.

---

# 61. User-Configured Power Mode Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Power Management

Primary Product Area:
Energy

Possible Consumers:
- Optimization
- Laptop
- Benchmark
- Gaming

Final UI Placement:
TBD

Descrição:
Detectar o Power Mode selecionado pelo usuário independentemente do Power Scheme tradicional.

Possível objetivo:
Distinguir corretamente:

- Power Scheme;
- Power Mode;
- Effective Power Mode.

Possível benefício:
Evitar diagnosticar energia apenas olhando Balanced / High Performance / Ultimate Performance.

Aplicabilidade:
Supported Windows Versions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic Diagnostic

Possíveis estados modernos:
- Best Power Efficiency;
- Balanced;
- Best Performance.

Dúvidas:
- Como tratar versões anteriores do Windows?
- Como OEM overlays interagem com esses modos?

Observação:
Power Modes eram anteriormente chamados de overlays/overlay schemes.

---

# 62. AC Power Mode Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Power Management

Primary Product Area:
Energy

Possible Consumers:
- Optimization
- Laptop

Final UI Placement:
TBD

Descrição:
Identificar especificamente o Power Mode configurado para uso conectado à energia.

Possível objetivo:
Separar preferência AC da preferência DC.

Possível benefício:
Permitir diagnóstico e perfis mais precisos.

Aplicabilidade:
Supported Windows Versions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

---

# 63. DC Power Mode Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Power Management

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Optimization

Final UI Placement:
TBD

Descrição:
Identificar especificamente o Power Mode configurado para alimentação por bateria.

Possível objetivo:
Diferenciar comportamento desejado em bateria.

Possível benefício:
Permitir recomendações de autonomia sem alterar comportamento AC.

Aplicabilidade:
Battery-capable PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

---

# 64. Effective Power Mode Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Power Management

Primary Product Area:
Shared

Possible Consumers:
- Energy
- CPU
- Gaming
- Benchmark
- Monitoring

Final UI Placement:
TBD

Descrição:
Detectar o modo de energia efetivamente aplicado pelo sistema.

Possível objetivo:
Distinguir a preferência configurada pelo usuário do estado realmente utilizado pelo Windows.

Possível benefício:
Explicar situações em que o usuário escolheu determinado modo, mas outro sinal do sistema alterou o comportamento efetivo.

Aplicabilidade:
Supported Windows Versions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

Possíveis estados:
- Energy Saver;
- Better Battery;
- Balanced;
- High Performance;
- Max Performance;
- Game Mode;
- outros estados suportados pela versão da API.

Dúvidas:
- Quais estados realmente aparecem nas builds alvo?
- Como explicar overrides sem expor complexidade excessiva?

---

# 65. Power Mode Change Monitoring

Status:
IDEA

Type:
Monitoring

Technical Domain:
Power Management

Primary Product Area:
Shared

Possible Consumers:
- Monitoring
- Benchmark
- Gaming Profiles
- Energy

Final UI Placement:
TBD

Descrição:
Receber notificações quando o modo efetivo de energia mudar durante uma sessão.

Possível objetivo:
Relacionar mudanças de comportamento energético com alterações de desempenho.

Possível benefício:
Evitar benchmark contaminado por mudança de power mode no meio do teste.

Aplicabilidade:
Supported Windows Versions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic Monitoring

---

# 66. Power Scheme × Power Mode Interaction Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Power Management

Primary Product Area:
Energy

Possible Consumers:
- Optimization
- Repair
- Benchmark

Final UI Placement:
TBD

Descrição:
Analisar a relação entre plano tradicional e Power Mode moderno.

Possível objetivo:
Explicar por que dois PCs com “Balanced” podem apresentar comportamento energético diferente.

Possível benefício:
Evitar recomendações baseadas apenas no nome do plano.

Aplicabilidade:
Supported Windows Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established / Platform-dependent details

Action Mode:
Diagnostic

Dúvidas:
- Quais opções do PPM são sobrescritas por overlay?
- Quanto é definido pelo Windows e quanto pelo OEM?

---

# 67. OEM Power Overlay Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
Power / OEM

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Optimization
- My PC

Final UI Placement:
TBD

Descrição:
Investigar alterações de energia aplicadas pelo fabricante da máquina sobre os mecanismos padrão do Windows.

Possível objetivo:
Entender por que determinada máquina reage de forma diferente às mesmas configurações.

Possível benefício:
Evitar conflito com políticas desenvolvidas especificamente pelo OEM.

Aplicabilidade:
OEM Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established capability / Vendor-dependent implementation

Action Mode:
Diagnostic

Possíveis fontes de influência:
- firmware;
- ACPI;
- INF;
- vendor services;
- vendor control software;
- power overlays.

Dúvidas:
- É possível identificar origem da política?
- Quanto pode ser observado genericamente?

---

# 68. Effective Power Configuration Explanation

Status:
IDEA

Type:
Diagnostic / Product Behavior

Technical Domain:
Power

Primary Product Area:
Energy

Possible Consumers:
- Optimization
- My PC

Final UI Placement:
TBD

Descrição:
Explicar ao usuário qual combinação de configurações está atualmente determinando comportamento energético.

Possível objetivo:
Transformar um sistema complexo de planos, overlays e estados em uma explicação simples.

Possível benefício:
Transparência.

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

Exemplo conceitual:

Power Scheme:
Balanced

User Power Mode:
Best Performance

Effective Power Mode:
Maximum Performance

Power Source:
AC

Resultado:
“O Windows está atualmente favorecendo desempenho.”

---

# 69. EcoQoS Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Process Power Management

Primary Product Area:
Energy

Possible Consumers:
- Processes
- Background Apps
- Monitoring

Final UI Placement:
TBD

Descrição:
Detectar suporte ao mecanismo EcoQoS e sua aplicabilidade.

Possível objetivo:
Identificar possibilidade de executar workloads não críticos de maneira mais eficiente.

Possível benefício:
Reduzir consumo, temperatura e ruído sem necessariamente prejudicar tarefas foreground.

Aplicabilidade:
Supported Windows Versions / Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Observação:
EcoQoS é destinado a trabalho que não contribui diretamente para experiência foreground crítica.

---

# 70. Process EcoQoS State Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
Process Power Management

Primary Product Area:
Energy

Possible Consumers:
- Processes
- Monitoring
- Background Apps

Final UI Placement:
TBD

Descrição:
Investigar possibilidade de identificar processos operando sob políticas de eficiência energética.

Possível objetivo:
Explicar por que determinado processo recebe comportamento energético diferente.

Possível benefício:
Maior visibilidade sobre Power Throttling.

Aplicabilidade:
Supported Windows Versions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Mechanism established / observability needs research

Action Mode:
Diagnostic

---

# 71. Background Work EcoQoS Advisor

Status:
IDEA

Type:
Optimization / Recommendation

Technical Domain:
Process Power Management

Primary Product Area:
Optimization

Possible Consumers:
- Background Apps
- Processes

Final UI Placement:
TBD

Descrição:
Identificar workloads claramente não interativos que potencialmente poderiam priorizar eficiência.

Possível objetivo:
Reduzir consumo de tarefas background sem alterar todo o PC.

Possível benefício:
Menor calor, ruído e consumo.

Aplicabilidade:
Specific Processes

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Context-dependent

Action Mode:
Recommendation / Experimental

Importante:
Nunca aplicar EcoQoS indiscriminadamente a jogos ou aplicações foreground.

Dúvidas:
- Podemos alterar políticas de processos de terceiros de forma suportada?
- O benefício permanece após reinicialização?
- É responsabilidade do próprio aplicativo optar por EcoQoS?

---

# 72. Power Throttling Diagnostics

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Process Power Management

Primary Product Area:
Energy

Possible Consumers:
- Processes
- Gaming
- Optimization

Final UI Placement:
TBD

Descrição:
Investigar se Power Throttling está relacionado a comportamento de performance de determinado processo.

Possível objetivo:
Diferenciar CPU/GPU insuficiente de política energética aplicada ao workload.

Possível benefício:
Melhor diagnóstico de performance inesperada.

Aplicabilidade:
Supported Windows Versions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established mechanism

Action Mode:
Diagnostic

---

# 73. Timer Resolution Request Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Timers / Power

Primary Product Area:
Energy

Possible Consumers:
- Latency
- Processes
- Gaming
- Monitoring

Final UI Placement:
TBD

Descrição:
Detectar processos solicitando resolução de timer elevada quando tecnicamente possível.

Possível objetivo:
Identificar software mantendo temporizadores mais frequentes e potencialmente prejudicando eficiência energética.

Possível benefício:
Diagnosticar idle power elevado e comportamento energético inesperado.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established mechanism

Action Mode:
Diagnostic

Dúvidas:
- Qual mecanismo permite identificar o processo responsável?
- ETW será necessário?

---

# 74. Excessive Timer Resolution Diagnostic

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Timers / Power / Performance

Primary Product Area:
Energy

Possible Consumers:
- Latency
- Optimization
- Monitoring

Final UI Placement:
TBD

Descrição:
Avaliar se pedidos persistentes de alta resolução de timer estão afetando eficiência energética.

Possível objetivo:
Encontrar aplicações que dificultam estados de economia do processador.

Possível benefício:
Menor idle power e melhor autonomia.

Aplicabilidade:
Affected PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established mechanism / Impact context-dependent

Action Mode:
Diagnostic First

Importante:
Alta resolução de timer pode melhorar precisão de alguns waits, mas também aumentar atividade do scheduler e impedir estados de economia.

---

# 75. Timer Resolution Energy Correlation

Status:
IDEA

Type:
Diagnostic / Benchmark

Technical Domain:
Timers / Energy

Primary Product Area:
Energy

Possible Consumers:
- Latency
- Benchmark

Final UI Placement:
TBD

Descrição:
Medir impacto energético de determinada resolução de timer.

Possível objetivo:
Separar benefício real de latência de custo energético.

Possível benefício:
Permitir decisão baseada em trade-off.

Aplicabilidade:
Specific Workloads

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Benchmark First

Possíveis métricas:
- package power;
- CPU residency;
- wakeups;
- scheduler activity;
- battery drain;
- performance/latency.

---

# 76. Timer Resolution Myth Guard

Status:
CANDIDATE

Type:
Product Behavior / Safety

Technical Domain:
Timers

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Latency
- Gaming

Final UI Placement:
TBD

Descrição:
Impedir que a aplicação trate “menor timer resolution” como sinônimo automático de menor latência ou maior FPS.

Possível objetivo:
Evitar tweak universal incorreto.

Possível benefício:
Reduzir alterações sem evidência.

Aplicabilidade:
Application-wide

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established principle

Action Mode:
Internal safeguard

---

# 77. Occluded Process Timer Throttling Detection

Status:
RESEARCHING

Type:
Diagnostic

Technical Domain:
Process Power / Timers

Primary Product Area:
Energy

Possible Consumers:
- Processes
- Background Apps

Final UI Placement:
TBD

Descrição:
Investigar o comportamento introduzido no Windows 11 que permite ignorar pedidos de alta resolução de timer de processos invisíveis/minimizados/não audíveis.

Possível objetivo:
Compreender melhor o que o Windows já otimiza automaticamente.

Possível benefício:
Evitar implementar tweaks redundantes.

Aplicabilidade:
Windows 11

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established behavior

Action Mode:
Diagnostic / Research

---

# 78. Modern Standby DRIPS Residency Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Modern Standby

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Repair
- Monitoring

Final UI Placement:
TBD

Descrição:
Analisar quanto tempo o sistema permanece efetivamente em seu estado profundo de baixo consumo durante Modern Standby.

Possível objetivo:
Avaliar qualidade real da suspensão.

Possível benefício:
Detectar máquinas que aparentemente dormem, mas permanecem energicamente ativas.

Aplicabilidade:
Modern Standby PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Possíveis métricas:
- software DRIPS;
- hardware DRIPS quando disponível;
- active time;
- low-power time;
- energy change;
- drain rate.

---

# 79. Poor DRIPS Residency Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Modern Standby

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Repair

Final UI Placement:
TBD

Descrição:
Identificar sessões de Modern Standby com residência inadequada em low-power state.

Possível objetivo:
Detectar standby energeticamente ineficiente.

Possível benefício:
Direcionar investigação para driver, dispositivo ou software culpado.

Aplicabilidade:
Modern Standby PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Dúvidas:
- Threshold deve ser contextual?
- Testes de engenharia da Microsoft usam métricas específicas, mas o produto não deve transformar um número isolado em regra universal.

---

# 80. Modern Standby Offender Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Modern Standby

Primary Product Area:
Energy

Possible Consumers:
- Repair
- Laptop

Final UI Placement:
TBD

Descrição:
Identificar componentes associados à atividade excessiva durante Modern Standby.

Possível objetivo:
Descobrir por que o sistema não consegue permanecer em low-power state.

Possível benefício:
Encontrar:
- drivers;
- USB;
- rede;
- software;
- firmware;
- interrupts;
- outros dispositivos problemáticos.

Aplicabilidade:
Modern Standby PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic Only

---

# 81. Modern Standby Driver Problem Detection

Status:
CANDIDATE

Type:
Diagnostic / Repair

Technical Domain:
Modern Standby / Drivers

Primary Product Area:
Repair

Possible Consumers:
- Energy
- Drivers
- Laptop

Final UI Placement:
TBD

Descrição:
Relacionar baixa residência DRIPS a drivers ausentes ou problemáticos quando houver evidência suficiente.

Possível objetivo:
Distinguir defeito de política de energia de problema real de driver.

Possível benefício:
Direcionar reparo correto.

Aplicabilidade:
Modern Standby PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established diagnostic pattern

Action Mode:
Diagnostic

---

# 82. Standby Drain Session Comparison

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Modern Standby / Battery

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Benchmark

Final UI Placement:
TBD

Descrição:
Comparar múltiplas sessões de suspensão para identificar padrões de drenagem.

Possível objetivo:
Evitar conclusões baseadas em uma única noite.

Possível benefício:
Detectar recorrência e influência de determinadas condições.

Aplicabilidade:
Modern Standby laptops

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Diagnostic

Possíveis comparações:
- Wi-Fi ON × OFF;
- AC × DC;
- aplicação instalada × removida;
- driver antigo × novo;
- before × after optimization.

---

# 83. Standby Energy Regression Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Modern Standby

Primary Product Area:
Energy

Possible Consumers:
- Drivers
- Windows Update
- Optimization

Final UI Placement:
TBD

Descrição:
Detectar piora significativa de consumo em standby após alteração conhecida.

Possível objetivo:
Relacionar regressões a driver, update ou software.

Possível benefício:
Facilitar investigação de “a bateria começou a acabar durante a noite”.

Aplicabilidade:
Battery Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Measurement-based

Action Mode:
Diagnostic

---

# 84. Hybrid Graphics Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU / Power

Primary Product Area:
Shared

Possible Consumers:
- GPU
- Energy
- Laptop
- Gaming

Final UI Placement:
TBD

Descrição:
Detectar sistemas com mais de uma GPU e possibilidade de escolher entre GPU de menor consumo e GPU de maior desempenho.

Possível objetivo:
Permitir decisões energéticas específicas por aplicativo.

Possível benefício:
Reduzir uso desnecessário da dGPU.

Aplicabilidade:
Hybrid Graphics PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

---

# 85. Application GPU Power Preference

Status:
CANDIDATE

Type:
Diagnostic / Configuration

Technical Domain:
GPU / Power

Primary Product Area:
Energy

Possible Consumers:
- GPU
- Gaming Profiles
- Laptop

Final UI Placement:
TBD

Descrição:
Investigar preferência de GPU por aplicativo entre menor consumo e maior desempenho.

Possível objetivo:
Evitar que aplicações leves utilizem GPU dedicada desnecessariamente ou que jogos utilizem GPU inadequada.

Possível benefício:
Autonomia e desempenho contextual.

Aplicabilidade:
Multi-GPU Systems

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Established mechanism

Action Mode:
Contextual User Choice

Possíveis conceitos:
- minimum-power GPU;
- high-performance GPU;
- unspecified/system choice.

---

# 86. Unnecessary dGPU Activation Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
GPU / Power

Primary Product Area:
Energy

Possible Consumers:
- GPU
- Processes
- Laptop
- Monitoring

Final UI Placement:
TBD

Descrição:
Detectar aplicações leves ou background workloads mantendo a GPU dedicada ativa sem benefício aparente.

Possível objetivo:
Diagnosticar autonomia ruim e idle power elevado em notebooks híbridos.

Possível benefício:
Permitir que dGPU volte a estado de baixo consumo quando não necessária.

Aplicabilidade:
Hybrid Graphics Laptops

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Hardware/vendor-dependent

Action Mode:
Diagnostic First

Possíveis culpados:
- browsers;
- launchers;
- overlays;
- monitoring tools;
- RGB software;
- hardware acceleration;
- external displays;
- background applications.

Dúvidas:
- Como determinar que a dGPU está realmente impedindo deep idle?
- APIs dependem do fabricante?

---

# 87. GPU Power Preference Recommendation

Status:
IDEA

Type:
Optimization

Technical Domain:
GPU / Power

Primary Product Area:
Energy

Possible Consumers:
- Gaming Profiles
- Laptop

Final UI Placement:
TBD

Descrição:
Recomendar GPU de baixa potência ou alta performance conforme tipo de aplicação.

Possível objetivo:
Equilibrar autonomia e desempenho.

Possível benefício:
Grande potencial em notebooks com iGPU + dGPU.

Aplicabilidade:
Hybrid Graphics PCs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Context-dependent

Action Mode:
Recommendation / User Choice

Importante:
Não classificar automaticamente aplicações desconhecidas sem evidência.

---

# 88. Display Refresh Rate Energy Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Display / Power

Primary Product Area:
Energy

Possible Consumers:
- Display
- Laptop
- Gaming

Final UI Placement:
TBD

Descrição:
Considerar taxa de atualização do display como componente do consumo energético.

Possível objetivo:
Detectar situações em que alta frequência é mantida desnecessariamente durante bateria.

Possível benefício:
Maior autonomia.

Aplicabilidade:
High-refresh-rate Laptops

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 89. Dynamic Refresh Rate Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Display / Power

Primary Product Area:
Energy

Possible Consumers:
- Display
- Laptop

Final UI Placement:
TBD

Descrição:
Detectar suporte e estado do Dynamic Refresh Rate do Windows 11.

Possível objetivo:
Permitir que refresh rate varie conforme atividade.

Possível benefício:
Combinar fluidez quando necessária com menor consumo em conteúdo estático.

Aplicabilidade:
Supported Windows 11 Displays

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic Diagnostic

---

# 90. Dynamic Refresh Rate Recommendation

Status:
CANDIDATE

Type:
Configuration / Recommendation

Technical Domain:
Display / Power

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Display

Final UI Placement:
TBD

Descrição:
Recomendar DRR quando suportado e adequado ao cenário.

Possível objetivo:
Evitar manter refresh rate máximo permanentemente.

Possível benefício:
Maior duração de bateria sem sacrificar completamente fluidez.

Aplicabilidade:
Supported Laptops

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established feature

Action Mode:
User Choice

---

# 91. Battery Refresh Rate Profile

Status:
IDEA

Type:
Configuration

Technical Domain:
Display / Power

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Gaming Profiles

Final UI Placement:
TBD

Descrição:
Investigar uso de refresh rate distinto em AC e bateria.

Possível objetivo:
Priorizar autonomia desconectado e fluidez conectado.

Possível benefício:
Economia significativa em displays de alta frequência dependendo do hardware.

Aplicabilidade:
High-refresh laptops

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established principle / implementation research needed

Action Mode:
User Choice

Exemplo:
AC:
165 Hz

DC:
60/90 Hz ou DRR

Importante:
Não aplicar sem preferência explícita.

---

# 92. Adaptive Brightness Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Display / Power

Primary Product Area:
Energy

Possible Consumers:
- Display
- Laptop

Final UI Placement:
TBD

Descrição:
Detectar suporte a brilho adaptativo baseado em sensor de luz.

Possível objetivo:
Entender comportamento automático do display.

Possível benefício:
Energia e experiência visual.

Aplicabilidade:
Supported Displays

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

---

# 93. Content Adaptive Brightness Detection

Status:
CANDIDATE

Type:
Diagnostic / Configuration

Technical Domain:
Display / Power

Primary Product Area:
Energy

Possible Consumers:
- Display
- Laptop

Final UI Placement:
TBD

Descrição:
Detectar recursos que ajustam brilho com base no conteúdo exibido.

Possível objetivo:
Explicar alterações perceptíveis de luminância e sua relação com economia energética.

Possível benefício:
Permitir escolha entre fidelidade visual e eficiência.

Aplicabilidade:
Supported Displays

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
User Choice

Importante:
Pode ser indesejável para trabalhos sensíveis a cor, mesmo quando economiza energia.

---

# 94. Display Power Consumption Advisor

Status:
IDEA

Type:
Diagnostic / Recommendation

Technical Domain:
Display / Power

Primary Product Area:
Energy

Possible Consumers:
- Laptop
- Display

Final UI Placement:
TBD

Descrição:
Consolidar fatores do display que influenciam consumo.

Possível objetivo:
Mostrar ao usuário que tela pode representar parte importante do gasto energético.

Possível benefício:
Recomendações mais inteligentes para autonomia.

Aplicabilidade:
Laptops

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established principles

Action Mode:
Recommendation

Possíveis fatores:
- brightness;
- refresh rate;
- DRR;
- HDR;
- display timeout;
- adaptive brightness;
- content adaptive brightness;
- OLED-specific behavior quando aplicável.

---

# 95. OLED Energy Context

Status:
IDEA

Type:
Diagnostic / Recommendation

Technical Domain:
Display / Power

Primary Product Area:
Energy

Possible Consumers:
- Display
- Laptop

Final UI Placement:
TBD

Descrição:
Considerar tipo de painel ao avaliar determinadas recomendações energéticas.

Possível objetivo:
Evitar tratar todos os displays de forma idêntica.

Possível benefício:
Recomendações mais corretas.

Aplicabilidade:
OLED-capable devices

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent

Action Mode:
Recommendation

Exemplo:
Dark Mode pode ter impacto energético diferente em OLED em comparação com LCD.

---

# 96. Energy Saver Background Activity Impact

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Energy Saver / Processes

Primary Product Area:
Energy

Possible Consumers:
- Background Apps
- Processes
- Laptop

Final UI Placement:
TBD

Descrição:
Investigar como Energy Saver altera comportamento de processos e tarefas background.

Possível objetivo:
Explicar diferenças de performance observadas quando Energy Saver está ativo.

Possível benefício:
Melhor troubleshooting e expectativa do usuário.

Aplicabilidade:
Supported Windows Versions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept

Action Mode:
Diagnostic

---

# 97. Energy Saver Automatic Activation Policy

Status:
CANDIDATE

Type:
Configuration

Technical Domain:
Energy Saver

Primary Product Area:
Energy

Possible Consumers:
- Laptop

Final UI Placement:
TBD

Descrição:
Detectar e permitir ajuste de condições de ativação automática do Energy Saver quando suportado.

Possível objetivo:
Adequar comportamento ao nível de bateria desejado.

Possível benefício:
Maior autonomia.

Aplicabilidade:
Supported PCs

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
User Choice

---

# 98. Energy Optimization Conflict Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Power / System

Primary Product Area:
Energy

Possible Consumers:
- Optimization
- Repair

Final UI Placement:
TBD

Descrição:
Detectar quando múltiplos mecanismos ou aplicações parecem disputar controle sobre energia/desempenho.

Possível objetivo:
Investigar máquinas cujo comportamento energético muda de maneira imprevisível.

Possível benefício:
Evitar que BeadWise concorra com software OEM ou ferramentas de tuning.

Aplicabilidade:
Affected PCs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Product concept

Action Mode:
Diagnostic First

Possíveis atores:
- Windows Power Mode;
- OEM utility;
- GPU software;
- laptop performance mode;
- gaming utility;
- custom power plan;
- BeadWise profile.

---

# 99. Energy State Timeline

Status:
IDEA

Type:
Monitoring / Diagnostic

Technical Domain:
Power / Telemetry

Primary Product Area:
Monitoring

Possible Consumers:
- Energy
- Benchmark
- Gaming

Final UI Placement:
TBD

Descrição:
Registrar alterações relevantes de estado energético em uma linha temporal.

Possível objetivo:
Correlacionar performance com mudanças de política.

Possível benefício:
Diagnóstico de comportamento dinâmico.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product concept using established telemetry

Action Mode:
Monitoring

Possíveis eventos:
- AC → DC;
- DC → AC;
- power mode change;
- Energy Saver activation;
- thermal limit;
- power limit;
- GPU activation;
- sleep/wake;
- display refresh change.

---

# 100. Energy Recommendation Confidence

Status:
IDEA

Type:
Product Behavior

Technical Domain:
Energy / Shared

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Energy

Final UI Placement:
TBD

Descrição:
Classificar recomendações de energia conforme força da evidência e especificidade daquele hardware.

Possível objetivo:
Distinguir configuração suportada pelo Windows de otimização realmente demonstrada naquele PC.

Possível benefício:
Evitar recomendações excessivamente confiantes.

Aplicabilidade:
Application-wide

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product principle

Action Mode:
Internal

Possíveis níveis:
- Established / directly detected;
- Hardware-specific;
- Benchmark-supported;
- Context-dependent;
- Experimental;
- Unsupported / reject.

---

# Consolidações com a primeira passada

Algumas descobertas não precisam virar novas features porque já estão cobertas.

## Power Requests

Já coberto por:
- 21. Power Requests Detection
- 22. Power Request Override Advisor

A segunda passada apenas reforça que Power Requests devem ser tratados como evidência de uma necessidade de manter sistema/display ativo, não como algo a remover automaticamente.

---

## Modern Standby

Os itens anteriores:
- 24. Modern Standby Detection
- 25. Modern Standby Quality Analysis
- 26. Sleep Transition Diagnostics

permanecem.

A segunda passada adiciona granularidade real através de:
- DRIPS;
- offender detection;
- driver problems;
- session comparison;
- regression analysis.

---

## Application Energy Usage

Já coberto em:
- 44. Historical Energy Usage
- 45. Energy Consumption by Application

Não criar outra feature apenas porque SRUM possui mais métricas.

---

## Performance-per-Watt

Continua em:
- 55. Performance-per-Watt Benchmark
- 56. Thermal / Power Efficiency Comparison

A segunda passada reforça sua importância, mas não exige duplicatas.

---

# Ideias avaliadas e NÃO transformadas em otimização padrão

## Timer Resolution = 0.5 ms ou 1 ms permanentemente

Não.

Maior resolução de timer pode aumentar precisão de waits, mas também elevar atividade do scheduler e dificultar estados de economia da CPU.

Deve ser:
- detectável;
- mensurável;
- workload-specific;
- benchmarkable.

---

## Desativar Power Throttling globalmente

Não.

O Windows utiliza mecanismos de QoS justamente para executar tarefas não críticas de forma energeticamente eficiente.

---

## Desativar EcoQoS

Não como padrão.

EcoQoS pode reduzir temperatura, consumo e ruído para workloads background.

---

## Best Performance permanentemente em notebooks

Não.

Pode ser adequado conectado à tomada ou em workload crítico, mas não deve ser automaticamente mantido em bateria.

---

## Forçar High Performance quando Best Performance já está efetivo

Não.

Plano e modo de energia não devem ser tratados como simples níveis crescentes de uma mesma escala.

---

## Desativar todas as economias de display

Não.

Brilho, DRR, refresh rate e adaptive brightness possuem impacto real sobre consumo.

---

## dGPU para todos os aplicativos

Não.

Em notebooks híbridos pode causar enorme desperdício energético sem qualquer vantagem para aplicações leves.

---

## iGPU para tudo

Também não.

Jogos e workloads gráficos podem exigir a GPU de alto desempenho.

---

# Novos princípios consolidados

1. **Power Scheme não é Power Mode.**

2. **User Power Mode não é necessariamente Effective Power Mode.**

3. O Windows moderno possui múltiplas camadas de gerenciamento energético.

4. OEMs podem alterar comportamento por firmware, drivers, INF e software.

5. Não devemos sobrescrever essas políticas sem entender o hardware.

6. Eficiência pode ser aplicada por processo, não apenas globalmente.

7. Trabalho background e foreground não precisam receber a mesma política.

8. Alta resolução de timer tem custo energético potencial.

9. Menor timer resolution não significa automaticamente menor input lag.

10. Modern Standby deve ser avaliado por comportamento real durante a sessão.

11. “Tela desligada” não significa necessariamente “PC em deep sleep”.

12. DRIPS é uma métrica importante de qualidade de Modern Standby.

13. Drivers e dispositivos podem impedir estados profundos de economia.

14. A GPU dedicada pode ser causa relevante de idle power em notebooks híbridos.

15. Refresh rate também é variável energética.

16. DRR oferece uma alternativa contextual a simplesmente reduzir refresh permanentemente.

17. Brilho e comportamento adaptativo de display pertencem à análise energética.

18. Desempenho máximo absoluto não é necessariamente configuração ideal.

19. Energia deve ser avaliada juntamente com:
   - desempenho;
   - temperatura;
   - ruído;
   - autonomia;
   - estabilidade.

20. Toda alteração agressiva deve provar que entrega benefício útil.

---

# Resultado da segunda passada

Primeira passada:
~60 capacidades/hipóteses.

Segunda passada:
~40 novos itens, subcapacidades e princípios.

Total bruto provisório:
~100 itens.

Esse número NÃO representa 100 features finais.

Muitos itens deverão ser consolidados durante especificação.

---

# Áreas que agora parecem suficientemente cobertas

## Power Plans
Cobertura forte.

## Modern Power Modes
Cobertura forte após segunda passada.

## CPU Power Management
Cobertura suficiente para Energia.
Detalhes profundos serão tratados novamente em CPU.

## Sleep / Wake
Cobertura forte.

## Modern Standby
Cobertura forte.

## Battery
Cobertura inicial forte.
Pode receber aprofundamento específico futuramente.

## Device Power
Cobertura inicial suficiente.
USB, Network, PCIe e GPU serão aprofundados nas respectivas áreas.

## Energy Telemetry
Cobertura forte.

## Display Energy
Cobertura inicial suficiente.
Display terá investigação própria.

## Process Energy
Cobertura inicial suficiente.
Processos terá investigação própria.

---

# Ideias que devem migrar para pesquisas futuras

## CPU
- EPP profundo;
- CPPC;
- HWP;
- boost;
- core parking;
- heterogeneous CPU;
- core classes;
- idle states;
- package power;
- power limits.

## GPU
- dGPU residency;
- board power;
- hybrid graphics;
- GPU preference;
- GPU low-power states.

## Display
- DRR;
- brightness;
- CABC;
- HDR power behavior;
- OLED.

## Network
- adapter power management;
- Wi-Fi power saving;
- Ethernet EEE;
- wake-on-LAN.

## Storage
- disk idle;
- NVMe power states;
- ASPM;
- APST.

## USB
- selective suspend;
- device wake;
- controller power states.

## Latency
- timer resolution;
- scheduler timers;
- HPET;
- Dynamic Tick;
- interrupt behavior.

---

# Questões que permanecem abertas para especificação técnica

- Como ler todos os parâmetros relevantes de Power Mode de forma suportada?
- Quais APIs modernas substituem parsing de `powercfg`?
- É possível consultar EcoQoS de processos externos?
- Qual telemetria permite identificar aplicações solicitando timer resolution?
- Como correlacionar timer resolution com energia real?
- Como detectar residência real de package C-states em Intel e AMD?
- Quais sensores de energia são portáveis?
- Quanto de package power vem de API/vendor-specific telemetry?
- Como detectar dGPU mantida ativa em NVIDIA/AMD/Intel?
- Como tratar MUX switches?
- Como detectar Advanced Optimus/SmartAccess Graphics equivalentes?
- OEM utilities podem sobrescrever Power Mode após BeadWise?
- Como detectar esses conflitos?
- Quais recursos de display podem ser configurados por API padrão?
- Quanto de Refresh Rate switching pode ser automatizado com segurança?
- Como evitar conflito com GPU driver/OEM software?
- Como medir consumo em desktop sem bateria e sem wattmeter externo?
- Performance-per-watt pode ser confiável usando apenas sensores internos?
- Quais recomendações exigem benchmark A/B?
- Qual duração mínima torna um teste energético estatisticamente útil?

---

# Avaliação de maturidade da área Energia

A primeira passada encontrou os mecanismos tradicionais.

A segunda adicionou principalmente:
- camadas modernas de Power Mode;
- EcoQoS;
- timers;
- DRIPS;
- GPU híbrida;
- display.

Nenhum novo grande domínio energético parece ausente.

Pesquisas adicionais neste momento provavelmente começarão a produzir principalmente:
- opções individuais de `powercfg`;
- parâmetros específicos de CPU;
- parâmetros específicos de hardware;
- funcionalidades que pertencem às próximas áreas.

Isso é sinal de saturação suficiente para discovery inicial.

Estado sugerido:

**INITIAL DISCOVERY COMPLETE**