using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Hanselman.Portable.Helpers;

namespace Hanselman.Portable.Views
{
    public partial class EventListPage : ContentPage, Renderer
	{
		private EventListViewModel ViewModel
		{
			get { return BindingContext as EventListViewModel; }
		}

		public EventListPage ()
		{
			InitializeComponent ();

			BindingContext = new EventListViewModel();

            // TODO: Move to a base class?
            ViewModel.Renderer = this;

            //await DisplayAlert("Filter","","");
			listView.ItemTapped += (sender, args) =>
			{
				if(listView.SelectedItem == null) { return; }
                Event eevent = args.Item as Event;
				this.Navigation.PushAsync(new EventDetailsPage(eevent));
			};
		}

        public void ShowSnack(string message, Command command)
        {
            SnackbarView.ShowSnack(Content, message, command);
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
	}
}

