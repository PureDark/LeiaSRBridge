#include "Startup.h"
#include <algorithm>
#include <vector>

namespace titan::app {
namespace {
constexpr wchar_t valueName[]=L"LeiaSRBridge";
constexpr const wchar_t* valueNames[]={valueName,L"Stereo3DBridge",L"TitanLeiaBridge"};
std::wstring Command(const std::filesystem::path& executable){return L"\""+executable.lexically_normal().wstring()+L"\" --background";}
bool Same(const std::wstring& a,const std::wstring& b){return CompareStringOrdinal(a.c_str(),-1,b.c_str(),-1,TRUE)==CSTR_EQUAL;}
bool IsLegacyCommand(const std::filesystem::path& executable,const std::wstring& command){
    if(!Same(executable.filename().wstring(),L"LeiaSRBridge.exe"))return false;
    return Same(command,Command(executable.parent_path()/L"Stereo3DBridge.exe"))||
        Same(command,Command(executable.parent_path()/L"TitanLeiaBridge.exe"));
}
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
LONG CopyStartupApproval(const StartupRegistryStore& store,const std::wstring& oldName){
    if(Same(oldName,valueName))return ERROR_SUCCESS;
    Key key;
    LONG result=RegOpenKeyExW(store.root,store.approvalKey.c_str(),0,KEY_QUERY_VALUE|KEY_SET_VALUE|KEY_WOW64_64KEY,&key.value);
    if(result==ERROR_FILE_NOT_FOUND)return ERROR_SUCCESS;
    if(result!=ERROR_SUCCESS)return result;
    DWORD bytes{},type{};
    result=RegQueryValueExW(key.value,oldName.c_str(),nullptr,&type,nullptr,&bytes);
    if(result==ERROR_FILE_NOT_FOUND)return ERROR_SUCCESS;
    if(result!=ERROR_SUCCESS)return result;
    if(type!=REG_BINARY||bytes<12||bytes>65536)return ERROR_INVALID_DATA;
    std::vector<BYTE> data(bytes);
    result=RegQueryValueExW(key.value,oldName.c_str(),nullptr,&type,data.data(),&bytes);
    if(result==ERROR_SUCCESS)result=RegSetValueExW(key.value,valueName,0,type,data.data(),bytes);
    return result;
}

}
StartupStatus ReadStartupStatus(const std::filesystem::path& executable,const StartupRegistryStore& store){
    StartupStatus status;Key key;
    LONG result=RegOpenKeyExW(store.root,store.key.c_str(),0,KEY_QUERY_VALUE|KEY_WOW64_64KEY,&key.value);
    if(result==ERROR_FILE_NOT_FOUND)return status;
    if(result!=ERROR_SUCCESS){status.error=Error(result);return status;}
    DWORD bytes{},type{};
    const wchar_t* name=valueName;
    for(const auto* candidate:valueNames){
        name=candidate;
        result=RegQueryValueExW(key.value,name,nullptr,&type,nullptr,&bytes);
        if(result!=ERROR_FILE_NOT_FOUND)break;
    }
    if(result==ERROR_FILE_NOT_FOUND)return status;
    if(result!=ERROR_SUCCESS){status.error=Error(result);return status;}
    if(type!=REG_SZ||bytes%sizeof(wchar_t)||bytes>65536){status.error=L"已有自启动配置格式不正确，请重新设置。";return status;}
    std::vector<wchar_t> text(bytes/sizeof(wchar_t)+1);
    result=RegQueryValueExW(key.value,name,nullptr,&type,reinterpret_cast<BYTE*>(text.data()),&bytes);
    if(result!=ERROR_SUCCESS){status.error=Error(result);return status;}
    status.valueName=name;status.legacy=!Same(name,valueName);
    status.command=text.data();status.registered=!status.command.empty();
    status.enabled=Same(status.command,Command(executable));
    if(status.legacy)status.enabled=status.enabled||IsLegacyCommand(executable,status.command);
    return status;
}
bool MigrateLegacyStartup(const std::filesystem::path& executable,std::wstring& error,const StartupRegistryStore& store){
    error.clear();
    const auto status=ReadStartupStatus(executable,store);
    if(!status.error.empty()){error=status.error;return false;}
    if(!status.legacy||!status.registered||!IsLegacyCommand(executable,status.command))return true;
    if(!ValidExecutable(executable,error))return false;
    Key key;
    LONG result=RegOpenKeyExW(store.root,store.key.c_str(),0,KEY_QUERY_VALUE|KEY_SET_VALUE|KEY_WOW64_64KEY,&key.value);
    // Copy the Windows enabled/disabled state before registering the new name.
    // Do not move another installation or enable an absent startup entry.
    if(result==ERROR_SUCCESS)result=CopyStartupApproval(store,status.valueName);
    if(result==ERROR_SUCCESS)result=WriteCommand(key.value,valueName,executable);
    if(result==ERROR_SUCCESS)result=RegDeleteValueW(key.value,status.valueName.c_str());
    if(result!=ERROR_SUCCESS){error=Error(result);return false;}
    return true;
}
bool ConfigureStartup(const std::filesystem::path& executable,bool enable,std::wstring& error,const StartupRegistryStore& store){
    error.clear();Key key;LONG result{};
    if(enable){
        if(!ValidExecutable(executable,error))return false;
        const auto existing=ReadStartupStatus(executable,store);
        if(!existing.error.empty()){error=existing.error;return false;}
        result=RegCreateKeyExW(store.root,store.key.c_str(),0,nullptr,REG_OPTION_NON_VOLATILE,
            KEY_SET_VALUE|KEY_WOW64_64KEY,nullptr,&key.value,nullptr);
        if(result==ERROR_SUCCESS&&existing.legacy&&existing.registered)result=CopyStartupApproval(store,existing.valueName);
        if(result==ERROR_SUCCESS)result=WriteCommand(key.value,valueName,executable);
        if(result==ERROR_SUCCESS){
            for(const auto* name:valueNames){
                if(Same(name,valueName))continue;
                result=RegDeleteValueW(key.value,name);
                if(result!=ERROR_SUCCESS&&result!=ERROR_FILE_NOT_FOUND)break;
                result=ERROR_SUCCESS;
            }
        }
    }else{
        result=RegOpenKeyExW(store.root,store.key.c_str(),0,KEY_SET_VALUE|KEY_WOW64_64KEY,&key.value);
        if(result==ERROR_FILE_NOT_FOUND)return true;
        if(result==ERROR_SUCCESS)for(const auto* name:valueNames){
            result=RegDeleteValueW(key.value,name);
            if(result!=ERROR_SUCCESS&&result!=ERROR_FILE_NOT_FOUND)break;
            result=ERROR_SUCCESS;
        }
    }
    if(result!=ERROR_SUCCESS){error=Error(result);return false;}
    return true;
}
}
