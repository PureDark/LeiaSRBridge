#pragma once
#include <Windows.h>
#include <atomic>
#include <filesystem>
#include <string>
#include <vector>

namespace titan::app {
struct ProcessInfo { DWORD pid{}; std::filesystem::path executable; };
std::vector<ProcessInfo> Processes();
bool InjectRuntime(DWORD pid, const std::filesystem::path& runtimeDirectory, std::wstring& error,
    const std::atomic<bool>* cancel = nullptr, bool odyssey = false);
// The game's PE entrypoint is held until compatibility DLLs are loaded.
// No executable or mod file is written, and the debugger is then detached.
bool LaunchWithRuntime(const std::filesystem::path& executable, const std::wstring& arguments,
    const std::filesystem::path& runtimeDirectory, DWORD& pid, std::wstring& error,
    const std::atomic<bool>* cancel = nullptr, bool odyssey = false);
// Caller owns handles; process must be created on this thread with DEBUG_ONLY_THIS_PROCESS, without CREATE_SUSPENDED.
bool PrepareCreatedProcess(const PROCESS_INFORMATION&,const std::filesystem::path&,std::wstring&,
    const std::atomic<bool>* cancel=nullptr,bool odyssey=true,bool resume=true);
std::wstring WindowsError(DWORD error);
} // namespace titan::app
