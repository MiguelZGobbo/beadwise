# C-SYSTEM-001 — Environment evidence prototype

Feature: System identity, baseline, and compatibility context
ID: C-SYSTEM-001
Objetivo da prova: Capturar uma matriz mínima, estruturada e reproduzível do Windows real usada por todas as provas da Fase 2.
Premissa central: CIM e as APIs de identidade do Windows expõem OS, arquitetura, CPU, GPU, placa-mãe, form factor, bateria, virtualização e estado de elevação sem mutação persistente.
Ambiente: Windows real onde o script é executado.
Pré-requisitos: PowerShell 7+ ou Windows PowerShell 5.1; CIM local disponível.
Risco: Baixo; somente leitura. O JSON persistido omite MAC, PnP instance IDs e outros identificadores únicos desnecessários.
Método: Consultar classes CIM documentadas e o token do processo; normalizar o resultado em JSON.
Como executar: `pwsh -NoProfile -File .\scripts\collect-environment.ps1`
Como testar: `pwsh -NoProfile -File .\tests\collect-environment.tests.ps1`
Resultado esperado: JSON com todos os campos obrigatórios e pelo menos uma CPU e uma GPU.
Resultado observado: Preenchido em `results/environment.json` após a execução real.
Limitações: Uma máquina não demonstra compatibilidade universal; chipset não é inferido sem uma fonte autoritativa; ausência de bateria/VM é válida somente para este host.
