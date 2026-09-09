# C-AUDIO-001 — Core Audio endpoint proof

This native .NET prototype uses the documented Windows Core Audio COM interfaces plus read-only
PnP queries to enumerate render and capture endpoints, stable endpoint identity hashes, MMDevice
state masks, property-store friendly-name availability, default roles, and transport candidates.
Raw IDs and names are discarded before the evidence artifact is written. It provides real,
read-only evidence for safe portions of:

- `C-AUDIO-001`: endpoint inventory, direction, state, identity, and role;
- `C-AUDIO-003`: naturally present active/disabled/unplugged endpoint states;
- `C-AUDIO-007`: capture endpoint presence only (no stream or recording);
- `C-AUDIO-006` and `C-AUDIO-010`: PnP transport candidate inventory and endpoint identity input
  for later exact endpoint-to-parent correlation.

Run:

```powershell
& .\prototypes\audio\C-AUDIO-001\tests\core-audio.tests.ps1
```

The probe does not change endpoint state, defaults, volume, mute, routing, privacy, services, or
drivers. It stores hashed endpoint/PnP IDs and aggregate counts only; exact endpoint-to-parent
correlation and virtual/physical classification remain untested until correlated with
DeviceTopology and consumer scenarios. No audio stream is opened and no user audio is captured or
retained.
