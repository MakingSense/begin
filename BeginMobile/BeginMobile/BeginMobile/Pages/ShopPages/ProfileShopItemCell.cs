using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.ShopPages
{
    public class ProfileShopItemCell: ViewCell
    {
        private static string GroupImage
        {
            get
            {
                return Device.OS == TargetPlatform.iOS ? "userdefault.png" : "userdefault3.png";
            }
        }

        public ProfileShopItemCell()
        {
            var circleShopImage = new CircleImage
                                  {
                                      BorderColor =
                                          Device.OnPlatform(Color.Black, Color.White, Color.White),
                                      BorderThickness = Device.OnPlatform(2, 3, 3),
                                      HeightRequest = Device.OnPlatform(50, 100, 100),
                                      WidthRequest = Device.OnPlatform(50, 100, 100),

                                      Aspect = Aspect.AspectFill,
                                      HorizontalOptions = LayoutOptions.Start,
                                      Source = GroupImage
                                  };

            circleShopImage.SetBinding(CircleImage.SourceProperty, "Thumbnail");

            var labelTitle = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 FontAttributes = FontAttributes.Bold,
                                 Style = App.Styles.ListItemTextStyle

                             };

            labelTitle.SetBinding(Label.TextProperty, "Name");

            //Left section
            var labelCreateDateTitle = new Label
                                       {
                                           YAlign = TextAlignment.Center,
                                           Text = "Date:",
                                           FontAttributes = FontAttributes.Bold,
                                           Style = App.Styles.ListItemTextStyle,
                                           HorizontalOptions = LayoutOptions.Start
                                       };

            var labelCreate = new Label
            {
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle,
                HorizontalOptions = LayoutOptions.Center
            };

            labelCreate.SetBinding(Label.TextProperty, "CreationDate");

            //Right section
            var labelPriceTitle = new Label
                                  {
                                      YAlign = TextAlignment.Center,
                                      Text = "Price:",
                                      FontAttributes = FontAttributes.Bold,
                                      Style = App.Styles.ListItemTextStyle,
                                      HorizontalOptions = LayoutOptions.Start
                                  };

            var labelPrice = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 XAlign = TextAlignment.Center,
                                 Style = App.Styles.ListItemDetailTextStyle,
                                 HorizontalOptions = LayoutOptions.End
                             };

            labelPrice.SetBinding(Label.TextProperty, "Price");

            var gridDetails = new Grid
                              {
                                  Padding = new Thickness(10, 5, 10, 5),
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
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

            gridDetails.Children.Add(labelCreateDateTitle, 0, 0);
            gridDetails.Children.Add(labelCreate, 0, 1);

            gridDetails.Children.Add(labelPriceTitle, 1, 0);
            gridDetails.Children.Add(labelPrice, 1, 1);

            var stackLayoutCenter = new StackLayout
                                 {
                                     HorizontalOptions = LayoutOptions.FillAndExpand,
                                     Children =
                                     {
                                         labelTitle,
                                         gridDetails
                                     }
                                 };

            var stackLayoutItem = new StackLayout
                           {
                               Orientation = StackOrientation.Horizontal,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               Children =
                               {
                                   circleShopImage,
                                   stackLayoutCenter
                               }
                           };

            View = stackLayoutItem;
        }
    }
}