# SDK no-face fallback repair (0.8.1)

## Report and established code defect

Stellar Blade through the bridge displays black when no eyes are detected.
This machine is currently using an original LeiaSR display in preview mode;
the reported Titan/Stellar Blade image has not been reproduced locally.

The broker and IpcClient::SnapshotForWeave already accept held/default optical
positions. However, HeadTracking.inl previously returned NaN for Head, HeadPose,
EyeTracker and createRaw whenever the optical position was not kEyeTracked.
Only the C callback adapters sanitized that data. OdysseySenses.inl separately
emitted UserLost on held/default state. Both paths could make a native client
stop rendering despite a usable weaving position. The modern DX12 predicted-eye
query also rejected anything except a fresh measured pair.

## Compatibility contract

- Keep measured broker fields and UI face status unchanged.
- While SnapshotForWeave is usable, fill missing SDK components from the
  calibrated optical center: symmetric 64 mm eyes, neutral Rodrigues rotation,
  symmetric 140 mm estimated ears. Preserve available measured components.
- Synthetic components have time=0; frameId continues increasing. Default data
  does not become a measured sensor observation.
- Continue stationary synthetic callbacks approximately every 16 ms.
- System events advertise a virtual viewer while fallback is usable. UserLost
  and device-unavailable events still occur on actual unusable device state.
- DX12 predicted-eye queries use the same builder and retain per-recording
  snapshot selection. They do not create a recording or double prediction.
- C adapters retain their earlier finite-transform protection even on actual
  disconnection; readiness events still report that disconnection.

No MCU calibration, provider filtering, +8 mm / 40 ms defaults, native preview
or game files are changed by this repair.

## Validation

AbiSmoke loads the actual built forwarding DLLs and exercises SDK objects.
It covers no-face startup, calibrated held/default coordinates, continuous
callbacks, measured/raw reacquisition, stale eyes/rotation, UserFound/UserLost,
true unavailability, DX12 predicted fallback and per-recording coherence.
The new regression failed against the previous runtime and passed after repair.
Release test details are included in the package verification directory.

Live acceptance remains: launch Stellar Blade through the updated bridge,
leave and re-enter the camera view, and confirm that output continues without
black frames while the UI accurately reports default/held/tracked status.
Restart the Hub and game so they load the updated runtime DLL.
