# Feature Specification — C-STORAGE-007 — Storage write-cache & flush-policy diagnostics

## 3. Identificação

Nome: Storage write-cache & flush-policy diagnostics  
ID: C-STORAGE-007  
Tipo: Configuration, Diagnostic  
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
Diagnosticar write cache e flush semantics; não alterar política automaticamente porque desempenho e integridade de dados estão em tensão direta.

**Base conceitual do Discovery:** [47] Storage Write Cache Detection — `CANDIDATE`; [48] Write Cache Policy Modification — `RESEARCHING`; [49] Storage Flush Behavior Context — `IDEA`

### Objetivo
Mostrar se o dispositivo relata write cache e comportamento de flush/write-through e impedir recomendações simplistas de “enable write cache = faster”.

### Problema que resolve
Write caching melhora desempenho, mas pode ampliar perda de dados em power loss quando hardware/policy não garante persistência.

### Benefício esperado
Diagnostic / Stability

## 5. Aplicabilidade

### Quando se aplica?
Dispositivos que expõem StorageDeviceWriteCacheProperty ou propriedades equivalentes.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não habilitar/desabilitar write cache automaticamente.
- Não desabilitar buffer flushing como tweak de performance.
- Não inferir proteção por capacitor/bateria sem evidência do dispositivo/fabricante.

## 5.1 Uso dentro do produto

### Consumidores
- My PC
- Monitoring
- Optimization
- Cleaning
- Benchmark
- Gaming

### Uso direto pelo usuário
Partial

### Capacidade compartilhada
N/A — o Discovery classifica `Shared Capability: No`. Outros consumidores podem reutilizar o output desta capability, mas o ownership permanece aqui e nenhuma infraestrutura compartilhada adicional é criada por esta spec.

## 6. Recomendação

### A feature deve ser recomendável automaticamente?
No

### Critérios de recomendação
N/A; capability é diagnóstica. Alteração permanece RESEARCHING no Discovery de origem.

### Motivo apresentado ao usuário
Explicar trade-off de performance vs durabilidade de gravação.

### Grau de confiança
High para property reportada pelo stack; Low para proteção física contra power loss sem vendor evidence.

## 7. Estado atual

### O que precisa ser detectado?
- IOCTL_STORAGE_QUERY_PROPERTY com StorageDeviceWriteCacheProperty.
- Registrar propriedades de flush/write-through support quando descriptor expuser.

### Como detectar?
Query-only Win32 adapter.

### Fonte da verdade
Storage stack/driver property. Não testar power-loss safety em produção.

### Estados possíveis
- Write cache enabled
- Disabled
- Changeable/Not changeable
- Flush supported/unknown
- Unsupported
- Unknown

## 8. Estado alvo
Somente diagnóstico; Apply = N/A nesta spec.

## 9. Implementação técnica

### Método principal
Query-only Win32 adapter.

### Tecnologias utilizadas
- [ ] .NET API
- [x] Win32
- [ ] Registry
- [ ] PowerShell
- [ ] CMD / executable
- [ ] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- IOCTL_STORAGE_QUERY_PROPERTY / StorageDeviceWriteCacheProperty

### Alternativas avaliadas
Device Manager UI é superfície manual, não contrato de backend.

### Abordagem escolhida
Propriedade documentada do driver.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Handle físico pode exigir elevação.

### O que acontece se não houver permissão?
PARTIAL_DATA/ADMIN_REQUIRED.

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
Feature: C-STORAGE-007
Current state: resultado estruturado de Detect
Target state: Somente diagnóstico; Apply = N/A nesta spec.
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
Somente diagnóstico; Apply = N/A nesta spec.

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
- Interpretação errada pode induzir usuário a alterar política manualmente.

### Pior cenário plausível
Perda de dados se futura feature de alteração for aprovada sem safeguards; esta spec explicitamente não implementa alteração.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Windows 10/11; driver/hardware dependent.

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

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Vendor RAID cache policy pode existir acima/abaixo do device property.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- PROPERTY_UNSUPPORTED
- ACCESS_DENIED

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
Comparar property com Device Manager/vendor tool em dispositivos com políticas conhecidas.

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
Local planejado: `/prototypes/storage/c-storage-007/`

### Resultado
Detection API documented; multi-device validation PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- writecache: https://learn.microsoft.com/en-us/windows-hardware/drivers/storage/querying-for-the-write-cache-property
- flush: https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-flushfilebuffers

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
N/A

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Write cache state
- Flush/write-through support
- Integrity warning
- Confidence

### Ações disponíveis
- Details

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- writeCacheState
- flushSupport
- changeable
- confidence

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Mapear campos exatos de STORAGE_WRITE_CACHE_PROPERTY úteis para UI sem expor detalhes enganosos.

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
