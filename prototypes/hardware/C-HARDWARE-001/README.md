# C-HARDWARE-001 cluster — native firmware and evidence handling

Runs the exact safe premises for `C-HARDWARE-001`, `C-HARDWARE-004`, and `C-HARDWARE-009`: raw SMBIOS size through Win32 `GetSystemFirmwareTable`, CIM cross-check, cross-source topology counts, and identifier redaction/integrity. The host is read-only. The privacy case uses a synthetic payload.

Run: `pwsh -NoProfile -File .\tests\hardware-proof.tests.ps1`

Limits: one x64 desktop; no malformed SMBIOS, dock/VM/laptop, vendor health failure, or real failed hardware session.
