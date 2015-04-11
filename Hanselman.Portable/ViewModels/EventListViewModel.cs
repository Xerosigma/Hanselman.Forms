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

		public EventListViewModel()
		{
			Title = "Events";
			Icon = "slideout.png";
			Events = new ObservableCollection<Event>();

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

