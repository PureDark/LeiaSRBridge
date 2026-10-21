$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path $PSScriptRoot -Parent
$target = Join-Path $projectRoot 'dependencies/WebView2'
New-Item -ItemType Directory -Path $target -Force | Out-Null
$archive = Join-Path $target 'sdk.zip'
Invoke-WebRequest 'https://www.nuget.org/api/v2/package/Microsoft.Web.WebView2/1.0.4258.31' -OutFile $archive
$expected = '56f7f4b8bf9aee4b8efefbbdd4f67d5f74ebd1b100ed0806da71bf76af481aa9'
if ((Get-FileHash -LiteralPath $archive -Algorithm SHA256).Hash.ToLowerInvariant() -ne $expected) {
    throw 'WebView2 SDK archive identity mismatch'
}
Expand-Archive -LiteralPath $archive -DestinationPath (Join-Path $target 'sdk') -Force
Write-Output 'WebView2 SDK 1.0.4258.31 ready; dependencies remain outside Git.'
