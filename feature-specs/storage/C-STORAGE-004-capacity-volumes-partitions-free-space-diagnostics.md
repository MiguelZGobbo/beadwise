# Feature Specification — C-STORAGE-004 — Capacity, volumes, partitions & free-space diagnostics

## 3. Identificação

Nome: Capacity, volumes, partitions & free-space diagnostics  
ID: C-STORAGE-004  
Tipo: Diagnostic, Diagnostic Tool, Monitoring, Optimization, Tool  
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
Mapear capacidade, volumes, partições, espaço livre e consumidores de espaço, sem transformar “espaço livre abaixo de X%” em regra universal.

**Base conceitual do Discovery:** [22] Storage Capacity Detection — `CANDIDATE`; [23] Volume Inventory — `CANDIDATE`; [24] Partition Layout Detection — `CANDIDATE`; [25] GPT / MBR Detection — `CANDIDATE`; [26] Unexpected Unallocated Space Detection — `IDEA`; [27] Low Free Space Detection — `CANDIDATE`; [28] Free Space Trend — `IDEA`; [29] “What Is Using My Disk Space?” Analysis — `CANDIDATE`; [30] Large File Discovery — `CANDIDATE`; [31] Duplicate File Discovery — `IDEA`; [181] SSD Free-Space Performance Context — `CANDIDATE`; [182] Low SSD Free-Space Warning — `CANDIDATE`; [183] SSD Overprovisioning Awareness — `IDEA`; [184] Manual Overprovisioning Recommendation — `QUESTIONABLE`

### Objetivo
Dar visão física/lógica do espaço e identificar pressão real, arquivos/categorias grandes e espaço não alocado inesperado com proteção de dados.

### Problema que resolve
Usuário pode estar sem espaço sem saber onde ele está; percentuais fixos ignoram tamanho do disco e necessidades de servicing/update.

### Benefício esperado
Storage / Diagnostic

## 5. Aplicabilidade

### Quando se aplica?
Volumes locais montados ou acessíveis; análise de conteúdo requer permissão de leitura.

### Condições necessárias
- Windows e componentes indicados na seção Compatibilidade.
- As fontes da verdade listadas em Estado atual precisam retornar dado suficiente para a decisão.
- Dependências compartilhadas devem estar saudáveis ou o resultado deve degradar explicitamente para Partial/Unknown.

### Quando NÃO deve ser aplicada?
- Manual overprovisioning permanece fora do escopo ativo.
- Não classificar espaço não alocado como “problema” quando layout/Recovery/OEM/Storage Spaces justificar.
- Não varrer diretórios sem consentimento/escopo quando implicar privacidade ou custo elevado.

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
Espaço insuficiente para operação pretendida (update/benchmark/cleanup target) ou tendência comprovada de esgotamento; grandes consumidores exibidos como informação, não culpa.

### Motivo apresentado ao usuário
Exibir bytes livres, tendência e requisito concreto que pode falhar; evitar regra fixa percentual.

### Grau de confiança
High para capacidade/layout; Medium para “what uses space” quando há inacessíveis/reparse/cloud placeholders.

## 7. Estado atual

### O que precisa ser detectado?
- Get-Disk/Get-Partition/Get-Volume/CIM para layout e size/free space.
- GetVolumeInformation/GetDiskFreeSpaceEx como Win32 alternativo.
- Scanner de filesystem opt-in com tratamento de reparse points, hardlinks, sparse/compressed/cloud placeholders.
- Histórico de free bytes por volume para tendência.

### Como detectar?
Collector + analyzer read-only; regras de pressure são contextuais por operação, e tendência usa dados históricos.

### Fonte da verdade
APIs de volume/partição do Windows para capacidade/layout. Para uso por arquivo, tamanho alocado real quando possível; não somar hardlinks duplicadamente.

### Estados possíveis
- Healthy capacity
- Pressure
- Critical for requested operation
- Unallocated expected
- Unallocated suspicious
- Scan partial
- Unsupported/Unknown

## 8. Estado alvo
Diagnóstico; ações de limpeza pertencem a C-STORAGE-011. Nenhuma criação/redimensionamento de partição nesta capability.

## 9. Implementação técnica

### Método principal
Collector + analyzer read-only; regras de pressure são contextuais por operação, e tendência usa dados históricos.

### Tecnologias utilizadas
- [x] .NET API
- [x] Win32
- [ ] Registry
- [ ] PowerShell
- [ ] CMD / executable
- [x] WMI / CIM
- [ ] Vendor API
- [ ] File modification
- [ ] Service Control Manager
- [ ] Other

### Comandos / APIs / chaves
- Get-Disk
- Get-Partition
- Get-Volume
- GetDiskFreeSpaceEx
- filesystem enumeration

### Alternativas avaliadas
Storage Settings UI não fornece API de categorização completa; scanner próprio é necessário para detalhamento, com custo controlado.

### Abordagem escolhida
APIs de storage + scan opt-in com deduplicação semântica.

## 10. Permissões

### Requer administrador?
Conditional

### Quando ocorre a elevação?
Algumas pastas do sistema são inacessíveis sem admin; inventário de volume não deve exigir.

### O que acontece se não houver permissão?
Retornar scan partial e bytes known; não estimar o restante.

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
Feature: C-STORAGE-004
Current state: resultado estruturado de Detect
Target state: Diagnóstico; ações de limpeza pertencem a C-STORAGE-011. Nenhuma criação/redimensionamento de partição nesta capability.
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
Diagnóstico; ações de limpeza pertencem a C-STORAGE-011. Nenhuma criação/redimensionamento de partição nesta capability.

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
- Scan de árvore pode consumir I/O/CPU.
- Cloud placeholders podem ser hidratados se APIs inadequadas forem usadas.
- Hardlinks podem causar dupla contagem.

### Pior cenário plausível
Hidratação ou varredura pesada; usar metadata-only e flags de placeholder/reparse.

### Recuperação manual possível?
Yes

### Procedimento manual
TBD por ambiente quando houver falha real. Nunca prescrever ação destrutiva genérica apenas para preencher a spec.

## 21. Compatibilidade

### Windows
Windows 10/11; NTFS/ReFS/FAT/exFAT com diferenças de metadata.

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
- filesystem metadata layer

### Serviços / APIs / Componentes / Drivers / Internet / Apps externas
Somente os explicitamente necessários pelas dependências e APIs desta spec. Internet não é requisito para detecção local, salvo futura consulta vendor explicitamente definida.

## 23. Conflitos
- Antivírus e cloud sync podem alterar arquivos durante scan.

## 24. Idempotência
Yes

A detecção é idempotente por design. Apply, quando existe, deve re-detectar e produzir no-op se o target já estiver satisfeito.

## 25. Falhas possíveis
- SCAN_PARTIAL
- ACCESS_DENIED
- VOLUME_CHANGED
- REPARSE_LOOP_GUARD

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
Histórico agregado de free-space; nunca persistir lista completa de arquivos por padrão.

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
Testar hardlinks, sparse, compressed, cloud placeholders, junctions e volumes grandes; validar que scanner não hidrata conteúdo cloud.

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
Local planejado: `/prototypes/storage/c-storage-004/`

### Resultado
Core detection documented; filesystem edge-case prototype PENDING.

Não elevar status para `PROVEN` antes de executar os testes aplicáveis e registrar ambiente/resultados.

### Ambiente utilizado
TBD até execução da prova: Windows version, hardware/storage topology, driver, admin state e date.

## 31. Evidências
- fsinfo: https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/fsutil-fsinfo
- physicaldisk: https://learn.microsoft.com/en-us/powershell/module/storage/get-physicaldisk?view=windowsserver2025-ps

Classificação atual:
- **Documented behavior:** itens sustentados pelas referências oficiais acima.
- **Observed behavior:** TBD até execução dos protótipos/testes.

## 32. Benefício real
Proven

A classificação refere-se à utilidade da capability conforme escopo desta spec, não a qualquer tweak popular associado ao tema.

## 33. Impacto mensurável

### Pode ser medido?
Yes

### Como?
Bytes livres/usados e tendência; scan reports known/unknown bytes.

## 34. Experiência do usuário

### Informações que devem ser apresentadas
- Capacity
- Free space
- Trend
- Partitions/unallocated
- Top categories/files (opt-in)
- Scan completeness

### Ações disponíveis
- Analyze space
- Details

Final UI Placement permanece `TBD`/`N/A` conforme UX futura; esta seção define apenas o contrato informacional.

## 35. Dados necessários pelo frontend
- volumes[]
- partitions[]
- freeBytes
- usedBytes
- pressureState
- trend
- spaceConsumers[]
- scanCompleteness

Separação:
- Data exposed directly to UI: somente campos necessários para explicar estado/ação.
- Data consumed internally: provenance detalhada, raw descriptors, stable mapping IDs e telemetry interna salvo necessidade de Details.

## 36. Questões em aberto
- Fonte segura para tamanho “on disk” de placeholders cloud sem hidratação.

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
