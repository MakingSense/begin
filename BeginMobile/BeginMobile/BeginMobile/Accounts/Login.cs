using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Menu;
using BeginMobile.Utils;

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
                Placeholder = "Email",
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
                    Application.Current.Properties["IsLoggedIn"] = true;
                    await Navigation.PushAsync(new HomePage());

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
