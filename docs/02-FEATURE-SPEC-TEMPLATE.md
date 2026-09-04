# PC Optimizer V2 — Feature Specification Template

## 1. Objetivo deste documento

Este arquivo define o template oficial de especificação técnica de funcionalidades.

Toda feature que sair da fase de descoberta e for considerada candidata real ao produto deverá passar por este modelo antes da implementação definitiva.

O objetivo é evitar repetir o principal problema da primeira versão:

> saber o que uma funcionalidade deveria fazer, mas descobrir durante a implementação como ela realmente funciona.

A especificação deve reduzir essa incerteza antes de entrar no desenvolvimento final.

---

# 2. Regra principal

Uma feature não deve ser implementada definitivamente enquanto perguntas essenciais sobre seu comportamento técnico ainda estiverem em aberto.

Não é obrigatório saber cada linha de código.

É obrigatório saber suficientemente:

- o que detectar;
- o que decidir;
- o que alterar;
- como alterar;
- como validar;
- como desfazer;
- quando não executar.

---

# 3. Identificação

```text
Nome:
ID:
Tipo:
Área:
Status:
Prioridade:
Responsável:
Última revisão:
```

### Tipo

Usar inicialmente:

```text
Diagnostic
Optimization
Repair
Configuration
Tool
```

### Status

Possíveis estados:

```text
RESEARCH
SPECIFIED
PROTOTYPING
PROVEN
APPROVED
BLOCKED
DEFERRED
REJECTED
```

---

# 4. Resumo

## O que é?

Descrever a funcionalidade em poucas linhas.

## Objetivo

Qual resultado ela tenta produzir?

## Problema que resolve

Qual problema real do usuário ou do sistema justifica sua existência?

## Benefício esperado

Classificar quando aplicável:

```text
Performance
Latency
Stability
Energy
Storage
Privacy
Repair
Diagnostic
Quality of Life
Visual
Compatibility
```

---

# 5. Aplicabilidade

## Quando se aplica?

Exemplos:

```text
All PCs
Desktop
Laptop
Gaming
Specific Hardware
Specific Windows Version
Optional / Preference
```

## Condições necessárias

Exemplo:

```text
- Windows 11
- dispositivo USB compatível
- plano de energia disponível
```

## Quando NÃO deve ser aplicada?

Registrar cenários em que a feature deve:

- ficar oculta;
- ficar indisponível;
- não ser recomendada;
- gerar aviso;
- exigir decisão manual.

---

# 6. Recomendação

## A feature deve ser recomendável automaticamente?

```text
Yes / No / Conditional
```

## Critérios de recomendação

Quais condições fazem com que o app sugira essa mudança?

Exemplo conceitual:

```text
IF desktop
AND USB selective suspend = enabled
AND use case = low-latency gaming
THEN recommendation = candidate
```

## Motivo apresentado ao usuário

Qual explicação técnica e compreensível será exibida?

## Grau de confiança

```text
High
Medium
Low
Not Applicable
```

---

# 7. Estado atual

## O que precisa ser detectado?

Listar os dados necessários para entender o estado atual.

## Como detectar?

Descrever o mecanismo.

Pode envolver:

```text
Registry
Win32 API
WMI / CIM
PowerShell
CLI
File
Service
Driver API
Vendor API
```

## Fonte da verdade

Qual informação será considerada autoritativa?

Evitar detectar uma configuração por um método e aplicar por outro sem entender possíveis inconsistências.

## Estados possíveis

Exemplo:

```text
Enabled
Disabled
Unavailable
Unsupported
Unknown
Error
```

---

# 8. Estado alvo

Qual deve ser o resultado desejado?

Registrar exatamente o que significa "aplicado com sucesso".

Exemplo:

```text
USB selective suspend = disabled
```

ou, para uma feature composta:

```text
A = X
B = Y
C remains unchanged
```

---

# 9. Implementação técnica

## Método principal

Descrever a abordagem escolhida.

## Tecnologias utilizadas

Marcar quando aplicável:

```text
[ ] .NET API
[ ] Win32
[ ] Registry
[ ] PowerShell
[ ] CMD / executable
[ ] WMI / CIM
[ ] Vendor API
[ ] File modification
[ ] Service Control Manager
[ ] Other
```

## Comandos / APIs / chaves

Registrar detalhes suficientes para que a prova técnica possa ser construída.

## Alternativas avaliadas

Se existem múltiplas formas de fazer a mesma alteração, registrar:

```text
Approach A:
Pros:
Cons:

Approach B:
Pros:
Cons:
```

## Abordagem escolhida

Explicar por que ela foi escolhida.

---

# 10. Permissões

## Requer administrador?

```text
Yes / No / Conditional / Unknown
```

## Quando ocorre a elevação?

Registrar futuramente de acordo com a arquitetura.

## O que acontece se não houver permissão?

A feature deve retornar erro estruturado, não falhar silenciosamente.

Exemplo conceitual:

```text
ADMIN_REQUIRED
```

---

# 11. Reinicialização

## Requer:

```text
[ ] Nada
[ ] Reinício do aplicativo
[ ] Reinício de processo
[ ] Reinício de serviço
[ ] Logoff
[ ] Reboot do Windows
[ ] Desconhecido
```

## A alteração só pode ser validada depois da reinicialização?

Explicar.

## Rollback também exige reinicialização?

Explicar.

---

# 12. Change Plan

A feature deve conseguir representar o que pretende fazer antes de executar.

Exemplo conceitual:

```text
ChangePlan

Feature:
Current state:
Target state:

Changes:
1.
2.
3.

Admin required:
Restart required:
Risk:
Reversible:
```

O plano deve ser utilizável pelo dry-run e pela execução real.

---

# 13. Dry-run

## É suportado?

```text
Yes / Partial / No
```

## O que o dry-run deve fazer?

Sempre que aplicável:

```text
Detect
Validate prerequisites
Build Change Plan
Show expected changes
Estimate restart/admin requirements
```

## O que o dry-run NÃO pode fazer?

Não deve executar alterações persistentes.

## Limitações

Registrar o que só pode ser comprovado durante execução real.

---

# 14. Snapshot

## É necessário?

```text
Yes / No
```

## O que precisa ser salvo antes da alteração?

Registrar o estado real.

Exemplo:

```text
Registry value
value type
key existence
service startup type
active power plan
device configuration
```

## Estado inexistente também deve ser registrado

Exemplo importante:

```text
Registry value did not exist before apply
```

Nesse caso, rollback pode precisar remover o valor, não simplesmente escrever um valor padrão.

---

# 15. Apply

## Sequência de execução

Registrar em ordem.

Exemplo:

```text
1. validar compatibilidade
2. confirmar snapshot
3. executar alteração A
4. executar alteração B
5. registrar resultado
```

## Atomicidade

O que acontece se a feature possuir várias mudanças e falhar no meio?

Definir estratégia:

```text
Rollback already applied changes
Stop and report partial failure
Continue safe independent changes
```

A estratégia deve ser explícita.

---

# 16. Verify

## Como comprovar que funcionou?

A validação não deve se limitar a:

```text
command returned exit code 0
```

Sempre que possível, detectar novamente o estado real.

Fluxo:

```text
Apply
↓
Detect
↓
Compare actual vs expected
```

## Critério de sucesso

Definir objetivamente.

## Resultado parcial

É possível existir?

```text
Yes / No
```

Se sim, explicar.

---

# 17. Rollback

## É reversível?

```text
Fully
Partially
No
Unknown
```

## Método de rollback

Descrever como restaurar o snapshot.

## O rollback restaura:

```text
estado original
```

e não necessariamente:

```text
valor padrão
```

## Ordem de reversão

Para features compostas, normalmente considerar ordem inversa da aplicação quando tecnicamente apropriado.

---

# 18. Verify Rollback

Depois de rollback:

```text
Detect
↓
Compare current vs original snapshot
```

## Critério de sucesso

Definir.

## O que fazer se rollback falhar?

Registrar:

- erro;
- orientação;
- possibilidade de nova tentativa;
- necessidade de System Restore;
- necessidade de ação manual.

---

# 19. System Restore

## A feature exige ponto de restauração?

```text
Required
Recommended
Not Required
TBD
```

## Motivo

Explicar com base no risco.

O ponto de restauração não elimina a necessidade de snapshot e rollback quando estes forem aplicáveis.

---

# 20. Risco

## Classificação

```text
Low
Medium
High
Critical
Unknown
```

## Possíveis efeitos negativos

Listar.

## Pior cenário plausível

Registrar.

## Recuperação manual possível?

```text
Yes / No / Partial
```

## Procedimento manual

Se conhecido, registrar para uso interno/testes.

---

# 21. Compatibilidade

## Windows

Registrar versões testadas ou esperadas.

Exemplo:

```text
Windows 10 22H2:
Windows 11 23H2:
Windows 11 24H2:
Windows 11 25H2:
```

Não assumir compatibilidade sem evidência.

## Arquitetura

```text
x64
ARM64
Other
```

## Hardware

Dependências:

```text
CPU vendor:
GPU vendor:
Laptop/Desktop:
Device class:
```

---

# 22. Dependências

## Outras features

Esta feature depende de outra?

## Serviços

## APIs

## Componentes do Windows

## Drivers

## Internet

## Aplicações externas

Registrar todas as dependências conhecidas.

---

# 23. Conflitos

A feature pode conflitar com:

- outra feature do app;
- software de fabricante;
- política corporativa;
- Windows Update;
- antivírus;
- software de tuning;
- configuração personalizada do usuário?

Registrar comportamento esperado.

---

# 24. Idempotência

Pergunta importante:

> Executar Apply duas vezes causa o mesmo resultado sem produzir efeitos adicionais indesejados?

```text
Yes / No / Unknown
```

Se não for idempotente, explicar.

---

# 25. Falhas possíveis

Listar cenários.

Exemplos:

```text
ADMIN_REQUIRED
UNSUPPORTED_WINDOWS_VERSION
SETTING_NOT_FOUND
COMMAND_FAILED
VERIFY_FAILED
ROLLBACK_FAILED
RESTART_REQUIRED
HARDWARE_UNSUPPORTED
POLICY_BLOCKED
ACCESS_DENIED
```

Os códigos definitivos serão padronizados na arquitetura.

---

# 26. Logging

Registrar quais eventos precisam aparecer nos logs.

Exemplo:

```text
Detection started
Current state found
Plan generated
Snapshot captured
Apply started
Change A succeeded
Change B failed
Verify failed
Rollback started
Rollback verified
```

Não registrar informações sensíveis sem necessidade.

---

# 27. Persistência

## O estado precisa sobreviver ao fechamento do app?

```text
Yes / No
```

## O snapshot precisa sobreviver reboot?

```text
Yes / No
```

## Arquivos/dados necessários

Registrar.

---

# 28. Testes

## Teste unitário

O que pode ser testado sem Windows real?

Exemplos:

- parser;
- regras de recomendação;
- geração de ChangePlan;
- comparação de estado;
- validação de compatibilidade.

## Dry-run

Descrever cenário esperado.

## Integração real

Descrever aplicação real.

## Rollback

Descrever.

## VM

```text
Safe / Not Suitable / Partial
```

## Hardware real

```text
Required / Optional / No
```

---

# 29. Cenários mínimos de teste

Sempre que aplicável testar:

```text
[ ] Feature já está no estado desejado
[ ] Feature precisa ser alterada
[ ] Feature não é suportada
[ ] Falta permissão
[ ] Apply falha
[ ] Verify falha
[ ] Rollback funciona
[ ] Rollback falha
[ ] Estado inicial personalizado
[ ] Configuração inexistente
[ ] Execução repetida
```

---

# 30. Prova técnica

## Script/protótipo

Local:

```text
/prototypes/...
```

ou referência correspondente.

## Resultado

```text
DETECT:
PLAN:
DRY-RUN:
APPLY:
VERIFY:
ROLLBACK:
RESTORE VERIFY:
```

Exemplo:

```text
DETECT: PASS
PLAN: PASS
DRY-RUN: PASS
APPLY: PASS
VERIFY: PASS
ROLLBACK: PASS
RESTORE VERIFY: PASS
```

## Ambiente utilizado

Registrar:

```text
Windows version:
Hardware:
Admin:
Date:
```

---

# 31. Evidências

Registrar links ou referências para:

- documentação Microsoft;
- documentação de fabricante;
- APIs;
- comportamento observado;
- testes;
- benchmarks relevantes.

Separar claramente:

```text
documented behavior
```

de:

```text
observed behavior
```

---

# 32. Benefício real

Antes de aprovar a feature, responder:

> Existe motivo técnico suficiente para acreditar que isso beneficia o usuário?

Classificação:

```text
Proven
Reasonable
Situational
Preference
Uncertain
No Meaningful Benefit
```

Isso ajuda a não vender preferência como otimização.

---

# 33. Impacto mensurável

## Pode ser medido?

```text
Yes / No / Partial
```

## Como?

Exemplos:

```text
FPS
frametime
latency
boot time
energy consumption
disk space
error resolved
temperature
resource usage
```

Nem toda feature precisa ter impacto mensurável.

---

# 34. Experiência do usuário

Ainda sem definir layout final, registrar o necessário para a UI.

## Informações que devem ser apresentadas

Exemplo:

```text
Name
Description
Current state
Recommended state
Reason
Risk
Restart
Admin
Reversible
```

## Ações disponíveis

```text
Apply
Skip
Details
Restore Default
Rollback
```

Somente quando aplicável.

---

# 35. Dados necessários pelo frontend

Definir quais dados a camada de aplicação deverá fornecer.

Exemplo conceitual:

```text
id
name
description
currentState
recommendedState
recommended
recommendationReason
risk
requiresAdmin
requiresRestart
reversible
status
```

Isso ajuda futuramente na definição do contrato sem acoplar a feature ao layout.

---

# 36. Questões em aberto

Manter uma lista explícita.

```text
- ?
- ?
- ?
```

Não esconder incerteza dentro da especificação.

---

# 37. Critério para PROVEN

A feature pode receber status `PROVEN` quando, quando aplicável:

```text
[ ] Detect funciona
[ ] ChangePlan representa corretamente as alterações
[ ] Dry-run não altera o sistema
[ ] Apply funciona
[ ] Verify confirma o resultado
[ ] Snapshot captura o estado correto
[ ] Rollback restaura o estado anterior
[ ] Verify Rollback confirma a restauração
[ ] Riscos principais foram identificados
[ ] Compatibilidade mínima foi testada
[ ] Falhas conhecidas estão documentadas
```

Nem todas as features exigirão todos os itens.

Exemplo:

Uma ferramenta puramente de diagnóstico não precisa de rollback.

---

# 38. Critério para APPROVED

Uma feature `PROVEN` não entra automaticamente no produto.

Para `APPROVED`, avaliar:

```text
[ ] É útil
[ ] É confiável
[ ] Pertence ao escopo
[ ] Risco é aceitável
[ ] Manutenção futura é aceitável
[ ] UX pode apresentá-la corretamente
[ ] Não duplica outra feature
[ ] Não depende de comportamento frágil sem justificativa
```

---

# 39. Exemplo resumido

```text
Nome:
USB Selective Suspend

Tipo:
Optimization / Configuration

Área:
Energy / USB

Objetivo:
Avaliar e, quando apropriado, desativar suspensão seletiva USB.

Detect:
Consultar configuração efetiva do plano de energia.

Plan:
Enabled → Disabled

Dry-run:
Mostrar alteração planejada sem executar.

Snapshot:
Salvar valor original por plano/configuração relevante.

Apply:
Alterar através do mecanismo escolhido.

Verify:
Consultar novamente estado efetivo.

Rollback:
Restaurar exatamente o estado salvo.

Verify Rollback:
Confirmar igualdade com snapshot.

Admin:
TBD

Restart:
TBD

Risk:
Low/Medium — confirmar.

Compatibility:
TBD

Status:
RESEARCH
```

O exemplo acima é propositalmente incompleto.

A especificação real deve ser baseada em pesquisa e prova técnica.

---

# 40. Princípio final

O objetivo deste template não é criar burocracia.

Ele existe para transformar:

```text
"acho que essa feature funciona assim"
```

em:

```text
"sabemos o que ela faz,
como funciona,
como provar,
como detectar falha
e como recuperar"
```

Se um campo não se aplicar, registrar:

```text
N/A
```

Se ainda não soubermos:

```text
TBD
```

Nunca preencher uma lacuna importante com suposição apenas para marcar a especificação como concluída.