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
                                      Source =  BeginApplication.Styles.DefaultShopIcon
                                  };

            circleShopImage.SetBinding(CircleImage.SourceProperty, "Thumbnail");

            var labelTitle = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 Style = BeginApplication.Styles.ListItemTextStyle
                             };

            labelTitle.SetBinding(Label.TextProperty, "Name");

            //Left section
            var labelCreateDateTitle = new Label
                                       {
                                           YAlign = TextAlignment.Center,
                                           Text = AppResources.LabelShopDate,
                                           Style = BeginApplication.Styles.ListItemTextStyle,
                                           HorizontalOptions = LayoutOptions.Start
                                       };

            var labelCreate = new Label
                              {
                                  YAlign = TextAlignment.Center,
                                  Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                  HorizontalOptions = LayoutOptions.Start
                              };

            labelCreate.SetBinding(Label.TextProperty, "CreationDate");

            var labelStatus = new Label
            {
                YAlign = TextAlignment.Center,
                XAlign = TextAlignment.Start,
                Style = BeginApplication.Styles.ListItemDetailTextStyle,
                HorizontalOptions = LayoutOptions.Start
            };

            labelStatus.SetBinding(Label.TextProperty, "Status");

            //Right section
            var labelPriceTitle = new Label
                                  {
                                      YAlign = TextAlignment.Center,
                                      Text = AppResources.LabelShopPrice,
                                      Style = BeginApplication.Styles.ListItemTextStyle,
                                      HorizontalOptions = LayoutOptions.Start
                                  };

            var labelPrice = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 XAlign = TextAlignment.Start,
                                 Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                 HorizontalOptions = LayoutOptions.Start
                             };

            labelPrice.SetBinding(Label.TextProperty, "Price",stringFormat:"{0} u$s");

            var gridDetails = new Grid
                              {
                                  Padding = BeginApplication.Styles.ThicknessBetweenImageAndDetails,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  },
                                  //ColumnDefinitions =
                                  //{
                                  //    new ColumnDefinition {Width = GridLength.Auto},
                                  //    new ColumnDefinition {Width = GridLength.Auto}
                                  //}
                              };

           // gridDetails.Children.Add(labelCreateDateTitle, 0, 0);
            gridDetails.Children.Add(labelTitle, 0, 0);
            gridDetails.Children.Add(labelStatus, 0, 1);
            gridDetails.Children.Add(labelPrice, 0, 2);


            var stackLayoutItem = new StackLayout
                                  {                  
                                      Padding = BeginApplication.Styles.ThicknessInsideListView,
                                      Orientation = StackOrientation.Horizontal,
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      Children =
                                      {
                                          circleShopImage,
                                          gridDetails
                                      }
                                  };

            View = stackLayoutItem;
        }
    }
}