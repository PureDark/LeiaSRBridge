// Included after HeadTracking.inl, inside SR.
class SystemEventStream::Impl : public ListenerSlot<SystemEventListener> {public:using ListenerSlot::ListenerSlot;};
SystemEventStream::SystemEventStream(SystemSense*,SystemEventListener* listener):pimpl(std::make_unique<Impl>(listener)){}
SystemEventStream::~SystemEventStream(){close();}
void SystemEventStream::stopListening(){pimpl->Close();}
void SystemEventStream::close(){stopListening();}
void SystemEventStream::update(SystemEvent frame){pimpl->Update(frame);}
namespace {
class BridgeSystem final : public SystemSense {
    struct Feed {
        std::shared_ptr<ContextState> context;
        std::atomic<bool> stopped{true};std::mutex mutex;
        struct Entry {std::weak_ptr<SystemEventStream> stream;bool initialized{},ready{},lens{},user{};};
        std::vector<Entry> entries;
        explicit Feed(std::shared_ptr<ContextState> c):context(std::move(c)){}
        void Run(){
            while(!stopped.load()){
                titan::Snapshot s{};const bool ready=context->alive.load()&&context->client->SnapshotForWeave(s);
                const bool lens=ready&&s.lensEnabled;
                // Advertise the virtual viewer while optical fallback is usable.
                // UserLost can make native Odyssey clients stop drawing. Actual
                // face availability remains in broker/UI measurement flags.
                const bool user=ready;
                std::vector<std::pair<std::shared_ptr<SystemEventStream>,std::vector<SR_eventType>>> events;
                {std::lock_guard lock(mutex);
                    for(auto it=entries.begin();it!=entries.end();){
                        auto stream=it->stream.lock();if(!stream){it=entries.erase(it);continue;}
                        std::vector<SR_eventType> batch;
                        if(!it->initialized||it->ready!=ready){
                            if(ready)batch={USBNotConnectedResolved,DisplayNotConnectedResolved,DeviceConnectedAndReady,SRRestored};
                            else batch={DeviceDisconnected,SRUnavailable};
                        }
                        if(!it->initialized||it->lens!=lens)batch.push_back(lens?LensOn:LensOff);
                        if(!it->initialized||it->user!=user)batch.push_back(user?UserFound:UserLost);
                        it->initialized=true;it->ready=ready;it->lens=lens;it->user=user;++it;
                        if(!batch.empty())events.emplace_back(std::move(stream),std::move(batch));
                    }
                }
                for(auto& [stream,batch]:events)for(auto type:batch){if(stopped.load())break;try{const auto now=GetTickCount64();stream->update({ObservationTime(now,now),type,"LeiaSRBridge device state"});}catch(...){stream->close();}}
                Sleep(20);
            }
        }
    };
    std::shared_ptr<Feed> feed_;std::thread worker_;
public:
    explicit BridgeSystem(std::shared_ptr<ContextState> c):feed_(std::make_shared<Feed>(std::move(c))){}
    ~BridgeSystem()override{stop();std::lock_guard lock(feed_->mutex);for(auto& e:feed_->entries)if(auto s=e.stream.lock())s->close();}
    std::string getName()override{return "LeiaSRBridge system";}
    std::string getDescription()override{return "Live broker readiness, lens and virtual viewer availability";}
    void start()override{if(feed_->stopped.exchange(false)){try{worker_=std::thread([feed=feed_]{feed->Run();});}catch(...){feed_->stopped=true;throw;}}}
    void stop()override{feed_->stopped=true;if(worker_.joinable()){if(worker_.get_id()==std::this_thread::get_id())worker_.detach();else worker_.join();}}
    std::shared_ptr<SystemEventStream> openSystemEventStream(SystemEventListener* listener)override{
        if(!listener)throw std::invalid_argument("Null system listener");auto stream=std::make_shared<SystemEventStream>(this,listener);
        std::lock_guard lock(feed_->mutex);feed_->entries.push_back({stream});return stream;
    }
    void streamClosed(SystemEventStream* stream)override{if(stream)stream->close();}
};
class BridgeDisplay final : public Display {
    std::shared_ptr<ContextState> context_;
public:
    explicit BridgeDisplay(std::shared_ptr<ContextState> c):context_(std::move(c)){}
    MONITORINFOEXW Info(){MONITORINFOEXW info{};info.cbSize=sizeof(info);GetMonitorInfoW(MonitorFromPoint({0,0},MONITOR_DEFAULTTOPRIMARY),&info);return info;}
    const int getResolutionHeight()override{auto i=Info();return i.rcMonitor.bottom-i.rcMonitor.top;}
    const int getResolutionWidth()override{auto i=Info();return i.rcMonitor.right-i.rcMonitor.left;}
    const int getPhysicalResolutionHeight()override{return getResolutionHeight();}
    const int getPhysicalResolutionWidth()override{return getResolutionWidth();}
    const float getPhysicalSizeHeight()override{return getDotPitch()*getResolutionHeight();}
    const float getPhysicalSizeWidth()override{return getDotPitch()*getResolutionWidth();}
    const float getDotPitch()override{titan::Snapshot s{};return context_->client->SnapshotForWeave(s)?static_cast<float>(s.refractParams[1]/10.0):0;}
    SR_recti getLocation()override{auto i=Info();return {i.rcMonitor.left,i.rcMonitor.top,i.rcMonitor.right,i.rcMonitor.bottom};}
    int getRecommendedViewsTextureWidth()override{return titan::NativeDisplayPixels(Info().szDevice).width/2;}
    int getRecommendedViewsTextureHeight()override{return titan::NativeDisplayPixels(Info().szDevice).height/2;}
};
std::map<SRContext*,std::unique_ptr<BridgeDisplay>> displayObjects;
std::mutex displayMutex;
void ReleaseLegacyDisplay(SRContext* c){std::lock_guard lock(displayMutex);displayObjects.erase(c);}
}
SystemSense* SystemSense::create(SRContext& context){
    const auto& entries=context.getSenses("Stereo3DBridge.System");if(!entries.empty())return static_cast<SystemSense*>(entries.front());
    auto state=FindContext(&context);auto sense=std::make_unique<BridgeSystem>(state);if(state->started.load())sense->start();context.addSense("Stereo3DBridge.System",sense.get());return sense.release();
}
WorldObject::WorldObject():parent(nullptr),relativePosition(0,0,0){}
WorldObject::WorldObject(WorldObject* value):parent(value),relativePosition(0,0,0){}
cv::Vec3d WorldObject::getRelativePosition(){return relativePosition;}
cv::Vec3d WorldObject::getAbsolutePosition(){return relativePosition;}
Display* Display::create(SRContext& c){std::lock_guard lock(displayMutex);auto& display=displayObjects[&c];if(!display)display=std::make_unique<BridgeDisplay>(FindContext(&c));return display.get();}
