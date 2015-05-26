using BeginMobile.Interfaces;
using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class TabContent : BaseContentPage
    {
        public TabContent(string title, string icon)
        {
            //Style = BeginApplication.Styles.PageStyle;
            BackgroundColor = BeginApplication.Styles.ColorWhiteBackground;
            Title = title;
            Icon = icon;
            Padding = new Thickness(0, 0, 0, 0);

			if (Device.OS == TargetPlatform.iOS)
				BackgroundColor = Color.White;

        }
    }
}