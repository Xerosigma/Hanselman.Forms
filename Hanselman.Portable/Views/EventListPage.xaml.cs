using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hanselman.Portable.Views
{
	public partial class EventListPage : ContentPage
	{
		private EventListViewModel ViewModel
		{
			get { return BindingContext as EventListViewModel; }
		}

		public EventListPage ()
		{
			InitializeComponent ();

			BindingContext = new EventListViewModel();

			listView.ItemTapped += (sender, args) =>
			{
				if(listView.SelectedItem == null) { return;}
				//DisplayAlert("Hooray", "You selected an item.", "Cool!");
				ShowSnack("Hooray", null);

				// TODO: Render details page.
			};
		}


		protected override void OnAppearing()
		{
			base.OnAppearing();

			if(ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.Events.Count > 0)
			{
				return;
			}

			ViewModel.LoadEventsCommand.Execute(null);

			StackLayout sl = Content.FindByName<StackLayout>("snackbarLayout");
			sl.IsVisible = false;
		}

		private async Task ShowSnack(string message, Delegate action)
		{
			StackLayout sl = Content.FindByName<StackLayout>("snackbarLayout");
			Label text = sl.FindByName<Label>("snackbarText");
			Label actionText = sl.FindByName<Label>("snackbarActionText");

			text.Text = message;

			sl.Opacity = 0;
			sl.IsVisible = true;
			sl.FadeTo(1,500);

			await Task.Delay(3500);
			await sl.FadeTo(0,125);
			sl.IsVisible = false;
		}
	}
}

