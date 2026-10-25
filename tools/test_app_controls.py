"""Hidden mock UI: defaults, global hotkeys, persisted tuning and process restart."""
import ctypes as c
from ctypes import wintypes as w
from pathlib import Path
import shutil, subprocess, sys, tempfile, time, json
u=c.WinDLL('user32',use_last_error=True);k=c.WinDLL('kernel32',use_last_error=True)
u.FindWindowW.argtypes=[w.LPCWSTR,w.LPCWSTR];u.FindWindowW.restype=w.HWND
u.GetWindowThreadProcessId.argtypes=[w.HWND,c.POINTER(w.DWORD)]
u.SendMessageW.argtypes=[w.HWND,w.UINT,w.WPARAM,w.LPARAM];u.SendMessageW.restype=c.c_ssize_t
u.GetDlgItem.argtypes=[w.HWND,c.c_int];u.GetDlgItem.restype=w.HWND
u.IsWindowVisible.argtypes=[w.HWND];u.IsIconic.argtypes=[w.HWND]
k.OpenFileMappingW.argtypes=[w.DWORD,w.BOOL,w.LPCWSTR];k.OpenFileMappingW.restype=w.HANDLE
k.MapViewOfFile.argtypes=[w.HANDLE,w.DWORD,w.DWORD,w.DWORD,c.c_size_t];k.MapViewOfFile.restype=c.c_void_p
k.UnmapViewOfFile.argtypes=[c.c_void_p];k.CloseHandle.argtypes=[w.HANDLE]
def wait(predicate):
    end=time.monotonic()+15
    while time.monotonic()<end:
        if predicate():return
        time.sleep(.02)
    raise AssertionError('Timed out waiting for app control')
exe=Path(sys.argv[1]).resolve()
with tempfile.TemporaryDirectory(prefix='controls-',dir=exe.parent.parent/'tests') as tmp:
    path=Path(tmp);shutil.copy2(exe,path/exe.name)
    for stage in range(3):
        p=subprocess.Popen([str(path/exe.name),'--mock','--background','--duration-ms','15000'],cwd=path)
        handle=view=window=None
        try:
            def own_window():
                global window
                window=u.FindWindowW('TitanLeiaBridge.Main.v1',None);pid=w.DWORD()
                if window:u.GetWindowThreadProcessId(window,c.byref(pid))
                return window and pid.value==p.pid
            wait(own_window)
            def mapping():
                global handle
                handle=k.OpenFileMappingW(4,False,'Local\\TitanLeiaBridge.TrackingControl.v2')
                return bool(handle)
            wait(mapping)
            view=k.MapViewOfFile(handle,4,0,0,24);assert view
            fields=(c.c_int32*4).from_address(view)
            def check(offset,prediction):wait(lambda:fields[2]==offset and fields[3]==prediction)
            def hotkey(key):u.SendMessageW(window,0x312,key,0)
            def command(key):u.SendMessageW(window,0x111,key,0)
            def send(action,**args):
                class CopyData(c.Structure):
                    _fields_=[('dwData',c.c_size_t),('cbData',w.DWORD),('lpData',c.c_void_p)]
                text=json.dumps(dict(action=action,**args),ensure_ascii=False)
                data=c.create_unicode_buffer(text)
                copy=CopyData(0x544c4255,len(text.encode('utf-16-le')),c.addressof(data))
                assert u.SendMessageW(window,0x4a,0,c.addressof(copy))
            def number(value):send('offset',value=round(float(value)*10))
            if stage==0:
                check(80,40)
                duplicate=subprocess.Popen([str(path/exe.name),'--mock','--background'],cwd=path)
                assert duplicate.wait(timeout=5)==0,'Autostart duplicate should exit quietly'
                check(80,40)
                assert not u.GetDlgItem(window,118),'Removed filter control remains'
                assert not u.GetDlgItem(window,121),'Legacy native controls remain'
                # Mock mode must refuse system changes without a UAC helper.
                send('enable-integration');send('restore-integration')
                send('minimize');wait(lambda:u.IsIconic(window))
                send('hide');assert not u.IsWindowVisible(window)
                u.SendMessageW(window,0x8002,0,0x202) # tray left-button release
                wait(lambda:u.IsWindowVisible(window) and not u.IsIconic(window))
                hotkey(3);check(85,40);hotkey(5);check(86,40)
                hotkey(4);check(85,40);hotkey(2);check(80,40)
                number('-3.2');check(-32,40);hotkey(6);check(0,40)
                number('8.3');check(83,40)
                send('prediction',value=30)
                check(83,30)
                # Starting the native test is intentionally not exercised: no hardware.
            elif stage==1:
                check(83,30);hotkey(1);check(83,0)
            else:
                check(83,0);hotkey(1);check(83,30)
            command(112);assert p.wait(timeout=5)==0
            config=(path/'settings.ini').read_text()
            assert 'HorizontalOffsetTenths=83' in config
            assert 'PredictionMs='+('0' if stage==1 else '30') in config
            assert 'LastEnabledPredictionMs=30' in config
            assert not (path/'settings.ini.tmp').exists()
        finally:
            if view:k.UnmapViewOfFile(view)
            if handle:k.CloseHandle(handle)
            if p.poll() is None:
                if window:u.SendMessageW(window,0x111,112,0)
                try:p.wait(timeout=5)
                except subprocess.TimeoutExpired:p.terminate();p.wait()
    log=(path/'logs/LeiaSRBridge.log').read_text(encoding='utf-8')
    assert '快捷键注册失败' not in log and 'Eye observations' not in log and 'Eye filter' not in log
    print('AppControls PASS: +8 mm/40 ms defaults; global coarse/fine/zero; negative/numeric offset; prediction cycle/toggle; three-process save/reload including disabled prediction; no hardware/game.')
