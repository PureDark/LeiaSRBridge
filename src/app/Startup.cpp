#include "Startup.h"
#include <algorithm>
#include <vector>

namespace titan::app {
namespace {
constexpr wchar_t valueName[]=L"Stereo3DBridge";
constexpr wchar_t legacyValueName[]=L"TitanLeiaBridge";
std::wstring Command(const std::filesystem::path& executable){return L"\""+executable.lexically_normal().wstring()+L"\" --background";}
bool Same(const std::wstring& a,const std::wstring& b){return CompareStringOrdinal(a.c_str(),-1,b.c_str(),-1,TRUE)==CSTR_EQUAL;}
std::wstring Error(LONG code){
    wchar_t* message{};
    FormatMessageW(FORMAT_MESSAGE_ALLOCATE_BUFFER|FORMAT_MESSAGE_FROM_SYSTEM|FORMAT_MESSAGE_IGNORE_INSERTS,
        nullptr,code,0,reinterpret_cast<wchar_t*>(&message),0,nullptr);
    const std::wstring result=message?message:L"无法访问 Windows 启动设置。";
    if(message)LocalFree(message);return result;
}
struct Key {HKEY value{};~Key(){if(value)RegCloseKey(value);}};
bool ValidExecutable(const std::filesystem::path& executable,std::wstring& error){
    std::error_code ec;
    if(!executable.is_absolute()||executable.wstring().find(L'"')!=std::wstring::npos||Command(executable).size()>260||
        !std::filesystem::is_regular_file(executable,ec)){
        error=L"无法设置自启动：程序路径无效或过长，请将完整程序放在固定的较短路径中。";return false;
    }
    return true;
}
LONG WriteCommand(HKEY key,const wchar_t* name,const std::filesystem::path& executable){
    const auto command=Command(executable);
    return RegSetValueExW(key,name,0,REG_SZ,reinterpret_cast<const BYTE*>(command.c_str()),
        static_cast<DWORD>((command.size()+1)*sizeof(wchar_t)));
}
}
StartupStatus ReadStartupStatus(const std::filesystem::path& executable,const StartupRegistryStore& store){
    StartupStatus status;Key key;
    LONG result=RegOpenKeyExW(store.root,store.key.c_str(),0,KEY_QUERY_VALUE|KEY_WOW64_64KEY,&key.value);
    if(result==ERROR_FILE_NOT_FOUND)return status;
    if(result!=ERROR_SUCCESS){status.error=Error(result);return status;}
    DWORD bytes{},type{};
    const wchar_t* name=valueName;
    result=RegQueryValueExW(key.value,name,nullptr,&type,nullptr,&bytes);
    if(result==ERROR_FILE_NOT_FOUND){
        name=legacyValueName;status.legacy=true;
        result=RegQueryValueExW(key.value,name,nullptr,&type,nullptr,&bytes);
    }
    if(result==ERROR_FILE_NOT_FOUND)return status;
    if(result!=ERROR_SUCCESS){status.error=Error(result);return status;}
    if(type!=REG_SZ||bytes%sizeof(wchar_t)||bytes>65536){status.error=L"已有自启动配置格式不正确，请重新设置。";return status;}
    std::vector<wchar_t> text(bytes/sizeof(wchar_t)+1);
    result=RegQueryValueExW(key.value,name,nullptr,&type,reinterpret_cast<BYTE*>(text.data()),&bytes);
    if(result!=ERROR_SUCCESS){status.error=Error(result);return status;}
    status.command=text.data();status.registered=!status.command.empty();
    status.enabled=Same(status.command,Command(executable));
    if(status.legacy&&Same(executable.filename().wstring(),L"Stereo3DBridge.exe"))
        status.enabled=status.enabled||Same(status.command,Command(executable.parent_path()/L"TitanLeiaBridge.exe"));
    return status;
}
bool MigrateLegacyStartup(const std::filesystem::path& executable,std::wstring& error,const StartupRegistryStore& store){
    error.clear();
    const auto status=ReadStartupStatus(executable,store);
    if(!status.error.empty()){error=status.error;return false;}
    if(!status.legacy||!status.registered||!Same(executable.filename().wstring(),L"Stereo3DBridge.exe")||
        !Same(status.command,Command(executable.parent_path()/L"TitanLeiaBridge.exe")))return true;
    if(!ValidExecutable(executable,error))return false;
    Key key;
    LONG result=RegOpenKeyExW(store.root,store.key.c_str(),0,KEY_QUERY_VALUE|KEY_SET_VALUE|KEY_WOW64_64KEY,&key.value);
    // Preserve the legacy Run value identity and its Windows StartupApproved
    // state. Do not move another installation or enable an absent startup entry.
    if(result==ERROR_SUCCESS)result=WriteCommand(key.value,legacyValueName,executable);
    if(result!=ERROR_SUCCESS){error=Error(result);return false;}
    return true;
}
bool ConfigureStartup(const std::filesystem::path& executable,bool enable,std::wstring& error,const StartupRegistryStore& store){
    error.clear();Key key;LONG result{};
    if(enable){
        if(!ValidExecutable(executable,error))return false;
        const auto existing=ReadStartupStatus(executable,store);
        const bool legacy=existing.legacy&&existing.registered;
        result=RegCreateKeyExW(store.root,store.key.c_str(),0,nullptr,REG_OPTION_NON_VOLATILE,
            KEY_SET_VALUE|KEY_WOW64_64KEY,nullptr,&key.value,nullptr);
        if(result==ERROR_SUCCESS)result=WriteCommand(key.value,legacy?legacyValueName:valueName,executable);
        if(result==ERROR_SUCCESS){
            result=RegDeleteValueW(key.value,legacy?valueName:legacyValueName);
            if(result==ERROR_FILE_NOT_FOUND)result=ERROR_SUCCESS;
        }
    }else{
        result=RegOpenKeyExW(store.root,store.key.c_str(),0,KEY_SET_VALUE|KEY_WOW64_64KEY,&key.value);
        if(result==ERROR_FILE_NOT_FOUND)return true;
        if(result==ERROR_SUCCESS)for(const auto* name:{valueName,legacyValueName}){
            result=RegDeleteValueW(key.value,name);
            if(result!=ERROR_SUCCESS&&result!=ERROR_FILE_NOT_FOUND)break;
            result=ERROR_SUCCESS;
        }
    }
    if(result!=ERROR_SUCCESS){error=Error(result);return false;}
    return true;
}
}
