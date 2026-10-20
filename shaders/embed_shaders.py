"""Embed already-compiled DXBC, so the deployed runtime needs no compiler/files."""
from pathlib import Path
p = Path(__file__).resolve().parent
names=['FullscreenVS','LeftEyePS','BoeInterlacePS']
out=['#pragma once','#include <cstddef>','namespace titan::render::shaders {']
for name in names:
    data=(p/(name+'.dxbc')).read_bytes()
    out.append('inline constexpr unsigned char '+name+'[] = {')
    for i in range(0,len(data),24):
        out.append(','.join(str(b) for b in data[i:i+24])+',')
    out.append('};')
out.append('}')
(p.parent/'src/render/ShaderBytecode.h').write_text('\n'.join(out)+'\n')
