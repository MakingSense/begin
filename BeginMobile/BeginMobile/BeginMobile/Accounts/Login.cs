using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Menu;
using BeginMobile.Utils;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using BeginMobile.Interfaces;

namespace BeginMobile.Accounts
{
    public class Login : ContentPage
    {
        private Entry email;
        private Entry password;

        public Login(ILoginManager iLoginManager)
        {
            Title = "Login Form";
            email = new Entry
            {
                Placeholder = "Username",
                Keyboard = Keyboard.Email
            };
            password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
            };

            var buttonForgotPassword = new Button
            {
                Text = "Forgot Password?",
                Style = CustomizedButtonStyle.GetControlButtonStyle(), 
                WidthRequest = 100
            };

            var buttonLogin = new Button
            {
                TextColor = Color.White,
                //BackgroundColor = Color.FromHex("77D065"),
                Text = "Login",
                Style = CustomizedButtonStyle.GetButtonStyle(),

            };
            var buttonRegister = new Button
            {
                Text = "Register",
                //TextColor = Color.White,
                //BackgroundColor = Color.FromHex("77D065")
                Style = CustomizedButtonStyle.GetButtonStyle(),
            };

            buttonForgotPassword.Clicked += (s, e) =>
            {
                MessagingCenter.Send<ContentPage>(this,"ForgotPassword"); };


            buttonLogin.Clicked += async (s, e) =>
            {
                if (String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(password.Text))
                {
                    await DisplayAlert("Validation Error", "Username and Password are required",
                                 "Re - Try");
                }
                else
                {

                    //iLoginManager.ShowMainPage(null); 

                    var loginUserManager = new LoginUserManager();
                    var loginUser = loginUserManager.Login(email.Text, password.Text);

                    if (loginUser != null)
                    {
                        App.Current.Properties["IsLoggedIn"] = true;
                        App.Current.Properties["LoginUser"] = loginUser;

                        iLoginManager.ShowMainPage(loginUser); 
                    }
                    else
                    {
                        DisplayAlert("Authentification Error", "Invalid username or password ",
                               "Re - Try");
                    }                      
                }
            };

            buttonRegister.Clicked += async (s, e) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Register");
                //await Navigation.PushAsync(new Register());
            };

            Content = new StackLayout
            {
                Spacing = 20,
                Padding = 50,
                VerticalOptions = LayoutOptions.Center,
                Children =
                                  {
                                      email,
                                      password,
                                      buttonLogin,
                                      buttonForgotPassword,
                                      buttonRegister
                                  }
            };
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            Entry entry = sender as Entry;
            String value = entry.Text;


        }
    }
}
