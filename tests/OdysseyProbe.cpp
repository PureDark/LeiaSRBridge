#include <Windows.h>
#include <filesystem>
#include <string>
#include <iostream>
int wmain(int argc,wchar_t** argv){
    if(argc==2&&std::wstring(argv[1])==L"--external-wait"){
        for(unsigned i=0;i<1500;i++){if(GetModuleHandleW(L"TitanOdysseyHook.dll")&&GetModuleHandleW(L"UEVRBackend.dll"))return 0;Sleep(10);}return 30;
    }
    const bool live=argc==2&&(std::wstring(argv[1])==L"--live"||std::wstring(argv[1])==L"--live-child");
    auto hook=GetModuleHandleW(L"TitanOdysseyHook.dll");if(!hook)return 10;
    wchar_t module[32768]{};GetModuleFileNameW(hook,module,32768);auto runtime=std::filesystem::path(module).parent_path();
    HKEY key{};if(RegOpenKeyExW(HKEY_LOCAL_MACHINE,L"SOFTWARE\\Dimenco\\Simulated Reality",0,KEY_READ,&key)!=ERROR_SUCCESS)return 11;
    DWORD bytes{},type{};auto status=RegQueryValueExW(key,nullptr,nullptr,&type,nullptr,&bytes);if(status||type!=REG_SZ||bytes<4)return 12;
    wchar_t value[32768]{};DWORD shortSize=2;if(RegQueryValueExW(key,nullptr,nullptr,nullptr,reinterpret_cast<BYTE*>(value),&shortSize)!=ERROR_MORE_DATA)return 13;
    bytes=sizeof(value);status=RegQueryValueExW(key,L"",nullptr,&type,reinterpret_cast<BYTE*>(value),&bytes);RegCloseKey(key);
    if(status||runtime!=std::filesystem::path(value)||!std::filesystem::is_directory(runtime/L"bin"))return 14;
    // Native Unreal integration uses the ANSI registry API, including size
    // queries and a small fixed buffer. The default value has the same meaning.
    if(RegOpenKeyExA(HKEY_LOCAL_MACHINE,"SOFTWARE\\Dimenco\\Simulated Reality",0,KEY_READ,&key))return 50;
    bytes=0;if(RegQueryValueExA(key,nullptr,nullptr,&type,nullptr,&bytes)||type!=REG_SZ||bytes<2)return 51;
    char ansi[32768]{};DWORD ansiShort=1;
    if(RegQueryValueExA(key,"",nullptr,nullptr,reinterpret_cast<BYTE*>(ansi),&ansiShort)!=ERROR_MORE_DATA||ansiShort!=bytes)return 52;
    bytes=sizeof(ansi);status=RegQueryValueExA(key,"",nullptr,&type,reinterpret_cast<BYTE*>(ansi),&bytes);RegCloseKey(key);
    if(status||runtime!=std::filesystem::path(ansi))return 53;
    const wchar_t* preloads[]={L"DimencoWeaving",L"SimulatedRealityCameras",L"realsense2",L"glog",L"LeapC"};
    for(auto name:preloads){
        auto path=runtime/L"bin"/(std::wstring(name)+L".dll");
        auto module=LoadLibraryW(path.c_str());
        if(!module||!GetProcAddress(module,"TitanOdysseyPreloadIdentity"))return 54;
        FreeLibrary(module);
    }
    auto proxy=LoadLibraryW(L"C:\\Program Files\\Odyssey 3D Hub\\txBridge.dll");if(!proxy)return 15;
    GetModuleFileNameW(proxy,module,32768);if(std::filesystem::path(module)!=runtime/L"odyssey"/L"txBridge.dll")return 16;
    auto ready=reinterpret_cast<int(*)()>(GetProcAddress(proxy,"txBridge_getDeviceRespondingFlag"));if(!ready||ready()!=(live?1:0))return 17;
    auto core=LoadLibraryW(L"Z:\\NeverInstalled\\SimulatedRealityCore.dll");if(!core||!GetProcAddress(core,"newSRContext"))return 18;
    if(argc==4&&std::wstring(argv[1])==L"--inject"){
        HANDLE process=OpenProcess(PROCESS_ALL_ACCESS,FALSE,std::stoul(argv[2]));if(!process)return 40;
        const auto bytes=(wcslen(argv[3])+1)*sizeof(wchar_t);auto parameter=VirtualAllocEx(process,nullptr,bytes,MEM_COMMIT|MEM_RESERVE,PAGE_READWRITE);
        if(!parameter||!WriteProcessMemory(process,parameter,argv[3],bytes,nullptr))return 41;
        auto load=reinterpret_cast<LPTHREAD_START_ROUTINE>(GetProcAddress(GetModuleHandleW(L"kernel32.dll"),"LoadLibraryW"));
        auto thread=CreateRemoteThread(process,nullptr,0,load,parameter,0,nullptr);if(!thread)return 42;
        auto wait=WaitForSingleObject(thread,10000);DWORD code{};GetExitCodeThread(thread,&code);CloseHandle(thread);
        VirtualFreeEx(process,parameter,0,MEM_RELEASE);CloseHandle(process);if(wait!=WAIT_OBJECT_0||!code)return 43;
    }
    if(argc==1||(argc==2&&std::wstring(argv[1])==L"--live")){
        GetModuleFileNameW(nullptr,module,32768);std::wstring command=L"\""+std::wstring(module)+(live?L"\" --live-child":L"\" --child");
        STARTUPINFOW startup{sizeof(startup)};PROCESS_INFORMATION child{};
        if(!CreateProcessW(module,command.data(),nullptr,nullptr,FALSE,CREATE_SUSPENDED|CREATE_NO_WINDOW,nullptr,nullptr,&startup,&child))return 20;
        if(WaitForSingleObject(child.hProcess,100)!=WAIT_TIMEOUT)return 21;
        if(ResumeThread(child.hThread)!=1)return 22;
        DWORD wait=WaitForSingleObject(child.hProcess,10000),exit{};GetExitCodeProcess(child.hProcess,&exit);CloseHandle(child.hThread);CloseHandle(child.hProcess);
        if(wait!=WAIT_OBJECT_0||exit)return 23;
    }
    Sleep(500);return 0;
}
