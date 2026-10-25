#include "../src/app/Update.h"
#include <objbase.h>
#include <iostream>
int main(){
    CoInitializeEx(nullptr,COINIT_APARTMENTTHREADED);
    titan::app::Update update;
    update.Check(std::filesystem::current_path(),false);
    for(unsigned i=0;i<900;++i){
        update.Poll();
        const auto state=update.State();
        const auto phase=state.value("phase","");
        if(phase!="checking"){std::cout<<state.dump()<<std::endl;update.Stop();CoUninitialize();return phase=="error"?2:0;}
        Sleep(100);
    }
    update.Stop();CoUninitialize();return 3;
}
