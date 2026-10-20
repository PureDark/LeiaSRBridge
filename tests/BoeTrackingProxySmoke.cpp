#include "common/BoeTrackingProtocol.h"
#include <array>
#include <iostream>
#include <string>
#include <thread>

namespace {
using DoEyeTrack = int(__cdecl*)(const void*, int, int, int, void*);
int failures{};
void Check(bool value, const char* message) {
    if (!value) { std::cerr << message << '\n'; ++failures; }
}
int Call(DoEyeTrack call, int width, std::array<std::uint8_t, 168>& bytes) {
    return call(reinterpret_cast<const void*>(std::uintptr_t(0x123456789ABC)),
        width, 480, 7, bytes.data());
}
}

int wmain(int argc, wchar_t** argv) {
    if (argc < 2) return 2;
    HMODULE proxy = LoadLibraryExW(argv[1], nullptr,
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR | LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
    Check(proxy != nullptr, "load proxy");
    if (!proxy) return 1;
    auto call = reinterpret_cast<DoEyeTrack>(GetProcAddress(proxy, "doEyeTrack_3D"));
    Check(call != nullptr, "resolve intercepted export");
    if (!call) return 1;
    std::array<std::uint8_t, 168> bytes{};
    if (argc == 4 && std::wstring(argv[2]) == L"--producer") {
        HANDLE stop = OpenEventW(SYNCHRONIZE, FALSE, argv[3]);
        if (!stop) return 3;
        while (WaitForSingleObject(stop, 1) == WAIT_TIMEOUT) Call(call, 640, bytes);
        CloseHandle(stop);
        return 0;
    }
    std::wstring vendorPath(argv[1]);
    vendorPath.resize(vendorPath.find_last_of(L"\\/") + 1);
    vendorPath += L"TitanBoeTrackingVendor.dll";
    HMODULE vendor = LoadLibraryExW(vendorPath.c_str(), nullptr,
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR | LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
    Check(vendor != nullptr, "load fake vendor");
    const char* exports[]{"initBoeEyeTrack_3D", "releaseBoeEyeTrack_3D",
        "setCameraMat_3D", "setEnableTrackArea_3D", "setFaceAreaTime_3D", "setHighConArea_3D"};
    for (int i = 0; i < 6; ++i) {
        const auto original = GetProcAddress(vendor, exports[i]);
        Check(original && GetProcAddress(proxy, exports[i]) == original, "PE name forwarding");
        Check(GetProcAddress(proxy, MAKEINTRESOURCEA(i + 2)) == original, "PE ordinal forwarding");
    }
    Check(GetProcAddress(proxy, MAKEINTRESOURCEA(1)) == reinterpret_cast<FARPROC>(call),
        "intercepted ordinal");
    Check(Call(call, 640, bytes) == 1, "argument and return forwarding");
    const auto creation = titan::BoeProcessCreationTime(GetCurrentProcess());
    titan::BoeTrackingReader reader;
    Check(!reader.Open(GetCurrentProcessId(), creation + 1), "PID reuse rejection");
    Check(reader.Open(GetCurrentProcessId(), creation), "open producer IPC");
    titan::BoeTrackingFrame frame{};
    Check(reader.Read(frame), "read full frame");
    Check(frame.sequence == 1 && frame.observationTick <= GetTickCount64() &&
        frame.centerValid && frame.pairValid && frame.center[0] == 12 &&
        frame.eyeA[0] == -20 && frame.eyeB[2] == 610, "decoded independent eyes");
    Check(std::memcmp(frame.raw, bytes.data(), bytes.size()) == 0, "all 168 bytes retained");
    Check(!frame.facePointsValid,"Unrecognized vendor binary must not enable private layout reads");
    Check(Call(call, 1001, bytes) == 0, "failed SDK return preserved");
    Check(reader.Read(frame) && frame.sequence == 2 && !frame.centerValid &&
        !frame.pairValid, "failed observation invalidated");
    Check(bytes == std::array<std::uint8_t, 168>{}, "untouched failure cannot retain prior eyes");
    Call(call, 1002, bytes);
    Check(reader.Read(frame) && frame.centerValid && !frame.pairValid, "NaN eye invalid");
    Call(call, 1003, bytes);
    Check(reader.Read(frame) && !frame.centerValid && !frame.pairValid, "invalid depth rejected");
    Call(call, 1004, bytes);
    Check(reader.Read(frame) && frame.centerValid && !frame.pairValid, "coincident eyes rejected");

    // Check nonblocking contention: a slow reader must not hold the camera thread.
    wchar_t mappingName[96]{}, mutexName[96]{};
    titan::BoeTrackingNames(GetCurrentProcessId(), mappingName, mutexName);
    HANDLE mutex = OpenMutexW(SYNCHRONIZE | MUTEX_MODIFY_STATE, FALSE, mutexName);
    Check(mutex && WaitForSingleObject(mutex, 0) == WAIT_OBJECT_0, "lock publisher IPC");
    std::thread blockedConsumer([&] { std::array<std::uint8_t, 168> local{}; Call(call, 640, local); });
    blockedConsumer.join();
    ReleaseMutex(mutex);
    Check(reader.Read(frame) && frame.sequence == 5, "contended publish remains stale");
    CloseHandle(mutex);
    Call(call, 640, bytes);
    Check(reader.Read(frame) && frame.sequence == 7 && frame.pairValid, "publication resumes");
    HANDLE mapping = OpenFileMappingW(FILE_MAP_WRITE, FALSE, mappingName);
    auto* shared = static_cast<titan::BoeTrackingState*>(
        MapViewOfFile(mapping, FILE_MAP_WRITE, 0, 0, sizeof(titan::BoeTrackingState)));
    Check(shared != nullptr, "open synthetic interrupted transaction");
    if (shared) {
        // Thread exits while owning the mutex after it has begun a write. The
        // process stays alive; all subsequent reads must still reject the frame.
        std::thread abandoned([&] {
            HANDLE lock = OpenMutexW(SYNCHRONIZE | MUTEX_MODIFY_STATE, FALSE, mutexName);
            if (WaitForSingleObject(lock, INFINITE) == WAIT_OBJECT_0) {
                InterlockedExchange64(&shared->committedSequence, 0);
                shared->frame.center[0] = 999;
            }
            CloseHandle(lock);
        });
        abandoned.join();
        Check(reader.IsAlive() && !reader.Read(frame) && !reader.Read(frame),
            "abandoned partial write rejected repeatedly");
        Call(call, 640, bytes);
        Check(reader.Read(frame) && frame.sequence == 8 && frame.center[0] == 12,
            "new complete transaction repairs abandonment");
        UnmapViewOfFile(shared);
    }
    if (mapping) CloseHandle(mapping);

    // A second process publishes into its own mapping. The expected identity is
    // checked independently, and the reader refuses data after the producer dies.
    wchar_t exe[32768]{};
    GetModuleFileNameW(nullptr, exe, DWORD(std::size(exe)));
    const std::wstring eventName = L"Local\\TitanBoeTrackingTest." + std::to_wstring(GetCurrentProcessId());
    HANDLE stop = CreateEventW(nullptr, TRUE, FALSE, eventName.c_str());
    std::wstring command = L"\"" + std::wstring(exe) + L"\" \"" + argv[1] +
        L"\" --producer \"" + eventName + L"\"";
    STARTUPINFOW startup{sizeof(startup)};
    PROCESS_INFORMATION child{};
    Check(CreateProcessW(exe, command.data(), nullptr, nullptr, FALSE, CREATE_NO_WINDOW,
        nullptr, nullptr, &startup, &child) != FALSE, "launch synthetic producer");
    if (child.hProcess) {
        titan::BoeTrackingReader other;
        bool opened = false, read = false;
        for (int i = 0; i < 200 && !read; ++i) {
            if (!opened) opened = other.Open(child.dwProcessId,
                titan::BoeProcessCreationTime(child.hProcess));
            if (opened) read = other.Read(frame);
            if (!read) Sleep(5);
        }
        Check(read && frame.processId == child.dwProcessId && frame.pairValid,
            "cross-process complete snapshot");
        titan::BoeTrackingFrame own{};
        Check(reader.Read(own) && own.processId == GetCurrentProcessId() &&
            own.sequence == 8, "producers remain isolated");
        SetEvent(stop);
        Check(WaitForSingleObject(child.hProcess, 10000) == WAIT_OBJECT_0, "producer exits");
        Check(!other.Read(frame), "dead producer rejected");
        CloseHandle(child.hThread); CloseHandle(child.hProcess);
    }
    if (stop) CloseHandle(stop);
    reader.Close();
    if (vendor) FreeLibrary(vendor);
    FreeLibrary(proxy);
    std::cout << "BoeTrackingProxy failures=" << failures << '\n';
    return failures ? 1 : 0;
}
