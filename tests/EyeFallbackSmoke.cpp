#include "broker/EyePosition.h"
#include "render/OpticalConstants.h"
#include <cstdio>
#include <cstdlib>
#include <initializer_list>
#include <limits>

using namespace titan;
void Require(bool ok, const char* message) {
    if (!ok) { std::fprintf(stderr, "FAIL: %s\n", message); std::exit(1); }
}
int main() {
    Snapshot s{};
    s.status = ProviderStatus::Ready;
    s.calibrationValid = 1;
    s.refractParams[0] = 1; s.refractParams[1] = .15525; s.refractParams[2] = 1;
    s.wholeMat[3] = 12; s.wholeMat[4] = -5; s.wholeMat[5] = 8;
    broker::SelectEyePosition(s, S_OK, 0, 0, 0, 100);
    Require(s.eyePositionValid == kEyeDefault && s.eye[0] == 0 && s.eye[1] == 0 && s.eye[2] == 600,
        "startup without a face must provide default eye");
    render::OpticalConstants optics{};
    Require(render::MakeOpticalConstants(s, 2560, 1440, 5120, 1440, optics) &&
        optics.eyeX == 12 && optics.eyeY == -5 && optics.eyeZ == 608,
        "fallback must pass renderer admission and use monitor calibration");
    s.lensEnabled = 1;
    broker::SelectEyePosition(s, S_OK, 20, -10, 750, 200);
    Require(s.eyePositionValid == kEyeTracked && s.eye[0] == 20 && s.eye[1] == -10 &&
        s.eye[2] == 750 && s.observationTick == 200, "tracking must resume automatically");
    broker::SelectEyePosition(s, S_OK, 0, 0, 0, 300);
    Require(s.eyePositionValid == kEyeHeld && s.eye[0] == 20 && s.eye[1] == -10 && s.eye[2] == 750 && s.lensEnabled &&
        s.observationTick == 200 && !s.sensorFreshnessKnown, "brief loss must preserve last position and its observation time");
    broker::SelectEyePosition(s, E_FAIL, 20, -10, 750, 400);
    Require(s.eyePositionValid == kEyeHeld && s.observationTick == 200, "COM error must not renew held position");
    Require(render::MakeOpticalConstants(s, 2560, 1440, 5120, 1440, optics) &&
        optics.eyeX == 32 && optics.eyeY == -15 && optics.eyeZ == 758, "held eye must retain calibrated optical mapping");
    broker::SelectEyePosition(s, S_OK, 25, -12, 745, 450);
    Require(s.eyePositionValid == kEyeTracked && s.eye[0] == 25 && s.eye[2] == 745 && s.observationTick == 450,
        "valid recovery must be immediate without smoothing lag");
    broker::SelectEyePosition(s, S_OK, 0, 0, 0, 950);
    Require(s.eyePositionValid == kEyeHeld && s.observationTick == 450, "hold must last through 500 ms boundary");
    broker::SelectEyePosition(s, S_OK, 0, 0, 0, 951);
    Require(s.eyePositionValid == kEyeDefault && s.eye[2] == 600 && s.lensEnabled && !s.observationTick,
        "continuous loss must eventually fall back without renewing hold");
    for (float bad : {std::numeric_limits<float>::quiet_NaN(), std::numeric_limits<float>::infinity(), -1.f, 10001.f}) {
        broker::SelectEyePosition(s, S_OK, 0, 0, bad, 1000);
        Require(s.eyePositionValid == kEyeDefault, "invalid depth must fall back");
    }
    broker::SelectEyePosition(s, S_OK, std::numeric_limits<float>::quiet_NaN(), 0, 600, 1000);
    Require(s.eyePositionValid == kEyeDefault, "invalid lateral position must fall back");
    broker::SelectEyePosition(s, S_OK, 30, 10, 700, 1100);
    Require(s.eyePositionValid == kEyeTracked && s.eye[2] == 700, "reacquisition after failures");
    s.calibrationValid = 0;
    broker::SelectEyePosition(s, S_OK, 0, 0, 0, 1200);
    Require(!s.eyePositionValid && !render::MakeOpticalConstants(s, 2560, 1440, 5120, 1440, optics),
        "fallback must not bypass missing calibration");
    s.calibrationValid = 1; s.status = ProviderStatus::NoDevice;
    broker::SelectEyePosition(s, S_OK, 0, 0, 600, 1300);
    Require(!s.eyePositionValid, "fallback must not bypass device loss");
    s.status = ProviderStatus::Ready;
    broker::SelectEyePosition(s, S_OK, 0, 0, 0, 1400);
    Require(s.eyePositionValid == kEyeDefault, "device loss must discard held position");
    std::puts("PASS: no-face startup, calibrated optics, brief loss hold, immediate recovery, 500 ms timeout, invalid samples, device/calibration gates.");
}
