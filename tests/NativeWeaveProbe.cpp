// Explicitly injected, process-local diagnostic. Never loaded by the package.
#include <Windows.h>
#include <d3d12.h>
#include <delayimp.h>
#include <d3d12sdklayers.h>
#include <wrl/client.h>
#include <atomic>
#include <map>
#include <mutex>
#include <string>
#include <vector>
using Microsoft::WRL::ComPtr;
namespace {
using SetList=void(*)(void*,ID3D12GraphicsCommandList*);
using SetOutput=void(*)(void*,ID3D12Resource*);
using Weave=void(*)(void*,unsigned,unsigned);
SetList originalList{};SetOutput originalOutput{};Weave originalWeave{};
struct Inputs{ID3D12GraphicsCommandList* list{};ID3D12Resource* output{};};
std::mutex mutex;std::map<void*,Inputs> inputs;
std::atomic<unsigned> mode{1};
std::map<ID3D12Device*,ComPtr<ID3D12DescriptorHeap>> heaps;
void Log(const std::string& s){
 wchar_t path[256];swprintf_s(path,L"G:\\TitanOdyssey-Diagnostics-20261021\\weave-probe-%lu.log",GetCurrentProcessId());
 HANDLE f=CreateFileW(path,FILE_APPEND_DATA,FILE_SHARE_READ|FILE_SHARE_WRITE,nullptr,OPEN_ALWAYS,0,nullptr);
 if(f!=INVALID_HANDLE_VALUE){DWORD n;auto line=s+"\r\n";WriteFile(f,line.data(),static_cast<DWORD>(line.size()),&n,nullptr);CloseHandle(f);}
}
}
#include "PresentTrace.inl"
namespace {
void List(void* self,ID3D12GraphicsCommandList* list){originalList(self,list);std::lock_guard lock(mutex);inputs[self].list=list;}
void Output(void* self,ID3D12Resource* output){originalOutput(self,output);std::lock_guard lock(mutex);inputs[self].output=output;}
void Draw(void* self,unsigned w,unsigned h){
 Inputs in;{std::lock_guard lock(mutex);in=inputs[self];}
 const auto current=mode.load();
 if(!current||!in.list||!in.output){originalWeave(self,w,h);return;}
 ComPtr<ID3D12Device> device;if(FAILED(in.output->GetDevice(IID_PPV_ARGS(&device)))){originalWeave(self,w,h);return;}
 BeginPresentTrace(device.Get());lastOutput=reinterpret_cast<uintptr_t>(in.output);lastList=reinterpret_cast<uintptr_t>(in.list);++weaveSequence;
 ComPtr<ID3D12InfoQueue> messages;device.As(&messages);
 if(messages)messages->ClearStoredMessages();
 D3D12_CPU_DESCRIPTOR_HANDLE rtv{};
 {std::lock_guard lock(mutex);auto& heap=heaps[device.Get()];
  if(!heap){D3D12_DESCRIPTOR_HEAP_DESC d{};d.Type=D3D12_DESCRIPTOR_HEAP_TYPE_RTV;d.NumDescriptors=1;
   if(FAILED(device->CreateDescriptorHeap(&d,IID_PPV_ARGS(&heap)))){originalWeave(self,w,h);return;}}
  rtv=heap->GetCPUDescriptorHandleForHeapStart();device->CreateRenderTargetView(in.output,nullptr,rtv);
 }
 if(current==2)in.list->OMSetRenderTargets(1,&rtv,FALSE,nullptr);
 originalWeave(self,w,h);
 const float pink[]={1,0,1,1},cyan[]={0,1,1,1};
 D3D12_RECT a{64,64,192,192},b{224,64,352,192};
 in.list->ClearRenderTargetView(rtv,pink,1,&a);in.list->ClearRenderTargetView(rtv,cyan,1,&b);
 static std::atomic<bool> first{true};if(first.exchange(false))Log("Weave output probe active: magenta/cyan markers; mode="+std::to_string(current));
 if(messages){for(UINT64 i=0;i<messages->GetNumStoredMessagesAllowedByRetrievalFilter();++i){SIZE_T n{};messages->GetMessage(i,nullptr,&n);std::vector<unsigned char> bytes(n);auto* m=reinterpret_cast<D3D12_MESSAGE*>(bytes.data());
  if(SUCCEEDED(messages->GetMessage(i,m,&n))&&m->Severity<=D3D12_MESSAGE_SEVERITY_ERROR)Log(std::string("During weave/marker: ")+m->pDescription);}}
}
bool PatchTable(unsigned char* base,DWORD namesRva,DWORD tableRva,const char* name,void* replacement,void** original){
 if(!namesRva||!tableRva)return false;
 auto* names=reinterpret_cast<IMAGE_THUNK_DATA64*>(base+namesRva);auto* table=reinterpret_cast<IMAGE_THUNK_DATA64*>(base+tableRva);
 for(;names->u1.AddressOfData;++names,++table){if(IMAGE_SNAP_BY_ORDINAL64(names->u1.Ordinal))continue;
  auto* item=reinterpret_cast<IMAGE_IMPORT_BY_NAME*>(base+names->u1.AddressOfData);if(strcmp(reinterpret_cast<char*>(item->Name),name))continue;
  DWORD old;if(!VirtualProtect(&table->u1.Function,sizeof(void*),PAGE_READWRITE,&old))return false;
  *original=reinterpret_cast<void*>(table->u1.Function);
  InterlockedExchangePointer(reinterpret_cast<void* volatile*>(&table->u1.Function),replacement);
  DWORD ignored;VirtualProtect(&table->u1.Function,sizeof(void*),old,&ignored);return true;
 }return false;
}
bool Patch(const char* name,void* replacement,void** original){
 auto* base=reinterpret_cast<unsigned char*>(GetModuleHandleW(nullptr));auto* dos=reinterpret_cast<IMAGE_DOS_HEADER*>(base);auto* nt=reinterpret_cast<IMAGE_NT_HEADERS64*>(base+dos->e_lfanew);
 const auto normal=nt->OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_IMPORT].VirtualAddress;
 if(normal){auto* desc=reinterpret_cast<IMAGE_IMPORT_DESCRIPTOR*>(base+normal);
  for(;desc->Name;++desc)if(PatchTable(base,desc->OriginalFirstThunk,desc->FirstThunk,name,replacement,original))return true;}
 const auto delayed=nt->OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_DELAY_IMPORT].VirtualAddress;
 if(delayed){auto* desc=reinterpret_cast<ImgDelayDescr*>(base+delayed);
  for(;desc->rvaDLLName;++desc)if((desc->grAttrs&dlattrRva)&&PatchTable(base,desc->rvaINT,desc->rvaIAT,name,replacement,original))return true;}
 Log(std::string("Missing import: ")+name);return false;
}
}
extern "C" __declspec(dllexport) DWORD WINAPI TitanProbeControl(void* value){mode=static_cast<unsigned>(reinterpret_cast<uintptr_t>(value));Log("mode="+std::to_string(mode.load()));return 1;}
extern "C" __declspec(dllexport) DWORD WINAPI TitanProbeInitialize(void*){
 static bool initialized{};if(initialized)return 1;
 wchar_t exe[32768];GetModuleFileNameW(nullptr,exe,32768);if(!wcsstr(exe,L"SB-Win64-Shipping.exe"))return 0;
 HMODULE self;GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_PIN,reinterpret_cast<LPCWSTR>(&TitanProbeInitialize),&self);
 if(!Patch("?setCommandList@DX12WeaverBase@SR@@QEAAXPEAUID3D12GraphicsCommandList@@@Z",reinterpret_cast<void*>(&List),reinterpret_cast<void**>(&originalList)))return 0;
 if(!Patch("?setOutputFrameBuffer@DX12WeaverBase@SR@@QEAAXPEAUID3D12Resource@@@Z",reinterpret_cast<void*>(&Output),reinterpret_cast<void**>(&originalOutput)))return 0;
 if(!Patch("?weave@DX12WeaverBase@SR@@QEAAXII@Z",reinterpret_cast<void*>(&Draw),reinterpret_cast<void**>(&originalWeave)))return 0;
 initialized=true;Log("Probe initialized");return 1;
}
