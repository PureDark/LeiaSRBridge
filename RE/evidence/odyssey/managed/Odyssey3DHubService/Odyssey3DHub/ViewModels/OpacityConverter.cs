using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace Odyssey3DHub.ViewModels
{
	// Token: 0x0200001F RID: 31
	public class OpacityConverter : IValueConverter
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00004DA8 File Offset: 0x00002FA8
		[NullableContext(1)]
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool)
			{
				return ((bool)value) ? 1.0 : 0.5;
			}
			return 1.0;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004DEF File Offset: 0x00002FEF
		[NullableContext(1)]
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
