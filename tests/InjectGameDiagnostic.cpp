#include "app/Injector.h"
#include <iostream>
int wmain(int argc,wchar_t** argv){if(argc!=3)return 2;std::wstring error;bool ok=titan::app::InjectRuntime(std::stoul(argv[1]),argv[2],error,nullptr,true);std::wcout<<(ok?L"Injected":error)<<L'\n';return ok?0:1;}
