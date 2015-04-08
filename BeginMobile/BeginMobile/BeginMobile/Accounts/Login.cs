using System;
using BeginMobile.Interfaces;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.ManagerServices;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
    public class Login : ContentPage
    {
        private readonly Entry _entryEmail;
        private readonly Entry _entryPassword;

        public Login(ILoginManager iLoginManager)
        {
            var logo = new Image
            {
                Source = ImageSource.FromFile("logotype.png"),
                Aspect = Aspect.AspectFit,
            };

            Title = AppResources.LoginFormTitle;
            _entryEmail = new Entry
            {
                Placeholder = AppResources.UsernamePlaceholder,
                Keyboard = Keyboard.Email
            };

            _entryPassword = new Entry
            {
                Placeholder = AppResources.PasswordPlaceholder,
                IsPassword = true,
            };

            var buttonForgotPassword = new Button
            {
                Text = AppResources.ButtonForgotPassword,
                Style = App.Styles.LinkButton,
            };

            var buttonLogin = new Button
            {
                Text = AppResources.ButtonLogin,
                Style = App.Styles.DefaultButton,
            };

            var buttonRegister = new Button
            {
                Text = AppResources.ButtonRegister,
                Style = App.Styles.DefaultButton,
            };

            buttonForgotPassword.Clicked += (sender, eventArgs) =>
            {
                MessagingCenter.Send<ContentPage>(this, "ForgotPassword");
            };


            buttonLogin.Clicked += async (sender, eventArgs) =>
            {
                if (String.IsNullOrEmpty(_entryEmail.Text) || String.IsNullOrEmpty(_entryPassword.Text))
                {
                    await DisplayAlert("Validation Error", "Username and Password are required",
                                 "Re - Try");
                }
                else
                {
                    var loginUserManager = new LoginUserManager();
                    var loginUser = loginUserManager.Login(_entryEmail.Text, _entryPassword.Text);

                    if (loginUser != null)
                    {
                        App.Current.Properties["IsLoggedIn"] = true;
                        App.Current.Properties["LoginUser"] = loginUser;

                        iLoginManager.ShowMainPage(loginUser);
                    }
                    else
                    {
                        await DisplayAlert("Authentification Error", "Invalid username or password ",
                               "Re - Try");
                    }
                }
            };

            buttonRegister.Clicked += (sender, eventArgs) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Register");
            };

            Content = new StackLayout
                      {
                          Spacing = 20,
                          Padding = App.Styles.LayoutThickness,
                          VerticalOptions = LayoutOptions.Center,
                          Children =
                          {
                              logo,
                              _entryEmail,
                              _entryPassword,
                              buttonLogin,
                              buttonForgotPassword,
                              buttonRegister
                          }
                      };
        }
    }
}
