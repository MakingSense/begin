using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Wall
{
    public class WallItemCell : ViewCell
    {

        public WallItemCell()
        {
            var userImage = BeginApplication.Styles.DefaultWallIcon;
            var starImage = BeginApplication.Styles.RatinGoffIcon;
            var labelTitle = new Label
                             {
                                 YAlign = TextAlignment.End,
                                 FontAttributes = FontAttributes.Bold,
                                 Style = BeginApplication.Styles.ListItemTextStyle
                             };
            labelTitle.SetBinding(Label.TextProperty, "Title");

            var labelReason = new Label
                              {
                                  YAlign = TextAlignment.End,
                                  Style = BeginApplication.Styles.ListItemTextStyle
                              };
            labelReason.SetBinding(Label.TextProperty, "Reason");

            var labelDescription = new Label
                                   {
                                       YAlign = TextAlignment.End,
                                       Style = BeginApplication.Styles.ListItemDetailTextStyle
                                   };

            labelDescription.SetBinding(Label.TextProperty, "Description");

            var labelDate = new Label
                            {
                                YAlign = TextAlignment.End,
                                Style = BeginApplication.Styles.ListItemDetailTextStyle
                            };

            labelDate.SetBinding(Label.TextProperty, "Date");

            var gridDetails = new Grid
                              {
                                  Padding = BeginApplication.Styles.GridOfListView,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  },
                                  ColumnDefinitions =
                                  {
                                      new ColumnDefinition {Width = GridLength.Auto}
                                  }
                              };

            gridDetails.Children.Add(labelTitle, 0, 0);
            gridDetails.Children.Add(labelReason, 0, 2);
            gridDetails.Children.Add(labelDescription, 0, 3);
            gridDetails.Children.Add(labelDate, 0, 4);

            var circleImageShop = new CircleImage
                                  {
                                      Style = BeginApplication.Styles.CircleImageCommon,
                                      Source = userImage
                                  };

            var imageStar = new Image
                            {
                                Aspect = Aspect.AspectFit,
                                VerticalOptions = LayoutOptions.Start,
                                Source = starImage
                            };


            var layoutStackItem = new StackLayout
                                  {
                                      Orientation = StackOrientation.Horizontal,
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      Children =
                                      {
                                          circleImageShop,
                                          gridDetails,
                                          imageStar
                                      }
                                  };

            View = layoutStackItem;
        }
    }
}