using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hanselman.Portable
{
	public class HomeViewModel : BaseViewModel
	{
		public ObservableCollection<HomeMenuItem> MenuItems { get; set; }

		public HomeViewModel()
		{
			CanLoadMore = true;
			Title = "Hanselman";

			MenuItems = new ObservableCollection<HomeMenuItem>();

			MenuItems.Add(new HomeMenuItem
			{
				Id = 0,
				Title = "Events",
				MenuType = MenuType.Events,
				Icon = "about.png"
			});

			MenuItems.Add(new HomeMenuItem
			{
				Id = 1,
				Title = "Creat Event",
				MenuType = MenuType.CreateEvent,
				Icon = "blog.png"
			});
		}
	}
}