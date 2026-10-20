#include "GlobalRuntime.h"
#include <algorithm>
#include <cstring>
#include <fstream>
#include <iterator>
#include <stdexcept>
#include <vector>

namespace titan::app {
namespace {
struct Key {
    HKEY value{};
    ~Key() { if (value) RegCloseKey(value); }
};
struct RegistryFailure { LONG code; };
void Check(LONG code) { if (code != ERROR_SUCCESS) throw RegistryFailure{code}; }
void Open(Key& key, const GlobalRuntimeRegistryStore& store, const std::wstring& path, bool write) {
    const REGSAM access = (write ? KEY_QUERY_VALUE | KEY_SET_VALUE : KEY_QUERY_VALUE) | KEY_WOW64_64KEY;
    if (write) Check(RegCreateKeyExW(store.root, path.c_str(), 0, nullptr, 0, access, nullptr, &key.value, nullptr));
    else {
        LONG result = RegOpenKeyExW(store.root, path.c_str(), 0, access, &key.value);
        if (result != ERROR_FILE_NOT_FOUND) Check(result);
    }
}
struct Value { std::wstring text; DWORD type = REG_EXPAND_SZ; bool exists{}; };
Value Read(HKEY key, const wchar_t* name) {
    Value value;
    if (!key) return value;
    DWORD bytes{};
    LONG result = RegQueryValueExW(key, name, nullptr, &value.type, nullptr, &bytes);
    if (result == ERROR_FILE_NOT_FOUND) return value;
    Check(result);
    if ((value.type != REG_SZ && value.type != REG_EXPAND_SZ) || bytes % sizeof(wchar_t))
        throw std::runtime_error("Invalid registry string type");
    std::vector<wchar_t> buffer(bytes / sizeof(wchar_t) + 1);
    Check(RegQueryValueExW(key, name, nullptr, &value.type,
        reinterpret_cast<BYTE*>(buffer.data()), &bytes));
    size_t count = bytes / sizeof(wchar_t);
    if (count && buffer[count - 1] == L'\0') --count;
    if (std::find(buffer.begin(), buffer.begin() + count, L'\0') != buffer.begin() + count)
        throw std::runtime_error("Embedded null in registry value");
    value.text.assign(buffer.data(), count);
    value.exists = true;
    return value;
}
void Write(HKEY key, const wchar_t* name, const std::wstring& text, DWORD type = REG_SZ) {
    Check(RegSetValueExW(key, name, 0, type, reinterpret_cast<const BYTE*>(text.c_str()),
        static_cast<DWORD>((text.size() + 1) * sizeof(wchar_t))));
}
void Erase(HKEY key, const wchar_t* name) {
    LONG result = RegDeleteValueW(key, name);
    if (result != ERROR_FILE_NOT_FOUND) Check(result);
}
std::wstring Normalized(std::wstring value) {
    const auto first = value.find_first_not_of(L" \t");
    if (first == std::wstring::npos) return {};
    value = value.substr(first, value.find_last_not_of(L" \t") - first + 1);
    if (value.size() >= 2 && value.front() == L'"' && value.back() == L'"')
        value = value.substr(1, value.size() - 2);
    DWORD required = ExpandEnvironmentStringsW(value.c_str(), nullptr, 0);
    if (required) {
        std::wstring expanded(required, L'\0');
        if (ExpandEnvironmentStringsW(value.c_str(), expanded.data(), required) == required) {
            expanded.resize(required - 1);
            value = std::move(expanded);
        }
    }
    std::replace(value.begin(), value.end(), L'/', L'\\');
    if (std::filesystem::path(value).is_absolute())
        value = std::filesystem::path(value).lexically_normal().wstring();
    while (value.size() > 3 && value.back() == L'\\') value.pop_back();
    return value;
}
bool Same(const std::wstring& a, const std::wstring& b) {
    const auto left = Normalized(a), right = Normalized(b);
    return CompareStringOrdinal(left.c_str(), static_cast<int>(left.size()),
        right.c_str(), static_cast<int>(right.size()), TRUE) == CSTR_EQUAL;
}
std::vector<std::wstring> Split(const std::wstring& path) {
    std::vector<std::wstring> parts;
    size_t start = 0;
    do {
        const size_t end = path.find(L';', start);
        parts.push_back(path.substr(start, end == std::wstring::npos ? end : end - start));
        if (end == std::wstring::npos) break;
        start = end + 1;
    } while (true);
    return parts;
}
bool Contains(const std::wstring& path, const std::wstring& entry) {
    const auto parts = Split(path);
    return std::any_of(parts.begin(), parts.end(), [&](const auto& part) { return Same(part, entry); });
}
void RemoveOwned(std::vector<std::wstring>& parts, const std::wstring& entry) {
    if (entry.empty()) return;
    const auto found = std::find_if(parts.begin(), parts.end(), [&](const auto& part) { return Same(part, entry); });
    if (found != parts.end()) parts.erase(found); // Only the one entry we inserted.
}
std::wstring Reconcile(const std::wstring& path, const std::wstring& old, const std::wstring& pending) {
    auto parts = Split(path);
    RemoveOwned(parts, old);
    if (!Same(old, pending)) RemoveOwned(parts, pending);
    if (!pending.empty()) parts.insert(parts.begin(), pending);
    std::wstring result;
    for (size_t i = 0; i < parts.size(); ++i) {
        if (i) result += L';';
        result += parts[i];
    }
    return result;
}

// Inspect PE bytes instead of loading DLLs into the elevated configuration process.
struct PeFile {
    std::vector<char> data;
    const IMAGE_NT_HEADERS64* nt{};
    const IMAGE_SECTION_HEADER* sections{};
    template<class T> const T* At(size_t offset, size_t count = 1) const {
        if (offset > data.size() || count > (data.size() - offset) / sizeof(T)) return nullptr;
        return reinterpret_cast<const T*>(data.data() + offset);
    }
    const char* Rva(DWORD rva, size_t bytes) const {
        if (rva < nt->OptionalHeader.SizeOfHeaders) return At<char>(rva, bytes);
        for (unsigned i = 0; i < nt->FileHeader.NumberOfSections; ++i) {
            const auto& section = sections[i];
            if (rva >= section.VirtualAddress && rva - section.VirtualAddress < section.SizeOfRawData) {
                const DWORD delta = rva - section.VirtualAddress;
                if (bytes > section.SizeOfRawData - delta) return nullptr;
                return At<char>(static_cast<size_t>(section.PointerToRawData) + delta, bytes);
            }
        }
        return nullptr;
    }
    std::string String(DWORD rva) const {
        std::string result;
        for (unsigned i = 0; i < 1024; ++i) {
            if (rva > MAXDWORD - i) return {};
            const char* p = Rva(rva + i, 1);
            if (!p) return {};
            if (!*p) return result;
            result += *p;
        }
        return {};
    }
    explicit PeFile(const std::filesystem::path& path) {
        std::ifstream input(path, std::ios::binary);
        if (!input) throw std::runtime_error("Missing runtime DLL");
        data.assign(std::istreambuf_iterator<char>(input), {});
        const auto* dos = At<IMAGE_DOS_HEADER>(0);
        if (!dos || dos->e_magic != IMAGE_DOS_SIGNATURE || dos->e_lfanew < 0)
            throw std::runtime_error("Invalid PE");
        nt = At<IMAGE_NT_HEADERS64>(static_cast<size_t>(dos->e_lfanew));
        if (!nt || nt->Signature != IMAGE_NT_SIGNATURE ||
            nt->FileHeader.Machine != IMAGE_FILE_MACHINE_AMD64 ||
            nt->OptionalHeader.Magic != IMAGE_NT_OPTIONAL_HDR64_MAGIC ||
            nt->FileHeader.SizeOfOptionalHeader < sizeof(IMAGE_OPTIONAL_HEADER64))
            throw std::runtime_error("Not an x64 PE");
        sections = At<IMAGE_SECTION_HEADER>(static_cast<size_t>(dos->e_lfanew) +
            sizeof(DWORD) + sizeof(IMAGE_FILE_HEADER) + nt->FileHeader.SizeOfOptionalHeader,
            nt->FileHeader.NumberOfSections);
        if (!sections) throw std::runtime_error("Invalid PE sections");
    }
    bool BridgeExport(bool thin) const {
        const auto& directory = nt->OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_EXPORT];
        if (directory.Size < sizeof(IMAGE_EXPORT_DIRECTORY)) return false;
        const auto* exports = reinterpret_cast<const IMAGE_EXPORT_DIRECTORY*>(
            Rva(directory.VirtualAddress, sizeof(IMAGE_EXPORT_DIRECTORY)));
        if (!exports) return false;
        const auto* names = reinterpret_cast<const DWORD*>(Rva(exports->AddressOfNames, size_t(exports->NumberOfNames) * 4));
        const auto* ordinals = reinterpret_cast<const WORD*>(Rva(exports->AddressOfNameOrdinals, size_t(exports->NumberOfNames) * 2));
        const auto* functions = reinterpret_cast<const DWORD*>(Rva(exports->AddressOfFunctions, size_t(exports->NumberOfFunctions) * 4));
        if (!names || !ordinals || !functions) return false;
        bool found = false;
        for (DWORD i = 0; i < exports->NumberOfNames; ++i) {
            if (ordinals[i] >= exports->NumberOfFunctions) return false;
            const DWORD address = functions[ordinals[i]];
            if (!thin) {
                if (String(names[i]) == "TitanLeiaRuntimeVersion") found = true;
            } else {
                if (address < directory.VirtualAddress ||
                    address - directory.VirtualAddress >= directory.Size ||
                    String(address).rfind("TitanLeiaRuntime.", 0) != 0) return false;
                found = true;
            }
        }
        return found;
    }
};
std::wstring Validate(const std::filesystem::path& runtime) {
    if (!runtime.is_absolute()) throw std::runtime_error("Runtime path must be absolute");
    const auto path = runtime.lexically_normal().wstring();
    if (path.find_first_of(L";\"%") != std::wstring::npos)
        throw std::runtime_error("Unsafe runtime path");
    if (!PeFile(runtime / L"TitanLeiaRuntime.dll").BridgeExport(false))
        throw std::runtime_error("Missing bridge identity export");
    for (const auto* module : {L"Core", L"DirectX", L"Displays", L"FaceTrackers", L"OpenGL"})
        if (!PeFile(runtime / (std::wstring(L"SimulatedReality") + module + L".dll")).BridgeExport(true))
            throw std::runtime_error("Runtime is not a Titan bridge forwarder");
    PeFile dependency(runtime / L"opencv_world343.dll");
    return Normalized(path);
}
void Recover(HKEY environment, HKEY ownership) {
    const auto pending = Read(ownership, L"PendingPath");
    if (!pending.exists) return;
    const auto old = Read(ownership, L"ManagedPath").text;
    const auto path = Read(environment, L"Path");
    Write(environment, L"Path", Reconcile(path.text, old, pending.text), path.type);
    Check(RegFlushKey(environment));
    // Pending remains until the committed owner is durable.
    Write(ownership, L"ManagedPath", pending.text);
    Check(RegFlushKey(ownership));
    Erase(ownership, L"PendingPath");
    Check(RegFlushKey(ownership));
}
std::wstring ErrorText(LONG code) {
    wchar_t* text{};
    FormatMessageW(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM |
        FORMAT_MESSAGE_IGNORE_INSERTS, nullptr, code, 0, reinterpret_cast<wchar_t*>(&text), 0, nullptr);
    std::wstring result = text ? text : L"注册表操作失败";
    if (text) LocalFree(text);
    return result;
}
}

GlobalRuntimeStatus ReadGlobalRuntimeStatus(const std::filesystem::path& runtime,
    const GlobalRuntimeRegistryStore& store) {
    GlobalRuntimeStatus status;
    try {
        Key environment, ownership;
        Open(environment, store, store.environmentKey, false);
        Open(ownership, store, store.ownershipKey, false);
        const auto managed = Read(ownership.value, L"ManagedPath").text;
        status.managedDirectory = managed;
        status.pendingRecovery = Read(ownership.value, L"PendingPath").exists;
        const auto parts = Split(Read(environment.value, L"Path").text);
        status.enabled = !status.pendingRecovery && !managed.empty() &&
            Same(managed, runtime.lexically_normal().wstring()) && !parts.empty() && Same(parts.front(), managed);
    } catch (const RegistryFailure& failure) { status.error = ErrorText(failure.code); }
    catch (const std::exception&) { status.error = L"全局运行库配置格式不正确。"; }
    return status;
}

bool ConfigureGlobalRuntime(const std::filesystem::path& runtime, bool enable,
    std::wstring& error, const GlobalRuntimeRegistryStore& store) {
    error.clear();
    HANDLE mutex = CreateMutexW(nullptr, FALSE, L"Global\\TitanLeiaBridge.RuntimeIntegration");
    if (!mutex) { error = ErrorText(GetLastError()); return false; }
    const DWORD wait = WaitForSingleObject(mutex, 30000);
    if (wait != WAIT_OBJECT_0 && wait != WAIT_ABANDONED) {
        CloseHandle(mutex);
        error = L"另一个进程正在配置全局运行库，请稍后重试。";
        return false;
    }
    bool success = false;
    try {
        const std::wstring target = enable ? Validate(runtime) : L"";
        bool unconfigured = false;
        if (!enable) {
            Key existingOwnership;
            Open(existingOwnership, store, store.ownershipKey, false);
            unconfigured = Read(existingOwnership.value, L"ManagedPath").text.empty() &&
                !Read(existingOwnership.value, L"PendingPath").exists;
        }
        if (unconfigured) {
            // A fresh disable must not create either the ownership key or an empty Path.
            ReleaseMutex(mutex);
            CloseHandle(mutex);
            return true;
        }
        Key environment, ownership;
        Open(environment, store, store.environmentKey, true);
        Open(ownership, store, store.ownershipKey, true);
        Recover(environment.value, ownership.value);
        const auto managed = Read(ownership.value, L"ManagedPath").text;
        const auto path = Read(environment.value, L"Path");
        if (enable && !Same(target, managed) && Contains(path.text, target))
            throw std::runtime_error("Runtime directory already exists in Path without bridge ownership");
        // This single durable value journals both update and uninstall. Empty means uninstall.
        Write(ownership.value, L"PendingPath", target);
        Check(RegFlushKey(ownership.value));
        Recover(environment.value, ownership.value);
        success = true;
    } catch (const RegistryFailure& failure) { error = ErrorText(failure.code); }
    catch (const std::exception& failure) {
        const std::string text = failure.what();
        error = L"无法配置全局运行库：";
        error.append(text.begin(), text.end());
    }
    ReleaseMutex(mutex);
    CloseHandle(mutex);
    if (success && store.broadcast) {
        DWORD_PTR result{};
        SendMessageTimeoutW(HWND_BROADCAST, WM_SETTINGCHANGE, 0, reinterpret_cast<LPARAM>(L"Environment"),
            SMTO_ABORTIFHUNG, 2000, &result);
    }
    return success;
}
} // namespace titan::app
