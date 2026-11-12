# Fase 10 — Jornada principal

Protótipo local em português, com HTML/CSS/JavaScript sem dependências de aplicação. Não executa ações no Windows nem chama APIs de produção. Os exemplos representam capacidades futuras, não funcionalidades entregues pelo backend.

## Executar

Na raiz do repositório:

```powershell
python -m http.server 8710 --bind 127.0.0.1 --directory prototypes/ux/phase-10
```

Abra http://127.0.0.1:8710. É necessário servir por HTTP devido aos módulos JavaScript. Nenhuma instalação npm é necessária. Testes: `node --test prototypes/ux/phase-10/state.test.js`.

## Fluxo e comportamento

Início → Análise → Escolha → Preparação → Execução → Verificação → Resultado → Recuperação (quando elegível) → Resultado.

Análise pode ser cancelada. Preparação permite voltar e modificar seleção, invalidando o plano anterior. Confirmação congela a seleção; execução e recuperação não permitem cancelamento. O avanço das operações é manual e explicitamente simulado, para tornar todos os estados inspecionáveis. Recarregar oferece retomar a sessão salva, inclusive durante execução/recuperação. Armazenamento indisponível mantém a sessão em memória e informa a limitação.

O painel inferior é exclusivo da demonstração; carregar um cenário reinicia sua sessão simulada. A marca recarrega a página sem apagar o estado salvo. Manter e concluir retorna ao início. Nova análise também inicia uma sessão limpa. Não há histórico permanente.

## Matriz de cenários

| Cenário | Comportamento verificável |
| --- | --- |
| Sucesso completo | Selecionar, revisar, confirmar, avançar, verificar; estado aplicado distinto da etapa de verificação |
| Sem recomendações | Resultado vazio honesto, sem botão de aplicar |
| Dados indisponíveis | Energia indisponível; outras opções continuam selecionáveis |
| Permissão necessária | Selecionar energia bloqueia confirmação; voltar e remover permite seguir |
| Falha parcial | Selecionar pelo menos duas operações: segunda falha, demais permanecem aplicadas |
| Reinício pendente | Plano indica Reboot; resultado mantém verificação pendente, sem reiniciar o PC |
| Recuperação disponível | Após sucesso, restaurar somente operações reversíveis aplicadas |
| Falha na recuperação | Recuperação termina com estado incerto e erro legível |

Todos os cenários não vazios mostram capacidade não suportada e configuração já aplicada. A limpeza é irreversível: nunca participa da recuperação. A recuperação não mede desempenho. No cenário de reinício, a continuidade pós-reboot é representada por sessão persistida e pendência; o protótipo não simula um boot completo.

## Contratos e limites

| Apresentação | Conceito atual | Limitação |
| --- | --- | --- |
| Plano confirmado | ChangePlan: PlanId e Operations | Identificador fixo apenas na simulação; não é serialização de produção |
| Estado atual/alvo, risco, privilégio, reinício, reversibilidade | Operation | Cópia visual dos conceitos, sem adaptador C# |
| Resultado por operação | ResultStatus: Success / Failed | Cenário parcial agrega resultados; não executa Verify real |
| Etapas | SessionState: Created, Planned, Approved, Applying, Verifying, Completed | Navegação possui estados próprios; não amplia enum de domínio |
| Recuperação | RecoveryState: NotRequired, InProgress, Completed, Failed | Generalização visual futura; sem recuperação real |
| Achados e explicações | Lacuna de recomendação/análise | Fixtures de apresentação, sem afirmar motor implementado |

`state.js` contém transições e persistência; `scenarios.js` contém exemplos; `app.js` apresenta o estado e conecta eventos; `styles.css` define a referência visual. O navegador persiste somente a chave `beadwise-phase10-session`. Dados restaurados são validados; operações são reconstruídas a partir de fixtures locais.

## Inventário para a fase 11

Shell desktop, marca, indicador de etapa, cabeçalho, painel de conteúdo, opção selecionável, metadados de risco, detalhes expansíveis, aviso contextual, ações primária/secundária, lista de progresso, resultado por operação e retomada de sessão. Base clara, azul da marca, fonte do sistema, espaçamentos de 8/16/24/32px, bordas discretas, foco explícito e estados com texto. Não é ainda uma biblioteca WinUI.

## Aceitação

Testes de estado cobrem seleção, revisão, aprovação, bloqueios, irreversibilidade, resultados, cancelamento, persistência e retomada. Inspeção no navegador e capturas ficam documentadas separadamente pela revisão da entrega. Nenhuma captura por si só prova acessibilidade com leitor de tela; elementos nativos e rótulos são a base para a avaliação posterior em WinUI.
