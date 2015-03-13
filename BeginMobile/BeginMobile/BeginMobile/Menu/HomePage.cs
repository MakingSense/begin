using Xamarin.Forms;

namespace BeginMobile.Menu
{
    public class HomePage : MasterDetailPage
    {
        public HomePage()
        {
            Title = "Home";
            Master = new Menu();
            Detail = new DetailPage();
        }

    }
}
