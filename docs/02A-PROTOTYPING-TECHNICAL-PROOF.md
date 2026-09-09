# BeadWise / PC Optimizer V2 — Prototipagem e Prova Técnica Automatizada das Feature Specs

## 1. Objetivo deste documento

Este documento define a forma de execução da etapa prática da Fase 2 do BeadWise / PC Optimizer V2.

Ele deve ser usado em conjunto com:

- `02-FEATURE-SPEC-TEMPLATE.md` — regra canônica da Fase 2;
- as Feature Specs maduras em `feature-specs/`;
- `03-ARCHITECTURE.md` — apenas como limite e contexto da próxima fase;
- `prototypes/` — diretório exclusivo para provas técnicas.

O objetivo desta etapa não é implementar o backend final.

O objetivo é reduzir incerteza técnica por meio de:

```text
pesquisa
↓
protótipo isolado
↓
execução real
↓
teste
↓
observação
↓
correção da Feature Spec
↓
reteste quando necessário
↓
classificação de maturidade baseada em evidência
```

A regra principal continua sendo:

```text
não tentar promover uma feature de status

tentar descobrir qual status ela realmente merece
```

---

# 2. Autoridade dos documentos

Usar a seguinte ordem de autoridade:

## 1. `02-FEATURE-SPEC-TEMPLATE.md`

É a regra canônica desta etapa.

Ele define:

- estados de maturidade;
- critérios para `PROVEN`;
- critérios para `APPROVED`;
- detecção;
- ChangePlan;
- dry-run;
- Snapshot;
- Apply;
- Verify;
- Rollback;
- Verify Rollback;
- compatibilidade;
- risco;
- falhas;
- logging;
- persistência;
- testes;
- prova técnica;
- evidências;
- benefício real;
- impacto mensurável;
- contrato necessário para o frontend.

Não criar metodologia paralela.

## 2. Feature Specs atuais

São a fonte canônica para o comportamento técnico já investigado.

Preferir sempre:

```text
ler
→ investigar
→ prototipar
→ testar
→ corrigir
→ validar
```

e não:

```text
reescrever do zero
```

## 3. Discoverys finais

Consultar apenas quando necessário para recuperar:

- intenção original;
- problema que a capability deveria resolver;
- fronteiras;
- ownership;
- relações;
- decisões já consolidadas.

Não reabrir decisões arbitrariamente.

## 4. `03-ARCHITECTURE.md`

Usar somente como:

- limite da Fase 2;
- contexto para não tomar decisões prematuras;
- fonte de requisitos que dependem de provas técnicas.

Não iniciar a Fase 3 durante este trabalho.

---

# 3. Ambiente de execução

O agente está autorizado a trabalhar diretamente em uma máquina Windows real com acesso ao projeto.

Pode:

- criar arquivos;
- editar Feature Specs;
- criar protótipos;
- compilar;
- executar comandos;
- executar PowerShell;
- usar .NET;
- usar Win32/PInvoke;
- consultar Registry;
- usar WMI/CIM;
- usar Performance Counters;
- usar ETW;
- consultar APIs de fabricante quando disponíveis;
- executar testes;
- inspecionar logs;
- fazer iteração sobre protótipos;
- registrar resultados;
- fazer commits quando apropriado.

A existência de acesso à máquina não transforma automaticamente um teste em evidência universal.

Sempre distinguir:

```text
funcionou neste ambiente
```

de:

```text
é compatível em todos os ambientes suportados
```

---

# 4. Estrutura de prototypes

Usar:

```text
prototypes/
└── <domain>/
    └── <capability-id>/
        ├── README.md
        ├── src/
        ├── scripts/
        ├── tests/
        ├── results/
        └── artifacts/
```

Criar somente as subpastas realmente necessárias.

Exemplo:

```text
prototypes/
└── energy/
    └── C-ENERGY-004/
        ├── README.md
        ├── src/
        └── results/
```

## README do protótipo

Cada prototype criado deve explicar no mínimo:

```text
Feature:
ID:
Objetivo da prova:
Premissa central:
Ambiente:
Pré-requisitos:
Risco:
Método:
Como executar:
Resultado esperado:
Resultado observado:
Limitações:
```

---

# 5. Regra de automação

Esta etapa deve ser executada automaticamente.

O agente NÃO deve pedir autorização:

- entre capabilities do mesmo domínio;
- entre testes normais;
- entre correções documentais;
- entre protótipos;
- entre revisões;
- entre domínios.

Fluxo:

```text
domínio
↓
ler todas as specs
↓
classificar
↓
priorizar
↓
executar provas necessárias
↓
atualizar specs
↓
revisão cruzada
↓
corrigir
↓
próximo domínio
```

Continuar automaticamente até concluir todos os domínios.

---

# 6. Ordem de trabalho

Preservar a ordem real dos domínios em `feature-specs/`.

Não reorganizar a Fase 2 por tecnologia.

Não criar lotes artificiais como:

```text
Win32 batch
Registry batch
WMI batch
PowerShell batch
```

A unidade principal de trabalho é:

```text
domínio
→ Feature Specs do domínio
```

Tecnologias são apenas mecanismos utilizados dentro das provas.

---

# 7. Classificação inicial por domínio

Ao iniciar um domínio:

1. ler todas as Feature Specs;
2. mapear status atual;
3. localizar `TBD`;
4. localizar `NOT_RUN`;
5. localizar questões em aberto;
6. identificar mutações;
7. identificar riscos;
8. identificar dependências;
9. identificar requisitos de hardware;
10. identificar alegações de benefício;
11. identificar necessidade de medição;
12. identificar compatibilidade ainda não demonstrada.

Prioridade:

```text
RESEARCH
↓
SPECIFIED sem prova
↓
TBD crítico
↓
rollback incerto
↓
fonte da verdade incerta
↓
Apply/Verify incerto
↓
alegação de benefício sem medição
↓
compatibilidade não comprovada
```

---

# 8. Fluxo obrigatório por Feature Spec

Para cada feature:

## Etapa A — Leitura

Ler a spec integralmente.

Identificar exatamente:

```text
o que já sabemos
o que apenas assumimos
o que está documentado
o que precisa ser observado
o que precisa ser medido
o que pode falhar
```

## Etapa B — Pergunta de prova

Definir a menor pergunta técnica capaz de reduzir a incerteza principal.

Exemplos:

```text
A API retorna o estado real?
A alteração persiste?
O estado efetivo pode ser validado depois do Apply?
O rollback restaura exatamente o estado anterior?
A configuração é por máquina, usuário, dispositivo ou perfil?
Há diferença após reboot?
O comportamento muda entre hardware vendors?
```

## Etapa C — Pesquisa

Priorizar:

1. Microsoft Learn / documentação oficial Windows;
2. AMD / Intel / NVIDIA;
3. fabricante de hardware;
4. especificação oficial;
5. documentação de API;
6. documentação técnica confiável.

Comunidades podem levantar hipóteses, mas não substituir evidência primária.

Registrar as fontes na própria Feature Spec.

## Etapa D — Decisão sobre prototype

Criar prototype somente quando a incerteza não puder ser resolvida adequadamente apenas por documentação.

O prototype deve testar uma premissa concreta.

Não criar prototype por burocracia.

## Etapa E — Execução

Executar o teste no Windows real quando apropriado.

Registrar:

```text
entrada
estado inicial
comando/API
exit code
stdout/stderr quando aplicável
estado observado
efeitos colaterais
reboot/logoff/restart quando necessário
```

## Etapa F — Comparação

Comparar:

```text
spec esperava X
realidade apresentou Y
```

Se `X != Y`:

```text
corrigir a Feature Spec
```

Nunca ajustar a interpretação do teste apenas para preservar a spec.

## Etapa G — Reteste

Se a spec foi alterada de forma que afete a prova:

```text
retestar
```

quando tecnicamente necessário.

## Etapa H — Status

Alterar status apenas quando a evidência justificar.

---

# 9. Features diagnósticas

Para features puramente diagnósticas não forçar:

```text
Apply
Snapshot
Rollback
System Restore
```

Usar `N/A` quando correto.

Fluxo típico:

```text
Detect
↓
fonte da verdade
↓
normalização
↓
interpretação
↓
erro / unsupported / unknown
```

Validar:

- resultados válidos;
- ausência de dado;
- unsupported;
- access denied;
- múltiplos dispositivos;
- hardware incomum;
- valores inconsistentes;
- diferença entre fontes;
- interpretação incorreta.

---

# 10. Features mutáveis

Para qualquer feature que altere o sistema, validar quando aplicável:

```text
Detect
↓
Snapshot
↓
ChangePlan
↓
Dry-run
↓
Apply
↓
Verify
↓
Keep / Rollback
↓
Verify Rollback
```

## Snapshot

Deve capturar o estado original real.

Também registrar inexistência.

Exemplo:

```text
Registry value did not exist
```

Rollback nesse caso pode exigir remoção e não escrita de default.

## ChangePlan

Dry-run e Apply devem derivar do mesmo plano lógico.

## Dry-run

Não pode causar alteração persistente.

## Apply

Registrar o que realmente mudou.

## Verify

Não aceitar apenas:

```text
exit code = 0
```

Reler o estado efetivo.

## Rollback

Restaurar:

```text
estado original
```

não:

```text
default presumido
```

## Verify Rollback

Comparar contra snapshot.

---

# 11. Operações de maior risco

Exigir rigor extra para features relacionadas a:

- boot;
- BCD;
- WinRE;
- drivers;
- serviços essenciais;
- armazenamento;
- filesystem;
- BitLocker;
- Windows Update;
- rede;
- firewall;
- segurança;
- dispositivos;
- energia;
- GPU tuning;
- clocks;
- temperatura;
- firmware;
- virtualização;
- dados do usuário.

Automação não significa executar uma ação sem método de recuperação conhecido.

Se uma prova destrutiva não puder ser executada de forma suficientemente segura:

```text
não improvisar
```

Registrar na spec:

```text
BLOCKED
DEFERRED
RESEARCH
```

conforme aplicável, com motivo concreto.

---

# 12. System Restore

System Restore não substitui rollback específico.

Usar quando a spec exigir:

```text
Required
Recommended
```

mas ainda preservar:

```text
Snapshot
Rollback
Verify Rollback
```

quando aplicáveis.

---

# 13. Reboot e logoff

Quando a feature exigir reboot/logoff/restart:

1. persistir resultado intermediário;
2. registrar estado antes do reboot;
3. executar reboot quando tecnicamente apropriado;
4. retomar a prova;
5. executar Verify;
6. continuar a rotina automaticamente.

Não declarar uma feature `PROVEN` se o comportamento pós-reboot for essencial e não tiver sido testado.

---

# 14. Compatibilidade

Uma única máquina não prova compatibilidade universal.

Na máquina atual registrar detalhadamente:

```text
Windows edition:
Windows version:
Windows build:
Architecture:
CPU:
GPU:
Chipset:
Motherboard:
Laptop/Desktop:
Drivers relevantes:
Admin:
Virtualization:
Date:
```

Distinguir:

```text
TESTED
EXPECTED
UNTESTED
UNSUPPORTED
UNKNOWN
```

Não escrever simplesmente:

```text
Windows 10/11 supported
```

sem evidência suficiente.

---

# 15. Testes mínimos

Quando aplicável, cobrir:

```text
[ ] já está no estado desejado
[ ] precisa ser alterado
[ ] não suportado
[ ] falta permissão
[ ] Apply falha
[ ] Verify falha
[ ] Rollback funciona
[ ] Rollback falha
[ ] estado inicial personalizado
[ ] configuração inexistente
[ ] execução repetida
```

Nem todo cenário precisa ser artificialmente provocado se isso introduzir risco desproporcional.

Quando um cenário não puder ser executado:

```text
NOT_TESTED
```

com justificativa.

Nunca registrar:

```text
PASS
```

sem teste real.

---

# 16. Idempotência

Para features mutáveis testar quando apropriado:

```text
Apply
↓
Apply novamente
```

Verificar que a segunda execução não:

- cria entradas duplicadas;
- degrada estado;
- perde snapshot;
- altera algo adicional;
- muda estado inesperadamente.

---

# 17. Magic values e otimizações

Nenhum valor recomendado deve ser aceito sem:

```text
origem
justificativa
condição
ou mecanismo adaptativo
```

Especialmente:

- timers;
- scheduler;
- CPU;
- GPU;
- network;
- cache;
- services;
- power policy;
- registry tweaks;
- hidden flags.

Para alegações de performance:

```text
qual comportamento muda?
↓
por que ajudaria?
↓
em qual workload?
↓
como medir?
↓
pode piorar?
↓
o Windows já gerencia isso?
```

Sem resposta suficiente:

```text
não aprovar como otimização
```

---

# 18. Medição

Quando a feature alegar benefício mensurável, utilizar método apropriado.

Exemplos:

```text
FPS
frametime
latency
boot time
energy
temperature
resource usage
disk space
error resolved
stability
```

Sempre que possível:

```text
baseline
↓
Apply
↓
same workload
↓
measurement
↓
comparison
```

Evitar benchmark sem repetibilidade.

Registrar ruído e limitações.

---

# 19. Evidência documental vs observada

Separar claramente na Feature Spec:

```text
DOCUMENTED BEHAVIOR
```

de:

```text
OBSERVED BEHAVIOR
```

Uma documentação oficial pode provar existência/contrato de API.

Ela não prova que o prototype foi executado corretamente.

Um teste nesta máquina prova comportamento observado neste ambiente.

Ele não prova automaticamente compatibilidade global.

---

# 20. Critério para SPECIFIED

Usar quando o comportamento necessário está definido o suficiente para permitir uma prova técnica.

Não significa testado.

Uma spec pode permanecer `SPECIFIED` após a campanha se:

- o prototype necessário não puder ser executado;
- a matriz de hardware necessária não estiver disponível;
- houver cenário ainda não testado essencial para `PROVEN`.

---

# 21. Critério para PROTOTYPING

Usar enquanto:

- o prototype está sendo criado;
- testes estão sendo executados;
- comportamento está sendo investigado;
- resultados ainda não foram consolidados.

Ao terminar o trabalho da feature:

- voltar para `RESEARCH`, se a premissa falhar ou continuar incerta;
- usar `SPECIFIED`, se a definição ficou pronta mas prova ainda é insuficiente;
- usar `PROVEN`, somente se o gate aplicável for satisfeito.

---

# 22. Critério para PROVEN

Usar somente quando, quando aplicável:

```text
[ ] Detect funciona
[ ] ChangePlan representa corretamente a alteração
[ ] Dry-run não altera o sistema
[ ] Apply funciona
[ ] Verify confirma o estado real
[ ] Snapshot captura corretamente o estado anterior
[ ] Rollback restaura o estado anterior
[ ] Verify Rollback confirma a restauração
[ ] riscos principais foram identificados
[ ] compatibilidade mínima foi testada
[ ] falhas conhecidas estão documentadas
```

Uma feature diagnóstica não precisa satisfazer operações que não se aplicam.

Nunca promover por porcentagem de campos preenchidos.

---

# 23. Gate para APPROVED

`PROVEN` não implica `APPROVED`.

Depois de `PROVEN`, avaliar:

```text
[ ] útil
[ ] confiável
[ ] dentro do escopo
[ ] risco aceitável
[ ] manutenção futura aceitável
[ ] UX consegue apresentar corretamente
[ ] não duplica outra feature
[ ] não depende de mecanismo frágil sem justificativa
```

Resultado pode ser:

```text
APPROVED
DEFERRED
REJECTED
```

Não aprovar por conveniência.

---

# 24. RESEARCH / BLOCKED / DEFERRED / REJECTED

## RESEARCH

Ainda existe incerteza técnica solucionável por investigação.

## BLOCKED

Existe bloqueio objetivo atual.

Exemplos:

- hardware indisponível;
- API necessária inacessível;
- dependência externa;
- ambiente de teste ausente.

## DEFERRED

É tecnicamente possível, mas não deve avançar agora.

Exemplos:

- custo de manutenção desproporcional;
- precisa de infraestrutura que pertence à fase futura;
- exige matriz de hardware não disponível nesta campanha.

## REJECTED

A premissa não se sustenta ou a feature não merece implementação.

Exemplos:

- tweak myth;
- ausência de benefício relevante;
- duplicação;
- risco maior que benefício;
- comportamento não suportado/frágil sem justificativa.

---

# 25. Revisão cruzada por domínio

Ao terminar cada domínio verificar:

- fonte da verdade consistente;
- ownership único;
- nenhuma mutação duplicada;
- Shared Capabilities não duplicadas;
- dependências sem ciclos;
- rollback compatível;
- nomenclatura consistente;
- estados de erro coerentes;
- sem arquitetura prematura;
- sem tweak myth;
- sem magic values injustificados;
- contratos coerentes;
- resultados de prova coerentes com status.

Corrigir as Feature Specs antes de seguir.

---

# 26. Revisão cruzada global

Depois de todos os domínios:

```text
duplicação de infraestrutura
ownership conflitante
dependências circulares
fontes da verdade inconsistentes
Apply/Rollback incompatíveis
contratos incompatíveis
mecanismos repetidos
responsabilidades sobrepostas
feature duplicada
benefício contraditório
status sem evidência suficiente
```

Corrigir diretamente as specs.

Não criar relatório administrativo separado como substituto da correção.

---

# 27. Limite da Fase 2

NÃO iniciar:

- backend final;
- solution final;
- `Optimizer.App`;
- `Optimizer.Application`;
- `Optimizer.Core`;
- `Optimizer.Windows`;
- `Optimizer.Persistence`;
- arquitetura final;
- DI final;
- IPC final;
- processo auxiliar elevado definitivo;
- banco/persistência final;
- logging framework definitivo;
- contratos finais de features;
- FeatureCatalog runtime definitivo;
- UI;
- páginas;
- integração com frontend;
- sistema de plugins;
- packaging final;
- updater final.

Protótipo pode usar C#/.NET/Win32/PInvoke/etc., mas:

```text
prototype != production
```

Não migrar silenciosamente prototype para código final.

---

# 28. Uso do `03-ARCHITECTURE.md`

Pode consultar o documento para identificar perguntas que as provas da Fase 2 devem ajudar a responder.

Especialmente:

- C#/.NET atende aos mecanismos necessários?
- P/Invoke é suficiente onde .NET não cobre?
- PowerShell será exceção ou mecanismo frequente?
- quantas features realmente exigem admin?
- quantas exigem reboot?
- quais precisam persistir snapshot?
- quais precisam de recuperação pós-reboot?
- quais exigem monitoramento contínuo?
- quais exigem processo de longa duração?
- quais dependem de vendor APIs?
- quais capacidades são realmente compartilhadas?

Registrar evidência nas Feature Specs.

Não decidir ainda a arquitetura final.

---

# 29. Commits

Usar commits pequenos e rastreáveis quando apropriado.

Preferência:

```text
proof(<domain>): validate <capability-id>
spec(<domain>): update <capability-id> after proof
```

Evitar um único commit gigante cobrindo toda a campanha.

Não fazer commit de:

- binários temporários desnecessários;
- secrets;
- dumps sensíveis;
- caches;
- arquivos de build sem utilidade.

---

# 30. Integridade dos arquivos

Preservar:

- IDs;
- estrutura das specs;
- histórico relevante;
- questões resolvidas quando úteis;
- evidências;
- resultados de teste;
- limitações.

Não apagar incerteza histórica apenas para deixar o documento “limpo”.

Converter:

```text
TBD
```

em resposta somente quando houver evidência.

---

# 31. Resultado obrigatório por Feature Spec

Ao terminar uma feature, a spec deve permitir responder:

```text
Como detectar?
Qual é a fonte da verdade?
Qual estado representa?
Quando é aplicável?
Quando não executar?
Como planejar?
Como dry-run funciona?
Como alterar, se aplicável?
Como verificar?
Como capturar estado anterior?
Como desfazer?
Como verificar rollback?
Quais permissões?
Quais reinicializações?
Quais riscos?
Quais falhas?
Quais compatibilidades foram realmente testadas?
Qual benefício existe?
Como foi medido?
Qual evidência sustenta isso?
Qual status a feature realmente merece?
```

---

# 32. Pacote de retorno obrigatório após concluir a etapa

Ao terminar toda a campanha, preservar no repositório tudo o que for necessário para auditoria.

O retorno para revisão deve conter obrigatoriamente:

## A. Feature Specs finais

Diretório completo:

```text
feature-specs/
```

com todas as alterações pós-prova.

## B. Prototypes

Diretório completo:

```text
prototypes/
```

incluindo código-fonte, scripts necessários, README e resultados relevantes.

## C. Estado do Git

Fornecer:

```text
git status
git log --oneline
```

e manter os commits da campanha acessíveis.

## D. Diff

A revisão deve conseguir inspecionar:

```text
git diff <commit-base>..<commit-final>
```

ou referência equivalente.

## E. Inventário final de status

Gerar contagem:

```text
RESEARCH:
SPECIFIED:
PROTOTYPING:
PROVEN:
APPROVED:
BLOCKED:
DEFERRED:
REJECTED:
```

e lista de Feature IDs por status.

## F. Matriz de ambiente realmente testado

Registrar no mínimo:

```text
Windows edition
Windows version/build
architecture
CPU
GPU
motherboard/chipset
drivers relevantes
desktop/laptop
admin/non-admin
VM quando usada
```

## G. Lista de provas executadas

Por Feature ID:

```text
DETECT
PLAN
DRY-RUN
APPLY
VERIFY
SNAPSHOT
ROLLBACK
VERIFY_ROLLBACK
MEASURE
REBOOT_TEST
```

com:

```text
PASS
FAIL
N/A
NOT_TESTED
BLOCKED
```

## H. Lista de falhas encontradas

Incluir:

- prototype que falhou;
- premissa incorreta;
- API com comportamento diferente;
- incompatibilidade;
- rollback incompleto;
- necessidade de reboot inesperada;
- problema de permissão;
- edge case;
- documentação contradita por comportamento observado.

## I. Features que mudaram de decisão

Listar transições importantes:

```text
RESEARCH → SPECIFIED
SPECIFIED → PROVEN
PROVEN → APPROVED
PROVEN → DEFERRED
PROVEN → REJECTED
SPECIFIED → RESEARCH
RESEARCH → BLOCKED
```

com motivo curto e referência à própria spec.

## J. Questões ainda abertas

Lista explícita de tudo que ainda impede:

```text
PROVEN
```

ou:

```text
APPROVED
```

## K. Dependências de hardware ainda não testadas

Exemplo:

```text
Intel GPU not tested
NVIDIA GPU not tested
laptop battery path not tested
ARM64 not tested
BitLocker scenario not tested
corporate policy not tested
```

## L. Evidência útil para a Fase 3

Sem tomar decisões arquiteturais, consolidar fatos observados como:

```text
quantas features precisaram de admin
quantas precisaram de reboot
quantas precisaram de PowerShell
quantas precisaram de P/Invoke
quantas precisaram de WMI/CIM
quantas precisaram de Registry
quantas precisaram de vendor APIs
quantas precisam de monitoramento contínuo
quantas precisam persistir snapshot
quantas precisam retomar após reboot
quantas exigem rollback multi-etapas
quais capabilities são compartilhadas por múltiplas features
```

Esses dados serão usados posteriormente para validar o `03-ARCHITECTURE.md`.

Não converter essa consolidação em arquitetura final.

---

# 33. O que NÃO preciso receber separadamente

Se estiver registrado corretamente nas specs/prototypes, não é necessário criar documentos extras para:

- cada teste individual;
- cada pequena decisão;
- cada link pesquisado;
- cada comando executado.

Evitar burocracia duplicada.

A Feature Spec deve permanecer a fonte principal do estado da feature.

O prototype deve permanecer a fonte principal da prova executável.

---

# 34. Critério de conclusão desta etapa

A campanha está concluída quando:

```text
[ ] todos os domínios foram revisitados
[ ] todas as specs RESEARCH foram reavaliadas
[ ] todas as SPECIFIED relevantes tiveram necessidade de prototype analisada
[ ] prototypes necessários foram criados
[ ] testes possíveis no ambiente atual foram executados
[ ] PASS só existe para teste realmente executado
[ ] specs foram corrigidas com comportamento observado
[ ] retestes necessários foram executados
[ ] compatibilidade foi descrita honestamente
[ ] mutações possuem rollback adequado quando aplicável
[ ] benefícios alegados foram avaliados
[ ] status refletem evidência
[ ] PROVEN só foi usado com gate satisfeito
[ ] APPROVED só foi usado após avaliação final
[ ] revisão cruzada por domínio foi concluída
[ ] revisão cruzada global foi concluída
[ ] nenhum código de prototype virou backend final
[ ] pacote de retorno foi preparado
```

Não é necessário que todas as features terminem `PROVEN` ou `APPROVED`.

Resultados legítimos incluem:

```text
APPROVED
PROVEN
SPECIFIED
RESEARCH
BLOCKED
DEFERRED
REJECTED
```

quando tecnicamente justificados.

---

# 35. Regra final de execução

Executar em loop:

```text
READ
↓
QUESTION
↓
RESEARCH
↓
PROTOTYPE IF NEEDED
↓
TEST
↓
OBSERVE
↓
UPDATE SPEC
↓
RETEST IF NEEDED
↓
CLASSIFY
↓
CROSS-REVIEW DOMAIN
↓
NEXT DOMAIN
```

Até finalizar o conjunto inteiro.

Nunca:

```text
preencher para concluir
promover para concluir
aprovar para concluir
implementar produção para concluir
```

O resultado esperado da Fase 2 é:

```text
sabemos o que funciona,
como funciona,
como provar,
como detectar falha,
como recuperar,
em quais condições funciona,
e quais features realmente merecem seguir para implementação.
```
