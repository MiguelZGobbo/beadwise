# BeadWise — Design system

Status: DEFINIDO — implementação e validação nativas pendentes.

Data: 2026-11-12. Fase 11. Decisão: DEC-052 no [registro de decisões](06-DECISIONS.md).

## 1. Objetivo, referência e limites

Esta especificação consolida a identidade visual da fase 10 para componentes reutilizáveis em WinUI 3. Destina-se a quem implementar e revisar a interface. Não entrega uma biblioteca de controles, telas nativas, integração com backend ou novos contratos C#.

A identidade permanece clara, com azul da marca, fonte do sistema, bordas discretas e informações técnicas reveladas progressivamente. O tema escuro fica para evolução posterior; alto contraste é requisito desde a primeira implementação. Textos do produto são em português do Brasil.

Referências locais:

- [Protótipo e inventário](../prototypes/ux/phase-10/README.md), [CSS de origem](../prototypes/ux/phase-10/styles.css), [apresentação da jornada](../prototypes/ux/phase-10/app.js), [resultados](../prototypes/ux/phase-10/result.js) e [cenários](../prototypes/ux/phase-10/scenarios.js).
- Capturas: [início](../prototypes/ux/phase-10/captures/home-1280.png), [escolhas](../prototypes/ux/phase-10/captures/choices-1280.png), [preparação](../prototypes/ux/phase-10/captures/preview-1920.png), [preparação ampliada](../prototypes/ux/phase-10/captures/preview-zoom200.png) e [resultado](../prototypes/ux/phase-10/captures/result-1280.png).
- [Validação da fase 10](../prototypes/ux/phase-10/VALIDATION.md), [fluxo de UX](04-UX-FLOW.md) e [arquitetura](03A-ARCHITECTURE-FINAL.md).

As capturas são referências, não comprovação de acessibilidade nativa. A ampliação CSS da fase 10 não equivale à escala do Windows. Painel de cenários, avanço manual de operações, aviso de demonstração e armazenamento do navegador não são componentes do produto. A marca pode ser reutilizada a partir do [asset existente](../prototypes/ux/phase-10/logo.png).

## 2. Fundamentos e tokens

Os nomes abaixo são contratos de design, não recursos XAML já implementados. Na implementação, usar recursos compartilhados com prefixo `BeadWise`, preservando a separação entre cor, estilo e comportamento. Os valores numéricos de geometria são unidades lógicas, não pixels físicos. Valores extraídos do CSS passam a referências em unidades efetivas do WinUI; não converter espaçamentos pela escala do monitor manualmente.

### 2.1 Cores do tema claro

| Token | Valor | Uso e origem no CSS |
| --- | --- | --- |
| Color.Canvas | `#F5F7FA` | Fundo da aplicação; `:root` |
| Color.Surface | `#FFFFFF` | Navegação, painéis e botão secundário |
| Color.Text | `#172333` | Texto principal |
| Color.TextMuted | `#526174` | Explicações, categorias, descrições e rótulos |
| Color.BrandText | `#152C4B` | Nome BeadWise |
| Color.Accent | `#1456C0` | Ação primária, seleção de checkbox, progresso e foco |
| Color.AccentStrong | `#104AAB` | Etapa atual; também feedback de ação primária |
| Color.Link | `#2457A0` | Expansão de detalhes e links textuais |
| Color.HoverSurface | `#EDF2FA` | Hover de ação secundária |
| Color.InfoSurface | `#EAF1FC` | Informação, etapa atual e ação secundária pressionada |
| Color.MetadataSurface | `#F1F4F8` | Metadados e fundo de ação desabilitada |
| Color.BorderSubtle | `#D8DFE8` | Contorno decorativo dos painéis; trilho de progresso |
| Color.StepBorder | `#B8C5D7` | Círculo decorativo do número de etapa |
| Color.Separator | `#E4E9F0` | Separação decorativa entre resultados |
| Color.ControlBorder | `#526174` | Contorno necessário de botão secundário e checkbox vazio; correção de contraste |
| Color.Status | `#365269` | Texto neutro de estado por operação |
| Color.Success | `#23613F` | Texto/ícone de sucesso confirmado |
| Color.Error | `#A12626` | Texto/ícone de falha |
| Color.WarningSurface | `#FFF7E8` | Aviso contextual |
| Color.WarningAccent | `#A76A00` | Barra/ícone de aviso, nunca texto pequeno sobre WarningSurface |
| Color.WarningText | `#8B3F00` | Rótulo de atenção; extraído de `#notice`, sem importar o painel de demonstração |

Combinações permitidas: Text e TextMuted nos fundos Canvas, Surface, HoverSurface, InfoSurface, MetadataSurface e WarningSurface; branco sobre Accent/AccentStrong; AccentStrong sobre InfoSurface; Link sobre esses fundos claros. Status, Success e Error usam Surface/Canvas. WarningText usa Surface/WarningSurface. Não introduzir fundo vermelho ou verde sem especificar e medir a combinação.

### 2.2 Tipografia

Fonte nativa: **Segoe UI**, com fallback do sistema para caracteres ausentes. A origem usa `system-ui, sans-serif`. Manter escalabilidade do texto; tamanhos abaixo são base a 100%.

| Token | Tamanho / peso | Uso |
| --- | --- | --- |
| Type.PageTitle | 30 / Semibold 600 | Título da etapa; 26 em layout compacto |
| Type.SectionTitle | 19 / Bold 700 | Cabeçalho de painel/cartão |
| Type.SubsectionTitle | 16 / Bold 700 | Subdivisão de conteúdo |
| Type.Body | 15 / Regular 400 | Texto e rótulos de botões |
| Type.Detail | 14 / Regular 400 | Expansor, pares de detalhes e nota lateral |
| Type.Status | 13 / Regular 400 | Estado, rótulo da etapa e seção de navegação |
| Type.Caption | 12 / Regular 400 | Categoria, metadados, número da etapa e etapa compacta |
| Type.Brand | 22 / Bold 700 | Marca; 19 em layout compacto |
| Type.CurrentStep | 15 / Semibold 600 | Etapa atual; 12 em layout compacto |

Entrelinha de parágrafos: 1,65 × tamanho; avisos: 1,6 ×; nota lateral: 1,7 ×. São referências de distância entre linhas, não altura fixa do bloco. Cabeçalhos usam entrelinha nativa. Textos longos quebram linha; o título não deve ocultar palavras com reticências. Não usar texto de 11 unidades: no protótipo ele pertence apenas ao aviso de demonstração.

### 2.3 Espaçamento e geometria

Escala principal preservada: `Space.Small=8`, `Space.Medium=16`, `Space.Large=24`, `Space.XLarge=32`. As medidas específicas abaixo também são tokens; não arredondar todo o protótipo silenciosamente para múltiplos de oito. Pares de padding estão em ordem **vertical / horizontal**.

| Token | Valor | Uso |
| --- | --- | --- |
| Layout.NavigationWidth | 248 | Coluna lateral ampla |
| Layout.ContentMaxWidth | 1250 | Área principal, incluindo padding |
| Layout.CopyMaxWidth | 720 | Subtítulo explicativo |
| Layout.CompactThreshold | 850 | Largura útil da janela; compacto em ≤ 850 |
| Layout.ContentPadding | 48 / clamp(24, 5% da largura útil da janela, 90) | Área principal ampla |
| Layout.CompactPadding | 24 | Área principal compacta |
| Layout.NavigationPadding | 30 / 22 | Navegação ampla; compacto: 16 / 24 |
| Layout.NavigationGap | 22 | Entre grupos; compacto: 12 |
| Layout.IntroGap | 24 | Duas colunas introdutórias |
| Layout.DetailLabelWidth | 150 | Coluna de rótulos dos detalhes no layout amplo |
| Panel.Padding / Panel.MarginY | 24 / 16 | Painéis e cartões |
| Button.PaddingY / Button.PaddingX | 11 / 17 | Botões; altura cresce com o rótulo |
| Card.SelectionColumn / Card.Gap | 24 / 14 | Coluna de seleção e conteúdo |
| Card.DescriptionMarginY / Card.CategoryMarginBottom | 7 / 9 | Texto do cartão |
| Metadata.Gap / Metadata.MarginY | 12 / 13 | Grupo de metadados com quebra de linha |
| Metadata.PaddingY / Metadata.PaddingX | 5 / 8 | Cada rótulo |
| Notice.PaddingY / Notice.PaddingX / Notice.MarginY | 16 / 20 / 18 | Avisos e informação |
| Actions.Gap / Actions.MarginTop | 12 / 28 | Grupo de ações |
| Details.MarginTop / Details.Gap | 14 / 12 | Expansor e grade interna |
| Result.PaddingY / Result.Gap | 17 / 20 | Linha de operação |
| Step.Padding / Step.Gap / Step.MarginY | 12 / 12 / 4 | Etapa ampla; padding compacto: 6 |
| Header.MarginTop / Header.MarginBottom | 10 / 12 | Título de página |
| Section.MarginBottom / Subsection.MarginBottom | 12 / 8 | Cabeçalhos internos |
| Subtitle.MarginBottom / NavigationTitle.MarginTop | 30 / 18 | Subtítulo e título lateral |
| Brand.IconSize / Brand.Gap | 34 / 10 | Marca; preservar proporção da imagem |
| Step.NumberSize | 24 | Diâmetro mínimo; crescer se a escala de texto exigir |
| Progress.Thickness / Progress.MarginY | 8 / 18 | Barra de progresso |
| Empty.PaddingY / Empty.IconSize | 32 / 28 | Estado vazio |
| Border.Width / Warning.BorderWidth | 1 / 3 | Borda comum e barra esquerda de aviso |
| Radius.Panel / Radius.Button / Radius.Small / Radius.Metadata | 9 / 7 / 6 / 4 | Painéis, botões, etapas/informação, metadados |
| Focus.Width / Focus.Gap | 3 / 4 | Indicador de foco externo sem recorte |
| Motion.FeedbackDuration | 150 ms | Transição discreta de cor |

Não há sombra, gradiente, efeito de vidro ou animação decorativa. O raio de 50% dos números significa círculo, não um raio fixo em unidades. As dimensões de checkbox (19) e select (padding 9, raio 5) do HTML são referências de navegador: o checkbox nativo mantém sua geometria acessível; o select de cenários está fora do produto. O desconto de 70 px na altura do shell existe por causa do rodapé de demonstração e não se aplica ao app.

### 2.4 Estados interativos

| Estado | Ação primária | Ação secundária / seleção |
| --- | --- | --- |
| Normal | Accent, texto branco | Surface, Text, ControlBorder |
| Hover | AccentStrong, texto branco | HoverSurface, Text, ControlBorder |
| Pressionado | AccentStrong e contorno interno branco de 1 unidade | InfoSurface, Text, ControlBorder |
| Foco de teclado | Anel externo Accent de 3, separado por 4 unidades de Surface | Mesmo anel; não substituir hover/seleção |
| Selecionado | Botão de comando não fica selecionado | Checkbox com marca branca sobre Accent; etapa atual com AccentStrong/InfoSurface e identificação textual |
| Desabilitado | MetadataSurface, TextMuted, BorderSubtle, opacidade 1 | Mesmo tratamento; não responde a ativação |

Hover/pressed só aparecem em elementos interativos habilitados. Foco é sobreposto ao estado de interação, não apaga a seleção. Checkbox e expansão preservam seus padrões nativos de teclado e marcação; nunca transformar todo o cartão em um segundo controle de seleção que conflite com seus botões internos.

Motivo de bloqueio deve ficar visível junto da ação e disponível ao leitor de tela, sem depender de tooltip em controle desabilitado. Estado indisponível de uma funcionalidade não esmaece todo seu texto. Seleção não usa apenas mudança de cor: checkbox marcado e contagem textual acompanham a escolha.

Animações de cor podem durar 150 ms. Movimento reduzido desliga transições personalizadas; foco e notificações são imediatos. Trocas de etapa não usam slides ou zoom. Progresso nativo deve respeitar as configurações do sistema; com movimento reduzido, o texto de atividade continua suficiente para comunicar andamento.

### 2.5 Diferenças deliberadas em relação ao protótipo

| Diferença | Motivo e regra para implementação futura |
| --- | --- |
| `system-ui` → Segoe UI; peso 650 → Semibold 600 | Usar família e peso nativos explícitos; preservar níveis e tamanhos |
| Letter-spacing de título −0,7 px → espaçamento padrão da fonte | Evitar compressão na escala ampliada e conversão literal entre unidades CSS/XAML |
| BorderSubtle → ControlBorder nos contornos interativos necessários | A borda original tem contraste de apenas 1,34:1 sobre branco; conservar nos painéis decorativos |
| Opacidade 0,55 → cores desabilitadas explícitas com opacidade 1 | Manter rótulos e motivos legíveis; diferenciar inatividade também pelo comportamento |
| Hover/pressed primários e pressed secundário explícitos | O CSS não diferencia pressed e mantém o fundo primário no hover; reutilizar cores já existentes |
| Foco com separação branca e área reservada | Garantir percepção junto do botão azul e impedir recorte por containers |
| Estados nativos e checkbox preservam semântica/geometria do controle | Não copiar dimensões do navegador às custas de teclado, escala ou UI Automation |

Essas diferenças são especificações para o app futuro. O protótipo e suas capturas permanecem inalterados.

## 3. Contratos de apresentação e catálogo

As entradas e eventos abaixo descrevem responsabilidades de apresentação, não assinaturas públicas C#. Conteúdo obrigatório é indicado como **obrigatório**; o restante é opcional. Listas recebem identidade estável para preservar seleção e foco. Ações recebem rótulo, disponibilidade e motivo de bloqueio; os componentes emitem intenção e aguardam o estado devolvido pelo consumidor.

Um componente não calcula risco, elegibilidade de recuperação, necessidade de privilégio ou permissão para navegar. Application/Core continuam sendo a fonte dessas regras. O consumidor da UI adapta os contratos existentes; lacunas são documentadas, sem ampliar enums de domínio por conveniência visual. Não apresentar campos ausentes como “nenhum risco” ou “verificado”. Usar “Não informado” quando o campo for relevante, ou omiti-lo quando opcional.

### 3.1 Estrutura e orientação

| Componente / finalidade / WinUI | Conteúdo e entradas | Variantes e estados | Eventos, teclado e foco |
| --- | --- | --- | --- |
| **AppShell**: estrutura comum; `Grid`, `ScrollViewer`, região de conteúdo | **Obrigatório:** marca, conteúdo e título atual; opcionais navegação global e etapas | Amplo/compacto; região principal rolável; não contém fluxo de negócio | Nenhum evento de negócio. Tab percorre só controles; mudança explícita de página leva foco ao título/região principal |
| **Brand**: identidade; `Image` + `TextBlock` | **Obrigatório:** BeadWise e imagem existente; descrição da imagem omitida quando duplicar o nome | Ampla/compacta; estática | Sem evento nem Tab por padrão. Não copiar o reload da marca do HTML |
| **Navigation**: destinos globais; `NavigationView` | **Obrigatório:** itens com id, rótulo, destino atual e disponibilidade; ícone e motivo opcionais | Ampla lateral/compacta recolhível; atual, disponível, bloqueada | Emite intenção de navegação apenas para destino habilitado. Padrão nativo de Tab/setas; bloqueio explicado, sem abandonar operação crítica |
| **StepIndicator**: orientação da jornada; lista composta com texto, número e marcador | **Obrigatório:** etapas ordenadas, etapa atual e descrição de subetapa; nenhuma rota | Atual, concluída, futura; vertical amplo, quebra em linhas compacto | Sem clique, sem evento e fora de Tab; leitor de tela anuncia ordem e etapa atual. Nunca usar a etapa como atalho implícito |
| **PageHeader / SectionHeader**: hierarquia; `TextBlock` em `StackPanel` | **Obrigatório:** título; subtítulo e rótulo de etapa opcionais | Página, seção, subseção; tamanho compacto para página | Sem evento. Cabeçalhos expostos à UI Automation; título da página pode receber foco programático na navegação |
| **ContentPanel**: agrupar conteúdo; `Border` + `Grid` | **Obrigatório:** conteúdo; cabeçalho opcional | Padrão; altura automática | Não recebe foco próprio nem evento; ordem dos filhos segue leitura visual |

A navegação global ainda não possui um mapa final de destinos nesta fase; o contrato acima não autoriza inventar páginas. Na jornada existente há seis marcos: Início, Análise, Escolha, Preparação, Execução, Resultado. Verificação aparece como subetapa textual de Execução; Recuperação como subetapa de Resultado. Conclusão de uma etapa não significa sucesso de suas operações. Essa lista não redefine `SessionState`.

O `NavigationView` é reservado aos destinos globais, não à lista de etapas. Seu uso como navegação adaptável segue a [documentação oficial](https://learn.microsoft.com/en-us/windows/apps/develop/ui/controls/navigationview); separar esses dois papéis é uma decisão do BeadWise.

### 3.2 Escolha, detalhes e ações

| Componente / finalidade / WinUI | Conteúdo e entradas | Variantes e estados | Eventos, teclado e foco |
| --- | --- | --- | --- |
| **PrimaryButton / SecondaryButton**: ação; estilos de `Button` | **Obrigatório:** rótulo e disponibilidade; motivo obrigatório quando bloqueado; ícone opcional | Estados da seção 2.4; texto com quebra; no máximo uma primária por grupo | Emite ativação; Tab e Enter/Espaço nativos; clique repetido bloqueado pelo estado de andamento fornecido pelo consumidor |
| **ActionBar**: ordenar comandos; `Grid` e containers com quebra/adaptação | **Obrigatório:** ações; contagem selecionada opcional | Ampla em linha, compacta com quebra/empilhamento; sem sobrepor conteúdo | Sem novo evento: encaminha ações dos filhos. Ordem de Tab igual à visual; contagem não entra em Tab |
| **FeatureCard**: capacidade e estado; composição `Border`, textos e conteúdo | **Obrigatório:** id, título e estado; categoria, explicação e detalhes opcionais | Somente leitura, já aplicado, não suportado, indisponível | Não selecionável por si. Ações internas opcionais mantêm foco próprio; estado é texto acessível |
| **RecommendationCard**: proposta selecionável; composição de FeatureCard + `CheckBox` | **Obrigatório:** id, título, motivo, efeito esperado, risco, reversibilidade, privilégio, continuidade, seleção e elegibilidade; explicação de impedimento quando necessário | Recomendada/opcional, não selecionada/selecionada, bloqueada. Usa a mesma estrutura visual de FeatureCard | Emite mudança de seleção; Espaço alterna checkbox, Tab alcança detalhes sem alterar seleção. Título associa nome ao checkbox; atualização preserva foco pelo id |
| **MetadataGroup**: resumo de risco e requisitos; textos em `Border` com quebra | **Obrigatório:** pares rótulo/valor; explicação adicional opcional | Risco, privilégio, reversibilidade, continuidade; texto neutro, sem badge clicável | Sem eventos nem Tab; leitura inclui o contexto (“Risco moderado”, não apenas “Moderado”) |
| **DetailsExpander**: detalhes sob demanda; `Expander` + grade de rótulos/valores | **Obrigatório:** título, conteúdo e estado expandido; código técnico opcional | Recolhido/expandido; duas colunas amplas, uma compacta | Emite mudança de expansão. Header com teclado nativo; ao recolher com foco no conteúdo, devolver ao header; preserva expansão em atualizações da mesma operação |

Recomendação responde o que muda, por quê, efeito esperado e custos/riscos. “Pode desfazer” não é promessa de restauração global: detalhes informam limites por operação. Nenhuma seleção é automática no fluxo atual. Confirmação se concentra na preparação; diálogos não devem duplicar cada escolha.

O uso de `Expander` preserva conteúdo primário visível e insere os detalhes no fluxo da página, conforme a [referência WinUI](https://learn.microsoft.com/en-us/windows/apps/develop/ui/controls/expander).

### 3.3 Feedback, resultado e continuidade

| Componente / finalidade / WinUI | Conteúdo e entradas | Variantes e estados | Eventos, teclado e foco |
| --- | --- | --- | --- |
| **Notice / WarningCard**: informação contextual; estilo/composição de `InfoBar` | **Obrigatório:** severidade e mensagem; título, ação e detalhes opcionais; possibilidade de fechar explícita | Informação, aviso, erro, sucesso; avisos de risco/bloqueio não fecháveis | Emite ação/fechamento quando oferecidos; Tab só nos filhos interativos; atualização anunciada sem roubar foco |
| **ProgressIndicator / ProgressList**: andamento; `ProgressBar` + lista composta | **Obrigatório:** atividade; contagem/total apenas se conhecidos; linhas por operação opcionais | Indeterminado, determinado, concluído, falhou; conclusão do progresso não implica sucesso | Sem evento de mutação. Barra fora de Tab; expor nome, valor quando conhecido e estado textual; anunciar mudanças relevantes, não cada frame |
| **OperationResult / ResultSummary**: resultado por operação e agregado; `Grid`, textos e detalhes | **Obrigatório:** id/título e resultado de aplicação; verificação/recuperação/continuidade se disponíveis; contagens e explicações | Sucesso, falha, parcial, pendente, incerto; sem esconder linhas com falha | Eventos somente nas ações explícitas fornecidas. Conteúdo legível fora de Tab; expansão e ações seguem controles nativos |
| **EmptyState**: ausência de recomendações; painel e textos | **Obrigatório:** título e explicação; ícone e ação opcional | Vazio após análise válida; não serve para falha de coleta | Só ação recebe Tab/evento. Sem “Aplicar”; título acessível e ícone redundante ignorado pelo leitor |
| **ResumeSessionPanel**: continuidade; painel + ActionBar | **Obrigatório:** identificação/resumo da sessão, etapa e ações permitidas; pendências e motivo de bloqueio quando existirem | Retomável, exige revisão, indisponível; descarte somente quando autorizado pelo estado recebido | Emite retomar/revisar/descartar quando oferecidos. Tab entre ações; nunca executar novamente apenas por abrir o painel |
| **ConfirmationDialog**: decisão realmente bloqueante; `ContentDialog` | **Obrigatório:** título, consequência e opções explícitas; detalhes opcionais; ação destrutiva identificada | Aberto/fechado; confirmação ou erro que exige decisão | Emite escolha, sem executar backend. Foco inicial na opção segura; Tab contido, Escape cancela a decisão prévia e fechamento devolve foco ao invocador |

Em WarningCard, a barra de atenção usa WarningAccent e o corpo usa Text. Erro/sucesso têm rótulo e ícone sem depender apenas da cor. Aviso crítico permanece visível até o estado mudar; o botão de fechar nunca oculta uma condição que bloqueia a próxima ação. O `InfoBar` permite mensagem contextual e ação sem interromper a página: [referência](https://learn.microsoft.com/en-us/windows/apps/develop/ui/controls/infobar).

Progresso usa contagens reais recebidas. “2 de 3 operações processadas” pode incluir falha; não escrever “2 bem-sucedidas” sem esse dado. Tempo desconhecido usa indicador indeterminado e texto, sem percentual fictício. A escolha de `ProgressBar` segue os modos [documentados para WinUI](https://learn.microsoft.com/en-us/windows/apps/develop/ui/controls/progress-controls).

Diálogos são exceção, não telas de progresso. Apenas um modal por janela; fechar um diálogo não cancela uma operação já iniciada. Para confirmação destrutiva, Enter não deve assumir a alternativa destrutiva como padrão. Seguir foco/modalidade do [ContentDialog](https://learn.microsoft.com/en-us/windows/apps/develop/ui/controls/dialogs-and-flyouts/dialogs).

## 4. Estados da jornada e ações

Os estados abaixo são vocabulário de apresentação. Aplicação, verificação, recuperação e continuidade são dimensões distintas: uma operação pode estar aplicada e ainda depender de reinício. O resultado agregado deve preservar esses detalhes, sem forçar todos em um único badge.

| Situação | Mensagem / apresentação | Ações e limites |
| --- | --- | --- |
| Em análise | Atividade e progresso indeterminado quando não há total | Cancelar análise quando permitido; não oferecer aplicar durante coleta |
| Recomendada / opcional | Motivo, efeito esperado, riscos e detalhes | Selecionar somente elegíveis; revisar exige pelo menos uma seleção |
| Não suportado | “Não suportado” e capacidade ausente | Somente leitura; sem checkbox de aplicação |
| Indisponível | “Indisponível” e motivo de falta de dados | Não avaliar como seguro nem selecionar; outras opções válidas continuam utilizáveis |
| Já aplicado | “Já aplicado”, com estado observado | Somente leitura; não executar novamente |
| Permissão necessária | Requisito no cartão e impedimento na preparação | Confirmação bloqueada quando faltar permissão; voltar e remover operação. Não criar fluxo de elevação nesta fase |
| Preparação | Seleção exata, risco, continuidade e reversibilidade por operação | Voltar preserva escolha e invalida plano anterior; confirmar solicita execução daquele plano |
| Aplicando | Plano bloqueado, progresso por operação e motivo para não interromper | Sem editar seleção, saltar etapa ou cancelar a aplicação crítica |
| Aplicada | “Aplicada · verificação pendente” enquanto não houver confirmação | Não apresentar “verificada” baseado apenas em retorno de aplicação |
| Verificada | Estado observado corresponde ao esperado | Não afirmar ganho de desempenho; medição exige evidência própria |
| Falha parcial | Quantas aplicadas/falharam e quais pendências permanecem | Não sugerir rollback automático; nova tentativa só se explicitamente disponível |
| Reinício pendente | Aplicado, tipo de continuidade, pendência e condição de retomada | Distinguir reiniciar Windows de reconectar dispositivo; sem reinício automático nem promessa de retomada não implementada |
| Recuperação disponível | Operações elegíveis e valores anteriores | Oferecer somente recuperação autorizada; operações irreversíveis não entram |
| Recuperação em andamento | Restaurar e verificar; seleção bloqueada | Não interromper etapa crítica; fechamento da UI não equivale a cancelamento seguro |
| Recuperação concluída | “Restaurada e verificada” apenas nas operações confirmadas | Operações irreversíveis aplicadas continuam descritas como aplicadas |
| Falha na recuperação | “Estado incerto”, explicação e detalhes técnicos | Não mostrar sucesso; orientar revisão antes de novas mudanças; não repetir automaticamente |
| Sessão incompleta | Resumo, etapa, pendências e opções permitidas | Retomar/revisar; descartar só quando seguro segundo o consumidor, não como botão universal |
| Falha de persistência | Não foi possível salvar; informar se continuidade em memória está disponível | Não prometer retomada. A UI reflete a capacidade recebida; não implementa storage |

Rótulos de risco: Baixo, Moderado, Alto quando esses valores forem fornecidos. Requisitos: “Sem elevação” ou “Requer administrador”; não traduzir genericamente todo `None` como o mesmo texto em contextos diferentes. Para continuidade, informar “Não requer reinício” quando conhecido. Para campo desconhecido, informar a lacuna.

Em estado incerto ou pendente, usar ação neutra como “Concluir revisão” quando permitida; não copiar indiscriminadamente “Manter e concluir” do demonstrador. A conclusão da visualização não apaga a incerteza nem a pendência da sessão. Erros mostram primeiro causa compreensível e próximo passo; códigos ficam nos detalhes. Não prometer ponto de restauração, undo universal, suporte a reboot real ou funcionalidades que ainda não existem.

## 5. Layout, teclado e acessibilidade

### 5.1 Adaptação

Acima de 850 unidades úteis: navegação/etapas em coluna lateral de 248, conteúdo com largura máxima de 1250, alinhado ao início, e padding fluido da seção 2.3. A largura se refere à área cliente, excluindo moldura/título do Windows. Shell ocupa a área disponível sem descontar rodapé de demonstração.

Em 850 ou menos: etapas passam para região superior com quebra de linha; navegação global, quando existir, usa painel recolhível separado. Introdução passa a uma coluna. Detalhes colocam cada valor abaixo do rótulo, com 10 unidades após o valor. Marca e título usam tamanhos compactos. Nota lateral e título secundário de navegação podem desaparecer; nome da página, estado e ações continuam disponíveis.

Metadados e ações quebram linha em qualquer largura. Quando um botão não couber, ocupar a largura disponível e quebrar seu texto; não diminuir fonte para fazê-lo caber. Estado da operação pode ir para a linha seguinte também em layout amplo se houver texto ampliado. Conteúdo expande verticalmente, com rolagem da região principal; nenhuma altura fixa de cartão ou texto. Menus/etapas também permanecem acessíveis quando a altura é pequena. Não manter barra de ações flutuante cobrindo conteúdo.

### 5.2 Teclado e leitor de tela

- Ordem de leitura: marca e navegação, orientação da jornada, título, conteúdo, ações. Tab percorre somente controles habilitados; textos e estados continuam disponíveis à navegação de leitura.
- Foco visível não pode ser cortado por borda, ScrollViewer ou diálogo. Ao focar controle fora da área visível, trazê-lo à vista.
- Troca de página iniciada pelo usuário move foco para o título; atualizações de progresso/estado não roubam foco. Se o controle focado desaparecer, mover para o próximo controle habilitado do mesmo grupo ou para o título da região.
- Nome acessível do checkbox inclui título da recomendação; metadados e motivo de bloqueio ficam relacionados à descrição. Expor marcado, expandido, indisponível e valor de progresso por semântica nativa/UI Automation.
- Mensagens relevantes são anunciadas de modo não interruptivo; falha que exige ação recebe anúncio prioritário sem repetição contínua. Mudança de foco no título não deve duplicar anúncio da página.
- Informar a etapa atual em texto/semântica acessível, além do fundo azul. Não usar símbolos “✓” ou cor como única informação de sucesso.

Esses requisitos usam os pilares de acesso programático, teclado e contraste da [orientação de acessibilidade Windows](https://learn.microsoft.com/en-us/windows/apps/develop/accessibility). A especificação não certifica funcionamento com Narrador: isso depende do app implementado.

### 5.3 Alto contraste e escala

Na implementação, usar recursos de tema (`ThemeResource`/dicionários de tema) para substituir a paleta clara por cores do sistema: fundo/texto de janela, fundo/texto de botão, seleção/texto selecionado, texto desabilitado e links. Não fixar os hexadecimais da marca em alto contraste. Conservar bordas e texto de estado quando fundos desaparecerem. Preferir templates nativos; estilos personalizados precisam passar pela mesma matriz. Referência: [temas de contraste Windows](https://learn.microsoft.com/en-us/windows/apps/design/accessibility/high-contrast-themes).

Respeitar escala de exibição e ampliação de texto sem reduzir fontes para compensar. A opção de tema claro não pode impedir o alto contraste do sistema. Verificar pelo menos um tema de contraste com fundo escuro e outro com fundo claro; trocar tema com a janela aberta deve atualizar cores e manter foco/seleção.

### 5.4 Contraste verificado na especificação

Critério de projeto: pelo menos 4,5:1 para todo texto do produto, inclusive pequenos metadados; 3:1 para pistas visuais essenciais de controles, estados e foco. São critérios de referência das WCAG, não uma declaração de conformidade de um app ainda inexistente: [contraste de texto](https://www.w3.org/WAI/WCAG22/Understanding/contrast-minimum.html) e [contraste não textual](https://www.w3.org/WAI/WCAG22/Understanding/non-text-contrast.html).

Cálculo em sRGB opaco: normalizar canal para 0–1; linearizar por `c/12,92` se `c ≤ 0,04045`, senão `((c+0,055)/1,055)^2,4`; luminância `0,2126 R + 0,7152 G + 0,0722 B`; razão `(Lmaior+0,05)/(Lmenor+0,05)`. Valores arredondados a duas casas apenas para exibição; avaliar o valor completo contra o limite.

| Primeiro plano / fundo | Razão | Resultado e uso |
| --- | --- | --- |
| `#172333` / `#FFFFFF` | 15,85:1 | Texto principal |
| `#172333` / `#F5F7FA` | 14,77:1 | Texto na página |
| `#526174` / `#FFFFFF` | 6,32:1 | Texto secundário e borda de controle |
| `#526174` / `#F5F7FA` | 5,89:1 | Subtítulos |
| `#526174` / `#F1F4F8` | 5,73:1 | Metadados/desabilitado |
| `#526174` / `#EDF2FA` | 5,63:1 | Contorno de controle no hover |
| `#526174` / `#EAF1FC` | 5,57:1 | Contorno de controle pressionado |
| `#FFFFFF` / `#1456C0` | 6,73:1 | Ação primária/checkbox |
| `#FFFFFF` / `#104AAB` | 8,11:1 | Ação primária em hover/pressed |
| `#104AAB` / `#EAF1FC` | 7,14:1 | Etapa atual |
| `#2457A0` / `#FFFFFF` | 7,13:1 | Detalhes/links |
| `#2457A0` / `#FFF7E8` | 6,69:1 | Detalhes no aviso |
| `#365269` / `#FFFFFF` | 8,18:1 | Estado de operação |
| `#23613F` / `#FFFFFF` | 7,36:1 | Sucesso confirmado |
| `#A12626` / `#FFFFFF` | 7,45:1 | Falha |
| `#172333` / `#FFF7E8` | 14,89:1 | Corpo do aviso |
| `#172333` / `#EAF1FC` | 13,96:1 | Informação |
| `#A76A00` / `#FFF7E8` | 4,19:1 | Passa para barra/ícone; não usar como texto pequeno |
| `#8B3F00` / `#FFFFFF` | 7,48:1 | Rótulo de atenção |
| `#1456C0` / `#F5F7FA` | 6,27:1 | Foco sobre canvas |
| `#1456C0` / `#EAF1FC` | 5,92:1 | Foco sobre informação |
| `#1456C0` / `#D8DFE8` | 5,01:1 | Progresso contra trilho |
| `#D8DFE8` / `#FFFFFF` | 1,34:1 | Apenas borda decorativa; reprovada para contorno essencial |

Os pares adicionais permitidos pela seção 2.1 devem ser medidos junto desta tabela na revisão documental. Bordas de painel, separadores e círculos de etapa não carregam informação sozinhos. Alto contraste, antialiasing, template nativo e foco efetivamente renderizado continuam sujeitos à validação futura.

Na conferência de 2026-11-12, os pares adicionais também passaram: mínimos de 13,96:1 para Text, 5,57:1 para TextMuted e 6,28:1 para Link nos seis fundos claros permitidos; BrandText sobre Surface: 14,06:1; Status, Success e Error sobre Surface/Canvas: mínimos de 7,62:1, 6,86:1 e 6,94:1; WarningText sobre Surface/WarningSurface: mínimo de 7,02:1.

## 6. Rastreabilidade e cenários

### 6.1 Inventário da fase 10 → catálogo

| Inventário no README da fase 10 | Componente desta especificação |
| --- | --- |
| Shell desktop | AppShell + ContentPanel |
| Marca | Brand |
| Indicador de etapa | StepIndicator; separado de Navigation global |
| Cabeçalho | PageHeader / SectionHeader |
| Painel de conteúdo | ContentPanel |
| Opção selecionável | RecommendationCard + FeatureCard |
| Metadados de risco | MetadataGroup |
| Detalhes expansíveis | DetailsExpander |
| Aviso contextual | Notice / WarningCard |
| Ações primária/secundária | PrimaryButton / SecondaryButton + ActionBar |
| Lista de progresso | ProgressIndicator / ProgressList |
| Resultado por operação | OperationResult / ResultSummary |
| Retomada de sessão | ResumeSessionPanel |

EmptyState aparece no cenário vazio. ConfirmationDialog e Navigation global são padrões de uso futuro previstos no fluxo de UX, não telas comprovadas pela fase 10. Documentar o padrão não adiciona um modal nem um destino à jornada atual.

### 6.2 Oito cenários de referência

| Cenário existente | Componentes principais | Critério observável na futura UI |
| --- | --- | --- |
| Sucesso completo (`success`) | RecommendationCard, ActionBar, ProgressList, ResultSummary | Selecionar, revisar e confirmar o mesmo plano; distinguir aplicada de verificada; não alegar medição |
| Sem recomendações (`empty`) | EmptyState | Mensagem honesta; sem botão de aplicar nem resultado de erro disfarçado de vazio |
| Dados indisponíveis (`unavailable`) | FeatureCard, WarningCard | Energia não selecionável com motivo; demais recomendações permanecem disponíveis |
| Permissão necessária (`admin`) | MetadataGroup, WarningCard, ActionBar | Energia selecionada bloqueia confirmação; voltar/remover permite revisão válida |
| Falha parcial (`partial`) | ProgressList, OperationResult, DetailsExpander | Selecionar ao menos duas operações; mostrar quais falharam e quais continuam aplicadas |
| Reinício pendente (`restart`) | Notice, OperationResult, ResumeSessionPanel | Preservar estado aplicado e verificação pendente, sem reiniciar nem simular boot real |
| Recuperação disponível (`recovery`) | ResultSummary, ActionBar, ProgressList | Recuperar apenas reversíveis aplicadas; limpeza irreversível permanece fora e visível no resultado |
| Falha na recuperação (`recoveryFailed`) | WarningCard, OperationResult | Estado incerto e erro legível; sem mensagem de restauração confirmada |

Em todos os cenários não vazios, manter os exemplos “Não suportado” e “Já aplicado”. Exercitar seleção vazia, voltar da preparação, cancelamento da análise, expansão de detalhes, retomada em etapa intermediária e persistência indisponível. Cenários são fixtures de apresentação; não provam capacidades Windows.

## 7. Validação, aceite e passagem para implementação

### 7.1 Verificação documental desta fase

- Conferir tokens contra CSS e registrar explicitamente as diferenças na seção 2.5.
- Calcular razões de contraste para todos os pares autorizados; reprovar usos essenciais abaixo do limite.
- Conferir os 13 itens do inventário, os oito cenários e os contratos de conteúdo/estado/evento/foco de cada componente.
- Verificar links locais, coerência entre decisão/backlog/especificação e `git diff --check`.
- Não criar testes de código, executar operações Windows ou declarar os testes nativos abaixo como realizados.

### 7.2 Matriz futura de validação nativa — PENDENTE

As dimensões físicas abaixo são da área cliente de teste; o tamanho externo da janela deve compensar a moldura. Registrar também a área útil efetiva em unidades lógicas, escala de exibição e escala de texto. O breakpoint usa a área útil, não a resolução total do monitor.

| Configuração / cenário | Aceite esperado |
| --- | --- |
| 1280×720 e 1920×1080, exibição/texto 100% | Comparação com capturas, descontando diferenças documentadas; sem ações cortadas |
| Larguras úteis 851, 850 e 600; altura útil 720 | Transição ampla/compacta correta; etapas, detalhes e ações acessíveis sem rolagem horizontal |
| 1280×720 e 1920×1080, escala de exibição 200%, texto 100% | Layout usa largura efetiva reduzida; rolagem vertical alcança todos os controles |
| 1280×720, exibição 100%, texto 200%; repetir com ambas em 200% | Sem truncar rótulos, sobrepor texto ou reduzir fonte; estados/quebras continuam legíveis |
| Títulos, motivos e erros longos nos cartões/resultados | Altura automática; metadados, detalhes e ações quebram linha; sem esconder informação essencial |
| Teclado em seleção, preparação, resultado e diálogo | Tab/Shift+Tab, Enter, Espaço, setas nativas e Escape coerentes; sem armadilhas fora do modal |
| Narrador em todos os oito cenários | Nomes, estados, ordem, seleção, expansão e progresso compreensíveis; anúncios sem duplicação |
| Alto contraste claro/escuro; troca com janela aberta | Texto, seleção, avisos, contornos e foco visíveis; sem perda de estado |
| Movimento reduzido; progresso e mudança de etapa | Sem transição decorativa; atividade e conclusão comunicadas por texto |
| Retomada, falha parcial, reinício e recuperação incerta | Nenhuma ação indisponível torna-se executável; nenhuma pendência vira sucesso visual |

### 7.3 Critério de conclusão

A fase 11 está definida quando há referência central revisável, tokens e diferenças explícitos, catálogo rastreável, estados/ações descritos e critérios de validação. Implementação, comparação visual WinUI, teclado/Narrador e integração real continuam pendentes no [backlog de frontend](08-BACKLOG.md). Não mudar APIs de Core/Application nem o protótipo para marcar esta fase como concluída.

### 7.4 Registro da revisão documental

Em 2026-11-12: 23 razões da tabela recalculadas e conferidas, combinações adicionais verificadas, 20 vínculos locais conferidos entre esta especificação e as adições de decisão/backlog, oito identificadores de cenário encontrados e 13 itens do inventário rastreados. Revisão direta do catálogo e do diff; `git diff --check` sem problemas. Conteúdo anterior do registro de decisões preservado, e arquivos do protótipo/capturas conferidos por SHA-256 sem alterações. Nenhum teste de código ou teste nativo foi executado nesta fase. A tentativa de revisão independente não iniciou por limite de uso do agente; a revisão documental foi concluída pelo autor.
