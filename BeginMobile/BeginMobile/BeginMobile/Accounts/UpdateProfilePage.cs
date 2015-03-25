using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using BeginMobile.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
    public class UpdateProfilePage : ContentPage
    {
        private Entry _nameSurname;
        private Button _buttonUpdateProfile;

        public UpdateProfilePage()
        {
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            var loginUserManager = new LoginUserManager();

            Title = "Update profile";

            _nameSurname = new Entry
            {
                Placeholder = "Name and Surname",
                IsPassword = false,
            };

            _buttonUpdateProfile = new Button
            {
                Text = "Update",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Style = App.Styles.DefaultButton
            };


            _buttonUpdateProfile.Clicked += async (s, e) =>
            {
                var updateProfileResponse =
                    loginUserManager.UpdateProfile(_nameSurname.Text, currentUser.AuthToken);

                if (updateProfileResponse == true)
                {
                    await this.DisplayAlert("Successfuly updated!", "Your profile has been updated successfuly", "Ok");
                    await this.Navigation.PopToRootAsync();
                }
                else
                {
                    await DisplayAlert("Server Error", "An error happened on the server", "Ok");
                    await this.Navigation.PopToRootAsync();
                }
            };

            var mainLayout = new StackLayout
            {
                Spacing = 20,
                Padding = 50,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    _nameSurname, _buttonUpdateProfile
                }
            };

            Content = new ScrollView
            {
                Content = mainLayout
            };
        }
    }
}
