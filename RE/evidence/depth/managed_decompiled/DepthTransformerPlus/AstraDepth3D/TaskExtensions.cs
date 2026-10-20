using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AstraDepth3D.WindowHelper;

namespace AstraDepth3D
{
	// Token: 0x02000011 RID: 17
	public static class TaskExtensions
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00005595 File Offset: 0x00003795
		public static void LogExceptions(this Task task)
		{
			task.ContinueWith(delegate(Task t)
			{
				if (t.Exception != null)
				{
					foreach (Exception exception in t.Exception.Flatten().InnerExceptions)
					{
						NLogService.WriteLog("异常：" + exception.Message);
					}
				}
			}, TaskContinuationOptions.OnlyOnFaulted);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000055C4 File Offset: 0x000037C4
		public static Task<T> RunWithTimeout<T>(Func<Task<T>> action, TimeSpan timeout)
		{
			TaskExtensions.<RunWithTimeout>d__1<T> <RunWithTimeout>d__;
			<RunWithTimeout>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<RunWithTimeout>d__.action = action;
			<RunWithTimeout>d__.timeout = timeout;
			<RunWithTimeout>d__.<>1__state = -1;
			<RunWithTimeout>d__.<>t__builder.Start<TaskExtensions.<RunWithTimeout>d__1<T>>(ref <RunWithTimeout>d__);
			return <RunWithTimeout>d__.<>t__builder.Task;
		}
	}
}
