using System;
using System.Linq;
using System.Text.RegularExpressions;
using BeginMobile.Interfaces;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
    public class Register : ContentPage
    {
        private const string EmailRegex =
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private readonly Entry _entryUsername;
        private readonly Entry _entryFullName;
        private readonly Entry _entryEmail;
        private readonly Entry _entryPassword;
        private readonly Entry _entryConfirmPassword;
        private bool _switchStatus;
        public Register(ILoginManager iLoginManager)
        {
            if (AppResources.RegisterFormTitle != null) Title = AppResources.RegisterFormTitle;

            var imageLogo = new Image
            {
                Source = Device.OS == TargetPlatform.iOS
                    ? ImageSource.FromFile("logotype.png")
                    : ImageSource.FromFile("logotype.png"),
                Aspect = Aspect.AspectFit,
            };

            _entryUsername = new Entry
            {
                Placeholder = AppResources.EntryUsernamePlaceholder
            };

            _entryFullName = new Entry
            {
                Placeholder = AppResources.EntryFullNamePlaceholder
            };

            _entryEmail = new Entry
            {
                Placeholder = AppResources.EntryEmailPlaceholder
            };

            _entryPassword = new Entry
            {
                Placeholder = AppResources.EntryPasswordPlaceholder,
                IsPassword = true
            };

            _entryConfirmPassword = new Entry
            {
                Placeholder = AppResources.EntryConfirmPasswordPlaceholder,
                IsPassword = true
            };

            var buttonTermsAndConditions = new Button()
            {
                Text = "I agree the Terms & Conditions",
                FontSize = 10,
                Style = App.Styles.LinkButton,
                TextColor = Color.FromHex("77D065")
            };

            var switchTermsAndConditions = new Switch();
            switchTermsAndConditions.Toggled += (sender, eventArgs) => { _switchStatus = eventArgs != null && eventArgs.Value; };

            var buttonRegister = new Button
            {
                Text = AppResources.ButtonRegister,
                Style = App.Styles.DefaultButton

            };

            var buttonCancel = new Button
            {
                Text = AppResources.ButtonCancel,
                Style = App.Styles.DefaultButton
            };

            buttonCancel.Clicked += (sender, eventArgs) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Login");
            };

            buttonTermsAndConditions.Clicked += (senedr, eventArgs) =>
            {
                MessagingCenter.Send<ContentPage>(this, "TermsAndConditions");
            };

            buttonRegister.Clicked += async (sender, eventArgs) =>
            {
                if (String.IsNullOrEmpty(_entryFullName.Text) ||
                    String.IsNullOrEmpty(_entryEmail.Text)
                    || String.IsNullOrEmpty(_entryPassword.Text)
                    || String.IsNullOrEmpty(_entryConfirmPassword.Text)
                    )
                {
                    await DisplayAlert("Validation Error",
                                 "All fields are required",
                                 "Re - Try");
                }
                else
                {
                    var isEmailValid = Regex.IsMatch(_entryEmail.Text, EmailRegex);
                    if (isEmailValid)
                    {
                        if (_entryPassword.Text.Equals(_entryConfirmPassword.Text))
                        {
                            if (_switchStatus)
                            {
                                var loginUserManager = new LoginUserManager();
                                var registerUser = await loginUserManager.Register(_entryUsername.Text, _entryEmail.Text,
                                    _entryPassword.Text, _entryFullName.Text);

                                if (registerUser.Errors != null)
                                {
                                    var errorMessages = registerUser.Errors.Aggregate("", (current, error) => current + (error.Label + "\n"));

                                    await DisplayAlert("Error", errorMessages, "OK");

                                }
                                else
                                {
                                    await DisplayAlert("Successfull!", "You've successfully registered.", "OK");

                                    var loginUser = new LoginUser
                                                    {
                                        AuthToken = registerUser.AuthToken,
                                        User = registerUser.User
                                    };

                                    App.Current.Properties["IsLoggedIn"] = true;
                                    App.Current.Properties["LoginUser"] = loginUser;

                                    iLoginManager.ShowMainPage(loginUser);
                                }
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

            var stackLayoutSwitch = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
                Children = { buttonTermsAndConditions, switchTermsAndConditions }
            };


            Content = new ScrollView
            {
                Content = new StackLayout
            {
                Spacing = 10,
                Padding = 10,
                VerticalOptions = LayoutOptions.Center,
                Children =
                                  {    imageLogo,                                  
                                      _entryUsername,
                                      _entryFullName,
                                      _entryEmail,
                                      _entryPassword,
                                      _entryConfirmPassword,
                                      _entryConfirmPassword,
                                      stackLayoutSwitch,
                                      buttonRegister,
                                      buttonCancel
                                  }
            }
            };
        }
    }
}
