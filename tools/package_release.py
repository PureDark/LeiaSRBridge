"""Package an already tested build; never start hardware or game processes."""
from pathlib import Path
import argparse, datetime, hashlib, json, re, shutil, subprocess, sys, zipfile
from verify_artifacts import PE, sha

def write_json(path, value):
    path.write_text(json.dumps(value, ensure_ascii=False, indent=2), encoding='utf-8')

def main():
    parser=argparse.ArgumentParser()
    parser.add_argument('--build',type=Path,required=True)
    parser.add_argument('--crt',type=Path,required=True)
    parser.add_argument('--clients',nargs='+',required=True)
    args=parser.parse_args()
    root=Path(__file__).resolve().parents[1]
    build=args.build.resolve()
    version=re.search(r'project\(TitanLeiaBridge VERSION ([0-9.]+)',(root/'CMakeLists.txt').read_text()).group(1)
    package=root/'dist'/f'TitanLeiaBridge-{version}-win64'
    archive=Path(str(package)+'.zip')
    assert not package.exists() and not archive.exists(),'Release already exists; do not replace it silently'
    test_log=build/'full-tracking-tests.log'
    assert '100% tests passed' in test_log.read_text(encoding='utf-8'),'Complete successful test log required'
    source=build/'package'
    shutil.copytree(source,package,ignore=shutil.ignore_patterns(
        'logs','settings.ini','games.txt','boe_mcu_mat.ini','*.log','*.tmp','verification','manifest.json'))
    # README changes alone do not trigger the EXE's POST_BUILD copy.
    shutil.copy2(root/'README.zh-CN.md',package/'README.zh-CN.md')
    for name in ('msvcp140.dll','vcruntime140.dll','vcruntime140_1.dll'):
        shutil.copy2(args.crt/name,package/name)
        # Normal game loading uses runtime on PATH, not our EXE's directory.
        shutil.copy2(args.crt/name,package/'runtime'/name)
    # Preserve original provenance, explicitly identifying the renamed algorithm.
    provenance=json.loads((package/'vendor/provenance.json').read_text(encoding='utf-8'))
    provenance=[p for p in provenance if p['file']!='eyetracking/TitanBoeTrackingVendor.dll']
    for record in provenance:
        if record['file']=='eyetracking/BOE_3DET_SDK.dll':
            record['source_file']=record['file']
            record['file']='eyetracking/TitanBoeTrackingVendor.dll'
            record['note']='Unmodified original algorithm; result/subpixel proxy verifies this SHA256 before private reads.'
        actual=sha(package/'vendor'/record['file'])
        assert actual==record['source_sha256'] or record['file']=='eyetracking/FaceTracking.exe',record
        record['packaged_sha256']=actual
    provenance.append({'file':'eyetracking/BOE_3DET_SDK.dll',
        'packaged_sha256':sha(package/'vendor/eyetracking/BOE_3DET_SDK.dll'),
        'note':'TitanLeiaBridge result/subpixel-copy proxy; exact vendor identity and same-frame checks; no code/filter patch.'})
    write_json(package/'vendor/provenance.json',provenance)
    verification=package/'verification';verification.mkdir()
    shutil.copy2(root/'RE/bridge_rotation_0.4.0.md',verification/'rotation.md')
    shutil.copy2(test_log,verification/'test.log')
    shutil.copy2(build/'Testing/Temporary/LastTest.log',verification/'test-details.log')
    shutil.copy2(source/'logs/StereoTest-offline.log',verification/'StereoTest-offline.log')
    subprocess.run([sys.executable,'-B',str(root/'tools/verify_artifacts.py'),str(package),
        '--symbols',str(build/'symbols'),'--clients',*args.clients,
        '--out',str(verification/'artifact-verification.json')],check=True)
    tracking=package/'vendor/eyetracking'
    local={p.name.lower():PE(p) for p in tracking.glob('*.dll')}
    checked=0
    for binary in [PE(tracking/'FaceTracking.exe'),*local.values()]:
        for module,names in (binary.imports()|binary.imports(True)).items():
            if module.lower() not in local:continue
            for name in names:
                assert local[module.lower()].has_export(name),(binary.path.name,module,name)
                checked+=1
    assert PE(tracking/'FaceTracking.exe').directory(4)==(0,0)
    assert not list(package.rglob('boe_mcu_mat.ini')),'Do not package another display calibration'
    assert sha(package/'vendor/MCU_library.dll')=='0a2e51c617b585e53e0733135ccede7d5c3d3f30d58bea23313026641e050858'
    write_json(verification/'portable-import-verification.json',{
        'bundled_imported_symbols':checked,'private_exe_signature_directory_cleared':True,
        'foreign_device_calibration_in_package':False,'vendor_algorithm_unmodified':True,
        'result_proxy_filter_bypass':False,'head_pose':'stereo subpixel facial geometry; Rodrigues radians',
        'mcu_sha256':sha(package/'vendor/MCU_library.dll')})
    manifest={'product':'TitanLeiaBridge','version':version,'sdk':'1.34.10',
        'architecture':'x64','graphics':'DX12','protocol':3,
        'packagedAtUtc':datetime.datetime.now(datetime.timezone.utc).isoformat(),
        'files':[{'file':p.relative_to(package).as_posix(),'bytes':p.stat().st_size,'sha256':sha(p)}
                 for p in sorted(package.rglob('*')) if p.is_file()]}
    write_json(package/'manifest.json',manifest)
    with zipfile.ZipFile(archive,'w',zipfile.ZIP_DEFLATED,compresslevel=6) as output:
        for p in sorted(package.rglob('*')):
            if p.is_file():output.write(p,p.relative_to(package).as_posix())
    with zipfile.ZipFile(archive) as output:
        assert output.testzip() is None
        for entry in manifest['files']:
            assert hashlib.sha256(output.read(entry['file'])).hexdigest()==entry['sha256']
    print(json.dumps({'archive':str(archive),'sha256':sha(archive),'bytes':archive.stat().st_size,
        'manifest_entries':len(manifest['files'])},ensure_ascii=False))

if __name__=='__main__':main()
