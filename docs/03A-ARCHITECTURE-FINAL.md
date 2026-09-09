# BeadWise / PC Optimizer V2 — Architecture

## 1. Status deste documento

Este documento é a arquitetura consolidada da Fase 3 do BeadWise V2.

Ele substitui a versão conceitual anterior de `03-ARCHITECTURE.md` como referência arquitetural da fase seguinte.

A arquitetura foi consolidada após:

```text
Discovery
↓
Feature Specs
↓
Prototipagem e provas técnicas
↓
Auditoria da Fase 2
↓
Auditoria da arquitetura existente
↓
Extração dos requisitos arquiteturais reais
↓
Consolidação de padrões
↓
Definição da arquitetura alvo
↓
Revisão adversarial
```

A Fase 3 não implementa o backend completo. Ela define uma base suficientemente estável para iniciar implementação sem congelar prematuramente detalhes que ainda dependem de evidência prática.

---

# 2. Objetivo arquitetural

A arquitetura deve permitir implementar o BeadWise completo de forma:

- segura;
- testável;
- modular;
- sustentável;
- reversível quando aplicável;
- observável;
- compatível com operações privilegiadas controladas;
- capaz de sobreviver a interrupções e reinicializações quando necessário.

A prioridade não é produzir a arquitetura mais sofisticada possível.

A prioridade é produzir:

```text
a arquitetura mais simples que suporte corretamente
as necessidades reais do BeadWise
```

---

# 3. Filosofia do produto preservada na arquitetura

Fluxo conceitual principal:

```text
Detect / Observe
↓
Understand / Analyze
↓
Recommend
↓
Plan
↓
Preview / Dry-run
↓
Apply
↓
Verify
↓
Measure
↓
Keep / Recover / Rollback
```

Esse fluxo não é obrigatório para todas as features.

Features puramente diagnósticas podem terminar em:

```text
Observe
↓
Normalize
↓
Analyze
↓
Interpret
```

Monitoring e Benchmark possuem pipelines próprios.

---

# 4. Princípios obrigatórios

## 4.1 UI desacoplada do Windows

A interface não deve chamar diretamente:

- Registry;
- PowerShell;
- Win32;
- WMI/CIM;
- serviços;
- executáveis do Windows;
- APIs de fabricante;
- ETW;
- Performance Counters.

A UI trabalha com operações, estados e contratos de alto nível.

## 4.2 Backend testável sem UI

O backend deve poder ser executado e testado sem interface gráfica.

A UI deve poder ser desenvolvida com mocks que implementem os mesmos contratos expostos pelo backend real.

## 4.3 Product Area não define ownership técnico

Uma feature exibida em `My PC`, `Optimization`, `Benchmark`, `Monitoring`, `Repair` ou outra área não deve automaticamente viver em um módulo técnico exclusivo daquela tela.

Implementação deve seguir responsabilidade técnica e reutilização.

## 4.4 Shared capabilities devem ser realmente compartilhadas

Evitar duplicação como:

```text
MyPcGpuDetector
OptimizationGpuDetector
BenchmarkGpuDetector
MonitoringGpuDetector
```

quando todos consomem a mesma capacidade básica de detecção de GPU.

## 4.5 Não criar uma classe por Feature ID

Existem 234 Feature Specs.

Isso não implica:

```text
234 services
234 interfaces
234 handlers
234 executors
```

Uma Feature Spec pode representar:

- regra;
- policy;
- analyzer;
- consumidor de capability compartilhada;
- composição de operações;
- workflow;
- comportamento de produto.

## 4.6 Segurança e reversibilidade são requisitos arquiteturais

Quando aplicável, a arquitetura deve suportar:

```text
ChangePlan
Dry-run
Snapshot
Apply
Verify
Rollback / Recovery
Verify Recovery
```

## 4.7 Estado original é diferente de default

Snapshots devem preservar o estado real anterior.

Se algo não existia antes da alteração, recovery pode exigir remoção em vez de escrita de um valor presumido.

## 4.8 Elevação não deve contaminar o programa inteiro

O aplicativo não deve rodar permanentemente como administrador por padrão.

Operações privilegiadas passam por uma fronteira explícita e controlada.

## 4.9 Restart, logoff e reboot fazem parte do fluxo

A arquitetura deve conseguir representar continuidade após:

- reinício do aplicativo;
- reinício de processo;
- reinício de serviço;
- reinício de dispositivo quando aplicável;
- logoff;
- reboot.

## 4.10 Monitoring é diferente de operação pontual

Pipelines contínuos não devem ser forçados a usar o mesmo modelo de uma mutação de configuração.

---

# 5. Direção tecnológica

## 5.1 Stack base

Direção arquitetural atual:

```text
C#
.NET
WinUI 3
Windows App SDK
```

APIs nativas continuam permitidas quando necessárias.

Possíveis mecanismos:

```text
.NET APIs
Win32 / PInvoke
COM
Registry
PowerShell
WMI / CIM
Executáveis nativos
Service Control Manager
ETW
Performance Counters / PDH
Vendor APIs
```

O uso de integração nativa não implica reescrever a aplicação em C/C++.

## 5.2 Estado

```text
Direção de stack (C#/.NET/WinUI 3/Windows App SDK): PROVISIONAL
Versão exata do .NET: OPEN
Versão mínima do Windows: OPEN
Suporte final a Windows 10: OPEN
Packaging: OPEN
Updater: OPEN
```

A direção tecnológica é forte o suficiente para orientar a arquitetura, mas ainda não é tratada como decisão imutável até o fechamento das versões/plataformas suportadas.

---

# 6. Estrutura alvo da solution

Estrutura inicial recomendada:

```text
BeadWise.App
BeadWise.Application
BeadWise.Core
BeadWise.Windows

BeadWise.Core.Tests
BeadWise.Application.Tests
BeadWise.Windows.Tests
```

Não criar inicialmente assemblies separados apenas por domínio funcional.

Exemplos que permanecem condicionais:

```text
BeadWise.Persistence
BeadWise.Monitoring
BeadWise.Benchmark
BeadWise.DevTools
```

Eles só devem virar projetos próprios quando existir complexidade suficiente que justifique separação física.

---

# 7. Dependências entre projetos

Direção desejada:

```text
BeadWise.App
      ↓
BeadWise.Application
      ↓
BeadWise.Core
      ↑
BeadWise.Windows
```

Regras:

- `Core` não depende de `App`;
- `Core` não depende de `Windows`;
- `Application` coordena casos de uso usando contratos;
- `Windows` implementa contratos de infraestrutura/plataforma;
- `App` consome Application e contratos de apresentação;
- dependências circulares são proibidas.

A composição concreta ocorre em um Composition Root no startup.

---

# 8. BeadWise.App

Responsável por:

- WinUI 3;
- XAML;
- MVVM;
- navegação;
- apresentação;
- animações;
- loading/progress;
- recomendações;
- warnings;
- erros compreensíveis;
- interação do usuário.

Não deve conter lógica específica de Windows.

A UI consome contratos de alto nível, como:

```text
AnalysisResult
Recommendation
ChangePlan
ExecutionProgress
ExecutionResult
SessionState
MonitoringSample
BenchmarkResult
```

O frontend deve poder operar com mocks equivalentes ao backend real.

---

# 9. BeadWise.Application

Responsável pela orchestration dos casos de uso.

Não deve virar uma God Layer.

Organização interna recomendada:

```text
Application/
├── Analysis/
├── Recommendations/
├── Planning/
├── Execution/
├── Sessions/
├── Recovery/
├── Monitoring/
└── Measurement/
```

Essa separação é lógica; não exige assemblies diferentes.

Exemplos conceituais de casos de uso:

```text
AnalyzeComputer
BuildRecommendations
PreviewChanges
StartSession
ApplyPlan
VerifySession
RecoverSession
ResumeSession
StartMonitoring
RunMeasurement
```

Application coordena, mas não conhece detalhes de Registry, PowerShell, WMI, Win32 ou vendor APIs.

---

# 10. BeadWise.Core

Core contém apenas conceitos e contratos centrais do produto.

Conceitos principais:

```text
Feature
Capability
Observation
Analysis
Recommendation
Operation
ChangePlan
Snapshot
Session
ExecutionResource
Risk
RestartRequirement
Result
Error
Policy
```

Evitar transformar Core em depósito de tudo.

---

# 11. Capability, Feature e Product Area

## 11.1 Capability

Capability é uma capacidade técnica reutilizável.

Exemplos:

```text
Detect GPU
Inspect Processes
Read Power Configuration
Collect CPU Metrics
Create Restore Point
```

## 11.2 Feature

Feature é comportamento de produto composto a partir de capabilities, regras, analyzers, policies e/ou operations.

## 11.3 Product Area

Product Area é contexto de apresentação/uso.

Exemplos:

```text
My PC
Optimization
Benchmark
Monitoring
Repair
Tools
Settings
```

Uma capability pode alimentar múltiplas features e áreas.

---

# 12. Modelo de Feature

Uma feature não possui contrato universal obrigatório.

Evitar:

```text
IOptimizationFeature
  Detect
  Analyze
  Plan
  Apply
  Verify
  Rollback
```

como interface aplicada a todo o produto.

A direção é composição por comportamentos/capabilities menores.

Conceitualmente:

```text
Feature
├── Metadata
├── Applicability
├── Consumers
├── CapabilityDependencies
├── Analyzer(s)
├── RecommendationPolicy
└── Operation(s), quando aplicável
```

Contratos concretos devem existir somente quando houver consumidor real e benefício de teste/isolamento/substituição.

---

# 13. Famílias arquiteturais consolidadas

A arquitetura deve suportar oito famílias de comportamento.

## 13.1 Observation

```text
Collect
↓
Normalize
↓
Return structured observation
```

## 13.2 Analysis

```text
Observations
+
Context
↓
Interpretation / Correlation
```

## 13.3 Recommendation

```text
Observation
+
Analysis
+
Context
+
Compatibility
+
Risk
↓
Recommendation
```

## 13.4 Planned Mutation

```text
Observe
↓
Plan
↓
Validate
↓
Preview
↓
Approve
↓
Snapshot when needed
↓
Execute
↓
Verify
```

## 13.5 Recovery

```text
Failure or user request
↓
Recover
↓
Observe again
↓
Compare with original state
```

## 13.6 Monitoring

```text
Collector
↓
Sample
↓
Normalize
↓
Aggregate
↓
Publish
```

## 13.7 Measurement / Workload

```text
Prepare
↓
Baseline
↓
Workload
↓
Measure
↓
Normalize
↓
Compare
```

## 13.8 Session / Orchestration

Coordena plans, operations, snapshots, progress, failures, restart, recovery e medição.

---

# 14. Observation e fontes da verdade

Uma observation deve ser estruturada e poder representar:

```text
Value
Source
Timestamp
Availability
Confidence / Quality when applicable
Unsupported
Unknown
Error
```

Cada feature/spec deve definir a fonte da verdade adequada.

Evitar detectar por um mecanismo e alterar por outro sem entender inconsistências possíveis.

---

# 15. Recommendation Engine

Recomendação é separada de execução.

```text
Observation
↓
Analysis
↓
Recommendation Engine
↓
ChangePlan
↓
Executor
```

O Recommendation Engine decide:

- se recomendar;
- motivo;
- confiança;
- benefício esperado;
- risco;
- applicability.

O Executor não decide sozinho se uma mudança é benéfica.

---

# 16. Operation

Uma `Operation` representa uma unidade concreta de execução ou ação técnica.

Modelo conceitual:

```text
Operation
├── Id
├── Description
├── CurrentState
├── TargetState
├── Risk
├── PrivilegeRequirement
├── RestartRequirement
├── Reversibility
├── Dependencies
├── ExecutionResources
└── RecoveryMetadata
```

Admin, restart e reversibilidade pertencem preferencialmente à operação concreta, não a um booleano global na feature inteira.

---

# 17. ChangePlan

`ChangePlan` é conceito central e definitivo.

Modelo conceitual:

```text
ChangePlan
├── PlanId
├── FeatureIds
├── Operations[]
├── Preconditions
├── Risk
├── Warnings
├── PrivilegeRequirements
├── RestartRequirements
└── RecoveryInformation
```

Regra obrigatória:

```text
Preview
+
Apply
```

devem usar o mesmo plano lógico aprovado.

Não recalcular silenciosamente uma execução diferente daquela apresentada ao usuário.

---

# 18. Dry-run

Dry-run não pode executar alteração persistente.

Fluxo típico de uma mutação:

```text
Observe
↓
Validate prerequisites
↓
Build ChangePlan
↓
Estimate privilege/restart/risk
↓
Return preview
```

Em features puramente diagnósticas, dry-run pode equivaler ao pipeline de observação/análise sem alteração de estado.

---

# 19. Snapshot

Snapshots são dados estruturados.

Modelo conceitual:

```text
Snapshot
├── SnapshotId
├── SessionId
├── OperationId
├── CapturedAt
├── StateKind
├── OriginalState
└── ExistenceSemantics
```

Snapshots devem registrar também inexistência.

Exemplo:

```text
ValueExistedBefore = false
```

não é equivalente a:

```text
ValueExistedBefore = true
Value = 0
```

---

# 20. Recovery e Rollback

Não existe um rollback mágico universal.

A arquitetura possui dois níveis.

## 20.1 Operation Recovery

Cada mecanismo conhece a própria forma de compensação/restauração.

Exemplos:

```text
Registry operation
→ restore previous value/existence

Service operation
→ restore previous configuration/status

Power operation
→ restore previous power configuration
```

## 20.2 Session Recovery

Coordena:

- quais operações foram executadas;
- quais falharam;
- quais podem ser revertidas;
- ordem de recovery;
- recovery pendente;
- falhas de rollback;
- necessidade de ação manual/System Restore.

---

# 21. System Restore

System Restore é uma rede adicional de segurança.

Ele não substitui snapshots e recovery específicos.

A criação de restore point deve permanecer encapsulada por capability/service próprio.

---

# 22. Session

O conceito central deixa de ser `OptimizationSession` e passa a ser uma sessão genérica de execução.

Nome concreto pode ser `ExecutionSession`, `OperationSession` ou equivalente.

Modelo conceitual:

```text
Session
├── SessionId
├── CreatedAt
├── Status
├── Plan
├── Operations
├── Snapshots
├── Results
├── Failures
├── Progress
├── PendingRestart
├── RecoveryState
└── MeasurementContext
```

## 22.1 Estados conceituais

```text
Created
Analyzing
Planning
Ready
Executing
Verifying
Completed
PartiallyCompleted
Failed
RecoveryRequired
RollingBack
RolledBack
RestartPending
Interrupted
```

A state machine real deve impedir transições inválidas.

---

# 23. Restart e continuidade

`RequiresRestart = bool` não é suficiente.

Modelo conceitual:

```text
RestartRequirement
├── None
├── App
├── Process
├── Service
├── Device
├── Logoff
└── Reboot
```

Fluxo de continuidade:

```text
Persist session
↓
Restart / Logoff / Reboot
↓
Resume
↓
Observe / Verify
↓
Continue or Recover
```

Estado necessário para retomada deve sobreviver a crash/reboot quando a operação exigir.

---

# 24. Privilege Boundary

A fronteira privilegiada é uma decisão arquitetural obrigatória.

A aplicação normal não deve conhecer detalhes de elevação.

Conceitualmente:

```text
Application
normal user
     │
     ▼
IPrivilegedExecutor (ou contrato equivalente)
     │
     ▼
Validation / authorization
     │
     ▼
Elevated execution mechanism
     │
     ▼
Windows
```

Regras:

- elevação somente quando necessária;
- inputs privilegiados devem ser validados;
- nenhuma string arbitrária deve virar comando privilegiado sem controle;
- retorno deve ser estruturado;
- falha de elevação deve ser recuperável/explicável;
- UI inteira não deve permanecer elevada sem necessidade.

## 24.1 OPEN

Ainda não está congelado:

```text
UAC pontual
vs
processo auxiliar elevado

IPC concreto
Named Pipes / outra opção
```

A fronteira existe mesmo antes da escolha do mecanismo concreto.

---

# 25. BeadWise.Windows

Responsável por toda comunicação direta com Windows e hardware/vendor APIs.

Organização possível:

```text
Windows/
├── Registry/
├── Power/
├── Services/
├── Processes/
├── Network/
├── Display/
├── Devices/
├── Storage/
├── Security/
├── Shell/
├── Wmi/
├── PowerShell/
├── Win32/
├── Monitoring/
├── Vendor/
└── Restore/
```

A estrutura física final deve seguir responsabilidades reais, não obrigação estética.

---

# 26. Scripts, PowerShell e executáveis

Scripts podem existir quando forem a melhor ferramenta.

Devem passar por abstração responsável por:

- execução;
- argumentos;
- validação;
- timeout;
- exit code;
- stdout/stderr;
- logging;
- segurança.

Não espalhar `Process.Start` e PowerShell diretamente pela codebase.

Evitar parsing frágil quando houver API estruturada mais confiável.

---

# 27. Monitoring

Monitoring possui infraestrutura própria.

Modelo conceitual:

```text
MetricProvider
↓
Collector
↓
Sampling Pipeline
↓
Normalizer
↓
Aggregator
↓
Consumer
```

Possíveis consumidores:

- Live UI;
- FPS/frametime;
- Diagnostics;
- Session recording;
- Benchmark;
- Recommendation.

Monitoring não deve ser implementado como uma feature pontual rodando em loop artificialmente.

## 27.1 OPEN

Ainda dependem de implementação/prova:

- frequência de sampling por provider;
- retenção;
- volume máximo;
- persistência completa de time-series;
- necessidade real de processo separado/overlay.

---

# 28. Measurement e Benchmark

Infraestrutura de medição é diferente da Feature de produto `Benchmark`.

Conceitos reutilizáveis:

```text
Workload
MeasurementProvider
Baseline
MeasurementSession
Result
Comparison
```

Consumidores possíveis:

- Benchmark;
- Before/After Optimization;
- Hardware Diagnostics;
- Stress Tests;
- Validation;
- Regression comparison.

Evitar cada feature criar seu próprio sistema de medição.

---

# 29. Execution Resource e concorrência

Dependências apenas entre Feature IDs não são suficientes.

Operações podem disputar recursos técnicos compartilhados.

Modelo conceitual:

```text
ExecutionResource
```

Exemplos:

```text
PowerConfiguration
Service:<name>
Registry:<scope/key>
Device:<id>
NetworkStack
DisplayConfiguration
StorageVolume:<id>
```

Operations declaram recursos necessários.

O orchestrator usa isso para:

- exclusividade;
- ordenação;
- prevenção de conflito;
- locks;
- concorrência segura.

## 29.1 Política inicial

Reads independentes podem rodar em paralelo quando seguro.

Mutações sensíveis são sequenciais por padrão.

Paralelismo só é introduzido com benefício e segurança claros.

---

# 30. Dependencies e conflicts

Metadados conceituais possíveis:

```text
DependsOn
ConflictsWith
RunBefore
RunAfter
RequiredCapability
ExecutionResources
```

Não criar uma teia artificial entre 234 Feature IDs quando a dependência real é de capability, recurso ou prerequisite.

---

# 31. Cancelamento

Cancelamento é propriedade da operação/workflow.

Estados possíveis:

```text
Cancelable
CancelableBetweenSteps
NotCancelable
```

Nunca interromper arbitrariamente uma mutação crítica no meio se isso puder deixar o sistema inconsistente.

---

# 32. Progress

Progress deve ser estruturado.

Modelo conceitual:

```text
ExecutionProgress
├── SessionId
├── FeatureId
├── OperationId
├── Step
├── Completed
├── Total
└── Message
```

A UI exibe progresso sem conhecer detalhes de baixo nível.

---

# 33. Error Model

Exceções técnicas não chegam diretamente à UI.

Resultado conceitual:

```text
Result<T>
├── Status
├── Value
├── Error
├── Warnings
└── Evidence
```

Erro estruturado:

```text
Error
├── Code
├── Category
├── UserMessage
├── TechnicalDetails
├── Recoverable
├── SuggestedAction
└── RestartImpact
```

Categorias relevantes:

```text
AccessDenied
AdminRequired
Unsupported
Unavailable
Blocked
Timeout
Cancelled
ApplyFailed
VerifyFailed
RollbackFailed
PartialFailure
HardwareUnsupported
PolicyBlocked
```

`Unsupported`, `Unavailable`, `Unknown` e `Error` devem permanecer estados distintos.

---

# 34. Logging

Logging deve ser estruturado e centralizado.

Campos conceituais:

```text
Timestamp
Severity
SessionId
FeatureId
OperationId
Component
Event
Result
ErrorCode
TechnicalContext
```

Requisitos:

- auditoria;
- debugging;
- suporte;
- correlação;
- redaction/sanitização;
- retenção controlada.

Logs não são fonte primária de estado e não substituem persistência operacional.

---

# 35. Persistência

Persistência é híbrida e separada por finalidade.

## 35.1 Settings

JSON é apropriado para:

- configurações pequenas;
- preferências;
- manifests;
- export/import quando necessário.

## 35.2 Operational State

Necessita consistência maior:

- sessões;
- plans;
- snapshots metadata;
- recovery state;
- pending restart.

SQLite é o candidato principal, sujeito à validação de implementação.

## 35.3 History

Pode incluir:

- executions;
- resultados;
- benchmark history;
- diagnostic evidence;
- correlação de sessões.

SQLite é candidato principal.

## 35.4 Monitoring Data

Monitoring/time-series deve possuir política própria de retenção e volume.

Não despejar amostras de monitoring indiscriminadamente nas mesmas tabelas críticas de sessão.

## 35.5 Estado

```text
Separar persistência por finalidade: DECIDED
Persistência híbrida: DECIDED como princípio
JSON para settings/manifests: PROVISIONAL
SQLite para estado estruturado/histórico: PROVISIONAL
Schema definitivo: OPEN
Retenção de monitoring: OPEN
Formato final de snapshot payloads: OPEN
```

---

# 36. Hardware e compatibilidade

A arquitetura deve representar capacidade e disponibilidade explicitamente.

Conceitos possíveis:

```text
SystemCapabilities
WindowsVersion
Architecture
CpuVendor
GpuVendor
HasBattery
SupportsFeatureX
```

Hardware ausente ou não suportado não deve gerar comportamento ambíguo.

Estados possíveis:

```text
Supported
Unsupported
Unavailable
Unknown
Error
```

Vendor APIs ficam isoladas em `BeadWise.Windows`.

---

# 37. Feature Catalog / Runtime Catalog

**Status: PROVISIONAL.**

Pode existir catálogo runtime para registrar features e capabilities disponíveis.

Responsabilidades possíveis:

```text
all features
supported features
features by capability
features by product area
```

O catálogo não deve presumir que todas as 234 specs estão implementadas.

Features `RESEARCH`, `BLOCKED` ou `DEFERRED` não forçam implementação prematura.

---

# 38. Composition Root

A aplicação possui um ponto central de composição de dependências.

Exemplo:

```text
IRegistryReader
→ WindowsRegistryReader

ISessionRepository
→ SqliteSessionRepository

IPrivilegedExecutor
→ future concrete implementation
```

Evitar criação arbitrária de dependências dentro de features.

---

# 39. Testing Architecture

Separar:

```text
Unit
Integration
System
E2E
```

Também classificar testes por ambiente/risco:

```text
Pure
Fixture / Sandbox
Real Windows
Admin
Hardware-dependent
Vendor-dependent
Restart-dependent
Destructive-safe
```

## 39.1 Core Tests

Testar:

- models;
- policies;
- analyzers puros;
- planning;
- state machines;
- dependency/resource resolution;
- error mapping.

## 39.2 Application Tests

Testar orchestration com mocks/fakes:

- sessions;
- planning;
- execution flow;
- recovery;
- restart handling;
- progress;
- cancellation.

## 39.3 Windows Tests

Usar quando necessário:

- ambiente real;
- wrappers;
- fixtures;
- VM;
- hardware específico.

Backend deve continuar testável sem UI.

---

# 40. Mocks e frontend

O frontend usa mocks que implementam os mesmos contratos do backend real.

Deve ser possível simular:

- analyzing;
- no recommendations;
- recommendations found;
- unsupported;
- admin required;
- restart required;
- partial failure;
- success;
- rollback available;
- interrupted session.

A integração UI/backend ocorre por contratos estáveis.

---

# 41. Ownership cross-domain

Fronteiras que merecem atenção:

```text
Display ↔ Energy
CPU ↔ Processes
Update ↔ Drivers
Network ↔ Security
Cleaning ↔ Storage
Repair ↔ Drivers/PnP
```

Ownership técnico deve refletir capabilities e mechanisms compartilhados, não apenas o domínio documental da Feature Spec.

---

# 42. O que evitar

Evitar:

```text
God classes
God Application Service
static globals
UI chamando Registry/PowerShell/Win32
scripts espalhados
parsers copiados
uma classe por Feature ID
interfaces sem consumidor
repositories/managers/factories por estética
features acopladas a XAML
um backend monolítico gigantesco
exceções técnicas chegando diretamente ao usuário
logs usados como banco de estado
rollback genérico mágico
```

---

# 43. Regra de simplicidade

Não adicionar sem necessidade concreta:

- CQRS;
- event sourcing;
- microservices;
- message broker;
- plugin system sofisticado;
- múltiplos processos;
- banco excessivamente complexo.

Complexidade arquitetural também possui custo.

---

# 44. Arquitetura evolutiva

A arquitetura pode evoluir.

Novos assemblies só devem surgir quando responsabilidades, dependências ou escala justificarem.

Mudanças arquiteturais relevantes devem ser registradas em documento de decisões/ADR com:

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

---

# 45. Decisões consolidadas da Fase 3

## DECIDED

- UI desacoplada de Windows;
- Application como camada de orchestration;
- Core independente de UI/Windows;
- Windows isolado atrás de contratos;
- backend testável sem UI;
- frontend compatível com mocks;
- Product Area diferente de ownership técnico;
- shared capabilities reutilizáveis;
- evitar 1 service/interface por Feature ID;
- evitar contrato universal `IOptimizationFeature`;
- Feature composta por capabilities, analyzers, policies e operations;
- ChangePlan central;
- Preview e Apply usando o mesmo plano;
- dry-run sem mutação persistente;
- snapshots estruturados com semântica de existência;
- recovery em nível de operation e session;
- session genérica, não apenas OptimizationSession;
- restart como tipo, não bool;
- privilege boundary explícita;
- monitoring como pipeline contínuo próprio;
- measurement como infraestrutura reutilizável;
- ExecutionResource para conflitos/locks;
- concorrência conservadora para mutações;
- logging estruturado separado de estado;
- error model estruturado;
- persistência híbrida por finalidade;
- Composition Root;
- arquitetura de testes independente da UI.

## PROVISIONAL

- C#/.NET/WinUI 3/Windows App SDK como stack base;
- SQLite como principal persistência estruturada;
- JSON para settings/manifests;
- quatro assemblies principais inicialmente;
- nomes exatos de alguns contratos/modelos (`ExecutionSession`, `IPrivilegedExecutor`, etc.).

## OPEN

- versão exata do .NET;
- versão mínima do Windows;
- suporte final a Windows 10;
- mecanismo concreto de elevação;
- processo auxiliar elevado ou elevação pontual;
- IPC concreto se necessário;
- schema definitivo do SQLite;
- storage/retention final de monitoring;
- processo separado para overlay/monitoring se necessário;
- packaging;
- updater;
- design system;
- separação futura de Monitoring/Benchmark/Persistence em assemblies próprios.

---

# 46. Gate de implementação após a Fase 3

A arquitetura está madura para iniciar **vertical slices controlados**, mas a implementação ampla não deve escalar antes de resolver os itens fundacionais ainda abertos que forem necessários para os slices seguintes.

Clareza atual:

```text
[x] arquitetura de camadas
[x] projetos base
[x] direção de dependências
[x] Feature vs Capability vs Product Area
[x] modelo composicional
[x] ChangePlan
[x] dry-run
[x] snapshot
[x] recovery
[x] session
[x] restart model
[x] privilege boundary
[x] monitoring model
[x] measurement model
[x] error model
[x] logging model
[x] persistência por finalidade
[x] testing strategy
[x] mocks / frontend contract

[ ] versão final .NET / Windows support
[ ] mecanismo final de elevação / IPC
[ ] schema final de persistência
[ ] packaging / updater
```

Resultado do gate:

```text
FASE 3 / ARCHITECTURE: PASS
VERTICAL SLICE CONTROLADO: GO
IMPLEMENTAÇÃO AMPLA: CONDITIONAL GO
```

Os itens ainda abertos não impedem um vertical slice controlado. Porém, mecanismo de elevação/IPC deve ser fechado antes de escalar operações privilegiadas; versão/plataformas suportadas antes de comprometer compatibilidade ampla; e decisões de persistência/packaging devem ser fechadas antes de módulos que dependam diretamente delas.

---

# 47. Estratégia recomendada para início da implementação

Não implementar 234 features em sequência.

Começar com um **vertical slice representativo** que exercite a arquitetura.

Esse slice deve, idealmente, cobrir:

```text
Observation
Analysis
Recommendation
ChangePlan
Dry-run
Snapshot
Apply
Verify
Recovery
Persistence
Structured logging
Error handling
Progress
Mock UI contract
```

Depois validar arquitetura com:

- uma feature read-only diagnóstica;
- uma mutação simples e reversível;
- uma operação com privilégio;
- uma operação com restart/recovery;
- uma pequena amostra de monitoring;
- uma pequena amostra de measurement.

Somente após essas provas a implementação deve escalar para mais features.

---

# 48. Auditoria documental final da Fase 3

A revisão documental final verificou:

- consistência entre decisões `DECIDED`, `PROVISIONAL` e `OPEN`;
- aderência às Feature Specs e às evidências da Fase 2;
- ausência de obrigação 1:1 entre Feature ID e service/interface;
- separação entre UI, Application, Core e Windows;
- representação explícita de ChangePlan, dry-run, snapshot, recovery, restart e privilege boundary;
- tratamento distinto para monitoring e measurement;
- persistência separada por finalidade;
- error model, logging, concorrência, mocks e testing;
- manutenção dos pontos sem evidência suficiente como `OPEN`.

Correções realizadas na auditoria final:

1. a direção de stack passou a ser classificada consistentemente como `PROVISIONAL`;
2. persistência foi separada entre princípio arquitetural `DECIDED` e tecnologias concretas `PROVISIONAL`;
3. o Runtime Feature Catalog foi explicitamente marcado como `PROVISIONAL`;
4. o gate de implementação foi esclarecido: vertical slice controlado pode começar, enquanto escala ampla continua condicionada aos pontos fundacionais ainda abertos.

Nenhum blocker arquitetural residual foi encontrado.

```text
FASE 3: CLOSED
ARCHITECTURE AUDIT: PASS
GATE PARA VERTICAL SLICE / IMPLEMENTATION PLANNING: GO
```

---

# 49. Princípio final

A arquitetura deve permitir:

```text
Backend funciona sozinho
```

assim como:

```text
Frontend funciona com mocks
```

para então conectar ambos por contratos estáveis.

Se alterar uma tela exigir reescrever uma capability, ou alterar uma capability exigir redesenhar a UI, a separação arquitetural provavelmente está falhando.

Se adicionar uma nova feature exigir criar uma nova camada, um novo processo e dezenas de abstrações sem necessidade concreta, a arquitetura provavelmente está ficando complexa demais.

Se uma operação do Windows puder falhar, reiniciar, exigir admin ou precisar de recovery, isso deve estar representado explicitamente no modelo — não escondido em scripts ou efeitos colaterais.
