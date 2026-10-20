"""Read LF3D PE metadata and known vtables without loading target modules."""
from pathlib import Path
import struct
import json

root = Path(__file__).resolve().parents[1]
def inspect(path, tables):
    data = path.read_bytes()
    u16 = lambda x: struct.unpack_from('<H', data, x)[0]
    u32 = lambda x: struct.unpack_from('<I', data, x)[0]
    pe = u32(0x3c)
    opt = pe + 24
    is64 = u16(opt) == 0x20b
    base = struct.unpack_from('<Q' if is64 else '<I', data, opt + (24 if is64 else 28))[0]
    dirs = opt + (112 if is64 else 96)
    segs = []
    for i in range(u16(pe + 6)):
        off = opt + u16(pe + 20) + i * 40
        vs, rva, rs, rp = struct.unpack_from('<IIII', data, off + 8)
        segs.append((rva, max(vs, rs), rp))
    def offset(rva):
        for start, size, raw in segs:
            if start <= rva < start + size:
                return raw + rva - start
        raise ValueError(hex(rva))
    def string(rva):
        off = offset(rva)
        return data[off:data.index(b'\0',off)].decode(errors='replace')
    exp = u32(dirs)
    exports = []
    if exp:
        p = offset(exp)
        ordinal, nfunc, nnames, funcs, names, ords = struct.unpack_from('<IIIIII',data,p+16)
        for i in range(nnames):
            idx = u16(offset(ords)+i*2)
            exports.append({'name': string(u32(offset(names)+i*4)), 'ordinal':ordinal+idx,'va':hex(base+u32(offset(funcs)+idx*4))})
    vtables = {}
    for name, (va,n) in tables.items():
        off = offset(va-base)
        vtables[name] = [{'slot':i,'va':hex(struct.unpack_from('<Q',data,off+8*i)[0])} for i in range(n)]
    return {'path':str(path), 'base':hex(base), 'clr_rva':hex(u32(dirs+14*8)), 'exports':exports,'vtables':vtables}

native = inspect(root/'inputs/depth/LF3DRenderCore.dll', {'LF3DDX11Render':(0x180084c68,19),'LF3DSimpleRender':(0x180084e48,12),'RenderCore_D3D11':(0x18007ef60,5),'LF3DRenderCoreAPI_D3D11':(0x180084de8,5)})
wrapper = inspect(root/'inputs/depth/LF3DRenderCoreForCSharp.dll', {})
result = {'native':native,'wrapper':wrapper}
(root/'evidence/depth/lf3d_pe_metadata.json').write_text(json.dumps(result,indent=2),encoding='utf-8')
print(json.dumps(result,indent=2))
