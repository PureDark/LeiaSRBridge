#include <Windows.h>
#include <fstream>
#include <filesystem>
int wmain(int argc,wchar_t**){
    if(argc>1){Sleep(30000);return 0;}
    wchar_t path[32768]{};GetModuleFileNameW(nullptr,path,32768);
    std::ofstream(std::filesystem::path(path).parent_path()/L"restart-ok.txt")<<"restarted";
    return 0;
}
