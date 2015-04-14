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
					(filterCommand = new Command(() => { ExecuteFilterCommand(); }, () =>
						{
							return !IsBusy;
						}));
			}
		}
		public async Task ExecuteFilterCommand()
		{
			if (IsBusy) { return; }

			IsBusy = true;
			FilterCommand.ChangeCanExecute();
			try
			{
				var page = new ContentPage();
				page.DisplayAlert("Filter", "You're filtering.", "Awesome");
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
		public void Test() { new ContentPage().DisplayAlert("????", "???", "???"); }
		private Command maybeCommand;
		public Command MaybeCommand
		{
			get
			{
				return maybeCommand ??
					(maybeCommand = new Command((args) => { ExecuteMaybeCommand(args); }, (args) =>
						{
							return !IsBusy;
						}));
			}
		}
		public async Task ExecuteMaybeCommand(object obj)
		{
			if (IsBusy) { return; }

			IsBusy = true;
			MaybeCommand.ChangeCanExecute();
			try
			{
				var page = new ContentPage();
				page.DisplayAlert("Click", "Moving to maybe???", "Maybe");
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				page.DisplayAlert("Error", "Unable to execute onClick()", "OK");
			}

			IsBusy = false;
			MaybeCommand.ChangeCanExecute();
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

