using System;
using System.Globalization;

using Xamarin.Forms;

namespace Hanselman.Portable.Helpers
{
	public class DateTimeToStringConverter : IValueConverter
	{
		public DateTimeToStringConverter ()
		{
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			String retSource = null;
			if (value != null)
			{
				DateTime dateTime = (DateTime) value;
				retSource = dateTime.ToString();
			}

			return retSource;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			// TODO: Why the HELL the converter sending DateTimes to both methods!?
			return Convert (value, targetType, parameter, culture);
		}
	}
}

