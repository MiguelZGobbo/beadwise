# C-DISPLAY-002 — native display inventory and validation proof

This read-only prototype covers the safe portions of:

- `C-DISPLAY-001`: native active-path count plus adapter/display identity;
- `C-DISPLAY-002`: current/supported mode enumeration and non-persistent validation.

The .NET console calls `GetDisplayConfigBufferSizes`, `EnumDisplayDevices`,
`EnumDisplaySettings`, and `ChangeDisplaySettingsEx` with `CDS_TEST`. It validates the current mode,
submits a deliberately invalid 1x1 mode for validation, and proves that width, height, frequency,
and color depth remain unchanged afterward. It never calls a display Apply operation.

Run:

```powershell
& .\prototypes\display\C-DISPLAY-002\tests\display-native.tests.ps1
```

The evidence does not yet resolve target names through full CCD path structures, parse EDID, test
hotplug, or prove a real mode Apply/Rollback. Those gates remain open.
