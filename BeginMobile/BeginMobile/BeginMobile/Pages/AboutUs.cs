using BeginMobile.LocalizeResources.Resources;
using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class AboutUs : ContentPage
    {
        public AboutUs()
        {
            Style = BeginApplication.Styles.PageStyle;
            Title = AppResources.AboutUsTitle;
            Content = new StackLayout
                      {
                          Spacing = 20,
                          Padding = 50,
                          Children = { new Label { Text = string.Empty } }
                      };
        }
    }
}