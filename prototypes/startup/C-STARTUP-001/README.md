# C-STARTUP-001 — Controlled startup inventory

Feature: Startup entry inventory & source coverage
ID: C-STARTUP-001
Premissa: coletores por fonte preservam escopo e proveniência sem reduzir toda inicialização a um único mecanismo.
Risco: baixo. O coletor é read-only; o teste cria uma entrada HKCU Run inofensiva, detecta-a e restaura exatamente o estado anterior em `finally`. A entrada não é executada e nenhum logoff/reboot ocorre.
Execução: `pwsh -NoProfile -File .\scripts\collect-startup-inventory.ps1 -OutputPath .\results\startup-inventory.json`
Teste: `pwsh -NoProfile -File .\tests\startup-inventory.tests.ps1`
Privacidade: comandos reais são omitidos por padrão e substituídos por SHA-256.
Limites: prova HKCU/HKLM Run e Startup folders; não prova RunOnce/WOW64, atalhos, Task Scheduler, SCM, packaged startup tasks ou logon real.
