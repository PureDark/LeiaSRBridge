"""Offline PE import/export and binary/PDB identity checks; executes no target DLL."""
from pathlib import Path
import argparse, hashlib, json, struct, uuid

class PE:
    def __init__(self, path):
        self.path=Path(path);self.data=self.path.read_bytes()
        self.pe=self.u32(60)
        assert self.data[:2]==b'MZ' and self.data[self.pe:self.pe+4]==b'PE\0\0'
        assert self.u16(self.pe+4)==0x8664,'expected x64'
        self.opt=self.pe+24
        assert self.u16(self.opt)==0x20b
        self.base=self.u64(self.opt+24)
        self.sections=[]
        for i in range(self.u16(self.pe+6)):
            s=self.opt+self.u16(self.pe+20)+i*40
            self.sections.append(struct.unpack_from('<IIII',self.data,s+8))
    def u16(self,o):return struct.unpack_from('<H',self.data,o)[0]
    def u32(self,o):return struct.unpack_from('<I',self.data,o)[0]
    def u64(self,o):return struct.unpack_from('<Q',self.data,o)[0]
    def off(self,rva):
        for vsize,va,rsize,raw in self.sections:
            if va<=rva<va+max(vsize,rsize):return raw+rva-va
        if rva<self.u32(self.opt+60):return rva
        raise ValueError(f'RVA outside PE: {rva}')
    def cstr(self,rva):
        o=self.off(rva);return self.data[o:self.data.index(0,o)].decode('ascii')
    def directory(self,n):return struct.unpack_from('<II',self.data,self.opt+112+n*8)
    def exports(self):
        rva,size=self.directory(0)
        if not rva:return {}
        o=self.off(rva);count=self.u32(o+24);funcs,names,ords=struct.unpack_from('<III',self.data,o+28)
        result={}
        for i in range(count):
            name=self.cstr(self.u32(self.off(names)+i*4));ordinal=self.u16(self.off(ords)+i*2)
            address=self.u32(self.off(funcs)+ordinal*4)
            result[name]=self.cstr(address) if rva<=address<rva+size else None
        return result
    def imports(self,delay=False):
        rva,size=self.directory(13 if delay else 1)
        if not rva:return {}
        result={};o=self.off(rva)
        while any(self.data[o:o+(32 if delay else 20)]):
            if delay:
                attrs,name,_,_,thunk,_,_,_=struct.unpack_from('<8I',self.data,o)
                assert attrs&1,'VA-form delay descriptors not supported'
            else:thunk,_,_,name,iat=struct.unpack_from('<5I',self.data,o);thunk=thunk or iat
            module=self.cstr(name);names=[];p=self.off(thunk)
            while self.u64(p):
                value=self.u64(p);p+=8
                names.append(f'#{value&65535}' if value>>63 else self.cstr(value+2))
            result[module]=names;o+=32 if delay else 20
        return result
    def has_export(self,name):
        if not name.startswith('#'):return name in self.exports()
        rva,_=self.directory(0)
        if not rva:return False
        o=self.off(rva);index=int(name[1:])-self.u32(o+16)
        return 0<=index<self.u32(o+20) and self.u32(self.off(self.u32(o+28))+index*4)!=0
    def codeview(self):
        rva,size=self.directory(6)
        if not rva:return None
        for i in range(size//28):
            o=self.off(rva)+i*28
            if self.u32(o+12)!=2:continue
            length,_,raw=struct.unpack_from('<III',self.data,o+16)
            if self.data[raw:raw+4]!=b'RSDS':continue
            guid=self.data[raw+4:raw+20];age=self.u32(raw+20)
            path=self.data[raw+24:raw+length].split(b'\0')[0].decode('utf-8','replace')
            return {'guid':str(uuid.UUID(bytes_le=guid)),'age':age,'pdb':Path(path.replace('\\','/')).name}

def pdb_identity(path):
    data=Path(path).read_bytes()
    assert data.startswith(b'Microsoft C/C++ MSF 7.00')
    block=struct.unpack_from('<I',data,32)[0];directory_size=struct.unpack_from('<I',data,44)[0]
    block_map=struct.unpack_from('<I',data,52)[0]
    count=(directory_size+block-1)//block
    directory_blocks=struct.unpack_from(f'<{count}I',data,block_map*block)
    directory=b''.join(data[b*block:(b+1)*block] for b in directory_blocks)[:directory_size]
    streams=struct.unpack_from('<I',directory,0)[0]
    sizes=struct.unpack_from(f'<{streams}I',directory,4);offset=4+streams*4
    content=None
    for index,size in enumerate(sizes):
        n=0 if size==0xffffffff else (size+block-1)//block
        blocks=struct.unpack_from(f'<{n}I',directory,offset);offset+=4*n
        if index==1:content=b''.join(data[b*block:(b+1)*block] for b in blocks)[:size]
    assert content and len(content)>=28
    return {'guid':str(uuid.UUID(bytes_le=content[12:28])),'age':struct.unpack_from('<I',content,8)[0]}

def sha(path):return hashlib.sha256(Path(path).read_bytes()).hexdigest()
def main():
    parser=argparse.ArgumentParser();parser.add_argument('package');parser.add_argument('--symbols');parser.add_argument('--clients',nargs='*',default=[])
    parser.add_argument('--out',required=True);args=parser.parse_args()
    package=Path(args.package);runtime=package/'runtime'
    modules={p.name.lower():PE(p) for p in runtime.glob('*.dll')}
    implementation=modules['titanleiaruntime.dll'].exports();forward_count=0
    for name,module in modules.items():
        if not name.startswith('simulatedreality'):continue
        for exported,target in module.exports().items():
            assert '"' not in exported and target and target.startswith('TitanLeiaRuntime.'),(name,exported,target)
            assert target.split('.',1)[1] in implementation,(name,exported,'missing forward target')
            forward_count+=1
    clients=[]
    for path in args.clients:
        pe=PE(path);checked=0
        for module,names in (pe.imports()|pe.imports(True)).items():
            if module.lower().startswith('simulatedreality'):
                assert module.lower() in modules,('missing module',module)
                exports=modules[module.lower()].exports()
                for name in names:assert name in exports,('missing client import',path,module,name)
                checked+=len(names)
        clients.append({'file':str(path),'sha256':sha(path),'supported_imports':checked})
    pairs=[]
    if args.symbols:
        for path in [package/'Stereo3DBridge.exe',package/'Stereo3DBridgeTest.exe',package/'TitanLeiaBridge.exe']+list(runtime.glob('SimulatedReality*.dll'))+[runtime/'TitanLeiaRuntime.dll',runtime/'TitanOdysseyHook.dll',runtime/'odyssey/txBridge.dll',package/'vendor/eyetracking/BOE_3DET_SDK.dll']:
            cv=PE(path).codeview();assert cv,('missing CodeView',path)
            pdb=Path(args.symbols)/cv['pdb'];ident=pdb_identity(pdb)
            assert cv['guid']==ident['guid'] and cv['age']==ident['age'],('PDB mismatch',path,pdb)
            pairs.append({'binary':str(path),'binary_sha256':sha(path),'pdb':str(pdb),'pdb_sha256':sha(pdb),**ident})
    proxy=PE(package/'vendor/eyetracking/BOE_3DET_SDK.dll')
    vendor=PE(package/'vendor/eyetracking/TitanBoeTrackingVendor.dll')
    assert set(proxy.exports())==set(vendor.exports()),'BOE proxy export surface mismatch'
    for name,target in proxy.exports().items():
        assert (target is None if name=='doEyeTrack_3D' else target=='TitanBoeTrackingVendor.'+name),(name,target)
    assert sha(vendor.path)=='752cb438a6d98848f45870c2502ea4ffc886c8daf14c1ee411af77518cd328b5','Vendor algorithm changed'
    result={'forwarded_exports':forward_count,'client_checks':clients,'binary_pdb_pairs':pairs,'boe_proxy_exports':list(proxy.exports()),'boe_vendor_sha256':sha(vendor.path)}
    Path(args.out).write_text(json.dumps(result,ensure_ascii=False,indent=2),encoding='utf-8')
    print(json.dumps({'forwarded_exports':forward_count,'clients':len(clients),'matched_binary_pdb_pairs':len(pairs)}))
if __name__=='__main__':main()
