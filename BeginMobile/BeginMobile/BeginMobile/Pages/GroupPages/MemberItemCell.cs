using ImageCircle.Forms.Plugin.Abstractions;
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
                                  BorderColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                                  BorderThickness = Device.OnPlatform(2, 3, 3),
                                  HeightRequest = Device.OnPlatform(40, 80, 80),
                                  WidthRequest = Device.OnPlatform(45, 80, 80),

                                  Aspect = Aspect.AspectFill,
                                  HorizontalOptions = LayoutOptions.Start,
                                  Source = GroupImage
                              };

            var gridListRow = new Grid
                              {
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  }
                              };

            var lblName = new Label
                          {
                              YAlign = TextAlignment.End,
                              Style = App.Styles.ListItemTextStyle,
                              FontAttributes = FontAttributes.Bold,
                              HorizontalOptions = LayoutOptions.Start
                          };

            lblName.SetBinding(Label.TextProperty,  "NameUsername");

            var lblEmail = new Label
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