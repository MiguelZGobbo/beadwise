# C-SYSTEM-018 — Restart Manager owned-lock proof

Creates one file and one lock-holder process in a unique `%TEMP%` directory, identifies the owner with `RmStartSession`/`RmRegisterResources`/`RmGetList`, terminates only that known child, verifies exclusive access, restarts a new owned child, verifies it, and cleans everything in `finally`.

Run: `pwsh -NoProfile -File .\tests\restart-manager.tests.ps1`

`RmShutdown`/`RmRestart` are intentionally not used. No external, critical, service, kernel, antivirus, user, or system-owned process/resource is touched.
