using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Odyssey3DHub
{
	// Token: 0x02000006 RID: 6
	public static class CommandExtensions
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002386 File Offset: 0x00000586
		[NullableContext(1)]
		public static void Execute(this ICommand command)
		{
			command.Execute(null);
		}
	}
}
