#pragma once
#include <sr/sense/headtracker/head.h>
#include <cmath>

namespace titan::legacy {
inline bool Finite(const SR_point3d& p) {
    return std::isfinite(p.x) && std::isfinite(p.y) && std::isfinite(p.z);
}
inline SR_point3d SafeCenter(SR_point3d p) {
    return Finite(p) && p.z > 0 ? p : SR_point3d{0, 0, 600};
}
inline SR_eyePair SafeEyes(SR_eyePair eyes, SR_point3d center) {
    if (Finite(eyes.left) && Finite(eyes.right)) return eyes;
    center = SafeCenter(center);
    eyes.left = {center.x - 32, center.y, center.z};
    eyes.right = {center.x + 32, center.y, center.z};
    eyes.time = 0; // A display fallback, not a measured camera sample.
    return eyes;
}
inline SR_head SafeHead(SR_head head, SR_point3d center) {
    if (!Finite(head.headPose.position)) {
        head.headPose.position = SafeCenter(center);
        head.headPose.time = head.time = 0;
    }
    if (!Finite(head.headPose.orientation)) {
        head.headPose.orientation = {0, 0, 0};
        head.headPose.time = 0;
    }
    head.eyes = SafeEyes(head.eyes, head.headPose.position);
    if (!Finite(head.ears.left) || !Finite(head.ears.right)) {
        const auto p = head.headPose.position;
        head.ears.left = {p.x - 70, p.y, p.z};
        head.ears.right = {p.x + 70, p.y, p.z};
        head.ears.time = 0;
    }
    return head;
}
}
