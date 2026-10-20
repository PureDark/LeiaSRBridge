#pragma once
#include "../common/BoeTrackingProtocol.h"
#include "../common/PoseMath.h"
#include "../render/OpticalConstants.h"
#include <algorithm>
#include <array>
#include <cstring>

namespace titan::broker {
// Public BOE result, no internal hooks or extra inference. Camera calibration
// layout verified at 180038D30/180039070; geometry at 1800AFB70.
class HeadPose {
public:
    bool Configure(const std::array<double,9>& left,const std::array<double,9>& right,
        const std::array<double,10>& stereo) {
        *this={};left_=left;right_=right;stereo_=stereo;
        for(const auto* c:{&left_,&right_}){
            for(auto v:*c)if(!std::isfinite(v))return false;
            if((*c)[0]<=0||(*c)[1]<=0)return false;
        }
        for(auto v:stereo_)if(!std::isfinite(v))return false;
        if(stereo_[0]<=0||std::abs(stereo_[3])<1||std::abs(stereo_[3])>1000)return false;
        rotations_[0]=pose::Rodrigues({stereo_[4],stereo_[5],stereo_[6]});
        rotations_[1]=pose::Rodrigues({stereo_[7],stereo_[8],stereo_[9]});
        return configured_=true;
    }
    // Distortion order is k1,k2,p1,p2,k3. Check forward reprojection after
    // iterative inversion; a divergent inversion must never become a pose.
    bool Rectify(unsigned camera,double u,double v,pose::Vec& out) const {
        if(!configured_||camera>1||!std::isfinite(u)||!std::isfinite(v))return false;
        const auto& c=camera?right_:left_;
        const double xd=(u-c[2])/c[0],yd=(v-c[3])/c[1];
        double x=xd,y=yd;
        for(unsigned i=0;i<12;++i){
            const double r2=x*x+y*y,radial=1+c[4]*r2+c[5]*r2*r2+c[8]*r2*r2*r2;
            if(!std::isfinite(radial)||std::abs(radial)<1e-6)return false;
            const double dx=2*c[6]*x*y+c[7]*(r2+2*x*x);
            const double dy=c[6]*(r2+2*y*y)+2*c[7]*x*y;
            x=(xd-dx)/radial;y=(yd-dy)/radial;
        }
        const double r2=x*x+y*y,radial=1+c[4]*r2+c[5]*r2*r2+c[8]*r2*r2*r2;
        const double checkX=(x*radial+2*c[6]*x*y+c[7]*(r2+2*x*x)-xd)*c[0];
        const double checkY=(y*radial+c[6]*(r2+2*y*y)+2*c[7]*x*y-yd)*c[1];
        if(!std::isfinite(checkX)||!std::isfinite(checkY)||std::hypot(checkX,checkY)>.25)return false;
        const auto ray=pose::Transform(rotations_[camera],{x,y,1});
        if(!std::isfinite(ray[2])||ray[2]<=1e-6)return false;
        out={stereo_[0]*ray[0]/ray[2]+stereo_[1],stereo_[0]*ray[1]/ray[2]+stereo_[2],1};
        return std::isfinite(out[0])&&std::isfinite(out[1]);
    }
    bool Solve(const BoeTrackingFrame& frame,const Snapshot& s,pose::Mat& result) const {
        if(!configured_||frame.sdkResult!=1||!frame.centerValid||!frame.facePointsValid||!s.calibrationValid)return false;
        // eye A, eye B, nose landmark 4, mouth landmarks 57 and 287.
        // Read-only subpixel copy verified against the public int result.
        const auto& image=frame.facePoints;
        std::array<pose::Vec,5> points{};
        double disparitySign=0;
        for(unsigned i=0;i<5;++i){
            pose::Vec a{},b{};
            for(unsigned cam=0;cam<2;++cam)for(auto pixel:image[cam][i])
                if(!std::isfinite(pixel)||pixel<0||pixel>16384)return false;
            if(!Rectify(0,image[0][i][0],image[0][i][1],a)||
                !Rectify(1,image[1][i][0],image[1][i][1],b))return false;
            const double disparity=a[0]-b[0];
            if(std::abs(disparity)<1||std::abs(a[1]-b[1])>3)return false;
            if(i==0)disparitySign=disparity;
            if(disparity*disparitySign<=0)return false;
            const double z=std::abs(stereo_[3])*stereo_[0]/std::abs(disparity);
            if(z<100||z>2500)return false;
            // BOE uses the midpoint of rectified camera rays, flips X/Y,
            // and keeps +Z toward the viewer. Do not add half a baseline.
            const pose::Vec camera{
                -(a[0]+b[0]-2*stereo_[1])*z/(2*stereo_[0]),
                -(a[1]+b[1]-2*stereo_[2])*z/(2*stereo_[0]),z};
            auto sample=s;
            for(unsigned axis=0;axis<3;++axis)sample.eye[axis]=static_cast<float>(camera[axis]);
            sample.eyePositionValid=kEyeTracked;
            render::OpticalConstants display{};
            if(!render::MakeOpticalConstants(sample,1,1,2,1,display))return false;
            points[i]={display.eyeX,display.eyeY,display.eyeZ};
        }
        return FromFace(points,result);
    }
    // A geometric head frame: X across eyes, Y from mouth toward eyes with
    // the X component removed, Z completing a right-handed frame. Pitch is
    // estimated from the facial plane, not assumed zero from two eyes.
    static bool FromFace(const std::array<pose::Vec,5>& p,pose::Mat& result){
        for(const auto& point:p)for(auto value:point)if(!std::isfinite(value))return false;
        auto x=pose::Sub(p[1],p[0]);
        const double ipd=pose::Length(x);
        if(ipd<20||ipd>100||std::abs(x[0])<10)return false;
        if(x[0]<0)x=pose::Scale(x,-1);
        pose::Unit(x);
        const auto eyes=pose::Scale(pose::Add(p[0],p[1]),.5);
        const auto mouth=pose::Scale(pose::Add(p[3],p[4]),.5);
        auto mouthAxis=pose::Sub(p[4],p[3]);
        const auto mouthWidth=pose::Length(mouthAxis);
        if(mouthWidth<15||mouthWidth>100||!pose::Unit(mouthAxis)||std::abs(pose::Dot(x,mouthAxis))<.8)return false;
        auto y=pose::Sub(eyes,mouth);
        y=pose::Sub(y,pose::Scale(x,pose::Dot(x,y)));
        const double height=pose::Length(y);
        if(height<20||height>110||!pose::Unit(y)||y[1]<.2)return false;
        const auto z=pose::Cross(x,y);
        if(z[2]<.2)return false; // reject profile/back-facing ambiguity
        const auto nose=pose::Sub(p[2],eyes);
        // Reject mismatched stereo points or severe expression/occlusion.
        if(std::abs(pose::Dot(nose,x))>ipd*.65||
            pose::Dot(nose,y)>height*.3||pose::Dot(nose,y)<-height*1.3||
            pose::Dot(nose,z)<-70||pose::Dot(nose,z)>20)return false;
        result={x[0],y[0],z[0],x[1],y[1],z[1],x[2],y[2],z[2]};
        return pose::IsRotation(result);
    }
    void Update(Snapshot& s,const BoeTrackingFrame* frame,std::uint64_t now) {
        s.headPoseValid=0;
        if(!frame||!frame->sequence||!frame->observationTick||frame->observationTick>now||
            now-frame->observationTick>250||s.eyePositionValid!=kEyeTracked){
            cachedValid_=false;lastSequence_=lastTick_=lastCreation_=0;lastPid_=0;return;
        }
        const bool sameProducer=frame->processId==lastPid_&&frame->processCreationTime==lastCreation_;
        if(!sameProducer||frame->sequence!=lastSequence_){
            cachedValid_=Solve(*frame,s,cached_);
            lastSequence_=frame->sequence;lastTick_=frame->observationTick;
            lastCreation_=frame->processCreationTime;lastPid_=frame->processId;
        }
        if(!cachedValid_)return;
        s.headPoseValid=1;s.headPoseObservationTick=lastTick_;s.headPoseSequence=lastSequence_;
        std::copy(cached_.begin(),cached_.end(),s.headRotation);
    }
private:
    bool configured_{},cachedValid_{};
    std::array<double,9> left_{},right_{};
    std::array<double,10> stereo_{};
    pose::Mat rotations_[2]{},cached_{};
    std::uint64_t lastSequence_{},lastTick_{},lastCreation_{};
    std::uint32_t lastPid_{};
};
}
