#include "Injector.h"
#include "GlobalRuntime.h"
#include "Startup.h"
#include "WebUi.h"
#include <nlohmann/json.hpp>
#include <dwmapi.h>
#include <windowsx.h>
#include "../broker/BrokerService.h"
#include "../render/OpticalConstants.h"
#include <CommCtrl.h>
#include <commdlg.h>
#include <shellapi.h>
#include <atomic>
#include <chrono>
#include <condition_variable>
#include <deque>
#include <fstream>
#include <mutex>
#include <set>
#include <thread>
#include <algorithm>
#include <cmath>

namespace {
using namespace titan;
constexpr UINT kLogMessage = WM_APP + 1, kTrayMessage = WM_APP + 2, kVisibilityMessage = WM_APP + 3;
constexpr int kExit=112;
constexpr int kPredictionHotkey=1;
auto predictionMs=std::make_shared<std::atomic<unsigned>>(40u);
auto horizontalTenths=std::make_shared<std::atomic<int>>(80);
unsigned lastEnabledPrediction=40;
struct Profile { std::filesystem::path executable; std::wstring arguments; };
struct Action { bool launch{}; Profile profile; bool odyssey{}; };
std::filesystem::path appDirectory;
HWND mainWindow{};
app::WebUi ui;
bool uiReady{},uiTest{};
std::deque<std::wstring> uiLogs;
app::GlobalRuntimeStatus integrationStatus;
app::StartupStatus startupStatus;
std::filesystem::path hubExecutable=L"C:\\Program Files\\Odyssey 3D Hub\\Odyssey3DHubUI.exe";
std::atomic<DWORD> hubOwnedPid{};
std::atomic<bool> hubLaunching{};
broker::BrokerService service;
std::mutex dataMutex, fileMutex;
std::condition_variable wake;
std::vector<Profile> profiles;
std::deque<Action> actions;
std::thread worker;
std::atomic<bool> stopping{}, autoAttach{false};
std::ofstream logFile;
NOTIFYICONDATAW tray{};
bool mockMode{};
HANDLE integrationProcess{};
ULONGLONG nextIntegrationStatus{};
void SaveTuning();

void RefreshIntegrationStatus() {
    integrationStatus=app::ReadGlobalRuntimeStatus(appDirectory/L"runtime");
    startupStatus=app::ReadStartupStatus(appDirectory/L"TitanLeiaBridge.exe");
}

std::string Utf8(const std::wstring& value) {
    if(value.empty()) return {};
    const int size=WideCharToMultiByte(CP_UTF8,0,value.data(),static_cast<int>(value.size()),nullptr,0,nullptr,nullptr);
    std::string out(size,0); WideCharToMultiByte(CP_UTF8,0,value.data(),static_cast<int>(value.size()),out.data(),size,nullptr,nullptr); return out;
}
std::wstring Wide(const std::string& value) {
    if(value.empty()) return {};
    const int size=MultiByteToWideChar(CP_UTF8,0,value.data(),static_cast<int>(value.size()),nullptr,0);
    std::wstring out(size,0); MultiByteToWideChar(CP_UTF8,0,value.data(),static_cast<int>(value.size()),out.data(),size); return out;
}
void Log(const std::wstring& line) {
    SYSTEMTIME now{}; GetLocalTime(&now); wchar_t time[32]{};
    swprintf_s(time,L"%02u:%02u:%02u ",now.wHour,now.wMinute,now.wSecond);
    const std::wstring text=std::wstring(time)+line;
    { std::lock_guard lock(fileMutex); if(logFile){logFile<<Utf8(text)<<'\n'; logFile.flush();} }
    if(mainWindow){ auto* copy=new std::wstring(text+L"\r\n"); if(!PostMessageW(mainWindow,kLogMessage,0,reinterpret_cast<LPARAM>(copy)))delete copy; }
}
void ConfigureIntegration(bool enable) {
    if(integrationProcess)return;
    RefreshIntegrationStatus();
    if(enable&&integrationStatus.enabled){
        const std::wstring message=L"免列表接入已经启用并永久保存，重启后仍有效，无需再次授权。";
        Log(message);if(uiReady)ui.Post(Wide(nlohmann::json{{"type","notice"},{"text",Utf8(message)}}.dump()));return;
    }
    if(mockMode){Log(L"离线模拟不修改系统接入配置。");return;}
    const auto exe=appDirectory/L"TitanLeiaBridge.exe";
    SHELLEXECUTEINFOW request{sizeof(request)};
    request.fMask=SEE_MASK_NOCLOSEPROCESS|SEE_MASK_NOASYNC;
    request.hwnd=mainWindow;request.lpVerb=L"runas";request.lpFile=exe.c_str();
    request.lpParameters=enable?L"--enable-global-runtime":L"--disable-global-runtime";
    request.lpDirectory=appDirectory.c_str();request.nShow=SW_HIDE;
    if(!ShellExecuteExW(&request)){
        Log(GetLastError()==ERROR_CANCELLED?L"已取消系统接入配置。":L"无法启动系统接入配置："+app::WindowsError(GetLastError()));
        return;
    }
    integrationProcess=request.hProcess;

}
void PollIntegration() {
    if(integrationProcess && WaitForSingleObject(integrationProcess,0)==WAIT_OBJECT_0){
        DWORD code{};GetExitCodeProcess(integrationProcess,&code);
        CloseHandle(integrationProcess);integrationProcess=nullptr;

        Log(code==0?L"接入配置已更新。请完全退出并重新打开 Steam 等启动器和游戏；旧进程不会自动更换运行库。":
            L"接入配置失败，请查看 logs/RuntimeIntegration.log。退出码="+std::to_wstring(code));
        if(code==0){
            autoAttach=false;SaveTuning();
        }
        nextIntegrationStatus=0;
    }
    if(GetTickCount64()>=nextIntegrationStatus){
        nextIntegrationStatus=GetTickCount64()+2000;RefreshIntegrationStatus();
    }
}
void SaveTuning() {
    const auto temp=appDirectory/L"settings.ini.tmp", target=appDirectory/L"settings.ini";
    std::ofstream file(temp,std::ios::binary|std::ios::trunc);
    file<<"[Tracking]\nHorizontalOffsetTenths="<<horizontalTenths->load()
        <<"\nPredictionMs="<<predictionMs->load()<<"\nLastEnabledPredictionMs="<<lastEnabledPrediction
        <<"\n[Integration]\nAutoAttachList="<<(autoAttach.load()?1:0)<<"\n[Odyssey]\nExecutable="<<Utf8(hubExecutable.wstring())<<"\n";
    file.flush();const bool written=bool(file);file.close();
    if(!written||!MoveFileExW(temp.c_str(),target.c_str(),MOVEFILE_REPLACE_EXISTING|MOVEFILE_WRITE_THROUGH)){
        Log(L"无法保存 settings.ini，请确认解压目录可写。");
        if(uiReady)ui.Post(LR"({"type":"notice","text":"设置未保存，请确认解压目录可写。"})");
    }else if(uiReady)ui.Post(LR"({"type":"saved"})");
}
void LoadTuning() {
    std::ifstream settings(appDirectory/L"settings.ini",std::ios::binary);std::string row;
    while(std::getline(settings,row))if(row.starts_with("Executable=")){
        if(!row.empty()&&row.back()=='\r')row.pop_back();
        const auto path=Wide(row.substr(11));if(!path.empty())hubExecutable=path;
    }
    const auto path=appDirectory/L"settings.ini";
    const int offset=static_cast<int>(GetPrivateProfileIntW(L"Tracking",L"HorizontalOffsetTenths",80,path.c_str()));
    const unsigned prediction=GetPrivateProfileIntW(L"Tracking",L"PredictionMs",40,path.c_str());
    horizontalTenths->store(std::clamp(offset,-300,300));
    predictionMs->store(prediction<=40&&prediction%10==0?prediction:40);
    const unsigned last=GetPrivateProfileIntW(L"Tracking",L"LastEnabledPredictionMs",40,path.c_str());
    lastEnabledPrediction=last>=10&&last<=40&&last%10==0?last:40;
    autoAttach=GetPrivateProfileIntW(L"Integration",L"AutoAttachList",0,path.c_str())!=0;
}
void SaveProfiles() {
    std::lock_guard lock(dataMutex);
    std::ofstream file(appDirectory/L"games.txt",std::ios::binary|std::ios::trunc);
    for(const auto& p:profiles)file<<Utf8(p.executable.wstring())<<'\t'<<Utf8(p.arguments)<<'\n';
    file.flush();
    if(!file){Log(L"无法保存 games.txt，请确认解压目录可写。");if(uiReady)ui.Post(LR"({"type":"notice","text":"兼容列表未保存，请确认解压目录可写。"})");}
    else if(uiReady)ui.Post(LR"({"type":"saved"})");
}
void Worker() {
    std::set<DWORD> attempted;
    while(!stopping) {
        Action action; bool haveAction{};
        std::vector<Profile> list;
        {
            std::unique_lock lock(dataMutex);
            wake.wait_for(lock,std::chrono::milliseconds(400),[]{return stopping || !actions.empty();});
            if(stopping)break;
            if(!actions.empty()){ action=std::move(actions.front());actions.pop_front();haveAction=true; }
            list=profiles;
        }
        struct LaunchGuard {bool active;~LaunchGuard(){if(active)hubLaunching=false;}} guard{haveAction&&action.odyssey};
        if(haveAction && action.launch) {
            const auto state=service.ReadStatus();
            if(!state.calibrationValid || !state.eyePositionValid ||
                (state.status!=ProviderStatus::Ready && state.status!=ProviderStatus::Mock)) {
                Log(L"设备尚未就绪："+std::wstring(state.message));continue;
            }
            std::wstring error;DWORD pid{};
            if(action.odyssey){
                bool exists=false;
                for(const auto& p:app::Processes())if(_wcsicmp(p.executable.filename().c_str(),L"Odyssey3DHubUI.exe")==0||_wcsicmp(p.executable.filename().c_str(),L"Odyssey3DHubService.exe")==0)exists=true;
                if(exists){Log(L"请完全退出已运行的 Odyssey 3D Hub（包括后台服务），再使用兼容启动。");continue;}
                const auto hub=action.profile.executable.parent_path();
                DWORD servicePid{};
                if(!std::filesystem::is_regular_file(action.profile.executable)||!app::LaunchWithRuntime(hub/L"Odyssey3DHubService.exe",L"",appDirectory/L"runtime",servicePid,error,&stopping,true)){
                    Log(L"Odyssey 服务启动失败："+error);continue;
                }
                if(!app::LaunchWithRuntime(action.profile.executable,L"-force-d3d11",appDirectory/L"runtime",pid,error,&stopping,true)){
                    HANDLE child=OpenProcess(PROCESS_TERMINATE,FALSE,servicePid);if(child){TerminateProcess(child,1);CloseHandle(child);}
                    Log(L"Odyssey 界面启动失败："+error);continue;
                }
                attempted.insert(pid);attempted.insert(servicePid);hubOwnedPid=pid;
                Log(L"Odyssey 兼容启动已完成；界面与直接启动的 x64 子进程已接入，实际 3D 输出待验证。");continue;
            }
            if(app::LaunchWithRuntime(action.profile.executable,action.profile.arguments,appDirectory/L"runtime",pid,error,&stopping)) {
                attempted.insert(pid); Log(L"兼容运行时已在游戏入口前加载，PID="+std::to_wstring(pid));
            } else Log(L"启动失败："+error);
        }
        if((!autoAttach || list.empty()) && !(haveAction&&!action.launch))continue;
        if(haveAction&&!action.launch)attempted.clear(); // An explicit retry must retry prior failures.
        const auto state=service.ReadStatus();
        if(!state.calibrationValid || !state.eyePositionValid ||
            (state.status!=ProviderStatus::Ready && state.status!=ProviderStatus::Mock))continue;
        const auto processes=app::Processes();
        std::set<DWORD> alive;
        for(const auto& process:processes)alive.insert(process.pid);
        for(auto it=attempted.begin();it!=attempted.end();)if(!alive.count(*it))it=attempted.erase(it);else ++it;
        for(const auto& process:processes) {
            if(stopping)break;
            if(attempted.count(process.pid)||process.pid==GetCurrentProcessId())continue;
            bool match{};
            for(const auto& profile:list) {std::error_code ec;if(std::filesystem::equivalent(process.executable,profile.executable,ec)){match=true;break;}}
            if(!match)continue;
            attempted.insert(process.pid); std::wstring error;
            if(app::InjectRuntime(process.pid,appDirectory/L"runtime",error,&stopping))Log(L"已接管 PID="+std::to_wstring(process.pid)+L" "+process.executable.filename().wstring());
            else Log(L"接管失败 PID="+std::to_wstring(process.pid)+L"："+error);
        }
    }
}
void Show() {ShowWindow(mainWindow,SW_RESTORE);SetForegroundWindow(mainWindow);}
void SetPrediction(unsigned value, bool notify) {
    predictionMs->store(value);
    if(value)lastEnabledPrediction=value;
    const std::wstring label=L"水平眼位预测："+std::to_wstring(value)+L" ms";

    SaveTuning();
    if(notify){tray.uFlags=NIF_INFO;wcscpy_s(tray.szInfoTitle,L"TitanLeiaBridge");
        const std::wstring info=label+L"；Ctrl+Alt+F9 开关";
        wcsncpy_s(tray.szInfo,info.c_str(),_TRUNCATE);tray.dwInfoFlags=NIIF_INFO;
        Shell_NotifyIconW(NIM_MODIFY,&tray);}
}
void NotifyTuning(const std::wstring& label) {
    tray.uFlags=NIF_TIP;wcsncpy_s(tray.szTip,label.c_str(),_TRUNCATE);Shell_NotifyIconW(NIM_MODIFY,&tray);
    static ULONGLONG last{};const auto now=GetTickCount64();
    if(now-last<500)return;
    last=now;tray.uFlags=NIF_INFO;wcscpy_s(tray.szInfoTitle,L"TitanLeiaBridge");
    wcsncpy_s(tray.szInfo,label.c_str(),_TRUNCATE);tray.dwInfoFlags=NIIF_INFO;Shell_NotifyIconW(NIM_MODIFY,&tray);
}
void SetOffset(int value,bool notify) {
    value=std::clamp(value,-300,300);horizontalTenths->store(value);
    wchar_t number[32]{};swprintf_s(number,L"%+.1f",value*0.1);

    const auto label=std::wstring(L"全局水平眼位补偿：")+number+L" mm";
    SaveTuning();
    if(notify)NotifyTuning(label);
}
#include "UiBindings.inl"
} // namespace

int WINAPI wWinMain(HINSTANCE instance,HINSTANCE,PWSTR,int) {
    const HRESULT com=CoInitializeEx(nullptr,COINIT_APARTMENTTHREADED);
    if(FAILED(com))return 1;
    SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
    wchar_t module[32768]{};GetModuleFileNameW(nullptr,module,static_cast<DWORD>(std::size(module)));
    appDirectory=std::filesystem::path(module).parent_path();
    int argc{};auto argv=CommandLineToArgvW(GetCommandLineW(),&argc);bool mock{},background{};DWORD duration{};int integration{};
    for(int i=1;i<argc;++i){if(std::wstring(argv[i])==L"--mock")mock=true;if(std::wstring(argv[i])==L"--background")background=true;
        if(std::wstring(argv[i])==L"--ui-test")uiTest=true;
        if(std::wstring(argv[i])==L"--duration-ms"&&i+1<argc)duration=wcstoul(argv[++i],nullptr,10);
        if(std::wstring(argv[i])==L"--enable-global-runtime")integration=1;
        if(std::wstring(argv[i])==L"--disable-global-runtime")integration=-1;}
    LocalFree(argv);
    mockMode=mock;uiTest=uiTest&&mock;
    std::filesystem::create_directories(appDirectory/L"logs");
    if(integration){
        std::wstring error;
        const bool ok=!mock && app::ConfigureGlobalRuntime(appDirectory/L"runtime",integration>0,error);
        std::ofstream result(appDirectory/L"logs"/L"RuntimeIntegration.log",std::ios::binary|std::ios::app);
        result<<Utf8(ok?(integration>0?L"免列表接入已配置。重启启动器和游戏后生效。":L"已移除桥接管理的系统路径。重启启动器和游戏后恢复原有搜索路径。"):
            L"配置失败："+error)<<'\n';
        return ok?0:1;
    }
    if(const auto existing=FindWindowW(L"TitanLeiaBridge.Main.v1",nullptr)){
        if(!background){ShowWindow(existing,SW_RESTORE);SetForegroundWindow(existing);}
        CoUninitialize();return 0;
    }
    logFile.open(appDirectory/L"logs"/L"TitanLeiaBridge.log",std::ios::binary|std::ios::app);
    std::ifstream games(appDirectory/L"games.txt",std::ios::binary);std::string line;
    while(std::getline(games,line)){if(!line.empty()&&line.back()=='\r')line.pop_back();const auto tab=line.find('\t');
        const auto path=Wide(line.substr(0,tab));if(!path.empty())profiles.push_back({path,tab==std::string::npos?L"":Wide(line.substr(tab+1))});}
    LoadTuning();
    WNDCLASSW wc{};wc.lpfnWndProc=WindowProc;wc.hInstance=instance;wc.lpszClassName=L"TitanLeiaBridge.Main.v1";
    wc.hCursor=LoadCursorW(nullptr,IDC_ARROW);wc.hbrBackground=CreateSolidBrush(RGB(250,249,246));wc.hIcon=LoadIconW(instance,MAKEINTRESOURCEW(101));
    RegisterClassW(&wc);
    HWND window=CreateWindowW(wc.lpszClassName,L"TitanLeiaBridge",WS_POPUP|WS_THICKFRAME|WS_SYSMENU|WS_MINIMIZEBOX,
        CW_USEDEFAULT,CW_USEDEFAULT,MulDiv(920,GetDpiForSystem(),96),MulDiv(760,GetDpiForSystem(),96),nullptr,nullptr,instance,nullptr);
    if(!window)return 1;
    broker::ProviderOptions options;options.mock=mock;options.logDirectory=appDirectory/L"logs";options.vendorDirectory=appDirectory/L"vendor";
    options.predictionMs=predictionMs;
    options.horizontalTenths=horizontalTenths;
    if(!service.Start(options,Log)){if(!background)MessageBoxW(window,L"转接器已在运行，或无法建立后台服务。",L"TitanLeiaBridge",MB_ICONINFORMATION);DestroyWindow(window);return 1;}
    SetPrediction(predictionMs->load(),false);SetOffset(horizontalTenths->load(),false);
    worker=std::thread(Worker);if(!background)ShowWindow(window,SW_SHOW);
    Log(L"后台进程已启动，设备就绪状态请查看上方。SDK 1.34.10 / x64 / DX11·DX12；便携设备组件。");
    const auto end=duration?GetTickCount64()+duration:0;
    MSG message{};
    while(GetMessageW(&message,nullptr,0,0)>0){TranslateMessage(&message);DispatchMessageW(&message);if(end&&GetTickCount64()>=end)DestroyWindow(window);}
    stopping=true;wake.notify_all();service.Stop();if(worker.joinable())worker.join();
    if(integrationProcess)CloseHandle(integrationProcess);
    mainWindow=nullptr;
    CoUninitialize();
    return 0;
}
