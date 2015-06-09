using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using BeginMobile.Interfaces;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.ManagerServices;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
    public class LoginMenu : BaseContentPage
	{

        private readonly ScrollView _mainScrollView;

        readonly ContentPage _contentPageLogin;
        readonly ContentPage _contentPageRegister;
        readonly ContentPage _contentPageForgotPassword;

        public LoginMenu(ILoginManager iloginManager)
        {
            //BackgroundImage = "login_background.png";
            var backgroundImage = new Image
            {
                Source = ImageSource.FromFile(BeginApplication.Styles.DefaultLoginBackgroundImage),
                Aspect = Aspect.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            _contentPageLogin = new Login(iloginManager);
            _contentPageRegister = new Register(iloginManager);
            _contentPageForgotPassword = new ForgotPassword();

            Style = BeginApplication.Styles.InitialPageStyle;

            Title = "";

            var logo = new CircleImage
                       {
                           Source = BeginApplication.Styles.LogoIcon,
                           Style = BeginApplication.Styles.CircleImageLogo,
                           HorizontalOptions = LayoutOptions.CenterAndExpand
                       };
            var tapGestureRecognizer = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };

            var buttonForgotPassword = new Label
                                       {
                                           Text = AppResources.ButtonForgotPassword,
                                           XAlign = TextAlignment.Center,
                                           YAlign = TextAlignment.Start,
                                           VerticalOptions = LayoutOptions.Start,
                                           HorizontalOptions = LayoutOptions.Center,
                                           FontFamily = BeginApplication.Styles.FontFamilyRobotoRegular,
                                           //FontSize = BeginApplication.Styles.TextFontSizeMedium
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
                                  Text = "Log in with User Name", //AppResources.ButtonLogin,
                                  Style = BeginApplication.Styles.DefaultButton,
                                  FontSize = 16
                              };

            var buttonRegister = new Button
                                 {
                                     Text = "Create Account", //AppResources.ButtonRegister,
                                     Style = BeginApplication.Styles.DefaultButton,
                                     BackgroundColor = BeginApplication.Styles.ColorBrown,
                                     VerticalOptions = LayoutOptions.End,
                                     TextColor = BeginApplication.Styles.ColorWhiteBackground,
                                     FontSize = 16
                                 };

            tapGestureRecognizer.Tapped += async (sender, eventArgs) =>
            {
                await Navigation.PushAsync(_contentPageForgotPassword);
            };

            buttonLogin.Clicked += async (sender, eventArgs) =>
            {
                await Navigation.PushAsync(_contentPageLogin);
            };

            buttonRegister.Clicked += async (sender, eventArgs) =>
            {
                await Navigation.PushAsync(_contentPageRegister);
            };

            _mainScrollView = new ScrollView();
        

        var mainGrid = new Grid
            {
                Padding = BeginApplication.Styles.InitialPagesThickness,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                RowDefinitions = new RowDefinitionCollection
                                 {
                                     new RowDefinition{ Height = new GridLength(140,GridUnitType.Absolute)},
                                     new RowDefinition{ Height = new GridLength(130,GridUnitType.Absolute)},
                                     new RowDefinition{ Height = GridLength.Auto},
                                     new RowDefinition{ Height = new GridLength(1,GridUnitType.Absolute)},
                                     new RowDefinition{ Height = 45},
                                     new RowDefinition{ Height = GridLength.Auto},
                                 }                                 
            };

            mainGrid.Children.Add(logo,0,0);
            mainGrid.Children.Add(buttonLogin,0,2);
            mainGrid.Children.Add(buttonForgotPassword,0,4);
            mainGrid.Children.Add(buttonRegister,0,5);

            var relativeLayout = new RelativeLayout ();            
            relativeLayout.Children.Add(backgroundImage,
               xConstraint: Constraint.Constant(0),
               yConstraint: Constraint.Constant(0),
               widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
               heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            relativeLayout.Children.Add(mainGrid,
              xConstraint: Constraint.Constant(0),
              yConstraint: Constraint.Constant(0),
              widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
              heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));
  
            _mainScrollView.Content = relativeLayout;
            Content = _mainScrollView;
            //SizeChanged += (sender, e) => SetOrientation(this);
		}
        public void SetOrientation(Page page)
        {
            if (_mainScrollView != null)
                _mainScrollView.Padding = page.Width > page.Height //width > Height landscape else portrait
                    ? new Thickness(page.Width * 0.02, 0, page.Width * 0.02, 0)
                    : new Thickness(page.Width * 0.02, 0, page.Width * 0.02, 0); // top = 0 =>  page.Height * 0.20
        }
	}
}
