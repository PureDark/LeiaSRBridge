// C ABI consumed by SRUnityInternal and Odyssey3DHubService.
#include "LegacyTrackingFallback.h"
namespace {
SR_point3d LegacyCenter(const std::shared_ptr<titan::IpcClient>& client) {
    titan::Snapshot snapshot{};
    SR_point3d center{0,0,600};
    if(client->SnapshotForWeave(snapshot)) SR::DisplayPoint(snapshot,snapshot.eye,center);
    return titan::legacy::SafeCenter(center);
}
struct CSystemListener final : SR::SystemEventListener {
    void(*callback)(SR_systemEvent);std::shared_ptr<SR::SystemEventStream> stream;
    explicit CSystemListener(void(*cb)(SR_systemEvent)):callback(cb){}
    ~CSystemListener(){if(stream)stream->stopListening();}
    void accept(const SR::SystemEvent& e)override{auto copy=e;callback(static_cast<SR_systemEvent>(copy));}
};
struct CHeadListener final : SR::HeadListener {
    void(*callback)(SR_head);std::shared_ptr<SR::HeadStream> stream;
    std::shared_ptr<titan::IpcClient> client=titan::IpcClient::Connect();
    explicit CHeadListener(void(*cb)(SR_head)):callback(cb){}
    ~CHeadListener(){if(stream)stream->stopListening();}
    // Legacy Unity consumers copy these values straight into camera transforms.
    // C++ streams already supply optical fallback while the device is usable.
    // Retain finite Unity transforms even during actual device disconnection;
    // system readiness events still report the unavailable device.
    void accept(const SR_head& e)override{callback(titan::legacy::SafeHead(e,LegacyCenter(client)));}
};
struct CEyeListener final : SR::EyePairListener {
    void(*callback)(SR_eyePair);std::shared_ptr<SR::EyePairStream> stream;
    std::shared_ptr<titan::IpcClient> client=titan::IpcClient::Connect();
    explicit CEyeListener(void(*cb)(SR_eyePair)):callback(cb){}
    ~CEyeListener(){if(stream)stream->stopListening();}
    void accept(const SR_eyePair& e)override{callback(titan::legacy::SafeEyes(e,LegacyCenter(client)));}
};
template<class F> auto CBoundary(F&& f)noexcept->decltype(f()) {try{return f();}catch(const std::exception& e){titan::RuntimeLog(e.what());}catch(...){titan::RuntimeLog("Leia C ABI operation failed");}if constexpr(!std::is_void_v<decltype(f())>)return {};}
}
#define TITAN_C extern "C" __declspec(dllexport)
TITAN_C void* newSRContext(){return CBoundary([]()->void*{return SR::SRContext::create();});}
TITAN_C void* newSRContextLensPreference(bool){return newSRContext();}
TITAN_C void initializeSRContext(void* c){CBoundary([&]{if(c)static_cast<SR::SRContext*>(c)->initialize();});}
TITAN_C void deleteSRContext(void* c){CBoundary([&]{if(c)SR::SRContext::deleteSRContext(static_cast<SR::SRContext*>(c));});}
TITAN_C void* createSystemSense(void* c){return CBoundary([&]()->void*{return c?SR::SystemSense::create(*static_cast<SR::SRContext*>(c)):nullptr;});}
TITAN_C void* createSystemEventListener(void* sense,void(*callback)(SR_systemEvent)){
    return CBoundary([&]()->void*{if(!sense||!callback)return nullptr;auto listener=std::make_unique<CSystemListener>(callback);listener->stream=static_cast<SR::SystemSense*>(sense)->openSystemEventStream(listener.get());return listener.release();});
}
TITAN_C void deleteSystemEventListener(void* p){CBoundary([&]{delete static_cast<CSystemListener*>(p);});}
TITAN_C void* createDisplay(void* c){return CBoundary([&]()->void*{return c?SR::Display::create(*static_cast<SR::SRContext*>(c)):nullptr;});}
TITAN_C void* createScreen(void* c){return createDisplay(c);}
#define DISPLAY_GET(T,name) TITAN_C T name(void* d){return CBoundary([&]()->T{return d?static_cast<SR::Display*>(d)->name():T{};});}
DISPLAY_GET(int,getResolutionHeight) DISPLAY_GET(int,getResolutionWidth)
DISPLAY_GET(int,getPhysicalResolutionHeight) DISPLAY_GET(int,getPhysicalResolutionWidth)
DISPLAY_GET(float,getPhysicalSizeHeight) DISPLAY_GET(float,getPhysicalSizeWidth) DISPLAY_GET(float,getDotPitch)
DISPLAY_GET(int,getRecommendedViewsTextureWidth) DISPLAY_GET(int,getRecommendedViewsTextureHeight)
#undef DISPLAY_GET
TITAN_C void* createSwitchableLensHint(void* c){return CBoundary([&]()->void*{return c?SR::SwitchableLensHint::create(*static_cast<SR::SRContext*>(c)):nullptr;});}
TITAN_C void lensEnableHint(void* h){CBoundary([&]{if(h)static_cast<SR::SwitchableLensHint*>(h)->enable();});}
TITAN_C void lensDisableHint(void* h){CBoundary([&]{if(h)static_cast<SR::SwitchableLensHint*>(h)->disable();});}
// Context retains ownership of the shared hint; deletion releases its request.
TITAN_C void deleteSwitchableLensHint(void* h){lensDisableHint(h);}
TITAN_C bool isLensEnabled(void* h){return h&&static_cast<SR::SwitchableLensHint*>(h)->isEnabled();}
TITAN_C bool isLensEnabledByPreference(void* h){return h&&static_cast<SR::SwitchableLensHint*>(h)->isEnabledByPreference();}
TITAN_C void* createHeadTracker(void* c){return CBoundary([&]()->void*{return c?SR::HeadTracker::create(*static_cast<SR::SRContext*>(c)):nullptr;});}
TITAN_C void* createHeadListener(void* sense,void(*callback)(SR_head)){
    return CBoundary([&]()->void*{if(!sense||!callback)return nullptr;auto listener=std::make_unique<CHeadListener>(callback);listener->stream=static_cast<SR::HeadTracker*>(sense)->openHeadStream(listener.get());return listener.release();});
}
TITAN_C void deleteHeadListener(void* p){CBoundary([&]{delete static_cast<CHeadListener*>(p);});}
TITAN_C void* createEyeTracker(void* c){return CBoundary([&]()->void*{return c?SR::EyeTracker::create(*static_cast<SR::SRContext*>(c)):nullptr;});}
TITAN_C void* createEyePairListener(void* sense,void(*callback)(SR_eyePair)){
    return CBoundary([&]()->void*{if(!sense||!callback)return nullptr;auto listener=std::make_unique<CEyeListener>(callback);listener->stream=static_cast<SR::EyeTracker*>(sense)->openEyePairStream(listener.get());return listener.release();});
}
TITAN_C void deleteEyePairListener(void* p){CBoundary([&]{delete static_cast<CEyeListener*>(p);});}
#undef TITAN_C
