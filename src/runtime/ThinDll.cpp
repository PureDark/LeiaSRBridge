#include <Windows.h>
// The actual implementation and all state live in TitanLeiaRuntime.dll.
// In particular, no IPC or threads are created while holding the loader lock.
BOOL WINAPI DllMain(HINSTANCE, DWORD, LPVOID) { return TRUE; }
