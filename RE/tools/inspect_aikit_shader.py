"""Extract a proven DXBC range and disassemble with Microsoft's D3DDisassemble.

Does not load or execute AiSetereoKit.dll.
"""
from pathlib import Path
import ctypes
import struct
import json

root = Path(__file__).resolve().parents[1]
data = (root / 'inputs' / 'AiSetereoKit.dll').read_bytes()
pe = struct.unpack_from('<I', data, 0x3c)[0]
count = struct.unpack_from('<H', data, pe + 6)[0]
opt_size = struct.unpack_from('<H', data, pe + 20)[0]
sections = pe + 24 + opt_size
base = struct.unpack_from('<Q', data, pe + 24 + 24)[0]
va = 0x18057D450
rva = va - base
for i in range(count):
    s = sections + i * 40
    virtual_size, section_rva, raw_size, raw_ptr = struct.unpack_from('<IIII', data, s + 8)
    if section_rva <= rva < section_rva + max(virtual_size, raw_size):
        raw = raw_ptr + rva - section_rva
        break
else:
    raise RuntimeError('shader VA is outside sections')
shader = data[raw:raw + 18440]
assert shader[:4] == b'DXBC'
assert struct.unpack_from('<I', shader, 24)[0] == len(shader)
compiler = ctypes.WinDLL('D3DCompiler_47.dll')
compiler.D3DDisassemble.argtypes = [ctypes.c_void_p, ctypes.c_size_t, ctypes.c_uint, ctypes.c_char_p, ctypes.POINTER(ctypes.c_void_p)]
compiler.D3DDisassemble.restype = ctypes.c_long
blob = ctypes.c_void_p()
source = ctypes.create_string_buffer(shader)
hr = compiler.D3DDisassemble(source, len(shader), 0, None, ctypes.byref(blob))
if hr < 0:
    raise RuntimeError(f'D3DDisassemble failed: {hr}')
vt = ctypes.cast(blob, ctypes.POINTER(ctypes.POINTER(ctypes.c_void_p)))[0]
ptr = ctypes.WINFUNCTYPE(ctypes.c_void_p, ctypes.c_void_p)(vt[3])(blob)
size = ctypes.WINFUNCTYPE(ctypes.c_size_t, ctypes.c_void_p)(vt[4])(blob)
assembly = ctypes.string_at(ptr, size).rstrip(b'\0').decode('utf-8', errors='replace')
ctypes.WINFUNCTYPE(ctypes.c_ulong, ctypes.c_void_p)(vt[2])(blob)
out = root / 'evidence' / 'aikit_stereo_pixel_shader.asm'
out.write_text(assembly, encoding='utf-8')
print(json.dumps({'source_va': hex(va), 'file_offset': hex(raw), 'bytes': len(shader), 'assembly': str(out)}))
