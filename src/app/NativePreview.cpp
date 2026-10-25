#include "NativePreview.h"
#include <windows.h>
#include <objbase.h>
#include <sr/sense/headtracker/head.h>
#include <atomic>
#include <cmath>
#include <condition_variable>
#include <filesystem>
#include <mutex>
#include <stdexcept>
#include <thread>

namespace titan::app {
namespace {
template<class T> T Entry(HMODULE module,const char* name) {
    const auto proc=GetProcAddress(module,name);
    if(!proc)throw std::runtime_error(std::string("Missing native LeiaSR export: ")+name);
    HMODULE owner{};
    if(!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
        reinterpret_cast<LPCWSTR>(proc),&owner)||owner!=module)
        throw std::runtime_error("Native LeiaSR export forwarded to another runtime.");
    return reinterpret_cast<T>(proc);
}
bool Point(const SR_point3d& p) {
    return std::isfinite(p.x)&&std::isfinite(p.y)&&std::isfinite(p.z);
}
std::filesystem::path RuntimeDirectory() {
    wchar_t path[32768]{};DWORD size=sizeof(path);
    const auto error=RegGetValueW(HKEY_LOCAL_MACHINE,L"SOFTWARE\\Dimenco\\Simulated Reality",nullptr,
        RRF_RT_REG_SZ|RRF_RT_REG_EXPAND_SZ|RRF_SUBKEY_WOW6464KEY,nullptr,path,&size);
    if(error!=ERROR_SUCCESS)throw std::runtime_error("Native LeiaSR installation is not registered.");
    const std::filesystem::path root(path);
    if(!root.is_absolute())throw std::runtime_error("Native LeiaSR runtime path must be absolute.");
    return std::filesystem::canonical(root/L"bin");
}
}
struct NativePreview::Impl {
    mutable std::mutex mutex;
    std::condition_variable wake;
    std::thread worker;
    bool stopping{};
    NativePreviewState state;
    ULONGLONG eyeTick{},poseTick{};
    uint64_t eyeFrames{},headFrames{},validEyeFrames{},validHeadFrames{};
    std::function<void(const std::wstring&)> log;
    // The app has one preview provider. The callback gate prevents teardown races.
    static inline std::mutex callbackMutex;
    static inline Impl* active{};
    static void Eyes(SR_eyePair eyes) {
        std::lock_guard gate(callbackMutex);
        if(active)active->AcceptEyes(eyes);
    }
    static void Head(SR_head head) {
        std::lock_guard gate(callbackMutex);
        if(!active)return;
        active->AcceptEyes(head.eyes);
        std::lock_guard lock(active->mutex);
        ++active->headFrames;
        const auto& p=head.headPose.orientation;
        active->state.rotation=Point(p);
        if(active->state.rotation) {
            ++active->validHeadFrames;
            constexpr double degrees=180.0/3.141592653589793;
            active->state.pitch=p.x*degrees;active->state.yaw=p.y*degrees;active->state.roll=p.z*degrees;
            active->poseTick=GetTickCount64();
        }
    }
    void AcceptEyes(const SR_eyePair& eyes) {
        std::lock_guard lock(mutex);
        ++eyeFrames;
        state.face=Point(eyes.left)&&Point(eyes.right)&&eyes.left.z>0&&eyes.right.z>0;
        if(state.face) {
            ++validEyeFrames;
            state.x=(eyes.left.x+eyes.right.x)*0.5;
            state.y=(eyes.left.y+eyes.right.y)*0.5;
            state.z=(eyes.left.z+eyes.right.z)*0.5;
            eyeTick=GetTickCount64();
        }
    }
    void Run() {
        const HRESULT com=CoInitializeEx(nullptr,COINIT_MULTITHREADED);
        HMODULE core{},face{},displays{};
        DLL_DIRECTORY_COOKIE directory{};
        void* context{};void* eyes{};void* head{};void* display{};
        int(*viewWidth)(void*){};int(*viewHeight)(void*){};
        void(*deleteContext)(void*){};void(*deleteEyes)(void*){};void(*deleteHead)(void*){};
        try {
            const auto bin=RuntimeDirectory();
            // Never search PATH: it may deliberately point to the bridge runtime.
            if(!SetDefaultDllDirectories(LOAD_LIBRARY_SEARCH_DEFAULT_DIRS))
                throw std::runtime_error("Cannot configure native runtime DLL search.");
            directory=AddDllDirectory(bin.c_str());
            if(!directory)throw std::runtime_error("Cannot add native runtime DLL directory.");
            if(GetModuleHandleW(L"SimulatedRealityCore.dll")||GetModuleHandleW(L"SimulatedRealityFaceTrackers.dll")||
                GetModuleHandleW(L"SimulatedRealityDisplays.dll")||GetModuleHandleW(L"TitanLeiaRuntime.dll"))
                throw std::runtime_error("A LeiaSR runtime is already loaded; refusing to mix runtimes.");
            constexpr DWORD flags=LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR|LOAD_LIBRARY_SEARCH_DEFAULT_DIRS;
            core=LoadLibraryExW((bin/L"SimulatedRealityCore.dll").c_str(),nullptr,flags);
            if(!core)throw std::runtime_error("Cannot load native SimulatedRealityCore.dll: "+std::to_string(GetLastError()));
            // Bridge DLLs forward this API to TitanLeiaRuntime; reject before creating a context.
            if(GetModuleHandleW(L"TitanLeiaRuntime.dll"))throw std::runtime_error("Registered runtime points to the bridge, not native LeiaSR.");
            face=LoadLibraryExW((bin/L"SimulatedRealityFaceTrackers.dll").c_str(),nullptr,flags);
            if(!face)throw std::runtime_error("Cannot load native SimulatedRealityFaceTrackers.dll: "+std::to_string(GetLastError()));
            const auto create=Entry<void*(*)(bool)>(core,"newSRContextLensPreference");
            const auto initialize=Entry<void(*)(void*)>(core,"initializeSRContext");
            deleteContext=Entry<void(*)(void*)>(core,"deleteSRContext");
            const auto eyeTracker=Entry<void*(*)(void*)>(face,"createEyeTracker");
            const auto headTracker=Entry<void*(*)(void*)>(face,"createHeadTracker");
            const auto listenEyes=Entry<void*(*)(void*,void(*)(SR_eyePair))>(face,"createEyePairListener");
            const auto listenHead=Entry<void*(*)(void*,void(*)(SR_head))>(face,"createHeadListener");
            deleteEyes=Entry<void(*)(void*)>(face,"deleteEyePairListener");
            deleteHead=Entry<void(*)(void*)>(face,"deleteHeadListener");
            log(L"原生 LeiaSR 预览："+bin.wstring()+L"；仅订阅追踪，不请求镜片、不创建桥接服务。");
            context=create(false);
            if(!context)throw std::runtime_error("Native LeiaSR context creation failed.");
            // Optional display query: an older runtime must not disable tracking.
            try {
                displays=LoadLibraryExW((bin/L"SimulatedRealityDisplays.dll").c_str(),nullptr,flags);
                if(!displays)throw std::runtime_error("Cannot load native Displays module.");
                const auto createDisplay=Entry<void*(*)(void*)>(displays,"createDisplay");
                viewWidth=Entry<int(*)(void*)>(displays,"getRecommendedViewsTextureWidth");
                viewHeight=Entry<int(*)(void*)>(displays,"getRecommendedViewsTextureHeight");
                display=createDisplay(context); // Owned by the context, no lens request.
            }catch(const std::exception&) {
                display=nullptr;
                log(L"原厂 SDK 推荐视图尺寸暂不可用；追踪预览继续运行。");
            }
            const auto eyeSense=eyeTracker(context),headSense=headTracker(context);
            if(!eyeSense||!headSense)throw std::runtime_error("Native LeiaSR tracking senses are unavailable.");
            {std::lock_guard gate(callbackMutex);active=this;}
            eyes=listenEyes(eyeSense,Eyes);head=listenHead(headSense,Head);
            if(!eyes||!head)throw std::runtime_error("Native LeiaSR tracking subscription failed.");
            initialize(context);
            {
                std::lock_guard lock(mutex);
                state.connected=true;state.message=L"原厂 LeiaSR 追踪预览；不接管游戏、不进行交织。";
            }
            for(;;) {
                int width{},height{};
                if(display&&viewWidth&&viewHeight) {
                    try { width=viewWidth(display);height=viewHeight(display); } catch(...) { width=height=0; }
                }
                std::unique_lock lock(mutex);
                state.recommendedWidth=width>0&&height>0?width:0;
                state.recommendedHeight=width>0&&height>0?height:0;
                if(wake.wait_for(lock,std::chrono::seconds(1),[&]{return stopping;}))break;
            }
        }catch(const std::exception& e) {
            const std::string error=e.what();
            std::wstring text(error.begin(),error.end());
            {std::lock_guard lock(mutex);state.connected=false;state.error=true;state.message=L"原生预览失败："+text;}
            log(L"原生预览失败："+text);
        }catch(...) {
            {std::lock_guard lock(mutex);state.connected=false;state.error=true;state.message=L"原厂 LeiaSR 初始化发生异常。";}
            log(L"原厂 LeiaSR 初始化发生异常。");
        }
        {std::lock_guard gate(callbackMutex);active=nullptr;}
        // Listener callbacks are detached before their context and DLLs are released.
        if(head&&deleteHead)deleteHead(head);
        if(eyes&&deleteEyes)deleteEyes(eyes);
        if(context&&deleteContext)deleteContext(context);
        log(L"原生预览结束：head samples="+std::to_wstring(headFrames)+L", eye samples="+std::to_wstring(eyeFrames)+L", valid head="+std::to_wstring(validHeadFrames)+L", valid eyes="+std::to_wstring(validEyeFrames));
        if(displays)FreeLibrary(displays);
        if(face)FreeLibrary(face);
        if(core)FreeLibrary(core);
        if(directory)RemoveDllDirectory(directory);
        if(SUCCEEDED(com))CoUninitialize();
    }
};
NativePreview::NativePreview():impl_(std::make_unique<Impl>()){}
NativePreview::~NativePreview(){Stop();}
void NativePreview::Start(std::function<void(const std::wstring&)> log) {
    impl_->log=std::move(log);impl_->worker=std::thread([this]{impl_->Run();});
}
void NativePreview::Stop() {
    {std::lock_guard lock(impl_->mutex);impl_->stopping=true;}
    impl_->wake.notify_all();if(impl_->worker.joinable())impl_->worker.join();
}
NativePreviewState NativePreview::Read() const {
    std::lock_guard lock(impl_->mutex);
    auto state=impl_->state;const auto now=GetTickCount64();
    state.face=state.face&&impl_->eyeTick&&now-impl_->eyeTick<=500;
    state.rotation=state.rotation&&state.face&&impl_->poseTick&&now-impl_->poseTick<=500;
    return state;
}
}
