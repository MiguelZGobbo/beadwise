# C-SYSTEM-012 - ETW/WPR preflight proof

Feature: Performance, file, registry tracing and failed I/O diagnostics
ID: C-SYSTEM-012

Objetivo da prova: validar se WPR/ETW esta presente, quais perfis existem e se uma sessao curta pode ser iniciada nesta conta nao elevada.

Premissa central: ETW/WPR e ferramenta real para diagnostico profundo, mas pode exigir elevacao. Falha por permissao deve permanecer como evidencia, nao como PASS inferido.

Ambiente: Windows real registrado em `prototypes/system/C-SYSTEM-001/results/environment.json`.

Pre-requisitos: `wpr.exe` no sistema.

Risco: baixo a medio. O script tenta iniciar e parar uma sessao WPR curta; se iniciar, para imediatamente e salva o `.etl` em `artifacts/`.

Metodo: `tests/etw-wpr.tests.ps1` executa `scripts/etw-wpr-proof.ps1` e valida que a presenca/status/perfis foram observados. A tentativa de trace pode ser PASS ou FAIL com erro real.

Como executar:

```powershell
pwsh -NoProfile -File .\tests\etw-wpr.tests.ps1
```

Resultado esperado: WPR_PRESENT, WPR_STATUS e WPR_PROFILES com `PASS`; TRACE_START_STOP reflete o resultado real no host.

Resultado observado: consultar `results/etw-wpr-proof.json`.

Limitacoes: nao faz analise WPA/ETL nem prova diagnostico de I/O real; sem elevacao, a captura pode ficar bloqueada.
