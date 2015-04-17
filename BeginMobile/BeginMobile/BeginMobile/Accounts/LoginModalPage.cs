using BeginMobile.Interfaces;
using BeginMobile.Pages;
using BeginMobile.Services.Utils;
using System;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
    public class LoginModalPage: CarouselPage
    {
        readonly ContentPage _contentPageLogin;
        readonly ContentPage _contentPageRegister;
        readonly ContentPage _contentPageTermsAndConditions;
        readonly ContentPage _contentPageForgotPassword;

        public LoginModalPage(ILoginManager iloginManager)
        {
            const bool isLoadByLogin = true;

            _contentPageLogin = new Login(iloginManager);
            _contentPageRegister = new Register(iloginManager);
            _contentPageTermsAndConditions = new TermsAndConditions(isLoadByLogin);
            _contentPageForgotPassword = new ForgotPassword();

            Children.Add(_contentPageLogin);
            Children.Add(_contentPageRegister);
            Children.Add(_contentPageTermsAndConditions);
            Children.Add(_contentPageForgotPassword);

            MessagingCenter.Subscribe<ContentPage>(this, "Login", sender =>
            {
                SelectedItem = _contentPageLogin;
            });
            MessagingCenter.Subscribe<ContentPage>(this, "Register", sender =>
            {
                SelectedItem = _contentPageRegister;
            });

            MessagingCenter.Subscribe<ContentPage>(this, "TermsAndConditions", sender =>
            {
                SelectedItem = _contentPageTermsAndConditions;
            });
            MessagingCenter.Subscribe<ContentPage>(this, "ForgotPassword", sender =>
            {
                SelectedItem = _contentPageForgotPassword;
            });


            MessagingCenter.Subscribe<AppContextError>(this, AppContextError.NamedMessage, OnAppContextErrorOccurred);
 
        }

        private async void OnAppContextErrorOccurred(AppContextError appContextError)
        {
            await DisplayAlert(appContextError.Title, appContextError.Message, appContextError.Accept);
        }
    }
}
