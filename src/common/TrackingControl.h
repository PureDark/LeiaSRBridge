#pragma once
#include <Windows.h>
#include <cstdint>

namespace titan {
inline constexpr wchar_t kTrackingControlName[] = L"Local\\TitanLeiaBridge.TrackingControl.v2";
inline constexpr LONG kTrackingControlMagic = 0x544C4332;
// Broker-owned settings for the standalone test HUD; game IPC is unchanged.
struct alignas(8) TrackingControl {
    LONG magic{};
    LONG brokerPid{};
    volatile LONG offsetTenths{};
    volatile LONG predictionMs{};
    volatile LONG64 heartbeat{};
};
inline LONG ReadControl(const volatile LONG& value) {
    return InterlockedCompareExchange(const_cast<volatile LONG*>(&value),0,0);
}
inline std::uint64_t ReadControlTime(const volatile LONG64& value) {
    return static_cast<std::uint64_t>(InterlockedCompareExchange64(const_cast<volatile LONG64*>(&value),0,0));
}
inline bool ControlAlive(const TrackingControl* control) {
    if(!control||control->magic!=kTrackingControlMagic)return false;
    const auto now=GetTickCount64(),tick=ReadControlTime(control->heartbeat);
    return tick&&now>=tick&&now-tick<2000;
}
struct TrackingView {
    HANDLE mapping{};
    TrackingControl* data{};
    ~TrackingView(){if(data)UnmapViewOfFile(data);if(mapping)CloseHandle(mapping);}
    bool Open(){
        if(data)return true;
        mapping=OpenFileMappingW(FILE_MAP_ALL_ACCESS,FALSE,kTrackingControlName);
        if(mapping)data=static_cast<TrackingControl*>(MapViewOfFile(mapping,FILE_MAP_ALL_ACCESS,0,0,sizeof(TrackingControl)));
        if(!data&&mapping){CloseHandle(mapping);mapping=nullptr;}
        return data!=nullptr;
    }
};
}
