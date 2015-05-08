using System;
using Xamarin.Forms;
using BeginMobile.Pages.Profile;
using BeginMobile.Services.DTO;

namespace BeginMobile.MenuProfile
{
    public class MasterDetailProfile: MasterDetailPage
    {
        public MasterDetailProfile()
        {
            var menuPage = new MenuPage();
            menuPage.ListViewMenu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);

            LoginUser user = null;
            
            Master = menuPage;
            Detail = new NavigationPage(new ProfileMe(user));
        }

        public void NavigateTo(MenuItem menu)
        {
            var displayPage = (Page)Activator.CreateInstance(menu.TargetType);
            Detail = new NavigationPage(displayPage);
            IsPresented = false;
        }
    }
}
