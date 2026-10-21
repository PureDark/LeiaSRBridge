#include "render/Renderer.h"
#ifdef TITAN_LEGACY_TEST
#include "LegacyFixture.h"
#endif
#include "render/OpticalConstants.h"
#include <d3d12sdklayers.h>
#include <dxgi1_6.h>
#include <wrl/client.h>
#include <array>
#include <fstream>
#include <iostream>
#include <stdexcept>
#include <vector>

using Microsoft::WRL::ComPtr;
using namespace titan;
using namespace titan::render;
namespace {
void Check(HRESULT hr, const char* what) {
    if (FAILED(hr)) throw std::runtime_error(std::string(what) + " HRESULT " + std::to_string(hr));
}
void Require(bool condition, const char* what) { if (!condition) throw std::runtime_error(what); }
D3D12_RESOURCE_DESC TextureDesc(UINT width, UINT height, D3D12_RESOURCE_FLAGS flags) {
    D3D12_RESOURCE_DESC d{};
    d.Dimension = D3D12_RESOURCE_DIMENSION_TEXTURE2D;
    d.Width = width; d.Height = height; d.DepthOrArraySize = 1; d.MipLevels = 1;
    d.Format = DXGI_FORMAT_R8G8B8A8_UNORM; d.SampleDesc.Count = 1; d.Flags = flags;
    return d;
}
ComPtr<ID3D12Resource> Texture(ID3D12Device* device, UINT width, UINT height,
    D3D12_RESOURCE_FLAGS flags, D3D12_RESOURCE_STATES state) {
    D3D12_HEAP_PROPERTIES heap{}; heap.Type = D3D12_HEAP_TYPE_DEFAULT;
    auto d = TextureDesc(width, height, flags);
    D3D12_CLEAR_VALUE clear{}; clear.Format = d.Format;
    ComPtr<ID3D12Resource> resource;
    Check(device->CreateCommittedResource(&heap, D3D12_HEAP_FLAG_NONE, &d,
        state, (flags & D3D12_RESOURCE_FLAG_ALLOW_RENDER_TARGET) ? &clear : nullptr,
        IID_PPV_ARGS(&resource)), "Create texture");
    return resource;
}
ComPtr<ID3D12Resource> Buffer(ID3D12Device* device, UINT64 bytes, D3D12_HEAP_TYPE type) {
    D3D12_HEAP_PROPERTIES heap{}; heap.Type = type;
    D3D12_RESOURCE_DESC d{};
    d.Dimension = D3D12_RESOURCE_DIMENSION_BUFFER; d.Width = bytes; d.Height = 1;
    d.DepthOrArraySize = 1; d.MipLevels = 1; d.SampleDesc.Count = 1; d.Layout = D3D12_TEXTURE_LAYOUT_ROW_MAJOR;
    ComPtr<ID3D12Resource> r;
    Check(device->CreateCommittedResource(&heap, D3D12_HEAP_FLAG_NONE, &d,
        type == D3D12_HEAP_TYPE_UPLOAD ? D3D12_RESOURCE_STATE_GENERIC_READ : D3D12_RESOURCE_STATE_COPY_DEST,
        nullptr, IID_PPV_ARGS(&r)), "Create buffer");
    return r;
}
void Barrier(ID3D12GraphicsCommandList* list, ID3D12Resource* r,
    D3D12_RESOURCE_STATES before, D3D12_RESOURCE_STATES after) {
    D3D12_RESOURCE_BARRIER b{}; b.Type = D3D12_RESOURCE_BARRIER_TYPE_TRANSITION;
    b.Transition = {r, D3D12_RESOURCE_BARRIER_ALL_SUBRESOURCES, before, after};
    list->ResourceBarrier(1, &b);
}
struct Readback {
    ComPtr<ID3D12Resource> resource;
    D3D12_PLACED_SUBRESOURCE_FOOTPRINT footprint{};
};
Readback MakeReadback(ID3D12Device* device, ID3D12Resource* texture) {
    Readback r; auto desc = texture->GetDesc(); UINT64 size{};
    device->GetCopyableFootprints(&desc, 0, 1, 0, &r.footprint, nullptr, nullptr, &size);
    r.resource = Buffer(device, size, D3D12_HEAP_TYPE_READBACK);
    return r;
}
void CopyReadback(ID3D12GraphicsCommandList* list, ID3D12Resource* texture, const Readback& r) {
    D3D12_TEXTURE_COPY_LOCATION src{}, dst{};
    src.pResource = texture; src.Type = D3D12_TEXTURE_COPY_TYPE_SUBRESOURCE_INDEX;
    dst.pResource = r.resource.Get(); dst.Type = D3D12_TEXTURE_COPY_TYPE_PLACED_FOOTPRINT;
    dst.PlacedFootprint = r.footprint;
    list->CopyTextureRegion(&dst, 0, 0, 0, &src, nullptr);
}
Snapshot Calibration(float x, float y, float z, UINT mode) {
    Snapshot s{}; s.calibrationValid = s.eyePositionValid = 1;
    s.refractParams[0] = 0.345; s.refractParams[1] = 0.233;
    s.refractParams[2] = 0.8; s.refractParams[3] = 9.3;
    s.refractParams[4] = 0.017; s.refractParams[5] = 0.02;
    s.wholeMat[0] = 0.4; s.wholeMat[1] = -0.6; s.wholeMat[2] = 0.3;
    s.wholeMat[3] = 1.3; s.wholeMat[4] = -2.1; s.wholeMat[5] = 0.7;
    s.eye[0] = x; s.eye[1] = y; s.eye[2] = z; s.stereoMode = mode;
    return s;
}
} // namespace

int main(int argc, char** argv) {
    try {
        Require(argc == 2, "Pass shaders/BoeOriginalCS.dxbc as the single argument");
        std::ifstream file(argv[1], std::ios::binary);
        std::vector<char> original((std::istreambuf_iterator<char>(file)), {});
        Require(original.size() == 23656, "Missing or wrong original CS bytecode");
        // CPU matrix fixture exercises the recovered rotation order/translation.
        auto fixture = Calibration(2, 3, 400, 0);
        for (double& v : fixture.wholeMat) v = 0;
        fixture.wholeMat[2] = 90; fixture.wholeMat[3] = 10;
        fixture.wholeMat[4] = 20; fixture.wholeMat[5] = 30;
        OpticalConstants fc{};
        Require(MakeOpticalConstants(fixture,64,32,128,32,fc), "Matrix constants failed");
        Require(std::abs(fc.eyeX-7)<0.0001f && std::abs(fc.eyeY-22)<0.0001f &&
            std::abs(fc.eyeZ-430)<0.0001f, "Recovered wholeMat rotation failed");
        ComPtr<ID3D12Debug> debug;
        Check(D3D12GetDebugInterface(IID_PPV_ARGS(&debug)), "D3D12 debug layer is required for this test");
        debug->EnableDebugLayer();
        ComPtr<IDXGIFactory4> factory;
        Check(CreateDXGIFactory2(0,IID_PPV_ARGS(&factory)), "CreateDXGIFactory2");
        ComPtr<IDXGIAdapter> warp;
        Check(factory->EnumWarpAdapter(IID_PPV_ARGS(&warp)), "EnumWarpAdapter");
        ComPtr<ID3D12Device> device;
        Check(D3D12CreateDevice(warp.Get(),D3D_FEATURE_LEVEL_11_0,IID_PPV_ARGS(&device)), "Create WARP device");
        ComPtr<ID3D12InfoQueue> messages;
        Check(device.As(&messages), "D3D12InfoQueue");
        ComPtr<ID3D12CommandQueue> queue;
        D3D12_COMMAND_QUEUE_DESC q{};
        Check(device->CreateCommandQueue(&q,IID_PPV_ARGS(&queue)), "CreateCommandQueue");
        ComPtr<ID3D12CommandAllocator> allocator;
        Check(device->CreateCommandAllocator(D3D12_COMMAND_LIST_TYPE_DIRECT,IID_PPV_ARGS(&allocator)),"CreateAllocator");
        ComPtr<ID3D12GraphicsCommandList> list;
        Check(device->CreateCommandList(0,D3D12_COMMAND_LIST_TYPE_DIRECT,allocator.Get(),nullptr,IID_PPV_ARGS(&list)),"CreateList");
        constexpr UINT width=64, height=32, inputWidth=128, passes=6;
        auto input=Texture(device.Get(),inputWidth,height,D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS,D3D12_RESOURCE_STATE_COPY_DEST);
        auto inDesc=input->GetDesc(); D3D12_PLACED_SUBRESOURCE_FOOTPRINT inFoot{}; UINT64 uploadSize{};
        device->GetCopyableFootprints(&inDesc,0,1,0,&inFoot,nullptr,nullptr,&uploadSize);
        auto upload=Buffer(device.Get(),uploadSize,D3D12_HEAP_TYPE_UPLOAD);
        unsigned char* mapped{}; Check(upload->Map(0,nullptr,reinterpret_cast<void**>(&mapped)),"Map input");
        for (UINT y=0;y<height;++y) for (UINT x=0;x<inputWidth;++x) {
            auto* p=mapped+inFoot.Offset+y*inFoot.Footprint.RowPitch+x*4;
            p[0]=static_cast<unsigned char>((x*17+y*3)%251);
            p[1]=static_cast<unsigned char>((x*5+y*19+(x>=width?89:0))%253);
            p[2]=static_cast<unsigned char>((x*13+y*7+(x>=width?57:0))%255); p[3]=255;
        }
        upload->Unmap(0,nullptr);
        D3D12_TEXTURE_COPY_LOCATION uploadLocation{},inputLocation{};
        uploadLocation.pResource=upload.Get(); uploadLocation.Type=D3D12_TEXTURE_COPY_TYPE_PLACED_FOOTPRINT;
        uploadLocation.PlacedFootprint=inFoot;
        inputLocation.pResource=input.Get(); inputLocation.Type=D3D12_TEXTURE_COPY_TYPE_SUBRESOURCE_INDEX;
        list->CopyTextureRegion(&inputLocation,0,0,0,&uploadLocation,nullptr);
        Barrier(list.Get(),input.Get(),D3D12_RESOURCE_STATE_COPY_DEST,D3D12_RESOURCE_STATE_PIXEL_SHADER_RESOURCE);

        D3D12_DESCRIPTOR_RANGE ranges[2]{};
        ranges[0]={D3D12_DESCRIPTOR_RANGE_TYPE_SRV,1,0,0,0};
        ranges[1]={D3D12_DESCRIPTOR_RANGE_TYPE_UAV,1,0,0,1};
        D3D12_ROOT_PARAMETER params[2]{};
        params[0].ParameterType=D3D12_ROOT_PARAMETER_TYPE_32BIT_CONSTANTS;
        params[0].Constants={0,0,20};
        params[1].ParameterType=D3D12_ROOT_PARAMETER_TYPE_DESCRIPTOR_TABLE;
        params[1].DescriptorTable={2,ranges};
        D3D12_ROOT_SIGNATURE_DESC rootDesc{}; rootDesc.NumParameters=2; rootDesc.pParameters=params;
        ComPtr<ID3DBlob> blob,diagnostic;
        Check(D3D12SerializeRootSignature(&rootDesc,D3D_ROOT_SIGNATURE_VERSION_1,&blob,&diagnostic),"Serialize CS root");
        ComPtr<ID3D12RootSignature> csRoot;
        Check(device->CreateRootSignature(0,blob->GetBufferPointer(),blob->GetBufferSize(),IID_PPV_ARGS(&csRoot)),"Create CS root");
        D3D12_COMPUTE_PIPELINE_STATE_DESC csDesc{};
        csDesc.pRootSignature=csRoot.Get();csDesc.CS={original.data(),original.size()};
        ComPtr<ID3D12PipelineState> cs;
        Check(device->CreateComputePipelineState(&csDesc,IID_PPV_ARGS(&cs)),"Create original CS PSO");
        D3D12_DESCRIPTOR_HEAP_DESC heapDesc{};
        heapDesc.Type=D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV;heapDesc.NumDescriptors=2*passes;
        heapDesc.Flags=D3D12_DESCRIPTOR_HEAP_FLAG_SHADER_VISIBLE;
        ComPtr<ID3D12DescriptorHeap> csHeap;
        Check(device->CreateDescriptorHeap(&heapDesc,IID_PPV_ARGS(&csHeap)),"Create CS heap");
        heapDesc={};heapDesc.Type=D3D12_DESCRIPTOR_HEAP_TYPE_RTV;heapDesc.NumDescriptors=passes;
        ComPtr<ID3D12DescriptorHeap> rtvHeap;
        Check(device->CreateDescriptorHeap(&heapDesc,IID_PPV_ARGS(&rtvHeap)),"Create RTV heap");
        const auto srvStride=device->GetDescriptorHandleIncrementSize(D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV);
        const auto rtvStride=device->GetDescriptorHandleIncrementSize(D3D12_DESCRIPTOR_HEAP_TYPE_RTV);
        std::array<ComPtr<ID3D12Resource>,passes> expected,actual;
        std::array<Readback,passes> expectedRead,actualRead;
        std::array<Snapshot,passes> samples={Calibration(-53,32,600,0),Calibration(80,-20,720,0),
            Calibration(12,6,550,1),Calibration(1,2,250,0),Calibration(-5,-8,850,1),Calibration(0,0,600,0)};
#ifdef TITAN_LEGACY_TEST
        for(auto& sample:samples)sample=samples[0];
        LegacyFixture fixtureLegacy(samples[0]);
#endif
        {
            Renderer renderer(device.Get());
            for (UINT pass=0;pass<passes;++pass) {
                expected[pass]=Texture(device.Get(),width,height,D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS,D3D12_RESOURCE_STATE_UNORDERED_ACCESS);
                actual[pass]=Texture(device.Get(),width+16,height+12,D3D12_RESOURCE_FLAG_ALLOW_RENDER_TARGET,D3D12_RESOURCE_STATE_RENDER_TARGET);
                expectedRead[pass]=MakeReadback(device.Get(),expected[pass].Get());
                actualRead[pass]=MakeReadback(device.Get(),actual[pass].Get());
                D3D12_SHADER_RESOURCE_VIEW_DESC srv{};srv.Format=DXGI_FORMAT_R8G8B8A8_UNORM;
                srv.ViewDimension=D3D12_SRV_DIMENSION_TEXTURE2D;srv.Shader4ComponentMapping=D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING;srv.Texture2D.MipLevels=1;
                auto cpu=csHeap->GetCPUDescriptorHandleForHeapStart();cpu.ptr+=2*pass*srvStride;
                device->CreateShaderResourceView(input.Get(),&srv,cpu);cpu.ptr+=srvStride;
                D3D12_UNORDERED_ACCESS_VIEW_DESC uav{};uav.Format=DXGI_FORMAT_R8G8B8A8_UNORM;uav.ViewDimension=D3D12_UAV_DIMENSION_TEXTURE2D;
                device->CreateUnorderedAccessView(expected[pass].Get(),nullptr,&uav,cpu);
                auto gpu=csHeap->GetGPUDescriptorHandleForHeapStart();gpu.ptr+=2*pass*srvStride;
                OpticalConstants constants{};
                Require(MakeOpticalConstants(samples[pass],width,height,inputWidth,height,constants),"Optical constants");
                Barrier(list.Get(),input.Get(),D3D12_RESOURCE_STATE_PIXEL_SHADER_RESOURCE,D3D12_RESOURCE_STATE_NON_PIXEL_SHADER_RESOURCE);
                list->SetPipelineState(cs.Get());list->SetComputeRootSignature(csRoot.Get());
                ID3D12DescriptorHeap* heaps[]={csHeap.Get()};list->SetDescriptorHeaps(1,heaps);
                list->SetComputeRoot32BitConstants(0,20,&constants,0);list->SetComputeRootDescriptorTable(1,gpu);
                list->Dispatch(width/8,height/8,1);
                Barrier(list.Get(),expected[pass].Get(),D3D12_RESOURCE_STATE_UNORDERED_ACCESS,D3D12_RESOURCE_STATE_COPY_SOURCE);
                CopyReadback(list.Get(),expected[pass].Get(),expectedRead[pass]);
                Barrier(list.Get(),input.Get(),D3D12_RESOURCE_STATE_NON_PIXEL_SHADER_RESOURCE,D3D12_RESOURCE_STATE_PIXEL_SHADER_RESOURCE);
                auto rtv=rtvHeap->GetCPUDescriptorHandleForHeapStart();rtv.ptr+=pass*rtvStride;
                device->CreateRenderTargetView(actual[pass].Get(),nullptr,rtv);
                const float clear[]={0,0,0,0};list->ClearRenderTargetView(rtv,clear,0,nullptr);
                list->OMSetRenderTargets(1,&rtv,FALSE,nullptr);
                WeaveRequest request{};request.commandList=list.Get();request.inputSbs=input.Get();
                request.inputFormat=request.outputFormat=DXGI_FORMAT_R8G8B8A8_UNORM;
                request.viewport={8,6,static_cast<float>(width),static_cast<float>(height),0,1};
                request.scissor={10,8,static_cast<LONG>(width+6),static_cast<LONG>(height+4)};
#ifdef TITAN_COLOR_TEST
                request.readSrgb=pass==passes-1;
#endif
                std::string error;
#ifdef TITAN_LEGACY_TEST
                SR::PredictingDX12Weaver legacy(*fixtureLegacy.context,device.Get(),allocator.Get(),queue.Get(),input.Get(),actual[pass].Get(),nullptr);
                Require(legacy.canWeave(),"legacy canWeave");
                legacy.setLatency(40000);Require(legacy.getLatency()==40000,"legacy latency ABI");
                legacy.setCommandList(list.Get());
                Barrier(list.Get(),input.Get(),D3D12_RESOURCE_STATE_PIXEL_SHADER_RESOURCE,D3D12_RESOURCE_STATE_UNORDERED_ACCESS);
                legacy.weave(width,height,8,6);
                Barrier(list.Get(),input.Get(),D3D12_RESOURCE_STATE_UNORDERED_ACCESS,D3D12_RESOURCE_STATE_PIXEL_SHADER_RESOURCE);
#else
                if (!renderer.Record(request,pass==passes-1?nullptr:&samples[pass],error)) throw std::runtime_error(error);
#endif
                Barrier(list.Get(),actual[pass].Get(),D3D12_RESOURCE_STATE_RENDER_TARGET,D3D12_RESOURCE_STATE_COPY_SOURCE);
                CopyReadback(list.Get(),actual[pass].Get(),actualRead[pass]);
            }
        } // Destroy the weaver before submission: GPU state must remain alive.
        Check(list->Close(),"Close");ID3D12CommandList* lists[]={list.Get()};queue->ExecuteCommandLists(1,lists);
        ComPtr<ID3D12Fence> fence;Check(device->CreateFence(0,D3D12_FENCE_FLAG_NONE,IID_PPV_ARGS(&fence)),"CreateFence");
        Check(queue->Signal(fence.Get(),1),"Signal");HANDLE event=CreateEventW(nullptr,FALSE,FALSE,nullptr);
        Require(event!=nullptr,"CreateEvent");Check(fence->SetEventOnCompletion(1,event),"SetEvent");
        const DWORD wait=WaitForSingleObject(event,30000);CloseHandle(event);Require(wait==WAIT_OBJECT_0,"GPU wait timed out");
        Check(device->GetDeviceRemovedReason(),"Device removed");
        std::size_t mismatches=0, compared=0;unsigned maxDifference=0;
        for (UINT pass=0;pass<passes;++pass) {
            std::size_t passMismatches = 0;
            unsigned char *a{},*b{};Check(actualRead[pass].resource->Map(0,nullptr,reinterpret_cast<void**>(&a)),"Map actual");
            Check(expectedRead[pass].resource->Map(0,nullptr,reinterpret_cast<void**>(&b)),"Map expected");
            for (UINT y=0;y<height+12;++y) for (UINT x=0;x<width+16;++x) for (UINT c=0;c<4;++c) {
                unsigned expectedByte=0;
#ifdef TITAN_LEGACY_TEST
                if (x>=8&&x<width+8&&y>=6&&y<height+6) {
                    if (false) {
#else
                if (x>=10&&x<width+6&&y>=8&&y<height+4) {
                    if (pass==passes-1) {
#endif
                        UINT ix=x-8,iy=y-6;
                        expectedByte=c==0?(ix*17+iy*3)%251:c==1?(ix*5+iy*19)%253:c==2?(ix*13+iy*7)%255:255;
                    } else expectedByte=b[expectedRead[pass].footprint.Offset+(y-6)*expectedRead[pass].footprint.Footprint.RowPitch+(x-8)*4+c];
                }
#ifdef TITAN_COLOR_TEST
                if(pass==passes-1&&c<3){double value=expectedByte/255.0;value=value<=0.04045?value/12.92:std::pow((value+0.055)/1.055,2.4);expectedByte=static_cast<unsigned>(std::round(value*255));}
#endif
                unsigned actualByte=a[actualRead[pass].footprint.Offset+y*actualRead[pass].footprint.Footprint.RowPitch+x*4+c];
                const unsigned diff=actualByte>expectedByte?actualByte-expectedByte:expectedByte-actualByte;
#ifdef TITAN_COLOR_TEST
                if (diff > (pass==passes-1?1u:0u)) {
#else
                if (diff) {
#endif
                    if (passMismatches < 3) std::cout << "pass " << pass << " at " << x << ',' << y << ',' << c << ": expected " << expectedByte << ", actual " << actualByte << '\n';
                    ++passMismatches; ++mismatches;maxDifference=std::max(maxDifference,diff);
                }++compared;
            }
            actualRead[pass].resource->Unmap(0,nullptr);expectedRead[pass].resource->Unmap(0,nullptr);
            std::cout << "pass " << pass << " mismatches " << passMismatches << '\n';
        }
        unsigned debugErrors=0;
        for (UINT64 i=0;i<messages->GetNumStoredMessages();++i) {
            SIZE_T bytes{};messages->GetMessage(i,nullptr,&bytes);std::vector<unsigned char> storage(bytes);
            auto* message=reinterpret_cast<D3D12_MESSAGE*>(storage.data());messages->GetMessage(i,message,&bytes);
            if (message->Severity<=D3D12_MESSAGE_SEVERITY_WARNING) {++debugErrors;std::cerr<<message->pDescription<<'\n';}
        }
        std::cout<<"WARP original-CS/direct-PS comparison: "<<compared<<" channels, "<<mismatches
            <<" mismatches, max difference "<<maxDifference<<"; debug errors/warnings "<<debugErrors<<'\n';
        Require(mismatches==0,"Optical CS/PS or fallback/scissor mismatch");
        Require(debugErrors==0,"D3D12 debug validation failed");
        std::cout<<"PASS: wholeMat, LR/RL, near-eye branch, five optical passes, left-eye fallback, viewport/scissor, destroy-before-submit.\n";
        return 0;
    } catch (const std::exception& e) {std::cerr<<"FAIL: "<<e.what()<<'\n';return 1;}
}
