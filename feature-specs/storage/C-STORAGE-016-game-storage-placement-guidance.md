# Feature Specification — C-STORAGE-016 — Game storage placement guidance

## 3. Identificação

Nome: Game storage placement guidance
ID: C-STORAGE-016
Tipo: Diagnostic / Recommendation, Recommendation
Technical Domain: STORAGE
Primary Product Area: TBD
Also Used By: My PC, Monitoring, Optimization, Cleaning, Benchmark, Gaming
Shared Capability: No
Final UI Placement: TBD
Status: RESEARCH
Prioridade: TBD
Responsável: TBD
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Recomendar localização de jogos somente quando há evidência de storage significativamente mais adequado e caminho de migração seguro.

**Base conceitual do Discovery:** [121] Game Storage Location Analysis — `IDEA`; [122] Move Game to Faster Storage Advisor — `IDEA`

### Objetivo
Identificar jogos em HDD/storage lento quando outro volume local saudável oferece melhor perfil e espaço suficiente, sem prometer FPS médio maior.

### Problema que resolve
Storage pode afetar loading/streaming/stutter em jogos, mas mover jogo entre SSDs similares pode não trazer benefício perceptível.

### Benefício esperado
Performance / Quality of Life

## 5. Aplicabilidade

### Quando se aplica?
Jogos cuja localização pode ser detectada por launcher/app e volumes comparáveis.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não recomendar mover entre SSDs equivalentes sem evidência.
- Não mover arquivos diretamente quando launcher exige mecanismo próprio.
- Não prometer aumento de FPS médio; benefício típico é load/streaming e depende do jogo.

## 5.1 Uso dentro do produto

### Consumidores
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

### Uso direto pelo usuário
Yes

### Capacidade compartilhada
N/A — o Discovery classifica `Shared Capability: No`. Outros consumidores podem reutilizar o output desta capability, mas o ownership permanece aqui e nenhuma infraestrutura compartilhada adicional é criada por esta spec.

## 6. Recomendação

### A feature deve ser recomendável automaticamente?
Conditional

### Critérios de recomendação
Jogo localizado em HDD ou storage com anomalia/limitação conhecida; destino saudável com espaço; game/workload plausivelmente storage-sensitive; método de move suportado.

### Motivo apresentado ao usuário
Explicar diferença de media/health/performance e qual aspecto pode melhorar.

### Grau de confiança
Medium; High só com benchmark/game-specific evidence.

## 7. Estado atual

### O que precisa ser detectado?
- Correlacionar install path do launcher/plugin futuro a volume.
- Storage inventory/health/performance baseline do origem/destino.
- DirectStorage readiness quando relevante.

### Como detectar?
Scoring condicional sem magic weights fixos: primeiro regras categóricas (HDD→healthy SSD), depois evidência de baseline; casos ambíguos não recomendam.

### Fonte da verdade
Localização real + características do volume; benchmark sintético é apoio, não garantia de ganho no jogo.

### Estados possíveis
- No better target
- Candidate target
- Insufficient space
- Destination unhealthy
- Move unsupported
- Already optimal

## 8. Estado alvo
Recommendation only nesta spec; execução de move pertence a integração específica de launcher e não é implementada genericamente.

## 9. Implementação técnica

### Método principal
Scoring condicional sem magic weights fixos: primeiro regras categóricas (HDD→healthy SSD), depois evidência de baseline; casos ambíguos não recomendam.

### Tecnologias utilizadas
- [x] .NET API
- [ ] Win32
- [ ] Registry
- [ ] PowerShell
- [ ] CMD / executable
- [ ] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [x] Other

### Comandos / APIs / chaves
- Launcher/plugin APIs TBD

### Alternativas avaliadas
Mover diretório manualmente com junction é rejeitado como mecanismo universal.

### Abordagem escolhida
Recommendation first; launcher-specific execution future.

## 10. Permissões

### Requer administrador?
No

### Quando ocorre a elevação?
N/A para diagnóstico.

### O que acontece se não houver permissão?
N/A

## 11. Reinicialização

### Requer
- [x] Nada
- [ ] Reinício do aplicativo
- [ ] Reinício de processo
- [ ] Reinício de serviço
- [ ] Logoff
- [ ] Reboot do Windows
- [ ] Desconhecido

**Nota:** Nada

### A alteração só pode ser validada depois da reinicialização?
No

### Rollback também exige reinicialização?
No

## 12. Change Plan

```text
Feature: C-STORAGE-016
Current state: resultado estruturado de Detect
Target state: Recommendation only nesta spec; execução de move pertence a integração específica de launcher e não é implementada genericamente.
Changes: derivadas somente se a capability tiver Apply aprovado; caso contrário lista vazia
Admin required: No
Restart required: Nada
Risk: Low
Reversible: N/A
```

O plano é gerado a partir do mesmo resultado de Detect usado pelo dry-run; nenhum target mutável pode ser inventado pela UI.

## 13. Dry-run

### É suportado?
Yes

### O que o dry-run deve fazer?
- Detectar e validar prerequisites.
- Construir o ChangePlan real.
- Exibir estado, evidência, risco, admin/restart e mudanças exatas quando existirem.
- Para feature somente leitura, mostrar que `Changes = []` em vez de simular Apply inexistente.

### O que o dry-run NÃO pode fazer?
Não executar alterações persistentes, não iniciar benchmark write/repair e não alterar política do sistema.

### Limitações
Resultados que dependem de carga, temperatura, hardware/vendor ou reboot só podem ser confirmados na prova/execução correspondente.

## 14. Snapshot

### É necessário?
No

### O que precisa ser salvo antes da alteração?
N/A — feature somente leitura ou guard; nenhum estado do sistema é alterado.

### Estado inexistente também deve ser registrado
N/A.

## 15. Apply

### Sequência de execução
N/A — esta capability não executa alteração persistente. Para consumidores, apenas Detect → interpretar → retornar estado/guard.

### Atomicidade
N/A.

## 16. Verify

### Como comprovar que funcionou?
Executar novamente a mesma detecção e validar schema/coerência; para diagnostics, sucesso significa dados válidos ou estado Unsupported/Partial explicitamente representado.

### Critério de sucesso
Recommendation only nesta spec; execução de move pertence a integração específica de launcher e não é implementada genericamente.

### Resultado parcial
Yes — quando a fonte da verdade expõe subset, quando uma camada abstrai hardware ou quando parte independente do plano falha de maneira representável. Resultado parcial nunca é promovido silenciosamente a sucesso total.

## 17. Rollback

### É reversível?
N/A

### Método de rollback
N/A — nenhuma alteração persistente.

### Ordem de reversão
Para mudanças compostas, ordem inversa quando tecnicamente apropriado; N/A para diagnostics.

## 18. Verify Rollback

### Critério de sucesso
N/A.

### O que fazer se rollback falhar?
Registrar `ROLLBACK_FAILED`, manter snapshot/audit log, permitir retry somente se seguro e fornecer orientação manual/System Restore apenas quando realmente aplicável.

## 19. System Restore

### A feature exige ponto de restauração?
Not Required

### Motivo
A decisão segue o risco real da operação. Diagnostics/guards não justificam restore point; operações com rollback próprio não devem usar System Restore como mecanismo primário.

## 20. Risco

### Classificação
Low

### Possíveis efeitos negativos
- Recomendação pode consumir espaço valioso em SSD.

### Pior cenário plausível
Jogo deixa de funcionar se move manual; por isso Apply não existe sem integração suportada.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Depende de launchers e formatos; initial recommendation pode usar paths fornecidos pelo usuário.

Matriz mínima a validar antes de `PROVEN`:
- Windows 10 22H2: expected/test TBD quando aplicável.
- Windows 11 23H2: expected/test TBD.
- Windows 11 24H2: expected/test TBD.
- Windows 11 25H2: expected/test TBD.

### Arquitetura
- x64: Expected; test TBD.
- ARM64: Expected somente se APIs/binários usados existirem; test TBD.

### Hardware
A compatibilidade de hardware segue as limitações descritas acima; `Unsupported`/`Partial` são estados válidos e nunca devem ser convertidos em erro genérico.

## 22. Dependências

### Outras features
- output de C-STORAGE-001
- output de C-STORAGE-002
- output de C-STORAGE-009
- Launcher detection future

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Cloud/game launcher update/move operations.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- GAME_PATH_UNKNOWN
- NO_BETTER_TARGET
- INSUFFICIENT_SPACE

Códigos definitivos serão alinhados à arquitetura global; estes nomes descrevem semanticamente os casos necessários.

## 26. Logging
- Detection started / completed com stable target id e provenance.
- Current state + confidence/partial flags.
- ChangePlan generated e guard decision.
- Snapshot captured quando aplicável.
- Apply/Verify/Rollback lifecycle quando aplicável.
- Falhas estruturadas e restart/admin requirements.
- Nunca registrar secrets, conteúdo de arquivos do usuário ou recovery keys.

## 27. Persistência

### O estado precisa sobreviver ao fechamento do app?
Optional

### O snapshot precisa sobreviver reboot?
No

### Arquivos/dados necessários
Preferências e game→volume mapping, não credenciais.

## 28. Testes

### Teste unitário
- Parsing/normalização dos estados e erros.
- Regras de recomendação/guard.
- Geração determinística de ChangePlan.
- Comparação actual vs expected e snapshot quando aplicável.
- Compatibilidade/Unsupported/Partial.

### Dry-run
Gerar exatamente o plano que seria executado e provar que nenhuma alteração persistente ocorre.

### Integração real
Comparar HDD vs SSD em jogos com asset streaming/load; validar que recommendation não aparece para SSDs similares sem evidence.

### Rollback
N/A para feature somente leitura; testar que não existe path de mutação.

### VM
Not Suitable

### Hardware real
Required

## 29. Cenários mínimos de teste
- [ ] Feature já está no estado desejado
- [ ] Estado normal/healthy
- [ ] Feature não é suportada
- [ ] Falta permissão
- [ ] Detection falha
- [ ] Verify falha
- [ ] Resultado Partial/Unknown
- [ ] Fonte secundária indisponível
- [ ] Estado inicial personalizado
- [ ] Configuração inexistente
- [ ] Execução repetida

## 30. Prova técnica

### Script/protótipo
Local planejado: `/prototypes/storage/c-storage-016/`

### Resultado
Recommendation model specified; game evidence calibration PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- directstorage_req: https://learn.microsoft.com/en-us/windows/whats-new/windows-11-requirements
- performance: https://learn.microsoft.com/en-us/windows-server/storage/storage-spaces/performance-history-for-drives

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.

<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — game placement source/performance context.
- Evidência realmente executada: NENHUMA.
- Execução segura neste host / teste: Parcial — inventário de volumes somente.
- Impedimento ou limitação restante: Escopo de launchers/APIs suportadas ainda não definido e não há install/move controlado.
- Disposição: `RESEARCH`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

<!-- PHASE2-FINAL-RESEARCH-AUDIT:START -->
**Reavaliação final de RESEARCH (2026-09-08):**
- O status `RESEARCH` foi revisto e preservado porque ainda há incerteza técnica solucionável descrita nas seções 30, 31 e 36; documentação ou probe compartilhado parcial não foi convertido em `PASS`.
- Nenhuma prova prática isolada nesta máquina elimina essa incerteza sem antes definir fonte, contrato, fixture, hardware ou dependência indicada pela própria spec.
- Próximo gate: concluir a investigação registrada, então decidir se cabe prototype seguro, `SPECIFIED`, `BLOCKED`, `DEFERRED` ou `REJECTED`; não promover diretamente a `PROVEN`/`APPROVED`.
<!-- PHASE2-FINAL-RESEARCH-AUDIT:END -->

## 32. Benefício real
Situational

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
Partial

### Como?
Loading time/stutter/asset streaming profile when game-specific test exists; otherwise no performance claim.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Current drive
- Candidate drive
- Why
- Expected effect
- Space required
- Confidence

### Ações disponíveis
- Details
- Open launcher move instructions

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- gameId
- currentVolume
- candidateVolume
- reason
- expectedEffect
- confidence

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Quais launchers entram no escopo e APIs suportadas para detectar/mover instalações.

## 37. Critério para PROVEN
- [ ] Detect funciona nos ambientes aplicáveis.
- [ ] ChangePlan representa corretamente alterações ou lista vazia para diagnostic.
- [ ] Dry-run não altera o sistema.
- [ ] Apply/Verify funcionam quando aplicáveis.
- [ ] Snapshot/Rollback/Verify Rollback funcionam quando aplicáveis.
- [ ] Riscos principais validados.
- [ ] Compatibilidade mínima testada.
- [ ] Falhas conhecidas documentadas.

**Estado atual:** SPECIFIED. Não é `PROVEN` sem a prova técnica registrada.

## 38. Critério para APPROVED
Mesmo após `PROVEN`, requer avaliação de utilidade, confiabilidade, escopo, risco, manutenção, UX, duplicação e fragilidade conforme o template canônico.
