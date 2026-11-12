export function renderResult(state, button, title) {
  const failed = state.completed.some((r) => r.status === "Failed");
  const restored = state.recovery === "Completed";
  const uncertain = state.recovery === "Failed";
  const pending = state.scenario === "restart" && !restored;
  const eligible = state.plan.operations.filter(
    (o) =>
      o.reversibility === "Reversible" &&
      state.completed.some((r) => r.id === o.id && r.status === "Success"),
  );
  const heading = uncertain
    ? "A recuperação precisa de atenção"
    : restored
      ? "Configurações recuperadas"
      : pending
        ? "Reinício pendente"
        : failed
          ? "Parte do plano precisa de atenção"
          : "Alterações verificadas";
  const rows = state.plan.operations
    .map((o) => {
      const failed =
        state.completed.find((r) => r.id === o.id)?.status === "Failed";
      const status = failed
        ? "Não aplicada"
        : uncertain && o.reversibility === "Reversible"
          ? "Estado incerto"
          : restored && o.reversibility === "Reversible"
            ? "Restaurada e verificada"
            : pending
              ? "Aplicada · verificação pendente"
              : "Aplicada e verificada";
      return `<div class="task-row"><span>${o.title}</span><span class="status">${status}</span></div>`;
    })
    .join("");
  return (
    title(
      heading,
      "O resultado descreve estados de configuração. Nenhum ganho de desempenho foi medido.",
    ) +
    `<section class="panel"><h2>Resumo da sessão</h2>${rows}</section>` +
    (failed
      ? `<div class="warning">Uma operação foi bloqueada pelo Windows simulado. ${restored ? "As operações reversíveis concluídas foram restauradas." : uncertain ? "O estado das operações submetidas à recuperação é incerto." : "As operações concluídas permanecem aplicadas."}<details><summary>Detalhes técnicos</summary>Código: DEMO_ACCESS_DENIED. Revise a permissão antes de iniciar outro plano.</details></div>`
      : "") +
    (pending
      ? '<div class="warning">Reinício do Windows pendente. A verificação depende do reinício, apenas representado neste protótipo. Nenhuma reinicialização será solicitada.</div>'
      : "") +
    (!restored &&
    !uncertain &&
    state.plan.operations.some((o) => o.restart === "Device")
      ? '<div class="info">Reconecte o dispositivo USB para que a configuração entre em efeito. A configuração foi verificada; o comportamento após reconexão ainda não foi avaliado.</div>'
      : "") +
    (uncertain
      ? '<div class="warning">Não foi possível confirmar a restauração. Revise o estado antes de tentar novas alterações. Código: DEMO_RECOVERY_FAILED.</div>'
      : "") +
    `<div class="actions">${button("new", "Manter e concluir", true)}${eligible.length && state.recovery === "NotRequired" ? button("recover", "Iniciar recuperação das alterações reversíveis") : ""}${button("new", "Nova análise")}</div>`
  );
}
