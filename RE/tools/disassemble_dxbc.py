"""Disassemble a saved DXBC blob using the system compiler, without a GPU/device.

Input can also be a get_bytes MCP JSON response (one region per shader).
No analyzed executable or DLL is loaded.
"""
import argparse
import ctypes
import json
import struct
from pathlib import Path

p = argparse.ArgumentParser()
p.add_argument('input')
p.add_argument('output')
p.add_argument('--pe-va', type=int, help='Decimal VA, verified via IDA int_convert')
p.add_argument('--size', type=int)
a = p.parse_args()
src = Path(a.input)
shader = src.read_bytes()
if a.pe_va is not None:
    pe = struct.unpack_from('<I', shader, 0x3c)[0]
    count = struct.unpack_from('<H', shader, pe + 6)[0]
    opt_size = struct.unpack_from('<H', shader, pe + 20)[0]
    sections = pe + 24 + opt_size
    base = struct.unpack_from('<Q', shader, pe + 24 + 24)[0]
    rva = a.pe_va - base
    for i in range(count):
        s = sections + i * 40
        vsize, srva, rsize, raw = struct.unpack_from('<IIII', shader, s + 8)
        if srva <= rva < srva + max(vsize, rsize):
            offset = raw + rva - srva
            shader = shader[offset:offset + a.size]
            break
    else:
        raise RuntimeError('Shader VA outside sections')
elif src.suffix == '.json':
    result = json.loads(shader)['result']['structuredContent']['result']
    assert len(result) == 1
    data = result[0]['data']
    assert '...' not in data, 'Truncated MCP output cannot be used as shader bytes'
    shader = bytes(int(part, 16) for part in data.split())
assert shader[:4] == b'DXBC'
assert struct.unpack_from('<I', shader, 24)[0] == len(shader)
compiler = ctypes.WinDLL('D3DCompiler_47.dll')
compiler.D3DDisassemble.argtypes = [ctypes.c_void_p, ctypes.c_size_t,
    ctypes.c_uint, ctypes.c_char_p, ctypes.POINTER(ctypes.c_void_p)]
compiler.D3DDisassemble.restype = ctypes.c_long
blob = ctypes.c_void_p()
source = ctypes.create_string_buffer(shader)
hr = compiler.D3DDisassemble(source, len(shader), 0, None, ctypes.byref(blob))
if hr < 0:
    raise RuntimeError(f'D3DDisassemble failed: {hr}')
vt = ctypes.cast(blob, ctypes.POINTER(ctypes.POINTER(ctypes.c_void_p)))[0]
try:
    ptr = ctypes.WINFUNCTYPE(ctypes.c_void_p, ctypes.c_void_p)(vt[3])(blob)
    size = ctypes.WINFUNCTYPE(ctypes.c_size_t, ctypes.c_void_p)(vt[4])(blob)
    asm = ctypes.string_at(ptr, size).rstrip(b'\0').decode('utf-8', errors='replace')
    Path(a.output).write_text(asm, encoding='utf-8')
finally:
    ctypes.WINFUNCTYPE(ctypes.c_ulong, ctypes.c_void_p)(vt[2])(blob)
print(json.dumps({'bytes': len(shader), 'assembly': a.output}))
