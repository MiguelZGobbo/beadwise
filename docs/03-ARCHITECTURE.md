# PC Optimizer V2 — Architecture

## 1. Objetivo deste documento

Este documento define a arquitetura conceitual da V2 e registra a direção técnica atual.

O objetivo não é congelar prematuramente todos os detalhes de implementação.

A arquitetura final deverá ser ajustada com base em:

- funcionalidades aprovadas;
- provas técnicas;
- requisitos de privilégio;
- necessidade de reboot;
- necessidade de IPC;
- hardware suportado;
- persistência;
- distribuição;
- segurança.

A prioridade é criar uma estrutura em que:

- frontend e backend sejam desacoplados;
- features possam ser testadas sem UI;
- operações do Windows fiquem isoladas;
- lógica de produto não dependa diretamente de detalhes de infraestrutura;
- mudanças futuras não exijam reescrever o aplicativo inteiro.

---

# 2. Direção tecnológica atual

Direção inicial:

```text
C#
.NET
WinUI 3
Windows App SDK
```

Essa escolha ainda deve ser validada após uma amostra representativa de provas técnicas.

O uso de APIs nativas continuará sendo permitido quando necessário.

Possíveis mecanismos:

```text
.NET APIs
Win32
P/Invoke
Registry
PowerShell
WMI / CIM
Executáveis nativos
APIs de fabricantes
```

A existência de integração nativa não significa que toda a aplicação precise ser escrita em C ou C++.

---

# 3. Princípio arquitetural principal

A interface não deve saber como o Windows é alterado.

Conceitualmente:

```text
UI
↓
Application
↓
Core
↓
Infrastructure
↓
Windows
```

Exemplo:

```text
EnergyPage
↓
OptimizationService
↓
UsbSelectiveSuspendFeature
↓
IPowerConfiguration
↓
WindowsPowerConfiguration
↓
Windows API / powercfg
```

A UI não deve executar diretamente:

```text
Registry.SetValue(...)
Process.Start("powercfg", ...)
PowerShell(...)
```

---

# 3.1 Capabilities compartilhadas

A arquitetura deve distinguir:

```text
Product Area
```

de:

```text
System Capability
```

As áreas de produto representam experiências ou contextos de uso.

Exemplos provisórios:

```text
Optimization
My PC
Benchmark
Monitoring
Tools
```

Capabilities representam capacidades técnicas reutilizáveis.

Exemplos:

```text
System Information
Hardware Detection
Performance Monitoring
Windows Configuration
Storage Inspection
Display Detection
```

Uma mesma capability pode atender várias áreas.

Exemplo:

```text
GpuInformationService
        │
        ├── My PC
        ├── Optimization
        ├── Benchmark
        └── Monitoring
```

Não criar:

```text
MyPcGpuDetector
OptimizationGpuDetector
BenchmarkGpuDetector
MonitoringGpuDetector
```

quando todos precisam da mesma informação básica.

## Shared Services

Possíveis serviços compartilhados:

```text
ISystemInformationService
IHardwareDetectionService
ICapabilityService
IPerformanceMonitoringService
ISystemRestoreService
IProcessService
```

A lista definitiva deve surgir das features reais.

Não criar serviços apenas para preencher uma arquitetura teórica.

---

# 4. Camadas propostas

Estrutura conceitual inicial:

```text
Optimizer.App
Optimizer.Application
Optimizer.Core
Optimizer.Windows
Optimizer.Persistence
Optimizer.DevTools
```

E:

```text
Optimizer.Core.Tests
Optimizer.Application.Tests
Optimizer.Windows.Tests
```

Essa divisão poderá ser simplificada ou refinada posteriormente.

---

# 5. Optimizer.App

Responsável pela interface gráfica.

Provável tecnologia:

```text
WinUI 3
XAML
MVVM
```

Responsabilidades:

- navegação;
- apresentação;
- animações;
- componentes visuais;
- estados de loading;
- erros;
- recomendações;
- progresso;
- interação do usuário.

Não deve conter lógica específica de Registry, PowerShell, Win32 ou comandos do Windows.

---

# 6. MVVM

MVVM será considerado como padrão da camada visual.

Conceitualmente:

```text
View
↕
ViewModel
↓
Application Services
```

O ViewModel pode:

- solicitar análise;
- carregar recomendações;
- solicitar preview;
- iniciar aplicação;
- exibir resultados.

O ViewModel não deve:

- editar Registry;
- executar powercfg;
- reiniciar serviços;
- chamar diretamente APIs de baixo nível.

---

# 7. Optimizer.Application

Responsável por coordenar casos de uso.

Exemplos:

```text
AnalyzeComputer
GetRecommendations
BuildOptimizationPlan
StartOptimizationSession
ApplyOptimization
RollbackSession
GetOptimizationStatus
```

Essa camada coordena várias partes, mas não deve conter detalhes desnecessários de infraestrutura.

Exemplo:

```text
StartOptimizationSession
↓
Create restore point
↓
Capture required state
↓
Build plan
↓
Execute selected changes
↓
Verify
↓
Return result
```

---

# 8. Optimizer.Core

Contém os conceitos centrais do produto.

Possíveis modelos:

```text
Feature
FeatureState
Recommendation
Change
ChangePlan
Snapshot
OptimizationSession
RiskLevel
ApplyResult
VerificationResult
RollbackResult
FeatureError
```

Também pode conter:

- regras de recomendação;
- classificação;
- contratos;
- validações;
- interfaces.

O Core deve depender o mínimo possível de detalhes específicos de UI ou infraestrutura.

---

# 9. Tipos de feature

Modelo conceitual inicial:

```text
Feature
├── Diagnostic
├── Optimization
├── Repair
├── Configuration
└── Tool
```

Nem todos os tipos precisam implementar as mesmas operações.

Exemplo:

```text
Diagnostic
→ Detect
→ Analyze
```

Enquanto uma Optimization pode usar:

```text
Detect
Plan
Apply
Verify
Rollback
```

---

# 10. Contrato conceitual de Optimization

Exemplo inicial:

```csharp
public interface IOptimizationFeature
{
    string Id { get; }

    Task<FeatureState> DetectAsync();
    Task<Recommendation> AnalyzeAsync();
    Task<ChangePlan> PlanAsync();
    Task<ApplyResult> ApplyAsync(ChangePlan plan);
    Task<VerificationResult> VerifyAsync();
    Task<RollbackResult> RollbackAsync(Snapshot snapshot);
}
```

Esse contrato ainda não é definitivo.

A interface real poderá ser dividida em contratos menores para evitar obrigar features diferentes a implementar operações desnecessárias.

---

# 11. Preferência por contratos menores

Evitar uma interface gigante se diferentes tipos de feature tiverem capacidades distintas.

Possível direção:

```text
IDetectable
IPlannable
IApplicable
IVerifiable
IRollbackable
IRecommendable
```

Assim uma feature pode implementar apenas o que realmente suporta.

Exemplo:

```text
DiskHealthDiagnostic
→ IDetectable
→ IRecommendable
```

Enquanto:

```text
UsbSelectiveSuspendOptimization
→ IDetectable
→ IPlannable
→ IApplicable
→ IVerifiable
→ IRollbackable
```

Essa decisão deverá ser validada durante a implementação inicial.

---

# 12. Optimizer.Windows

Responsável por tudo que conversa diretamente com o Windows.

Possível organização:

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
└── Win32/
```

Exemplos:

```text
WindowsRegistryService
WindowsPowerService
WindowsServiceManager
WindowsDisplayService
WindowsNetworkService
```

Essa camada encapsula detalhes específicos da plataforma.

---

# 13. Abstrações de infraestrutura

Preferir:

```text
IPowerConfiguration
IRegistryService
IServiceManager
IDisplayService
INetworkService
```

em vez de permitir que features chamem diretamente APIs de baixo nível por toda a codebase.

Isso ajuda:

- testes;
- mocks;
- substituição de implementação;
- isolamento;
- manutenção.

---

# 14. Scripts

Scripts podem ser utilizados em:

```text
provas técnicas
testes
operações específicas
```

Mas scripts não devem virar automaticamente a arquitetura definitiva.

Quando um script for mantido no produto final, deve existir uma abstração responsável por:

- execução;
- argumentos;
- validação;
- exit code;
- timeout;
- logging;
- captura de stdout/stderr;
- segurança.

---

# 15. PowerShell

PowerShell poderá ser utilizado quando for a melhor ferramenta.

Entretanto:

- não deve ser chamado diretamente pela UI;
- comandos devem ser controlados;
- inputs devem ser validados;
- resultados devem ser interpretados;
- falhas devem virar erros estruturados.

Evitar construir lógica crítica baseada apenas em parsing frágil de texto quando houver API mais confiável.

---

# 16. P/Invoke e Win32

Quando APIs .NET não forem suficientes, pode ser utilizado:

```text
P/Invoke
COM
Win32 APIs
```

Essas integrações devem ficar isoladas na infraestrutura.

Evitar espalhar código nativo por toda a aplicação.

---

# 17. Application API

O frontend deve enxergar operações de alto nível.

Exemplos:

```text
AnalyzeAsync()
GetRecommendationsAsync()
PreviewAsync()
ApplyAsync()
RollbackAsync()
```

Não operações técnicas como:

```text
SetRegistryValue()
ExecutePowerCfg()
RestartService()
```

---

# 18. Recommendation Engine

Separar recomendação de execução.

Fluxo conceitual:

```text
Analyzer
↓
Recommendation Engine
↓
Change Plan
↓
Executor
```

O Recommendation Engine decide:

```text
should recommend?
why?
confidence?
risk?
```

O Executor não deve decidir se uma mudança é boa para o usuário.

Ele executa um plano já aprovado.

---

# 19. Change

Uma alteração individual poderá ser representada por um modelo estruturado.

Exemplo conceitual:

```text
Change

Id
Description
CurrentValue
TargetValue
Risk
RequiresAdmin
RequiresRestart
Reversible
```

Dependendo da arquitetura final, comportamento de execução poderá ficar:

- no próprio Change;
- em handlers;
- em executors específicos.

Essa decisão ainda está aberta.

---

# 20. ChangePlan

Um ChangePlan representa o conjunto de alterações que seriam executadas.

Exemplo:

```text
ChangePlan

FeatureId
Changes[]
Risk
RequiresAdmin
RequiresRestart
Reversible
Warnings[]
```

Dry-run e execução real devem partir do mesmo plano.

---

# 21. Dry-run na arquitetura

Fluxo:

```text
Detect
↓
Build ChangePlan
↓
Validate
↓
Return
```

Sem:

```text
Apply
```

O frontend poderá utilizar o resultado do dry-run para apresentar preview.

---

# 22. Snapshot

Snapshots devem ser tratados como dados estruturados.

Eles podem conter:

```text
SessionId
FeatureId
Timestamp
OriginalState
Metadata
```

O snapshot deve ser criado antes de alterações reversíveis.

---

# 23. Optimization Session

A aplicação de múltiplas features deve ser agrupada em uma sessão.

Exemplo:

```text
OptimizationSession

SessionId
StartedAt
SelectedFeatures
RestorePointStatus
Snapshots
AppliedChanges
FailedChanges
RestartRequired
Status
```

Possíveis status:

```text
Created
Preparing
Applying
Verifying
Completed
PartiallyCompleted
RollingBack
RolledBack
Failed
```

---

# 24. Persistência

Direção inicial:

```text
JSON
```

Possíveis necessidades:

```text
settings
sessão atual
snapshots temporários
estado de recuperação
```

O restante da aplicação não deve conhecer diretamente os arquivos.

Exemplo:

```text
ISettingsRepository
ISessionRepository
ISnapshotRepository
```

Implementações iniciais:

```text
JsonSettingsRepository
JsonSessionRepository
JsonSnapshotRepository
```

---

# 25. Logs

Logging deve ser centralizado.

Possível abstração:

```text
ILogger
```

Registrar:

- operação;
- feature;
- session id;
- resultado;
- erro;
- estado esperado;
- estado encontrado.

Não utilizar logs como armazenamento principal de estado.

---

# 26. Error model

Erros devem ser estruturados.

Exemplo conceitual:

```text
FeatureError

Code
Message
TechnicalDetails
Recoverable
RequiresRestart
SuggestedAction
```

Códigos possíveis:

```text
ADMIN_REQUIRED
UNSUPPORTED
ACCESS_DENIED
APPLY_FAILED
VERIFY_FAILED
ROLLBACK_FAILED
POLICY_BLOCKED
HARDWARE_UNSUPPORTED
```

A UI deve consumir esse modelo em vez de interpretar mensagens técnicas brutas.

---

# 27. Privilégios administrativos

Decisão ainda aberta.

Preferência conceitual:

não executar toda a aplicação permanentemente como administrador se isso puder ser evitado com complexidade aceitável.

Possibilidades:

```text
Opção A:
App inteiro elevado

Opção B:
Elevação sob demanda

Opção C:
Processo auxiliar privilegiado
```

---

# 28. Opção A — App inteiro elevado

Vantagens:

- implementação mais simples;
- menos IPC;
- menos estados de privilégio.

Desvantagens:

- superfície de risco maior;
- UI inteira elevada;
- integração com outros processos pode ficar diferente;
- permissões excessivas.

Não escolher automaticamente apenas por simplicidade.

---

# 29. Opção B — Elevação sob demanda

A aplicação roda normalmente e solicita elevação quando necessário.

Pode ser adequada se operações privilegiadas forem isoladas.

Desafios:

- UX;
- UAC;
- passagem de contexto;
- continuidade da operação.

---

# 30. Opção C — Processo auxiliar privilegiado

Conceito:

```text
Optimizer.App
normal user
     │
     │ IPC
     ▼
Optimizer.Elevated
administrator
     │
     ▼
Windows
```

Vantagens:

- isolamento;
- UI não precisa rodar elevada;
- superfície privilegiada menor.

Desvantagens:

- IPC;
- autenticação/validação;
- mais complexidade;
- lifecycle;
- recuperação de falhas.

Só adotar se os requisitos justificarem.

---

# 31. Processo único vs múltiplos processos

Direção inicial:

preferir a arquitetura mais simples que satisfaça segurança e requisitos.

Não criar múltiplos processos apenas por parecer mais sofisticado.

A decisão deve ser tomada após entender:

- quantas features exigem admin;
- operações longas;
- reboot;
- monitoramento;
- necessidade de isolamento.

---

# 32. IPC

Somente será necessário se processos separados forem utilizados.

Possibilidades futuras:

```text
Named Pipes
RPC-like abstraction
Local sockets
Other Windows IPC
```

A escolha depende da arquitetura final.

Não definir antes de confirmar que IPC realmente é necessário.

---

# 33. DevTools

Projeto separado:

```text
Optimizer.DevTools
```

Objetivo:

permitir execução sem UI.

Exemplos:

```text
optimizer-dev detect <feature>
optimizer-dev plan <feature>
optimizer-dev test <feature>
```

Deve reutilizar a mesma Application/Core usados pelo app real.

Não deve possuir uma implementação paralela das features.

---

# 34. Mocks

O frontend deve poder trabalhar com implementações simuladas.

Exemplo:

```text
IFeatureProvider
├── MockFeatureProvider
└── RealFeatureProvider
```

Ou abstração equivalente.

Mocks devem respeitar exatamente os contratos reais.

---

# 35. Dependency Injection

Provavelmente será utilizada.

Objetivos:

- desacoplar implementações;
- permitir mocks;
- substituir infraestrutura;
- facilitar testes.

Exemplo conceitual:

```text
IRegistryService
→ WindowsRegistryService

ISnapshotRepository
→ JsonSnapshotRepository
```

A biblioteca/abordagem definitiva será decidida junto com a versão do .NET.

---

# 36. Configuração

Configurações do aplicativo deverão ser separadas de estado temporário.

Exemplo:

```text
App settings
User preferences
Session state
Snapshots
Logs
```

Não colocar tudo em um único arquivo JSON gigante.

---

# 37. Estrutura conceitual de dados

Possível:

```text
/data
├── settings/
│   └── appsettings.json
├── sessions/
│   └── active-session.json
├── snapshots/
│   └── ...
└── logs/
```

A estrutura definitiva poderá mudar.

---

# 38. Reboot recovery

Algumas operações podem exigir reboot.

A arquitetura deve permitir retomar uma sessão.

Exemplo:

```text
Session persisted
↓
Reboot
↓
App starts
↓
Session detected
↓
Resume verification
```

Isso não precisa ser implementado antes de existir uma feature real que exija esse comportamento.

---

# 39. Restore Point Service

A criação de ponto de restauração deve ser encapsulada.

Exemplo:

```text
ISystemRestoreService
```

Possíveis operações:

```text
IsAvailable
CreateRestorePoint
GetStatus
```

O fluxo de otimização poderá utilizar esse serviço antes de alterações relevantes.

---

# 40. Hardware capabilities

A arquitetura deve suportar detecção de capacidades.

Exemplo:

```text
SystemCapabilities

HasBattery
GpuVendor
CpuVendor
SupportsFeatureX
WindowsVersion
Architecture
```

Isso poderá ser utilizado para:

- recomendação;
- visibilidade de features;
- compatibilidade.

---

# 41. Feature Registry / Catalog

Pode existir uma forma central de registrar features disponíveis.

Exemplo conceitual:

```text
FeatureCatalog
```

Responsável por descobrir:

```text
all features
supported features
features by category
features by capability
```

Não confundir isso com o catálogo de discovery em documentação.

Esse seria um conceito runtime.

O runtime poderá distinguir:

```text
Capabilities
Features
Product Areas
```

Conceitualmente:

```text
Capability
→ algo que o sistema sabe fazer ou detectar

Feature
→ comportamento ou função do produto

Product Area
→ contexto de apresentação/uso
```

Exemplo:

```text
Capability:
Detect GPU

Feature:
Show PC Specifications

Product Area:
My PC
```

Outro:

```text
Capability:
Detect GPU

Feature:
Determine available GPU optimizations

Product Area:
Optimization
```

---

# 41.1 Monitoring

A possibilidade de monitoramento/FPS introduz requisitos arquiteturais ainda não avaliados.

Possíveis necessidades futuras:

```text
continuous sampling
performance counters
GPU metrics
FPS / frametime collection
overlay
background process
session recording
time-series data
```

Nenhuma dessas decisões deve ser fechada antes da especificação e prova técnica das features de monitoramento.

Monitoring deve ser considerado uma área capaz de introduzir requisitos diferentes das otimizações tradicionais.

---

# 41.2 Benchmark

Benchmarks também devem ser tratados como capacidades próprias.

Possíveis requisitos:

```text
workload execution
measurement
timing
result normalization
hardware awareness
thermal considerations
result comparison
```

O sistema não deve misturar lógica de benchmark diretamente com UI.

---

# 42. Feature IDs

Cada feature deve possuir ID estável.

Exemplo:

```text
energy.usb-selective-suspend
gaming.game-dvr
system.restore-point
```

O nome visível ao usuário pode mudar sem alterar o ID interno.

Evitar usar texto localizado como identificador.

---

# 43. Versionamento de features

Pode ser útil futuramente registrar versão de implementação.

Exemplo:

```text
FeatureId
ImplementationVersion
```

Isso pode ajudar quando snapshots ou comportamentos mudarem.

Não é obrigatório no início.

---

# 44. Threading e async

Operações que podem bloquear não devem travar a UI.

Preferir APIs assíncronas na Application layer quando apropriado.

Exemplo:

```csharp
Task<ApplyResult> ApplyAsync(...)
```

Isso não significa tornar toda função assíncrona sem necessidade.

---

# 45. Cancelamento

Operações longas devem avaliar suporte a cancelamento.

Exemplo:

```text
CancellationToken
```

Mas cancelar deve ser seguro.

Nunca interromper arbitrariamente uma alteração no meio se isso puder deixar o sistema inconsistente.

Cada operação deve declarar se:

```text
Cancelable
Not Cancelable
Cancelable between steps
```

---

# 46. Progress reporting

Operações compostas podem reportar progresso estruturado.

Exemplo:

```text
OptimizationProgress

SessionId
FeatureId
Step
Completed
Total
Message
```

Isso permitirá ao frontend exibir progresso sem conhecer detalhes da execução.

---

# 47. Concorrência

Inicialmente, evitar aplicar várias alterações sensíveis simultaneamente.

Preferência inicial:

```text
sequential execution
```

principalmente quando features:

- mexem na mesma área;
- possuem dependências;
- precisam de rollback;
- requerem admin.

Paralelismo só deve ser introduzido onde houver benefício e segurança claros.

---

# 48. Dependências entre features

A arquitetura deverá suportar metadados como:

```text
DependsOn
ConflictsWith
RunBefore
RunAfter
```

A implementação definitiva dependerá da fase de dependências e conflitos.

---

# 49. Ordem de execução

Não assumir que a ordem visual é igual à ordem técnica.

Exemplo:

```text
UI order:
Energy
Gaming
Network
```

A execução pode exigir outra ordem internamente.

O Application layer deve coordenar isso.

---

# 50. Frontend criado a partir do Stitch

O HTML/CSS exportado será referência.

Processo:

```text
Stitch
↓
Reference files
↓
Design system extraction
↓
Native implementation
↓
Mock data
↓
Visual validation
↓
Real integration
```

Evitar misturar código gerado pelo Stitch diretamente com lógica do backend.

---

# 51. Estrutura possível da solution

Exemplo inicial:

```text
Optimizer.sln

src/
├── Optimizer.App/
├── Optimizer.Application/
├── Optimizer.Core/
├── Optimizer.Windows/
├── Optimizer.Persistence/
└── Optimizer.DevTools/

tests/
├── Optimizer.Core.Tests/
├── Optimizer.Application.Tests/
└── Optimizer.Windows.Tests/

docs/
prototypes/
```

Não criar projetos separados sem necessidade real.

A divisão deve refletir responsabilidades, não burocracia.

---

# 52. Diretório de prototypes

Provas técnicas deverão ficar separadas da implementação final.

Exemplo:

```text
/prototypes
├── energy/
├── display/
├── network/
└── ...
```

Código de prototype pode ser descartado, adaptado ou migrado.

Não assumir que prototype = production code.

---

# 53. Arquitetura de testes

Testes de Core devem preferir mocks.

Testes de Windows podem utilizar:

- ambiente real;
- wrappers;
- fixtures;
- VM.

Separar claramente:

```text
Unit
Integration
System
E2E
```

---

# 54. Dependência entre projetos

Direção desejada:

```text
Optimizer.App
    ↓
Optimizer.Application
    ↓
Optimizer.Core
```

Infrastructure implementa contratos consumidos por Application/Core.

Evitar:

```text
Core
↓
App
```

ou dependências circulares.

---

# 55. Possível direção de dependências

Conceitualmente:

```text
            App
             ↓
        Application
             ↓
            Core
             ↑
     ┌───────┴────────┐
     │                │
 Windows        Persistence
```

A composição concreta ocorre no ponto de inicialização da aplicação.

---

# 56. Composition Root

A aplicação deverá possuir um ponto central onde implementações são conectadas.

Exemplo:

```text
IRegistryService
→ WindowsRegistryService

ISnapshotRepository
→ JsonSnapshotRepository
```

Isso evita criação arbitrária de dependências dentro das features.

---

# 57. O que evitar

Evitar:

```text
God classes
static globals
UI chamando Registry
scripts espalhados
duplicação de lógica
parsers copiados
features diretamente acopladas a XAML
um único arquivo de backend gigantesco
exceções técnicas chegando diretamente ao usuário
```

---

# 58. Regra de simplicidade

Arquitetura deve resolver problemas reais.

Não adicionar:

- CQRS;
- event sourcing;
- microservices;
- banco complexo;
- message broker;
- múltiplos processos;
- plugin system sofisticado;

sem uma necessidade concreta.

Esse é um aplicativo local para Windows.

Complexidade arquitetural também possui custo.

---

# 59. Arquitetura evolutiva

A arquitetura pode mudar.

A descoberta global do produto poderá revelar novos módulos de infraestrutura.

Exemplo:

```text
Optimizer.Monitoring
Optimizer.Benchmark
```

Esses projetos só devem ser criados se houver complexidade suficiente para justificar separação.

Inicialmente, preferir organizar responsabilidades dentro dos projetos existentes até existir motivo real para novos assemblies.

Mudanças relevantes devem ser registradas em:

```text
06-DECISIONS.md
```

Quando necessário:

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

## Decisão arquitetural provisória

A posição de uma feature na UI não deve determinar onde sua implementação vive.

Exemplo:

Uma detecção de GPU exibida em `My PC` não deve necessariamente ficar em um namespace ou serviço exclusivo da tela `My PC`.

A implementação deve refletir responsabilidade técnica e reutilização.

---

# 60. Pontos ainda não definidos

Ainda precisam ser decididos:

```text
Versão do .NET
Versão mínima do Windows
Suporte a Windows 10
Modelo de elevação
Processo único ou auxiliar elevado
IPC
Logging framework
Packaging
Updater
Persistência definitiva
Recovery após reboot
Estrutura final de interfaces de feature
Design system
```

Essas decisões devem ser tomadas com evidência.

---

# 61. Critério para congelar arquitetura V1 da V2

Antes de iniciar implementação ampla, deve existir clareza sobre:

```text
[ ] stack
[ ] projetos da solution
[ ] dependências
[ ] feature contracts
[ ] change plan
[ ] snapshot
[ ] error model
[ ] logging
[ ] persistência
[ ] admin/elevation
[ ] testes
[ ] mocks
[ ] devtools
```

Não significa que nunca mais poderá mudar.

Significa que a implementação pode começar com uma base coerente.

---

# 62. Princípio final

A arquitetura deve permitir que uma feature seja desenvolvida e testada sem depender da interface.

Também deve permitir que a interface seja construída sem depender de features reais prontas.

O objetivo é conseguir:

```text
Backend
funciona sozinho
```

e:

```text
Frontend
funciona com mocks
```

para então conectar ambos através de contratos estáveis.

Se alterar uma tela exigir reescrever uma otimização, ou alterar uma otimização exigir redesenhar a tela, a separação arquitetural provavelmente está falhando.
