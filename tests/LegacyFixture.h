#pragma once
#include "common/SharedProtocol.h"
#include <sr/management/srcontext.h>
#include <sr/weaver/dx12weaver.h>
#include <stdexcept>
struct LegacyFixture {
    HANDLE mutex{},mapping{};titan::SharedState* state{};SR::SRContext* context{};
    explicit LegacyFixture(titan::Snapshot sample){
        mutex=CreateMutexW(nullptr,TRUE,titan::kBrokerMutexName);
        if(!mutex||GetLastError()==ERROR_ALREADY_EXISTS)throw std::runtime_error("Close bridge before legacy test");
        mapping=CreateFileMappingW(INVALID_HANDLE_VALUE,nullptr,PAGE_READWRITE,0,sizeof(titan::SharedState),titan::kMappingName);
        if(!mapping||GetLastError()==ERROR_ALREADY_EXISTS)throw std::runtime_error("Existing broker mapping");
        state=static_cast<titan::SharedState*>(MapViewOfFile(mapping,FILE_MAP_ALL_ACCESS,0,0,sizeof(titan::SharedState)));
        if(!state)throw std::runtime_error("Map mock state");*state={};state->magic=titan::kProtocolMagic;state->version=titan::kProtocolVersion;state->size=sizeof(*state);
        sample.status=titan::ProviderStatus::Mock;sample.brokerPid=GetCurrentProcessId();sample.heartbeatTick=sample.observationTick=GetTickCount64();titan::WriteSnapshot(state,sample);
        context=SR::SRContext::create(true);context->initialize();
    }
    ~LegacyFixture(){if(context)SR::SRContext::deleteSRContext(context);if(state)UnmapViewOfFile(state);if(mapping)CloseHandle(mapping);if(mutex){ReleaseMutex(mutex);CloseHandle(mutex);}}
};
