#pragma once
#include <Windows.h>
#include <filesystem>
#include <string>

namespace titan::app {
struct StartupRegistryStore {
    HKEY root=HKEY_CURRENT_USER;
    std::wstring key=L"Software\\Microsoft\\Windows\\CurrentVersion\\Run";
};
struct StartupStatus {
    bool enabled{};
    bool registered{};
    std::wstring command;
    std::wstring error;
};
StartupStatus ReadStartupStatus(const std::filesystem::path& executable,const StartupRegistryStore& store={});
bool ConfigureStartup(const std::filesystem::path& executable,bool enable,std::wstring& error,const StartupRegistryStore& store={});
}
