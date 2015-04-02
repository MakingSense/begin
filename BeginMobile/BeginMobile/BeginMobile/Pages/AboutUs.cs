using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class AboutUs : ContentPage
    {
        public AboutUs()
        {
            Title = "About Us";
            Content = new StackLayout
                      {
                          Spacing = 20,
                          Padding = 50,
                          Children = { new Label { Text = string.Empty } }
                      };
        }
    }
}