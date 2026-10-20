$ErrorActionPreference = 'Stop'
$root = 'F:\目标软件\ModexLite\RE\evidence\sr-service-repair-20261020'
Start-Transcript -Path ($root + '\repair-transcript.txt') -Force
try {
    $principal = [Security.Principal.WindowsPrincipal]::new([Security.Principal.WindowsIdentity]::GetCurrent())
    if (-not $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) { throw 'Administrator token is required.' }
    $items = @(
        @{Name='SR Eye Tracker';Exe='C:\Program Files\LeiaSR\Tracker\bin\SREyeTracker.exe';Stem='tracker'},
        @{Name='SR Service';Exe='C:\Program Files\LeiaSR\Platform\bin\SRService.exe';Stem='platform'}
    )
    foreach ($item in $items) {
        $key = 'HKLM:\SYSTEM\CurrentControlSet\Services\' + $item.Name
        $svc = Get-CimInstance Win32_Service | Where-Object Name -eq $item.Name
        if (Test-Path -LiteralPath $key) { throw ($item.Name + ': registry configuration now exists; stop to review changed state.') }
        if ($svc -and $svc.State -ne 'Stopped') { throw ($item.Name + ': service is not stopped; stop to review changed state.') }
        $sig = Get-AuthenticodeSignature -LiteralPath $item.Exe
        if ($sig.Status -ne 'Valid' -or $sig.SignerCertificate.Subject -notmatch 'Leia') { throw ($item.Exe + ': vendor signature validation failed.') }
        $baseline = (Get-Content -LiteralPath ($root + '\before.json') -Raw -Encoding UTF8 | ConvertFrom-Json) | Where-Object Name -eq $item.Name
        if ((Get-FileHash -LiteralPath $item.Exe -Algorithm SHA256).Hash -ne $baseline.SHA256) { throw ($item.Exe + ': file changed since inspection.') }
    }
    foreach ($item in $items) {
        Write-Output ('Re-registering broken service: ' + $item.Name)
        & sc.exe delete $item.Name
        if ($LASTEXITCODE -ne 0) { throw ('sc delete failed: ' + $LASTEXITCODE) }
        $removed = $false
        for ($attempt=0; $attempt -lt 10; $attempt++) {
            & sc.exe query $item.Name
            if ($LASTEXITCODE -eq 1060) { $removed=$true; break }
            Start-Sleep -Milliseconds 500
        }
        if (-not $removed) { throw ($item.Name + ': pending deletion; close Services MMC before continuing.') }
        $proc = Start-Process -FilePath $item.Exe -ArgumentList '-install' -WorkingDirectory (Split-Path -Parent $item.Exe) -WindowStyle Hidden -RedirectStandardOutput ($root + '\' + $item.Stem + '-install.stdout.txt') -RedirectStandardError ($root + '\' + $item.Stem + '-install.stderr.txt') -PassThru
        if (-not $proc.WaitForExit(30000)) { throw ($item.Name + ': installer did not exit within 30 seconds; PID=' + $proc.Id) }
        Write-Output ('Vendor installer exit code: ' + $proc.ExitCode)
        Get-Content -LiteralPath ($root + '\' + $item.Stem + '-install.stdout.txt'),($root + '\' + $item.Stem + '-install.stderr.txt')
        $key = 'HKLM:\SYSTEM\CurrentControlSet\Services\' + $item.Name
        if (-not (Test-Path -LiteralPath $key)) { throw ($item.Name + ': vendor install did not restore registry key.') }
        $cfg = Get-ItemProperty -LiteralPath $key
        if ($cfg.ImagePath -notlike ('*' + $item.Exe + '*')) { throw ($item.Name + ': restored executable path unexpected: ' + $cfg.ImagePath) }
        & sc.exe qc $item.Name
        & sc.exe qfailure $item.Name
        & reg.exe export ('HKLM\SYSTEM\CurrentControlSet\Services\' + $item.Name) ($root + '\' + $item.Stem + '-restored.reg') /y
    }
    foreach ($item in $items) { Start-Service -Name $item.Name }
    Start-Sleep -Seconds 15
    $after = Get-CimInstance Win32_Service | Where-Object { $_.Name -in @('SR Service','SR Eye Tracker') } | Select-Object Name,DisplayName,State,StartMode,StartName,PathName,ProcessId,ExitCode
    $after | Format-List
    $after | ConvertTo-Json -Depth 4 | Set-Content -LiteralPath ($root + '\after.json') -Encoding UTF8
    if (@($after | Where-Object State -ne 'Running').Count -gt 0 -or @($after).Count -ne 2) { throw 'Both services must remain Running.' }
    'SUCCESS' | Set-Content -LiteralPath ($root + '\result.txt')
} catch {
    $_ | Out-String | Set-Content -LiteralPath ($root + '\error.txt') -Encoding UTF8
    'FAILED' | Set-Content -LiteralPath ($root + '\result.txt')
    Write-Output $_
    exit 1
} finally { Stop-Transcript }
