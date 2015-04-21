using BeginMobile.LocalizeResources.Resources;
using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class HelpCenter : ContentPage
    {
        public HelpCenter()
        {
            Title = AppResources.HelpCenterTitle;

            Content = new StackLayout
                      {
                          Spacing = 20,
                          Padding = 50,
                          Children = { new Label { Text = string.Empty } }
                      };
        }
    }
}
