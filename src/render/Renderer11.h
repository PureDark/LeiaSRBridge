#pragma once
#include <d3d11_1.h>
#include <memory>
#include <string>
#include "../common/SharedProtocol.h"
namespace titan::render {
// Draw into the caller's RTV; preserve the immediate context state.
class Renderer11 final {
public:
    explicit Renderer11(ID3D11Device* device);
    ~Renderer11();
    bool Draw(ID3D11DeviceContext*, ID3D11ShaderResourceView*, const D3D11_VIEWPORT&,
        const Snapshot*, std::string& error, bool readSrgb=false, bool writeSrgb=false);
private:
    struct Impl;
    std::unique_ptr<Impl> impl_;
};
}
