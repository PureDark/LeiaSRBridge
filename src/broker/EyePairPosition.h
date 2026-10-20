#pragma once
#include "../render/OpticalConstants.h"
#include <algorithm>
#include <cmath>

namespace titan::broker {
// BOE supplies two independently triangulated eye-contour centers. Resolve
// left/right in calibrated display coordinates, not from a landmark's name.
inline bool SelectEyePair(Snapshot& state, const float center[3],
    const float eyeA[3], const float eyeB[3], std::uint64_t sequence,
    std::uint64_t observed, std::uint64_t now, float predictionOffset) noexcept {
    state.eyePairValid = 0;
    state.eyePairSequence = sequence;
    state.eyePairObservationTick = observed;
    if (!sequence || !observed || observed > now || now-observed > 250 ||
        state.eyePositionValid != kEyeTracked || !std::isfinite(predictionOffset)) return false;
    render::OpticalConstants points[3]{};
    const float* raw[] = {center, eyeA, eyeB};
    for(unsigned i=0;i<3;++i){
        Snapshot point = state;
        for(unsigned j=0;j<3;++j){
            if(!std::isfinite(raw[i][j]) || std::abs(raw[i][j])>10000) return false;
            point.eye[j]=raw[i][j];
        }
        if(raw[i][2]<=0 || !render::MakeOpticalConstants(point,1,1,2,1,points[i]) ||
            points[i].eyeZ<=0) return false;
    }
    const double dx=points[2].eyeX-points[1].eyeX;
    const double dy=points[2].eyeY-points[1].eyeY;
    const double dz=points[2].eyeZ-points[1].eyeZ;
    const double distance=std::sqrt(dx*dx+dy*dy+dz*dz);
    // Reject coincident/implausible pairs and near-profile ordering ambiguity.
    if(distance<20 || distance>100 || std::abs(dx)<10) return false;
    const float* left=dx>0?eyeA:eyeB;
    const float* right=dx>0?eyeB:eyeA;
    std::copy_n(center,3,state.eyePairCenter);
    std::copy_n(left,3,state.eyePair[0]);
    std::copy_n(right,3,state.eyePair[1]);
    std::copy_n(left,3,state.rawEyePair[0]);
    std::copy_n(right,3,state.rawEyePair[1]);
    state.eyePairCenter[0]+=predictionOffset;
    state.eyePair[0][0]+=predictionOffset;
    state.eyePair[1][0]+=predictionOffset;
    state.eyePairValid=1;
    return true;
}
}
