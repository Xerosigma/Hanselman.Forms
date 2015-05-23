using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Hanselman.Portable.Helpers;

namespace Hanselman.Portable.Views
{
	public partial class EventDetailsPage : ContentPage, Renderer
	{
		private EventDetailsViewModel ViewModel
		{
			get { return BindingContext as EventDetailsViewModel; }
		}

		public EventDetailsPage (Event eevent)
		{
			InitializeComponent ();

			BindingContext = new EventDetailsViewModel(eevent);

			// TODO: Move to a base class?
			ViewModel.Renderer = this;

			listView.ItemTapped += (sender, args) =>
			{
				if(listView.SelectedItem == null) { return; }
				Person person = args.Item as Person;
				ShowSnack("TODO: Attendee Details.", null);
				//this.Navigation.PushAsync(new EventDetailsPage(eevent));
			};
		}

		public void ShowSnack(string message, Command command)
		{
			SnackbarView.ShowSnack(Content, message, command);
		}


		protected override void OnAppearing()
		{
			base.OnAppearing();

			if(ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.Attendees.Count > 0)
			{
				return;
			}

			ViewModel.LoadEventsCommand.Execute(null);
		}
	}
}

