# BeadWise / PC Optimizer V2 — Network Discovery

## First Pass — Broad Discovery

Status da área: FIRST PASS COMPLETE

Objetivo desta passada:

Descobrir capabilities relevantes para conectividade, desempenho, diagnóstico, reparo e configuração de rede no Windows sem transformar cada propriedade avançada de adaptador, parâmetro TCP ou valor de Registry em uma feature separada.

Princípio da área:

> Network optimization deve começar por identificar caminho, interface, link, configuração, perda, latência e saturação antes de recomendar qualquer alteração.

Fluxo conceitual:

**Detect → Identify Path → Measure → Diagnose → Recommend → Apply Contextually → Validate → Keep/Rollback**

Regras específicas:

- não assumir que menor ping sintético significa melhor experiência real;
- não usar "internet booster" como conceito técnico;
- não desativar IPv6 como otimização universal;
- não aplicar valores mágicos de MTU;
- não desativar TCP Auto-Tuning indiscriminadamente;
- não aplicar `TcpAckFrequency`, `TCPNoDelay`, `NetworkThrottlingIndex` ou tweaks semelhantes como preset universal;
- propriedades de NIC são hardware/driver-specific;
- throughput, latency, jitter e packet loss medem aspectos diferentes;
- DNS pode alterar tempo de resolução, mas não reduz magicamente a latência do caminho após a conexão estar estabelecida;
- Network Reset é reparo de último recurso;
- alterações relevantes devem ser reversíveis quando possível.

---

# 1. Network Adapter Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Hardware

Primary Product Area: My PC

Possible Consumers:

- Optimization
- Network Diagnostics
- Monitoring
- Drivers
- Energy

Final UI Placement: TBD

Descrição: Identificar adaptadores de rede físicos e lógicos presentes no sistema.

Possível objetivo: Servir como base para capabilities condicionais de Ethernet, Wi-Fi, VPN, virtualização e mobile broadband.

Possível benefício: Evitar aplicar recomendações ao adaptador errado ou tratar interfaces virtuais como hardware físico.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- adapter name;
- interface description;
- interface index;
- interface GUID;
- hardware/vendor;
- physical vs virtual;
- Ethernet/Wi-Fi/mobile broadband;
- MAC address;
- operational state;
- driver;
- link speed;
- media type;
- interface metric;
- hidden/disabled adapters quando relevante.

---

# 2. Active Network Path Identification

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Routing

Primary Product Area: Shared

Possible Consumers:

- Network Diagnostics
- Gaming
- Monitoring
- Benchmark
- Repair

Final UI Placement: TBD

Descrição: Identificar qual interface, gateway e rota estão efetivamente transportando o tráfego atual.

Possível objetivo: Diferenciar adaptador instalado de adaptador realmente utilizado.

Possível benefício: Impedir diagnóstico de Wi-Fi quando o tráfego está saindo por Ethernet, VPN ou outra interface.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis dados:

- default route;
- active interface;
- next hop;
- route metric;
- interface metric;
- IPv4 path;
- IPv6 path;
- VPN/tunnel path;
- multiple default routes;
- route changes.

---

# 3. Network Configuration Baseline

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Configuration

Primary Product Area: Network

Possible Consumers:

- Repair
- Optimization
- My PC
- Diagnostics

Final UI Placement: TBD

Descrição: Registrar a configuração de rede atual antes de diagnosticar ou alterar qualquer parâmetro.

Possível objetivo: Criar baseline reutilizável para troubleshooting, comparação e rollback.

Possível benefício: Detectar configurações incomuns e preservar o estado original.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- DHCP/static IP;
- IPv4 address;
- IPv6 address;
- subnet/prefix;
- default gateway;
- DNS servers;
- DNS suffix;
- DHCP server;
- interface metric;
- proxy;
- network profile;
- MTU;
- active routes.

---

# 4. End-to-End Connectivity Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Connectivity

Primary Product Area: Repair

Possible Consumers:

- Network
- Gaming
- Monitoring
- Recommendation Engine

Final UI Placement: TBD

Descrição: Testar progressivamente os diferentes níveis necessários para uma conexão funcional.

Possível objetivo: Localizar em qual estágio a conectividade está falhando.

Possível benefício: Evitar usar "sem internet" como diagnóstico único.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis estágios:

- adapter operational;
- link established;
- valid IP configuration;
- default gateway reachable;
- DNS resolution;
- external IP connectivity;
- HTTPS connectivity;
- Internet/NCSI state;
- captive portal suspicion.

Possíveis resultados:

- No Link;
- No Address;
- Gateway Failure;
- DNS Failure;
- Internet Path Failure;
- Captive Portal;
- Connected.

---

# 5. Latency, Jitter & Packet Loss Diagnostics

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Network Quality

Primary Product Area: Network

Possible Consumers:

- Gaming
- Monitoring
- Benchmark
- Repair

Final UI Placement: TBD

Descrição: Medir latência, variação temporal e perda de pacotes para destinos apropriados.

Possível objetivo: Diferenciar conexão simplesmente funcional de conexão estável e responsiva.

Possível benefício: Detectar problemas que throughput tests podem esconder.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis métricas:

- round-trip latency;
- minimum/average/maximum latency;
- jitter;
- packet loss;
- timeout rate;
- sample count;
- test duration;
- IPv4 vs IPv6;
- gateway latency;
- Internet latency.

Observação:

Um único ping não é evidência suficiente de qualidade de conexão.

---

# 6. Route & Hop Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Routing

Primary Product Area: Network

Possible Consumers:

- Repair
- Gaming
- Advanced Diagnostics

Final UI Placement: TBD

Descrição: Inspecionar o caminho entre o PC e um destino para identificar alterações, falhas ou aumento de latência ao longo da rota.

Possível objetivo: Separar problema local de problema fora da máquina.

Possível benefício: Evitar aplicar tweaks no Windows quando a anomalia está no roteador, ISP ou rota externa.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis dados:

- hop sequence;
- per-hop latency;
- timeout behavior;
- route change;
- IPv4/IPv6 route;
- local gateway;
- destination reachability.

Observação:

ICMP rate limiting ou ausência de resposta em um hop não prova que aquele roteador está causando packet loss no tráfego encaminhado.

---

# 7. DNS Configuration & Resolution Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: DNS

Primary Product Area: Network

Possible Consumers:

- Repair
- Privacy
- Benchmark
- Gaming

Final UI Placement: TBD

Descrição: Avaliar configuração e funcionamento da resolução de nomes no Windows.

Possível objetivo: Detectar falhas ou atrasos de DNS sem confundir resolução de nome com latência do tráfego de aplicação já conectado.

Possível benefício: Corrigir sites/serviços que falham por DNS e identificar servidores inconsistentes.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- configured DNS servers;
- source DHCP/manual;
- IPv4/IPv6 resolvers;
- resolution success;
- lookup latency;
- timeout;
- cache behavior;
- suffix/search list;
- adapter-specific DNS;
- secure DNS capability.

Regra:

Trocar DNS pode melhorar resolução ou confiabilidade, mas não deve ser vendido como redução universal de ping em jogos.

---

# 8. Encrypted DNS / DoH Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: DNS Privacy

Primary Product Area: Privacy

Possible Consumers:

- Network
- Security
- Settings

Final UI Placement: TBD

Descrição: Detectar suporte e uso de DNS criptografado, especialmente DNS over HTTPS em versões compatíveis do Windows.

Possível objetivo: Diferenciar escolha de privacidade/transporte DNS de otimização de performance.

Possível benefício: Informar quando consultas DNS podem usar transporte criptografado e evitar recomendações contraditórias.

Aplicabilidade: Specific Windows Version

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- DoH-capable resolver;
- encrypted vs plaintext DNS;
- fallback behavior quando identificável;
- per-interface configuration;
- manual/automatic resolver configuration.

Observação:

DoH altera o transporte das consultas DNS; não é um "latency tweak" universal.

---

# 9. DNS Resolver Performance & Reliability Comparison

Status: CANDIDATE

Type: Benchmark

Technical Domain: DNS

Primary Product Area: Benchmark

Possible Consumers:

- Network
- Privacy
- Recommendation Engine

Final UI Placement: TBD

Descrição: Comparar resolvers DNS sob condições controladas para disponibilidade, consistência e tempo de resolução.

Possível objetivo: Ajudar o usuário a escolher um resolver funcional para sua rede.

Possível benefício: Detectar resolver lento, indisponível ou inconsistente.

Aplicabilidade: Optional

Shared Capability: No

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Benchmark First

Possíveis métricas:

- lookup latency;
- timeout rate;
- cached vs uncached behavior;
- IPv4/IPv6 resolution;
- multiple domains;
- repeated samples;
- encrypted/unencrypted path quando aplicável.

Não fazer:

- escolher "melhor DNS para gaming" por um único teste;
- prometer ganho de FPS ou redução permanente do RTT do servidor de jogo.

---

# 10. Ethernet Link Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Ethernet

Primary Product Area: Network

Possible Consumers:

- My PC
- Benchmark
- Repair
- Drivers

Final UI Placement: TBD

Descrição: Diagnosticar estado e capacidade do link Ethernet negociado.

Possível objetivo: Detectar limitações físicas ou de negociação antes de mexer na pilha TCP.

Possível benefício: Encontrar casos de conexão Gigabit/2.5GbE operando abaixo do esperado.

Aplicabilidade: Ethernet PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- link state;
- negotiated speed;
- full/half duplex quando exposto;
- media type;
- adapter capability;
- cable/link changes;
- error/discard counters quando disponíveis;
- reconnect history.

---

# 11. Ethernet Negotiation & Duplex Anomaly Detection

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Ethernet

Primary Product Area: Network

Possible Consumers:

- Repair
- Drivers
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Detectar sinais de negociação inadequada, duplex incorreto ou link abaixo da capacidade esperada.

Possível objetivo: Encontrar problemas físicos/configuracionais que podem causar baixo throughput, retransmissões ou instabilidade.

Possível benefício: Evitar "otimizar TCP" quando o problema está no link.

Aplicabilidade: Ethernet PCs

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis sinais:

- negotiated speed unexpectedly low;
- half-duplex state quando aplicável;
- frequent link renegotiation;
- error counters;
- cable/device capability mismatch;
- forced Speed & Duplex property.

Regra:

Auto Negotiation deve ser tratado como comportamento normal; forçar velocidade/duplex exige motivo específico.

---

# 12. Wi-Fi Capability & Connection Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Wi-Fi

Primary Product Area: Network

Possible Consumers:

- My PC
- Gaming
- Repair
- Drivers
- Energy

Final UI Placement: TBD

Descrição: Identificar capacidades do adaptador Wi-Fi e características da conexão atual.

Possível objetivo: Criar contexto técnico para diagnosticar throughput, latência, roaming e estabilidade wireless.

Possível benefício: Diferenciar limitação de sinal/PHY de problema da pilha de rede.

Aplicabilidade: Wi-Fi PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- Wi-Fi adapter;
- radio/PHY capabilities;
- standard in use;
- band;
- channel;
- channel width;
- link rates;
- signal quality;
- BSSID;
- security type;
- authentication;
- roaming state;
- driver;
- connected SSID.

---

# 13. Wi-Fi Signal & Radio Environment Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Wi-Fi RF

Primary Product Area: Network

Possible Consumers:

- Gaming
- Repair
- Recommendation Engine

Final UI Placement: TBD

Descrição: Avaliar qualidade do sinal e contexto de rádio da conexão Wi-Fi.

Possível objetivo: Identificar quando baixa qualidade de conexão pode estar associada a sinal, banda, canal ou ambiente wireless.

Possível benefício: Direcionar recomendações físicas/configuracionais em vez de tweaks TCP.

Aplicabilidade: Wi-Fi PCs

Shared Capability: No

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis dados:

- signal quality/RSSI quando disponível;
- band;
- channel;
- channel width;
- negotiated receive/transmit rate;
- nearby network density quando acessível;
- roaming events;
- disconnect/reconnect;
- link-rate fluctuation.

Observação:

O Windows não expõe necessariamente uma visão completa do espectro RF; o diagnóstico deve respeitar os dados realmente disponíveis.

---

# 14. Wi-Fi Connection History & WLAN Report

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Wi-Fi Stability

Primary Product Area: Network

Possible Consumers:

- Repair
- Drivers
- Monitoring

Final UI Placement: TBD

Descrição: Utilizar histórico de sessões Wi-Fi e eventos do Windows para analisar desconexões e falhas recorrentes.

Possível objetivo: Diagnosticar problemas intermitentes que não aparecem em um snapshot atual.

Possível benefício: Encontrar padrões de disconnect, driver failure ou authentication issue.

Aplicabilidade: Wi-Fi PCs

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis dados:

- session history;
- connection/disconnection events;
- disconnect reasons;
- adapter list;
- driver information;
- network-related command outputs;
- failures over recent history.

Observação:

O Windows fornece `netsh wlan show wlanreport`, que gera relatório de sessões Wi-Fi e eventos recentes.

---

# 15. Network Driver & Device Health Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Drivers

Primary Product Area: Shared

Possible Consumers:

- Drivers
- Repair
- Network
- Stability

Final UI Placement: TBD

Descrição: Correlacionar versão/estado do driver com funcionamento dos adaptadores.

Possível objetivo: Detectar device errors, drivers ausentes ou regressões sem implementar um atualizador de drivers indiscriminado.

Possível benefício: Separar problema de driver de configuração da rede.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- provider;
- version;
- date;
- device status;
- error code;
- recent driver update;
- rollback availability;
- generic vs vendor driver quando identificável;
- adapter restart events.

---

# 16. Network Adapter Advanced Property Inventory

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Adapter Configuration

Primary Product Area: Network

Possible Consumers:

- Optimization
- Drivers
- Advanced Diagnostics

Final UI Placement: TBD

Descrição: Inventariar propriedades avançadas expostas pelo driver da NIC sem assumir que todos os adaptadores possuem os mesmos nomes ou valores.

Possível objetivo: Criar base hardware-aware para análises específicas.

Possível benefício: Evitar Registry tweak packs e permitir interpretar a configuração realmente suportada pelo driver.

Aplicabilidade: Specific Hardware

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis categorias de propriedades:

- RSS;
- RSC;
- checksum offload;
- LSO;
- interrupt moderation;
- flow control;
- Energy Efficient Ethernet;
- power-saving options;
- VLAN/QoS;
- jumbo frames;
- speed/duplex;
- receive/transmit buffers;
- vendor-specific properties.

Regra:

Uma propriedade existente != recomendação para alterá-la.

---

# 17. Receive Side Scaling Diagnostics

Status: CANDIDATE

Type: Diagnostic / Optimization

Technical Domain: Network Processing

Primary Product Area: Network

Possible Consumers:

- Performance
- CPU
- Benchmark
- Advanced Diagnostics

Final UI Placement: TBD

Descrição: Detectar capacidade e configuração de Receive Side Scaling (RSS).

Possível objetivo: Identificar se processamento de recepção pode ser distribuído adequadamente entre CPUs em workloads de rede relevantes.

Possível benefício: Encontrar bottlenecks de processamento em links rápidos ou cargas intensas.

Aplicabilidade: Supported Hardware

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Benchmark First

Possíveis informações:

- RSS supported;
- RSS enabled;
- receive queues;
- processor distribution;
- maximum processors;
- profile/configuration;
- CPU/DPC load under network traffic.

Observação:

Mais CPUs RSS não significa automaticamente melhor desempenho; aumenta também overhead e deve ser contextualizado.

---

# 18. Receive Segment Coalescing Diagnostics

Status: CANDIDATE

Type: Diagnostic / Optimization

Technical Domain: Network Offload

Primary Product Area: Network

Possible Consumers:

- Benchmark
- Latency
- Throughput

Final UI Placement: TBD

Descrição: Detectar suporte e estado de Receive Segment Coalescing (RSC).

Possível objetivo: Avaliar trade-off entre redução de overhead e requisitos específicos de workloads de baixa latência.

Possível benefício: Evitar recomendação universal de ligar ou desligar coalescing.

Aplicabilidade: Supported Hardware

Shared Capability: No

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Benchmark First

Possíveis informações:

- hardware capability;
- RSC state;
- coalescing counters quando disponíveis;
- throughput;
- CPU usage;
- latency under load;
- virtualization interaction.

---

# 19. Network Offload Capability Diagnostics

Status: CANDIDATE

Type: Diagnostic / Optimization

Technical Domain: Network Offload

Primary Product Area: Network

Possible Consumers:

- Benchmark
- CPU
- Drivers
- Advanced Diagnostics

Final UI Placement: TBD

Descrição: Avaliar de forma consolidada offloads de processamento suportados pelo adaptador e pela pilha.

Possível objetivo: Entender quando a NIC está assumindo trabalho que de outra forma seria realizado pela CPU.

Possível benefício: Diagnosticar throughput, CPU overhead ou incompatibilidades sem criar uma feature para cada flag.

Aplicabilidade: Supported Hardware

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Benchmark First

Possíveis mecanismos:

- TCP checksum offload;
- UDP checksum offload;
- IPv4/IPv6 checksum;
- Large Send Offload;
- segmentation offload;
- receive coalescing;
- protocol offloads;
- virtualization-related offloads.

Regra:

Não desabilitar offloads globalmente em nome de "menor ping".

---

# 20. Interrupt Moderation & Network DPC Correlation

Status: CANDIDATE

Type: Diagnostic / Optimization

Technical Domain: Network Latency

Primary Product Area: Network

Possible Consumers:

- DPC Diagnostics
- Gaming
- Audio
- Benchmark

Final UI Placement: TBD

Descrição: Correlacionar interrupt moderation, interrupções da NIC e DPC/ISR com latência e CPU overhead.

Possível objetivo: Investigar workloads realmente sensíveis a latência sem presumir que "interrupt moderation OFF" é melhor para todos.

Possível benefício: Encontrar equilíbrio entre interrupt rate, CPU cost e latency.

Aplicabilidade: Supported Hardware / Latency-sensitive workloads

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Benchmark First

Possíveis sinais:

- interrupt moderation state;
- interrupt/DPC rate;
- driver DPC latency;
- CPU utilization;
- packet rate;
- throughput;
- latency under load.

---

# 21. TCP Stack Configuration Audit

Status: CANDIDATE

Type: Diagnostic

Technical Domain: TCP/IP

Primary Product Area: Network

Possible Consumers:

- Optimization
- Repair
- Benchmark
- Advanced Diagnostics

Final UI Placement: TBD

Descrição: Registrar o estado das principais configurações modernas da pilha TCP antes de considerar alterações.

Possível objetivo: Detectar configurações realmente não padrão ou quebradas sem aplicar tweak packs.

Possível benefício: Identificar alterações legadas deixadas por scripts, softwares ou tutoriais antigos.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- TCP global settings;
- receive window auto-tuning;
- congestion control/provider;
- ECN state;
- timestamps quando expostos;
- RSC state;
- chimney/legacy state quando relevante;
- supplemental TCP templates/settings;
- non-default configuration drift.

---

# 22. TCP Receive Window Auto-Tuning Diagnostics

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: TCP Performance

Primary Product Area: Network

Possible Consumers:

- Throughput Benchmark
- Repair
- Recommendation Engine

Final UI Placement: TBD

Descrição: Detectar se TCP Receive Window Auto-Tuning está em estado apropriado e investigar configurações restritivas.

Possível objetivo: Encontrar throughput limitado por configuração alterada da janela TCP.

Possível benefício: Corrigir regressões causadas por tweaks antigos que desativaram ou restringiram auto-tuning.

Aplicabilidade: All PCs

Shared Capability: No

Risco inicial: Medium

Evidence Level: Established

Action Mode: Benchmark First

Possíveis estados:

- Disabled;
- Highly Restricted;
- Restricted;
- Normal;
- Experimental.

Regra:

Não desabilitar Auto-Tuning como "otimização". A documentação Microsoft atual recomenda estado normal em cenários de problemas causados por janela de recepção sem scaling adequado.

---

# 23. TCP Congestion Control Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: TCP Performance

Primary Product Area: Network

Possible Consumers:

- Benchmark
- Advanced Diagnostics

Final UI Placement: TBD

Descrição: Identificar o algoritmo/configuração de congestion control utilizado em contextos onde isso seja relevante.

Possível objetivo: Registrar comportamento da pilha sem vender um algoritmo como "mais rápido" para todos os links.

Possível benefício: Facilitar troubleshooting de throughput em cenários avançados.

Aplicabilidade: Advanced / Context-dependent

Shared Capability: No

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- active TCP template;
- congestion control algorithm;
- non-default configuration;
- policy-managed settings;
- historical/configuration drift.

---

# 24. MTU & Path MTU Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: IP Networking

Primary Product Area: Network

Possible Consumers:

- VPN
- Repair
- Gaming
- Benchmark

Final UI Placement: TBD

Descrição: Verificar MTU configurado e investigar incompatibilidades com o caminho real.

Possível objetivo: Detectar fragmentação, black-hole MTU ou overhead de túnel sem aplicar números mágicos.

Possível benefício: Corrigir problemas específicos de VPN, PPPoE, tunnel ou caminhos incomuns.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis verificações:

- interface MTU;
- IPv4/IPv6 MTU;
- path MTU behavior;
- tunnel overhead;
- fragmentation symptoms;
- ICMP behavior;
- VPN-specific constraints.

Não fazer:

- usar 1500, 1492 ou qualquer outro valor como regra universal;
- alterar MTU sem evidência de incompatibilidade.

---

# 25. IPv4 / IPv6 Health & Preference Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: IP Networking

Primary Product Area: Network

Possible Consumers:

- Repair
- DNS
- Routing
- Gaming

Final UI Placement: TBD

Descrição: Avaliar funcionamento de IPv4 e IPv6 separadamente e identificar falhas de uma das pilhas.

Possível objetivo: Detectar fallback, rota quebrada ou DNS inconsistente sem recomendar desabilitação global de IPv6.

Possível benefício: Melhorar troubleshooting em redes dual-stack.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- IPv4 connectivity;
- IPv6 connectivity;
- addresses;
- gateways/routes;
- DNS resolution;
- preferred path;
- fallback behavior;
- broken/partial IPv6.

Regra:

IPv6 enabled != problema.

---

# 26. QoS / DSCP Policy Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Quality of Service

Primary Product Area: Network

Possible Consumers:

- Gaming
- Enterprise
- Advanced Optimization

Final UI Placement: TBD

Descrição: Detectar políticas QoS e marcações de tráfego configuradas no sistema.

Possível objetivo: Saber se tráfego está sendo classificado, marcado ou limitado antes de criar recomendações.

Possível benefício: Identificar policy conflicts e evitar aplicar "gaming priority" sem infraestrutura que realmente respeite a marcação.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- policy name;
- application match;
- port/protocol match;
- DSCP marking;
- 802.1p priority;
- throttle rate;
- machine/user policy;
- policy source.

Observação:

DSCP é classificação. Benefício fim a fim depende de equipamentos e políticas ao longo do caminho.

---

# 27. Network Traffic Attribution

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Network Usage

Primary Product Area: Monitoring

Possible Consumers:

- Optimization
- Processes
- Gaming
- Privacy

Final UI Placement: TBD

Descrição: Identificar quais processos e aplicações estão gerando ou recebendo tráfego relevante.

Possível objetivo: Explicar saturação, downloads inesperados ou competição de banda.

Possível benefício: Direcionar recomendações ao workload real em vez de alterar a rede globalmente.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- process/application;
- upload/download rate;
- connection count;
- local/remote endpoints quando apropriado;
- protocol;
- total transferred;
- session history;
- foreground/background classification.

---

# 28. Bandwidth Saturation Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Performance

Primary Product Area: Network

Possible Consumers:

- Gaming
- Monitoring
- Optimization
- Benchmark

Final UI Placement: TBD

Descrição: Detectar quando o link disponível está sendo saturado por upload ou download.

Possível objetivo: Diferenciar "internet lenta" de congestionamento produzido pelo próprio PC ou rede local.

Possível benefício: Encontrar causa de latency spikes e throughput degradation sob carga.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis sinais:

- link utilization;
- upload saturation;
- download saturation;
- process attribution;
- latency increase under load;
- queueing behavior;
- throughput collapse;
- concurrent transfer activity.

---

# 29. Queueing Latency / Bufferbloat Diagnostics

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Network Latency

Primary Product Area: Network

Possible Consumers:

- Gaming
- Video Calls
- Benchmark
- Recommendation Engine

Final UI Placement: TBD

Descrição: Comparar latência ociosa e latência durante carga para identificar aumento de delay por filas excessivas.

Possível objetivo: Detectar situações em que a conexão tem bom ping quando ociosa, mas fica ruim durante upload/download.

Possível benefício: Explicar lag sob carga sem culpar automaticamente a NIC ou o Windows.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Benchmark First

Possíveis métricas:

- idle latency;
- loaded download latency;
- loaded upload latency;
- jitter under load;
- packet loss under load;
- throughput during test.

Observação:

A causa pode estar no roteador/modem/ISP e não necessariamente ser corrigível pelo PC.

---

# 30. Background Network Activity Diagnostics

Status: CANDIDATE

Type: Diagnostic / Optimization

Technical Domain: Network Usage

Primary Product Area: Optimization

Possible Consumers:

- Processes
- Gaming
- Windows Update
- Cloud Apps

Final UI Placement: TBD

Descrição: Detectar workloads em segundo plano consumindo rede de forma relevante.

Possível objetivo: Encontrar competição de banda sem classificar todo tráfego de background como desperdício.

Possível benefício: Melhorar experiência em chamadas, jogos ou downloads quando há contenção real.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis fontes:

- Windows Update;
- Delivery Optimization;
- cloud sync;
- launchers;
- browsers;
- backup software;
- game downloads;
- Store updates;
- streaming;
- user applications.

Regra:

Background network usage != unnecessary network usage.

---

# 31. VPN, Proxy & Tunnel Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Virtualization

Primary Product Area: Network

Possible Consumers:

- Repair
- Gaming
- Privacy
- Routing
- Benchmark

Final UI Placement: TBD

Descrição: Detectar quando proxy, VPN ou túnel altera o caminho da rede.

Possível objetivo: Impedir diagnósticos errados de rota, DNS, MTU ou latência.

Possível benefício: Explicar mudanças de endpoint, overhead e comportamento de conectividade.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- active VPN interface;
- proxy configuration;
- default route change;
- DNS path;
- tunnel MTU;
- virtual adapter;
- split-tunnel indication quando identificável;
- connected/disconnected state.

Não fazer:

- desativar VPN automaticamente para "melhorar ping";
- tratar VPN como problema por definição.

---

# 32. Virtual Network Adapter & Virtual Switch Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Virtualization

Primary Product Area: Shared

Possible Consumers:

- Hyper-V
- WSL
- Containers
- Repair
- Network

Final UI Placement: TBD

Descrição: Distinguir interfaces físicas de adapters/switches criados por virtualização.

Possível objetivo: Evitar alterações destrutivas em ambientes com Hyper-V, WSL, containers ou software semelhante.

Possível benefício: Reduzir falsos positivos e preservar infraestrutura de desenvolvimento/virtualização.

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis contextos:

- Hyper-V virtual switch;
- WSL networking;
- container networks;
- virtual machine adapters;
- VPN virtual adapters;
- bridge/team interfaces;
- host-only adapters.

---

# 33. Network Profile & Firewall Context Awareness

Status: CANDIDATE

Type: Diagnostic / Safeguard

Technical Domain: Network Security Context

Primary Product Area: Shared

Possible Consumers:

- Security
- Repair
- Network
- Sharing

Final UI Placement: TBD

Descrição: Identificar se a rede está classificada como pública, privada ou gerenciada e preservar implicações de firewall/descoberta.

Possível objetivo: Impedir que reparos ou resets alterem silenciosamente o contexto de segurança do usuário.

Possível benefício: Evitar perda de compartilhamento ou exposição indevida após alterações.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- network category;
- firewall profile;
- domain-managed context;
- network discovery implications;
- sharing implications;
- profile changes after network reset.

---

# 34. Metered Connection & Data Usage Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Network Policy

Primary Product Area: Network

Possible Consumers:

- Windows Update
- Energy
- Background Apps
- Settings

Final UI Placement: TBD

Descrição: Detectar conexões medidas e políticas que influenciam consumo de dados em segundo plano.

Possível objetivo: Evitar interpretar redução deliberada de tráfego como falha de performance.

Possível benefício: Preservar preferências de usuários com limites de franquia ou tethering.

Aplicabilidade: Optional / Laptop / Mobile Connection

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- metered state;
- interface type;
- data usage;
- policy behavior;
- background transfer restrictions.

---

# 35. Network Adapter Power Management Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Network Power Management

Primary Product Area: Energy

Possible Consumers:

- Network
- Battery
- Sleep/Wake
- Repair

Final UI Placement: TBD

Descrição: Avaliar capacidades e políticas de energia do adaptador sem desabilitar economia de energia indiscriminadamente.

Possível objetivo: Correlacionar disconnects, sleep behavior ou battery usage com power management da NIC.

Possível benefício: Diagnosticar problemas de rede em notebooks e Modern Standby mantendo equilíbrio energético.

Aplicabilidade: Laptop / Supported Hardware

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- device idle power capability;
- wake patterns;
- Magic Packet;
- media-change wake;
- low-power states;
- Modern Standby behavior;
- driver-exposed power properties.

Observação:

O Windows faz decisões próprias de política de energia; uma checkbox isolada não representa todo o comportamento.

---

# 36. Energy Efficient Ethernet Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Ethernet Power

Primary Product Area: Energy

Possible Consumers:

- Network
- Latency
- Benchmark

Final UI Placement: TBD

Descrição: Detectar suporte/estado de Energy Efficient Ethernet e investigar somente quando houver suspeita de compatibilidade ou latency behavior.

Possível objetivo: Tratar EEE como mecanismo de economia de energia, não como tweak universal a ser desativado.

Possível benefício: Permitir troubleshooting contextual de determinados links ou adaptadores.

Aplicabilidade: Supported Ethernet Hardware

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Benchmark First

Possíveis informações:

- EEE capability;
- driver property/state;
- link power behavior;
- link stability;
- latency under load;
- energy context.

---

# 37. Wake-on-LAN Capability & Configuration

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Network Power Management

Primary Product Area: Tools

Possible Consumers:

- Energy
- Network
- Remote Access

Final UI Placement: TBD

Descrição: Detectar capacidades de Wake-on-LAN e configuração relacionada.

Possível objetivo: Permitir diagnóstico/orientação para PCs que precisam despertar por evento de rede.

Possível benefício: Recurso útil de administração remota sem confundi-lo com performance.

Aplicabilidade: Supported Hardware

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- WoL supported;
- Magic Packet support;
- pattern-match wake;
- wake from sleep/hibernate capability;
- driver/device configuration;
- firmware dependency quando aplicável.

---

# 38. Network Connectivity History & Stability Monitoring

Status: CANDIDATE

Type: Diagnostic / Monitoring

Technical Domain: Network Stability

Primary Product Area: Monitoring

Possible Consumers:

- Repair
- Wi-Fi
- Ethernet
- Gaming

Final UI Placement: TBD

Descrição: Registrar mudanças de conectividade durante um período para detectar problemas intermitentes.

Possível objetivo: Diagnosticar "internet cai às vezes" com evidência temporal.

Possível benefício: Correlacionar desconexões com adapter reset, link loss, route change ou Wi-Fi roaming.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis eventos:

- link up/down;
- IP change;
- gateway change;
- DNS change;
- route change;
- Wi-Fi roam;
- VPN connect/disconnect;
- adapter reset;
- Internet state change;
- captive portal state.

---

# 39. Network Configuration Drift Detection

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Configuration

Primary Product Area: Network

Possible Consumers:

- Repair
- History
- Recommendation Engine

Final UI Placement: TBD

Descrição: Comparar configuração atual com baseline conhecido para identificar alterações significativas.

Possível objetivo: Responder "o que mudou desde quando a rede funcionava?".

Possível benefício: Encontrar proxy, DNS, route, adapter property ou TCP setting alterado recentemente.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis mudanças:

- DNS;
- proxy;
- route;
- IP mode;
- MTU;
- TCP globals;
- advanced NIC property;
- VPN install;
- virtual adapter;
- driver update;
- network profile.

---

# 40. Staged Network Repair

Status: CANDIDATE

Type: Repair

Technical Domain: Network Repair

Primary Product Area: Repair

Possible Consumers:

- Network
- Recommendation Engine

Final UI Placement: TBD

Descrição: Organizar reparos de rede em uma sequência progressiva, começando por ações de baixo impacto.

Possível objetivo: Evitar executar Network Reset ou operações destrutivas cedo demais.

Possível benefício: Resolver falhas comuns preservando configuração personalizada sempre que possível.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis estágios:

1. refresh diagnostics;
2. reconnect interface;
3. renew DHCP quando apropriado;
4. flush DNS quando houver evidência;
5. adapter restart;
6. targeted TCP/IP/Winsock repair;
7. driver repair/rollback guidance;
8. Network Reset como último recurso.

Regra:

Cada estágio deve possuir diagnóstico, motivo e estado antes/depois.

---

# 41. DHCP Lease & Address Repair

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: IP Configuration

Primary Product Area: Repair

Possible Consumers:

- Network

Final UI Placement: TBD

Descrição: Detectar problemas de obtenção de endereço e renovar configuração DHCP quando apropriado.

Possível objetivo: Corrigir casos de endereço inválido, lease problem ou gateway ausente sem resetar toda a rede.

Possível benefício: Reparação mais específica e de menor impacto.

Aplicabilidade: DHCP Networks

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis verificações/ações:

- DHCP enabled;
- lease state;
- APIPA/self-assigned address;
- gateway presence;
- DHCP server;
- release/renew quando apropriado;
- validation after renewal.

---

# 42. DNS Cache & Resolver Repair

Status: CANDIDATE

Type: Diagnostic / Repair

Technical Domain: DNS Repair

Primary Product Area: Repair

Possible Consumers:

- Network

Final UI Placement: TBD

Descrição: Reparar estado local de resolução quando houver evidência de cache ou resolver inconsistente.

Possível objetivo: Usar flush/retry de DNS como ação específica e não como ritual de "internet optimization".

Possível benefício: Corrigir determinados problemas de resolução com baixo impacto.

Aplicabilidade: Context-dependent

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis ações:

- inspect configured resolvers;
- flush DNS cache;
- restart relevant resolution path quando apropriado;
- resolve known test names;
- validate before/after.

---

# 43. Winsock / TCP-IP Stack Repair

Status: CANDIDATE

Type: Repair

Technical Domain: Network Repair

Primary Product Area: Repair

Possible Consumers:

- Network

Final UI Placement: TBD

Descrição: Disponibilizar reparo da pilha Winsock/TCP-IP somente quando sintomas e etapas anteriores justificarem.

Possível objetivo: Corrigir corrupção ou configuração de stack sem usar reset como "boost".

Possível benefício: Recuperar conectividade em casos específicos.

Aplicabilidade: Context-dependent

Shared Capability: No

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis ações:

- Winsock reset;
- TCP/IP reset;
- reboot requirement;
- before-state capture;
- post-reboot validation.

Safeguard:

Alertar sobre impactos em software que instala providers, filters ou componentes de rede.

---

# 44. Full Network Reset Safeguard

Status: CANDIDATE

Type: Repair / Safeguard

Technical Domain: Network Repair

Primary Product Area: Repair

Possible Consumers:

- Network
- VPN
- Virtualization

Final UI Placement: TBD

Descrição: Executar ou orientar Network Reset somente como etapa de último recurso com inventário e aviso prévio.

Possível objetivo: Impedir uso casual de uma operação que remove adaptadores e restaura configurações.

Possível benefício: Tornar o reparo mais seguro para usuários com VPN, Hyper-V ou networking customizado.

Aplicabilidade: Windows 10 / Windows 11

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: User Choice

Pre-checks:

- installed adapters;
- VPN software;
- virtual switches;
- static IP;
- custom DNS;
- proxy;
- network profile;
- custom routes;
- adapter advanced settings.

Possíveis efeitos:

- adapters removed/reinstalled;
- adapter settings reset;
- reboot;
- VPN/virtual networking reconfiguration may be required;
- network profile may return as Public.

---

# 45. Network Throughput Benchmark

Status: CANDIDATE

Type: Benchmark

Technical Domain: Network Performance

Primary Product Area: Benchmark

Possible Consumers:

- Network
- Gaming
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Medir throughput de rede com metodologia clara e contexto do link.

Possível objetivo: Diferenciar capacidade do adaptador/link de velocidade obtida até um endpoint externo.

Possível benefício: Detectar limitação local, Wi-Fi, Ethernet, router ou Internet path.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis métricas:

- download throughput;
- upload throughput;
- test duration;
- transferred bytes;
- link speed;
- protocol;
- server/peer;
- local LAN vs Internet benchmark;
- CPU usage;
- parallel streams quando metodologicamente apropriado.

Observação:

Internet speed test != benchmark puro da NIC.

---

# 46. Local Network vs Internet Performance Separation

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Network Performance

Primary Product Area: Network

Possible Consumers:

- Repair
- Benchmark
- Hardware Diagnostics

Final UI Placement: TBD

Descrição: Separar testes dentro da LAN de testes através da Internet.

Possível objetivo: Descobrir se a limitação está entre PC e rede local ou além do gateway.

Possível benefício: Evitar culpar ISP quando o Wi-Fi local está ruim e vice-versa.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Benchmark First

Possíveis comparações:

- adapter/link capability;
- LAN throughput;
- gateway latency;
- Internet throughput;
- Internet latency;
- packet loss;
- Wi-Fi signal/link rate.

---

# 47. Multi-Interface & Failover Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Routing

Primary Product Area: Network

Possible Consumers:

- Laptop
- VPN
- Gaming
- Repair

Final UI Placement: TBD

Descrição: Diagnosticar sistemas com Ethernet, Wi-Fi, mobile broadband ou tunnels ativos simultaneamente.

Possível objetivo: Explicar qual interface tem preferência e quando o caminho muda.

Possível benefício: Detectar routing surprises e comportamento inesperado de failover.

Aplicabilidade: Multi-Interface PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- active interfaces;
- interface metrics;
- default routes;
- route preference;
- connectivity by interface;
- failover event;
- simultaneous network state.

---

# 48. Captive Portal / Limited Connectivity Detection

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Connectivity

Primary Product Area: Network

Possible Consumers:

- Repair
- Wi-Fi

Final UI Placement: TBD

Descrição: Identificar situações em que o link está conectado, mas o acesso depende de autenticação ou a Internet está limitada.

Possível objetivo: Evitar diagnosticar captive portal como falha de DNS ou TCP.

Possível benefício: Explicar corretamente redes de hotéis, aeroportos, universidades e hotspots.

Aplicabilidade: Wi-Fi / Public Networks

Shared Capability: No

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis sinais:

- Windows connectivity/NCSI state;
- local link operational;
- gateway reachable;
- Internet probe mismatch;
- HTTP redirect;
- authentication requirement.

---

# 49. Network Recommendation Explanation

Status: CANDIDATE

Type: Diagnostic / Quality of Life

Technical Domain: Recommendation Engine

Primary Product Area: Shared

Possible Consumers:

- Network
- Optimization
- Repair
- Benchmark

Final UI Placement: TBD

Descrição: Explicar por que determinada recomendação de rede existe, quais sinais a sustentam e qual trade-off está envolvido.

Possível objetivo: Impedir recomendações opacas como "Optimize Internet".

Possível benefício: Aumentar transparência e reduzir aplicação de tweaks sem contexto.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possível explicação:

- symptom observed;
- layer affected;
- evidence;
- suspected cause;
- confidence;
- proposed action;
- expected benefit;
- risk;
- rollback;
- validation method;
- alternative "do nothing".

---

# 50. Network Change Backup & Rollback

Status: CANDIDATE

Type: Safeguard / Repair

Technical Domain: Network Configuration

Primary Product Area: Shared

Possible Consumers:

- Optimization
- Repair
- Network
- History

Final UI Placement: TBD

Descrição: Registrar alterações de rede realizadas pelo produto e restaurar estado anterior quando possível.

Possível objetivo: Tornar experimentos e reparos auditáveis e reversíveis.

Possível benefício: Reduzir risco de perda de conectividade após configuração inadequada.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis dados:

- interface;
- property;
- original value;
- new value;
- route/DNS/TCP context;
- timestamp;
- reason;
- benchmark result;
- connectivity validation;
- rollback result.

Possíveis operações:

- undo individual;
- restore adapter profile;
- rollback after failed connectivity validation;
- compare before/after.

---

# 51. Network Optimization Outcome Validation

Status: CANDIDATE

Type: Diagnostic / Benchmark

Technical Domain: Recommendation Validation

Primary Product Area: Optimization

Possible Consumers:

- Network
- Benchmark
- History

Final UI Placement: TBD

Descrição: Verificar se uma alteração de rede realmente melhorou o indicador que justificou sua aplicação.

Possível objetivo: Evitar manter mudanças apenas porque foram aplicadas com sucesso tecnicamente.

Possível benefício: Separar configuração aplicada de otimização comprovada.

Aplicabilidade: Benchmarkable Network Changes

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis resultados:

- Improved;
- Neutral / Within Variance;
- Regressed;
- Connectivity Regression;
- Inconclusive;
- Not Comparable.

Possíveis métricas:

- latency;
- jitter;
- packet loss;
- throughput;
- loaded latency;
- CPU overhead;
- connectivity stability.

---

# Itens propositalmente NÃO separados nesta passada

Os assuntos abaixo foram considerados, mas não devem virar features independentes nesta granularidade:

- cada advanced property de Intel/Realtek/Killer/Broadcom;
- Receive Buffers;
- Transmit Buffers;
- cada checksum offload;
- cada LSO version;
- cada RSS profile;
- cada Registry value de TCP;
- `TcpAckFrequency`;
- `TCPNoDelay`;
- `TcpDelAckTicks`;
- `NetworkThrottlingIndex`;
- `SystemResponsiveness`;
- Nagle como tweak isolado;
- cada algoritmo de congestion control;
- cada DNS provider;
- cada comando `netsh`;
- cada contador de adapter;
- cada tipo de route;
- cada ICMP test;
- cada Wi-Fi band/channel como feature;
- cada VPN vendor;
- cada virtual adapter.

Esses detalhes devem ficar dentro de capabilities consolidadas ou ser avaliados somente na Feature Spec.

---

# Tweaks clássicos que NÃO devem ser assumidos como otimização

## Disable Nagle / TcpAckFrequency / TCPNoDelay

Status conceitual: QUESTIONABLE / Context-dependent

Motivo:

São mecanismos relacionados ao comportamento TCP, não botões universais de "menor ping". Aplicações e protocolos diferentes têm padrões diferentes e jogos modernos podem usar UDP ou controlar seu próprio comportamento de rede.

Não criar preset global.

---

## NetworkThrottlingIndex

Status conceitual: QUESTIONABLE

Motivo:

Não tratar valores populares de tweak packs como otimização moderna comprovada. Se o mecanismo ainda for relevante a algum cenário, deve ser investigado especificamente na Feature Spec e medido.

---

## Disable IPv6

Status conceitual: REJECTED como otimização universal

Motivo:

IPv6 é parte normal da pilha moderna. Problemas de IPv6 devem ser diagnosticados, não resolvidos removendo a pilha inteira por padrão.

---

## Disable TCP Auto-Tuning

Status conceitual: REJECTED como otimização universal

Motivo:

A configuração normal permite scaling da receive window e a própria documentação Microsoft usa `Normal` como correção para determinados problemas de throughput.

---

## MTU mágico

Status conceitual: REJECTED

Motivo:

O valor depende do link e do caminho. MTU deve ser diagnosticado quando houver evidência de fragmentação/tunnel overhead.

---

## Disable RSS

Status conceitual: REJECTED como regra global

Motivo:

RSS existe para distribuir processamento de recepção e reduzir bottlenecks de CPU em sistemas multiprocessados.

---

## Disable RSC universally

Status conceitual: REJECTED como regra global

Motivo:

RSC reduz overhead e favorece throughput, embora determinados workloads de baixa latência possam justificar benchmark específico.

---

## Disable all NIC offloads

Status conceitual: REJECTED como regra global

Motivo:

Offloads existem para deslocar trabalho da CPU e possuem trade-offs diferentes por workload/hardware.

---

## Disable Interrupt Moderation

Status conceitual: QUESTIONABLE / Benchmark First

Motivo:

Pode reduzir batching em alguns cenários, mas aumenta interrupt/DPC rate e CPU overhead. Exige medição contextual.

---

## Disable Energy Efficient Ethernet

Status conceitual: QUESTIONABLE / Benchmark First

Motivo:

EEE é mecanismo real de economia de energia. Só deve ser investigado se houver comportamento problemático demonstrável.

---

## "Best DNS for Gaming"

Status conceitual: QUESTIONABLE

Motivo:

DNS influencia resolução inicial e confiabilidade. Após o endpoint ser resolvido, o RTT do tráfego para o servidor não é determinado pelo resolver DNS.

---

# Principais grupos futuros

Sem consolidar formalmente ainda, os 51 itens desta passada parecem formar grupos como:

- Network Hardware & Adapter Inventory
- Active Path & Routing
- Connectivity Diagnostics
- Latency / Jitter / Loss
- DNS
- Ethernet
- Wi-Fi
- Adapter Driver & Configuration
- RSS / Offloads / Interrupts
- TCP/IP
- QoS
- Traffic Attribution
- Saturation & Queueing
- VPN / Virtual Networking
- Network Power Management
- Stability & History
- Network Repair
- Network Benchmark
- Explain Why / Rollback / Validation

Não consolidar agora.

---

# Hipóteses / tópicos para Second Pass — Gap Audit

A segunda passada deverá procurar principalmente lacunas em:

- Wi-Fi roaming behavior;
- Wi-Fi 6/6E/7 capability nuances;
- MLO / Multi-Link Operation quando exposto;
- mobile broadband;
- Bluetooth tethering;
- USB Ethernet/docking behavior;
- NIC error/statistics depth;
- retransmission diagnostics;
- TCP connection-state analysis;
- UDP-specific diagnostics;
- QUIC/HTTP3 awareness;
- DNS cache/NRPT/split DNS;
- VPN split tunneling;
- proxy/PAC/WPAD;
- captive portal/NCSI edge cases;
- Hyper-V/WSL networking;
- SMB/local-network diagnostics sem virar file-server product;
- ICS/mobile hotspot;
- network bridge/teaming;
- jumbo frames;
- VLAN;
- packet capture / ETW / pktmon;
- firewall/filter-driver interference;
- third-party network filter/LSP/WFP awareness;
- security software interference;
- driver regression correlation;
- sleep/wake network failures;
- Modern Standby connectivity;
- network benchmark methodology;
- public test endpoint/privacy;
- gaming-specific correlation sem duplicar Gaming;
- repair escalation;
- recommendation confidence.

---

# Principais conclusões da First Pass

1. **Network optimization começa pelo caminho real da conexão.**

2. **Latency, jitter, packet loss e throughput são métricas diferentes.**

3. **Wi-Fi precisa ser diagnosticado como link de rádio, não apenas como IP networking.**

4. **RSS é mecanismo real de scaling e não deve ser desativado genericamente.**

5. **RSC e interrupt moderation possuem trade-offs entre throughput, CPU overhead e latência.**

6. **Offloads de NIC não devem ser desligados em bloco.**

7. **TCP Auto-Tuning desativado pode ser regressão, não otimização.**

8. **IPv6 não deve ser desativado como tweak universal.**

9. **MTU não possui valor mágico aplicável a todos.**

10. **DNS pode afetar resolução e confiabilidade, mas não é um "ping booster".**

11. **DSCP/QoS só produz resultado útil quando o restante da infraestrutura respeita a política.**

12. **Background traffic só é problema quando existe contenção relevante.**

13. **VPNs e virtual adapters mudam o caminho e precisam ser considerados antes de diagnosticar a rede.**

14. **Network Reset deve ser último recurso.**

15. **Um reparo aplicado com sucesso não significa que houve melhora; é necessário validar.**

16. **Nothing to optimize é resultado válido.**

---

# Fontes técnicas de referência desta passada

## Microsoft — Receive Side Scaling

https://learn.microsoft.com/windows-hardware/drivers/network/introduction-to-receive-side-scaling

## Microsoft — Receive Segment Coalescing

https://learn.microsoft.com/windows-hardware/drivers/network/overview-of-receive-segment-coalescing

## Microsoft — Network Adapter Advanced Properties

https://learn.microsoft.com/powershell/module/netadapter/get-netadapteradvancedproperty

## Microsoft — TCP/IP performance / Auto-Tuning

https://learn.microsoft.com/troubleshoot/windows-server/networking/tcpip-performance-known-issues

## Microsoft — High Performance Networking / NIC features

https://learn.microsoft.com/windows-server/networking/technologies/hpn/hpn-hardware-only-features

## Microsoft — Policy-based QoS

https://learn.microsoft.com/windows-server/networking/technologies/qos/qos-policy-top

## Microsoft — Wi-Fi Wireless Network Report

https://support.microsoft.com/windows/analyze-the-wireless-network-report

## Microsoft — Fix Ethernet Connection Problems / Network Reset

https://support.microsoft.com/windows/fix-ethernet-connection-problems-in-windows

## Microsoft — Fix Wi-Fi Connection Problems

https://support.microsoft.com/windows/fix-wi-fi-connection-issues-in-windows

## Microsoft — DNS over HTTPS

https://learn.microsoft.com/windows-server/networking/dns/dns-encryption-dns-over-https

## Microsoft — Networking Power Management / Modern Standby

https://learn.microsoft.com/windows-hardware/design/device-experiences/networking-power-management-for-modern-standby-platforms

## Microsoft — NetAdapterCx Power Management

https://learn.microsoft.com/windows-hardware/drivers/netcx/configuring-power-management

## Microsoft — Wake on LAN

https://learn.microsoft.com/windows-hardware/drivers/network/low-power-for-wake-on-lan

---

# Status final

**Network — First Pass: COMPLETE**

Próximo passo da área:

**Second Pass — Consolidated Gap Audit**

# BeadWise / PC Optimizer V2 — Network Discovery

## Second Pass — Consolidated Gap Audit

Status da área: SECOND PASS COMPLETE

Base analisada:

- Network First Pass;
- 01-DISCOVERY;
- inventário funcional V1 apenas como fonte secundária;
- documentação técnica atual da Microsoft quando necessário.

Objetivo desta passada:

Encontrar perguntas relevantes de Network ainda não respondidas pelas 51 capabilities da First Pass.

Regra desta passada:

> adicionar somente lacunas reais; não decompor novamente propriedades de NIC, counters, comandos, protocolos ou Registry values em features independentes.

As capabilities abaixo continuam a numeração da First Pass.

---

# 52. Wi-Fi Generation & Multi-Link Capability Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Wi-Fi Capability

Primary Product Area: Network

Possible Consumers:

- My PC
- Gaming
- Benchmark
- Drivers

Final UI Placement: TBD

Descrição: Identificar corretamente a geração Wi-Fi suportada pelo adaptador e os recursos modernos disponíveis na conexão atual, incluindo Wi-Fi 6/6E/7 e Multi-Link Operation quando expostos.

Possível objetivo: Diferenciar capacidade nominal do adaptador de recursos realmente negociados com o access point.

Possível benefício: Explicar por que um dispositivo Wi-Fi moderno não está usando 6 GHz, largura de canal esperada ou múltiplos links.

Aplicabilidade: Wi-Fi PCs / Supported Hardware

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- supported 802.11 generations;
- Wi-Fi 6 / 6E / 7 capability;
- current PHY generation;
- 2.4 / 5 / 6 GHz support;
- channel width;
- WPA2/WPA3 capability;
- MLO capability;
- MLO active state;
- number of active links quando disponível;
- bands participating in MLO;
- driver capability.

Observação:

A documentação WiFiCx atual inclui requisitos específicos para Wi-Fi 7 e Multi-Link Operation. Capability do adaptador não garante que AP, driver e conexão atual estejam usando esses recursos.

---

# 53. Wi-Fi Roaming & BSSID Transition Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Wi-Fi Stability

Primary Product Area: Network

Possible Consumers:

- Monitoring
- Gaming
- Repair

Final UI Placement: TBD

Descrição: Detectar mudanças entre access points/BSSIDs e correlacioná-las com variações de conectividade ou latência.

Possível objetivo: Diagnosticar ambientes mesh, enterprise ou com múltiplos APs onde o PC alterna entre rádios.

Possível benefício: Explicar disconnects curtos, latency spikes ou troca frequente de access point.

Aplicabilidade: Wi-Fi / Multi-AP Environments

Shared Capability: No

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis informações:

- SSID;
- BSSID;
- roaming event;
- previous/new BSSID;
- signal before/after;
- band before/after;
- channel before/after;
- link rate;
- reconnect duration;
- packet loss/latency around transition.

Regra:

Roaming != problema por definição. O diagnóstico deve buscar comportamento excessivo ou correlação com sintomas.

---

# 54. Mobile Broadband & Cellular Network Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Mobile Broadband

Primary Product Area: Network

Possible Consumers:

- Laptop
- Battery
- Metered Connection
- Monitoring

Final UI Placement: TBD

Descrição: Reconhecer conexões de banda larga móvel/celular como classe distinta de Ethernet e Wi-Fi.

Possível objetivo: Evitar interpretar comportamento de rede celular usando expectativas de link fixo.

Possível benefício: Contextualizar latência variável, data usage, sinal e conexão medida.

Aplicabilidade: Specific Hardware / Mobile Broadband

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- mobile broadband adapter;
- connection state;
- provider/operator quando exposto;
- signal indication;
- metered state;
- roaming state quando disponível;
- data usage;
- interface route;
- tethering capability.

---

# 55. Mobile Hotspot & Internet Connection Sharing Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration / Repair

Technical Domain: Network Sharing

Primary Product Area: Tools

Possible Consumers:

- Network
- Mobile Broadband
- Wi-Fi
- Repair

Final UI Placement: TBD

Descrição: Detectar e diagnosticar quando o PC está compartilhando sua conexão por Mobile Hotspot ou Internet Connection Sharing (ICS).

Possível objetivo: Tratar o computador também como gateway/NAT temporário quando esse recurso está ativo.

Possível benefício: Diagnosticar clientes que conectam ao hotspot mas não recebem Internet, problemas de DHCP/NAT ou conflitos com adaptadores virtuais.

Aplicabilidade: Supported Windows Systems

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações:

- hotspot/ICS enabled;
- source Internet interface;
- shared interface;
- virtual Wi-Fi adapter;
- SharedAccess/ICS state;
- client count quando disponível;
- DHCP/NAT context;
- sharing configuration;
- upstream connectivity.

Safeguard:

Não alterar ICS automaticamente em sistemas que possam depender de compartilhamento, laboratório ou infraestrutura virtual.

---

# 56. Docking, USB Ethernet & Adapter Transition Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Hardware

Primary Product Area: Shared

Possible Consumers:

- USB / Peripherals
- Laptop
- Network
- Monitoring

Final UI Placement: TBD

Descrição: Reconhecer adaptadores de rede conectados por USB, dock ou Thunderbolt e mudanças de interface causadas por conectar/desconectar acessórios.

Possível objetivo: Explicar alterações repentinas de rota, MAC, link speed ou driver quando um notebook entra ou sai de um dock.

Possível benefício: Melhor troubleshooting de escritórios e notebooks com múltiplas interfaces físicas possíveis.

Aplicabilidade: Laptop / USB or Dock Networking

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações:

- bus/connection type;
- dock-associated NIC;
- USB Ethernet;
- interface appearance/disappearance;
- route transition;
- link speed;
- driver;
- power transition;
- reconnect history.

---

# 57. Transport Protocol Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Transport Protocols

Primary Product Area: Network

Possible Consumers:

- Monitoring
- Gaming
- Advanced Diagnostics
- Benchmark

Final UI Placement: TBD

Descrição: Distinguir tráfego e problemas associados a TCP, UDP e protocolos modernos baseados em UDP como QUIC quando tecnicamente observáveis.

Possível objetivo: Evitar aplicar raciocínio TCP a workloads que não usam TCP.

Possível benefício: Reduzir recomendações irrelevantes como Nagle/TCP tuning para aplicações majoritariamente UDP/QUIC.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- TCP connections;
- UDP endpoints;
- QUIC usage quando observável;
- port/protocol;
- process attribution;
- connection direction;
- local/remote endpoints.

Regra:

A existência de tweak TCP não significa relevância para qualquer aplicação de rede.

---

# 58. TCP Retransmission & Loss-Recovery Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: TCP Performance

Primary Product Area: Network

Possible Consumers:

- Benchmark
- Repair
- Routing
- Wi-Fi

Final UI Placement: TBD

Descrição: Identificar sinais de retransmissão e recuperação de perda em conexões TCP para complementar ping e packet-loss tests.

Possível objetivo: Detectar degradação percebida pela pilha TCP durante tráfego real.

Possível benefício: Encontrar problemas de link, congestionamento ou caminho que podem não aparecer em um teste curto de ICMP.

Aplicabilidade: TCP Workloads

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis sinais:

- TCP retransmissions;
- retransmission rate;
- duplicate ACK patterns quando observáveis;
- connection resets;
- timeout behavior;
- throughput reduction;
- latency under load;
- correlated adapter errors.

Observação:

Retransmission indica recuperação de entrega TCP; não identifica sozinha onde a perda ocorreu.

---

# 59. TCP Connection State & Socket Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Connections

Primary Product Area: Network

Possible Consumers:

- Processes
- Repair
- Security
- Advanced Diagnostics

Final UI Placement: TBD

Descrição: Inspecionar estados de conexões e sockets para diagnosticar aplicações que não conseguem conectar, portas em conflito ou grande acúmulo de conexões.

Possível objetivo: Diferenciar falha de DNS, ausência de listener, conexão recusada, timeout ou comportamento anormal da aplicação.

Possível benefício: Tornar troubleshooting de conectividade por aplicação mais preciso.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- LISTENING;
- ESTABLISHED;
- SYN_SENT;
- SYN_RECEIVED;
- TIME_WAIT;
- CLOSE_WAIT;
- local endpoint;
- remote endpoint;
- owning process;
- connection count;
- port binding conflicts.

---

# 60. Proxy / PAC / WPAD Diagnostics

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: HTTP Proxy

Primary Product Area: Network

Possible Consumers:

- Repair
- Privacy
- Enterprise
- Connectivity

Final UI Placement: TBD

Descrição: Detectar configuração manual, automática ou descoberta de proxy utilizada pelo Windows e por componentes WinHTTP.

Possível objetivo: Identificar quando problemas de HTTP/HTTPS, updates ou aplicações são causados por proxy/PAC/WPAD em vez da conectividade IP básica.

Possível benefício: Diagnosticar acesso seletivo à Internet e configurações corporativas sem removê-las por engano.

Aplicabilidade: Context-dependent / Managed PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- manual proxy;
- auto-detect;
- PAC URL;
- WPAD discovery;
- WinHTTP proxy;
- user proxy configuration;
- bypass list;
- policy-managed state.

Observação:

WinHTTP possui suporte explícito a AutoProxy/WPAD/PAC. Uma configuração automática existente pode ser necessária para acesso corporativo.

---

# 61. Split DNS & Name Resolution Policy Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: DNS Policy

Primary Product Area: Network

Possible Consumers:

- VPN
- Enterprise
- Repair
- Security

Final UI Placement: TBD

Descrição: Detectar políticas que fazem namespaces diferentes seguirem regras ou resolvers distintos, incluindo NRPT quando aplicável.

Possível objetivo: Evitar concluir que "DNS está errado" quando determinadas zonas são deliberadamente resolvidas por política diferente.

Possível benefício: Melhor troubleshooting de VPN, DirectAccess e ambientes corporativos.

Aplicabilidade: Context-dependent / Managed PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- effective NRPT policy;
- namespace-specific rules;
- specified name servers;
- DNSSEC requirements;
- VPN-related resolution behavior;
- split DNS indication;
- policy source.

Regra:

Não substituir DNS globalmente antes de verificar políticas de resolução existentes.

---

# 62. WSL & Hyper-V Network Architecture Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Virtual Networking

Primary Product Area: Shared

Possible Consumers:

- Network
- WSL
- Hyper-V
- Repair
- Developers

Final UI Placement: TBD

Descrição: Identificar o modelo de rede usado por WSL/Hyper-V e como ele altera interfaces, firewall e reachability.

Possível objetivo: Evitar interpretar NAT, mirrored networking ou virtual switches como configuração corrompida.

Possível benefício: Melhor compatibilidade com máquinas de desenvolvimento e virtualização.

Aplicabilidade: WSL / Hyper-V Systems

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações:

- WSL installed/running;
- NAT vs mirrored mode;
- virtual interfaces;
- Hyper-V virtual switch;
- IPv6 availability;
- localhost reachability context;
- VPN compatibility;
- Hyper-V firewall context;
- LAN accessibility.

Observação:

No Windows 11 compatível, WSL pode usar mirrored networking, arquitetura distinta do NAT tradicional.

---

# 63. Layer-2 Advanced Configuration Awareness

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Ethernet Advanced Configuration

Primary Product Area: Network

Possible Consumers:

- Enterprise
- Virtualization
- Advanced Diagnostics

Final UI Placement: TBD

Descrição: Consolidar configurações avançadas de camada 2 que podem existir em adaptadores específicos sem torná-las presets de performance.

Possível objetivo: Detectar configurações incompatíveis com a infraestrutura local.

Possível benefício: Diagnosticar problemas em redes especializadas sem expor dezenas de toggles desconectados.

Aplicabilidade: Specific Hardware / Advanced Networks

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis mecanismos:

- VLAN tagging;
- VLAN ID;
- Jumbo Frames/Jumbo Packet;
- network bridge;
- adapter team/bond-like configuration quando suportado;
- priority tagging;
- vendor-specific L2 settings.

Regra:

Jumbo Frames exigem suporte fim a fim no caminho relevante; não são "more performance" automaticamente.

---

# 64. Native Packet Capture & Drop Diagnostics

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: Network Tracing

Primary Product Area: Tools

Possible Consumers:

- Repair
- Advanced Diagnostics
- Virtualization
- Developers

Final UI Placement: TBD

Descrição: Orquestrar captura e diagnóstico de pacotes usando mecanismos nativos do Windows quando uma investigação aprofundada for necessária.

Possível objetivo: Observar tráfego e pontos de drop sem depender inicialmente de drivers de captura de terceiros.

Possível benefício: Fornecer evidência técnica para problemas que snapshots e ping não conseguem explicar.

Aplicabilidade: Advanced Diagnostics

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis mecanismos:

- Pktmon;
- ETW/network tracing;
- packet filtering;
- packet counters;
- drop detection;
- component-level path visibility;
- ETL capture/export;
- targeted capture window.

Safeguards:

- limitar duração;
- minimizar coleta de payload quando possível;
- informar que captura pode conter dados sensíveis;
- não coletar continuamente por padrão.

---

# 65. Network Filter & WFP Interference Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Filtering

Primary Product Area: Network

Possible Consumers:

- Security
- VPN
- Repair
- Stability

Final UI Placement: TBD

Descrição: Identificar componentes de filtragem de rede que podem participar do caminho entre aplicação e rede.

Possível objetivo: Diagnosticar interferência causada por firewall de terceiros, VPN, antivírus, parental control, traffic inspection ou outros filtros.

Possível benefício: Evitar resetar TCP/NIC quando o tráfego está sendo bloqueado ou modificado por uma camada de filtragem.

Aplicabilidade: All PCs / Third-party Network Software

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis componentes:

- Windows Filtering Platform providers;
- callouts;
- filter drivers;
- VPN filters;
- antivirus/security filters;
- parental control;
- packet inspection;
- network monitoring components.

Observação:

A Windows Filtering Platform permite filtragem e modificação de dados em múltiplas camadas da pilha. A presença de filtro não prova que ele é defeituoso.

---

# 66. Firewall / Port Reachability Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Security

Primary Product Area: Repair

Possible Consumers:

- Security
- Network
- Applications
- Gaming

Final UI Placement: TBD

Descrição: Diferenciar falha de conectividade geral de bloqueio específico por firewall, regra, porta ou aplicação.

Possível objetivo: Responder por que um serviço específico não conecta apesar de a Internet funcionar.

Possível benefício: Evitar desativar firewall inteiro para testar um único problema.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis verificações:

- listening local port;
- outbound reachability;
- inbound rule context;
- outbound rule context;
- active firewall profile;
- application rule;
- blocked/logged event quando disponível;
- third-party filtering awareness.

Não fazer:

- recomendar Firewall OFF como solução normal.

---

# 67. Local Network Service Reachability Diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: LAN Connectivity

Primary Product Area: Network

Possible Consumers:

- Repair
- Sharing
- Devices
- Tools

Final UI Placement: TBD

Descrição: Diagnosticar acesso a dispositivos e serviços locais separadamente da conectividade com a Internet.

Possível objetivo: Diferenciar "Internet funciona" de "não consigo acessar NAS/impressora/outro PC".

Possível benefício: Encontrar problemas de subnet, profile, firewall, name resolution ou service reachability dentro da LAN.

Aplicabilidade: LAN Environments

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis verificações:

- same-subnet reachability;
- gateway/local device reachability;
- local hostname resolution;
- service port reachability;
- network profile;
- discovery/sharing context;
- IPv4/IPv6 local path.

Observação:

A capability não transforma o produto em file server ou scanner de rede indiscriminado.

---

# 68. Sleep / Wake Network Failure Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Power & Stability

Primary Product Area: Network

Possible Consumers:

- Energy
- Modern Standby
- Wi-Fi
- Ethernet
- Drivers

Final UI Placement: TBD

Descrição: Correlacionar perda ou degradação de conectividade com transições de sleep, Modern Standby, hibernate ou resume.

Possível objetivo: Identificar problemas que surgem apenas após o PC acordar.

Possível benefício: Direcionar investigação para driver/power management em vez de reset de rede genérico.

Aplicabilidade: Laptop / Sleep-capable PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis sinais:

- sleep/resume timestamp;
- adapter disappearance/restart;
- IP loss;
- DNS loss;
- gateway loss;
- Wi-Fi reconnect;
- driver reset;
- power-management state;
- recovery time.

---

# 69. Network Regression Correlation

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Network Stability

Primary Product Area: Network

Possible Consumers:

- Drivers
- Windows Update
- Repair
- History

Final UI Placement: TBD

Descrição: Correlacionar início de problemas de rede com mudanças recentes no sistema.

Possível objetivo: Responder "o que mudou desde quando a rede funcionava normalmente?".

Possível benefício: Identificar regressões de driver, Windows update, VPN, security software ou configuração.

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Context-dependent

Action Mode: Diagnostic Only

Possíveis mudanças:

- network driver update;
- Windows update;
- VPN installation/update;
- firewall/security software update;
- adapter property change;
- DNS/proxy change;
- WSL/Hyper-V configuration change;
- new virtual adapter;
- power-policy change.

Regra:

Correlação temporal != causalidade comprovada.

---

# 70. Network Benchmark Methodology & Privacy Safeguards

Status: CANDIDATE

Type: Benchmark / Safeguard

Technical Domain: Network Benchmark

Primary Product Area: Benchmark

Possible Consumers:

- Network
- Privacy
- Recommendation Engine

Final UI Placement: TBD

Descrição: Definir quando e como testes ativos de rede podem ser executados sem produzir resultados enganosos ou enviar tráfego desnecessário.

Possível objetivo: Garantir comparabilidade, transparência e privacidade em latency/throughput/DNS/bufferbloat tests.

Possível benefício: Evitar benchmark que mede principalmente distância até servidor, congestionamento externo ou endpoint inadequado.

Aplicabilidade: Network Benchmarks

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis controles:

- endpoint selection;
- server geography/context;
- LAN vs Internet test;
- test protocol;
- test duration;
- sample count;
- transferred data estimate;
- background network activity;
- VPN state;
- Wi-Fi/Ethernet path;
- IPv4/IPv6;
- warm-up;
- repeated-run variance;
- privacy disclosure.

Possíveis estados:

- Valid;
- High External Variance;
- Saturated by Background Traffic;
- VPN/Tunnel Active;
- Endpoint Unsuitable;
- Insufficient Samples;
- Not Comparable.

---

# Lacunas avaliadas mas NÃO transformadas em novas capabilities

## Wi-Fi 6E

Não precisa de feature isolada.

É uma capacidade dentro de:

- Wi-Fi Capability & Connection Diagnostics;
- Wi-Fi Generation & Multi-Link Capability Diagnostics.

---

## MLO por link individual

Não criar uma capability para cada link MLO.

Links, bandas, channels e rates são dados internos da capability Wi-Fi 7/MLO.

---

## QUIC / HTTP/3 separados

Não criar "QUIC Optimizer" ou "HTTP/3 Optimizer".

A distinção de protocolo pertence a Transport Protocol Awareness e tracing avançado.

---

## UDP optimization

Não há justificativa para uma capability genérica "Optimize UDP".

UDP é um transporte. Aplicações diferentes possuem comportamentos próprios.

---

## Jumbo Frames

Não virar item independente.

Fica dentro de Layer-2 Advanced Configuration Awareness, porque requer suporte fim a fim e contexto de rede específico.

---

## VLAN

Também permanece dentro da capability consolidada de Layer 2.

---

## Receive/Transmit Buffers

Continuam como advanced NIC properties, não capabilities.

---

## Packet Capture

Foi adicionada uma capability de orquestração, mas:

- cada provider ETW;
- cada event;
- cada filter;
- cada Pktmon command;

não vira discovery item separado.

---

## Windows Firewall

Network apenas precisa diagnosticar reachability e contexto.

Discovery aprofundado de Firewall pertence à área Security.

---

## SMB

Não criar "SMB Optimizer" nesta área.

LAN reachability é pertinente; tuning/protocolo aprofundado só justificaria nova capability se o escopo futuro do produto exigir isso.

---

## Bluetooth tethering

Pode ser consumido futuramente por Mobile Broadband / Internet Sharing e USB/Peripherals.

Não justifica item próprio nesta granularidade.

---

# Resultado do Gap Audit

Capabilities adicionadas nesta passada: **19**

Numeração adicionada:

**52–70**

Total bruto da área Network após as duas passadas:

- First Pass: 51 capabilities
- Second Pass: 19 capabilities
- Total bruto atual: **70 capabilities**

Isso NÃO significa 70 features finais.

Network possui muitos mecanismos e métricas de baixo nível, então esta área deverá sofrer consolidação significativa mais tarde.

---

# Principais grupos que começam a emergir

Sem consolidar formalmente ainda, Network parece caminhar para algo próximo de:

1. Adapter & Hardware Inventory
2. Active Path & Configuration
3. Connectivity Diagnostics
4. Latency / Loss / Routing
5. DNS & Proxy
6. Ethernet
7. Wi-Fi
8. Cellular / Sharing
9. NIC Processing & Offloads
10. TCP/IP & Transport
11. Traffic & Saturation
12. VPN / Virtual Networking
13. Filtering & Firewall Context
14. Network Power & Stability
15. Network Tracing
16. Repair
17. Benchmark
18. Rollback / Explain Why / Validation

Esses grupos são apenas sinalização para a futura consolidação.

---

# Principais conclusões da Second Pass

1. **Wi-Fi 7/MLO exige capability awareness**, mas não dezenas de features por banda/link.

2. **Roaming precisa ser analisado temporalmente**, especialmente em mesh e ambientes com múltiplos APs.

3. **Mobile broadband e tethering são contextos distintos de Wi-Fi/Ethernet comuns.**

4. **Dock/USB NIC pode explicar mudanças de rota e comportamento em notebooks.**

5. **TCP tweaks são irrelevantes para workloads que não usam TCP.**

6. **Retransmissions complementam ping/packet loss**, mas não identificam sozinhas onde a perda ocorreu.

7. **Proxy/PAC/WPAD podem explicar Internet parcialmente funcional.**

8. **Split DNS/NRPT precisa ser preservado em ambientes corporativos/VPN.**

9. **WSL/Hyper-V podem usar arquiteturas de networking diferentes, incluindo mirrored networking.**

10. **VLAN/Jumbo/Layer-2 settings precisam permanecer hardware/infrastructure-aware.**

11. **Pktmon/ETW justificam uma capability de tracing avançado**, mas não um catálogo de comandos.

12. **WFP/filter drivers podem bloquear ou modificar tráfego em múltiplas camadas.**

13. **Firewall troubleshooting deve ser específico**, não "disable firewall".

14. **LAN reachability é diferente de Internet connectivity.**

15. **Sleep/resume pode ser causa temporal de falhas de rede.**

16. **Driver/update/VPN/security regressions devem participar do diagnóstico histórico.**

17. **Benchmark de rede precisa controlar endpoint e variáveis externas.**

18. **Resultados externos possuem variância que o produto não controla.**

19. **Nothing to optimize continua sendo resultado válido.**

---

# Fontes técnicas relevantes desta passada

## Microsoft — WiFiCx Wi-Fi 7 / MLO

https://learn.microsoft.com/windows-hardware/drivers/netcx/wificx-wi-fi-7

## Microsoft Support — Wi-Fi moderno no Windows

https://support.microsoft.com/windows/faster-and-more-secure-wi-fi-in-windows

## Microsoft — Windows Filtering Platform

https://learn.microsoft.com/windows/win32/fwp/windows-filtering-platform-start-page

## Microsoft — WFP Callout Drivers

https://learn.microsoft.com/windows-hardware/drivers/network/introduction-to-windows-filtering-platform-callout-drivers

## Microsoft — Packet Monitor (Pktmon)

https://learn.microsoft.com/windows/win32/pktmon/pktmon

## Microsoft — WSL Networking

https://learn.microsoft.com/windows/wsl/networking

## Microsoft — Name Resolution Policy Table

https://learn.microsoft.com/windows-server/networking/dns/name-resolution-policy-table

## Microsoft — WinHTTP AutoProxy / WPAD / PAC

https://learn.microsoft.com/windows/win32/winhttp/winhttp-autoproxy-api

## Microsoft — Internet Connection Sharing / Tethering

https://learn.microsoft.com/windows-hardware/drivers/mobilebroadband/creating-and-configuring-internet-sharing-experiences

---

# Status final da área

**Network — INITIAL DISCOVERY COMPLETE**

Passadas concluídas:

- First Pass — Broad Discovery
- Second Pass — Consolidated Gap Audit