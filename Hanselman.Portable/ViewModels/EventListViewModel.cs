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
	public class EventListViewModel : BaseViewModel
	{
        public ObservableCollection<Event> Events { get; set; }
        public ObservableCollection<Event> DismissedEvents { get; set; }
        public ObservableCollection<Event> TentativeEvents { get; set; }


		public EventListViewModel()
		{
			Title = "Events";
			Icon = "slideout.png";
            Events = new ObservableCollection<Event>();
            DismissedEvents = new ObservableCollection<Event>();
            TentativeEvents = new ObservableCollection<Event>();
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

		private Command searchCommand;
		public Command SearchCommand
		{
			get
			{
				return searchCommand ??
					(searchCommand = new Command((args) => { ExecuteSearchCommand(args); }, (args) =>
						{
							return !IsBusy;
						}));
			}
		}
		public async Task ExecuteSearchCommand(object obj)
		{
			if (IsBusy) { return; }

			IsBusy = true;
			SearchCommand.ChangeCanExecute();
			try
			{
				string searchText = obj as string;
				Renderer.ShowSnack(string.Format("Searching for {0}", searchText), null);

				// TODO: Perform search. HAL Request to backend.
			}
			catch (Exception ex)
			{
				new ContentPage().DisplayAlert("Error", "Unable to search.", "OK");
			}

			IsBusy = false;
			SearchCommand.ChangeCanExecute();
		}

        private Command dismissCommand;
        public Command DismissCommand
		{
			get
			{
                return dismissCommand ??
                    (dismissCommand = new Command((args) => { ExecuteDismissCommand(args); }, (args) =>
						{
							return !IsBusy;
						}));
			}
		}
        public async Task ExecuteDismissCommand(object obj)
		{
			if (IsBusy) { return; }

			IsBusy = true;
            DismissCommand.ChangeCanExecute();
			try
			{
                Event eventObj = obj as Event;

                Events.Remove(eventObj);
                DismissedEvents.Add(eventObj);

                Renderer.ShowSnack(string.Format("Dismissed {0}", eventObj.Name),
                    new Command(() => {
                    DismissedEvents.Remove(eventObj);
                    Events.Add(eventObj);
                    Renderer.ShowSnack("Action reverted.", null);
                }));
			}
			catch (Exception ex)
			{
                new ContentPage().DisplayAlert("Error", "Unable to dismiss event.", "OK");
			}

			IsBusy = false;
            DismissCommand.ChangeCanExecute();
        }

        private Command tentativeCommand;
        public Command TentativeCommand
        {
            get
            {
                return tentativeCommand ??
                    (tentativeCommand = new Command((args) => { ExecuteTentativeCommand(args); }, (args) =>
                        {
                            return !IsBusy;
                        }));
            }
        }
		public async Task ExecuteTentativeCommand(object obj)
        {
            if (IsBusy) { return; }

            IsBusy = true;
            TentativeCommand.ChangeCanExecute();
            try
			{
				Event eventObj = obj as Event;

				Events.Remove(eventObj);
				TentativeEvents.Add(eventObj);

				Renderer.ShowSnack(string.Format("Maybe {0}", eventObj.Name),
					new Command(() => {
						TentativeEvents.Remove(eventObj);
						Events.Add(eventObj);
						Renderer.ShowSnack("Action reverted.", null);
					}));
            }
            catch (Exception ex)
            {
                new ContentPage().DisplayAlert("Error", "Unable to set event.", "OK");
            }

            IsBusy = false;
            TentativeCommand.ChangeCanExecute();
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

