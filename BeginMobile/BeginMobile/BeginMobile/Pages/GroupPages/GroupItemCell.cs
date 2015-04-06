using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.GroupPages
{
    public class GroupItemCell : ViewCell
    {
        private const string GroupImage = "userdefault.png";
        public GroupItemCell()
        {
            StyleId = "Cell";
            var circleGroupImage = new CircleImage
            {
                BorderColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                BorderThickness = Device.OnPlatform(2, 3, 3),
                HeightRequest = Device.OnPlatform(40, 80, 80),
                WidthRequest = Device.OnPlatform(45, 80, 80),

                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Source = GroupImage
            };

            //First center column
            var labelGroupName = new Label
            {
                StyleId = "CellGroupName",
                YAlign = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                Style = App.Styles.ListItemTextStyle
            };

            labelGroupName.SetBinding(Label.TextProperty, "Name");

            var labelGroupCategory = new Label
            {
                StyleId = "CellGroupCategory",
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle
            };

            labelGroupCategory.SetBinding(Label.TextProperty, "Category");

            var stackLayoutLeft = new StackLayout
                                  {
                                      HorizontalOptions = LayoutOptions.Start,
                                      Children =
                {
                    labelGroupName, labelGroupCategory
                }
                                  };

            //Second center column
            var labelType = new Label
            {
                StyleId = "CellGroupType",
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle
            };

            labelType.SetBinding(Label.TextProperty, "Type");

            var labelMemebers = new Label
            {
                StyleId = "CellGroupMembers",
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle
            };

            labelMemebers.SetBinding(Label.TextProperty, "Members");


            var stackLayoutRight = new StackLayout
                                   {
                                       HorizontalOptions = LayoutOptions.CenterAndExpand,
                                       Children =
                                       {
                                           labelType,
                                           labelMemebers
                                       }
                                   };

            var stackLayoutCenter = new StackLayout
                                    {
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Orientation = StackOrientation.Horizontal,
                                        Children =
                                        {
                                            stackLayoutLeft,
                                            stackLayoutRight
                                        }
                                    };

            var imageArrow = new Image
                        {
                            StyleId = "CellArrow",
                            HorizontalOptions = LayoutOptions.End
                        };

            var layoutView = new StackLayout
                         {
                             Padding = new Thickness(5, 0, 5, 0),
                             Orientation = StackOrientation.Horizontal,
                             HorizontalOptions = LayoutOptions.FillAndExpand,
                             Children = { circleGroupImage, stackLayoutCenter, imageArrow }
                         };

            View = layoutView;
        }
    }
}