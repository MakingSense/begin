using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using BeginMobile.Utils;

namespace BeginMobile.Accounts
{
    public class ChangePasswordPage : ContentPage
    {
        private Entry currentPassword;
        private Entry newPassword;
        private Entry repeatNewPassword;
        private Button buttonChangePassword;

        public ChangePasswordPage()
        {
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            var loginUserManager = new LoginUserManager();

            Title = "Change your Password";

            currentPassword = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
            };

            newPassword = new Entry
            {
                Placeholder = "New Password",
                IsPassword = true,
            };

            repeatNewPassword = new Entry
            {
                Placeholder = "Repeat New Password",
                IsPassword = true,
            };

            buttonChangePassword = new Button
            {
                Text = "Send",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Style = App.Styles.DefaultButton
            };

            buttonChangePassword.Clicked += async (s, e) =>
            {
                var changePasswordResponse =
                    loginUserManager.ChangeYourPassword(currentPassword.Text, newPassword.Text, repeatNewPassword.Text, currentUser.AuthToken);

                if (changePasswordResponse != null)
                {
                    var messageErrors = "";

                    if (changePasswordResponse.Errors != null)
                    {
                        foreach (var error in changePasswordResponse.Errors)
                        {
                            messageErrors += error.Label + "\n";
                        }
                        await DisplayAlert("Error", messageErrors, "Re-try");
                        currentPassword.Text = "";
                        newPassword.Text = "";
                        repeatNewPassword.Text = "";
                    }
                    else
                    {
                        await DisplayAlert("Server Error", "An error happened on the server", "Ok");
                        await this.Navigation.PopToRootAsync();
                    }
                }
                else
                {
                    await this.DisplayAlert("Successfuly changed!", "Your password has been changed successfuly", "Ok");
                    await this.Navigation.PopToRootAsync();
                }
            };

            StackLayout mainLayout = new StackLayout
            {
                Spacing = 20,
                Padding = 50,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    currentPassword, newPassword, repeatNewPassword, buttonChangePassword
                }
            };

            Content = new ScrollView
            {
                Content = mainLayout
            };
        }
    }
}
