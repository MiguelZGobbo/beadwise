# Feature Specification — C-STORAGE-012 — System restore, VSS & shadow-storage diagnostics

## 3. Identificação

Nome: System restore, VSS & shadow-storage diagnostics  
ID: C-STORAGE-012  
Tipo: Cleanup, Configuration / Recommendation, Diagnostic  
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
Diagnosticar snapshots VSS, shadow storage, writers/providers e pressão de espaço; não apagar restore points automaticamente.

**Base conceitual do Discovery:** [98] System Restore Storage Usage — `CANDIDATE`; [99] Shadow Storage Pressure Detection — `IDEA`; [160] VSS Snapshot Inventory — `CANDIDATE`; [161] Shadow Storage Allocation Detection — `CANDIDATE`; [162] Shadow Storage Usage Analysis — `CANDIDATE`; [163] Shadow Storage Resize Advisor — `RESEARCHING`; [164] Delete Restore Points for Space — `QUESTIONABLE`; [165] VSS Writer Health — `CANDIDATE`; [166] VSS Provider Inventory — `IDEA`; [167] Orphaned / Excessive Snapshot Diagnosis — `IDEA`

### Objetivo
Explicar uso de VSS/System Restore e falhas de writer/provider sem confundir espaço ocupado com lixo.

### Problema que resolve
Shadow storage pode crescer ou falhar, mas snapshots são mecanismos de recuperação/backup e remoção agressiva reduz segurança.

### Benefício esperado
Diagnostic / Repair

## 5. Aplicabilidade

### Quando se aplica?
Sistemas com VSS disponível e volumes participantes.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Delete Restore Points for Space permanece QUESTIONABLE e fora de Apply.
- Resize shadow storage não é recomendado automaticamente; permanece advisory/research até critérios robustos.

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
Writer failed, shadow storage pressionando volume de forma demonstrável, snapshot orphan/excessive segundo contexto de backup conhecido.

### Motivo apresentado ao usuário
Mostrar que snapshots protegem recuperação e qualquer redução pode afetar restore/backup.

### Grau de confiança
High para inventory/writer state; Medium para “excessive” sem política de backup conhecida.

## 7. Estado atual

### O que precisa ser detectado?
- vssadmin list shadows/shadowstorage/writers/providers como protótipo.
- Futuramente VSS COM APIs para output estruturado quando viável.

### Como detectar?
Read-only VSS diagnostics; parser robusto ou COM API preferível em runtime.

### Fonte da verdade
VSS provider/writer/snapshot state; não interpretar tamanho isoladamente como desperdício.

### Estados possíveis
- Healthy
- Writer error
- Provider issue
- Shadow storage pressure
- Snapshots present
- No snapshots
- Unknown

## 8. Estado alvo
Diagnóstico. Apply N/A nesta spec para deletion; resize advisor apenas recommendation manual se futuramente validado.

## 9. Implementação técnica

### Método principal
Read-only VSS diagnostics; parser robusto ou COM API preferível em runtime.

### Tecnologias utilizadas
- [ ] .NET API
- [x] Win32
- [ ] Registry
- [ ] PowerShell
- [x] CMD / executable
- [ ] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- vssadmin list shadows
- vssadmin list shadowstorage
- vssadmin list writers
- vssadmin list providers

### Alternativas avaliadas
WMI/COM VSS APIs fornecem estrutura melhor, mas exigem maior implementação; investigar para backend final.

### Abordagem escolhida
Começar contrato em VSS state, não em delete commands.

## 10. Permissões

### Requer administrador?
Yes

### Quando ocorre a elevação?
Leitura completa VSS normalmente privilegiada.

### O que acontece se não houver permissão?
ADMIN_REQUIRED com UI informativa.

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
Feature: C-STORAGE-012
Current state: resultado estruturado de Detect
Target state: Diagnóstico. Apply N/A nesta spec para deletion; resize advisor apenas recommendation manual se futuramente validado.
Changes: derivadas somente se a capability tiver Apply aprovado; caso contrário lista vazia
Admin required: Yes
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
Diagnóstico. Apply N/A nesta spec para deletion; resize advisor apenas recommendation manual se futuramente validado.

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
- Diagnóstico pode conflitar com snapshot em criação e gerar estado transitório.

### Pior cenário plausível
Recomendação de remover proteção; explicitamente bloqueada.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Windows 10/11; System Restore pode estar desabilitado; VSS também usado por backup.

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
- VSS service/components

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Backup software/VSS providers terceiros.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- ADMIN_REQUIRED
- VSS_UNAVAILABLE
- WRITER_ENUM_FAILED
- PROVIDER_ENUM_FAILED

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
Último health state e snapshot count opcional.

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
VM: criar snapshot/restore point, induzir writer state observável se possível, verificar inventory sem deletar.

### Rollback
N/A para feature somente leitura; testar que não existe path de mutação.

### VM
Safe

### Hardware real
No

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
Local planejado: `/prototypes/storage/c-storage-012/`

### Resultado
Diagnostic commands documented; structured API prototype PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- vss: https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/vssadmin

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
Shadow storage bytes e writer/provider status; não usar “performance”.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Snapshot count
- Shadow storage used/max
- Writer health
- Providers
- Recovery impact warning

### Ações disponíveis
- Refresh
- Details

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- snapshots[]
- shadowStorage
- writers[]
- providers[]
- healthState

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Escolher VSS COM API final e como identificar snapshots orphan sem conhecer software de backup.

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
