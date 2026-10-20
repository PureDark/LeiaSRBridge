#pragma once
#include <d3d12.h>
#include <memory>
#include <string>
#include "../common/SharedProtocol.h"

namespace titan::render {
struct WeaveRequest {
    ID3D12GraphicsCommandList* commandList{};
    ID3D12Resource* inputSbs{};
    DXGI_FORMAT inputFormat{DXGI_FORMAT_UNKNOWN};
    DXGI_FORMAT outputFormat{DXGI_FORMAT_UNKNOWN};
    D3D12_VIEWPORT viewport{};
    D3D12_RECT scissor{};
    bool readSrgb{};
    bool writeSrgb{};
};

// Records into the host's already-bound render target. Does not bind RTVs,
// transition resources, execute command lists, or present. Input is a 2D SBS
// texture in PIXEL_SHADER_RESOURCE. The caller owns target state and submits.
// Immutable descriptors, PSOs and root signatures live in a bounded process
// cache. The host owns input texture lifetime through GPU completion, exactly
// as it owns every other resource read by its command list.
class Renderer final {
public:
    explicit Renderer(ID3D12Device* device);
    ~Renderer();
    Renderer(const Renderer&) = delete;
    Renderer& operator=(const Renderer&) = delete;
    bool Record(const WeaveRequest&, const Snapshot*, std::string& error);
private:
    struct Impl;
    std::unique_ptr<Impl> impl_;
};
} // namespace titan::render
