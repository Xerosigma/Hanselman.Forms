using System;

using Xamarin.Forms;

namespace Hanselman.Portable
{
	public interface NavigationController
	{
		void Navigate (Page page, MenuType type);
	}
}

