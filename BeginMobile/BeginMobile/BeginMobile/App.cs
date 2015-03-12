using Xamarin.Forms;

namespace BeginMobile
{
    public class App : Xamarin.Forms.Application
	{
		public App ()
		{
			// The root page of your application
			/*MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							XAlign = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						}
					}
				}
			};*/

           // MainPage = new MenuProfile.MasterDetailProfile();

            var mainNavigation = new NavigationPage(new Login());
            MainPage = mainNavigation;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
