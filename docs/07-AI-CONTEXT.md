# PC Optimizer V2 — AI Context

## 1. Objetivo

Este documento define o contexto e as regras que agentes de IA devem seguir ao trabalhar no projeto PC Optimizer V2.

Ele existe para impedir implementações apressadas, acoplamento indevido e mudanças que ignorem decisões já registradas.

Antes de executar tarefas relevantes no projeto, o agente deve considerar este documento e os arquivos relacionados em `/docs`.

---

# 2. Prioridade dos documentos

Quando houver dúvida, consultar nesta ordem:

```text
00-PROJECT-PLAN.md
06-DECISIONS.md
03-ARCHITECTURE.md
02-FEATURE-SPEC-TEMPLATE.md
05-TESTING-STRATEGY.md
04-UX-FLOW.md
01-DISCOVERY.md
08-BACKLOG.md
```

Se houver conflito entre documentos, priorizar decisões mais recentes registradas em `06-DECISIONS.md`.

---

# 3. Regra principal

Não implementar funcionalidades importantes apenas com base em uma descrição superficial.

Antes de implementar uma feature, verificar se ela possui especificação suficiente.

Quando aplicável, devem estar definidos:

```text
Detect
Plan
Dry-run
Snapshot
Apply
Verify
Rollback
Verify Rollback
Risk
Compatibility
Testing
```

Se informações essenciais estiverem ausentes, não inventar silenciosamente.

Registrar a lacuna ou apontar necessidade de especificação.

---

# 4. Não confundir protótipo com produção

Código em:

```text
/prototypes
```

serve para validar hipóteses técnicas.

Não assumir automaticamente que:

```text
prototype code = production code
```

Ao migrar uma prova técnica para produção:

- respeitar arquitetura;
- criar abstrações adequadas;
- tratar erros;
- adicionar logging;
- adicionar testes;
- remover dependências temporárias.

---

# 5. Frontend não acessa Windows diretamente

Nunca colocar na camada de UI chamadas diretas para:

```text
Registry
PowerShell
powercfg
Win32
WMI
Service Control Manager
CMD
drivers
vendor APIs
```

Fluxo esperado:

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

---

# 6. ViewModels não são backend

ViewModels podem:

- carregar dados;
- expor estado;
- reagir a comandos da UI;
- chamar Application Services.

ViewModels não devem:

- editar Registry;
- iniciar PowerShell diretamente;
- executar comandos do sistema;
- manipular serviços;
- conter regras complexas de otimização.

---

# 7. Preservar contratos

O frontend e DevTools devem depender de contratos estáveis.

Não adaptar o backend a cada tela específica.

Preferir modelos estruturados como:

```text
FeatureState
Recommendation
ChangePlan
ApplyResult
VerificationResult
RollbackResult
FeatureError
```

A estrutura definitiva desses modelos deve seguir a arquitetura vigente.

---

# 8. Dry-run

Dry-run nunca altera o sistema.

Ele deve apenas:

```text
Detect
Validate
Build Plan
Return Preview
```

Não executar:

```text
Apply
Write
Restart
Modify
```

O mesmo `ChangePlan` utilizado no dry-run deve servir de base para a execução real quando possível.

---

# 9. Snapshot

Antes de alterações reversíveis, preservar o estado real encontrado.

Não assumir que rollback deve restaurar valores padrão.

Exemplo:

```text
Original = custom value
Apply = target
Rollback = original custom value
```

Ausência de configuração também é estado válido.

---

# 10. Rollback

Rollback deve restaurar o estado anterior.

Não confundir com:

```text
Restore Default
```

Quando aplicável:

```text
Apply
↓
Verify
↓
Rollback
↓
Verify Rollback
```

---

# 11. Verify

Não considerar sucesso apenas porque um comando retornou exit code 0.

Sempre que possível:

```text
Apply
↓
Detect actual state
↓
Compare with target
```

O mesmo vale para rollback.

---

# 12. Falhas parciais

Não esconder estados intermediários.

Se uma feature possui várias mudanças e falha no meio:

- seguir a política definida na especificação;
- registrar o que foi alterado;
- tentar rollback quando apropriado;
- retornar resultado estruturado.

Nunca fingir sucesso completo.

---

# 13. Erros estruturados

Não enviar exceções técnicas brutas diretamente à UI.

Preferir erros com código.

Exemplos:

```text
ADMIN_REQUIRED
UNSUPPORTED
ACCESS_DENIED
APPLY_FAILED
VERIFY_FAILED
ROLLBACK_FAILED
HARDWARE_UNSUPPORTED
POLICY_BLOCKED
```

Detalhes técnicos devem ficar disponíveis para logs/debug.

---

# 14. Não espalhar chamadas de baixo nível

Evitar chamadas repetidas e diretas a:

```text
Registry
PowerShell
Win32
```

em múltiplas features.

Quando houver responsabilidade reutilizável, criar ou reutilizar abstração apropriada.

Exemplo:

```text
IRegistryService
IPowerConfiguration
IServiceManager
```

Não criar abstrações desnecessárias apenas por padrão.

---

# 15. Preferir simplicidade

Não introduzir sem justificativa:

```text
microservices
CQRS
event sourcing
message brokers
complex plugin frameworks
multiple processes
SQLite
IPC
```

O projeto é um aplicativo local para Windows.

Complexidade precisa resolver um problema real.

---

# 16. Persistência

Direção inicial:

```text
JSON
```

Utilizar abstrações para acesso.

Não permitir que toda a codebase leia/escreva arquivos diretamente.

Exemplo:

```text
ISettingsRepository
ISessionRepository
ISnapshotRepository
```

Não introduzir SQLite sem necessidade documentada.

---

# 17. Logging

Operações relevantes devem ser registradas.

Exemplos:

```text
Detect started
Plan generated
Snapshot captured
Apply started
Apply succeeded
Verify failed
Rollback started
Rollback verified
```

Logs não são fonte principal de estado.

Não armazenar dados sensíveis sem necessidade.

---

# 18. Testabilidade

Backend deve ser testável sem frontend.

Ao implementar uma feature, considerar:

```text
unit test
dry-run
integration test
controlled real test
rollback test
```

Não depender de clicar manualmente pela UI para validar comportamento técnico.

---

# 19. DevTools

`Optimizer.DevTools` deve reutilizar a mesma lógica do produto.

Não criar implementação paralela apenas para CLI.

Exemplo desejado:

```text
Optimizer.App ─────┐
                  ├→ Application/Core
Optimizer.DevTools┘
```

---

# 20. Mocks

Mocks devem respeitar os mesmos contratos das implementações reais.

Não criar estruturas fake incompatíveis apenas para facilitar uma tela.

Objetivo:

```text
Mock Provider
↓
same contract
↑
Real Provider
```

---

# 21. Recomendações

Não recomendar uma alteração apenas porque tecnicamente é possível.

Recomendação deve ser separada da execução.

Conceito:

```text
Analyzer
↓
Recommendation Engine
↓
Change Plan
↓
Executor
```

O executor não deve inventar regras de recomendação.

---

# 22. Não vender preferência como otimização

Diferenciar:

```text
Optimization
Configuration
Privacy
Visual Preference
Diagnostic
Repair
```

Não afirmar benefício de performance sem justificativa.

---

# 23. Não inventar métricas

Não criar:

```text
+20% performance
PC score 95
latency reduced 40%
```

sem metodologia e medição confiáveis.

Quando não for possível medir, usar linguagem adequada.

---

# 24. Compatibilidade

Não assumir suporte.

Diferenciar:

```text
SUPPORTED
TESTED
NOT TESTED
UNSUPPORTED
```

`NOT TESTED` nunca deve ser tratado como `SUPPORTED`.

---

# 25. Features dependentes de hardware

Quando uma feature depender de:

```text
GPU
monitor
USB
battery
driver
vendor API
sensor
```

não assumir comportamento universal.

Registrar capacidades e limitações.

---

# 26. Privilégios

Não assumir que todo o app deve rodar como administrador.

A estratégia de elevação ainda é decisão aberta.

Até decisão formal:

- isolar operações privilegiadas;
- identificar quais features realmente precisam de admin;
- evitar espalhar dependência de privilégios.

---

# 27. Reboot

Features que exigem reboot devem declarar isso explicitamente.

Não reiniciar automaticamente sem fluxo previsto.

Se estado precisar sobreviver reboot, utilizar sessão persistente apropriada.

---

# 28. Cancelamento

Não assumir que toda operação pode ser cancelada.

Se cancelar no meio puder causar inconsistência:

```text
Not Cancelable
```

ou:

```text
Cancelable Between Steps
```

deve ser respeitado.

---

# 29. Concorrência

Direção inicial:

```text
sequential execution
```

para alterações sensíveis.

Não paralelizar operações do Windows apenas para melhorar velocidade sem análise de dependências e segurança.

---

# 30. Feature IDs

Utilizar IDs internos estáveis.

Exemplo:

```text
energy.usb-selective-suspend
gaming.game-dvr
```

Não usar texto visível/localizado como identificador.

---

# 31. Frontend

O design poderá vir de Google Stitch.

Arquivos HTML/CSS exportados devem ser tratados como referência visual.

Não converter cegamente todo o projeto.

Fluxo preferido:

```text
Analyze reference
↓
Extract reusable components
↓
Implement AppShell
↓
Implement shared components
↓
Implement one page
↓
Validate
↓
Continue
```

---

# 32. Design System

Antes de implementar muitas telas, identificar componentes reutilizáveis.

Exemplos:

```text
Button
FeatureCard
RecommendationCard
Navigation
Warning
Progress
StepIndicator
Modal
```

Não duplicar componentes equivalentes em páginas diferentes.

---

# 33. Mocks antes de integração

Quando possível:

```text
Frontend + Mocks
```

primeiro.

Depois:

```text
Frontend + Real Backend
```

Isso facilita separar bugs visuais de bugs de integração.

---

# 34. UX

Preservar a filosofia de fluxo contínuo.

Conceito:

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

Não transformar o app em uma coleção de telas desconectadas apenas porque isso é mais simples de implementar.

---

# 35. Informação progressiva

A UI deve priorizar simplicidade.

Detalhes técnicos podem existir em níveis secundários.

Não expor Registry, GUIDs, APIs e comandos como informação principal para usuário comum.

---

# 36. Tarefas para agentes devem ser pequenas

Evitar prompts como:

```text
implemente todas as otimizações de energia
```

Preferir:

```text
implemente a detecção de USB Selective Suspend
```

Depois:

```text
implemente geração de ChangePlan
```

Depois:

```text
implemente Apply + Verify
```

quando isso fizer sentido.

---

# 37. Critérios de aceitação

Toda tarefa relevante deve possuir critérios objetivos.

Exemplo:

```text
[ ] detecta estado atual
[ ] retorna Unsupported corretamente
[ ] não altera sistema em dry-run
[ ] unit tests passam
[ ] nenhum acesso direto à UI
```

Não considerar tarefa pronta apenas porque compila.

---

# 38. Não modificar áreas fora do escopo sem necessidade

Se uma tarefa é sobre feature X, evitar refatorar grandes partes não relacionadas.

Se mudança arquitetural for necessária:

- explicar;
- limitar escopo;
- atualizar documentação relevante.

---

# 39. Não duplicar código existente

Antes de criar:

```text
new registry helper
new command runner
new logger
new parser
```

verificar se já existe abstração equivalente.

Reutilizar quando adequado.

---

# 40. Não preservar código ruim apenas porque já existe

Código da V1 pode ser reaproveitado apenas quando fizer sentido.

Ao reutilizar:

- avaliar qualidade;
- adaptar à arquitetura;
- adicionar testes;
- não manter acoplamento antigo.

---

# 41. Documentação após mudanças importantes

Quando uma implementação alterar uma decisão arquitetural ou comportamento previsto:

atualizar os documentos correspondentes.

Possíveis arquivos:

```text
03-ARCHITECTURE.md
05-TESTING-STRATEGY.md
06-DECISIONS.md
```

Não deixar documentação deliberadamente desatualizada.

---

# 42. Decisões abertas

Não decidir silenciosamente assuntos ainda abertos.

Exemplos:

```text
Windows 10 support
elevation model
IPC
SQLite
updater
packaging
telemetry
```

Se uma tarefa exigir uma dessas decisões, registrar ou solicitar decisão arquitetural apropriada.

---

# 43. Segurança

O aplicativo modifica configurações do sistema.

Prioridades:

```text
correctness
reversibility
transparency
safety
```

antes de:

```text
number of tweaks
speed of implementation
visual polish
```

---

# 44. Princípio final

Antes de modificar código, o agente deve conseguir responder:

```text
O que estou mudando?
Por que?
Qual camada é responsável?
Como será testado?
Como saberemos se funcionou?
Como falha?
Como é revertido?
```

Se essas respostas não existirem para uma mudança crítica, a tarefa provavelmente ainda não está pronta para implementação.