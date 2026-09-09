# C-PROCESSES-007 — Owned process restart lifecycle

Feature: Guarded process termination, restart & resource-owner remediation
ID: C-PROCESSES-007
Premissa: ChangePlan/dry-run e lifecycle podem ser validados com um processo-filho exclusivamente pertencente ao prototype.
Risco: baixo; dois `pwsh` ocultos e inofensivos são criados serialmente e encerrados em `finally`. Nenhum processo do usuário é tocado.
Execução: `pwsh -NoProfile -File .\scripts\restart-owned-process.ps1 -OutputPath .\results\process-restart.json`
Teste: `pwsh -NoProfile -File .\tests\process-restart.tests.ps1`
Limites: não prova processos protegidos, serviços, aplicações GUI com estado não salvo, Restart Manager, UWP ou múltiplos owners.
