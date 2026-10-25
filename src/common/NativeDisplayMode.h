#pragma once
#include <windows.h>
#include <vector>

namespace titan {
struct DisplayPixels { int width{},height{}; };
// Query the panel preferred mode, independent of desktop resolution and DPI.
// The bridge uses the primary monitor; unknown native dimensions return zero.
inline DisplayPixels NativeDisplayPixels(const wchar_t* sourceName) {
    for(unsigned retry=0;retry<3;++retry) {
        UINT32 pathCount{},modeCount{};
        if(GetDisplayConfigBufferSizes(QDC_ONLY_ACTIVE_PATHS,&pathCount,&modeCount)!=ERROR_SUCCESS)return {};
        std::vector<DISPLAYCONFIG_PATH_INFO> paths(pathCount);
        std::vector<DISPLAYCONFIG_MODE_INFO> modes(modeCount);
        const auto result=QueryDisplayConfig(QDC_ONLY_ACTIVE_PATHS,&pathCount,paths.data(),&modeCount,modes.data(),nullptr);
        if(result==ERROR_INSUFFICIENT_BUFFER)continue;
        if(result!=ERROR_SUCCESS)return {};
        for(UINT32 i=0;i<pathCount;++i) {
            const auto& path=paths[i];
            DISPLAYCONFIG_SOURCE_DEVICE_NAME source{};
            source.header={DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME,sizeof(source),path.sourceInfo.adapterId,path.sourceInfo.id};
            if(DisplayConfigGetDeviceInfo(&source.header)!=ERROR_SUCCESS||_wcsicmp(source.viewGdiDeviceName,sourceName))continue;
            DISPLAYCONFIG_TARGET_PREFERRED_MODE preferred{};
            preferred.header={DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE,sizeof(preferred),path.targetInfo.adapterId,path.targetInfo.id};
            if(DisplayConfigGetDeviceInfo(&preferred.header)==ERROR_SUCCESS&&preferred.width&&preferred.height)
                return {static_cast<int>(preferred.width),static_cast<int>(preferred.height)};
        }
        return {};
    }
    return {};
}
}
