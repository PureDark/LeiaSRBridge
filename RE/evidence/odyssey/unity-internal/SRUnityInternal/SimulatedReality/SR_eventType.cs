using System;

namespace SimulatedReality
{
	// Token: 0x02000002 RID: 2
	public enum SR_eventType
	{
		// Token: 0x04000002 RID: 2
		Info,
		// Token: 0x04000003 RID: 3
		ContextInvalid,
		// Token: 0x04000004 RID: 4
		SRUnavailable,
		// Token: 0x04000005 RID: 5
		SRRestored,
		// Token: 0x04000006 RID: 6
		USBNotConnected,
		// Token: 0x04000007 RID: 7
		USBNotConnectedResolved,
		// Token: 0x04000008 RID: 8
		DisplayNotConnected,
		// Token: 0x04000009 RID: 9
		DisplayNotConnectedResolved,
		// Token: 0x0400000A RID: 10
		Duplicated,
		// Token: 0x0400000B RID: 11
		DuplicatedResolved,
		// Token: 0x0400000C RID: 12
		NonNativeResolution,
		// Token: 0x0400000D RID: 13
		NonNativeResolutionResolved,
		// Token: 0x0400000E RID: 14
		DeviceConnectedAndReady,
		// Token: 0x0400000F RID: 15
		DeviceDisconnected
	}
}
