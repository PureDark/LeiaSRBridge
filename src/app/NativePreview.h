#pragma once
#include <functional>
#include <memory>
#include <string>

namespace titan::app {
struct NativePreviewState {
    bool connected{}, face{}, rotation{}, error{};
    int recommendedWidth{}, recommendedHeight{};
    double x{}, y{}, z{600}, pitch{}, yaw{}, roll{};
    std::wstring message=L"正在连接原厂 LeiaSR 服务…";
};
class NativePreview {
public:
    NativePreview();
    ~NativePreview();
    void Start(std::function<void(const std::wstring&)> log);
    void Stop();
    NativePreviewState Read() const;
private:
    struct Impl;
    std::unique_ptr<Impl> impl_;
};
}
