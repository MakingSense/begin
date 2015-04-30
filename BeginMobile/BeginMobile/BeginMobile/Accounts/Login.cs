using System;
using System.Linq;
using BeginMobile.Interfaces;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.Interfaces;
//using BeginMobile.Services.Logging;
using BeginMobile.Services.Logging;
using BeginMobile.Services.ManagerServices;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
    public class Login : BaseContentPage
    {
        private readonly Entry _entryEmail;
        private readonly Entry _entryPassword;
        private readonly ILoggingService _log = Logger.Current;
        private StackLayout _landscapeStackLayout;
        private StackLayout _portraitStackLayout;
        private readonly StackLayout _componentsLayout;

        public Login(ILoginManager iLoginManager)
        {
            var logo = new Image
                       {
                           Source = ImageSource.FromFile("logotype.png"),
                           Aspect = Aspect.AspectFit
                       };

            Title = AppResources.LoginFormTitle;

            _entryEmail = new Entry
                          {
                              Placeholder = AppResources.UsernamePlaceholder,
                              Keyboard = Keyboard.Email
                          };

            _entryPassword = new Entry
                             {
                                 Placeholder = AppResources.PasswordPlaceholder,
                                 IsPassword = true
                             };

            var buttonForgotPassword = new Button
                                       {
                                           Text = AppResources.ButtonForgotPassword,
                                           Style = BeginApplication.Styles.LinkButton,
                                       };
            var buttonLoginWithFacebook = new Button
                                          {
                                              Text = "Login with Facebook", //AppResources.ButtonLoginWithFacebook,
                                              Style = BeginApplication.Styles.DefaultButton
                                          };
            var buttonLogin = new Button
                              {
                                  Text = "Login with Email", //AppResources.ButtonLogin,
                                  Style = BeginApplication.Styles.DefaultButton
                              };

            var buttonRegister = new Button
                                 {
                                     Text = "Create account", //AppResources.ButtonRegister,
                                     Style = BeginApplication.Styles.DefaultButton
                                 };

            buttonForgotPassword.Clicked +=
                (sender, eventArgs) => MessagingCenter.Send<ContentPage>(this, "ForgotPassword");


            buttonLogin.Clicked += async (sender, eventArgs) =>
                                         {
                                             if (String.IsNullOrEmpty(_entryEmail.Text) ||
                                                 String.IsNullOrEmpty(_entryPassword.Text))
                                             {
                                                 await
                                                     DisplayAlert(AppResources.ApplicationValidationError,
                                                         AppResources.LoginAlertValidationUsernameAndPass,
                                                         AppResources.AlertReTry);
                                             }

                                             else
                                             {
                                                 ActivityIndicatorLoading.IsVisible = true;
                                                 ActivityIndicatorLoading.IsRunning = true;

                                                 var loginUserManager = new LoginUserManager();
                                                 var loginUser =
                                                     await loginUserManager.Login(_entryEmail.Text, _entryPassword.Text);

                                                 if (loginUser != null)
                                                 {
                                                     if (loginUser.Errors != null)
                                                     {
                                                         var errorMessage = loginUser.Errors.Aggregate("",
                                                             (current, error) => current + (error.ErrorMessage + "\n"));
                                                         await
                                                             DisplayAlert(AppResources.LoginAlertValidationError,
                                                                 errorMessage,
                                                                 AppResources.AlertReTry);
                                                     }

                                                     else
                                                     {
                                                         Application.Current.Properties["IsLoggedIn"] = true;
                                                         Application.Current.Properties["LoginUser"] = loginUser;

                                                         iLoginManager.ShowMainPage(loginUser);
                                                     }
                                                 }

                                                 ActivityIndicatorLoading.IsVisible = false;
                                                 ActivityIndicatorLoading.IsRunning = false;
                                             }
                                         };

            buttonRegister.Clicked += (sender, eventArgs) => MessagingCenter.Send<ContentPage>(this, "Register");

            //var buttonLog = new Button()
            //{
            //    Text = "Log",
            //    Style = BeginApplication.Styles.DefaultButton
            //};

            //buttonLog.Clicked += delegate(object sender, EventArgs args)
            //{
            //    _log.Info("Clicked Log Info");
            //    _log.Warning("Clicked Log Warning");
            //    _log.Error("Clicked Log Error");
            //    _log.DebugFormat("Clicked Log Debug {0}", "hi");
            //};

            var stackLayoutLoading = CreateStackLayoutWithLoadingIndicator();

            _componentsLayout = new StackLayout
                                {
                                    VerticalOptions = LayoutOptions.FillAndExpand,
                                    Spacing = 10,
                                    Children =
                                    {
                                        stackLayoutLoading,
                                        logo,
                                        _entryEmail,
                                        _entryPassword,
                                       // buttonLoginWithFacebook,
                                        buttonLogin,
                                        buttonForgotPassword,
                                        buttonRegister
                                    }
                                };

            SizeChanged += (sender, e) => SetOrientation(this);
        }

        public void SetOrientation(Page page)
        {
            _landscapeStackLayout = new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.FillAndExpand,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Padding = new Thickness(page.Width*0.02, 0, page.Width*0.02, 0),
                                    };

            _portraitStackLayout = new StackLayout
                                   {
                                       VerticalOptions = LayoutOptions.FillAndExpand,
                                       HorizontalOptions = LayoutOptions.FillAndExpand,
                                       Padding = new Thickness(page.Width*0.02, page.Height*0.25, page.Width*0.02, 0),
                                   };
            if (page.Width > page.Height)
            {
                _landscapeStackLayout.Children.Add(_componentsLayout);
                Content = new ScrollView
                          {
                              Content = _landscapeStackLayout
                          };
            }
            else
            {
                _portraitStackLayout.Children.Add(_componentsLayout);
                Content = new ScrollView
                          {
                              Content = _portraitStackLayout
                          };
            }
        }
    }
}