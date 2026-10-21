param([Parameter(Mandatory=$true)][int]$GamePid)
$ErrorActionPreference='Stop'
Add-Type @'
using System;
using System.Runtime.InteropServices;
public static class OdysseyDeviceNotice {
 [DllImport("kernel32.dll", SetLastError=true)] public static extern IntPtr OpenThread(uint access,bool inherit,uint tid);
 [DllImport("kernel32.dll")] public static extern uint GetProcessIdOfThread(IntPtr thread);
 [DllImport("kernel32.dll")] public static extern bool CloseHandle(IntPtr h);
 [DllImport("user32.dll",SetLastError=true)] public static extern bool PostThreadMessageW(uint thread,uint message,UIntPtr magic,IntPtr value);
}
'@
$map=[IO.MemoryMappedFiles.MemoryMappedFile]::OpenExisting('Odyssey3DPlugin',[IO.MemoryMappedFiles.MemoryMappedFileRights]::Read)
try {
 $view=$map.CreateViewAccessor(0,533,[IO.MemoryMappedFiles.MemoryMappedFileAccess]::Read)
 try { $receiver=$view.ReadUInt32(528) } finally { $view.Dispose() }
 $thread=[OdysseyDeviceNotice]::OpenThread(0x800,$false,$receiver)
 if($thread -eq [IntPtr]::Zero){throw 'Cannot validate receiver thread'}
 try { if([OdysseyDeviceNotice]::GetProcessIdOfThread($thread) -ne $GamePid){throw 'Receiver belongs to a different process'} } finally { [void][OdysseyDeviceNotice]::CloseHandle($thread) }
 if(-not [OdysseyDeviceNotice]::PostThreadMessageW($receiver,0x400,[UIntPtr]::new(0x5044334F),[IntPtr]::new(2))){throw 'PostThreadMessage failed'}
 "Sent one Odyssey device-ready notification to verified PID=$GamePid thread=$receiver"
} finally {$map.Dispose()}
