# Safe CPU capability proofs

Mapped specs: `C-CPU-001`, `C-CPU-002`, `C-CPU-004`, `C-CPU-011`, `C-CPU-015`.

The prototype executes distinct evidence paths: native processor topology through
`GetLogicalProcessorInformationEx`, deterministic bounded single/multi-thread workloads,
CPU-vs-temporary-file-I/O fixtures, a default-deny tweak safety gate with an OS-state
fingerprint, and bounded `GetSystemTimes` monitoring-overhead measurement.

It never changes Registry, boot configuration, power policy, affinity, or priority. The only
filesystem mutation is a unique temporary directory deleted in `finally`. Results explicitly
retain `PARTIAL`, `UNSUPPORTED`, and `FAIL`; the proof does not claim cross-hardware coverage.

Run:

```powershell
pwsh -NoProfile -File .\tests\cpu-safe-capabilities.tests.ps1
```
