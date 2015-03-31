using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.GroupPages
{
    public class MemberItemCell : ViewCell
    {
        private const string GroupImage = "userprofile.png";
        public MemberItemCell()
        {
            var memberImage = new CircleImage
            {
                BorderColor = Device.OnPlatform<Color>(iOS: Color.Black, Android: Color.White, WinPhone: Color.White),
                BorderThickness = Device.OnPlatform<int>(iOS: 2, Android: 3, WinPhone: 3),
                HeightRequest = Device.OnPlatform<int>(iOS: 40, Android: 80, WinPhone: 80),
                WidthRequest = Device.OnPlatform<int>(iOS: 45, Android: 80, WinPhone: 80),

                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Source = GroupImage
            };

            var gridListRow = new Grid()
            {
                HorizontalOptions =  LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition{Height = GridLength.Auto},
                    new RowDefinition{Height = GridLength.Auto}
                }
            };

            var lblName= new Label()
            {
                YAlign = TextAlignment.End,
                Style = App.Styles.ListItemTextStyle,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start,
            };
            lblName.SetBinding(Label.TextProperty,  "NameUsername");

            var lblEmail= new Label()
            {
                YAlign = TextAlignment.End,
                Style = App.Styles.ListItemTextStyle,
                HorizontalOptions = LayoutOptions.Start
            };
            lblEmail.SetBinding(Label.TextProperty,  "Email");

            gridListRow.Children.Add(lblName, 0, 0);
            gridListRow.Children.Add(lblEmail, 0, 1);

            var stackLayoutRow = new StackLayout
            {
                Padding = new Thickness(10, 5, 10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {memberImage, gridListRow}
            };
            

            View = stackLayoutRow;
        }
    }
}
