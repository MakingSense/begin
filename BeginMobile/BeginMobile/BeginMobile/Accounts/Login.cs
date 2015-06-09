using System;
using System.Linq;
using BeginMobile.Interfaces;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.Interfaces;
//using BeginMobile.Services.Logging;
using BeginMobile.Services.Logging;
using BeginMobile.Services.ManagerServices;
using Xamarin.Forms;
using BeginMobile.Services.DTO;

namespace BeginMobile.Accounts
{
    public class Login : BaseContentPage
    {
        private readonly Entry _entryEmail;
        private readonly Entry _entryPassword;
        private readonly ILoggingService _log = Logger.Current;
        private readonly ScrollView _mainScrollView;

        readonly ContentPage _contentPageForgotPassword;

        public Login(ILoginManager iLoginManager)
        {
            //BackgroundImage = "login_background.png";
            _contentPageForgotPassword = new ForgotPassword();

            Style = BeginApplication.Styles.InitialPageStyle;          
            var mainTitle = new Label
                            {
                                Text = "Log In",//AppResources.LoginFormTitle,
                                Style = BeginApplication.Styles.TitleStyle,
                                XAlign = TextAlignment.Center,
                                YAlign = TextAlignment.Center
                            };
            Title = mainTitle.Text;

            _entryEmail = new Entry
                          {
                              Placeholder = AppResources.UsernamePlaceholder,
                              Keyboard = Keyboard.Email,
                              Style = BeginApplication.Styles.EntryStyle,
                          };

            _entryPassword = new Entry
                             {
                                 Placeholder = AppResources.PasswordPlaceholder,
                                 IsPassword = true,
                                 Style = BeginApplication.Styles.EntryStyle
                             };
            var tapGestureRecognizer = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };
            tapGestureRecognizer.Tapped += async (sender, eventArgs) =>
            {
                await Navigation.PushAsync(_contentPageForgotPassword);
            };
            var buttonForgotPassword = new Label
                                       {
                                           Text = AppResources.ButtonForgotPassword,
                                           XAlign = TextAlignment.Center,
                                           YAlign = TextAlignment.Start,
                                           VerticalOptions = LayoutOptions.End,
                                           HorizontalOptions = LayoutOptions.Center,
                                           FontFamily = BeginApplication.Styles.FontFamilyRobotoRegular,
                                           FontSize = 16
                                       };
            buttonForgotPassword.GestureRecognizers.Add(tapGestureRecognizer);

            var buttonLoginWithFacebook = new Button
                                          {
                                              Text = "Login with Facebook", //AppResources.ButtonLoginWithFacebook,
                                              Style = BeginApplication.Styles.DefaultButton
                                          };
            var buttonLogin = new Button
                              {
                                  Text = "Log In", //AppResources.ButtonLogin,
                                  Style = BeginApplication.Styles.DefaultButton,
                                  FontSize = 16
                              };

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

                                                         //iLoginManager.ShowMainPage(loginUser);
                                                         iLoginManager.ShowUploaderPage();
                                                     }
                                                 }
                                                 
                                                 ActivityIndicatorLoading.IsVisible = false;
                                                 ActivityIndicatorLoading.IsRunning = false;
                                             }
                                         };

            var stackLayoutLoading = CreateStackLayoutWithLoadingIndicator();
            _mainScrollView = new ScrollView();
            var componentsLayout = new Grid
                                   {
                                       VerticalOptions = LayoutOptions.FillAndExpand,
                                       Padding = BeginApplication.Styles.InitialPagesThickness,
                                       RowDefinitions = new RowDefinitionCollection
                                                        {
                                                            new RowDefinition {Height = GridLength.Auto},
                                                            new RowDefinition {Height = GridLength.Auto},
                                                            new RowDefinition {Height = GridLength.Auto},
                                                            new RowDefinition{ Height = new GridLength(15,GridUnitType.Absolute)}, 
                                                            new RowDefinition {Height = GridLength.Auto},   
                                                            new RowDefinition {Height = 30}
                                                        }
                                   };
            componentsLayout.Children.Add(stackLayoutLoading, 0, 0);
            componentsLayout.Children.Add(_entryEmail, 0, 1);
            componentsLayout.Children.Add(_entryPassword, 0, 2);
            componentsLayout.Children.Add(buttonLogin, 0, 4);
            componentsLayout.Children.Add(buttonForgotPassword, 0, 5);

            var backgroundImage = new Image
            {
                Source = ImageSource.FromFile(BeginApplication.Styles.DefaultLoginBackgroundImage),
                Aspect = Aspect.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            var relativeLayout = new RelativeLayout();
            relativeLayout.Children.Add(backgroundImage,
               xConstraint: Constraint.Constant(0),
               yConstraint: Constraint.Constant(0),
               widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
               heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            relativeLayout.Children.Add(componentsLayout,
              xConstraint: Constraint.Constant(0),
              yConstraint: Constraint.Constant(0),
              widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
              heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));
            _mainScrollView.Content = relativeLayout;
            Content = _mainScrollView;
           // SizeChanged += (sender, e) => SetOrientation(this);
        }

        private void LoginUserMock(ILoginManager iLoginManagerMock)
        {
            var loginUserMock = new LoginUser()
            {
                AuthToken = "8d16fcbd56cdafde0fc1ba4e68c4636c",
                User = new User()
                {
                    Avatar = "",
                    DisplayName = "Yoe",
                    Description = "Desc",
                    Email = "yoe@gmail.com",
                    FirstName = "Yoe",
                    NameSurname = "Yoe",
                    NameUsername = "Yoe",
                    NickName = "Yoe",
                }
            };

             Application.Current.Properties["IsLoggedIn"] = true;
             Application.Current.Properties["LoginUser"] = loginUserMock;

            iLoginManagerMock.ShowMainPage(loginUserMock);
        }

        public void SetOrientation(Page page)
        {
            if (_mainScrollView != null)
                _mainScrollView.Padding = page.Width > page.Height //width > Height landscape else portrait
                    ? new Thickness(page.Width * 0.02, 0, page.Width * 0.02, 0)
                    : new Thickness(page.Width*0.02, page.Height*0.20, page.Width*0.02, 0);
        }
    }
}