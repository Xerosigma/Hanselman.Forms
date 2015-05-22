using System;

using Xamarin.Forms;

namespace Hanselman.Portable
{
	public static class Extensions
	{
		public static T AbsoluteLayoutBottomRight<T>(this T view) where T : View
		{
			AbsoluteLayout.SetLayoutFlags(view, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(view, new Rectangle(1.0, 1.0, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			return view;
		}
	}
}

