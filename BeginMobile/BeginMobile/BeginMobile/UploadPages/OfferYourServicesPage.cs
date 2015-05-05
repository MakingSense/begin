using BeginMobile.Services.DTO;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.UploadPages
{
    public class OfferYourServices : ContentPage
    {
        private readonly StackLayout _mainStackLayout;

        public OfferYourServices()
        {
            var user = (LoginUser)BeginApplication.Current.Properties["LoginUser"];
            Style = BeginApplication.Styles.PageStyle;
            BackgroundColor = BeginApplication.Styles.UploadBackgroundColor;
            var labelServicesTitle = new Label
                                     {
                                         Text = "Offer your Services",
                                         Style = BeginApplication.Styles.TitleStyle,
                                         XAlign = TextAlignment.Center
                                     };

            var labelWhatDoYouDo = new Label
                                   {
                                       Text = "What do you do?",
                                       Style = BeginApplication.Styles.SubtitleStyle,
                                       XAlign = TextAlignment.Center
                                   };

            var imageCarier = new CircleImage
                              {
                                  Source = BeginApplication.Styles.DefaultWallIcon,
                                  Style = BeginApplication.Styles.CircleImageUpload
                              };

            

            var pickerCarier = new Picker
                               {
                                   Items =
                                   {
                                       "I'm  a Web Designer",
                                       "I'm  a Software Developer",
                                       "I'm  a Teacher",
                                       "I'm  a Painter",
                                       "I'm  a Student"
                                   },
                                   Style = BeginApplication.Styles.PickerStyle
                               };

            var buttonOkReady = new Button
                                {
                                    Text = "Ok, I'm Ready",
                                    Style = BeginApplication.Styles.LinkButton,
                                    FontSize = 16
                                };

            

            buttonOkReady.Clicked += (s, e) =>
            {
                BeginApplication.CurrentBeginApplication.ShowMainPage(user);
            };

            _mainStackLayout = new StackLayout
                      {
                          HorizontalOptions = LayoutOptions.Center,
                          BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor,
                          Children =
                          {
                              labelServicesTitle,
                              labelWhatDoYouDo,
                              imageCarier,
                              pickerCarier,
                              buttonOkReady
                          }
                      };
            _mainStackLayout.Padding = new Thickness(10,30, 10, 10);
            Content = _mainStackLayout;

            //SizeChanged +=(sender,e)=> ChangePadding(this);

        }
        private void ChangePadding(Page page)
        {
            _mainStackLayout.Padding = page.Width > page.Height
                ? new Thickness(page.Width * 0.01, page.Height * 0.15, page.Width * 0.01, page.Height * 0.01)
                : new Thickness(page.Width * 0.01, page.Height * 0.25, page.Width * 0.01, page.Height * 0.01);
        }
    }
}