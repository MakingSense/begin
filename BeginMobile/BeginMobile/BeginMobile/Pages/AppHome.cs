using BeginMobile.Pages.GroupPages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Pages.Notifications;
using BeginMobile.Pages.MessagePages;
using BeginMobile.Pages.Wall;

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

            this.Children.Add(new WallPage("New Feed", "RSS.png"));
            this.Children.Add(new MessageListPage("Message", "Messages.png"));
            this.Children.Add(new Notification("Notifications", "Chat.png"));
            this.Children.Add(new GroupListPage("Groups", "Users_three_2.png"));
            this.Children.Add(new TabContent("Opt", "More.png"));
        }
    }
}
