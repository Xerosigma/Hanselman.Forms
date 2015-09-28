using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LinqToTwitter;
using Hanselman.Portable.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Labs.Controls;
using Xamarin.Forms.Labs;
using Xamarin.Forms.Labs.Services;
using Xamarin.Forms.Labs.Services.Media;

using Core;

namespace Hanselman.Portable
{
	public class LoginViewModel : BaseViewModel
	{
		public LoginViewModel()
		{
			Title = "Login";
			Icon = "slideout.png";
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
				Renderer.ShowSnack(string.Format("Logging in: {0}", ""), null);
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				page.DisplayAlert("Error", String.Format("Unable to complete login: {0}", ex.Message), "OK");
			}

			IsBusy = false;
			SubmitCommand.ChangeCanExecute();
		}
	}
}

