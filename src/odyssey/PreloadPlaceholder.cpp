// Native Odyssey Unreal plugins preload these legacy runtime components before
// calling the public SR APIs. Titan supplies rendering and tracking in its own
// runtime, so it has no dependency on Dimenco's weaver, camera backends or hand
// trackers. These modules only satisfy that preload list; they intentionally
// export no vendor APIs. A caller needing a vendor API must fail to resolve it.
#include <Windows.h>
extern "C" __declspec(dllexport) const char* TitanOdysseyPreloadIdentity(){return "TitanLeiaBridge legacy preload placeholder; no vendor APIs";}
BOOL WINAPI DllMain(HINSTANCE instance,DWORD reason,LPVOID){if(reason==DLL_PROCESS_ATTACH)DisableThreadLibraryCalls(instance);return TRUE;}
