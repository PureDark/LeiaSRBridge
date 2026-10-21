// Included inside SR. Legacy class layouts come from the unmodified SDK headers.
namespace {
struct LegacyState {
    std::shared_ptr<ContextState> state;
    HWND window{};
    uint64_t latency{16666};
    bool latencyFrames{},readSrgb{},writeSrgb{},reportedError{};
    float contrast{1};
    explicit LegacyState(std::shared_ptr<ContextState> value):state(std::move(value)){state->client->AddLensRequest();}
    ~LegacyState(){state->client->RemoveLensRequest();}
    bool Snapshot(titan::Snapshot& s)const{return state->alive.load()&&state->client->SnapshotForWeave(s);}
    uint64_t Latency()const {
        if(!latencyFrames)return latency;
        MONITORINFOEXW info{};info.cbSize=sizeof(info);DEVMODEW mode{};mode.dmSize=sizeof(mode);DWORD hz=60;
        if(GetMonitorInfoW(MonitorFromWindow(window,MONITOR_DEFAULTTOPRIMARY),&info)&&EnumDisplaySettingsW(info.szDevice,ENUM_CURRENT_SETTINGS,&mode)&&mode.dmDisplayFrequency>1)hz=mode.dmDisplayFrequency;
        return latency>UINT64_MAX/1000000?UINT64_MAX:latency*1000000/hz;
    }
    void Result(bool rendered,bool usable,const std::string& error){state->client->MarkWeave(window,rendered&&usable);if(!rendered&&!reportedError)titan::RuntimeLog(error.c_str());reportedError=!rendered;}
};
DXGI_FORMAT LegacyFormat(DXGI_FORMAT f){
    switch(f){case DXGI_FORMAT_R8G8B8A8_TYPELESS:return DXGI_FORMAT_R8G8B8A8_UNORM;case DXGI_FORMAT_B8G8R8A8_TYPELESS:return DXGI_FORMAT_B8G8R8A8_UNORM;case DXGI_FORMAT_R16G16B16A16_TYPELESS:return DXGI_FORMAT_R16G16B16A16_FLOAT;default:return f;}
}
}
class DX11WeaverBase::Impl : public LegacyState {
public:
    ComPtr<ID3D11Device> device;
    ComPtr<ID3D11DeviceContext> context;
    ComPtr<ID3D11Texture2D> texture;
    ComPtr<ID3D11RenderTargetView> inputRtv;
    ComPtr<ID3D11ShaderResourceView> input;
    titan::render::Renderer11 renderer;
    Impl(std::shared_ptr<ContextState> s,ID3D11Device* dev,ID3D11DeviceContext* ctx,unsigned w,unsigned h,DXGI_FORMAT format,HWND hwnd)
        :LegacyState(std::move(s)),device(dev),context(ctx),renderer(dev){
        if(!dev||!ctx||!w||!h||w%2)throw std::invalid_argument("Invalid legacy DX11 SBS parameters");window=hwnd;
        D3D11_TEXTURE2D_DESC d{};d.Width=w;d.Height=h;d.MipLevels=d.ArraySize=d.SampleDesc.Count=1;d.Format=LegacyFormat(format);d.BindFlags=D3D11_BIND_RENDER_TARGET|D3D11_BIND_SHADER_RESOURCE;
        if(FAILED(dev->CreateTexture2D(&d,nullptr,&texture))||FAILED(dev->CreateRenderTargetView(texture.Get(),nullptr,&inputRtv))||FAILED(dev->CreateShaderResourceView(texture.Get(),nullptr,&input)))throw std::runtime_error("Cannot create legacy DX11 SBS target");
    }
};
DX11WeaverBase::DX11WeaverBase()=default;
DX11WeaverBase::~DX11WeaverBase()=default;
PredictingDX11Weaver::PredictingDX11Weaver(SRContext& c,ID3D11Device* d,ID3D11DeviceContext* dc,unsigned w,unsigned h,DXGI_FORMAT f,HWND hwnd){pimpl=std::make_unique<Impl>(FindContext(&c),d,dc,w,h,f,hwnd);}
PredictingDX11Weaver::PredictingDX11Weaver(SRContext& c,ID3D11Device* d,ID3D11DeviceContext* dc,unsigned w,unsigned h,HWND hwnd):PredictingDX11Weaver(c,d,dc,w,h,DXGI_FORMAT_R8G8B8A8_UNORM,hwnd){}
PredictingDX11Weaver::PredictingDX11Weaver(SRContext& c,ID3D11Device* d,ID3D11DeviceContext* dc,unsigned w,unsigned h):PredictingDX11Weaver(c,d,dc,w,h,nullptr){}
PredictingDX11Weaver::PredictingDX11Weaver(SRContext& c,ID3D11Device* d,ID3D11DeviceContext* dc,unsigned w,unsigned h,DXGI_FORMAT f):PredictingDX11Weaver(c,d,dc,w,h,f,nullptr){}
PredictingDX11Weaver::~PredictingDX11Weaver()=default;
ID3D11RenderTargetView* DX11WeaverBase::getFrameBuffer(){return pimpl?pimpl->inputRtv.Get():nullptr;}
void DX11WeaverBase::setInputFrameBuffer(ID3D11ShaderResourceView* v){pimpl->input=v;}
void DX11WeaverBase::setContext(ID3D11DeviceContext* c){pimpl->context=c;}
void DX11WeaverBase::setWindowHandle(HWND w){pimpl->window=w;}
void DX11WeaverBase::enableLateLatching(bool){}
bool DX11WeaverBase::isLateLatchingEnabled()const{return false;}
void DX11WeaverBase::setContrast(float v){pimpl->contrast=v;}
float DX11WeaverBase::getContrast()const{return pimpl->contrast;}
void DX11WeaverBase::setShaderSRGBConversion(bool r,bool w){pimpl->readSrgb=r;pimpl->writeSrgb=w;}
void DX11WeaverBase::setACTMode(WeaverACTMode){}
WeaverACTMode DX11WeaverBase::getACTMode()const{return static_cast<WeaverACTMode>(0);}
void DX11WeaverBase::setCrosstalkStaticFactor(float){}
float DX11WeaverBase::getCrosstalkStaticFactor()const{return 0;}
void DX11WeaverBase::setCrosstalkDynamicFactor(float){}
float DX11WeaverBase::getCrosstalkDynamicFactor()const{return 0;}
bool DX11WeaverBase::canWeave(){titan::Snapshot s{};return pimpl&&pimpl->input&&pimpl->context&&pimpl->Snapshot(s);}
bool DX11WeaverBase::canWeave(unsigned w,unsigned h){return w&&h&&canWeave();}
bool DX11WeaverBase::canWeave(unsigned w,unsigned h,unsigned,unsigned){return canWeave(w,h);}
void DX11WeaverBase::weave(unsigned w,unsigned h){weave(w,h,0,0);}
void DX11WeaverBase::weave(unsigned w,unsigned h,unsigned x,unsigned y){
    if(!pimpl)return;
    titan::Snapshot s{};const bool usable=pimpl->Snapshot(s);std::string error;
    const D3D11_VIEWPORT viewport{static_cast<float>(x),static_cast<float>(y),static_cast<float>(w),static_cast<float>(h),0,1};
    const bool ok=pimpl->renderer.Draw(pimpl->context.Get(),pimpl->input.Get(),viewport,usable?&s:nullptr,error,pimpl->readSrgb,pimpl->writeSrgb);
    pimpl->Result(ok,usable,error);
}
void PredictingDX11Weaver::setLatency(uint64_t v){pimpl->latency=v;pimpl->latencyFrames=false;}
uint64_t PredictingDX11Weaver::getLatency(){return pimpl->Latency();}
void PredictingDX11Weaver::setLatencyInFrames(uint64_t v){pimpl->latency=v;pimpl->latencyFrames=true;}

class DX12WeaverBase::Impl : public LegacyState {
public:
    ComPtr<ID3D12Resource> input;
    ID3D12Resource* output{}; // Legacy API does not retain the host's backbuffer.
    ID3D12GraphicsCommandList* list{};
    DXGI_FORMAT format{DXGI_FORMAT_UNKNOWN};
    titan::render::Renderer renderer;
    Impl(std::shared_ptr<ContextState> s,ID3D12Device* d):LegacyState(std::move(s)),renderer(d){}
};
DX12WeaverBase::DX12WeaverBase()=default;
DX12WeaverBase::~DX12WeaverBase()=default;
PredictingDX12Weaver::PredictingDX12Weaver(SRContext& c,ID3D12Device* d,ID3D12CommandAllocator*,ID3D12CommandQueue*,ID3D12Resource* input,ID3D12Resource* output,HWND hwnd,DXGI_FORMAT f){pimpl=std::make_unique<Impl>(FindContext(&c),d);pimpl->window=hwnd;pimpl->output=output;setInputFrameBuffer(input,f);}
PredictingDX12Weaver::PredictingDX12Weaver(SRContext& c,ID3D12Device* d,ID3D12CommandAllocator* a,ID3D12CommandQueue* q,ID3D12Resource* i,ID3D12Resource* o,HWND w):PredictingDX12Weaver(c,d,a,q,i,o,w,DXGI_FORMAT_UNKNOWN){}
PredictingDX12Weaver::PredictingDX12Weaver(SRContext& c,ID3D12Device* d,ID3D12CommandAllocator* a,ID3D12CommandQueue* q,ID3D12Resource* i,ID3D12Resource* o):PredictingDX12Weaver(c,d,a,q,i,o,nullptr){}
PredictingDX12Weaver::~PredictingDX12Weaver()=default;
ID3D12Resource* DX12WeaverBase::getFrameBuffer(){return pimpl?pimpl->input.Get():nullptr;}
void DX12WeaverBase::setInputFrameBuffer(ID3D12Resource* input){setInputFrameBuffer(input,DXGI_FORMAT_UNKNOWN);}
void DX12WeaverBase::setInputFrameBuffer(ID3D12Resource* input,DXGI_FORMAT f){pimpl->input=input;pimpl->format=f;}
void DX12WeaverBase::setOutputFrameBuffer(ID3D12Resource* output){pimpl->output=output;}
void DX12WeaverBase::setCommandList(ID3D12GraphicsCommandList* list){pimpl->list=list;}
void DX12WeaverBase::setWindowHandle(HWND w){pimpl->window=w;}
void DX12WeaverBase::setContrast(float v){pimpl->contrast=v;}
float DX12WeaverBase::getContrast()const{return pimpl->contrast;}
void DX12WeaverBase::setShaderSRGBConversion(bool r,bool w){pimpl->readSrgb=r;pimpl->writeSrgb=w;}
void DX12WeaverBase::setACTMode(WeaverACTMode){}
WeaverACTMode DX12WeaverBase::getACTMode()const{return static_cast<WeaverACTMode>(0);}
void DX12WeaverBase::setCrosstalkStaticFactor(float){}
float DX12WeaverBase::getCrosstalkStaticFactor()const{return 0;}
void DX12WeaverBase::setCrosstalkDynamicFactor(float){}
float DX12WeaverBase::getCrosstalkDynamicFactor()const{return 0;}
bool DX12WeaverBase::canWeave(){titan::Snapshot s{};return pimpl&&pimpl->input&&pimpl->output&&pimpl->Snapshot(s);}
bool DX12WeaverBase::canWeave(unsigned w,unsigned h){return w&&h&&canWeave();}
bool DX12WeaverBase::canWeave(unsigned w,unsigned h,unsigned,unsigned){return canWeave(w,h);}
void DX12WeaverBase::weave(unsigned w,unsigned h){weave(w,h,0,0);}
void DX12WeaverBase::weave(unsigned w,unsigned h,unsigned x,unsigned y){
    if(!pimpl||!pimpl->list||!pimpl->input||!pimpl->output){if(pimpl)pimpl->Result(false,false,"Legacy DX12 weave requires command list, SBS input and output resource");return;}
    titan::Snapshot live{},s{};const bool liveValid=pimpl->Snapshot(live);bool usable{};std::string error;
    if(!titan::SelectRecordingSnapshot(pimpl->list,liveValid?&live:nullptr,s,usable,error)){pimpl->Result(false,false,error);return;}
    titan::render::WeaveRequest r{};r.commandList=pimpl->list;r.inputSbs=pimpl->input.Get();r.inputFormat=pimpl->format;r.outputFormat=LegacyFormat(pimpl->output->GetDesc().Format);r.readSrgb=pimpl->readSrgb;r.writeSrgb=pimpl->writeSrgb;
    r.viewport={static_cast<float>(x),static_cast<float>(y),static_cast<float>(w),static_cast<float>(h),0,1};r.scissor={static_cast<LONG>(x),static_cast<LONG>(y),static_cast<LONG>(x+w),static_cast<LONG>(y+h)};
    // Deprecated SDK contract: input enters/leaves in UAV, target is already bound in RT state.
    D3D12_RESOURCE_BARRIER barrier{};barrier.Type=D3D12_RESOURCE_BARRIER_TYPE_TRANSITION;barrier.Transition={pimpl->input.Get(),D3D12_RESOURCE_BARRIER_ALL_SUBRESOURCES,D3D12_RESOURCE_STATE_UNORDERED_ACCESS,D3D12_RESOURCE_STATE_PIXEL_SHADER_RESOURCE};
    pimpl->list->ResourceBarrier(1,&barrier);
    const bool ok=pimpl->renderer.Record(r,usable?&s:nullptr,error);
    std::swap(barrier.Transition.StateBefore,barrier.Transition.StateAfter);pimpl->list->ResourceBarrier(1,&barrier);
    pimpl->Result(ok,usable,error);
}
void DX12WeaverBase::weave(ID3D12GraphicsCommandList* l,unsigned w,unsigned h){setCommandList(l);weave(w,h);}
void DX12WeaverBase::weave(ID3D12GraphicsCommandList* l,unsigned w,unsigned h,unsigned x,unsigned y){setCommandList(l);weave(w,h,x,y);}
void PredictingDX12Weaver::setLatency(uint64_t v){pimpl->latency=v;pimpl->latencyFrames=false;}
uint64_t PredictingDX12Weaver::getLatency(){return pimpl->Latency();}
void PredictingDX12Weaver::setLatencyInFrames(uint64_t v){pimpl->latency=v;pimpl->latencyFrames=true;}
