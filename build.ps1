param(
    [string]$BuildDirectory = "$PSScriptRoot/build",
    [string]$Preset = 'windows-x64'
)
$ErrorActionPreference = 'Stop'
$BuildDirectory = [IO.Path]::GetFullPath($BuildDirectory)
Push-Location $PSScriptRoot
try {
    cmake --preset $Preset -B $BuildDirectory
    if ($LASTEXITCODE -ne 0) { throw 'CMake configure failed' }
    cmake --build $BuildDirectory --config Release --parallel
    if ($LASTEXITCODE -ne 0) { throw 'Build failed' }
    ctest --test-dir $BuildDirectory -C Release --output-on-failure --no-tests=error --output-log "$BuildDirectory/full-tracking-tests.log"
    if ($LASTEXITCODE -ne 0) { throw 'Offline checks failed' }
    Write-Output "Package: $BuildDirectory/package"
} finally {
    Pop-Location
}
