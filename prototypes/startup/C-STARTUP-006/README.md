# C-STARTUP-006 cluster — startup contracts and owned rollback

Exercises `C-STARTUP-003`, `005`, `006`, `007`, and `008` with explicit identity confidence, drift, command-kind resolution, safety guards, and a hash-verified rollback of a prototype-owned `%TEMP%` file.

Run: `pwsh -NoProfile -File .\tests\startup-contract.tests.ps1`

No Run/RunOnce key, Startup folder, task, service, application, logoff or reboot is changed or invoked. Registry rollback remains unproved by this cluster.
