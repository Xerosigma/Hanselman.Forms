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
		public NavigationController NavigationController { get; private set;}

		private EventListViewModel ViewModel
		{
			get { return BindingContext as EventListViewModel; }
		}

		public EventListPage (NavigationController navigationController)
		{
			InitializeComponent ();

			NavigationController = navigationController;

			BindingContext = new EventListViewModel();

            // TODO: Move to a base class?
            ViewModel.Renderer = this;

            //await DisplayAlert("Filter","","");
			listView.ItemTapped += (sender, args) =>
			{
				if(listView.SelectedItem == null) { return; }
                Event eevent = args.Item as Event;
				//Navigation.PushAsync(new EventDetailsPage(eevent));
				Navigation.PushModalAsync(new EventDetailsPage(eevent));
			};

			searchEntry.Completed += (sender, e) => 
			{
				string searchText = searchEntry.Text.Trim();
				if(String.IsNullOrEmpty(searchText)) {
					ShowSnack(string.Format("Search text is empty."), null);
					searchEntry.Text = "";
					return;
				}
				searchButton.Command.Execute(searchText);
			};
		}

        public void ShowSnack(string message, Command command)
        {
            SnackbarView.ShowSnack(Content, message, command);
        }

		public void SetViewData(string viewName, Object data)
		{
			throw new NotImplementedException ();
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

