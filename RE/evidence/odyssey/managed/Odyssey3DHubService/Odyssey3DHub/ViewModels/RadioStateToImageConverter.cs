using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using <PrivateImplementationDetails>{3775CED9-2FDA-4847-B909-D584B8E8020E};

namespace Odyssey3DHub.ViewModels
{
	// Token: 0x0200001E RID: 30
	public class RadioStateToImageConverter : IMultiValueConverter
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00004D30 File Offset: 0x00002F30
		[NullableContext(1)]
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = values[0] is bool && (bool)values[0];
			string text;
			if (!(values[1] is bool) || !(bool)values[1])
			{
				text = (flag ? 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2028() : 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2027());
			}
			else
			{
				text = (flag ? 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u202A() : 0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u2029());
			}
			return new BitmapImage(new Uri(0057D359-365F-4587-A5BB-CA7237C6FD6F.\u1680\u202B() + text));
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004DA1 File Offset: 0x00002FA1
		[NullableContext(1)]
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
