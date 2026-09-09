# C-DRIVERS-001 — read-only driver evidence proof

This prototype exercises safe portions of five driver capabilities:

- `C-DRIVERS-001`: signed-driver metadata and present-device inventory;
- `C-DRIVERS-002`: actual healthy/problem-state enumeration plus explicit synthetic normalization fixtures;
- `C-DRIVERS-005`: conservative dry-run refusal for present critical device classes;
- `C-DRIVERS-007`: SetupAPI/Event Log timeline sources without a causal claim;
- `C-DRIVERS-008`: a hashed metadata/history manifest, explicitly not called a backup or rollback.

Sources are `Win32_PnPSignedDriver`, `Get-PnpDevice`, `pnputil.exe /enum-drivers`,
`setupapi.dev.log`, and read-only System Event Log queries. Localized `pnputil` text is hashed but
not parsed as structured truth. Raw device IDs and names are discarded; the artifact keeps counts,
class/provider distributions, and hashes only. No install, removal, export, disable, restart,
`/force`, or other driver mutation is attempted.

Run:

```powershell
& .\prototypes\drivers\C-DRIVERS-001\tests\driver-readonly.tests.ps1
```

The result remains partial: this host had no actual problem device, controlled regression, managed
policy fixture, sacrificial driver package, or restore test.
