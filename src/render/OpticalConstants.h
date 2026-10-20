#pragma once
#include "../common/SharedProtocol.h"
#include <bit>
#include <cmath>
#include <cstdint>

namespace titan::render {
struct OpticalConstants {
    float pixel, placeHeight, pitch, angle;
    float offset, zAdd, ng, beta;
    float cr, eyeX, eyeY, eyeZ;
    std::uint32_t layout, textureLayout, width, height;
    std::uint32_t inputWidth, inputHeight, unusedWidth, unusedHeight;
};
static_assert(sizeof(OpticalConstants) == 80);

// Recovered from BOE_SetWholeTransform @ 0x18005EBD0 and the application
// @ 0x18005F160. The misleading IDA "log" thunks resolve to cosf/sinf.
inline bool MakeOpticalConstants(const Snapshot& snapshot, std::uint32_t width,
    std::uint32_t height, std::uint32_t inputWidth, std::uint32_t inputHeight,
    OpticalConstants& out) noexcept {
    if (!snapshot.calibrationValid || !snapshot.eyePositionValid ||
        snapshot.stereoMode > 1 || !width || !height || !inputWidth || !inputHeight)
        return false;
    for (double v : snapshot.wholeMat) if (!std::isfinite(v)) return false;
    for (double v : snapshot.refractParams) if (!std::isfinite(v)) return false;
    for (float v : snapshot.eye) if (!std::isfinite(v)) return false;
    float whole[6];
    for (unsigned i = 0; i != 6; ++i) whole[i] = static_cast<float>(snapshot.wholeMat[i]);
    const float ax = (whole[0] * 3.1415925f) / 180.0f;
    const float ay = (whole[1] * 3.1415925f) / 180.0f;
    const float az = (whole[2] * 3.1415925f) / 180.0f;
    const float cx = std::cos(ax), sx = std::sin(ax);
    const float cy = std::cos(ay), sy = std::sin(ay);
    const float cz = std::cos(az), sz = std::sin(az);
    const float m[9] = {
        cy * cz, -cy * sz, sy,
        cx * sz + sx * sy * cz, cx * cz - sx * sy * sz, -cy * sx,
        sx * sz - cx * sy * cz, sx * cz + cx * sy * sz, cx * cy
    };
    const float* eye = snapshot.eye;
    out = {};
    out.eyeX = ((m[0] * eye[0] + m[1] * eye[1]) + m[2] * eye[2]) + whole[3];
    out.eyeY = ((m[3] * eye[0] + m[4] * eye[1]) + m[5] * eye[2]) + whole[4];
    out.eyeZ = ((m[6] * eye[0] + m[7] * eye[1]) + m[8] * eye[2]) + whole[5];
    out.pitch = static_cast<float>(snapshot.refractParams[0]);
    out.pixel = static_cast<float>(snapshot.refractParams[1]);
    out.placeHeight = static_cast<float>(snapshot.refractParams[2]);
    out.angle = static_cast<float>(snapshot.refractParams[3]);
    out.offset = static_cast<float>(snapshot.refractParams[4]);
    out.zAdd = static_cast<float>(snapshot.refractParams[5]);
    out.ng = std::bit_cast<float>(1069673349u);
    out.beta = std::bit_cast<float>(1036831949u);
    out.cr = std::bit_cast<float>(1020054733u);
    out.layout = snapshot.stereoMode == 0 ? 1u : 0u;
    out.textureLayout = 2;
    out.width = width; out.height = height;
    out.inputWidth = inputWidth; out.inputHeight = inputHeight;
    return std::isfinite(out.eyeX) && std::isfinite(out.eyeY) && std::isfinite(out.eyeZ) &&
        std::isfinite(out.pitch) && std::isfinite(out.pixel) && std::isfinite(out.placeHeight) &&
        std::isfinite(out.angle) && std::isfinite(out.offset) && std::isfinite(out.zAdd) &&
        std::abs(out.pitch) > 1e-8f && std::abs(out.pixel) > 1e-8f &&
        std::abs(out.placeHeight) > 1e-8f;
}
} // namespace titan::render
