using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.GroupPages
{
    public class GroupItemCell: ViewCell
    {
        private const string GroupImage = "userdefault.png";
        public GroupItemCell()
        {
            //
            this.StyleId = "Cell";
            
            
            /*var groupImage = new Image
            {
                StyleId = "CellGroup",
                Source = FileImageSource.FromFile("Icon.png"),
                HorizontalOptions = LayoutOptions.Start};*/

            var groupImage = new CircleImage
            {
                BorderColor = Device.OnPlatform<Color>(iOS: Color.Black, Android: Color.White, WinPhone: Color.White),
                BorderThickness = Device.OnPlatform<int>(iOS: 2, Android: 3, WinPhone: 3),
                HeightRequest = Device.OnPlatform<int>(iOS: 40, Android: 80, WinPhone: 80),
                WidthRequest = Device.OnPlatform<int>(iOS: 45, Android: 80, WinPhone: 80),

                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Source = GroupImage
            };

            //First center column
            var lblGroupName = new Label
            {
                StyleId = "CellGroupName",
                YAlign = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                Style = App.Styles.ListItemTextStyle,
            };
            lblGroupName.SetBinding(Label.TextProperty, "Name");

            var lblGroupCategory = new Label
            {
                StyleId = "CellGroupCategory",
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle,
            };
            lblGroupCategory.SetBinding(Label.TextProperty, "Category");

            var stackLayoutLeft = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Start,
                Children =
                {
                    lblGroupName, lblGroupCategory
                }
            };

            //Second center column
            var lblType = new Label
            {
                StyleId = "CellGroupType",
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle,
            };
            lblType.SetBinding(Label.TextProperty, "Type");

            var lblMemebers = new Label
            {
                StyleId = "CellGroupMembers",
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle,
            };
            lblMemebers.SetBinding(Label.TextProperty, "Members");


            var stackLayoutRight = new StackLayout()
            {
                //HorizontalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    lblType, lblMemebers
                }
            };



            var stackLayoutCenter = new StackLayout()
            {
                //HorizontalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    stackLayoutLeft,
                    stackLayoutRight
                }
            };

            var arrow = new Image
            {
                StyleId = "CellArrow",
                //Source = FileImageSource.FromFile("Icon.png"),
                HorizontalOptions = LayoutOptions.End
            };

            var layout = new StackLayout
            {
                Padding = new Thickness(5, 0, 5, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {groupImage, stackLayoutCenter, arrow }
            };

            View = layout;
        }
    }
}
