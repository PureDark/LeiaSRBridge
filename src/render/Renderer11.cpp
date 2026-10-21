#include "Renderer11.h"
#include "OpticalConstants.h"
#include "ShaderBytecode.h"
#include <wrl/client.h>
#include <mutex>
#include <stdexcept>
namespace titan::render {
using Microsoft::WRL::ComPtr;
namespace { void Check11(HRESULT hr) { if(FAILED(hr))throw std::runtime_error("DX11 interlace resource creation failed"); } }
struct Renderer11::Impl {
    ComPtr<ID3D11Device1> device;
    ComPtr<ID3DDeviceContextState> cleanState;
    ComPtr<ID3D11VertexShader> vs;
    ComPtr<ID3D11PixelShader> stereo,mono;
    ComPtr<ID3D11Buffer> constants,origin;
    std::mutex mutex;
    explicit Impl(ID3D11Device* value) {
        if(!value)throw std::invalid_argument("Null DX11 device");
        Check11(value->QueryInterface(IID_PPV_ARGS(&device)));
        const D3D_FEATURE_LEVEL levels[]{D3D_FEATURE_LEVEL_11_1,D3D_FEATURE_LEVEL_11_0};
        D3D_FEATURE_LEVEL selected{};
        Check11(device->CreateDeviceContextState(0,levels,2,D3D11_SDK_VERSION,__uuidof(ID3D11Device),&selected,&cleanState));
        Check11(device->CreateVertexShader(shaders::FullscreenVS,sizeof(shaders::FullscreenVS),nullptr,&vs));
        Check11(device->CreatePixelShader(shaders::BoeInterlacePS,sizeof(shaders::BoeInterlacePS),nullptr,&stereo));
        Check11(device->CreatePixelShader(shaders::LeftEyePS,sizeof(shaders::LeftEyePS),nullptr,&mono));
        D3D11_BUFFER_DESC desc{};desc.Usage=D3D11_USAGE_DYNAMIC;desc.BindFlags=D3D11_BIND_CONSTANT_BUFFER;desc.CPUAccessFlags=D3D11_CPU_ACCESS_WRITE;
        desc.ByteWidth=sizeof(OpticalConstants);Check11(device->CreateBuffer(&desc,nullptr,&constants));
        desc.ByteWidth=16;Check11(device->CreateBuffer(&desc,nullptr,&origin));
    }
};
Renderer11::Renderer11(ID3D11Device* device):impl_(std::make_unique<Impl>(device)){}
Renderer11::~Renderer11()=default;
bool Renderer11::Draw(ID3D11DeviceContext* context,ID3D11ShaderResourceView* input,
    const D3D11_VIEWPORT& viewport,const Snapshot* snapshot,std::string& error,bool readSrgb,bool writeSrgb) {
    if(!context||!input||context->GetType()!=D3D11_DEVICE_CONTEXT_IMMEDIATE||viewport.Width<=0||viewport.Height<=0){error="DX11 weaving requires an immediate context, input and viewport";return false;}
    ComPtr<ID3D11Device> owner;context->GetDevice(&owner);
    if(owner.Get()!=impl_->device.Get()){error="DX11 context belongs to another device";return false;}
    std::lock_guard lock(impl_->mutex);
    ComPtr<ID3D11DeviceContext1> ctx; if(FAILED(context->QueryInterface(IID_PPV_ARGS(&ctx)))){error="DX11.1 context unavailable";return false;}
    ComPtr<ID3D11Resource> resource;input->GetResource(&resource);
    ComPtr<ID3D11Texture2D> texture;
    if(FAILED(resource.As(&texture))){error="DX11 input must be a 2D SBS texture";return false;}
    D3D11_TEXTURE2D_DESC desc{};texture->GetDesc(&desc);
    D3D11_SHADER_RESOURCE_VIEW_DESC view{};input->GetDesc(&view);
    if(view.ViewDimension!=D3D11_SRV_DIMENSION_TEXTURE2D||view.Texture2D.MostDetailedMip||desc.SampleDesc.Count!=1||desc.ArraySize!=1||desc.Width%2){error="Unsupported DX11 SBS view";return false;}
    ComPtr<ID3D11RenderTargetView> target;context->OMGetRenderTargets(1,&target,nullptr);
    if(!target){error="DX11 output RTV is not bound";return false;}
    OpticalConstants constants{};
    if(snapshot){if(!MakeOpticalConstants(*snapshot,static_cast<UINT>(viewport.Width),static_cast<UINT>(viewport.Height),desc.Width,desc.Height,constants)){error="Invalid DX11 optical calibration";return false;}}
    else {constants.width=static_cast<UINT>(viewport.Width);constants.height=static_cast<UINT>(viewport.Height);constants.inputWidth=desc.Width;constants.inputHeight=desc.Height;}
    struct Restore {ID3D11DeviceContext1* ctx;ComPtr<ID3DDeviceContextState> saved;~Restore(){ctx->SwapDeviceContextState(saved.Get(),nullptr);}} restore{ctx.Get(),{}};
    ctx->SwapDeviceContextState(impl_->cleanState.Get(),&restore.saved);
    auto upload=[&](ID3D11Buffer* buffer,const void* data,size_t size){D3D11_MAPPED_SUBRESOURCE mapped{};if(FAILED(ctx->Map(buffer,0,D3D11_MAP_WRITE_DISCARD,0,&mapped)))return false;memcpy(mapped.pData,data,size);ctx->Unmap(buffer,0);return true;};
    const float origin[]{viewport.TopLeftX,viewport.TopLeftY,readSrgb?1.f:0.f,writeSrgb?1.f:0.f};
    if(!upload(impl_->constants.Get(),&constants,sizeof(constants))||!upload(impl_->origin.Get(),origin,sizeof(origin))){error="DX11 constant upload failed";return false;}
    auto* rtv=target.Get();ctx->OMSetRenderTargets(1,&rtv,nullptr);
    ctx->VSSetShader(impl_->vs.Get(),nullptr,0);ctx->PSSetShader(snapshot?impl_->stereo.Get():impl_->mono.Get(),nullptr,0);
    ID3D11Buffer* buffers[]{impl_->constants.Get(),impl_->origin.Get()};ctx->PSSetConstantBuffers(0,2,buffers);
    ctx->PSSetShaderResources(0,1,&input);ctx->RSSetViewports(1,&viewport);ctx->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
    ctx->Draw(3,0);
    ID3D11ShaderResourceView* empty{};ctx->PSSetShaderResources(0,1,&empty);
    return true;
}
}
