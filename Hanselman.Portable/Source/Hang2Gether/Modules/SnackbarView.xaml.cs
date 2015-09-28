using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hanselman.Portable.Helpers
{
    public partial class SnackbarView : ContentView
    {
        public SnackbarView ()
        {
            InitializeComponent ();
        }

        public static async Task ShowSnack(View content, string message, Command command)
        {
            ContentView snackbarView = content.FindByName<ContentView>("snackbarView");
            StackLayout sl = snackbarView.Content.FindByName<StackLayout>("snackbarLayout");
            Label messageLabel = sl.FindByName<Label>("snackbarText");
            Label actionLabel = sl.FindByName<Label>("snackbarActionText");

            if(command != null) {
                actionLabel.GestureRecognizers.Add(new TapGestureRecognizer(){Command = command});
            }

            messageLabel.Text = message;

            snackbarView.Opacity = 0;
            snackbarView.IsVisible = true;
            await snackbarView.FadeTo(1,375);

            await Task.Delay(3500);
            await snackbarView.FadeTo(0,125);

            snackbarView.IsVisible = false;
        }
    }
}

