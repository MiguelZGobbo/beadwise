# Validação — fase 10

Data: 2026-11-12. Ambiente: Windows, Edge headless via Playwright do runtime local (não é dependência do protótipo).

- `node --test prototypes/ux/phase-10/state.test.js`: 13 testes, 13 aprovados.
- Percursos pelo DOM: sucesso, vazio, indisponibilidade, administrador bloqueado, falha parcial, reinício pendente, recuperação concluída e recuperação falha. Sem erros JavaScript de página.
- Recarregamento durante execução seguido de retomada em cada cenário executável.
- Voltar do preview preserva seleção; remover uma opção produz plano com somente a opção restante.
- Inspeção visual: 1280×720 e 1920×1080; ampliação de conteúdo de 200% via CSS zoom, sem overflow horizontal no preview. Essa ampliação não equivale a uma certificação de zoom nativo de todos os navegadores.
- Teclado: Tab após mudança de etapa alcança botão; foco explícito em controles, labels associados a checkboxes, mensagens e estados textuais. Não foi realizado teste com leitor de tela.
- Revisão independente identificou orientação ausente de reconexão e mensagem inconsistente após recuperação parcial; ambas corrigidas no renderer de resultados.
- `git diff --check`: sem problemas.

## Capturas

- [Início, 1280](captures/home-1280.png)
- [Escolhas, 1280](captures/choices-1280.png)
- [Preparação, 1920](captures/preview-1920.png)
- [Preparação, ampliação 200%](captures/preview-zoom200.png)
- [Resultado, 1280](captures/result-1280.png)

A validação cobre a demonstração local. Não comprova operações Windows, impacto em desempenho ou recuperação real. O cenário de reboot conserva uma pendência; não simula o boot completo.
