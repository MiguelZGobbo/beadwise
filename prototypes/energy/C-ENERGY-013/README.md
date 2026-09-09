# Timer-resolution energy diagnostics proof

Mapped spec: `C-ENERGY-013`.

The prototype executes `NtQueryTimerResolution` repeatedly, records the effective resolution, and
captures read-only `powercfg /requests` output. It also attempts a one-second `powercfg /energy`
capture into a unique temporary directory and preserves the real exit/result before deleting the
report. It never calls `timeBeginPeriod`, changes a power plan, or infers energy impact from timer
resolution alone. Per-process attribution remains explicitly `UNSUPPORTED` until a stable
structured source is proven.

Run:

```powershell
pwsh -NoProfile -File .\tests\timer-resolution.tests.ps1
```
