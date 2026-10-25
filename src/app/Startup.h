#pragma once
#include <Windows.h>
#include <filesystem>
#include <string>

namespace titan::app {
struct StartupRegistryStore {
    HKEY root=HKEY_CURRENT_USER;
    std::wstring key=L"Software\\Microsoft\\Windows\\CurrentVersion\\Run";
    std::wstring approvalKey=L"Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\StartupApproved\\Run";
};
struct StartupStatus {
    bool enabled{};
    bool registered{};
    bool legacy{};
    std::wstring valueName;
    std::wstring command;
    std::wstring error;
};
StartupStatus ReadStartupStatus(const std::filesystem::path& executable,const StartupRegistryStore& store={});
// Moves only a same-directory legacy entry to the current name, preserving StartupApproved state.
bool MigrateLegacyStartup(const std::filesystem::path& executable,std::wstring& error,const StartupRegistryStore& store={});
bool ConfigureStartup(const std::filesystem::path& executable,bool enable,std::wstring& error,const StartupRegistryStore& store={});
}
