param([string]$Source,[string]$Root)
$ErrorActionPreference='Stop'
. $Source -Mode Library
function Assert($Value,$Message) { if (!$Value) { throw $Message } }
function Reject([scriptblock]$Action,$Label) {
    $rejected=$false
    try { & $Action | Out-Null } catch { $rejected=$true }
    Assert $rejected "Expected rejection: $Label"
}
$release=@{tag_name='v0.10.0';draft=$false;prerelease=$false;body='Release notes';
 assets=@(@{name='LeiaSRBridge-0.10.0-win64.zip';state='uploaded';size=100;
 browser_download_url='https://github.com/PureDark/LeiaSRBridge/releases/download/v0.10.0/LeiaSRBridge-0.10.0-win64.zip';
 digest=('sha256:'+('a'*64))})}
Assert ((Validate-Release $release '0.9.2').newer) 'Numeric version ordering'
Assert (!(Validate-Release $release '0.10.0').newer) 'Same version'
Assert (!(Validate-Release $release '1.0.0').newer) 'No downgrade'
$release.prerelease=$true;Reject { Validate-Release $release '0.9.2' } 'Prerelease'
$release.prerelease=$false;$release.assets[0].digest=$null
Reject { Validate-Release $release '0.9.2' } 'Missing digest'
$release.assets[0].digest='sha256:'+('a'*64);$release.assets[0].browser_download_url='https://evil.invalid/a.zip'
Reject { Validate-Release $release '0.9.2' } 'Untrusted asset'
foreach ($bad in @('../x','/abs','C:/x','foo\bar','settings.ini','logs/x','vendor/boe_mcu_mat.ini','a:b','a/CON.txt','x./y')) {
 Reject { Safe-Relative $bad } $bad
}
Write-Json (Join-Path $Root 'atomic.json') @{value=1}
Write-Json (Join-Path $Root 'atomic.json') @{value=2}
Assert ((Read-Json (Join-Path $Root 'atomic.json')).value -eq 2) 'Atomic status replacement'
Assert ((Http-Error 404).Contains('HTTP 404')) '404 detail'
Assert ((Http-Error 403).Contains('额度')) 'Rate limit detail'

$stage=Join-Path $Root 'stage'
$manifest=Expand-CheckedPackage (Join-Path $Root 'valid.zip') $stage '0.9.3'
foreach ($bad in @('traversal','hash','extra','duplicate','config','version','symlink')) {
 $badStage=Join-Path $Root ('reject-'+$bad)
 Reject { Expand-CheckedPackage (Join-Path $Root ($bad+'.zip')) $badStage '0.9.3' } $bad
}
$target=Join-Path $Root '安装 中文 space'
[IO.Directory]::CreateDirectory($target) | Out-Null
[IO.File]::WriteAllText((Join-Path $target 'settings.ini'),'my settings')
[IO.File]::WriteAllText((Join-Path $target 'games.txt'),'my games')
[IO.Directory]::CreateDirectory((Join-Path $target 'vendor/eyetracking')) | Out-Null
[IO.File]::WriteAllText((Join-Path $target 'vendor/eyetracking/boe_mcu_mat.ini'),'my calibration')
[IO.File]::WriteAllText((Join-Path $target 'LeiaSRBridge.exe'),'old exe')
# A locked target fails before replacing any file.
$locked=[IO.File]::Open((Join-Path $target 'LeiaSRBridge.exe'),'Open','Read','None')
try { Reject { Install-Package $stage $target $manifest } 'Locked executable' }
finally { $locked.Dispose() }
Assert ([IO.File]::ReadAllText((Join-Path $target 'LeiaSRBridge.exe')) -eq 'old exe') 'Lock preserved original'
# Fail the second copy after the first atomic replacement, then verify rollback.
$script:copies=0
function Copy-UpdateFile($Source,$Destination) {
 $script:copies++
 if ($script:copies -eq 2) { throw 'Injected disk failure' }
 [IO.File]::Copy($Source,$Destination,$true)
}
Reject { Install-Package $stage $target $manifest } 'Mid-install failure'
Assert ([IO.File]::ReadAllText((Join-Path $target 'LeiaSRBridge.exe')) -eq 'old exe') 'Rollback restored original'
Assert (!(Test-Path -LiteralPath (Join-Path $target 'LeiaSRBridgeTest.exe'))) 'Rollback removed newly installed files'
function Copy-UpdateFile($Source,$Destination) { [IO.File]::Copy($Source,$Destination,$true) }
Install-Package $stage $target $manifest
Assert ([IO.File]::ReadAllText((Join-Path $target 'LeiaSRBridge.exe')) -eq 'new exe') 'Installed version'
Assert ([IO.File]::ReadAllText((Join-Path $target 'settings.ini')) -eq 'my settings') 'Settings preserved'
Assert ([IO.File]::ReadAllText((Join-Path $target 'games.txt')) -eq 'my games') 'Games preserved'
Assert ([IO.File]::ReadAllText((Join-Path $target 'vendor/eyetracking/boe_mcu_mat.ini')) -eq 'my calibration') 'Calibration preserved'
Assert (@(Get-ChildItem -LiteralPath $target -Filter '.leiasr-update-*' -Force).Count -eq 0) 'Backups cleaned'
# Exercise the real download loop with an in-memory HTTP response, without credentials or network.
$JobDirectory=$Root
$script:networkBytes=[IO.File]::ReadAllBytes((Join-Path $Root 'valid.zip'))
function Open-Response($Url) {
 $response=New-Object PSObject
 $response | Add-Member ScriptMethod GetResponseStream { New-Object IO.MemoryStream(,$script:networkBytes) }
 $response | Add-Member ScriptMethod Close {}
 return $response
}
$download=@{url='fixture';bytes=$script:networkBytes.Length;version='0.9.3';sha256=(Hash-File (Join-Path $Root 'valid.zip'))}
Download-Package $download (Join-Path $Root 'downloaded.zip')
$download.sha256='0'*64
Reject { Download-Package $download (Join-Path $Root 'corrupt-download.zip') } 'Download hash'
$download.bytes++
Reject { Download-Package $download (Join-Path $Root 'short-download.zip') } 'Truncated download'
Write-Output 'Updater PASS: versions, assets, download hash/length, ZIP traversal/duplicates/links/hash/config, file locks, rollback and preserved settings/calibration.'
