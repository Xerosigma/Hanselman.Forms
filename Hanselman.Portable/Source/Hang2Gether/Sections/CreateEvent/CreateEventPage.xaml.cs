using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Hanselman.Portable.Helpers;

namespace Hanselman.Portable.Views
{
	public partial class CreateEventPage : ContentPage, Renderer
	{
		public NavigationController NavigationController { get; private set; }

		private CreateEventViewModel ViewModel
		{
			get { return BindingContext as CreateEventViewModel; }
		}

		public CreateEventPage (NavigationController navigationController)
		{
			InitializeComponent ();

			NavigationController = navigationController;

			BindingContext = new CreateEventViewModel(NavigationController);

			ViewModel.Renderer = this;

			Map map = new Map { 
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			map.MoveToRegion (new MapSpan (new Position (0,0), 360, 360) );
			//contentLayout.Children.Add (map); // FIXME:
		}

		public void ShowSnack(string message, Command command)
		{
			SnackbarView.ShowSnack(Content, message, command);
		}

		public void SetViewData(string viewName, Object data)
		{
			if(viewName == "eventImage") {
				eventImage.Source = ImageSource.FromStream(() => new MemoryStream((byte[])data));
			}
		}
			
		protected override void OnAppearing()
		{
			base.OnAppearing();

			if(ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy)
			{
				return;
			}

			// TODO: Clear fields. Pages appear to be reused.

			ShowSnack ("Page loaded.", null);
		}
	}
}

