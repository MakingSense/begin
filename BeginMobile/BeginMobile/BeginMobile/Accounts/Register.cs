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
    public class Register : BaseContentPage
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
        private readonly ILoginManager _iLoginManager;
        public Register(ILoginManager iLoginManager)
        {
            _iLoginManager = iLoginManager;
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
                Text = AppResources.RegisterButtonTermsAndConditions,
                FontSize = 10,
                Style = BeginApplication.Styles.LinkButton,
                TextColor = Color.FromHex("77D065")
            };

            var switchTermsAndConditions = new Switch();
            switchTermsAndConditions.Toggled += (sender, eventArgs) => { _switchStatus = eventArgs != null && eventArgs.Value; };

            var buttonRegister = new Button
            {
                Text = AppResources.ButtonRegister,
                Style = BeginApplication.Styles.DefaultButton

            };

            var buttonCancel = new Button
            {
                Text = AppResources.ButtonCancel,
                Style = BeginApplication.Styles.DefaultButton
            };

            buttonCancel.Clicked += (sender, eventArgs) => MessagingCenter.Send<ContentPage>(this, "Login");

            buttonTermsAndConditions.Clicked += (sender, eventArgs) => MessagingCenter.Send<ContentPage>(this, "TermsAndConditions");

            buttonRegister.Clicked += RegisterClickEventHandler;

            var stackLayoutSwitch = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
                Children = { buttonTermsAndConditions, switchTermsAndConditions }
            };

            var stackLayoutLoading = CreateStackLayoutWithLoadingIndicator();


            Content = new ScrollView
                      {
                          Content = new StackLayout
                                    {
                                        Spacing = 10,
                                        Padding = 10,
                                        VerticalOptions = LayoutOptions.Center,
                                        Children =
                                        {
                                            stackLayoutLoading,
                                            imageLogo,
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

        public async void RegisterClickEventHandler(object sender, EventArgs eventArgs)
        {
            var userName = _entryUsername.Text.Trim();
            var fullName = _entryFullName.Text.Trim();
            var email = _entryEmail.Text.Trim();
            var password = _entryPassword.Text.Trim();
            var confirmPassword = _entryConfirmPassword.Text.Trim();

            if (String.IsNullOrEmpty(fullName) ||
                String.IsNullOrEmpty(email)
                || String.IsNullOrEmpty(password)
                || String.IsNullOrEmpty(confirmPassword)
                )
            {
                await DisplayAlert(AppResources.ApplicationValidationError,
                    AppResources.RegisterAlertFieldsAreRequired,
                    AppResources.AlertReTry);
            }
            else
            {
                var isEmailValid = Regex.IsMatch(email, EmailRegex);
                if (isEmailValid)
                {
                    if (password.Equals(confirmPassword))
                    {
                        if (_switchStatus)
                        {
                            ActivityIndicatorLoading.IsVisible = true;
                            ActivityIndicatorLoading.IsRunning = true;

                            var loginUserManager = new LoginUserManager();
                            var registerUser = await loginUserManager.Register(userName, email,
                                password, fullName);

                            if (registerUser != null)
                            {

                                if (registerUser.Errors != null)
                                {
                                    var errorMessages = registerUser.Errors.Aggregate("",
                                        (current, error) => current + (error.ErrorMessage + "\n"));

                                    await
                                        DisplayAlert(AppResources.ApplicationError, errorMessages, AppResources.AlertOk);

                                }
                                else
                                {
                                    await
                                        DisplayAlert(AppResources.ServerMessageSuccess,
                                            AppResources.RegisterAlertSuccessMessage, AppResources.AlertOk);

                                    var loginUser = new LoginUser
                                                    {
                                                        AuthToken = registerUser.AuthToken,
                                                        User = registerUser.User
                                                    };

                                    Application.Current.Properties["IsLoggedIn"] = true;
                                    Application.Current.Properties["LoginUser"] = loginUser;

                                    _iLoginManager.ShowMainPage(loginUser);
                                }
                            }

                            ActivityIndicatorLoading.IsVisible = false;
                            ActivityIndicatorLoading.IsRunning = false;
                        }

                        else
                        {
                            await DisplayAlert(AppResources.ApplicationValidationError,
                                AppResources.RegisterAlertValidationTermsAndConditions,
                                AppResources.AlertReTry);
                        }
                    }
                    else
                    {
                        await DisplayAlert(AppResources.ApplicationValidationError,
                            AppResources.RegisterAlertValidationPassAndConfirm,
                            AppResources.AlertReTry);
                    }
                }
                else
                {
                    await DisplayAlert(AppResources.ApplicationValidationError,
                        AppResources.RegisterAlertValidationEmail,
                        AppResources.AlertReTry);
                }
            }

        }
    }
}