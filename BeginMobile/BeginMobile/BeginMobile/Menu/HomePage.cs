using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages;
using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Menu
{
    public class HomePage : MasterDetailPage
    {
        private readonly LoginUser _loginUser;

        public HomePage(LoginUser loginUser)
        {
            _loginUser = loginUser;
            Title = AppResources.HomePageTitle;

            Icon = Device.OS != TargetPlatform.iOS ? null : new FileImageSource {File = "Icon-57.png"};
            
            Master = new Menu(OnToggleRequest);
            Detail = new AppHome();
        }

        private void OnToggleRequest()
        {
            IsPresented = !IsPresented;
        }
    }
}
