#include <windows.h>
#include <psapi.h>
#include <sr/sense/headtracker/headtracker.h>
#include <sr/sense/headtracker/headposetracker.h>
#include <sr/sense/eyetracker/eyetracker.h>
#include <algorithm>
#include <array>
#include <cmath>
#include <fstream>
#include <iomanip>
#include <iostream>
#include <mutex>
#include <string>
#include <vector>

// This observer connects only to the existing SR service. It never opens cameras,
// creates a display/lens/weaver, or writes configuration.
template<class T, class Listener> struct Collector : Listener {
    std::mutex mutex;
    std::vector<T> frames;
    void accept(const T& f) override {
        std::lock_guard lock(mutex);
        if (frames.size() < 5000) frames.push_back(f);
    }
    std::vector<T> take() { std::lock_guard lock(mutex); return frames; }
    void clear() { std::lock_guard lock(mutex); frames.clear(); }
};
struct Stat {
    size_t n=0, invalid=0;
    double lo=INFINITY, hi=-INFINITY, mean=0, m2=0;
    void add(double x) {
        if (!std::isfinite(x)) { ++invalid; return; }
        ++n; lo=std::min(lo,x); hi=std::max(hi,x);
        double d=x-mean; mean+=d/n; m2+=d*(x-mean);
    }
    double sd() const { return n?std::sqrt(m2/n):INFINITY; }
    void json(std::ostream& o) const {
        o<<"{\"finite\":"<<n<<",\"invalid\":"<<invalid;
        if(n) o<<",\"min\":"<<lo<<",\"max\":"<<hi<<",\"mean\":"<<mean<<",\"sd\":"<<sd();
        o<<"}";
    }
};
double distance(SR_point3d a, SR_point3d b) {
    return std::hypot(a.x-b.x,a.y-b.y,a.z-b.z);
}
template<class T> void timing(std::ostream& o,const std::vector<T>& f) {
    size_t advances=0,timeAdvances=0; Stat dt;
    for(size_t i=1;i<f.size();++i) {
        advances+=f[i].frameId>f[i-1].frameId;
        timeAdvances+=f[i].time>f[i-1].time;
        if(f[i].time>f[i-1].time) dt.add(double(f[i].time-f[i-1].time));
    }
    o<<"\"callbacks\":"<<f.size()<<",\"frame_advances\":"<<advances<<",\"time_advances\":"<<timeAdvances<<",\"dt_us\":";
    dt.json(o);
    if(!f.empty()) o<<",\"first_frame\":"<<f.front().frameId<<",\"last_frame\":"<<f.back().frameId
        <<",\"first_time_us\":"<<f.front().time<<",\"last_time_us\":"<<f.back().time;
}
void vecStats(std::ostream& o,const std::array<Stat,3>& v) {
    o<<"[";for(int i=0;i<3;++i){if(i)o<<",";v[i].json(o);}o<<"]";
}
void add(std::array<Stat,3>& s,SR_point3d p) { for(int i=0;i<3;++i)s[i].add(p.p[i]); }
SR_point3d rotate(SR_point3d p,int axis,double a) {
    const int j=(axis+1)%3,k=(axis+2)%3;
    const double v=p.p[j],w=p.p[k],c=std::cos(a),s=std::sin(a);
    p.p[j]=c*v-s*w;p.p[k]=s*v+c*w;return p;
}
SR_point3d rotateVector(SR_point3d p,SR_point3d v) {
    double a=std::hypot(v.x,v.y,v.z);
    if(a<1e-12)return p;
    v=v/a;const double c=std::cos(a),s=std::sin(a),d=v.x*p.x+v.y*p.y+v.z*p.z;
    SR_point3d cross{v.y*p.z-v.z*p.y,v.z*p.x-v.x*p.z,v.x*p.y-v.y*p.x};
    return p*c+cross*s+v*(d*(1-c));
}
void rotationVectorFit(std::ostream& o,const std::vector<SR_head>& frames) {
    int bestSigns=0;double best=INFINITY;std::array<std::array<Stat,3>,4> bestStats{};
    for(int signs=0;signs<8;++signs){
        std::array<std::array<Stat,3>,4>s{};
        for(auto&f:frames){
            auto v=f.headPose.orientation;
            for(int a=0;a<3;++a)v.p[a]*=(signs&(1<<a))?1:-1;
            SR_point3d points[]={f.eyes.left,f.eyes.right,f.ears.left,f.ears.right};
            for(int p=0;p<4;++p)add(s[p],rotateVector(points[p]-f.headPose.position,v));
        }
        double score=0;for(int p=2;p<4;++p)for(auto&axis:s[p])score+=axis.sd()*axis.sd();
        if(score<best){best=score;bestSigns=signs;bestStats=s;}
    }
    o<<"{\"hypothesis\":\"Rodrigues rotation vector\"";
    if(std::isfinite(best)){
        o<<",\"signs\":[";for(int a=0;a<3;++a){if(a)o<<",";o<<((bestSigns&(1<<a))?1:-1);}o<<"],\"ear_combined_variance_mm2\":"<<best;
        const char*names[]={"left_eye_local_mm","right_eye_local_mm","left_ear_local_mm","right_ear_local_mm"};
        for(int p=0;p<4;++p){o<<",\""<<names[p]<<"\":";vecStats(o,bestStats[p]);}
    }
    o<<"}";
}
// Test conventions rather than assume SDK's Euler multiplication order.
void localFits(std::ostream& o,const std::vector<SR_head>& frames,bool earOnly=false) {
    std::array<int,3> order{0,1,2},bestOrder{};
    int bestSigns=0; double best=INFINITY;
    std::array<std::array<Stat,3>,4> bestStats{};
    do {
        for(int signs=0;signs<8;++signs) {
            std::array<std::array<Stat,3>,4> s{};
            for(auto& f:frames) {
                SR_point3d points[]={f.eyes.left,f.eyes.right,f.ears.left,f.ears.right};
                for(int p=0;p<4;++p) {
                    auto q=points[p]-f.headPose.position;
                    for(int a:order)q=rotate(q,a,f.headPose.orientation.p[a]*((signs&(1<<a))?1:-1));
                    add(s[p],q);
                }
            }
            double score=0;for(int pi=earOnly?2:0;pi<4;++pi)for(auto& axis:s[pi])score+=axis.sd()*axis.sd();
            if(score<best){best=score;bestOrder=order;bestSigns=signs;bestStats=s;}
        }
    }while(std::next_permutation(order.begin(),order.end()));
    o<<"{\"convention_search_only\":true";
    if(std::isfinite(best)) {
        o<<",\"axis_application_order\":["<<bestOrder[0]<<","<<bestOrder[1]<<","<<bestOrder[2]<<"],\"signs\":[";
        for(int a=0;a<3;++a){if(a)o<<",";o<<((bestSigns&(1<<a))?1:-1);}o<<"],\"combined_variance_mm2\":"<<best;
        const char* names[]={"left_eye_local_mm","right_eye_local_mm","left_ear_local_mm","right_ear_local_mm"};
        for(int p=0;p<4;++p){o<<",\""<<names[p]<<"\":";vecStats(o,bestStats[p]);}
    }
    o<<"}";
}
bool verifyModules(std::ostream& o) {
    HMODULE mods[1024]; DWORD bytes=0; bool ok=true;
    EnumProcessModules(GetCurrentProcess(),mods,sizeof(mods),&bytes);
    for(unsigned i=0;i<bytes/sizeof(HMODULE);++i) {
        wchar_t path[32768];GetModuleFileNameW(mods[i],path,32768);
        std::wstring p(path),lower=p;std::transform(lower.begin(),lower.end(),lower.begin(),towlower);
        if(lower.find(L"simulatedreality")!=std::wstring::npos||lower.find(L"titan")!=std::wstring::npos) {
            std::string s(p.begin(),p.end());o<<s<<"\n";
            if(lower.find(L"titan")!=std::wstring::npos||lower.find(L"c:\\program files\\leiasr\\platform\\bin\\")!=0)ok=false;
        }
    }
    return ok;
}
int main(int argc,char** argv) {
    const std::string base=argc>1?argv[1]:".";
    std::ofstream log(base+"/modules-and-status.log");
    log<<std::unitbuf;
    if(!verifyModules(log)){log<<"Rejected non-vendor module\n";return 2;}
    Collector<SR_head,SR::HeadListener> h;
    Collector<SR_headPose,SR::HeadPoseListener> p;
    Collector<SR_eyePair,SR::EyePairListener> e;
    SR::SRContext* context=nullptr;
    try {
        context=SR::SRContext::create(false,SR::SRContext::NonBlockingClientMode);
        log<<"Connected as NonBlockingClientMode, lensPreference=false. No Display/Lens/Camera/Weaver created.\n";
        auto ht=SR::HeadTracker::create(*context);
        auto pt=SR::HeadPoseTracker::create(*context);
        auto et=SR::EyeTracker::create(*context);
        auto hs=ht->openHeadStream(&h);
        auto ps=pt->openHeadPoseStream(&p);
        auto es=et->openEyePairStream(&e);
        context->initialize();
        log<<"Discarding 2-second subscription warm-up.\n";
        Sleep(2000); h.clear();p.clear();e.clear();
        log<<"Subscribed; observing 10 seconds.\n";
        Sleep(10000);
        hs.reset();ps.reset();es.reset();
        verifyModules(log);
        SR::SRContext::deleteSRContext(context);context=nullptr;
        log<<"Observer unsubscribed and context destroyed.\n";
    } catch(const std::exception& x) {
        log<<"SDK exception: "<<x.what()<<"\n";
        if(context)SR::SRContext::deleteSRContext(context);
    }
    auto heads=h.take();auto poses=p.take();auto eyes=e.take();
    std::ofstream out(base+"/live-aggregate.json");out<<std::setprecision(12);
    out<<"{\"sdk\":\"1.34.10\",\"warmup_seconds\":2,\"duration_seconds\":10,\"head\":{";
    timing(out,heads);std::array<Stat,3> position{},orientation{};Stat ipd,ears,radius[4];
    size_t metadataMatches=0,zeroEyeDistance=0;
    Stat earMidpoint;
    for(auto& f:heads) {
        add(position,f.headPose.position);add(orientation,f.headPose.orientation);
        ipd.add(distance(f.eyes.left,f.eyes.right));ears.add(distance(f.ears.left,f.ears.right));
        zeroEyeDistance+=distance(f.eyes.left,f.eyes.right)<0.000001;
        earMidpoint.add(distance((f.ears.left+f.ears.right)/2,f.headPose.position));
        SR_point3d points[]={f.eyes.left,f.eyes.right,f.ears.left,f.ears.right};
        for(int i=0;i<4;++i)radius[i].add(distance(points[i],f.headPose.position));
        metadataMatches+=(f.frameId==f.headPose.frameId &&f.frameId==f.eyes.frameId&&f.frameId==f.ears.frameId
            &&f.time==f.headPose.time&&f.time==f.eyes.time&&f.time==f.ears.time);
    }
    out<<",\"nested_frame_time_matches\":"<<metadataMatches<<",\"position_mm\":";vecStats(out,position);
    out<<",\"orientation_radians\":";vecStats(out,orientation);
    out<<",\"eye_distance_mm\":";ipd.json(out);out<<",\"zero_eye_distance_samples\":"<<zeroEyeDistance;
    out<<",\"ear_distance_mm\":";ears.json(out);
    out<<",\"ear_midpoint_head_distance_mm\":";earMidpoint.json(out);
    out<<",\"point_distance_from_head_origin_mm\":[";
    for(int i=0;i<4;++i){if(i)out<<",";radius[i].json(out);}out<<"],\"local_fit_all_points\":";localFits(out,heads);
    out<<",\"local_fit_ears_only\":";localFits(out,heads,true);
    out<<",\"rotation_vector_fit_ears_only\":";rotationVectorFit(out,heads);
    out<<"},\"headPose\":{";timing(out,poses);position={};orientation={};
    for(auto&f:poses){add(position,f.position);add(orientation,f.orientation);}
    out<<",\"position_mm\":";vecStats(out,position);out<<",\"orientation_radians\":";vecStats(out,orientation);
    out<<"},\"eyePair\":{";timing(out,eyes);ipd={};
    std::array<Stat,3>left{},right{};
    for(auto&f:eyes){ipd.add(distance(f.left,f.right));add(left,f.left);add(right,f.right);}
    out<<",\"left_mm\":";vecStats(out,left);out<<",\"right_mm\":";vecStats(out,right);
    out<<",\"eye_distance_mm\":";ipd.json(out);out<<"}}\n";
    log<<"Callbacks: Head="<<heads.size()<<" HeadPose="<<poses.size()<<" EyePair="<<eyes.size()<<"\n";
    return heads.empty()&&poses.empty()&&eyes.empty()?3:0;
}
