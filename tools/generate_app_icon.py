"""Generate the transparent Opus 5.5 double-circle application icon (stdlib only)."""
from pathlib import Path
import struct, zlib
root=Path(__file__).resolve().parents[1]
# Icon: union of two radius-6 circles at centers (6,6),(12,6), exact multiply overlap.
# PNG compressed into ICO at standard Windows sizes. Supersample coverage on transparent canvas.
def png(size):
    pixels=bytearray()
    scale=size/24
    for y in range(size):
        pixels.append(0)
        for x in range(size):
            vals=[]
            for sy in range(8):
                for sx in range(8):
                    px=(x+(sx+.5)/8)/scale-3
                    py=(y+(sy+.5)/8)/scale-6
                    left=(px-6)**2+(py-6)**2<=36
                    right=(px-12)**2+(py-6)**2<=36
                    if left and right: vals.append(tuple(round(a*b/255) for a,b in zip((238,107,77),(26,163,155))))
                    elif left: vals.append((238,107,77))
                    elif right: vals.append((26,163,155))
            if vals:pixels.extend([*[round(sum(c[i] for c in vals)/len(vals)) for i in range(3)],round(255*len(vals)/64)])
            else:pixels.extend([0,0,0,0])
    def chunk(t,d):return struct.pack('>I',len(d))+t+d+struct.pack('>I',zlib.crc32(t+d)&0xffffffff)
    return b'\x89PNG\r\n\x1a\n'+chunk(b'IHDR',struct.pack('>IIBBBBB',size,size,8,6,0,0,0))+chunk(b'IDAT',zlib.compress(pixels))+chunk(b'IEND',b'')
sizes=[16,20,24,32,40,48,64,128,256]
images=[png(n) for n in sizes];offset=6+16*len(sizes)
ico=bytearray(struct.pack('<HHH',0,1,len(sizes)))
for size,data in zip(sizes,images):
    ico.extend(struct.pack('<BBBBHHII',size%256,size%256,0,0,1,32,len(data),offset));offset+=len(data)
ico.extend(b''.join(images))
(root/'src/app/resources/LeiaSRBridge.ico').write_bytes(ico)
(root/'src/app/resources/TitanBridge.png').write_bytes(images[-1])
(root/'src/app/resources/TitanBridge.svg').write_text('<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><defs><clipPath id="l"><circle cx="9" cy="12" r="6"/></clipPath></defs><circle cx="9" cy="12" r="6" fill="#EE6B4D"/><circle cx="15" cy="12" r="6" fill="#1AA39B"/><circle cx="15" cy="12" r="6" clip-path="url(#l)" fill="#18442F"/></svg>\n')

