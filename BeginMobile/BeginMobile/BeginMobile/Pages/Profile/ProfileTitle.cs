using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ProfileTitle: Label
    {
        public ProfileTitle(string title)
        {
            Text = title;
            FontSize = Device.GetNamedSize(NamedSize.Large, typeof (Label));
            FontAttributes = FontAttributes.Bold;
            HorizontalOptions = LayoutOptions.Center;
        }
    }
}