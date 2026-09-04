# PC Optimizer V2 — Discovery

## 1. Objetivo deste documento

Este documento define como será conduzida a fase de descoberta de funcionalidades da V2.

A fase de descoberta existe para responder:

> O que faz sentido existir em um aplicativo moderno de otimização, diagnóstico, manutenção, reparo e configuração para Windows?

O objetivo não é implementar funcionalidades nesta etapa.

O objetivo é criar um catálogo amplo, organizado e pesquisável de possibilidades, para que posteriormente cada item relevante possa passar por especificação técnica, prova de implementação e seleção.

---

# 2. O que deve sair desta fase

Ao final da descoberta inicial, devemos possuir:

- uma visão ampla das áreas que o aplicativo pode abordar;
- uma lista organizada de possíveis funcionalidades;
- uma breve descrição de cada item;
- uma noção inicial de benefício;
- uma noção inicial de risco;
- uma classificação conceitual;
- dúvidas técnicas registradas;
- ideias duplicadas consolidadas;
- itens obviamente ruins ou obsoletos descartados;
- candidatos para a fase de especificação.

Não é necessário saber ainda exatamente como cada funcionalidade será implementada.

Essa investigação aprofundada pertence às fases seguintes.

---

# 3. Regra principal

Nesta fase devemos descobrir:

```text
O QUE pode fazer sentido
```

e não ainda resolver completamente:

```text
COMO será implementado
```

Entretanto, se durante a descoberta surgir uma limitação técnica evidente, ela deve ser registrada.

Exemplo:

```text
Feature:
Ajuste automático de determinada configuração de GPU

Observação:
Pode depender de API específica do fabricante.

Dúvida:
Existe API pública e confiável para NVIDIA, AMD e Intel?
```

Essa dúvida será tratada posteriormente.

---

# 4. O que NÃO fazer durante a descoberta

Evitar:

- desenvolver código definitivo;
- desenhar arquitetura específica para uma única feature;
- criar telas;
- decidir fluxo final da aplicação;
- escrever scripts completos;
- assumir que toda ideia encontrada na internet é útil;
- copiar listas de "Windows tweaks" sem análise;
- definir uma feature como recomendada apenas porque outro software a oferece;
- transformar cada pequena chave de Registry em uma feature separada.

---

# 5. Unidade de descoberta

Cada item deve representar uma capacidade ou comportamento compreensível para o produto.

Evitar granularidade excessiva.

Ruim:

```text
Alterar Registry X
Alterar Registry Y
Alterar Registry Z
```

Melhor:

```text
Reduzir atividades de captura e gravação em segundo plano
```

As alterações específicas de Registry poderão aparecer posteriormente na especificação técnica.

Da mesma forma, múltiplas alterações técnicas podem fazer parte de uma única feature.

---

# 6. Template inicial de item

Durante a descoberta, utilizar uma estrutura leve.

```text
## Nome

Tipo:
Área:

Descrição:
O que é esta funcionalidade em poucas linhas?

Possível objetivo:
Qual problema ela pode resolver?

Possível benefício:
O que pode melhorar?

Aplicabilidade:
Em quais cenários parece fazer sentido?

Risco inicial:
Low / Medium / High / Unknown

Dúvidas:
O que ainda precisa ser investigado?

Fonte/observação:
Opcional.
```

Não é necessário preencher campos desconhecidos à força.

`Unknown` é melhor do que inventar uma resposta.

---

# 7. Tipos iniciais

Cada item pode receber um tipo provisório:

```text
Diagnostic
Optimization
Repair
Configuration
Tool
```

Outras categorias podem ser criadas se a descoberta mostrar necessidade real.

---

# 8. Áreas iniciais de investigação

A lista abaixo serve como mapa para evitar lacunas.

Ela não representa a estrutura final da interface.

---

## 8.1 Sistema operacional

Investigar possibilidades relacionadas a:

- comportamento geral do Windows;
- processos;
- serviços;
- tarefas em segundo plano;
- inicialização;
- recursos opcionais;
- configurações de sistema;
- componentes desnecessários;
- experiência do usuário;
- manutenção do sistema;
- integridade do Windows.

---

## 8.2 Inicialização e boot

Possíveis temas:

- programas de inicialização;
- serviços iniciados automaticamente;
- tarefas agendadas;
- tempo de boot;
- processos desnecessários;
- diagnóstico de inicialização lenta;
- recuperação de problemas de boot.

Evitar assumir que simplesmente desabilitar mais itens = melhor desempenho.

---

## 8.3 Energia

Investigar:

- planos de energia;
- comportamento de CPU;
- suspensão;
- hibernação;
- USB selective suspend;
- PCI Express power management;
- timers;
- economia de energia;
- diferenças entre desktop e notebook;
- comportamento conectado à tomada/bateria.

---

## 8.4 CPU

Possíveis temas:

- identificação do processador;
- carga;
- frequência;
- temperatura, quando disponível;
- throttling;
- comportamento energético;
- scheduling;
- afinidade, se fizer sentido;
- identificação de gargalos;
- configurações específicas de arquitetura/hardware.

Evitar tweaks sem evidência de benefício.

---

## 8.5 Memória RAM

Investigar:

- uso atual;
- pressão de memória;
- aplicações consumidoras;
- paginação;
- pagefile;
- diagnóstico de pouca memória;
- configurações potencialmente problemáticas;
- comportamento de standby/cache.

Cuidado especial com "RAM cleaners", que frequentemente não representam otimização real.

---

## 8.6 Armazenamento

Investigar:

- HDD;
- SATA SSD;
- NVMe;
- espaço livre;
- saúde do disco;
- SMART;
- TRIM;
- otimização/desfragmentação apropriada por tipo;
- arquivos temporários;
- caches;
- diagnóstico de desempenho;
- configurações de armazenamento;
- erros;
- integridade do filesystem.

---

## 8.7 GPU

Investigar:

- identificação de GPU;
- drivers;
- configurações relevantes;
- hardware acceleration;
- power management;
- seleção de GPU por aplicativo;
- diagnóstico;
- integração com NVIDIA, AMD e Intel;
- recursos específicos de jogos.

Não assumir que configurações específicas de fabricante são acessíveis ou seguras sem prova técnica.

---

## 8.8 Jogos

Possíveis temas:

- Game Mode;
- Game Bar;
- captura;
- overlays;
- processos relacionados;
- otimizações por jogo;
- energia;
- GPU;
- fullscreen/windowed behavior;
- scheduling;
- latência;
- periféricos;
- diagnóstico de desempenho.

Separar claramente:

```text
melhoria comprovável
```

de:

```text
preferência do usuário
```

---

## 8.9 Rede

Investigar:

- adaptadores;
- DNS;
- latência;
- perda de pacotes;
- configuração TCP/IP;
- Wi-Fi;
- Ethernet;
- power management;
- drivers;
- diagnóstico de conexão;
- reset de rede;
- configurações potencialmente problemáticas;
- priorização, quando tecnicamente justificável.

Evitar "internet booster" baseado em alterações sem fundamento.

---

## 8.10 USB e periféricos

Investigar:

- suspensão seletiva;
- gerenciamento de energia;
- mouse;
- teclado;
- controladores;
- dispositivos desconectando;
- polling, quando realmente acessível;
- diagnóstico;
- drivers;
- hubs USB;
- dispositivos de áudio.

---

## 8.11 Monitor e imagem

Possíveis temas:

- resolução;
- taxa de atualização;
- HDR;
- escalonamento;
- perfis de cor;
- gamma;
- brilho, quando disponível;
- contraste, quando disponível;
- calibração;
- teste de pixels;
- tentativa de recuperação de pixel preso;
- uniformidade;
- identificação do monitor.

Diferenciar:

```text
pixel morto
```

de:

```text
pixel preso
```

e evitar prometer correção quando tecnicamente não existe garantia.

---

## 8.12 Áudio

Investigar:

- dispositivos;
- formato;
- sample rate;
- enhancements;
- spatial audio;
- drivers;
- latência;
- dispositivos padrão;
- problemas comuns;
- diagnóstico.

---

## 8.13 Drivers

Investigar:

- detecção;
- versões;
- dispositivos com erro;
- drivers genéricos;
- drivers de fabricante;
- rollback;
- problemas conhecidos;
- atualizações.

Qualquer mecanismo de atualização automática de drivers exige análise de risco elevada.

---

## 8.14 Processos e aplicações

Investigar:

- aplicações consumindo CPU;
- RAM;
- disco;
- rede;
- programas em segundo plano;
- startup;
- processos suspeitos;
- aplicações desnecessárias;
- conflitos.

Não transformar o aplicativo em antivírus sem intenção explícita.

---

## 8.15 Limpeza

Possíveis temas:

- temporários;
- caches;
- lixeira;
- arquivos de atualização;
- logs;
- thumbnails;
- browser caches;
- caches de aplicações;
- restos de instalações;
- arquivos grandes.

Cada categoria deve ser avaliada quanto a:

- segurança;
- benefício;
- possibilidade de perda de dados;
- necessidade real.

---

## 8.16 Privacidade

Investigar:

- permissões;
- telemetria;
- publicidade;
- background apps;
- localização;
- activity history;
- diagnostics;
- configurações de privacidade.

Diferenciar claramente:

```text
privacidade
```

de:

```text
otimização de desempenho
```

---

## 8.17 Segurança

Investigar funcionalidades de:

- diagnóstico de configurações;
- firewall;
- Microsoft Defender;
- integridade;
- UAC;
- Secure Boot;
- TPM;
- proteções do Windows.

Regra:

Nunca recomendar desabilitar proteções importantes apenas para obter ganho marginal de desempenho.

---

## 8.18 Reparos do Windows

Possíveis temas:

- SFC;
- DISM;
- Windows Update;
- reset de componentes;
- rede;
- Store;
- serviços;
- arquivos do sistema;
- cache;
- componentes quebrados.

---

## 8.19 Windows Update

Investigar:

- estado;
- atualizações pendentes;
- reinicialização pendente;
- erros;
- diagnóstico;
- políticas;
- comportamento automático.

Evitar comprometer segurança por tentativa de "otimização".

---

## 8.20 Diagnóstico de hardware

Possíveis áreas:

- CPU;
- GPU;
- RAM;
- storage;
- monitor;
- rede;
- bateria;
- temperatura;
- sensores;
- dispositivos com erro.

---

## 8.21 Temperatura e refrigeração

Investigar:

- sensores disponíveis;
- CPU;
- GPU;
- storage;
- thermal throttling;
- detecção de comportamento anormal.

Importante:

Nem todo hardware expõe sensores de forma padronizada.

---

## 8.22 Bateria

Para notebooks:

- saúde;
- capacidade de projeto;
- capacidade atual;
- ciclos, quando disponíveis;
- consumo;
- plano de energia;
- diagnóstico;
- recomendações.

---

## 8.23 Ponto de restauração e recuperação

Investigar:

- disponibilidade;
- proteção do sistema;
- criação;
- falhas;
- espaço;
- restauração;
- integração com o ciclo de otimização.

---

## 8.24 Aparência e personalização

Possíveis temas:

- tema;
- transparência;
- animações;
- efeitos visuais;
- taskbar;
- Explorer;
- comportamento visual.

Separar:

```text
preferência estética
```

de:

```text
otimização de desempenho
```

---

## 8.25 Acessibilidade

Investigar se determinadas configurações ou otimizações podem:

- prejudicar recursos de acessibilidade;
- alterar comportamento necessário para alguns usuários;
- conflitar com configurações existentes.

O app deve evitar destruir configurações personalizadas do usuário.

---

## 8.26 Diagnóstico geral

Uma possível capacidade central do produto é realizar uma análise ampla e gerar um panorama.

Exemplos:

- gargalos;
- problemas;
- configurações;
- atualizações;
- armazenamento;
- temperatura;
- energia;
- hardware;
- processos.

Essa área poderá alimentar o sistema de recomendações.

---

## 8.27 Benchmark e medição

Investigar se faz sentido medir:

- antes;
- depois;
- diferença real.

Exemplos:

- tempo de boot;
- latência;
- uso de recursos;
- velocidade de armazenamento;
- desempenho gráfico;
- estabilidade.

Cuidado para não transformar benchmarks sintéticos em promessa de melhoria real.

---

# 9. Pesquisa de funcionalidades

Durante a descoberta, usar fontes variadas.

Prioridade:

1. documentação oficial da Microsoft;
2. documentação oficial de fabricantes;
3. APIs e documentação técnica;
4. artigos técnicos confiáveis;
5. projetos open source relevantes;
6. comportamento de ferramentas existentes;
7. fóruns e comunidades como fonte de ideias, não como prova final.

Informações encontradas em Reddit, fóruns, vídeos ou listas de tweaks devem ser tratadas como:

```text
hipótese
```

até serem verificadas tecnicamente.

---

# 10. Concorrentes e produtos semelhantes

Durante a descoberta também devemos analisar ferramentas existentes.

Objetivo:

- descobrir categorias de funcionalidades;
- entender problemas que tentam resolver;
- observar experiências interessantes;
- identificar recursos esperados;
- encontrar oportunidades.

Não copiar funcionalidades automaticamente.

Perguntas:

```text
Por que esse recurso existe?
Ele resolve problema real?
Como parece funcionar?
Ele é transparente?
É seguro?
É reversível?
É útil para nosso produto?
Podemos fazer melhor?
```

---

# 11. Regra contra mitos de otimização

Todo tweak encontrado deve ser tratado inicialmente com ceticismo saudável.

Sinais de alerta:

- "aumenta FPS em qualquer PC";
- "reduz input lag drasticamente";
- "libera RAM instantaneamente";
- "desativa dezenas de serviços";
- "otimiza Registry";
- "melhora internet";
- "remove telemetria e ganha desempenho";
- valores mágicos sem documentação;
- comandos sem explicação;
- tweak copiado entre versões antigas do Windows.

Esses itens podem ser investigados, mas não assumidos como válidos.

---

# 12. Benefício esperado

Durante a descoberta, utilizar classificação provisória.

```text
Performance
Latency
Stability
Energy
Storage
Privacy
Quality of Life
Repair
Diagnostic
Visual
Compatibility
```

Uma feature pode possuir mais de um benefício.

---

# 13. Aplicabilidade

Registrar quando possível:

```text
All PCs
Desktop
Laptop
Gaming
Workstation
Specific Hardware
Specific Windows Version
Optional / Preference
Unknown
```

Isso será refinado na especificação.

---

# 14. Risco inicial

Classificação provisória:

```text
Low
Medium
High
Unknown
```

O objetivo nesta etapa não é realizar análise completa de risco.

É apenas sinalizar itens que exigirão atenção especial posteriormente.

---

# 15. Duplicatas

Durante a descoberta, diferentes ideias podem representar a mesma funcionalidade.

Exemplo:

```text
Desativar captura do Xbox
Desativar Game DVR
Desativar gravação em segundo plano
```

Podem acabar pertencendo à mesma feature ou ao mesmo grupo.

Ao encontrar duplicatas:

- não apagar imediatamente;
- consolidar;
- registrar termos alternativos;
- decidir a granularidade posteriormente.

---

# 16. Features compostas

Uma feature pode exigir múltiplas mudanças.

Exemplo conceitual:

```text
Optimization:
Reduzir captura de jogos em segundo plano

Pode envolver:
- configuração A
- configuração B
- serviço C
```

Nesta fase, registrar a capacidade de alto nível.

Os detalhes entram na especificação.

---

# 17. Features condicionais

Algumas funcionalidades só existem quando determinadas condições são detectadas.

Exemplo:

```text
Battery Optimization
```

não se aplica necessariamente a desktop.

Da mesma maneira:

```text
NVIDIA-specific feature
```

não deve aparecer para AMD.

Registrar essas condições quando forem óbvias.

---

# 18. Recomendações não precisam significar alteração

Durante a descoberta, considerar funcionalidades cujo resultado pode ser apenas:

```text
Informar
Alertar
Diagnosticar
Recomendar ação manual
```

Exemplo:

> O SSD possui pouco espaço livre.

Isso pode ser útil mesmo sem existir um botão de "otimizar".

---

# 19. Status durante descoberta

Estados permitidos:

```text
IDEA
RESEARCHING
CANDIDATE
DUPLICATE
QUESTIONABLE
REJECTED
READY_FOR_SPEC
```

### IDEA

Recém-descoberta.

### RESEARCHING

Precisa de investigação inicial.

### CANDIDATE

Parece relevante.

### DUPLICATE

Provavelmente coberta por outra ideia.

### QUESTIONABLE

Benefício ou validade duvidosos.

### REJECTED

Não será perseguida no momento.

### READY_FOR_SPEC

Já possui informação suficiente para iniciar a especificação técnica.

---

# 20. Critério para READY_FOR_SPEC

Uma feature não precisa ter implementação conhecida ainda.

Mas deve existir entendimento suficiente de:

```text
[ ] O que é
[ ] Qual problema tenta resolver
[ ] Por que poderia ser útil
[ ] Para quem se aplica
[ ] Qual é seu tipo
[ ] Quais são as principais dúvidas técnicas
[ ] Não existe motivo óbvio para descartá-la
```

---

# 21. Estrutura sugerida do catálogo

O arquivo pode crescer inicialmente assim:

```text
# Discovery Catalog

## Sistema

### Feature X
...

### Feature Y
...

## Energia

### Feature Z
...

## Rede
...
```

Se o catálogo crescer demais, poderá ser dividido posteriormente.

Exemplo:

```text
/discovery
├── system.md
├── energy.md
├── network.md
├── display.md
└── ...
```

Não criar essa fragmentação antes de ela ser necessária.

---

# 22. Processo de descoberta

Para cada área:

```text
1. Entender a área
2. Levantar problemas comuns
3. Levantar configurações relevantes
4. Levantar diagnósticos possíveis
5. Levantar reparos possíveis
6. Levantar otimizações possíveis
7. Levantar ferramentas úteis
8. Identificar riscos
9. Identificar mitos
10. Registrar candidatos
```

---

# 23. Ordem sugerida de pesquisa

A ordem não representa o fluxo final do aplicativo.

Sugestão:

```text
1. Sistema
2. Energia
3. CPU
4. Memória
5. Armazenamento
6. GPU
7. Jogos
8. Rede
9. USB e periféricos
10. Monitor e imagem
11. Áudio
12. Drivers
13. Inicialização
14. Processos
15. Limpeza
16. Reparos
17. Segurança
18. Privacidade
19. Update
20. Hardware diagnostics
21. Temperatura
22. Bateria
23. Recovery
24. Personalização
25. Benchmark
```

A ordem pode mudar conforme a pesquisa.

---

# 24. Quando parar a descoberta inicial

A descoberta não deve tentar atingir "100% de tudo que existe".

Ela estará suficientemente madura quando:

- principais áreas estiverem cobertas;
- novas pesquisas começarem a produzir muitas duplicatas;
- houver material suficiente para formar um produto coerente;
- existirem candidatos suficientes para iniciar especificações;
- lacunas críticas não forem evidentes.

Depois disso, discovery continua como processo contínuo, mas deixa de bloquear as próximas fases.

---

# 25. Perguntas que devem ser feitas constantemente

Para cada ideia:

```text
Isso resolve um problema real?

É otimização, reparo, diagnóstico, configuração ou ferramenta?

O Windows já faz isso sozinho?

O usuário realmente precisa de controle sobre isso?

Isso pode prejudicar estabilidade?

Existe benefício mensurável?

É apenas uma preferência?

É reversível?

Depende de hardware?

Depende de versão do Windows?

Existe API ou método confiável?

Estamos prometendo mais do que podemos entregar?

Isso pertence ao nosso produto?
```

---

# 26. Resultado da fase

O resultado desta fase não é código.

O resultado é conhecimento organizado.

O catálogo deverá permitir que a equipe escolha uma feature e diga:

> Agora sabemos o suficiente sobre o que ela pretende fazer para começar a investigar exatamente como implementá-la.

Nesse momento ela passa para:

```text
02-FEATURE-SPEC-TEMPLATE.md
```

---

# 27. Próximo passo operacional

Após este documento ser aprovado:

1. criar a seção inicial do catálogo;
2. começar por uma área;
3. pesquisar e listar funcionalidades;
4. registrar itens com pouca profundidade;
5. consolidar duplicatas;
6. marcar candidatos;
7. avançar gradualmente para as demais áreas.

A recomendação inicial é começar por:

```text
Sistema
```

e trabalhar área por área em vez de tentar listar o aplicativo inteiro de memória.

---

# 28. Princípio final

Descoberta ampla não significa aceitar tudo.

A fase deve ser:

```text
ampla na busca
+
crítica na avaliação
```

O objetivo é conhecer as possibilidades antes de decidir o produto.