"""Read PE imports without loading target modules."""
import json
import struct
from pathlib import Path

def imports(path):
    b = path.read_bytes()
    pe = struct.unpack_from('<I', b, 60)[0]
    nsec = struct.unpack_from('<H', b, pe + 6)[0]
    optlen = struct.unpack_from('<H', b, pe + 20)[0]
    opt = pe + 24
    magic = struct.unpack_from('<H', b, opt)[0]
    is64 = magic == 523
    ddir = opt + (112 if is64 else 96)
    import_rva = struct.unpack_from('<I', b, ddir + 8)[0]
    sections = []
    for i in range(nsec):
        off = opt + optlen + i * 40
        vsize, va, rsize, raw = struct.unpack_from('<IIII', b, off + 8)
        sections.append((va, max(vsize, rsize), raw))
    def offset(rva):
        return next(raw + rva - va for va, size, raw in sections if va <= rva < va + size)
    def cstr(rva):
        off = offset(rva)
        return b[off:b.index(b'\0', off)].decode('ascii', 'replace')
    result = []
    off = offset(import_rva)
    while True:
        orig, stamp, chain, name, first = struct.unpack_from('<IIIII', b, off)
        if not name:
            break
        module = cstr(name)
        pos = offset(orig or first)
        names = []
        while True:
            value = struct.unpack_from('<Q' if is64 else '<I', b, pos)[0]
            if not value:
                break
            names.append(f'ordinal:{value & 65535}' if value >> (63 if is64 else 31) else cstr(value + 2))
            pos += 8 if is64 else 4
        result.append({'module': module, 'names': names})
        off += 20
    return {'file': str(path), 'imports': result}

out = [imports(Path('RE/inputs/depth') / name) for name in ('BOEInterleaving.dll', 'BOERenderWapper.dll')]
Path('RE/evidence/depth/managed_native_imports.json').write_text(json.dumps(out, ensure_ascii=False, indent=2), encoding='utf-8')
for item in out:
    print(item['file'])
    print(json.dumps([m for m in item['imports'] if any('BOE_' in n for n in m['names'])], indent=2))
