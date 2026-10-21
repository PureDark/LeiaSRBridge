#include "broker/BrokerService.h"
#include <sr/types.h>
#include <sr/sense/system/systemevent.h>
#include <sr/sense/headtracker/head.h>
#include <sr/sense/eyetracker/eyepair.h>
#include <filesystem>
#include <atomic>
#include <iostream>
#include <stdexcept>
std::atomic<unsigned> events[32]{};std::atomic<unsigned> heads{},eyes{};
void System(SR_systemEvent e){if(e.eventType<32)++events[e.eventType];}
void Head(SR_head){++heads;}
void Eyes(SR_eyePair){++eyes;}
void Require(bool value,const char* message){if(!value)throw std::runtime_error(message);}
template<class T>T Fn(HMODULE module,const char* name){auto p=GetProcAddress(module,name);Require(p!=nullptr,name);return reinterpret_cast<T>(p);}
int wmain(int argc,wchar_t** argv){try{
    Require(argc==2,"Pass runtime path");std::filesystem::path dir=argv[1];
    titan::broker::ProviderOptions options{};options.mock=true;options.vendorDirectory=L"Z:\\NoVendor";titan::broker::BrokerService broker;Require(broker.Start(options,{}),"Mock start");
    for(unsigned i=0;i<100&&!broker.ReadStatus().calibrationValid;++i)Sleep(10);
    LoadLibraryW((dir/L"opencv_world343.dll").c_str());auto runtime=LoadLibraryW((dir/L"TitanLeiaRuntime.dll").c_str());Require(runtime,"runtime load");
    auto core=LoadLibraryW((dir/L"SimulatedRealityCore.dll").c_str()),face=LoadLibraryW((dir/L"SimulatedRealityFaceTrackers.dll").c_str()),display=LoadLibraryW((dir/L"SimulatedRealityDisplays.dll").c_str());
    auto create=Fn<void*(*)()>(core,"newSRContext");auto init=Fn<void(*)(void*)>(core,"initializeSRContext");auto destroy=Fn<void(*)(void*)>(core,"deleteSRContext");
    auto createSense=Fn<void*(*)(void*)>(core,"createSystemSense");auto listen=Fn<void*(*)(void*,void(*)(SR_systemEvent))>(core,"createSystemEventListener");auto unlisten=Fn<void(*)(void*)>(core,"deleteSystemEventListener");
    auto c=create();Require(c,"context");auto system=createSense(c);auto listener=listen(system,System);Require(listener,"system listener");
    auto h=Fn<void*(*)(void*)>(face,"createHeadTracker")(c);auto hl=Fn<void*(*)(void*,void(*)(SR_head))>(face,"createHeadListener")(h,Head);
    auto e=Fn<void*(*)(void*)>(face,"createEyeTracker")(c);auto el=Fn<void*(*)(void*,void(*)(SR_eyePair))>(face,"createEyePairListener")(e,Eyes);
    auto d=Fn<void*(*)(void*)>(display,"createDisplay")(c);Require(d&&Fn<int(*)(void*)>(display,"getResolutionWidth")(d)>0,"display dimensions");
    init(c);for(unsigned i=0;i<100&&(!events[12]||!heads||!eyes);i++)Sleep(10);
    Require(events[12]&&events[3]&&heads&&eyes,"ready/system/head/eye callbacks");
    broker.Stop();for(unsigned i=0;i<300&&!events[13];i++)Sleep(10);Require(events[13]&&events[2],"disconnect events");
    // Context may be destroyed before managed listener handles; no callbacks after teardown.
    destroy(c);auto count=heads.load();Sleep(50);Require(heads==count,"callback after context destruction");
    unlisten(listener);Fn<void(*)(void*)>(face,"deleteHeadListener")(hl);Fn<void(*)(void*)>(face,"deleteEyePairListener")(el);
    std::cout<<"PASS C ABI events, head/eyes, display and context-first teardown\n";return 0;
}catch(const std::exception& e){std::cerr<<e.what()<<'\n';return 1;}}
