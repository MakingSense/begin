using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using BeginMobile.Services.Utils;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
    public class UpdateProfilePage : ContentPage
    {
        private readonly Entry _entryNameSurname;

        public UpdateProfilePage()
        {
            Style = BeginApplication.Styles.PageStyle;
            var currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];
            var loginUserManager = new LoginUserManager();

            Title = AppResources.UpdateProfileTitle;

            _entryNameSurname = new Entry
                           {
                               Placeholder = AppResources.UpdateProfileEntryNameSurname,
                               IsPassword = false,
                           };

            var buttonUpdateProfile = new Button
                                      {
                                          Text = AppResources.UpdateProfileButtonUpdate,
                                          HorizontalOptions = LayoutOptions.FillAndExpand,
                                          Style = BeginApplication.Styles.DefaultButton
                                      };


            buttonUpdateProfile.Clicked += async (s, e) =>
                                                 {
                                                     
                                                     var result = await loginUserManager.UpdateProfile(_entryNameSurname.Text,
                                                         currentUser.AuthToken);

                                                     if (result == "")
                                                     {
                                                         await
                                                         DisplayAlert(AppResources.UpdateProfileAlertSuccess,
                                                             AppResources.UpdateProfileAlertMessage, AppResources.AlertOk);
                                                     }

                                                     
                                                     await Navigation.PopToRootAsync();
                                                 };

            var mainLayout = new StackLayout
                             {
                                 Padding = BeginApplication.Styles.InitialPagesThickness,
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