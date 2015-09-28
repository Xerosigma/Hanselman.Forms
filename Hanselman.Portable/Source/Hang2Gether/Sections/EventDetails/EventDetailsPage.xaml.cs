using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using Core;
using Hanselman.Portable.Helpers;

namespace Hanselman.Portable.Views
{
	public partial class EventDetailsPage : ContentPage, Renderer
	{
		private EventDetailsViewModel ViewModel
		{
			get { return BindingContext as EventDetailsViewModel; }
		}

		public Event Event { get; set; }
		public EventDetailsPage (Event eevent)
		{
			InitializeComponent ();

			if (eevent.ImageId == null) {
				eevent.ImageId = "0";
			}

			BindingContext = new EventDetailsViewModel(eevent);
			Event = eevent;

			// TODO: Move to a base class?
			ViewModel.Renderer = this;

			listView.ItemTapped += (sender, args) =>
			{
				if(listView.SelectedItem == null) { return; }
				Attendee person = args.Item as Attendee;
				ShowSnack("TODO: Attendee Details.", null);
				//this.Navigation.PushAsync(new EventDetailsPage(eevent));
			};

			((EventDetailsViewModel)BindingContext).LoadEventImageCommand.Execute (null);
		}

		public void ShowSnack(string message, Command command)
		{
			SnackbarView.ShowSnack(Content, message, command);
		}
			
		public void SetViewData(string viewName, Object data)
		{
			if (viewName.Equals ("EventImage")) {
				EventImage.Source = (ImageSource)data;
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if(ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy)
			{
				return;
			}

			//ViewModel.LoadEventsCommand.Execute(null);
		}
	}
}

