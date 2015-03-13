using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using BeginMobile.Pages;
using BeginMobile.Utils;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using BeginMobile.Interfaces;

namespace BeginMobile.Accounts
{
    public class Register : ContentPage
    {
        private const string EmailRegex =
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private readonly Entry _username;
        private readonly Entry _fullName;
        private readonly Entry _email;
        private readonly Entry _password;
        private readonly Entry _confirmPassword;
        private readonly RadioButton _radio;

        public Register(ILoginManager iLoginManager)
        {
            Title = "Register";
            _username = new Entry
            {
                Placeholder = "Username"
            };
            _fullName = new Entry
            {
                Placeholder = "Full Name"
            };
            _email = new Entry
            {
                Placeholder = "Email"
            };
            _password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true
            };
            _confirmPassword = new Entry
            {
                Placeholder = "Confirm Password",
                IsPassword = true
            };

            //Intengrar radio button para tdos 

            var buttonTermsAndConditions = new Button()
            {
                Text = " Terms & Conditions",
                Style = CustomizedButtonStyle.GetControlButtonStyle(),
                TextColor = Color.FromHex("77D065")
            };
            _radio = new RadioButton
            {
                Text = "I agree to the ",
                StyleId = "#FF0000"
            };


            var buttonRegister = new Button
            {
                Text = "Register",
                BackgroundColor = Color.FromHex("77D065")
            };

            var btCancel = new Button { Text = "Cancel", BackgroundColor = Color.FromHex("77D065") };
            btCancel.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Login");
            };

            buttonTermsAndConditions.Clicked += async (s , e)=>{
                MessagingCenter.Send<ContentPage>(this, "TermsAndConditions");
            };

            buttonRegister.Clicked += async (s, e) =>
            {
                if (String.IsNullOrEmpty(_fullName.Text) ||
                    String.IsNullOrEmpty(_email.Text)
                    || String.IsNullOrEmpty(_password.Text)
                    || String.IsNullOrEmpty(_confirmPassword.Text)
                    )
                {
                    DisplayAlert("Validation Error",
                                 "All fields are required",
                                 "Re - Try");
                }
                else
                {
                    var isEmailValid = Regex.IsMatch(_email.Text, EmailRegex);
                    if (isEmailValid)
                    {
                        // Application.Current.Properties["IsRegistered"] = true;
                        if (_password.Text.Equals(_confirmPassword.Text))
                        {
                            if (_radio.IsToggled)
                            {
                                //
                                LoginUserManager LoginUserManager = new LoginUserManager();

                                var registerUser = LoginUserManager.Register(_username.Text, _email.Text,
                                    _password.Text, _fullName.Text);

                                if(registerUser!=null){

                                    DisplayAlert("Successfull!", "You have successfully registered", "OK");

                                    var loginUser = new LoginUser()
                                    {
                                        AuthToken = registerUser.AuthToken,
                                        User = registerUser.User
                                    };

                                    App.Current.Properties["IsLoggedIn"] = true;
                                    iLoginManager.ShowMainPage(loginUser);

                                    //await Navigation.PushAsync(new Login());
                                }
                                else
                                {
                                    DisplayAlert("Error", "Has been happened an error", "OK");
                                }
                            }
                            else
                            {
                                DisplayAlert("Validation Error",
                                             "Please agree the Terms and Conditions!",
                                             "Re - Try");
                            }
                        }
                        else
                        {
                            DisplayAlert("Validation Error",
                                         "Password and Confirm password is not match!",
                                         "Re - Try");
                        }
                    }
                    else
                    {
                        DisplayAlert("Validation Error",
                                     "Please enter a valid email address",
                                     "Re - Try");
                    }
                }
            };

            var layoutRadioButton = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Start,
                Children = { _radio, buttonTermsAndConditions }
            };

            Content = new StackLayout
            {
                Spacing = 10,
                Padding = 10,
                VerticalOptions = LayoutOptions.Center,
                Children =
                                  {
                                      _username,
                                      _fullName,
                                      _email,
                                      _password,
                                      _confirmPassword,
                                      _confirmPassword,
                                      layoutRadioButton,
                                      buttonRegister,
                                      btCancel
                                  }
            };
        }
    }
}
