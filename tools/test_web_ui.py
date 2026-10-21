"""Render and exercise the real WebView2 UI in an isolated hardware-free app copy."""
import json, shutil, subprocess, sys, time, tempfile
from pathlib import Path
exe=Path(sys.argv[1]).resolve()
with tempfile.TemporaryDirectory(prefix='webui-',dir=exe.parent.parent/'tests') as temp:
    root=Path(temp);shutil.copy2(exe,root/exe.name)
    (root/'games.txt').write_text(str(root/'Smoke & 测试.exe')+'\t\n',encoding='utf-8')
    p=subprocess.Popen([str(root/exe.name),'--mock','--ui-test','--duration-ms','14000'],cwd=root)
    try:
        p.wait(timeout=25)
        assert p.returncode==0,p.returncode
        result=json.loads((root/'logs/ui-test.json').read_text(encoding='utf-8'))
        assert result['ok'],result
        config=(root/'settings.ini').read_text(encoding='utf-8')
        assert 'HorizontalOffsetTenths=80' in config and 'PredictionMs=40' in config and 'AutoAttachList=0' in config
        assert (root/'games.txt').read_text(encoding='utf-8')==''
        out=exe.parent.parent/'verification-ui';out.mkdir(exist_ok=True)
        for name in ('overview','games','diag','odyssey'):
            capture=root/'logs'/f'ui-{name}.png';assert capture.stat().st_size>10000
            shutil.copy2(capture,out/capture.name)
        shutil.copy2(root/'logs/ui-test.json',out/'ui-test.json')
        print('WebUi PASS:',result)
    finally:
        if p.poll() is None:p.terminate();p.wait()
