#pragma once
#include "BoeTrackingProtocol.h"

namespace titan {
// Work object offsets are version-specific. The caller must verify the exact
// vendor SHA256 before supplying this pointer. Pure read; no cv::vector ABI calls.
// Called immediately after doEyeTrack while the proxy's call mutex is held.
inline bool CopyBoeFacePoints(const void* work,BoeTrackingFrame& frame) noexcept {
    frame.facePointsValid=0;
    if(!work||frame.sdkResult!=1||!frame.centerValid)return false;
    float copy[2][5][2]{};
    __try {
        for(unsigned cam=0;cam<2;++cam){
            std::uintptr_t vector[3]{};
            std::memcpy(vector,static_cast<const std::uint8_t*>(work)+168+cam*24,sizeof(vector));
            if(!vector[0]||vector[1]<vector[0]||vector[2]<vector[1]||
                vector[1]-vector[0]!=sizeof(copy[cam])||vector[2]-vector[0]>4096)return false;
            std::memcpy(copy[cam],reinterpret_cast<const void*>(vector[0]),sizeof(copy[cam]));
        }
    }__except(EXCEPTION_EXECUTE_HANDLER){return false;}
    for(unsigned cam=0;cam<2;++cam)for(unsigned p=0;p<5;++p)for(unsigned axis=0;axis<2;++axis){
        const float value=copy[cam][p][axis];
        std::int32_t integer{};
        std::memcpy(&integer,frame.raw+84+cam*40+p*8+axis*4,4);
        if(!std::isfinite(value)||value<0||value>16384||static_cast<int>(value)!=integer)return false;
    }
    std::memcpy(frame.facePoints,copy,sizeof(copy));
    frame.facePointsValid=1;return true;
}
}
