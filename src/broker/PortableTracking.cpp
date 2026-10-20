#include "PortableTracking.h"
#include <cmath>
#include <fstream>
#include <iomanip>
#include <limits>
#include <locale>

namespace titan::broker {
namespace {
constexpr CLSID kTracker{0xd98a272f, 0xde1c, 0x4e73,
    {0xa1, 0xd0, 0xa9, 0x3a, 0x8f, 0x06, 0x8b, 0x4b}};
}
PortableTracking::~PortableTracking() { Reset(); }
bool PortableTracking::Activate(const std::filesystem::path& directory, std::wstring& error) {
    Reset();
    directory_ = std::filesystem::absolute(directory);
    const auto executable = directory_ / L"FaceTracking.exe";
    const auto dll = directory_ / L"FaceTrackingManagerService.dll";
    if (!std::filesystem::is_regular_file(executable) || !std::filesystem::is_regular_file(dll)) {
        error = L"便携眼追文件缺失，请完整解压新测试包，保留 vendor/eyetracking 目录。"; return false;
    }
    // Read the same embedded manifest Windows uses for the real child process.
    ACTCTXW descriptor{sizeof(descriptor)};
    descriptor.dwFlags = ACTCTX_FLAG_RESOURCE_NAME_VALID;
    descriptor.lpSource = executable.c_str();
    descriptor.lpResourceName = MAKEINTRESOURCEW(1);
    context_ = CreateActCtxW(&descriptor);
    if (context_ == INVALID_HANDLE_VALUE || !ActivateActCtx(context_, &cookie_)) {
        error = L"无法激活便携眼追清单，Win32=" + std::to_wstring(GetLastError()); Reset(); return false;
    }
    return true;
}
HRESULT PortableTracking::Create(IDispatch** result) {
    if (!result) return E_POINTER;
    *result = nullptr;
    if (!cookie_) return E_UNEXPECTED;
    HRESULT hr = CoCreateInstance(kTracker, nullptr, CLSCTX_INPROC_SERVER, IID_IDispatch,
        reinterpret_cast<void**>(result));
    if (FAILED(hr)) return hr;
    // Never silently fall back to an installed COM copy: eye_info is shared
    // only by processes mapping the identical physical DLL.
    HMODULE module{};
    auto** vtable = *reinterpret_cast<void***>(*result);
    wchar_t path[32768]{};
    std::error_code error;
    if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(vtable[0]), &module) ||
        !GetModuleFileNameW(module, path, static_cast<DWORD>(std::size(path))) ||
        !std::filesystem::equivalent(path, directory_ / L"FaceTrackingManagerService.dll", error)) {
        (*result)->Release(); *result = nullptr; return HRESULT_FROM_WIN32(ERROR_BAD_PATHNAME);
    }
    return S_OK;
}
void PortableTracking::Reset() {
    if (cookie_) { DeactivateActCtx(0, cookie_); cookie_ = 0; }
    if (context_ != INVALID_HANDLE_VALUE) { ReleaseActCtx(context_); context_ = INVALID_HANDLE_VALUE; }
}

bool SaveCameraCalibration(const std::filesystem::path& file,
    const std::array<double, 9>& left, const std::array<double, 9>& right,
    const std::array<double, 10>& stereo, std::wstring& error) {
    for (const auto* values : {&left, &right})
        for (double value : *values) if (!std::isfinite(value)) { error = L"摄像头校准包含无效数值。"; return false; }
    for (double value : stereo) if (!std::isfinite(value)) { error = L"双目校准包含无效数值。"; return false; }
    if (left[0] <= 0 || left[1] <= 0 || right[0] <= 0 || right[1] <= 0) {
        error = L"摄像头校准焦距无效。"; return false;
    }
    const auto temporary = std::filesystem::path(file.wstring() + L".tmp");
    {
        std::ofstream out(temporary, std::ios::binary | std::ios::trunc);
        out.imbue(std::locale::classic());
        out << std::setprecision(std::numeric_limits<double>::max_digits10) << "[calibration_data]\n";
        auto row = [&](const char* key, const auto& values) {
            out << key << '=';
            for (std::size_t i = 0; i < values.size(); ++i) { if (i) out << ','; out << values[i]; }
            out << '\n';
        };
        row("E1", left); row("E2", right); row("E3", stereo);
        out.flush();
        if (!out) { out.close(); DeleteFileW(temporary.c_str()); error = L"无法写入便携眼追校准，请把整个程序解压到可写目录。"; return false; }
    }
    if (!MoveFileExW(temporary.c_str(), file.c_str(), MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH)) {
        error = L"无法替换便携眼追校准，Win32=" + std::to_wstring(GetLastError()); DeleteFileW(temporary.c_str()); return false;
    }
    return true;
}
}
