using System;
using System.Linq;
using BeginMobile.Interfaces;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.ManagerServices;
using Xamarin.Forms;
using BeginMobile.Services.Utils;

namespace BeginMobile.Accounts
{
    public class Login : BaseContentPage
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
                    await DisplayAlert("Validation Error", "Username and Password are required", "Re - Try");
                }

                else
                {
                    ActivityIndicatorLoading.IsVisible = true;
                    ActivityIndicatorLoading.IsRunning = true;

                    var loginUserManager = new LoginUserManager();
                    var loginUser = await loginUserManager.Login(_entryEmail.Text, _entryPassword.Text);

                    if (loginUser != null)
                    {
                        if (loginUser.Errors != null)
                        {
                            var errorMessage = loginUser.Errors.Aggregate("", (current, error) => current + (error.ErrorMessage + "\n"));
                            await DisplayAlert("Error login validation", errorMessage, "Re - Try");
                        }

                        else
                        {
                            App.Current.Properties["IsLoggedIn"] = true;
                            App.Current.Properties["LoginUser"] = loginUser;

                            iLoginManager.ShowMainPage(loginUser);
                        }
                    }

                    else
                    {
                        await DisplayAlert("Connection Failed", "Server not found.", "Re - Try");
                    }

                    ActivityIndicatorLoading.IsVisible = false;
                    ActivityIndicatorLoading.IsRunning = false;
                }
            };

            buttonRegister.Clicked += (sender, eventArgs) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Register");
            };

            var stackLayoutLoading = CreateStackLayoutWithLoadingIndicator();

            Content = new StackLayout
                      {
                          Spacing = 20,
                          Padding = App.Styles.LayoutThickness,
                          VerticalOptions = LayoutOptions.Center,
                          Children =
                          {
                              stackLayoutLoading,
                              logo,
                              _entryEmail,
                              _entryPassword,
                              buttonLogin,
                              buttonForgotPassword,
                              buttonRegister
                          }
                      };

            MessagingCenter.Subscribe<AppContextError>(this, "AppContextError", OnAppContextErrorOccurred);
        }
        private async void OnAppContextErrorOccurred(AppContextError appContextError)
        {
            await DisplayAlert(appContextError.Title, appContextError.Message, appContextError.Accept);
            ActivityIndicatorLoading.IsVisible = false;
            ActivityIndicatorLoading.IsRunning = false;
        }
    }
}