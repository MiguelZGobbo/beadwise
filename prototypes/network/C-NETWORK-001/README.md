# C-NETWORK-001 cluster — active path, temporal sample and drift

Read-only probes for `C-NETWORK-001`, `002`, `003`, `005`, `007`, and `013`. They exercise the current adapter/route, loopback and gateway separately, a bounded temporal jitter sample, Ethernet advanced properties, driver correlation, and deterministic drift normalization.

Run: `pwsh -NoProfile -File .\tests\network-proof.tests.ps1`

Limits: loopback is explicitly `PARTIAL` for WAN diagnostics; no captive portal, packet impairment, cable fault, Wi-Fi/VPN transition, or driver rollback.
