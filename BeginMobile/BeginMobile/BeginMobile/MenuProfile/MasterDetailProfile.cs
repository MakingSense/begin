using System;
using Xamarin.Forms;
using BeginMobile.Profile;

namespace BeginMobile.MenuProfile
{
    public class MasterDetailProfile: MasterDetailPage
    {
        public MasterDetailProfile()
        {
            var menuPage = new MenuPage();
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);

            
            Master = menuPage;
            Detail = new NavigationPage(new ProfileMe());
        }

        public void NavigateTo(MenuItem menu)
        {
            var displayPage = (Page)Activator.CreateInstance(menu.TargetType);
            Detail = new NavigationPage(displayPage);
            IsPresented = false;
        }
    }
}
