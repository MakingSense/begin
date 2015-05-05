using System.Linq;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using Xamarin.Forms;
using BeginMobile.Services.Utils;
using BeginMobile.LocalizeResources.Resources;

namespace BeginMobile.Accounts
{
    public class ChangePasswordPage : ContentPage
    {
        private readonly Entry _entryCurrentPassword;
        private readonly Entry _entryNewPassword;
        private readonly Entry _entryRepeatNewPassword;

        public ChangePasswordPage()
        {
            Style = BeginApplication.Styles.PageStyle;
            var currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];
            var loginUserManager = new LoginUserManager();

            Title = AppResources.ChangePasswordTitle;

            _entryCurrentPassword = new Entry
            {
                Placeholder = AppResources.PasswordPlaceholder,
                IsPassword = true,
                Style = BeginApplication.Styles.PageStyle
            };

            _entryNewPassword = new Entry
            {
                Placeholder = AppResources.PlaceholderChangePasswordNewPass,
                IsPassword = true,
            };

            _entryRepeatNewPassword = new Entry
            {
                Placeholder = AppResources.PlaceholderChangePasswordConfirmNewPass,
                IsPassword = true,
            };

            var buttonChangePassword = new Button
                                           {
                                               Text = AppResources.ButtonChangePasswordSend,
                                               HorizontalOptions = LayoutOptions.FillAndExpand,
                                               Style = BeginApplication.Styles.DefaultButton
                                           };

            buttonChangePassword.Clicked += async (sender, eventArgs) =>
            {
                var changePasswordResponse =
                    await loginUserManager.ChangeYourPassword(_entryCurrentPassword.Text, _entryNewPassword.Text, _entryRepeatNewPassword.Text, currentUser.AuthToken);

                if (changePasswordResponse != null)
                {
                    if (changePasswordResponse.HasError)
                    {

                        var messageErrors = ErrorMessages.GetTranslatedErrors(changePasswordResponse.Errors).Aggregate("", (current, error) => current + (error + "\n"));
                        await DisplayAlert(AppResources.ErrorMessageTitle, messageErrors, AppResources.AlertReTry);
                        
                        _entryCurrentPassword.Text = "";
                        _entryNewPassword.Text = "";
                        _entryRepeatNewPassword.Text = "";
                    }
                    else
                    {
                        await DisplayAlert(AppResources.ServerErrorMessageName, AppResources.ServerErrorMessage, AppResources.AlertOk);
                        await Navigation.PopToRootAsync();
                    }
                }
                else
                {
                    await Navigation.PopToRootAsync();
                }
            };

            var mainLayout = new StackLayout
            {
                Spacing = 20,
                Padding = BeginApplication.Styles.LayoutThickness,
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
