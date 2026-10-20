#include "../src/app/GlobalRuntime.h"
#include <array>
#include <iostream>
#include <stdexcept>
#include <vector>

namespace fs = std::filesystem;
using namespace titan::app;
namespace {
void Require(bool value, const char* message) { if (!value) throw std::runtime_error(message); }
void Put(const std::wstring& keyPath, const wchar_t* name, const std::wstring& value, DWORD type = REG_SZ) {
    HKEY key{};
    Require(RegCreateKeyExW(HKEY_CURRENT_USER, keyPath.c_str(), 0, nullptr, 0,
        KEY_SET_VALUE | KEY_WOW64_64KEY, nullptr, &key, nullptr) == ERROR_SUCCESS, "create test key");
    const LONG result = RegSetValueExW(key, name, 0, type, reinterpret_cast<const BYTE*>(value.c_str()),
        static_cast<DWORD>((value.size() + 1) * sizeof(wchar_t)));
    RegCloseKey(key);
    Require(result == ERROR_SUCCESS, "set test value");
}
std::wstring Get(const std::wstring& keyPath, const wchar_t* name, DWORD* type = nullptr) {
    std::array<wchar_t, 32768> buffer{};
    DWORD bytes = static_cast<DWORD>(buffer.size() * sizeof(wchar_t));
    DWORD actualType{};
    Require(RegGetValueW(HKEY_CURRENT_USER, keyPath.c_str(), name,
        RRF_RT_REG_SZ | RRF_RT_REG_EXPAND_SZ | RRF_NOEXPAND | RRF_SUBKEY_WOW6464KEY,
        &actualType, buffer.data(), &bytes) == ERROR_SUCCESS, "get test value");
    if (type) *type = actualType;
    return buffer.data();
}
struct Fixture {
    std::wstring key = L"Software\\TitanLeiaBridgeTests\\GlobalRuntime-" + std::to_wstring(GetCurrentProcessId());
    fs::path folder = fs::temp_directory_path() / (L"TitanLeiaGlobalRuntime-" + std::to_wstring(GetCurrentProcessId()));
    GlobalRuntimeRegistryStore store{HKEY_CURRENT_USER, key + L"\\Environment", key + L"\\Owner", false};
    Fixture() {
        folder=fs::absolute(folder).lexically_normal();
        Require(fs::equivalent(folder.parent_path(),fs::temp_directory_path()) &&
            folder.filename()==(L"TitanLeiaGlobalRuntime-"+std::to_wstring(GetCurrentProcessId())),
            "unexpected temporary test path");
        Require(!fs::exists(folder), "test directory already exists"); fs::create_directories(folder);
    }
    ~Fixture() {
        RegDeleteTreeW(HKEY_CURRENT_USER, key.c_str());
        RegDeleteKeyExW(HKEY_CURRENT_USER, L"Software\\TitanLeiaBridgeTests", KEY_WOW64_64KEY, 0);
        std::error_code ignored;
        fs::remove_all(folder, ignored);
    }
    fs::path Copy(const fs::path& source, const wchar_t* name) {
        const fs::path target = folder / name;
        fs::create_directory(target);
        for (const wchar_t* file : {L"TitanLeiaRuntime.dll", L"SimulatedRealityCore.dll",
            L"SimulatedRealityDirectX.dll", L"SimulatedRealityDisplays.dll",
            L"SimulatedRealityFaceTrackers.dll", L"SimulatedRealityOpenGL.dll", L"opencv_world343.dll"})
            fs::copy_file(source / file, target / file);
        return target;
    }
};
}
int wmain(int argc, wchar_t** argv) {
    try {
        Require(argc == 2, "usage: GlobalRuntimeSmoke <built runtime directory>");
        Fixture fixture;
        const auto first = fixture.Copy(argv[1], L"runtime-one");
        const auto second = fixture.Copy(argv[1], L"runtime-two");
        std::wstring error;
        Require(ConfigureGlobalRuntime(first, false, error, fixture.store), "fresh disable succeeds");
        HKEY untouched{};
        Require(RegOpenKeyExW(HKEY_CURRENT_USER, fixture.store.environmentKey.c_str(), 0,
            KEY_READ | KEY_WOW64_64KEY, &untouched) == ERROR_FILE_NOT_FOUND,
            "fresh disable does not create environment or Path");
        Require(RegOpenKeyExW(HKEY_CURRENT_USER, fixture.store.ownershipKey.c_str(), 0,
            KEY_READ | KEY_WOW64_64KEY, &untouched) == ERROR_FILE_NOT_FOUND,
            "fresh disable does not create ownership");
        const std::wstring baseline = L"%SystemRoot%\\system32;C:\\Other;;\"C:\\Space Dir\";";
        Put(fixture.store.environmentKey, L"Path", baseline, REG_EXPAND_SZ);
        Require(ConfigureGlobalRuntime(first, true, error, fixture.store), "enable");
        DWORD type{};
        Require(Get(fixture.store.environmentKey, L"Path", &type) == first.wstring() + L";" + baseline &&
            type == REG_EXPAND_SZ, "preserve original expanded string bytes/type");
        Require(ReadGlobalRuntimeStatus(first, fixture.store).enabled, "enabled status");
        Require(ConfigureGlobalRuntime(first, true, error, fixture.store), "repeat enable");
        Require(Get(fixture.store.environmentKey, L"Path") == first.wstring() + L";" + baseline, "no duplicate");
        Require(ConfigureGlobalRuntime(second, true, error, fixture.store), "upgrade");
        Require(Get(fixture.store.environmentKey, L"Path") == second.wstring() + L";" + baseline, "upgrade removes only old");
        Put(fixture.store.environmentKey, L"Path",
            second.wstring() + L";" + baseline + L";C:\\AddedExternally", REG_EXPAND_SZ);
        Require(ConfigureGlobalRuntime(second, false, error, fixture.store), "disable");
        Require(Get(fixture.store.environmentKey, L"Path") == baseline + L";C:\\AddedExternally", "external additions survive");
        Require(!ReadGlobalRuntimeStatus(second, fixture.store).enabled, "disabled status");

        Put(fixture.store.environmentKey, L"Path", first.wstring() + L";" + baseline, REG_SZ);
        Require(!ConfigureGlobalRuntime(first, true, error, fixture.store), "reject preexisting unowned path");
        Require(ConfigureGlobalRuntime(first, false, error, fixture.store), "unowned disable");
        Require(Get(fixture.store.environmentKey, L"Path", &type) == first.wstring() + L";" + baseline &&
            type == REG_SZ, "unowned path retained with REG_SZ");

        // Simulate a crash after journaling an upgrade but before updating Path.
        Put(fixture.store.ownershipKey, L"ManagedPath", first.wstring());
        Put(fixture.store.ownershipKey, L"PendingPath", second.wstring());
        Require(ReadGlobalRuntimeStatus(first, fixture.store).pendingRecovery, "pending status");
        Require(ConfigureGlobalRuntime(second, true, error, fixture.store), "recover pre-write crash");
        Require(Get(fixture.store.environmentKey, L"Path", &type) == second.wstring() + L";" + baseline &&
            type == REG_SZ, "pending upgrade recovered");
        // Simulate a crash after the environment write, before committing the owner.
        Put(fixture.store.ownershipKey, L"ManagedPath", first.wstring());
        Put(fixture.store.ownershipKey, L"PendingPath", second.wstring());
        Require(ConfigureGlobalRuntime(second, true, error, fixture.store), "recover post-write crash");
        Require(Get(fixture.store.environmentKey, L"Path") == second.wstring() + L";" + baseline,
            "post-write recovery idempotent");
        // Simulate an uninstall journal. Recovery must not need the old DLLs.
        Put(fixture.store.ownershipKey, L"PendingPath", L"");
        Require(ConfigureGlobalRuntime(fixture.folder / L"missing", false, error, fixture.store), "recover uninstall");
        Require(Get(fixture.store.environmentKey, L"Path") == baseline, "pending uninstall restores path");
        Require(!ConfigureGlobalRuntime(fixture.folder / L"missing", true, error, fixture.store), "missing DLL rejected");
        Require(!ConfigureGlobalRuntime(fixture.folder / L"bad;path", true, error, fixture.store), "semicolon rejected");
        Require(Get(fixture.store.environmentKey, L"Path") == baseline, "invalid enable does not change path");
        std::cout << "Global runtime isolated registry smoke passed.\n";
        return 0;
    } catch (const std::exception& failure) {
        std::cerr << "Global runtime smoke failed: " << failure.what() << '\n';
        return 1;
    }
}
