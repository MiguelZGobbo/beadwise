# DXGI and D3D GPU capability proofs

Mapped specs: `C-GPU-001`, `C-GPU-002`, `C-GPU-006`.

This package exercises the actual Windows graphics interfaces without changing driver or display
state: DXGI adapter enumeration and descriptors, `IDXGIAdapter3::QueryVideoMemoryInfo`, CIM
identity correlation, and D3D11 hardware feature-level negotiation. Each capability emits its own
status and limitation; unavailable interfaces remain `UNSUPPORTED` or `FAIL`.

Run:

```powershell
pwsh -NoProfile -File .\tests\gpu-dxgi.tests.ps1
```
