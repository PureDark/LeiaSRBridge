using System;
using System.Runtime.InteropServices;
using Windows.System;

namespace AstraDepth3D.Composition.WindowsRuntimeHelpers
{
	// Token: 0x0200001B RID: 27
	public static class CoreMessagingHelper
	{
		// Token: 0x06000153 RID: 339
		[DllImport("CoreMessaging.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern uint CreateDispatcherQueueController(CoreMessagingHelper.DispatcherQueueOptions options, out IntPtr dispatcherQueueController);

		// Token: 0x06000154 RID: 340 RVA: 0x0000AF14 File Offset: 0x00009114
		public static DispatcherQueueController CreateDispatcherQueueControllerForCurrentThread()
		{
			CoreMessagingHelper.DispatcherQueueOptions options = new CoreMessagingHelper.DispatcherQueueOptions
			{
				dwSize = Marshal.SizeOf<CoreMessagingHelper.DispatcherQueueOptions>(),
				threadType = CoreMessagingHelper.DISPATCHERQUEUE_THREAD_TYPE.DQTYPE_THREAD_CURRENT,
				apartmentType = CoreMessagingHelper.DISPATCHERQUEUE_THREAD_APARTMENTTYPE.DQTAT_COM_NONE
			};
			DispatcherQueueController controller = null;
			IntPtr controllerPointer;
			if (CoreMessagingHelper.CreateDispatcherQueueController(options, out controllerPointer) == 0U)
			{
				controller = (Marshal.GetObjectForIUnknown(controllerPointer) as DispatcherQueueController);
				Marshal.Release(controllerPointer);
			}
			return controller;
		}

		// Token: 0x0200008C RID: 140
		private enum DISPATCHERQUEUE_THREAD_APARTMENTTYPE
		{
			// Token: 0x040003E5 RID: 997
			DQTAT_COM_NONE,
			// Token: 0x040003E6 RID: 998
			DQTAT_COM_ASTA,
			// Token: 0x040003E7 RID: 999
			DQTAT_COM_STA
		}

		// Token: 0x0200008D RID: 141
		private enum DISPATCHERQUEUE_THREAD_TYPE
		{
			// Token: 0x040003E9 RID: 1001
			DQTYPE_THREAD_DEDICATED = 1,
			// Token: 0x040003EA RID: 1002
			DQTYPE_THREAD_CURRENT
		}

		// Token: 0x0200008E RID: 142
		private struct DispatcherQueueOptions
		{
			// Token: 0x040003EB RID: 1003
			public int dwSize;

			// Token: 0x040003EC RID: 1004
			public CoreMessagingHelper.DISPATCHERQUEUE_THREAD_TYPE threadType;

			// Token: 0x040003ED RID: 1005
			public CoreMessagingHelper.DISPATCHERQUEUE_THREAD_APARTMENTTYPE apartmentType;
		}
	}
}
