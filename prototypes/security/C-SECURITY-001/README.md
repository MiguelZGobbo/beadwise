# Read-only Windows security capability proofs

Mapped specs: `C-SECURITY-001`, `C-SECURITY-003`, `C-SECURITY-007`.

The prototype reads effective Defender/firewall/provider state, managed-policy key presence,
Secure Boot, TPM, Device Guard, OS build, hotfixes, and Windows Update history. Each source has an
independent `PASS`, `UNSUPPORTED`, or `FAIL` outcome. No security setting is changed. Lifecycle
classification remains explicitly `UNSUPPORTED` because local state is not an official,
versioned lifecycle dataset.

Run:

```powershell
pwsh -NoProfile -File .\tests\security-readonly.tests.ps1
```
