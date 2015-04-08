using System.Linq;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
    public class ChangePasswordPage : ContentPage
    {
        private readonly Entry _entryCurrentPassword;
        private readonly Entry _entryNewPassword;
        private readonly Entry _entryRepeatNewPassword;

        public ChangePasswordPage()
        {
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            var loginUserManager = new LoginUserManager();

            Title = "Change your Password";

            _entryCurrentPassword = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
            };

            _entryNewPassword = new Entry
            {
                Placeholder = "New Password",
                IsPassword = true,
            };

            _entryRepeatNewPassword = new Entry
            {
                Placeholder = "Confirm New Password",
                IsPassword = true,
            };

            var buttonChangePassword = new Button
                                           {
                                               Text = "Send",
                                               HorizontalOptions = LayoutOptions.FillAndExpand,
                                               Style = App.Styles.DefaultButton
                                           };

            buttonChangePassword.Clicked += async (sender, eventArgs) =>
            {
                var changePasswordResponse =
                    await loginUserManager.ChangeYourPassword(_entryCurrentPassword.Text, _entryNewPassword.Text, _entryRepeatNewPassword.Text, currentUser.AuthToken);

                if (changePasswordResponse != null)
                {
                    if (changePasswordResponse.Errors != null)
                    {
                        var messageErrors = changePasswordResponse.Errors.Aggregate("", (current, error) => current + "\n");
                        await DisplayAlert("Error", messageErrors, "Re-try");
                        _entryCurrentPassword.Text = "";
                        _entryNewPassword.Text = "";
                        _entryRepeatNewPassword.Text = "";
                    }
                    else
                    {
                        await DisplayAlert("Server Error", "An error happened on the server", "Ok");
                        await Navigation.PopToRootAsync();
                    }
                }
                else
                {
                    await DisplayAlert("Successfuly changed!", "Your password has been changed successfuly", "Ok");
                    await Navigation.PopToRootAsync();
                }
            };

            var mainLayout = new StackLayout
            {
                Spacing = 20,
                Padding = App.Styles.LayoutThickness,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    _entryCurrentPassword, _entryNewPassword, _entryRepeatNewPassword, buttonChangePassword
                }
            };

            Content = new ScrollView
            {
                Content = mainLayout
            };
        }
    }
}
