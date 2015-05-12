using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hanselman.Portable.Helpers
{
    public partial class DialogView : ContentView
    {
        public DialogView ()
        {
            InitializeComponent ();
        }

        public static async Task ShowDialog(View content, string message, Action<object> action)
        {
            ContentView snackbarView = content.FindByName<ContentView>("dialogView");
            // TODO: Make dialog.
            /*
            StackLayout sl = snackbarView.Content.FindByName<StackLayout>("dialogLayout");
            Label messageLabel = sl.FindByName<Label>("snackbarText");
            Label actionLabel = sl.FindByName<Label>("snackbarActionText");

            messageLabel.Text = message;*/

            snackbarView.Opacity = 0;
            snackbarView.IsVisible = true;
            await snackbarView.FadeTo(1,375);

            await Task.Delay(3500);
            await snackbarView.FadeTo(0,125);

            snackbarView.IsVisible = false;
        }
    }
}

