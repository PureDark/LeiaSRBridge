// Included inside Main.cpp's private namespace: UI commands reuse the existing broker/worker.
using Json=nlohmann::json;
void PublishUi();
bool uiAnimationActive=true;
void UpdatePoseTimer(){
    KillTimer(mainWindow,2);
    if(uiReady&&uiAnimationActive&&IsWindowVisible(mainWindow)&&!IsIconic(mainWindow))SetTimer(mainWindow,2,16,nullptr);
}
void Notice(const std::wstring& text){ui.Post(Wide(Json{{"type","notice"},{"text",Utf8(text)}}.dump()));}
void SelectExecutable(bool hub) {
    wchar_t path[32768]{};OPENFILENAMEW dialog{sizeof(dialog)};
    dialog.hwndOwner=mainWindow;dialog.lpstrFilter=L"应用程序 (*.exe)\0*.exe\0";
    dialog.lpstrTitle=hub?L"选择 Odyssey3DHubUI.exe":L"添加游戏主程序";
    dialog.lpstrFile=path;dialog.nMaxFile=static_cast<DWORD>(std::size(path));
    dialog.Flags=OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST|OFN_NOCHANGEDIR;
    if(!GetOpenFileNameW(&dialog))return;
    if(hub){
        if(_wcsicmp(std::filesystem::path(path).filename().c_str(),L"Odyssey3DHubUI.exe")!=0){Notice(L"请选择 Odyssey3DHubUI.exe。");return;}
        hubExecutable=path;SaveTuning();
    }else{
        {std::lock_guard lock(dataMutex);profiles.push_back({path,{}});}
        SaveProfiles();
    }
}
void HandleUi(const std::wstring& message) {
    try{
        const auto data=Json::parse(Utf8(message));
        const auto action=data.value("action",std::string{});
        if(nativeMode&&nativePreviewTest&&action=="native-preview-test-result"){
            std::ofstream file(appDirectory/L"logs"/L"native-preview-test.json");file<<data.dump(2);return;
        }
        if(nativeMode&&nativePreviewTest&&action=="native-preview-test-capture"){
            ui.Capture(appDirectory/L"logs"/L"native-preview.png");return;
        }
        if(action=="preview-mode"){
            if(mockMode){Notice(L"离线模拟不切换真实设备模式。");return;}
            restartMode=data.at("value").get<bool>()?1:0;SaveTuning();
            PostMessageW(mainWindow,WM_COMMAND,kExit,0);return;
        }
        if(nativeMode&&action!="ready"&&action!="viz-active"&&action!="ui-error"&&action!="logs"&&action!="hide"&&action!="minimize"&&action!="drag"&&action!="exit"){
            Notice(L"原生预览只读取追踪；请切回设备桥接模式后使用此功能。");return;
        }
        if(action=="ui-error"){Log(Wide(data.dump()));}
        else if(action=="ready"){
            uiReady=true;PublishUi();UpdatePoseTimer();
            for(const auto& line:uiLogs)ui.Post(Wide(Json{{"type","log"},{"text",Utf8(line)}}.dump()));
            if(uiTest||(nativeMode&&nativePreviewTest)){
                const auto r=FindResourceW(nullptr,MAKEINTRESOURCEW(nativeMode?104:103),RT_RCDATA);
                const auto bytes=SizeofResource(nullptr,r);
                const auto script=static_cast<const char*>(LockResource(LoadResource(nullptr,r)));
                if(script)ui.Script(Wide(std::string(script,bytes)));
            }
        }
        else if(action=="viz-active"){
            uiAnimationActive=data.at("value").get<bool>();UpdatePoseTimer();
        }
        else if(action=="offset"){
            const int value=data.at("value").get<int>();
            if(value>=-300&&value<=300)SetOffset(value,false);
        }else if(action=="prediction"){
            const unsigned value=data.at("value").get<unsigned>();
            if(value<=40&&value%10==0)SetPrediction(value,false);
        }else if(action=="auto"){autoAttach=data.at("value").get<bool>();SaveTuning();}
        else if(action=="startup"){
            if(mockMode)Notice(L"离线模拟不修改 Windows 自启动设置。");
            else{
                std::wstring error;const bool enable=data.at("value").get<bool>();
                if(app::ConfigureStartup(appDirectory/L"Stereo3DBridge.exe",enable,error))
                    Notice(enable?L"已开启：登录 Windows 后自动在后台运行。":L"已关闭开机自启动，免列表接入配置仍保留。");
                else{Notice(error);Log(error);}
            }
            RefreshIntegrationStatus();
        }
        else if(action=="enable-integration")ConfigureIntegration(true);
        else if(action=="restore-integration")ConfigureIntegration(false);
        else if(action=="add-game")SelectExecutable(false);
        else if(action=="select-hub")SelectExecutable(true);
        else if(action=="refresh-hub")nextIntegrationStatus=0;
        else if(action=="remove-game"||action=="save-game"||action=="launch"){
            const auto i=data.at("index").get<size_t>();
            {std::lock_guard lock(dataMutex);
                if(i>=profiles.size())return;
                if(action=="remove-game")profiles.erase(profiles.begin()+i);
                else if(action=="save-game")profiles[i].arguments=Wide(data.at("args").get<std::string>());
                else actions.push_back({true,profiles[i]});
            }
            if(action!="launch")SaveProfiles();else wake.notify_one();
        }else if(action=="attach"){
            {std::lock_guard lock(dataMutex);actions.push_back({false,{}});}wake.notify_one();
        }else if(action=="hub"){
            if(!hubLaunching.exchange(true)){
                {std::lock_guard lock(dataMutex);actions.push_back({true,{hubExecutable,L""},true});}wake.notify_one();
            }
        }else if(action=="logs")ShellExecuteW(mainWindow,L"open",(appDirectory/L"logs").c_str(),nullptr,nullptr,SW_SHOW);
        else if(action=="stereo"){
            if(reinterpret_cast<INT_PTR>(ShellExecuteW(mainWindow,L"open",(appDirectory/L"Stereo3DBridgeTest.exe").c_str(),nullptr,appDirectory.c_str(),SW_SHOW))<=32)
                Log(L"无法启动交织测试，请完整解压安装包。");
        }else if(action=="hide")ShowWindow(mainWindow,SW_HIDE);
        else if(action=="minimize")ShowWindow(mainWindow,SW_MINIMIZE);
        else if(action=="drag"){ReleaseCapture();PostMessageW(mainWindow,WM_NCLBUTTONDOWN,HTCAPTION,0);}
        else if(action=="exit")PostMessageW(mainWindow,WM_COMMAND,kExit,0);
        else if(action=="ui-test-result"&&uiTest){
            std::ofstream file(appDirectory/L"logs"/L"ui-test.json");file<<data.dump(2);
        }else if(action=="ui-test-capture"&&uiTest){
            const auto name=data.value("name",std::string{});
            if(name=="overview"||name=="games"||name=="diag"||name=="odyssey")ui.Capture(appDirectory/L"logs"/Wide("ui-"+name+".png"));
        }
        PublishUi();
    }catch(const std::exception&){Log(L"界面命令无效，未执行该操作。");}
}
// Animation-only packets avoid rebuilding profiles, status and diagnostics at frame rate.
Json UiPose(const Snapshot& s){
    const auto now=GetTickCount64();
    const bool rotation=s.headPoseValid&&s.headPoseObservationTick&&s.headPoseObservationTick<=now&&now-s.headPoseObservationTick<=250;
    constexpr double degrees=180.0/3.141592653589793;
    render::OpticalConstants optical{};
    const bool position=render::MakeOpticalConstants(s,1,1,2,1,optical)&&optical.eyeZ>0;
    return Json{{"x",position?optical.eyeX:0.f},{"y",position?optical.eyeY:0.f},{"z",position?optical.eyeZ:600.f},
        {"pitch",rotation?std::atan2(s.headRotation[7],s.headRotation[8])*degrees:0},
        {"yaw",rotation?std::asin(std::clamp(-s.headRotation[6],-1.0,1.0))*degrees:0},
        {"roll",rotation?std::atan2(s.headRotation[3],s.headRotation[0])*degrees:0}};
}
Json NativeUiPose(const app::NativePreviewState& s){
    return Json{{"x",s.x},{"y",s.y},{"z",s.z},{"pitch",s.rotation?s.pitch:0},
        {"yaw",s.rotation?s.yaw:0},{"roll",s.rotation?s.roll:0}};
}
void PublishPose(){
    if(uiReady)ui.Post(Wide(Json{{"type","pose"},{"pose",nativeMode?NativeUiPose(nativePreview.Read()):UiPose(service.ReadStatus())}}.dump()));
}
void PublishUi(){
    if(!uiReady)return;
    const auto n=nativePreview.Read();
    const auto s=nativeMode?Snapshot{}:service.ReadStatus();
    const bool ready=nativeMode?n.connected:(s.status==ProviderStatus::Ready||s.status==ProviderStatus::Mock)&&s.calibrationValid&&s.eyePositionValid;
    const char* device=ready?"ready":nativeMode?(n.error?"error":"init"):s.status==ProviderStatus::Busy?"busy":s.status==ProviderStatus::NoDevice?"nodevice":s.status==ProviderStatus::Error?"error":"init";
    const auto now=GetTickCount64();
    const bool rotation=s.headPoseValid&&s.headPoseObservationTick&&s.headPoseObservationTick<=now&&now-s.headPoseObservationTick<=250;
    const auto pose=nativeMode?NativeUiPose(n):UiPose(s);
    Json games=Json::array();
    {std::lock_guard lock(dataMutex);for(const auto& p:profiles)games.push_back({{"exe",Utf8(p.executable.filename().wstring())},{"path",Utf8(p.executable.wstring())},{"args",Utf8(p.arguments)},{"color","#5D5952"}});}
    static ULONGLONG nextProcesses{};
    static bool hubRunning{},hubSession{};
    static std::string clients="—";
    if(now>=nextProcesses){
        nextProcesses=now+2000;hubRunning=false;hubSession=false;clients.clear();
        const auto processes=app::Processes();
        for(const auto& p:processes){
            const auto name=p.executable.filename();
            if(_wcsicmp(name.c_str(),L"Odyssey3DHubUI.exe")==0||_wcsicmp(name.c_str(),L"Odyssey3DHubService.exe")==0)hubRunning=true;
            if(p.pid==hubOwnedPid.load())hubSession=true;
        }
        HANDLE mapping=nativeMode?nullptr:OpenFileMappingW(FILE_MAP_READ,FALSE,kMappingName);
        if(mapping){
            const auto shared=static_cast<const SharedState*>(MapViewOfFile(mapping,FILE_MAP_READ,0,0,sizeof(SharedState)));
            if(shared&&shared->magic==kProtocolMagic&&shared->version==kProtocolVersion&&shared->size==sizeof(SharedState)){
                for(const auto& slot:shared->clients){
                    const auto pid=static_cast<DWORD>(slot.processId);const auto tick=static_cast<ULONGLONG>(slot.heartbeatTick);
                    if(!pid||!tick||tick>now||now-tick>kLeaseMs)continue;
                    for(const auto& p:processes)if(p.pid==pid){
                        if(!clients.empty())clients+=" · ";clients+=Utf8(p.executable.stem().wstring())+" ("+std::to_string(pid)+")";break;
                    }
                }
            }
            if(shared)UnmapViewOfFile(shared);CloseHandle(mapping);
        }
        if(clients.empty())clients="—";
    }
    std::error_code ec;const bool installed=std::filesystem::is_regular_file(hubExecutable,ec);
    const char* eye=nativeMode?(n.face?"原厂跟踪中":"等待人脸"):s.eyePositionValid==kEyeTracked?"跟踪中":s.eyePositionValid==kEyeHeld?"短暂保持":s.eyePositionValid==kEyeDefault?"默认眼位":"不可用";
    Json state{{"device",device},{"displayName",nativeMode?"LeiaSR 原厂服务":""},{"face",nativeMode?n.face:s.eyePositionValid==kEyeTracked},{"nativePreview",nativeMode},
        {"recommendedWidth",nativeMode?n.recommendedWidth:0},{"recommendedHeight",nativeMode?n.recommendedHeight:0},
        {"integrated",integrationStatus.enabled},{"integrating",integrationProcess!=nullptr},
        {"startup",startupStatus.enabled},{"startupOther",startupStatus.registered&&!startupStatus.enabled},{"startupError",Utf8(startupStatus.error)},
        {"offset",horizontalTenths->load()},{"prediction",predictionMs->load()},{"lastPrediction",lastEnabledPrediction},
        {"autoAttach",!nativeMode&&autoAttach.load()},{"games",games},{"calibration",s.calibrationValid!=0},
        {"lens",s.lensEnabled!=0},{"rotationValid",nativeMode?n.rotation:rotation},{"eyeLabel",eye},{"clients",clients},
        {"mock",mockMode},{"message",Utf8(nativeMode?n.message:s.message)},
        {"integrationDetail",Utf8(!integrationStatus.error.empty()?integrationStatus.error:integrationStatus.pendingRecovery?L"上次配置未完成，请重新启用或恢复原厂接入。":!integrationStatus.enabled&&!integrationStatus.managedDirectory.empty()?L"已登记其他桥接目录；启用可更新到当前版本。":L"")},{"hub",hubRunning?"running":installed?"installed":"missing"},
        {"hubSession",hubSession},{"hubLaunching",hubLaunching.load()},{"hubPath",Utf8(hubExecutable.parent_path().wstring())}};
    ui.Post(Wide(Json{{"type","state"},{"state",state},{"pose",pose}}.dump()));
}
LRESULT CALLBACK WindowProc(HWND window,UINT message,WPARAM wParam,LPARAM lParam){
    switch(message){
    case WM_CREATE:{
        mainWindow=window;
        const auto dpi=GetDpiForWindow(window);
        SendMessageW(window,WM_SETICON,ICON_BIG,reinterpret_cast<LPARAM>(LoadImageW(GetModuleHandleW(nullptr),MAKEINTRESOURCEW(101),IMAGE_ICON,GetSystemMetricsForDpi(SM_CXICON,dpi),GetSystemMetricsForDpi(SM_CYICON,dpi),LR_SHARED)));
        SendMessageW(window,WM_SETICON,ICON_SMALL,reinterpret_cast<LPARAM>(LoadImageW(GetModuleHandleW(nullptr),MAKEINTRESOURCEW(101),IMAGE_ICON,GetSystemMetricsForDpi(SM_CXSMICON,dpi),GetSystemMetricsForDpi(SM_CYSMICON,dpi),LR_SHARED)));
        DWORD corners=2;DwmSetWindowAttribute(window,DWMWA_WINDOW_CORNER_PREFERENCE,&corners,sizeof(corners));
        const MARGINS margins{1,1,1,1};DwmExtendFrameIntoClientArea(window,&margins);
        wchar_t local[32768]{};GetEnvironmentVariableW(L"LOCALAPPDATA",local,static_cast<DWORD>(std::size(local)));
        // UI cache is per user; it is never put in the portable release or git.
        // Preserve the existing cache across the product rename.
        const auto cache=std::filesystem::path(local)/L"TitanLeiaBridge"/(mockMode?L"WebView2-Mock":L"WebView2");
        ui.Start(window,cache,HandleUi,[](const std::wstring& error){
            Log(error);
            CreateWindowW(L"STATIC",error.c_str(),WS_CHILD|WS_VISIBLE|SS_LEFT,40,70,800,160,mainWindow,nullptr,GetModuleHandleW(nullptr),nullptr);
        });
        tray.cbSize=sizeof(tray);tray.hWnd=window;tray.uID=1;tray.uFlags=NIF_MESSAGE|NIF_ICON|NIF_TIP;
        tray.uCallbackMessage=kTrayMessage;tray.hIcon=LoadIconW(GetModuleHandleW(nullptr),MAKEINTRESOURCEW(101));
        wcscpy_s(tray.szTip,L"Stereo3DBridge");Shell_NotifyIconW(NIM_ADD,&tray);
        if(!nativeMode){
        if(!RegisterHotKey(window,kPredictionHotkey,MOD_CONTROL|MOD_ALT|MOD_NOREPEAT,VK_F9))Log(L"Ctrl+Alt+F9 已被占用，请使用窗口调节。");
        const UINT keys[]{VK_LEFT,VK_RIGHT,VK_LEFT,VK_RIGHT,VK_HOME};
        for(int i=0;i<5;++i){
            const UINT mods=MOD_CONTROL|MOD_ALT|(i==2||i==3?MOD_SHIFT:0)|(i>=4?MOD_NOREPEAT:0);
            if(!RegisterHotKey(window,2+i,mods,keys[i]))Log(L"全局调节快捷键注册失败，编号="+std::to_wstring(2+i));
        }
        }
        RefreshIntegrationStatus();SetTimer(window,1,100,nullptr);return 0;}
    case WM_NCCALCSIZE:if(wParam)return 0;break;
    case WM_SHOWWINDOW:PostMessageW(window,kVisibilityMessage,0,0);break;
    case kVisibilityMessage:ui.Resize();UpdatePoseTimer();PublishUi();return 0;
    case WM_SIZE:ui.Resize();UpdatePoseTimer();return 0;
    case WM_MOVE:ui.Resize();return 0;
    case WM_DPICHANGED:{const auto rect=reinterpret_cast<const RECT*>(lParam);SetWindowPos(window,nullptr,rect->left,rect->top,rect->right-rect->left,rect->bottom-rect->top,SWP_NOZORDER|SWP_NOACTIVATE);ui.Resize();return 0;}
    case WM_GETMINMAXINFO:{const auto info=reinterpret_cast<MINMAXINFO*>(lParam);const auto dpi=GetDpiForWindow(window);info->ptMinTrackSize={MulDiv(920,dpi,96),MulDiv(760,dpi,96)};return 0;}
    case WM_TIMER:
        if(wParam==2){if(IsWindowVisible(window)&&!IsIconic(window))PublishPose();return 0;}
        PollIntegration();if(IsWindowVisible(window)&&!IsIconic(window))PublishUi();return 0;
    case WM_HOTKEY:
        if(wParam==kPredictionHotkey)SetPrediction(predictionMs->load()?0:lastEnabledPrediction,true);
        if(wParam>=2&&wParam<=5)SetOffset(horizontalTenths->load()+(wParam==2?-5:wParam==3?5:wParam==4?-1:1),true);
        if(wParam==6)SetOffset(0,true);
        PublishUi();return 0;
    case WM_COPYDATA:{ // Only the hardware-free smoke test accepts native test commands.
        const auto copy=reinterpret_cast<const COPYDATASTRUCT*>(lParam);
        if(!mockMode||copy->dwData!=0x544c4255||!copy->lpData||copy->cbData>65536||copy->cbData%2)return FALSE;
        HandleUi(std::wstring(static_cast<const wchar_t*>(copy->lpData),copy->cbData/sizeof(wchar_t)));return TRUE;}
    case kLogMessage:{
        std::unique_ptr<std::wstring> line(reinterpret_cast<std::wstring*>(lParam));
        while(!line->empty()&&(line->back()==L'\n'||line->back()==L'\r'))line->pop_back();
        uiLogs.push_back(*line);if(uiLogs.size()>200)uiLogs.pop_front();
        if(uiReady)ui.Post(Wide(Json{{"type","log"},{"text",Utf8(*line)}}.dump()));
        return 0;}
    case kTrayMessage:
        if(lParam==WM_LBUTTONDBLCLK||lParam==WM_LBUTTONUP)Show();
        if(lParam==WM_RBUTTONUP){
            HMENU menu=CreatePopupMenu();AppendMenuW(menu,MF_STRING,1,L"显示窗口");AppendMenuW(menu,MF_STRING,2,L"退出");
            POINT p{};GetCursorPos(&p);SetForegroundWindow(window);
            const auto choice=TrackPopupMenu(menu,TPM_RETURNCMD|TPM_RIGHTBUTTON,p.x,p.y,0,window,nullptr);DestroyMenu(menu);
            if(choice==1)Show();if(choice==2)DestroyWindow(window);
        }return 0;
    case WM_COMMAND:if(LOWORD(wParam)==kExit)DestroyWindow(window);return 0;
    case WM_CLOSE:ShowWindow(window,SW_HIDE);return 0;
    case WM_DESTROY:
        ui.Close();uiReady=false;
        for(int i=1;i<=6;++i)UnregisterHotKey(window,i);
        KillTimer(window,1);KillTimer(window,2);Shell_NotifyIconW(NIM_DELETE,&tray);PostQuitMessage(0);return 0;
    }
    return DefWindowProcW(window,message,wParam,lParam);
}
