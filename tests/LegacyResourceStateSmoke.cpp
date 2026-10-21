#include <sr/management/srcontext.h>
#include <sr/weaver/dx12weaver.h>
#include <d3d12sdklayers.h>
#include <dxgi1_6.h>
#include <wrl/client.h>
#include <iostream>
#include <stdexcept>
#include <vector>
using Microsoft::WRL::ComPtr;
static void Check(HRESULT hr,const char* what){if(FAILED(hr))throw std::runtime_error(what);}
int main(){try{
    ComPtr<ID3D12Debug> debug;Check(D3D12GetDebugInterface(IID_PPV_ARGS(&debug)),"Debug layer unavailable");debug->EnableDebugLayer();
    ComPtr<IDXGIFactory4> factory;Check(CreateDXGIFactory2(0,IID_PPV_ARGS(&factory)),"Factory");
    ComPtr<IDXGIAdapter> adapter;Check(factory->EnumWarpAdapter(IID_PPV_ARGS(&adapter)),"WARP");
    ComPtr<ID3D12Device> device;Check(D3D12CreateDevice(adapter.Get(),D3D_FEATURE_LEVEL_11_0,IID_PPV_ARGS(&device)),"Device");
    ComPtr<ID3D12InfoQueue> messages;Check(device.As(&messages),"InfoQueue");
    auto* context=SR::SRContext::create(SR::SRContext::StandaloneMode);
    context->initialize();
    for(auto format:{DXGI_FORMAT_R8G8B8A8_UNORM,DXGI_FORMAT_R10G10B10A2_UNORM})
    for(auto inputState:{D3D12_RESOURCE_STATE_UNORDERED_ACCESS,D3D12_RESOURCE_STATE_RENDER_TARGET}){
        ComPtr<ID3D12CommandAllocator> allocator;Check(device->CreateCommandAllocator(D3D12_COMMAND_LIST_TYPE_DIRECT,IID_PPV_ARGS(&allocator)),"Allocator");
        ComPtr<ID3D12GraphicsCommandList> list;Check(device->CreateCommandList(0,D3D12_COMMAND_LIST_TYPE_DIRECT,allocator.Get(),nullptr,IID_PPV_ARGS(&list)),"List");
        D3D12_HEAP_PROPERTIES heap{};heap.Type=D3D12_HEAP_TYPE_DEFAULT;
        D3D12_RESOURCE_DESC desc{};desc.Dimension=D3D12_RESOURCE_DIMENSION_TEXTURE2D;desc.Width=128;desc.Height=32;
        desc.DepthOrArraySize=desc.MipLevels=1;desc.SampleDesc.Count=1;desc.Format=format;
        desc.Flags=inputState==D3D12_RESOURCE_STATE_RENDER_TARGET?D3D12_RESOURCE_FLAG_ALLOW_RENDER_TARGET:D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS;
        ComPtr<ID3D12Resource> input,output;
        Check(device->CreateCommittedResource(&heap,D3D12_HEAP_FLAG_NONE,&desc,inputState,nullptr,IID_PPV_ARGS(&input)),"Input");
        desc.Width=64;desc.Flags=D3D12_RESOURCE_FLAG_ALLOW_RENDER_TARGET;
        Check(device->CreateCommittedResource(&heap,D3D12_HEAP_FLAG_NONE,&desc,D3D12_RESOURCE_STATE_RENDER_TARGET,nullptr,IID_PPV_ARGS(&output)),"Output");
        ComPtr<ID3D12DescriptorHeap> rtvs;D3D12_DESCRIPTOR_HEAP_DESC hd{};hd.Type=D3D12_DESCRIPTOR_HEAP_TYPE_RTV;hd.NumDescriptors=1;
        Check(device->CreateDescriptorHeap(&hd,IID_PPV_ARGS(&rtvs)),"RTV heap");
        auto rtv=rtvs->GetCPUDescriptorHandleForHeapStart();device->CreateRenderTargetView(output.Get(),nullptr,rtv);list->OMSetRenderTargets(1,&rtv,FALSE,nullptr);
        {
            SR::PredictingDX12Weaver weaver(*context,device.Get(),allocator.Get(),nullptr,input.Get(),output.Get(),nullptr);
            weaver.setCommandList(list.Get());weaver.weave(64,32);
        }
        // Verify the adapter restored the host's input state. A wrong initial
        // state or restoration poisons Close(), as it did in Stellar Blade.
        D3D12_RESOURCE_BARRIER b{};b.Type=D3D12_RESOURCE_BARRIER_TYPE_TRANSITION;
        b.Transition={input.Get(),D3D12_RESOURCE_BARRIER_ALL_SUBRESOURCES,inputState,D3D12_RESOURCE_STATE_COPY_SOURCE};list->ResourceBarrier(1,&b);
        const auto closed=list->Close();
        bool error=false;
        for(UINT64 i=0;i<messages->GetNumStoredMessagesAllowedByRetrievalFilter();++i){
            SIZE_T n{};messages->GetMessage(i,nullptr,&n);std::vector<unsigned char> bytes(n);auto* m=reinterpret_cast<D3D12_MESSAGE*>(bytes.data());
            Check(messages->GetMessage(i,m,&n),"Read debug message");
            if(m->Severity<=D3D12_MESSAGE_SEVERITY_ERROR){error=true;std::cerr<<m->pDescription<<'\n';}
        }
        messages->ClearStoredMessages();Check(closed,"Close rejected legacy input barriers");if(error)throw std::runtime_error("D3D12 debug error");
        std::cout<<"PASS inputState="<<inputState<<" format="<<format<<'\n';
    }
    SR::SRContext::deleteSRContext(context);
    return 0;
}catch(const std::exception& e){std::cerr<<e.what()<<'\n';return 1;}}
