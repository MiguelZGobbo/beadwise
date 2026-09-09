param(
    [string]$OutputPath = (Join-Path (Split-Path -Parent $PSScriptRoot) 'results\performance-counter-proof.json')
)

$ErrorActionPreference = 'Stop'

$steps = [Collections.Generic.List[object]]::new()

function Add-Step {
    param([string]$Name, [string]$Status, [string]$Detail)
    $steps.Add([ordered]@{ name = $Name; status = $Status; detail = $Detail })
}

$englishPath = '\Processor(_Total)\% Processor Time'
$englishResult = $null
try {
    $englishSample = Get-Counter -Counter $englishPath -SampleInterval 1 -MaxSamples 1 -ErrorAction Stop
    $englishResult = [ordered]@{
        status = 'PASS'
        path = $englishPath
        value = [double]$englishSample.CounterSamples[0].CookedValue
    }
    Add-Step 'ENGLISH_PATH' 'PASS' 'English counter path was available.'
}
catch {
    $englishResult = [ordered]@{
        status = 'FAIL'
        path = $englishPath
        error = $_.Exception.Message
    }
    Add-Step 'ENGLISH_PATH' 'FAIL' 'English counter path was not available on this localized Windows installation.'
}

$sets = @(Get-Counter -ListSet *processador* -ErrorAction SilentlyContinue)
if ($sets.Count -eq 0) {
    $sets = @(Get-Counter -ListSet *processor* -ErrorAction SilentlyContinue)
}

$allCandidatePaths = @($sets | ForEach-Object { $_.PathsWithInstances } | Where-Object { $_ -like '*(_Total)*' })
$preferredPatterns = @(
    '% tempo de processador',
    '% Processor Time',
    '% de Utilitario do Processador',
    '% de Utilitário do Processador'
)

$selectedPath = $null
foreach ($pattern in $preferredPatterns) {
    $selectedPath = $allCandidatePaths | Where-Object { $_ -like "*$pattern" } | Select-Object -First 1
    if (-not [string]::IsNullOrWhiteSpace($selectedPath)) {
        break
    }
}

if ([string]::IsNullOrWhiteSpace($selectedPath)) {
    $selectedPath = $allCandidatePaths | Where-Object { $_ -match '%.*processador|%.*Processor|%.*Utilit' } | Select-Object -First 1
}
if ([string]::IsNullOrWhiteSpace($selectedPath)) {
    Add-Step 'DISCOVER' 'FAIL' 'No localized CPU utilization counter path was found.'
    $sample = $null
}
else {
    Add-Step 'DISCOVER' 'PASS' "Discovered localized counter path: $selectedPath"
    $counter = Get-Counter -Counter $selectedPath -SampleInterval 1 -MaxSamples 1 -ErrorAction Stop
    $counterSample = $counter.CounterSamples[0]
    $sample = [ordered]@{
        path = $counterSample.Path
        instanceName = $counterSample.InstanceName
        cookedValue = [double]$counterSample.CookedValue
    }
    Add-Step 'SAMPLE' 'PASS' "Collected CPU counter sample: $([math]::Round($sample.cookedValue, 2))"
}

$result = [ordered]@{
    schemaVersion = 1
    capturedAt = (Get-Date).ToString('o')
    featureId = 'C-CPU-003'
    englishPath = $englishResult
    selectedPath = $selectedPath
    sample = $sample
    steps = @($steps)
}

$outputDirectory = Split-Path -Parent $OutputPath
New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
$json = ($result | ConvertTo-Json -Depth 8).Replace("`r`n", "`n")
[IO.File]::WriteAllText($OutputPath, $json, [Text.UTF8Encoding]::new($false))
Write-Output $OutputPath
