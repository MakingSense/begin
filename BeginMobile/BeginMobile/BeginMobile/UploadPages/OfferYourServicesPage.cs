using BeginMobile.Services.DTO;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.UploadPages
{
    public class OfferYourServices : ContentPage
    {
        public OfferYourServices()
        {
            var user = (LoginUser)BeginApplication.Current.Properties["LoginUser"];
            var labelServicesTitle = new Label
                                     {
                                         Text = "Offer your Services",
                                         Style = BeginApplication.Styles.TitleStyle
                                     };

            var labelWhatDoYouDo = new Label
                                   {
                                       Text = "What do you do?",
                                       Style = BeginApplication.Styles.SubtitleStyle
                                   };

            var imageCarier = new CircleImage
                              {
                                  Source = BeginApplication.Styles.DefaultWallIcon,
                                  Style = BeginApplication.Styles.CircleImageCommon,
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
                                   }
                               };

            var buttonOkReady = new Button
                                {
                                    Text = "Ok, I'm Ready",
                                    BackgroundColor = Color.Transparent
                                };

            

            buttonOkReady.Clicked += (s, e) =>
            {
                BeginApplication.CurrentBeginApplication.ShowMainPage(user);
            };

            Content = new StackLayout
                      {
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
        }
    }
}