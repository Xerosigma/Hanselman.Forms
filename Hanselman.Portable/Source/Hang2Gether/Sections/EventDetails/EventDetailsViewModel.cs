using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;
using LinqToTwitter;

using Core;
using Hanselman.Portable.Helpers;

namespace Hanselman.Portable
{
	public class EventDetailsViewModel : BaseViewModel
	{
		public Event Event { get; set; }
		public ObservableCollection<Attendee> Attendees { get; set; }

		public EventDetailsViewModel (Event eevent)
		{
			Title = "Details";
			Icon = "slideout.png";
			Event = eevent;
			Attendees = new ObservableCollection<Attendee>();

			if (null != Event.Attendees) {
				Attendees.AddRange (Event.Attendees);
			} else {
				List<Attendee> atten = new List<Attendee> ();

				atten.Add(new Attendee(){
					AccountId = "0",
					Name = "---"
				});

				atten.Add(new Attendee(){
					AccountId = "2",
					Name = "dwwwwdwdwdwdwd"
				});

				Attendees.AddRange (atten);
			}
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

				// TODO: Backend request: Update Person.
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
				// TODO: Backend Call: Request Event again (full refresh), refresh all Attendies and such.
				Renderer.ShowSnack("Refreshing event...", null);
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				page.DisplayAlert("Error", "Unable to load attendees.", "OK");
			}

			IsBusy = false;
			LoadEventsCommand.ChangeCanExecute();
		}


		/// <summary>
		/// The load event image command.
		/// </summary>
		private Command loadEventImageCommand;
		public Command LoadEventImageCommand
		{
			get
			{
				return loadEventImageCommand ??
					(loadEventImageCommand = new Command(() => { ExecuteLoadEventImageCommand(); }, () =>
						{
							return !IsBusy;
						}));
			}
		}

		public async Task ExecuteLoadEventImageCommand()
		{
			if (IsBusy) return;

			IsBusy = true;
			LoadEventImageCommand.ChangeCanExecute();
			try
			{
				Renderer.ShowSnack("Loading event image...", null);
				new Task(GetEventImage).Start();
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				page.DisplayAlert("Error", "Unable to load event image.", "OK");
			}

			IsBusy = false;
			LoadEventImageCommand.ChangeCanExecute();
		}

		protected async void GetEventImage ()
		{
			EventRepository.EventImageRepository eventRepo = new EventRepository.EventImageRepository();

			KeyValuePair<String, String> queryParams = new KeyValuePair<string, string> ("id", Event.ImageId);

			RequestResponse<byte[]> resp = await eventRepo.Get(queryParams);

			ImageSource imageSource = ImageSource.FromStream (() => new MemoryStream(resp.content));

			Device.BeginInvokeOnMainThread ( () => {
				Renderer.SetViewData ("EventImage", imageSource);
			});
		}
	}
}

