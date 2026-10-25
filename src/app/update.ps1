param(
    [ValidateSet('Check','Prepare','Apply','Library')][string]$Mode,
    [string]$JobDirectory
)
$ErrorActionPreference = 'Stop'
$ProgressPreference = 'SilentlyContinue'
Add-Type -AssemblyName System.IO.Compression.FileSystem
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
$script:Utf8 = New-Object Text.UTF8Encoding($false)
$script:Repo = 'PureDark/LeiaSRBridge'

function Write-Json($Path, $Value) {
    $temporary = $Path + '.tmp'
    [IO.File]::WriteAllText($temporary, ($Value | ConvertTo-Json -Depth 12), $script:Utf8)
    if ([IO.File]::Exists($Path)) { [IO.File]::Replace($temporary, $Path, [NullString]::Value) }
    else { [IO.File]::Move($temporary, $Path) }
}
function Status($Phase, $Message, $Extra = @{}) {
    $state = @{ phase=$Phase; message=$Message }
    foreach ($key in $Extra.Keys) { $state[$key]=$Extra[$key] }
    Write-Json (Join-Path $JobDirectory 'status.json') $state
}
function Hash-File($Path) {
    $stream=[IO.File]::OpenRead($Path)
    $algorithm=[Security.Cryptography.SHA256]::Create()
    try { return ([BitConverter]::ToString($algorithm.ComputeHash($stream))).Replace('-','').ToLowerInvariant() }
    finally { $algorithm.Dispose(); $stream.Dispose() }
}
function Read-Json($Path) { [IO.File]::ReadAllText($Path, [Text.Encoding]::UTF8) | ConvertFrom-Json }
function Version-Number([string]$Text) {
    if ($Text -cnotmatch '^v?([0-9]+)\.([0-9]+)\.([0-9]+)$') { throw "不支持的版本号：$Text" }
    return [version]($Text.TrimStart('v'))
}
function Validate-Release($Release, [string]$Current) {
    if ($Release.draft -or $Release.prerelease) { throw '只接受正式发布的版本。' }
    $version = Version-Number $Release.tag_name
    $currentVersion = Version-Number $Current
    $name = 'LeiaSRBridge-' + $version.ToString(3) + '-win64.zip'
    $assets = @($Release.assets | Where-Object { $_.name -ceq $name -and $_.state -eq 'uploaded' })
    if ($version -le $currentVersion) { return @{ newer=$false; version=$version.ToString(3) } }
    if ($assets.Count -ne 1) { throw "Release 缺少唯一的 $name 完整包。" }
    $asset = $assets[0]
    $expected = "https://github.com/$script:Repo/releases/download/$($Release.tag_name)/$name"
    if ($asset.browser_download_url -cne $expected) { throw '下载地址不属于预期的 GitHub Release。' }
    if ($asset.size -le 0 -or $asset.size -gt 1GB) { throw '更新包大小无效或超过 1 GB。' }
    if ($asset.digest -cnotmatch '^sha256:([0-9a-fA-F]{64})$') { throw 'GitHub 未提供更新包 SHA-256，无法验证下载。' }
    return @{ newer=$true; version=$version.ToString(3); tag=[string]$Release.tag_name; name=$name;
        url=$expected; bytes=[long]$asset.size; sha256=$asset.digest.Substring(7).ToLowerInvariant();
        notes=([string]$Release.body).Substring(0,[Math]::Min(65536,([string]$Release.body).Length)) }
}
function Http-Error([int]$Code) {
    switch ($Code) {
        404 { return 'GitHub HTTP 404：仓库仍为私有、尚未发布 Release，或文件不存在。当前功能匿名访问，不使用令牌。' }
        403 { return 'GitHub HTTP 403：访问被拒绝或匿名 API 额度已用尽，请稍后重试。' }
        429 { return 'GitHub HTTP 429：请求过于频繁，请稍后重试。' }
        default { return "GitHub HTTP $Code：无法访问更新，请检查网络或稍后重试。" }
    }
}
function Open-Response([string]$Url) {
    for ($redirect=0; $redirect -lt 6; ++$redirect) {
        $uri = [uri]$Url
        if ($uri.Scheme -ne 'https' -or $uri.Port -ne 443 -or $uri.UserInfo -or
            $uri.Host -notin @('api.github.com','github.com','release-assets.githubusercontent.com','objects.githubusercontent.com')) {
            throw '更新下载跳转到了不允许的地址。'
        }
        $request = [Net.HttpWebRequest]::Create($uri)
        $request.UserAgent = 'LeiaSRBridge-Updater'
        $request.Accept = 'application/vnd.github+json'
        $request.Timeout = 30000
        $request.ReadWriteTimeout = 30000
        $request.AllowAutoRedirect = $false
        $request.UseDefaultCredentials = $false
        try { $response = $request.GetResponse() }
        catch [Net.WebException] {
            if ($_.Exception.Response) {
                $code = [int]$_.Exception.Response.StatusCode
                $_.Exception.Response.Close()
                throw (Http-Error $code)
            }
            throw "网络连接失败：$($_.Exception.Status)。请检查网络、代理或证书。"
        }
        $code = [int]$response.StatusCode
        if ($code -ge 300 -and $code -lt 400) {
            $location = $response.Headers['Location']
            $response.Close()
            if (!$location) { throw 'GitHub 返回无效跳转。' }
            $Url = ([uri]::new($uri,$location)).AbsoluteUri
            continue
        }
        if ($code -ne 200) { $response.Close(); throw (Http-Error $code) }
        return $response
    }
    throw 'GitHub 下载跳转次数过多。'
}
function Get-Latest {
    $response = Open-Response "https://api.github.com/repos/$script:Repo/releases/latest"
    try {
        $stream = $response.GetResponseStream()
        $buffer = New-Object byte[] 16384
        $memory = New-Object IO.MemoryStream
        try {
            while (($read=$stream.Read($buffer,0,$buffer.Length)) -gt 0) {
                if ($memory.Length + $read -gt 2MB) { throw 'Release 响应过大。' }
                $memory.Write($buffer,0,$read)
            }
            return ([Text.Encoding]::UTF8.GetString($memory.ToArray()) | ConvertFrom-Json)
        } finally { $memory.Dispose(); $stream.Dispose() }
    } finally { $response.Close() }
}
function Download-Package($Release, $Path) {
    $response = Open-Response $Release.url
    try {
        $inputStream = $response.GetResponseStream()
        $outputStream = [IO.File]::Open($Path,[IO.FileMode]::CreateNew,[IO.FileAccess]::Write,[IO.FileShare]::None)
        try {
            $buffer = New-Object byte[] 131072
            [long]$total=0; $last=[datetime]::MinValue; $started=[datetime]::UtcNow
            while (($count=$inputStream.Read($buffer,0,$buffer.Length)) -gt 0) {
                $total += $count
                if ($total -gt $Release.bytes) { throw '下载内容超出 Release 声明的大小。' }
                if (([datetime]::UtcNow-$started).TotalMinutes -gt 30) { throw '下载超时，请重试。' }
                $outputStream.Write($buffer,0,$count)
                if (([datetime]::UtcNow-$last).TotalMilliseconds -ge 400) {
                    $percent=[int]($total*100/$Release.bytes)
                    Status 'downloading' "正在下载 $($Release.version)：$percent%" @{percent=$percent;version=$Release.version}
                    $last=[datetime]::UtcNow
                }
            }
            if ($total -ne $Release.bytes) { throw '更新包下载不完整。' }
        } finally { $outputStream.Dispose(); $inputStream.Dispose() }
    } finally { $response.Close() }
    if ((Hash-File $Path) -ine $Release.sha256) { throw '更新包 SHA-256 校验失败。' }
}
function Safe-Relative([string]$Name) {
    if (!$Name -or $Name.Length -gt 240 -or $Name.Contains('\') -or $Name -match '[:*?"<>|]' -or $Name.StartsWith('/')) {
        throw "不安全的包内路径：$Name"
    }
    foreach ($part in $Name.Split('/')) {
        if (!$part -or $part -in @('.','..') -or $part.EndsWith('.') -or $part.EndsWith(' ') -or
            $part -match '^(?i:CON|PRN|AUX|NUL|COM[0-9]|LPT[0-9])(?:\.|$)') { throw "不安全的包内路径：$Name" }
    }
    if ($Name -match '^(?i:settings\.ini|games\.txt|logs)(?:/|$)' -or
        $Name -match '(?i)(^|/)boe_mcu_mat\.ini$' -or $Name -match '(?i)^\.leiasr-update') {
        throw "更新包不允许覆盖用户配置或标定：$Name"
    }
    return $Name
}
function Child-Path([string]$Root,[string]$Relative) {
    $null=Safe-Relative $Relative
    $rootPath=[IO.Path]::GetFullPath($Root).TrimEnd('\')+'\'
    $path=[IO.Path]::GetFullPath((Join-Path $rootPath $Relative))
    if (!$path.StartsWith($rootPath,[StringComparison]::OrdinalIgnoreCase)) { throw '目标路径越界。' }
    $check=$path
    while ($check) {
        if (Test-Path -LiteralPath $check) {
            if ((Get-Item -LiteralPath $check -Force).Attributes -band [IO.FileAttributes]::ReparsePoint) { throw "更新路径含链接：$check" }
        }
        $parent=[IO.Path]::GetDirectoryName($check)
        if ($parent -eq $check) { break }
        $check=$parent
    }
    return $path
}
function Expand-CheckedPackage($ZipPath,$Stage,[string]$Version) {
    [IO.Directory]::CreateDirectory($Stage) | Out-Null
    $zip=[IO.Compression.ZipFile]::OpenRead($ZipPath)
    try {
        $names=New-Object 'Collections.Generic.HashSet[string]' ([StringComparer]::OrdinalIgnoreCase)
        [long]$total=0
        foreach ($entry in $zip.Entries) {
            $name=Safe-Relative $entry.FullName
            if (!$names.Add($name)) { throw "更新包含重复路径：$name" }
            if (($entry.ExternalAttributes -band 0x400) -ne 0 -or (($entry.ExternalAttributes -shr 16) -band 0xF000) -eq 0xA000) { throw '更新包不允许符号链接。' }
            $total += $entry.Length
            if ($total -gt 2GB -or $names.Count -gt 10000) { throw '更新包展开大小或文件数超限。' }
            $path=Child-Path $Stage $name
            [IO.Directory]::CreateDirectory([IO.Path]::GetDirectoryName($path)) | Out-Null
            [IO.Compression.ZipFileExtensions]::ExtractToFile($entry,$path,$false)
        }
    } finally { $zip.Dispose() }
    $manifest=Read-Json (Join-Path $Stage 'manifest.json')
    if ($manifest.product -cne 'LeiaSRBridge' -or $manifest.version -cne $Version -or $manifest.architecture -cne 'x64') {
        throw '更新包产品、版本或架构不匹配。'
    }
    $listed=New-Object 'Collections.Generic.HashSet[string]' ([StringComparer]::OrdinalIgnoreCase)
    foreach ($file in $manifest.files) {
        $name=Safe-Relative $file.file
        if ($name -ieq 'manifest.json' -or !$listed.Add($name) -or $file.sha256 -notmatch '^[0-9a-fA-F]{64}$') { throw '文件清单无效。' }
        $path=Child-Path $Stage $name
        if (!(Test-Path -LiteralPath $path -PathType Leaf) -or (Get-Item -LiteralPath $path).Length -ne $file.bytes -or
            (Hash-File $path) -ine $file.sha256) { throw "文件完整性检查失败：$name" }
    }
    if ($names.Count -ne $listed.Count+1) { throw '更新包包含清单之外的文件。' }
    foreach ($required in @('LeiaSRBridge.exe','LeiaSRBridgeTest.exe','runtime/TitanLeiaRuntime.dll','vendor/MCU_library.dll','vendor/eyetracking/FaceTracking.exe')) {
        if (!$listed.Contains($required)) { throw "更新包缺少组件：$required" }
    }
    return $manifest
}
function Copy-UpdateFile($Source,$Destination) { [IO.File]::Copy($Source,$Destination,$true) }
function Install-Package($Stage,$Target,$Manifest) {
    $files=@($Manifest.files | ForEach-Object { [string]$_.file }) + @('manifest.json')
    foreach ($name in $files) {
        $path=Child-Path $Target $name
        if (Test-Path -LiteralPath $path) {
            try {
                $handle=[IO.File]::Open($path,[IO.FileMode]::Open,[IO.FileAccess]::ReadWrite,[IO.FileShare]::None)
                $handle.Dispose()
            } catch { throw "文件被占用或目录不可写，未替换任何文件：$path。请退出游戏、测试窗口和 Hub 后重试。$($_.Exception.Message)" }
        }
    }
    $backup=Join-Path $Target ('.leiasr-update-'+[guid]::NewGuid().ToString('N'))
    [IO.Directory]::CreateDirectory($backup) | Out-Null
    $changed=New-Object 'Collections.Generic.List[object]'
    $complete=$false; $rolledBack=$true
    try {
        foreach ($name in $files) {
            $path=Child-Path $Target $name
            $source=Child-Path $Stage $name
            $saved=Child-Path $backup ('old/'+$name)
            $incoming=Child-Path $backup ('new/'+$name)
            [IO.Directory]::CreateDirectory([IO.Path]::GetDirectoryName($path)) | Out-Null
            [IO.Directory]::CreateDirectory([IO.Path]::GetDirectoryName($saved)) | Out-Null
            [IO.Directory]::CreateDirectory([IO.Path]::GetDirectoryName($incoming)) | Out-Null
            Copy-UpdateFile $source $incoming
            $existed=[IO.File]::Exists($path)
            if ($existed) { [IO.File]::Replace($incoming,$path,$saved) }
            else { [IO.File]::Move($incoming,$path) }
            $changed.Add(@{path=$path;saved=$saved;existed=$existed})
        }
        $complete=$true
    } catch {
        $original=$_
        for ($i=$changed.Count-1; $i -ge 0; --$i) {
            $item=$changed[$i]
            try {
                if ($item.existed) { [IO.File]::Copy($item.saved,$item.path,$true) }
                else { [IO.File]::Delete($item.path) }
            } catch { $rolledBack=$false }
        }
        if (!$rolledBack) { throw "更新失败且回滚未完成，请从 $backup 恢复，勿启动游戏。原因：$original" }
        throw "更新未完成，已恢复旧文件。请退出游戏、测试窗口或 Hub 后重试。原因：$original"
    } finally {
        if ($complete -or $rolledBack) {
            $resolved=[IO.Path]::GetFullPath($backup)
            $prefix=[IO.Path]::GetFullPath($Target).TrimEnd('\')+'\.leiasr-update-'
            if (!$resolved.StartsWith($prefix,[StringComparison]::OrdinalIgnoreCase)) { throw '备份清理路径校验失败。' }
            Remove-Item -LiteralPath $resolved -Recurse -Force
        }
    }
}
if ($Mode -eq 'Library') { return }
$applied=$false; $parentExited=$false; $canRestart=$true
try {
    $config=Read-Json (Join-Path $JobDirectory 'config.json')
    $target=[IO.Path]::GetFullPath([string]$config.directory)
    $releasePath=Join-Path $JobDirectory 'release.json'
    $zipPath=Join-Path $JobDirectory 'package.zip'
    $stage=Join-Path $JobDirectory 'stage'
    switch ($Mode) {
        'Check' {
            Status 'checking' '正在匿名访问 GitHub Release…'
            $info=Validate-Release (Get-Latest) $config.current
            if (!$info.newer) { Status 'current' "当前 $($config.current) 已是最新正式版。" @{version=$info.version} }
            else {
                Write-Json $releasePath $info
                Status 'available' "发现新版本 $($info.version)" @{version=$info.version;notes=$info.notes;bytes=$info.bytes}
            }
        }
        'Prepare' {
            $info=Read-Json $releasePath
            if (Test-Path -LiteralPath $zipPath) { [IO.File]::Delete($zipPath) }
            Download-Package $info $zipPath
            Status 'validating' '下载完成，正在校验和解压…'
            $null=Expand-CheckedPackage $zipPath $stage $info.version
            Status 'ready' "版本 $($info.version) 已下载并通过校验，请退出游戏后安装。" @{version=$info.version}
        }
        'Apply' {
            $info=Read-Json $releasePath
            $parent=Get-Process -Id ([int]$config.parentId) -ErrorAction SilentlyContinue
            if ($parent -and $parent.StartTime.ToUniversalTime().ToFileTimeUtc().ToString() -eq [string]$config.parentStart) {
                if (!$parent.WaitForExit(120000)) { throw '等待主程序退出超时，未替换任何文件。' }
            }
            $parentExited=$true
            $verifyStage=Join-Path $JobDirectory 'verified'
            if ((Hash-File $zipPath) -ine $info.sha256) { throw '安装前 ZIP 哈希校验失败。' }
            $manifest=Expand-CheckedPackage $zipPath $verifyStage $info.version
            Install-Package $verifyStage $target $manifest
            $applied=$true
            Status 'installed' "已更新至 $($info.version)。"
        }
    }
} catch {
    $errorText=[string]$_.Exception.Message
    if ($errorText -like '*回滚未完成*') { $canRestart=$false }
    Status 'error' $errorText
    if ($Mode -eq 'Apply') {
        try {
            $logs=Child-Path $target 'update-result.txt'
            [IO.File]::WriteAllText($logs,([datetime]::Now.ToString('s')+[Environment]::NewLine+$errorText),$script:Utf8)
            Add-Type -AssemblyName System.Windows.Forms
            [Windows.Forms.MessageBox]::Show($errorText,'LeiaSRBridge 更新未完成','OK','Error') | Out-Null
        } catch {}
    }
    if ($Mode -ne 'Apply') { exit 1 }
} finally {
    if ($Mode -eq 'Apply' -and $parentExited -and $canRestart) {
        try { Start-Process -FilePath (Join-Path $target 'LeiaSRBridge.exe') -WorkingDirectory $target }
        catch {
            Add-Type -AssemblyName System.Windows.Forms
            [Windows.Forms.MessageBox]::Show(('更新助手已结束，请手动启动 LeiaSRBridge。'+[Environment]::NewLine+$_.Exception.Message),'LeiaSRBridge','OK','Information') | Out-Null
        }
    }
    if ($Mode -eq 'Apply' -and $applied) {
        $jobPath=[IO.Path]::GetFullPath($JobDirectory)
        $temporaryRoot=[IO.Path]::GetFullPath([IO.Path]::GetTempPath()).TrimEnd('\')+'\'
        if ($jobPath.StartsWith($temporaryRoot,[StringComparison]::OrdinalIgnoreCase) -and
            [IO.Path]::GetFileName($jobPath).StartsWith('LeiaSRBridge-update-')) {
            Remove-Item -LiteralPath $jobPath -Recurse -Force -ErrorAction SilentlyContinue
        }
    }
}
