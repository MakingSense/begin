using Xamarin.Forms;
using BeginMobile.Accounts;
using BeginMobile.Interfaces;
using BeginMobile.Menu;
using BeginMobile.Services.DTO;

namespace BeginMobile
{
    public class App : Xamarin.Forms.Application, ILoginManager
	{
        static ILoginManager loginManager;
        public static App Current;

		public App ()
		{
		    Current = this;
		    loginManager = this;

            var isLoggedIn = Properties.ContainsKey("IsLoggedIn") ? (bool)Properties["IsLoggedIn"] : false;
            MainPage = new LoginModalPage(this);

		}

        public void ShowMainPage(LoginUser loginUser)
        {
            MainPage = new NavigationPage(new HomePage(loginUser));
        }

        public void Logout()
        {
            Properties["IsLoggedIn"] = false;
            Properties["LoginUser"] = null;

            MainPage = new LoginModalPage(this);
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
