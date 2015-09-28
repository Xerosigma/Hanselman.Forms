using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Hanselman.Portable.Helpers;

namespace Hanselman.Portable.Views
{
	public partial class LoginPage : ContentPage, Renderer
	{
		public NavigationController NavigationController { get; private set; }

		private LoginViewModel ViewModel
		{
			get { return BindingContext as LoginViewModel; }
		}

		public LoginPage (NavigationController navigationController)
		{
			InitializeComponent ();

			BindingContext = new LoginViewModel();

			ViewModel.Renderer = this;
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

			if(ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy)
			{
				return;
			}

			ShowSnack ("Page loaded.", null);
		}
	}
}

