using BeginMobile.Pages.GroupPages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Pages.Notifications;

namespace BeginMobile.Pages
{
    public class AppHome: TabbedPage
    {
        public AppHome()
        {
            this.LoadChilds();
        }

        public void LoadChilds()
        {
            this.Title = "Home";
            this.Padding = new Thickness(0, 0, 0, 0);

            this.Children.Add(new TabContent("New Feed", "padlock.png"));
            this.Children.Add(new TabContent("Message", "padlock.png"));
            this.Children.Add(new Notification("Notifications", "padlock.png"));
            this.Children.Add(new GroupListPage("Groups", "padlock.png"));
            this.Children.Add(new TabContent("Opt", "menunav.png"));
        }
    }
}
