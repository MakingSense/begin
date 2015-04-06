using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
    public class UpdateProfilePage : ContentPage
    {
        private readonly Entry _entryNameSurname;

        public UpdateProfilePage()
        {
            var currentUser = (LoginUser) App.Current.Properties["LoginUser"];
            var loginUserManager = new LoginUserManager();

            Title = "Update profile";

            _entryNameSurname = new Entry
                           {
                               Placeholder = "Name and Surname",
                               IsPassword = false,
                           };

            var buttonUpdateProfile = new Button
                                      {
                                          Text = "Update",
                                          HorizontalOptions = LayoutOptions.FillAndExpand,
                                          Style = App.Styles.DefaultButton
                                      };


            buttonUpdateProfile.Clicked += async (s, e) =>
                                                 {
                                                     loginUserManager.UpdateProfile(_entryNameSurname.Text,
                                                         currentUser.AuthToken);

                                                     await
                                                         DisplayAlert("Successfuly updated!",
                                                             "Your profile has been updated successfuly", "Ok");
                                                     await Navigation.PopToRootAsync();
                                                 };

            var mainLayout = new StackLayout
                             {
                                 Spacing = 20,
                                 Padding = 50,
                                 VerticalOptions = LayoutOptions.Start,
                                 Orientation = StackOrientation.Vertical,
                                 Children =
                                 {
                                     _entryNameSurname,
                                     buttonUpdateProfile
                                 }
                             };

            Content = new ScrollView
                      {
                          Content = mainLayout
                      };
        }
    }
}