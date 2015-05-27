using BeginMobile.LocalizeResources.Resources;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.ShopPages
{
    public class ProfileShopItemCell : ViewCell
    {
        private static string GroupImage
        {
            get { return BeginApplication.Styles.DefaultShopIcon; }
        }

        public ProfileShopItemCell()
        {
            var circleShopImage = new CircleImage
                                  {
                                      Style = BeginApplication.Styles.CircleImageCommon,
                                      //Source = GroupImage
                                  };

            circleShopImage.SetBinding(CircleImage.SourceProperty, "Thumbnail");

            var labelTitle = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 FontAttributes = FontAttributes.Bold,
                                 Style = BeginApplication.Styles.ListItemTextStyle
                             };

            labelTitle.SetBinding(Label.TextProperty, "Name");

            //Left section
            var labelCreateDateTitle = new Label
                                       {
                                           YAlign = TextAlignment.Center,
                                           Text = AppResources.LabelShopDate,
                                           FontAttributes = FontAttributes.Bold,
                                           Style = BeginApplication.Styles.ListItemTextStyle,
                                           HorizontalOptions = LayoutOptions.Start
                                       };

            var labelCreate = new Label
                              {
                                  YAlign = TextAlignment.Center,
                                  Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                  HorizontalOptions = LayoutOptions.Center
                              };

            labelCreate.SetBinding(Label.TextProperty, "CreationDate");

            //Right section
            var labelPriceTitle = new Label
                                  {
                                      YAlign = TextAlignment.Center,
                                      Text = AppResources.LabelShopPrice,
                                      FontAttributes = FontAttributes.Bold,
                                      Style = BeginApplication.Styles.ListItemTextStyle,
                                      HorizontalOptions = LayoutOptions.Start
                                  };

            var labelPrice = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 XAlign = TextAlignment.Center,
                                 Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                 HorizontalOptions = LayoutOptions.End
                             };

            labelPrice.SetBinding(Label.TextProperty, "Price");

            var gridDetails = new Grid
                              {
                                  Padding = BeginApplication.Styles.ThicknessInsideListView,
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
                                        Padding = BeginApplication.Styles.ThicknessBetweenImageAndDetails,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Children =
                                        {
                                            labelTitle,
                                            gridDetails
                                        }
                                    };

            var stackLayoutItem = new StackLayout
                                  {                  
                                      Padding = BeginApplication.Styles.ThicknessInsideListView,
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