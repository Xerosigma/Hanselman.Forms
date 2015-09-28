using System;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Labs;
using Xamarin.Forms.Labs.Services;

namespace Hanselman.Portable.Helpers
{
	public partial class DateTimeControl : StackLayout
	{
		public DatePicker _date;
		public TimePicker _time;
		public IDisplay _display;

		public DateTimeControl()
		{
			IDevice device = Resolver.Resolve<IDevice>();

			_display = device.Display;
			this.Orientation = StackOrientation.Vertical;
			this.Padding = 2;
			_date = new DatePicker();
			_date.Format = CultureInfo.CurrentUICulture.DateTimeFormat.LongDatePattern;
			_date.WidthRequest = ((_display.Width / 2) / 3) * 2 - 50;
			_time = new TimePicker();
			_time.Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern;
			_time.WidthRequest = ((_display.Width / 2) / 3) - 10;
			this.Children.Add(_date);
			this.Children.Add(_time);
			_date.PropertyChanged += DateOnPropertyChanged;
			_time.PropertyChanged += TimeOnPropertyChanged;

			_date.Date = DateTime.Now.Date;
			_time.Time = DateTime.Now.TimeOfDay;
		}
		private void TimeOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName == "Time")
			{
				Value = _date.Date.Add(_time.Time);
			}
		}
		private void DateOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName == "Date")
			{
				Value = _date.Date.Add(_time.Time);
			}
		}

		#region Value
		public static readonly BindableProperty ValueProperty = 
			BindableProperty.Create<DateTimeControl, DateTime>(p => p.Value, 
				defaultValue: default(DateTime),
				defaultBindingMode: BindingMode.TwoWay,
				propertyChanging: (bindable, oldValue, newValue) => {
					ffffff(bindable, oldValue, newValue);
					//DateTimeControl ctrl = (DateTimeControl)bindable;
					//ctrl._date.Date = newValue;
				});

		public static void ffffff (BindableObject bindable, DateTime oldValue, DateTime newValue)
		{
			DateTimeControl ctrl = (DateTimeControl)bindable;
			ctrl._date.Date = newValue;
		}

		public DateTime Value
		{
			get
			{
				return (DateTime)GetValue(ValueProperty);
			}
			set
			{
				SetValue(ValueProperty, value);
			}
		}
		#endregion
	}
}

