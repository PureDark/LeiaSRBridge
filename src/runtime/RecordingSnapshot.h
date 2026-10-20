#pragma once
#include "common/SharedProtocol.h"
#include <d3d12.h>
#include <string>

namespace titan {
// A successful command-list Reset starts a new recording generation. The first
// valid optical snapshot is then reused by every weave in that generation.
// Live broker validity is checked by the caller before using this cache.
bool SelectRecordingSnapshot(ID3D12GraphicsCommandList* list, const Snapshot* live,
    Snapshot& selected, bool& haveOpticalSnapshot, std::string& error);
bool InspectRecordingSnapshot(ID3D12GraphicsCommandList* list, Snapshot& snapshot,
    std::uint64_t& generation) noexcept;
}
