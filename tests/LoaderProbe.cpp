#include <Windows.h>
int WINAPI wWinMain(HINSTANCE,HINSTANCE,PWSTR,int) {
    const wchar_t* names[]={L"TitanLeiaRuntime.dll",L"SimulatedRealityCore.dll",L"SimulatedRealityDirectX.dll",
        L"SimulatedRealityDisplays.dll",L"SimulatedRealityFaceTrackers.dll"};
    for(auto name:names)if(!GetModuleHandleW(name))return 10;
    const bool valid=GetProcAddress(GetModuleHandleW(L"SimulatedRealityCore.dll"),"?create@SRContext@SR@@SAPEAV12@W4NetworkMode@12@@Z")!=nullptr;
    Sleep(500); // Let the parent open a wait handle before this tiny probe exits.
    return valid?0:11;
}
