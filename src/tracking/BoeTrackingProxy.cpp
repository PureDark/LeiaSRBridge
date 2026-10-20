#include "common/BoeTrackingProtocol.h"
#include "common/BoeFacePoints.h"
#include <wincrypt.h>
#include <mutex>
#include <string>
#pragma comment(lib,"advapi32.lib")

// Keep forwarders in linker directives: this MSVC version fails import-library
// generation when a .def mixes a local C export with C-name module forwarders.
#pragma comment(linker, "/export:initBoeEyeTrack_3D=TitanBoeTrackingVendor.initBoeEyeTrack_3D,@2")
#pragma comment(linker, "/export:releaseBoeEyeTrack_3D=TitanBoeTrackingVendor.releaseBoeEyeTrack_3D,@3")
#pragma comment(linker, "/export:setCameraMat_3D=TitanBoeTrackingVendor.setCameraMat_3D,@4")
#pragma comment(linker, "/export:setEnableTrackArea_3D=TitanBoeTrackingVendor.setEnableTrackArea_3D,@5")
#pragma comment(linker, "/export:setFaceAreaTime_3D=TitanBoeTrackingVendor.setFaceAreaTime_3D,@6")
#pragma comment(linker, "/export:setHighConArea_3D=TitanBoeTrackingVendor.setHighConArea_3D,@7")

namespace {
// ABI verified against doEyeTrack_3D and the FaceTracking.exe call site:
// image pointer in RCX, three int arguments in EDX/R8D/R9D, output on stack.
using DoEyeTrack = int(__cdecl*)(const void*, int, int, int, void*);
HMODULE selfModule{};
HMODULE vendorModule{};
DoEyeTrack vendorDoEyeTrack{};
const void* vendorWork{};
INIT_ONCE vendorOnce = INIT_ONCE_STATIC_INIT;
std::mutex callMutex;

bool KnownVendor(const wchar_t* path) {
    constexpr char hex[]="752cb438a6d98848f45870c2502ea4ffc886c8daf14c1ee411af77518cd328b5";
    static_assert(sizeof(hex)==65);
    HCRYPTPROV provider{};HCRYPTHASH hash{};
    HANDLE file=CreateFileW(path,GENERIC_READ,FILE_SHARE_READ,nullptr,OPEN_EXISTING,FILE_ATTRIBUTE_NORMAL,nullptr);
    if(file==INVALID_HANDLE_VALUE)return false;
    bool ok=CryptAcquireContextW(&provider,nullptr,nullptr,PROV_RSA_AES,CRYPT_VERIFYCONTEXT)&&
        CryptCreateHash(provider,CALG_SHA_256,0,0,&hash);
    unsigned char buffer[16384]{};DWORD bytes{};
    while(ok){
        if(!ReadFile(file,buffer,sizeof(buffer),&bytes,nullptr)){ok=false;break;}
        if(!bytes)break;
        ok=CryptHashData(hash,buffer,bytes,0)!=FALSE;
    }
    unsigned char digest[32]{};DWORD length=sizeof(digest);
    if(ok)ok=CryptGetHashParam(hash,HP_HASHVAL,digest,&length,0)&&length==32;
    constexpr char digits[]="0123456789abcdef";
    for(unsigned i=0;ok&&i<32;++i)ok=hex[2*i]==digits[digest[i]>>4]&&hex[2*i+1]==digits[digest[i]&15];
    if(hash)CryptDestroyHash(hash);
    if(provider)CryptReleaseContext(provider,0);
    CloseHandle(file);return ok;
}

BOOL CALLBACK LoadVendor(PINIT_ONCE, PVOID, PVOID*) {
    wchar_t path[32768]{};
    const DWORD length = GetModuleFileNameW(selfModule, path, DWORD(std::size(path)));
    if (!length || length >= std::size(path)) return TRUE;
    std::wstring fullPath(path, length);
    const auto separator = fullPath.find_last_of(L"\\/");
    if (separator == std::wstring::npos) return TRUE;
    fullPath.resize(separator + 1);
    fullPath += L"TitanBoeTrackingVendor.dll";
    vendorModule = LoadLibraryExW(fullPath.c_str(), nullptr,
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR | LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
    if (vendorModule) vendorDoEyeTrack = reinterpret_cast<DoEyeTrack>(
        GetProcAddress(vendorModule, "doEyeTrack_3D"));
    // sub_180098700 returns singleton RVA 13C050; doEyeTrack passes +16 as
    // its work object. No private function is called and no code is patched.
    if(vendorDoEyeTrack&&KnownVendor(fullPath.c_str()))
        vendorWork=reinterpret_cast<const unsigned char*>(vendorModule)+0x13c050+16;
    return TRUE;
}

class Publisher {
public:
    ~Publisher() {
        if (view_) UnmapViewOfFile(view_);
        if (mapping_) CloseHandle(mapping_);
        if (mutex_) CloseHandle(mutex_);
    }
    void Publish(int result, const void* raw) noexcept {
        if (!view_ && !Open()) return;
        titan::BoeTrackingFrame frame{};
        frame.processId = GetCurrentProcessId();
        frame.processCreationTime = creationTime_;
        frame.sequence = ++sequence_;
        frame.observationTick = GetTickCount64();
        frame.sdkResult = result;
        if (raw) std::memcpy(frame.raw, raw, sizeof(frame.raw));
        titan::DecodeBoeTrackingFrame(frame);
        if(vendorWork)titan::CopyBoeFacePoints(vendorWork,frame);
        // Never delay camera acquisition behind a consumer. A skipped publication
        // remains stale at the consumer; it is not relabelled as a newer sample.
        const DWORD wait = WaitForSingleObject(mutex_, 0);
        if (wait != WAIT_OBJECT_0 && wait != WAIT_ABANDONED) return;
        InterlockedExchange64(&view_->committedSequence, 0);
        std::memcpy(&view_->frame, &frame, sizeof(frame));
        InterlockedExchange64(&view_->committedSequence, LONG64(frame.sequence));
        ReleaseMutex(mutex_);
    }
private:
    bool Open() noexcept {
        creationTime_ = titan::BoeProcessCreationTime(GetCurrentProcess());
        if (!creationTime_) return false;
        wchar_t mappingName[96]{}, mutexName[96]{};
        titan::BoeTrackingNames(GetCurrentProcessId(), mappingName, mutexName);
        mutex_ = CreateMutexW(nullptr, FALSE, mutexName);
        if (!mutex_) return false;
        mapping_ = CreateFileMappingW(INVALID_HANDLE_VALUE, nullptr, PAGE_READWRITE,
            0, sizeof(titan::BoeTrackingState), mappingName);
        if (mapping_) view_ = static_cast<titan::BoeTrackingState*>(
            MapViewOfFile(mapping_, FILE_MAP_WRITE, 0, 0, sizeof(titan::BoeTrackingState)));
        if (!view_) {
            if (mapping_) CloseHandle(mapping_);
            CloseHandle(mutex_);
            mapping_ = mutex_ = nullptr;
            return false;
        }
        return true;
    }
    HANDLE mutex_{}, mapping_{};
    titan::BoeTrackingState* view_{};
    std::uint64_t creationTime_{}, sequence_{};
};
Publisher publisher;
}

extern "C" int __cdecl doEyeTrack_3D(const void* image, int width, int height,
    int format, void* output) {
    std::lock_guard lock(callMutex);
    InitOnceExecuteOnce(&vendorOnce, LoadVendor, nullptr, nullptr);
    // Some original failure branches leave output untouched. Clear it so a failed
    // SDK call cannot republish the previous frame as newly measured eyes.
    if (output) std::memset(output, 0, titan::kBoeResultSize);
    const int result = vendorDoEyeTrack && output ?
        vendorDoEyeTrack(image, width, height, format, output) : 0;
    publisher.Publish(result, output);
    return result;
}

BOOL WINAPI DllMain(HINSTANCE module, DWORD reason, LPVOID) {
    if (reason == DLL_PROCESS_ATTACH) {
        selfModule = module;
        DisableThreadLibraryCalls(module);
    }
    return TRUE;
}
