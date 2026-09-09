# C-GPU-015 — AMD ADL availability and adapter enumeration

Feature: GPU vendor capability & profile layer
ID: C-GPU-015
Premissa: o driver AMD instalado expõe a API pública ADL e permite criar/destruir contexto e enumerar adapters sem mudança de configuração.
Risco: baixo; somente APIs `Get`/lifecycle, nenhuma API `Set`, tuning ou perfil.
Execução: `pwsh -NoProfile -File .\scripts\probe-amd-adl.ps1 -OutputPath .\results\amd-adl.json`
Teste: `pwsh -NoProfile -File .\tests\amd-adl.tests.ps1`
Fonte técnica: AMD GPUOpen Display Library SDK (`ADL_Main_Control_Create`, `ADL_Adapter_NumberOfAdapters_Get`, `ADL_Main_Control_Destroy`).
Limites: prova somente AMD ADL classic neste driver/RX 570; não prova ADLX, NVIDIA, Intel, telemetria, tuning ou profile apply/rollback.
