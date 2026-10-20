#pragma once
#include "../common/SharedProtocol.h"
#include <filesystem>
#include <atomic>
#include <functional>
#include <memory>
#include <string>

namespace titan::broker {
using LogFunction = std::function<void(const std::wstring&)>;

struct ProviderOptions {
    std::filesystem::path vendorDirectory;
    std::filesystem::path logDirectory;
    bool mock{};
    std::shared_ptr<std::atomic<unsigned>> predictionMs;
    std::shared_ptr<std::atomic<int>> horizontalTenths;
};

// All calls, including destruction, belong to the broker worker thread.
// No vendor code is loaded or called in mock mode.
class DeviceProvider {
public:
    DeviceProvider(ProviderOptions options, LogFunction logger);
    ~DeviceProvider();
    DeviceProvider(const DeviceProvider&) = delete;
    DeviceProvider& operator=(const DeviceProvider&) = delete;
    bool Initialize();
    void Poll();
    bool SetLens(bool enabled);
    void Shutdown();
    const Snapshot& State() const noexcept;
private:
    struct Impl;
    std::unique_ptr<Impl> impl_;
};
} // namespace titan::broker
