#include "../src/app/Startup.h"
#include <iostream>
#include <stdexcept>
#include <vector>
using namespace titan::app;
void Require(bool ok,const char* what){if(!ok)throw std::runtime_error(what);}
int wmain(int argc,wchar_t** argv){
    if(argc==4&&std::wstring(argv[1])==L"--read"){
        const auto status=ReadStartupStatus(argv[3],{HKEY_CURRENT_USER,argv[2]});
        return status.enabled&&status.error.empty()?0:1;
    }
    const auto base=L"Software\\TitanLeiaBridgeTests\\Startup-"+std::to_wstring(GetCurrentProcessId());
    StartupRegistryStore store{HKEY_CURRENT_USER,base+L"\\Run"};
    wchar_t exe[32768]{};GetModuleFileNameW(nullptr,exe,32768);
    const auto folder=std::filesystem::temp_directory_path()/(L"Titan startup 测试 "+std::to_wstring(GetCurrentProcessId()));
    bool created=false;
    auto clean=[&]{RegDeleteTreeW(HKEY_CURRENT_USER,base.c_str());if(created)std::filesystem::remove_all(folder);};
    try{
        Require(!std::filesystem::exists(folder),"test folder already exists");
        std::filesystem::create_directory(folder);created=true;
        const auto first=folder/L"TitanLeiaBridge.exe",second=folder/L"Next version.exe";
        std::filesystem::copy_file(exe,first);std::filesystem::copy_file(exe,second);
        std::wstring error;
        Require(ConfigureStartup(first,false,error,store),"fresh disable");
        Require(!ReadStartupStatus(first,store).registered,"fresh state");
        HKEY key{};
        Require(RegCreateKeyExW(store.root,store.key.c_str(),0,nullptr,0,KEY_ALL_ACCESS|KEY_WOW64_64KEY,nullptr,&key,nullptr)==ERROR_SUCCESS,"fixture key");
        const wchar_t other[]=L"Unrelated startup";
        RegSetValueExW(key,L"OtherApp",0,REG_SZ,reinterpret_cast<const BYTE*>(other),sizeof(other));RegCloseKey(key);
        Require(ConfigureStartup(first,true,error,store),"enable");
        auto status=ReadStartupStatus(first,store);
        Require(status.enabled&&status.command==L"\""+first.wstring()+L"\" --background","quoted Unicode path and background flag");
        Require(status.command.find(L"runas")==std::wstring::npos,"ordinary user startup");
        std::wstring command=L"\""+std::wstring(exe)+L"\" --read \""+store.key+L"\" \""+first.wstring()+L"\"";
        STARTUPINFOW si{sizeof(si)};PROCESS_INFORMATION pi{};
        Require(CreateProcessW(exe,command.data(),nullptr,nullptr,FALSE,CREATE_NO_WINDOW,nullptr,nullptr,&si,&pi)!=0,"child read");
        CloseHandle(pi.hThread);Require(WaitForSingleObject(pi.hProcess,5000)==WAIT_OBJECT_0,"child completed");
        DWORD code{};GetExitCodeProcess(pi.hProcess,&code);CloseHandle(pi.hProcess);Require(code==0,"setting persists across processes");
        Require(ConfigureStartup(first,true,error,store),"idempotent enable");
        Require(ConfigureStartup(second,true,error,store),"update location");
        Require(ReadStartupStatus(second,store).enabled&&!ReadStartupStatus(first,store).enabled,"old location detected");
        Require(!ConfigureStartup(folder/L"missing.exe",true,error,store),"missing executable rejected");
        Require(ReadStartupStatus(second,store).enabled,"failed update preserves entry");
        Require(ConfigureStartup(second,false,error,store)&&!ReadStartupStatus(second,store).registered,"disable only own value");
        wchar_t value[64]{};DWORD size=sizeof(value);
        Require(RegGetValueW(store.root,store.key.c_str(),L"OtherApp",RRF_RT_REG_SZ,nullptr,value,&size)==ERROR_SUCCESS&&std::wstring(value)==other,"unrelated startup untouched");
        clean();std::cout<<"Startup PASS: isolated HKCU; quoted Unicode path; persistence; update/disable; no elevation; unrelated values preserved.\n";return 0;
    }catch(const std::exception& e){clean();std::cerr<<e.what()<<'\n';return 1;}
}
