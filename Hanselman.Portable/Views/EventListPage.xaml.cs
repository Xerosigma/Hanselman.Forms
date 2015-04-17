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
                Event eevent = args as Event;
                ShowSnack(eevent.Name, null);

				// TODO: Render event details page.
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
		}

        private async Task ShowSnack(string message, Action<object> action)
		{
            ContentView snackbarView = Content.FindByName<ContentView>("snackbarView");
            StackLayout sl = snackbarView.Content.FindByName<StackLayout>("snackbarLayout");
			Label messageLabel = sl.FindByName<Label>("snackbarText");
			Label actionLabel = sl.FindByName<Label>("snackbarActionText");

            messageLabel.Text = message;

            snackbarView.Opacity = 0;
            snackbarView.IsVisible = true;
            await snackbarView.FadeTo(1,375);

			await Task.Delay(3500);
            await snackbarView.FadeTo(0,125);

            snackbarView.IsVisible = false;
		}
	}
}

