// Included in the temporary native weave probe, outside its first namespace.
#include <dxgi1_4.h>
#include <MinHook.h>
#include <sstream>
namespace {
std::atomic<uintptr_t> lastOutput{},lastList{};
std::atomic<unsigned long long> weaveSequence{};
using PresentFn=HRESULT(STDMETHODCALLTYPE*)(IDXGISwapChain*,UINT,UINT);
using Present1Fn=HRESULT(STDMETHODCALLTYPE*)(IDXGISwapChain1*,UINT,UINT,const DXGI_PRESENT_PARAMETERS*);
using ExecuteFn=void(STDMETHODCALLTYPE*)(ID3D12CommandQueue*,UINT,ID3D12CommandList* const*);
PresentFn realPresent{};Present1Fn realPresent1{};ExecuteFn realExecute{};
void TracePresent(IDXGISwapChain* chain){
 static std::atomic<unsigned> count{};if(count.fetch_add(1)>=16)return;
 DXGI_SWAP_CHAIN_DESC desc{};chain->GetDesc(&desc);ComPtr<IDXGISwapChain3> chain3;chain->QueryInterface(IID_PPV_ARGS(&chain3));
 std::ostringstream s;s<<"Present chain="<<chain<<" hwnd="<<desc.OutputWindow<<" size="<<desc.BufferDesc.Width<<'x'<<desc.BufferDesc.Height
  <<" index="<<(chain3?chain3->GetCurrentBackBufferIndex():999)<<" weaveSeq="<<weaveSequence.load()<<" woven="<<reinterpret_cast<void*>(lastOutput.load());
 for(UINT i=0;i<desc.BufferCount;++i){ComPtr<ID3D12Resource> buffer;if(SUCCEEDED(chain->GetBuffer(i,IID_PPV_ARGS(&buffer))))s<<" buffer["<<i<<"]="<<buffer.Get();}
 Log(s.str());
}
HRESULT STDMETHODCALLTYPE Present(IDXGISwapChain* c,UINT a,UINT b){TracePresent(c);return realPresent(c,a,b);}
HRESULT STDMETHODCALLTYPE Present1(IDXGISwapChain1* c,UINT a,UINT b,const DXGI_PRESENT_PARAMETERS* p){TracePresent(c);return realPresent1(c,a,b,p);}
void STDMETHODCALLTYPE Execute(ID3D12CommandQueue* q,UINT n,ID3D12CommandList* const* lists){
 static std::atomic<unsigned> count{};if(count.fetch_add(1)<32){std::ostringstream s;s<<"Submit queue="<<q<<" wovenList="<<reinterpret_cast<void*>(lastList.load());for(UINT i=0;i<n;++i)s<<" list="<<lists[i];Log(s.str());}
 realExecute(q,n,lists);
}
void BeginPresentTrace(ID3D12Device* device){
 static std::once_flag once;std::call_once(once,[&]{
  wchar_t system[32768];GetSystemDirectoryW(system,32768);std::wstring path=std::wstring(system)+L"\\dxgi.dll";
  auto dll=LoadLibraryW(path.c_str());auto create=reinterpret_cast<decltype(&CreateDXGIFactory2)>(GetProcAddress(dll,"CreateDXGIFactory2"));
  ComPtr<IDXGIFactory2> factory;ComPtr<ID3D12CommandQueue> queue;ComPtr<IDXGISwapChain1> chain;
  D3D12_COMMAND_QUEUE_DESC q{};DXGI_SWAP_CHAIN_DESC1 d{};d.Width=d.Height=64;d.Format=DXGI_FORMAT_R8G8B8A8_UNORM;d.SampleDesc.Count=1;
  d.BufferUsage=DXGI_USAGE_RENDER_TARGET_OUTPUT;d.BufferCount=2;d.SwapEffect=DXGI_SWAP_EFFECT_FLIP_DISCARD;d.Scaling=DXGI_SCALING_STRETCH;d.AlphaMode=DXGI_ALPHA_MODE_IGNORE;
  if(!create||FAILED(create(0,IID_PPV_ARGS(&factory)))||FAILED(device->CreateCommandQueue(&q,IID_PPV_ARGS(&queue)))||FAILED(factory->CreateSwapChainForComposition(queue.Get(),&d,nullptr,&chain))){Log("Cannot construct presentation trace interfaces");return;}
  auto** sv=*reinterpret_cast<void***>(chain.Get());auto** qv=*reinterpret_cast<void***>(queue.Get());
  if(MH_Initialize()!=MH_OK){Log("Trace hook library initialization failed");return;}
  const auto a=MH_CreateHook(sv[8],reinterpret_cast<void*>(&Present),reinterpret_cast<void**>(&realPresent));
  const auto b=MH_CreateHook(sv[22],reinterpret_cast<void*>(&Present1),reinterpret_cast<void**>(&realPresent1));
  const auto c=MH_CreateHook(qv[10],reinterpret_cast<void*>(&Execute),reinterpret_cast<void**>(&realExecute));
  Log("Presentation hooks created: "+std::to_string(a)+","+std::to_string(b)+","+std::to_string(c));
  if(a==MH_OK)MH_EnableHook(sv[8]);if(b==MH_OK)MH_EnableHook(sv[22]);if(c==MH_OK)MH_EnableHook(qv[10]);
 });
}
}
