#include "app/Injector.h"
#include <iostream>
int wmain(int argc,wchar_t** argv){if(argc!=4)return 2;SetEnvironmentVariableW(L"SteamAppId",L"3489700");SetEnvironmentVariableW(L"SteamGameId",L"3489700");std::wstring error;DWORD pid{};bool ok=titan::app::LaunchWithRuntime(argv[1],argv[3],argv[2],pid,error,nullptr,true);std::wcout<<(ok?L"Launched PID="+std::to_wstring(pid):error)<<L'\n';return ok?0:1;}
