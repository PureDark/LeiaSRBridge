Add-Type @"
using System;using System.Collections.Generic;using System.Runtime.InteropServices;
public static class NativeCpuSample {
 [DllImport("ntdll.dll")] static extern int NtQuerySystemInformation(int cls,IntPtr data,int size,out int needed);
 public class Row { public int Id; public string Name; public long Ticks; }
 public static Row[] Read(){
  int size=1024*1024;
  for(int attempt=0;attempt<5;attempt++){
   IntPtr data=Marshal.AllocHGlobal(size);
   try { int needed; int status=NtQuerySystemInformation(5,data,size,out needed);
    if(status==unchecked((int)0xc0000004)){size=Math.Max(size*2,needed+65536);continue;}
    if(status<0)throw new Exception("NT status "+status.ToString("X"));
    var rows=new List<Row>();int offset=0;
    while(offset+96<=size){IntPtr p=IntPtr.Add(data,offset);int next=Marshal.ReadInt32(p);int length=(ushort)Marshal.ReadInt16(p,56);IntPtr name=Marshal.ReadIntPtr(p,64);
     rows.Add(new Row{Id=(int)Marshal.ReadIntPtr(p,80).ToInt64(),Name=name==IntPtr.Zero?"Idle":Marshal.PtrToStringUni(name,length/2),Ticks=Marshal.ReadInt64(p,40)+Marshal.ReadInt64(p,48)});
     if(next==0)break;if(next<96 || next>size-offset)throw new Exception("Invalid offset");offset+=next;
    } return rows.ToArray();
   } finally {Marshal.FreeHGlobal(data);}
  } throw new Exception("Buffer growth limit");
 }
}
"@
$before=@{};[NativeCpuSample]::Read() | ForEach-Object {$before[$_.Id]=$_.Ticks}
$watch=[Diagnostics.Stopwatch]::StartNew();Start-Sleep -Seconds 5
$after=[NativeCpuSample]::Read();$seconds=$watch.Elapsed.TotalSeconds
$after | Where-Object {$_.Id -ne 0 -and $before.ContainsKey($_.Id)} | ForEach-Object {
 [pscustomobject]@{Id=$_.Id;Name=$_.Name;CPUPercent=[math]::Round(100*($_.Ticks-$before[$_.Id])/10000000/$seconds/[Environment]::ProcessorCount,1)}
} | Sort-Object CPUPercent -Descending | Select-Object -First 15 | Format-Table -AutoSize
