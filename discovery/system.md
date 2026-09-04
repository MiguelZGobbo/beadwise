# Discovery Catalog — Sistema

> Área inicial de descoberta da V2.
>
> Este catálogo descreve capacidades possíveis.  
> A presença de uma ideia aqui **não significa que ela será implementada** nem que sua forma de execução já foi decidida.
>
> O objetivo nesta etapa é descobrir possibilidades, benefícios, riscos, aplicabilidade e dúvidas técnicas.

---

# 1. System Baseline / Estado Inicial do Sistema

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
System / Recovery / Shared

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Repair
- Benchmark
- My PC
- Monitoring

Final UI Placement:
TBD

Descrição:
Capturar um panorama do estado relevante do Windows antes de qualquer otimização, reparo ou alteração realizada pelo aplicativo.

Possível objetivo:
Criar uma referência confiável do estado original da máquina.

Possível benefício:
Permitir comparação antes/depois, diagnóstico de regressões e rollback mais confiável.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

Possíveis dados:
- versão/build do Windows;
- uptime;
- último boot;
- plano de energia;
- estado de serviços relevantes;
- startup entries;
- Scheduled Tasks relevantes;
- Windows Features;
- principais drivers;
- armazenamento livre;
- processos ativos;
- reboot pendente;
- integridade conhecida;
- configurações que futuramente forem modificáveis pelo BeadWise.

Dúvidas:
- Qual deve ser a granularidade do snapshot?
- Quanto do estado deve ser persistido?
- Quais informações são necessárias para rollback e quais são apenas diagnósticas?
- Como detectar alterações externas realizadas depois do snapshot?

Fonte/observação:
Capacidade estrutural do produto. Relacionada ao ciclo Detect → Backup → Apply → Verify → Rollback.

---

# 2. Windows Identification

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Operating System

Primary Product Area:
Shared

Possible Consumers:
- My PC
- Optimization
- Repair
- Benchmark
- Settings

Final UI Placement:
TBD

Descrição:
Identificar precisamente o sistema operacional e suas características relevantes.

Possível objetivo:
Evitar recomendações ou alterações incompatíveis com a versão instalada do Windows.

Possível benefício:
Permitir comportamento condicional e reduzir risco de aplicar configurações inadequadas.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

Possíveis dados:
- Windows edition;
- major version;
- build;
- revision;
- architecture;
- installation type;
- activation state, se acessível de forma apropriada;
- idioma;
- região;
- modo UEFI/Legacy quando relacionado;
- data aproximada da instalação;
- Windows Insider status, quando detectável;
- recursos presentes na build.

Dúvidas:
- Quais APIs são mais confiáveis para identificar build e edition?
- Como detectar corretamente builds Insider?
- Devemos registrar histórico de atualização de versão?

---

# 3. System Capability Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Operating System

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- My PC
- Benchmark
- Monitoring
- Repair

Final UI Placement:
TBD

Descrição:
Detectar quais capacidades do Windows e do hardware realmente estão disponíveis antes de oferecer determinada funcionalidade.

Possível objetivo:
Substituir decisões baseadas apenas em versão por detecção real de capacidade.

Possível benefício:
Evitar mostrar funcionalidades impossíveis ou irrelevantes para determinada máquina.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

Exemplos:
- recurso existe nessa build?
- determinado comando/API está disponível?
- hardware possui suporte necessário?
- serviço necessário existe?
- determinada feature está instalada?
- alteração requer reboot?
- configuração é gerenciada por política?
- usuário possui privilégios necessários?

Dúvidas:
- Criar um Capability Registry interno?
- Como versionar capacidades conforme Windows muda?

---

# 4. System Health Scan

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
System

Primary Product Area:
Optimization

Possible Consumers:
- Optimization
- My PC
- Repair

Final UI Placement:
TBD

Descrição:
Executar uma análise geral da máquina e consolidar diferentes sinais de saúde do Windows.

Possível objetivo:
Responder de forma amigável à pergunta:

“Existe algo errado ou claramente mal configurado neste PC?”

Possível benefício:
Transformar diversas capacidades diagnósticas independentes em uma visão compreensível do estado da máquina.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept / product-level

Action Mode:
Automatic

Possíveis sinais:
- integridade do Windows;
- falhas recorrentes;
- drivers problemáticos;
- erros de hardware;
- pouco espaço;
- reboot pendente;
- processos anormais;
- startup pesado;
- serviços com falha;
- componentes quebrados;
- Search/indexing problemático;
- problemas registrados no Event Log;
- atualizações problemáticas;
- configurações incompatíveis.

Possível saída:
- Healthy
- Attention Recommended
- Problems Detected
- Critical Issue Detected

Dúvidas:
- Deve existir um “health score” numérico?
- Um número simplificaria demais diagnósticos diferentes?
- Como explicar exatamente de onde cada recomendação veio?

---

# 5. System Recommendation Engine

Status:
CANDIDATE

Type:
Diagnostic / Optimization

Technical Domain:
System / Shared

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- My PC
- Repair
- Benchmark
- Monitoring

Final UI Placement:
TBD

Descrição:
Transformar dados coletados pelo BeadWise em recomendações condicionais em vez de aplicar uma lista fixa de tweaks.

Possível objetivo:
Determinar se uma determinada capacidade é relevante especificamente para aquele computador.

Possível benefício:
Evitar “otimização por receita” e permitir decisões fundamentadas em contexto.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Product logic

Action Mode:
Automatic diagnosis / Contextual recommendation

Estados possíveis:
- Recommended
- Optional
- Already Optimal
- Not Applicable
- Manual Action Required
- Benchmark First
- Experimental
- Risky
- Problem Detected

Dúvidas:
- Como calcular recomendação sem produzir falsos positivos?
- Cada feature deverá fornecer suas próprias regras?
- Devemos permitir níveis diferentes de confiança?

---

# 6. System Bottleneck Overview

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System / Performance

Primary Product Area:
Optimization

Possible Consumers:
- Optimization
- Monitoring
- Benchmark
- My PC

Final UI Placement:
TBD

Descrição:
Analisar sinais gerais de CPU, memória, GPU, armazenamento e processos para identificar possíveis gargalos sistêmicos.

Possível objetivo:
Determinar por que uma máquina apresenta lentidão ou desempenho abaixo do esperado.

Possível benefício:
Direcionar o usuário para a causa provável em vez de sugerir alterações aleatórias.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent

Action Mode:
Diagnostic Only

Possíveis conclusões:
- CPU saturation;
- memory pressure;
- storage saturation;
- GPU saturation;
- background activity;
- thermal limitation;
- driver-related issue;
- startup overload;
- unknown bottleneck.

Dúvidas:
- Quanto tempo de amostragem é necessário?
- Como evitar confundir correlação com causalidade?
- Devemos exigir workload ativo para determinados diagnósticos?

---

# 7. Process Resource Overview

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Processes

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Monitoring
- My PC
- Repair

Final UI Placement:
TBD

Descrição:
Identificar quais processos estão utilizando recursos significativos da máquina.

Possível objetivo:
Descobrir aplicações responsáveis por CPU, memória, disco, GPU ou rede excessivos.

Possível benefício:
Permitir diagnosticar lentidão causada por software em execução.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic / Diagnostic

Possíveis métricas:
- CPU;
- memory;
- committed memory;
- disk I/O;
- network;
- GPU;
- VRAM;
- process uptime;
- thread count;
- handle count;
- executable path;
- publisher/signature;
- parent process.

Dúvidas:
- Quais métricas podem ser coletadas sem custo relevante?
- Qual intervalo de amostragem?
- Como tratar processos protegidos?

Observação:
Processos terá uma investigação própria posteriormente. Este item representa apenas a capacidade sistêmica compartilhada.

---

# 8. Abnormal Resource Consumption Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Processes / Performance

Primary Product Area:
Optimization

Possible Consumers:
- Optimization
- Monitoring

Final UI Placement:
TBD

Descrição:
Detectar processos que permanecem consumindo recursos de maneira anormal ou inesperada durante determinado período.

Possível objetivo:
Diferenciar um pico normal de um comportamento persistente potencialmente problemático.

Possível benefício:
Identificar aplicações causando lentidão, aquecimento, consumo energético ou atividade de disco contínua.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent

Action Mode:
Diagnostic Only

Possíveis sinais:
- CPU persistentemente alta;
- consumo crescente de memória;
- disk I/O constante;
- GPU ativa sem workload aparente;
- processo mantendo sistema fora de idle;
- processo fechado visualmente que permanece pesado em background.

Dúvidas:
- Como definir “anormal” sem thresholds arbitrários?
- É possível criar baseline adaptativa?
- Devemos comparar comportamento idle × workload?

---

# 9. Automatic Startup Inventory

Status:
CANDIDATE

Type:
Diagnostic / Configuration

Technical Domain:
Startup / System

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- My PC
- Tools

Final UI Placement:
TBD

Descrição:
Construir inventário abrangente dos mecanismos pelos quais software pode iniciar automaticamente no Windows.

Possível objetivo:
Encontrar software carregado sem necessidade durante boot, logon ou outros eventos.

Possível benefício:
Reduzir trabalho desnecessário na inicialização e descobrir mecanismos de persistência pouco visíveis.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic diagnosis / User Choice

Possíveis origens:
- Run;
- RunOnce;
- Startup folders;
- Scheduled Tasks;
- Services;
- Explorer extensions;
- Winlogon-related entries;
- outros locais de autostart relevantes.

Dúvidas:
- Devemos reproduzir cobertura semelhante ao Autoruns?
- Quais categorias podem ser gerenciadas com segurança?
- Como diferenciar autostart legítimo, necessário e desnecessário?

Fonte/observação:
O Sysinternals Autoruns demonstra que existem muitos mecanismos de inicialização automática além da lista tradicional de Startup Apps.

---

# 10. Startup Impact Analysis

Status:
CANDIDATE

Type:
Diagnostic / Optimization

Technical Domain:
Startup / Performance

Primary Product Area:
Optimization

Possible Consumers:
- Optimization
- Benchmark
- Monitoring

Final UI Placement:
TBD

Descrição:
Avaliar o impacto real das aplicações iniciadas junto ao Windows.

Possível objetivo:
Priorizar recomendações de desativação com base em impacto mensurável.

Possível benefício:
Reduzir boot/logon sem desligar indiscriminadamente software necessário.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic / User Choice

Possíveis dados:
- CPU utilizada durante startup;
- disk I/O durante startup;
- tempo até inicialização do processo;
- impacto acumulado;
- frequência de utilização do aplicativo, se futuramente mensurável.

Possível classificação:
- High Impact
- Medium Impact
- Low Impact
- Unknown

Dúvidas:
- Conseguimos acessar as mesmas métricas utilizadas pelo Windows?
- Devemos medir diretamente usando ETW?
- Como medir impacto de serviços e tarefas que não aparecem em Startup Apps?

---

# 11. Boot Performance Diagnosis

Status:
CANDIDATE

Type:
Diagnostic / Benchmark

Technical Domain:
Startup / Performance

Primary Product Area:
Benchmark

Possible Consumers:
- Optimization
- Benchmark
- Monitoring

Final UI Placement:
TBD

Descrição:
Medir e analisar o processo de boot além do simples tempo entre botão e desktop.

Possível objetivo:
Identificar onde o tempo de inicialização está sendo gasto.

Possível benefício:
Diagnosticar boot lento de maneira baseada em dados.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic Only

Possíveis métricas:
- boot duration;
- kernel/init phase;
- service initialization;
- logon;
- startup apps;
- disk activity;
- CPU activity;
- tempo até sistema utilizável.

Dúvidas:
- Quanto pode ser obtido diretamente?
- Quando ETW/WPR será necessário?
- Como medir de forma repetível sem exigir ADK?

---

# 12. Windows Services Inventory

Status:
CANDIDATE

Type:
Diagnostic / Configuration

Technical Domain:
Services

Primary Product Area:
System

Possible Consumers:
- Optimization
- Repair
- My PC

Final UI Placement:
TBD

Descrição:
Inventariar serviços instalados, seu estado e configuração.

Possível objetivo:
Entender quais serviços existem e detectar situações potencialmente problemáticas.

Possível benefício:
Permitir diagnóstico e futuras recomendações contextuais.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic diagnosis

Possíveis dados:
- service name;
- display name;
- status;
- startup type;
- binary path;
- account;
- publisher quando identificável;
- dependencies;
- dependents;
- trigger-start quando acessível;
- Windows / third-party / OEM classification.

Dúvidas:
- Como classificar serviços de maneira confiável?
- Como detectar serviços órfãos?
- Como diferenciar “parado porque não é necessário agora” de problema?

---

# 13. Service Dependency Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Services

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Repair

Final UI Placement:
TBD

Descrição:
Analisar dependências antes de sugerir alteração em um serviço.

Possível objetivo:
Evitar quebrar funcionalidades ao desativar um serviço aparentemente desnecessário.

Possível benefício:
Tornar gerenciamento de serviços muito mais seguro.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic safeguard

Dúvidas:
- Dependências declaradas são suficientes?
- Existem dependências comportamentais não expressas pelo SCM?
- Como representar serviços trigger-start?

Fonte/observação:
O Service Control Manager mantém informações de inicialização e relacionamento dos serviços; serviços modernos também podem operar por gatilhos e não precisam permanecer ativos continuamente.

---

# 14. Service Failure Detection

Status:
CANDIDATE

Type:
Diagnostic / Repair

Technical Domain:
Services / Event Logs

Primary Product Area:
Repair

Possible Consumers:
- Repair
- My PC
- Optimization

Final UI Placement:
TBD

Descrição:
Detectar serviços importantes que falham repetidamente ao iniciar ou executar.

Possível objetivo:
Encontrar problemas de Windows ou software que aparecem apenas como falhas de serviço.

Possível benefício:
Diagnóstico de funcionalidades quebradas, boot lento ou erros silenciosos.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic / Repair candidate

Possíveis sinais:
- serviço não inicia;
- timeout;
- dependência ausente;
- executável inexistente;
- DLL ausente;
- falha repetitiva;
- configuração incorreta.

Dúvidas:
- Quais eventos são suficientemente confiáveis para recomendação automática?
- Como evitar alertar sobre serviços opcionais que falharam apenas uma vez?

---

# 15. Scheduled Tasks Inventory

Status:
CANDIDATE

Type:
Diagnostic / Configuration

Technical Domain:
Task Scheduler

Primary Product Area:
System

Possible Consumers:
- Optimization
- Repair
- Tools

Final UI Placement:
TBD

Descrição:
Inventariar tarefas agendadas do Windows e de aplicações instaladas.

Possível objetivo:
Descobrir processos periódicos, agentes de atualização, telemetria, tarefas OEM e outras atividades automáticas.

Possível benefício:
Identificar trabalho desnecessário ou tarefas quebradas.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic diagnosis / User Choice

Possíveis dados:
- task path;
- publisher;
- triggers;
- executable/actions;
- last run;
- last result;
- next run;
- enabled state;
- run frequency;
- privileges.

Dúvidas:
- Como classificar tarefas Microsoft × third-party × OEM?
- Como detectar tarefas órfãs?
- Como determinar se frequência excessiva está prejudicando o PC?

---

# 16. Scheduled Task Failure Detection

Status:
CANDIDATE

Type:
Diagnostic / Repair

Technical Domain:
Task Scheduler

Primary Product Area:
Repair

Possible Consumers:
- Repair
- Optimization

Final UI Placement:
TBD

Descrição:
Detectar tarefas agendadas que falham repetidamente ou apontam para componentes inexistentes.

Possível objetivo:
Identificar resíduos de programas removidos e componentes quebrados.

Possível benefício:
Reduzir erros silenciosos e atividade inútil.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Possíveis casos:
- executable missing;
- repeated error result;
- invalid working directory;
- orphaned task;
- malformed trigger;
- serviço Task Scheduler problemático.

Dúvidas:
- Quando uma tarefa antiga pode ser considerada realmente órfã?
- Remoção deverá ser automática ou sempre opt-in?

---

# 17. Orphaned System Entries Detection

Status:
IDEA

Type:
Diagnostic / Cleanup

Technical Domain:
System

Primary Product Area:
Optimization

Possible Consumers:
- Optimization
- Cleanup
- Tools

Final UI Placement:
TBD

Descrição:
Detectar referências persistentes a software ou componentes que não existem mais.

Possível objetivo:
Encontrar resíduos estruturais deixados por desinstalações incompletas.

Possível benefício:
Reduzir erros, tentativas inúteis de inicialização e lixo de configuração.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Context-dependent

Action Mode:
Diagnostic / User Choice

Possíveis objetos:
- services;
- Scheduled Tasks;
- startup entries;
- missing executables;
- broken uninstall entries;
- shell integrations;
- application leftovers.

Dúvidas:
- Como provar que uma entrada é realmente órfã?
- Quais objetos podem ser removidos com segurança?
- Deve esta capacidade pertencer posteriormente ao Uninstaller/Cleanup?

---

# 18. Optional Windows Features Inventory

Status:
CANDIDATE

Type:
Diagnostic / Configuration

Technical Domain:
Windows Features

Primary Product Area:
System

Possible Consumers:
- Optimization
- My PC
- Repair

Final UI Placement:
TBD

Descrição:
Detectar recursos opcionais e Features on Demand instalados ou habilitados.

Possível objetivo:
Entender quais componentes opcionais fazem parte da máquina.

Possível benefício:
Permitir recomendações contextuais e detectar dependências antes de alterações.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic diagnosis / Contextual User Choice

Possíveis exemplos:
- Hyper-V;
- WSL;
- Virtual Machine Platform;
- Sandbox;
- Containers;
- IIS;
- legacy components;
- .NET components;
- media-related features;
- Features on Demand.

Dúvidas:
- Quais features merecem aparecer ao usuário?
- Como determinar utilização real?
- Devemos medir footprint antes de recomendar remoção?

---

# 19. Windows Feature Dependency Safeguard

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Windows Features

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Repair

Final UI Placement:
TBD

Descrição:
Verificar se um recurso opcional aparentemente desnecessário é requerido por software ou por outra capacidade da máquina.

Possível objetivo:
Evitar que uma otimização que desabilite uma feature quebre aplicações existentes.

Possível benefício:
Aumentar segurança do gerenciamento de Windows Features.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent

Action Mode:
Automatic safeguard

Dúvidas:
- O Windows expõe dependências suficientes?
- Será necessário catálogo próprio de software conhecido?
- Como detectar uso indireto de Hyper-V/VBS/WSL?

---

# 20. Windows Image Health

Status:
CANDIDATE

Type:
Diagnostic / Repair

Technical Domain:
System Integrity

Primary Product Area:
Repair

Possible Consumers:
- Repair
- Optimization
- My PC

Final UI Placement:
TBD

Descrição:
Verificar a integridade do Component Store e da imagem do Windows.

Possível objetivo:
Detectar corrupção do sistema antes que ela produza comportamento imprevisível.

Possível benefício:
Diferenciar problema de configuração de corrupção real do Windows.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic / Repair

Possíveis níveis:
- CheckHealth;
- ScanHealth;
- RestoreHealth quando necessário.

Dúvidas:
- Qual operação deve ser usada no scan normal?
- Devemos evitar ScanHealth automático por custo/tempo?
- Quando RestoreHealth deve ser oferecido?

Fonte/observação:
DISM possui operações documentadas especificamente para detectar e reparar corrupção do Component Store.

---

# 21. Protected System Files Integrity

Status:
CANDIDATE

Type:
Diagnostic / Repair

Technical Domain:
System Integrity

Primary Product Area:
Repair

Possible Consumers:
- Repair
- My PC

Final UI Placement:
TBD

Descrição:
Verificar arquivos protegidos do sistema operacional e reparar arquivos ausentes ou corrompidos.

Possível objetivo:
Detectar corrupção que possa afetar estabilidade ou componentes do Windows.

Possível benefício:
Correção de problemas sem reinstalação do sistema.

Aplicabilidade:
All PCs

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic / Repair

Possível mecanismo:
Windows Resource Protection / System File Checker.

Dúvidas:
- Devemos oferecer Verify-only antes de Scannow?
- Como interpretar resultados e logs para o usuário?
- Como relacionar erros SFC com DISM?

---

# 22. Component Store Analysis

Status:
CANDIDATE

Type:
Diagnostic / Maintenance

Technical Domain:
System Servicing

Primary Product Area:
System

Possible Consumers:
- Optimization
- Cleanup
- Repair

Final UI Placement:
TBD

Descrição:
Analisar o estado do Windows Component Store antes de qualquer tentativa de limpeza.

Possível objetivo:
Distinguir crescimento normal do WinSxS de espaço realmente recuperável.

Possível benefício:
Permitir manutenção segura e evitar ferramentas que simplesmente tentam apagar arquivos do WinSxS.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic First

Possíveis informações:
- component store health;
- reclaimable packages;
- cleanup recommended;
- last cleanup;
- reclaimable size.

Dúvidas:
- Qual informação pode ser convertida em recomendação simples?
- Quando StartComponentCleanup realmente traz benefício?

---

# 23. Component Store Maintenance

Status:
CANDIDATE

Type:
Maintenance / Cleanup

Technical Domain:
System Servicing

Primary Product Area:
Optimization

Possible Consumers:
- Optimization
- Cleanup
- Repair

Final UI Placement:
TBD

Descrição:
Executar manutenção suportada do Component Store quando houver benefício detectado.

Possível objetivo:
Remover versões substituídas de componentes usando mecanismos suportados do Windows.

Possível benefício:
Recuperar armazenamento e manter o servicing store.

Aplicabilidade:
All PCs

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Established

Action Mode:
Contextual

Dúvidas:
- Quando usar StartComponentCleanup?
- `ResetBase` deve ser excluído devido à perda da capacidade de desinstalar atualizações substituídas?
- Devemos manter qualquer operação irreversível fora da otimização normal?

Observação:
Operações que alterem capacidade futura de rollback de updates devem ser tratadas separadamente.

---

# 24. Windows Search / Index Health

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Search / System

Primary Product Area:
System

Possible Consumers:
- Optimization
- Repair

Final UI Placement:
TBD

Descrição:
Avaliar a saúde e o impacto do Windows Search Indexer.

Possível objetivo:
Detectar tanto indexação quebrada quanto indexação excessivamente pesada.

Possível benefício:
Corrigir Search lento sem recorrer à desativação indiscriminada do serviço Windows Search.

Aplicabilidade:
All PCs

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic First

Possíveis sinais:
- index service status;
- quantidade de itens indexados;
- tamanho do banco;
- indexing paused;
- insufficient disk;
- insufficient memory;
- indexing errors;
- index em manutenção;
- atividade excessiva.

Dúvidas:
- É possível obter essas informações por API?
- Qual número de itens deve ser tratado como contextual, e não threshold universal?
- Como estimar se SearchIndexer realmente está impactando o usuário?

Observação:
Não considerar “desativar Windows Search” como otimização padrão.

---

# 25. Windows Search Repair

Status:
CANDIDATE

Type:
Repair

Technical Domain:
Search

Primary Product Area:
Repair

Possible Consumers:
- Repair
- Tools

Final UI Placement:
TBD

Descrição:
Oferecer reparos suportados quando a indexação ou pesquisa estiver quebrada.

Possível objetivo:
Restaurar Search sem exigir ações manuais complexas.

Possível benefício:
Resolver resultados ausentes, indexação travada ou banco corrompido.

Aplicabilidade:
Affected PCs

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Established

Action Mode:
Problem-triggered

Possíveis ações:
- restart Search components;
- rebuild index;
- reset Search;
- regenerate Search package state quando aplicável.

Dúvidas:
- Quais operações permanecem válidas em cada build do Windows?
- Devemos implementar reparos específicos por versão?

---

# 26. Event Log Health Analysis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Event Logs

Primary Product Area:
Shared

Possible Consumers:
- My PC
- Repair
- Optimization
- Monitoring

Final UI Placement:
TBD

Descrição:
Consultar logs relevantes do Windows e transformar eventos técnicos recorrentes em diagnósticos compreensíveis.

Possível objetivo:
Usar informações que o Windows já registra para detectar problemas silenciosos.

Possível benefício:
Descobrir falhas que não seriam identificadas apenas observando configuração atual.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic / Diagnostic

Possíveis categorias:
- application crashes;
- service failures;
- driver failures;
- disk/filesystem errors;
- hardware errors;
- unexpected shutdown;
- Windows Update failures;
- Task Scheduler failures;
- device errors.

Dúvidas:
- Quais providers/event IDs têm sinal suficientemente forte?
- Qual janela temporal usar?
- Como eliminar ruído?
- Como agrupar eventos repetidos?

---

# 27. Hardware Error History via WHEA

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
System / Hardware Reliability

Primary Product Area:
Shared

Possible Consumers:
- My PC
- Monitoring
- Benchmark
- Repair

Final UI Placement:
TBD

Descrição:
Consultar erros de hardware registrados pela Windows Hardware Error Architecture.

Possível objetivo:
Detectar sinais de instabilidade que podem não provocar crash imediato.

Possível benefício:
Identificar problemas relacionados a CPU, memória, PCIe ou outros componentes antes de atribuir sintomas a “Windows lento”.

Aplicabilidade:
Supported Hardware

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic Only

Possíveis usos:
- histórico de WHEA;
- erros corrigidos;
- erros fatais;
- correlação temporal com benchmark;
- correlação com overclock/undervolt;
- correlação com driver/GPU/PCIe quando possível.

Dúvidas:
- Quão bem os registros podem ser traduzidos para componentes compreensíveis?
- Como evitar diagnosticar incorretamente causa física?
- Devemos monitorar novos eventos em tempo real?

---

# 28. Application Crash / Hang History

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Reliability / WER

Primary Product Area:
Shared

Possible Consumers:
- My PC
- Repair
- Monitoring

Final UI Placement:
TBD

Descrição:
Analisar falhas e travamentos de aplicações registrados pelo Windows.

Possível objetivo:
Identificar software instável e padrões recorrentes.

Possível benefício:
Mostrar que determinada aplicação, módulo ou driver está falhando repetidamente.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic Only

Possíveis informações:
- application;
- version;
- faulting module;
- error code;
- date/time;
- crash frequency;
- hang frequency.

Dúvidas:
- Quais informações podem ser obtidas via WER versus Event Log?
- Devemos analisar dumps quando disponíveis ou deixar isso para uma feature mais avançada?

---

# 29. System Crash / BSOD History

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Reliability / Kernel

Primary Product Area:
Repair

Possible Consumers:
- My PC
- Repair
- Monitoring

Final UI Placement:
TBD

Descrição:
Identificar histórico de bugchecks, desligamentos inesperados e dumps existentes.

Possível objetivo:
Dar contexto inicial para problemas de estabilidade.

Possível benefício:
Permitir reconhecer padrões e direcionar investigação posterior.

Aplicabilidade:
Affected PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept

Action Mode:
Diagnostic Only

Possíveis dados:
- bugcheck code;
- timestamp;
- dump availability;
- recurring code;
- unexpected shutdown;
- associated WHEA events.

Dúvidas:
- Até onde analisar dump sem tentar reproduzir WinDbg?
- É seguro sugerir provável driver causador automaticamente?
- Devemos criar uma feature futura de Crash Analyzer?

---

# 30. Performance Trace Capture

Status:
CANDIDATE

Type:
Diagnostic Tool

Technical Domain:
ETW / Performance

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Benchmark
- Monitoring
- Repair

Final UI Placement:
TBD

Descrição:
Capturar eventos de desempenho do sistema por um período determinado para investigação aprofundada.

Possível objetivo:
Permitir diagnóstico baseado em atividade real em vez de apenas estado/configuração.

Possível benefício:
Investigar lentidão, boot, CPU, disk I/O, processos e outros problemas difíceis de detectar por polling simples.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
On-demand Diagnostic

Possível tecnologia:
Event Tracing for Windows.

Dúvidas:
- É necessário depender de WPR/WPA/ADK ou podemos consumir ETW diretamente?
- Quais providers são relevantes?
- Como limitar overhead e tamanho dos traces?
- Como transformar trace bruto em diagnóstico de produto?

---

# 31. “Why Is My PC Slow?” Diagnostic Session

Status:
IDEA

Type:
Diagnostic Tool

Technical Domain:
System / Performance

Primary Product Area:
Optimization

Possible Consumers:
- Optimization
- Monitoring

Final UI Placement:
TBD

Descrição:
Executar uma sessão temporária de diagnóstico enquanto o usuário reproduz a lentidão.

Possível objetivo:
Responder a um problema subjetivo através de dados coletados durante o momento em que ele acontece.

Possível benefício:
Localizar causas que um scan estático não conseguiria encontrar.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product concept based on established telemetry

Action Mode:
User-triggered Diagnostic

Possível fluxo:
1. iniciar captura;
2. usuário reproduz lentidão;
3. encerrar captura;
4. analisar CPU;
5. analisar memória;
6. analisar disco;
7. analisar GPU;
8. analisar processos;
9. correlacionar eventos;
10. apresentar possíveis culpados.

Dúvidas:
- Qual duração máxima?
- ETW ou métricas periódicas?
- Como produzir explicação confiável de causa provável?

---

# 32. Clean Boot Diagnostic Assistant

Status:
IDEA

Type:
Diagnostic / Tool

Technical Domain:
System / Startup / Services

Primary Product Area:
Repair

Possible Consumers:
- Repair
- Tools

Final UI Placement:
TBD

Descrição:
Auxiliar o usuário a realizar testes controlados reduzindo temporariamente software de terceiros iniciado com o Windows.

Possível objetivo:
Diagnosticar conflito causado por serviço ou aplicação de startup.

Possível benefício:
Automatizar parcialmente um processo comum de troubleshooting.

Aplicabilidade:
Affected PCs

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Established troubleshooting method

Action Mode:
Explicit User Choice / Temporary

Possível fluxo:
- snapshot;
- selecionar grupos de terceiros;
- desativar temporariamente;
- reboot;
- testar;
- restaurar automaticamente;
- comparar resultado.

Dúvidas:
- Quais componentes nunca devem ser desativados?
- Como garantir recuperação após reboot?
- Como tratar antivírus e software de hardware?

---

# 33. Pending Reboot Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
System Maintenance

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Repair
- Windows Update
- Drivers

Final UI Placement:
TBD

Descrição:
Detectar situações em que o Windows ou algum componente aguarda reinicialização para concluir alterações.

Possível objetivo:
Evitar diagnósticos e otimizações sobre um sistema em estado intermediário.

Possível benefício:
Reduzir falsos problemas e alterações conflitantes.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Automatic

Possíveis origens:
- Windows Update;
- servicing;
- driver installation;
- pending file operations;
- BeadWise changes.

Dúvidas:
- Quais sinais constituem detecção confiável?
- Devemos diferenciar origem do reboot pendente?

---

# 34. Boot / Shutdown History

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System Reliability

Primary Product Area:
My PC

Possible Consumers:
- Benchmark
- Repair
- Optimization

Final UI Placement:
TBD

Descrição:
Registrar ou recuperar histórico recente de inicializações, reinicializações e desligamentos anormais.

Possível objetivo:
Fornecer contexto temporal para problemas de estabilidade ou desempenho.

Possível benefício:
Permitir correlação com updates, crashes e alterações recentes.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Possíveis informações:
- last boot;
- uptime;
- normal shutdown;
- unexpected shutdown;
- restart frequency;
- boot duration quando disponível.

Dúvidas:
- Quanto histórico manter?
- Devemos construir histórico próprio além dos logs do Windows?

---

# 35. System Change History

Status:
CANDIDATE

Type:
Diagnostic / Safety

Technical Domain:
Shared

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Repair
- Settings

Final UI Placement:
TBD

Descrição:
Registrar alterações feitas pelo BeadWise e relacioná-las temporalmente com alterações de comportamento observadas.

Possível objetivo:
Saber exatamente o que foi alterado, quando e por qual feature.

Possível benefício:
Rollback, auditoria e investigação de regressões.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product infrastructure

Action Mode:
Automatic

Possíveis dados:
- feature;
- timestamp;
- previous state;
- new state;
- reason/recommendation;
- risk;
- reboot requirement;
- verification result;
- benchmark result;
- rollback state.

Dúvidas:
- Histórico deve ser permanente?
- Como tratar configurações modificadas externamente depois?

---

# 36. Configuration Drift Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System / Shared

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Repair

Final UI Placement:
TBD

Descrição:
Detectar quando uma configuração gerenciada ou otimizada anteriormente pelo BeadWise foi alterada posteriormente pelo Windows, driver ou outro aplicativo.

Possível objetivo:
Explicar por que uma otimização deixou de estar ativa.

Possível benefício:
Evitar que o usuário ache que a alteração simplesmente “não funcionou”.

Aplicabilidade:
Previously Optimized PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product concept

Action Mode:
Automatic Diagnostic

Exemplo:
- BeadWise alterou configuração X;
- atualização do driver restaurou valor padrão;
- próxima análise detecta diferença;
- usuário é informado.

Dúvidas:
- Quais configurações merecem monitoramento?
- Devemos reaplicar automaticamente? Provavelmente não por padrão.

---

# 37. System Configuration Anomaly Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System

Primary Product Area:
Optimization

Possible Consumers:
- Optimization
- Repair

Final UI Placement:
TBD

Descrição:
Detectar configurações claramente anormais, contraditórias ou incompatíveis com o hardware/software presente.

Possível objetivo:
Encontrar problemas causados por tweaks antigos, softwares de otimização ou configuração manual incorreta.

Possível benefício:
Restaurar comportamento esperado do Windows.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Context-dependent

Action Mode:
Diagnostic / Contextual Repair

Possíveis exemplos:
- serviço essencial desativado;
- feature requerida ausente;
- Search quebrado por tweak;
- configuração de sistema incompatível;
- startup referenciando executável inexistente;
- política residual;
- configuração de performance herdada de ferramenta antiga.

Dúvidas:
- Precisaremos de catálogo de defaults por build?
- Como diferenciar configuração intencional de erro?
- Devemos oferecer “Restore Windows default” por configuração?

---

# 38. Third-Party Optimizer Residue Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System

Primary Product Area:
Optimization

Possible Consumers:
- Repair
- Optimization

Final UI Placement:
TBD

Descrição:
Investigar alterações conhecidas deixadas por antigos “PC optimizers”, debloat scripts ou tweak packs.

Possível objetivo:
Encontrar estados não padrão que possam estar prejudicando estabilidade ou funcionalidade.

Possível benefício:
Desfazer danos ou configurações sem fundamento herdadas de ferramentas anteriores.

Aplicabilidade:
Affected PCs

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Unverified / requires research

Action Mode:
Diagnostic First

Possíveis alvos:
- serviços desativados;
- Windows Search;
- Windows Update;
- Defender;
- timers;
- Registry “performance tweaks”;
- network tweaks;
- scheduler tweaks;
- pagefile;
- disabled features.

Dúvidas:
- Como identificar origem de cada alteração?
- É suficiente detectar desvio de defaults?
- Precisaremos construir catálogo de tweaks populares?

---

# 39. Explorer / Shell Extension Diagnosis

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Shell / System

Primary Product Area:
System

Possible Consumers:
- Optimization
- Repair

Final UI Placement:
TBD

Descrição:
Detectar extensões de shell e integrações de terceiros que podem afetar Explorer, menus de contexto ou login.

Possível objetivo:
Investigar Explorer lento, menu de contexto lento ou crashes relacionados a extensões.

Possível benefício:
Diagnosticar problemas que normalmente parecem ser “Windows lento”.

Aplicabilidade:
Affected PCs

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established concept

Action Mode:
Diagnostic / User Choice

Possíveis objetos:
- shell extensions;
- context-menu handlers;
- Explorer add-ons;
- third-party integrations.

Dúvidas:
- Como medir impacto individual?
- Podemos desativar temporariamente para teste?
- Assinatura/publisher é suficiente para classificação?

---

# 40. Windows Core Component Availability Check

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System Integrity

Primary Product Area:
Repair

Possible Consumers:
- Repair
- Optimization

Final UI Placement:
TBD

Descrição:
Verificar se serviços, arquivos ou componentes fundamentais esperados para determinada funcionalidade do Windows estão disponíveis.

Possível objetivo:
Detectar componentes quebrados antes de executar reparos genéricos.

Possível benefício:
Permitir reparo direcionado.

Aplicabilidade:
Affected PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept

Action Mode:
Diagnostic

Exemplos:
- serviço existe?;
- DLL esperada existe?;
- pacote está registrado?;
- feature necessária está habilitada?;
- componente responde?;

Dúvidas:
- Criar verificações específicas por subsistema?
- Isso deve ser framework compartilhado pelos módulos Repair?

---

# 41. System Maintenance State

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Maintenance

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Repair
- Benchmark

Final UI Placement:
TBD

Descrição:
Determinar se o Windows está executando atividades importantes de manutenção no momento.

Possível objetivo:
Evitar benchmark, otimização ou diagnóstico enquanto outra operação pesada distorce os resultados.

Possível benefício:
Melhorar confiabilidade das medições.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent

Action Mode:
Automatic safeguard

Possíveis atividades:
- Windows Update;
- Search indexing;
- Defender scan;
- component servicing;
- installation;
- pending restart;
- disk optimization;
- heavy background maintenance.

Dúvidas:
- Quais estados são detectáveis de forma confiável?
- Devemos simplesmente alertar ou adiar algumas operações?

---

# 42. Benchmark Readiness Check

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System / Benchmark

Primary Product Area:
Shared

Possible Consumers:
- Benchmark
- Optimization

Final UI Placement:
TBD

Descrição:
Verificar se o sistema está em condições adequadas para produzir um benchmark comparável.

Possível objetivo:
Evitar resultados contaminados por tarefas externas.

Possível benefício:
Tornar Before × After mais confiável.

Aplicabilidade:
Benchmark Sessions

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product logic

Action Mode:
Automatic safeguard

Possíveis verificações:
- Windows Update ativo;
- Defender scan;
- indexing pesado;
- instalação em andamento;
- temperatura inicial elevada;
- CPU em uso;
- GPU em uso;
- bateria/AC;
- reboot pendente;
- processos pesados.

Dúvidas:
- Quais thresholds usar?
- Quanto tempo aguardar pela estabilização?
- Permitir benchmark mesmo assim com aviso?

---

# 43. Accessibility Configuration Safeguard

Status:
CANDIDATE

Type:
Safety / Diagnostic

Technical Domain:
System / Accessibility

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Settings

Final UI Placement:
TBD

Descrição:
Detectar configurações relevantes de acessibilidade antes de realizar alterações que possam afetar a experiência do usuário.

Possível objetivo:
Evitar que otimizações visuais, input ou comportamento do Windows destruam uma configuração necessária.

Possível benefício:
Tornar otimizações adaptativas e seguras.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established product requirement

Action Mode:
Automatic safeguard

Dúvidas:
- Quais configurações precisam ser protegidas?
- Como distinguir preferência de necessidade sem inferir informações pessoais?

---

# 44. System Diagnostic Export

Status:
IDEA

Type:
Tool

Technical Domain:
System / Shared

Primary Product Area:
Tools

Possible Consumers:
- My PC
- Repair
- Support

Final UI Placement:
TBD

Descrição:
Gerar relatório técnico com informações relevantes do sistema e diagnósticos encontrados.

Possível objetivo:
Facilitar troubleshooting, suporte e comparação.

Possível benefício:
Permitir ao usuário compartilhar informações sem capturas de dezenas de telas.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Product concept

Action Mode:
User-triggered

Possíveis conteúdos:
- Windows;
- hardware;
- drivers;
- health findings;
- WHEA;
- crashes;
- startup;
- services;
- temperatures;
- storage;
- benchmark results.

Dúvidas:
- Quais informações devem ser removidas por privacidade?
- Devemos ter versão “Technical” e “Safe to Share”?
- Quais identificadores nunca devem sair no relatório?

---

# 45. Diagnostic Evidence Correlation

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System / Shared

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Monitoring
- Repair
- Benchmark

Final UI Placement:
TBD

Descrição:
Correlacionar eventos e métricas provenientes de diferentes subsistemas em uma mesma linha temporal.

Possível objetivo:
Aumentar a capacidade de distinguir sintomas de possíveis causas.

Possível benefício:
Produzir diagnósticos mais úteis do que analisar cada métrica isoladamente.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product concept

Action Mode:
Diagnostic Only

Exemplo conceitual:
- frametime spike;
- simultaneamente CPU chegou a 100%;
- processo X iniciou atividade intensa;
- disk I/O aumentou;
- Event Log registrou falha próxima.

Resultado:
“Existe correlação entre o problema observado e atividade do processo X.”

Dúvidas:
- Qual nível de confiança devemos apresentar?
- Nunca devemos transformar correlação em afirmação causal automática.
- Precisaremos de timestamps uniformizados entre fontes?

---

# 46. System State Comparison

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
System / Shared

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Repair
- Benchmark

Final UI Placement:
TBD

Descrição:
Comparar dois estados da mesma máquina.

Possível objetivo:
Entender exatamente o que mudou entre dois momentos.

Possível benefício:
Avaliar otimizações, investigar regressões e descobrir alterações provocadas por updates ou software.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product infrastructure

Action Mode:
Automatic / User-triggered

Possíveis comparações:
- before optimization × after;
- before update × after;
- before driver installation × after;
- healthy state × problematic state.

Dúvidas:
- Quais categorias participarão do diff?
- Como evitar excesso de informações irrelevantes?

---

# 47. System Defaults Reference

Status:
RESEARCHING

Type:
Shared Capability

Technical Domain:
System Configuration

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Repair
- Settings

Final UI Placement:
TBD

Descrição:
Manter conhecimento sobre estados padrão ou esperados de determinadas configurações do Windows quando tecnicamente possível.

Possível objetivo:
Permitir identificar tweaks antigos, configurações quebradas e restaurar defaults conhecidos.

Possível benefício:
Base para “Restore Default” seguro.

Aplicabilidade:
Specific Windows Versions

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Requires extensive validation

Action Mode:
Diagnostic / Repair Support

Dúvidas:
- Defaults mudam por build, SKU, hardware e OEM?
- Quais configurações possuem realmente um default universal?
- Podemos obter o default do próprio Windows em vez de manter tabela?
- Como evitar restaurar algo que o usuário configurou intencionalmente?

---

# 48. System Diagnostic Framework

Status:
CANDIDATE

Type:
Shared Capability

Technical Domain:
Architecture / Diagnostic

Primary Product Area:
Shared

Possible Consumers:
- All diagnostic areas

Final UI Placement:
TBD

Descrição:
Criar um conceito comum para capacidades de diagnóstico independentemente da área técnica.

Possível objetivo:
Permitir que CPU, GPU, Storage, Network, Windows e demais módulos produzam resultados consistentes.

Possível benefício:
Viabilizar Health Scan, Recommendation Engine e correlação de evidências.

Aplicabilidade:
Application-wide

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product infrastructure

Action Mode:
Internal

Possível modelo conceitual de finding:
- ID;
- category;
- severity;
- confidence;
- evidence;
- current state;
- expected state;
- possible impact;
- recommendation;
- action availability;
- risk;
- requires reboot;
- reversible;
- source capability.

Dúvidas:
- Isso pertence à arquitetura futura, portanto não deve ser especificado profundamente no Discovery.
- Registrar apenas a necessidade conceitual por enquanto.

---

# 49. “Nothing To Optimize” Result

Status:
CANDIDATE

Type:
Product Behavior

Technical Domain:
System / Shared

Primary Product Area:
Optimization

Possible Consumers:
- Optimization

Final UI Placement:
TBD

Descrição:
Permitir que o resultado válido de uma análise seja não recomendar nenhuma alteração.

Possível objetivo:
Evitar que o produto invente trabalho para justificar a existência de um botão Optimize.

Possível benefício:
Aumentar confiança e impedir alterações sem benefício.

Aplicabilidade:
All PCs

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Product principle

Action Mode:
Automatic

Exemplo:
“Não encontramos alterações que justifiquem modificar este sistema no momento.”

Dúvidas:
Nenhuma técnica relevante nesta fase.

---

# 50. Explain Why

Status:
CANDIDATE

Type:
Diagnostic / Product Behavior

Technical Domain:
Shared

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Repair
- My PC
- Benchmark

Final UI Placement:
TBD

Descrição:
Toda recomendação relevante deve conseguir explicar por que apareceu.

Possível objetivo:
Evitar “caixa preta de otimização”.

Possível benefício:
Permitir que o usuário compreenda risco, benefício e evidência.

Aplicabilidade:
Application-wide

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Product principle

Action Mode:
Automatic

Possível explicação:
- o que detectamos;
- por que isso importa;
- qual benefício esperamos;
- quão forte é a evidência;
- quais riscos existem;
- se é reversível;
- se exige benchmark;
- por que não recomendamos em outro cenário.

Dúvidas:
Como manter explicações tecnicamente corretas sem sobrecarregar usuários menos avançados?

---

# Observações provisórias da área Sistema

## Ideias que parecem especialmente fortes

- System Baseline;
- Windows Identification;
- Capability Detection;
- System Health Scan;
- Recommendation Engine;
- Startup Inventory;
- Startup Impact Analysis;
- Services Inventory;
- Dependency Analysis;
- Scheduled Tasks;
- Windows Image Health;
- Protected System Files Integrity;
- Component Store Analysis;
- Event Log Analysis;
- WHEA History;
- Crash/Hang History;
- Performance Trace Capture;
- Pending Reboot Detection;
- System Change History;
- Benchmark Readiness;
- Explain Why.

## Ideias promissoras que exigem maior pesquisa

- automatic bottleneck classification;
- abnormal process detection;
- orphan detection;
- configuration anomaly detection;
- third-party optimizer residue detection;
- diagnostic evidence correlation;
- System Defaults Reference;
- clean boot assistant;
- automated interpretation of performance traces.

## Princípios que surgem desta área

1. Detectar antes de alterar.
2. Não considerar estado diferente do padrão automaticamente como problema.
3. Não considerar serviço parado automaticamente como problema.
4. Não considerar serviço ativo automaticamente como desperdício.
5. Não desativar Windows Search indiscriminadamente.
6. Não remover Windows Features sem entender utilização/dependências.
7. Não executar reparos pesados apenas porque eles existem.
8. Não limpar Component Store de maneira irreversível como otimização padrão.
9. Logs devem ser tratados como evidência, não como diagnóstico conclusivo isoladamente.
10. Correlação não significa causalidade.
11. Toda recomendação deve possuir explicação.
12. “Nada precisa ser alterado” é um resultado válido.
13. O mesmo diagnóstico deve poder alimentar várias áreas do produto.
14. Alterações precisam ser mensuráveis e reversíveis quando possível.
15. O aplicativo não deve criar problemas para depois alegar que os corrigiu.

---

# Hipóteses para pesquisas futuras surgidas durante Sistema

- Podemos construir análise de boot baseada diretamente em ETW sem instalar o Windows ADK?
- Podemos reproduzir parte da visibilidade do Autoruns usando APIs e estruturas nativas?
- Existe maneira confiável de obter o Startup Impact calculado pelo Windows?
- Podemos relacionar WHEA com benchmark/stability tests?
- Qual conjunto mínimo de Event Providers oferece excelente sinal sem produzir ruído?
- Podemos detectar software impedindo o PC de entrar corretamente em idle?
- É possível construir baseline adaptativa de uso de recursos por máquina?
- Podemos detectar “tweaks antigos” comparando estado atual com configuração suportada pela build?
- Como saber que uma configuração foi alterada por OEM e não pelo usuário?
- Podemos oferecer diagnóstico de Explorer lento através de shell extensions?
- Quais informações de WER estão disponíveis localmente sem depender de serviços externos?
- Até onde podemos interpretar minidumps com segurança?
- Quais capacidades do Windows Performance Toolkit podem inspirar ferramentas nativas do BeadWise?
- Como produzir um health scan útil sem transformar diferentes métricas em um score arbitrário?

# Discovery Catalog — Sistema
## Segunda passada — auditoria de lacunas

> Este bloco complementa os itens 1–50 levantados na primeira passada da área Sistema.
>
> O objetivo não é substituir os itens anteriores, mas registrar capacidades novas encontradas durante uma investigação adicional de:
>
> - Sysinternals;
> - Windows Performance Toolkit;
> - ETW;
> - diagnósticos de processos;
> - memória;
> - energia/suspensão;
> - integridade de executáveis;
> - troubleshooting avançado.

---

# 51. File / Registry Activity Trace

Status:
CANDIDATE

Type:
Diagnostic Tool

Technical Domain:
System / Processes / I/O

Primary Product Area:
Tools

Possible Consumers:
- Repair
- Optimization
- Monitoring
- Diagnostics

Final UI Placement:
TBD

Descrição:
Capturar temporariamente operações de sistema de arquivos, Registry e processos/threads realizadas durante um problema.

Possível objetivo:
Descobrir exatamente o que uma aplicação ou componente está tentando acessar durante lentidão, erro ou comportamento anormal.

Possível benefício:
Permitir diagnóstico baseado em atividade real em vez de inferência baseada apenas em estado atual.

Aplicabilidade:
All PCs / Troubleshooting scenarios

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
User-triggered Diagnostic

Possíveis dados:
- file read;
- file write;
- file create/delete;
- Registry read;
- Registry write;
- process creation;
- thread activity;
- result codes;
- timestamps;
- process identity;
- stack information quando disponível.

Possíveis casos:
- programa demora para abrir;
- aplicação procura arquivo inexistente repetidamente;
- Registry lookup falhando;
- serviço tentando acessar recurso inexistente;
- aplicação fazendo milhares de operações desnecessárias;
- erro de permissão.

Dúvidas:
- Implementar via ETW ou outras APIs?
- Qual overhead?
- Como filtrar bilhões de eventos para encontrar sinal útil?
- Devemos disponibilizar análise avançada manual semelhante a uma ferramenta ou apenas diagnósticos guiados?

Observação:
Inspirado conceitualmente no Process Monitor.

---

# 52. Repeated Failed I/O Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System / I/O

Primary Product Area:
Repair

Possible Consumers:
- Repair
- Optimization

Final UI Placement:
TBD

Descrição:
Detectar processos realizando repetidamente operações de arquivo ou Registry que falham.

Possível objetivo:
Encontrar componentes quebrados, configurações órfãs ou comportamentos capazes de gerar atraso e trabalho inútil.

Possível benefício:
Transformar traces extensos em findings acionáveis.

Aplicabilidade:
Affected PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Context-dependent

Action Mode:
Diagnostic Only

Possíveis padrões:
- NAME NOT FOUND repetitivo;
- PATH NOT FOUND;
- ACCESS DENIED;
- tentativa repetida de abrir arquivo removido;
- Registry key inexistente;
- procura contínua por DLL/configuração ausente.

Dúvidas:
- Quantidade de falhas necessária para considerar um padrão relevante?
- Muitos desses eventos são comportamento normal do Windows.
- Como diferenciar probing normal de problema real?

---

# 53. Open File / Resource Lock Diagnosis

Status:
CANDIDATE

Type:
Diagnostic Tool

Technical Domain:
Processes / Object Manager

Primary Product Area:
Tools

Possible Consumers:
- Repair
- Cleanup
- Uninstaller

Final UI Placement:
TBD

Descrição:
Identificar qual processo mantém determinado arquivo, diretório ou outro recurso aberto.

Possível objetivo:
Responder a problemas como:

“Por que este arquivo não pode ser apagado/movido?”

Possível benefício:
Melhorar reparos, limpeza e desinstalações sem exigir que o usuário procure manualmente pelo processo responsável.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic / User Choice

Possíveis informações:
- process;
- PID;
- object type;
- resource path;
- handle.

Possíveis ações futuras:
- abrir processo;
- solicitar encerramento normal;
- encerrar processo;
- tentar novamente a operação.

Dúvidas:
- Fechar handles diretamente deve ser evitado?
- Como tratar processos protegidos?
- Quando encerrar processo pode causar perda de dados?

---

# 54. Locked File Cleanup Assistant

Status:
IDEA

Type:
Repair / Tool

Technical Domain:
System / Cleanup

Primary Product Area:
Tools

Possible Consumers:
- Cleanup
- Uninstaller
- Repair

Final UI Placement:
TBD

Descrição:
Auxiliar operações que falham porque um arquivo está em uso.

Possível objetivo:
Permitir concluir limpeza ou desinstalação sem comportamento destrutivo.

Possível benefício:
Resolver leftovers resistentes e arquivos temporários presos.

Aplicabilidade:
Affected PCs

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Established concept

Action Mode:
User Choice

Possível fluxo:
1. detectar arquivo bloqueado;
2. identificar processo;
3. tentar encerramento normal;
4. oferecer reboot quando necessário;
5. repetir operação depois.

Dúvidas:
- Devemos usar operações pendentes para o próximo boot?
- Quais arquivos nunca devem ser manipulados dessa forma?

---

# 55. Loaded Module Inventory

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Processes / Modules

Primary Product Area:
Shared

Possible Consumers:
- Repair
- Monitoring
- Diagnostics

Final UI Placement:
TBD

Descrição:
Identificar DLLs e outros módulos carregados por um processo.

Possível objetivo:
Investigar conflitos, crashes, extensões e software injetado em processos.

Possível benefício:
Fornecer contexto muito mais profundo para problemas de aplicação.

Aplicabilidade:
Troubleshooting scenarios

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic Only

Possíveis dados:
- module path;
- version;
- publisher;
- digital signature;
- loaded address;
- process using module.

Possíveis casos:
- aplicação crasha depois de instalar overlay;
- Explorer carrega shell extension problemática;
- software antigo injeta DLL;
- módulo incompatível permanece após upgrade.

Dúvidas:
- Até onde isso deve ser mostrado ao usuário comum?
- Como correlacionar módulo com crash sem afirmar causalidade?

---

# 56. Unsigned / Untrusted Module Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System / Processes

Primary Product Area:
Shared

Possible Consumers:
- Repair
- Diagnostics
- Security

Final UI Placement:
TBD

Descrição:
Detectar executáveis e módulos sem assinatura digital válida ou com cadeia de confiança problemática.

Possível objetivo:
Adicionar contexto de confiança a processos, startup entries, serviços e módulos carregados.

Possível benefício:
Ajudar a distinguir componentes conhecidos de elementos que merecem investigação adicional.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established mechanism / Context-dependent interpretation

Action Mode:
Diagnostic Only

Possíveis dados:
- signed;
- unsigned;
- invalid signature;
- publisher;
- certificate chain;
- file version;
- hash.

Importante:
“Unsigned” não significa “malicioso”.

Dúvidas:
- Como evitar falsos alarmes?
- Devemos verificar somente elementos relevantes para determinado finding?
- Integração externa com reputação/hash pertence ao escopo do produto?

---

# 57. Executable Trust Metadata

Status:
CANDIDATE

Type:
Shared Diagnostic Capability

Technical Domain:
System / Security Metadata

Primary Product Area:
Shared

Possible Consumers:
- Startup
- Services
- Processes
- Drivers
- Uninstaller
- Repair

Final UI Placement:
TBD

Descrição:
Fornecer um mecanismo compartilhado para consultar assinatura, publisher, versão e hash de arquivos executáveis.

Possível objetivo:
Evitar que cada área implemente sua própria lógica de identificação de software.

Possível benefício:
Melhorar classificação de serviços, startup entries e processos.

Aplicabilidade:
Application-wide

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Internal / Automatic

Possíveis informações:
- publisher;
- signature status;
- certificate;
- file version;
- product name;
- company name;
- SHA hash.

Dúvidas:
- Cache de resultados?
- Como validar arquivos grandes sem overhead excessivo?

---

# 58. Process Hang Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Processes / Reliability

Primary Product Area:
Monitoring

Possible Consumers:
- Repair
- Monitoring

Final UI Placement:
TBD

Descrição:
Detectar aplicações que deixam de responder de maneira persistente.

Possível objetivo:
Diferenciar aplicação apenas ocupada de aplicação realmente travada.

Possível benefício:
Permitir diagnóstico automático de hangs recorrentes.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Possíveis informações:
- process;
- duration;
- recurrence;
- CPU state;
- memory usage;
- associated crash/hang records.

Dúvidas:
- Usar conceito de “not responding” do Windows?
- Quanto tempo deve transcorrer antes de registrar finding?

---

# 59. Triggered Process Dump Capture

Status:
IDEA

Type:
Advanced Diagnostic Tool

Technical Domain:
Processes / Debugging

Primary Product Area:
Tools

Possible Consumers:
- Repair
- Advanced Diagnostics

Final UI Placement:
TBD

Descrição:
Capturar dump de um processo quando ocorrer uma condição específica.

Possível objetivo:
Investigar problemas que aparecem apenas durante picos ou travamentos.

Possível benefício:
Permitir diagnóstico técnico muito mais profundo.

Aplicabilidade:
Advanced Troubleshooting

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Established

Action Mode:
Explicit User-triggered Diagnostic

Possíveis gatilhos:
- CPU spike;
- memory threshold;
- process hang;
- unhandled exception;
- performance counter threshold.

Dúvidas:
- O BeadWise deve analisar dump ou apenas exportá-lo?
- Tamanho e privacidade dos dumps.
- Deve ser uma ferramenta avançada escondida do usuário comum?

---

# 60. Memory Allocation Overview

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
Memory / System

Primary Product Area:
Shared

Possible Consumers:
- My PC
- Optimization
- Monitoring

Final UI Placement:
TBD

Descrição:
Mostrar como a memória física do Windows está realmente sendo utilizada.

Possível objetivo:
Evitar interpretações simplistas como:

“RAM ocupada = RAM desperdiçada”.

Possível benefício:
Diagnosticar pressão real de memória e explicar cache/standby/kernel usage.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic Only

Possíveis categorias:
- process working sets;
- standby;
- modified;
- free;
- kernel;
- drivers;
- file cache;
- paged/nonpaged usage.

Dúvidas:
- Quanto dessas informações pode ser obtido via APIs públicas?
- RAM terá investigação específica depois; este item é apenas capacidade sistêmica compartilhada.

---

# 61. Memory Snapshot Comparison

Status:
IDEA

Type:
Diagnostic

Technical Domain:
Memory

Primary Product Area:
Monitoring

Possible Consumers:
- Monitoring
- Repair

Final UI Placement:
TBD

Descrição:
Salvar dois estados de uso de memória e comparar sua evolução.

Possível objetivo:
Identificar crescimento anormal ou possíveis vazamentos.

Possível benefício:
Melhor diagnóstico de processos que consomem memória progressivamente.

Aplicabilidade:
Troubleshooting scenarios

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept

Action Mode:
Diagnostic

Possível uso:
snapshot A → executar aplicação → snapshot B → comparar.

Dúvidas:
- O diagnóstico será sistêmico ou por processo?
- Quanto tempo entre snapshots?
- Como distinguir cache legítimo de leak?

---

# 62. Per-Process Virtual Memory Analysis

Status:
IDEA

Type:
Advanced Diagnostic

Technical Domain:
Memory / Processes

Primary Product Area:
Tools

Possible Consumers:
- Monitoring
- Repair

Final UI Placement:
TBD

Descrição:
Analisar tipos de memória virtual e committed memory de um processo específico.

Possível objetivo:
Investigar aplicações com consumo anormal ou crescimento progressivo.

Possível benefício:
Identificar melhor a origem do consumo de memória do que apenas mostrar working set.

Aplicabilidade:
Advanced Troubleshooting

Shared Capability:
No

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic Only

Possíveis informações:
- private data;
- image;
- mapped file;
- heap;
- stack;
- committed;
- reserved;
- working set.

Dúvidas:
- É profundidade excessiva para o produto principal?
- Pode existir somente em Advanced Tools.

---

# 63. Resource Usage History

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System / Historical Telemetry

Primary Product Area:
Monitoring

Possible Consumers:
- Optimization
- Monitoring
- Battery
- Diagnostics

Final UI Placement:
TBD

Descrição:
Consultar dados históricos de utilização já mantidos pelo Windows quando disponíveis.

Possível objetivo:
Responder perguntas sobre consumo anterior sem precisar manter o BeadWise rodando continuamente.

Possível benefício:
Permitir descobrir quais aplicações utilizaram mais recursos em determinado intervalo.

Aplicabilidade:
Supported Windows Systems

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established mechanism

Action Mode:
Diagnostic

Possível fonte:
System Resource Usage Monitor (SRUM).

Possíveis tipos de informação:
- consumo de recursos;
- energia;
- aplicações;
- histórico agregado.

Dúvidas:
- Quais dados estão disponíveis em Windows desktop atual?
- Quais APIs são documentadas?
- Existe risco de depender de formato interno instável?

---

# 64. Sleep Blocker Detection

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
System / Power

Primary Product Area:
Shared

Possible Consumers:
- Optimization
- Power
- Laptop
- Repair

Final UI Placement:
TBD

Descrição:
Detectar processos, serviços ou drivers que estão impedindo automaticamente suspensão ou desligamento do display.

Possível objetivo:
Diagnosticar PC que nunca entra em sleep ou permanece ativo sem motivo aparente.

Possível benefício:
Reduzir consumo de energia, aquecimento e comportamento inesperado.

Aplicabilidade:
All PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic First

Possíveis dados:
- process power request;
- service power request;
- driver power request;
- display request;
- system request;
- away mode request.

Dúvidas:
- `requestsoverride` deve ser oferecido apenas como ação avançada?
- Nunca devemos ignorar uma Power Request sem explicar quem criou e por quê.

---

# 65. Wake Source Diagnosis

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
System / Power

Primary Product Area:
Power

Possible Consumers:
- Laptop
- Repair
- Optimization

Final UI Placement:
TBD

Descrição:
Identificar o que acordou o computador da última suspensão e quais fontes podem acordá-lo futuramente.

Possível objetivo:
Diagnosticar PCs que ligam ou acordam inesperadamente.

Possível benefício:
Melhorar comportamento de sleep e consumo energético.

Aplicabilidade:
Sleep-capable PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic / User Choice

Possíveis fontes:
- device wake;
- network adapter;
- mouse;
- keyboard;
- USB device;
- wake timer;
- scheduled maintenance.

Dúvidas:
- Quais wake sources podem ser desativadas com segurança?
- Devemos apenas orientar ou permitir configuração?

---

# 66. Sleep Transition History

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
System / Power

Primary Product Area:
Power

Possible Consumers:
- Laptop
- My PC
- Repair

Final UI Placement:
TBD

Descrição:
Analisar histórico de transições de sleep/standby e períodos de atividade inesperada.

Possível objetivo:
Diagnosticar consumo de bateria durante suspensão e falhas de Modern Standby.

Possível benefício:
Descobrir aplicações ou dispositivos responsáveis por atividade durante períodos em que o usuário esperava baixo consumo.

Aplicabilidade:
Supported Sleep Systems / Especially Laptops

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic Only

Possíveis fontes:
- SleepStudy;
- system sleep diagnostics;
- system power reports.

Dúvidas:
- Como interpretar resultados de forma amigável?
- Deve aparecer apenas quando Modern Standby estiver disponível?

---

# 67. Power Efficiency Diagnostic

Status:
CANDIDATE

Type:
Diagnostic

Technical Domain:
System / Power

Primary Product Area:
Power

Possible Consumers:
- Optimization
- Laptop
- My PC

Final UI Placement:
TBD

Descrição:
Executar análise suportada pelo Windows em busca de problemas comuns de eficiência energética.

Possível objetivo:
Identificar dispositivos, configurações ou software afetando consumo energético.

Possível benefício:
Melhorar autonomia e comportamento idle.

Aplicabilidade:
All PCs / Especially Laptops

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established

Action Mode:
Diagnostic

Possível fonte:
`powercfg /energy` ou APIs equivalentes.

Dúvidas:
- Quais findings também são relevantes para performance?
- Devemos reutilizar diretamente o relatório ou reconstruir a análise internamente?

---

# 68. Pending File Operation Detection

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System / Maintenance

Primary Product Area:
Repair

Possible Consumers:
- Repair
- Cleanup
- Uninstaller

Final UI Placement:
TBD

Descrição:
Detectar operações de arquivo que aguardam reinicialização para serem concluídas.

Possível objetivo:
Explicar arquivos que persistem após instalação/desinstalação ou reparos.

Possível benefício:
Evitar novas alterações enquanto o sistema possui estado pendente.

Aplicabilidade:
Affected PCs

Shared Capability:
Yes

Risco inicial:
Low

Evidence Level:
Established concept

Action Mode:
Diagnostic

Dúvidas:
- Consolidar com Pending Reboot Detection?
- Talvez este seja subfinding da feature 33 em vez de feature independente.

---

# 69. Environment Configuration Health

Status:
IDEA

Type:
Diagnostic

Technical Domain:
System Configuration

Primary Product Area:
Repair

Possible Consumers:
- Repair
- Developer Tools
- My PC

Final UI Placement:
TBD

Descrição:
Analisar configurações sistêmicas de ambiente que possam estar quebradas ou excessivamente poluídas.

Possível objetivo:
Detectar problemas causados por instalações/desinstalações e ferramentas antigas.

Possível benefício:
Resolver conflitos de execução e localizar resíduos.

Aplicabilidade:
Affected PCs

Shared Capability:
No

Risco inicial:
Medium

Evidence Level:
Context-dependent

Action Mode:
Diagnostic First

Possíveis objetos:
- PATH;
- environment variables;
- entries pointing to nonexistent directories;
- duplicate paths.

Dúvidas:
- Isso é relevante para usuário comum?
- Pode permanecer como ferramenta avançada.

---

# 70. System Diagnostic Capture Package

Status:
IDEA

Type:
Diagnostic Tool

Technical Domain:
Shared

Primary Product Area:
Tools

Possible Consumers:
- Repair
- Support

Final UI Placement:
TBD

Descrição:
Executar um conjunto selecionado de capturas diagnósticas durante um problema e produzir um pacote único.

Possível objetivo:
Evitar que troubleshooting avançado exija executar manualmente várias ferramentas.

Possível benefício:
Facilitar suporte técnico e investigação profunda.

Aplicabilidade:
Advanced Troubleshooting

Shared Capability:
Yes

Risco inicial:
Medium

Evidence Level:
Product concept using established mechanisms

Action Mode:
User-triggered

Possível conteúdo:
- system snapshot;
- event timeline;
- process/resource samples;
- trace ETW;
- relevant logs;
- WHEA;
- crash/hang data;
- startup;
- services;
- modules;
- optional dump.

Dúvidas:
- tamanho máximo;
- privacidade;
- duração;
- dados potencialmente sensíveis;
- formato exportável.

---

# Consolidação com itens da primeira passada

Algumas ideias encontradas nesta pesquisa **não devem virar novas features**, porque já estão suficientemente cobertas.

## Autoruns

Já coberto principalmente por:

- 9. Automatic Startup Inventory
- 10. Startup Impact Analysis
- 39. Explorer / Shell Extension Diagnosis

A segunda pesquisa reforça que Startup Inventory precisa olhar além de Run/RunOnce, incluindo extensões do Explorer, serviços, Winlogon e outros locais de autostart.

---

## Process Explorer

Não precisamos de uma feature chamada “Process Explorer”.

Suas capacidades se distribuem entre:

- 7. Process Resource Overview
- 8. Abnormal Resource Consumption Detection
- 53. Open File / Resource Lock Diagnosis
- 55. Loaded Module Inventory
- futuras capacidades específicas de Processos.

---

## Process Monitor

Não copiar a ferramenta como produto.

Suas ideias relevantes viraram:

- 51. File / Registry Activity Trace
- 52. Repeated Failed I/O Detection
- 31. “Why Is My PC Slow?” Diagnostic Session

---

## RAMMap / VMMap

A análise profunda de memória ficará majoritariamente para a área RAM.

Nesta área Sistema basta registrar:

- 60. Memory Allocation Overview
- 61. Memory Snapshot Comparison
- 62. Per-Process Virtual Memory Analysis

---

## Sigcheck

A capacidade importante não é “integrar Sigcheck”, mas possuir:

- 56. Unsigned / Untrusted Module Detection
- 57. Executable Trust Metadata

---

## ProcDump

A funcionalidade principal relevante é:

- 58. Process Hang Detection
- 59. Triggered Process Dump Capture

Não precisamos reproduzir um debugger completo.

---

## Power diagnostics

A pesquisa trouxe várias possibilidades, mas a maior parte deverá ser aprofundada posteriormente em Energia.

Em Sistema ficam registradas como capacidades compartilhadas:

- 64. Sleep Blocker Detection
- 65. Wake Source Diagnosis
- 66. Sleep Transition History
- 67. Power Efficiency Diagnostic

---

# Ideias encontradas mas deliberadamente NÃO adicionadas como feature

## “Clear Standby Memory”

Não transformar em feature de otimização.

A existência de ferramentas que conseguem manipular listas de memória não prova benefício de desempenho.

Análise de memória:
sim.

“RAM Cleaner”:
não.

---

## Fechar handles arbitrariamente

É tecnicamente possível em alguns cenários, mas pode causar corrupção ou comportamento imprevisível.

Preferência:

identificar processo → solicitar encerramento apropriado → tentar novamente.

---

## VirusTotal automático para todo executável

Não foi incorporado como requisito.

Pode futuramente ser pesquisado como serviço opcional, mas envolve:
- rede;
- privacidade;
- terceiros;
- termos de serviço;
- possibilidade de upload.

A necessidade principal nesta fase é conhecer assinatura/hash/publisher localmente.

---

## Sysmon como dependência

Sysmon é extremamente poderoso, mas não há motivo nesta fase para exigir instalação permanente de um driver/serviço apenas para o BeadWise funcionar.

Pode ser estudado posteriormente para diagnósticos avançados.

---

## Limpeza manual do WinSxS

Rejeitar.

Somente mecanismos suportados de servicing devem ser investigados.

---

# Resultado da segunda passada

Itens anteriores:
50

Novas capacidades/hipóteses registradas:
20

Total provisório da área Sistema:
~70

Entretanto, várias das novas capacidades são ferramentas diagnósticas avançadas e poderão futuramente:
- ser consolidadas;
- migrar para Processos;
- migrar para RAM;
- migrar para Energia;
- migrar para Repair;
- permanecer como Shared Capability.

O número de itens não representa o número final de features do produto.

---

# Avaliação de maturidade da área Sistema

A segunda pesquisa encontrou principalmente extensões das seguintes capacidades já conhecidas:

- observabilidade;
- processos;
- memória;
- startup;
- tracing;
- reliability;
- repair;
- power diagnostics.

Poucos novos “domínios inteiros” apareceram.

Isso indica que a área Sistema está começando a atingir saturação de discovery inicial.

Ainda existem ideias possíveis, porém novas pesquisas amplas provavelmente começarão a produzir principalmente:

- subfeatures;
- variações técnicas;
- duplicatas;
- capacidades pertencentes a áreas que ainda serão pesquisadas separadamente.

Conclusão provisória:

**Sistema possui cobertura suficiente para não bloquear o avanço para Energia.**

Discovery de Sistema permanece aberto e novos itens podem ser adicionados posteriormente conforme surgirem durante outras áreas.

---

# Candidatos particularmente interessantes descobertos na segunda passada

## Alta prioridade conceitual

- File / Registry Activity Trace
- Open File / Resource Lock Diagnosis
- Executable Trust Metadata
- Process Hang Detection
- Memory Allocation Overview
- Sleep Blocker Detection
- Wake Source Diagnosis
- Sleep Transition History

## Diferenciais avançados potenciais

- Repeated Failed I/O Detection
- Loaded Module Inventory
- Triggered Process Dump Capture
- Resource Usage History
- Diagnostic Capture Package

## Provavelmente ferramentas avançadas e não fluxo principal

- Per-Process Virtual Memory Analysis
- Environment Configuration Health
- Triggered Process Dump Capture
- Diagnostic Capture Package

---

# Estado final provisório — Sistema

Sistema agora cobre, em nível de discovery:

- identificação do Windows;
- capability detection;
- baseline;
- health;
- recomendações;
- bottlenecks;
- processos;
- startup;
- services;
- scheduled tasks;
- optional features;
- servicing;
- SFC/DISM;
- Component Store;
- Search;
- Event Logs;
- WHEA;
- WER;
- crashes;
- BSOD;
- ETW/WPR;
- boot;
- configuration drift;
- shell;
- accessibility safeguards;
- reporting;
- resource correlation;
- file/Registry activity;
- handles;
- loaded modules;
- executable trust;
- hangs;
- dumps;
- memory observability;
- resource history;
- sleep blockers;
- wake sources;
- sleep diagnostics;
- power efficiency diagnostics;
- pending operations;
- advanced diagnostic capture.

Estado sugerido da área:

**INITIAL DISCOVERY COMPLETE**
