# PC Optimizer V2 — Project Plan

## 1. Objetivo deste documento

Este documento é o guia principal de desenvolvimento da V2 do aplicativo.

Ele existe para evitar os principais problemas encontrados na primeira versão:

- implementação iniciada sem definição técnica suficiente;
- funcionalidades descritas apenas pelo que deveriam fazer, sem definir como seriam executadas;
- frontend e backend excessivamente acoplados;
- dificuldade para testar funcionalidades sem navegar pela interface;
- fluxo de uso fragmentado;
- retrabalho constante;
- decisões importantes perdidas durante o desenvolvimento;
- funcionalidades implementadas parcialmente ou de forma diferente do planejado.

A V2 deve ser construída de forma deliberada, validando cada etapa antes de avançar.

Este documento define:

- filosofia do produto;
- princípios de desenvolvimento;
- fases do projeto;
- critérios para avançar entre fases;
- decisões já tomadas;
- decisões ainda abertas;
- documentação complementar;
- ordem geral de trabalho.

---

# 2. Visão do produto

O projeto é um aplicativo de otimização, diagnóstico, manutenção e configuração para Windows.

O objetivo não é apenas oferecer uma coleção de tweaks.

O aplicativo deve analisar o computador, entender seu estado atual e apresentar ao usuário ações relevantes de maneira clara, segura e organizada.

O produto deve priorizar:

- utilidade real;
- segurança;
- transparência;
- reversibilidade;
- compatibilidade;
- facilidade de uso;
- validação técnica;
- experiência fluida.

Uma otimização não deve ser recomendada apenas porque existe.

Sempre que possível, o aplicativo deve entender:

1. qual é o estado atual;
2. se existe algo que pode ser melhorado;
3. se a mudança faz sentido naquele computador;
4. quais alterações seriam necessárias;
5. quais riscos existem;
6. se a mudança pode ser revertida;
7. se a alteração realmente funcionou depois de aplicada.

---

# 3. Filosofia de experiência

A experiência do aplicativo deve funcionar como um fluxo contínuo.

A referência conceitual é um **rio**.

O usuário não deve sentir que está navegando entre dezenas de ferramentas desconectadas.

Cada etapa deve levar naturalmente à próxima.

A experiência deve transmitir:

- progressão;
- continuidade;
- clareza;
- contexto;
- confiança;
- controle.

A divisão final das etapas será definida depois que o catálogo de funcionalidades estiver suficientemente desenvolvido.

A organização visual não deve determinar prematuramente a arquitetura técnica.

Da mesma forma, a arquitetura técnica não deve limitar desnecessariamente a experiência do usuário.

---

# 4. Princípios fundamentais

## 4.1 Planejar antes de implementar

Nenhuma funcionalidade deve entrar diretamente em desenvolvimento apenas com uma descrição superficial.

Antes de sua implementação definitiva, deve existir especificação suficiente para responder:

- o que ela faz;
- por que ela existe;
- quando faz sentido;
- como detectar o estado atual;
- como será implementada;
- como será validada;
- como será testada;
- quais riscos possui;
- como será revertida;
- quais sistemas e versões suporta.

---

## 4.2 Não confundir ideia com feature aprovada

Durante a descoberta poderão surgir dezenas ou centenas de ideias.

Uma ideia ainda não é uma funcionalidade do produto.

Uma feature só deve ser considerada aprovada depois de passar por:

**Descoberta → Especificação → Prova técnica → Avaliação → Seleção**

---

## 4.3 Não implementar tweaks apenas porque são populares

Toda otimização deve possuir justificativa técnica.

Devem ser evitadas alterações:

- sem benefício demonstrável;
- baseadas apenas em mitos de otimização;
- redundantes;
- obsoletas;
- incompatíveis com versões atuais do Windows;
- que causem perda de funcionalidade sem benefício proporcional;
- que apresentem risco maior que o benefício esperado.

O aplicativo também deve ser capaz de concluir:

> Nenhuma alteração é recomendada.

---

## 4.4 Frontend e backend devem ser independentes

A interface não deve conhecer detalhes de implementação do Windows.

A UI não deve executar diretamente:

- PowerShell;
- Registry;
- powercfg;
- comandos CMD;
- APIs Win32;
- alterações de serviços;
- scripts do sistema.

A interface deve conversar com uma camada de aplicação através de contratos bem definidos.

Conceitualmente:

```text
Frontend
    ↓
Application Layer
    ↓
Core / Domain
    ↓
Windows Infrastructure
    ↓
Windows
```

Isso deve permitir:

- desenvolvimento do backend sem frontend;
- desenvolvimento do frontend com dados simulados;
- testes independentes;
- substituição da interface sem reescrever funcionalidades;
- alteração da implementação de uma feature sem modificar a UI.

---

## 4.5 Toda alteração deve ser observável

Sempre que aplicável, uma feature deve possuir o ciclo:

```text
Detect
↓
Plan
↓
Snapshot
↓
Apply
↓
Verify
```

E quando reversível:

```text
Rollback
↓
Verify Rollback
```

---

## 4.6 Dry-run nunca altera o sistema

Dry-run significa:

- detectar estado;
- gerar plano;
- calcular alterações;
- validar requisitos;
- mostrar o que seria feito.

Dry-run não deve:

- escrever no Registry;
- alterar serviços;
- executar mudanças persistentes;
- modificar planos;
- reiniciar componentes;
- alterar o sistema.

---

## 4.7 Rollback não significa restaurar padrão

Rollback deve restaurar o estado encontrado antes da alteração.

Exemplo:

```text
Estado inicial personalizado
↓
Aplicação
↓
Rollback
↓
Estado inicial personalizado
```

Não:

```text
Rollback
↓
Valor padrão do Windows
```

Quando necessário, poderá existir separadamente a operação:

```text
Restore Default
```

---

## 4.8 Ponto de restauração é uma proteção adicional

No início do ciclo de otimização, o aplicativo poderá criar um ponto de restauração do Windows.

Esse ponto de restauração não substitui rollback granular.

Camadas de proteção:

```text
Feature rollback
↓
Session rollback
↓
System Restore Point
```

O ponto de restauração deve ser tratado como proteção de último nível para problemas maiores.

---

# 5. Tipos de funcionalidades

Nem tudo no aplicativo deve ser tratado como "Optimization".

A arquitetura deve considerar diferentes categorias conceituais.

Inicialmente:

```text
Feature
├── Diagnostic
├── Optimization
├── Repair
├── Configuration
└── Tool
```

### Diagnostic

Analisa o computador e retorna informações ou possíveis problemas.

### Optimization

Altera alguma configuração com objetivo de melhorar comportamento, desempenho, consumo ou experiência.

### Repair

Tenta corrigir um problema identificado.

### Configuration

Permite personalizar comportamentos que não representam necessariamente uma otimização.

### Tool

Funcionalidade independente que fornece alguma utilidade.

Essa classificação poderá ser refinada durante a descoberta.

---

# 6. Processo oficial de desenvolvimento

A V2 seguirá as fases abaixo.

---

# FASE 1 — DESCOBERTA

## Objetivo

Mapear tudo que pode ser relevante para um aplicativo de otimização, diagnóstico, manutenção e configuração de PC.

Nesta fase, profundidade técnica não é necessária.

Cada item pode inicialmente conter apenas:

- nome;
- descrição breve;
- objetivo;
- possível benefício;
- categoria aproximada.

Exemplo:

```text
Suspensão seletiva USB

Permite ao Windows suspender portas/dispositivos USB para reduzir consumo de energia.

Possível aplicação:
Avaliar se desabilitar essa função pode ser útil em determinados cenários.
```

## Durante esta fase

Não:

- implementar;
- escrever código definitivo;
- decidir layout;
- decidir ordem final de apresentação.

Pode:

- pesquisar;
- comparar abordagens;
- descartar ideias ruins;
- identificar funcionalidades semelhantes;
- registrar dúvidas.

## Resultado esperado

Um catálogo amplo de possibilidades.

## Critério para avançar

A descoberta inicial deve estar suficientemente completa para representar as principais áreas do produto.

Ela não precisa ser definitiva.

Novas ideias poderão ser adicionadas depois.

---

# FASE 2 — ESPECIFICAÇÃO DE FEATURES

## Objetivo

Transformar ideias selecionadas em funcionalidades tecnicamente compreendidas.

Cada feature deverá utilizar o template oficial definido em:

```text
02-FEATURE-SPEC-TEMPLATE.md
```

A especificação deverá abordar, quando aplicável:

- objetivo;
- problema resolvido;
- benefício;
- estado atual;
- estado desejado;
- condições de recomendação;
- condições para não recomendar;
- implementação;
- APIs;
- Registry;
- PowerShell;
- comandos;
- privilégios;
- reinicialização;
- compatibilidade;
- hardware;
- riscos;
- detecção;
- dry-run;
- snapshot;
- apply;
- verify;
- rollback;
- verify rollback;
- testes.

## Critério para avançar

A feature deve estar suficientemente compreendida para que a implementação não dependa de descobrir sua lógica básica durante o desenvolvimento definitivo.

---

# FASE 3 — PROVA TÉCNICA

## Objetivo

Comprovar que a funcionalidade é realmente implementável.

Nesta fase podem ser utilizados:

- scripts PowerShell;
- pequenos programas C#;
- comandos;
- protótipos;
- APIs;
- ferramentas de teste.

O código produzido nesta fase não precisa ser o código final.

## Fluxo recomendado

```text
Detect
↓
Snapshot
↓
Plan
↓
Dry-run
↓
Apply
↓
Detect novamente
↓
Compare Expected vs Actual
↓
Verify
↓
Rollback
↓
Detect novamente
↓
Compare Original vs Restored
```

## Resultado possível

```text
PLAN: PASS
APPLY: PASS
VERIFY: PASS
ROLLBACK: PASS
RESTORE VERIFY: PASS
```

## Classificação de testabilidade

Cada feature deverá registrar:

```text
Dry-run supported:
Real system test required:
Safe in VM:
Hardware dependency:
Admin required:
Restart required:
Rollback supported:
Rollback tested:
```

## Critério para avançar

A implementação deve ter evidência suficiente de que funciona de forma previsível.

Features que falharem poderão:

- voltar para pesquisa;
- mudar de abordagem;
- ser adiadas;
- ser descartadas.

---

# FASE 4 — AVALIAÇÃO E SELEÇÃO

## Objetivo

Decidir quais features realmente pertencem ao produto.

Possíveis estados:

```text
Approved
Deferred
Experimental
Rejected
Needs Research
```

Critérios:

- benefício;
- risco;
- compatibilidade;
- confiabilidade;
- complexidade;
- relevância;
- experiência;
- manutenção futura.

Também será definido o primeiro escopo real da nova versão.

Nem toda feature descoberta precisa entrar no primeiro lançamento.

---

# FASE 5 — AGRUPAMENTO

## Objetivo

Organizar as funcionalidades em conjuntos coerentes.

A organização não deve considerar apenas a tecnologia utilizada.

Exemplo de organização técnica:

```text
Power
Registry
Services
Display
Network
GPU
Storage
Windows
```

Exemplo de organização para o usuário:

```text
Desempenho
Energia
Jogos
Sistema
Rede
Imagem
Manutenção
Diagnóstico
```

A organização final poderá combinar ambos os conceitos.

## Importante

A divisão deve favorecer a experiência do usuário.

Duas features tecnicamente diferentes podem aparecer juntas caso façam sentido na mesma etapa.

---

# FASE 6 — DEPENDÊNCIAS E CONFLITOS

## Objetivo

Mapear relações entre funcionalidades.

Perguntas obrigatórias:

```text
Feature A depende de B?
A deve executar antes de B?
A e B entram em conflito?
A só funciona em determinado hardware?
A invalida B?
A exige reboot antes de continuar?
A só deve ser recomendada em determinado cenário?
```

O resultado dessa fase será utilizado na definição do fluxo.

---

# FASE 7 — UX E JORNADA

## Objetivo

Transformar as funcionalidades organizadas em uma experiência contínua.

Neste momento será definido:

- início da jornada;
- etapas;
- ordem;
- transições;
- momentos de decisão;
- recomendações;
- preview;
- aplicação;
- resultados;
- restauração.

Possível estrutura conceitual:

```text
Descobrir
↓
Entender
↓
Escolher
↓
Preparar
↓
Executar
↓
Confirmar
↓
Manter
```

A estrutura definitiva será definida apenas depois das fases anteriores.

---

# FASE 8 — ARQUITETURA TÉCNICA FINAL

## Objetivo

Fechar as decisões arquiteturais com base nos requisitos reais descobertos.

Direção atual:

```text
C#
.NET
WinUI 3
Windows App SDK
```

A arquitetura conceitual seguirá separação em camadas.

Possível estrutura:

```text
Optimizer.App
Optimizer.Application
Optimizer.Core
Optimizer.Windows
Optimizer.Persistence
Optimizer.DevTools
```

E testes:

```text
Optimizer.Core.Tests
Optimizer.Application.Tests
Optimizer.Windows.Tests
```

Essa estrutura ainda não é definitiva.

Ela será validada após as provas técnicas.

## Decisões que devem ser fechadas nesta fase

- versão do .NET;
- WinUI 3 definitivo ou alternativa;
- MVVM;
- dependency injection;
- estrutura da solution;
- modelo de privilégios;
- processo único ou múltiplos processos;
- IPC se necessário;
- persistência;
- logging;
- serialização;
- sistema de updates;
- packaging;
- modelo de feature;
- contratos;
- tratamento de erros;
- execução privilegiada.

---

# FASE 9 — CONTRATOS

## Objetivo

Definir como as diferentes partes do sistema se comunicam.

Cada feature deverá possuir um comportamento previsível.

Conceitualmente:

```text
detect()
analyze()
plan()
apply()
verify()
rollback()
```

Nem todos os tipos de feature precisarão possuir todas as operações.

Exemplo conceitual:

```text
FeatureState
Recommendation
ChangePlan
Snapshot
ApplyResult
VerificationResult
RollbackResult
FeatureError
```

O frontend deve trabalhar com esses contratos e não com detalhes internos do Windows.

---

# FASE 10 — DESIGN E PROTOTIPAÇÃO

## Objetivo

Desenvolver a interface e validar a experiência antes da implementação visual definitiva.

Ferramenta atualmente considerada:

```text
Google Stitch
```

O Stitch deverá ser utilizado como:

- ferramenta de prototipação;
- referência visual;
- definição de fluxo;
- experimentação;
- construção do design system.

O HTML/CSS gerado não será tratado obrigatoriamente como código final.

Fluxo esperado:

```text
Stitch
↓
HTML / CSS / Assets / Referência visual
↓
Agente analisa
↓
Extrai componentes reutilizáveis
↓
Reimplementa no framework real
↓
Comparação visual
↓
Ajustes
↓
Integração
```

---

# FASE 11 — DESIGN SYSTEM

Antes de converter as telas em implementação definitiva deverão ser identificados componentes reutilizáveis.

Exemplos:

```text
AppShell
Navigation
PrimaryButton
SecondaryButton
FeatureCard
RecommendationCard
WarningCard
StepIndicator
ProgressIndicator
SectionHeader
Modal
```

Também deverão ser definidos:

- tipografia;
- espaçamento;
- bordas;
- radius;
- estados;
- animações;
- hierarquia;
- transições;
- comportamento responsivo, se aplicável.

---

# FASE 12 — IMPLEMENTAÇÃO DO BACKEND

## Objetivo

Transformar as features comprovadas em implementação definitiva.

A implementação deve respeitar:

- contratos;
- arquitetura;
- abstrações;
- testes;
- isolamento da infraestrutura.

A UI não deve ser necessária para validar o backend.

---

# FASE 13 — DEVTOOLS

Deverá existir uma forma de executar e testar funcionalidades sem frontend.

Inicialmente poderá ser uma CLI.

Exemplo:

```text
optimizer-dev detect usb-selective-suspend
optimizer-dev plan usb-selective-suspend
optimizer-dev test usb-selective-suspend
```

Possível resultado:

```text
SNAPSHOT
PASS

DRY RUN
PASS

APPLY
PASS

VERIFY
PASS

ROLLBACK
PASS

RESTORE VERIFY
PASS
```

Isso deverá permitir desenvolvimento e diagnóstico independente da interface.

---

# FASE 14 — IMPLEMENTAÇÃO DO FRONTEND

## Objetivo

Reproduzir a experiência validada no framework real.

Estratégia:

```text
Design de referência
↓
Componentes
↓
Telas
↓
Mocks
↓
Validação visual
↓
Integração real
```

O frontend deve inicialmente funcionar com dados simulados.

Exemplo:

```text
MockFeatureProvider
```

Posteriormente:

```text
RealFeatureProvider
```

O contrato deve permanecer o mesmo.

---

# FASE 15 — INTEGRAÇÃO

Nesta fase serão conectados:

```text
Frontend
↓
Application
↓
Core
↓
Windows
```

Problemas de UI, integração e backend deverão poder ser diagnosticados separadamente.

---

# FASE 16 — TESTES DE PRODUTO

O produto completo deverá ser testado em diferentes níveis.

## Teste unitário

Lógica isolada.

## Teste de integração

Interação real com Windows.

## Dry-run

Planejamento sem alteração.

## Teste real controlado

Aplicação, verificação e rollback.

## Teste de interface

Comportamento visual.

## End-to-end

Fluxo completo:

```text
UI
↓
Backend
↓
Windows
↓
Resultado
↓
UI
```

---

# 7. Persistência

Direção atual:

**Não utilizar SQLite inicialmente sem necessidade real.**

Preferência inicial:

```text
JSON
```

Possíveis arquivos:

```text
settings.json
session.json
```

E:

```text
logs/
```

Snapshots temporários também poderão ser armazenados em JSON.

Exemplo:

```text
/data
├── settings/
│   └── appsettings.json
├── sessions/
│   └── current-session.json
└── logs/
```

O aplicativo não deve acessar esses arquivos diretamente por toda a codebase.

Deverá existir abstração.

Exemplo:

```text
ISnapshotRepository
ISettingsRepository
```

Implementação inicial:

```text
JsonSnapshotRepository
JsonSettingsRepository
```

Se futuramente houver necessidade, a persistência poderá ser substituída sem alterar o Core.

---

# 8. Segurança e rollback

Cada mudança deverá possuir classificação de risco.

Modelo inicial:

```text
Low
Medium
High
Critical
```

A classificação final ainda será definida.

A estratégia de segurança poderá variar por feature.

Exemplos:

### Baixo risco

- alteração simples;
- facilmente detectável;
- reversível;
- sem reboot.

### Médio risco

- serviços;
- energia;
- rede;
- alteração com impacto maior.

### Alto risco

- boot;
- drivers;
- storage;
- segurança;
- componentes críticos.

Features mais sensíveis deverão possuir exigências adicionais de teste.

---

# 9. Ambientes de teste

Serão considerados:

```text
Dry-run
VM Windows
PC secundário
Hardware real
PC principal
```

Nem toda feature pode ser validada corretamente em VM.

Features dependentes de:

- monitor;
- GPU;
- USB;
- drivers;
- hardware específico;

podem exigir máquina física.

---

# 10. Recomendações

A recomendação e a execução devem ser responsabilidades separadas.

Conceitualmente:

```text
Analyzer
↓
Recommendation Engine
↓
Change Plan
↓
Executor
```

### Analyzer

Descobre o estado.

### Recommendation Engine

Decide se uma mudança faz sentido.

### Change Plan

Define exatamente o que será alterado.

### Executor

Executa o plano.

Isso permite evoluir a inteligência do aplicativo sem alterar as implementações de baixo nível.

---

# 11. Decisões já tomadas

## D-001

Frontend e backend devem ser desacoplados.

## D-002

O backend deve ser testável sem interface gráfica.

## D-003

Dry-run não altera o sistema.

## D-004

Features reversíveis devem utilizar snapshot do estado real anterior.

## D-005

Apply e Rollback devem ser validados separadamente.

## D-006

O estado restaurado deve ser comparado com o snapshot original.

## D-007

Ponto de restauração não substitui rollback granular.

## D-008

A interface será prototipada antes de sua implementação definitiva.

## D-009

Google Stitch poderá ser utilizado como referência visual e protótipo.

## D-010

HTML/CSS do Stitch não deve ser convertido de forma cega.

## D-011

A implementação visual deverá reutilizar componentes.

## D-012

Frontend poderá ser desenvolvido utilizando mocks.

## D-013

Persistência inicial deverá priorizar JSON.

## D-014

SQLite só deverá ser introduzido mediante necessidade concreta.

## D-015

A direção tecnológica atual é C# + .NET + WinUI 3 + Windows App SDK.

## D-016

A arquitetura técnica definitiva só será congelada após descoberta e provas técnicas representativas.

---

# 12. Decisões ainda abertas

- versão mínima do Windows;
- Windows 10 será suportado?
- Windows 11 será o foco?
- versão do .NET;
- modelo definitivo de elevação administrativa;
- processo único ou processo privilegiado separado;
- necessidade de IPC;
- estratégia definitiva de logging;
- mecanismo de atualização;
- sistema de packaging;
- persistência definitiva;
- duração dos snapshots;
- duração dos logs;
- política de rollback após reinício;
- funcionamento de operações que exigem reboot;
- critérios formais de recomendação;
- critérios formais de risco;
- comportamento caso uma aplicação falhe no meio;
- estratégia de recovery;
- escopo da primeira versão;
- nomenclatura final do produto;
- modelo definitivo de navegação;
- design system;
- suporte a idiomas;
- telemetria;
- privacidade.

Essas decisões deverão ser tomadas quando houver informação suficiente.

---

# 13. Documentação do projeto

Estrutura inicial:

```text
/docs
├── 00-PROJECT-PLAN.md
├── 01-DISCOVERY.md
├── 02-FEATURE-SPEC-TEMPLATE.md
├── 03-ARCHITECTURE.md
├── 04-UX-FLOW.md
├── 05-TESTING-STRATEGY.md
├── 06-DECISIONS.md
├── 07-AI-CONTEXT.md
└── 08-BACKLOG.md
```

---

# 14. Status de features

Modelo inicial:

```text
IDEA
↓
RESEARCH
↓
SPECIFIED
↓
PROTOTYPING
↓
PROVEN
↓
APPROVED
↓
IMPLEMENTING
↓
IMPLEMENTED
↓
TESTED
```

Estados alternativos:

```text
BLOCKED
DEFERRED
REJECTED
EXPERIMENTAL
```

---

# 15. Regra para agentes de IA

Agentes não devem receber listas gigantes de mudanças sem contexto e serem instruídos apenas a implementar tudo.

Toda tarefa relevante deve conter:

- escopo;
- objetivo;
- arquivos envolvidos;
- arquitetura relacionada;
- critérios de aceitação;
- limitações;
- comportamento esperado;
- testes esperados.

Agentes devem preferir tarefas pequenas e verificáveis.

---

# 16. Regra para implementação

Antes de implementar uma feature, confirmar:

```text
[ ] Feature especificada
[ ] Implementação conhecida
[ ] Método de detecção definido
[ ] Dry-run definido
[ ] Snapshot definido
[ ] Apply definido
[ ] Verify definido
[ ] Rollback definido, quando aplicável
[ ] Riscos conhecidos
[ ] Compatibilidade conhecida
[ ] Teste planejado
[ ] Contrato compatível com arquitetura
```

Se itens essenciais estiverem indefinidos, a feature volta para especificação ou pesquisa.

---

# 17. Regra de avanço do projeto

Não avançar de fase apenas porque existe vontade de começar a programar.

A implementação deve começar quando o nível de incerteza estiver suficientemente reduzido.

Ao mesmo tempo, o planejamento não deve virar paralisia.

Quando algo não puder ser decidido teoricamente:

```text
Pesquisa
↓
Protótipo
↓
Teste
↓
Decisão
```

---

# 18. Próximo passo

O próximo documento a ser desenvolvido é:

```text
01-DISCOVERY.md
```

Objetivo:

Criar a metodologia e estrutura utilizadas para levantar todas as possíveis funcionalidades do aplicativo.

Depois disso, inicia-se o catálogo inicial de funcionalidades.

---

# 19. Princípio final

A V2 não deve tentar prever perfeitamente todo o futuro.

Ela deve construir uma base em que mudanças futuras sejam possíveis sem destruir o que já funciona.

O objetivo do planejamento é reduzir retrabalho e incerteza, não eliminar toda mudança.

Sempre que surgir uma nova decisão importante:

1. avaliar;
2. documentar;
3. registrar motivo;
4. atualizar os documentos afetados;
5. só então implementar.
