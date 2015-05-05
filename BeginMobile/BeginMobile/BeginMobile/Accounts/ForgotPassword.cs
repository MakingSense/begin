using System.Text.RegularExpressions;
using BeginMobile.Services.ManagerServices;
using Xamarin.Forms;
using BeginMobile.Interfaces;
using BeginMobile.LocalizeResources.Resources;

namespace BeginMobile.Accounts
{
    public class ForgotPassword : BaseContentPage
    {
        private const string EmailRegex =
              @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        private readonly Entry _entryEmail;

	    public ForgotPassword ()
		{
            Style = BeginApplication.Styles.PageStyle;
		    var logo = new Image
            {
                Source = Device.OS == TargetPlatform.iOS
                    ? ImageSource.FromFile("logotype.png")
                    : ImageSource.FromFile("logotype.png"),
                Aspect = Aspect.AspectFit,
            };

            var labelTitle = new Label
                               {
                                   Text = AppResources.ForgotPassLabelPasswordRecovery,
                                   Style = BeginApplication.Styles.TitleStyle
                               };

            var labelSubTitle = new Label{
                Text = AppResources.ForgotPassLabelEnterEmail, 
                                               Style = BeginApplication.Styles.TextBodyStyle
                                           };

            _entryEmail = new Entry { Placeholder = AppResources.ForgotPassPlaceHolderEmail };

            var buttonReset = new Button {
                Text = AppResources.ButtonSend, 
                                                Style = BeginApplication.Styles.DefaultButton
                                            };

	        buttonReset.Clicked += async (sender, eventArgs) =>
	                                     {

	                                         var email = _entryEmail.Text.Trim();
	                                         if (!string.IsNullOrEmpty(email))
	                                         {
	                                             var isEmailValid = Regex.IsMatch(email, EmailRegex);

	                                             if (isEmailValid)
	                                             {
	                                                 ActivityIndicatorLoading.IsVisible = true;
	                                                 ActivityIndicatorLoading.IsRunning = true;

	                                                 var loginUserManager = new LoginUserManager();
	                                                 string webPage = await loginUserManager.RetrievePassword(email);

	                                                 if (webPage != null)
	                                                 {
	                                                     if (webPage.Equals(""))
	                                                     {
	                                                         await
	                                                             DisplayAlert(AppResources.ForgotPassAlertInformation,
	                                                                 AppResources.ForgotPassAlertCheckEmail,
	                                                                 AppResources.AlertOk);
	                                                         MessagingCenter.Send<ContentPage>(this, "Login");
	                                                     }
	                                                     else
	                                                     {
	                                                         await DisplayAlert(AppResources.ApplicationError,
	                                                             AppResources.ForgotPassAlertErrorServer,
	                                                             AppResources.AlertReTry);
	                                                     }
	                                                 }

	                                                 ActivityIndicatorLoading.IsVisible = false;
	                                                 ActivityIndicatorLoading.IsRunning = false;
	                                             }
	                                             else
	                                             {
	                                                 await DisplayAlert(AppResources.ApplicationValidationError,
	                                                     AppResources.ForgotPassValidationEmail,
	                                                     AppResources.AlertReTry);
	                                             }
	                                         }
	                                         else
	                                         {
	                                             await DisplayAlert(AppResources.ApplicationValidationError,
	                                                 AppResources.ForgotPassEmptyValidationEmail,
	                                                 AppResources.AlertReTry);
	                                         }
	                                     };

            var buttonBack = new Button { Text = AppResources.ButtonCancel, Style = BeginApplication.Styles.DefaultButton };
            buttonBack.Clicked += (sender, eventArgs) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Login");
            };

            var stackLayoutLoading = CreateStackLayoutWithLoadingIndicator();
             
			Content = new StackLayout {
                Spacing = 10,
                Padding = BeginApplication.Styles.LayoutThickness,
                VerticalOptions = LayoutOptions.Center,
                Children = { 
                    stackLayoutLoading,
                    logo,
                    labelTitle,
                    labelSubTitle,
                    _entryEmail,
                    buttonReset,
                    buttonBack}                   			
			};
		}
	}
}
