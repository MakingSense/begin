using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using BeginMobile.Utils;
using BeginMobile.Services.ManagerServices;

namespace BeginMobile.Accounts
{
	public class ForgotPassword : ContentPage
    {
        private const string EmailRegex =
              @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        private Entry email;
        private Label labeTitle;
        private Label labelSubTitle;
        private Button buttonReset;
        private Button btBack;
	 
		public ForgotPassword ()
		{
            labeTitle = new Label
            {
                Text = "Password Recovery",
                Style = CustomizedButtonStyle.GetTitleStyle(),
            };
            labelSubTitle = new Label{
                Text = "Enter the e-mail address you registered with the Application. Instructions to reset your password will be sent to this address.",            
            };

            email = new Entry { Placeholder = "Enter your e-mail address"};
            buttonReset = new Button { Text= "Send", Style = CustomizedButtonStyle.GetButtonStyle()};
            buttonReset.Clicked += async (s, e) =>
                                         {
                                            
                    var isEmailValid = Regex.IsMatch(email.Text, EmailRegex);
                                             if (isEmailValid)
                                             {
                                                 LoginUserManager loginUserManager = new LoginUserManager();
                                                 string webPage = loginUserManager.RetrievePassword(email.Text);
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
                                                     DisplayAlert("Error",
                                                    "An error happened on the server",
                                                    "Re - Try");
                                                 }
                                             }
                                             else
                                             {
                                                 DisplayAlert("Validation Error",
                                                     "Email has wrong format",
                                                     "Re - Try");
                                             }
                                         };
          
            btBack = new Button { Text = "Cancel", Style=CustomizedButtonStyle.GetButtonStyle()};
            btBack.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Login");
            };

             
			Content = new StackLayout {
                Spacing = 10,
                Padding = 10,
                VerticalOptions = LayoutOptions.Center,
                Children = { labeTitle,labelSubTitle, email,buttonReset, btBack}                   			
			};
		}
	}
}
