#pragma once
#include "../common/SharedProtocol.h"
#include <cmath>

namespace titan::broker {
inline constexpr std::uint64_t kEyeHoldMs = 500;
// Positions use the vendor camera coordinate system in millimetres; the
// renderer still applies this monitor's wholeMat calibration to the fallback.
inline void SelectEyePosition(Snapshot& state, HRESULT result, float x, float y, float z,
                              std::uint64_t observationTick) noexcept {
    state.sensorFreshnessKnown = 0;
    if (state.status != ProviderStatus::Ready || !state.calibrationValid) {
        state.eyePositionValid = 0;
        state.observationTick = 0;
        return;
    }
    const bool tracked = SUCCEEDED(result) && std::isfinite(x) && std::isfinite(y) &&
        std::isfinite(z) && z > 0 && z <= 10000 && std::abs(x) <= 10000 && std::abs(y) <= 10000;
    // Keep the last successful observation through brief detector dropouts.
    // Never renew observationTick while held, or loss could persist forever.
    if (!tracked && (state.eyePositionValid == kEyeTracked || state.eyePositionValid == kEyeHeld) &&
        observationTick >= state.observationTick && observationTick - state.observationTick <= kEyeHoldMs) {
        state.eyePositionValid = kEyeHeld;
        return;
    }
    state.eyePositionValid = tracked ? kEyeTracked : kEyeDefault;
    state.eye[0] = tracked ? x : 0.0f;
    state.eye[1] = tracked ? y : 0.0f;
    state.eye[2] = tracked ? z : 600.0f;
    // A fallback has no tracker observation. Do not invent a sensor timestamp.
    state.observationTick = tracked ? observationTick : 0;
}
} // namespace titan::broker
