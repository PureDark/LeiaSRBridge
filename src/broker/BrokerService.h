#pragma once
#include "DeviceProvider.h"
#include <memory>

namespace titan::broker {
class BrokerService {
public:
    BrokerService();
    ~BrokerService();
    BrokerService(const BrokerService&) = delete;
    BrokerService& operator=(const BrokerService&) = delete;
    bool Start(const ProviderOptions& options, LogFunction logger);
    void Stop();
    Snapshot ReadStatus() const;
private:
    struct Impl;
    std::unique_ptr<Impl> impl_;
};
} // namespace titan::broker
