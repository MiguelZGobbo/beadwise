import { renderResult } from "./result.js";
import { features, scenarios, labels } from "./scenarios.js";
import { createSession, transition, serialize, restore } from "./state.js";
const key = "beadwise-phase10-session";
const main = document.querySelector("#main");
let storageWarning = "";
let saved = null;
try {
  saved = restore(localStorage.getItem(key));
} catch {
  storageWarning =
    "Armazenamento indisponível. Esta demonstração funcionará sem salvar a sessão.";
}
let state = saved || createSession();
let showWelcome = Boolean(saved && saved.stage !== "home");
const names = [
  "Início",
  "Análise",
  "Escolha",
  "Preparação",
  "Execução",
  "Resultado",
];
const stageIndex = {
  home: 0,
  analysis: 1,
  choices: 2,
  preview: 3,
  execution: 4,
  verification: 4,
  result: 5,
  recovery: 5,
};
const button = (action, text, primary = false, disabled = false) =>
  `<button data-action="${action}" class="${primary ? "primary" : ""}" ${disabled ? "disabled" : ""}>${text}</button>`;
const title = (heading, sub) =>
  `<h1>${heading}</h1><p class="subtitle">${sub}</p>`;
const metadata = (f) =>
  `<div class="tags"><span>Risco ${labels[f.risk]?.toLowerCase()}</span><span>${labels[f.reversibility]}</span><span>${f.privilege === "Administrator" ? "Requer administrador" : "Sem elevação"}</span></div>`;
const technical = (f) =>
  `<details><summary>Ver detalhes da alteração</summary><dl><dt>Estado atual</dt><dd>${f.current}</dd><dt>Estado desejado</dt><dd>${f.target}</dd><dt>Continuidade</dt><dd>${labels[f.restart]}</dd><dt>Recuperação</dt><dd>${labels[f.reversibility]}</dd></dl></details>`;
function home() {
  return (
    title(
      "Um cuidado de cada vez.",
      "Entenda o que merece atenção no seu PC. Revise cada proposta antes de decidir o que mudar.",
    ) +
    `<section class="panel"><h2>Comece conhecendo o estado atual</h2><p>A análise reúne observações e explica possíveis próximos passos. Analisar não altera configurações.</p><div class="intro-grid"><div><h3>Você mantém o controle</h3><p>Escolha as alterações e veja seus riscos antes da confirmação.</p></div><div><h3>Resultados com contexto</h3><p>Confira o que foi aplicado, o que foi verificado e o que ainda precisa de atenção.</p></div></div>${button("analyze", "Analisar meu PC", true)}</section><p class="subtitle">Nesta demonstração, todas as observações e operações são simuladas.</p>${state.cancelled ? '<div class="info">Análise cancelada. Nenhuma alteração foi realizada.</div>' : ""}`
  );
}
function analysis() {
  return (
    title(
      "Conhecendo seu PC",
      "Estamos reunindo informações para apresentar opções relevantes.",
    ) +
    `<section class="panel"><h2>Análise em andamento</h2><progress aria-label="Análise em andamento"></progress><div class="task-row"><span>Sistema e compatibilidade</span><span class="status success">Concluído</span></div><div class="task-row"><span>Energia e dispositivos</span><span class="status">Em análise</span></div><div class="task-row"><span>Inicialização e armazenamento</span><span class="status">Aguardando</span></div></section><div class="actions">${button("cancel", "Cancelar análise")}${button("finishAnalysis", "Ver análise simulada", true)}</div>`
  );
}
function choices() {
  let list = state.scenario === "empty" ? [] : features;
  return (
    title(
      "Escolha o que faz sentido",
      "Cada opção explica o motivo, o efeito esperado e o cuidado necessário. Nenhuma alteração está selecionada automaticamente.",
    ) +
    (state.scenario === "unavailable"
      ? '<div class="warning">Dados de energia indisponíveis. Não foi possível avaliar dispositivos USB. As demais observações continuam disponíveis.</div>'
      : "") +
    (list.length
      ? list
          .map((f) => {
            const unavailable =
              state.scenario === "unavailable" && f.id === "energy";
            const disabled = f.status || unavailable;
            return `<article class="feature"><div>${disabled ? "—" : `<input type="checkbox" id="${f.id}" data-feature="${f.id}" ${state.selected.includes(f.id) ? "checked" : ""}>`}</div><div><div class="category">${f.category}</div><h2>${disabled ? f.title : `<label for="${f.id}">${f.title}</label>`}</h2><p>${f.reason}</p>${disabled ? `<div class="tags"><span>${unavailable ? "Indisponível" : f.status === "Unsupported" ? "Não suportado" : "Já aplicado"}</span></div>` : `<p>${f.benefit}</p>${metadata(f)}${technical(f)}`}</div></article>`;
          })
          .join("")
      : '<section class="panel empty"><span class="check">✓</span><h2>Nenhuma alteração recomendada</h2><p>Neste cenário, não encontramos ajustes relevantes. Você pode manter a configuração atual.</p></section>') +
    `<div class="actions"><span class="selection-count">${state.selected.length} selecionada(s)</span>${button("new", "Nova análise")}${list.length ? button("preview", "Revisar alterações", true, !state.selected.length) : ""}</div>`
  );
}
function preview() {
  const blocked =
    state.scenario === "admin" &&
    state.plan.operations.some((o) => o.privilege === "Administrator");
  return (
    title(
      "Confira antes de continuar",
      "Este é o plano que será usado na execução simulada. Volte à escolha se quiser alterar alguma opção.",
    ) +
    state.plan.operations
      .map(
        (f) =>
          `<section class="panel"><h2>${f.title}</h2>${metadata(f)}${technical(f)}${f.reversibility === "Irreversible" ? '<div class="warning">Esta remoção não pode ser desfeita. Confirme somente se quiser incluir essa operação.</div>' : ""}</section>`,
      )
      .join("") +
    (blocked
      ? '<div class="warning">Permissão de administrador não concedida neste cenário. Remova a alteração de energia ou escolha outro cenário no painel de demonstração.</div>'
      : "") +
    '<div class="info">Nenhum ponto de restauração é prometido. A possibilidade de desfazer é indicada individualmente em cada operação.</div>' +
    `<div class="actions">${button("back", "Voltar à escolha")}${button("approve", "Confirmar plano e simular", true, blocked)}</div>`
  );
}
function execution() {
  return (
    title(
      "Aplicando seu plano",
      "A seleção está bloqueada para manter o plano confirmado consistente.",
    ) +
    `<section class="panel"><h2>${state.completed.length} de ${state.plan.operations.length} operações concluídas</h2><progress max="${state.plan.operations.length}" value="${state.completed.length}" aria-label="Operações concluídas"></progress>${state.plan.operations.map((o, i) => `<div class="task-row"><span>${o.title}</span><span class="status">${i < state.completed.length ? (state.completed[i].status === "Failed" ? "Falhou" : "Aplicada") : i === state.completed.length ? "Próxima operação" : "Aguardando"}</span></div>`).join("")}</section><div class="warning">Esta etapa representa operações que não devem ser interrompidas no meio. Não há cancelamento durante a aplicação.</div><div class="actions">${button("step", "Avançar operação simulada", true)}</div>`
  );
}
function verification() {
  return (
    title(
      "Vamos conferir o resultado",
      "Aplicar uma alteração e verificar seu estado são etapas diferentes.",
    ) +
    `<section class="panel"><h2>Verificação pendente</h2><p>Vamos comparar o estado observado com o estado esperado para cada operação aplicada.</p>${button("verify", "Verificar alterações simuladas", true)}</section>`
  );
}
function result() {
  return renderResult(state, button, title);
}
function recovery() {
  return (
    title(
      "Recuperação das configurações",
      "Somente operações aplicadas e reversíveis participam desta etapa.",
    ) +
    `<section class="panel"><h2>Restaurar e verificar</h2>${state.plan.operations
      .filter(
        (o) =>
          o.reversibility === "Reversible" &&
          state.completed.some((r) => r.id === o.id && r.status === "Success"),
      )
      .map(
        (o) =>
          `<div class="task-row"><span>${o.title}</span><span>${o.current}</span></div>`,
      )
      .join(
        "",
      )}<p>Os valores anteriores serão restaurados na simulação e conferidos em seguida. Arquivos removidos não serão recuperados.</p><div class="warning">Não interrompa a recuperação em andamento.</div>${button("finishRecovery", "Concluir recuperação simulada", true)}</section>`
  );
}
const views = {
  home,
  analysis,
  choices,
  preview,
  execution,
  verification,
  result,
  recovery,
};
function render() {
  document.querySelector("#steps").innerHTML =
    `<ol>${names.map((name, i) => `<li class="${i === stageIndex[state.stage] ? "active" : ""}" ${i === stageIndex[state.stage] ? 'aria-current="step"' : ""}><span class="number">${i + 1}</span>${name}</li>`).join("")}</ol>`;
  main.innerHTML = showWelcome
    ? title(
        "Sua sessão está aqui",
        "Encontramos uma sessão de demonstração salva neste navegador.",
      ) +
      `<section class="panel"><h2>Continue de onde parou</h2><p>Etapa: ${names[stageIndex[state.stage]]}. Cenário: ${scenarios[state.scenario]}.</p><div class="actions">${button("resume", "Retomar sessão", true)}${button("new", "Descartar simulação e começar")}</div></section>`
    : views[state.stage]();
  document.querySelector("#notice").textContent = storageWarning;
}
function save() {
  try {
    localStorage.setItem(key, serialize(state));
  } catch {
    storageWarning =
      "Não foi possível salvar a sessão. Você pode continuar nesta página.";
  }
}
main.addEventListener("click", (e) => {
  const action = e.target.closest("[data-action]")?.dataset.action;
  if (!action) return;
  try {
    if (action === "resume") showWelcome = false;
    else if (action === "new") {
      state = createSession(state.scenario);
      showWelcome = false;
    } else state = transition(state, action);
    save();
    render();
    main.focus();
  } catch (error) {
    document.querySelector("#notice").textContent = error.message;
  }
});
main.addEventListener("change", (e) => {
  if (e.target.dataset.feature) {
    state = transition(state, "toggle", e.target.dataset.feature);
    save();
    const id = e.target.id;
    render();
    document.getElementById(id)?.focus();
  }
});
document.querySelector("#scenario").innerHTML = Object.entries(scenarios)
  .map(([id, name]) => `<option value="${id}">${name}</option>`)
  .join("");
document.querySelector("#scenario").value = state.scenario;
document.querySelector("#reset").addEventListener("click", () => {
  state = createSession(document.querySelector("#scenario").value);
  showWelcome = false;
  save();
  render();
  main.focus();
});
render();
