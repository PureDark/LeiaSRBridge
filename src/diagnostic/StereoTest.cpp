#include "runtime/IpcClient.h"
#include "common/TrackingControl.h"
#include "render/OpticalConstants.h"
#include "../../RE/recovered_boe_interlace_api.h"
#include <d3d11.h>
#include <dxgi1_3.h>
#include <wrl/client.h>
#include <filesystem>
#include <fstream>
#include <vector>
#include <stdexcept>
#include <cstdio>
#include <algorithm>

using Microsoft::WRL::ComPtr;
namespace {
constexpr DXGI_FORMAT format = DXGI_FORMAT_B8G8R8A8_UNORM;
std::filesystem::path directory;
std::ofstream logFile;
bool start{}, quit{};
unsigned pattern{};
int horizontalTenths{},predictionMs{};
titan::TrackingView trackingView;
bool hudDirty{true};
float HorizontalOffset() { return horizontalTenths*0.1f; }
void RefreshTuning() {
    trackingView.Open();const auto* c=trackingView.data;
    const bool live=titan::ControlAlive(c);
    const int offset=live?titan::ReadControl(c->offsetTenths):0;
    const int prediction=live?titan::ReadControl(c->predictionMs):0;
    if(offset!=horizontalTenths||prediction!=predictionMs){
        horizontalTenths=offset;predictionMs=prediction;hudDirty=true;
    }
}
void Log(const std::string& value) { logFile << GetTickCount64() << " " << value << '\n'; logFile.flush(); }
void Check(HRESULT hr, const char* what) {
    if (FAILED(hr)) throw std::runtime_error(std::string(what)+" HRESULT="+std::to_string(hr));
}
template<class T> T Export(HMODULE module, const char* name) {
    auto address=GetProcAddress(module,name);
    if(!address)throw std::runtime_error(std::string("Missing vendor export: ")+name);
    return reinterpret_cast<T>(address);
}
struct Vendor {
    HMODULE module{};
    recovered_boe::DoInterleave draw{};
    void Init(ID3D11Device* device, const titan::Snapshot& s, unsigned width, unsigned height) {
        // Vendor owns a process-global renderer. Unload while D3D11 is still alive,
        // after unbinding/flushing; releaseSDK alone only writes a log message.
        module=LoadLibraryExW((directory/L"vendor/interlace/interlaceSDK.dll").c_str(),nullptr,
            LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR|LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
        if(!module)throw std::runtime_error("Cannot load bundled interlaceSDK.dll, Win32="+std::to_string(GetLastError()));
        auto init=Export<recovered_boe::InitSDK>(module,"BOE_Interleaving_initSDK");
        draw=Export<recovered_boe::DoInterleave>(module,"BOE_Interleaving_doInterleave");
        auto whole=Export<recovered_boe::SetWholeParam>(module,"BOE_Interleaving_setWholeParam");
        auto panel=Export<recovered_boe::SetDeviceParam>(module,"BOE_Interleaving_setDeviceParam");
        auto refract=Export<recovered_boe::SetRefractDeviceParam>(module,"BOE_Interleaving_setRefractDeviceParam");
        // Match the original managed DX11 caller; arrays remain device-specific.
        const int result=init(device,nullptr,s.deviceParams,s.wholeMat,s.refractParams,11,0,0,
            static_cast<int>(width),static_cast<int>(height),format,"./logs/");
        whole(s.wholeMat);panel(s.deviceParams);refract(s.refractParams);
        Log("Native DX11 init return="+std::to_string(result)+"; output="+std::to_string(width)+"x"+std::to_string(height));
    }
};
ComPtr<ID3D11Texture2D> Texture(ID3D11Device* device, unsigned w,unsigned h,UINT bind,
    const std::vector<unsigned>* pixels=nullptr) {
    D3D11_TEXTURE2D_DESC desc{};desc.Width=w;desc.Height=h;desc.MipLevels=desc.ArraySize=1;
    desc.Format=format;desc.SampleDesc.Count=1;desc.Usage=D3D11_USAGE_DEFAULT;desc.BindFlags=bind;
    D3D11_SUBRESOURCE_DATA data{};if(pixels){data.pSysMem=pixels->data();data.SysMemPitch=w*4;}
    ComPtr<ID3D11Texture2D> texture;Check(device->CreateTexture2D(&desc,pixels?&data:nullptr,&texture),"CreateTexture2D");return texture;
}
std::vector<unsigned> Pattern(unsigned w,unsigned h,unsigned mode) {
    std::vector<unsigned> pixels(static_cast<size_t>(w)*2*h);
    for(unsigned y=0;y<h;++y)for(unsigned x=0;x<2*w;++x){
        const unsigned eye=x/w, local=x%w;
        unsigned color{};
        if(mode) color=(eye==mode-1)?0xffffffffu:0xff000000u;
        else {
            const int px=static_cast<int>(local)+(eye?8:-8);
            const bool grid=(px%80+80)%80<2 || y%80<2;
            const bool circle=(px-static_cast<int>(w/2))*(static_cast<long long>(px)-w/2)+
                (static_cast<int>(y)-static_cast<long long>(h/2))*(static_cast<int>(y)-static_cast<long long>(h/2))<
                static_cast<long long>(h/5)*(h/5);
            color=grid?0xff909090u:circle?0xffe0e0e0u:0xff101010u;
        }
        pixels[static_cast<size_t>(y)*2*w+x]=color;
    }
    return pixels;
}
struct Graphics {
    ComPtr<ID3D11Device> device;
    ComPtr<ID3D11DeviceContext> context;
    ComPtr<IDXGISwapChain1> swap;
    ComPtr<ID3D11Texture2D> output;
    ComPtr<ID3D11RenderTargetView> rtv;
    ComPtr<ID3D11ShaderResourceView> input[3];
    Vendor vendor;
    unsigned width{},height{};
    ~Graphics() {
        if(context){context->ClearState();context->Flush();}
        if(vendor.module)FreeLibrary(vendor.module);
    }
    void Init(HWND window,HMONITOR monitor,unsigned w,unsigned h,const titan::Snapshot& s,bool offline) {
        width=w;height=h;
        ComPtr<IDXGIFactory2> factory;Check(CreateDXGIFactory1(IID_PPV_ARGS(&factory)),"Create factory");
        ComPtr<IDXGIAdapter1> selected;
        if(!offline){
            for(UINT a=0;!selected;++a){ComPtr<IDXGIAdapter1> adapter;
                if(factory->EnumAdapters1(a,&adapter)==DXGI_ERROR_NOT_FOUND)break;
                for(UINT o=0;;++o){ComPtr<IDXGIOutput> outputDevice;
                    if(adapter->EnumOutputs(o,&outputDevice)==DXGI_ERROR_NOT_FOUND)break;
                    DXGI_OUTPUT_DESC desc{};Check(outputDevice->GetDesc(&desc),"Output description");
                    if(desc.Monitor==monitor){selected=adapter;break;}
                }
            }
            if(!selected)throw std::runtime_error("Cannot find the GPU owning the selected display");
        }
        Check(D3D11CreateDevice(selected.Get(),offline?D3D_DRIVER_TYPE_WARP:D3D_DRIVER_TYPE_UNKNOWN,
            nullptr,D3D11_CREATE_DEVICE_BGRA_SUPPORT,nullptr,0,D3D11_SDK_VERSION,&device,nullptr,&context),"Create D3D11 device");
        if(window){
            DXGI_SWAP_CHAIN_DESC1 desc{};desc.Width=w;desc.Height=h;desc.Format=format;
            desc.SampleDesc.Count=1;desc.BufferUsage=DXGI_USAGE_RENDER_TARGET_OUTPUT;desc.BufferCount=2;
            desc.SwapEffect=DXGI_SWAP_EFFECT_FLIP_DISCARD;desc.Scaling=DXGI_SCALING_NONE;
            desc.Flags=DXGI_SWAP_CHAIN_FLAG_FRAME_LATENCY_WAITABLE_OBJECT;
            Check(factory->CreateSwapChainForHwnd(device.Get(),window,&desc,nullptr,nullptr,&swap),"Create swapchain");
            ComPtr<IDXGISwapChain2> pacingSwap;
            Check(swap.As(&pacingSwap),"Query pacing swapchain");
            Check(pacingSwap->SetMaximumFrameLatency(3),"Set frame latency");
            CloseHandle(pacingSwap->GetFrameLatencyWaitableObject());
            Check(factory->MakeWindowAssociation(window,DXGI_MWA_NO_ALT_ENTER),"Disable automatic Alt+Enter");
            Check(swap->GetBuffer(0,IID_PPV_ARGS(&output)),"Get backbuffer");
        }else output=Texture(device.Get(),w,h,D3D11_BIND_RENDER_TARGET);
        Check(device->CreateRenderTargetView(output.Get(),nullptr,&rtv),"Create RTV");
        for(unsigned i=0;i<3;++i){auto pixels=Pattern(w,h,i);auto texture=Texture(device.Get(),w*2,h,D3D11_BIND_SHADER_RESOURCE,&pixels);
            Check(device->CreateShaderResourceView(texture.Get(),nullptr,&input[i]),"Create SBS SRV");}
        vendor.Init(device.Get(),s,w,h);
    }
    void Draw(const titan::Snapshot& s,unsigned mode) {
        const float clear[]{0.125f,0.25f,0.5f,1};context->ClearRenderTargetView(rtv.Get(),clear);
        D3D11_VIEWPORT vp{0,0,static_cast<float>(width),static_cast<float>(height),0,1};
        context->RSSetViewports(1,&vp);
        // Return value is always 1 in this vendor DLL; it is not a success check.
        vendor.draw(s.eye[0],s.eye[1],s.eye[2],context.Get(),rtv.Get(),input[mode].Get(),
            width*2,height,format,static_cast<int>(s.stereoMode),0);
        Check(device->GetDeviceRemovedReason(),"Device removed");
    }
    void UpdateHud() {
        // Identical text in both SBS eyes, interleaved by the vendor like the
        // test image. No extra window, scaling or post-interlace GDI drawing.
        const unsigned w=std::min(width,1400u),h=std::min(height,72u);
        BITMAPINFO bi{};bi.bmiHeader.biSize=sizeof(BITMAPINFOHEADER);
        bi.bmiHeader.biWidth=w;bi.bmiHeader.biHeight=-static_cast<LONG>(h);
        bi.bmiHeader.biPlanes=1;bi.bmiHeader.biBitCount=32;bi.bmiHeader.biCompression=BI_RGB;
        void* bits{};HDC dc=CreateCompatibleDC(nullptr);
        HBITMAP bitmap=CreateDIBSection(dc,&bi,DIB_RGB_COLORS,&bits,nullptr,0);
        HFONT font=CreateFontW(-26,0,0,0,FW_NORMAL,FALSE,FALSE,FALSE,DEFAULT_CHARSET,
            OUT_DEFAULT_PRECIS,CLIP_DEFAULT_PRECIS,DEFAULT_QUALITY,DEFAULT_PITCH,L"Segoe UI");
        if(!dc||!bitmap||!font){
            if(font)DeleteObject(font);if(bitmap)DeleteObject(bitmap);if(dc)DeleteDC(dc);
            throw std::runtime_error("Cannot create test HUD");
        }
        auto oldBitmap=SelectObject(dc,bitmap),oldFont=SelectObject(dc,font);
        memset(bits,0,static_cast<size_t>(w)*h*4);SetBkMode(dc,TRANSPARENT);SetTextColor(dc,RGB(255,255,255));
        wchar_t label[256]{};
        swprintf_s(label,L"Global X: %+.1f mm | Ctrl+Alt+Left/Right: 0.5, +Shift: 0.1, Home: zero\nPrediction: %d ms | Ctrl+Alt+F9: toggle | Pattern: %u | Esc: exit",
            HorizontalOffset(),predictionMs,pattern+1);
        RECT rect{8,3,static_cast<LONG>(w),static_cast<LONG>(h)};
        DrawTextW(dc,label,-1,&rect,DT_LEFT|DT_NOPREFIX);GdiFlush();
        auto pixels=static_cast<unsigned*>(bits);
        for(size_t i=0;i<static_cast<size_t>(w)*h;++i)pixels[i]|=0xff000000u;
        context->ClearState();
        for(auto& view:input){
            ComPtr<ID3D11Resource> texture;view->GetResource(&texture);
            for(unsigned eye=0;eye<2;++eye){
                D3D11_BOX box{eye*width,0,0,eye*width+w,h,1};
                context->UpdateSubresource(texture.Get(),0,&box,bits,w*4,0);
            }
        }
        SelectObject(dc,oldFont);SelectObject(dc,oldBitmap);
        DeleteObject(font);DeleteObject(bitmap);DeleteDC(dc);
        hudDirty=false;
    }
    std::vector<unsigned> Readback(ID3D11Texture2D* source=nullptr) {
        if(!source)source=output.Get();
        D3D11_TEXTURE2D_DESC desc{};source->GetDesc(&desc);desc.Usage=D3D11_USAGE_STAGING;
        desc.BindFlags=0;desc.CPUAccessFlags=D3D11_CPU_ACCESS_READ;
        ComPtr<ID3D11Texture2D> staging;Check(device->CreateTexture2D(&desc,nullptr,&staging),"Create readback");
        context->CopyResource(staging.Get(),source);D3D11_MAPPED_SUBRESOURCE map{};
        Check(context->Map(staging.Get(),0,D3D11_MAP_READ,0,&map),"Readback");
        std::vector<unsigned> pixels(static_cast<size_t>(width)*height);
        for(unsigned y=0;y<height;++y)memcpy(pixels.data()+static_cast<size_t>(y)*width,
            static_cast<const char*>(map.pData)+static_cast<size_t>(y)*map.RowPitch,width*4);
        context->Unmap(staging.Get(),0);return pixels;
    }
};
LRESULT CALLBACK WindowProc(HWND window,UINT message,WPARAM wParam,LPARAM lParam) {
    switch(message){
    case WM_KEYDOWN:
        if(wParam==VK_ESCAPE)DestroyWindow(window);
        if(wParam==VK_RETURN)start=true;
        if(wParam>='1'&&wParam<='3'){pattern=static_cast<unsigned>(wParam-'1');hudDirty=true;Log("Pattern="+std::to_string(pattern));}
        return 0;
    case WM_PAINT:{PAINTSTRUCT ps{};auto dc=BeginPaint(window,&ps);RECT r{};GetClientRect(window,&r);
        wchar_t text[640]{};
        swprintf_s(text,L"原厂交织对照测试\n\n先把本窗口移到裸眼 3D 显示器，再按 Enter。\n\n1：立体网格和圆盘   2：左眼白 / 右眼黑   3：左眼黑 / 右眼白\n\n全局 Ctrl+Alt+← / →：±0.5 mm；另加 Shift：±0.1 mm\nCtrl+Alt+Home 归零；Ctrl+Alt+F9 开关预测\n补偿和预测由桥接程序控制，对所有桥接客户端生效。\n\nEsc 退出。原生分辨率，关闭 HDR。");
        SetBkMode(dc,TRANSPARENT);DrawTextW(dc,text,-1,&r,DT_CENTER|DT_WORDBREAK);
        EndPaint(window,&ps);return 0;}
    case WM_CLOSE:DestroyWindow(window);return 0;
    case WM_DESTROY:quit=true;PostQuitMessage(0);return 0;
    }
    return DefWindowProcW(window,message,wParam,lParam);
}
void Pump() {MSG msg{};while(PeekMessageW(&msg,nullptr,0,0,PM_REMOVE)){if(msg.message==WM_QUIT)quit=true;TranslateMessage(&msg);DispatchMessageW(&msg);}}
int SelfTest() {
    // No IPC, camera, MCU, window or lens requests on this path.
    titan::Snapshot s{};s.calibrationValid=s.eyePositionValid=1;s.eye[2]=600;
    const double refract[]{0.345,0.233,0.8,9.3,0.017,0.02,0,0};memcpy(s.refractParams,refract,sizeof(refract));
    const double whole[]{0.4,-0.6,0.3,1.3,-2.1,0.7};memcpy(s.wholeMat,whole,sizeof(whole));
    Graphics graphics;graphics.Init(nullptr,nullptr,128,64,s,true);
    graphics.Draw(s,1);auto first=graphics.Readback();s.eye[0]=25;
    graphics.Draw(s,1);auto moved=graphics.Readback();
    unsigned changed{},mixed{};
    for(size_t i=0;i<first.size();++i){changed+=first[i]!=moved[i];mixed+=(first[i]&0xffffff)!=0 && (first[i]&0xffffff)!=0xffffff;}
    if(changed<100||mixed<100)throw std::runtime_error("Native interleaver did not produce eye-dependent subpixel output");
    Log("PASS native WARP readback: eye-dependent pixels="+std::to_string(changed)+" mixed="+std::to_string(mixed));
    // Independently compare native CPU calibration mapping + DX11 output with
    // the bridge's constant mapper feeding the recovered original CS.
    std::ifstream shaderFile(directory.parent_path().parent_path()/L"shaders/BoeOriginalCS.dxbc",std::ios::binary);
    std::vector<char> shader((std::istreambuf_iterator<char>(shaderFile)),{});
    if(shader.size()!=23656)throw std::runtime_error("Self-test needs source shaders/BoeOriginalCS.dxbc");
    ComPtr<ID3D11ComputeShader> cs;Check(graphics.device->CreateComputeShader(shader.data(),shader.size(),nullptr,&cs),"Reference CS");
    D3D11_TEXTURE2D_DESC td{};graphics.output->GetDesc(&td);td.Format=DXGI_FORMAT_R8G8B8A8_UNORM;td.BindFlags=D3D11_BIND_UNORDERED_ACCESS;
    ComPtr<ID3D11Texture2D> reference;Check(graphics.device->CreateTexture2D(&td,nullptr,&reference),"Reference texture");
    ComPtr<ID3D11UnorderedAccessView> uav;Check(graphics.device->CreateUnorderedAccessView(reference.Get(),nullptr,&uav),"Reference UAV");
    D3D11_BUFFER_DESC bd{};bd.ByteWidth=sizeof(titan::render::OpticalConstants);bd.BindFlags=D3D11_BIND_CONSTANT_BUFFER;
    ComPtr<ID3D11Buffer> cb;Check(graphics.device->CreateBuffer(&bd,nullptr,&cb),"Reference CB");
    size_t compared{},mismatches{};unsigned maxDifference{};
    for(unsigned pass=0;pass<6;++pass){
        s.eye[0]=(pass%3==0?-53.0f:pass%3==1?0.0f:80.0f);s.eye[1]=pass%2?32.0f:-20.0f;
        s.eye[2]=pass%2?600.0f:250.0f;s.stereoMode=pass/3;
        graphics.Draw(s,0);const auto native=graphics.Readback();
        titan::render::OpticalConstants constants{};
        if(!titan::render::MakeOpticalConstants(s,128,64,256,64,constants))throw std::runtime_error("Reference constants invalid");
        graphics.context->ClearState();graphics.context->UpdateSubresource(cb.Get(),0,nullptr,&constants,0,0);
        graphics.context->CSSetShader(cs.Get(),nullptr,0);graphics.context->CSSetConstantBuffers(0,1,cb.GetAddressOf());
        graphics.context->CSSetShaderResources(0,1,graphics.input[0].GetAddressOf());graphics.context->CSSetUnorderedAccessViews(0,1,uav.GetAddressOf(),nullptr);
        graphics.context->Dispatch(16,8,1);graphics.context->ClearState();const auto expected=graphics.Readback(reference.Get());
        for(size_t i=0;i<native.size();++i)for(unsigned c=0;c<4;++c){
            const unsigned a=(native[i]>>(8*c))&255,b=(expected[i]>>(8*(c<3?2-c:c)))&255;
            const auto diff=a>b?a-b:b-a;maxDifference=std::max(maxDifference,diff);mismatches+=diff!=0;++compared;
        }
    }
    Log("Native vs bridge constants/original CS: channels="+std::to_string(compared)+" mismatches="+
        std::to_string(mismatches)+" max_difference="+std::to_string(maxDifference));
    if(mismatches)throw std::runtime_error("Native/reference optical mismatch");
    graphics.UpdateHud();graphics.Draw(s,0);
    if(graphics.Readback().empty())throw std::runtime_error("HUD readback failed");
    Log("PASS SBS HUD upload and native draw");
    return 0;
}
}
int WINAPI wWinMain(HINSTANCE instance,HINSTANCE,PWSTR args,int) {
    const bool offline=std::wstring(args)==L"--self-test";
    try {
        SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
        wchar_t path[32768]{};GetModuleFileNameW(nullptr,path,32768);directory=std::filesystem::path(path).parent_path();
        std::filesystem::current_path(directory);std::filesystem::create_directories(directory/L"logs");
        logFile.open(directory/L"logs"/(offline?L"StereoTest-offline.log":L"StereoTest.log"),offline?std::ios::trunc:std::ios::app);
        Log(offline?"Offline WARP native SDK test":"Original native DX11 interleaver; shared broker offset/prediction");
        if(offline)return SelfTest();
        auto ipc=titan::IpcClient::Connect();titan::Snapshot s{};
        if(!ipc->SnapshotForWeave(s))throw std::runtime_error("Start LeiaSRBridge and wait for device/calibration readiness");
        WNDCLASSW wc{};wc.hInstance=instance;wc.lpfnWndProc=WindowProc;wc.lpszClassName=L"Stereo3DBridgeTest";
        wc.hCursor=LoadCursorW(nullptr,IDC_ARROW);wc.hbrBackground=reinterpret_cast<HBRUSH>(COLOR_WINDOW+1);
        RegisterClassW(&wc);HWND window=CreateWindowW(wc.lpszClassName,L"原厂交织测试 — 移动到目标屏后按 Enter",WS_OVERLAPPEDWINDOW,
            CW_USEDEFAULT,CW_USEDEFAULT,800,400,nullptr,nullptr,instance,nullptr);
        if(!window)throw std::runtime_error("Create test window failed");
        ShowWindow(window,SW_SHOW);
        while(!start&&!quit){Pump();if(!start&&!quit)WaitMessage();}
        if(quit)return 0;
        const auto monitor=MonitorFromWindow(window,MONITOR_DEFAULTTONEAREST);MONITORINFO mi{sizeof(mi)};
        if(!GetMonitorInfoW(monitor,&mi))throw std::runtime_error("GetMonitorInfo failed");
        const unsigned width=mi.rcMonitor.right-mi.rcMonitor.left,height=mi.rcMonitor.bottom-mi.rcMonitor.top;
        SetWindowLongPtrW(window,GWL_STYLE,WS_POPUP|WS_VISIBLE);
        SetWindowPos(window,HWND_TOP,mi.rcMonitor.left,mi.rcMonitor.top,width,height,SWP_FRAMECHANGED);
        SetForegroundWindow(window);
        if(!ipc->SnapshotForWeave(s))throw std::runtime_error("Device became unavailable before test");
        Graphics graphics;graphics.Init(window,monitor,width,height,s,false);
        ipc->AddLensRequest();
        while(!quit){
            Pump();if(quit)break;
            if(GetForegroundWindow()!=window){ipc->MarkWeave(window,false);Sleep(20);continue;}
            RefreshTuning();if(hudDirty)graphics.UpdateHud();
            if(!ipc->SnapshotForWeave(s)){ipc->MarkWeave(window,false);throw std::runtime_error("Broker/device unavailable; test stopped");}
            graphics.Draw(s,pattern);
            const HRESULT result=graphics.swap->Present(1,0);Check(result,"Present");
            ipc->MarkWeave(window,result==S_OK);
            if(result==DXGI_STATUS_OCCLUDED)Sleep(20);
        }
        ipc->MarkWeave(window,false);ipc->RemoveLensRequest();graphics.context->ClearState();graphics.context->Flush();
        Log("Test stopped");return 0;
    }catch(const std::exception& e){Log(std::string("ERROR: ")+e.what());
        if(!offline)MessageBoxA(nullptr,e.what(),"LeiaSRBridgeTest",MB_ICONERROR);return 1;}
}
