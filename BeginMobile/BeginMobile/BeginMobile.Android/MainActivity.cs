using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using BeginMobile.Android.Renderers;

namespace BeginMobile.Android
{
    [Activity(Label = "BeginMobile", Icon = "@drawable/icon", MainLauncher = true, Theme = "@style/BeginMobileTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();

            

            LoadApplication(new BeginMobile.App());
        }
    }
}

