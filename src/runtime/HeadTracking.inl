// Included inside namespace SR, using the original SDK declarations/layouts.
namespace {
SR_point3d UnknownPoint() {
    const double n=std::numeric_limits<double>::quiet_NaN();
    return {n,n,n};
}
bool FreshObservation(uint64_t observation,uint64_t now) {
    return observation&&observation<=now&&now-observation<=250;
}
uint64_t ObservationTime(uint64_t observation,uint64_t now) {
    // Local arrival time, NEVER a camera exposure timestamp.
    FILETIME time{};GetSystemTimePreciseAsFileTime(&time);
    ULARGE_INTEGER ticks{};ticks.LowPart=time.dwLowDateTime;ticks.HighPart=time.dwHighDateTime;
    const uint64_t epochUs=(ticks.QuadPart-116444736000000000ULL)/10;
    const auto age=now-observation;
    return epochUs>age*1000?epochUs-age*1000:0;
}
bool DisplayPoint(const titan::Snapshot& s,const float* point,SR_point3d& result) {
    auto transformed=s;std::copy_n(point,3,transformed.eye);
    titan::render::OpticalConstants optical{};
    if(!titan::render::MakeOpticalConstants(transformed,1,1,2,1,optical)||optical.eyeZ<=0)return false;
    // Same wholeMat as optical weaving: +right/+up/+viewer, millimetres.
    result={optical.eyeX,optical.eyeY,optical.eyeZ};return true;
}
bool MeasuredPair(const titan::Snapshot& s,bool usable,uint64_t now,bool raw,
    SR_eyePair& pair,SR_point3d& center) {
    if(!usable||s.eyePositionValid!=titan::kEyeTracked||!s.eyePairValid||
        !FreshObservation(s.eyePairObservationTick,now))return false;
    const auto& eyes=raw?s.rawEyePair:s.eyePair;
    SR_point3d left{},right{};
    if(!DisplayPoint(s,s.eyePairCenter,center)||!DisplayPoint(s,eyes[0],left)||
        !DisplayPoint(s,eyes[1],right))return false;
    const double dx=right.x-left.x,dy=right.y-left.y,dz=right.z-left.z;
    const double length=std::sqrt(dx*dx+dy*dy+dz*dz);
    if(!std::isfinite(length)||length<1e-6)return false;
    pair.left=left;pair.right=right;pair.time=ObservationTime(s.eyePairObservationTick,now);
    return true;
}
SR_head TrackingFrame(const titan::Snapshot& s,bool usable,uint64_t sequence,bool raw=false) {
    SR_head frame{};frame.frameId=frame.headPose.frameId=frame.eyes.frameId=frame.ears.frameId=sequence;
    frame.headPose.position=frame.headPose.orientation=UnknownPoint();
    frame.eyes.left=frame.eyes.right=frame.ears.left=frame.ears.right=UnknownPoint();
    const auto now=GetTickCount64();SR_point3d center{};
    if(MeasuredPair(s,usable,now,raw,frame.eyes,center)) {
        frame.headPose.position=center;
        frame.time=frame.headPose.time=frame.ears.time=frame.eyes.time;
        const auto& left=frame.eyes.left;const auto& right=frame.eyes.right;
        const double dx=right.x-left.x,dy=right.y-left.y,dz=right.z-left.z;
        const double scale=70.0/std::sqrt(dx*dx+dy*dy+dz*dz);
        // Leia's 140 mm ear model, aligned to the measured eye axis. This is an
        // estimate, not detected ears or a complete head orientation (pitch).
        frame.ears.left={center.x-dx*scale,center.y-dy*scale,center.z-dz*scale};
        frame.ears.right={center.x+dx*scale,center.y+dy*scale,center.z+dz*scale};
    } else if(usable&&s.eyePositionValid==titan::kEyeTracked&&FreshObservation(s.observationTick,now)&&
        DisplayPoint(s,s.eye,center)) {
        // Retain position-only COM support when the richer result is absent.
        frame.headPose.position=center;
        frame.time=frame.headPose.time=ObservationTime(s.observationTick,now);
    }
    if(usable&&s.eyePositionValid==titan::kEyeTracked&&s.headPoseValid&&
        FreshObservation(s.headPoseObservationTick,now)&&std::isfinite(frame.headPose.position.x)) {
        titan::pose::Mat rotation{};
        std::copy_n(s.headRotation,9,rotation.begin());
        if(titan::pose::IsRotation(rotation)){
            const auto r=titan::pose::RotationVector(rotation);
            frame.headPose.orientation={r[0],r[1],r[2]};
            // Rodrigues rotation vector in radians, matching the inspected Leia
            // implementation. Keep the pose's own time, never renew stale angles
            // with a newer COM position or eye-pair observation.
            frame.headPose.time=ObservationTime(s.headPoseObservationTick,now);
            const auto& p=frame.headPose.position;
            frame.ears.left={p.x-70*rotation[0],p.y-70*rotation[3],p.z-70*rotation[6]};
            frame.ears.right={p.x+70*rotation[0],p.y+70*rotation[3],p.z+70*rotation[6]};
            frame.ears.time=frame.headPose.time;
        }
    }
    // SDK clients use these values directly in projection/camera transforms.
    // Keep broker measurement flags honest, but supply a usable display pose
    // whenever weaving can continue (held/default, stale or partial tracking).
    // Synthetic components retain time=0 instead of inventing sensor samples.
    if(usable) {
        SR_point3d fallback{0,0,600};
        DisplayPoint(s,s.eye,fallback);
        frame=titan::legacy::SafeHead(frame,fallback);
    }
    return frame;
}
bool SamePoint(const SR_point3d& a,const SR_point3d& b) {
    const auto same=[](double x,double y){return x==y||(std::isnan(x)&&std::isnan(y));};
    return same(a.x,b.x)&&same(a.y,b.y)&&same(a.z,b.z);
}
bool SameTracking(const SR_head& a,const SR_head& b) {
    return SamePoint(a.headPose.position,b.headPose.position)&&SamePoint(a.headPose.orientation,b.headPose.orientation)&&
        SamePoint(a.eyes.left,b.eyes.left)&&SamePoint(a.eyes.right,b.eyes.right)&&
        SamePoint(a.ears.left,b.ears.left)&&SamePoint(a.ears.right,b.ears.right);
}
template<class Listener> struct ListenerSlot {
    std::recursive_mutex mutex;
    Listener* listener{};
    explicit ListenerSlot(Listener* value):listener(value){}
    void Close(){std::lock_guard lock(mutex);listener=nullptr;}
    template<class Frame> void Update(const Frame& frame){
        std::lock_guard lock(mutex);
        if(listener)listener->accept(frame);
    }
};
}
class HeadStream::Impl : public ListenerSlot<HeadListener> {public:using ListenerSlot::ListenerSlot;};
HeadStream::HeadStream(HeadTracker*,HeadListener* listener):pimpl(std::make_unique<Impl>(listener)){}
HeadStream::~HeadStream(){close();}
void HeadStream::stopListening(){pimpl->Close();}
void HeadStream::close(){stopListening();}
void HeadStream::update(Frame& frame){pimpl->Update(frame);}
class HeadPoseStream::Impl : public ListenerSlot<HeadPoseListener> {public:using ListenerSlot::ListenerSlot;};
HeadPoseStream::HeadPoseStream(HeadPoseTracker*,HeadPoseListener* listener):pimpl(std::make_unique<Impl>(listener)){}
HeadPoseStream::~HeadPoseStream(){close();}
void HeadPoseStream::stopListening(){pimpl->Close();}
void HeadPoseStream::close(){stopListening();}
void HeadPoseStream::update(Frame& frame){pimpl->Update(frame);}
class EyePairStream::Impl : public ListenerSlot<EyePairListener> {public:using ListenerSlot::ListenerSlot;};
EyePairStream::EyePairStream(EyeTracker*,EyePairListener* listener):pimpl(std::make_unique<Impl>(listener)){}
EyePairStream::~EyePairStream(){close();}
void EyePairStream::stopListening(){pimpl->Close();}
void EyePairStream::close(){stopListening();}
void EyePairStream::update(Frame frame){pimpl->Update(frame);}
namespace {
template<class Base,class Stream> class PositionSense : public Base {
    struct Feed {
        std::shared_ptr<ContextState> context;
        std::atomic<bool> stopped{true};
        std::mutex mutex;
        std::vector<std::weak_ptr<Stream>> streams;
        uint64_t sequence{};
        bool raw{};
        explicit Feed(std::shared_ptr<ContextState> value,bool rawEyes):context(std::move(value)),raw(rawEyes){}
        void Run() {
            uint64_t lastObservation{},lastPairSequence{},lastPoseSequence{},lastEmit{};bool emitted{};SR_head last{};
            while(!stopped.load()) {
                titan::Snapshot snapshot{};
                const bool usable=context->alive.load()&&context->client->SnapshotForWeave(snapshot);
                auto tracking=TrackingFrame(snapshot,usable,sequence+1,raw);
                const bool pairValid=tracking.eyes.time!=0;
                const auto observation=pairValid?snapshot.eyePairObservationTick:
                    (tracking.time?snapshot.observationTick:0);
                const auto pairSequence=pairValid?snapshot.eyePairSequence:0;
                const auto poseSequence=tracking.headPose.time?snapshot.headPoseSequence:0;
                // Some clients consume the callback as their tracking heartbeat.
                // Continue stationary fallback at ~60 Hz without renewing time.
                const auto now=GetTickCount64();
                const bool fallbackDue=usable&&(!tracking.eyes.time||!tracking.headPose.time)&&now-lastEmit>=16;
                if(emitted&&!fallbackDue&&lastObservation==observation&&lastPairSequence==pairSequence&&lastPoseSequence==poseSequence&&SameTracking(last,tracking)){
                    Sleep(4);continue;
                }
                emitted=true;lastEmit=now;last=tracking;lastObservation=observation;lastPairSequence=pairSequence;lastPoseSequence=poseSequence;++sequence;
                typename Stream::Frame frame{};
                if constexpr(std::is_same_v<Stream,HeadPoseStream>)frame=tracking.headPose;
                else if constexpr(std::is_same_v<Stream,EyePairStream>)frame=tracking.eyes;
                else frame=tracking;
                std::vector<std::shared_ptr<Stream>> targets;
                {
                    std::lock_guard lock(mutex);
                    for(auto it=streams.begin();it!=streams.end();){
                        if(auto stream=it->lock()){targets.push_back(std::move(stream));++it;}
                        else it=streams.erase(it);
                    }
                }
                for(auto& target:targets){
                    if(stopped.load())break;
                    try{target->update(frame);}catch(...){target->close();titan::RuntimeLog("Tracking listener threw; its stream was closed");}
                }
                Sleep(4);
            }
        }
    };
    std::shared_ptr<Feed> feed_;
    std::thread worker_;
public:
    explicit PositionSense(std::shared_ptr<ContextState> context,bool raw=false):feed_(std::make_shared<Feed>(std::move(context),raw)){}
    ~PositionSense() override {
        stop();
        std::lock_guard lock(feed_->mutex);
        for(auto& weak:feed_->streams)if(auto stream=weak.lock())stream->close();
    }
    std::string getName() override{return "TitanLeiaBridge tracking";}
    std::string getDescription() override{return feed_->raw?
        "BOE eyes without bridge prediction; synthetic optical fallback has time=0":
        "Display-space eyes/center in mm; Rodrigues radians; estimated 140 mm ears; synthetic fallback has time=0";}
    void start() override {
        if(!feed_->stopped.exchange(false))return;
        try{worker_=std::thread([feed=feed_]{feed->Run();});}catch(...){feed_->stopped=true;throw;}
    }
    void stop() override {
        feed_->stopped=true;
        if(worker_.joinable()){
            if(worker_.get_id()==std::this_thread::get_id())worker_.detach();
            else worker_.join();
        }
    }
    template<class Listener> std::shared_ptr<Stream> Open(Listener* listener){
        if(!listener)throw std::invalid_argument("Null tracking listener");
        auto stream=std::make_shared<Stream>(this,listener);
        std::lock_guard lock(feed_->mutex);feed_->streams.push_back(stream);return stream;
    }
};
class HeadPosition final : public PositionSense<HeadTracker,HeadStream> {
public:
    using PositionSense::PositionSense;
    std::shared_ptr<HeadStream> openHeadStream(HeadListener* listener) override{return Open(listener);}
    void streamClosed(HeadStream* stream) override{if(stream)stream->close();}
};
class PosePosition final : public PositionSense<HeadPoseTracker,HeadPoseStream> {
public:
    using PositionSense::PositionSense;
    std::shared_ptr<HeadPoseStream> openHeadPoseStream(HeadPoseListener* listener) override{return Open(listener);}
    void streamClosed(HeadPoseStream* stream) override{if(stream)stream->close();}
};
class EyePosition final : public PositionSense<EyeTracker,EyePairStream> {
public:
    using PositionSense::PositionSense;
    std::shared_ptr<EyePairStream> openEyePairStream(EyePairListener* listener) override{return Open(listener);}
    void streamClosed(EyePairStream* stream) override{if(stream)stream->close();}
};
template<class Tracker> Tracker* GetPositionSense(SRContext& context,const char* identifier,bool raw=false){
    const auto& existing=context.getSenses(identifier);
    if(!existing.empty())return static_cast<Tracker*>(existing.front());
    auto state=FindContext(&context);
    auto sense=std::make_unique<Tracker>(state,raw);
    if(state->started.load())sense->start();
    context.addSense(identifier,sense.get());return sense.release();
}
}
HeadTracker* HeadTracker::create(SRContext& context){return GetPositionSense<HeadPosition>(context,"TitanLeiaBridge.Head");}
HeadPoseTracker* HeadPoseTracker::create(SRContext& context){return GetPositionSense<PosePosition>(context,"TitanLeiaBridge.HeadPose");}
EyeTracker* EyeTracker::create(SRContext& context){return GetPositionSense<EyePosition>(context,"TitanLeiaBridge.Eyes");}
EyeTracker* EyeTracker::createRaw(SRContext& context){return GetPositionSense<EyePosition>(context,"TitanLeiaBridge.RawEyes",true);}
