#pragma once
#include <Windows.h>
#include <oaidl.h>
#include <array>
#include <filesystem>
#include <string>

namespace titan::broker {
// Used and destroyed on the provider thread. No registry registration is made.
class PortableTracking {
public:
    ~PortableTracking();
    bool Activate(const std::filesystem::path& directory, std::wstring& error);
    HRESULT Create(IDispatch** result);
    void Reset();
private:
    HANDLE context_{INVALID_HANDLE_VALUE};
    ULONG_PTR cookie_{};
    std::filesystem::path directory_;
};
bool SaveCameraCalibration(const std::filesystem::path& file,
    const std::array<double, 9>& left, const std::array<double, 9>& right,
    const std::array<double, 10>& stereo, std::wstring& error);
}
