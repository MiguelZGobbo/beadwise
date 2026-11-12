import { test } from "node:test";
import assert from "node:assert/strict";
import { createSession, transition, restore, serialize } from "./state.js";
test("selection requires a fresh preview and cannot change an approved plan", () => {
  let s = transition(createSession(), "analyze");
  s = transition(s, "finishAnalysis");
  s = transition(s, "toggle", "energy");
  s = transition(s, "preview");
  assert.equal(s.plan.operations.length, 1);
  s = transition(s, "approve");
  assert.throws(() => transition(s, "toggle", "startup"));
  assert.deepEqual(restore(serialize(s)), s);
});
test("empty selection cannot be approved", () =>
  assert.throws(() => transition(createSession(), "preview")));
test("invalid stored sessions are rejected", () => {
  assert.equal(restore("{"), null);
  assert.equal(restore("{}"), null);
});
test("irreversible operation is never recovered", () => {
  let s = transition(createSession(), "analyze");
  s = transition(s, "finishAnalysis");
  s = transition(s, "toggle", "cleanup");
  s = transition(s, "preview");
  s = transition(s, "approve");
  while (s.stage === "execution") s = transition(s, "step");
  s = transition(s, "verify");
  assert.throws(() => transition(s, "recover"));
});
test("all demonstration outcomes complete deterministically", () => {
  for (const scenario of [
    "success",
    "partial",
    "restart",
    "recovery",
    "recoveryFailed",
  ]) {
    let s = transition(createSession(scenario), "analyze");
    s = transition(s, "finishAnalysis");
    s = transition(s, "toggle", "energy");
    s = transition(s, "toggle", "startup");
    s = transition(s, "preview");
    s = transition(s, "approve");
    while (s.stage === "execution") s = transition(s, "step");
    s = transition(s, "verify");
    assert.equal(s.stage, "result");
    if (scenario.startsWith("recovery")) {
      s = transition(s, "recover");
      s = transition(s, "finishRecovery");
      assert.equal(
        s.recovery,
        scenario === "recoveryFailed" ? "Failed" : "Completed",
      );
    }
  }
});

function analyzed(scenario = "success") {
  return transition(
    transition(createSession(scenario), "analyze"),
    "finishAnalysis",
  );
}
test("editing invalidates preview and rebuilds its operations", () => {
  let s = transition(analyzed(), "toggle", "energy");
  s = transition(s, "preview");
  s = transition(s, "back");
  assert.equal(s.plan, null);
  s = transition(s, "toggle", "energy");
  s = transition(s, "toggle", "startup");
  s = transition(s, "preview");
  assert.deepEqual(
    s.plan.operations.map((o) => o.id),
    ["startup"],
  );
});
test("empty scenario cannot select recommendations", () =>
  assert.throws(() => transition(analyzed("empty"), "toggle", "energy")));
test("admin denial blocks approval but allows revising plan", () => {
  let s = transition(analyzed("admin"), "toggle", "energy");
  s = transition(s, "preview");
  assert.throws(() => transition(s, "approve"));
  assert.equal(transition(s, "back").stage, "choices");
});
test("analysis cancellation leaves system unchanged", () => {
  const s = transition(transition(createSession(), "analyze"), "cancel");
  assert.equal(s.stage, "home");
  assert.equal(s.plan, null);
  assert.equal(s.cancelled, true);
});
test("execution and recovery can be restored without losing progress", () => {
  let s = transition(analyzed(), "toggle", "energy");
  s = transition(s, "preview");
  s = transition(s, "approve");
  s = restore(serialize(s));
  assert.equal(s.stage, "execution");
  s = transition(s, "step");
  s = transition(s, "verify");
  s = transition(s, "recover");
  assert.deepEqual(restore(serialize(s)), s);
});
test("unavailable observation cannot enter the plan", () =>
  assert.throws(() => transition(analyzed("unavailable"), "toggle", "energy")));
test("restart requirement exists in approved plan", () => {
  let s = transition(analyzed("restart"), "toggle", "startup");
  s = transition(s, "preview");
  assert.equal(s.plan.operations[0].restart, "Reboot");
});
test("restoration rejects impossible completion and canonicalizes content", () => {
  let s = transition(analyzed(), "toggle", "energy");
  s = transition(s, "preview");
  s.plan.operations[0].title = "<script>unsafe</script>";
  assert.equal(
    restore(serialize(s)).plan.operations[0].title,
    "Revisar suspensão de dispositivos USB",
  );
  s.stage = "result";
  assert.equal(restore(serialize(s)), null);
});
