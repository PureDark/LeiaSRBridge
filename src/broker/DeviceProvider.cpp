#include "DeviceProvider.h"
#include "PortableTracking.h"
#include "EyePosition.h"
#include "EyePrediction.h"
#include "EyePairPosition.h"
#include "HeadPose.h"
#include "../common/BoeTrackingProtocol.h"
#include <OleAuto.h>
#include <TlHelp32.h>
#include <algorithm>
#include <array>
#include <cmath>
#include <cwctype>
#include <fstream>
#include <sstream>
#include <vector>

namespace titan::broker {
namespace {
std::wstring HResultText(HRESULT value) {
    wchar_t text[32]{};
    swprintf_s(text, L"0x%08X", static_cast<unsigned>(value));
    return text;
}
void SetMessage(Snapshot& state, const std::wstring& text) {
    wcsncpy_s(state.message, text.c_str(), _TRUNCATE);
}
bool IsVendorGuiRunning() {
    HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if (snapshot == INVALID_HANDLE_VALUE) return false;
    PROCESSENTRY32W entry{sizeof(entry)};
    bool found = false;
    for (BOOL more = Process32FirstW(snapshot, &entry); more && !found;
         more = Process32NextW(snapshot, &entry))
        found = !_wcsicmp(entry.szExeFile, L"DepthTransformerPlus.exe");
    CloseHandle(snapshot);
    return found;
}
DWORD FindPortableTracker(const std::filesystem::path& executable, std::uint64_t& creation) {
    HANDLE list=CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS,0);
    if(list==INVALID_HANDLE_VALUE)return 0;
    PROCESSENTRY32W entry{sizeof(entry)};
    DWORD found{};creation=0;
    for(BOOL more=Process32FirstW(list,&entry);more;more=Process32NextW(list,&entry)){
        if(_wcsicmp(entry.szExeFile,L"FaceTracking.exe"))continue;
        HANDLE process=OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION|SYNCHRONIZE,FALSE,entry.th32ProcessID);
        if(!process)continue;
        wchar_t path[32768]{};DWORD size=static_cast<DWORD>(std::size(path));
        std::error_code error;
        const bool same=QueryFullProcessImageNameW(process,0,path,&size) &&
            std::filesystem::equivalent(path,executable,error);
        const auto started=ProcessCreationTime(process);
        const bool running=WaitForSingleObject(process,0)==WAIT_TIMEOUT;
        CloseHandle(process);
        if(!same || !started || !running)continue;
        // Ambiguous ownership: do not combine two different cameras/providers.
        if(found){found=0;creation=0;break;}
        found=entry.th32ProcessID;creation=started;
    }
    CloseHandle(list);
    return found;
}
std::string AnsiPath(const std::filesystem::path& path) {
    const auto wide = path.wstring();
    BOOL lossy = FALSE;
    // Windows' "Use Unicode UTF-8" locale makes ACP equal to 65001. That
    // code page rejects WC_NO_BEST_FIT_CHARS and lpUsedDefaultChar entirely.
    const bool utf8 = GetACP() == CP_UTF8;
    const UINT codePage = utf8 ? CP_UTF8 : CP_ACP;
    const DWORD flags = utf8 ? WC_ERR_INVALID_CHARS : WC_NO_BEST_FIT_CHARS;
    BOOL* usedDefault = utf8 ? nullptr : &lossy;
    const int count = WideCharToMultiByte(codePage, flags, wide.c_str(),
        -1, nullptr, 0, nullptr, usedDefault);
    if (!count || lossy) return {};
    std::string result(count, '\0');
    if (!WideCharToMultiByte(codePage, flags, wide.c_str(), -1,
                            result.data(), count, nullptr, usedDefault) || lossy) return {};
    result.pop_back();
    if (result.empty() || (result.back() != '/' && result.back() != '\\')) result += '\\';
    return result;
}
bool FiniteCalibration(const Snapshot& state) {
    for (double v : state.deviceParams) if (!std::isfinite(v)) return false;
    for (double v : state.wholeMat) if (!std::isfinite(v)) return false;
    for (double v : state.refractParams) if (!std::isfinite(v)) return false;
    // The active shader divides by pixel pitch and lenticular pitch.
    return state.refractParams[0] > 0 && state.refractParams[1] > 0;
}
HRESULT Invoke(IDispatch* dispatch, DISPID method, VARIANTARG* arguments = nullptr,
               UINT count = 0) {
    DISPPARAMS params{arguments, nullptr, count, 0};
    EXCEPINFO exception{};
    UINT argumentError{};
    HRESULT hr = dispatch->Invoke(method, IID_NULL, LOCALE_INVARIANT, DISPATCH_METHOD,
                                  &params, nullptr, &exception, &argumentError);
    SysFreeString(exception.bstrSource);
    SysFreeString(exception.bstrDescription);
    SysFreeString(exception.bstrHelpFile);
    return hr;
}
} // namespace

struct DeviceProvider::Impl {
    using InitSdk = int (__cdecl *)(const char*, int);
    using IntCall = int (__cdecl *)();
    using VoidCall = void (__cdecl *)();
    using SetMode = int (__cdecl *)(bool);
    using GetParams = int (__cdecl *)(double*);

    ProviderOptions options;
    LogFunction logger;
    Snapshot state{};
    HMODULE module{};
    InitSdk initSdk{};
    IntCall openMcu{}, getUsbState{};
    VoidCall closeMcu{}, releaseSdk{};
    SetMode setMode{};
    GetParams getDevice{}, getWhole{}, getRefract{}, getLeftCamera{}, getRightCamera{}, getStereoCamera{};
    IDispatch* tracker{};
    PortableTracking portableTracker;
    bool comInitialized{}, sdkInitialized{}, mcuOpened{}, trackerOpened{}, cameraStarted{};
    bool ready{}, lost{};
    ULONGLONG lastUsbPoll{};
    EyePrediction prediction;
    HeadPose headPose;
    std::array<float, 3> rawEye{};
    ULONGLONG lastEyeLog{};
    BoeTrackingReader pairReader;
    BoeTrackingFrame lastPair{};
    ULONGLONG nextPairConnect{};
    bool pairConnectedLog{};

    Impl(ProviderOptions value, LogFunction log) : options(std::move(value)), logger(std::move(log)) {}
    void Log(const std::wstring& text) { if (logger) logger(text); }
    bool Fail(ProviderStatus status, const std::wstring& message) {
        state.status = status;
        state.calibrationValid = state.eyePositionValid = state.eyePairValid = state.headPoseValid = 0;
        SetMessage(state, message);
        Log(message);
        return false;
    }
    template<typename T> bool Bind(T& target, const char* name) {
        target = reinterpret_cast<T>(GetProcAddress(module, name));
        return target != nullptr;
    }
    bool PrepareCameraCalibration(const std::filesystem::path& trackerPath, std::wstring& error) {
        std::array<double, 9> left{}, right{};
        std::array<double, 10> stereo{};
        const int l = getLeftCamera(left.data());
        const int r = getRightCamera(right.data());
        const int s = getStereoCamera(stereo.data());
        if (l || r || s) {
            error = L"无法读取当前显示器的摄像头校准，结果=" + std::to_wstring(l) + L"/" + std::to_wstring(r) + L"/" + std::to_wstring(s);
            return false;
        }
        if(!headPose.Configure(left,right,stereo))
            Log(L"头部旋转不可用：双目姿态校准无效；继续使用现有眼位显示。");
        return SaveCameraCalibration(trackerPath.parent_path() / L"boe_mcu_mat.ini", left, right, stereo, error);
    }
    bool Initialize() {
        state = {};
        state.status = ProviderStatus::Initializing;
        state.brokerPid = GetCurrentProcessId();
        state.heartbeatTick = GetTickCount64();
        if (options.mock) {
            state.status = ProviderStatus::Mock;
            state.calibrationValid = 1;
            state.eyePositionValid = 1;
            state.eye[2] = 600.0f;
            state.eyePairValid=1;
            state.eyePairCenter[2]=600.0f;
            state.eyePair[0][0]=state.rawEyePair[0][0]=-32.0f;
            state.eyePair[1][0]=state.rawEyePair[1][0]=32.0f;
            state.eyePair[0][2]=state.eyePair[1][2]=600.0f;
            state.rawEyePair[0][2]=state.rawEyePair[1][2]=600.0f;
            state.deviceParams[0] = 1.0;
            state.refractParams[0] = 1.0;
            state.refractParams[1] = 0.15525;
            state.refractParams[2] = 1.0;
            SetMessage(state, L"Simulation: synthetic calibration; no vendor DLL, MCU or camera opened.");
            ready = true;
            Log(state.message);
            return true;
        }
        if (IsVendorGuiRunning())
            return Fail(ProviderStatus::Busy, L"Close DepthTransformerPlus before starting the bridge: MCU and camera require one owner.");
        const auto trackerPath = options.vendorDirectory / L"eyetracking" / L"FaceTrackingManagerService.dll";
        if (!std::filesystem::is_regular_file(trackerPath))
            return Fail(ProviderStatus::Error, L"便携眼追文件缺失，请完整解压测试包的 vendor 目录；不需要安装 DepthTransformerPlus。");
        std::error_code error;
        std::filesystem::create_directories(options.logDirectory, error);
        if (!error) std::filesystem::create_directories(options.vendorDirectory / L"logs", error);
        if (error) return Fail(ProviderStatus::Error, L"Cannot create the vendor SDK log directory.");
        const auto logPath = AnsiPath(options.logDirectory);
        if (logPath.empty()) return Fail(ProviderStatus::Error, L"The vendor SDK log path cannot be represented in the Windows ANSI code page.");
        const auto dll = options.vendorDirectory / L"MCU_library.dll";
        module = LoadLibraryExW(dll.c_str(), nullptr, LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR | LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
        if (!module) return Fail(ProviderStatus::Error, L"Cannot load MCU_library.dll from the selected installation. Win32=" + std::to_wstring(GetLastError()));
        if (!(Bind(initSdk, "BOE_MCU_initSDK") && Bind(openMcu, "BOE_MCU_openMCU") &&
              Bind(closeMcu, "BOE_MCU_closeMCU") && Bind(releaseSdk, "BOE_MCU_releaseSDK") &&
              Bind(setMode, "BOE_MCU_set3DMode") && Bind(getUsbState, "BOE_MCU_getUSBState") &&
              Bind(getDevice, "BOE_MCU_getDeviceParam") && Bind(getWhole, "BOE_MCU_getWholeMat") &&
              Bind(getRefract, "BOE_MCU_getRefractionDeviceParam") &&
              Bind(getLeftCamera, "BOE_MCU_getLeftCameraMatParam") &&
              Bind(getRightCamera, "BOE_MCU_getRightCameraMatParam") &&
              Bind(getStereoCamera, "BOE_MCU_getStereoCameraDistParam")))
            return Fail(ProviderStatus::Error, L"The MCU DLL is missing a required export; initialization stopped.");
        Log(L"MCU: initSDK (no AI or capture modules are loaded).");
        const int initResult = initSdk(logPath.c_str(), 2);
        if (initResult) return Fail(ProviderStatus::Error, L"BOE_MCU_initSDK failed: " + std::to_wstring(initResult));
        sdkInitialized = true;
        const int openResult = openMcu();
        if (openResult) return Fail(ProviderStatus::NoDevice,
            L"Cannot open the supported MCU. Check the monitor USB cable and close other 3D controllers. Result=" + std::to_wstring(openResult));
        mcuOpened = true;
        const int usb = getUsbState();
        if (usb != 1 && usb != 2) return Fail(ProviderStatus::NoDevice, L"MCU opened but USB device state is not connected: " + std::to_wstring(usb));
        const int deviceResult = getDevice(state.deviceParams);
        const int wholeResult = getWhole(state.wholeMat);
        const int refractResult = getRefract(state.refractParams);
        if (deviceResult || wholeResult || refractResult || !FiniteCalibration(state))
            return Fail(ProviderStatus::Error, L"MCU optical calibration failed or is invalid (device/whole/refract): " +
                std::to_wstring(deviceResult) + L"/" + std::to_wstring(wholeResult) + L"/" + std::to_wstring(refractResult));
        state.calibrationValid = 1;
        Log(L"MCU: read this device's 6/6/8 optical calibration values.");
        std::wstring portableError;
        if (!PrepareCameraCalibration(trackerPath, portableError))
            return Fail(ProviderStatus::Error, portableError);
        Log(L"已从当前显示器生成 9/9/10 摄像头校准；没有使用其他屏的安装包标定。");
        const HRESULT comResult = CoInitializeEx(nullptr, COINIT_MULTITHREADED);
        if (FAILED(comResult)) return Fail(ProviderStatus::Error, L"CoInitializeEx failed: " + HResultText(comResult));
        comInitialized = true;
        if (!portableTracker.Activate(trackerPath.parent_path(), portableError))
            return Fail(ProviderStatus::Error, portableError);
        HRESULT hr = portableTracker.Create(&tracker);
        if (FAILED(hr)) return Fail(ProviderStatus::Error, L"无法激活随包眼追组件（无需注册），HRESULT=" + HResultText(hr));
        hr = Invoke(tracker, 1);
        if (FAILED(hr)) return Fail(ProviderStatus::Error, L"Eye provider Open failed: " + HResultText(hr));
        trackerOpened = true;
        hr = Invoke(tracker, 11);
        if (FAILED(hr)) return Fail(ProviderStatus::Error, L"Eye provider StartCamera failed: " + HResultText(hr));
        cameraStarted = true;
        ready = true;
        state.status = ProviderStatus::Ready;
        state.sensorFreshnessKnown = 0;
        Log(L"Portable eye provider started without machine COM registration; observation times are not sensor frame times.");
        Poll();
        return true;
    }
    bool SetLens(bool enabled) {
        if (enabled && (!ready || lost || !state.calibrationValid || !state.eyePositionValid)) return false;
        if (static_cast<bool>(state.lensEnabled) == enabled) return true;
        if (!options.mock && !mcuOpened) return false;
        const int result = options.mock ? 0 : setMode(enabled);
        if (result) {
            Log(L"MCU set3DMode failed: " + std::to_wstring(result));
            return false;
        }
        state.lensEnabled = enabled ? 1u : 0u;
        Log(enabled ? L"Lens requested ON (software state; no hardware acknowledgement)." :
                      L"Lens requested OFF (queued by vendor SDK).");
        return true;
    }
    void Poll() {
        state.heartbeatTick = GetTickCount64();
        if (!ready || lost) return;
        if (options.mock) {
            state.observationTick = state.eyePairObservationTick = state.heartbeatTick;
            ++state.eyePairSequence;
            return;
        }
        if (state.heartbeatTick - lastUsbPoll >= 1000) {
            lastUsbPoll = state.heartbeatTick;
            const int usb = getUsbState();
            if (usb != 1 && usb != 2) {
                SetLens(false);
                lost = true;
                ready = false;
                Fail(ProviderStatus::NoDevice, L"The MCU disconnected; rendering and lens requests are disabled.");
                return;
            }
        }
        float x{}, y{}, z{}, unusedLeft{}, unusedRight{};
        VARIANTARG args[5]{};
        float* output[] = {&unusedRight, &unusedLeft, &z, &y, &x};
        for (std::size_t i = 0; i != std::size(args); ++i) {
            args[i].vt = VT_R4 | VT_BYREF;
            args[i].pfltVal = output[i];
        }
        HRESULT hr = Invoke(tracker, 6, args, static_cast<UINT>(std::size(args)));
        const auto previousEye = state.eyePositionValid;
        const auto now = GetTickCount64();
        BoeTrackingFrame pair{};
        bool havePair=pairReader.Read(pair);
        if(havePair)lastPair=pair;
        else if(pairReader.IsAlive() && lastPair.sequence){pair=lastPair;havePair=true;}
        if(!havePair && !pairReader.IsAlive() && now>=nextPairConnect){
            nextPairConnect=now+1000;
            lastPair={};
            std::uint64_t creation{};
            const auto pid=FindPortableTracker(options.vendorDirectory/L"eyetracking"/L"FaceTracking.exe",creation);
            if(pid && pairReader.Open(pid,creation)){
                havePair=pairReader.Read(pair);
                if(havePair)lastPair=pair;
            }
        }
        // Prefer a complete SDK result over the COM DLL's independently written
        // scalar fields. Keep COM as the display-only compatibility fallback.
        std::uint64_t observed=now;
        if(havePair){
            if(!pairConnectedLog){Log(L"已连接 BOE 完整双眼通道；使用 SDK 调用完成时间，不是曝光时间。");pairConnectedLog=true;}
            const bool fresh=pair.observationTick && pair.observationTick<=now && now-pair.observationTick<=250;
            hr=pair.centerValid&&fresh?S_OK:E_FAIL;
            x=pair.center[0];y=pair.center[1];z=pair.center[2];
            if(SUCCEEDED(hr))observed=pair.observationTick;
        }else if(pairConnectedLog)hr=E_FAIL; // A dead producer must not revive stale COM scalars.
        // The hold policy must retain raw coordinates, never feed prediction back.
        std::copy(rawEye.begin(), rawEye.end(), state.eye);
        SelectEyePosition(state, hr, x, y, z, observed);
        std::copy_n(state.eye, 3, rawEye.begin());
        const unsigned predictionMs = options.predictionMs ? std::min(options.predictionMs->load(), 40u) : 0;
        LARGE_INTEGER counter{}, frequency{};
        if (QueryPerformanceCounter(&counter) && QueryPerformanceFrequency(&frequency) && frequency.QuadPart > 0)
            prediction.Apply(state, static_cast<double>(counter.QuadPart) * 1000.0 / frequency.QuadPart, predictionMs);
        else prediction = {};
        state.eyePairValid=0;
        if(havePair && pair.pairValid)
            SelectEyePair(state,pair.center,pair.eyeA,pair.eyeB,pair.sequence,
                pair.observationTick,now,state.eye[0]-rawEye[0]);
        headPose.Update(state,havePair?&pair:nullptr,now);
        if (state.eyePositionValid != previousEye) {
            SetMessage(state, state.eyePositionValid == kEyeTracked ?
                L"设备就绪；使用跟踪眼位（传感器帧时间未知），可启动游戏。" :
                state.eyePositionValid == kEyeHeld ?
                L"设备就绪；短暂未获取眼位，保持上次位置，持续丢失超过 500 ms 后使用默认眼位。" :
                L"设备就绪；未获取有效眼位，使用默认 (0, 0, 600 mm)，可启动游戏；识别恢复后自动跟踪。");
            // Detector flicker must not cause a synchronous disk flush per poll.
            if (!previousEye || now - lastEyeLog >= 1000) { Log(state.message); lastEyeLog = now; }
        }
    }
    void Shutdown() {
        if (options.mock) {
            SetLens(false);
        } else if (mcuOpened) {
            // set3DMode(false) queues a command. A following synchronous getter
            // shares its FIFO and provides a bounded drain barrier, not a lens ACK.
            const int off = setMode(false);
            state.lensEnabled = 0;
            std::array<double, 6> discarded{};
            const int drain = getDevice(discarded.data());
            Log(L"MCU shutdown: off request=" + std::to_wstring(off) +
                L", queued read barrier=" + std::to_wstring(drain) +
                (drain ? L" (queue completion could not be confirmed)." : L" (not a physical lens acknowledgement)."));
        }
        if (cameraStarted) { const HRESULT hr = Invoke(tracker, 12); Log(L"Eye StopCamera: " + HResultText(hr)); cameraStarted = false; }
        if (trackerOpened) { const HRESULT hr = Invoke(tracker, 5); Log(L"Eye Close: " + HResultText(hr)); trackerOpened = false; }
        if (tracker) { tracker->Release(); tracker = nullptr; }
        if (comInitialized) { CoUninitialize(); comInitialized = false; }
        portableTracker.Reset();
        if (mcuOpened) { closeMcu(); mcuOpened = false; Log(L"MCU closed."); }
        if (sdkInitialized) { releaseSdk(); sdkInitialized = false; Log(L"MCU SDK released."); }
        if (module) { FreeLibrary(module); module = nullptr; }
        ready = false;
        state.calibrationValid = state.eyePositionValid = state.eyePairValid = state.headPoseValid = state.lensEnabled = 0;
    }
};

DeviceProvider::DeviceProvider(ProviderOptions options, LogFunction logger)
    : impl_(std::make_unique<Impl>(std::move(options), std::move(logger))) {}
DeviceProvider::~DeviceProvider() { impl_->Shutdown(); }
bool DeviceProvider::Initialize() { return impl_->Initialize(); }
void DeviceProvider::Poll() { impl_->Poll(); }
bool DeviceProvider::SetLens(bool enabled) { return impl_->SetLens(enabled); }
void DeviceProvider::Shutdown() { impl_->Shutdown(); }
const Snapshot& DeviceProvider::State() const noexcept { return impl_->state; }
} // namespace titan::broker
