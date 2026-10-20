"""Build a private portable vendor tree; never register COM or run vendor code."""
from pathlib import Path
import argparse, hashlib, json, shutil, struct, subprocess
from verify_artifacts import PE

def digest(path):
    return hashlib.sha256(Path(path).read_bytes()).hexdigest()

def embed_manifest(executable, manifest, mt):
    # Resource modification invalidates the original Authenticode signature.
    # Remove the stale certificate from the PRIVATE copy, not the source file.
    pe = PE(executable)
    offset, size = pe.directory(4)
    data = bytearray(pe.data)
    struct.pack_into('<II', data, pe.opt + 112 + 4 * 8, 0, 0)
    if offset and offset + size == len(data):
        del data[offset:]
    executable.write_bytes(data)
    subprocess.run([str(mt), '-nologo', '-manifest', str(manifest),
                    f'-outputresource:{executable};#1'], check=True)

def main():
    p = argparse.ArgumentParser()
    p.add_argument('source', type=Path)
    p.add_argument('destination', type=Path)
    p.add_argument('--mt', type=Path, required=True)
    p.add_argument('--crt', type=Path, required=True)
    a = p.parse_args()
    source, dest = a.source.resolve(), a.destination.resolve()
    assert source != dest and source not in dest.parents, 'Use a separate workspace vendor directory'
    expected = {
        'MCU_library.dll': '0A2E51C617B585E53E0733135CCEDE7D5C3D3F30D58BEA23313026641E050858',
        'eyetracking/FaceTrackingManagerService.dll': '3132DD6FE285772B327B4AAB99DE5A05C5CFE4376432E9DEFDC2680C465E4604',
        'eyetracking/FaceTracking.exe': '85254AD5838FC6D61C68A9EEA1B216D578103239D9432CEF0BE19E0A1CBC37C4',
    }
    for relative, sha in expected.items():
        assert digest(source / relative).upper() == sha, f'Unexpected vendor version: {relative}'
    files = ['MCU_library.dll'] + ['eyetracking/' + name for name in (
        'FaceTracking.exe', 'FaceTrackingManagerService.dll', 'BOE_3DET_SDK.dll',
        'opencv_world4110.dll', 'onnxruntime.dll', 'BoeLandmark.dll', 'faceTrackingConfig.ini',
        'models/face_det.onnx', 'models/data21_v2.mdata', 'models/landmark_det.onnx')]
    records = []
    for relative in files:
        packaged = ('eyetracking/TitanBoeTrackingVendor.dll'
                    if relative == 'eyetracking/BOE_3DET_SDK.dll' else relative)
        target = dest / packaged
        target.parent.mkdir(parents=True, exist_ok=True)
        shutil.copy2(source / relative, target)
        records.append({'file': packaged, 'source_file': relative, 'source_sha256': digest(source / relative)})
    embed_manifest(dest / 'eyetracking/FaceTracking.exe',
                   Path(__file__).resolve().parents[1] / 'src/broker/FaceTracking.manifest', a.mt)
    for name in ('msvcp140.dll', 'msvcp140_1.dll', 'vcruntime140.dll', 'vcruntime140_1.dll', 'concrt140.dll'):
        shutil.copy2(a.crt / name, dest / 'eyetracking' / name)
    # No boe_mcu_mat.ini from this machine: production writes current-device data.
    for record in records:
        record['packaged_sha256'] = digest(dest / record['file'])
    (dest / 'provenance.json').write_text(json.dumps(records, indent=2), encoding='utf-8')
    print(f'Portable vendor tree prepared: {dest}')

if __name__ == '__main__':
    main()
