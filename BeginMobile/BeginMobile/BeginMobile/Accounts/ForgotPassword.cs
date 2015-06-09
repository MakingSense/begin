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
            Style = BeginApplication.Styles.InitialPageStyle;

	        Title = "Forgot Password";

            /*var mainTitle = new Label
            {
                Text = "Forgot Password",//AppResources.LoginFormTitle,
                Style = BeginApplication.Styles.InitialPageTitleStyle,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Center
            };*/

            var labelTitle = new Label
                               {
                                   Text = AppResources.ForgotPassLabelPasswordRecovery,
                                   Style = BeginApplication.Styles.TitleStyle
                               };

            var labelSubTitle = new Label{
                Text = AppResources.ForgotPassLabelEnterEmail, 
                                               Style = BeginApplication.Styles.TextBodyStyle
                                           };

            _entryEmail = new Entry
            {
                Placeholder = AppResources.ForgotPassPlaceHolderEmail,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };

            var buttonReset = new Button {
                Text = "Forgot Password",//AppResources.ButtonSend, 
                                                Style = BeginApplication.Styles.DefaultButton,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 16
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

            var stackLayoutLoading = CreateStackLayoutWithLoadingIndicator();

            var componentsLayout = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = BeginApplication.Styles.InitialPagesThickness,
                RowDefinitions = new RowDefinitionCollection
                                                        {
                                                            new RowDefinition {Height = GridLength.Auto},
                                                            new RowDefinition {Height = GridLength.Auto},
                                                            new RowDefinition {Height = GridLength.Auto},
                                                            
                                                        }
            };
            componentsLayout.Children.Add(stackLayoutLoading, 0, 0);
            componentsLayout.Children.Add(_entryEmail, 0, 1);
            componentsLayout.Children.Add(buttonReset, 0, 2);
            var backgroundImage = new Image
            {
                Source = ImageSource.FromFile(BeginApplication.Styles.DefaultLoginBackgroundImage),
                Aspect = Aspect.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            var relativeLayout = new RelativeLayout();
            relativeLayout.Children.Add(backgroundImage,
               xConstraint: Constraint.Constant(0),
               yConstraint: Constraint.Constant(0),
               widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
               heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            relativeLayout.Children.Add(componentsLayout,
              xConstraint: Constraint.Constant(0),
              yConstraint: Constraint.Constant(0),
              widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
              heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

	        Content = relativeLayout;
		}
	}
}
