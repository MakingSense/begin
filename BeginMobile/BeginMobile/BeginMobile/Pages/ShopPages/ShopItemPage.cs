using Xamarin.Forms;

namespace BeginMobile.Pages.ShopPages
{
    public class ShopItemPage: ContentPage
    {
        public ShopItemPage()
        {
            Title = "Shop detail";

            var browser = new WebView();
            browser.SetBinding(WebView.SourceProperty, "Link");

            Content = browser;

        }
    }
}
