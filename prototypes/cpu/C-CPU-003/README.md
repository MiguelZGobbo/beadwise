# C-CPU-003 — PDH CPU sampling

Feature: CPU utilization & saturation diagnostics
ID: C-CPU-003
Premissa: `PdhAddEnglishCounterW` permite usar o nome inglês estável do contador mesmo em Windows localizado.
Risco: baixo; somente leitura.
Execução: `pwsh -NoProfile -File .\scripts\sample-cpu-pdh.ps1 -OutputPath .\results\pdh-samples.json`
Teste: `pwsh -NoProfile -File .\tests\pdh-sampling.tests.ps1`
ETW: `capture-cpu-etw.ps1` tenta uma captura WPR CPU curta somente quando não existe sessão ativa; registra bloqueio/falha de forma estruturada e cancela uma sessão iniciada pelo próprio prototype em caso de erro.
Limites: uma sessão curta não prova thresholds, workload, per-core nem compatibilidade ampla.
