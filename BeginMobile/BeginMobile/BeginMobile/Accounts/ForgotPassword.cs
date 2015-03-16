using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Utils;

namespace BeginMobile.Accounts
{
	public class ForgotPassword : ContentPage
	{
        private Entry username;
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
                Text = "Enter the username and e-mail address you registered with the Application. Instructions to reset your password will be sent to this address.",            
            };

            username = new Entry { Placeholder = "Enter your username" };
            email = new Entry { Placeholder = "Enter your e-mail address"};
            buttonReset = new Button { Text= "Reset", Style = CustomizedButtonStyle.GetButtonStyle()};
            buttonReset.Clicked += async(s, e) => { await DisplayAlert("Underconstruction", "underconstruction", "Ok"); };
          
            btBack = new Button { Text = "Cancel", Style=CustomizedButtonStyle.GetButtonStyle()};
            btBack.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Login");
            };

             
			Content = new StackLayout {
                Spacing = 10,
                Padding = 10,
                VerticalOptions = LayoutOptions.Center,
                Children = { labeTitle,labelSubTitle, username, email,buttonReset, btBack}                   			
			};
		}
	}
}
