"""Exercise old executable entry and portable settings in an isolated, hardware-free upgrade."""
import ctypes as c
from ctypes import wintypes as w
from pathlib import Path
import shutil, subprocess, sys, tempfile, time
exe=Path(sys.argv[1]).resolve()
shim=Path(sys.argv[2]).resolve()
u=c.WinDLL('user32',use_last_error=True)
u.FindWindowW.argtypes=[w.LPCWSTR,w.LPCWSTR];u.FindWindowW.restype=w.HWND
u.GetWindowTextW.argtypes=[w.HWND,w.LPWSTR,c.c_int]
u.SendMessageW.argtypes=[w.HWND,w.UINT,w.WPARAM,w.LPARAM]
u.IsWindowVisible.argtypes=[w.HWND]
with tempfile.TemporaryDirectory(prefix='rename 升级 ',dir=exe.parent.parent/'tests') as temp:
    root=Path(temp)
    shutil.copy2(exe,root/'Stereo3DBridge.exe')
    shutil.copy2(shim,root/'TitanLeiaBridge.exe')
    settings='[Tracking]\nHorizontalOffsetTenths=125\nPredictionMs=20\nLastEnabledPredictionMs=30\n[Application]\nNativePreview=0\n[Integration]\nAutoAttachList=0\n[Odyssey]\nExecutable=C:\\Old Hub\\Odyssey3DHubUI.exe\n'
    (root/'settings.ini').write_text(settings,encoding='utf-8')
    games=str(root/'测试 Game.exe')+'\t--argument "with spaces"\n'
    (root/'games.txt').write_text(games,encoding='utf-8')
    p=subprocess.Popen([str(root/'TitanLeiaBridge.exe'),'--mock','--background','--duration-ms','8000'],cwd=root)
    window=None
    try:
        end=time.monotonic()+5
        while time.monotonic()<end:
            window=u.FindWindowW('TitanLeiaBridge.Main.v1',None)
            if window:break
            assert p.poll() is None,'Legacy launcher exited before the app started'
            time.sleep(.03)
        assert window,'Renamed app not launched via legacy entry'
        title=c.create_unicode_buffer(128);u.GetWindowTextW(window,title,128)
        assert title.value=='Stereo3DBridge',title.value
        assert not u.IsWindowVisible(window),'Legacy launcher dropped --background'
        # Keep the old window-class identity: a pre-rename and renamed instance cannot both open the camera.
        duplicate=subprocess.Popen([str(root/'Stereo3DBridge.exe'),'--mock','--background'],cwd=root)
        assert duplicate.wait(timeout=5)==0
        u.SendMessageW(window,0x111,112,0)
        assert p.wait(timeout=5)==0,'Legacy launcher did not propagate successful exit'
        saved=(root/'settings.ini').read_text(encoding='utf-8')
        for line in ['HorizontalOffsetTenths=125','PredictionMs=20','NativePreview=0','AutoAttachList=0','Executable=C:\\Old Hub\\Odyssey3DHubUI.exe']:
            assert line in saved,line
        assert (root/'games.txt').read_text(encoding='utf-8')==games
        assert (root/'logs/Stereo3DBridge.log').is_file()
        print('RenameUpgrade PASS: old EXE entry, Unicode path, background args, single-instance compatibility, tuning/Hub/game-list persistence.')
    finally:
        if p.poll() is None:
            if window:u.SendMessageW(window,0x111,112,0)
            try:p.wait(timeout=5)
            except subprocess.TimeoutExpired:p.kill();p.wait()
