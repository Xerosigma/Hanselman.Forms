using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using LinqToTwitter;
using System.Threading.Tasks;
using System.Linq;

namespace Hanselman.Portable
{
	public class EventListViewModel : BaseViewModel
	{

		public ObservableCollection<Event> Events { get; set; }

		public object SelectedItem { get; set; }


		public EventListViewModel()
		{
			Title = "Events";
			Icon = "slideout.png";
			Events = new ObservableCollection<Event>();
		}

		// TODO: Move to partial "FilterCommand"
		#region FilterCommand
		private Command filterCommand;
		public Command FilterCommand
		{
			get
			{
				return filterCommand ?? 
					(filterCommand = new Command((args) => { ExecuteFilterCommand(args); }, (args) => 
						{ 
							return !IsBusy; 
						}));
			}
		}
		public async Task ExecuteFilterCommand(object filterObj)
		{
			if (IsBusy) { return; }

			IsBusy = true;
			FilterCommand.ChangeCanExecute();
			try
			{
				string filterTag = ((Label) filterObj).Text;
				var page = new ContentPage();
				page.DisplayAlert("Filter", string.Format("You're filtering on {0}", filterTag), "Awesome");
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				page.DisplayAlert("Error", "Unable to filter.", "OK");
			}

			IsBusy = false;
			FilterCommand.ChangeCanExecute();
		}
		#endregion

		private Command itemActionCommand;
		public Command ItemActionCommand
		{
			get
			{
				return itemActionCommand ??
					(itemActionCommand = new Command((args) => { ExecuteItemActionCommand(args as Event); }, (args) =>
						{
							return !IsBusy;
						}));
			}
		}
		public async Task ExecuteItemActionCommand(Event eventObj)
		{
			if (IsBusy) { return; }

			IsBusy = true;
			ItemActionCommand.ChangeCanExecute();
			try
			{
				//string actionTag = ((Label) action).Text;
				var page = new ContentPage();
				page.DisplayAlert(eventObj.Name, string.Format("{0}", eventObj.Name), eventObj.Name);
			}
			catch (Exception ex)
			{
				new ContentPage().DisplayAlert("Error", "Unable to execute onClick()", "OK");
			}

			IsBusy = false;
			ItemActionCommand.ChangeCanExecute();
		}


		private Command loadEventsCommand;

		public Command LoadEventsCommand
		{
			get
			{
				return loadEventsCommand ??
					(loadEventsCommand = new Command(() => { ExecuteLoadEventsCommand(); }, () =>
						{
							return !IsBusy;
						}));
			}
		}

		public async Task ExecuteLoadEventsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;
			LoadEventsCommand.ChangeCanExecute();
			try
			{
				var events = new List<Event>();
				events.Add(new Event{ Name = "Super Awesome Party", Location = "@Divebar", FriendsGoing = 13 });
				events.Add(new Event{ Name = "D&D", Location = "@Nestor's Place", FriendsGoing = 3 });
				events.Add(new Event{ Name = "Pool Tournament", Location = "@BilliardsNStuff", FriendsGoing = 7 });

				foreach (var e in events)
				{
					Events.Add(e);
				}
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				page.DisplayAlert("Error", "Unable to load tweets.", "OK");
			}

			IsBusy = false;
			LoadEventsCommand.ChangeCanExecute();
		}
	}
}

