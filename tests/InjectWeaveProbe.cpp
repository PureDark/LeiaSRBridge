// Standalone diagnostic compiled from the same source tree as the injector.
#include "../src/app/Injector.cpp"
#include <iostream>
int wmain(int argc,wchar_t** argv){
 if(argc<3)return 2;DWORD pid=std::stoul(argv[1]);std::filesystem::path dll=std::filesystem::absolute(argv[2]);
 HANDLE process=OpenProcess(PROCESS_QUERY_INFORMATION|PROCESS_VM_READ|PROCESS_VM_WRITE|PROCESS_VM_OPERATION|PROCESS_CREATE_THREAD|SYNCHRONIZE,FALSE,pid);
 if(!process)return 3;std::wstring error;
 if(!titan::app::LoadRemote(process,pid,dll,false,error,nullptr)){std::wcerr<<error;CloseHandle(process);return 4;}
 HMODULE local=LoadLibraryExW(dll.c_str(),nullptr,DONT_RESOLVE_DLL_REFERENCES);
 const char* entry=argc>3?"TitanProbeControl":"TitanProbeInitialize";auto fn=GetProcAddress(local,entry);
 auto rva=reinterpret_cast<uintptr_t>(fn)-reinterpret_cast<uintptr_t>(local);FreeLibrary(local);if(!fn)return 5;
 uintptr_t remote{};for(const auto& m:titan::app::Modules(pid))if(titan::app::Fold(m.name)==titan::app::Fold(dll.filename().wstring()))remote=m.base;
 if(!remote)return 6;void* argument=argc>3?reinterpret_cast<void*>(std::stoull(argv[3])):nullptr;
 HANDLE thread=CreateRemoteThread(process,nullptr,0,reinterpret_cast<LPTHREAD_START_ROUTINE>(remote+rva),argument,0,nullptr);
 if(!thread)return 7;DWORD result{};const auto waited=WaitForSingleObject(thread,15000);if(waited==WAIT_OBJECT_0)GetExitCodeThread(thread,&result);
 CloseHandle(thread);CloseHandle(process);std::cout<<entry<<" result="<<result<<'\n';return result==1?0:8;
}
