# C-SYSTEM-007 — Registry drift capture

Feature: System change history, drift & anomaly detection
ID: C-SYSTEM-007
Premissa: snapshots normalizados e com proveniência distinguem estado inexistente de estado criado e produzem drift explícito.
Risco: baixo; o coletor é read-only. O teste cria apenas `HKCU:\Software\BeadWise\Phase2Proof\C-SYSTEM-007` e sempre o remove em `finally`.
Execução controlada: `pwsh -NoProfile -File .\scripts\run-controlled-proof.ps1 -ResultDirectory .\results` captura estado inexistente, cria somente a fixture, captura drift e remove a fixture em `finally`.
Teste: `pwsh -NoProfile -File .\tests\drift-capture.tests.ps1`
Limites: prova somente uma fonte Registry controlada; não prova SCM, Task Scheduler, DISM, Event Log, ETW, correlação causal ou persistência após reboot.
