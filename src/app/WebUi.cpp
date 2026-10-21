#include "WebUi.h"
#include <shlwapi.h>

namespace titan::app {
using Microsoft::WRL::Callback;
using Microsoft::WRL::ComPtr;
void WebUi::Start(HWND window,const std::filesystem::path& cache,Command command,Error error) {
    window_=window;command_=std::move(command);error_=std::move(error);
    const auto fail=[this](HRESULT hr) {
        if(!window_)return;
        error_(L"界面加载失败（"+std::to_wstring(static_cast<unsigned long>(hr))+L"）。请安装 Microsoft Edge WebView2 Runtime 后重新打开；后台状态见 logs。下载地址：https://go.microsoft.com/fwlink/p/?LinkId=2124703");
    };
    const HRESULT result=CreateCoreWebView2EnvironmentWithOptions(nullptr,cache.c_str(),nullptr,
        Callback<ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler>(
        [this,fail](HRESULT hr,ICoreWebView2Environment* environment)->HRESULT {
            if(!window_)return S_OK;
            if(FAILED(hr)||!environment){fail(hr);return S_OK;}
            const auto result=environment->CreateCoreWebView2Controller(window_,
                Callback<ICoreWebView2CreateCoreWebView2ControllerCompletedHandler>(
                [this,fail](HRESULT hr,ICoreWebView2Controller* controller)->HRESULT {
                    if(!window_)return S_OK;
                    if(FAILED(hr)||!controller){fail(hr);return S_OK;}
                    controller_=controller;controller_->get_CoreWebView2(&view_);Resize();
                    ComPtr<ICoreWebView2Controller2> color;
                    if(SUCCEEDED(controller_.As(&color)))color->put_DefaultBackgroundColor({255,250,249,246});
                    ComPtr<ICoreWebView2Settings> settings;view_->get_Settings(&settings);
                    settings->put_AreDevToolsEnabled(FALSE);settings->put_AreDefaultContextMenusEnabled(FALSE);
                    settings->put_IsStatusBarEnabled(FALSE);settings->put_IsZoomControlEnabled(FALSE);
                    ComPtr<ICoreWebView2Settings3> settings3; if(SUCCEEDED(settings.As(&settings3)))settings3->put_AreBrowserAcceleratorKeysEnabled(FALSE);
                    EventRegistrationToken token{};
                    view_->add_WebMessageReceived(Callback<ICoreWebView2WebMessageReceivedEventHandler>(
                        [this](ICoreWebView2*,ICoreWebView2WebMessageReceivedEventArgs* args)->HRESULT {
                            LPWSTR source{},message{};args->get_Source(&source);
                            if(source && wcscmp(source,L"about:blank")==0 && SUCCEEDED(args->get_WebMessageAsJson(&message)) && message)
                                command_(message);
                            CoTaskMemFree(source);CoTaskMemFree(message);return S_OK;
                        }).Get(),&token);
                    view_->add_NavigationStarting(Callback<ICoreWebView2NavigationStartingEventHandler>(
                        [loaded=false](ICoreWebView2*,ICoreWebView2NavigationStartingEventArgs* args) mutable ->HRESULT {
                            LPWSTR uri{};args->get_Uri(&uri);
                            // NavigateToString reports a data URI at NavigationStarting, then about:blank as its source.
                            const bool initial=!loaded&&uri&&(wcsncmp(uri,L"data:text/html",14)==0||wcscmp(uri,L"about:blank")==0);
                            args->put_Cancel(!initial);if(initial)loaded=true;
                            CoTaskMemFree(uri);return S_OK;
                        }).Get(),&token);
                    view_->add_NewWindowRequested(Callback<ICoreWebView2NewWindowRequestedEventHandler>(
                        [](ICoreWebView2*,ICoreWebView2NewWindowRequestedEventArgs* args)->HRESULT {args->put_Handled(TRUE);return S_OK;}).Get(),&token);
                    const auto resource=FindResourceW(nullptr,MAKEINTRESOURCEW(102),RT_RCDATA);
                    const auto data=static_cast<const char*>(LockResource(LoadResource(nullptr,resource)));
                    const int bytes=static_cast<int>(SizeofResource(nullptr,resource));
                    if(!data||!bytes){fail(E_FAIL);return S_OK;}
                    const int size=MultiByteToWideChar(CP_UTF8,0,data,bytes,nullptr,0);
                    std::wstring html(size,L'\0');MultiByteToWideChar(CP_UTF8,0,data,bytes,html.data(),size);
                    view_->add_NavigationCompleted(Callback<ICoreWebView2NavigationCompletedEventHandler>(
                        [this](ICoreWebView2*,ICoreWebView2NavigationCompletedEventArgs* args)->HRESULT {
                            BOOL success{};args->get_IsSuccess(&success);
                            if(!success){COREWEBVIEW2_WEB_ERROR_STATUS status{};args->get_WebErrorStatus(&status);error_(L"UI navigation failed: "+std::to_wstring(status));}
                            return S_OK;
                        }).Get(),&token);
                    const auto nav=view_->NavigateToString(html.c_str());if(FAILED(nav))fail(nav);
                    return S_OK;
                }).Get());
            if(FAILED(result))fail(result);return S_OK;
        }).Get());
    if(FAILED(result))fail(result);
}
void WebUi::Resize(){if(controller_&&window_){RECT bounds{};GetClientRect(window_,&bounds);controller_->put_Bounds(bounds);controller_->put_IsVisible(IsWindowVisible(window_)&&!IsIconic(window_));controller_->NotifyParentWindowPositionChanged();}}
void WebUi::Close(){window_=nullptr;if(controller_)controller_->Close();view_.Reset();controller_.Reset();}
void WebUi::Post(const std::wstring& json){if(view_)view_->PostWebMessageAsJson(json.c_str());}
void WebUi::Script(const std::wstring& script){if(view_)view_->ExecuteScript(script.c_str(),nullptr);}
void WebUi::Capture(const std::filesystem::path& file){
    if(!view_)return;
    ComPtr<IStream> stream;
    if(SUCCEEDED(SHCreateStreamOnFileEx(file.c_str(),STGM_CREATE|STGM_WRITE|STGM_SHARE_EXCLUSIVE,FILE_ATTRIBUTE_NORMAL,TRUE,nullptr,&stream)))
        view_->CapturePreview(COREWEBVIEW2_CAPTURE_PREVIEW_IMAGE_FORMAT_PNG,stream.Get(),
            Callback<ICoreWebView2CapturePreviewCompletedHandler>([stream](HRESULT)->HRESULT{return S_OK;}).Get());
}
}

