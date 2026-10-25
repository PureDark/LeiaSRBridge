#include "Update.h"
#include <objbase.h>
#include <fstream>
#include <string>

namespace titan::app {
namespace {
std::string Utf8(const std::wstring& value) {
    const int bytes=WideCharToMultiByte(CP_UTF8,0,value.data(),static_cast<int>(value.size()),nullptr,0,nullptr,nullptr);
    std::string result(bytes,0);
    WideCharToMultiByte(CP_UTF8,0,value.data(),static_cast<int>(value.size()),result.data(),bytes,nullptr,nullptr);
    return result;
}
}
Update::~Update(){Stop();}
void Update::Error(const std::string& text){state_={{"phase","error"},{"message",text}};}
void Update::Stop(){
    if(process_){if(!handedOff_&&WaitForSingleObject(process_,0)==WAIT_TIMEOUT){TerminateProcess(process_,1);WaitForSingleObject(process_,5000);}CloseHandle(process_);process_=nullptr;}
    if(!handedOff_&&!job_.empty()){std::error_code ec;std::filesystem::remove_all(job_,ec);job_.clear();}
}
bool Update::Launch(const wchar_t* mode){
    wchar_t system[MAX_PATH]{};
    if(!GetSystemDirectoryW(system,MAX_PATH)){Error("无法定位 Windows PowerShell。");return false;}
    const auto shell=std::filesystem::path(system)/L"WindowsPowerShell/v1.0/powershell.exe";
    std::wstring command=L"\""+shell.wstring()+L"\" -NoLogo -NoProfile -NonInteractive -ExecutionPolicy Bypass -File \""+
        (job_/L"update.ps1").wstring()+L"\" -Mode "+mode+L" -JobDirectory \""+job_.wstring()+L"\"";
    SECURITY_ATTRIBUTES security{sizeof(security),nullptr,TRUE};
    const auto output=CreateFileW((job_/L"helper.log").c_str(),GENERIC_WRITE,FILE_SHARE_READ,&security,CREATE_ALWAYS,FILE_ATTRIBUTE_NORMAL,nullptr);
    if(output==INVALID_HANDLE_VALUE){Error("无法写入更新助手日志。");return false;}
    STARTUPINFOW startup{sizeof(startup)};PROCESS_INFORMATION process{};
    startup.dwFlags=STARTF_USESTDHANDLES;startup.hStdOutput=output;startup.hStdError=output;startup.hStdInput=GetStdHandle(STD_INPUT_HANDLE);
    if(!CreateProcessW(shell.c_str(),command.data(),nullptr,nullptr,TRUE,CREATE_NO_WINDOW,nullptr,job_.c_str(),&startup,&process)){
        const auto error=GetLastError();CloseHandle(output);Error("无法启动更新助手，Windows 错误 "+std::to_string(error));return false;
    }
    CloseHandle(output);CloseHandle(process.hThread);process_=process.hProcess;return true;
}
void Update::Check(const std::filesystem::path& directory,bool mock){
    if(process_)return;
    Stop();
    if(mock){Error("离线模拟不访问 GitHub 或安装更新。");return;}
    try{
        wchar_t temp[32768]{};if(!GetTempPathW(32768,temp))throw std::runtime_error("无法读取临时目录。");
        GUID guid{};if(FAILED(CoCreateGuid(&guid)))throw std::runtime_error("无法创建更新会话。");
        wchar_t id[40]{};StringFromGUID2(guid,id,40);
        job_=std::filesystem::path(temp)/(std::wstring(L"LeiaSRBridge-update-")+id);
        if(!std::filesystem::create_directory(job_))throw std::runtime_error("无法创建更新缓存。");
        const auto resource=FindResourceW(nullptr,MAKEINTRESOURCEW(105),RT_RCDATA);
        const auto bytes=SizeofResource(nullptr,resource);
        const auto script=static_cast<const char*>(LockResource(LoadResource(nullptr,resource)));
        if(!script||!bytes)throw std::runtime_error("更新助手资源缺失。");
        std::ofstream file(job_/L"update.ps1",std::ios::binary);
        // Windows PowerShell 5.1 requires a BOM for embedded Chinese text.
        file.write("\xEF\xBB\xBF",3);file.write(script,bytes);file.close();
        if(!file)throw std::runtime_error("无法写入更新助手。");
        nlohmann::json config{{"directory",Utf8(directory.wstring())},{"current",LEIASR_VERSION},{"parentId",GetCurrentProcessId()}};
        FILETIME creation{},exit{},kernel{},user{};GetProcessTimes(GetCurrentProcess(),&creation,&exit,&kernel,&user);
        const auto ticks=(static_cast<unsigned long long>(creation.dwHighDateTime)<<32)|creation.dwLowDateTime;
        config["parentStart"]=std::to_string(ticks);
        std::ofstream settings(job_/L"config.json",std::ios::binary);settings<<config.dump();settings.close();
        if(!settings)throw std::runtime_error("无法写入更新配置。");
        state_={{"phase","checking"},{"message","正在匿名访问 GitHub Release…"}};
        Launch(L"Check");
    }catch(const std::exception& e){Error(e.what());}
}
void Update::Download(){
    if(process_||state_.value("phase","")!="available")return;
    std::error_code ec;std::filesystem::remove(job_/L"status.json",ec);
    state_["phase"]="downloading";state_["message"]="正在下载更新…";state_["percent"]=0;
    Launch(L"Prepare");
}
bool Update::Install(){
    if(process_||state_.value("phase","")!="ready")return false;
    if(!Launch(L"Apply"))return false;
    handedOff_=true;state_["phase"]="installing";return true;
}
void Update::Poll(){
    if(!process_||handedOff_)return;
    const bool done=WaitForSingleObject(process_,0)==WAIT_OBJECT_0;
    try{std::ifstream input(job_/L"status.json");if(input){auto value=nlohmann::json::parse(input);if(value.is_object()&&value.contains("phase"))state_=std::move(value);}}catch(const std::exception&){}
    if(done){
        DWORD code{};GetExitCodeProcess(process_,&code);CloseHandle(process_);process_=nullptr;
        const auto phase=state_.value("phase","");
        if(code!=0&&phase!="error"){
            std::ifstream log(job_/L"helper.log",std::ios::binary);
            std::string detail(4096,0);log.read(detail.data(),static_cast<std::streamsize>(detail.size()));detail.resize(static_cast<size_t>(log.gcount()));
            // PowerShell's error pipe uses the console OEM code page on some systems.
            if(!detail.empty()){
                const auto count=MultiByteToWideChar(CP_OEMCP,0,detail.data(),static_cast<int>(detail.size()),nullptr,0);
                std::wstring wide(count,0);MultiByteToWideChar(CP_OEMCP,0,detail.data(),static_cast<int>(detail.size()),wide.data(),count);detail=Utf8(wide);
            }
            Error("更新助手失败，退出码 "+std::to_string(code)+"。 "+detail);
        }
        else if(phase=="checking"||phase=="downloading"||phase=="validating")Error("更新助手未返回完整结果，请重试。");
    }
}
}
