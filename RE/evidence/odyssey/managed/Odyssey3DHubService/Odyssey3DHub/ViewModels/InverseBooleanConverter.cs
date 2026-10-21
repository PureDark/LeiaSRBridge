using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace Odyssey3DHub.ViewModels
{
	// Token: 0x02000020 RID: 32
	public class InverseBooleanConverter : IValueConverter
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00004DF8 File Offset: 0x00002FF8
		[NullableContext(1)]
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag2;
			if (value is bool)
			{
				bool flag = (bool)value;
				flag2 = !flag;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004E20 File Offset: 0x00003020
		[NullableContext(1)]
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag2;
			if (value is bool)
			{
				bool flag = (bool)value;
				flag2 = !flag;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}
	}
}
