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
				DisplayAlert("Hooray", "You selected an item.", "Cool!");

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
		}
	}
}

