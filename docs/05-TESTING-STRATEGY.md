# PC Optimizer V2 — Testing Strategy

## 1. Objetivo deste documento

Este documento define a estratégia oficial de testes da V2.

O objetivo é garantir que cada funcionalidade possa ser validada de forma independente da interface e que alterações no Windows sejam testadas com o máximo de controle possível.

A estratégia deve responder:

- a lógica está correta?
- o plano gerado é correto?
- a alteração realmente funciona?
- o resultado corresponde ao esperado?
- o rollback funciona?
- o estado original foi realmente restaurado?
- a feature funciona em diferentes ambientes?
- a falha é detectável e recuperável?

---

# 2. Princípio central

A UI não deve ser necessária para testar o backend.

Uma funcionalidade deve poder ser validada através de:

```text id="1gr97a"
Unit Tests
DevTools / CLI
Dry-run
Integration Tests
Controlled Real Tests
Rollback Tests
End-to-End Tests
```

O frontend entra apenas nas etapas em que comportamento visual ou integração completa precisam ser testados.

---

# 3. Modelo geral de validação

Para features que alteram o sistema:

```text id="0wf5g3"
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
Detect Again
↓
Verify
↓
Rollback
↓
Detect Again
↓
Verify Rollback
```

A feature não deve ser considerada tecnicamente comprovada apenas porque o comando de alteração foi executado sem erro.

---

# 4. Camadas de teste

## 4.1 Teste unitário

Testa lógica isolada sem modificar o Windows real.

Exemplos:

- regras de recomendação;
- parsers;
- comparação de estados;
- geração de ChangePlan;
- validação de compatibilidade;
- classificação de risco;
- transformação de resultados;
- tratamento de erros.

Exemplo:

```text id="w5ggz6"
Given:
USB Selective Suspend = Enabled

When:
BuildPlan()

Expected:
Current = Enabled
Target = Disabled
Changes = 1
```

---

## 4.2 Teste de contrato

Valida se componentes respeitam interfaces e formatos esperados.

Exemplos:

```text id="i677iw"
FeatureState
Recommendation
ChangePlan
ApplyResult
VerificationResult
RollbackResult
FeatureError
```

Isso reduz problemas de integração entre backend e frontend.

---

## 4.3 Dry-run

Dry-run valida:

- detecção;
- requisitos;
- lógica;
- plano;
- valores atuais;
- valores desejados;
- permissões esperadas;
- necessidade de reinício;
- risco;
- reversibilidade.

Dry-run nunca altera o sistema.

Fluxo:

```text id="hpm1br"
Detect
↓
Build Plan
↓
Validate
↓
Return Plan
```

Sem:

```text id="38fi2n"
Apply
```

---

# 5. Dry-run e execução real usam a mesma lógica

Não devem existir duas implementações independentes:

```text id="irxxoa"
fakeDryRunLogic()
realApplyLogic()
```

Preferir:

```text id="kr0lxs"
state = Detect()
plan = BuildPlan(state)
```

Depois:

```text id="q1ry92"
if dryRun
    return plan
else
    Apply(plan)
```

Isso reduz divergência entre o que o app promete alterar e o que realmente altera.

---

# 6. Snapshot

Antes de qualquer alteração reversível, capturar o estado original.

O snapshot deve representar o estado real encontrado.

Exemplo:

```text id="vazlln"
Feature:
USB Selective Suspend

Original:
Enabled

Target:
Disabled
```

Para Registry, considerar também:

```text id="a6d8ug"
Key existed:
Value existed:
Value type:
Original value:
```

A ausência de uma configuração também é estado válido.

---

# 7. Persistência de snapshot durante testes

Durante execução real, o snapshot deve sobreviver pelo menos até o encerramento seguro da operação.

Se a feature exigir:

- reinício do app;
- logoff;
- reboot;
- reinício de serviço;

o snapshot deve ser persistido de forma adequada.

Possível estrutura:

```text id="r5e7s9"
sessions/
└── test-session-id.json
```

---

# 8. Controlled Real Test

Depois do dry-run, a funcionalidade deve ser executada de verdade quando necessário.

Fluxo:

```text id="j335t4"
Snapshot
↓
Apply
↓
Detect
↓
Compare Expected vs Actual
```

Resultado esperado:

```text id="63nvs9"
APPLY: PASS
VERIFY: PASS
```

---

# 9. Verify

Verify deve consultar o estado real sempre que possível.

Não considerar suficiente:

```text id="4il631"
exit code = 0
```

Preferir:

```text id="hn58m0"
Apply
↓
Detect actual state
↓
Compare with target
```

Possíveis resultados:

```text id="ue8sdy"
PASS
FAIL
PARTIAL
UNKNOWN
```

---

# 10. Rollback

Depois da verificação, um teste reversível deve restaurar o snapshot.

Fluxo:

```text id="n0d0os"
Apply
↓
Verify
↓
Rollback(snapshot)
```

Rollback deve restaurar:

```text id="fsvsjc"
estado original
```

e não:

```text id="3y3j54"
valor default
```

---

# 11. Verify Rollback

Depois do rollback:

```text id="4vpmjk"
Detect
↓
Compare current state with original snapshot
```

Resultado ideal:

```text id="g0xmjn"
Original == Restored
```

A feature só deve ser considerada completamente reversível depois dessa etapa ser comprovada.

---

# 12. Teste transacional

Para alterações simples, o teste pode funcionar como uma pequena transação:

```text id="5dfctw"
snapshot = capture()

try:
    plan = buildPlan(snapshot)
    apply(plan)
    verify(plan)
finally:
    rollback(snapshot)
    verifyRollback(snapshot)
```

Mesmo se Verify falhar, o sistema ainda tenta restaurar o estado original.

---

# 13. Falha durante Apply

Para features compostas:

```text id="7w5pmy"
Change A
Change B
Change C
```

Se B falhar depois que A já foi aplicada, a estratégia deve ser previamente definida.

Possíveis políticas:

```text id="njzdpt"
ROLLBACK_APPLIED_CHANGES
STOP_AND_REPORT
CONTINUE_SAFE_INDEPENDENT_CHANGES
```

A política não pode ser improvisada durante o erro.

---

# 14. Teste de falhas

Sempre que possível, testar deliberadamente cenários de erro.

Exemplos:

```text id="6u681y"
sem admin
configuração inexistente
Windows incompatível
comando ausente
serviço indisponível
hardware incompatível
permissão negada
verify failure
rollback failure
```

Uma feature robusta deve ser boa não apenas no caminho feliz.

---

# 15. Testes de idempotência

Quando aplicável:

```text id="5c6ozg"
Apply
↓
Apply novamente
```

A segunda execução não deve produzir comportamento inesperado.

Testar também:

```text id="v6hnly"
Rollback
↓
Rollback novamente
```

quando fizer sentido.

---

# 16. Teste com estado personalizado

Nunca testar apenas valores default.

Exemplo:

```text id="57ctl3"
Original = Custom Value
Apply = Target Value
Rollback = Custom Value
```

Isso prova que rollback restaura o usuário, não o padrão do Windows.

---

# 17. Teste com configuração ausente

Exemplo Registry:

```text id="x69rvm"
Original:
Value does not exist

Apply:
Create value = X

Rollback:
Remove value
```

Isso deve ser tratado explicitamente.

---

# 18. Classificação de ambientes

## Dry-run

Risco mínimo.

Uso:

- desenvolvimento diário;
- validação de lógica;
- inspeção do ChangePlan.

---

## VM

Boa para:

- Registry;
- serviços;
- configurações de Windows;
- reboot;
- falhas controladas;
- testes destrutivos;
- compatibilidade por versão.

Limitações:

- GPU;
- monitor;
- sensores;
- periféricos;
- comportamento físico;
- alguns drivers.

---

## PC secundário

Preferível para:

- hardware real;
- drivers;
- GPU;
- USB;
- rede;
- reboot;
- energia.

---

## PC principal

Usar principalmente para:

- dry-run;
- features já compreendidas;
- testes de baixo risco;
- validação final controlada.

Evitar usar o PC principal como único ambiente de prova.

---

# 19. Classificação inicial por risco

## Low

Exemplo conceitual:

- configuração simples;
- detectável;
- reversível;
- sem reboot;
- sem impacto em componentes críticos.

Pode ser testada com menor isolamento.

---

## Medium

Exemplos:

- energia;
- rede;
- serviços;
- alterações que afetam comportamento geral.

Preferir:

```text id="137h1k"
Dry-run
↓
VM/PC secundário
↓
PC principal se necessário
```

---

## High

Exemplos:

- drivers;
- boot;
- segurança;
- storage;
- componentes críticos.

Exigir ambiente isolado ou dedicado.

---

## Critical

Features capazes de causar perda de acesso, boot ou dados devem receber análise especial e podem ser excluídas do produto.

---

# 20. System Restore durante testes

Ponto de restauração pode ser utilizado como camada adicional.

Ele não substitui:

```text id="v0j71u"
Snapshot
Rollback
Verify Rollback
```

Deve ser tratado como mecanismo de recuperação mais amplo.

---

# 21. Features com reboot

Fluxo possível:

```text id="kgfa8r"
Create Test Session
↓
Capture Snapshot
↓
Apply
↓
Persist Session
↓
Reboot
↓
Resume Test
↓
Detect
↓
Verify
↓
Rollback
↓
Reboot if needed
↓
Verify Rollback
↓
Close Session
```

A sessão precisa ser identificável de forma persistente.

---

# 22. Features dependentes de hardware

Dry-run pode validar apenas parte do comportamento.

Exemplo:

```text id="7vw6g9"
Monitor calibration
GPU vendor setting
USB device behavior
Battery configuration
```

Registrar:

```text id="p9ewxf"
Logic validated
Real hardware validation pending
```

Não marcar como plenamente comprovada antes do teste adequado.

---

# 23. Testes de compatibilidade

Cada feature deve registrar os ambientes em que foi comprovada.

Exemplo:

```text id="k9el7g"
Windows 11 24H2 x64
PASS

Windows 11 25H2 x64
PASS

Windows 10 22H2
NOT TESTED
```

Nunca converter:

```text id="o5ae6w"
NOT TESTED
```

em:

```text id="g4ffzk"
SUPPORTED
```

---

# 24. Matriz de teste

Pode ser mantida futuramente em estrutura semelhante:

```text id="yvy6ex"
Feature | Win Version | HW | Detect | Apply | Verify | Rollback | Status
```

Exemplo:

```text id="ts0x0a"
USB Suspend | W11 24H2 | Desktop | PASS | PASS | PASS | PASS | PROVEN
```

---

# 25. DevTools / CLI

Deve existir uma interface de desenvolvimento independente da UI.

Possíveis comandos:

```text id="wr57fj"
optimizer-dev features
optimizer-dev detect <feature>
optimizer-dev plan <feature>
optimizer-dev dry-run <feature>
optimizer-dev apply <feature>
optimizer-dev verify <feature>
optimizer-dev rollback <feature>
optimizer-dev test <feature>
```

---

# 26. Saída padronizada

Exemplo:

```text id="x4zekn"
Feature:
usb-selective-suspend

Environment:
Windows 11 25H2

DETECT
PASS
Current: Enabled

SNAPSHOT
PASS

PLAN
PASS
Enabled -> Disabled

DRY-RUN
PASS
No system changes made

APPLY
PASS

VERIFY
PASS
Actual: Disabled

ROLLBACK
PASS

RESTORE VERIFY
PASS
Actual: Enabled

RESULT
TEST PASSED
```

---

# 27. Logs

Toda execução de teste real deve produzir logs suficientes para diagnóstico.

Registrar:

```text id="37v0l0"
timestamp
test/session id
feature
environment
operation
input state
expected state
actual state
result
error code
```

Evitar armazenar informação sensível desnecessária.

---

# 28. Error codes

A estratégia de testes deve preferir erros estruturados.

Exemplos provisórios:

```text id="pv7cq7"
ADMIN_REQUIRED
UNSUPPORTED
NOT_FOUND
ACCESS_DENIED
APPLY_FAILED
VERIFY_FAILED
ROLLBACK_FAILED
RESTORE_VERIFY_FAILED
RESTART_REQUIRED
HARDWARE_UNSUPPORTED
POLICY_BLOCKED
```

A lista definitiva pertence à arquitetura.

---

# 29. Testes do Recommendation Engine

Separar de Apply.

Exemplo:

```text id="cg8qlm"
Given:
Desktop
USB Selective Suspend = Enabled
Profile = Gaming

Expected:
Recommendation = Candidate
```

Outro:

```text id="xyg9kg"
Given:
Laptop on battery
USB Selective Suspend = Enabled

Expected:
Recommendation != automatically disable
```

Assim mudanças na recomendação não afetam a execução técnica.

---

# 30. Testes de ChangePlan

O plano deve ser determinístico quando o estado de entrada for o mesmo.

Exemplo:

```text id="5wctj1"
State A
↓
BuildPlan()
↓
Plan X
```

Executar novamente com State A deve produzir Plan X, salvo informação externa relevante.

---

# 31. Testes de mocks

Mocks devem respeitar os mesmos contratos das implementações reais.

Exemplo:

```text id="u1ekgx"
MockFeatureProvider
RealFeatureProvider
```

Ambos retornam:

```text id="vccn46"
FeatureState
Recommendation
ChangePlan
ApplyResult
```

Isso permite desenvolver o frontend de forma independente.

---

# 32. Testes do frontend

Devem verificar:

- estado normal;
- loading;
- erro;
- unsupported;
- recommendation;
- restart required;
- admin required;
- partial success;
- rollback available;
- feature unavailable.

Não testar apenas o estado ideal.

---

# 33. Teste end-to-end

Só depois da integração.

Fluxo:

```text id="2ocf8t"
User Action
↓
Frontend
↓
Application
↓
Core
↓
Windows Infrastructure
↓
Windows
↓
Result
↓
Frontend
```

Validar tanto comportamento técnico quanto apresentação correta.

---

# 34. Critério para feature PROVEN

Quando aplicável:

```text id="bbkc20"
[ ] Unit tests
[ ] Detect tested
[ ] Plan tested
[ ] Dry-run tested
[ ] Apply tested
[ ] Verify tested
[ ] Snapshot tested
[ ] Rollback tested
[ ] Verify Rollback tested
[ ] Failure cases tested
[ ] Compatibility recorded
[ ] Logs reviewed
```

---

# 35. Critério para feature TESTED no produto

Uma feature implementada pode ser considerada TESTED quando:

```text id="it8ho2"
[ ] implementação final testada
[ ] contratos testados
[ ] integração testada
[ ] UI states testados
[ ] rollback final testado
[ ] ambiente suportado comprovado
[ ] erros tratados
[ ] documentação atualizada
```

---

# 36. Regra sobre testes manuais

Teste manual é válido.

Mas:

```text id="x3hhy4"
"eu cliquei e parece que funcionou"
```

não deve ser a única evidência.

Sempre que possível, registrar:

- estado antes;
- ação;
- estado esperado;
- estado depois;
- rollback;
- estado restaurado.

---

# 37. Regra de regressão

Quando uma implementação de feature mudar, repetir testes relevantes.

Especialmente:

```text id="bbwt0e"
Detect
Plan
Apply
Verify
Rollback
```

Uma melhoria interna não deve assumir que rollback continua funcionando.

---

# 38. Automação futura

Conforme o projeto amadurecer, automatizar o máximo possível.

Principalmente:

- testes unitários;
- contratos;
- parsers;
- ChangePlan;
- recomendações;
- compatibilidade lógica;
- integração não destrutiva.

Testes que alteram Windows real podem continuar exigindo ambientes controlados.

---

# 39. Teste não substitui especificação

Não usar tentativa e erro como substituto para entender a feature.

Fluxo correto:

```text id="nv74dp"
Research
↓
Specification
↓
Prototype
↓
Test
```

Não:

```text id="zwn0gq"
Random command
↓
Did it work?
```

---

# 40. Princípio final

O objetivo não é provar apenas que:

```text id="b02m22"
"conseguimos mudar isso"
```

O objetivo é provar que:

```text id="xjn4n5"
sabemos detectar,
sabemos planejar,
sabemos alterar,
sabemos confirmar,
sabemos detectar falha
e sabemos restaurar.
```

Esse é o padrão de confiança esperado para uma feature que modifica o sistema do usuário.