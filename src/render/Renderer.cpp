#include "Renderer.h"
#include "OpticalConstants.h"
#include "ShaderBytecode.h"
#include <wrl/client.h>
#include <algorithm>
#include <atomic>
#include <climits>
#include <map>
#include <mutex>
#include <sstream>
#include <vector>

namespace titan::render {
using Microsoft::WRL::ComPtr;
namespace {
constexpr unsigned kDescriptorCapacity = 4096;
constexpr unsigned kDeviceCapacity = 8;
// Per-resource identity survives pointer reuse; no reference to historical
// input textures is retained by this cache.
constexpr GUID kInputIdentity = {0x8ec882d1,0xce03,0x4406,{0x81,0xa9,0xde,0xf4,0x27,0x5d,0x87,0x02}};
std::atomic<std::uint64_t> nextIdentity{1};

bool Error(std::string& error, const char* operation, HRESULT hr) {
    std::ostringstream s;
    s << operation << " failed (HRESULT 0x" << std::hex << static_cast<unsigned>(hr) << ')';
    error = s.str();
    return false;
}

struct DeviceState {
    ComPtr<ID3D12Device> device;
    ComPtr<ID3D12RootSignature> root;
    ComPtr<ID3D12DescriptorHeap> descriptors;
    std::map<std::pair<std::uint64_t, DXGI_FORMAT>, unsigned> inputSlots;
    std::map<std::pair<DXGI_FORMAT, bool>, ComPtr<ID3D12PipelineState>> pipelines;
    UINT descriptorStride{};
    unsigned nextSlot{};
    std::mutex mutex;

    bool Initialize(std::string& error) {
        D3D12_DESCRIPTOR_RANGE range{};
        range.RangeType = D3D12_DESCRIPTOR_RANGE_TYPE_SRV;
        range.NumDescriptors = 1;
        range.BaseShaderRegister = 0;
        D3D12_ROOT_PARAMETER parameters[3]{};
        parameters[0].ParameterType = D3D12_ROOT_PARAMETER_TYPE_32BIT_CONSTANTS;
        parameters[0].Constants = {0, 0, 20};
        parameters[0].ShaderVisibility = D3D12_SHADER_VISIBILITY_PIXEL;
        parameters[1].ParameterType = D3D12_ROOT_PARAMETER_TYPE_32BIT_CONSTANTS;
        parameters[1].Constants = {1, 0, 4};
        parameters[1].ShaderVisibility = D3D12_SHADER_VISIBILITY_PIXEL;
        parameters[2].ParameterType = D3D12_ROOT_PARAMETER_TYPE_DESCRIPTOR_TABLE;
        parameters[2].DescriptorTable = {1, &range};
        parameters[2].ShaderVisibility = D3D12_SHADER_VISIBILITY_PIXEL;
        D3D12_ROOT_SIGNATURE_DESC desc{};
        desc.NumParameters = 3;
        desc.pParameters = parameters;
        desc.Flags = D3D12_ROOT_SIGNATURE_FLAG_ALLOW_INPUT_ASSEMBLER_INPUT_LAYOUT;
        ComPtr<ID3DBlob> blob, diagnostic;
        HRESULT hr = D3D12SerializeRootSignature(&desc, D3D_ROOT_SIGNATURE_VERSION_1,
            &blob, &diagnostic);
        if (FAILED(hr)) return Error(error, "D3D12SerializeRootSignature", hr);
        hr = device->CreateRootSignature(0, blob->GetBufferPointer(), blob->GetBufferSize(),
            IID_PPV_ARGS(&root));
        if (FAILED(hr)) return Error(error, "CreateRootSignature", hr);
        D3D12_DESCRIPTOR_HEAP_DESC heap{};
        heap.Type = D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV;
        heap.NumDescriptors = kDescriptorCapacity;
        heap.Flags = D3D12_DESCRIPTOR_HEAP_FLAG_SHADER_VISIBLE;
        hr = device->CreateDescriptorHeap(&heap, IID_PPV_ARGS(&descriptors));
        if (FAILED(hr)) return Error(error, "CreateDescriptorHeap", hr);
        descriptorStride = device->GetDescriptorHandleIncrementSize(heap.Type);
        root->SetName(L"TitanLeiaBridge immutable root signature");
        descriptors->SetName(L"TitanLeiaBridge immutable input SRV heap");
        return true;
    }

    bool GetInput(ID3D12Resource* input, DXGI_FORMAT format,
        D3D12_GPU_DESCRIPTOR_HANDLE& handle, std::string& error) {
        std::uint64_t identity{};
        UINT size = sizeof(identity);
        if (FAILED(input->GetPrivateData(kInputIdentity, &size, &identity)) ||
            size != sizeof(identity) || !identity) {
            identity = nextIdentity.fetch_add(1);
            HRESULT hr = input->SetPrivateData(kInputIdentity, sizeof(identity), &identity);
            if (FAILED(hr)) return Error(error, "SetPrivateData(input identity)", hr);
        }
        auto key = std::make_pair(identity, format);
        auto found = inputSlots.find(key);
        unsigned slot{};
        if (found != inputSlots.end()) slot = found->second;
        else {
            if (nextSlot >= kDescriptorCapacity) {
                error = "Immutable input descriptor limit (4096) reached; restart the game";
                return false;
            }
            slot = nextSlot++;
            D3D12_SHADER_RESOURCE_VIEW_DESC srv{};
            srv.Format = format;
            srv.ViewDimension = D3D12_SRV_DIMENSION_TEXTURE2D;
            srv.Shader4ComponentMapping = D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING;
            srv.Texture2D.MipLevels = 1;
            auto cpu = descriptors->GetCPUDescriptorHandleForHeapStart();
            cpu.ptr += static_cast<SIZE_T>(slot) * descriptorStride;
            device->CreateShaderResourceView(input, &srv, cpu);
            inputSlots.emplace(key, slot);
        }
        handle = descriptors->GetGPUDescriptorHandleForHeapStart();
        handle.ptr += static_cast<UINT64>(slot) * descriptorStride;
        return true;
    }

    bool GetPipeline(DXGI_FORMAT format, bool stereo, ID3D12PipelineState*& pipeline,
        std::string& error) {
        const auto key = std::make_pair(format, stereo);
        const auto found = pipelines.find(key);
        if (found != pipelines.end()) { pipeline = found->second.Get(); return true; }
        D3D12_GRAPHICS_PIPELINE_STATE_DESC desc{};
        desc.pRootSignature = root.Get();
        desc.VS = {shaders::FullscreenVS, sizeof(shaders::FullscreenVS)};
        desc.PS = stereo ? D3D12_SHADER_BYTECODE{shaders::BoeInterlacePS, sizeof(shaders::BoeInterlacePS)} :
            D3D12_SHADER_BYTECODE{shaders::LeftEyePS, sizeof(shaders::LeftEyePS)};
        desc.BlendState.RenderTarget[0].RenderTargetWriteMask = D3D12_COLOR_WRITE_ENABLE_ALL;
        desc.BlendState.RenderTarget[0].SrcBlend = D3D12_BLEND_ONE;
        desc.BlendState.RenderTarget[0].DestBlend = D3D12_BLEND_ZERO;
        desc.BlendState.RenderTarget[0].BlendOp = D3D12_BLEND_OP_ADD;
        desc.BlendState.RenderTarget[0].SrcBlendAlpha = D3D12_BLEND_ONE;
        desc.BlendState.RenderTarget[0].DestBlendAlpha = D3D12_BLEND_ZERO;
        desc.BlendState.RenderTarget[0].BlendOpAlpha = D3D12_BLEND_OP_ADD;
        desc.BlendState.RenderTarget[0].LogicOp = D3D12_LOGIC_OP_NOOP;
        desc.SampleMask = UINT_MAX;
        desc.RasterizerState.FillMode = D3D12_FILL_MODE_SOLID;
        desc.RasterizerState.CullMode = D3D12_CULL_MODE_NONE;
        desc.RasterizerState.DepthClipEnable = TRUE;
        desc.DepthStencilState.DepthEnable = FALSE;
        desc.DepthStencilState.DepthFunc = D3D12_COMPARISON_FUNC_ALWAYS;
        desc.DepthStencilState.StencilEnable = FALSE;
        desc.PrimitiveTopologyType = D3D12_PRIMITIVE_TOPOLOGY_TYPE_TRIANGLE;
        desc.NumRenderTargets = 1;
        desc.RTVFormats[0] = format;
        desc.SampleDesc.Count = 1;
        ComPtr<ID3D12PipelineState> created;
        HRESULT hr = device->CreateGraphicsPipelineState(&desc, IID_PPV_ARGS(&created));
        if (FAILED(hr)) return Error(error, "CreateGraphicsPipelineState(interlace)", hr);
        created->SetName(stereo ? L"TitanLeiaBridge BOE optical PS" : L"TitanLeiaBridge left eye fallback PS");
        pipeline = created.Get();
        pipelines.emplace(key, std::move(created));
        return true;
    }
};

struct Registry {
    std::mutex mutex;
    std::vector<std::unique_ptr<DeviceState>> devices;
};
// Intentional process-lifetime allocation: SDK Delete may be called before a
// host queue has drained. No heap/PSO/root is retired by elapsed frame count.
Registry& GetRegistry() { static Registry* registry = new Registry; return *registry; }

DeviceState* GetDeviceState(ID3D12Device* device, std::string& error) {
    if (!device) { error = "Null D3D12 device"; return nullptr; }
    auto& registry = GetRegistry();
    std::lock_guard lock(registry.mutex);
    for (auto& state : registry.devices)
        if (state->device.Get() == device) return state.get();
    if (registry.devices.size() >= kDeviceCapacity) {
        error = "Immutable device cache limit (8) reached; restart the game";
        return nullptr;
    }
    auto state = std::make_unique<DeviceState>();
    state->device = device;
    if (!state->Initialize(error)) return nullptr;
    auto result = state.get();
    registry.devices.push_back(std::move(state));
    return result;
}

bool IsTypedColorFormat(DXGI_FORMAT format) {
    switch (format) {
    case DXGI_FORMAT_R8G8B8A8_UNORM:
    case DXGI_FORMAT_R8G8B8A8_UNORM_SRGB:
    case DXGI_FORMAT_B8G8R8A8_UNORM:
    case DXGI_FORMAT_B8G8R8A8_UNORM_SRGB:
    case DXGI_FORMAT_R10G10B10A2_UNORM:
    case DXGI_FORMAT_R16G16B16A16_FLOAT:
    case DXGI_FORMAT_R16G16B16A16_UNORM:
    case DXGI_FORMAT_R32G32B32A32_FLOAT:
    case DXGI_FORMAT_R11G11B10_FLOAT:
    case DXGI_FORMAT_R16G16_FLOAT:
    case DXGI_FORMAT_R32G32_FLOAT:
    case DXGI_FORMAT_R8_UNORM:
    case DXGI_FORMAT_R16_FLOAT:
    case DXGI_FORMAT_R32_FLOAT: return true;
    default: return false;
    }
}
bool CompatibleFormat(DXGI_FORMAT resource, DXGI_FORMAT view) {
    if (resource == view) return true;
    switch (resource) {
    case DXGI_FORMAT_R8G8B8A8_TYPELESS: return view == DXGI_FORMAT_R8G8B8A8_UNORM || view == DXGI_FORMAT_R8G8B8A8_UNORM_SRGB;
    case DXGI_FORMAT_B8G8R8A8_TYPELESS: return view == DXGI_FORMAT_B8G8R8A8_UNORM || view == DXGI_FORMAT_B8G8R8A8_UNORM_SRGB;
    case DXGI_FORMAT_R16G16B16A16_TYPELESS: return view == DXGI_FORMAT_R16G16B16A16_FLOAT || view == DXGI_FORMAT_R16G16B16A16_UNORM;
    case DXGI_FORMAT_R32G32B32A32_TYPELESS: return view == DXGI_FORMAT_R32G32B32A32_FLOAT;
    case DXGI_FORMAT_R10G10B10A2_TYPELESS: return view == DXGI_FORMAT_R10G10B10A2_UNORM;
    case DXGI_FORMAT_R16_TYPELESS: return view == DXGI_FORMAT_R16_FLOAT;
    case DXGI_FORMAT_R32_TYPELESS: return view == DXGI_FORMAT_R32_FLOAT;
    case DXGI_FORMAT_R8_TYPELESS: return view == DXGI_FORMAT_R8_UNORM;
    default: return false;
    }
}
} // namespace

struct Renderer::Impl {
    ComPtr<ID3D12Device> device;
    DeviceState* state{};
    explicit Impl(ID3D12Device* d) : device(d) {}
};
Renderer::Renderer(ID3D12Device* device) : impl_(std::make_unique<Impl>(device)) {}
Renderer::~Renderer() = default;

bool Renderer::Record(const WeaveRequest& request, const Snapshot* snapshot, std::string& error) {
    error.clear();
    if (!request.commandList || !request.inputSbs || !impl_->device) {
        error = "Missing command list, SBS texture, or D3D12 device"; return false;
    }
    if (request.commandList->GetType() != D3D12_COMMAND_LIST_TYPE_DIRECT) {
        error = "Weave requires a direct graphics command list"; return false;
    }
    const auto desc = request.inputSbs->GetDesc();
    if (desc.Dimension != D3D12_RESOURCE_DIMENSION_TEXTURE2D || desc.DepthOrArraySize != 1 ||
        desc.SampleDesc.Count != 1 || desc.Width < 2 || desc.Width > UINT_MAX || !desc.Height ||
        (desc.Flags & D3D12_RESOURCE_FLAG_DENY_SHADER_RESOURCE)) {
        error = "Input must be a non-MSAA single 2D SBS shader resource"; return false;
    }
    auto format = request.inputFormat == DXGI_FORMAT_UNKNOWN ? desc.Format : request.inputFormat;
    if (!IsTypedColorFormat(format) || !CompatibleFormat(desc.Format, format) ||
        !IsTypedColorFormat(request.outputFormat)) {
        error = "Unsupported input SRV or output RTV format"; return false;
    }
    const auto& vp = request.viewport;
    if (!std::isfinite(vp.Width) || !std::isfinite(vp.Height) || !std::isfinite(vp.TopLeftX) ||
        !std::isfinite(vp.TopLeftY) || vp.Width < 1 || vp.Height < 1 || vp.Width > 16384 ||
        vp.Height > 16384 || vp.Width != std::floor(vp.Width) || vp.Height != std::floor(vp.Height) ||
        request.scissor.right <= request.scissor.left || request.scissor.bottom <= request.scissor.top) {
        error = "Invalid weave viewport/scissor"; return false;
    }
    ComPtr<ID3D12Device> inputDevice, commandDevice;
    HRESULT hr = request.inputSbs->GetDevice(IID_PPV_ARGS(&inputDevice));
    if (FAILED(hr)) return Error(error, "GetDevice(input)", hr);
    hr = request.commandList->GetDevice(IID_PPV_ARGS(&commandDevice));
    if (FAILED(hr)) return Error(error, "GetDevice(command list)", hr);
    if (inputDevice.Get() != impl_->device.Get() || commandDevice.Get() != impl_->device.Get()) {
        error = "Input, command list and weaver belong to different D3D12 devices"; return false;
    }
    OpticalConstants constants{};
    if (snapshot) {
        if (!MakeOpticalConstants(*snapshot, static_cast<UINT>(vp.Width), static_cast<UINT>(vp.Height),
            static_cast<UINT>(desc.Width), desc.Height, constants)) {
            error = "Invalid optical calibration or eye position"; return false;
        }
    } else {
        constants.width = static_cast<UINT>(vp.Width); constants.height = static_cast<UINT>(vp.Height);
        constants.inputWidth = static_cast<UINT>(desc.Width); constants.inputHeight = desc.Height;
    }
    if (!impl_->state) impl_->state = GetDeviceState(impl_->device.Get(), error);
    if (!impl_->state) return false;
    auto& state = *impl_->state;
    std::lock_guard lock(state.mutex);
    ID3D12PipelineState* pipeline{};
    D3D12_GPU_DESCRIPTOR_HANDLE input{};
    if (!state.GetPipeline(request.outputFormat, snapshot != nullptr, pipeline, error) ||
        !state.GetInput(request.inputSbs, format, input, error)) return false;
    auto* list = request.commandList;
    list->SetPipelineState(pipeline);
    list->SetGraphicsRootSignature(state.root.Get());
    ID3D12DescriptorHeap* heaps[] = {state.descriptors.Get()};
    list->SetDescriptorHeaps(1, heaps);
    list->SetGraphicsRoot32BitConstants(0, 20, &constants, 0);
    const float origin[4] = {vp.TopLeftX, vp.TopLeftY, request.readSrgb ? 1.f : 0.f, request.writeSrgb ? 1.f : 0.f};
    list->SetGraphicsRoot32BitConstants(1, 4, origin, 0);
    list->SetGraphicsRootDescriptorTable(2, input);
    list->RSSetViewports(1, &vp);
    list->RSSetScissorRects(1, &request.scissor);
    list->IASetPrimitiveTopology(D3D_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
    list->DrawInstanced(3, 1, 0, 0);
    return true;
}
} // namespace titan::render
