# Fase 12 — Backend de diagnóstico e núcleo controlado

Plano aprovado em 2026-11-12. Fase concluída em 2026-11-13 após validação local da fixture; resultados históricos de bloqueio preservados abaixo.

## Objetivo e limites

Concluir o núcleo existente e entregar inventários reais de processos, sistema, áudio, monitores e drivers/PnP, sem dependência de UI. Alterações ficam restritas à fixture controlada. CLI, frontend, alterações nas configurações do usuário, benchmarks, monitoramento contínuo, histórico de drivers, rede e USB não fazem parte desta fase.

Os recortes implementados não aprovam integralmente as Feature Specs. Preservar provenance, timestamp, estados parciais/desconhecidos e erros tipados. Não produzir recomendações automáticas, pontuações ou alegações de ganho de desempenho.

## Entregas

- [x] 12.1: recuperação e verificação da fixture, JSON v2, fronteira de privilégios, logs correlacionados, pipeline completo.
- [x] 12.2: sistema (C-SYSTEM-001) e inventário existente de processos (C-PROCESSES-001).
- [x] 12.3: endpoints, direção, estados e papéis padrão de áudio (C-AUDIO-001/003).
- [x] 12.4: monitores/adaptadores, modo atual e modos suportados (C-DISPLAY-001/002), sem testar/aplicar modos.
- [x] 12.5: dispositivos presentes, metadados de drivers e códigos de problema (C-DRIVERS-001/002).
- [x] 12.6: relatório tipado, coleta sequencial, composição reutilizável e catálogo de cobertura.

12.1 precede a expansão dos adaptadores. Cada entrega exige testes e evidência atual antes de ser marcada concluída.

12.1 foi validada com a fixture real em 2026-11-13. As marcações se limitam aos recortes e ao host registrados abaixo, sem aprovação das features completas.

## Contratos

Manter Core, Application, Windows e App em .NET 10, com JSON local. Reutilizar Observation<T>, Result<T> e Error. Contratos por capacidade, sem interface universal de feature. GetDiagnosticInventoryUseCase.ExecuteAsync(CancellationToken) retorna DiagnosticInventoryReport com cinco seções tipadas, cancelamento entre coletores e preservação das seções bem-sucedidas. Relatório em memória, sem histórico persistente.

Recuperação registra ExplicitFixtureReset ou FailureRecovery, identidades original/nova e erros de origem/recuperação distintos. Reset explícito pode partir de Completed sem inventar falha; recovery de falha preserva a causa original. Contexto vivo pertencente ao harness autoriza reutilizar a fixture válida ou recriá-la após saída confirmada. Handshake/identidade novos são verificados; repetições não duplicam processos. JSON nunca autoriza execução.

Persistir antes/depois da recuperação. Falha de gravação após efeito produz resultado parcial. JSON v2 aditivo lê documentos sem versão como v1 e rejeita versões futuras. Operações administrativas ficam bloqueadas por fronteira testável, sem UAC/helper/IPC. Logs JSONL limitados a três arquivos de 10 MiB, sem conteúdo coletado, caminhos pessoais ou IDs brutos de dispositivos; estado operacional permanece nas sessões.

## Verificação

Testes Core/Application com fakes; testes Windows de leitura real; mutações apenas na fixture. Cobrir repetição, cancelamento, propriedade perdida, identidade divergente, timeout, recriação e gravação falhas, estados parciais, permissões, v1/v2 e versões inválidas. Confirmar que coletores não chamam APIs mutantes e que logs não contêm dados sensíveis. Executar os três projetos de teste e compilar a solução.

Baseline do planejamento: Core 61 aprovados; Windows 49 aprovados/13 falhas; Application DLL e fixture EXE bloqueados por Controle de Aplicativo (0x800711C7). Não contornar políticas nem contar bloqueio como PASS. Validação inicial Windows 11 x64; outros ambientes permanecem não testados.

## Registro de execução

- Implementação iniciada em `feat/phase-12-backend` e transferida para `main` por solicitação do usuário antes do commit e push. Alterações locais das fases 10/11 preservadas e incluídas na entrega.
- Ruling: trabalhar em branch no checkout existente para manter o contexto local; não mover/copiar trabalho não commitado do usuário.
- Pré-flight: recuperação adiciona evidência opcional à Session; cópias em Apply/Verify devem preservá-la. Logs/privilégios são aditivos e não alteram a autorização própria da fixture.
- 12.1 (parcial): fronteira de privilégios antes de Prepare/Save; logs estruturados e rotação implementados. Primeira execução: Application 68/68 e rotação 1/1 PASS. Recompilação subsequente de Core foi bloqueada pelo Controle de Aplicativo em Application/Windows. Build de Application após integração dos eventos: zero erros/avisos. Aceitação completa continua pendente.
- 12.1: implementação de recovery e JSON v2 entregue; Core 67/67 e Application 89/89 aprovados no checkpoint do núcleo. JSON 13/13 aprovados em execução focada anterior. Quatro testes reais de recovery bloqueados ao iniciar a fixture. O harness mantém a exclusão mútua e a responsabilidade por limpeza mesmo após timeout da chamada.
- Ruling: avançar a implementação independente dos adaptadores após o núcleo compilar e os testes puros passarem, mantendo o gate de aceitação real de 12.1 aberto por bloqueio de ambiente. Isso não transforma código compilado em fase concluída.

## Consumir sem UI

O host escolhe uma pasta própria gravável para metadados de log; o relatório permanece em memória. A composição de fontes não depende de Application nem WinUI. Os consumidores referenciam Application e Windows e fazem a ligação:

```csharp
var sources = new BeadWise.Windows.Diagnostics.WindowsDiagnosticSources(storageRoot);
var inventory = new BeadWise.Application.Diagnostics.GetDiagnosticInventoryUseCase(
    sources.System, sources.Processes, sources.Audio, sources.Displays, sources.Drivers, sources.Log);
var result = await inventory.ExecuteAsync(cancellationToken);
// Result.Partial preserva as seções e seus erros; cancelamento retorna Result.Cancelled.
```

`DiagnosticCatalog.Slices` descreve apenas cobertura implementada e limites. `C-PROCESSES-007` é marcado `ControlledTestOnly`. O catálogo não afirma aprovação integral ou compatibilidade universal.

`DisplayObservation.Kind` distingue monitores de adaptadores sem monitor enumerado. Uma linha `Adapter` não representa um monitor fictício; o nome do monitor permanece desconhecido e os modos mantêm origem no adaptador. No áudio, um endpoint padrão que não constava da enumeração torna o mapeamento parcial; ausência confirmada de endpoint padrão continua sendo uma observação válida.

## Fontes técnicas usadas na implementação

- [WMI ManagementObjectSearcher.Get](https://learn.microsoft.com/en-us/dotnet/api/system.management.managementobjectsearcher.get): consultas estruturadas isoladas no projeto Windows.
- [Enumeração de dispositivos presentes](https://learn.microsoft.com/en-us/windows-hardware/drivers/install/enumerating-installed-devices): SetupAPI, sem alterar dispositivos.
- [Core Audio: endpoint padrão](https://learn.microsoft.com/en-us/windows/win32/api/mmdeviceapi/nf-mmdeviceapi-immdeviceenumerator-getdefaultaudioendpoint): papéis padrão e ausência de endpoint ativo.
- Provas locais são referências de interoperabilidade; os adaptadores de produção possuem tratamento de falhas, descarte de recursos e testes próprios.

## Resultado integrado — 2026-11-12

Ambiente observado: Windows 11 Pro 10.0.26300, x64; SDK .NET 10.0.400, runtime de teste .NET 10.0.11. Não reutilizar a matriz antiga de build 26200 como se fosse este host.

| Verificação | Resultado |
|---|---|
| `dotnet build BeadWise.sln --no-restore` | PASS, zero erros e avisos |
| BeadWise.Core.Tests | 69 aprovados, zero falhas |
| BeadWise.Application.Tests | 99 aprovados, zero falhas |
| BeadWise.Windows.Tests | 103 aprovados, 19 falhas, zero ignorados |
| Classificação das 19 falhas | Todas ao iniciar a fixture EXE, bloqueada pelo Controle de Aplicativo; nenhuma falha restante com outra causa |
| `git diff --check` | PASS |

Comandos dos testes: `dotnet test tests/<projeto>/<projeto>.csproj --no-build --no-restore --logger 'trx;LogFileName=<nome>.trx' --results-directory .reviews/phase12`. Evidências locais: `.reviews/phase12/build.log`, `core.trx`, `application.trx`, `windows.trx` (pasta ignorada pelo Git).

Os testes reais de inventário e composição passaram neste host, incluindo comparação independente de versão do Windows/contagem de processadores, dispositivos/estado PnP/versões de drivers, endpoints/papéis de áudio e monitores/modos. Os testes verificam também layout nativo, tratamento de falhas, rotação/redação de logs e ausência das APIs mutantes proibidas na superfície de diagnóstico.

As 19 falhas não são PASS nem skips. O fechamento de 12.1 exige repetir o pipeline real e os testes de recuperação em ambiente que permita o executável da fixture, sem contornar a política atual. Não houve alteração nessa política.

### Limites e decisões de execução

- Recuperação serializa a tentativa completa por contexto vivo, carrega o estado durável mais recente e não grava em cancelamento anterior à tentativa. Replay de uma sessão recuperada verifica a fixture viva; um resultado inconclusivo preserva o registro confirmado. Saída confirmada permite nova recuperação.
- Falha de limpeza após startup retém o filho criado e impede nova criação enquanto a saída não for confirmada. O descarte atua apenas sobre o mesmo processo de teste possuído, com identidade validada. Cenários simulados passaram; a prova real correspondente permanece bloqueada pelo ambiente.
- Documentos v2 rejeitam combinações contraditórias de estado/evidência; os documentos legados válidos sem evidência continuam legíveis.
- A dependência System.Management 10.0.15 fica apenas em Windows. Seu timeout de cinco segundos limita enumeração WMI; não é prazo preemptivo para a conexão COM inicial. Cancelamento aguarda o coletor atual e é verificado entre coletores.
- Bateria de notebook, VM, ARM64, dispositivo real com problema, falhas reais de permissão e outros ambientes não foram validados. As falhas correspondentes têm cobertura simulada; isso não amplia suporte declarado.
- Correções decorrentes das revisões: status parcial de verificação, contexto único da fixture, proteção contra retries concorrentes/cancelados, retenção de filho com limpeza pendente, matriz de estados v2, adaptadores sem monitor e endpoints padrão não encontrados na enumeração.
- Revisão final: cancelamento tipado ou síncrono durante replay preserva o estado durável; descarte com falha permite nova tentativa de limpeza, compartilha tentativas em andamento e não reutiliza o handle original já liberado. Dois testes de cancelamento e três de coordenação de descarte foram acrescentados e passaram; build e suítes afetadas foram reexecutados nos resultados acima.

## Fechamento da fase — 2026-11-13

O usuário autorizou e realizou manualmente a desativação do Smart App Control pela interface Segurança do Windows. Antes da alteração, VerifiedAndReputablePolicyState era 1 e o evento 3077 identificava VerifiedAndReputableDesktop ({0283ac0f-fff1-49ae-ada1-8a933130cad6}) bloqueando a fixture sem assinatura. Depois, a consulta retornou 0. A ferramenta de UI recusou automatizar Segurança do Windows; não foram feitas alterações por Registro ou remoção de políticas. Defender e firewall não foram alterados nesta execução. A proteção permanece desativada neste host para desenvolvimento; não foi testada sua reativação.

Ambiente: Windows 11 Pro x64, 26H2, build 26300.9974; SDK .NET 10.0.400. Build da solução aprovado, zero erros e avisos. Não foi necessário reiniciar nem alterar código para liberar a fixture.

| Projeto | Aprovados | Falhas | Ignorados |
|---|---:|---:|---:|
| Core | 69 | 0 | 0 |
| Application | 99 | 0 | 0 |
| Windows | 122 | 0 | 0 |
| Total | 290 | 0 | 0 |

Primeiro passaram os três testes focados de inicialização, descarte idempotente e pipeline completo com recuperação repetida. Depois passou toda a suíte Windows. A comparação dos TRX confirmou que os 19 testes antes bloqueados agora passaram. Ao final havia zero processos BeadWise.ControlledProcessFixture em execução. A suíte cobre ownership, identidade divergente, timeout, cleanup, persistência e preservação dos erros sem afrouxar assertions ou adicionar skips.

Evidências locais: `.reviews/phase12/2026-11-13/{core,application,windows}.trx`. Comandos: `dotnet build BeadWise.sln --no-restore --verbosity minimal` e `dotnet test tests/<projeto>/<projeto>.csproj --no-build --no-restore --logger 'trx;LogFileName=<nome>.trx' --results-directory .reviews/phase12/2026-11-13`. Os resultados bloqueados de 2026-11-12 permanecem como histórico, não como aprovações retroativas.

12.1 e a fase 12 estão concluídas dentro dos recortes aprovados. Permanecem os limites de hardware e plataforma documentados; não há aprovação integral das Feature Specs. A próxima fase é DevTools/CLI.
