using System;
using System.Globalization;
using System.IO;

using Xamarin.Forms;

namespace Hanselman.Portable.Helpers
{
	public class ImageSourceToByteArrayConverter : IValueConverter
	{
		public ImageSourceToByteArrayConverter ()
		{
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ImageSource retSource = null;
			if (value != null)
			{
				byte[] imageAsBytes = (byte[]) value;
				retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
			}

			return retSource;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

