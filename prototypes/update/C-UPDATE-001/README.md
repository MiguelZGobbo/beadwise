# C-UPDATE-001 cluster — read-only Update Agent evidence

Exercises `C-UPDATE-001`, `003`, `006`, `008`, and `010`: Update Agent history, independent reboot markers, servicing prerequisites, volume capacity, temporal-correlation rejection, and policy-source presence.

Run: `pwsh -NoProfile -File .\tests\update-proof.tests.ps1`

The prototype does not search/download/install/uninstall updates, start repair, change policy, restart a service, or reboot. Missing payload requirements and effective MDM/WSUS ownership remain `PARTIAL`/`UNKNOWN`.
