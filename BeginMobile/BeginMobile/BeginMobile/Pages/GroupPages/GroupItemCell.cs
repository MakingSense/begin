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
                Style = BeginApplication.Styles.CircleImageCommon,
                Source = GroupImage
            };

            //First center column
            var labelGroupName = new Label
            {
                StyleId = "CellGroupName",
                YAlign = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                Style = BeginApplication.Styles.ListItemTextStyle
            };

            labelGroupName.SetBinding(Label.TextProperty, "Name");

            var labelGroupCategory = new Label
            {
                StyleId = "CellGroupCategory",
                YAlign = TextAlignment.Center,
                Style = BeginApplication.Styles.ListItemDetailTextStyle
            };

            labelGroupCategory.SetBinding(Label.TextProperty, "Category");

            var stackLayoutLeft = new StackLayout
                                  {
                                      HorizontalOptions = LayoutOptions.Start,
                                      Children =
                                      {
                                          labelGroupName,
                                          labelGroupCategory
                                      }
                                  };

            //Second center column
            var labelType = new Label
            {
                StyleId = "CellGroupType",
                YAlign = TextAlignment.Center,
                Style = BeginApplication.Styles.ListItemDetailTextStyle
            };

            labelType.SetBinding(Label.TextProperty, "Type");

            var labelMemebers = new Label
            {
                StyleId = "CellGroupMembers",
                YAlign = TextAlignment.Center,
                Style = BeginApplication.Styles.ListItemDetailTextStyle
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
                             Spacing = 2,
                             Padding = BeginApplication.Styles.LayoutThickness,
                             Orientation = StackOrientation.Horizontal,
                             HorizontalOptions = LayoutOptions.FillAndExpand,
                             Children = { circleGroupImage, stackLayoutCenter, imageArrow }
                         };

            View = layoutView;
        }
    }
}