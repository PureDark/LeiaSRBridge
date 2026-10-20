using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AstraDepth3D.Properties
{
	// Token: 0x02000017 RID: 23
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "18.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	public class Resources
	{
		// Token: 0x06000125 RID: 293 RVA: 0x0000AB71 File Offset: 0x00008D71
		internal Resources()
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000AB79 File Offset: 0x00008D79
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("AstraDepth3D.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000ABA5 File Offset: 0x00008DA5
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000ABAC File Offset: 0x00008DAC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x0400008C RID: 140
		private static ResourceManager resourceMan;

		// Token: 0x0400008D RID: 141
		private static CultureInfo resourceCulture;
	}
}
