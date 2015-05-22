using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Wall
{
    public class WallItemCell : ViewCell
    {

        public WallItemCell(ImageSource imageSourceDefault)
        {
            //var userImage = BeginApplication.Styles.DefaultWallIcon;
            var starImage = BeginApplication.Styles.RatinGoffIcon;

            var labelUserName = new Label
            {
                YAlign = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                Style = BeginApplication.Styles.ListTitleWallStyle,
                LineBreakMode = LineBreakMode.WordWrap,
            };
            labelUserName.SetBinding(Label.TextProperty, "DisplayName", stringFormat: "@ {0}");

            var labelDatePublic = new Label()
            {
                Style = BeginApplication.Styles.ListDescriptionWallStyle,
            };
            labelDatePublic.SetBinding(Label.TextProperty, "PublicDateShort");

            var gridUserAndTime = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition(){ Height = GridLength.Auto}
                }
            };
            gridUserAndTime.Children.Add(labelUserName, 0, 0);
            gridUserAndTime.Children.Add(labelDatePublic, 1, 0);

            var labelTitle = new Label
                             {
                                 YAlign = TextAlignment.End,
                                 //FontAttributes = FontAttributes.Bold,
                                 Style = BeginApplication.Styles.ListDescriptionWallStyle
                             };
            labelTitle.SetBinding(Label.TextProperty, "Title");

            var labelReason = new Label
                              {
                                  YAlign = TextAlignment.End,
                                  Style = BeginApplication.Styles.ListDescriptionWallStyle,
                                  LineBreakMode = LineBreakMode.WordWrap,
                              };
            labelReason.SetBinding(Label.TextProperty, "Reason");

            var labelDescription = new Label
                                   {
                                       YAlign = TextAlignment.End,
                                       Style = BeginApplication.Styles.ListDescriptionWallStyle,
                                       LineBreakMode = LineBreakMode.WordWrap,
                                   };

            labelDescription.SetBinding(Label.TextProperty, "Description");

            var labelDate = new Label
                            {
                                YAlign = TextAlignment.End,
                                Style = BeginApplication.Styles.ListDescriptionWallStyle
                            };

            labelDate.SetBinding(Label.TextProperty, "Date");

            var labelComment = new Label()
            {
                Text = "Comment(0)",
                Style =  BeginApplication.Styles.ListTitleWallStyle,
            };

            var labelDivider = new Label()
            {
                Text = " | ",
                Style = BeginApplication.Styles.ListTitleWallStyle,
                IsVisible = false
            };

            var labelDelete = new Label()
            {
                Text = "Delete",
                Style = BeginApplication.Styles.ListTitleWallStyle,
                IsVisible = false
            };

            var stackLayoutOptions = new StackLayout()
            {
                Children =
                {
                    labelComment, labelDivider, labelDelete
                }
            };

            var gridDetails = new Grid
                              {
                                  Padding = BeginApplication.Styles.WallPageGridRowListView,
                                  //HorizontalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  },
                                  ColumnDefinitions =
                                  {
                                      //new ColumnDefinition {Width = GridLength.Auto}
                                  }
                              };

            gridDetails.Children.Add(gridUserAndTime, 0 ,0);
            gridDetails.Children.Add(labelTitle, 0, 1);
            gridDetails.Children.Add(labelReason, 0, 2);
            gridDetails.Children.Add(labelDescription, 0, 3);
            gridDetails.Children.Add(stackLayoutOptions, 0, 4);

            var circleImageWall = new CircleImage
                                  {
                                      Style = BeginApplication.Styles.PageCircleImageCommon,
                                      Source = imageSourceDefault,
                                  };

            var imageStar = new Image
                            {
                                Aspect = Aspect.AspectFit,
                                VerticalOptions = LayoutOptions.Start,
                                Source = starImage
                            };


            var layoutStackItem = new StackLayout
                                  {
                                      //BackgroundColor = Color.White,
                                      Padding = BeginApplication.Styles.PageStandardThickness,
                                      Orientation = StackOrientation.Horizontal,
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      Children =
                                      {
                                          circleImageWall,
                                          gridDetails
                                      }
                                  };

            View = layoutStackItem;
        }
    }
}