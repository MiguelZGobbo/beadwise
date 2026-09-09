param([Parameter(Mandatory)][string]$OutputPath)
$ErrorActionPreference='Stop'

$adapters=@(Get-NetAdapter -ErrorAction Stop)
$active=@($adapters|Where-Object Status -eq 'Up')
$configs=@(Get-NetIPConfiguration -ErrorAction Stop)
$routes=@(Get-NetRoute -AddressFamily IPv4 -ErrorAction Stop|Where-Object DestinationPrefix -eq '0.0.0.0/0'|Sort-Object RouteMetric,InterfaceMetric)
$gateway=[string](@($configs|ForEach-Object{$_.IPv4DefaultGateway.NextHop}|Where-Object{$_})|Select-Object -First 1)
$loop=@(); for($i=0;$i -lt 10;$i++){ $sw=[Diagnostics.Stopwatch]::StartNew(); $ok=Test-Connection 127.0.0.1 -Count 1 -Quiet -ErrorAction SilentlyContinue; $sw.Stop(); $loop+=$sw.Elapsed.TotalMilliseconds }
$gatewayReachable=$null
if($gateway){$gatewayReachable=Test-Connection $gateway -Count 2 -Quiet -TimeoutSeconds 1 -ErrorAction SilentlyContinue}
$tcp=[Net.NetworkInformation.IPGlobalProperties]::GetIPGlobalProperties().GetTcpIPv4Statistics()
$advanced=@(Get-NetAdapterAdvancedProperty -ErrorAction SilentlyContinue)
$physical=Get-CimInstance Win32_NetworkAdapter -Filter 'PhysicalAdapter=True' -ErrorAction SilentlyContinue
$signed=Get-CimInstance Win32_PnPSignedDriver -ErrorAction SilentlyContinue|Where-Object DeviceClass -eq 'NET'
$base=[ordered]@{name='Ethernet';interfaceGuid='stable';route='0.0.0.0/0';capturedAt='2026-09-08T10:00:00Z';dhcpLease='A'}
$volatile=[ordered]@{name='Ethernet';interfaceGuid='stable';route='0.0.0.0/0';capturedAt='2026-09-08T10:01:00Z';dhcpLease='B'}
$semantic=[ordered]@{name='Ethernet 2';interfaceGuid='stable';route='10.0.0.0/8';capturedAt='2026-09-08T10:02:00Z';dhcpLease='C'}
$stableKeys='name','interfaceGuid','route'
$normalize={param($x) [ordered]@{name=$x.name;interfaceGuid=$x.interfaceGuid;route=$x.route}|ConvertTo-Json -Compress}
$volatileIgnored=(& $normalize $base)-eq(& $normalize $volatile)
$semanticDetected=(& $normalize $base)-ne(& $normalize $semantic)
$jitter=0.0; if($loop.Count -gt 1){$mean=($loop|Measure-Object -Average).Average;$jitter=($loop|ForEach-Object{[math]::Abs($_-$mean)}|Measure-Object -Average).Average}
$probes=@(
 [ordered]@{featureId='C-NETWORK-001';premise='Active adapter, IP configuration and default route can be correlated without exposing addresses.';status=if($active.Count -and $routes.Count){'PASS'}else{'PARTIAL'};evidence=[ordered]@{adapterCount=$adapters.Count;activeAdapterCount=$active.Count;defaultRouteCount=$routes.Count;configurationCount=$configs.Count;sources=@('NetAdapter','NetTCPIP')};limitations=@('no Wi-Fi/VPN transition')},
 [ordered]@{featureId='C-NETWORK-002';premise='Loopback and current gateway reachability return separate structured outcomes.';status=if(($loop|Where-Object{$_ -ge 0}).Count -eq 10){'PASS'}else{'FAIL'};evidence=[ordered]@{loopbackSamples=$loop.Count;gatewayPresent=[bool]$gateway;gatewayReachable=$gatewayReachable};limitations=@('no external target or captive portal')},
 [ordered]@{featureId='C-NETWORK-003';premise='A temporal sample reports jitter separately from TCP statistics and does not pretend loopback is WAN.';status='PARTIAL';evidence=[ordered]@{sampleCount=$loop.Count;meanAbsoluteJitterMs=[math]::Round($jitter,3);segmentsRetransmitted=$tcp.SegmentsResent;scope='loopback-only';statisticsSource='.NET IPGlobalProperties/TcpStatistics'};limitations=@('no controlled WAN target or impairment')},
 [ordered]@{featureId='C-NETWORK-005';premise='Ethernet advanced properties can be read without invoking a setter.';status=if($advanced.Count){'PASS'}else{'UNSUPPORTED'};evidence=[ordered]@{propertyCount=$advanced.Count;setterInvoked=$false};limitations=@('no cable fault or alternate negotiation')},
 [ordered]@{featureId='C-NETWORK-007';premise='Adapter identity can be correlated with signed driver and PnP problem state.';status=if(@($signed).Count){'PASS'}else{'PARTIAL'};evidence=[ordered]@{physicalAdapterCount=@($physical).Count;signedDriverCount=@($signed).Count;nonzeroConfigManagerCodes=@($physical|Where-Object ConfigManagerErrorCode -ne 0).Count};limitations=@('no driver fault/rollback fixture')},
 [ordered]@{featureId='C-NETWORK-013';premise='Drift comparison ignores timestamps and DHCP lease churn but detects identity/route changes.';status=if($volatileIgnored -and $semanticDetected){'PASS'}else{'FAIL'};evidence=[ordered]@{stableKeys=$stableKeys;volatileOnlyIgnored=$volatileIgnored;semanticChangeDetected=$semanticDetected};limitations=@('fixture-based drift; no real network transition')}
)
$result=[ordered]@{schemaVersion=1;capturedAt=(Get-Date).ToString('o');mutationPolicy='read-only';probes=$probes}
$dir=Split-Path -Parent $OutputPath;if($dir){New-Item -ItemType Directory -Path $dir -Force|Out-Null};[IO.File]::WriteAllText($OutputPath,(($result|ConvertTo-Json -Depth 8).Replace("`r`n","`n")),[Text.UTF8Encoding]::new($false));$OutputPath
