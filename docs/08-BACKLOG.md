# PC Optimizer V2 — Backlog

## 1. Objetivo deste documento

Este arquivo mantém uma visão resumida do trabalho pendente do projeto.

Ele não substitui:

- documentação de discovery;
- especificações de features;
- decisões arquiteturais;
- issues detalhadas;
- sistema de tarefas futuro.

O objetivo é responder rapidamente:

> O que ainda precisa ser feito?

---

# 2. Regra principal

O backlog deve conter itens de trabalho, não documentação técnica completa.

Ruim:

```text
Implementar toda a otimização de energia com todos os detalhes técnicos...
```

Melhor:

```text
Especificar USB Selective Suspend
```

ou:

```text
Provar tecnicamente detecção de plano de energia
```

Detalhes devem ficar nos documentos específicos.

---

# 3. Estados

Utilizar inicialmente:

```text
TODO
IN_PROGRESS
BLOCKED
DONE
DEFERRED
CANCELLED
```

---

# 4. Prioridade

Quando necessário:

```text
P0 — bloqueia o projeto
P1 — importante
P2 — normal
P3 — baixa prioridade
```

Não atribuir prioridade a tudo apenas por hábito.

---

# 5. Tipos de item

Possíveis categorias:

```text
DISCOVERY
RESEARCH
SPEC
PROTOTYPE
ARCHITECTURE
UX
DESIGN
BACKEND
FRONTEND
TEST
INFRA
DOCS
DECISION
BUG
```

---

# 6. Formato sugerido

```text
- [ ] [P1] [SPEC] Especificar USB Selective Suspend
```

Quando necessário:

```text
  Depends on:
  Blocked by:
  Notes:
```

Evitar transformar cada item em uma descrição enorme.

---

# 7. Current Focus

Manter esta seção pequena.

Ela representa apenas o trabalho mais próximo.

```text
## Current Focus

- [ ] [P0] [DISCOVERY] Iniciar levantamento de funcionalidades
- [ ] [P0] [DISCOVERY] Cobrir área Sistema
- [ ] [P1] [DISCOVERY] Cobrir área Energia
```

Quando esses itens forem concluídos, substituir pelos próximos.

---

# 8. Planning Backlog

```text
## Planning

- [x] Criar 00-PROJECT-PLAN.md
- [x] Criar 01-DISCOVERY.md
- [x] Criar 02-FEATURE-SPEC-TEMPLATE.md
- [x] Criar 03-ARCHITECTURE.md
- [x] Criar 04-UX-FLOW.md
- [x] Criar 05-TESTING-STRATEGY.md
- [x] Criar 06-DECISIONS.md
- [x] Criar 07-AI-CONTEXT.md
- [x] Criar 08-BACKLOG.md

- [ ] Revisar documentação após primeira rodada de discovery
- [ ] Atualizar decisões abertas com evidências encontradas
```

---

# 9. Discovery Backlog

```text
## Discovery

- [ ] [P0] Sistema
- [ ] [P1] Energia
- [ ] [P1] CPU
- [ ] [P1] Memória RAM
- [ ] [P1] Armazenamento
- [ ] [P1] GPU
- [ ] [P1] Jogos
- [ ] [P1] Rede
- [ ] [P2] USB e periféricos
- [ ] [P2] Monitor e imagem
- [ ] [P2] Áudio
- [ ] [P2] Drivers
- [ ] [P2] Inicialização
- [ ] [P2] Processos e aplicações
- [ ] [P2] Limpeza
- [ ] [P2] Reparos do Windows
- [ ] [P2] Segurança
- [ ] [P2] Privacidade
- [ ] [P2] Windows Update
- [ ] [P2] Diagnóstico de hardware
- [ ] [P2] Temperatura e refrigeração
- [ ] [P2] Bateria
- [ ] [P2] Recovery / Restore Point
- [ ] [P3] Personalização
- [ ] [P3] Benchmark e medição
```

A ordem pode mudar conforme novas informações surgirem.

---

# 10. Specification Backlog

Features só entram aqui quando estiverem prontas para sair de Discovery.

```text
## Features Ready for Specification

<!-- Exemplo:
- [ ] [P1] [SPEC] energy.usb-selective-suspend
- [ ] [P1] [SPEC] gaming.game-dvr
-->
```

---

# 11. Prototype Backlog

```text
## Technical Prototypes

<!-- Exemplo:
- [ ] [P1] [PROTOTYPE] Testar detecção de USB Selective Suspend
- [ ] [P1] [PROTOTYPE] Implementar dry-run da feature
- [ ] [P1] [PROTOTYPE] Testar Apply → Verify → Rollback
-->
```

---

# 12. Architecture Backlog

Itens que dependem das provas técnicas.

```text
## Architecture

- [ ] Definir versão mínima do Windows
- [ ] Decidir suporte a Windows 10
- [ ] Definir versão do .NET
- [ ] Validar WinUI 3 como UI definitiva
- [ ] Definir estrutura final da solution
- [ ] Definir contratos finais de Feature
- [ ] Definir Error Model
- [ ] Definir ChangePlan final
- [ ] Definir Snapshot model
- [ ] Definir OptimizationSession
- [ ] Definir logging
- [ ] Definir modelo de elevação administrativa
- [ ] Decidir processo único vs auxiliar privilegiado
- [ ] Definir estratégia de recovery após reboot
- [ ] Definir packaging
- [ ] Definir estratégia de updates
```

Não resolver esses itens prematuramente se ainda faltarem requisitos.

---

# 13. UX Backlog

```text
## UX

- [ ] Definir agrupamento inicial das features
- [ ] Mapear dependências
- [ ] Mapear conflitos
- [ ] Definir jornada principal
- [ ] Definir etapas do fluxo
- [ ] Definir comportamento de preview
- [ ] Definir fluxo de aplicação
- [ ] Definir resultado
- [ ] Definir rollback na UX
- [ ] Definir comportamento de reboot
- [ ] Definir acesso a ferramentas independentes
- [ ] Definir modo avançado, se necessário
```

---

# 14. Design Backlog

Só iniciar quando houver maturidade suficiente de UX.

```text
## Design

- [ ] Criar primeira exploração visual no Stitch
- [ ] Prototipar jornada principal
- [ ] Validar fluxo
- [ ] Definir Design System
- [ ] Identificar componentes reutilizáveis
- [ ] Definir AppShell
- [ ] Definir Navigation
- [ ] Definir estados das features
- [ ] Definir loading/error/success states
- [ ] Exportar referências visuais
```

---

# 15. Backend Backlog

Ainda não deve ser preenchido extensivamente durante discovery.

```text
## Backend

- [ ] Criar solution inicial após arquitetura ser aprovada
- [ ] Criar Optimizer.Core
- [ ] Criar Optimizer.Application
- [ ] Criar Optimizer.Windows
- [ ] Criar Optimizer.Persistence
- [ ] Criar Optimizer.DevTools
- [ ] Criar projetos de testes
```

Depois, features aprovadas poderão gerar itens específicos.

---

# 16. DevTools Backlog

```text
## DevTools

- [ ] Definir comandos base
- [ ] Implementar list features
- [ ] Implementar detect
- [ ] Implementar plan
- [ ] Implementar dry-run
- [ ] Implementar apply
- [ ] Implementar verify
- [ ] Implementar rollback
- [ ] Implementar test
```

A implementação deve reutilizar a mesma lógica do produto.

---

# 17. Frontend Backlog

```text
## Frontend

- [ ] Criar AppShell
- [ ] Criar Navigation
- [ ] Implementar Design System
- [ ] Criar componentes compartilhados
- [ ] Criar MockFeatureProvider
- [ ] Implementar primeira tela com mocks
- [ ] Validar visualmente
- [ ] Implementar telas restantes gradualmente
- [ ] Integrar backend real
```

---

# 18. Testing Backlog

```text
## Testing

- [ ] Definir estrutura dos testes unitários
- [ ] Definir testes de contrato
- [ ] Configurar ambiente de VM
- [ ] Definir PC/hardware de teste
- [ ] Criar matriz de compatibilidade
- [ ] Definir estratégia de testes privilegiados
- [ ] Definir estratégia para reboot tests
- [ ] Definir regressão por feature
```

---

# 19. Decisions Backlog

Itens explicitamente ainda abertos:

```text
## Decisions

- [ ] Windows 10 support
- [ ] Minimum Windows version
- [ ] .NET version
- [ ] Elevation model
- [ ] Single vs multiple processes
- [ ] IPC, se necessário
- [ ] Logging framework
- [ ] Packaging
- [ ] Updater
- [ ] Telemetry
- [ ] Privacy model
- [ ] Snapshot retention
- [ ] Session history
- [ ] First release scope
```

Ao decidir, registrar em `06-DECISIONS.md`.

---

# 20. Deferred

Usar para itens válidos que conscientemente não serão trabalhados agora.

```text
## Deferred

<!--
- [ ] Feature X — motivo
-->
```

Não usar `Deferred` apenas para esconder backlog excessivo.

---

# 21. Rejected / Cancelled

Itens rejeitados devem permanecer registrados quando houver valor histórico.

```text
## Rejected / Cancelled

<!--
- [x] Tweak X
  Reason: sem benefício técnico comprovável.
-->
```

Isso evita pesquisar e discutir novamente a mesma ideia sem perceber.

---

# 22. Bugs

Quando a implementação começar:

```text
## Bugs

<!--
- [ ] [P0] [BUG] Rollback de Feature X não restaura estado ausente
-->
```

Bugs devem ter reprodução e critério de correção em issue/tarefa própria quando necessário.

---

# 23. Research Questions

Questões importantes que ainda não viraram decisão:

```text
## Research Questions

- [ ] Quais operações exigirão administrador?
- [ ] Quais features exigirão reboot?
- [ ] Quais features não funcionam adequadamente em VM?
- [ ] Quais APIs de GPU serão necessárias?
- [ ] Qual nível de suporte a monitor físico será possível?
- [ ] Quais configurações possuem fonte de verdade confiável?
```

Adicionar perguntas conforme discovery avança.

---

# 24. Definition of Ready

Antes de uma tarefa de implementação relevante entrar em `IN_PROGRESS`, verificar quando aplicável:

```text
[ ] objetivo definido
[ ] feature especificada
[ ] arquitetura responsável conhecida
[ ] dependências conhecidas
[ ] critérios de aceitação definidos
[ ] estratégia de teste definida
```

Se não estiver pronta, voltar para pesquisa ou especificação.

---

# 25. Definition of Done

Uma tarefa não está concluída apenas porque compila.

Quando aplicável:

```text
[ ] implementação concluída
[ ] testes relevantes passam
[ ] erro tratado
[ ] logs adequados
[ ] documentação atualizada
[ ] nenhum TODO crítico foi deixado silenciosamente
[ ] critérios de aceitação cumpridos
```

---

# 26. Limite de trabalho simultâneo

Evitar abrir muitas features em paralelo.

Durante especificação/prototipação, preferir poucas features em progresso simultaneamente.

Objetivo:

```text
finish
before
starting too much
```

Isso reduz trabalho abandonado pela metade.

---

# 27. Regra para agentes

Um item grande do backlog deve ser quebrado antes de ser entregue a um agente.

Exemplo ruim:

```text
Implementar sistema de energia
```

Exemplo melhor:

```text
1. implementar abstração de detecção
2. testar parser
3. implementar ChangePlan
4. implementar dry-run
5. implementar Apply
6. implementar Verify
7. implementar Rollback
```

Somente quebrar dessa forma depois que a feature estiver especificada.

---

# 28. Atualização do backlog

Quando uma tarefa for concluída:

- marcar;
- adicionar próxima tarefa relevante;
- registrar decisão, se houver;
- atualizar especificação, se mudou comportamento.

Não manter tarefas concluídas como se ainda estivessem abertas.

---

# 29. Backlog não é roadmap

Backlog responde:

> O que existe para fazer?

Roadmap responde:

> O que pretendemos entregar e quando?

Ainda não existe necessidade de roadmap de releases.

Ele poderá ser criado depois que o escopo da primeira versão for definido.

---

# 30. Próximo passo atual

Com a documentação-base concluída, o próximo trabalho real é:

```text
DISCOVERY
```

Começar por uma área e construir o catálogo inicial conforme `01-DISCOVERY.md`.

Sugestão inicial:

```text
Sistema
```

Depois avançar gradualmente pelas demais áreas.

---

# 31. Princípio final

O backlog deve reduzir ansiedade, não aumentá-la.

Ele existe para permitir que a equipe pense:

> Não preciso fazer tudo agora. Preciso saber qual é o próximo trabalho correto.

Sempre priorizar terminar uma etapa bem compreendida antes de abrir dezenas de tarefas novas.