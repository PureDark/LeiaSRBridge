// Exercise ordinary name-based DLL discovery from a fresh process. The only
// search-path change is that child's environment; no registry, injection,
// AddDllDirectory, SetDllDirectory or absolute-path LoadLibrary is used.
#include <sr/management/srcontext.h>
#include <sr/sense/headtracker/headtracker.h>
#include <sr/sense/headtracker/headposetracker.h>
#include <sr/sense/eyetracker/eyetracker.h>
#include <sr/weaver/dx12weaver.h>
#include <dxgi1_6.h>
#include <wrl/client.h>
#include "broker/BrokerService.h"
#include <algorithm>
#include <cmath>
#include <cstdio>
#include <filesystem>
#include <fstream>
#include <io.h>
#include <mutex>
#include <stdexcept>
#include <string>
#include <vector>

namespace fs = std::filesystem;
using Microsoft::WRL::ComPtr;
namespace {
void Require(bool condition, const char* message) {
    if (!condition) throw std::runtime_error(message);
}
fs::path ModulePath(HMODULE module) {
    std::wstring buffer(32768, L'\0');
    const auto count = GetModuleFileNameW(module, buffer.data(), static_cast<DWORD>(buffer.size()));
    Require(count && count < buffer.size(), "Cannot obtain module path");
    buffer.resize(count);
    return buffer;
}
bool SamePath(const fs::path& a, const fs::path& b) {
    return _wcsicmp(fs::weakly_canonical(a).c_str(), fs::weakly_canonical(b).c_str()) == 0;
}
void ModuleInRuntime(HMODULE module, const fs::path& runtime) {
    Require(module && SamePath(ModulePath(module).parent_path(), runtime),
        "A DLL resolved outside the selected bridge runtime");
}
template<class T> T Symbol(HMODULE module, const char* name) {
    const auto symbol = GetProcAddress(module, name);
    if (!symbol) throw std::runtime_error(std::string("Missing export: ") + name);
    return reinterpret_cast<T>(symbol);
}
void CheckForwarders(HMODULE module, HMODULE implementation) {
    const auto* base = reinterpret_cast<const unsigned char*>(module);
    const auto* dos = reinterpret_cast<const IMAGE_DOS_HEADER*>(base);
    const auto* nt = reinterpret_cast<const IMAGE_NT_HEADERS64*>(base + dos->e_lfanew);
    const auto rva = nt->OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_EXPORT].VirtualAddress;
    Require(rva != 0, "Forwarding module has no exports");
    const auto* exports = reinterpret_cast<const IMAGE_EXPORT_DIRECTORY*>(base + rva);
    const auto* names = reinterpret_cast<const DWORD*>(base + exports->AddressOfNames);
    Require(exports->NumberOfNames != 0, "Forwarding module has no named exports");
    for (DWORD i = 0; i < exports->NumberOfNames; ++i) {
        const auto address = GetProcAddress(module, reinterpret_cast<const char*>(base + names[i]));
        HMODULE owner{};
        Require(address && GetModuleHandleExW(
            GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(address), &owner) && owner == implementation,
            "A forwarded export is missing or resolves to a different runtime");
    }
}
void NoHardwareModules() {
    for (const auto* module : {L"MCU_library.dll", L"FaceTrackingManagerService.dll",
             L"BOE_3DET_SDK.dll", L"TitanBoeTrackingVendor.dll", L"blink.dll"})
        Require(!GetModuleHandleW(module), "The loader test unexpectedly loaded a hardware provider");
}
template<class Frame, class Base> struct Listener final : Base {
    std::mutex mutex;
    Frame frame{};
    unsigned count{};
    void accept(const Frame& value) override {
        std::lock_guard lock(mutex);
        frame = value;
        ++count;
    }
    Frame Read() { std::lock_guard lock(mutex); return frame; }
    unsigned Count() { std::lock_guard lock(mutex); return count; }
};
template<class Predicate> void WaitFor(Predicate predicate, const char* message) {
    const auto end = GetTickCount64() + 2500;
    while (!predicate() && GetTickCount64() < end) Sleep(5);
    Require(predicate(), message);
}

int Child(const fs::path& runtime) {
    std::puts("Child: verify isolated directories");
    Require(!SamePath(ModulePath(nullptr).parent_path(), runtime) &&
        !SamePath(fs::current_path(), runtime), "Test does not isolate the executable and working directories");
    for (const auto& directory : {ModulePath(nullptr).parent_path(), fs::current_path()})
        for (const auto& entry : fs::directory_iterator(directory))
            Require(_wcsicmp(entry.path().extension().c_str(), L".dll") != 0,
                "Test executable/working directory unexpectedly contains a DLL");

    // Match the SR-lib path: DirectX is explicitly loaded by basename, then
    // the other SDK modules are found through the same normal loader policy.
    const auto directx = LoadLibraryW(L"SimulatedRealityDirectX.dll");
    const auto core = LoadLibraryW(L"SimulatedRealityCore.dll");
    const auto faces = LoadLibraryW(L"SimulatedRealityFaceTrackers.dll");
    const auto displays = LoadLibraryW(L"SimulatedRealityDisplays.dll");
    const auto gl = LoadLibraryW(L"SimulatedRealityOpenGL.dll");
    std::puts("Child: validate named modules and forwarders");
    for (auto module : {directx, core, faces, displays, gl}) ModuleInRuntime(module, runtime);
    const auto create = Symbol<SR::SRContext*(*)(SR::SRContext::NetworkMode)>(
        core, "?create@SRContext@SR@@SAPEAV12@W4NetworkMode@12@@Z");
    // Resolving a forwarder must itself locate the common runtime by name.
    const auto implementation = GetModuleHandleW(L"TitanLeiaRuntime.dll");
    ModuleInRuntime(implementation, runtime);
    ModuleInRuntime(GetModuleHandleW(L"opencv_world343.dll"), runtime);
    for (auto module : {directx, core, faces, displays, gl}) CheckForwarders(module, implementation);
    const auto initialize = Symbol<void(*)(SR::SRContext*)>(
        core, "?initialize@SRContext@SR@@QEAAXXZ");
    const auto destroy = Symbol<void(*)(SR::SRContext*)>(
        core, "?deleteSRContext@SRContext@SR@@SAXPEAV12@@Z");
    const auto headFactory = Symbol<decltype(&SR::HeadTracker::create)>(
        faces, "?create@HeadTracker@SR@@SAPEAV12@AEAVSRContext@2@@Z");
    const auto poseFactory = Symbol<decltype(&SR::HeadPoseTracker::create)>(
        faces, "?create@HeadPoseTracker@SR@@SAPEAV12@AEAVSRContext@2@@Z");
    const auto eyeFactory = Symbol<decltype(&SR::EyeTracker::create)>(
        faces, "?create@EyeTracker@SR@@SAPEAV12@AEAVSRContext@2@@Z");
    const auto stopHead = Symbol<void(*)(SR::HeadStream*)>(
        faces, "?stopListening@HeadStream@SR@@QEAAXXZ");
    const auto stopPose = Symbol<void(*)(SR::HeadPoseStream*)>(
        faces, "?stopListening@HeadPoseStream@SR@@QEAAXXZ");
    const auto stopEye = Symbol<void(*)(SR::EyePairStream*)>(
        faces, "?stopListening@EyePairStream@SR@@QEAAXXZ");
    const auto weaverFactory = Symbol<decltype(&SR::CreateDX12Weaver)>(directx,
        "?CreateDX12Weaver@SR@@YA?AW4WeaverErrorCode@@PEAVSRContext@1@PEAUID3D12Device@@PEAUHWND__@@PEAPEAVIDX12Weaver1@1@@Z");
    auto* context = create(SR::SRContext::NonBlockingClientMode);
    std::puts("Child: initialize SDK streams");
    Require(context != nullptr, "Cannot create compatibility context through named DLL");
    Listener<SR_head, SR::HeadListener> headListener;
    Listener<SR_headPose, SR::HeadPoseListener> poseListener;
    Listener<SR_eyePair, SR::EyePairListener> eyeListener;
    auto head = headFactory(*context)->openHeadStream(&headListener);
    auto pose = poseFactory(*context)->openHeadPoseStream(&poseListener);
    auto eye = eyeFactory(*context)->openEyePairStream(&eyeListener);
    initialize(context);
    WaitFor([&] { return headListener.Count() > 1 && poseListener.Count() > 1 && eyeListener.Count() > 1 &&
        std::isfinite(eyeListener.Read().left.x) && std::isfinite(headListener.Read().eyes.left.x); },
        "Named SDK modules did not receive mock broker streams");
    const auto headFrame = headListener.Read();
    const auto poseFrame = poseListener.Read();
    const auto eyeFrame = eyeListener.Read();
    Require(std::isfinite(headFrame.headPose.position.z) && std::isfinite(poseFrame.position.z) &&
        eyeFrame.right.x > eyeFrame.left.x &&
        std::abs(headFrame.eyes.left.x - eyeFrame.left.x) < .001 &&
        std::abs(headFrame.eyes.right.z - eyeFrame.right.z) < .001 &&
        std::abs(poseFrame.position.x - (eyeFrame.left.x + eyeFrame.right.x) * .5) < .001 &&
        std::abs(headFrame.ears.right.x - headFrame.ears.left.x - 140) < .001,
        "SDK streams did not expose the synthetic measured eye pair");
    ComPtr<IDXGIFactory4> factory;
    std::puts("Child: create WARP DX12 weaver");
    ComPtr<IDXGIAdapter> adapter;
    ComPtr<ID3D12Device> device;
    Require(SUCCEEDED(CreateDXGIFactory2(0, IID_PPV_ARGS(&factory))) &&
        SUCCEEDED(factory->EnumWarpAdapter(IID_PPV_ARGS(&adapter))) &&
        SUCCEEDED(D3D12CreateDevice(adapter.Get(), D3D_FEATURE_LEVEL_11_0, IID_PPV_ARGS(&device))),
        "Cannot create WARP DX12 test device");
    SR::IDX12Weaver1* weaver{};
    Require(weaverFactory(context, device.Get(), nullptr, &weaver) == WeaverSuccess && weaver,
        "DX12 factory could not be reached through the named forwarding DLL");
    float left[3]{}, right[3]{};
    weaver->getPredictedEyePositions(left, right);
    Require(std::isfinite(left[2]) && right[0] > left[0], "DX12 factory cannot read broker eye pair");
    weaver->query<SR::IDestroyable>()->destroy();
    stopHead(head.get()); stopPose(pose.get()); stopEye(eye.get());
    head.reset(); pose.reset(); eye.reset();
    destroy(context);
    NoHardwareModules();
    // Let normal process teardown unload dependencies after all SDK objects.
    std::puts("PASS child: name-only SDK/forwarders/dependencies, Head/HeadPose/Eye streams and WARP DX12 factory");
    return 0;
}

std::vector<wchar_t> ChildEnvironment(const fs::path& runtime) {
    const auto block = GetEnvironmentStringsW();
    Require(block != nullptr, "Cannot read inherited environment");
    std::vector<std::wstring> entries;
    std::wstring inheritedPath;
    for (auto p = block; *p; p += wcslen(p) + 1) {
        if (_wcsnicmp(p, L"PATH=", 5) == 0) inheritedPath = p + 5;
        else entries.emplace_back(p);
    }
    FreeEnvironmentStringsW(block);
    entries.emplace_back(L"PATH=" + runtime.wstring() + L";" + inheritedPath);
    std::sort(entries.begin(), entries.end(), [](const auto& a, const auto& b) {
        return _wcsicmp(a.c_str(), b.c_str()) < 0;
    });
    std::vector<wchar_t> result;
    for (const auto& entry : entries) {
        result.insert(result.end(), entry.begin(), entry.end());
        result.push_back(L'\0');
    }
    result.push_back(L'\0');
    return result;
}
struct TempDirectory {
    fs::path root;
    TempDirectory() {
        wchar_t base[MAX_PATH]{}, unique[MAX_PATH]{};
        Require(GetTempPathW(MAX_PATH, base) != 0 &&
            GetTempFileNameW(base, L"TGL", 0, unique) != 0, "Cannot reserve temporary directory");
        root = fs::absolute(unique).lexically_normal();
        Require(SamePath(root.parent_path(), fs::path(base)) &&
            root.filename().wstring().starts_with(L"TGL"), "Temporary root is outside the requested directory");
        Require(DeleteFileW(root.c_str()) != FALSE && CreateDirectoryW(root.c_str(), nullptr) != FALSE,
            "Cannot create temporary directory");
    }
    ~TempDirectory() {
        // The exact freshly reserved absolute root is owned by this test.
        std::error_code error;
        fs::remove_all(root, error);
    }
};
struct ChildProcess {
    PROCESS_INFORMATION process{};
    bool completed{};
    ~ChildProcess() {
        if (process.hProcess && !completed) {
            TerminateProcess(process.hProcess, 2);
            WaitForSingleObject(process.hProcess, 5000);
        }
        if (process.hThread) CloseHandle(process.hThread);
        if (process.hProcess) CloseHandle(process.hProcess);
    }
};
int Parent(const fs::path& selectedRuntime) {
    const auto runtime = fs::canonical(selectedRuntime);
    for (const auto* name : {L"SimulatedRealityCore.dll", L"SimulatedRealityDirectX.dll",
             L"SimulatedRealityFaceTrackers.dll", L"SimulatedRealityDisplays.dll",
             L"SimulatedRealityOpenGL.dll", L"TitanLeiaRuntime.dll", L"opencv_world343.dll"})
        Require(fs::is_regular_file(runtime / name), "Selected runtime is incomplete");
    TempDirectory temporary;
    const auto exeDir = temporary.root / L"exe";
    const auto workDir = temporary.root / L"work";
    fs::create_directory(exeDir);
    fs::create_directory(workDir);
    const auto executable = exeDir / L"GlobalLoaderSmoke.exe";
    fs::copy_file(ModulePath(nullptr), executable);
    titan::broker::ProviderOptions options;
    options.mock = true;
    options.vendorDirectory = temporary.root / L"nonexistent-vendor";
    options.horizontalTenths = std::make_shared<std::atomic<int>>(0);
    titan::broker::BrokerService broker;
    Require(broker.Start(options, {}), "Cannot start mock broker; close the real bridge before this test");
    WaitFor([&] { return broker.ReadStatus().status == titan::ProviderStatus::Mock; },
        "Mock broker never became ready");
    auto environment = ChildEnvironment(runtime);
    std::wstring command = L"\"" + executable.wstring() + L"\" --child \"" + runtime.wstring() + L"\"";
    STARTUPINFOW startup{}; startup.cb = sizeof(startup);
    ChildProcess child;
    Require(CreateProcessW(executable.c_str(), command.data(), nullptr, nullptr, FALSE,
        CREATE_UNICODE_ENVIRONMENT | CREATE_NO_WINDOW, environment.data(), workDir.c_str(),
        &startup, &child.process) != FALSE, "Cannot launch isolated loader test");
    Require(WaitForSingleObject(child.process.hProcess, 30000) == WAIT_OBJECT_0,
        "Isolated loader test timed out");
    child.completed = true;
    std::ifstream childLog(workDir / L"GlobalLoaderSmoke-child.log");
    const std::string childText((std::istreambuf_iterator<char>(childLog)), std::istreambuf_iterator<char>());
    std::fputs(childText.c_str(), stdout);
    DWORD exitCode{};
    Require(GetExitCodeProcess(child.process.hProcess, &exitCode) != FALSE, "Cannot read child exit code");
    if (exitCode != 0) {
        std::fprintf(stderr, "Child exit code: 0x%08lX\n", exitCode);
        throw std::runtime_error("Isolated loader test failed");
    }
    NoHardwareModules();
    Require(!broker.ReadStatus().lensEnabled, "Loader-only client unexpectedly enabled the mock lens");
    broker.Stop();
    std::puts("PASS: isolated process discovers bridge via its PATH; original PATH retained; no injection, DLL-directory APIs, games, registry or hardware");
    return 0;
}
}

int wmain(int argc, wchar_t** argv) {
    try {
        if (argc == 3 && wcscmp(argv[1], L"--child") == 0) {
            FILE* output{};
            Require(freopen_s(&output, "GlobalLoaderSmoke-child.log", "w", stdout) == 0,
                "Cannot create child diagnostic log");
            setvbuf(stdout, nullptr, _IONBF, 0);
            // CREATE_NO_WINDOW may give stderr an invalid CRT descriptor.
            // Open NUL once to establish it, then duplicate the already-open
            // log descriptor rather than reopening the same file exclusively.
            FILE* error{};
            Require(freopen_s(&error, "NUL", "w", stderr) == 0 &&
                _dup2(_fileno(stdout), _fileno(stderr)) == 0,
                "Cannot redirect child errors");
            return Child(fs::canonical(argv[2]));
        }
        Require(argc == 2, "Usage: GlobalLoaderSmoke <bridge-runtime-directory>");
        return Parent(argv[1]);
    } catch (const std::exception& error) {
        std::fprintf(stderr, "FAIL: %s (Win32=%lu)\n", error.what(), GetLastError());
        return 1;
    }
}
