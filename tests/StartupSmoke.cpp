#include "../src/app/Startup.h"
#include <iostream>
#include <stdexcept>
#include <vector>
#include <algorithm>
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
        const auto first=folder/L"Stereo3DBridge.exe",second=folder/L"Next version.exe";
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

        // Simulate an old installation, including a Windows-disabled approval marker.
        Require(RegOpenKeyExW(store.root,store.key.c_str(),0,KEY_ALL_ACCESS|KEY_WOW64_64KEY,&key)==ERROR_SUCCESS,"legacy fixture");
        const auto legacyCommand=L"\""+(folder/L"TitanLeiaBridge.exe").wstring()+L"\" --background";
        Require(RegSetValueExW(key,L"TitanLeiaBridge",0,REG_SZ,reinterpret_cast<const BYTE*>(legacyCommand.c_str()),
            static_cast<DWORD>((legacyCommand.size()+1)*sizeof(wchar_t)))==ERROR_SUCCESS,"legacy Run entry");
        RegCloseKey(key);
        const auto approvalKey=base+L"\\StartupApproved\\Run";
        Require(RegCreateKeyExW(store.root,approvalKey.c_str(),0,nullptr,0,KEY_ALL_ACCESS|KEY_WOW64_64KEY,nullptr,&key,nullptr)==ERROR_SUCCESS,"approval fixture");
        const BYTE disabled[12]={3,0,0,0,1,2,3,4,5,6,7,8};
        Require(RegSetValueExW(key,L"TitanLeiaBridge",0,REG_BINARY,disabled,sizeof(disabled))==ERROR_SUCCESS,"disabled marker");RegCloseKey(key);
        Require(ReadStartupStatus(first,store).enabled&&ReadStartupStatus(first,store).legacy,"recognize same-directory legacy entry");
        const auto elsewhere=folder/L"other"/L"Stereo3DBridge.exe";
        Require(MigrateLegacyStartup(elsewhere,error,store),"another directory is left alone");
        Require(ReadStartupStatus(first,store).command==legacyCommand,"do not relocate another installation");
        Require(MigrateLegacyStartup(first,error,store),"migrate old executable command");
        status=ReadStartupStatus(first,store);
        Require(status.enabled&&status.legacy&&status.command==L"\""+first.wstring()+L"\" --background","migrated command and legacy value identity");
        BYTE approval[12]{};size=sizeof(approval);
        Require(RegGetValueW(store.root,approvalKey.c_str(),L"TitanLeiaBridge",RRF_RT_REG_BINARY,nullptr,approval,&size)==ERROR_SUCCESS&&
            std::equal(std::begin(disabled),std::end(disabled),std::begin(approval)),"StartupApproved remains unchanged");
        Require(MigrateLegacyStartup(first,error,store),"idempotent migration");
        Require(ConfigureStartup(first,false,error,store)&&!ReadStartupStatus(first,store).registered,"disable legacy entry");
        Require(MigrateLegacyStartup(first,error,store)&&!ReadStartupStatus(first,store).registered,"absent startup remains absent");
        Require(ConfigureStartup(first,true,error,store)&&!ReadStartupStatus(first,store).legacy,"new installation uses new identity");
        Require(ConfigureStartup(first,false,error,store),"cleanup new startup");
        clean();std::cout<<"Startup PASS: isolated HKCU; quoted Unicode path; persistence; update/disable; no elevation; unrelated values preserved.\n";return 0;
    }catch(const std::exception& e){clean();std::cerr<<e.what()<<'\n';return 1;}
}
