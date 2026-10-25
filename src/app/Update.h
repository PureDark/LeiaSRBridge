#pragma once
#include <Windows.h>
#include <filesystem>
#include <nlohmann/json.hpp>

namespace titan::app {
// PowerShell runs the bundled updater out of process; UI polling never blocks tracking.
class Update {
public:
    ~Update();
    void Check(const std::filesystem::path& directory, bool mock);
    void Download();
    bool Install();
    void Poll();
    void Stop();
    nlohmann::json State() const { return state_; }
private:
    bool Launch(const wchar_t* mode);
    void Error(const std::string& text);
    std::filesystem::path job_;
    HANDLE process_{};
    bool handedOff_{};
    nlohmann::json state_{{"phase","idle"},{"message","点击检查 GitHub 最新正式版本。"}};
};
}
