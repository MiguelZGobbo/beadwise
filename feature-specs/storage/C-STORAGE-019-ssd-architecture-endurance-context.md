# Feature Specification — C-STORAGE-019 — SSD architecture & endurance context

## 3. Identificação

Nome: SSD architecture & endurance context  
ID: C-STORAGE-019  
Tipo: Diagnostic, Shared Diagnostic Capability  
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
Fornecer contexto de arquitetura/endurance de SSD apenas quando metadados têm fonte confiável; “DRAM-less” não pode ser inferido universalmente por heurística.

**Base conceitual do Discovery:** [136] Host Memory Buffer Capability Detection — `RESEARCHING`; [137] DRAM-less SSD Context Detection — `RESEARCHING`; [138] SSD Architecture Metadata Layer — `IDEA`; [139] SSD Rated Endurance Context — `CANDIDATE`

### Objetivo
Expor HMB capability, endurance documentada e arquitetura conhecida para interpretar benchmarks/wear sem transformar marketing/spec lookup em fonte frágil.

### Problema que resolve
SSD DRAM-less, HMB e TBW variam por modelo/firmware; Windows não expõe diretamente toda a arquitetura comercial.

### Benefício esperado
Diagnostic / Compatibility

## 5. Aplicabilidade

### Quando se aplica?
NVMe com HMB feature consultável; rated endurance somente quando vendor/model source confiável estiver resolvida.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Não inferir DRAM-less apenas porque HMB existe.
- Não estimar TBW por capacidade NAND/host writes sem spec oficial.
- Não bloquear benchmark apenas por “DRAM-less”.

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
N/A

### Motivo apresentado ao usuário
N/A

### Grau de confiança
High para HMB protocol capability; TBD para DRAM/rated endurance catalog.

## 7. Estado atual

### O que precisa ser detectado?
- NVMe Get Features/Identify relacionados a Host Memory Buffer quando expostos.
- Rated endurance exige catálogo oficial de fabricante/modelo ou dado protocol/vendor documentado.

### Como detectar?
Protocol adapter + future vendor metadata provider com provenance/version. Não fazer web scraping em runtime como fonte canônica.

### Fonte da verdade
Protocolo NVMe para HMB; documentação oficial do fabricante para endurance/architecture. Sem fonte, Unknown.

### Estados possíveis
- HMB supported/enabled/unknown
- Architecture documented
- Architecture unknown
- Rated endurance known
- Rated endurance unknown

## 8. Estado alvo
Diagnóstico somente.

## 9. Implementação técnica

### Método principal
Protocol adapter + future vendor metadata provider com provenance/version. Não fazer web scraping em runtime como fonte canônica.

### Tecnologias utilizadas
- [ ] .NET API
- [x] Win32
- [ ] Registry
- [ ] PowerShell
- [ ] CMD / executable
- [ ] WMI / CIM
- [x] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [x] Other

### Comandos / APIs / chaves
- NVMe Get Features / Host Memory Buffer fields

### Alternativas avaliadas
Bancos comunitários de SSD são úteis para pesquisa, não verdade canônica.

### Abordagem escolhida
Facts protocol/vendor only.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Protocol query pode exigir.

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
Feature: C-STORAGE-019
Current state: resultado estruturado de Detect
Target state: Diagnóstico somente.
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
Diagnóstico somente.

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
- Metadado vendor pode mudar por revisão de hardware sob mesmo marketing name.

### Pior cenário plausível
Classificação de arquitetura errada altera interpretação; por isso confidence/provenance e status RESEARCH.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Primariamente NVMe; SATA architecture metadata é ainda mais limitada.

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
- Vendor metadata source TBD

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Model names podem ser reusados com revisões de hardware.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- HMB_QUERY_UNSUPPORTED
- VENDOR_METADATA_NOT_FOUND
- AMBIGUOUS_MODEL

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
No

### Arquivos/dados necessários
Cache de metadados vendor versionado e source URL/version se futuramente aprovado.

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
Confirmar HMB query em modelos HMB e não-HMB; validar revisão de hardware antes de aprovar catálogo endurance.

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
Local planejado: `/prototypes/storage/c-storage-019/`

### Resultado
HMB API documented; architecture/endurance data-source strategy BLOCKED/PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências

- hmb: https://learn.microsoft.com/en-us/windows/win32/api/nvme/ns-nvme-nvme_cdw12_feature_host_memory_buffer
- nvme: https://learn.microsoft.com/en-us/windows/win32/fileio/working-with-nvme-devices

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior:** TBD até execução dos protótipos/testes.
- https://learn.microsoft.com/windows/win32/fileio/working-with-nvme-devices
- https://learn.microsoft.com/windows-hardware/drivers/storage/working-with-nvme-devices

## 32. Benefício real
Situational

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
No

### Como?
N/A

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- HMB capability
- Architecture (only if sourced)
- Rated endurance (only if sourced)
- Source/confidence

### Ações disponíveis
- Details

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- hmb
- architecture
- ratedEndurance
- source
- confidence

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto

- Capability read-only: contextualizar SSD/NVMe/SATA, health/endurance somente quando protocolo ou vendor expõe atributo com semântica conhecida.
- Não converter atributos SMART vendor-specific em “vida restante” universal nem inventar TBW quando o modelo não fornece especificação confiável.
- `Unknown` é resultado correto para endurance não exposta.
- `PROVEN` exige comparar ao menos NVMe e SATA e validar normalização contra ferramenta/vendor oficial.

## 37. Critério para PROVEN
- [ ] Detect funciona nos ambientes aplicáveis.
- [ ] ChangePlan representa corretamente alterações ou lista vazia para diagnostic.
- [ ] Dry-run não altera o sistema.
- [ ] Apply/Verify funcionam quando aplicáveis.
- [ ] Snapshot/Rollback/Verify Rollback funcionam quando aplicáveis.
- [ ] Riscos principais validados.
- [ ] Compatibilidade mínima testada.
- [ ] Falhas conhecidas documentadas.

**Estado atual:** RESEARCH. Não é `PROVEN` sem a prova técnica registrada.

## 38. Critério para APPROVED
Mesmo após `PROVEN`, requer avaliação de utilidade, confiabilidade, escopo, risco, manutenção, UX, duplicação e fragilidade conforme o template canônico.
