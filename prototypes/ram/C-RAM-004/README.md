# C-RAM-004 cluster — bounded native memory proof

Exercises `C-RAM-001`, `004`, `006`, `009`, `010`, `012`, `014`, `016`, and `017` with native memory/NUMA/large-page reads, a 64 MiB process-local allocation/copy, a localized PDH-backed counter, and explicit guard fixtures.

Run: `pwsh -NoProfile -File .\tests\ram-proof.tests.ps1`

The allocation is capped and released in-process. No policy, pagefile, privilege, registry, service, or persistent state is changed. `PARTIAL` is preserved for leak, large-page allocation, and workload-classification claims.
