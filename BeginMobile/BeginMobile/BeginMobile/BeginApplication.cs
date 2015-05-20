using BeginMobile.Accounts;
using BeginMobile.Interfaces;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Menu;
using BeginMobile.MenuProfile;
using BeginMobile.Pages;
using BeginMobile.Services;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Logging;
using BeginMobile.UploadPages;
using BeginMobile.Utils;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile
{
    public class BeginApplication : Application, ILoginManager
    {
        private static ILoginManager _loginManager;
        public static BeginApplication CurrentBeginApplication;
        private readonly ILoggingService _log;
        private NavigationPage nav;
        public BeginApplication()
        {
            CurrentBeginApplication = this;

            _log = Logger.Current = DependencyService.Get<ILoggingService>();

            _loginManager = this;

            if (Device.OS != TargetPlatform.WinPhone)
            {
                AppResources.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }

            AppDomain.CurrentDomain.UnhandledException += AppExceptionEventHander;

            _log.Info("Start Begin Xamarin Application.");

            nav = new NavigationPage(new SplashPage());    //load splash page      
            MainPage = nav;
            LoadData();                                                
        }
        
        private async void LoadData()
        {
            await Task.Delay(3000);
            MainPage = new NavigationPage(new LoginModalPage(this));
        }
        private void AppExceptionEventHander(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            MessagingCenter.Send(this, "UnhandledException", eventArgs);
            MessagingCenter.Unsubscribe<BeginApplication, UnhandledExceptionEventArgs>(this, "UnhandledException");
            _log.Exception(eventArgs.ExceptionObject as Exception);
        }

        public async void ShowMainPage(LoginUser loginUser)
        {
            //MainPage = new NavigationPage(new HomePage(loginUser));
            MainPage = new NavigationPage(new AppHome());
        }

        public void ShowUploaderPage()
        {
            MainPage = new NavigationPage(new Uploader());
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

    public class SplashPage : ContentPage
    {
        public SplashPage()
        {
            BackgroundColor = Color.FromHex("646567");
            var image = new Image { Source = ImageSource.FromFile(BeginApplication.Styles.SplashImage), Aspect = Aspect.AspectFit };
            Content = new StackLayout
                      {
                          
                          HorizontalOptions = LayoutOptions.Center,
                          VerticalOptions = LayoutOptions.Center,
                          Children = { image}
                      };
        }
    }
}