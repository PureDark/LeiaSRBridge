#include "runtime/LegacyTrackingFallback.h"
#include <limits>
#include <stdexcept>
#include <iostream>
void Require(bool condition,const char* message){if(!condition)throw std::runtime_error(message);}
int main(){try{
    using namespace titan::legacy;
    const auto n=std::numeric_limits<double>::quiet_NaN();
    SR_head head{};head.frameId=17;head.time=123;
    head.headPose.position=head.headPose.orientation={n,n,n};
    head.eyes.left=head.eyes.right=head.ears.left=head.ears.right={n,n,n};
    auto safe=SafeHead(head,{8,10,600});
    Require(Finite(safe.headPose.position)&&Finite(safe.headPose.orientation)&&Finite(safe.eyes.left)&&Finite(safe.eyes.right)&&Finite(safe.ears.left)&&Finite(safe.ears.right),"NaN reached legacy camera");
    Require(safe.headPose.position.x==8&&safe.eyes.left.x==-24&&safe.eyes.right.x==40,"fallback lost broker center");
    Require(safe.frameId==17&&safe.time==0&&safe.eyes.time==0&&safe.headPose.time==0,"fallback masquerades as sensor sample");
    head=safe;head.time=123;head.headPose.time=124;head.eyes.time=125;head.ears.time=126;
    safe=SafeHead(head,{99,99,999});
    Require(safe.time==123&&safe.headPose.time==124&&safe.eyes.time==125&&safe.ears.time==126&&safe.eyes.left.x==head.eyes.left.x,"valid measured data changed");
    head.eyes.left.y=n;safe=SafeHead(head,{0,0,600});
    Require(Finite(safe.eyes.left)&&safe.eyes.time==0,"partial invalid eye accepted");
    Require(SafeCenter({0,0,-1}).z==600&&SafeCenter({n,0,0}).z==600,"invalid fallback depth");
    std::cout<<"PASS legacy no-face, partial-invalid, finite default, measured-frame preservation and synthetic timestamps\n";
}catch(const std::exception& e){std::cerr<<e.what()<<'\n';return 1;}}
