# PC Optimizer V2 — UX Flow

## 1. Objetivo deste documento

Este documento define os princípios de experiência e fluxo da V2.

O foco aqui não é definir cores, componentes, tamanhos ou layouts finais.

O objetivo é preservar a lógica de navegação e a sensação de continuidade do produto antes da implementação visual.

A interface deve ser construída em torno da ideia de que o usuário percorre uma jornada contínua, e não uma coleção de ferramentas isoladas.

---

# 2. Conceito central

A experiência deve funcionar como um **rio**.

Isso significa:

- uma etapa conduz naturalmente à próxima;
- o usuário entende onde está;
- o usuário entende o que aconteceu antes;
- o usuário entende o que vem depois;
- decisões importantes aparecem no momento certo;
- o fluxo não deve parecer fragmentado.

Evitar sensação de:

```text
menu
→ ferramenta
→ voltar
→ outra ferramenta
→ voltar
→ outra ferramenta
```

Preferir:

```text
análise
↓
entendimento
↓
recomendação
↓
decisão
↓
preview
↓
execução
↓
validação
↓
resultado
```

---

# 3. Filosofia de interação

A interface deve reduzir esforço cognitivo.

O usuário não deve precisar entender detalhes técnicos para usar o aplicativo.

Ao mesmo tempo, o app deve ser transparente o suficiente para usuários avançados.

A UX deve equilibrar:

- simplicidade;
- transparência;
- controle;
- profundidade opcional.

---

# 4. Dois níveis de informação

Sempre que possível:

## Nível principal

Mostrar apenas o necessário para decidir.

Exemplo:

```text
Desativar suspensão seletiva USB

Recomendado para seu perfil

Pode reduzir problemas de energia/latência em alguns dispositivos.

Risco: baixo
Reinício: não
```

## Nível avançado

Detalhes opcionais:

```text
estado atual
estado alvo
método técnico
configurações afetadas
motivo da recomendação
rollback
```

O usuário não deve ser obrigado a ler detalhes técnicos.

---

# 5. Jornada conceitual

Estrutura inicial:

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

---

# 6. Descobrir

Objetivo:

entender o computador atual.

Possíveis atividades:

- detectar hardware;
- detectar Windows;
- identificar configurações;
- medir estado;
- localizar problemas;
- identificar oportunidades.

Pergunta respondida:

> O que existe neste PC e o que pode precisar de atenção?

---

# 7. Entender

Objetivo:

transformar dados técnicos em informação útil.

Exemplo:

```text
Detectado:
USB selective suspend ativado

Contexto:
Desktop focado em jogos

Interpretação:
Pode fazer sentido avaliar desativação
```

Pergunta respondida:

> O que esses dados significam?

---

# 8. Escolher

Objetivo:

permitir que o usuário selecione o que deseja alterar.

A interface deve distinguir:

```text
Recommended
Optional
Not Recommended
Unsupported
Already Optimized
```

Pergunta respondida:

> O que eu quero fazer?

---

# 9. Preparar

Antes da execução, mostrar o plano.

Exemplo:

```text
12 alterações selecionadas

3 exigem administrador
2 exigem reinício
1 possui risco médio

Ponto de restauração será criado
Rollback disponível
```

Pergunta respondida:

> O que exatamente vai acontecer?

---

# 10. Executar

Durante aplicação:

- mostrar progresso;
- mostrar etapa atual;
- não sobrecarregar com detalhes;
- permitir detalhes técnicos quando necessário.

Exemplo:

```text
Aplicando otimizações

7 / 12 concluídas

Energia
Desativando suspensão seletiva USB
```

---

# 11. Confirmar

Depois de aplicar:

- verificar resultados;
- mostrar sucesso;
- mostrar falhas;
- mostrar alterações parciais;
- indicar reboot.

Pergunta respondida:

> Funcionou?

---

# 12. Manter

Após a sessão:

- informar estado;
- permitir rollback quando aplicável;
- indicar reinício pendente;
- mostrar problemas encontrados;
- permitir nova análise.

Pergunta respondida:

> E agora?

---

# 13. Possível fluxo principal

Exemplo conceitual:

```text
Home
↓
Analyze PC
↓
Analysis Result
↓
Recommendations
↓
Category Review
↓
Optimization Summary
↓
Create Restore Point
↓
Apply
↓
Verify
↓
Results
```

A estrutura final ainda não está definida.

---

# 14. Fluxo não precisa ser totalmente linear

“Rio” não significa impedir navegação.

O usuário pode:

- voltar;
- revisar;
- pular;
- abrir detalhes;
- alterar seleção.

Mas o caminho principal deve continuar evidente.

---

# 15. Navegação global

A aplicação pode possuir acesso lateral ou superior a áreas permanentes.

Exemplos:

```text
Home
Optimization
Diagnostics
Tools
History
Settings
```

Isso não deve destruir o fluxo principal.

A navegação global serve para acesso direto.

O fluxo de otimização continua sendo uma jornada.

---

# 16. Categorias não são telas obrigatórias

Uma categoria técnica como:

```text
Energy
Network
System
```

não precisa necessariamente virar uma tela isolada.

Ela pode aparecer como:

- seção;
- etapa;
- agrupamento;
- filtro;
- bloco.

A estrutura visual será decidida depois.

---

# 17. Organização por intenção

Priorizar linguagem orientada ao usuário.

Internamente pode existir:

```text
Registry
Power
Services
```

Mas a UI pode apresentar:

```text
Performance
Energy
Gaming
Stability
```

Não expor organização técnica apenas porque é mais fácil para o backend.

---

# 18. Estado das features

A UI deverá lidar com estados claros.

Exemplos:

```text
Analyzing
Recommended
Optional
Already Applied
Unsupported
Unavailable
Applying
Applied
Failed
Rollback Available
Restart Required
```

Esses estados devem vir do backend através de contratos.

---

# 19. Recomendações

Uma recomendação deve responder:

```text
O que?
Por quê?
Qual benefício?
Qual risco?
Precisa reiniciar?
Pode desfazer?
```

Evitar:

```text
Optimize now
```

sem explicação.

---

# 20. Confiança

Quando aplicável, o app pode indicar confiança.

Exemplo:

```text
Strongly Recommended
Recommended
Situational
Optional
Not Recommended
```

Ou estrutura equivalente.

Isso será refinado depois.

---

# 21. Linguagem

Evitar promessas absolutas.

Ruim:

```text
Increase FPS
Fix latency
Boost internet
```

Preferir:

```text
Pode reduzir...
Pode melhorar...
Recomendado neste cenário...
```

Quando o benefício for comprovável, ser específico.

---

# 22. Transparência

O usuário deve poder descobrir o que será alterado.

Exemplo:

```text
View technical details
```

Pode mostrar:

```text
Current state
Target state
Changes
Rollback
```

Detalhes técnicos não precisam ficar visíveis por padrão.

---

# 23. Preview

Antes de alterações relevantes:

```text
Preview Changes
```

deve utilizar o mesmo `ChangePlan` usado pelo backend.

Isso evita inconsistência entre:

```text
o que a UI diz
```

e:

```text
o que o backend executa
```

---

# 24. Dry-run e UX

O dry-run pode alimentar o preview.

Fluxo:

```text
User selects features
↓
Backend builds plan
↓
UI renders plan
↓
User confirms
↓
Apply same plan
```

Esse é o comportamento desejado.

---

# 25. Falhas

Falhas devem ser apresentadas de forma compreensível.

Evitar mostrar:

```text
HRESULT 0x80070005
```

como mensagem principal.

Preferir:

```text
Não foi possível aplicar esta alteração porque o Windows bloqueou a operação.
```

Detalhes técnicos podem aparecer em uma área expandida.

---

# 26. Falha parcial

Se 10 de 12 alterações funcionarem:

não mostrar simplesmente:

```text
Optimization failed
```

Mostrar:

```text
10 concluídas
2 falharam
```

E explicar:

- quais;
- por quê;
- se podem ser tentadas novamente;
- se rollback ocorreu.

---

# 27. Reinício

Quando houver reboot:

a UX deve deixar claro:

- o que já foi aplicado;
- o que depende do reboot;
- se a sessão será retomada;
- o que acontecerá depois.

Evitar reiniciar sem contexto.

---

# 28. Ponto de restauração

A criação pode aparecer como parte natural da preparação.

Exemplo:

```text
Preparando otimização
✓ Verificando sistema
✓ Criando ponto de restauração
✓ Salvando estados atuais
```

Não precisa virar uma ferramenta separada no fluxo principal.

---

# 29. Usuário avançado

Pode existir futuramente:

```text
Advanced Mode
```

Possíveis recursos:

- detalhes completos;
- alterações individuais;
- comportamento técnico;
- filtros;
- configurações experimentais.

O modo básico não deve depender dele.

---

# 30. Usuário iniciante

O caminho principal deve exigir poucas decisões técnicas.

Exemplo:

```text
Recommended
Optional
Skip
```

Sem exigir conhecimento de Registry, serviços ou powercfg.

---

# 31. Design progressivo

Mostrar complexidade conforme necessária.

Exemplo:

```text
Card
↓
Details
↓
Technical Details
```

Não colocar tudo na primeira camada.

---

# 32. Estados de loading

Análise real pode demorar.

A interface deve mostrar progresso útil.

Evitar apenas:

```text
Loading...
```

Preferir:

```text
Analisando hardware
Verificando energia
Verificando armazenamento
```

Sem fingir precisão falsa.

---

# 33. Percepção de progresso

O fluxo deve transmitir avanço.

Pode utilizar:

- step indicator;
- progresso;
- transições;
- mudança de contexto;
- resumo da etapa.

A solução visual será definida no design.

---

# 34. Transições

Transições devem reforçar continuidade.

Não usar animações apenas como decoração.

Elas podem mostrar:

```text
uma etapa terminou
↓
a próxima começou
```

O movimento deve apoiar a metáfora do rio.

---

# 35. Persistência visual

Se o usuário sair de uma etapa e voltar:

a seleção e contexto não devem desaparecer inesperadamente.

A UX precisa considerar estado da sessão.

---

# 36. Interrupção

Se o app fechar no meio de uma sessão:

a experiência futura deve considerar:

```text
Existe uma sessão incompleta
```

Possível comportamento:

```text
Retomar
Revisar
Rollback
Descartar quando seguro
```

Isso depende da arquitetura de recovery.

---

# 37. Cancelamento

Nem toda etapa deve poder ser cancelada.

Exemplo:

```text
analysis
→ cancelável
```

Mas:

```text
writing critical system configuration
```

pode não ser seguro cancelar no meio.

A UI deve refletir essa diferença.

---

# 38. Feedback imediato

Toda ação deve produzir feedback.

Exemplo:

```text
Selected
Applied
Pending
Requires Restart
Failed
```

Evitar cliques sem resposta perceptível.

---

# 39. Consistência

Uma feature deve parecer e se comportar como outras features equivalentes.

Exemplo:

todo card de Optimization deve apresentar informações similares.

Isso reduz aprendizado do usuário.

---

# 40. Design system

Antes da implementação definitiva, extrair do protótipo:

```text
Buttons
Cards
Headers
Navigation
Progress
Warnings
Recommendations
Feature States
Dialogs
```

Esses componentes devem ser reutilizados.

---

# 41. Stitch

Google Stitch será utilizado como ferramenta de:

- exploração;
- prototipação;
- teste visual;
- definição de fluxo;
- criação de referência.

O objetivo não é obrigatoriamente usar o código exportado diretamente.

---

# 42. Processo Stitch → App

Fluxo:

```text
UX definido
↓
Stitch
↓
Prototipação
↓
Validação
↓
Export de referência
↓
Extração do design system
↓
Implementação nativa
↓
Mocks
↓
Comparação visual
↓
Integração real
```

---

# 43. Conversão por etapas

Evitar:

```text
Converter todo o frontend de uma vez
```

Preferir:

```text
AppShell
↓
Navigation
↓
Core Components
↓
Uma tela
↓
Validar
↓
Próxima tela
```

Isso reduz erros e inconsistências.

---

# 44. Mocks

O frontend deve ser desenvolvido inicialmente com dados simulados.

Exemplo:

```text
MockFeatureProvider
```

Deve retornar os mesmos contratos do backend real.

Isso permite testar:

- fluxo;
- estados;
- erros;
- carregamento;
- recomendações;
- resultados.

---

# 45. Cenários obrigatórios no protótipo

O design não deve mostrar apenas caminho feliz.

Criar estados para:

```text
analyzing
no recommendations
recommendations found
unsupported feature
admin required
restart required
partial failure
full success
rollback available
```

---

# 46. Tela vazia

Definir comportamento quando nada for encontrado.

Exemplo:

```text
Seu PC já está bem configurado para este perfil.
```

Não fabricar recomendações apenas para preencher a interface.

---

# 47. Perfis

Pode existir futuramente conceito de perfil:

```text
Gaming
Battery
Balanced
Workstation
Custom
```

Ainda não está decidido.

Se adotado, perfis devem influenciar recomendação, não criar scripts opacos.

---

# 48. Personalização

O usuário pode ter preferências que mudem recomendações.

Exemplo:

```text
priorizar bateria
priorizar desempenho
priorizar silêncio
```

Esse conceito deve ser avaliado na fase de produto.

---

# 49. Explicação de risco

Risco deve ser apresentado em linguagem útil.

Não apenas:

```text
Medium
```

Mas possivelmente:

```text
Esta alteração afeta gerenciamento de energia e pode aumentar consumo.
```

A classificação visual pode continuar existindo.

---

# 50. Confirmações

Não pedir confirmação para cada alteração pequena.

Isso destrói fluidez.

Concentrar confirmação em momentos relevantes:

```text
Preview
↓
Confirm selected plan
```

Features de risco elevado podem exigir confirmação adicional.

---

# 51. Evitar excesso de modal

Modais interrompem o fluxo.

Usar apenas quando:

- decisão bloqueia continuação;
- risco exige atenção;
- erro exige ação.

Preferir conteúdo integrado à jornada.

---

# 52. Histórico

Se houver histórico futuro, ele não deve ser obrigatório para o fluxo principal.

Pode servir para:

- sessões anteriores;
- alterações;
- resultados;
- troubleshooting.

A necessidade ainda será avaliada.

---

# 53. Home

A Home não precisa ser um painel cheio de métricas.

Ela deve responder rapidamente:

```text
Qual é o estado do meu PC?
O que posso fazer agora?
```

A definição final depende do design.

---

# 54. Ferramentas independentes

Algumas Tools podem não fazer parte do fluxo principal.

Exemplos conceituais:

```text
pixel test
color calibration
repair tool
diagnostic utility
```

Podem existir em área separada sem quebrar o conceito do rio.

---

# 55. Fluxo principal vs ferramentas

Separar:

```text
Core Optimization Journey
```

de:

```text
Standalone Tools
```

Isso evita tentar encaixar tudo artificialmente na mesma sequência.

---

# 56. Acessibilidade

O design deve considerar:

- contraste;
- escala;
- navegação por teclado;
- leitores de tela quando aplicável;
- animações excessivas;
- foco visual.

Não sacrificar acessibilidade pela estética.

---

# 57. Performance da UI

A interface deve permanecer responsiva durante análises e execução.

Operações longas devem ocorrer fora da thread da UI.

Estados devem ser atualizados progressivamente.

---

# 58. Erros técnicos

A UI deve possuir:

```text
User message
Technical details
Error code
```

Exemplo:

```text
Não foi possível alterar o plano de energia.

Código: ADMIN_REQUIRED

Detalhes técnicos
[expandir]
```

---

# 59. Estado offline

O aplicativo deve avaliar quais funcionalidades dependem de internet.

Quando não houver:

```text
feature unavailable due to network
```

não deve parecer falha interna.

---

# 60. Telemetria

Se existir futuramente:

- não deve ser necessária para o fluxo básico;
- precisa respeitar privacidade;
- precisa ser documentada.

Decisão ainda aberta.

---

# 61. Critério para fechar o fluxo

Antes de iniciar o frontend definitivo, devemos conseguir responder:

```text
[ ] Como o usuário começa?
[ ] Como ocorre análise?
[ ] Como recomendações aparecem?
[ ] Como o usuário seleciona?
[ ] Como vê o preview?
[ ] Como confirma?
[ ] Como acompanha execução?
[ ] Como recebe resultado?
[ ] Como erros aparecem?
[ ] Como reboot aparece?
[ ] Como rollback aparece?
[ ] Como ferramentas independentes são acessadas?
```

---

# 62. Critério para iniciar Stitch

Não é necessário conhecer todas as features.

Mas precisamos ter:

```text
[ ] categorias suficientemente maduras
[ ] jornada principal
[ ] principais estados
[ ] dados esperados do backend
[ ] ações do usuário
```

Depois disso, o design pode começar.

---

# 63. O que não precisa estar decidido antes do Stitch

Ainda pode mudar:

- cor;
- tipografia final;
- tamanho;
- posição;
- animações;
- organização visual específica;
- componentes detalhados.

Stitch serve justamente para explorar isso.

---

# 64. Regra de validação do protótipo

Ao revisar uma tela, perguntar:

```text
Eu sei onde estou?

Eu sei o que aconteceu?

Eu sei o que devo fazer agora?

Eu sei o que acontecerá se continuar?

A informação mais importante está evidente?

Existe complexidade desnecessária?

Essa tela leva naturalmente para a próxima?
```

---

# 65. Regra contra “dashboarditis”

Não preencher a UI com:

- gráficos;
- scores;
- números;
- indicadores;

apenas para parecer sofisticado.

Todo elemento deve ajudar o usuário a entender ou decidir algo.

---

# 66. Score geral

Se futuramente existir algo como:

```text
PC Score: 82
```

deve possuir metodologia defensável.

Não criar score arbitrário.

---

# 67. Comparação antes/depois

Quando houver medição confiável:

```text
Before
After
```

pode ser apresentada.

Quando não houver:

não inventar porcentagem de melhoria.

---

# 68. Resultado honesto

É aceitável mostrar:

```text
Configuração aplicada com sucesso.
Impacto real depende da carga e do hardware.
```

Transparência é preferível a promessas exageradas.

---

# 69. Princípio final

O design deve servir à jornada.

Não construir primeiro telas bonitas e depois tentar encaixar o produto nelas.

Fluxo correto:

```text
Produto
↓
Jornada
↓
Estados
↓
Informação
↓
Design
↓
Implementação
```

A estética pode mudar.

O coração do produto — continuidade, clareza e sensação de progresso — deve permanecer.