#pragma once
#include <Windows.h>
#include <filesystem>
#include <string>

namespace titan::app {
struct GlobalRuntimeStatus {
    bool enabled{};
    bool pendingRecovery{};
    std::filesystem::path managedDirectory;
    std::wstring error;
};
// Alternate registry locations are only used by the isolated smoke test.
struct GlobalRuntimeRegistryStore {
    HKEY root = HKEY_LOCAL_MACHINE;
    std::wstring environmentKey = L"SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment";
    // Persistent compatibility key: retain ownership of pre-rename PATH entries.
    std::wstring ownershipKey = L"Software\\TitanLeiaBridge\\RuntimeIntegration";
    bool broadcast = true;
};
GlobalRuntimeStatus ReadGlobalRuntimeStatus(const std::filesystem::path& runtimeDirectory,
    const GlobalRuntimeRegistryStore& store = {});
bool ConfigureGlobalRuntime(const std::filesystem::path& runtimeDirectory, bool enable,
    std::wstring& error, const GlobalRuntimeRegistryStore& store = {});
} // namespace titan::app
