#include "../src/app/Injector.h"
#include <iostream>
int wmain(int argc,wchar_t**argv) {
    if(argc!=3&&argc!=4)return 2;
    DWORD pid{};std::wstring error;
    if(!titan::app::LaunchWithRuntime(argv[1],L"",argv[2],pid,error,nullptr,argc==4)){std::wcerr<<error<<L'\n';return 1;}
    HANDLE process=OpenProcess(SYNCHRONIZE|PROCESS_QUERY_LIMITED_INFORMATION,FALSE,pid);
    if(!process){std::wcerr<<L"Probe exited before exit-code read\n";return 3;}
    const auto wait=WaitForSingleObject(process,10000);DWORD exit=99;GetExitCodeProcess(process,&exit);CloseHandle(process);
    std::cout<<"Loader probe: wait="<<wait<<" exit="<<exit<<'\n';return wait==WAIT_OBJECT_0&&exit==0?0:4;
}
