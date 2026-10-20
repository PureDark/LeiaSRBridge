#include "../src/broker/EyePairPosition.h"
#include <cstdio>
#include <stdexcept>
#include <limits>

using namespace titan;
static void Require(bool ok,const char* text){if(!ok)throw std::runtime_error(text);}
int main(){
    try{
        Snapshot s{};s.calibrationValid=1;s.eyePositionValid=kEyeTracked;
        s.refractParams[0]=1;s.refractParams[1]=.15;s.refractParams[2]=1;
        const float center[]{4,2,600},a[]{36,2,610},b[]{-28,2,590};
        Require(broker::SelectEyePair(s,center,a,b,3,1000,1050,7),"Valid pair rejected");
        Require(s.eyePair[0][0]==-21&&s.eyePair[1][0]==43&&s.eyePairCenter[0]==11,"Prediction/order incorrect");
        Require(s.rawEyePair[0][0]==-28&&s.rawEyePair[1][0]==36,"Raw eyes predicted");
        Require(s.eyePair[0][2]==590&&s.eyePair[1][2]==610,"Measured unequal depths discarded");
        Require(s.eyePairObservationTick==1000&&s.eyePairSequence==3,"Observation was renewed");
        // Ordering must use transformed display X, even when camera X flips.
        s.wholeMat[2]=180;
        Require(broker::SelectEyePair(s,center,a,b,4,1001,1050,0)&&s.eyePair[0][0]==36,"Calibration ignored in ordering");
        s.wholeMat[2]=0;
        Require(!broker::SelectEyePair(s,center,a,b,5,700,1050,0)&&!s.eyePairValid,"Stale pair accepted");
        Require(!broker::SelectEyePair(s,center,a,b,5,1100,1050,0),"Future pair accepted");
        Require(!broker::SelectEyePair(s,center,a,a,5,1000,1050,0),"Coincident pair accepted");
        const float bad[]{std::numeric_limits<float>::quiet_NaN(),0,600};
        Require(!broker::SelectEyePair(s,center,bad,b,5,1000,1050,0),"NaN pair accepted");
        s.eyePositionValid=kEyeHeld;
        Require(!broker::SelectEyePair(s,center,a,b,5,1000,1050,0),"Held optical state masqueraded as measurement");
        puts("Eye pair selection: calibration, unequal depth, prediction/raw, stale, invalid and hold passed.");
        return 0;
    }catch(const std::exception& e){fprintf(stderr,"%s\n",e.what());return 1;}
}
