#include "app/Injector.h"
#include <Windows.h>
#include <winternl.h>
#include <TlHelp32.h>
#include <MinHook.h>
#include <algorithm>
#include <cwctype>
#include <map>
#include <mutex>
#include <string>
#include <vector>
namespace {
std::filesystem::path directory;
using LdrLoad=NTSTATUS(NTAPI*)(PWSTR,PULONG,PUNICODE_STRING,PHANDLE);
LdrLoad realLoad{};
decltype(&CreateProcessW) realCreate{};
decltype(&CreateRemoteThread) realRemote{};
decltype(&RegOpenKeyExW) realOpen{};
decltype(&RegQueryValueExW) realQuery{};
decltype(&RegCloseKey) realClose{};
decltype(&DisplayConfigGetDeviceInfo) realDisplay{};
std::mutex keyMutex;
std::map<HKEY,bool> keys;
thread_local bool loading{},creating{},injecting{};
void Log(const std::wstring& message){
    auto path=directory.parent_path()/L"logs"/L"OdysseyHook.log";
    HANDLE file=CreateFileW(path.c_str(),FILE_APPEND_DATA,FILE_SHARE_READ|FILE_SHARE_WRITE,nullptr,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,nullptr);
    if(file==INVALID_HANDLE_VALUE)return;
    std::wstring line=std::to_wstring(GetCurrentProcessId())+L" "+message+L"\r\n";
    int length=WideCharToMultiByte(CP_UTF8,0,line.data(),static_cast<int>(line.size()),nullptr,0,nullptr,nullptr);
    std::string utf8(length,'\0');WideCharToMultiByte(CP_UTF8,0,line.data(),static_cast<int>(line.size()),utf8.data(),length,nullptr,nullptr);
    DWORD written{};WriteFile(file,utf8.data(),length,&written,nullptr);CloseHandle(file);
}
std::wstring Lower(std::wstring v){std::transform(v.begin(),v.end(),v.begin(),towlower);return v;}
NTSTATUS NTAPI Load(PWSTR search,PULONG flags,PUNICODE_STRING name,PHANDLE module){
    if(!loading&&name&&name->Buffer){
        std::wstring value(name->Buffer,name->Length/sizeof(wchar_t));auto base=Lower(std::filesystem::path(value).filename().wstring());
        const bool sr=base==L"simulatedrealitycore.dll"||base==L"simulatedrealitydirectx.dll"||base==L"simulatedrealitydisplays.dll"||base==L"simulatedrealityfacetrackers.dll"||base==L"simulatedrealityopengl.dll";
        if(sr||base==L"txbridge.dll"){
            const auto path=(directory/(sr?std::filesystem::path(base):std::filesystem::path(L"odyssey/txBridge.dll"))).wstring();
            UNICODE_STRING redirected{};redirected.Buffer=const_cast<PWSTR>(path.c_str());redirected.Length=static_cast<USHORT>(path.size()*2);redirected.MaximumLength=redirected.Length+2;
            loading=true;const auto status=realLoad(nullptr,flags,&redirected,module);loading=false;return status;
        }
    }
    return realLoad(search,flags,name,module);
}
LSTATUS WINAPI Open(HKEY key,LPCWSTR sub,DWORD options,REGSAM access,PHKEY out){
    if(key==HKEY_LOCAL_MACHINE&&sub&&Lower(sub)==L"software\\dimenco\\simulated reality"){
        // A real read-only handle provides normal RegistryKey lifetime semantics.
        auto status=realOpen(HKEY_CURRENT_USER,L"Software",0,KEY_READ,out);
        if(status==ERROR_SUCCESS){std::lock_guard lock(keyMutex);keys[*out]=true;}return status;
    }
    return realOpen(key,sub,options,access,out);
}
LSTATUS WINAPI Query(HKEY key,LPCWSTR name,LPDWORD reserved,LPDWORD type,LPBYTE data,LPDWORD bytes){
    bool ours{};{std::lock_guard lock(keyMutex);ours=keys.count(key)!=0;}
    if(ours&&(!name||!*name)){
        if(!bytes)return ERROR_INVALID_PARAMETER;auto value=directory.wstring();DWORD needed=static_cast<DWORD>((value.size()+1)*2);
        if(type)*type=REG_SZ;DWORD capacity=*bytes;*bytes=needed;if(!data)return ERROR_SUCCESS;if(capacity<needed)return ERROR_MORE_DATA;
        memcpy(data,value.c_str(),needed);return ERROR_SUCCESS;
    }
    return realQuery(key,name,reserved,type,data,bytes);
}
LSTATUS WINAPI Close(HKEY key){{std::lock_guard lock(keyMutex);keys.erase(key);}return realClose(key);}
LONG WINAPI Display(DISPLAYCONFIG_DEVICE_INFO_HEADER* request){
    const auto status=realDisplay(request);
    if(status!=ERROR_SUCCESS||!request||request->type!=DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME)return status;
    // Only rename the primary output, preserving real EDID, topology and modes.
    UINT32 paths{},modes{};
    if(GetDisplayConfigBufferSizes(QDC_ONLY_ACTIVE_PATHS,&paths,&modes)!=ERROR_SUCCESS)return status;
    std::vector<DISPLAYCONFIG_PATH_INFO> p(paths);std::vector<DISPLAYCONFIG_MODE_INFO> m(modes);
    if(QueryDisplayConfig(QDC_ONLY_ACTIVE_PATHS,&paths,p.data(),&modes,m.data(),nullptr)!=ERROR_SUCCESS)return status;
    MONITORINFOEXW primary{};primary.cbSize=sizeof(primary);GetMonitorInfoW(MonitorFromPoint({0,0},MONITOR_DEFAULTTOPRIMARY),&primary);
    for(UINT32 index=0;index<paths;++index){auto& path=p[index];
        if(path.targetInfo.id!=request->id||path.targetInfo.adapterId.LowPart!=request->adapterId.LowPart||path.targetInfo.adapterId.HighPart!=request->adapterId.HighPart)continue;
        DISPLAYCONFIG_SOURCE_DEVICE_NAME source{};source.header={DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME,sizeof(source),path.sourceInfo.adapterId,path.sourceInfo.id};
        if(realDisplay(&source.header)==ERROR_SUCCESS&&_wcsicmp(source.viewGdiDeviceName,primary.szDevice)==0){
            auto* target=reinterpret_cast<DISPLAYCONFIG_TARGET_DEVICE_NAME*>(request);wcscpy_s(target->monitorFriendlyDeviceName,L"Odyssey G90XF");break;
        }
    }
    return status;
}
BOOL WINAPI Create(LPCWSTR app,LPWSTR command,LPSECURITY_ATTRIBUTES processSecurity,LPSECURITY_ATTRIBUTES threadSecurity,
    BOOL inherit,DWORD flags,LPVOID environment,LPCWSTR cwd,LPSTARTUPINFOW startup,LPPROCESS_INFORMATION info){
    if(creating||(flags&(DEBUG_PROCESS|DEBUG_ONLY_THIS_PROCESS)))return realCreate(app,command,processSecurity,threadSecurity,inherit,flags,environment,cwd,startup,info);
    // Keep system helpers outside this process-local compatibility scope.
    std::wstring executable=app?app:L"";
    if(executable.empty()&&command){std::wstring c=command;if(c.front()==L'"'){auto end=c.find(L'"',1);if(end!=std::wstring::npos)executable=c.substr(1,end-1);}else executable=c.substr(0,c.find(L' '));}
    wchar_t system[32768]{};GetWindowsDirectoryW(system,32768);
    wchar_t resolved[32768]{};
    if(SearchPathW(nullptr,executable.c_str(),L".exe",32768,resolved,nullptr))executable=resolved;
    const auto folded=Lower(executable);const auto base=Lower(std::filesystem::path(executable).filename().wstring());
    if(folded.starts_with(Lower(std::wstring(system))+L"\\")||base==L"steam.exe"||base==L"steamwebhelper.exe"||base==L"unitycrashhandler64.exe")
        return realCreate(app,command,processSecurity,threadSecurity,inherit,flags,environment,cwd,startup,info);
    HANDLE image=CreateFileW(executable.c_str(),GENERIC_READ,FILE_SHARE_READ|FILE_SHARE_WRITE|FILE_SHARE_DELETE,nullptr,OPEN_EXISTING,FILE_ATTRIBUTE_NORMAL,nullptr);
    bool x64=false;
    if(image!=INVALID_HANDLE_VALUE){IMAGE_DOS_HEADER dos{};DWORD read{};
        if(ReadFile(image,&dos,sizeof(dos),&read,nullptr)&&read==sizeof(dos)&&dos.e_magic==IMAGE_DOS_SIGNATURE&&dos.e_lfanew>0){
            LARGE_INTEGER offset{};offset.QuadPart=dos.e_lfanew;IMAGE_NT_HEADERS64 pe{};
            if(SetFilePointerEx(image,offset,nullptr,FILE_BEGIN)&&ReadFile(image,&pe,sizeof(pe),&read,nullptr)&&read==sizeof(pe))x64=pe.Signature==IMAGE_NT_SIGNATURE&&pe.FileHeader.Machine==IMAGE_FILE_MACHINE_AMD64;
        }CloseHandle(image);
    }
    if(!x64)return realCreate(app,command,processSecurity,threadSecurity,inherit,flags,environment,cwd,startup,info);
    creating=true;
    const BOOL result=realCreate(app,command,processSecurity,threadSecurity,inherit,(flags&~CREATE_SUSPENDED)|DEBUG_ONLY_THIS_PROCESS,environment,cwd,startup,info);
    const DWORD originalError=GetLastError();creating=false;if(!result){SetLastError(originalError);return FALSE;}
    std::wstring error;
    const bool ok=titan::app::PrepareCreatedProcess(*info,directory,error,nullptr,true,(flags&CREATE_SUSPENDED)==0);
    if(!ok){Log(L"Child startup failed: "+executable+L" : "+error);CloseHandle(info->hProcess);CloseHandle(info->hThread);*info={};SetLastError(ERROR_DLL_INIT_FAILED);return FALSE;}
    Log(L"Child compatibility active: "+executable+L" PID="+std::to_wstring(info->dwProcessId));return TRUE;
}
bool IsRemoteLoader(DWORD pid,LPTHREAD_START_ROUTINE start,const char* name){
    auto local=GetProcAddress(GetModuleHandleW(L"kernel32.dll"),name);HMODULE owner{};
    if(!local||!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,reinterpret_cast<LPCWSTR>(local),&owner))return false;
    wchar_t path[32768]{};GetModuleFileNameW(owner,path,32768);auto filename=std::filesystem::path(path).filename();
    HANDLE snapshot=CreateToolhelp32Snapshot(TH32CS_SNAPMODULE|TH32CS_SNAPMODULE32,pid);if(snapshot==INVALID_HANDLE_VALUE)return false;
    MODULEENTRY32W entry{sizeof(entry)};bool matches=false;
    if(Module32FirstW(snapshot,&entry))do{if(_wcsicmp(filename.c_str(),entry.szModule)==0){
        auto address=reinterpret_cast<uintptr_t>(entry.modBaseAddr)+reinterpret_cast<uintptr_t>(local)-reinterpret_cast<uintptr_t>(owner);
        matches=address==reinterpret_cast<uintptr_t>(start);break;
    }}while(Module32NextW(snapshot,&entry));CloseHandle(snapshot);return matches;
}
HANDLE WINAPI Remote(HANDLE process,LPSECURITY_ATTRIBUTES security,SIZE_T stack,LPTHREAD_START_ROUTINE start,LPVOID argument,DWORD flags,LPDWORD id){
    const DWORD pid=GetProcessId(process);
    if(!injecting&&pid&&pid!=GetCurrentProcessId()&&argument){
        bool wide=IsRemoteLoader(pid,start,"LoadLibraryW");bool ansi=!wide&&IsRemoteLoader(pid,start,"LoadLibraryA");
        if(wide||ansi){
            // Read only the loader's bounded string argument. Restrict this path
            // to the two known Hub game modules, including Steam-owned games.
            std::wstring path;
            for(size_t i=0;i<32767;i++){
                wchar_t ch{};SIZE_T read{};const SIZE_T bytes=wide?2:1;
                if(!ReadProcessMemory(process,static_cast<const BYTE*>(argument)+i*bytes,&ch,bytes,&read)||read!=bytes){path.clear();break;}
                if(!ch)break;path.push_back(ch);
            }
            const auto base=Lower(std::filesystem::path(path).filename().wstring());
            if(base==L"odyssey3dsdk.dll"||base==L"uevrbackend.dll"){
                injecting=true;std::wstring error;bool ok=false;
                try{ok=titan::app::InjectRuntime(pid,directory,error,nullptr,true);}catch(...){error=L"exception preparing external game";}
                injecting=false;
                if(!ok){Log(L"External game compatibility failed PID="+std::to_wstring(pid)+L" : "+error);SetLastError(ERROR_DLL_INIT_FAILED);return nullptr;}
                Log(L"External game compatibility active PID="+std::to_wstring(pid)+L" before "+base);
            }
        }
    }
    return realRemote(process,security,stack,start,argument,flags,id);
}

bool Hook(const wchar_t* module,const char* name,void* replacement,void** original){
    auto library=GetModuleHandleW(module);if(!library)library=LoadLibraryW(module);
    auto target=library?GetProcAddress(library,name):nullptr;
    return target&&MH_CreateHook(target,replacement,original)==MH_OK;
}
}
extern "C" __declspec(dllexport) DWORD WINAPI TitanOdysseyInitialize(void*){
    static std::mutex initializationMutex;std::lock_guard initializationLock(initializationMutex);
    static bool initialized{};if(initialized)return 1;
    HMODULE self{};GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,reinterpret_cast<LPCWSTR>(&TitanOdysseyInitialize),&self);
    wchar_t path[32768]{};GetModuleFileNameW(self,path,32768);directory=std::filesystem::path(path).parent_path();
    if(MH_Initialize()!=MH_OK)return 0;
    const bool ok=Hook(L"ntdll.dll","LdrLoadDll",reinterpret_cast<void*>(&Load),reinterpret_cast<void**>(&realLoad))&&
        Hook(L"kernel32.dll","CreateProcessW",reinterpret_cast<void*>(&Create),reinterpret_cast<void**>(&realCreate))&&
        Hook(L"kernel32.dll","CreateRemoteThread",reinterpret_cast<void*>(&Remote),reinterpret_cast<void**>(&realRemote))&&
        Hook(L"advapi32.dll","RegOpenKeyExW",reinterpret_cast<void*>(&Open),reinterpret_cast<void**>(&realOpen))&&
        Hook(L"advapi32.dll","RegQueryValueExW",reinterpret_cast<void*>(&Query),reinterpret_cast<void**>(&realQuery))&&
        Hook(L"advapi32.dll","RegCloseKey",reinterpret_cast<void*>(&Close),reinterpret_cast<void**>(&realClose))&&
        Hook(L"user32.dll","DisplayConfigGetDeviceInfo",reinterpret_cast<void*>(&Display),reinterpret_cast<void**>(&realDisplay));
    if(!ok||MH_EnableHook(MH_ALL_HOOKS)!=MH_OK){MH_Uninitialize();Log(L"Hook initialization failed");return 0;}
    // Pin the module: hooks and registry handles remain valid until process exit.
    GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_PIN,reinterpret_cast<LPCWSTR>(&TitanOdysseyInitialize),&self);
    initialized=true;Log(L"Odyssey compatibility initialized");return 1;
}
