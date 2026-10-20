param(
    [string]$BuildDirectory = "$PSScriptRoot/build",
    [Parameter(Mandatory = $true)][string]$CrtDirectory,
    [Parameter(Mandatory = $true)][string[]]$Clients,
    [string]$Python = 'python'
)
$ErrorActionPreference = 'Stop'
# The release version comes from CMakeLists.txt. Existing releases are never replaced.
& $Python -B "$PSScriptRoot/tools/package_release.py" --build $BuildDirectory --crt $CrtDirectory --clients @Clients
if ($LASTEXITCODE -ne 0) { throw 'Release validation or packaging failed' }
