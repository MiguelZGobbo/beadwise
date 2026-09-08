# C-SYSTEM-017 — Shared diagnostic evidence bundle

Feature: Shared diagnostic evidence correlation and export
ID: C-SYSTEM-017
Objetivo da prova: Demonstrar coleta read-only, estruturada e correlacionável de sinais nativos usados por múltiplos domínios.
Premissa central: Um bundle comum pode registrar proveniência, status e dados resumidos sem transformar uma coleta genérica em prova de toda feature consumidora.
Ambiente: Windows real registrado por `C-SYSTEM-001`.
Pré-requisitos: PowerShell com módulos nativos CimCmdlets, PnpDevice, NetAdapter, Defender e Storage disponíveis conforme a edição.
Risco: Baixo; somente leitura. O modo completo executa `DISM /CheckHealth`, que não repara nem altera a imagem.
Método: Executar probes independentes por domínio, capturar resultado ou erro estruturado e gravar JSON UTF-8.
Como executar: `pwsh -NoProfile -File .\scripts\collect-domain-evidence.ps1`
Como testar: `pwsh -NoProfile -File .\tests\collect-domain-evidence.tests.ps1`
Resultado esperado: Vinte domínios, pelo menos um probe por domínio e status explícito por probe.
Resultado observado: `results/domain-evidence.json`; consultar cada probe. PASS significa que aquele probe executou, não que todas as Feature Specs mapeadas estejam PROVEN.
Limitações: Não exercita Apply, rollback, falhas artificiais, reboot, hardware ausente, vendors ausentes, VM, ARM64 nem política corporativa. Dados de CIM podem ser incompletos ou específicos do driver. O resultado persistido omite MAC, PnP instance IDs e nomes/IDs de processos.
