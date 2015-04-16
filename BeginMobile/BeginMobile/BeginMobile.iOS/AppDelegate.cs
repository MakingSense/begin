using BeginMobile.iOS.Renderers;
using Foundation;
using UIKit;

using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;

namespace BeginMobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        private UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
           window = new UIWindow(UIScreen.MainScreen.Bounds);

            Forms.Init();
            ImageCircleRenderer.Init();
            LoadApplication(new BeginApplication());
            return base.FinishedLaunching(app, options);
        }
    }
}
