#pragma once
#include "../common/SharedProtocol.h"
#include <algorithm>
#include <array>
#include <cmath>

namespace titan::broker {
// Uses local coordinate-change observation times, NOT camera exposure times.
// Horizontal prediction; neither calibration nor raw Y/Z is adjusted.
class EyePrediction {
public:
    float Apply(Snapshot& state, double nowMs, unsigned horizonMs) noexcept {
        horizonMs = std::min(horizonMs, 40u);
        if (horizonMs != horizon_) { Reset(); horizon_ = horizonMs; }
        if (!horizonMs || state.status != ProviderStatus::Ready || !state.calibrationValid ||
            state.eyePositionValid != kEyeTracked || !std::isfinite(nowMs)) {
            Reset(); return 0;
        }
        for (float v : state.eye) if (!std::isfinite(v)) { Reset(); return 0; }
        if (count_ && (nowMs < lastPollMs_ || nowMs - lastPollMs_ > 100)) Reset();
        lastPollMs_ = nowMs;
        const bool changed = !count_ || state.eye[0] != lastRaw_[0] ||
            state.eye[1] != lastRaw_[1] || state.eye[2] != lastRaw_[2];
        if (changed) {
            if (count_) {
                const double dt = nowMs - samples_[count_ - 1].time;
                const double dx = state.eye[0] - lastRaw_[0];
                // Discontinuity or a clear reversal: discard the old velocity.
                if (dt <= 0 || dt > 100 || std::abs(dx) > 25 || std::abs(dx) > dt * 1.5 ||
                    (std::abs(dx) > .3 && dx * velocity_ < 0)) Reset();
            }
            while (count_ && (count_ == samples_.size() || nowMs - samples_[0].time > 50)) {
                for (size_t i = 1; i < count_; ++i) samples_[i - 1] = samples_[i];
                --count_;
            }
            samples_[count_++] = {nowMs, state.eye[0]};
            std::copy_n(state.eye, 3, lastRaw_.begin());
        }
        const double age = nowMs - samples_[count_ - 1].time;
        if (age >= 30) { Reset(); return 0; }
        if (count_ < 3 || samples_[count_ - 1].time - samples_[0].time < 12) return 0;
        double meanT{}, meanX{}, variance{}, covariance{};
        for (size_t i = 0; i < count_; ++i) {
            meanT += samples_[i].time - samples_[0].time;
            meanX += samples_[i].x;
        }
        meanT /= count_; meanX /= count_;
        for (size_t i = 0; i < count_; ++i) {
            const double t = samples_[i].time - samples_[0].time - meanT;
            variance += t * t; covariance += t * (samples_[i].x - meanX);
        }
        velocity_ = variance > 0 ? covariance / variance : 0; // mm/ms
        if (std::abs(samples_[count_ - 1].x - samples_[0].x) < .5) velocity_ = 0;
        // Fade to zero if changed observations stop, instead of predicting forever.
        const double fade = age <= 15 ? 1 : (30 - age) / 15;
        const float offset = static_cast<float>(std::clamp(velocity_ * (horizonMs + std::min(age, 15.0)), -10.0, 10.0) * fade);
        state.eye[0] += offset;
        return offset;
    }
private:
    struct Sample { double time{}, x{}; };
    void Reset() noexcept { count_ = 0; velocity_ = 0; }
    std::array<Sample, 6> samples_{};
    std::array<float, 3> lastRaw_{};
    size_t count_{};
    double lastPollMs_{}, velocity_{};
    unsigned horizon_{};
};
} // namespace titan::broker
