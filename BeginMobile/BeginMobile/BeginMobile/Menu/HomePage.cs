using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Menu
{
    public class HomePage : MasterDetailPage
    {
        public HomePage(LoginUser loginUser)
        {
            Title = "Home";
            Master = new Menu(loginUser.User);
            Detail = new DetailPage();
        }

        
    }
}
