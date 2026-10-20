#include "broker/PortableTracking.h"
#include <wrl/client.h>
#include <cstdio>
#include <fstream>
#include <limits>
#include <stdexcept>
#include <sstream>
using Microsoft::WRL::ComPtr;
namespace {
void Require(bool condition, const char* message) { if (!condition) throw std::runtime_error(message); }
void Check(HRESULT result, const char* message) {
    if (FAILED(result)) { std::fprintf(stderr, "%s: HRESULT=%08lX\n", message, result); throw std::runtime_error(message); }
}
struct NoRegisteredClasses {
    HKEY empty{};
    NoRegisteredClasses() {
        Require(RegOpenKeyExW(HKEY_CURRENT_USER, L"Volatile Environment", 0, KEY_READ, &empty) == ERROR_SUCCESS, "open test registry root");
        Require(RegOverridePredefKey(HKEY_CLASSES_ROOT, empty) == ERROR_SUCCESS, "process-local HKCR override");
        HKEY existing{};
        Require(RegOpenKeyExW(HKEY_CLASSES_ROOT, L"CLSID\\{D98A272F-DE1C-4E73-A1D0-A93A8F068B4B}",
            0, KEY_READ, &existing) == ERROR_FILE_NOT_FOUND, "registered tracker must be invisible");
    }
    ~NoRegisteredClasses() { RegOverridePredefKey(HKEY_CLASSES_ROOT, nullptr); if (empty) RegCloseKey(empty); }
};
HRESULT Call(IDispatch* object, DISPID method, VARIANTARG* args, UINT count) {
    DISPPARAMS parameters{args, nullptr, count, 0};
    return object->Invoke(method, IID_NULL, LOCALE_INVARIANT, DISPATCH_METHOD, &parameters, nullptr, nullptr, nullptr);
}
void CheckModule(IDispatch* object, const std::filesystem::path& directory) {
    auto** vtable = *reinterpret_cast<void***>(object);
    HMODULE module{}; wchar_t path[32768]{};
    Require(GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
        reinterpret_cast<LPCWSTR>(vtable[0]), &module), "locate COM implementation");
    GetModuleFileNameW(module, path, static_cast<DWORD>(std::size(path)));
    Require(std::filesystem::equivalent(path, directory / L"FaceTrackingManagerService.dll"), "COM must use packaged DLL");
}
void SetPosition(IDispatch* object, float x, float y, float z) {
    wchar_t name[] = L"SetEyeInfo2"; LPOLESTR names[]{name}; DISPID method{};
    Check(object->GetIDsOfNames(IID_NULL, names, 1, LOCALE_INVARIANT, &method), "resolve SetEyeInfo2 without registration");
    VARIANTARG args[5]{};
    const float values[]{1, 1, z, y, x};
    for (int i=0;i<5;++i) { args[i].vt = VT_R4; args[i].fltVal = values[i]; }
    Check(Call(object, method, args, 5), "write shared position without camera");
}
}
int wmain(int argc, wchar_t** argv) {
    bool initialized{};
    try {
        Require(argc == 2, "expected tracker directory or --child");
        NoRegisteredClasses hiddenRegistry;
        const bool child = std::wstring(argv[1]) == L"--child";
        Check(CoInitializeEx(nullptr, child ? COINIT_APARTMENTTHREADED | COINIT_DISABLE_OLE1DDE : COINIT_MULTITHREADED), "COM initialization"); initialized = true;
        wchar_t exe[32768]{}; GetModuleFileNameW(nullptr, exe, static_cast<DWORD>(std::size(exe)));
        const auto directory = child ? std::filesystem::path(exe).parent_path() : std::filesystem::path(argv[1]);
        {
            titan::broker::PortableTracking activation;
            ComPtr<IDispatch> object;
            if (child) {
                // No explicit ActivateActCtx here: use this EXE's embedded
                // default manifest exactly as the vendor FaceTracking.exe does.
                const CLSID clsid{0xd98a272f,0xde1c,0x4e73,{0xa1,0xd0,0xa9,0x3a,0x8f,0x06,0x8b,0x4b}};
                const IID simple{0xe26ee08a,0xbcf1,0x49f8,{0xb0,0x1f,0x00,0x44,0xd2,0x89,0x7f,0x28}};
                ComPtr<IUnknown> native;
                Check(CoCreateInstance(clsid, nullptr, CLSCTX_INPROC_SERVER, simple,
                    reinterpret_cast<void**>(native.GetAddressOf())), "child default manifest COM with native IID");
                Check(native.As(&object), "native tracker IDispatch");
                CheckModule(object.Get(), directory);
                SetPosition(object.Get(), 123.5f, -7.0f, 600.0f);
            } else {
                std::wstring error;
                Require(activation.Activate(directory, error), "activate real FaceTracking.exe embedded manifest");
                Check(activation.Create(&object), "portable COM creation"); CheckModule(object.Get(), directory);
                SetPosition(object.Get(), 0, 0, 0);
                std::wstring command = L"\"" + (directory / L"PortableTrackingChild.exe").wstring() + L"\" --child";
                STARTUPINFOW startup{sizeof(startup)}; PROCESS_INFORMATION process{};
                Require(CreateProcessW((directory/L"PortableTrackingChild.exe").c_str(), command.data(), nullptr, nullptr,
                    FALSE, CREATE_NO_WINDOW, nullptr, directory.c_str(), &startup, &process), "start isolated COM writer");
                CloseHandle(process.hThread);
                const DWORD wait = WaitForSingleObject(process.hProcess, 10000);
                if (wait != WAIT_OBJECT_0) TerminateProcess(process.hProcess, 99);
                DWORD exit{}; GetExitCodeProcess(process.hProcess, &exit); CloseHandle(process.hProcess);
                Require(wait == WAIT_OBJECT_0 && exit == 0, "child COM writer completion");
                float x{},y{},z{},l{},r{}; float* values[]{&r,&l,&z,&y,&x}; VARIANTARG args[5]{};
                for(int i=0;i<5;++i) { args[i].vt=VT_R4|VT_BYREF;args[i].pfltVal=values[i]; }
                Check(Call(object.Get(), 6, args, 5), "read shared position without registration");
                Require(x==123.5f && y==-7.0f && z==600.0f, "parent and child shared eye_info");
                SetPosition(object.Get(), 0, 0, 0);
                const auto calibration = std::filesystem::path(exe).parent_path()/L"portable-calibration-test.ini";
                std::array<double,9> left{431.1234567890123,432,313,239,0.1,-0.2,0,0,0},right=left;
                std::array<double,10> stereo{432,313,238,-78,0,0,0,0,0,0};
                Require(titan::broker::SaveCameraCalibration(calibration,left,right,stereo,error),"write current-device calibration");
                wchar_t text[2048]{};
                Require(GetPrivateProfileStringW(L"calibration_data",L"E1",L"",text,2048,calibration.c_str())>0,"read generated calibration");
                std::wistringstream row(text);row.imbue(std::locale::classic());
                for(std::size_t i=0;i<left.size();++i) { double number{};Require(bool(row>>number)&&number==left[i],"lossless calibration values"); if(i+1<left.size()){wchar_t comma{};Require(bool(row>>comma)&&comma==L',',"calibration CSV");} }
                left[0]=std::numeric_limits<double>::quiet_NaN();
                Require(!titan::broker::SaveCameraCalibration(calibration,left,right,stereo,error),"invalid calibration must be rejected");
                DeleteFileW(calibration.c_str());
                std::puts("PASS: parent explicit and child embedded activation; HKCR hidden; identical packaged COM; shared xyz; calibration round-trip. No Open/StartCamera/MCU calls.");
            }
        }
        CoUninitialize(); return 0;
    } catch (const std::exception& error) {
        std::fprintf(stderr,"FAIL: %s\n",error.what()); if(initialized)CoUninitialize();return 1;
    }
}
