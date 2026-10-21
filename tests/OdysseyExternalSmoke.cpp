#include "app/Injector.h"
#include <iostream>
int wmain(int argc,wchar_t** argv){
    if(argc!=4)return 2;
    STARTUPINFOW startup{sizeof(startup)};PROCESS_INFORMATION external{};
    std::wstring command=L"\""+std::wstring(argv[1])+L"\" --external-wait";
    if(!CreateProcessW(argv[1],command.data(),nullptr,nullptr,FALSE,CREATE_NO_WINDOW,nullptr,nullptr,&startup,&external))return 3;
    std::wstring args=L"--inject "+std::to_wstring(external.dwProcessId)+L" \""+argv[3]+L"\"",error;DWORD pid{};
    bool launched=titan::app::LaunchWithRuntime(argv[1],args,argv[2],pid,error,nullptr,true);
    HANDLE proxy=launched?OpenProcess(SYNCHRONIZE|PROCESS_QUERY_INFORMATION,FALSE,pid):nullptr;
    DWORD code=99,externalCode=99;
    if(proxy){WaitForSingleObject(proxy,12000);GetExitCodeProcess(proxy,&code);CloseHandle(proxy);}
    WaitForSingleObject(external.hProcess,10000);GetExitCodeProcess(external.hProcess,&externalCode);
    if(externalCode==STILL_ACTIVE)TerminateProcess(external.hProcess,1);
    CloseHandle(external.hProcess);CloseHandle(external.hThread);
    std::wcerr<<L"external="<<externalCode<<L" injector="<<code<<L" "<<error<<L'\n';return launched&&code==0&&externalCode==0?0:1;
}
