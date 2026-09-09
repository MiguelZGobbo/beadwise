# C-SECURITY-006 - Admin/UAC token proof

Feature: UAC, encryption and sign-in protection
ID: C-SECURITY-006

Objetivo da prova: validar como a campanha deve registrar operacoes que exigem administrador quando a sessao atual nao esta elevada.

Premissa central: falta de privilegio precisa virar erro estruturado/admin-required observado, nao falha silenciosa nem PASS inferido.

Ambiente: Windows real registrado em `prototypes/system/C-SYSTEM-001/results/environment.json`.

Pre-requisitos: PowerShell e `net.exe`.

Risco: baixo, read-only. O comando `net session` e usado apenas como sonda de permissao e nao altera estado.

Metodo: `tests/admin-uac.tests.ps1` executa `scripts/admin-uac-proof.ps1` e valida o contrato JSON.

Como executar:

```powershell
pwsh -NoProfile -File .\tests\admin-uac.tests.ps1
```

Resultado esperado: TOKEN_DETECT com `PASS`; ADMIN_REQUIRED_SIGNAL com `PASS` quando a sonda administrativa retorna acesso negado em sessao nao elevada.

Resultado observado: consultar `results/admin-uac-proof.json`.

Limitacoes: nao dispara prompt UAC, nao testa helper elevado, IPC ou continuidade apos consentimento.
