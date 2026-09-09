# BeadWise / PC Optimizer V2 — Vertical Slice Implementation Plan

**Documento:** `04A-VERTICAL-SLICE-IMPLEMENTATION-PLAN.md`  
**Fase:** Vertical Slice / Implementation Planning  
**Status:** PROPOSED — READY FOR IMPLEMENTATION GATE  
**Data:** 2026-09-09

---

# 1. Objetivo deste documento

Este documento define o primeiro **Vertical Slice representativo** do BeadWise V2.

O objetivo desta fase **não é implementar o conjunto completo de 234 Feature Specs** e também não é iniciar a UI final.

O objetivo é implementar o menor conjunto possível de comportamento real e infraestrutura capaz de validar, de ponta a ponta, os fundamentos definidos em `03.5-ARCHITECTURE-FINAL.md`.

O slice selecionado deve permitir provar na prática:

- estrutura da solution;
- direção das dependências;
- separação entre Feature, Capability e Operation;
- Application orchestration;
- Windows adapters;
- Observation estruturada;
- ChangePlan;
- Dry-run / Preview;
- Snapshot;
- Apply;
- Verify;
- Recovery controlado;
- structured errors;
- logging correlacionável;
- persistência de sessão;
- privilege boundary;
- mocks/fakes;
- backend testável sem UI;
- capacidade de integração posterior com WinUI 3.

---

# 2. Autoridades deste plano

Ordem de autoridade:

1. `03.5-ARCHITECTURE-FINAL.md`
2. Feature Specs individuais
3. `PHASE-2-PROOF-MATRIX.md`
4. `PHASE-2-RETURN-PACKAGE.md`
5. prototypes da Fase 2 como evidência prática

Regras:

- prototypes não devem ser copiados diretamente para produção;
- evidência de prototype não transforma comportamento não suportado em contrato de produção;
- uma Feature `RESEARCH`, `BLOCKED` ou `DEFERRED` não deve forçar implementação prematura;
- decisões `OPEN` da arquitetura continuam `OPEN` até existir evidência suficiente.

---

# 3. Vertical Slice selecionado

## 3.1 Nome conceitual

**Process Observation + Controlled Process Mutation/Recovery**

## 3.2 Feature Specs utilizadas

### C-PROCESSES-001 — Process inventory, tree & app grouping

Papel no slice:

- capability read-only real;
- descoberta de processos;
- identidade estável;
- provenance;
- Observation;
- dados reutilizáveis por mais de um consumidor;
- degradação parcial quando determinada informação não puder ser lida.

### C-PROCESSES-007 — Process action workflow & safeguards

Papel no slice:

- safeguards;
- processo-alvo com identidade estável;
- ChangePlan;
- dry-run;
- operação controlada;
- Snapshot para contexto/auditoria;
- Apply;
- Verify;
- erros estruturados;
- privilege requirement;
- fluxo de recuperação da fixture.

**Importante:** o primeiro slice usa apenas uma **fatia controlada** de C-PROCESSES-007. Ele não implementa todo o workflow de ações de processo do produto final.

---

# 4. Justificativa da escolha

C-PROCESSES-007 é um candidato especialmente forte porque a campanha de provas técnicas registrou:

```text
DETECT           PASS
PLAN             PASS
DRY-RUN          PASS
APPLY            PASS
VERIFY           PASS
SNAPSHOT         PASS
ROLLBACK         PASS
VERIFY_ROLLBACK  PASS
SAFE_PROOF       PASS
```

Entretanto, o contrato final da Feature Spec esclarece que:

- terminar um processo real não é reversível;
- iniciar outro processo não restaura o estado em memória do processo anterior;
- restart é uma ação futura/controlada, não rollback universal;
- Restart Manager só pode ser usado quando o próprio contrato da API declarar aplicabilidade/restartability;
- processos críticos/protegidos/restritos devem ser bloqueados, não contornados.

O prototype da Fase 2 utilizou um processo filho controlado exclusivamente pelo harness. Portanto, neste primeiro slice, `Recovery` significa **restaurar a fixture controlada ao estado esperado de teste**, não prometer rollback de processos arbitrários do usuário.

---

# 5. Candidatos rejeitados para o primeiro slice

## 5.1 Energy / Power Plan

Vantagens:

- excelente para ChangePlan;
- excelente reversibilidade;
- verify simples;
- baixo custo conceitual.

Motivo para não selecionar agora:

- capabilities centrais relevantes ainda possuem estado `RESEARCH`/`BLOCKED`;
- iniciar por esse domínio poderia congelar decisões de produto ainda não maduras.

Status:

**POSTPONED — forte candidato a um slice posterior.**

## 5.2 Startup

Vantagens:

- diagnóstico;
- alteração;
- snapshot;
- rollback;
- alto valor de produto.

Motivo para não selecionar agora:

- múltiplos mechanisms/owners;
- Registry;
- startup folders;
- scheduled tasks;
- services;
- identidade de aplicativos;
- policies;
- parte do controle ainda não possui maturidade suficiente.

Status:

**POSTPONED.**

## 5.3 Cleaning

Vantagens:

- forte evidência de prototype;
- plan/dry-run/apply/verify bem demonstráveis.

Motivo:

- remoção de arquivos é destrutiva;
- não valida rollback real;
- aumenta o risco do primeiro slice sem agregar cobertura arquitetural proporcional.

Status:

**NOT SELECTED FOR FIRST SLICE.**

## 5.4 Display mode

Vantagens:

- detecção nativa;
- dry-run/validation;
- verify.

Motivo:

- Apply real em display adiciona risco operacional;
- rollback/recovery pode envolver UX e estado do monitor;
- não é necessário para validar a arquitetura inicial.

Status:

**POSTPONED.**

---

# 6. Escopo do slice

## 6.1 IN SCOPE

O slice deve implementar:

1. solution base;
2. projetos canônicos;
3. processo discovery real;
4. identidade estável do processo;
5. Observation normalizada;
6. provenance/source;
7. shared output utilizado por pelo menos dois consumidores;
8. modelo mínimo de Operation;
9. modelo mínimo de ChangePlan;
10. dry-run sem mutação;
11. processo controlado criado pelo test harness;
12. Snapshot do contexto da operação;
13. persistência de Session;
14. Apply sobre a fixture controlada;
15. Verify por reobservação;
16. Recovery da fixture;
17. Verify Recovery;
18. structured errors;
19. structured logging;
20. privilege boundary contratual;
21. mocks/fakes;
22. testes de Core;
23. testes de Application;
24. testes Windows;
25. execução integral do backend sem UI.

## 6.2 OUT OF SCOPE

O primeiro slice não deve implementar:

- controle arbitrário de processos do usuário;
- generic kill button;
- restart genérico por command line;
- promessa de recuperar estado em memória;
- `NtSuspendProcess`;
- `NtResumeProcess`;
- termination de processo crítico;
- bypass de protected process;
- Restart Manager completo;
- Registry mutation;
- power plan;
- startup management;
- System Restore;
- reboot real;
- resume pós-reboot;
- logoff;
- service restart;
- device restart;
- monitoring contínuo;
- ETW pipeline completo;
- benchmark;
- vendor APIs;
- destructive cleanup real;
- SQLite definitivo;
- packaging;
- updater;
- IPC definitivo;
- helper privilegiado definitivo;
- UI final.

Esses itens permanecem explicitamente **não validados** pelo primeiro slice.

---

# 7. Estrutura inicial da solution

Estrutura obrigatória:

```text
BeadWise.sln

src/
├── BeadWise.App/
├── BeadWise.Application/
├── BeadWise.Core/
└── BeadWise.Windows/

tests/
├── BeadWise.Core.Tests/
├── BeadWise.Application.Tests/
└── BeadWise.Windows.Tests/
```

Não criar inicialmente:

```text
BeadWise.Processes
BeadWise.Persistence
BeadWise.Diagnostics
BeadWise.Monitoring
BeadWise.Benchmark
BeadWise.DevTools
```

Assemblies adicionais só podem surgir quando houver complexidade real comprovada.

---

# 8. Regras de dependência

Direção:

```text
BeadWise.App
      ↓
BeadWise.Application
      ↓
BeadWise.Core
      ↑
BeadWise.Windows
```

Regras obrigatórias:

- `Core` não referencia `App`;
- `Core` não referencia `Windows`;
- `Application` não chama Win32 diretamente;
- `Application` coordena contratos;
- `Windows` implementa contratos de plataforma;
- `App` não contém lógica Windows;
- nenhuma dependência circular é permitida;
- concrete wiring ocorre no Composition Root.

---

# 9. Organização lógica inicial

## 9.1 BeadWise.Core

Organização inicial sugerida:

```text
Core/
├── Observations/
├── Operations/
├── Planning/
├── Sessions/
├── Recovery/
├── Errors/
└── Contracts/
```

Criar somente pastas que receberem tipos reais.

## 9.2 BeadWise.Application

```text
Application/
├── Analysis/
├── Planning/
├── Execution/
├── Sessions/
└── Recovery/
```

## 9.3 BeadWise.Windows

```text
Windows/
└── Processes/
```

Nenhuma subdivisão adicional deve ser criada antecipadamente.

---

# 10. Capability read-only

## 10.1 Capability conceitual

Nome de trabalho:

```text
ProcessInventory
```

Responsabilidade:

- enumerar processos;
- obter identidade;
- obter dados disponíveis com segurança;
- normalizar;
- anexar provenance;
- retornar `Partial`, `Unknown`, `Unsupported` ou erro quando necessário.

## 10.2 Identidade de processo

Nunca considerar somente:

```text
PID
```

como identidade suficiente.

Identidade mínima do slice:

```text
ProcessIdentity
├── ProcessId
└── CreationTime
```

Quando disponível, enriquecer com:

```text
ExecutablePath
SessionId
UserContext
ProcessName
```

PID + CreationTime deve ser revalidado antes de qualquer ação.

---

# 11. Observation

Modelo mínimo conceitual:

```text
Observation<T>
├── Value
├── Source
├── Timestamp
├── Availability
├── Quality / Confidence
└── Error
```

Estados mínimos:

```text
Available
Partial
Unsupported
Unknown
Error
```

O slice deve provar que:

- ausência de informação não vira valor presumido;
- `AccessDenied` pode produzir dado parcial em vez de falhar toda a observação;
- provenance permanece associado ao dado.

---

# 12. Dois consumidores da mesma informação

O output de ProcessInventory deve ser consumido por pelo menos dois fluxos:

```text
ProcessInventory
├── Process Diagnostics
└── Controlled Process Action
```

O objetivo é validar que uma capability de baixo nível não precisa ser duplicada por Product Area.

Não criar:

```text
DiagnosticsProcessDetector
OptimizationProcessDetector
ActionProcessDetector
```

quando a informação de base for a mesma.

---

# 13. Operation

Modelo mínimo:

```text
Operation
├── OperationId
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

Primeira operação concreta:

```text
ControlledProcessRestartOperation
```

ou nome equivalente.

Ela deve funcionar **somente** sobre processo criado/possuído pelo harness de teste.

Não generalizar a operação para processos arbitrários.

---

# 14. ChangePlan

Modelo mínimo:

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

Feature IDs associados:

```text
C-PROCESSES-001
C-PROCESSES-007
```

## Regra crítica

```text
Preview
+
Apply
```

devem usar o **mesmo plano lógico aprovado**.

Apply não pode reconstruir silenciosamente um plano diferente.

Cada plano precisa possuir identidade própria e ser persistível/correlacionável.

---

# 15. Dry-run

Fluxo:

```text
Observe
↓
Validate prerequisites
↓
Build ChangePlan
↓
Evaluate privilege/restart/risk
↓
Persist/return preview
```

Dry-run deve provar:

- nenhuma mutação persistente;
- nenhum processo finalizado;
- nenhum processo reiniciado;
- nenhuma fixture modificada;
- mesmo `PlanId`/conteúdo lógico utilizável posteriormente por Apply.

Teste obrigatório:

```text
Fingerprint Before
↓
Dry-run
↓
Fingerprint After

Before == After
```

---

# 16. Fixture de processo controlado

Criar um processo pertencente exclusivamente ao test harness.

Requisitos:

- executável/comando controlado pelo teste;
- marker único por execução;
- PID capturado;
- CreationTime capturado;
- nenhum processo externo selecionável;
- cleanup obrigatório em `finally`;
- nenhuma ação sobre processos do usuário.

Exemplo conceitual:

```text
BeadWise Test Process
Marker = <GUID>
```

O marker deve impedir ambiguidade com processos externos.

---

# 17. Snapshot

C-PROCESSES-007 exige Snapshot para contexto/auditoria, mas Snapshot **não torna termination reversível**.

Modelo:

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

Snapshot do slice deve preservar, quando disponível:

```text
PID
CreationTime
Executable
Session
User context
Critical state
Restricted/access state
Action selected
Fixture marker
```

Para futuras operações em outros domínios, o modelo deve continuar capaz de distinguir:

```text
ValueExistedBefore = false
```

de:

```text
ValueExistedBefore = true
Value = default/zero/empty
```

Não criar modelo específico de Snapshot exclusivo para processos se o conceito puder permanecer genérico.

---

# 18. Session

Usar:

```text
Session
```

e não:

```text
OptimizationSession
```

Modelo mínimo:

```text
Session
├── SessionId
├── State
├── CreatedAt
├── UpdatedAt
├── PlanId
├── Operations
├── Snapshots
├── Results
├── Failure
└── RecoveryState
```

Estados iniciais sugeridos:

```text
Created
Planned
Approved
Applying
Verifying
Completed
Failed
Recovering
Recovered
```

A lista pode ser refinada durante implementação se testes demonstrarem necessidade.

Não implementar ainda uma state machine excessivamente genérica.

---

# 19. Persistência de Session

Objetivo:

validar **Operational State**, não definir o storage permanente de todo o BeadWise.

Primeira implementação recomendada:

```text
JSON
```

Local conceitual:

```text
%LocalAppData%\BeadWise\
└── operational\
    └── sessions\
        └── <SessionId>.json
```

Motivos:

- simples;
- auditável;
- testável;
- fácil de isolar;
- fácil de destruir em fixture;
- suficiente para provar Session persistence.

Essa decisão **não** define que Settings, History ou Monitoring Data usarão JSON.

SQLite permanece candidato futuro.

Logging não pode substituir Session persistence.

---

# 20. Apply

Fluxo obrigatório:

```text
Receive approved ChangePlan
↓
Validate Plan identity/state
↓
Revalidate process identity
↓
Validate safeguards
↓
Capture Snapshot
↓
Persist Session
↓
Execute controlled Operation
↓
Persist operation result
↓
Verify
```

Se PID/CreationTime não corresponder mais ao processo original:

```text
STALE_PLAN / TARGET_CHANGED
```

e Apply deve parar.

Não selecionar novo processo automaticamente.

---

# 21. Verify

Sucesso de chamada de API não é suficiente.

Verify deve:

```text
Execute authoritative observation again
↓
Compare observed state
with expected post-condition
```

Para a fixture:

### Antes

```text
ProcessIdentity A
Running = true
```

### Depois de termination/restart controlado

```text
ProcessIdentity A
Running = false
```

e, quando o workflow do harness exigir substituição:

```text
ProcessIdentity B
Running = true
Marker = expected marker
```

O novo processo não deve ser chamado de “mesmo processo restaurado”.

Ele é uma **nova instância controlada**.

---

# 22. Recovery

## 22.1 Semântica correta

Neste slice:

```text
Recovery
```

significa:

```text
restore test fixture to the expected controlled state
```

Não significa:

```text
restore arbitrary terminated Windows process
```

C-PROCESSES-007 estabelece explicitamente que termination não é reversível.

## 22.2 Fluxo

```text
Failure or test recovery request
↓
Cleanup current controlled instance
↓
Recreate/restore fixture if required
↓
Observe again
↓
Compare with fixture recovery contract
```

## 22.3 Verify Recovery

PASS somente quando a fixture estiver novamente no estado esperado definido pelo harness.

Não usar `ROLLBACK` como termo para uma promessa que a Feature Spec não suporta.

Internamente, a arquitetura genérica continua possuindo Recovery; a operação concreta deve declarar sua reversibilidade corretamente.

---

# 23. Safeguards obrigatórios

Antes de uma ação sobre processo:

1. revalidar PID + CreationTime;
2. confirmar que o processo pertence à fixture;
3. confirmar marker;
4. bloquear alvo fora do ownership do harness;
5. avaliar critical/restricted state quando aplicável;
6. nunca tentar contornar access controls;
7. nunca agir somente pelo executable name;
8. nunca executar sobre processo crítico;
9. nunca transformar AccessDenied em tentativa de bypass;
10. exigir operação explicitamente aprovada pelo plano.

---

# 24. Privilege Boundary

A arquitetura exige uma fronteira explícita para operações privilegiadas.

Contrato conceitual mínimo:

```text
IPrivilegeExecutor
```

ou equivalente.

O mecanismo final permanece `OPEN`.

O primeiro slice **não deve decidir prematuramente** entre:

- elevação pontual;
- helper process;
- IPC;
- Named Pipes;
- outro mecanismo.

## 24.1 Validação no slice

Criar dois cenários de teste:

```text
Operation A
PrivilegeRequirement = None
```

e:

```text
Operation B
PrivilegeRequirement = Administrator
```

Application deve:

```text
detect privilege requirement
↓
route through privilege boundary
```

Nos testes unitários:

```text
FakePrivilegeExecutor
```

Nos testes Windows não elevados, quando apropriado:

```text
AdminRequired
```

deve ser resultado estruturado.

---

# 25. Contratos mínimos

Criar apenas contratos que possuam consumidor real.

Conjunto inicial provável:

```text
IProcessObservationSource
ISessionStore
IPrivilegeExecutor
```

Um contrato de executor pode existir se a composição concreta demonstrar necessidade:

```text
IOperationExecutor
```

Mas não criar interfaces apenas por simetria.

Evitar:

```text
IProcessFeature
IProcessFeatureService
IProcessFeatureHandler
IProcessFeatureManager
IProcessFeatureExecutor
```

como reflexo automático do Feature ID.

---

# 26. Windows adapters

Primeiros adapters necessários:

## 26.1 Process enumeration / identity

Responsável por:

- enumerar;
- obter PID;
- obter CreationTime;
- tentar obter executable path;
- session/user context quando requerido;
- representar acesso parcial.

Possíveis mecanismos:

- .NET Process APIs;
- Win32 process APIs;
- `OpenProcess`;
- `QueryFullProcessImageName`;
- `GetProcessTimes`.

A escolha específica deve favorecer API estruturada/documentada.

## 26.2 Safeguard adapter

Responsável por:

- critical-state quando suportado;
- minimum required rights;
- access restriction;
- target revalidation.

Não implementar Restart Manager completo no primeiro slice a menos que seja indispensável para a fixture.

---

# 27. Structured Errors

Modelo de erro deve ser independente de exceções técnicas.

Tipos inicialmente necessários:

```text
AdminRequired
AccessDenied
Unsupported
Unavailable
Blocked
Timeout
Cancelled
ApplyFailed
VerifyFailed
RecoveryFailed
PartialFailure
TargetChanged
InvalidPlan
```

Mapear Win32/.NET exceptions para erros estruturados.

Nunca entregar diretamente ao consumidor principal:

```text
Win32Exception
HRESULT
raw native error
```

Detalhes técnicos podem permanecer em logging/audit.

---

# 28. Result model

Operações e orchestration devem retornar Result estruturado.

Exemplo conceitual:

```text
Result<T>
├── Status
├── Value
├── Error
├── Warnings
└── Metadata
```

Estados mínimos:

```text
Success
Partial
Failed
Blocked
Unsupported
Cancelled
```

`Partial` nunca pode ser promovido silenciosamente a `Success`.

---

# 29. Logging

Logging precisa ser estruturado e correlacionável por:

```text
SessionId
PlanId
FeatureId
OperationId
```

Eventos mínimos:

```text
ObservationStarted
ObservationCompleted
PlanBuilt
DryRunCompleted
SessionPersisted
SnapshotCaptured
OperationStarted
OperationCompleted
VerifyStarted
VerifyCompleted
RecoveryStarted
RecoveryCompleted
Failure
```

Requisitos:

- timestamps;
- severity;
- error category;
- privacy/redaction;
- nenhuma dependência de logging como fonte de Operational State.

---

# 30. Mocks / Fakes

Criar equivalentes testáveis dos contratos utilizados.

Prováveis:

```text
FakeProcessObservationSource
FakeSessionStore
FakePrivilegeExecutor
FakeOperationExecutor
```

Objetivo:

provar que `BeadWise.Application` pode executar o fluxo completo sem Windows real e sem UI.

---

# 31. Backend sem UI

Antes de qualquer integração WinUI, o slice deve poder executar:

```text
Observe
↓
Build Plan
↓
Dry-run
↓
Approve in test harness
↓
Apply
↓
Verify
↓
Recover fixture
↓
Verify Recovery
```

por:

- testes;
- integration harness;
- runner mínimo de desenvolvimento, se necessário.

`BeadWise.App` não é requisito para o PASS do primeiro slice.

O projeto pode existir na solution, mas não deve ser usado para esconder dependência indevida.

---

# 32. Estratégia de testes

## 32.1 BeadWise.Core.Tests

Validar:

- ProcessIdentity equality semantics;
- Observation states;
- Result states;
- Operation invariants;
- ChangePlan invariants;
- Plan identity;
- Snapshot semantics;
- Session transitions;
- structured errors;
- RestartRequirement representation;
- Reversibility representation.

## 32.2 BeadWise.Application.Tests

Usando apenas mocks/fakes:

- observation orchestration;
- plan construction;
- dry-run;
- same-plan enforcement;
- stale plan rejection;
- snapshot orchestration;
- session persistence;
- Apply orchestration;
- verify;
- failure propagation;
- recovery orchestration;
- privilege routing;
- Partial result handling.

## 32.3 BeadWise.Windows.Tests

Em ambiente controlado:

- process enumeration;
- ProcessIdentity;
- executable path quando disponível;
- restricted/partial behavior;
- fixture creation;
- fixture ownership marker;
- apply sobre fixture;
- original PID desaparece;
- nova instância esperada aparece quando aplicável;
- cleanup;
- nenhum processo externo afetado.

## 32.4 Testes negativos obrigatórios

Testar explicitamente:

```text
PID reused / CreationTime changed
Target already exited
Wrong marker
Non-owned process
AccessDenied
AdminRequired
Plan changed
Verify mismatch
Recovery failure
Session persistence failure
```

---

# 33. Fingerprint de segurança

Antes e depois de dry-run, gerar fingerprint mínimo da fixture.

Pode considerar:

```text
Owned PIDs
CreationTimes
Markers
Expected fixture files/state
```

Critério:

```text
Dry-run fingerprint before == after
```

Qualquer mutação durante dry-run:

**FAIL imediato do slice.**

---

# 34. Ordem de implementação

A implementação deve ser incremental.

## Etapa 1 — Solution skeleton

Criar:

```text
BeadWise.App
BeadWise.Application
BeadWise.Core
BeadWise.Windows
BeadWise.Core.Tests
BeadWise.Application.Tests
BeadWise.Windows.Tests
```

Gate:

- compila;
- dependências corretas;
- testes vazios/base executam.

## Etapa 2 — Core primitives

Implementar somente o necessário para:

- Result;
- structured Error;
- Observation;
- ProcessIdentity;
- Operation;
- ChangePlan;
- Snapshot;
- Session;
- requirement enums/value objects.

Gate:

- Core.Tests PASS.

## Etapa 3 — Process read-only adapter

Implementar `IProcessObservationSource`.

Gate:

- enumeração real;
- identidade;
- provenance;
- partial data;
- Windows.Tests PASS.

## Etapa 4 — Application observation flow

Criar orchestration read-only.

Gate:

- Application funciona com FakeProcessObservationSource;
- Application não referencia Windows.

## Etapa 5 — ChangePlan + Dry-run

Criar operação da fixture e construção do plano.

Gate:

- dry-run zero mutation;
- plan persisted/returned;
- Apply ainda não necessário.

## Etapa 6 — Session persistence

Implementar `ISessionStore` inicial em JSON.

Gate:

- round-trip;
- failure handling;
- corrupt/partial file test;
- logging não usado como estado.

## Etapa 7 — Controlled fixture

Criar harness de processo próprio.

Gate:

- marker;
- ownership;
- deterministic cleanup.

## Etapa 8 — Apply

Executar operação somente sobre fixture.

Gate:

- plan identity validada;
- process identity revalidada;
- safeguards;
- operation result estruturado.

## Etapa 9 — Verify

Reobservar por fonte real.

Gate:

- sucesso somente por estado observado;
- retorno de API sozinho não basta.

## Etapa 10 — Recovery fixture

Restaurar estado de teste.

Gate:

- fixture volta ao estado esperado;
- nenhuma alegação de rollback de processo arbitrário.

## Etapa 11 — Privilege boundary

Introduzir contrato e cenários.

Gate:

- Application não executa UAC diretamente;
- admin requirement roteado pelo boundary;
- fake funciona em testes.

## Etapa 12 — Full slice test

Executar pipeline completo.

Gate:

- todos os critérios PASS abaixo.

---

# 35. Critérios de PASS do Vertical Slice

O slice só pode ser considerado `PASS` se:

```text
[PASS] solution usa a estrutura canônica
[PASS] dependências respeitam direção arquitetural
[PASS] Core não depende de Windows
[PASS] Application não contém chamadas Win32
[PASS] processo real pode ser observado por adapter
[PASS] ProcessIdentity usa PID + CreationTime
[PASS] Observation possui provenance
[PASS] Partial/Unknown/Unsupported são preservados
[PASS] output de processo possui >= 2 consumidores
[PASS] Operation contém metadata arquitetural mínima
[PASS] ChangePlan é criado
[PASS] Dry-run não altera estado
[PASS] Preview e Apply usam o mesmo plano lógico
[PASS] stale/changed target invalida o plano
[PASS] Snapshot é capturado
[PASS] Snapshot não implica falsa reversibilidade
[PASS] Session é persistida fora do logging
[PASS] Apply atua somente sobre fixture controlada
[PASS] Verify reobserva estado real
[PASS] Recovery restaura fixture
[PASS] Verify Recovery confirma fixture
[PASS] nenhuma ação toca processo externo
[PASS] structured errors chegam à Application
[PASS] privilege requirement cruza boundary
[PASS] Application funciona integralmente com mocks
[PASS] backend executa sem UI
[PASS] logs correlacionam Session/Plan/Feature/Operation
[PASS] testes negativos principais passam
```

---

# 36. Critérios de FAIL

O slice deve ser considerado `FAIL` arquitetural se ocorrer qualquer um:

- necessidade de chamada Win32 dentro de Application;
- Core precisar referenciar Windows;
- Apply recalcular silenciosamente outro plano;
- dry-run causar mutação;
- target ser escolhido apenas por executable name;
- PID ser tratado como identidade suficiente;
- processo fora da fixture puder ser afetado;
- access control ser contornado;
- termination ser apresentado como reversível;
- nova instância ser tratada como estado em memória restaurado;
- success ser aceito apenas por exit code;
- logging virar banco de estado;
- exception nativa escapar como modelo principal de erro;
- testes de Application exigirem Windows real;
- privilege flow exigir que o aplicativo inteiro rode elevado;
- novo assembly ser criado sem necessidade real.

---

# 37. O que este slice valida

Ao fechar em PASS, teremos evidência prática para:

```text
✓ solution/project structure
✓ dependency direction
✓ Core boundaries
✓ Application orchestration
✓ Windows adapter boundary
✓ Observation
✓ provenance
✓ shared data reuse
✓ Operation composition
✓ ChangePlan
✓ same-plan Preview/Apply
✓ Dry-run
✓ Snapshot
✓ Session
✓ persistence de Operational State
✓ Apply
✓ Verify
✓ controlled Recovery
✓ structured errors
✓ logging
✓ privilege boundary contract
✓ mocks
✓ backend sem UI
```

---

# 38. O que continuará NÃO validado

Depois do primeiro slice, continuarão sem validação prática completa:

```text
reboot real
resume pós-reboot
logoff
service restart
device restart
Registry mutation
System Restore
Restart Manager completo
protected-process matrix
GUI unsaved-state workflows
UWP/package activation
multiple resource owners
monitoring pipeline
ETW continuous telemetry
benchmark pipeline
before/after performance measurement
vendor APIs
SQLite/history definitivo
packaging
updater
IPC definitivo
helper privilegiado definitivo
Windows 10 support final
ARM64 completo
WinUI final
```

Isso deve constar no retorno da fase.

---

# 39. Riscos do slice

## 39.1 Falsa reversibilidade

Risco:

interpretar replacement process como rollback.

Mitigação:

usar terminologia de `Recovery fixture` e respeitar a Feature Spec final.

## 39.2 Overengineering

Risco:

criar abstrações para o produto inteiro antes de existir consumidor.

Mitigação:

contratos mínimos e tipos apenas quando usados.

## 39.3 Harness virar produção

Risco:

copiar lógica de prototype ou fixture para comportamento real.

Mitigação:

separar explicitamente test infrastructure de Windows production adapters.

## 39.4 Processo errado

Risco:

PID reuse ou seleção por nome.

Mitigação:

PID + CreationTime + marker + ownership.

## 39.5 Admin contaminar arquitetura

Risco:

resolver testes fazendo a aplicação inteira executar elevated.

Mitigação:

Privilege Boundary obrigatório.

## 39.6 Persistência prematura

Risco:

transformar JSON em decisão arquitetural universal.

Mitigação:

declarar JSON somente como storage inicial de Operational State.

---

# 40. Decisões que permanecem OPEN

O primeiro slice não deve congelar:

- versão exata do .NET;
- versão mínima final do Windows;
- suporte final a Windows 10;
- suporte ARM64 completo;
- packaging;
- updater;
- IPC;
- helper privilegiado;
- Named Pipes;
- storage definitivo por categoria;
- SQLite;
- retention de monitoring;
- assemblies adicionais;
- UI final;
- Restart Manager completo;
- interfaces futuras que ainda não possuem consumidor.

---

# 41. Artefatos esperados da implementação

Quando o slice for implementado, produzir:

1. código da solution;
2. testes;
3. fixture controlada;
4. resultados do full-slice test;
5. logs de uma sessão de prova;
6. exemplo de Session persistida;
7. exemplo de ChangePlan;
8. exemplo de Snapshot;
9. matriz PASS/FAIL;
10. relatório das lacunas não validadas;
11. eventual lista de ajustes arquiteturais descobertos.

---

# 42. Regra para alterações arquiteturais durante implementação

Se a implementação real revelar incompatibilidade com `03.5-ARCHITECTURE-FINAL.md`:

1. não criar workaround silencioso;
2. registrar o conflito;
3. produzir evidência;
4. distinguir:
   - implementação incorreta;
   - spec incompatível;
   - arquitetura insuficiente;
5. somente então propor alteração arquitetural.

O slice existe também para testar a arquitetura.

Falha bem explicada pode ser evidência útil.

---

# 43. Implementation Gate

Antes de escrever código de produção, confirmar:

```text
[GO] vertical slice selecionado
[GO] Feature Specs selecionadas
[GO] sem Feature RESEARCH/BLOCKED obrigatória
[GO] escopo controlado
[GO] sem processo arbitrário do usuário
[GO] sem falsa promessa de rollback
[GO] projetos definidos
[GO] contratos mínimos definidos
[GO] models iniciais definidos
[GO] dry-run definido
[GO] snapshot definido
[GO] session definida
[GO] persistence inicial definida
[GO] Apply definido
[GO] Verify definido
[GO] Recovery definido
[GO] privilege boundary definido como contrato
[GO] mocks definidos
[GO] testes definidos
[GO] PASS/FAIL definidos
[GO] lacunas explicitamente registradas
```

Quando este documento for aceito como canônico para a implementação do slice:

```text
VERTICAL SLICE PLANNING: CLOSED
IMPLEMENTATION GATE: GO
```

---

# 44. Resultado esperado

Ao final da implementação deste primeiro vertical slice, o BeadWise ainda não será um produto completo e não terá as 234 features implementadas.

O resultado esperado é mais importante:

> uma pequena parte real do BeadWise funcionando de ponta a ponta sobre a arquitetura definitiva, demonstrando que os principais contratos, limites, fluxos de mutação, persistência, verificação, segurança e testabilidade funcionam antes de escalar a implementação.

Se o slice passar, a arquitetura deixa de ser apenas documental e passa a possuir evidência prática de implementação.

---

# 45. Próxima decisão após o PASS

Somente depois do primeiro slice passar:

1. revisar descobertas arquiteturais;
2. corrigir documentação se necessário;
3. decidir o próximo slice;
4. escolher progressivamente domínios que cubram lacunas ainda não testadas.

Candidatos naturais posteriores:

```text
reversible Registry/config mutation
power configuration
startup control
restart/resume lifecycle
monitoring
measurement/benchmark
```

A sequência exata deve ser baseada nas lacunas observadas no primeiro slice, não em uma lista fixa pré-definida.
