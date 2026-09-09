# C-REPAIR-006 cluster — orchestration contracts without repair

Exercises `C-REPAIR-006`, `008`, `009`, and `010` with owner dispatch, temporal known-good history, source availability, and FULL/PARTIAL/NO readiness gates. Only executable presence is read from the host.

Run: `pwsh -NoProfile -File .\tests\repair-proof.tests.ps1`

No DISM/SFC/Store/device/subsystem repair, download, service action, snapshot, or reboot is executed.
