// txBridge replacement used only inside Odyssey compatibility processes.
// It never loads the Samsung USB/firmware proxy or writes monitor calibration.
#include "common/SharedProtocol.h"
#include <atomic>
namespace {
std::atomic<bool> connected{true};
bool State(titan::Snapshot& value){
    HANDLE map=OpenFileMappingW(FILE_MAP_READ,FALSE,titan::kMappingName);if(!map)return false;
    auto* shared=static_cast<const titan::SharedState*>(MapViewOfFile(map,FILE_MAP_READ,0,0,sizeof(titan::SharedState)));
    bool valid=shared&&titan::ReadSnapshot(shared,value);if(shared)UnmapViewOfFile(shared);CloseHandle(map);
    const auto now=GetTickCount64();return valid&&connected&&value.heartbeatTick<=now&&now-value.heartbeatTick<titan::kLeaseMs&&
        (value.status==titan::ProviderStatus::Ready||value.status==titan::ProviderStatus::Mock)&&value.calibrationValid&&value.eyePositionValid;
}
int Ready(){titan::Snapshot s{};return State(s)?1:0;}
int Lens(){titan::Snapshot s{};return State(s)&&s.lensEnabled?1:0;}
int Changed(int slot,int(*read)()){
    thread_local int previous[5]{-1,-1,-1,-1,-1};
    for(;;){int value=read();if(value!=previous[slot]||!connected){previous[slot]=value;return value;}Sleep(50);}
}
int Pivot(){MONITORINFOEXW i{};i.cbSize=sizeof(i);DEVMODEW m{};m.dmSize=sizeof(m);
    return GetMonitorInfoW(MonitorFromPoint({0,0},MONITOR_DEFAULTTOPRIMARY),&i)&&EnumDisplaySettingsW(i.szDevice,ENUM_CURRENT_SETTINGS,&m)&&(m.dmDisplayOrientation==DMDO_90||m.dmDisplayOrientation==DMDO_270);}
}
#define API extern "C" __declspec(dllexport) int
API txBridge_connect(){connected=true;return 0;}
API txBridge_disconnect(){connected=false;return 0;}
API txBridge_destroyOldProcess_ifNeeded(){return 0;}
API txBridge_spawnProxy(const char*){return 0;}
API txBridge_spawnProxyConsoleWindow(const char*){return 0;}
API txBridge_killProxy(){return 0;}
API txBridge_sendDeviceQuery(){return 0;}
API txBridge_sendDevicProxyQuery(){return 0;}
API txBridge_getDeviceRespondingFlag(){return Ready();}
API txBridge_getDeviceProxyRespondingFlag(){return Ready();}
API txBridge_getVideoCablePluggedFlag(){return Ready();}
API txBridge_getPowerStateFlag(){return Ready();}
API txBridge_getPivotOnFlag(){return Pivot();}
API txBridge_getPipOnFlag(){return 0;}
API txBridge_getFirmwareUpgradePendingState(){return 0;}
API txBridge_get3dFlag(){return Lens();}
// The rendering client's live weave lease owns lens switching. A background
// service's request must not override another foreground client's lease.
API txBridge_set3dFlag(int,int,int){return Ready()?0:-1;}
API txBridge_sendText(const char*){return 0;}
API txBridge_waitDeviceProxyReadyNotification(){return Changed(0,Ready);}
API txBridge_waitDeviceReadyNotification(){return Changed(1,Ready);}
API txBridge_waitCableConnectionChangeNotification(){return Changed(2,Ready);}
API txBridge_waitPowerStateChangeNotification(){return Changed(3,Ready);}
API txBridge_wait3dModeChangeNotification(){return Changed(4,Lens);}
