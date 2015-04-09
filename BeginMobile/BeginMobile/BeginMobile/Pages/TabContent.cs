using BeginMobile.Interfaces;
using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class TabContent : BaseContentPage
    {
        public TabContent(string title, string icon)
        {
            Title = title;
            Icon = icon;
            Padding = new Thickness(0, 0, 0, 0);
        }
    }
}