#include "render/Renderer11.h"
#include "render/OpticalConstants.h"
#include <d3d11sdklayers.h>
#include <wrl/client.h>
#include <fstream>
#include <vector>
#include <iostream>
#include <stdexcept>
#include <cmath>
#include <algorithm>
using Microsoft::WRL::ComPtr;
void Check(HRESULT h){if(FAILED(h))throw std::runtime_error("DX11 call failed");}
void Require(bool b,const char* s){if(!b)throw std::runtime_error(s);}
int main(int argc,char** argv){try{
    Require(argc==2,"Pass original BOE CS");std::ifstream file(argv[1],std::ios::binary);std::vector<char> code((std::istreambuf_iterator<char>(file)),{});
    ComPtr<ID3D11Device> d;ComPtr<ID3D11DeviceContext> c;D3D_FEATURE_LEVEL level{};
    Check(D3D11CreateDevice(nullptr,D3D_DRIVER_TYPE_WARP,nullptr,D3D11_CREATE_DEVICE_DEBUG,nullptr,0,D3D11_SDK_VERSION,&d,&level,&c));
    ComPtr<ID3D11InfoQueue> info;Check(d.As(&info));ComPtr<ID3D11ComputeShader> cs;Check(d->CreateComputeShader(code.data(),code.size(),nullptr,&cs));
    constexpr UINT w=64,h=32;std::vector<unsigned char> pixels(2*w*h*4);
    for(UINT y=0;y<h;y++)for(UINT x=0;x<2*w;x++){auto* p=&pixels[(y*2*w+x)*4];p[0]=(x*17+y*3)%251;p[1]=(x*5+y*19+(x>=w?89:0))%253;p[2]=(x*13+y*7+(x>=w?57:0))%255;p[3]=255;}
    D3D11_TEXTURE2D_DESC desc{};desc.Width=2*w;desc.Height=h;desc.MipLevels=desc.ArraySize=desc.SampleDesc.Count=1;desc.Format=DXGI_FORMAT_R8G8B8A8_UNORM;desc.BindFlags=D3D11_BIND_SHADER_RESOURCE;
    D3D11_SUBRESOURCE_DATA data{pixels.data(),2*w*4,0};ComPtr<ID3D11Texture2D> input,expected,actual,readA,readB;
    Check(d->CreateTexture2D(&desc,&data,&input));desc.Width=w;desc.BindFlags=D3D11_BIND_UNORDERED_ACCESS;Check(d->CreateTexture2D(&desc,nullptr,&expected));
    desc.BindFlags=D3D11_BIND_RENDER_TARGET;Check(d->CreateTexture2D(&desc,nullptr,&actual));desc.BindFlags=0;desc.Usage=D3D11_USAGE_STAGING;desc.CPUAccessFlags=D3D11_CPU_ACCESS_READ;Check(d->CreateTexture2D(&desc,nullptr,&readA));Check(d->CreateTexture2D(&desc,nullptr,&readB));
    ComPtr<ID3D11ShaderResourceView> srv;ComPtr<ID3D11UnorderedAccessView> uav;ComPtr<ID3D11RenderTargetView> rtv;
    Check(d->CreateShaderResourceView(input.Get(),nullptr,&srv));Check(d->CreateUnorderedAccessView(expected.Get(),nullptr,&uav));Check(d->CreateRenderTargetView(actual.Get(),nullptr,&rtv));
    D3D11_BUFFER_DESC bd{};bd.ByteWidth=sizeof(titan::render::OpticalConstants);bd.BindFlags=D3D11_BIND_CONSTANT_BUFFER;ComPtr<ID3D11Buffer> cb;Check(d->CreateBuffer(&bd,nullptr,&cb));
    titan::render::Renderer11 renderer(d.Get());unsigned differences=0;
    for(int pass=0;pass<7;pass++){
        titan::Snapshot s{};s.calibrationValid=s.eyePositionValid=1;s.refractParams[0]=0.345;s.refractParams[1]=0.233;s.refractParams[2]=0.8;s.refractParams[3]=9.3;s.refractParams[4]=0.017;s.refractParams[5]=0.02;s.eye[0]=pass==0?-53.f:80.f;s.eye[1]=32;s.eye[2]=pass==2?250.f:600.f;s.stereoMode=pass%2;
        titan::render::OpticalConstants optical{};Require(titan::render::MakeOpticalConstants(s,w,h,2*w,h,optical),"constants");c->UpdateSubresource(cb.Get(),0,nullptr,&optical,0,0);
        auto* buffer=cb.Get();auto* view=srv.Get();auto* output=uav.Get();c->CSSetShader(cs.Get(),nullptr,0);c->CSSetConstantBuffers(0,1,&buffer);c->CSSetShaderResources(0,1,&view);c->CSSetUnorderedAccessViews(0,1,&output,nullptr);c->Dispatch(w/8,h/8,1);
        ID3D11UnorderedAccessView* none{};c->CSSetUnorderedAccessViews(0,1,&none,nullptr);
        auto* target=rtv.Get();c->OMSetRenderTargets(1,&target,nullptr);c->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_LINELIST);
        D3D11_VIEWPORT viewport{0,0,float(w),float(h),0,1};std::string error;Require(renderer.Draw(c.Get(),srv.Get(),viewport,pass>=3?nullptr:&s,error,pass>=3&&((pass-3)&1),pass>=3&&((pass-3)&2)),error.c_str());
        D3D11_PRIMITIVE_TOPOLOGY topology{};c->IAGetPrimitiveTopology(&topology);Require(topology==D3D11_PRIMITIVE_TOPOLOGY_LINELIST,"host topology lost");
        ComPtr<ID3D11ComputeShader> saved;c->CSGetShader(&saved,nullptr,nullptr);Require(saved.Get()==cs.Get(),"host compute shader lost");
        c->CopyResource(readA.Get(),actual.Get());c->CopyResource(readB.Get(),expected.Get());D3D11_MAPPED_SUBRESOURCE a{},b{};Check(c->Map(readA.Get(),0,D3D11_MAP_READ,0,&a));Check(c->Map(readB.Get(),0,D3D11_MAP_READ,0,&b));
        for(UINT y=0;y<h;y++)for(UINT x=0;x<w;x++)for(UINT k=0;k<4;k++){
            auto actualByte=static_cast<unsigned char*>(a.pData)[y*a.RowPitch+x*4+k];auto expectedByte=pass>=3?pixels[(y*2*w+x)*4+k]:static_cast<unsigned char*>(b.pData)[y*b.RowPitch+x*4+k];if(pass>=3&&k<3){double v=expectedByte/255.0;
                if((pass-3)&1)v=v<=0.04045?v/12.92:std::pow((v+0.055)/1.055,2.4);
                if((pass-3)&2)v=v<=0.0031308?v*12.92:1.055*std::pow(v,1.0/2.4)-0.055;
                expectedByte=static_cast<unsigned char>(std::clamp(std::round(v*255),0.0,255.0));
            }
            differences+=std::abs(int(actualByte)-int(expectedByte))>(pass>=4?1:0);
        }
        c->Unmap(readA.Get(),0);c->Unmap(readB.Get(),0);
    }
    unsigned errors=0;for(UINT64 i=0;i<info->GetNumStoredMessages();i++){SIZE_T bytes{};info->GetMessage(i,nullptr,&bytes);std::vector<char> storage(bytes);auto* message=reinterpret_cast<D3D11_MESSAGE*>(storage.data());info->GetMessage(i,message,&bytes);if(message->Severity<=D3D11_MESSAGE_SEVERITY_WARNING){++errors;std::cerr<<message->pDescription<<'\n';}}
    std::cout<<"DX11 WARP CS parity/fallback: differences="<<differences<<" debug warnings/errors="<<errors<<'\n';Require(!differences&&!errors,"DX11 verification failed");return 0;
}catch(const std::exception& e){std::cerr<<e.what()<<'\n';return 1;}}
