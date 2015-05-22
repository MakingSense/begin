using BeginMobile.Interfaces;
using BeginMobile.Pages;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Utils;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
    public class LoginModalPage : CarouselPage
    {
        private readonly ContentPage _contentPageMenuLogin;
        readonly ContentPage _contentPageLogin;
        readonly ContentPage _contentPageRegister;
        readonly ContentPage _contentPageTermsAndConditions;
        readonly ContentPage _contentPageForgotPassword;

        public LoginModalPage(ILoginManager iloginManager)
        {
            const bool isLoadByLogin = true;
            NavigationPage.SetHasNavigationBar(this, false);

            _contentPageMenuLogin = new LoginMenu();
            _contentPageLogin = new Login(iloginManager);
            _contentPageRegister = new Register(iloginManager);
            _contentPageTermsAndConditions = new TermsAndConditions(isLoadByLogin);
            _contentPageForgotPassword = new ForgotPassword();

            Children.Add(_contentPageMenuLogin);
            Children.Add(_contentPageLogin);
            Children.Add(_contentPageRegister);
            Children.Add(_contentPageTermsAndConditions);
            Children.Add(_contentPageForgotPassword);

            MessagingCenter.Subscribe<ContentPage>(this, "Login", sender =>
            {
                CurrentPage = _contentPageMenuLogin;
            });
            MessagingCenter.Subscribe<ContentPage>(this, "LoginWithUserName", sender =>
            {
                CurrentPage = _contentPageLogin;
            });
            MessagingCenter.Subscribe<ContentPage>(this, "Register", sender =>
            {
                CurrentPage = _contentPageRegister;
            });

            MessagingCenter.Subscribe<ContentPage>(this, "TermsAndConditions", sender =>
            {
                CurrentPage = _contentPageTermsAndConditions;
            });
            MessagingCenter.Subscribe<ContentPage>(this, "ForgotPassword", sender =>
            {
                CurrentPage = _contentPageForgotPassword;
            });

            MessagingCenter.Subscribe<AppContextError>(this, AppContextError.NamedMessage, OnAppContextErrorOccurred);
        }

        public async void OnAppContextErrorOccurred(AppContextError appContextError)
        {
            await DisplayAlert(appContextError.Title, appContextError.Message, appContextError.Accept);
        }
    }
}