# Safe process capability proofs

Mapped specs: `C-PROCESSES-001`, `C-PROCESSES-002`, `C-PROCESSES-004`,
`C-PROCESSES-005`, `C-PROCESSES-006`.

All lifecycle and scheduling operations target child `pwsh` processes created by the prototype.
PID/PPID mapping, temporal CPU/working-set sampling, deterministic exit-code observation,
priority/affinity apply-and-restore, and thread/handle inspection produce separate evidence records.
Cleanup in `finally` stops every owned child and removes the unique temporary directory. User,
service, and protected processes are never mutated.

Run:

```powershell
pwsh -NoProfile -File .\tests\process-safe-capabilities.tests.ps1
```
