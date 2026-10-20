#include "broker/EyePrediction.h"
#include <cstdio>
#include <cstdlib>
#include <limits>

using namespace titan;
void Require(bool ok, const char* text) {
    if(!ok){std::fprintf(stderr,"FAIL: %s\n",text);std::exit(1);}
}
Snapshot Raw(float x, unsigned source=kEyeTracked) {
    Snapshot s{};s.status=ProviderStatus::Ready;s.calibrationValid=1;
    s.eyePositionValid=source;s.eye[0]=x;s.eye[1]=42;s.eye[2]=560;s.observationTick=123;
    return s;
}
int main() {
    broker::EyePrediction right,left;
    for(int i=0;i<10;++i){
        auto r=Raw(float(i)),l=Raw(float(-i));
        const float dr=right.Apply(r,i*10.,20),dl=left.Apply(l,i*10.,20);
        Require(std::abs(dr+dl)<.0001f,"left/right prediction must be symmetric");
        if(i>=2)Require(std::abs(dr-2)<.0001f,"100 mm/s movement at 20 ms must predict 2 mm");
        Require(r.eye[1]==42&&r.eye[2]==560&&!r.sensorFreshnessKnown&&r.observationTick==123,
            "prediction must not alter Y/Z or invent a camera timestamp");
    }
    auto repeat=Raw(9);
    Require(right.Apply(repeat,94,20)>2,"repeat read must extrapolate observation age without a new sample");
    repeat=Raw(9);right.Apply(repeat,106,20);
    repeat=Raw(9);Require(right.Apply(repeat,120,20)==0&&repeat.eye[0]==9,"unchanged coordinates must stop prediction after 30 ms");
    auto disabled=Raw(-10);
    Require(left.Apply(disabled,100,0)==0&&disabled.eye[0]==-10,"toggle off must publish raw position immediately");
    Require(left.Apply(disabled,104,20)==0,"toggle on must warm up from raw coordinates");
    broker::EyePrediction reverse;
    for(int i=0;i<6;++i){auto s=Raw(float(i));reverse.Apply(s,i*10.,20);}
    auto turn=Raw(4);
    Require(reverse.Apply(turn,60,20)==0&&turn.eye[0]==4,"clear direction reversal must discard old forward velocity");
    for(int i=0;i<3;++i){auto s=Raw(3.f-i);reverse.Apply(s,70.+10*i,20);}
    auto loss=Raw(1,kEyeHeld);
    Require(reverse.Apply(loss,100,20)==0&&loss.eye[0]==1,"held coordinates must not be extrapolated");
    auto recover=Raw(10);
    Require(reverse.Apply(recover,110,20)==0,"recovery must not reuse pre-loss velocity");
    broker::EyePrediction cap;
    for(int i=0;i<8;++i){auto s=Raw(i*10.f);const float d=cap.Apply(s,i*10.,40);Require(d>=0&&d<=10,"correction bound");}
    auto jump=Raw(200);
    Require(cap.Apply(jump,80,40)==0,"outlier must reset velocity");
    auto bad=Raw(std::numeric_limits<float>::quiet_NaN());
    Require(cap.Apply(bad,90,40)==0,"non-finite input must reset prediction");
    broker::EyePrediction cadence;
    for(int poll=0;poll<100;++poll){
        const double t=poll*4.;const int frame=int(t/(1000./90));
        auto s=Raw(float(frame*(1000./90)*.1));
        const float correction=cadence.Apply(s,t,20);
        if(t>60)Require(std::abs(s.eye[0]-float((t+20)*.1))<.6f,"90 Hz data with repeated 4 ms reads must not amplify velocity");
        Require(std::isfinite(correction),"finite correction");
    }
    auto stopped=Raw(40);stopped.status=ProviderStatus::NoDevice;
    Require(cadence.Apply(stopped,404,20)==0,"device loss must disable prediction");
    std::puts("PASS: bidirectional motion, repeated polling, bounded correction, reversal, stop, toggle, loss/recovery, invalid input.");
}
