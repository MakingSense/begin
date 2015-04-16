using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.GroupPages
{
    public class ProfileGroupItemCell : ViewCell
    {
        private const string GroupImage = "userdefault3.png";

        public ProfileGroupItemCell()
        {
            var circleGroupImage = new CircleImage
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

            var labelTitle = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 FontAttributes = FontAttributes.Bold,
                                 Style = BeginApplication.Styles.ListItemTextStyle
                             };

            labelTitle.SetBinding(Label.TextProperty, "Name");

            var labelDesc = new Label
                            {
                                YAlign = TextAlignment.Center,
                                Style = BeginApplication.Styles.ListItemDetailTextStyle
                            };

            labelDesc.SetBinding(Label.TextProperty, "Description");

            //Other section
            var labelStatus = new Label
                            {
                                YAlign = TextAlignment.Center,
                                HorizontalOptions = LayoutOptions.Start,
                                Style = BeginApplication.Styles.ListItemDetailTextStyle
                            };

            labelStatus.SetBinding(Label.TextProperty, "Status");

            var labelCreate = new Label
                            {
                                YAlign = TextAlignment.Center,
                                HorizontalOptions = LayoutOptions.End,
                                FontAttributes = FontAttributes.Bold,
                                Style = BeginApplication.Styles.ListItemTextStyle
                            };

            labelCreate.SetBinding(Label.TextProperty, "DateCreated");

            var stackLayoutPie = new StackLayout
                                 {
                                     Spacing = 2,
                                     Padding = BeginApplication.Styles.LayoutThickness,
                                     Orientation = StackOrientation.Horizontal,
                                     HorizontalOptions = LayoutOptions.FillAndExpand,
                                     Children =
                                     {
                                         labelStatus,
                                         labelCreate
                                     }
                                 };

            var stackLayoutCenter = new StackLayout
                                 {
                                     Spacing = 2,
                                     Padding = BeginApplication.Styles.LayoutThickness,
                                     Children =
                                     {
                                         labelTitle,
                                         labelDesc,
                                         stackLayoutPie
                                     }
                                 };

            var stackLayoutItem = new StackLayout
                           {
                               Spacing = 2,
                               Padding = BeginApplication.Styles.LayoutThickness,
                               Orientation = StackOrientation.Horizontal,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               Children =
                               {
                                   circleGroupImage,
                                   stackLayoutCenter
                               }
                           };

            View = stackLayoutItem;
        }
    }
}