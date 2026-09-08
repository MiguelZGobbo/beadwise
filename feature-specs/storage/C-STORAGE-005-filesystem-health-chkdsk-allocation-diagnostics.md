# Feature Specification — C-STORAGE-005 — Filesystem health, CHKDSK & allocation diagnostics

## 3. Identificação

Nome: Filesystem health, CHKDSK & allocation diagnostics  
ID: C-STORAGE-005  
Tipo: Configuration, Diagnostic, Diagnostic / Repair, Repair, Safety / Diagnostic  
Technical Domain: STORAGE  
Primary Product Area: TBD  
Also Used By: My PC, Monitoring, Optimization, Cleaning, Benchmark, Gaming  
Shared Capability: No  
Final UI Placement: TBD  
Status: SPECIFIED  
Prioridade: TBD  
Responsável: TBD  
Última revisão: 2026-09-08

## 4. Resumo

### O que é?
Diagnosticar integridade do filesystem e oferecer CHKDSK/repair somente quando há evidência, separando scan online de reparo potencialmente disruptivo.

**Base conceitual do Discovery:** [32] Filesystem Type Detection — `CANDIDATE`; [33] Filesystem Health Detection — `CANDIDATE`; [34] CHKDSK Online Scan — `CANDIDATE`; [35] CHKDSK Repair Scheduling — `CANDIDATE`; [36] Filesystem Error History — `CANDIDATE`; [79] Filesystem Allocation Unit Detection — `IDEA`; [80] Filesystem Compression Detection — `CANDIDATE`; [81] CompactOS State Detection — `CANDIDATE`; [82] CompactOS Configuration — `RESEARCHING`; [83] Sparse File Awareness — `IDEA`; [84] Hard Link Awareness — `CANDIDATE`

### Objetivo
Detectar dirty bit/corrupção reportada, executar scan não destrutivo quando apropriado e planejar reparo explícito com reboot/offline quando necessário.

### Problema que resolve
Executar CHKDSK /f /r indiscriminadamente pode ser lento/disruptivo; ignorar sinais de corrupção também é perigoso.

### Benefício esperado
Repair / Stability

## 5. Aplicabilidade

### Quando se aplica?
Volumes locais com filesystem suportado; lógica difere NTFS/ReFS e volume de sistema.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não executar /r como “otimização”.
- Não forçar lock/desmontagem de volume com dados ativos sem decisão explícita.
- CompactOS configuration não é objetivo principal desta capability; apenas detectar contexto quando relevante.

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
Dirty bit, filesystem events/corruption logs, scan report, self-healing backlog ou erro de I/O coerente com filesystem.

### Motivo apresentado ao usuário
Informar evidência, escopo do scan/reparo, possível indisponibilidade e reboot.

### Grau de confiança
High quando dirty bit/scan reporta problema; Medium quando somente eventos históricos.

## 7. Estado atual

### O que precisa ser detectado?
- fsutil dirty query para NTFS dirty bit.
- fsutil repair query/enumerate para NTFS self-healing quando aplicável.
- CHKDSK read-only/scan mode planejado conforme filesystem/volume.
- fsutil fsinfo para filesystem/allocation metadata.

### Como detectar?
Separar Detect/Scan e Repair em operações distintas. Reparos usam ferramentas Windows documentadas; /r só quando há razão para procurar setores defeituosos/erros físicos, não como rotina.

### Fonte da verdade
Estado retornado pelo filesystem/repair facilities; event log é complementar.

### Estados possíveis
- Healthy
- Dirty
- Corruption detected
- Repair pending
- Repair requires offline/reboot
- Unsupported
- Unknown/Error

## 8. Estado alvo
Scan: estado conhecido. Repair: volume retorna sem corrupção reportada e dirty/pending state resolvido, preservando dados.

## 9. Implementação técnica

### Método principal
Separar Detect/Scan e Repair em operações distintas. Reparos usam ferramentas Windows documentadas; /r só quando há razão para procurar setores defeituosos/erros físicos, não como rotina.

### Tecnologias utilizadas
- [ ] .NET API
- [ ] Win32
- [ ] Registry
- [ ] PowerShell
- [x] CMD / executable
- [x] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- fsutil dirty query
- fsutil repair query/enumerate
- chkdsk (mode chosen by condition)

### Alternativas avaliadas
Repair-Volume PowerShell pode ser adapter equivalente; manter uma fonte de decisão comum.

### Abordagem escolhida
Facilities nativas do filesystem reduzem comportamento não documentado.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Scan/reparo privilegiado e volume handles podem exigir admin.

### O que acontece se não houver permissão?
ADMIN_REQUIRED antes de ChangePlan de repair.

## 11. Reinicialização

### Requer
- [x] Nada
- [ ] Reinício do aplicativo
- [ ] Reinício de processo
- [ ] Reinício de serviço
- [ ] Logoff
- [x] Reboot do Windows
- [ ] Desconhecido

**Nota:** Conditional: Nada / Reboot do Windows

### A alteração só pode ser validada depois da reinicialização?
Quando reparo do volume de sistema for agendado, validação final só após reboot.

### Rollback também exige reinicialização?
N/A; reparo de filesystem não possui rollback determinístico.

## 12. Change Plan

```text
Feature: C-STORAGE-005
Current state: resultado estruturado de Detect
Target state: Scan: estado conhecido. Repair: volume retorna sem corrupção reportada e dirty/pending state resolvido, preservando dados.
Changes: derivadas somente se a capability tiver Apply aprovado; caso contrário lista vazia
Admin required: Conditional
Restart required: Conditional: Nada / Reboot do Windows
Risk: Medium
Reversible: No
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
1. Revalidar compatibilidade/preconditions.
2. Revalidar o ChangePlan imediatamente antes da execução.
3. Capturar snapshot necessário.
4. Executar somente as operações aprovadas.
5. Re-detectar estado.
6. Se Verify falhar, seguir estratégia de rollback/falha definida.

### Atomicidade
Cada operação mutável deve declarar se é independente. Em falha parcial, não continuar mudanças dependentes; restaurar mudanças já aplicadas quando rollback determinístico existir.

## 16. Verify

### Como comprovar que funcionou?
Re-detectar pela mesma fonte da verdade e comparar estado real com o target. Exit code 0 isolado nunca é suficiente.

### Critério de sucesso
Scan: estado conhecido. Repair: volume retorna sem corrupção reportada e dirty/pending state resolvido, preservando dados.

### Resultado parcial
Yes — quando a fonte da verdade expõe subset, quando uma camada abstrai hardware ou quando parte independente do plano falha de maneira representável. Resultado parcial nunca é promovido silenciosamente a sucesso total.

## 17. Rollback

### É reversível?
No

### Método de rollback
No. Restaurar exatamente o snapshot quando tecnicamente possível. Operações irreversíveis ou de reparo devem ser marcadas como tal e nunca fingir rollback por escrever “default”.

### Ordem de reversão
Para mudanças compostas, ordem inversa quando tecnicamente apropriado; N/A para diagnostics.

## 18. Verify Rollback

### Critério de sucesso
Re-detectar e comparar com snapshot original; divergência gera ROLLBACK_FAILED e orientação manual/escalonamento.

### O que fazer se rollback falhar?
Registrar `ROLLBACK_FAILED`, manter snapshot/audit log, permitir retry somente se seguro e fornecer orientação manual/System Restore apenas quando realmente aplicável.

## 19. System Restore

### A feature exige ponto de restauração?
Not Required

### Motivo
A decisão segue o risco real da operação. Diagnostics/guards não justificam restore point; operações com rollback próprio não devem usar System Restore como mecanismo primário.

## 20. Risco

### Classificação
Medium

### Possíveis efeitos negativos
- Long downtime.
- Repair pode descartar estruturas irrecuperáveis.
- /r pode exercer I/O intenso em disco degradado.

### Pior cenário plausível
Perda de dados já corrompidos/irrecuperáveis; orientar backup quando possível antes de repair.

### Recuperação manual possível?
Partial

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
NTFS e ReFS comportam-se de modo diferente; validar por Windows 10/11.

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
- Admin privileges
- filesystem utilities
- output de C-STORAGE-002 para diferenciar problema físico

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- VSS/shadow copies e open handles podem afetar lock/scan.

## 24. Idempotência
Conditional

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- ADMIN_REQUIRED
- VOLUME_IN_USE
- SCAN_FAILED
- REPAIR_SCHEDULED
- REPAIR_FAILED
- REBOOT_REQUIRED

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
Yes

### O snapshot precisa sobreviver reboot?
Yes

### Arquivos/dados necessários
Persistir job de repair planejado, evidência anterior e estado pós-reboot.

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
VM com volume de teste e cenários dirty/corrupção controlada; hardware real somente para caminhos que envolvam erro físico.

### Rollback
N/A para feature somente leitura; testar que não existe path de mutação.

### VM
Safe

### Hardware real
Optional

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
Local planejado: `/prototypes/storage/c-storage-005/`

### Resultado
Workflow documented; controlled corruption/repair prototype PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- dirty: https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/fsutil-dirty
- chkdsk: https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/chkdsk
- fsrepair: https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/fsutil-repair
- fsinfo: https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/fsutil-fsinfo

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.

## 32. Benefício real
Proven

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
Yes

### Como?
Erro resolvido: Detect pós-reparo sem dirty/corruption report; não usar performance como métrica.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Filesystem
- Health state
- Evidence
- Scan/repair mode
- Estimated disruption (qualitative)
- Restart requirement
- Backup warning

### Ações disponíveis
- Scan
- Repair (when recommended)
- Details

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- filesystem
- healthState
- evidence[]
- repairMode
- requiresRestart
- risk
- jobState

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Mapear exatamente modos CHKDSK/Repair-Volume por NTFS vs ReFS e build.

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
