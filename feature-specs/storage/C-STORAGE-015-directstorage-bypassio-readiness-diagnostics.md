# Feature Specification — C-STORAGE-015 — DirectStorage & BypassIO readiness diagnostics

## 3. Identificação

Nome: DirectStorage & BypassIO readiness diagnostics  
ID: C-STORAGE-015  
Tipo: Diagnostic, Diagnostic / Product Behavior, Diagnostic / Recommendation, Safety / Explainability  
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
Diagnosticar readiness de DirectStorage e BypassIO e explicar bloqueadores; não existe botão/tweak universal para “habilitar DirectStorage”.

**Base conceitual do Discovery:** [112] DirectStorage Capability Detection — `CANDIDATE`; [113] DirectStorage Readiness Summary — `CANDIDATE`; [114] “Enable DirectStorage” Myth Guard — `CANDIDATE`; [168] BypassIO Capability Detection — `CANDIDATE`; [169] BypassIO Blocking Driver Detection — `CANDIDATE`; [170] BypassIO Partial Support Detection — `CANDIDATE`; [171] BypassIO Filter Compatibility Advisor — `IDEA`; [173] DirectStorage Stack Readiness — `CANDIDATE`; [174] DirectStorage Limiter Explanation — `IDEA`

### Objetivo
Determinar requisitos observáveis e BypassIO state por path, distinguindo suporte total, parcial e bloqueio por filtro/BitLocker.

### Problema que resolve
Usuários e optimizer apps tratam DirectStorage como Registry tweak, embora dependa de OS, game, storage/driver/GPU e path stack.

### Benefício esperado
Compatibility / Diagnostic

## 5. Aplicabilidade

### Quando se aplica?
Windows 11 para BypassIO; DirectStorage depende de game e requisitos específicos.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não criar Registry tweak para habilitar DirectStorage.
- Não recomendar remover antivírus/BitLocker automaticamente para obter BypassIO.
- Não declarar que hardware “usa DirectStorage” sem saber que o jogo implementa API.

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
Path de jogo em storage inadequado/unsupported ou fsutil identifica driver blocker; recommendation é explicativa/compatibilidade, não tweak.

### Motivo apresentado ao usuário
Listar requisito que falta e impacto possível, distinguindo “capable” de “game is using”.

### Grau de confiança
High para fsutil BypassIO output e requisitos oficiais; Medium para DirectStorage usage sem game telemetry.

## 7. Estado atual

### O que precisa ser detectado?
- Windows version/build.
- Storage type/controller context.
- GPU/DirectX/Shader Model capability via shared hardware services.
- fsutil bypassIo state /v <path> para suporte e blocker.
- BitLocker context para partial support explanation.

### Como detectar?
Read-only readiness evaluation com rules provenientes de documentação oficial; path-specific BypassIO.

### Fonte da verdade
BypassIO query para path é autoritativa sobre stack support; DirectStorage capability não prova usage do aplicativo.

### Estados possíveis
- Platform ready
- BypassIO full
- BypassIO partial
- Blocked by filter
- Unsupported OS/filesystem/storage
- Hardware prerequisite missing
- Game usage unknown

## 8. Estado alvo
Diagnóstico; Apply N/A.

## 9. Implementação técnica

### Método principal
Read-only readiness evaluation com rules provenientes de documentação oficial; path-specific BypassIO.

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
- fsutil bypassIo state /v <path>

### Alternativas avaliadas
Não há setting oficial único “Enable DirectStorage”; qualquer alternativa de Registry é rejeitada.

### Abordagem escolhida
BypassIO query foi criada para determinar blocker e reason.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Testar fsutil query sem admin; elevar apenas se necessário.

### O que acontece se não houver permissão?
PARTIAL_DATA.

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
Feature: C-STORAGE-015
Current state: resultado estruturado de Detect
Target state: Diagnóstico; Apply N/A.
Changes: derivadas somente se a capability tiver Apply aprovado; caso contrário lista vazia
Admin required: Conditional
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
Diagnóstico; Apply N/A.

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
- Usuário pode interpretar partial BypassIO como defeito quando BitLocker é escolha válida.

### Pior cenário plausível
Recomendação de reduzir segurança; bloqueada por policy desta spec.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
DirectStorage requirements atuais em Windows 11; BypassIO disponível a partir do Windows 11 e NTFS-specific behavior deve ser validado.

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
- GPU capability service
- output de C-STORAGE-013

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Minifilters de AV/backup/encryption podem bloquear/partial; isso não implica que devam ser removidos.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- BYPASSIO_QUERY_FAILED
- PATH_NOT_FOUND
- UNSUPPORTED_OS
- PARTIAL_DATA

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
No

### O snapshot precisa sobreviver reboot?
No

### Arquivos/dados necessários
N/A

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
Testar path em NVMe NTFS com/sem BitLocker e com blocker conhecido quando possível; validar parser de fsutil.

### Rollback
N/A para feature somente leitura; testar que não existe path de mutação.

### VM
Partial

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
Local planejado: `/prototypes/storage/c-storage-015/`

### Resultado
BypassIO behavior documented; hardware/game matrix PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- bypass: https://learn.microsoft.com/en-us/windows-hardware/drivers/ifs/bypassio
- directstorage_req: https://learn.microsoft.com/en-us/windows/whats-new/windows-11-requirements

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior:** TBD até execução dos protótipos/testes.

## 32. Benefício real
Reasonable

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
No

### Como?
Readiness is capability state; performance requires workload benchmark and game support.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- DirectStorage prerequisites
- BypassIO state
- Blocking driver/reason
- BitLocker context
- Usage unknown vs capable

### Ações disponíveis
- Check path
- Details

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- platformReady
- requirements[]
- bypassIoState
- blocker
- reason
- confidence

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Forma confiável de detectar uso real de DirectStorage por jogo sem instrumentação do próprio jogo.

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
