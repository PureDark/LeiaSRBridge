from pathlib import Path
import argparse, shutil, subprocess
from prepare_vendor import embed_manifest

p=argparse.ArgumentParser()
p.add_argument('test',type=Path);p.add_argument('vendor',type=Path);p.add_argument('mt',type=Path)
a=p.parse_args()
directory=a.vendor.resolve()/'eyetracking'
child=directory/'PortableTrackingChild.exe'
try:
    shutil.copy2(a.test,child)
    embed_manifest(child,Path(__file__).resolve().parents[1]/'src/broker/FaceTracking.manifest',a.mt)
    subprocess.run([str(a.test.resolve()),str(directory)],check=True)
finally:
    child.unlink(missing_ok=True)
