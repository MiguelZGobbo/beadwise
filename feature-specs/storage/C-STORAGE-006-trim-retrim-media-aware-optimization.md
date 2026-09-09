# Feature Specification — C-STORAGE-006 — TRIM, ReTRIM & media-aware optimization

## 3. Identificação

Nome: TRIM, ReTRIM & media-aware optimization
ID: C-STORAGE-006
Tipo: Diagnostic, Maintenance, Safety
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
Usar a otimização nativa do Windows de forma media-aware: ReTRIM para SSD suportado, defrag para HDD quando necessário e análise antes de agir.

**Base conceitual do Discovery:** [37] TRIM Capability Detection — `CANDIDATE`; [38] ReTRIM Capability — `CANDIDATE`; [39] TRIM Misconfiguration Detection — `CANDIDATE`; [40] Volume Optimization Analysis — `CANDIDATE`; [41] Media-Aware Optimize Volume — `CANDIDATE`; [42] HDD Fragmentation Analysis — `CANDIDATE`; [43] HDD Defragmentation — `CANDIDATE`; [44] SSD Manual Defrag Guard — `CANDIDATE`; [45] Storage Maintenance Schedule Detection — `CANDIDATE`; [46] Redundant Optimization Detection — `IDEA`; [153] TRIM Filesystem State Detection — `CANDIDATE`; [154] TRIM End-to-End Confidence — `RESEARCHING`; [155] TRIM Pass-Through Context — `IDEA`; [156] ReTRIM Need Analysis — `CANDIDATE`

### Objetivo
Detectar se delete notifications/TRIM e manutenção de volume estão coerentes e executar apenas a operação apropriada ao tipo de mídia/provisioning.

### Problema que resolve
Tweaks populares confundem defrag de HDD, ReTRIM de SSD e TRIM capability; executar a operação errada pode ser inútil ou aumentar escrita.

### Benefício esperado
Performance / Maintenance

## 5. Aplicabilidade

### Quando se aplica?
Volumes suportados pelo Optimize-Volume; TRIM depende de filesystem e backing storage.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não “desfragmentar SSD para ficar mais rápido” como regra.
- Não habilitar TRIM se backing storage não suporta e não tratar DisableDeleteNotify=0 como prova end-to-end.
- Não repetir optimize se o Windows já executou manutenção adequada e Analyze não indica necessidade.

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
Analyze/Windows state indica necessidade; TRIM notifications desabilitadas em filesystem suportado e hardware compatível; manutenção automática ausente/fracassando.

### Motivo apresentado ao usuário
Mostrar operação nativa escolhida por mídia e por que ela é necessária.

### Grau de confiança
High para escolha padrão documentada do Optimize-Volume; Medium para “TRIM end-to-end” quando bridge/RAID não expõe pass-through.

## 7. Estado atual

### O que precisa ser detectado?
- fsutil behavior query DisableDeleteNotify para estado do filesystem.
- Optimize-Volume -Analyze para estado de otimização.
- output de C-STORAGE-001 para media/provisioning.
- Registrar histórico da manutenção para evitar repetição.

### Como detectar?
Preferir Optimize-Volume sem parâmetros ou operação explícita (-ReTrim/-Defrag) somente após análise e classificação da mídia; nunca enviar comando vendor bruto.

### Fonte da verdade
Optimize-Volume/Windows storage stack define operação por drive type; DisableDeleteNotify indica emissão de notificações, não garante recebimento físico.

### Estados possíveis
- No action needed
- ReTRIM candidate
- HDD defrag candidate
- TRIM notifications disabled
- Unsupported media
- Partial end-to-end confidence
- Error

## 8. Estado alvo
Volume otimizado pelo mecanismo Windows apropriado; TRIM filesystem state coerente quando alteração explicitamente justificada.

## 9. Implementação técnica

### Método principal
Preferir Optimize-Volume sem parâmetros ou operação explícita (-ReTrim/-Defrag) somente após análise e classificação da mídia; nunca enviar comando vendor bruto.

### Tecnologias utilizadas
- [ ] .NET API
- [ ] Win32
- [ ] Registry
- [x] PowerShell
- [ ] CMD / executable
- [x] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- Optimize-Volume -Analyze
- Optimize-Volume -ReTrim
- Optimize-Volume -Defrag
- fsutil behavior query/set DisableDeleteNotify (set apenas condicional)

### Alternativas avaliadas
defrag.exe oferece funcionalidade similar; PowerShell Storage é mais estruturado para integração/protótipo.

### Abordagem escolhida
Optimize-Volume já implementa política media-aware documentada.

## 10. Permissões

### Requer administrador?
Yes

### Quando ocorre a elevação?
Antes de Apply.

### O que acontece se não houver permissão?
ADMIN_REQUIRED; Detect pode continuar parcialmente.

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
Feature: C-STORAGE-006
Current state: resultado estruturado de Detect
Target state: Volume otimizado pelo mecanismo Windows apropriado; TRIM filesystem state coerente quando alteração explicitamente justificada.
Changes: derivadas somente se a capability tiver Apply aprovado; caso contrário lista vazia
Admin required: Yes
Restart required: Nada
Risk: Low
Reversible: Partially
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
Yes

### O que precisa ser salvo antes da alteração?
Capturar todos os valores/estados que o ChangePlan pretende alterar, incluindo ausência de valor/objeto quando relevante, além do stable-id do alvo e timestamp.

### Estado inexistente também deve ser registrado
Yes — ausência é um estado válido do snapshot.

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
Volume otimizado pelo mecanismo Windows apropriado; TRIM filesystem state coerente quando alteração explicitamente justificada.

### Resultado parcial
Yes — quando a fonte da verdade expõe subset, quando uma camada abstrai hardware ou quando parte independente do plano falha de maneira representável. Resultado parcial nunca é promovido silenciosamente a sucesso total.

## 17. Rollback

### É reversível?
Partially

### Método de rollback
Partially. Restaurar exatamente o snapshot quando tecnicamente possível. Operações irreversíveis ou de reparo devem ser marcadas como tal e nunca fingir rollback por escrever “default”.

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
Low

### Possíveis efeitos negativos
- I/O adicional.
- Alterar DisableDeleteNotify incorretamente pode mudar semantics global do filesystem.

### Pior cenário plausível
Configurar TRIM indevidamente em cenário incomum; snapshot do estado e apply só quando suporte é conhecido.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Windows 10/11; NTFS/ReFS e storage thin provisioning diferem.

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
- Admin
- Optimize-Volume

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Ferramentas vendor de SSD e políticas corporativas de maintenance.

## 24. Idempotência
Conditional

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- UNSUPPORTED_MEDIA
- ADMIN_REQUIRED
- ANALYZE_FAILED
- OPTIMIZE_FAILED
- VERIFY_FAILED

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
Snapshot de DisableDeleteNotify se alterado e último Optimize result.

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
Validar HDD, NVMe SSD e ao menos thin-provisioned/virtual cenário; confirmar Analyze→Apply→Analyze e que SSD path usa ReTRIM.

### Rollback
Testar snapshot→apply→verify→rollback→verify rollback incluindo estado inicial personalizado e inexistente.

### VM
Partial

### Hardware real
Required

## 29. Cenários mínimos de teste
- [ ] Feature já está no estado desejado
- [ ] Feature precisa ser alterada
- [ ] Feature não é suportada
- [ ] Falta permissão
- [ ] Apply falha
- [ ] Verify falha
- [ ] Rollback funciona
- [ ] Rollback falha
- [ ] Estado inicial personalizado
- [ ] Configuração inexistente
- [ ] Execução repetida

## 30. Prova técnica

### Script/protótipo
Local planejado: `/prototypes/storage/c-storage-006/`

### Resultado
Native semantics documented; media matrix PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- trim: https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/fsutil-behavior
- optimize: https://learn.microsoft.com/en-us/powershell/module/storage/optimize-volume?view=windowsserver2025-ps

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior (campanha 2026-09-08):** nenhuma prova específica desta capability foi executada neste host; resultado `NOT_TESTED`. A necessidade de prototype foi reavaliada e o status `SPECIFIED` foi preservado porque os gates aplicáveis continuam abertos.

<!-- PHASE2-SAFE-PROOF-20260908:START -->
**Observed safe proof (auditoria final, 2026-09-08):**
- Capability/premissa exercitada: TRIM policy capability can be queried without issuing retrim.
- Resultado observado deste probe: `PASS`.
- Evidência reproduzível: `/prototypes/storage/C-STORAGE-001/results/storage-proof.json` (script e teste no mesmo prototype).
- Limites preservados: does not prove end-to-end TRIM through RAID/USB
- Este resultado substitui `NOT_TESTED` somente para a premissa acima; não satisfaz por si só todos os cenários mínimos nem promove a Feature Spec a `PROVEN`/`APPROVED`.
<!-- PHASE2-SAFE-PROOF-20260908:END -->
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:START -->
**Auditoria final da necessidade de prova (2026-09-08):**
- Prova prática adicional essencial: Sim — TRIM capability/analysis sem retrim.
- Evidência realmente executada: Prototype seguro adicional executado: TRIM policy capability can be queried without issuing retrim. Resultado observado: `PASS`. Evidência: `/prototypes/storage/C-STORAGE-001/results/storage-proof.json`. A evidência anterior foi substituída por esta observação mais recente.
- Execução segura neste host / teste: Sim — `fsutil behavior query DisableDeleteNotify` e `Optimize-Volume -Analyze` onde suportado; não `-ReTrim`.
- Impedimento ou limitação restante: End-to-end TRIM sob RAID/USB não é verificável neste host.
- Disposição: `SPECIFIED`. Nenhum `PASS` foi inferido; o status reflete somente a evidência e os bloqueios registrados.
<!-- PHASE2-FINAL-SPECIFIED-AUDIT:END -->

## 32. Benefício real
Reasonable

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
Partial

### Como?
Verify por re-analyze e estado; ganho de performance não é garantido nem deve ser prometido.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Media type
- Optimization state
- Planned operation
- TRIM filesystem state
- Why/no action
- Risk

### Ações disponíveis
- Optimize
- Details

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- optimizationState
- mediaType
- trimState
- plannedOperation
- reason
- requiresAdmin
- result

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Como representar end-to-end TRIM confidence sob RAID/USB sem vendor-specific ATA passthrough?

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
