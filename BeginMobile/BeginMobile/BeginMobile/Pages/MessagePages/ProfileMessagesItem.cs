using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class ProfileMessagesItem : ViewCell
    {
        private static string GroupImage
        {
            get
            {
                return Device.OS == TargetPlatform.iOS ? "userdefault.png" : "userdefault3.png";
            }
        }

        public ProfileMessagesItem()
        {

            var circleShopImage = new CircleImage
                            {
                                BorderColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                                BorderThickness = Device.OnPlatform(2, 3, 3),
                                HeightRequest = Device.OnPlatform(50, 100, 100),
                                WidthRequest = Device.OnPlatform(50, 100, 100),
                                Aspect = Aspect.AspectFill,
                                HorizontalOptions = LayoutOptions.Start,
                                Source = GroupImage
                            };

            var labelTitle = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 Style = App.Styles.ListItemTextStyle,
                                 FontAttributes = FontAttributes.Bold,
                                 HorizontalOptions = LayoutOptions.Start
                             };

            labelTitle.SetBinding(Label.TextProperty, "Title");

            var labelCreate = new Label
                            {
                                YAlign = TextAlignment.Center,
                                Style = App.Styles.ListItemDetailTextStyle,
                                HorizontalOptions = LayoutOptions.End
                            };

            labelCreate.SetBinding(Label.TextProperty, "CreateDate", stringFormat: "Date: {0}");

            var labelContent = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 Style = App.Styles.ListItemDetailTextStyle,
                                 HorizontalOptions = LayoutOptions.StartAndExpand
                             };

            labelContent.SetBinding(Label.TextProperty, "Content");

            var gridDetails = new Grid
                              {
                                  Padding = new Thickness(10, 5, 10, 5),
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  },
                                  ColumnDefinitions =
                                  {
                                      new ColumnDefinition {Width = GridLength.Auto},
                                      new ColumnDefinition {Width = GridLength.Auto}
                                  }
                              };

            gridDetails.Children.Add(labelTitle, 0, 0);
            gridDetails.Children.Add(labelContent, 0, 1);

            gridDetails.Children.Add(labelCreate, 1, 0);

            var stackLayoutView = new StackLayout
                          {
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