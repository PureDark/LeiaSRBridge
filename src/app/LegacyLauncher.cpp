// Compatibility entry for existing shortcuts and Run entries after an in-place upgrade.
#include <windows.h>
#include <filesystem>
#include <string>

int WINAPI wWinMain(HINSTANCE,HINSTANCE,PWSTR arguments,int show){
    wchar_t module[32768]{};
    const auto count=GetModuleFileNameW(nullptr,module,static_cast<DWORD>(std::size(module)));
    if(!count||count==std::size(module))return ERROR_BAD_PATHNAME;
    const auto directory=std::filesystem::path(module).parent_path();
    const auto executable=directory/L"Stereo3DBridge.exe";
    std::wstring command=L"\""+executable.wstring()+L"\"";
    if(arguments&&*arguments){command+=L" ";command+=arguments;}
    STARTUPINFOW startup{sizeof(startup)};startup.dwFlags=STARTF_USESHOWWINDOW;startup.wShowWindow=static_cast<WORD>(show);
    PROCESS_INFORMATION process{};
    if(!CreateProcessW(executable.c_str(),command.data(),nullptr,nullptr,FALSE,0,nullptr,directory.c_str(),&startup,&process)){
        const auto error=GetLastError();
        MessageBoxW(nullptr,L"无法启动 Stereo3DBridge.exe，请将新版完整解压到当前目录。",L"Stereo3DBridge",MB_ICONERROR);
        return static_cast<int>(error);
    }
    CloseHandle(process.hThread);
    // Preserve exit status for older automation and elevated configuration commands.
    WaitForSingleObject(process.hProcess,INFINITE);
    DWORD result=ERROR_GEN_FAILURE;GetExitCodeProcess(process.hProcess,&result);CloseHandle(process.hProcess);
    return static_cast<int>(result);
}
