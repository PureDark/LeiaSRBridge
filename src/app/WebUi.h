#pragma once
#include <Windows.h>
#include <objbase.h>
#include <WebView2.h>
#include <wrl.h>
#include <filesystem>
#include <functional>
#include <string>

namespace titan::app {
class WebUi {
public:
    using Command = std::function<void(const std::wstring&)>;
    using Error = std::function<void(const std::wstring&)>;
    void Start(HWND window, const std::filesystem::path& cache, Command command, Error error);
    void Resize();
    void Close();
    void Post(const std::wstring& json);
    void Script(const std::wstring& script);
    void Capture(const std::filesystem::path& file);
private:
    HWND window_{};
    Microsoft::WRL::ComPtr<ICoreWebView2Controller> controller_;
    Microsoft::WRL::ComPtr<ICoreWebView2> view_;
    Command command_;
    Error error_;
};
}

