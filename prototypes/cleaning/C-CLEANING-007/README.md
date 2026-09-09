# C-CLEANING-007 — isolated cleanup contract proof

This prototype exercises the shared cleanup safety contract used by:

- `C-CLEANING-001`: candidate classification, provenance, and byte accounting;
- `C-CLEANING-002`: safe cleanup of allowlisted, rebuildable data;
- `C-CLEANING-005`: retention and locked diagnostic-artifact exclusion;
- `C-CLEANING-006`: protected, unknown, and reparse-point default-deny guards;
- `C-CLEANING-007`: plan, dry-run, apply, verify, and final fixture cleanup.

The script creates a unique directory below the current user's temporary directory. It never reads
or deletes real cleanup candidates. A `finally` block disposes the controlled lock and removes the
entire fixture. `results/cleanup-sandbox-proof.json` is the persisted evidence artifact.

Run:

```powershell
& .\prototypes\cleaning\C-CLEANING-007\tests\cleanup-sandbox.tests.ps1
```

This is a representative mechanism proof, not proof that production cleanup roots, multi-profile
ownership, every link type, or irreversible user-data cleanup are ready for `PROVEN`.
