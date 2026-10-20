#include "RecordingSnapshot.h"
#include <wrl/client.h>
#include <array>
#include <atomic>
#include <mutex>
#include <new>
#include <utility>

namespace titan {
namespace {
using Microsoft::WRL::ComPtr;
using ResetFunction = HRESULT(STDMETHODCALLTYPE*)(ID3D12GraphicsCommandList*, ID3D12CommandAllocator*, ID3D12PipelineState*);
// Windows SDK 10.0.26100.0 um/d3d12.h ID3D12GraphicsCommandListVtbl:
// IUnknown 0..2, ID3D12Object 3..6, GetDevice 7, GetType 8, Close 9, Reset 10.
constexpr std::size_t kResetSlot = 10;
constexpr std::size_t kMaxHookedVtables = 64;
constexpr GUID kRecordingToken = {0xc5398a76,0xab12,0x4f13,{0x96,0x28,0x9a,0x4d,0x73,0x9f,0x23,0x02}};
constexpr GUID kRecordingInterface = {0xb4399a13,0x4ada,0x4982,{0xb1,0x79,0x3d,0x1e,0x77,0x20,0x4c,0x2f}};

struct State final : IUnknown {
    std::atomic<ULONG> references{1};
    std::mutex mutex;
    std::uint64_t generation{};
    Snapshot snapshot{};
    bool valid{};
    HRESULT STDMETHODCALLTYPE QueryInterface(REFIID iid, void** out) override {
        if (!out) return E_POINTER;
        *out = nullptr;
        if (iid != __uuidof(IUnknown) && iid != kRecordingInterface) return E_NOINTERFACE;
        *out = static_cast<IUnknown*>(this); AddRef(); return S_OK;
    }
    ULONG STDMETHODCALLTYPE AddRef() override { return references.fetch_add(1) + 1; }
    ULONG STDMETHODCALLTYPE Release() override {
        const ULONG count = references.fetch_sub(1) - 1;
        if (!count) delete this;
        return count;
    }
    void ResetSucceeded() noexcept {
        std::lock_guard lock(mutex);
        ++generation;
        valid = false;
    }
};

ComPtr<State> GetState(ID3D12GraphicsCommandList* list) noexcept {
    ComPtr<IUnknown> object;
    UINT size = sizeof(IUnknown*);
    if (FAILED(list->GetPrivateData(kRecordingToken, &size, object.GetAddressOf())) ||
        size != sizeof(IUnknown*) || !object) return {};
    ComPtr<IUnknown> verified;
    if (FAILED(object->QueryInterface(kRecordingInterface, &verified))) return {};
    ComPtr<State> state;
    state.Attach(static_cast<State*>(verified.Detach()));
    return state;
}

struct HookEntry {
    void** vtable{};
    std::atomic<ResetFunction> original{};
    ResetFunction thunk{};
    bool installed{};
};
struct Registry {
    std::mutex mutex;
    std::array<HookEntry, kMaxHookedVtables> entries{};
    std::size_t used{};
};
Registry& Hooks() {
    // Vtable hooks outlive individual weavers, including during DLL shutdown.
    // The runtime is pinned before context creation; this registry is likewise
    // process-lifetime and never destroys its mutex while a thunk can run.
    static auto* registry = new Registry();
    return *registry;
}

template<std::size_t Index>
HRESULT STDMETHODCALLTYPE ResetThunk(ID3D12GraphicsCommandList* list,
    ID3D12CommandAllocator* allocator, ID3D12PipelineState* pipeline) noexcept {
    const auto original = Hooks().entries[Index].original.load(std::memory_order_acquire);
    // Each vtable has its own statically compiled thunk, so forwarding does not
    // depend on the object's current vtable (which an overlay may later clone).
    // No registry/state lock is held while calling the application's Reset.
    const HRESULT result = original ? original(list, allocator, pipeline) : E_UNEXPECTED;
    if (SUCCEEDED(result)) {
        if (auto state = GetState(list)) state->ResetSucceeded();
    }
    return result;
}
template<std::size_t... Indices>
constexpr auto Thunks(std::index_sequence<Indices...>) {
    return std::array<ResetFunction, sizeof...(Indices)>{&ResetThunk<Indices>...};
}
constexpr auto kThunks = Thunks(std::make_index_sequence<kMaxHookedVtables>{});

bool InstallLocked(ID3D12GraphicsCommandList* list, Registry& registry, std::string& error) {
    auto** vtable = *reinterpret_cast<void***>(list);
    auto* entryAddress = &vtable[kResetSlot];
    // An atomic read must not use a read/modify/write instruction on .rdata.
    const auto current = reinterpret_cast<ResetFunction>(*reinterpret_cast<void* const volatile*>(entryAddress));
    for (std::size_t i = 0; i < registry.used; ++i) {
        const auto& entry = registry.entries[i];
        if (entry.vtable != vtable) continue;
        if (entry.installed && current == entry.thunk) return true;
        error = "Command-list Reset vtable was changed by another component";
        return false;
    }
    // A cloned table that already points to our installed thunk is also safe:
    // the thunk retains the correct original function independently of table.
    for (std::size_t i = 0; i < registry.used; ++i)
        if (registry.entries[i].installed && current == registry.entries[i].thunk) return true;
    if (!current || registry.used == registry.entries.size()) {
        error = "Cannot track command-list Reset: missing function or vtable capacity exceeded"; return false;
    }
    MEMORY_BASIC_INFORMATION memory{};
    if (!VirtualQuery(entryAddress, &memory, sizeof(memory)) || memory.State != MEM_COMMIT ||
        (memory.Protect & (PAGE_NOACCESS | PAGE_GUARD))) {
        error = "Cannot access command-list Reset vtable"; return false;
    }
    DWORD previous{};
    const DWORD executable = PAGE_EXECUTE | PAGE_EXECUTE_READ | PAGE_EXECUTE_READWRITE | PAGE_EXECUTE_WRITECOPY;
    const DWORD writable = (memory.Protect & executable) ? PAGE_EXECUTE_READWRITE : PAGE_READWRITE;
    if (!VirtualProtect(entryAddress, sizeof(void*), writable, &previous)) {
        error = "Cannot make command-list Reset vtable writable"; return false;
    }
    // Publish original before making the thunk reachable. A failed/rolled-back
    // entry is deliberately never reused: an already-entered thunk may exist.
    const std::size_t index = registry.used++;
    auto& entry = registry.entries[index];
    entry.vtable = vtable;
    entry.thunk = kThunks[index];
    entry.original.store(current, std::memory_order_release);
    const auto exchanged = InterlockedCompareExchangePointer(entryAddress,
        reinterpret_cast<void*>(entry.thunk), reinterpret_cast<void*>(current));
    const bool changed = exchanged == reinterpret_cast<void*>(current);
    DWORD ignored{};
    const bool restored = VirtualProtect(entryAddress, sizeof(void*), previous, &ignored) != FALSE;
    if (!changed || !restored) {
        if (changed && !restored) {
            InterlockedCompareExchangePointer(entryAddress, reinterpret_cast<void*>(current), reinterpret_cast<void*>(entry.thunk));
            VirtualProtect(entryAddress, sizeof(void*), previous, &ignored);
        }
        error = "Failed to atomically install/protect command-list Reset tracker";
        return false;
    }
    entry.installed = true;
    return true;
}
}

bool SelectRecordingSnapshot(ID3D12GraphicsCommandList* list, const Snapshot* live,
    Snapshot& selected, bool& haveOpticalSnapshot, std::string& error) {
    haveOpticalSnapshot = false;
    if (!list) { error = "No command list for optical snapshot"; return false; }
    try {
        ComPtr<State> state;
        {
            auto& registry = Hooks();
            std::lock_guard lock(registry.mutex);
            if (!InstallLocked(list, registry, error)) return false;
            state = GetState(list);
            if (!state) {
                state.Attach(new State());
                if (FAILED(list->SetPrivateDataInterface(kRecordingToken, state.Get()))) {
                    error = "Cannot attach command-list recording token"; return false;
                }
            }
        }
        // Invalid current broker data always overrides a previously cached
        // snapshot. Keep the snapshot only for a possible recovery in this same
        // generation, so we never change optical mapping between valid passes.
        if (!live) return true;
        std::lock_guard lock(state->mutex);
        if (!state->valid) { state->snapshot = *live; state->valid = true; }
        selected = state->snapshot;
        haveOpticalSnapshot = true;
        return true;
    } catch (...) {
        error = "Cannot allocate command-list recording tracker"; return false;
    }
}

bool InspectRecordingSnapshot(ID3D12GraphicsCommandList* list, Snapshot& snapshot,
    std::uint64_t& generation) noexcept {
    if (!list) return false;
    if (auto state = GetState(list)) {
        std::lock_guard lock(state->mutex);
        generation = state->generation;
        if (!state->valid) return false;
        snapshot = state->snapshot;
        return true;
    }
    return false;
}
}

// Read-only diagnostics used by the ABI/WARP test and support tools. This does
// not modify broker state, calibration, the generation or any command stream.
extern "C" __declspec(dllexport) BOOL __cdecl TitanLeiaInspectRecordingSnapshot(
    ID3D12GraphicsCommandList* list, titan::Snapshot* snapshot, std::uint64_t* generation) noexcept {
    return snapshot && generation && titan::InspectRecordingSnapshot(list, *snapshot, *generation);
}
