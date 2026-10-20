using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace <CrtImplementationDetails>
{
	// Token: 0x020003CE RID: 974
	internal class ModuleUninitializer : Stack
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00003F80 File Offset: 0x00003380
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

		// Token: 0x060000F7 RID: 247 RVA: 0x000045BC File Offset: 0x000039BC
		[SecuritySafeCritical]
		private ModuleUninitializer()
		{
			EventHandler singletonHandler = new EventHandler(this.SingletonDomainUnload);
			AppDomain.CurrentDomain.DomainUnload += singletonHandler;
			AppDomain.CurrentDomain.ProcessExit += singletonHandler;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003FE0 File Offset: 0x000033E0
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

		// Token: 0x04000505 RID: 1285
		private static object @lock = new object();

		// Token: 0x04000506 RID: 1286
		internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();
	}
}
