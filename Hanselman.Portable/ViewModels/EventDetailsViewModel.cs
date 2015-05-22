using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using LinqToTwitter;
using System.Threading.Tasks;
using System.Linq;
using Hanselman.Portable.Helpers;

namespace Hanselman.Portable
{
	public class EventDetailsViewModel : BaseViewModel
	{
		public Event Event { get; set; }
		public ObservableCollection<Person> Attendees { get; set; }

		public EventDetailsViewModel (Event eevent)
		{
			Title = "Details";
			Icon = "slideout.png";
			Attendees = new ObservableCollection<Person>();
			Event = eevent;
		}

		private Command attendingCommand;
		public Command AttendingCommand
		{
			get
			{
				return attendingCommand ??
					(attendingCommand = new Command(() => { ExecuteAttendingCommand(); }, () =>
						{
							return !IsBusy;
						}));
			}
		}
		public async Task ExecuteAttendingCommand()
		{
			if (IsBusy) { return; }

			IsBusy = true;
			AttendingCommand.ChangeCanExecute();
			try
			{
				Renderer.ShowSnack(string.Format("Going to {0}", "event"), null);
			}
			catch (Exception ex)
			{
				new ContentPage().DisplayAlert("Error", "Unable to dismiss event.", "OK");
			}

			IsBusy = false;
			AttendingCommand.ChangeCanExecute();
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
				var attendees = new List<Person>();
				attendees.Add(new Person{ Name = "Jon Snow" });
				attendees.Add(new Person{ Name = "Walter White" });
				attendees.Add(new Person{ Name = "Harry Dresden" });
				attendees.Add(new Person{ Name = "Tony Stark" });
				attendees.Add(new Person{ Name = "Nestor Ledon" });

				foreach (var e in attendees)
				{
					Attendees.Add(e);
				}
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				page.DisplayAlert("Error", "Unable to load attendees.", "OK");
			}

			IsBusy = false;
			LoadEventsCommand.ChangeCanExecute();
		}
	}
}

