# PC Optimizer V2 — Decisions Log

## 1. Objetivo deste documento

Este arquivo registra decisões importantes tomadas durante o planejamento e desenvolvimento da V2.

O objetivo é preservar não apenas **o que foi decidido**, mas também **por que foi decidido**.

Sempre que uma decisão relevante mudar, registrar:

```text
problema
↓
alternativas
↓
decisão
↓
motivo
↓
consequências
```

Não apagar decisões antigas.

Quando uma decisão for substituída, marcar como:

```text
SUPERSEDED
```

e referenciar a nova decisão.

---

# 2. Formato padrão

Usar:

```text
## DEC-XXX — Título

Status:
Date:

Context:

Decision:

Reasoning:

Consequences:

Revisit when:
```

Estados:

```text
PROPOSED
ACCEPTED
SUPERSEDED
REJECTED
DEFERRED
```

---

# DEC-001 — Reiniciar a V2 estruturalmente

Status: ACCEPTED

Context:

A primeira versão acumulou problemas de planejamento, implementação parcial, acoplamento entre frontend e backend, fluxo visual distante da visão original e alto retrabalho.

Decision:

A V2 será tratada como uma nova base estrutural.

Partes comprovadamente funcionais da versão anterior poderão ser reaproveitadas.

Reasoning:

Continuar expandindo a arquitetura atual aumentaria dívida técnica.

Recomeçar estruturalmente permite aplicar os aprendizados da V1 sem descartar código funcional útil.

Consequences:

- projeto antigo vira referência;
- funcionalidades não são migradas automaticamente;
- código reaproveitado precisa respeitar a nova arquitetura;
- documentação e planejamento passam a preceder implementação.

---

# DEC-002 — Não finalizar a V1 apenas para atingir versão 1.0

Status: ACCEPTED

Context:

Existia a possibilidade de terminar superficialmente a versão atual e só depois iniciar a V2.

Decision:

Não investir esforço apenas para finalizar uma versão 1.0 que já sabemos estar estruturalmente desalinhada.

Reasoning:

Isso consumiria tempo em uma base que será substituída e aumentaria retrabalho.

Consequences:

A prioridade passa a ser a nova estrutura.

---

# DEC-003 — Planejamento precede implementação

Status: ACCEPTED

Context:

Na V1, funcionalidades eram frequentemente descritas apenas pelo resultado desejado.

Decision:

Nenhuma feature relevante deve entrar em implementação definitiva antes de possuir especificação suficiente.

Reasoning:

Evitar descobrir durante o desenvolvimento:

- o que exatamente alterar;
- como detectar;
- como executar;
- como validar;
- como reverter.

Consequences:

Cada feature passa por:

```text
Discovery
↓
Specification
↓
Prototype
↓
Validation
↓
Approval
↓
Implementation
```

---

# DEC-004 — Discovery será ampla, mas superficial inicialmente

Status: ACCEPTED

Context:

Precisamos descobrir o universo de funcionalidades antes de definir o produto final.

Decision:

A primeira fase listará possibilidades com descrição leve.

Reasoning:

Entrar profundamente em cada item durante discovery tornaria a fase lenta e excessivamente rígida.

Consequences:

A investigação técnica profunda acontece posteriormente.

---

# DEC-005 — Toda feature relevante terá ficha técnica

Status: ACCEPTED

Context:

A V1 sofreu com funcionalidades pouco especificadas.

Decision:

Features candidatas deverão utilizar o template oficial de especificação.

Reasoning:

Padronizar entendimento técnico e critérios de qualidade.

Consequences:

A especificação deve cobrir, quando aplicável:

- detect;
- plan;
- dry-run;
- snapshot;
- apply;
- verify;
- rollback;
- compatibility;
- risk;
- testing.

---

# DEC-006 — Provas técnicas fazem parte do planejamento

Status: ACCEPTED

Context:

Nem toda funcionalidade teoricamente interessante é realmente implementável.

Decision:

Antes da implementação definitiva, funcionalidades técnicas relevantes devem possuir protótipo ou script de prova.

Reasoning:

Comprovar:

- possibilidade técnica;
- comportamento real;
- compatibilidade;
- rollback;
- limitações.

Consequences:

Scripts de teste podem existir antes do backend definitivo.

---

# DEC-007 — Dry-run não altera o sistema

Status: ACCEPTED

Context:

Existe interesse em testar funcionalidades sem modificar o PC durante desenvolvimento.

Decision:

Dry-run deve apenas:

```text
Detect
Plan
Validate
Preview
```

Nunca Apply.

Reasoning:

Aplicar e reverter não é dry-run e ainda introduz risco.

Consequences:

Dry-run pode ser usado frequentemente no PC principal.

---

# DEC-008 — Teste controlado real é separado do dry-run

Status: ACCEPTED

Context:

Dry-run não consegue provar que o Windows realmente aceita e aplica determinada alteração.

Decision:

Features que modificam o sistema deverão, quando necessário, passar por execução real controlada.

Fluxo:

```text
Snapshot
↓
Apply
↓
Verify
↓
Rollback
↓
Verify Rollback
```

Reasoning:

Isso valida tanto aplicação quanto reversibilidade.

Consequences:

VMs ou máquinas secundárias serão usadas quando apropriado.

---

# DEC-009 — Snapshot salva estado real, não default

Status: ACCEPTED

Context:

Usuários podem possuir configurações personalizadas.

Decision:

Rollback deve restaurar o estado encontrado antes da alteração.

Reasoning:

Restaurar valores padrão pode destruir personalizações anteriores.

Consequences:

Ausência de valor também deve ser registrada como estado.

---

# DEC-010 — Rollback e Restore Default são conceitos diferentes

Status: ACCEPTED

Context:

Pode existir necessidade futura de retornar ao padrão do Windows.

Decision:

Definir:

```text
Rollback
→ estado anterior à alteração

Restore Default
→ valor padrão esperado
```

Reasoning:

São operações semanticamente diferentes.

Consequences:

A UI e arquitetura não devem misturá-las.

---

# DEC-011 — Ponto de restauração não substitui rollback granular

Status: ACCEPTED

Context:

O ciclo de otimização deverá criar ponto de restauração.

Decision:

Manter múltiplas camadas de recuperação:

```text
Feature rollback
↓
Session rollback
↓
System Restore Point
```

Reasoning:

Usar System Restore para desfazer uma pequena alteração seria excessivo.

Consequences:

Snapshots continuam necessários durante sessões.

---

# DEC-012 — Frontend e backend serão desacoplados

Status: ACCEPTED

Context:

Na V1, bugs de interface e lógica ficaram difíceis de separar.

Decision:

A UI não deve conhecer detalhes técnicos de implementação do Windows.

Reasoning:

Permitir:

- desenvolvimento independente;
- testes isolados;
- mocks;
- mudanças visuais sem reescrever backend.

Consequences:

O frontend conversa com Application/Core através de contratos.

---

# DEC-013 — Backend deve funcionar sem frontend

Status: ACCEPTED

Context:

Na V1 era necessário abrir o app e navegar visualmente para testar funcionalidades.

Decision:

O backend deve ser utilizável por testes e DevTools.

Reasoning:

Uma feature deve ser comprovável sem UI.

Consequences:

Será criada uma ferramenta de desenvolvimento, provavelmente CLI.

---

# DEC-014 — Criar Optimizer.DevTools

Status: ACCEPTED

Context:

Precisamos testar backend manualmente sem frontend.

Decision:

Criar uma interface interna de desenvolvimento.

Possíveis comandos:

```text
detect
plan
dry-run
apply
verify
rollback
test
```

Reasoning:

Facilita desenvolvimento, debugging e provas técnicas.

Consequences:

DevTools deverá reutilizar exatamente o mesmo backend do produto.

---

# DEC-015 — Frontend poderá funcionar com mocks

Status: ACCEPTED

Context:

Queremos permitir desenvolvimento visual antes de todas as features estarem prontas.

Decision:

A UI poderá usar providers simulados.

Exemplo:

```text
MockFeatureProvider
RealFeatureProvider
```

Reasoning:

Permite desenvolver e testar fluxo independentemente do Windows real.

Consequences:

Mocks devem respeitar os mesmos contratos do backend real.

---

# DEC-016 — O fluxo do produto vem antes do layout final

Status: ACCEPTED

Context:

A visão central da aplicação é uma experiência fluida semelhante a um "rio".

Decision:

Definir jornada e fluxo antes do design definitivo.

Reasoning:

Evitar criar telas bonitas e depois tentar encaixar o produto nelas.

Consequences:

Processo:

```text
Product
↓
Journey
↓
States
↓
Design
↓
Implementation
```

---

# DEC-017 — A experiência principal deve ser contínua

Status: ACCEPTED

Context:

A divisão da V1 ficou fragmentada.

Decision:

O ciclo principal deve conduzir naturalmente entre etapas.

Conceito inicial:

```text
Discover
↓
Understand
↓
Choose
↓
Prepare
↓
Execute
↓
Confirm
↓
Maintain
```

Reasoning:

Preservar a identidade central do produto.

Consequences:

A divisão final das telas ainda será definida posteriormente.

---

# DEC-018 — Nem toda ferramenta precisa fazer parte do fluxo principal

Status: ACCEPTED

Context:

O app inclui ferramentas como calibração, pixel test, reparos e utilidades.

Decision:

Separar quando necessário:

```text
Core Optimization Journey
```

de:

```text
Standalone Tools
```

Reasoning:

Forçar toda ferramenta no mesmo fluxo prejudicaria a experiência.

---

# DEC-019 — Separar recomendação de execução

Status: ACCEPTED

Context:

Uma feature técnica não deve decidir sozinha se deve ser recomendada ao usuário.

Decision:

Direção conceitual:

```text
Analyzer
↓
Recommendation Engine
↓
Change Plan
↓
Executor
```

Reasoning:

Permite mudar inteligência de recomendação sem alterar executor.

Consequences:

Recommendation Engine terá testes próprios.

---

# DEC-020 — Nem toda feature é Optimization

Status: ACCEPTED

Context:

O produto engloba comportamentos conceitualmente diferentes.

Decision:

Modelo inicial:

```text
Feature
├── Diagnostic
├── Optimization
├── Repair
├── Configuration
└── Tool
```

Reasoning:

Evitar tratar diagnósticos ou preferências como "otimização".

Consequences:

Contratos poderão variar por capacidade.

---

# DEC-021 — Evitar interface única gigante de feature

Status: ACCEPTED

Context:

Nem todos os tipos de feature precisam de Apply, Rollback ou Recommendation.

Decision:

Avaliar contratos menores como:

```text
IDetectable
IPlannable
IApplicable
IVerifiable
IRollbackable
IRecommendable
```

Reasoning:

Evitar métodos artificiais e N/A por toda a implementação.

Consequences:

A estrutura final será validada com features reais.

---

# DEC-022 — Direção inicial: C# + .NET

Status: ACCEPTED

Context:

O aplicativo interage profundamente com Windows.

Decision:

Manter C#/.NET como stack principal, salvo evidência futura em contrário.

Reasoning:

C# permite integração com:

- Win32;
- Registry;
- WMI;
- PowerShell;
- P/Invoke;
- Windows APIs.

Consequences:

Código nativo será usado apenas quando necessário.

---

# DEC-023 — Direção inicial de UI: WinUI 3

Status: ACCEPTED

Context:

A V2 será um aplicativo Windows moderno.

Decision:

Usar inicialmente:

```text
WinUI 3
Windows App SDK
```

Reasoning:

Boa integração com Windows e stack .NET.

Consequences:

Decisão ainda poderá ser reavaliada antes da implementação definitiva.

---

# DEC-024 — MVVM ficará restrito à camada visual

Status: ACCEPTED

Context:

MVVM é útil para WinUI, mas não deve virar arquitetura de todo o sistema.

Decision:

Usar conceitualmente:

```text
View
↕
ViewModel
↓
Application
```

Reasoning:

Separar arquitetura de UI da arquitetura geral.

Consequences:

ViewModels não acessam Registry, PowerShell ou APIs de sistema diretamente.

---

# DEC-025 — Arquitetura em camadas

Status: ACCEPTED

Context:

Precisamos separar responsabilidades.

Decision:

Direção inicial:

```text
Optimizer.App
Optimizer.Application
Optimizer.Core
Optimizer.Windows
Optimizer.Persistence
Optimizer.DevTools
```

Reasoning:

Facilitar manutenção e testes.

Consequences:

Não criar projetos adicionais sem necessidade real.

---

# DEC-026 — Operações do Windows ficam isoladas

Status: ACCEPTED

Context:

Features podem usar diversas tecnologias.

Decision:

Centralizar infraestrutura específica em `Optimizer.Windows`.

Reasoning:

Evitar APIs nativas espalhadas pela codebase.

Consequences:

Features consomem abstrações sempre que isso trouxer benefício real.

---

# DEC-027 — Não superarquitetar

Status: ACCEPTED

Context:

Existe risco de reagir aos problemas da V1 criando complexidade excessiva.

Decision:

Não introduzir sem necessidade:

```text
microservices
CQRS
event sourcing
message brokers
complex plugin systems
multiple processes
```

Reasoning:

O produto é um aplicativo local para Windows.

Consequences:

Complexidade precisa justificar seu custo.

---

# DEC-028 — Modelo de privilégio ainda está aberto

Status: DEFERRED

Context:

Muitas features poderão exigir administrador.

Alternativas:

```text
App inteiro elevado
Elevação sob demanda
Processo auxiliar privilegiado
```

Decision:

Não fechar ainda.

Reasoning:

Precisamos saber quantas features exigem elevação e quais operações serão executadas.

Revisit when:

Após provas técnicas representativas.

---

# DEC-029 — Processo único vs processo auxiliar ainda está aberto

Status: DEFERRED

Context:

Processo separado pode melhorar isolamento, mas aumenta muito complexidade.

Decision:

Preferir inicialmente a solução mais simples.

Não fechar até termos requisitos concretos.

Revisit when:

Forem conhecidos:

- uso de admin;
- operações longas;
- reboot;
- monitoramento;
- segurança.

---

# DEC-030 — IPC só será escolhido se necessário

Status: DEFERRED

Context:

IPC só é necessário se existirem processos separados.

Decision:

Não selecionar tecnologia de IPC antecipadamente.

Reasoning:

Evitar arquitetura para um problema que talvez não exista.

Revisit when:

Processo auxiliar for aprovado.

---

# DEC-031 — JSON será persistência inicial

Status: ACCEPTED

Context:

O app não precisa inicialmente de grande histórico ou consultas complexas.

Decision:

Começar com arquivos JSON para dados simples e temporários.

Exemplos:

```text
settings
sessions
snapshots
```

Reasoning:

Simplicidade.

Consequences:

O restante do sistema deve acessar persistência através de abstrações.

---

# DEC-032 — SQLite não será usado sem necessidade real

Status: ACCEPTED

Context:

SQLite foi considerado inicialmente para persistência.

Decision:

Não introduzir banco por padrão.

Reasoning:

O fluxo atual não exige grande volume de dados históricos.

Revisit when:

Surgirem necessidades como:

- histórico extenso;
- consultas complexas;
- relações estruturadas;
- grandes volumes de sessões.

---

# DEC-033 — Separar configurações, sessões, snapshots e logs

Status: ACCEPTED

Context:

Um único JSON gigante seria frágil e difícil de manter.

Decision:

Persistir responsabilidades separadamente.

Reasoning:

Melhor organização e recuperação.

Consequences:

Possível estrutura:

```text
data/
├── settings/
├── sessions/
├── snapshots/
└── logs/
```

---

# DEC-034 — Logs não são estado do sistema

Status: ACCEPTED

Context:

Logs podem ser úteis para diagnóstico.

Decision:

Não utilizar logs como mecanismo de persistência de rollback ou sessão.

Reasoning:

Logs são evidência, não fonte de verdade operacional.

---

# DEC-035 — Google Stitch será ferramenta de prototipação

Status: ACCEPTED

Context:

Stitch permite criar rapidamente interfaces e fluxos.

Decision:

Usá-lo como referência visual e ferramenta de exploração.

Reasoning:

Permite iterar UX antes da implementação definitiva.

Consequences:

O código exportado não é automaticamente código de produção.

---

# DEC-036 — HTML/CSS do Stitch não será convertido cegamente

Status: ACCEPTED

Context:

A ideia inicial era exportar todo o frontend e pedir para um agente converter.

Decision:

Os arquivos serão usados como especificação visual e poderão ser reimplementados no framework real.

Reasoning:

Conversão automática integral pode gerar:

- componentes ruins;
- duplicação;
- estado mal estruturado;
- acoplamento.

Consequences:

Conversão ocorrerá por componentes/telas.

---

# DEC-037 — Extrair Design System antes de converter todas as telas

Status: ACCEPTED

Context:

Telas do Stitch terão elementos repetidos.

Decision:

Identificar componentes comuns primeiro.

Exemplos:

```text
Buttons
Cards
Navigation
Warnings
Progress
Feature States
```

Reasoning:

Evitar implementação diferente da mesma coisa em várias telas.

Consequences:

Agentes devem reutilizar componentes existentes.

---

# DEC-038 — Converter frontend gradualmente

Status: ACCEPTED

Context:

Converter todo o app visual de uma vez aumenta chance de erro.

Decision:

Sequência recomendada:

```text
AppShell
↓
Navigation
↓
Shared Components
↓
One Page
↓
Validate
↓
Next Page
```

Reasoning:

Tornar problemas pequenos e localizáveis.

---

# DEC-039 — Preview deve usar o mesmo ChangePlan do Apply

Status: ACCEPTED

Context:

A UI poderá mostrar alterações antes da execução.

Decision:

Não criar uma lógica paralela para preview.

Reasoning:

Evitar:

```text
UI says A
backend executes B
```

Consequences:

Dry-run e Apply partem do mesmo plano.

---

# DEC-040 — Erros serão estruturados

Status: ACCEPTED

Context:

Mensagens técnicas não devem chegar diretamente ao usuário.

Decision:

Criar modelo de erro padronizado.

Exemplos:

```text
ADMIN_REQUIRED
UNSUPPORTED
VERIFY_FAILED
ROLLBACK_FAILED
```

Reasoning:

Facilitar frontend, debugging e testes.

---

# DEC-041 — Verify precisa detectar estado real

Status: ACCEPTED

Context:

Um comando pode retornar sucesso sem produzir o resultado esperado.

Decision:

Quando possível:

```text
Apply
↓
Detect
↓
Compare
```

Reasoning:

Exit code 0 não prova alteração efetiva.

Consequences:

Features devem documentar fonte da verdade.

---

# DEC-042 — Rollback também precisa de Verify

Status: ACCEPTED

Context:

Executar rollback não prova restauração.

Decision:

Depois de rollback:

```text
Detect
↓
Compare with original snapshot
```

Reasoning:

Reversibilidade precisa ser comprovada.

---

# DEC-043 — Testes devem cobrir estado personalizado

Status: ACCEPTED

Context:

Testar apenas configurações padrão pode esconder bugs de rollback.

Decision:

Testar cenários com estado inicial customizado.

Reasoning:

Comprovar preservação das escolhas do usuário.

---

# DEC-044 — Ambientes de teste serão escolhidos por risco

Status: ACCEPTED

Context:

Nem toda feature precisa de VM, mas nem toda feature deve ser testada no PC principal.

Decision:

Usar progressivamente:

```text
Dry-run
VM
PC secundário
Hardware real
PC principal
```

conforme necessidade.

Reasoning:

Equilibrar segurança e praticidade.

---

# DEC-045 — "Não testado" não significa "suportado"

Status: ACCEPTED

Context:

Compatibilidade varia entre versões do Windows e hardware.

Decision:

Registrar explicitamente ambientes comprovados.

Reasoning:

Evitar promessas de compatibilidade baseadas em suposição.

---

# DEC-046 — Não recomendar tweak apenas porque existe

Status: ACCEPTED

Context:

Ferramentas de otimização frequentemente reproduzem ajustes sem benefício real.

Decision:

Toda recomendação precisa de justificativa técnica suficiente.

Reasoning:

O produto deve priorizar confiança em vez de quantidade de tweaks.

Consequences:

É válido retornar:

```text
No change recommended
```

---

# DEC-047 — Diferenciar otimização de preferência

Status: ACCEPTED

Context:

Algumas alterações mudam comportamento sem melhorar desempenho.

Decision:

Classificar corretamente.

Exemplos:

```text
Optimization
Configuration
Privacy
Visual Preference
```

Reasoning:

Não vender gosto pessoal como ganho técnico.

---

# DEC-048 — Não fabricar métricas

Status: ACCEPTED

Context:

Scores e percentuais podem parecer atraentes visualmente.

Decision:

Não criar números sem metodologia defensável.

Reasoning:

Preservar credibilidade.

Consequences:

Before/After só será mostrado quando houver medição confiável.

---

# DEC-049 — Documentação será parte do projeto

Status: ACCEPTED

Context:

Decisões da V1 ficaram espalhadas ou implícitas.

Decision:

Manter documentação estruturada em `/docs`.

Reasoning:

Preservar contexto para equipe e agentes.

Consequences:

Mudanças importantes exigem atualização documental.

---

# DEC-050 — Agentes de IA trabalharão com tarefas pequenas e verificáveis

Status: ACCEPTED

Context:

Na V1, grandes arquivos `.md` eram entregues para implementação de muitas mudanças simultaneamente.

Decision:

Evitar prompts do tipo:

```text
implemente tudo isso
```

Preferir tarefas com:

- objetivo;
- escopo;
- arquivos;
- critérios de aceitação;
- testes.

Reasoning:

Melhorar completude e previsibilidade.

Consequences:

`07-AI-CONTEXT.md` definirá regras permanentes para agentes.

---

# 3. Decisões prioritárias ainda abertas

As seguintes decisões deverão ser revisitadas futuramente:

```text
Versão mínima do Windows
Windows 10 support
Versão do .NET
Modelo de elevação
Processo auxiliar privilegiado
IPC
Logging framework
Packaging
Updater
Recovery após reboot
Duração dos snapshots
Histórico do usuário
Telemetria
Privacidade
Design system final
Fluxo final do usuário
Escopo da primeira versão
```

---

# 4. Como adicionar uma nova decisão

Antes de registrar uma decisão, perguntar:

> Isso terá impacto suficiente para que alguém possa questionar no futuro por que fizemos assim?

Se sim, criar um novo `DEC`.

Exemplos:

```text
Escolher Named Pipes
Suportar apenas Windows 11
Trocar WinUI por outra tecnologia
Criar processo privilegiado
Mudar formato de snapshots
Adicionar SQLite
```

Detalhes locais de implementação normalmente não precisam virar decisão arquitetural.

---

# 5. Como substituir uma decisão

Nunca apagar a anterior.

Exemplo:

```text
DEC-023
Status: SUPERSEDED
Superseded by: DEC-074
```

Na nova decisão, explicar por que mudou.

Isso mantém histórico do projeto.

---

# 6. Princípio final

Uma decisão documentada não é permanente.

Ela representa:

> A melhor escolha conhecida com as informações disponíveis naquele momento.

Quando novas evidências aparecerem:

```text
reavaliar
↓
decidir
↓
registrar
```

O objetivo deste arquivo é evitar decisões acidentais, não impedir evolução.