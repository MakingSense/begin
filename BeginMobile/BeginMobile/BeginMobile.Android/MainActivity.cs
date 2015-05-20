using Android.App;
using Android.Content.PM;
using Android.OS;
using BeginMobile.Android.DependencyService;
using BeginMobile.Android.Renderers;
using BeginMobile.Services.Interfaces;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Geolocation;
using XLabs.Platform.Services.Media;

namespace BeginMobile.Android
{
    [Activity(Label = "BeginMobile", Icon = "@drawable/icon", MainLauncher = true, Theme = "@style/BeginMobileTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetIoC();

            Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            CustomTabbedRenderer.Init();

            LoadApplication(new BeginApplication());
        }

        private void SetIoC()
        {
            
            // New Xlabs
            if (Resolver.IsSet == false)
            {
                var resolverContainer = new SimpleContainer();
                resolverContainer.Register<IDevice>(t => AndroidDevice.CurrentDevice)
                    .Register<IMediaPicker, MediaPicker>()
                    .Register<IDependencyContainer>(t => resolverContainer);
                Resolver.SetResolver(resolverContainer.GetResolver());
            }

        }
    }
}