# C-USB-001 cluster — safe peripheral inventory

Read-only probes for `C-USB-001`, `002`, `005`, `010`, and `015`. They enumerate current PnP USB/HID/Bluetooth state, hash instance IDs, preserve negotiated speed as `UNSUPPORTED` when unavailable, and exercise explicit manual-review/policy safeguard fixtures.

Run: `pwsh -NoProfile -File .\tests\usb-proof.tests.ps1`

No device handle is opened, no input is injected, and no device/policy is restarted, disabled, re-scanned, paired, or changed.
