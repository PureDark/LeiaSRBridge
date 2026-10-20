#include "Injector.h"
#include "GlobalRuntime.h"
#include "../broker/BrokerService.h"
#include "../common/PoseMath.h"
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
constexpr UINT kLogMessage = WM_APP + 1, kTrayMessage = WM_APP + 2;
constexpr int kGames=101, kAdd=102, kRemove=103, kLaunch=104, kAttach=105, kArgs=106,
    kAuto=107, kLogs=108, kStatus=109, kLog=110, kHide=111, kExit=112, kPrediction=113, kStereoTest=114,
    kOffsetEdit=115,kOffsetApply=116,kOffsetZero=117,kOffsetMinus=119,kOffsetPlus=120,
    kEnableGlobal=121,kDisableGlobal=122,kGlobalStatus=123;
constexpr int kPredictionHotkey=1;
auto predictionMs=std::make_shared<std::atomic<unsigned>>(40u);
auto horizontalTenths=std::make_shared<std::atomic<int>>(80);
unsigned lastEnabledPrediction=40;
struct Profile { std::filesystem::path executable; std::wstring arguments; };
struct Action { bool launch{}; Profile profile; };
std::filesystem::path appDirectory;
HWND mainWindow{}, gamesList{}, argsEdit{}, statusLabel{}, logEdit{}, autoCheck{};
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
    const auto status=app::ReadGlobalRuntimeStatus(appDirectory/L"runtime");
    std::wstring text;
    if(!status.error.empty())text=L"统一接入状态："+status.error;
    else if(status.pendingRecovery)text=L"上次配置未完成，请重新启用或恢复原厂接入。";
    else if(status.enabled)text=L"免列表接入已配置：泰坦桥接";
    else if(!status.managedDirectory.empty())text=L"已登记其他桥接目录；启用可更新到当前版本。";
    else text=L"免列表接入未启用；可继续使用下方兼容列表。";
    SetDlgItemTextW(mainWindow,kGlobalStatus,text.c_str());
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
    EnableWindow(GetDlgItem(mainWindow,kEnableGlobal),FALSE);
    EnableWindow(GetDlgItem(mainWindow,kDisableGlobal),FALSE);
}
void PollIntegration() {
    if(integrationProcess && WaitForSingleObject(integrationProcess,0)==WAIT_OBJECT_0){
        DWORD code{};GetExitCodeProcess(integrationProcess,&code);
        CloseHandle(integrationProcess);integrationProcess=nullptr;
        EnableWindow(GetDlgItem(mainWindow,kEnableGlobal),TRUE);
        EnableWindow(GetDlgItem(mainWindow,kDisableGlobal),TRUE);
        Log(code==0?L"接入配置已更新。请完全退出并重新打开 Steam 等启动器和游戏；旧进程不会自动更换运行库。":
            L"接入配置失败，请查看 logs/RuntimeIntegration.log。退出码="+std::to_wstring(code));
        if(code==0){
            autoAttach=false;SendMessageW(autoCheck,BM_SETCHECK,BST_UNCHECKED,0);SaveTuning();
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
        <<"\n[Integration]\nAutoAttachList="<<(autoAttach.load()?1:0)<<"\n";
    file.flush();const bool written=bool(file);file.close();
    if(!written||!MoveFileExW(temp.c_str(),target.c_str(),MOVEFILE_REPLACE_EXISTING|MOVEFILE_WRITE_THROUGH))
        Log(L"无法保存 settings.ini，请确认解压目录可写。");
}
void LoadTuning() {
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
}
void RefreshProfiles() {
    SendMessageW(gamesList,LB_RESETCONTENT,0,0);
    std::lock_guard lock(dataMutex);
    for(const auto& p:profiles)SendMessageW(gamesList,LB_ADDSTRING,0,reinterpret_cast<LPARAM>(p.executable.c_str()));
    if(!profiles.empty())SendMessageW(gamesList,LB_SETCURSEL,0,0);
}
void SelectionChanged() {
    const auto index=SendMessageW(gamesList,LB_GETCURSEL,0,0);
    std::lock_guard lock(dataMutex);
    if(index>=0 && static_cast<size_t>(index)<profiles.size())SetWindowTextW(argsEdit,profiles[index].arguments.c_str());
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
        if(haveAction && action.launch) {
            const auto state=service.ReadStatus();
            if(!state.calibrationValid || !state.eyePositionValid ||
                (state.status!=ProviderStatus::Ready && state.status!=ProviderStatus::Mock)) {
                Log(L"设备尚未就绪："+std::wstring(state.message));continue;
            }
            std::wstring error;DWORD pid{};
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
    SetDlgItemTextW(mainWindow,kPrediction,label.c_str());
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
    SetDlgItemTextW(mainWindow,kOffsetEdit,number);
    const auto label=std::wstring(L"全局水平眼位补偿：")+number+L" mm";
    SaveTuning();
    if(notify)NotifyTuning(label);
}
void UpdateStatus() {
    const auto s=service.ReadStatus();
    const wchar_t* state=L"等待";
    switch(s.status){case ProviderStatus::Ready:state=L"设备就绪";break;case ProviderStatus::Initializing:state=L"初始化";break;
        case ProviderStatus::NoDevice:state=L"未找到显示器";break;case ProviderStatus::Busy:state=L"设备被占用";break;
        case ProviderStatus::Error:state=L"设备错误";break;case ProviderStatus::Mock:state=L"离线模拟（不控制硬件）";break;default:break;}
    std::wstring text=std::wstring(state)+L"  | 校准："+(s.calibrationValid?L"可用":L"不可用")+L"  | 眼位："+
        (s.eyePositionValid==kEyeDefault?L"默认眼位":s.eyePositionValid==kEyeHeld?L"短暂保持":s.eyePositionValid?L"跟踪中":L"不可用")+L"  | 3D 请求："+(s.lensEnabled?L"开启":L"关闭")+L"\r\n"+s.message;
    const auto now=GetTickCount64();
    if(s.headPoseValid&&s.headPoseObservationTick&&s.headPoseObservationTick<=now&&now-s.headPoseObservationTick<=250){
        wchar_t angles[128]{};
        constexpr double degrees=180.0/3.141592653589793;
        swprintf_s(angles,L"\r\n头部旋转（估算）：俯仰 %+.1f°  左右 %+.1f°  侧倾 %+.1f°",
            std::atan2(s.headRotation[7],s.headRotation[8])*degrees,
            std::asin(std::clamp(-s.headRotation[6],-1.0,1.0))*degrees,
            std::atan2(s.headRotation[3],s.headRotation[0])*degrees);
        text+=angles;
    }else text+=L"\r\n头部旋转：等待有效的双目面部点";
    SetWindowTextW(statusLabel,text.c_str());
}
HWND Control(const wchar_t* cls,const wchar_t* text,DWORD style,int x,int y,int w,int h,int id) {
    HWND result=CreateWindowExW(cls==std::wstring(L"EDIT")?WS_EX_CLIENTEDGE:0,cls,text,WS_CHILD|WS_VISIBLE|style,
        x,y,w,h,mainWindow,reinterpret_cast<HMENU>(static_cast<INT_PTR>(id)),GetModuleHandleW(nullptr),nullptr);
    SendMessageW(result,WM_SETFONT,reinterpret_cast<WPARAM>(GetStockObject(DEFAULT_GUI_FONT)),TRUE);return result;
}
LRESULT CALLBACK WindowProc(HWND window,UINT message,WPARAM wParam,LPARAM lParam) {
    switch(message){
    case WM_CREATE:{
        mainWindow=window;
        Control(L"STATIC",L"TitanLeiaBridge  ·  SDK 1.34.10 / x64 / DX12",0,18,14,740,25,0);
        statusLabel=Control(L"STATIC",L"正在初始化…",0,18,45,740,62,kStatus);
        Control(L"BUTTON",L"启用免列表接入",BS_PUSHBUTTON,18,108,175,30,kEnableGlobal);
        Control(L"BUTTON",L"恢复原厂接入",BS_PUSHBUTTON,205,108,175,30,kDisableGlobal);
        Control(L"STATIC",L"读取接入配置…",0,394,110,364,40,kGlobalStatus);
        Control(L"STATIC",L"首次配置需管理员权限；完成后重启 Steam 等启动器。保持本程序后台运行，游戏内选择 LeiaSR。",0,18,149,740,34,0);
        Control(L"STATIC",L"兼容列表（可选）：游戏自带原厂 DLL 或统一接入未生效时使用。",0,18,190,740,23,0);
        gamesList=Control(L"LISTBOX",L"",WS_BORDER|WS_VSCROLL|LBS_NOTIFY,18,217,740,80,kGames);
        Control(L"BUTTON",L"添加游戏",BS_PUSHBUTTON,18,308,105,30,kAdd);
        Control(L"BUTTON",L"移除",BS_PUSHBUTTON,133,308,78,30,kRemove);
        Control(L"BUTTON",L"启动游戏",BS_DEFPUSHBUTTON,221,308,115,30,kLaunch);
        Control(L"BUTTON",L"接管已运行游戏",BS_PUSHBUTTON,346,308,150,30,kAttach);
        autoCheck=Control(L"BUTTON",L"自动接管列表中的游戏",BS_AUTOCHECKBOX,510,308,245,30,kAuto);
        SendMessageW(autoCheck,BM_SETCHECK,autoAttach?BST_CHECKED:BST_UNCHECKED,0);
        Control(L"STATIC",L"启动参数：",0,18,353,85,24,0);
        argsEdit=Control(L"EDIT",L"",ES_AUTOHSCROLL,104,349,654,28,kArgs);
        Control(L"STATIC",L"Ctrl+Alt+F9 开关预测；右侧按钮切换 0/10/20/30/40 ms。",0,18,389,490,24,0);
        Control(L"BUTTON",L"水平眼位预测：0 ms",BS_PUSHBUTTON,510,384,245,30,kPrediction);
        Control(L"STATIC",L"全局 X 补偿 (mm)：",0,18,427,145,24,0);
        Control(L"EDIT",L"0.0",ES_AUTOHSCROLL,165,422,70,28,kOffsetEdit);
        Control(L"BUTTON",L"应用",BS_PUSHBUTTON,240,422,60,28,kOffsetApply);
        Control(L"BUTTON",L"−0.5",BS_PUSHBUTTON,305,422,60,28,kOffsetMinus);
        Control(L"BUTTON",L"+0.5",BS_PUSHBUTTON,370,422,60,28,kOffsetPlus);
        Control(L"BUTTON",L"归零",BS_PUSHBUTTON,435,422,65,28,kOffsetZero);
        Control(L"STATIC",L"Ctrl+Alt+←/→ 调节，另加 Shift 微调；Ctrl+Alt+Home 归零；参数自动保存。",0,18,459,740,24,0);
        logEdit=Control(L"EDIT",L"",ES_MULTILINE|ES_READONLY|ES_AUTOVSCROLL|WS_VSCROLL,18,490,740,180,kLog);
        Control(L"BUTTON",L"打开日志",BS_PUSHBUTTON,18,684,110,30,kLogs);
        Control(L"BUTTON",L"原厂交织测试",BS_PUSHBUTTON,142,684,150,30,kStereoTest);
        Control(L"BUTTON",L"后台运行",BS_PUSHBUTTON,518,684,110,30,kHide);
        Control(L"BUTTON",L"退出转接器",BS_PUSHBUTTON,638,684,120,30,kExit);
        RefreshProfiles();SelectionChanged();
        tray.cbSize=sizeof(tray);tray.hWnd=window;tray.uID=1;tray.uFlags=NIF_MESSAGE|NIF_ICON|NIF_TIP;
        tray.uCallbackMessage=kTrayMessage;tray.hIcon=LoadIconW(nullptr,IDI_APPLICATION);
        wcscpy_s(tray.szTip,L"TitanLeiaBridge");Shell_NotifyIconW(NIM_ADD,&tray);
        if(!RegisterHotKey(window,kPredictionHotkey,MOD_CONTROL|MOD_ALT|MOD_NOREPEAT,VK_F9))
            Log(L"Ctrl+Alt+F9 已被占用，使用窗口内的水平眼位预测按钮。");
        const UINT keys[]{VK_LEFT,VK_RIGHT,VK_LEFT,VK_RIGHT,VK_HOME};
        for(int i=0;i<5;++i){
            const UINT mods=MOD_CONTROL|MOD_ALT|(i==2||i==3?MOD_SHIFT:0)|(i>=4?MOD_NOREPEAT:0);
            if(!RegisterHotKey(window,2+i,mods,keys[i]))Log(L"全局调节快捷键注册失败，编号="+std::to_wstring(2+i)+L"；可使用窗口按钮。");
        }
        SetTimer(window,1,500,nullptr);return 0;}
    case WM_TIMER:UpdateStatus();PollIntegration();return 0;
    case WM_HOTKEY:
        if(wParam==kPredictionHotkey)SetPrediction(predictionMs->load()?0:lastEnabledPrediction,true);
        if(wParam>=2&&wParam<=5)SetOffset(horizontalTenths->load()+(wParam==2?-5:wParam==3?5:wParam==4?-1:1),true);
        if(wParam==6)SetOffset(0,true);
        return 0;
    case kLogMessage:{std::unique_ptr<std::wstring> line(reinterpret_cast<std::wstring*>(lParam));
        if(GetWindowTextLengthW(logEdit)>60000)SetWindowTextW(logEdit,L"");
        SendMessageW(logEdit,EM_SETSEL,static_cast<WPARAM>(-1),-1);SendMessageW(logEdit,EM_REPLACESEL,FALSE,reinterpret_cast<LPARAM>(line->c_str()));return 0;}
    case kTrayMessage:
        if(lParam==WM_LBUTTONDBLCLK||lParam==WM_LBUTTONUP)Show();
        if(lParam==WM_RBUTTONUP){HMENU menu=CreatePopupMenu();AppendMenuW(menu,MF_STRING,1,L"显示窗口");AppendMenuW(menu,MF_STRING,2,L"退出");
            POINT p{};GetCursorPos(&p);SetForegroundWindow(window);const auto choice=TrackPopupMenu(menu,TPM_RETURNCMD|TPM_RIGHTBUTTON,p.x,p.y,0,window,nullptr);
            DestroyMenu(menu);if(choice==1)Show();if(choice==2)DestroyWindow(window);}return 0;
    case WM_COMMAND:
        switch(LOWORD(wParam)){
        case kEnableGlobal:ConfigureIntegration(true);break;
        case kDisableGlobal:ConfigureIntegration(false);break;
        case kGames:if(HIWORD(wParam)==LBN_SELCHANGE)SelectionChanged();break;
        case kAdd:{wchar_t path[32768]{};OPENFILENAMEW ofn{sizeof(ofn)};ofn.hwndOwner=window;ofn.lpstrFilter=L"游戏主程序 (*.exe)\0*.exe\0";
            ofn.lpstrFile=path;ofn.nMaxFile=static_cast<DWORD>(std::size(path));ofn.Flags=OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST|OFN_NOCHANGEDIR;
            if(GetOpenFileNameW(&ofn)){{std::lock_guard lock(dataMutex);profiles.push_back({path,{}});}SaveProfiles();RefreshProfiles();SelectionChanged();}break;}
        case kRemove:{const auto i=SendMessageW(gamesList,LB_GETCURSEL,0,0);{std::lock_guard lock(dataMutex);if(i>=0&&static_cast<size_t>(i)<profiles.size())profiles.erase(profiles.begin()+i);}SaveProfiles();RefreshProfiles();SelectionChanged();break;}
        case kLaunch:{const auto i=SendMessageW(gamesList,LB_GETCURSEL,0,0);wchar_t args[8192]{};GetWindowTextW(argsEdit,args,8192);
            {std::lock_guard lock(dataMutex);if(i<0||static_cast<size_t>(i)>=profiles.size())break;profiles[i].arguments=args;actions.push_back({true,profiles[i]});}
            SaveProfiles();wake.notify_one();break;}
        case kAttach:{{std::lock_guard lock(dataMutex);actions.push_back({false,{}});}wake.notify_one();break;}
        case kAuto:autoAttach=SendMessageW(autoCheck,BM_GETCHECK,0,0)==BST_CHECKED;SaveTuning();break;
        case kLogs:ShellExecuteW(window,L"open",(appDirectory/L"logs").c_str(),nullptr,nullptr,SW_SHOW);break;
        case kHide:ShowWindow(window,SW_HIDE);break;
        case kPrediction:SetPrediction((predictionMs->load()+10)%50,false);break;
        case kOffsetMinus:SetOffset(horizontalTenths->load()-5,false);break;
        case kOffsetPlus:SetOffset(horizontalTenths->load()+5,false);break;
        case kOffsetZero:SetOffset(0,false);break;
        case kOffsetApply:{
            wchar_t number[64]{};GetDlgItemTextW(window,kOffsetEdit,number,64);wchar_t* end{};
            const double value=wcstod(number,&end);
            if(end==number||*end||!std::isfinite(value)||value < -30||value>30)Log(L"请输入 −30 到 +30 mm 的数值。");
            else SetOffset(static_cast<int>(std::lround(value*10)),false);
            break;}
        case kStereoTest:{
            const auto result=ShellExecuteW(window,L"open",(appDirectory/L"TitanStereoTest.exe").c_str(),nullptr,appDirectory.c_str(),SW_SHOW);
            if(reinterpret_cast<INT_PTR>(result)<=32)Log(L"无法启动原厂交织测试，请完整解压新包。");
            break;}
        case kExit:DestroyWindow(window);break;
        }return 0;
    case WM_CLOSE:ShowWindow(window,SW_HIDE);return 0;
    case WM_DESTROY:for(int i=1;i<=6;++i)UnregisterHotKey(window,i);KillTimer(window,1);Shell_NotifyIconW(NIM_DELETE,&tray);PostQuitMessage(0);return 0;
    }
    return DefWindowProcW(window,message,wParam,lParam);
}
} // namespace

int WINAPI wWinMain(HINSTANCE instance,HINSTANCE,PWSTR,int) {
    SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
    wchar_t module[32768]{};GetModuleFileNameW(nullptr,module,static_cast<DWORD>(std::size(module)));
    appDirectory=std::filesystem::path(module).parent_path();
    int argc{};auto argv=CommandLineToArgvW(GetCommandLineW(),&argc);bool mock{},background{};DWORD duration{};int integration{};
    for(int i=1;i<argc;++i){if(std::wstring(argv[i])==L"--mock")mock=true;if(std::wstring(argv[i])==L"--background")background=true;
        if(std::wstring(argv[i])==L"--duration-ms"&&i+1<argc)duration=wcstoul(argv[++i],nullptr,10);
        if(std::wstring(argv[i])==L"--enable-global-runtime")integration=1;
        if(std::wstring(argv[i])==L"--disable-global-runtime")integration=-1;}
    LocalFree(argv);
    mockMode=mock;
    std::filesystem::create_directories(appDirectory/L"logs");
    if(integration){
        std::wstring error;
        const bool ok=!mock && app::ConfigureGlobalRuntime(appDirectory/L"runtime",integration>0,error);
        std::ofstream result(appDirectory/L"logs"/L"RuntimeIntegration.log",std::ios::binary|std::ios::app);
        result<<Utf8(ok?(integration>0?L"免列表接入已配置。重启启动器和游戏后生效。":L"已移除桥接管理的系统路径。重启启动器和游戏后恢复原有搜索路径。"):
            L"配置失败："+error)<<'\n';
        return ok?0:1;
    }
    logFile.open(appDirectory/L"logs"/L"TitanLeiaBridge.log",std::ios::binary|std::ios::app);
    std::ifstream games(appDirectory/L"games.txt",std::ios::binary);std::string line;
    while(std::getline(games,line)){if(!line.empty()&&line.back()=='\r')line.pop_back();const auto tab=line.find('\t');
        const auto path=Wide(line.substr(0,tab));if(!path.empty())profiles.push_back({path,tab==std::string::npos?L"":Wide(line.substr(tab+1))});}
    LoadTuning();
    WNDCLASSW wc{};wc.lpfnWndProc=WindowProc;wc.hInstance=instance;wc.lpszClassName=L"TitanLeiaBridge.Main.v1";
    wc.hCursor=LoadCursorW(nullptr,IDC_ARROW);wc.hbrBackground=reinterpret_cast<HBRUSH>(COLOR_WINDOW+1);wc.hIcon=LoadIconW(nullptr,IDI_APPLICATION);
    RegisterClassW(&wc);
    HWND window=CreateWindowW(wc.lpszClassName,L"TitanLeiaBridge",WS_OVERLAPPED|WS_CAPTION|WS_SYSMENU|WS_MINIMIZEBOX,
        CW_USEDEFAULT,CW_USEDEFAULT,795,774,nullptr,nullptr,instance,nullptr);
    if(!window)return 1;
    broker::ProviderOptions options;options.mock=mock;options.logDirectory=appDirectory/L"logs";options.vendorDirectory=appDirectory/L"vendor";
    options.predictionMs=predictionMs;
    options.horizontalTenths=horizontalTenths;
    if(!service.Start(options,Log)){MessageBoxW(window,L"转接器已在运行，或无法建立后台服务。",L"TitanLeiaBridge",MB_ICONINFORMATION);DestroyWindow(window);return 1;}
    SetPrediction(predictionMs->load(),false);SetOffset(horizontalTenths->load(),false);
    worker=std::thread(Worker);if(!background)ShowWindow(window,SW_SHOW);
    Log(L"后台进程已启动，设备就绪状态请查看上方。SDK 1.34.10 / x64 / DX12；便携设备组件。");
    const auto end=duration?GetTickCount64()+duration:0;
    MSG message{};
    while(GetMessageW(&message,nullptr,0,0)>0){TranslateMessage(&message);DispatchMessageW(&message);if(end&&GetTickCount64()>=end)DestroyWindow(window);}
    stopping=true;wake.notify_all();service.Stop();if(worker.joinable())worker.join();
    if(integrationProcess)CloseHandle(integrationProcess);
    mainWindow=nullptr;
    return 0;
}
