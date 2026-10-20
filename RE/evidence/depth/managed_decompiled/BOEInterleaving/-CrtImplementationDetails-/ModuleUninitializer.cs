using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace <CrtImplementationDetails>
{
	// Token: 0x020001FD RID: 509
	internal class ModuleUninitializer : Stack
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000035D0 File Offset: 0x000029D0
		[SecuritySafeCritical]
		internal void AddHandler(EventHandler handler)
		{
			bool mustReleaseLock = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				Monitor.Enter(ModuleUninitializer.@lock, ref mustReleaseLock);
				RuntimeHelpers.PrepareDelegate(handler);
				this.Push(handler);
			}
			finally
			{
				if (mustReleaseLock)
				{
					Monitor.Exit(ModuleUninitializer.@lock);
				}
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003C0C File Offset: 0x0000300C
		[SecuritySafeCritical]
		private ModuleUninitializer()
		{
			EventHandler singletonHandler = new EventHandler(this.SingletonDomainUnload);
			AppDomain.CurrentDomain.DomainUnload += singletonHandler;
			AppDomain.CurrentDomain.ProcessExit += singletonHandler;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003630 File Offset: 0x00002A30
		[PrePrepareMethod]
		[SecurityCritical]
		private void SingletonDomainUnload(object source, EventArgs arguments)
		{
			bool mustReleaseLock = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				Monitor.Enter(ModuleUninitializer.@lock, ref mustReleaseLock);
				using (IEnumerator enumerator = this.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						((EventHandler)enumerator.Current)(source, arguments);
					}
				}
			}
			finally
			{
				if (mustReleaseLock)
				{
					Monitor.Exit(ModuleUninitializer.@lock);
				}
			}
		}

		// Token: 0x0400028E RID: 654
		private static object @lock = new object();

		// Token: 0x0400028F RID: 655
		internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();
	}
}
