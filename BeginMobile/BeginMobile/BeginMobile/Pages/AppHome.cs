using BeginMobile.Pages.GroupPages;
using BeginMobile.Pages.MessagePages;
using BeginMobile.Pages.Notifications;
using BeginMobile.Pages.Wall;
using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class AppHome : TabbedPage
    {
        public AppHome()
        {
            LoadChilds();
        }

        private void LoadChilds()
        {
            Title = "Home";
            var topPadding = Device.OnPlatform<double>(20, 0, 0);
            Padding = new Thickness(0, topPadding, 0, 0);

            Children.Add(new WallPage(
                new Label { Text = "News Feed", Style = App.Styles.StyleNavigationTitle }.Text, "RSS.png"));

            Children.Add(
                new MessageListPage(new Label { Text = "Message", Style = App.Styles.StyleNavigationTitle }.Text,
                    "Messages.png"));

            Children.Add(
                new Notification(new Label { Text = "Notifications", Style = App.Styles.StyleNavigationTitle }.Text,
                    "Chat.png"));

            Children.Add(
                new GroupListPage(new Label { Text = "Groups", Style = App.Styles.StyleNavigationTitle }.Text,
                    "Users_three_2.png"));

            Children.Add(new TabContent(new Label { Text = "Opt", Style = App.Styles.StyleNavigationTitle }.Text,
                "More.png"));
        }
    }
}