using System;
using Xamarin.Forms;
using BeginMobile.Accounts;
using BeginMobile.Interfaces;
using BeginMobile.Menu;
using BeginMobile.Services.DTO;
using BeginMobile.Services;
using BeginMobile.Services.Utils;
using BeginMobile.Utils;
using BeginMobile.LocalizeResources.Resources;

namespace BeginMobile
{
    public class App : Application, ILoginManager
    {

        private static ILoginManager _loginManager;
        public static App Current;

        public App()
        {
            Current = this;
            _loginManager = this;

            if (Device.OS != TargetPlatform.WinPhone)
            {
                AppResources.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }

            AppDomain.CurrentDomain.UnhandledException += AppExceptionEventHander;

            MainPage = new LoginModalPage(this);
        }

        void AppExceptionEventHander(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            MessagingCenter.Send(this, "UnhandledException", eventArgs);
            MessagingCenter.Unsubscribe<App, UnhandledExceptionEventArgs>(this, "UnhandledException");
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

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #region "Services"

        private static ProfileServices _profileServices;
        public static ProfileServices ProfileServices
        {
            get { return _profileServices ?? (_profileServices = new ProfileServices()); }
        }

        private static GlobalService _globalService;
        public static GlobalService GlobalService
        {
            get { return _globalService ?? (_globalService = new GlobalService()); }
        }

        #endregion

        #region "Styles"

        private static Styles _styles;

        public static Styles Styles
        {
            get { return _styles ?? (_styles = new Styles()); }
        }

        #endregion

       
    }
}