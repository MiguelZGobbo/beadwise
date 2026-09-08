# BeadWise V2 — Discovery — Cleaning — First Pass

**Status da área:** INITIAL DISCOVERY — FIRST PASS  
**Método:** Broad Discovery com filtro rígido de relevância, UX, segurança e granularidade consolidada  
**Regra canônica:** `01-DISCOVERY.md`  
**Filosofia:** Detect → Understand → Recommend → Apply → Measure → Keep / Rollback

---

# Escopo e interpretação da área

Cleaning deve ser tratada principalmente como **gerenciamento seguro de espaço e dados descartáveis**, e não como um mecanismo genérico de aumento de performance.

Uma limpeza só deve ser considerada valiosa quando houver pelo menos um destes resultados claros:

- recuperar espaço relevante;
- evitar ou resolver estado de pouco espaço livre;
- permitir updates/instalações que estavam bloqueados por falta de espaço;
- remover dados temporários realmente descartáveis;
- reduzir crescimento anormal de caches ou artefatos;
- melhorar organização/visibilidade do armazenamento;
- remover resíduos com ownership suficientemente confiável;
- auxiliar diagnóstico ou manutenção sem destruir evidências úteis.

Não assumir:

- cache = lixo;
- arquivo antigo = inútil;
- arquivo grande = removível;
- AppData = descartável;
- log = lixo;
- temporário = seguro para apagar em qualquer momento;
- mais espaço livre = mais FPS;
- limpeza frequente = PC mais rápido.

---

# 1. Storage Pressure & Recoverable Space Assessment

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Storage / Cleaning

Primary Product Area: Optimization

Possible Consumers:

- Optimization
- My PC
- Cleaning
- Recommendations

Final UI Placement: TBD

Descrição: Avaliar pressão de armazenamento e estimar se existe espaço recuperável relevante antes de propor qualquer limpeza.

Possível objetivo: Distinguir um PC realmente limitado por espaço de um sistema onde uma limpeza agressiva traria pouco valor.

Possível benefício: Diagnostic, Storage, Stability, Quality of Life

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- espaço livre absoluto e percentual por volume;
- volume do sistema vs volumes secundários;
- tendência de crescimento quando houver histórico;
- presença de estado de low disk;
- espaço potencialmente recuperável por categoria;
- impacto potencial sobre Windows Update, instalações e operação normal.

---

# 2. Cleanup Inventory & Category Classification

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Storage / Cleaning

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Optimization
- Recommendations

Final UI Placement: TBD

Descrição: Construir um inventário consolidado de categorias de dados potencialmente limpáveis, sem assumir que todas devem ser removidas.

Possível objetivo: Dar ao produto uma visão segura e explicável do que ocupa espaço.

Possível benefício: Storage, Diagnostic, Quality of Life

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- categoria;
- tamanho estimado;
- origem;
- ownership;
- idade;
- possibilidade de reconstrução;
- risco de remoção;
- necessidade de privilégios;
- mecanismo oficial disponível;
- dependência de rollback ou diagnóstico.

---

# 3. Safe Temporary File Cleanup

Status: CANDIDATE

Type: Optimization

Technical Domain: Windows / Storage / Cleaning

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Optimization

Final UI Placement: TBD

Descrição: Identificar e remover arquivos temporários que sejam considerados descartáveis pelo Windows ou pelo aplicativo proprietário, evitando exclusão cega de diretórios Temp inteiros.

Possível objetivo: Recuperar espaço ocupado por dados temporários sem interromper software ativo ou apagar dados de estado importantes.

Possível benefício: Storage, Quality of Life

Aplicabilidade: All PCs

Shared Capability: No

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- origem do temporário;
- idade;
- arquivo em uso/bloqueado;
- processo proprietário quando identificável;
- classificação pelo Windows;
- possibilidade de recriação;
- tamanho recuperável;
- falhas/skips por arquivos ativos.

---

# 4. Recycle Bin Space Management

Status: CANDIDATE

Type: Configuration / Optimization

Technical Domain: Storage / User Data

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Optimization
- Settings

Final UI Placement: TBD

Descrição: Medir conteúdo da Lixeira e permitir limpeza consciente, incluindo políticas de retenção compatíveis com o Storage Sense.

Possível objetivo: Recuperar espaço de arquivos que o usuário já decidiu excluir, preservando uma janela de recuperação razoável.

Possível benefício: Storage, Quality of Life

Aplicabilidade: All PCs

Shared Capability: No

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- tamanho total;
- idade dos itens;
- volume de origem;
- política atual de retenção;
- confirmação antes de remoção permanente;
- exclusão de itens recentes por padrão.

---

# 5. Downloads Folder Review

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: User Data / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Tools
- Recommendations

Final UI Placement: TBD

Descrição: Analisar Downloads como pasta de alto potencial de recuperação, mas nunca tratá-la como temporária por definição.

Possível objetivo: Encontrar instaladores, ISOs, arquivos antigos e grandes que o usuário pode não precisar mais.

Possível benefício: Storage, Quality of Life

Aplicabilidade: All PCs

Shared Capability: No

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- tamanho;
- idade;
- última abertura quando confiável;
- tipo de arquivo;
- duplicidade aparente;
- origem quando disponível;
- arquivo ainda associado a instalação/update em andamento;
- seleção explícita do usuário.

---

# 6. Large & Unused File Discovery

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: Storage / User Data

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- My PC
- Tools

Final UI Placement: TBD

Descrição: Localizar arquivos grandes ou aparentemente pouco utilizados sem classificá-los automaticamente como lixo.

Possível objetivo: Mostrar onde existe maior oportunidade real de recuperação de espaço.

Possível benefício: Storage, Diagnostic, Quality of Life

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- tamanho;
- caminho;
- tipo;
- idade;
- ownership;
- pasta conhecida;
- arquivo pessoal vs sistema/app;
- recomendações de mover, arquivar ou excluir.

---

# 7. Duplicate File Discovery

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: User Data / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Tools

Final UI Placement: TBD

Descrição: Detectar arquivos potencialmente duplicados por conteúdo, mantendo decisão final sob controle do usuário.

Possível objetivo: Recuperar espaço em coleções com cópias redundantes sem depender apenas de nome ou tamanho.

Possível benefício: Storage, Quality of Life

Aplicabilidade: Optional / Preference

Shared Capability: No

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- hash/conteúdo;
- tamanho;
- caminhos;
- origem;
- data;
- arquivo em pasta sincronizada;
- cópia de backup;
- relação hardlink/reparse point;
- seleção manual da cópia a preservar.

---

# 8. Unused Application Discovery & Uninstall Guidance

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Applications / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Optimization
- Processes
- Startup

Final UI Placement: TBD

Descrição: Identificar aplicativos grandes ou raramente usados e encaminhar para mecanismo suportado de desinstalação, sem apagar pastas manualmente.

Possível objetivo: Recuperar espaço e, secundariamente, reduzir software desnecessário.

Possível benefício: Storage, Quality of Life, Potential Background Resource Reduction

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- tamanho reportado;
- publisher;
- data/uso quando confiável;
- componentes dependentes;
- mecanismo oficial de uninstall;
- app de sistema/OEM;
- software de segurança/hardware;
- dados de usuário preservados após uninstall.

---

# 9. Cloud Content Dehydration / Free-Up-Space

Status: CANDIDATE

Type: Configuration / Optimization

Technical Domain: Cloud Storage / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Optimization
- Settings

Final UI Placement: TBD

Descrição: Tornar conteúdo sincronizado elegível em online-only quando o provedor oferece mecanismo seguro de Files On-Demand, sem apagar o arquivo da nuvem.

Possível objetivo: Recuperar espaço local preservando acesso lógico ao conteúdo.

Possível benefício: Storage, Quality of Life

Aplicabilidade: PCs using supported cloud sync providers

Shared Capability: No

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- provedor;
- estado local/online-only;
- atributo “always keep on this device”;
- última utilização;
- conectividade;
- tamanho local recuperável;
- exclusões configuradas pelo usuário.

---

# 10. Previous Windows Installation Cleanup

Status: CANDIDATE

Type: Optimization

Technical Domain: Windows Servicing / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Optimization
- Recovery

Final UI Placement: TBD

Descrição: Detectar instalações anteriores do Windows, como `Windows.old`, e oferecer remoção apenas com aviso explícito sobre perda da opção de rollback.

Possível objetivo: Recuperar vários GB após upgrade quando o usuário já não precisa voltar à versão anterior.

Possível benefício: Storage

Aplicabilidade: PCs after eligible Windows upgrade

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- existência;
- tamanho;
- data do upgrade;
- janela de rollback;
- possibilidade atual de “Go back”;
- confirmação explícita de irreversibilidade;
- mecanismo oficial do Windows.

---

# 11. Windows Update Temporary Data Cleanup

Status: CANDIDATE

Type: Optimization / Repair-adjacent

Technical Domain: Windows Update / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Windows Update
- Repair

Final UI Placement: TBD

Descrição: Identificar dados temporários ou obsoletos de update que o Windows classifica como limpáveis, sem apagar indiscriminadamente `SoftwareDistribution` ou componentes de servicing.

Possível objetivo: Recuperar espaço pós-update ou resolver crescimento anormal por mecanismos suportados.

Possível benefício: Storage, Repair

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- categoria exposta pelo Windows;
- update/reboot pendente;
- servicing ativo;
- download em progresso;
- espaço recuperável;
- necessidade de preservar arquivos para rollback;
- uso de APIs/ferramentas suportadas.

---

# 12. Delivery Optimization Cache Cleanup

Status: CANDIDATE

Type: Optimization

Technical Domain: Windows Update / Network / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Network
- Windows Update

Final UI Placement: TBD

Descrição: Expor o cache da Delivery Optimization como categoria contextual de limpeza, respeitando o fato de que o Windows já faz sua manutenção automaticamente.

Possível objetivo: Recuperar espaço adicional quando necessário, especialmente em estado de low disk.

Possível benefício: Storage

Aplicabilidade: Windows PCs using Delivery Optimization

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- tamanho;
- pressão de armazenamento;
- downloads Microsoft ativos;
- manutenção automática esperada;
- mecanismo oficial de limpeza.

---

# 13. Windows Component Store Cleanup

Status: CANDIDATE

Type: Maintenance / Optimization

Technical Domain: Windows Servicing / WinSxS

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Repair
- Windows Update

Final UI Placement: TBD

Descrição: Avaliar e, quando justificável, executar limpeza suportada do Component Store sem deletar manualmente a pasta WinSxS.

Possível objetivo: Remover componentes substituídos por mecanismos oficiais de servicing.

Possível benefício: Storage, Maintenance

Aplicabilidade: Windows PCs

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- estado do component store;
- tamanho real vs tamanho aparente;
- limpeza automática existente;
- updates/servicing pendentes;
- uso de `StartComponentCleanup`;
- evitar `ResetBase` como ação comum devido à redução de reversibilidade.

---

# 14. App-Owned Temporary Data Cleanup

Status: CANDIDATE

Type: Optimization

Technical Domain: Applications / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Tools

Final UI Placement: TBD

Descrição: Limpar dados temporários de aplicativos somente quando houver ownership e semântica suficientemente conhecidas.

Possível objetivo: Recuperar espaço de caches/temporários reconstruíveis sem apagar configuração, saves ou dados de usuário.

Possível benefício: Storage, Quality of Life

Aplicabilidade: Supported applications

Shared Capability: No

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: User Choice

Possíveis informações / dados / checks:

- aplicativo proprietário;
- categoria de dado;
- documentação/vendor knowledge;
- tamanho;
- idade;
- processo ativo;
- efeito esperado após limpeza;
- necessidade de novo download/rebuild.

---

# 15. Rebuildable Cache Classification

Status: CANDIDATE

Type: Diagnostic

Technical Domain: Applications / Windows / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Gaming
- Repair

Final UI Placement: TBD

Descrição: Classificar caches por reconstruibilidade, custo de reconstrução e utilidade, em vez de assumir que qualquer cache deve ser eliminado.

Possível objetivo: Evitar fake optimization e destacar apenas caches que oferecem recuperação de espaço ou repair contextual.

Possível benefício: Diagnostic, Storage, Stability

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- origem;
- tamanho;
- custo de rebuild/redownload;
- impacto temporário de performance após limpeza;
- função do cache;
- indicação de corrupção;
- frequência de crescimento.

---

# 16. Crash Dump & Diagnostic Artifact Retention

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Diagnostics / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Repair
- Hardware Diagnostics
- Stability Diagnostics

Final UI Placement: TBD

Descrição: Identificar dumps e artefatos de crash como dados potencialmente grandes, mas preservar evidências recentes ou ainda úteis para diagnóstico.

Possível objetivo: Recuperar espaço sem destruir a capacidade de investigar crashes.

Possível benefício: Storage, Diagnostic, Repair

Aplicabilidade: Systems with crash dumps

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- tipo de dump;
- tamanho;
- data;
- crash associado;
- diagnóstico ainda aberto;
- quantidade de artefatos similares;
- retenção mínima;
- opção de exportar antes de remover.

---

# 17. Log Retention & Cleanup Assessment

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows / Applications / Diagnostics

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Repair
- Diagnostics

Final UI Placement: TBD

Descrição: Avaliar logs que cresceram de forma relevante e diferenciar logs descartáveis de evidências necessárias para troubleshooting, auditoria ou segurança.

Possível objetivo: Controlar crescimento anormal sem apagar histórico útil indiscriminadamente.

Possível benefício: Storage, Diagnostic, Stability

Aplicabilidade: Context-dependent

Shared Capability: Yes

Risco inicial: High

Evidence Level: Context-dependent

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- origem;
- tamanho;
- retenção configurada;
- última escrita;
- componente ativo;
- valor diagnóstico;
- política corporativa;
- necessidade de exportação/backup.

---

# 18. Installation Residue Review

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: Applications / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Tools

Final UI Placement: TBD

Descrição: Detectar resíduos de instalação apenas quando houver evidência de que são arquivos transitórios ou pacotes já consumidos, sem tocar no Windows Installer Cache.

Possível objetivo: Encontrar ISOs, ZIPs, EXEs, logs e temporários de setup que permaneceram após instalações concluídas.

Possível benefício: Storage, Quality of Life

Aplicabilidade: All PCs

Shared Capability: No

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: User Choice

Possíveis informações / dados / checks:

- origem conhecida;
- instalação correspondente;
- processo de setup ativo;
- localização;
- idade;
- extensão;
- relação com cache necessário para repair/uninstall;
- confirmação explícita para arquivos fora de áreas temporárias.

---

# 19. User Data Archive / Move Recommendation

Status: CANDIDATE

Type: Recommendation / Tool

Technical Domain: User Data / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Tools
- My PC

Final UI Placement: TBD

Descrição: Recomendar mover ou arquivar dados grandes em vez de sempre sugerir exclusão.

Possível objetivo: Resolver falta de espaço preservando arquivos importantes.

Possível benefício: Storage, Quality of Life

Aplicabilidade: Systems with multiple drives, external storage or cloud storage

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- arquivos/pastas grandes;
- volumes disponíveis;
- espaço destino;
- velocidade/tipo do destino;
- nuvem disponível;
- verificação após movimentação;
- preservação de metadados quando relevante.

---

# 20. Active-Use & File-Lock Safeguard

Status: CANDIDATE

Type: Safety / Diagnostic

Technical Domain: Shared / Cleaning

Primary Product Area: Shared

Possible Consumers:

- Cleaning
- Repair
- Update

Final UI Placement: TBD

Descrição: Verificar se arquivos, diretórios ou serviços estão em uso antes de executar limpeza que possa interferir em software ativo.

Possível objetivo: Reduzir falhas, corrupção e comportamento inesperado.

Possível benefício: Stability, Safety, Compatibility

Aplicabilidade: All cleanup actions involving mutable application/system data

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- handles/locks;
- processos relacionados;
- serviço em execução;
- update/install ativo;
- sessão de aplicativo;
- retry/skip seguro;
- necessidade de reboot.

---

# 21. Cleanup Ownership & Provenance Validation

Status: CANDIDATE

Type: Safety / Diagnostic

Technical Domain: Shared / Cleaning

Primary Product Area: Shared

Possible Consumers:

- Cleaning
- Repair
- Applications

Final UI Placement: TBD

Descrição: Determinar quem criou/possui determinado dado e qual mecanismo é responsável por sua manutenção antes de oferecer remoção.

Possível objetivo: Evitar “limpeza de pasta” baseada apenas em caminho ou extensão.

Possível benefício: Safety, Diagnostic, Compatibility

Aplicabilidade: All non-trivial cleanup categories

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- componente/aplicativo proprietário;
- package identity quando houver;
- diretório conhecido;
- registro de instalação;
- documentação;
- permissões;
- relação com dados de usuário ou servicing.

---

# 22. Cleanup Preview / Dry Run

Status: CANDIDATE

Type: Safety / Tool

Technical Domain: Shared / Cleaning

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Optimization

Final UI Placement: TBD

Descrição: Mostrar exatamente o que será removido, quanto espaço será recuperado e quais consequências são esperadas antes da execução.

Possível objetivo: Tornar limpeza transparente e reversível quando possível.

Possível benefício: Safety, Quality of Life, Diagnostic

Aplicabilidade: All cleanup actions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- categorias;
- arquivos representativos;
- tamanho;
- quantidade;
- risco;
- reversibilidade;
- efeito funcional;
- itens excluídos por safeguard;
- diferenças entre estimativa e tamanho realmente recuperável.

---

# 23. Cleanup Exclusions & Protected Locations

Status: CANDIDATE

Type: Safety / Configuration

Technical Domain: Shared / Cleaning

Primary Product Area: Settings

Possible Consumers:

- Cleaning
- Optimization
- Repair

Final UI Placement: TBD

Descrição: Permitir exclusões persistentes e proteger categorias/pastas que não devem ser tocadas por automações genéricas.

Possível objetivo: Respeitar preferências do usuário, workflows profissionais e dados sensíveis à remoção.

Possível benefício: Safety, Compatibility, Quality of Life

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- caminhos excluídos;
- categorias excluídas;
- apps excluídos;
- “always keep”;
- políticas corporativas;
- herança por perfil;
- conflitos com regras automáticas.

---

# 24. Age / Context-Aware Cleanup Policy

Status: CANDIDATE

Type: Configuration

Technical Domain: Cleaning / Policy

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Settings

Final UI Placement: TBD

Descrição: Usar idade e contexto como sinais auxiliares para retenção, sem tratar timestamps como prova única de inutilidade.

Possível objetivo: Evitar exclusão prematura de itens recentes e tornar automações mais conservadoras.

Possível benefício: Safety, Storage, Quality of Life

Aplicabilidade: Recycle Bin, Downloads, temporary data, selected app caches

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: User Choice

Possíveis informações / dados / checks:

- criação/modificação;
- último acesso quando confiável;
- última abertura conhecida pelo aplicativo;
- categoria;
- tamanho;
- pressão de armazenamento;
- exceções do usuário.

---

# 25. Low-Disk Triggered Cleanup Recommendation

Status: CANDIDATE

Type: Optimization / Recommendation

Technical Domain: Storage / Cleaning

Primary Product Area: Optimization

Possible Consumers:

- Cleaning
- Optimization
- Notifications

Final UI Placement: TBD

Descrição: Recomendar ou acionar mecanismos suportados de limpeza quando o volume entra em estado realmente baixo de espaço, em vez de executar limpeza agressiva por calendário.

Possível objetivo: Concentrar Cleaning onde há benefício real.

Possível benefício: Storage, Stability, Quality of Life

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Medium

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- espaço livre;
- tamanho do volume;
- tendência de queda;
- espaço necessário para update;
- categorias seguras disponíveis;
- Storage Sense atual;
- frequência de alertas.

---

# 26. Storage Sense Configuration Awareness

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Optimization
- Settings

Final UI Placement: TBD

Descrição: Detectar e explicar a configuração do Storage Sense, evitando duplicar ou conflitar com a manutenção nativa do Windows.

Possível objetivo: Integrar o BeadWise ao comportamento oficial de limpeza automática em vez de competir com ele.

Possível benefício: Storage, Quality of Life, Compatibility

Aplicabilidade: Supported Windows versions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: User Choice

Possíveis informações / dados / checks:

- habilitado/desabilitado;
- cadence;
- política de temporários;
- retenção da Lixeira;
- Downloads;
- cloud dehydration;
- políticas administrativas;
- última execução quando disponível.

---

# 27. Cleanup Result Verification & History

Status: CANDIDATE

Type: Measurement / Diagnostic

Technical Domain: Cleaning / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Optimization
- History

Final UI Placement: TBD

Descrição: Medir o resultado real após uma limpeza e registrar o que foi removido, sem confundir estimativa com espaço efetivamente recuperado.

Possível objetivo: Dar transparência e permitir avaliação de valor real.

Possível benefício: Diagnostic, Storage, Quality of Life

Aplicabilidade: All cleanup actions

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- espaço antes/depois;
- bytes selecionados;
- bytes realmente removidos;
- itens ignorados;
- falhas;
- duração;
- categorias;
- ação executada;
- possibilidade de recuperação quando existente.

---

# Itens rejeitados / filtrados no First Pass

## Registry Cleaner

Status: REJECTED

Motivo: Não há relação confiável entre “limpar Registry” e ganho perceptível de performance; risco de quebra supera benefício plausível.

---

## RAM Cleaner / Working Set Cleaner

Status: REJECTED

Motivo: Não é Cleaning de armazenamento e já conflita com conclusões consolidadas da área RAM.

---

## Delete Prefetch / Superfetch Files as Routine Optimization

Status: REJECTED

Motivo: Cache de desempenho mantido pelo Windows; limpeza rotineira pode apenas forçar reconstrução sem ganho real.

---

## Delete Shader Caches Routinely

Status: REJECTED

Motivo: Pode causar recompilação, stutter e piora temporária de experiência. Só deve existir como ação de repair/contexto dentro de capability de cache reconstruível.

---

## Clear Browser Cache as Automatic Speed Boost

Status: REJECTED

Motivo: Browser cache existe para evitar novos downloads e acelerar acesso; limpeza pode ser útil por storage/privacy/troubleshooting, não como boost automático de performance.

---

## Clear Thumbnail / Icon Cache as Routine Cleaning

Status: REJECTED

Motivo: Pequeno benefício de storage e possível degradação temporária de UX; reconstrução faz mais sentido como repair quando há corrupção visual.

---

## Delete Windows Update Folders Manually

Status: REJECTED

Motivo: Evitar exclusão indiscriminada de `SoftwareDistribution`, `catroot2` ou servicing data. Reset desses componentes pertence a Repair/Windows Update e exige contexto.

---

## Delete Windows Installer Cache

Status: REJECTED

Motivo: `C:\Windows\Installer` contém arquivos necessários para update, repair e uninstall de produtos MSI. Não tratar como cache descartável.

---

## Delete Driver Store Packages in Bulk

Status: REJECTED

Motivo: Já coberto pela área Drivers; remoção em massa aumenta risco de quebra e recuperação pior.

---

## Delete Restore Points / VSS as Junk

Status: REJECTED

Motivo: Sacrifica recovery por espaço. Qualquer gestão de restore points pertence a Recovery e deve ser explícita.

---

## Delete Event Logs / Diagnostic Logs Indiscriminately

Status: REJECTED

Motivo: Pode destruir evidência necessária para Repair, Hardware Diagnostics, WHEA, crash analysis e segurança.

---

## Delete AppData by Folder Heuristics

Status: REJECTED

Motivo: AppData pode conter configuração, saves, bancos locais, tokens, dados offline e caches. Ownership/contexto são obrigatórios.

---

## “Clean All Temp” Without Active-Use Detection

Status: REJECTED

Motivo: Arquivos temporários podem estar ativos; limpar por caminho sem contexto não atende aos requisitos de segurança.

---

## Remove Windows Components for a Few MB

Status: REJECTED

Motivo: Degrada funcionalidade e UX por benefício marginal; componentes opcionais devem ser tratados por necessidade do usuário, não como cleaning agressivo.

---

## Disable Reserved Storage to Reclaim Space

Status: REJECTED

Motivo: Reserved Storage existe para melhorar previsibilidade de updates e manutenção. Não deve ser desativado como limpeza genérica.

---

# Principais conclusões do First Pass

1. **Cleaning não deve vender performance genérica.** O valor central é recuperar espaço real, evitar low-disk e melhorar gerenciamento de armazenamento.

2. **O Windows já possui mecanismos maduros de limpeza.** Storage Sense, Cleanup Recommendations, Disk Cleanup e servicing devem ser integrados/respeitados sempre que possível.

3. **“Cache” precisa de semântica.** Alguns caches são descartáveis, outros aceleram o sistema, outros são necessários para update/repair/uninstall.

4. **Ownership é um requisito arquitetural futuro importante.** Uma regra por caminho não é suficiente para decidir se algo é lixo.

5. **Downloads e arquivos pessoais são oportunidades grandes, mas de risco alto.** Devem ser revisão do usuário, não limpeza automática.

6. **Rollback não é igualmente possível para tudo.** Para user data, preferir Lixeira/move/archive quando viável. Para system cleanup, a ação deve deixar clara a irreversibilidade.

7. **Windows.old é o melhor exemplo de “muito espaço, alto custo de reversibilidade”.** Deve exigir aviso explícito.

8. **Crash dumps e logs não são lixo por definição.** O BeadWise deve saber quando preservar evidência diagnóstica.

9. **Automação deve ser conservadora e orientada por pressão de espaço.** “Rodar todo dia e apagar tudo” não é uma boa filosofia.

10. **Cleanup Preview / Dry Run é central para a UX.** O usuário deve saber o que será apagado, por quê e quanto será recuperado.

---

# Fontes técnicas relevantes

## Microsoft — Free up drive space in Windows
https://support.microsoft.com/en-us/windows/experience/storage-filemanagement/free-up-drive-space-in-windows

Relevância:
- Storage Sense;
- Cleanup Recommendations;
- temporary/system files;
- large or unused files;
- cloud files;
- unused apps;
- Windows.old;
- Disk Cleanup.

## Microsoft — Manage drive space with Storage Sense
https://support.microsoft.com/en-us/windows/experience/storage-filemanagement/manage-drive-space-with-storage-sense

Relevância:
- políticas de temporários;
- Lixeira;
- Downloads;
- cloud content;
- low-disk behavior;
- escopo da unidade do sistema.

## Microsoft Learn — Configure Storage Sense
https://learn.microsoft.com/en-us/windows/configuration/storage/storage-sense

Relevância:
- cadence;
- cleanup thresholds;
- temporary file cleanup;
- Downloads;
- Recycle Bin;
- cloud dehydration.

## Microsoft Learn — Clean Up the WinSxS Folder
https://learn.microsoft.com/en-us/windows-hardware/manufacture/desktop/clean-up-the-winsxs-folder?view=windows-11

Relevância:
- StartComponentCleanup;
- manutenção automática;
- grace period;
- DISM;
- riscos de ResetBase.

## Microsoft Learn — Delivery Optimization FAQ
https://learn.microsoft.com/windows/deployment/do/waas-delivery-optimization-faq

Relevância:
- cache da Delivery Optimization;
- manutenção automática;
- limpeza manual apenas quando necessário.

## Microsoft Learn — Missing Windows Installer cache
https://learn.microsoft.com/en-us/troubleshoot/windows-client/application-management/missing-windows-installer-cache

Relevância:
- `C:\Windows\Installer` não deve ser apagado;
- dependência para update/repair/uninstall.

## Microsoft Learn — Store and retrieve app data
https://learn.microsoft.com/en-us/windows/apps/develop/data/store-and-retrieve-app-data

Relevância:
- diferença entre app data e temporary app data;
- dados temporários podem ser reclaimable;
- ownership importa.

## Microsoft Learn — Read small memory dump files
https://learn.microsoft.com/en-us/troubleshoot/windows-client/performance/read-small-memory-dump-file

Relevância:
- dumps possuem valor diagnóstico;
- não devem ser tratados como lixo indiscriminado.

---

# Status final da área

**Cleaning — First Pass: INITIAL DISCOVERY COMPLETE**

Capabilities CANDIDATE: **27**

Itens rejeitados/filtrados explicitamente: **15**

A contagem foi mantida deliberadamente abaixo do padrão das áreas mais antigas. Safeguards só receberam item próprio quando respondem uma pergunta distinta e reutilizável do produto.

---

# Próximo passo

Executar:

**Cleaning — Second Pass / Consolidated Gap Audit**

Objetivo:

- procurar capabilities importantes que ficaram de fora;
- testar sobreposição entre itens;
- procurar casos modernos do Windows;
- identificar gaps de safety, cloud, multi-user, sparse/package data e automação;
- adicionar somente perguntas de produto realmente distintas.

# BeadWise V2 — Discovery — Cleaning — Second Pass / Consolidated Gap Audit

**Status da área:** SECOND PASS — CONSOLIDATED GAP AUDIT  
**Base:** `beadWise_Discovery_Cleaning_First_Pass.md`  
**Regra canônica:** `01-DISCOVERY.md`  
**Objetivo:** Encontrar lacunas reais sem transformar parâmetros, paths, caches ou tipos de arquivo em novas capabilities.

---

# Resultado do Gap Audit

O First Pass já cobre o núcleo da área:

- pressão de armazenamento;
- inventário/classificação;
- temporários;
- Lixeira;
- Downloads;
- arquivos grandes/duplicados;
- apps não usados;
- cloud dehydration;
- Windows.old;
- Windows Update / Delivery Optimization;
- Component Store;
- app-owned temp/cache;
- dumps/logs;
- resíduos de instalação;
- move/archive;
- ownership;
- active-use;
- preview;
- exclusions;
- políticas por contexto;
- Storage Sense;
- verificação pós-limpeza.

A segunda passada adiciona somente gaps que representam perguntas distintas do produto.

---

# 28. Multi-Drive Cleanup Awareness

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: Storage / Cleaning

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- My PC
- Storage

Final UI Placement: TBD

Descrição: Analisar oportunidades de limpeza e pressão de espaço em todos os volumes relevantes, sem assumir que Cleaning se resume ao `C:`.

Possível objetivo: Detectar volume secundário cheio, bibliotecas de jogos, scratch disks, downloads e caches em outras unidades.

Possível benefício: Storage, Diagnostic, Quality of Life

Aplicabilidade: Systems with multiple volumes

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- volumes montados;
- tipo de volume;
- capacidade/livre;
- system drive vs data drive;
- categorias por volume;
- discos removíveis;
- volumes de recovery/system protegidos;
- junctions/reparse points para evitar double counting.

---

# 29. Reparse Point / Hardlink / Sparse File Accounting

Status: CANDIDATE

Type: Diagnostic / Safety

Technical Domain: Filesystem / Storage

Primary Product Area: Shared

Possible Consumers:

- Cleaning
- Storage Analysis
- My PC

Final UI Placement: TBD

Descrição: Evitar estimativas erradas e exclusões perigosas causadas por hardlinks, junctions, reparse points, sparse files e outros mecanismos de filesystem.

Possível objetivo: Medir espaço recuperável real em vez de somar tamanhos lógicos ingenuamente.

Possível benefício: Diagnostic, Safety, Storage Accuracy

Aplicabilidade: Windows filesystems

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- allocated size vs logical size;
- hardlink count;
- reparse point target;
- sparse/compressed state;
- junction loops;
- deduplicação de contagem;
- volume físico associado.

---

# 30. Per-User / Multi-Profile Cleanup Awareness

Status: CANDIDATE

Type: Diagnostic / Safety

Technical Domain: Windows Profiles / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- My PC
- Admin Tools

Final UI Placement: TBD

Descrição: Distinguir dados do usuário atual de dados pertencentes a outros perfis e evitar que uma limpeza administrativa atravesse boundaries sem contexto.

Possível objetivo: Explicar por que espaço está ocupado e impedir exclusão acidental de dados de outros usuários.

Possível benefício: Safety, Storage, Diagnostic

Aplicabilidade: PCs with multiple user profiles

Shared Capability: Yes

Risco inicial: High

Evidence Level: Established

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- perfil proprietário;
- perfil carregado/ativo;
- última utilização;
- permissões;
- perfil local/domain;
- policy corporativa;
- dados pessoais vs temporários;
- possibilidade de remoção do perfil por mecanismo suportado.

---

# 31. Orphaned User Profile Assessment

Status: CANDIDATE

Type: Diagnostic / Configuration

Technical Domain: Windows Profiles / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- System
- Admin Tools

Final UI Placement: TBD

Descrição: Identificar perfis locais aparentemente abandonados como consumidores relevantes de espaço, mas não removê-los apenas por idade.

Possível objetivo: Resolver casos onde dezenas de GB pertencem a contas antigas ou migradas.

Possível benefício: Storage, Diagnostic

Aplicabilidade: Shared PCs / migrated systems / admin scenarios

Shared Capability: Yes

Risco inicial: High

Evidence Level: Context-dependent

Action Mode: Manual Guidance

Possíveis informações / dados / checks:

- SID;
- conta ainda existente;
- perfil carregado;
- última utilização;
- tamanho;
- domínio/Entra context;
- arquivos pessoais;
- backups;
- mecanismo oficial de remoção.

---

# 32. Installer / Archive Redundancy Correlation

Status: CANDIDATE

Type: Diagnostic / Tool

Technical Domain: User Data / Applications / Storage

Primary Product Area: Cleaning

Possible Consumers:

- Cleaning
- Tools

Final UI Placement: TBD

Descrição: Correlacionar instaladores, archives e ISOs baixados com software já instalado para sugerir possíveis resíduos, sem assumir que o usuário não quer preservar o instalador.

Possível objetivo: Melhorar qualidade das recomendações em Downloads e pastas de setup.

Possível benefício: Storage, Quality of Life

Aplicabilidade: Optional / Preference

Shared Capability: No

Risco inicial: Medium

Evidence Level: Context-dependent

Action Mode: User Choice

Possíveis informações / dados / checks:

- publisher/product metadata;
- versão;
- app instalado correspondente;
- hash;
- origem/download;
- idade;
- archive content;
- confirmação do usuário.

---

# 33. Cleanup Opportunity Ranking

Status: CANDIDATE

Type: Recommendation

Technical Domain: Cleaning / Decision Support

Primary Product Area: Optimization

Possible Consumers:

- Cleaning
- Optimization
- Recommendations

Final UI Placement: TBD

Descrição: Priorizar oportunidades de limpeza por relação entre espaço recuperável, risco, reversibilidade e custo de reconstrução.

Possível objetivo: Evitar que o usuário veja dezenas de categorias igualmente importantes.

Possível benefício: Quality of Life, Storage, Safety

Aplicabilidade: All PCs

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- bytes recuperáveis;
- confiança;
- risco;
- reversibilidade;
- reconstrução/redownload;
- impacto de UX;
- pressão atual de armazenamento;
- contexto de update/recovery.

---

# 34. Critical Free-Space Requirement Awareness

Status: CANDIDATE

Type: Diagnostic / Recommendation

Technical Domain: Storage / Windows Update / Applications

Primary Product Area: Optimization

Possible Consumers:

- Cleaning
- Windows Update
- Repair
- Installation Guidance

Final UI Placement: TBD

Descrição: Relacionar falta de espaço com uma operação concreta que precisa de capacidade adicional, como update, instalação, unpack ou recuperação.

Possível objetivo: Recomendar somente o volume de limpeza necessário para desbloquear uma tarefa.

Possível benefício: Storage, Repair, Quality of Life

Aplicabilidade: Low-space scenarios

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Diagnostic Only

Possíveis informações / dados / checks:

- operação bloqueada;
- espaço disponível;
- requisito conhecido/estimado;
- margem de segurança;
- categorias menos arriscadas suficientes;
- necessidade de armazenamento externo.

---

# 35. Cleanup Cancellation / Partial-Failure Safety

Status: CANDIDATE

Type: Safety / Reliability

Technical Domain: Cleaning / Shared

Primary Product Area: Shared

Possible Consumers:

- Cleaning
- Repair
- Optimization

Final UI Placement: TBD

Descrição: Garantir que cancelamento, arquivo bloqueado, permission failure ou erro parcial não deixem o sistema em estado inconsistente nem sejam reportados como sucesso completo.

Possível objetivo: Tornar a execução de Cleaning observável e resistente a falhas.

Possível benefício: Stability, Safety, Quality of Life

Aplicabilidade: All actionable cleanup

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- operações concluídas;
- operações pendentes;
- arquivos ignorados;
- motivo da falha;
- cleanup idempotente;
- retry seguro;
- espaço realmente recuperado;
- reboot requirement.

---

# 36. Cleanup Recommendation Confidence

Status: CANDIDATE

Type: Recommendation / Diagnostic

Technical Domain: Cleaning / Decision Support

Primary Product Area: Shared

Possible Consumers:

- Cleaning
- Optimization
- Recommendations

Final UI Placement: TBD

Descrição: Associar confiança à classificação de cada oportunidade de limpeza, diferenciando mecanismos oficialmente descartáveis de heurísticas baseadas em ownership/idade.

Possível objetivo: Comunicar incerteza e evitar que heurísticas sejam apresentadas como certeza.

Possível benefício: Safety, Diagnostic, Quality of Life

Aplicabilidade: All cleanup recommendations

Shared Capability: Yes

Risco inicial: Low

Evidence Level: Established

Action Mode: Automatic

Possíveis informações / dados / checks:

- fonte da classificação;
- regra oficial vs heurística;
- ownership conhecido;
- risco;
- reversibilidade;
- idade/contexto;
- nível de confiança;
- motivo da recomendação.

---

# Itens avaliados no Gap Audit e NÃO promovidos a nova capability

## Browser Cache Cleaning

Resultado: Não adicionar item separado.

Motivo: Já cabe em **App-Owned Temporary Data Cleanup** e **Rebuildable Cache Classification**. Quando o objetivo for privacidade, pertence à área Privacy.

---

## Shader Cache Cleaning

Resultado: Não adicionar item separado.

Motivo: Gaming/GPU já concluíram que não deve existir limpeza rotineira. Caso de corrupção cabe em Repair e no classificador de caches reconstruíveis.

---

## Thumbnail / Icon Cache

Resultado: Não adicionar item separado.

Motivo: Melhor tratado como repair específico de shell/visual quando há corrupção, não como Cleaning.

---

## DirectX Cache / Vendor GPU Caches

Resultado: Não adicionar item separado.

Motivo: São subtipos de cache reconstruível; criar item por cache violaria granularidade.

---

## DNS Cache Flush

Resultado: Não adicionar item.

Motivo: É Network Repair/diagnostic, não Cleaning de armazenamento.

---

## Font Cache

Resultado: Não adicionar item.

Motivo: Repair específico quando há sintomas; não oferece valor de storage relevante.

---

## Print Spooler Files

Resultado: Não adicionar item.

Motivo: Limpeza só faz sentido em repair de fila de impressão travada.

---

## Windows Search Index Deletion

Resultado: REJECTED como Cleaning.

Motivo: Destrói um índice útil e causa rebuild/atividade adicional. Repair apenas se houver corrupção diagnosticada.

---

## Hibernation File Removal

Resultado: Não adicionar em Cleaning.

Motivo: `hiberfil.sys` é consequência de uma feature de energia. Desabilitar hibernação/Fast Startup para recuperar espaço é trade-off de Energy/Configuration, não limpeza.

---

## Pagefile Removal

Resultado: REJECTED.

Motivo: Já rejeitado pela área RAM; não deve ser reinterpretado como “arquivo grande que pode ser apagado”.

---

## Memory Dump Configuration

Resultado: Não criar item separado.

Motivo: Configuração de dump pertence a Diagnostics/System; Cleaning só deve cuidar de retenção dos artefatos existentes.

---

## Reserved Storage Reclamation

Resultado: REJECTED como otimização.

Motivo: Reserved Storage aumenta previsibilidade de servicing/updates. Awareness pode existir internamente no diagnóstico de espaço, mas não é alvo normal de limpeza.

---

## NTFS Compression / CompactOS

Resultado: Não adicionar em Cleaning.

Motivo: São estratégias de capacity management/compression, não exclusão de dados; podem ser avaliadas futuramente em Storage/System se houver benefício de produto.

---

## User Profile Auto-Deletion by Age

Resultado: REJECTED como comportamento genérico.

Motivo: É apropriado apenas em cenários administrados/shared PC com policy explícita; não em consumidor geral.

---

# Auditoria de redundância entre First e Second Pass

## Capabilities que podem parecer próximas, mas devem continuar separadas

**Storage Pressure & Recoverable Space Assessment** vs **Critical Free-Space Requirement Awareness**

- primeira responde “há problema real de espaço?”;
- segunda responde “quanto preciso recuperar para desbloquear uma operação concreta?”.

**Cleanup Inventory & Category Classification** vs **Cleanup Opportunity Ranking**

- inventário determina “o que existe?”;
- ranking determina “o que vale priorizar?”.

**Cleanup Ownership & Provenance Validation** vs **Cleanup Recommendation Confidence**

- ownership é evidência técnica sobre quem controla o dado;
- confidence comunica qualidade/confiabilidade da recomendação final.

**Active-Use & File-Lock Safeguard** vs **Cleanup Cancellation / Partial-Failure Safety**

- um evita interferir em recursos ativos antes/durante a ação;
- outro garante comportamento coerente quando a operação não termina integralmente.

**Downloads Folder Review** vs **Installer / Archive Redundancy Correlation**

- Downloads é análise de uma área de user data;
- correlação é um mecanismo mais específico que aumenta confiança sobre instaladores/archives redundantes.

---

# Conclusões consolidadas da área Cleaning

## 1. Cleaning é predominantemente Storage Management

O BeadWise não deve apresentar limpeza como um mecanismo universal de performance.

O ganho perceptível esperado é principalmente:

- recuperar capacidade;
- evitar low disk;
- permitir updates/instalações;
- melhorar organização;
- remover resíduos realmente descartáveis.

---

## 2. O melhor cleaner é seletivo

Uma boa experiência não é:

> “Encontramos 8.423 problemas. Limpar tudo.”

É algo mais próximo de:

> “Há 18,4 GB recuperáveis.  
>  11,2 GB têm baixo risco.  
>  5,6 GB são arquivos pessoais que exigem sua revisão.  
>  1,6 GB preservamos porque ainda podem ser úteis para rollback/diagnóstico.”

---

## 3. Segurança precisa ser uma propriedade do sistema, não um aviso no botão

As safeguards relevantes são:

- ownership;
- active-use detection;
- protected locations;
- preview;
- confidence;
- reversibility;
- partial-failure handling;
- multi-user boundaries;
- filesystem-aware accounting.

---

## 4. Não duplicar o Windows sem necessidade

Storage Sense e Cleanup Recommendations já resolvem parte relevante do problema.

O BeadWise pode agregar valor ao:

- unificar múltiplas fontes;
- explicar;
- priorizar;
- correlacionar;
- evitar ações ruins;
- medir antes/depois;
- integrar com Recovery/Update/Repair;
- cobrir múltiplos volumes e apps de forma mais contextual.

---

## 5. “Cache” não deve virar árvore de features

A granularidade correta é:

**Rebuildable Cache Classification**

e não:

- DirectX cache;
- NVIDIA cache;
- AMD cache;
- browser cache;
- thumbnail cache;
- font cache;
- Teams cache;
- Discord cache;
- launcher cache;
- etc.

Cada subtipo poderá entrar futuramente na Feature Spec/knowledge base se houver suporte seguro.

---

## 6. Recovery e Diagnostics têm precedência sobre espaço marginal

Não apagar automaticamente:

- restore points;
- rollback data;
- crash dumps recentes;
- logs relevantes;
- installer cache;
- Driver Store;
- system servicing data.

Quando houver conflito entre alguns GB e capacidade de reparar/reverter, preservar recuperação por padrão.

---

## 7. Automation deve reagir a contexto

Melhores gatilhos futuros:

- low disk;
- update bloqueado;
- crescimento anormal;
- usuário solicitando cleanup;
- categoria explicitamente configurada.

Piores gatilhos:

- “todo boot”;
- “todo dia apagar tudo”;
- “cache existe”;
- “arquivo tem X dias” como regra única.

---

# Fontes técnicas relevantes adicionais / confirmatórias

## Microsoft Support — Storage settings in Windows
https://support.microsoft.com/en-us/windows/experience/storage-filemanagement/storage-settings-in-windows

Relevância:
- Cleanup Recommendations;
- multiple storage categories;
- Reserved Storage awareness.

## Microsoft Learn — Configure Storage Sense
https://learn.microsoft.com/en-us/windows/configuration/storage/storage-sense

Relevância:
- políticas de automatic cleanup;
- thresholds;
- user experience;
- escopo e comportamento suportado.

## Microsoft Learn — cleanmgr
https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/cleanmgr

Relevância:
- categorias de limpeza suportadas pelo Windows;
- funcionamento do Disk Cleanup.

## Microsoft Learn — Missing Windows Installer cache
https://learn.microsoft.com/en-us/troubleshoot/windows-client/application-management/missing-windows-installer-cache

Relevância:
- Installer Cache é necessário;
- limpeza manual é perigosa.

## Microsoft Learn — Memory dump documentation
https://learn.microsoft.com/en-us/troubleshoot/windows-client/performance/read-small-memory-dump-file

Relevância:
- dumps preservam informação útil de crash.

---

# Status final

**Cleaning — INITIAL DISCOVERY COMPLETE**

First Pass:

- 27 capabilities

Second Pass / Consolidated Gap Audit:

- +9 capabilities

Total bruto da área:

- **36 capabilities**

Itens adicionais avaliados e não promovidos/rejeitados no Gap Audit:

- 14

---

# Avaliação da contagem

**36 capabilities é uma contagem saudável para esta área.**

Ela ficou deliberadamente menor que áreas antigas porque:

- caches individuais foram consolidados;
- paths não viraram features;
- tweaks clássicos foram rejeitados cedo;
- repair-specific cleanup não foi duplicado;
- parâmetros de Storage Sense não viraram itens;
- safeguards só foram separados quando respondem perguntas distintas do produto.

Na consolidação global futura, alguns itens provavelmente serão absorvidos por shared capabilities já existentes em Storage/System/Recovery/Diagnostics.

---

# Próximo passo operacional

Área seguinte:

**Repair — First Pass**

Depois:

**Repair — Second Pass / Consolidated Gap Audit**

Não consolidar Cleaning com outras áreas ainda.