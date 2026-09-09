param(
    [Parameter(Mandatory = $true)]
    [string] $OutputPath
)

$ErrorActionPreference = 'Stop'

function Invoke-ReadOnlySource {
    param(
        [Parameter(Mandatory = $true)] [string] $Name,
        [Parameter(Mandatory = $true)] [scriptblock] $Operation
    )
    try {
        $value = & $Operation
        return [ordered]@{ name = $Name; status = 'PASS'; value = $value; error = $null }
    }
    catch [System.PlatformNotSupportedException] {
        return [ordered]@{ name = $Name; status = 'UNSUPPORTED'; value = $null; error = $_.Exception.Message }
    }
    catch {
        $status = if ($_.Exception.Message -match 'not supported|não.*suport|not found|não.*encontr') { 'UNSUPPORTED' } else { 'FAIL' }
        return [ordered]@{ name = $Name; status = $status; value = $null; error = $_.Exception.Message }
    }
}

$defender = Invoke-ReadOnlySource -Name 'Get-MpComputerStatus' -Operation {
    Get-MpComputerStatus | Select-Object AMServiceEnabled, AntivirusEnabled, AntispywareEnabled,
        BehaviorMonitorEnabled, IoavProtectionEnabled, NISEnabled, OnAccessProtectionEnabled,
        RealTimeProtectionEnabled, IsTamperProtected, AntivirusSignatureVersion,
        AntivirusSignatureLastUpdated
}
$firewallProfiles = @()
$firewallSource = Invoke-ReadOnlySource -Name 'Get-NetFirewallProfile' -Operation {
    @(Get-NetFirewallProfile | Select-Object Name, Enabled, DefaultInboundAction, DefaultOutboundAction)
}
if ($firewallSource.status -eq 'PASS') { $firewallProfiles = @($firewallSource.value) }
$providers = Invoke-ReadOnlySource -Name 'SecurityCenter2 AntiVirusProduct' -Operation {
    @(Get-CimInstance -Namespace 'root/SecurityCenter2' -ClassName AntiVirusProduct |
        Select-Object displayName, instanceGuid, pathToSignedProductExe, productState)
}
$managedPolicy = [ordered]@{
    defenderPolicyPresent = Test-Path -LiteralPath 'HKLM:\SOFTWARE\Policies\Microsoft\Windows Defender'
    firewallPolicyPresent = Test-Path -LiteralPath 'HKLM:\SOFTWARE\Policies\Microsoft\WindowsFirewall'
    policyManagerDefenderPresent = Test-Path -LiteralPath 'HKLM:\SOFTWARE\Microsoft\PolicyManager\current\device\Defender'
}
$baselineStatus = if ($firewallSource.status -eq 'PASS' -and ($defender.status -eq 'PASS' -or $providers.status -eq 'PASS')) { 'PASS' } else { 'PARTIAL' }

$secureBoot = Invoke-ReadOnlySource -Name 'Confirm-SecureBootUEFI' -Operation {
    [ordered]@{ enabled = [bool](Confirm-SecureBootUEFI) }
}
$tpm = Invoke-ReadOnlySource -Name 'Get-Tpm' -Operation {
    $tpmState = Get-Tpm
    if ($null -eq $tpmState -or $tpmState -is [string] -or -not $tpmState.PSObject.Properties['TpmPresent']) {
        throw [UnauthorizedAccessException]::new('Get-Tpm returned no structured state; this host requires administrator rights.')
    }
    $tpmState | Select-Object TpmPresent, TpmReady, TpmEnabled, TpmActivated, TpmOwned,
        RestartPending, ManufacturerId, ManufacturerVersion, ManagedAuthLevel
}
$deviceGuard = Invoke-ReadOnlySource -Name 'Win32_DeviceGuard' -Operation {
    Get-CimInstance -Namespace 'root/Microsoft/Windows/DeviceGuard' -ClassName Win32_DeviceGuard |
        Select-Object AvailableSecurityProperties, CodeIntegrityPolicyEnforcementStatus,
        InstanceIdentifier, RequiredSecurityProperties, SecurityFeaturesEnabled,
        SecurityServicesConfigured, SecurityServicesRunning, UsermodeCodeIntegrityPolicyEnforcementStatus,
        Version, VirtualizationBasedSecurityStatus
}
$platformSources = [ordered]@{ secureBoot = $secureBoot; tpm = $tpm; deviceGuard = $deviceGuard }
$platformPassCount = @($platformSources.Values | Where-Object status -eq 'PASS').Count
$platformStatus = if ($platformPassCount -eq 3) { 'PASS' } elseif ($platformPassCount -gt 0) { 'PARTIAL' } else { 'UNSUPPORTED' }

$osVersion = Invoke-ReadOnlySource -Name 'CurrentVersion registry' -Operation {
    Get-ItemProperty -LiteralPath 'HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion' |
        Select-Object ProductName, DisplayVersion, EditionID, InstallationType, CurrentBuildNumber, UBR
}
$hotfixes = Invoke-ReadOnlySource -Name 'Get-HotFix' -Operation {
    @(Get-HotFix | Sort-Object InstalledOn -Descending | Select-Object -First 10 HotFixID, Description, InstalledOn)
}
$updateHistory = Invoke-ReadOnlySource -Name 'Windows Update COM history' -Operation {
    $session = New-Object -ComObject Microsoft.Update.Session
    $searcher = $session.CreateUpdateSearcher()
    $count = [Math]::Min(20, $searcher.GetTotalHistoryCount())
    if ($count -eq 0) { return @() }
    @($searcher.QueryHistory(0, $count) | Select-Object Date, Title, ResultCode, Operation)
}
$osBuild = if ($osVersion.status -eq 'PASS') {
    "$($osVersion.value.CurrentBuildNumber).$($osVersion.value.UBR)"
} else {
    [Environment]::OSVersion.Version.Build.ToString()
}
$lifecycle = [ordered]@{
    status = 'UNSUPPORTED'
    reason = 'No versioned official lifecycle dataset is bundled with this prototype; local OS state cannot establish support dates.'
    inferredSupportState = $null
}

$probes = @(
    [ordered]@{
        featureId = 'C-SECURITY-001'; status = $baselineStatus;
        mechanism = 'Defender cmdlet + Firewall cmdlet + SecurityCenter2 CIM + policy Registry presence';
        evidence = [ordered]@{ defender = $defender; firewallSource = $firewallSource; firewallProfiles = $firewallProfiles; antivirusProviders = $providers; managedPolicyPresence = $managedPolicy; mutationAttempted = $false }
        limitations = @('Current host only; no third-party AV, MDM, or alternate tamper-state fixture was induced.')
    }
    [ordered]@{
        featureId = 'C-SECURITY-003'; status = $platformStatus;
        mechanism = 'Confirm-SecureBootUEFI + Get-Tpm + Win32_DeviceGuard CIM';
        evidence = [ordered]@{ sources = $platformSources; successfulSourceCount = $platformPassCount; mutationAttempted = $false }
        limitations = @('No BIOS-disabled, TPM-absent, or alternate-hardware fixture; observed errors remain explicit.')
    }
    [ordered]@{
        featureId = 'C-SECURITY-007'; status = 'PARTIAL';
        mechanism = 'CurrentVersion Registry + hotfix inventory + Windows Update COM history';
        evidence = [ordered]@{ osBuild = $osBuild; osVersion = $osVersion; hotfixes = $hotfixes; updateHistory = $updateHistory; lifecycle = $lifecycle; mutationAttempted = $false }
        limitations = @('Lifecycle exposure is deliberately UNSUPPORTED without versioned official metadata; no out-of-support host fixture.')
    }
)

$document = [ordered]@{
    generatedAt = [DateTimeOffset]::Now.ToString('o')
    safety = [ordered]@{ persistentMutation = $false; readOnlyCommandsOnly = $true }
    probes = $probes
}
$parent = Split-Path -Parent $OutputPath
if ($parent) { New-Item -ItemType Directory -Path $parent -Force | Out-Null }
$document | ConvertTo-Json -Depth 14 | Set-Content -LiteralPath $OutputPath -Encoding utf8
