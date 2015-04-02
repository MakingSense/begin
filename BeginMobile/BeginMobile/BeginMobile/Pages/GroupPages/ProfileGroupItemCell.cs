using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.GroupPages
{
    public class ProfileGroupItemCell : ViewCell
    {
        private const string GroupImage = "userdefault3.png";

        public ProfileGroupItemCell()
        {
            var groupImage = new CircleImage
                             {
                                 BorderColor =
                                     Device.OnPlatform(Color.Black, Color.White, Color.White),
                                 BorderThickness = Device.OnPlatform(2, 3, 3),
                                 HeightRequest = Device.OnPlatform(40, 80, 80),
                                 WidthRequest = Device.OnPlatform(45, 80, 80),

                                 Aspect = Aspect.AspectFill,
                                 HorizontalOptions = LayoutOptions.Start,
                                 Source = GroupImage
                             };

            var lblGrTitle = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 FontAttributes = FontAttributes.Bold,
                                 Style = App.Styles.ListItemTextStyle
                             };

            lblGrTitle.SetBinding(Label.TextProperty, "Name");

            var lblGrDesc = new Label
                            {
                                YAlign = TextAlignment.Center,
                                Style = App.Styles.ListItemDetailTextStyle
                            };

            lblGrDesc.SetBinding(Label.TextProperty, "Description", stringFormat: "Description: {0}");

            //Other section
            var lblStatus = new Label
                            {
                                YAlign = TextAlignment.Center,
                                HorizontalOptions = LayoutOptions.Start,
                                Style = App.Styles.ListItemDetailTextStyle
                            };

            lblStatus.SetBinding(Label.TextProperty, "Status");

            var lblCreate = new Label
                            {
                                YAlign = TextAlignment.Center,
                                HorizontalOptions = LayoutOptions.End,
                                FontAttributes = FontAttributes.Bold,
                                Style = App.Styles.ListItemTextStyle
                            };

            lblCreate.SetBinding(Label.TextProperty, "DateCreated");

            var stackLayoutPie = new StackLayout
                                 {
                                     Orientation = StackOrientation.Horizontal,
                                     HorizontalOptions = LayoutOptions.FillAndExpand,
                                     Children =
                                     {
                                         lblStatus,
                                         lblCreate
                                     }
                                 };

            var stackLayCenter = new StackLayout
                                 {
                                     Children =
                                     {
                                         lblGrTitle,
                                         lblGrDesc,
                                         stackLayoutPie
                                     }
                                 };

            var sLayItem = new StackLayout
                           {
                               Orientation = StackOrientation.Horizontal,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               Children =
                               {
                                   groupImage,
                                   stackLayCenter
                               }
                           };

            View = sLayItem;

        }
    }
}