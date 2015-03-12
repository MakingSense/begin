using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Menu;
using BeginMobile.Utils;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;

namespace BeginMobile.Accounts
{
    public class Login : ContentPage
    {
        private Entry email;
        private Entry password;

        public Login()
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

            var forgotPassword = new Label
            {
                Text = "Forgot Password?"
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


            buttonLogin.Clicked += async (s, e) =>
            {
                if (String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(password.Text))
                {
                    DisplayAlert("Validation Error", "Email and Password are required",
                                 "Re - Try");
                }
                else
                {
                    var loginUserManager = new LoginUserManager();
                    var loginUser = loginUserManager.Login(email.Text, password.Text);

                    if (loginUser != null)
                    {
                        //Application.Current.Properties["Authtoken"] = loginUser.Authtoken;
                        //Application.Current.Properties["login"] = loginUser.User;
                        await Navigation.PushAsync(new HomePage(loginUser));
                    }
                    else
                    {
                        DisplayAlert("Authentification Error", "Invalid email or password ",
                               "Re - Try");
                    }                      
                }
            };
            buttonRegister.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new Register());
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
                                      forgotPassword,
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
