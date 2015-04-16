using System.Text.RegularExpressions;
using BeginMobile.Services.ManagerServices;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
	public class ForgotPassword : ContentPage
    {
        private const string EmailRegex =
              @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        private readonly Entry _entryEmail;

	    public ForgotPassword ()
		{
		    var logo = new Image
            {
                Source = Device.OS == TargetPlatform.iOS
                    ? ImageSource.FromFile("logotype.png")
                    : ImageSource.FromFile("logotype.png"),
                Aspect = Aspect.AspectFit,
            };

            var labelTitle = new Label
                               {
                                   Text = "Password Recovery",
                                   Style = BeginApplication.Styles.TitleStyle
                               };

            var labelSubTitle = new Label{
                                               Text = "Enter the e-mail address you registered with the Application. Instructions to reset your password will be sent to this address.", 
                                               Style = BeginApplication.Styles.BodyStyle
                                           };

            _entryEmail = new Entry { Placeholder = "Enter your e-mail address"};

            var buttonReset = new Button { 
                                                Text= "Send", 
                                                Style = BeginApplication.Styles.DefaultButton
                                            };
            
            buttonReset.Clicked += async (sender, eventArgs) =>
                                         {                                            
                    var isEmailValid = Regex.IsMatch(_entryEmail.Text, EmailRegex);
                                             if (isEmailValid)
                                             {
                                                 var loginUserManager = new LoginUserManager();
                                                 string webPage = loginUserManager.RetrievePassword(_entryEmail.Text);
                                                 if (webPage.Equals(""))
                                                 {
                                                     await
                                                         DisplayAlert("Information",
                                                             "Please check your email address for reset your password",
                                                             "ok");
                                                     MessagingCenter.Send<ContentPage>(this, "Login");
                                                 }
                                                 else
                                                 {
                                                     await DisplayAlert("Error",
                                                    "An error happened on the server",
                                                    "Re - Try");
                                                 }
                                             }
                                             else
                                             {
                                                 await DisplayAlert("Validation Error",
                                                     "Email has wrong format",
                                                     "Re - Try");
                                             }
                                         };

            var buttonBack = new Button { Text = "Cancel", Style = BeginApplication.Styles.DefaultButton };
            buttonBack.Clicked += (sender, eventArgs) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Login");
            };

             
			Content = new StackLayout {
                Spacing = 10,
                Padding = BeginApplication.Styles.LayoutThickness,
                VerticalOptions = LayoutOptions.Center,
                Children = { 
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
