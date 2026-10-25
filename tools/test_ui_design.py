"""Guard exact design CSS reuse and transparent multi-size app icon geometry."""
from pathlib import Path
import struct, zlib
root=Path(__file__).resolve().parents[1]
original=(root/'design/titan-bridge-opus5.5.html').read_text(encoding='utf-8').split('<style>')[1].split('</style>')[0]
actual=(root/'src/app/ui/index.html.in').read_text(encoding='utf-8').split('<style>')[1].split('</style>')[0]
assert actual.startswith(original), 'The original design CSS changed'
html=(root/'src/app/ui/index.html.in').read_text(encoding='utf-8')
for forbidden in ('Math.random','simulate(','<div class="design-ctl">','PID 18244','演示：'):
    assert forbidden not in html,forbidden
ico=(root/'src/app/resources/LeiaSRBridge.ico').read_bytes()
assert struct.unpack_from('<HHH',ico)==(0,1,9)
for i in range(9):
    width,height,_,_,_,depth,size,offset=struct.unpack_from('<BBBBHHII',ico,6+i*16)
    assert depth==32
    png=ico[offset:offset+size];assert png.startswith(b'\x89PNG')
    pos=8;packed=b''
    while pos<len(png):
        n=struct.unpack_from('>I',png,pos)[0];kind=png[pos+4:pos+8];data=png[pos+8:pos+8+n];pos+=n+12
        if kind==b'IDAT':packed+=data
    pixels=zlib.decompress(packed);stride=(width or 256)*4+1
    assert pixels[4]==0 and pixels[stride-1]==0, 'Icon corners must be transparent'
    assert any(pixels[y*stride+1+x*4+3]==255 for y in range(height or 256) for x in range(width or 256))
print('UiDesign PASS: original stylesheet retained verbatim; prototype simulation removed; 9 RGBA icon sizes with transparent corners.')
