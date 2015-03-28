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

            this.Children.Add(new WallPage(new Label { Text = "New Feed", Style = App.Styles.StyleNavigationTitle }.Text, "RSS.png"));
            this.Children.Add(new MessageListPage(new Label { Text = "Message", Style = App.Styles.StyleNavigationTitle }.Text, "Messages.png"));
            this.Children.Add(new Notification(new Label { Text = "Notifications", Style = App.Styles.StyleNavigationTitle }.Text, "Chat.png"));
            this.Children.Add(new GroupListPage(new Label { Text = "Groups", Style = App.Styles.StyleNavigationTitle }.Text, "Users_three_2.png"));
            this.Children.Add(new TabContent(new Label { Text = "Opt", Style = App.Styles.StyleNavigationTitle }.Text, "More.png"));
        }
    }
}
