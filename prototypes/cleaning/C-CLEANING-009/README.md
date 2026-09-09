# C-CLEANING-009 — prova de awareness multi-volume

## Feature e ID

- Feature: Multi-drive cleanup awareness
- ID: `C-CLEANING-009`

## Objetivo da prova

Confirmar que o mecanismo consegue enumerar volumes montados de forma read-only, atribuir roots
candidatos ao volume correspondente e normalizar estados de volume com política conservadora, sem
executar limpeza nem persistir caminhos do perfil do usuário.

## Premissa central

`Get-Volume` fornece informação estruturada suficiente para identificar os volumes montados e seu
estado básico. Estados fixed, removable, network e offline podem ser normalizados com default-deny,
sem interpretar fixtures como hardware realmente conectado e sem autorizar cleanup automático.

## Ambiente

- Windows 11 Pro, versão/build `10.0.26200`, x64.
- Desktop AMD Ryzen 7 5700.
- Sessão não elevada (`Admin: No`).
- PowerShell 7 (`pwsh`) com o módulo Storage e o cmdlet `Get-Volume` disponíveis.
- Volumes montados observados na execução: `C:\` e `D:\`, ambos fixed, NTFS, healthy e operational
  `OK`.

## Pré-requisitos

- Windows com `Get-Volume` disponível.
- PowerShell capaz de executar scripts locais do workspace.
- Permissão de leitura dos metadados dos volumes.
- Diretório de saída gravável para o JSON de evidência.
- Não requer elevação, volume removível, share de rede nem volume offline real.

## Risco

Baixo. O script consulta metadados de volumes e grava somente o arquivo JSON indicado por
`-OutputPath`. Ele não remove arquivos, não executa cleanup, não monta/desmonta volumes e não altera
configuração do sistema. O resultado observado registrou `mutationCount: 0`.

## Método

1. Enumerar com `Get-Volume` os volumes que possuem drive letter.
2. Registrar root, tipo, filesystem, health, operational status, capacidade e espaço livre.
3. Atribuir somente os rótulos não sensíveis `SYSTEM_TEMP` e `PROTOTYPE_ROOT` quando aplicáveis.
4. Exercitar fixtures declaradas para `FIXED`, `REMOVABLE`, `NETWORK` e `OFFLINE`, mantendo
   `eligibleForAutomaticCleanup: false` em todas elas.
5. Persistir a evidência estruturada em JSON.
6. Validar no teste que existe ao menos um volume real, que o volume do sistema foi identificado,
   que as fixtures offline/network existem, que não houve mutação e que nenhum caminho de perfil foi
   exposto.

## Como executar

Gerar novamente o resultado no caminho do prototype:

```powershell
& .\prototypes\cleaning\C-CLEANING-009\scripts\inspect-multi-volume.ps1 `
  -OutputPath .\prototypes\cleaning\C-CLEANING-009\results\multi-volume.json
```

Executar o teste isolado:

```powershell
& .\prototypes\cleaning\C-CLEANING-009\tests\multi-volume.tests.ps1
```

## Resultado esperado

- JSON válido com `featureId: C-CLEANING-009`.
- Pelo menos um volume montado inventariado e volume do sistema presente.
- Estados fixed/removable/network/offline representados sem liberar cleanup automático.
- `mutationCount: 0`.
- Nenhum caminho bruto de perfil do usuário persistido.
- Teste concluído com `PASS`.

## Resultado observado

A execução de 2026-09-08 produziu
`results/multi-volume.json` com status `PASS`. Foram observados dois volumes reais (`C:\` e `D:\`),
ambos fixed/NTFS/healthy/OK; `C:\` recebeu o rótulo `SYSTEM_TEMP` e `D:\`, onde o prototype estava,
recebeu `PROTOTYPE_ROOT`. As quatro fixtures de normalização permaneceram inelegíveis para cleanup,
`mutationCount` foi zero e o teste registrou:

```text
PASS: multi-volume inventory and classification ran read-only
```

## Limitações

- Nenhum cleanup `Apply` foi executado.
- Removable, network e offline são fixtures de normalização, não alegações sobre hardware ou shares
  presentes durante a prova.
- Mount points sem drive letter, traversal por reparse points e double-counting em topologias
  adversariais não foram exercitados.
- A prova cobre este host Windows e não estabelece compatibilidade global.
- O `PASS` pertence à fatia executada do prototype; não promove a Feature Spec a `PROVEN` ou
  `APPROVED`.
