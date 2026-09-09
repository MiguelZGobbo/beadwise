# C-STORAGE-001 cluster — safe storage reads and owned rollback

Exercises exact premises for `C-STORAGE-001` through `006`, `009`, `011`, and `020`. Real disks are read-only: structured inventory, provider reliability/temperature, `chkdsk /scan`, TRIM policy query, and localized PDH sampling. The only mutation is a prototype-owned file under a unique `%TEMP%` directory, quarantined and restored under `finally` cleanup.

Run: `pwsh -NoProfile -File .\tests\storage-proof.tests.ps1`

All provider access failures and unsupported fields remain explicit. No fix, retrim, format, delete, optimization, cache-policy change, VSS change, or user-file operation is issued.
