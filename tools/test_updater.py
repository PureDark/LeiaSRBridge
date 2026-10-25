"""Exercise the production Windows PowerShell 5.1 updater in an isolated filesystem."""
import hashlib, json, os, shutil, subprocess, sys, tempfile, time, zipfile
from pathlib import Path
repo=Path(__file__).resolve().parents[1]
with tempfile.TemporaryDirectory(prefix='leiasr-update-test-') as tmp:
    root=Path(tmp)
    script=root/'update.ps1'
    script.write_text((repo/'src/app/update.ps1').read_text(encoding='utf-8'),encoding='utf-8-sig')
    test=root/'test.ps1'
    test.write_text((repo/'tests/UpdaterSmoke.ps1').read_text(encoding='utf-8'),encoding='utf-8-sig')
    data={'LeiaSRBridge.exe':b'new exe','LeiaSRBridgeTest.exe':b'test',
          'runtime/TitanLeiaRuntime.dll':b'runtime','vendor/MCU_library.dll':b'mcu',
          'vendor/eyetracking/FaceTracking.exe':b'tracker'}
    manifest={'product':'LeiaSRBridge','version':'0.9.3','architecture':'x64',
              'files':[{'file':n,'bytes':len(b),'sha256':hashlib.sha256(b).hexdigest()} for n,b in data.items()]}
    entries={**data,'manifest.json':json.dumps(manifest).encode()}
    def archive(name, values):
        with zipfile.ZipFile(root/(name+'.zip'),'w') as z:
            for n,b in values.items(): z.writestr(n,b)
    archive('valid',entries)
    archive('traversal',{**entries,'../escape':b'bad'})
    archive('hash',{**entries,'LeiaSRBridge.exe':b'bad'})
    archive('extra',{**entries,'extra.dll':b'extra'})
    archive('duplicate',{**entries,'leiasrbridge.EXE':b'duplicate'})
    archive('config',{**entries,'settings.ini':b'bad'})
    archive('version',{**entries,'manifest.json':json.dumps({**manifest,'version':'1.0.0'}).encode()})
    with zipfile.ZipFile(root/'symlink.zip','w') as z:
        for n,b in entries.items(): z.writestr(n,b)
        link=zipfile.ZipInfo('link');link.create_system=3;link.external_attr=0o120777<<16;z.writestr(link,b'target')
    powershell=Path(os.environ['SystemRoot'])/'System32/WindowsPowerShell/v1.0/powershell.exe'
    subprocess.run([str(powershell),'-NoProfile','-NonInteractive','-ExecutionPolicy','Bypass',
                    '-File',str(test),'-Source',str(script),'-Root',str(root)],check=True,timeout=60)


# Real Apply entry point: parent wait, on-disk replacement and restarted executable.
# The replacement is a harmless fixture, never the real device application.
if len(sys.argv)>1:
    fixture=Path(sys.argv[1]).resolve()
    with tempfile.TemporaryDirectory(prefix='leiasr-handoff-test-') as tmp:
        root=Path(tmp);job=root/'job';job.mkdir()
        script=job/'update.ps1'
        script.write_text((repo/'src/app/update.ps1').read_text(encoding='utf-8'),encoding='utf-8-sig')
        target=root/'安装 & 中文 space';target.mkdir()
        (target/'LeiaSRBridge.exe').write_bytes(b'old exe')
        (target/'settings.ini').write_bytes(b'keep me')
        data={**data,'LeiaSRBridge.exe':fixture.read_bytes()}
        manifest={**manifest,'files':[{'file':n,'bytes':len(b),'sha256':hashlib.sha256(b).hexdigest()} for n,b in data.items()]}
        with zipfile.ZipFile(job/'package.zip','w') as z:
            for n,b in data.items(): z.writestr(n,b)
            z.writestr('manifest.json',json.dumps(manifest))
        parent=subprocess.Popen([str(fixture),'wait'])
        try:
            # Query FILETIME through the same Windows process API used by the application.
            import ctypes
            from ctypes import wintypes
            kernel=ctypes.WinDLL('kernel32',use_last_error=True)
            kernel.OpenProcess.restype=wintypes.HANDLE
            kernel.GetProcessTimes.argtypes=[wintypes.HANDLE,*([ctypes.POINTER(wintypes.FILETIME)]*4)]
            kernel.CloseHandle.argtypes=[wintypes.HANDLE]
            handle=kernel.OpenProcess(0x1000,False,parent.pid)
            stamps=[wintypes.FILETIME() for _ in range(4)]
            assert kernel.GetProcessTimes(handle,*map(ctypes.byref,stamps))
            kernel.CloseHandle(handle)
            stamp=(stamps[0].dwHighDateTime<<32)|stamps[0].dwLowDateTime
            (job/'config.json').write_text(json.dumps({'directory':str(target),'current':'0.9.2','parentId':parent.pid,'parentStart':str(stamp)}),encoding='utf-8')
            (job/'release.json').write_text(json.dumps({'version':'0.9.3','sha256':hashlib.sha256((job/'package.zip').read_bytes()).hexdigest()}),encoding='utf-8')
            helper=subprocess.Popen([str(powershell),'-NoProfile','-NonInteractive','-ExecutionPolicy','Bypass','-File',str(script),'-Mode','Apply','-JobDirectory',str(job)])
            try:
                time.sleep(2)
                assert (target/'LeiaSRBridge.exe').read_bytes()==b'old exe','Must wait for parent exit'
                parent.terminate();parent.wait(timeout=5)
                helper.wait(timeout=30)
                assert helper.returncode==0
                end=time.time()+5
                while not (target/'restart-ok.txt').exists() and time.time()<end: time.sleep(.1)
                assert (target/'restart-ok.txt').read_text()=='restarted'
                assert (target/'settings.ini').read_bytes()==b'keep me'
                assert (target/'LeiaSRBridge.exe').read_bytes()==fixture.read_bytes()
                print('Updater handoff PASS: waits for exact parent process; installs and restarts from Unicode path; preserves settings.')
            finally:
                if helper.poll() is None: helper.terminate();helper.wait()
        finally:
            if parent.poll() is None:parent.terminate();parent.wait()
