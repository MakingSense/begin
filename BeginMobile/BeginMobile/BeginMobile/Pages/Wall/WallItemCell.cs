using ImageCircle.Forms.Plugin.Abstractions;
using System;
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
                YAlign = TextAlignment.Start,
                //FontAttributes = FontAttributes.Bold,
                Style = BeginApplication.Styles.ListTitleWallStyle,
                LineBreakMode = LineBreakMode.WordWrap,
            };
            labelUserName.SetBinding(Label.TextProperty, "DisplayName", stringFormat: "@{0}");

            var labelDatePublic = new Label()
            {
                Style = BeginApplication.Styles.ListDescriptionWallStyle,
                XAlign = TextAlignment.End,
                YAlign = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.End,
            };
            labelDatePublic.SetBinding(Label.TextProperty, "PublicDateShort");

            var gridUserAndTime = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition(){ Height = GridLength.Auto}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(){ Width = GridLength.Auto},
                    new ColumnDefinition(){ Width = new GridLength (50, GridUnitType.Absolute)},
                }
            };

            /*var stackLayoutTime = new StackLayout()
            {
                //
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Children =
                {
                    labelDatePublic
                }
            };

            var relativeLayoutTitle = new RelativeLayout();

            var label = new Label() {
                Text = "I want to be right-aligned.",
                WidthRequest = 100,
            };
            Func<RelativeLayout, double> getLabelWidth = (parent) => labelDatePublic.GetSizeRequest(relativeLayoutTitle.Width, relativeLayoutTitle.Height).Request.Width;
            relativeLayoutTitle.Children.Add(labelDatePublic,
                Constraint.RelativeToParent((rl) => rl.Width - getLabelWidth(rl)),
                Constraint.Constant(10));

            var button = new Button() {
                Text = "Invalidate"
            };
            button.Clicked += (object sender, EventArgs e) => relativeLayoutTitle.ForceLayout();
            relativeLayoutTitle.Children.Add(labelUserName,
                Constraint.Constant(10),
                Constraint.Constant(10));*/


            gridUserAndTime.Children.Add(labelUserName, 0, 0);
            gridUserAndTime.Children.Add(labelDatePublic, 1, 0);

            var labelTitle = new Label
                             {
                                 YAlign = TextAlignment.Start,
                                 //FontAttributes = FontAttributes.Bold,
                                 Style = BeginApplication.Styles.ListDescriptionWallStyle,
                             };
            labelTitle.SetBinding(Label.TextProperty, "Title");
            labelTitle.SetBinding(Label.IsVisibleProperty, "PublicTitle");

            var labelReason = new Label
                              {
                                  YAlign = TextAlignment.Start,
                                  Style = BeginApplication.Styles.ListDescriptionWallStyle,
                                  LineBreakMode = LineBreakMode.WordWrap,
                              };
            labelReason.SetBinding(Label.TextProperty, "Reason");

            var labelDescription = new Label
                                   {
                                       YAlign = TextAlignment.Start,
                                       Style = BeginApplication.Styles.ListDescriptionWallStyle,
                                       LineBreakMode = LineBreakMode.WordWrap,
                                   };

            labelDescription.SetBinding(Label.TextProperty, "Description");

            var labelDate = new Label
                            {
                                YAlign = TextAlignment.Start,
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

            gridDetails.Children.Add(gridUserAndTime, 0, 0);
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
                                      Padding = BeginApplication.Styles.PageStandardListThickness,
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