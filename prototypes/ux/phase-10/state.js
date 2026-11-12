import { features, scenarios } from "./scenarios.js";
const stages = [
  "home",
  "analysis",
  "choices",
  "preview",
  "execution",
  "verification",
  "result",
  "recovery",
];
export function createSession(scenario = "success") {
  return {
    version: 1,
    scenario,
    stage: "home",
    selected: [],
    plan: null,
    completed: [],
    recovery: "NotRequired",
    cancelled: false,
  };
}
function requireState(condition) {
  if (!condition) throw new Error("Ação indisponível nesta etapa.");
}
export function transition(session, action, id) {
  const s = structuredClone(session);
  switch (action) {
    case "analyze":
      requireState(s.stage === "home");
      s.stage = "analysis";
      break;
    case "finishAnalysis":
      requireState(s.stage === "analysis");
      s.stage = "choices";
      break;
    case "cancel":
      requireState(s.stage === "analysis");
      s.stage = "home";
      s.cancelled = true;
      break;
    case "toggle":
      requireState(
        s.stage === "choices" &&
          s.scenario !== "empty" &&
          !(s.scenario === "unavailable" && id === "energy") &&
          features.some((f) => f.id === id && !f.status),
      );
      s.selected = s.selected.includes(id)
        ? s.selected.filter((x) => x !== id)
        : [...s.selected, id];
      s.plan = null;
      break;
    case "preview":
      requireState(s.stage === "choices" && s.selected.length > 0);
      s.plan = {
        planId: "demo-plan",
        operations: s.selected.map((id) => ({
          ...structuredClone(features.find((f) => f.id === id)),
          restart:
            s.scenario === "restart"
              ? "Reboot"
              : features.find((f) => f.id === id).restart,
        })),
      };
      s.stage = "preview";
      break;
    case "back":
      requireState(s.stage === "preview");
      s.stage = "choices";
      s.plan = null;
      break;
    case "approve":
      requireState(
        s.stage === "preview" &&
          !(
            s.scenario === "admin" &&
            s.plan.operations.some((o) => o.privilege === "Administrator")
          ),
      );
      s.stage = "execution";
      break;
    case "step": {
      requireState(s.stage === "execution");
      const op = s.plan.operations[s.completed.length];
      s.completed.push({
        id: op.id,
        status:
          s.scenario === "partial" && s.completed.length === 1
            ? "Failed"
            : "Success",
      });
      if (s.completed.length === s.plan.operations.length)
        s.stage = "verification";
      break;
    }
    case "verify":
      requireState(s.stage === "verification");
      s.stage = "result";
      break;
    case "recover":
      requireState(
        s.stage === "result" &&
          s.recovery === "NotRequired" &&
          s.completed.some(
            (r) =>
              r.status === "Success" &&
              s.plan.operations.find((o) => o.id === r.id).reversibility ===
                "Reversible",
          ),
      );
      s.stage = "recovery";
      s.recovery = "InProgress";
      break;
    case "finishRecovery":
      requireState(s.stage === "recovery");
      s.recovery = s.scenario === "recoveryFailed" ? "Failed" : "Completed";
      s.stage = "result";
      break;
    default:
      throw new Error("Ação desconhecida.");
  }
  return s;
}
export const serialize = (s) => JSON.stringify(s);
export function restore(raw) {
  try {
    const s = JSON.parse(raw);
    if (
      s.version !== 1 ||
      !Object.hasOwn(scenarios, s.scenario) ||
      !stages.includes(s.stage) ||
      !Array.isArray(s.selected) ||
      !s.selected.every((id) =>
        features.some((f) => f.id === id && !f.status),
      ) ||
      !Array.isArray(s.completed) ||
      !["NotRequired", "InProgress", "Completed", "Failed"].includes(s.recovery)
    )
      return null;
    const needsPlan = [
      "preview",
      "execution",
      "verification",
      "result",
      "recovery",
    ].includes(s.stage);
    if (needsPlan) {
      if (
        !s.plan?.operations?.length ||
        !s.plan.operations.every((o) =>
          features.some((f) => f.id === o.id && !f.status),
        ) ||
        s.completed.length > s.plan.operations.length
      )
        return null;
      s.plan = {
        planId: "demo-plan",
        operations: s.plan.operations.map((o) => ({
          ...structuredClone(features.find((f) => f.id === o.id)),
          restart:
            s.scenario === "restart"
              ? "Reboot"
              : features.find((f) => f.id === o.id).restart,
        })),
      };
      if (
        !s.completed.every(
          (r, i) =>
            r.id === s.plan.operations[i].id &&
            ["Success", "Failed"].includes(r.status),
        )
      )
        return null;
      if (
        s.stage === "execution" &&
        s.completed.length >= s.plan.operations.length
      )
        return null;
      if (
        ["verification", "result", "recovery"].includes(s.stage) &&
        s.completed.length !== s.plan.operations.length
      )
        return null;
    } else if (s.plan !== null || s.completed.length) return null;
    return s;
  } catch {
    return null;
  }
}
