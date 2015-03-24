using BeginMobile.Pages.GroupPages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Pages.Notifications;
using BeginMobile.Pages.MessagePages;

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
            var topPadding = Device.OnPlatform<double>(20, 0, 0);
            this.Padding = new Thickness(0, topPadding, 0, 0);

            this.Children.Add(new TabContent("New Feed", "Icon-29.png"));
            this.Children.Add(new MessageListPage("Message", "Messages.png"));
            this.Children.Add(new Notification("Notifications", "Icon-29.png"));
            this.Children.Add(new GroupListPage("Groups", "Icon-29.png"));
            this.Children.Add(new TabContent("Opt", "More.png"));
        }
    }
}
