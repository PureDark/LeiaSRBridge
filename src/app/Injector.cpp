#include "Injector.h"
#include <TlHelp32.h>
#include <algorithm>
#include <cwctype>
#include <memory>

namespace titan::app {
namespace {
struct HandleCloser { void operator()(void* p) const { if (p && p != INVALID_HANDLE_VALUE) CloseHandle(p); } };
using Handle = std::unique_ptr<void, HandleCloser>;
std::wstring Fold(std::wstring s) { std::transform(s.begin(), s.end(), s.begin(), towlower); return s; }
struct Module { std::wstring name, path; uintptr_t base{}; };
std::vector<Module> Modules(DWORD pid) {
    std::vector<Module> result;
    Handle snapshot(CreateToolhelp32Snapshot(TH32CS_SNAPMODULE | TH32CS_SNAPMODULE32, pid));
    MODULEENTRY32W entry{sizeof(entry)};
    if (snapshot.get() != INVALID_HANDLE_VALUE && Module32FirstW(snapshot.get(), &entry)) {
        do { result.push_back({entry.szModule, entry.szExePath, reinterpret_cast<uintptr_t>(entry.modBaseAddr)}); }
        while (Module32NextW(snapshot.get(), &entry));
    }
    return result;
}
bool IsX64(HANDLE process, std::wstring& error) {
    USHORT processMachine{}, nativeMachine{};
    if (!IsWow64Process2(process, &processMachine, &nativeMachine) ||
        processMachine != IMAGE_FILE_MACHINE_UNKNOWN || nativeMachine != IMAGE_FILE_MACHINE_AMD64) {
        error = L"Only x64 Windows processes are supported."; return false;
    }
    return true;
}
bool CheckExisting(DWORD pid, const std::filesystem::path& directory, bool& loaded, std::wstring& error) {
    loaded = false;
    bool runtimeFound = false;
    unsigned shims = 0;
    for (const auto& module : Modules(pid)) {
        const auto name = Fold(module.name);
        const bool ours = name == L"titanleiaruntime.dll";
        const bool shim = name == L"simulatedrealitycore.dll" || name == L"simulatedrealitydirectx.dll" ||
            name == L"simulatedrealitydisplays.dll" || name == L"simulatedrealityfacetrackers.dll" || name == L"simulatedrealityopengl.dll";
        if (!ours && !shim) continue;
        std::error_code ec;
        if (!std::filesystem::equivalent(module.path, directory / module.name, ec)) {
            error = L"A different Leia runtime is already loaded. Close the game and launch it from Stereo3DBridge.";
            return false;
        }
        if (ours) runtimeFound = true;
        if (shim) ++shims;
    }
    loaded = runtimeFound && shims == 5;
    return true;
}
bool ContinueEvent(DEBUG_EVENT& event) {
    DWORD status = DBG_CONTINUE;
    if (event.dwDebugEventCode == EXCEPTION_DEBUG_EVENT &&
        event.u.Exception.ExceptionRecord.ExceptionCode != EXCEPTION_BREAKPOINT &&
        event.u.Exception.ExceptionRecord.ExceptionCode != EXCEPTION_SINGLE_STEP) status = DBG_EXCEPTION_NOT_HANDLED;
    if (event.dwDebugEventCode == LOAD_DLL_DEBUG_EVENT && event.u.LoadDll.hFile) CloseHandle(event.u.LoadDll.hFile);
    if (event.dwDebugEventCode == CREATE_PROCESS_DEBUG_EVENT && event.u.CreateProcessInfo.hFile)
        CloseHandle(event.u.CreateProcessInfo.hFile);
    // Debug-event process/thread handles are closed by ContinueDebugEvent at
    // their EXIT events. Do not close an aliased primary-thread handle here.
    return ContinueDebugEvent(event.dwProcessId, event.dwThreadId, status) != FALSE;
}
bool LoadRemote(HANDLE process, DWORD pid, const std::filesystem::path& dll, bool debugPump, std::wstring& error,
    const std::atomic<bool>* cancel) {
    if (cancel && cancel->load()) { error = L"Loader cancelled."; return false; }
    if (!std::filesystem::is_regular_file(dll)) { error = L"Missing runtime file: " + dll.wstring(); return false; }
    for (const auto& module : Modules(pid)) {
        std::error_code ec;
        if (std::filesystem::equivalent(module.path, dll, ec)) return true;
    }
    auto loadLibrary = GetProcAddress(GetModuleHandleW(L"kernel32.dll"), "LoadLibraryW");
    HMODULE owner{};
    if (!loadLibrary || !GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
        GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT, reinterpret_cast<LPCWSTR>(loadLibrary), &owner)) {
        error = L"Cannot locate LoadLibraryW."; return false;
    }
    wchar_t ownerPath[32768]{};
    GetModuleFileNameW(owner, ownerPath, static_cast<DWORD>(std::size(ownerPath)));
    const auto ownerName = Fold(std::filesystem::path(ownerPath).filename().wstring());
    uintptr_t remoteBase{};
    for (const auto& module : Modules(pid)) if (Fold(module.name) == ownerName) remoteBase = module.base;
    if (!remoteBase) { error = L"Windows loader is not ready in target process."; return false; }
    auto remoteLoad = reinterpret_cast<LPTHREAD_START_ROUTINE>(remoteBase +
        reinterpret_cast<uintptr_t>(loadLibrary) - reinterpret_cast<uintptr_t>(owner));
    const auto text = std::filesystem::absolute(dll).wstring();
    const SIZE_T bytes = (text.size() + 1) * sizeof(wchar_t);
    void* remoteString = VirtualAllocEx(process, nullptr, bytes, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
    if (!remoteString) { error = WindowsError(GetLastError()); return false; }
    SIZE_T written{};
    if (!WriteProcessMemory(process, remoteString, text.c_str(), bytes, &written) || written != bytes) {
        error = WindowsError(GetLastError()); VirtualFreeEx(process, remoteString, 0, MEM_RELEASE); return false;
    }
    Handle thread(CreateRemoteThread(process, nullptr, 0, remoteLoad, remoteString, 0, nullptr));
    if (!thread) { error = WindowsError(GetLastError()); VirtualFreeEx(process, remoteString, 0, MEM_RELEASE); return false; }
    const auto deadline = GetTickCount64() + 15000;
    while (WaitForSingleObject(thread.get(), 0) == WAIT_TIMEOUT && GetTickCount64() < deadline && !(cancel && cancel->load())) {
        if (debugPump) {
            DEBUG_EVENT event{};
            if (WaitForDebugEvent(&event, 20)) ContinueEvent(event);
        } else WaitForSingleObject(thread.get(), 20);
    }
    if (WaitForSingleObject(thread.get(), 0) != WAIT_OBJECT_0) {
        // The loader may still be reading its argument. Do not free it early.
        error = L"Target loader timed out; restart the game before retrying."; return false;
    }
    VirtualFreeEx(process, remoteString, 0, MEM_RELEASE);
    for (const auto& module : Modules(pid)) {
        std::error_code ec;
        if (std::filesystem::equivalent(module.path, dll, ec)) return true;
    }
    error = L"Target could not load " + dll.filename().wstring() + L" (check dependencies and runtime log).";
    return false;
}
bool LoadAll(HANDLE process, DWORD pid, const std::filesystem::path& directory, bool debugPump, std::wstring& error,
    const std::atomic<bool>* cancel) {
    if (!IsX64(process, error)) return false;
    bool loaded{};
    if (!CheckExisting(pid, directory, loaded, error)) return false;
    if (loaded) return true;
    // Absolute preloads make deployment independent of the game's PATH/CWD.
    const wchar_t* files[] = {L"opencv_world343.dll", L"TitanLeiaRuntime.dll", L"SimulatedRealityCore.dll",
        L"SimulatedRealityDisplays.dll", L"SimulatedRealityFaceTrackers.dll", L"SimulatedRealityDirectX.dll", L"SimulatedRealityOpenGL.dll"};
    for (auto file : files) if (!LoadRemote(process, pid, directory / file, debugPump, error, cancel)) return false;
    return true;
}
bool LoadOdyssey(HANDLE process,DWORD pid,const std::filesystem::path& directory,std::wstring& error,const std::atomic<bool>* cancel,bool debugPump){
    const auto file=directory/L"TitanOdysseyHook.dll";
    if(!LoadRemote(process,pid,file,debugPump,error,cancel))return false;
    HMODULE local=LoadLibraryExW(file.c_str(),nullptr,DONT_RESOLVE_DLL_REFERENCES);
    if(!local){error=L"Cannot inspect Odyssey hook";return false;}
    auto init=GetProcAddress(local,"TitanOdysseyInitialize");
    const auto rva=reinterpret_cast<uintptr_t>(init)-reinterpret_cast<uintptr_t>(local);
    FreeLibrary(local);if(!init){error=L"Missing Odyssey initialization export";return false;}
    uintptr_t base{};for(const auto& module:Modules(pid))if(Fold(module.name)==L"titanodysseyhook.dll")base=module.base;
    if(!base){error=L"Odyssey hook was not loaded";return false;}
    Handle thread(CreateRemoteThread(process,nullptr,0,reinterpret_cast<LPTHREAD_START_ROUTINE>(base+rva),nullptr,0,nullptr));
    if(!thread){error=WindowsError(GetLastError());return false;}
    const auto until=GetTickCount64()+15000;
    while(WaitForSingleObject(thread.get(),0)==WAIT_TIMEOUT&&GetTickCount64()<until){if(debugPump){DEBUG_EVENT event{};if(WaitForDebugEvent(&event,20))ContinueEvent(event);}else WaitForSingleObject(thread.get(),20);}
    DWORD code{};if(WaitForSingleObject(thread.get(),0)!=WAIT_OBJECT_0||!GetExitCodeThread(thread.get(),&code)||code!=1){error=L"Odyssey hooks failed to initialize; see runtime log";return false;}
    return true;
}

} // namespace

std::wstring WindowsError(DWORD error) {
    wchar_t* text{};
    FormatMessageW(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
        nullptr, error, 0, reinterpret_cast<LPWSTR>(&text), 0, nullptr);
    std::wstring result = text ? text : L"Windows operation failed";
    if (text) LocalFree(text);
    return result + L" (" + std::to_wstring(error) + L")";
}
std::vector<ProcessInfo> Processes() {
    std::vector<ProcessInfo> result;
    Handle snapshot(CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0));
    PROCESSENTRY32W entry{sizeof(entry)};
    if (snapshot.get() != INVALID_HANDLE_VALUE && Process32FirstW(snapshot.get(), &entry)) {
        do {
            Handle process(OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, FALSE, entry.th32ProcessID));
            wchar_t path[32768]{}; DWORD size = static_cast<DWORD>(std::size(path));
            if (process && QueryFullProcessImageNameW(process.get(), 0, path, &size)) result.push_back({entry.th32ProcessID, path});
        } while (Process32NextW(snapshot.get(), &entry));
    }
    return result;
}
bool InjectRuntime(DWORD pid, const std::filesystem::path& directory, std::wstring& error, const std::atomic<bool>* cancel, bool odyssey) {
    Handle process(OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ | PROCESS_VM_WRITE |
        PROCESS_VM_OPERATION | PROCESS_CREATE_THREAD | SYNCHRONIZE, FALSE, pid));
    if (!process) { error = WindowsError(GetLastError()); return false; }
    return LoadAll(process.get(), pid, directory, false, error, cancel)&&(!odyssey||LoadOdyssey(process.get(),pid,directory,error,cancel,false));
}
bool LaunchWithRuntime(const std::filesystem::path& executable, const std::wstring& arguments,
    const std::filesystem::path& directory, DWORD& pid, std::wstring& error, const std::atomic<bool>* cancel, bool odyssey) {
    pid = 0;
    if (cancel && cancel->load()) { error = L"Launch cancelled."; return false; }
    std::wstring command = L"\"" + executable.wstring() + L"\"";
    if (!arguments.empty()) command += L" " + arguments;
    STARTUPINFOW startup{sizeof(startup)};
    PROCESS_INFORMATION info{};
    const auto cwd = executable.parent_path().wstring();
    if (!CreateProcessW(executable.c_str(), command.data(), nullptr, nullptr, FALSE, DEBUG_ONLY_THIS_PROCESS,
        nullptr, cwd.c_str(), &startup, &info)) { error = WindowsError(GetLastError()); return false; }
    Handle process(info.hProcess),mainThread(info.hThread);
    if(!PrepareCreatedProcess(info,directory,error,cancel,odyssey,true))return false;
    pid=info.dwProcessId;return true;
}
bool PrepareCreatedProcess(const PROCESS_INFORMATION& info,const std::filesystem::path& directory,
    std::wstring& error,const std::atomic<bool>* cancel,bool odyssey,bool resume){
    const HANDLE process=info.hProcess, mainThread=info.hThread;
    // Keep the default kill-on-debugger-exit behavior until successful detach.
    // A loader crash must not leave our newly launched child suspended forever.
    bool breakpoint{}, exited{}, patchInstalled{};
    uintptr_t entryPoint{};
    BYTE entryByte{};
    auto patchByte = [&](BYTE value) {
        DWORD oldProtection{}; SIZE_T written{};
        void* address = reinterpret_cast<void*>(entryPoint);
        if (!VirtualProtectEx(process, address, 1, PAGE_EXECUTE_READWRITE, &oldProtection)) return false;
        const bool writtenOk = WriteProcessMemory(process, address, &value, 1, &written) && written == 1;
        DWORD ignored{};
        VirtualProtectEx(process, address, 1, oldProtection, &ignored);
        FlushInstructionCache(process, address, 1);
        return writtenOk;
    };
    const auto deadline = GetTickCount64() + 20000;
    while (GetTickCount64() < deadline && !(cancel && cancel->load())) {
        DEBUG_EVENT event{};
        if (!WaitForDebugEvent(&event, 100)) continue;
        if (event.dwDebugEventCode == EXIT_PROCESS_DEBUG_EVENT) exited = true;
        if (event.dwDebugEventCode == CREATE_PROCESS_DEBUG_EVENT) {
            const auto base = reinterpret_cast<uintptr_t>(event.u.CreateProcessInfo.lpBaseOfImage);
            IMAGE_DOS_HEADER dos{}; IMAGE_NT_HEADERS64 nt{}; SIZE_T read{};
            if (ReadProcessMemory(process, reinterpret_cast<void*>(base), &dos, sizeof(dos), &read) &&
                dos.e_magic == IMAGE_DOS_SIGNATURE && dos.e_lfanew > 0 &&
                ReadProcessMemory(process, reinterpret_cast<void*>(base + dos.e_lfanew), &nt, sizeof(nt), &read) &&
                nt.Signature == IMAGE_NT_SIGNATURE && nt.FileHeader.Machine == IMAGE_FILE_MACHINE_AMD64) {
                entryPoint = base + nt.OptionalHeader.AddressOfEntryPoint;
                if (ReadProcessMemory(process, reinterpret_cast<void*>(entryPoint), &entryByte, 1, &read))
                    patchInstalled = patchByte(0xCC);
            }
        }
        if (event.dwDebugEventCode == EXCEPTION_DEBUG_EVENT &&
            event.u.Exception.ExceptionRecord.ExceptionCode == EXCEPTION_BREAKPOINT && patchInstalled &&
            reinterpret_cast<uintptr_t>(event.u.Exception.ExceptionRecord.ExceptionAddress) == entryPoint) {
            // The ntdll startup breakpoint can still hold the loader lock.
            // Reach the actual PE entrypoint before running a loader thread.
            CONTEXT context{}; context.ContextFlags = CONTEXT_CONTROL;
            if (patchByte(entryByte) && GetThreadContext(mainThread, &context)) {
                context.Rip = entryPoint;
                if (SetThreadContext(mainThread, &context))
                    breakpoint = SuspendThread(mainThread) != static_cast<DWORD>(-1);
            }
            patchInstalled = false;
        }
        ContinueEvent(event);
        if (exited || breakpoint) break;
    }
    if (!breakpoint) {
        error = exited ? L"Game exited during startup." : L"Game entrypoint breakpoint timed out.";
        if (!exited) TerminateProcess(process, 1);
        DebugActiveProcessStop(info.dwProcessId);
        return false;
    }
    bool ok = LoadAll(process, info.dwProcessId, directory, true, error, cancel);
    if(ok&&odyssey)ok=LoadOdyssey(process,info.dwProcessId,directory,error,cancel,true);
    // Drain remaining loader/thread debug events before detaching.
    DEBUG_EVENT event{};
    while (WaitForDebugEvent(&event, 0)) ContinueEvent(event);
    if (!DebugActiveProcessStop(info.dwProcessId)) {
        const auto detachError = GetLastError();
        error += L" Could not detach startup loader: " + WindowsError(detachError); ok = false;
    }
    if (!ok || (cancel && cancel->load())) { TerminateProcess(process, 1); return false; }
    if (resume && ResumeThread(mainThread) == static_cast<DWORD>(-1)) {
        error = WindowsError(GetLastError()); TerminateProcess(process, 1); return false;
    }
    return true;
}
} // namespace titan::app
