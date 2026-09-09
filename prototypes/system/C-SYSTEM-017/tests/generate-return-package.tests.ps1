$ErrorActionPreference = 'Stop'
$prototypeRoot = Split-Path -Parent $PSScriptRoot
$generator = Join-Path $prototypeRoot 'scripts\generate-return-package.ps1'
$repositoryRoot = (Resolve-Path (Join-Path $prototypeRoot '..\..\..')).Path
$packagePath = Join-Path $env:TEMP 'beadwise-phase2-package-test.md'
$matrixPath = Join-Path $env:TEMP 'beadwise-phase2-matrix-test.md'
try {
    & $generator -RepositoryRoot $repositoryRoot -OutputPath $packagePath -RootPackagePath $packagePath -MatrixOutputPath $matrixPath -ResultsMatrixOutputPath $matrixPath -CommitBase 'e9ead73'
    $package = Get-Content -Raw -LiteralPath $packagePath
    $matrix = Get-Content -Raw -LiteralPath $matrixPath
    $rowCount = [regex]::Matches($matrix, '(?m)^\| C-[A-Z]+-\d+ \|').Count
    if ($rowCount -ne 234) { throw "Expected 234 matrix rows, found $rowCount" }
    foreach ($status in @('RESEARCH','SPECIFIED','PROTOTYPING','PROVEN','APPROVED','BLOCKED','DEFERRED','REJECTED')) {
        if ($package -notmatch "(?m)^- ${status}: \d+") { throw "Missing explicit status count: $status" }
    }
    if ($matrix -notmatch '\| SAFE_PROOF \|') { throw 'Matrix does not expose the independently executed safe-proof slice' }
    if ($matrix -notmatch '(?m)^\| C-SYSTEM-007 \| SPECIFIED \| PASS \| NOT_TESTED \| NOT_TESTED \| N/A \| PASS \| N/A \| N/A \| N/A \| N/A \| PASS \| N/A \|') {
        throw 'C-SYSTEM-007 matrix row misattributes fixture mutation to feature Apply/Rollback'
    }
    if ($matrix -notmatch '(?m)^\| C-CPU-001 \| SPECIFIED \| NOT_TESTED .*\| PASS \|') { throw 'CPU native safe proof is absent from matrix' }
    if ($matrix -notmatch '(?m)^\| C-SYSTEM-018 \| SPECIFIED .*\| PASS \|') { throw 'Restart Manager safe proof is absent from matrix' }
    if ($package -match '\$\(System\.Collections') { throw 'Package contains broken OrderedDictionary interpolation' }
    if ($package -notmatch 'inconsistência C-SYSTEM-007/C-SYSTEM-017 foi corrigida: C-SYSTEM-017 continua sendo o bundle compartilhado de coleta/export e não é prova de C-SYSTEM-007; C-SYSTEM-007 agora possui prototype próprio') { throw '007/017 conclusion missing' }
    if ($package -notmatch '(?m)^FASE 2 PRONTA PARA AUDITORIA FINAL$') { throw 'Final readiness declaration missing after successful verification' }
    Write-Output 'PASS: return package and 234-row proof matrix are structurally coherent'
}
finally {
    Remove-Item -LiteralPath $packagePath,$matrixPath -Force -ErrorAction SilentlyContinue
}
