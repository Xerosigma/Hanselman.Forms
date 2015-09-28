using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using ImageCircle.Forms.Plugin.Droid;

using XLabs.Platform.Device;
using XLabs.Ioc;

using Hanselman.Portable;

namespace HanselmanAndroid
{
    [Activity(Label = "Hanselman",
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


			Forms.Init(this, bundle);
			// FIXME: Xamarin.FormsMaps.Init(this, bundle);
            ImageCircleRenderer.Init();

			LoadApplication(new App());

			if (!Resolver.IsSet) {
				SimpleContainer resolverContainer = new SimpleContainer ();
				resolverContainer.Register<IDevice> (r => AndroidDevice.CurrentDevice);
				Resolver.SetResolver (resolverContainer.GetResolver ());
			}

            if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
            {
                ActionBar.SetIcon(
                    new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));
            }
        }
    }
}
