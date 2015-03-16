using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ProfileTitle: Label
    {
        public ProfileTitle(string title)
        {
            this.Text = title;
            this.FontSize = Device.GetNamedSize(NamedSize.Large, typeof (Label));
            this.FontAttributes = FontAttributes.Bold;
            this.HorizontalOptions = LayoutOptions.Center;
        }
    }
}
