using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class ProfileMessageHeader: ViewCell
    {
        public ProfileMessageHeader()
        {
            Height = 30;

            var labelHeader = new Label
                              {
                                  FontSize = 18,
                                  FontAttributes = FontAttributes.Bold
                              };

            labelHeader.SetBinding(Label.TextProperty, "Key");

            var stackLayoutView = new StackLayout
                           {
                               Spacing = 2,
                               Padding = BeginApplication.Styles.LayoutThickness,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               Orientation = StackOrientation.Horizontal,
                               Children =
                               {
                                   labelHeader
                               }
                           };

            View = stackLayoutView;
        }
    }
}