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
        private Switch switcher;
        private Label labelSwitcher;
        private bool switchStatus = false;
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
                Text = "I agree the Terms & Conditions",
                FontSize=10,
                Style = App.Styles.LinkButton,
                TextColor = Color.FromHex("77D065")
            };
            _radio = new RadioButton
            {
                Text = "I agree to the ",
                StyleId = "#FF0000"
            };

            //Switch
            labelSwitcher = new Label
            {
                Text = "I agree to the ",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                
                //HorizontalOptions = LayoutOptions.Start,
                //VerticalOptions = LayoutOptions.CenterAndExpand
            };
            switcher = new Switch
            {
                //HorizontalOptions = LayoutOptions.End,
                //VerticalOptions = LayoutOptions.CenterAndExpand
            };
            switcher.Toggled += async (se, ev) =>
            {
                if (ev.Value == true)
                {
                    switchStatus = true;
                }
                else
                {
                    switchStatus = false;
                }  
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

            buttonTermsAndConditions.Clicked += async (s,e)=>{
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
                    await DisplayAlert("Validation Error",
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
                            if (switchStatus)
                            {
                                //
                                LoginUserManager LoginUserManager = new LoginUserManager();

                                RegisterUser registerUser = LoginUserManager.Register(_username.Text, _email.Text,
                                    _password.Text, _fullName.Text);

                               if(registerUser.Errors!=null){
                                   string errorMessages = "";

                                   foreach(var error in registerUser.Errors){
                                       errorMessages += error.Label + "\n";
                                   }
                                   await DisplayAlert("Error",errorMessages, "OK");

                               }
                               else
                               {
                                   await DisplayAlert("Successfull!", "You've successfully registered.", "OK");

                                   var loginUser = new LoginUser()
                                   {
                                       AuthToken = registerUser.AuthToken,
                                       User = registerUser.User
                                   };

                                   App.Current.Properties["IsLoggedIn"] = true;
                                   App.Current.Properties["LoginUser"] = loginUser;

                                   iLoginManager.ShowMainPage(loginUser);
                               }
                                
                                //if(registerUser!=null){

                                //    await DisplayAlert("Successfull!", "You've successfully registered.", "OK");

                                //    var loginUser = new LoginUser()
                                //    {
                                //        AuthToken = registerUser.AuthToken,
                                //        User = registerUser.User
                                //    };

                                //    App.Current.Properties["IsLoggedIn"] = true;
                                //    App.Current.Properties["LoginUser"] = loginUser;

                                //    iLoginManager.ShowMainPage(loginUser);
                                //}
                                //else
                                //{
                                //    await DisplayAlert("Error", "Username already exists. Please choose a different usename", "OK");
                                //}
                            }
                            else
                            {
                                await DisplayAlert("Validation Error",
                                             "Please agree the Terms and Conditions!",
                                             "Re - Try");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Validation Error",
                                         "Password and Confirm password are not match!",
                                         "Re - Try");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Validation Error",
                                     "Email has wrong format",
                                     "Re - Try");
                    }
                }
            };

            var layoutRadioButton = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
                Children = { buttonTermsAndConditions, switcher }
            };


            ScrollView scroll = new ScrollView {
                Padding = 10,
            };
            StackLayout stackLayout = new StackLayout
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

            scroll.Content = stackLayout;
            Content = scroll;
        }
    }
}
