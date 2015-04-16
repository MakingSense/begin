using BeginMobile.LocalizeResources.Resources;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class ProfileMessagesItem : ViewCell
    {
        private static string GroupImage
        {
            get { return "userdefault3.png"; }
        }

        public ProfileMessagesItem()
        {
            var circleShopImage = new CircleImage
                                  {
                                      BorderColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                                      BorderThickness = Device.OnPlatform(2, 3, 3),
                                      HeightRequest = Device.OnPlatform(50, 100, 100),
                                      WidthRequest = Device.OnPlatform(50, 100, 100),
                                      Aspect = Aspect.AspectFit,
                                      HorizontalOptions = LayoutOptions.Start,
                                      Source = GroupImage
                                  };

            var labelSender = new Label
                              {
                                  YAlign = TextAlignment.Center,
                                  Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                  HorizontalOptions = LayoutOptions.StartAndExpand
                              };

            labelSender.SetBinding(Label.TextProperty, "SenderName", stringFormat: "From: {0}");

            var labelSubject = new Label
                               {
                                   YAlign = TextAlignment.Center,
                                   Style = BeginApplication.Styles.ListItemTextStyle,
                                   FontAttributes = FontAttributes.Bold,
                                   HorizontalOptions = LayoutOptions.Start
                               };
            labelSubject.SetBinding(Label.TextProperty, "Subject");

            var labelCreate = new Label
                              {
                                  YAlign = TextAlignment.Center,
                                  Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                  HorizontalOptions = LayoutOptions.End
                              };

            labelCreate.SetBinding(Label.TextProperty, "DateSent", stringFormat: "Date: {0}");

            var labelContent = new Label
                               {
                                   YAlign = TextAlignment.Center,
                                   Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                   HorizontalOptions = LayoutOptions.StartAndExpand
                               };
            labelContent.SetBinding(Label.TextProperty, "MessageContent");

            //var buttonRemove = new Label
            //                   {
            //                       Text = AppResources.ButtonRemoveFriend,
            //                       YAlign = TextAlignment.Center,
            //                       Style = BeginApplication.Styles.ListViewItemButton,
            //                   };

            var gridDetails = new Grid
                              {
                                  Padding = new Thickness(10, 5, 10, 5),
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  }
                              };
            gridDetails.Children.Add(labelSender, 0, 0);
            gridDetails.Children.Add(labelSubject, 0, 1);
            gridDetails.Children.Add(labelContent, 0, 2);
            gridDetails.Children.Add(labelCreate, 0, 3);
            //gridDetails.Children.Add(buttonRemove, 0, 4);
            var stackLayoutView = new StackLayout
                                  {
                                      Spacing = 2,
                                      Padding = BeginApplication.Styles.LayoutThickness,
                                      Orientation = StackOrientation.Horizontal,
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      Children =
                                      {
                                          circleShopImage,
                                          gridDetails
                                      }
                                  };

            View = stackLayoutView;
        }
    }
}