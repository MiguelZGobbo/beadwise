# Feature Specification — C-STORAGE-011 — Cleanup/storage-space artifacts & safeguards

## 3. Identificação

Nome: Cleanup/storage-space artifacts & safeguards  
ID: C-STORAGE-011  
Tipo: Cleanup, Diagnostic, Diagnostic / Cleanup, Safety, Safety / Diagnostic, Safety / Product Behavior, Validation  
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
Inventariar artefatos limpáveis e executar limpeza por categoria com preview, ownership e safeguards; não vender cache cleanup como aceleração universal.

**Base conceitual do Discovery:** [85] Cloud Placeholder Awareness — `CANDIDATE`; [86] Windows.old Detection — `CANDIDATE`; [87] Update Cache Detection — `CANDIDATE`; [88] Delivery Optimization Cache Detection — `CANDIDATE`; [89] DirectX Shader Cache Detection — `CANDIDATE`; [90] Browser / Application Cache Inventory — `CANDIDATE`; [91] Crash Dump Storage Analysis — `CANDIDATE`; [92] Log Growth Detection — `CANDIDATE`; [93] Temporary File Classification — `CANDIDATE`; [94] Cleanup Preview — `CANDIDATE`; [95] Cleanup Verification — `CANDIDATE`; [96] Cleanup Rollback Capability — `RESEARCHING`; [97] User Data Protection Guard — `CANDIDATE`; [198] Cleanup Performance Myth Guard — `CANDIDATE`

### Objetivo
Liberar espaço com risco explícito, proteger dados do usuário e verificar bytes realmente removidos.

### Problema que resolve
Cleaners costumam apagar indiscriminadamente, quebrar rollback/update ou remover caches que apenas serão recriados.

### Benefício esperado
Storage / Quality of Life

## 5. Aplicabilidade

### Quando se aplica?
Categorias com ownership e semantics conhecidos; usuário escolhe categorias.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não apagar Downloads/documentos pessoais por heurística.
- Não hidratar cloud placeholders para medir tamanho.
- Não apagar Windows.old quando usuário ainda pode precisar rollback sem aviso explícito.
- Não alegar ganho de FPS/performance por limpar cache sem evidência.

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
Pressão de espaço real + categoria segura identificada; itens com impacto funcional exigem explicação/consentimento.

### Motivo apresentado ao usuário
Mostrar bytes estimados, categoria, consequência (recriação, perda de rollback, logs úteis etc.).

### Grau de confiança
High para categorias nativas conhecidas; Medium para caches de terceiros; Low/blocked para pastas sem ownership claro.

## 7. Estado atual

### O que precisa ser detectado?
- Storage Sense/Windows-known categories quando possível.
- Filesystem metadata para Windows.old, dumps/logs/temp.
- Delivery Optimization cache tratado como cache gerenciado pelo próprio Windows.
- Cloud placeholder flags antes de qualquer scan de conteúdo.

### Como detectar?
Cada cleanup item vira operation com path/category/owner/reason/reversible class. Preferir APIs/Storage Sense/nativas quando houver; custom delete apenas para artefatos claramente owned e verificados.

### Fonte da verdade
Ownership/category rules documentadas; tamanho calculado por metadata. Não usar apenas nome de pasta genérico.

### Estados possíveis
- Safe candidate
- Recreatable cache
- Functional consequence
- User data protected
- In use/locked
- Unknown ownership
- Already clean

## 8. Estado alvo
Somente itens selecionados removidos, categorias protegidas intactas, bytes freed medidos e cleanup verificado.

## 9. Implementação técnica

### Método principal
Cada cleanup item vira operation com path/category/owner/reason/reversible class. Preferir APIs/Storage Sense/nativas quando houver; custom delete apenas para artefatos claramente owned e verificados.

### Tecnologias utilizadas
- [x] .NET API
- [ ] Win32
- [ ] Registry
- [x] PowerShell
- [ ] CMD / executable
- [ ] WMI / CIM
- [ ] Vendor API
- [x] File modification
- [ ] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- Storage Sense / Windows cleanup mechanisms when available
- controlled filesystem delete for owned artifacts

### Alternativas avaliadas
cleanmgr é legado e menos estruturado para automação moderna; pode ser fallback investigado.

### Abordagem escolhida
Categoria explícita + mecanismo Windows quando possível.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
System artifacts podem exigir admin; user temp não necessariamente.

### O que acontece se não houver permissão?
Remover somente itens autorizados; reportar skipped/access denied.

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
Feature: C-STORAGE-011
Current state: resultado estruturado de Detect
Target state: Somente itens selecionados removidos, categorias protegidas intactas, bytes freed medidos e cleanup verificado.
Changes: derivadas somente se a capability tiver Apply aprovado; caso contrário lista vazia
Admin required: Conditional
Restart required: Nada
Risk: Medium
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
Somente itens selecionados removidos, categorias protegidas intactas, bytes freed medidos e cleanup verificado.

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
Medium

### Possíveis efeitos negativos
- Perda de rollback Windows.old.
- Caches serão recriados.
- Logs/dumps podem ser úteis para diagnóstico.

### Pior cenário plausível
Exclusão de dado do usuário; bloqueada por ownership guard e ausência de heurística destrutiva.

### Recuperação manual possível?
Partial

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Windows 10/11; categorias mudam por build/apps.

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
- output de C-STORAGE-004
- Cloud placeholder detection
- UserDataProtectionGuard

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Apps podem recriar caches durante cleanup.
- Políticas corporativas podem controlar Storage Sense.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- ITEM_IN_USE
- ACCESS_DENIED
- CATEGORY_UNSUPPORTED
- VERIFY_FAILED
- PARTIAL_CLEANUP

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
Persistir plano e resultados; para itens não reversíveis, snapshot registra metadata, não conteúdo completo.

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
Testar cada categoria separadamente em VM; validar preview=apply set, locked files, cloud placeholders e Windows.old guard.

### Rollback
Testar snapshot→apply→verify→rollback→verify rollback incluindo estado inicial personalizado e inexistente.

### VM
Safe

### Hardware real
Optional

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
Local planejado: `/prototypes/storage/c-storage-011/`

### Resultado
Framework specified; category-by-category proof PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- storage_sense: https://learn.microsoft.com/en-us/windows/configuration/storage/storage-sense

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
Bytes freed reais por categoria e total; performance claim = N/A.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Category
- Estimated size
- Consequence
- Reversible?
- Requires admin
- Selected items
- Actual freed space

### Ações disponíveis
- Preview
- Clean
- Details

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- cleanupItems[]
- estimatedBytes
- actualBytes
- consequence
- reversible
- status

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Mecanismo oficial programático atual para algumas categorias específicas sem depender de UI/cleanmgr.

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
