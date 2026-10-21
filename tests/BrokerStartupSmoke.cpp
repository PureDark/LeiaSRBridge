// Test-only device provider: exercise the real broker's login retry without loading vendor code.
#include "../src/broker/BrokerService.h"
#include <atomic>
#include <chrono>
#include <thread>
#include <iostream>
#include <stdexcept>
std::atomic<int> attempts{},closed{};
namespace titan::broker {
struct DeviceProvider::Impl {Snapshot state{};bool opened{};};
DeviceProvider::DeviceProvider(ProviderOptions,LogFunction):impl_(std::make_unique<Impl>()){}
DeviceProvider::~DeviceProvider(){Shutdown();}
bool DeviceProvider::Initialize(){
    impl_->opened=true;impl_->state={};
    if(++attempts==1){impl_->state.status=ProviderStatus::NoDevice;return false;}
    impl_->state.status=ProviderStatus::Ready;impl_->state.calibrationValid=impl_->state.eyePositionValid=1;return true;
}
void DeviceProvider::Shutdown(){if(impl_->opened){++closed;impl_->opened=false;}impl_->state.calibrationValid=impl_->state.eyePositionValid=impl_->state.lensEnabled=0;}
void DeviceProvider::Poll(){}
bool DeviceProvider::SetLens(bool enabled){impl_->state.lensEnabled=enabled;return true;}
const Snapshot& DeviceProvider::State()const noexcept{return impl_->state;}
}
void Require(bool value,const char* message){if(!value)throw std::runtime_error(message);}
template<class F> bool Wait(F predicate,int milliseconds){
    const auto end=GetTickCount64()+milliseconds;
    while(GetTickCount64()<end){if(predicate())return true;std::this_thread::sleep_for(std::chrono::milliseconds(20));}return false;
}
int main(){
    using namespace titan;using namespace titan::broker;
    try{
        BrokerService broker;ProviderOptions options;
        Require(broker.Start(options,{}),"start");
        Require(Wait([&]{return broker.ReadStatus().status==ProviderStatus::NoDevice;},2000),"publish missing device");
        Require(closed==1,"release handles before retry");
        Require(!broker.ReadStatus().calibrationValid,"failed device not advertised ready");
        Require(Wait([&]{return broker.ReadStatus().status==ProviderStatus::Ready;},6500),"late USB ready without restarting app");
        Require(attempts==2,"one retry");broker.Stop();Require(closed==2,"close initialized provider");
        attempts=0;closed=0;
        Require(broker.Start(options,{}),"restart");
        Require(Wait([&]{return closed==1;},2000),"waiting after initial failure");
        const auto before=GetTickCount64();broker.Stop();Require(GetTickCount64()-before<1000,"interruptible retry wait");
        Require(attempts==1&&!GetModuleHandleW(L"MCU_library.dll"),"no later attempt or hardware");
        std::cout<<"BrokerStartup PASS: delayed USB retry; failed handles released; shutdown interrupts wait; fake provider only.\n";return 0;
    }catch(const std::exception& e){std::cerr<<e.what()<<'\n';return 1;}
}
