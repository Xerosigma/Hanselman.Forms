using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LinqToTwitter;
using Hanselman.Portable.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

using XLabs.Forms.Controls;
using XLabs.Platform.Device;
using XLabs.Ioc;
using XLabs.Platform.Services;
using XLabs.Platform.Services.Media;

using Core;
using Hanselman.Portable.Views;

namespace Hanselman.Portable
{
	public class CreateEventViewModel : BaseViewModel
	{
		private readonly static string OPTION_CAMERA = "From Camera";
		private readonly static string OPTION_GALLERY = "From Gallery";

		public NavigationController NavigationController { get; private set;}

		public Event Event { get; set; }
		public byte[] EventImage { get; set; }

		private IMediaPicker _mediaPicker;

		public CreateEventViewModel(NavigationController navigationController)
		{
			NavigationController = navigationController;
			Title = "Create Event";
			Icon = "slideout.png";
			Event = new Event();
		}

		private Command submitCommand;
		public Command SubmitCommand
		{
			get
			{
				return submitCommand ?? 
					(submitCommand = new Command(() => { ExecuteSubmitCommand(); }, () => 
						{ 
							return !IsBusy; 
						}));
			}
		}
		public async Task ExecuteSubmitCommand()
		{
			if (IsBusy) { return; }

			IsBusy = true;
			SubmitCommand.ChangeCanExecute();
			try
			{
				new Task(SaveEvent).Start();
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				page.DisplayAlert("Error", "Unable to filter.", "OK");
			}

			IsBusy = false;
			SubmitCommand.ChangeCanExecute();
		}

		protected async void SaveEvent ()
		{
			EventRepository eventRepo = new EventRepository();
			PlaceRepository placeRepo = new PlaceRepository();

			// TODO: API call to /places. Search for existing places that match.

			// TODO: If no existing place found, make one. Default private.

			// TODO: Change "zzz" to logged in Uder.Id
			Event.Place.Managers.Add("zzz", ManagerRole.Admin);
			Event.Managers.Add("zzz", ManagerRole.Admin);

			Event.Name = "feefeff";
			Event.Place.Address = new Address {
				Street = "5846 N Artesian Ave, ",
				City = "Chicago",
				State = "IL",
				Zip = "60659"
			};

			IEnumerable<Position> coords = await new Geocoder ().GetPositionsForAddressAsync(Event.Place.Address.ToString());

			// TODO: ValidateInput();

			if (coords.Count() == 0) {
				Renderer.ShowSnack("Could not validate address.", null);
				return;
			}

			// FIXME: coords may contain multiple pairs. How to handle? Spinner? GetAddressAsyncForPositions()???
			Position pos = coords.First();
			Event.SoftLocation.X = pos.Latitude;
			Event.SoftLocation.Y = pos.Longitude;

			RequestResponse<Place> placeSaveResponse = await placeRepo.Post(Event.Place);

			Dictionary<string, object> requestParts = new Dictionary<string, object> ();
			requestParts.Add ("eventImage", EventImage); // TODO: Make constant.

			RequestResponse<Event> response = await eventRepo.Post(Event, requestParts);
			Renderer.ShowSnack("Event Saved.", null);

			//Device.BeginInvokeOnMainThread (() => {
				NavigationController.Navigate(new EventDetailsPage(response.content), MenuType.EventDetails);
				//Navigation.PushAsync(new NavigationPage(new EventDetailsPage(response.content)));
				//Navigation.PushAsync(new EventDetailsPage(response.content));
				//Navigation.PushModalAsync(new EventDetailsPage(response.content));
				//App.Current.MainPage = new NavigationPage(new EventListPage());
				//App.Current.MainPage = new EventListPage();
			//});

			Event = new Event ();
		}


		private Command eventPhotoCommand;
		public Command EventPhotoCommand
		{
			get
			{
				return eventPhotoCommand ?? 
					(eventPhotoCommand = new Command((args) => { ExecuteEventPhotoCommand(args); }, (args) => 
						{ 
							return !IsBusy; 
						}));
			}
		}
		public async Task ExecuteEventPhotoCommand(object obj)
		{
			if (IsBusy) { return; }

			IsBusy = true;
			EventPhotoCommand.ChangeCanExecute();
			try
			{
				if (_mediaPicker == null) {

					IDevice device = Resolver.Resolve<IDevice>();

					////RM: hack for working on windows phone? 
					_mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;
				}

				var mediaFile = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
					{
						DefaultCamera = CameraDevice.Front,
						MaxPixelDimension = 200
					});

				Stream stream = /*ResizeImage(*/mediaFile.Source/*)*/;

				using(MemoryStream ms = new MemoryStream())
				{
					stream.CopyTo(ms);
					EventImage = ms.ToArray();
				}

				Renderer.SetViewData("eventImage", EventImage);

				//Image eventImage = (Image) obj;
				//eventImage.Source = ImageSource.FromStream(() => stream);

				Renderer.ShowSnack(string.Format("Taking a photo. {0}", ""), null);
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				page.DisplayAlert("Error", String.Format("Unable to take a photo: {0}", ex.Message), "OK");
			}

			IsBusy = false;
			EventPhotoCommand.ChangeCanExecute();
		}
	}
}

