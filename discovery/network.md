# Consolidated Discovery — NETWORK

> Source: `network.md`
> Canonical rules: `01-DISCOVERY.md`
> This file is a consolidation layer. The original discovery remains unchanged and traceable.

## Consolidation result

- Raw discovery items: **70**
- Consolidated active capability groups: **16**
- Explicitly rejected source items kept outside active catalog: **0**
- Items conservatively preserved as standalone because no safe merge was identified: **0**

## C-NETWORK-001 — Network adapters, active path & baseline

Status: CANDIDATE

Type: Diagnostic

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [1] Network Adapter Inventory — `CANDIDATE`
- [2] Active Network Path Identification — `CANDIDATE`
- [3] Network Configuration Baseline — `CANDIDATE`
- [47] Multi-Interface & Failover Diagnostics — `CANDIDATE`
- [56] Docking, USB Ethernet & Adapter Transition Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-002 — Connectivity, reachability & route diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [4] End-to-End Connectivity Diagnostics — `CANDIDATE`
- [6] Route & Hop Diagnostics — `CANDIDATE`
- [48] Captive Portal / Limited Connectivity Detection — `CANDIDATE`
- [66] Firewall / Port Reachability Diagnostics — `CANDIDATE`
- [67] Local Network Service Reachability Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-003 — Latency, jitter, loss & retransmission diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Benchmark

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [5] Latency, Jitter & Packet Loss Diagnostics — `CANDIDATE`
- [58] TCP Retransmission & Loss-Recovery Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-004 — DNS & name-resolution diagnostics

Status: CANDIDATE

Type: Benchmark, Diagnostic, Diagnostic / Configuration

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [7] DNS Configuration & Resolution Diagnostics — `CANDIDATE`
- [8] Encrypted DNS / DoH Awareness — `CANDIDATE`
- [9] DNS Resolver Performance & Reliability Comparison — `CANDIDATE`
- [61] Split DNS & Name Resolution Policy Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-005 — Ethernet link & advanced adapter diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [10] Ethernet Link Diagnostics — `CANDIDATE`
- [11] Ethernet Negotiation & Duplex Anomaly Detection — `CANDIDATE`
- [16] Network Adapter Advanced Property Inventory — `CANDIDATE`
- [63] Layer-2 Advanced Configuration Awareness — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-006 — Wi-Fi capability, radio & roaming diagnostics

Status: CANDIDATE

Type: Diagnostic

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [12] Wi-Fi Capability & Connection Diagnostics — `CANDIDATE`
- [13] Wi-Fi Signal & Radio Environment Diagnostics — `CANDIDATE`
- [14] Wi-Fi Connection History & WLAN Report — `CANDIDATE`
- [52] Wi-Fi Generation & Multi-Link Capability Diagnostics — `CANDIDATE`
- [53] Wi-Fi Roaming & BSSID Transition Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-007 — Network driver/device health

Status: CANDIDATE

Type: Diagnostic

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [15] Network Driver & Device Health Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-008 — TCP/IP stack & transport diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration, Diagnostic / Optimization, Diagnostic / Repair

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [17] Receive Side Scaling Diagnostics — `CANDIDATE`
- [18] Receive Segment Coalescing Diagnostics — `CANDIDATE`
- [19] Network Offload Capability Diagnostics — `CANDIDATE`
- [20] Interrupt Moderation & Network DPC Correlation — `CANDIDATE`
- [21] TCP Stack Configuration Audit — `CANDIDATE`
- [22] TCP Receive Window Auto-Tuning Diagnostics — `CANDIDATE`
- [23] TCP Congestion Control Awareness — `CANDIDATE`
- [24] MTU & Path MTU Diagnostics — `CANDIDATE`
- [25] IPv4 / IPv6 Health & Preference Diagnostics — `CANDIDATE`
- [57] Transport Protocol Awareness — `CANDIDATE`
- [59] TCP Connection State & Socket Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-009 — Firewall, profile, filter & policy diagnostics

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration, Diagnostic / Safeguard

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [26] QoS / DSCP Policy Diagnostics — `CANDIDATE`
- [33] Network Profile & Firewall Context Awareness — `CANDIDATE`
- [65] Network Filter & WFP Interference Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-010 — Traffic, bandwidth & bufferbloat diagnostics

Status: CANDIDATE

Type: Benchmark, Diagnostic, Diagnostic / Benchmark, Diagnostic / Configuration, Diagnostic / Monitoring, Diagnostic / Optimization

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [27] Network Traffic Attribution — `CANDIDATE`
- [28] Bandwidth Saturation Diagnostics — `CANDIDATE`
- [29] Queueing Latency / Bufferbloat Diagnostics — `CANDIDATE`
- [30] Background Network Activity Diagnostics — `CANDIDATE`
- [34] Metered Connection & Data Usage Awareness — `CANDIDATE`
- [45] Network Throughput Benchmark — `CANDIDATE`
- [46] Local Network vs Internet Performance Separation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-011 — VPN, proxy, tunnel & virtual-network awareness

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration, Diagnostic / Tool

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [31] VPN, Proxy & Tunnel Awareness — `CANDIDATE`
- [32] Virtual Network Adapter & Virtual Switch Awareness — `CANDIDATE`
- [60] Proxy / PAC / WPAD Diagnostics — `CANDIDATE`
- [62] WSL & Hyper-V Network Architecture Awareness — `CANDIDATE`
- [64] Native Packet Capture & Drop Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-012 — Network power-management & sleep/wake correlation

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [35] Network Adapter Power Management Diagnostics — `CANDIDATE`
- [36] Energy Efficient Ethernet Diagnostics — `CANDIDATE`
- [37] Wake-on-LAN Capability & Configuration — `CANDIDATE`
- [68] Sleep / Wake Network Failure Correlation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-013 — Network history, drift & regression correlation

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Monitoring

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [38] Network Connectivity History & Stability Monitoring — `CANDIDATE`
- [39] Network Configuration Drift Detection — `CANDIDATE`
- [69] Network Regression Correlation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-014 — Staged network repair & rollback

Status: CANDIDATE

Type: Diagnostic / Benchmark, Diagnostic / Repair, Repair, Repair / Safeguard, Safeguard / Repair

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: High

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [40] Staged Network Repair — `CANDIDATE`
- [41] DHCP Lease & Address Repair — `CANDIDATE`
- [42] DNS Cache & Resolver Repair — `CANDIDATE`
- [43] Winsock / TCP-IP Stack Repair — `CANDIDATE`
- [44] Full Network Reset Safeguard — `CANDIDATE`
- [50] Network Change Backup & Rollback — `CANDIDATE`
- [51] Network Optimization Outcome Validation — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-015 — Network recommendation & benchmark safeguards

Status: CANDIDATE

Type: Benchmark / Safeguard, Diagnostic / Quality of Life

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Low

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [49] Network Recommendation Explanation — `CANDIDATE`
- [70] Network Benchmark Methodology & Privacy Safeguards — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.

## C-NETWORK-016 — Mobile broadband & hotspot awareness

Status: CANDIDATE

Type: Diagnostic, Diagnostic / Configuration / Repair

Technical Domain: NETWORK

Primary Product Area: TBD

Final UI Placement: TBD

Risco inicial: Medium

Descrição:
Capacidade consolidada que reúne os itens abaixo por representarem partes do mesmo comportamento de produto, etapas do mesmo diagnóstico/ação, ou mecanismos inseparáveis de validação e segurança.

Origem / itens absorvidos:
- [54] Mobile Broadband & Cellular Network Awareness — `CANDIDATE`
- [55] Mobile Hotspot & Internet Connection Sharing Diagnostics — `CANDIDATE`

Decisão de consolidação:
Mantido como uma única capability de alto nível. Detalhes técnicos permanecem rastreáveis pelos itens de origem e deverão ser aprofundados somente na Feature Spec, conforme `01-DISCOVERY.md`.
