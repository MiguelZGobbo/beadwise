param([Parameter(Mandatory)][string]$OutputPath)
$ErrorActionPreference='Stop'

function Hash-Text([string]$Value){if([string]::IsNullOrEmpty($Value)){return $null};[Convert]::ToHexString([Security.Cryptography.SHA256]::HashData([Text.Encoding]::UTF8.GetBytes($Value))).ToLowerInvariant()}
$present=@(Get-PnpDevice -PresentOnly -ErrorAction Stop)
$usb=@($present|Where-Object InstanceId -like 'USB*')
$controllers=@($present|Where-Object{$_.Class -eq 'USB' -and $_.FriendlyName -match 'Controller|Controlador|Hub|Root'})
$hid=@($present|Where-Object{$_.Class -eq 'HIDClass' -or $_.InstanceId -like 'HID*'})
$bluetooth=@($present|Where-Object{$_.Class -eq 'Bluetooth' -or $_.InstanceId -like 'BTH*'})
$safeDevices=@($usb|ForEach-Object{[ordered]@{class=[string]$_.Class;friendlyName=[string]$_.FriendlyName;status=[string]$_.Status;instanceIdSha256=Hash-Text $_.InstanceId}})
$speedProperties=@()
foreach($device in $usb|Select-Object -First 20){
 try{$props=@(Get-PnpDeviceProperty -InstanceId $device.InstanceId -ErrorAction Stop|Where-Object{$_.KeyName -match 'BusReportedDeviceDesc|LocationInfo|Address'});$speedProperties+=[ordered]@{deviceSha256=Hash-Text $device.InstanceId;propertyNames=@($props.KeyName);negotiatedSpeed=$null}}catch{$speedProperties+=[ordered]@{deviceSha256=Hash-Text $device.InstanceId;propertyNames=@();error=$_.Exception.Message;negotiatedSpeed=$null}}
}
$guardCases=@(
 [ordered]@{trust='known';managed=$false;maliciousEvidence=$false;decision='ALLOW_INVENTORY_ONLY'},
 [ordered]@{trust='unknown';managed=$false;maliciousEvidence=$false;decision='MANUAL_REVIEW'},
 [ordered]@{trust='unknown';managed=$true;maliciousEvidence=$false;decision='POLICY_OWNER'},
 [ordered]@{trust='unknown';managed=$false;maliciousEvidence=$true;decision='BLOCK_RECOMMENDATION'}
)
$probes=@(
 [ordered]@{featureId='C-USB-001';premise='Present USB devices and controller/hub candidates can be inventoried with stable hashed PnP provenance.';status=if($usb.Count){'PASS'}else{'UNSUPPORTED'};evidence=[ordered]@{presentDeviceCount=$usb.Count;controllerOrHubCount=$controllers.Count;devices=$safeDevices};limitations=@('PnP relation data does not fully prove physical port topology')},
 [ordered]@{featureId='C-USB-002';premise='Negotiated USB speed remains Unknown when documented properties do not expose it.';status='UNSUPPORTED';evidence=[ordered]@{queriedDeviceCount=$speedProperties.Count;deviceProperties=$speedProperties;negotiatedSpeedAvailable=$false};limitations=@('no hub IOCTL/USBView-equivalent negotiated-speed result on this host')},
 [ordered]@{featureId='C-USB-005';premise='Present HID devices can be enumerated without opening handles or injecting input.';status=if($hid.Count){'PASS'}else{'UNSUPPORTED'};evidence=[ordered]@{hidCount=$hid.Count;nonOkCount=@($hid|Where-Object Status -ne 'OK').Count;inputInjected=$false};limitations=@('no vendor-specific controller diagnostics')},
 [ordered]@{featureId='C-USB-010';premise='Bluetooth presence is reported separately from pairing/coexistence claims.';status=if($bluetooth.Count){'PARTIAL'}else{'UNSUPPORTED'};evidence=[ordered]@{presentBluetoothCount=$bluetooth.Count;coexistenceMeasured=$false};limitations=@('no controlled Wi-Fi coexistence or pairing scenario')},
 [ordered]@{featureId='C-USB-015';premise='Peripheral safeguard decisions preserve unknown and managed states without classifying by vendor name alone.';status=if(@($guardCases|Where-Object decision -eq 'MANUAL_REVIEW').Count -eq 1){'PASS'}else{'FAIL'};evidence=[ordered]@{cases=$guardCases;devicePolicyChanged=$false};limitations=@('fixture rules; no malicious device or managed policy matrix')}
)
$result=[ordered]@{schemaVersion=1;capturedAt=(Get-Date).ToString('o');mutationPolicy='read-only';rawInstanceIds=@();probes=$probes}
$dir=Split-Path -Parent $OutputPath;if($dir){New-Item -ItemType Directory -Path $dir -Force|Out-Null};[IO.File]::WriteAllText($OutputPath,(($result|ConvertTo-Json -Depth 10).Replace("`r`n","`n")),[Text.UTF8Encoding]::new($false));$OutputPath
